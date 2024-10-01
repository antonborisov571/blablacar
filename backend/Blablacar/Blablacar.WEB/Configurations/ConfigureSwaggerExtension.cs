using System.Reflection;
using System.Xml.Linq;
using System.Xml.XPath;
using Blablacar.WEB.Configurations.SchemaFilters;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Newtonsoft;

namespace Blablacar.WEB.Configurations;

/// <summary>
/// Конфигурация сваггера
/// </summary>
public static class ConfigureSwaggerExtension
{
    /// <summary>
    /// Добавление SwaggerGen и настройка его Аутентификации и XML-документации
    /// </summary>
    /// <param name="services">Сервисы билдера</param>
    /// <returns>IServiceCollection</returns>
    public static IServiceCollection AddSwaggerGenWithAuth(this IServiceCollection services) => 
        services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Blablacar API",
                Description = "API для сайта Blablacar",
            });
            
            var xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly).ToList();
            xmlFiles.ForEach(xmlFile => opt.IncludeXmlComments(xmlFile));
            xmlFiles.ForEach(xmlFile => opt.SchemaFilter<EnumTypesSchemaFilter>(xmlFile));
            
            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Authorization token"
            });

            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        }).AddSwaggerGenNewtonsoftSupport();
}