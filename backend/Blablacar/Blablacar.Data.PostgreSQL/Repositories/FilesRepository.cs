using Blablacar.Core.Abstractions.Repositories;
using Blablacar.Core.Abstractions.Services;

namespace Blablacar.Data.PostgreSQL.Repositories;

/// <inheritdoc />
public class FilesRepository(IDbContext dbContext) : AbstractFilesRepository(dbContext);