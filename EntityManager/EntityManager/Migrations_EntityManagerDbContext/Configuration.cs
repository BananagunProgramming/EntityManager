using System;
using EntityManager.Domain.CodeFirst;

namespace EntityManager.Migrations_EntityManagerDbContext
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<DatabaseContexts.EntityManagerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations_EntityManagerDbContext";
        }

        protected override void Seed(DatabaseContexts.EntityManagerDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            context.Clients.AddOrUpdate(c => c.Name,
                new Client
                {
                    ClientId = Guid.NewGuid(),
                    Name = "American Pool Enterprises, Inc",
                    EntityCode = "APE",
                    YearIncorporated = 1969,
                    TaxId = "SS-1234343-55",
                    Phone = "888-888-8888",
                    Fax = "777-777-7777",
                    Email = "debra@ape.com",
                    Website = "http://www.ape.com",
                    Schedule = "always late",
                    YearEndDate = "2/5",
                    FiscalYearEndDate = "2/5",
                    Managed = "No",
                    CreatedDate = DateTime.UtcNow
                },
                new Client
                {
                    ClientId = Guid.NewGuid(),
                    Name = "Common Menu Noodle, Inc",
                    EntityCode = "CMN",
                    YearIncorporated = 1970,
                    TaxId = "SS-1238943-66",
                    Phone = "888-888-8888",
                    Fax = "777-777-7777",
                    Email = "debra@cmn.com",
                    Website = "http://www.ape.com",
                    Schedule = "M",
                    YearEndDate = "2/5",
                    FiscalYearEndDate = "2/5",
                    Managed = "Yes",
                    CreatedDate = DateTime.UtcNow
                });
        }
    }
}
