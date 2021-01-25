namespace TAK.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using TAK.Data.Common.Models;

    public class NewsPost : BaseDeletableModel<int>
    {
        public NewsPost()
        {
            this.NewsPostPictures = new HashSet<NewsPostPicture>();
        }

        [Required]
        public string Title { get; set; }

        [Required]
        public string LatinTitle { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public string VideoUrl { get; set; }

        public string Author { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public ICollection<NewsPostPicture> NewsPostPictures { get; set; }
    }
}
