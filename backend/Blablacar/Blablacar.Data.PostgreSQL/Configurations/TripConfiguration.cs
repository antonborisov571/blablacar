using Blablacar.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blablacar.Data.PostgreSQL.Configurations;

/// <summary>
/// Конфигурация для поездок
/// </summary>
public class TripConfiguration : IEntityTypeConfiguration<Trip>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Trip> builder)
    {
        builder
            .Property(x => x.Where)
            .IsRequired();

        builder
            .Property(x => x.WhereFrom)
            .IsRequired();

        builder
            .Property(x => x.CountPassengers)
            .IsRequired();

        builder
            .Property(x => x.Price)
            .IsRequired();

        builder
            .Property(x => x.DateTimeTrip)
            .IsRequired();

        builder
            .HasMany(x => x.Passengers)
            .WithMany(y => y.PassengerTrips);

        builder
            .HasOne(x => x.Driver)
            .WithMany(y => y.DriverTrips)
            .HasForeignKey(x => x.DriverId)
            .HasPrincipalKey(y => y.Id);
    }
}