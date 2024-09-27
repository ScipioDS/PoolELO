namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Games", "PoolPlayer_Id", "dbo.PoolPlayers");
            DropIndex("dbo.Games", new[] { "PoolPlayer_Id" });
            DropColumn("dbo.Games", "PoolPlayer_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Games", "PoolPlayer_Id", c => c.Int());
            CreateIndex("dbo.Games", "PoolPlayer_Id");
            AddForeignKey("dbo.Games", "PoolPlayer_Id", "dbo.PoolPlayers", "Id");
        }
    }
}
