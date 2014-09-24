namespace Cqs.Infrastructure.EntityFramework
{
    using System.Data.Entity;

    using Cqs.Domain;
    using Cqs.Infrastructure.EntityFramework.EntityConfigurations;

    public class CqsQueryContext : DbContext
    {
        public CqsQueryContext()
            : base("CqsContext")
        {
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new OrderConfiguration());
            modelBuilder.Configurations.Add(new CustomerConfiguration());
            base.OnModelCreating(modelBuilder);
        }

    }
}
