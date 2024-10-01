using Blablacar.Core.Abstractions.Services;
using File = Blablacar.Core.Entities.File;

namespace Blablacar.Core.Abstractions.Repositories;

/// <summary>
/// Репозиторий для файлов
/// </summary>
/// <param name="dbContext">Контекст базы данных</param>
public abstract class AbstractFilesRepository(IDbContext dbContext) 
    : GenericRepository<File, Guid>(dbContext);