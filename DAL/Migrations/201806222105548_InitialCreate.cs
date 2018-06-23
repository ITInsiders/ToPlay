namespace TP.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Login = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        FirstName = c.String(nullable: false),
                        SecondName = c.String(nullable: false),
                        MiddleName = c.String(),
                        PhoneNumber = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        DateOfRegistration = c.DateTime(nullable: false),
                        DateOfLastVisit = c.DateTime(nullable: false),
                        DateOfLastChange = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SenderId = c.Long(nullable: false),
                        FromId = c.Long(),
                        Value = c.String(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Comments", t => t.FromId)
                .ForeignKey("dbo.Users", t => t.SenderId, cascadeDelete: true)
                .Index(t => t.SenderId)
                .Index(t => t.FromId);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        URL = c.String(),
                        Main = c.Boolean(nullable: false),
                        DownloadDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SenderId = c.Long(),
                        RecipientId = c.Long(),
                        FromId = c.Long(nullable: false),
                        Value = c.String(nullable: false),
                        DepartureDate = c.DateTime(nullable: false),
                        ReceivingDate = c.DateTime(nullable: false),
                        User_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Messages", t => t.FromId)
                .ForeignKey("dbo.Users", t => t.RecipientId)
                .ForeignKey("dbo.Users", t => t.SenderId)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.SenderId)
                .Index(t => t.RecipientId)
                .Index(t => t.FromId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.IO_Answers",
                c => new
                    {
                        SenderId = c.Long(nullable: false),
                        RecipientId = c.Long(nullable: false),
                        TaskId = c.Long(nullable: false),
                        Gamer_Id = c.Long(),
                    })
                .PrimaryKey(t => new { t.SenderId, t.RecipientId, t.TaskId })
                .ForeignKey("dbo.Gamers", t => t.RecipientId)
                .ForeignKey("dbo.Gamers", t => t.SenderId)
                .ForeignKey("dbo.IO_Tasks", t => t.TaskId, cascadeDelete: true)
                .ForeignKey("dbo.Gamers", t => t.Gamer_Id)
                .Index(t => t.SenderId)
                .Index(t => t.RecipientId)
                .Index(t => t.TaskId)
                .Index(t => t.Gamer_Id);
            
            CreateTable(
                "dbo.IO_Tasks",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        En = c.String(),
                        De = c.String(),
                        Ru = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IO_GameTasks",
                c => new
                    {
                        GameSessionId = c.Long(nullable: false),
                        TaskId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.GameSessionId, t.TaskId })
                .ForeignKey("dbo.IO_GameSessions", t => t.GameSessionId)
                .ForeignKey("dbo.IO_Tasks", t => t.TaskId, cascadeDelete: true)
                .Index(t => t.GameSessionId)
                .Index(t => t.TaskId);
            
            CreateTable(
                "dbo.GameSessions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        GameId = c.Long(nullable: false),
                        Start = c.DateTime(nullable: false),
                        End = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .Index(t => t.GameId);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Href = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GameGamers",
                c => new
                    {
                        GamerId = c.Long(nullable: false),
                        GameSessionId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.GamerId, t.GameSessionId })
                .ForeignKey("dbo.Gamers", t => t.GamerId)
                .ForeignKey("dbo.GameSessions", t => t.GameSessionId, cascadeDelete: true)
                .Index(t => t.GamerId)
                .Index(t => t.GameSessionId);
            
            CreateTable(
                "dbo.IO_Features",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        En = c.String(),
                        De = c.String(),
                        Ru = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IO_FeatureAttributes",
                c => new
                    {
                        FeatureId = c.Long(nullable: false),
                        AttributeId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.FeatureId, t.AttributeId })
                .ForeignKey("dbo.IO_Attributes", t => t.AttributeId, cascadeDelete: true)
                .ForeignKey("dbo.IO_Features", t => t.FeatureId, cascadeDelete: true)
                .Index(t => t.FeatureId)
                .Index(t => t.AttributeId);
            
            CreateTable(
                "dbo.IO_Attributes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        En = c.String(),
                        De = c.String(),
                        Ru = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IO_TaskAttributes",
                c => new
                    {
                        TaskId = c.Long(nullable: false),
                        AttributeId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.TaskId, t.AttributeId })
                .ForeignKey("dbo.IO_Attributes", t => t.AttributeId, cascadeDelete: true)
                .ForeignKey("dbo.IO_Tasks", t => t.TaskId, cascadeDelete: true)
                .Index(t => t.TaskId)
                .Index(t => t.AttributeId);
            
            CreateTable(
                "dbo.SystemNames",
                c => new
                    {
                        En = c.String(nullable: false, maxLength: 128),
                        De = c.String(),
                        Ru = c.String(),
                    })
                .PrimaryKey(t => t.En);
            
            CreateTable(
                "dbo.Administrators",
                c => new
                    {
                        Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.GameComments",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        GameId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Comments", t => t.Id)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.GameId);
            
            CreateTable(
                "dbo.GameImages",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        GameId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Images", t => t.Id)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.GameId);
            
            CreateTable(
                "dbo.Gamers",
                c => new
                    {
                        Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.IO_GameGamers",
                c => new
                    {
                        GamerId = c.Long(nullable: false),
                        GameSessionId = c.Long(nullable: false),
                        IO_GameSession_Id = c.Long(),
                        FeatureId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.GamerId, t.GameSessionId })
                .ForeignKey("dbo.GameGamers", t => new { t.GamerId, t.GameSessionId })
                .ForeignKey("dbo.IO_GameSessions", t => t.IO_GameSession_Id)
                .ForeignKey("dbo.IO_Features", t => t.FeatureId, cascadeDelete: true)
                .Index(t => new { t.GamerId, t.GameSessionId })
                .Index(t => t.IO_GameSession_Id)
                .Index(t => t.FeatureId);
            
            CreateTable(
                "dbo.IO_GameSessions",
                c => new
                    {
                        Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GameSessions", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.MarkComments",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Like = c.Boolean(nullable: false),
                        Repost = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Comments", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.UserComments",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        RecipientID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Comments", t => t.Id)
                .ForeignKey("dbo.Users", t => t.RecipientID, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.RecipientID);
            
            CreateTable(
                "dbo.UserImages",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        UserId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Images", t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserImages", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserImages", "Id", "dbo.Images");
            DropForeignKey("dbo.UserComments", "RecipientID", "dbo.Users");
            DropForeignKey("dbo.UserComments", "Id", "dbo.Comments");
            DropForeignKey("dbo.MarkComments", "Id", "dbo.Comments");
            DropForeignKey("dbo.IO_GameSessions", "Id", "dbo.GameSessions");
            DropForeignKey("dbo.IO_GameGamers", "FeatureId", "dbo.IO_Features");
            DropForeignKey("dbo.IO_GameGamers", "IO_GameSession_Id", "dbo.IO_GameSessions");
            DropForeignKey("dbo.IO_GameGamers", new[] { "GamerId", "GameSessionId" }, "dbo.GameGamers");
            DropForeignKey("dbo.Gamers", "Id", "dbo.Users");
            DropForeignKey("dbo.GameImages", "GameId", "dbo.Games");
            DropForeignKey("dbo.GameImages", "Id", "dbo.Images");
            DropForeignKey("dbo.GameComments", "GameId", "dbo.Games");
            DropForeignKey("dbo.GameComments", "Id", "dbo.Comments");
            DropForeignKey("dbo.Administrators", "Id", "dbo.Users");
            DropForeignKey("dbo.Comments", "SenderId", "dbo.Users");
            DropForeignKey("dbo.IO_Answers", "Gamer_Id", "dbo.Gamers");
            DropForeignKey("dbo.IO_Answers", "TaskId", "dbo.IO_Tasks");
            DropForeignKey("dbo.IO_GameTasks", "TaskId", "dbo.IO_Tasks");
            DropForeignKey("dbo.IO_GameTasks", "GameSessionId", "dbo.IO_GameSessions");
            DropForeignKey("dbo.IO_FeatureAttributes", "FeatureId", "dbo.IO_Features");
            DropForeignKey("dbo.IO_FeatureAttributes", "AttributeId", "dbo.IO_Attributes");
            DropForeignKey("dbo.IO_TaskAttributes", "TaskId", "dbo.IO_Tasks");
            DropForeignKey("dbo.IO_TaskAttributes", "AttributeId", "dbo.IO_Attributes");
            DropForeignKey("dbo.GameGamers", "GameSessionId", "dbo.GameSessions");
            DropForeignKey("dbo.GameSessions", "GameId", "dbo.Games");
            DropForeignKey("dbo.GameGamers", "GamerId", "dbo.Gamers");
            DropForeignKey("dbo.IO_Answers", "SenderId", "dbo.Gamers");
            DropForeignKey("dbo.IO_Answers", "RecipientId", "dbo.Gamers");
            DropForeignKey("dbo.Messages", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Messages", "SenderId", "dbo.Users");
            DropForeignKey("dbo.Messages", "RecipientId", "dbo.Users");
            DropForeignKey("dbo.Messages", "FromId", "dbo.Messages");
            DropForeignKey("dbo.Comments", "FromId", "dbo.Comments");
            DropIndex("dbo.UserImages", new[] { "UserId" });
            DropIndex("dbo.UserImages", new[] { "Id" });
            DropIndex("dbo.UserComments", new[] { "RecipientID" });
            DropIndex("dbo.UserComments", new[] { "Id" });
            DropIndex("dbo.MarkComments", new[] { "Id" });
            DropIndex("dbo.IO_GameSessions", new[] { "Id" });
            DropIndex("dbo.IO_GameGamers", new[] { "FeatureId" });
            DropIndex("dbo.IO_GameGamers", new[] { "IO_GameSession_Id" });
            DropIndex("dbo.IO_GameGamers", new[] { "GamerId", "GameSessionId" });
            DropIndex("dbo.Gamers", new[] { "Id" });
            DropIndex("dbo.GameImages", new[] { "GameId" });
            DropIndex("dbo.GameImages", new[] { "Id" });
            DropIndex("dbo.GameComments", new[] { "GameId" });
            DropIndex("dbo.GameComments", new[] { "Id" });
            DropIndex("dbo.Administrators", new[] { "Id" });
            DropIndex("dbo.IO_TaskAttributes", new[] { "AttributeId" });
            DropIndex("dbo.IO_TaskAttributes", new[] { "TaskId" });
            DropIndex("dbo.IO_FeatureAttributes", new[] { "AttributeId" });
            DropIndex("dbo.IO_FeatureAttributes", new[] { "FeatureId" });
            DropIndex("dbo.GameGamers", new[] { "GameSessionId" });
            DropIndex("dbo.GameGamers", new[] { "GamerId" });
            DropIndex("dbo.GameSessions", new[] { "GameId" });
            DropIndex("dbo.IO_GameTasks", new[] { "TaskId" });
            DropIndex("dbo.IO_GameTasks", new[] { "GameSessionId" });
            DropIndex("dbo.IO_Answers", new[] { "Gamer_Id" });
            DropIndex("dbo.IO_Answers", new[] { "TaskId" });
            DropIndex("dbo.IO_Answers", new[] { "RecipientId" });
            DropIndex("dbo.IO_Answers", new[] { "SenderId" });
            DropIndex("dbo.Messages", new[] { "User_Id" });
            DropIndex("dbo.Messages", new[] { "FromId" });
            DropIndex("dbo.Messages", new[] { "RecipientId" });
            DropIndex("dbo.Messages", new[] { "SenderId" });
            DropIndex("dbo.Comments", new[] { "FromId" });
            DropIndex("dbo.Comments", new[] { "SenderId" });
            DropTable("dbo.UserImages");
            DropTable("dbo.UserComments");
            DropTable("dbo.MarkComments");
            DropTable("dbo.IO_GameSessions");
            DropTable("dbo.IO_GameGamers");
            DropTable("dbo.Gamers");
            DropTable("dbo.GameImages");
            DropTable("dbo.GameComments");
            DropTable("dbo.Administrators");
            DropTable("dbo.SystemNames");
            DropTable("dbo.IO_TaskAttributes");
            DropTable("dbo.IO_Attributes");
            DropTable("dbo.IO_FeatureAttributes");
            DropTable("dbo.IO_Features");
            DropTable("dbo.GameGamers");
            DropTable("dbo.Games");
            DropTable("dbo.GameSessions");
            DropTable("dbo.IO_GameTasks");
            DropTable("dbo.IO_Tasks");
            DropTable("dbo.IO_Answers");
            DropTable("dbo.Messages");
            DropTable("dbo.Images");
            DropTable("dbo.Comments");
            DropTable("dbo.Users");
        }
    }
}
