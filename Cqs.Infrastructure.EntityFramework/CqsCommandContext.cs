namespace Cqs.Infrastructure.EntityFramework
{
    using System.Data.Entity;

    using Cqs.Domain;
    using Cqs.Infrastructure.EntityFramework.EntityConfigurations;

    public class CqsCommandContext : DbContext
    {
        public CqsCommandContext()
            : base("CqsContext")
        {
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Event> DomainEvents { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new OrderConfiguration());
            modelBuilder.Configurations.Add(new CustomerConfiguration());
            modelBuilder.Configurations.Add(new EventConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}