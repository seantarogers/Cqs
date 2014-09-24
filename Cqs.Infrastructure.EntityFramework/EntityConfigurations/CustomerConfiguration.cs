namespace Cqs.Infrastructure.EntityFramework.EntityConfigurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using Cqs.Domain;

    public class CustomerConfiguration : EntityTypeConfiguration<Customer>
    {
        public CustomerConfiguration()
        {
            this.ToTable("Customer");
            this.HasKey(p => p.Id);
            this.Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(p => p.FirstName).IsRequired().HasMaxLength(255);
            this.Property(p => p.LastName).IsRequired().HasMaxLength(255);
            this.Property(p => p.IsActive).IsRequired();
            this.HasMany(p => p.Orders).WithRequired(c => c.Customer).HasForeignKey(c => c.CustomerId).WillCascadeOnDelete();
        }
    }
}