using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ITechArt.Blog.Models
{
    [Table("Tag")]
    public partial class Tag
    {
        public int Id { get; set; }

        [Required]
        [StringLength(25)]
        public string Name { get; set; }
    }
}
