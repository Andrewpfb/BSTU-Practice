using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace ITechArt.Blog.Models
{
    public class PostViewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Post Title")]
        public string Title { get; set; }

        [Display(Name = "Post Text")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public string Author { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d'th 'MMMM' 'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PostedOn { get; set; }

        public string ImagePath { get; set; }

        //public List<Post> postsList { get; set; }
        //public PostViewModel()
        //{
        //    postsList = new List<Post>();
        //}
        //public PostViewModel(List<Post> postsList)
        //{
        //    this.postsList = postsList;
        //}

        public IEnumerable<Post> Posts { get; set; }
        public PostPageInfoModel PageInfo { get; set; }
    }
}