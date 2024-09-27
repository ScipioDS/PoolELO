namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alpha0_2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "Player1_name", c => c.String());
            AddColumn("dbo.Games", "Player2_name", c => c.String());
            AddColumn("dbo.PoolPlayers", "Img", c => c.String());
            AddColumn("dbo.PoolPlayers", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PoolPlayers", "Description");
            DropColumn("dbo.PoolPlayers", "Img");
            DropColumn("dbo.Games", "Player2_name");
            DropColumn("dbo.Games", "Player1_name");
        }
    }
}
