using Blablacar.Core;
using Blablacar.Core.Models;
using Blablacar.Data.PostgreSQL;
using Blablacar.SFTP;
using Blablacar.WEB.Configurations;
using Blablacar.WEB.Hubs;
using Blablacar.WEB.Middlewares;
using Blablacar.Worker;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Internal;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Очищаем логи
//builder.Logging.ClearProviders(); 

// Добавлен NLog
//builder.Host.UseNLog();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenWithAuth();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHangfireWorker();

// Добавлен медиатр
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

// Добавлен SignalR
builder.Services.AddSignalR(); 

// Добавлен HttpClient
builder.Services.AddHttpClient();

// Добавлен middleware для обработки исключений
builder.Services.AddSingleton<ExceptionMiddleware>();

// Добавлен db контекст, настроен identity с юзерами и ролями, добавлен стор с identity таблицами
builder.Services.AddDbContextWithIdentity(configuration.GetConnectionString("DefaultConnection")!);

// Добавлена аутентификация и jwt bearer
builder.Services.AddAuthenticationWithJwtAndExternalServices(configuration);

// Добавлен Sftp Storage
builder.Services.AddSftpStorage(builder.Configuration.GetSection("Sftp").Get<SftpOptions>()!);

// Настройка CORS
builder.Services.AddCors(opt
    => opt.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowed(_ => true)
            .AllowCredentials();
    })
);

// Добавлен слой с db контекстом
builder.Services.AddPostgreSqlLayout();

// Добавлен слой Core
builder.Services.AddCoreLayout(builder.Configuration.GetSection("SearchCity").Get<SearchCityOptions>()!);
builder.Services.AddDistributedMemoryCache(options =>
{
    options.Clock = new SystemClock(); // Устанавливаем часы, используемые для временных меток
    options.ExpirationScanFrequency = TimeSpan.FromHours(2); // Частота сканирования для проверки просроченных записей
});

builder.Services.Configure<MemoryCacheOptions>(options =>
{
    options.ExpirationScanFrequency = TimeSpan.FromHours(2); // Частота сканирования для проверки просроченных записей
    options.SizeLimit = 100000; // Максимальное количество элементов в кэше
});

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Применение миграций
using var scoped = app.Services.CreateScope();
var migrator = scoped.ServiceProvider.GetRequiredService<Migrator>();
await migrator.MigrateAsync();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Добавлено использование middleware для обработки исключений
app.UseMiddleware<ExceptionMiddleware>();

// Использование Hangfire
app.UseHangfireWorker(builder.Configuration.GetSection("Hangfire").Get<HangfireOptions>()!);


// Настройка CORS
app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Использование Hub
app.MapHub<ChatHub>("/chat");

app.Run();