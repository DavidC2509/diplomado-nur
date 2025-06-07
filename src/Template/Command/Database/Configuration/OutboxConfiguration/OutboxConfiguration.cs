using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Template.Domain.OutboxAggregate;

namespace Template.Command.Database.Configuration.OutboxConfiguration
{
    public class OutboxConfiguration : IEntityTypeConfiguration<OutboxMessage>
    {
        public void Configure(EntityTypeBuilder<OutboxMessage> builder)
        {
            builder.ToTable("OutboxMessage");

            // Configure the Id property to use the database default value for new entities
            builder.Property(e => e.Id)
                   .HasDefaultValueSql("gen_random_uuid()");

        }
    }
}