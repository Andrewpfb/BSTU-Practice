using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ITechArt.Blog.Models
{
    [Table("Comment")]
    public partial class Comment
    {
        public int Id { get; set; }

        public int? ParentId { get; set; }

        [Required]
        [StringLength(1000)]
        public string Text { get; set; }

        public DateTime CommentOn { get; set; }

        [Required]
        [StringLength(500)]
        public string UserImagePath { get; set; }

        public int PostId { get; set; }

        public int AuthorId { get; set; }

        public int? Seed { get; set; }

        public virtual Post Post { get; set; }

        public virtual User User { get; set; }
    }
}
