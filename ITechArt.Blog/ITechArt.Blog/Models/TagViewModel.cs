using System.Collections.Generic;


namespace ITechArt.Blog.Models
{
    public class TagViewModel
    {
        public Dictionary<string, int> TagsDict { get; set; }
        public TagViewModel()
        {
            TagsDict = new Dictionary<string, int>();
        }
    }
}