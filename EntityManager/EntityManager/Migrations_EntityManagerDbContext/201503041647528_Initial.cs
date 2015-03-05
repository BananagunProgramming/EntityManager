namespace EntityManager.Migrations_EntityManagerDbContext
{
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ClientId = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        EntityCode = c.String(nullable: false),
                        YearIncorporated = c.Int(nullable: false),
                        TaxId = c.String(),
                        Phone = c.String(),
                        Fax = c.String(),
                        Email = c.String(),
                        Website = c.String(),
                        Schedule = c.String(),
                        YearEndDate = c.String(),
                        FiscalYearEndDate = c.String(),
                        Managed = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ClientId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Clients");
        }
    }
}
