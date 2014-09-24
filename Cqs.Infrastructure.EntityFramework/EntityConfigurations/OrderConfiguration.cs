namespace Cqs.Infrastructure.EntityFramework.EntityConfigurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using Cqs.Domain;

    public class OrderConfiguration : EntityTypeConfiguration<Order>
    {
        public OrderConfiguration()
        {
            this.ToTable("Order");
            this.HasKey(p => p.Id);
            this.Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(p => p.DispatchedOn);
            this.Property(p => p.PlacedOn)
                .IsRequired();
            this.Property(p => p.CustomerId)
                .IsRequired();
        }
    }
}