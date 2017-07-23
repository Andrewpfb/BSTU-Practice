namespace ITechArt.Blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BlogMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comment", "Seed", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comment", "Seed");
        }
    }
}
