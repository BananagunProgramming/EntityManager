using System.Data.Entity;
using System.Reflection;
using EntityManager.Models;

namespace EntityManager.DatabaseContexts
{
    public class EntityManagerDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }

        public EntityManagerDbContext() : base("DefaultConnection") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Overrides for the convention-based mappings.
            // We're assuming that all our fluent mappings are declared in this assembly.
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetAssembly(typeof(EntityManagerDbContext)));
        }
    }
}
