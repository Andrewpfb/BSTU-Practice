using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ITechArt.Blog.Models
{
    [Table("Post")]
    public partial class Post
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Post()
        {
            Comment = new HashSet<Comment>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        [StringLength(500)]
        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }

        [StringLength(200)]
        public string Tags { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d'th 'MMMM' 'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PostedOn { get; set; }

        [Required]
        [StringLength(500)]
        public string ImagePath { get; set; }

        public int AuthorId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comment { get; set; }

        public virtual User User { get; set; }
    }
}
