using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace ITechArt.Blog.Models
{
    public class CommentViewModel
    {
        public int CommentId { get; set; }
        public int ParentId { get; set; }
        public string ParentAuthor { get; set; }
        public string ImagePath { get; set; }
        public string Username { get; set; }
        public int NumberOfElapsedDays { get; set; }
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }
        public int NestedLvl { get; set; }
        public List<CommentViewModel> CommentsViewList { get; set; }
        public CommentViewModel()
        {
            CommentsViewList = new List<CommentViewModel>();
        }
    }
}