namespace PizzeriApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DettagliOrdine",
                c => new
                    {
                        IDdettagliOrdine = c.Int(nullable: false, identity: true),
                        Quantita = c.Int(),
                        IdPizza = c.Int(),
                        IdOrdini = c.Int(),
                        IdUtente = c.Int(),
                    })
                .PrimaryKey(t => t.IDdettagliOrdine)
                .ForeignKey("dbo.Ordini", t => t.IdOrdini)
                .ForeignKey("dbo.Utente", t => t.IdUtente)
                .ForeignKey("dbo.Pizza", t => t.IdPizza)
                .Index(t => t.IdPizza)
                .Index(t => t.IdOrdini)
                .Index(t => t.IdUtente);
            
            CreateTable(
                "dbo.Ordini",
                c => new
                    {
                        IDordini = c.Int(nullable: false, identity: true),
                        Confermato = c.Boolean(),
                        Note = c.String(),
                        Evaso = c.Boolean(),
                        IdUser = c.Int(),
                    })
                .PrimaryKey(t => t.IDordini)
                .ForeignKey("dbo.Utente", t => t.IdUser)
                .Index(t => t.IdUser);
            
            CreateTable(
                "dbo.Utente",
                c => new
                    {
                        IDutente = c.Int(nullable: false, identity: true),
                        Username = c.String(maxLength: 25),
                        Password = c.String(maxLength: 25),
                        Ruolo = c.String(maxLength: 5),
                    })
                .PrimaryKey(t => t.IDutente);
            
            CreateTable(
                "dbo.Pizza",
                c => new
                    {
                        IDpizza = c.Int(nullable: false, identity: true),
                        NomePizza = c.String(maxLength: 25),
                        Ingredienti = c.String(maxLength: 25),
                        PrezzoPizza = c.Decimal(precision: 10, scale: 2),
                        Img = c.String(maxLength: 25),
                    })
                .PrimaryKey(t => t.IDpizza);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DettagliOrdine", "IdPizza", "dbo.Pizza");
            DropForeignKey("dbo.Ordini", "IdUser", "dbo.Utente");
            DropForeignKey("dbo.DettagliOrdine", "IdUtente", "dbo.Utente");
            DropForeignKey("dbo.DettagliOrdine", "IdOrdini", "dbo.Ordini");
            DropIndex("dbo.Ordini", new[] { "IdUser" });
            DropIndex("dbo.DettagliOrdine", new[] { "IdUtente" });
            DropIndex("dbo.DettagliOrdine", new[] { "IdOrdini" });
            DropIndex("dbo.DettagliOrdine", new[] { "IdPizza" });
            DropTable("dbo.Pizza");
            DropTable("dbo.Utente");
            DropTable("dbo.Ordini");
            DropTable("dbo.DettagliOrdine");
        }
    }
}
