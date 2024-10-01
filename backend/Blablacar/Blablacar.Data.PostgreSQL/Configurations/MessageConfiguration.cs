using Blablacar.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blablacar.Data.PostgreSQL.Configurations;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder
            .Property(x => x.Dispatch)
            .IsRequired();

        builder
            .Property(x => x.Text)
            .IsRequired();

        builder
            .HasOne(x => x.Sender)
            .WithMany()
            .HasPrincipalKey(y => y.Id)
            .HasForeignKey(x => x.SenderId);

        builder
            .HasOne(x => x.Receiver)
            .WithMany()
            .HasPrincipalKey(y => y.Id)
            .HasForeignKey(x => x.ReceiverId);
    }
}