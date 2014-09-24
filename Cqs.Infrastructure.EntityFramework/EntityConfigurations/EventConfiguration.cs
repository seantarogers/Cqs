namespace Cqs.Infrastructure.EntityFramework.EntityConfigurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using Cqs.Domain;

    public class EventConfiguration : EntityTypeConfiguration<Event>
    {
        public EventConfiguration()
        {
            this.ToTable("Event");
            this.HasKey(p => p.Id);
            this.Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(255);
            this.Property(p => p.EntityId)
                .IsRequired();
            this.Property(p => p.DateCreated)
                .IsRequired();

        }
    }
}