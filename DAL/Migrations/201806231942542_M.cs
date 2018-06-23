namespace TP.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IO_Answers", "Coins", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.IO_Answers", "Coins");
        }
    }
}
