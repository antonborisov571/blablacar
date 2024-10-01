using Blablacar.Contracts.Enums;
using Blablacar.Core.Common.Converters;
using Blablacar.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blablacar.Data.PostgreSQL.Configurations;

/// <summary>
/// Конфигурация сущности user-а
/// </summary>
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .Property(p => p.Birthday);

        builder
            .Property(p => p.Phone);
        
        builder
            .Property(p => p.FirstName)
            .IsRequired();
        
        builder
            .Property(p => p.LastName)
            .IsRequired();

        builder
            .Property(p => p.IsConfirmed)
            .IsRequired();
        
        builder
            .Property(p => p.PreferencesMusic)
            .HasConversion(new EnumStringConverter<MusicType>())
            .HasDefaultValue(MusicType.MiddleMusic);

        builder
            .Property(p => p.PreferencesSmoking)
            .HasConversion(new EnumStringConverter<SmokingType>())
            .HasDefaultValue(SmokingType.MiddleSmoking);

        builder
            .Property(p => p.PreferencesTalk)
            .HasConversion(new EnumStringConverter<TalkType>())
            .HasDefaultValue(TalkType.MiddleTalk);
        
        builder
            .Property(p => p.PreferencesAnimal)
            .HasConversion(new EnumStringConverter<AnimalType>())
            .HasDefaultValue(AnimalType.MiddleAnimal);

        builder
            .HasMany(x => x.DriverTrips)
            .WithOne(y => y.Driver)
            .HasForeignKey(y => y.DriverId)
            .HasPrincipalKey(y => y.Id)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder
            .HasMany(x => x.PassengerTrips)
            .WithMany(y => y.Passengers);

        builder
            .HasMany(x => x.TripNotifications)
            .WithOne(y => y.User)
            .HasPrincipalKey(y => y.Id)
            .HasForeignKey(x => x.UserId);
    }
}