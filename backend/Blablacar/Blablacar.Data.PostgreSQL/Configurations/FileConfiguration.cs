using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using File = Blablacar.Core.Entities.File;

namespace Blablacar.Data.PostgreSQL.Configurations;

/// <inheritdoc />
public class FileConfiguration : IEntityTypeConfiguration<File>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<File> builder)
    {
        builder
            .Property(p => p.FileName);

        builder
            .Property(p => p.ContentType);

        builder
            .Property(p => p.Address)
            .IsRequired();

        builder
            .Property(p => p.Size)
            .IsRequired();
    }
}