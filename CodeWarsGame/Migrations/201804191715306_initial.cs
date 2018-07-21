namespace CodeWarsGame.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        PlayerId = c.Int(nullable: false, identity: true),
                        PlayerName = c.String(nullable: false, maxLength: 450),
                    })
                .PrimaryKey(t => t.PlayerId)
                .Index(t => t.PlayerName, unique: true);
            
            CreateTable(
                "dbo.SolvedTasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TaskId = c.Int(nullable: false),
                        PlayerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Players", t => t.PlayerId, cascadeDelete: true)
                .ForeignKey("dbo.Tasks", t => t.TaskId, cascadeDelete: true)
                .Index(t => t.TaskId)
                .Index(t => t.PlayerId);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TaskName = c.String(),
                        InputParameter = c.String(),
                        OutputParameter = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SolvedTasks", "TaskId", "dbo.Tasks");
            DropForeignKey("dbo.SolvedTasks", "PlayerId", "dbo.Players");
            DropIndex("dbo.SolvedTasks", new[] { "PlayerId" });
            DropIndex("dbo.SolvedTasks", new[] { "TaskId" });
            DropIndex("dbo.Players", new[] { "PlayerName" });
            DropTable("dbo.Tasks");
            DropTable("dbo.SolvedTasks");
            DropTable("dbo.Players");
        }
    }
}
