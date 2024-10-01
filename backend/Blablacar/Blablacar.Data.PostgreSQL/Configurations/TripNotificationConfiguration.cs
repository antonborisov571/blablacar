using Blablacar.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blablacar.Data.PostgreSQL.Configurations;


public class TripNotificationConfiguration : IEntityTypeConfiguration<TripNotification>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<TripNotification> builder)
    {
        builder.Property(x => x.Where)
            .IsRequired();

        builder.Property(x => x.WhereFrom)
            .IsRequired();

        builder.Property(x => x.DateTimeTrip)
            .IsRequired();
        
        builder
            .HasOne(x => x.User)
            .WithMany(y => y.TripNotifications)
            .HasPrincipalKey(y => y.Id)
            .HasForeignKey(x => x.UserId);
    }
}