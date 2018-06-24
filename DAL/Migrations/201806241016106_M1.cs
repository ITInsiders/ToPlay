namespace TP.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M1 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.IO_Answers");
            AddColumn("dbo.IO_Answers", "GameSessionId", c => c.Long(nullable: false));
            AddPrimaryKey("dbo.IO_Answers", new[] { "SenderId", "RecipientId", "TaskId", "GameSessionId" });
            CreateIndex("dbo.IO_Answers", "GameSessionId");
            AddForeignKey("dbo.IO_Answers", "GameSessionId", "dbo.IO_GameSessions", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IO_Answers", "GameSessionId", "dbo.IO_GameSessions");
            DropIndex("dbo.IO_Answers", new[] { "GameSessionId" });
            DropPrimaryKey("dbo.IO_Answers");
            DropColumn("dbo.IO_Answers", "GameSessionId");
            AddPrimaryKey("dbo.IO_Answers", new[] { "SenderId", "RecipientId", "TaskId" });
        }
    }
}
