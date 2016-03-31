using System.Data.Entity;
using System.Reflection;
using EntityManager.Domain.CodeFirst;

namespace EntityManager.DatabaseContexts
{
    public class EntityManagerDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Subgroup> SubGroups { get; set; }

        public EntityManagerDbContext() : base("DefaultConnection")
        {
            Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Overrides for the convention-based mappings.
            // We're assuming that all our fluent mappings are declared in this assembly.
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetAssembly(typeof(EntityManagerDbContext)));
        }
    }
}
