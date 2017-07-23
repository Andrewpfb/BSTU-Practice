namespace ITechArt.Blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentId = c.Int(),
                        Text = c.String(nullable: false, maxLength: 1000),
                        CommentOn = c.DateTime(nullable: false),
                        UserImagePath = c.String(nullable: false, maxLength: 500),
                        PostId = c.Int(nullable: false),
                        AuthorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Post", t => t.PostId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.AuthorId)
                .Index(t => t.PostId)
                .Index(t => t.AuthorId);
            
            CreateTable(
                "dbo.Post",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        ShortDescription = c.String(nullable: false, maxLength: 500),
                        Tags = c.String(maxLength: 200),
                        Description = c.String(nullable: false),
                        PostedOn = c.DateTime(nullable: false),
                        ImagePath = c.String(nullable: false, maxLength: 500),
                        AuthorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.AuthorId, cascadeDelete: true)
                .Index(t => t.AuthorId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50),
                        Role = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Role", t => t.Role)
                .Index(t => t.Role);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tag",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 25),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User", "Role", "dbo.Role");
            DropForeignKey("dbo.Post", "AuthorId", "dbo.User");
            DropForeignKey("dbo.Comment", "AuthorId", "dbo.User");
            DropForeignKey("dbo.Comment", "PostId", "dbo.Post");
            DropIndex("dbo.User", new[] { "Role" });
            DropIndex("dbo.Post", new[] { "AuthorId" });
            DropIndex("dbo.Comment", new[] { "AuthorId" });
            DropIndex("dbo.Comment", new[] { "PostId" });
            DropTable("dbo.Tag");
            DropTable("dbo.Role");
            DropTable("dbo.User");
            DropTable("dbo.Post");
            DropTable("dbo.Comment");
        }
    }
}
