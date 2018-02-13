namespace TP.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ToPlay : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserAdministrators",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Password = c.String(),
                        FirstName = c.String(),
                        SecondName = c.String(),
                        MiddleName = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        DateOfRegistration = c.DateTime(nullable: false),
                        DateOfLastVisit = c.DateTime(nullable: false),
                        DateOfLastChange = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserPhotoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        SRC = c.String(),
                        Main = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserRatings",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Rating = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.UserVerifications",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserAdministrators", "Id", "dbo.Users");
            DropForeignKey("dbo.UserVerifications", "Id", "dbo.Users");
            DropForeignKey("dbo.UserRatings", "Id", "dbo.Users");
            DropForeignKey("dbo.UserPhotoes", "UserId", "dbo.Users");
            DropIndex("dbo.UserVerifications", new[] { "Id" });
            DropIndex("dbo.UserRatings", new[] { "Id" });
            DropIndex("dbo.UserPhotoes", new[] { "UserId" });
            DropIndex("dbo.UserAdministrators", new[] { "Id" });
            DropTable("dbo.UserVerifications");
            DropTable("dbo.UserRatings");
            DropTable("dbo.UserPhotoes");
            DropTable("dbo.Users");
            DropTable("dbo.UserAdministrators");
        }
    }
}
