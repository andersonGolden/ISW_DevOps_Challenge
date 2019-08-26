namespace ISW_RestAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Deployments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ScheduleTime = c.DateTime(nullable: false),
                        Status = c.String(),
                        IssuesEncountered = c.String(),
                        Description = c.String(),
                        DurationOfDeployment = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Deployments");
        }
    }
}
