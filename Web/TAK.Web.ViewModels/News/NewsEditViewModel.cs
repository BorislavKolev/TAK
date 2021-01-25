namespace TAK.Web.ViewModels.News
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Http;
    using TAK.Data.Models;
    using TAK.Services.Mapping;

    public class NewsEditViewModel : IMapFrom<NewsPost>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public ICollection<IFormFile> Pictures { get; set; }

        public string LatinTitle { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }

        public string ImageUrl { get; set; }

        public string VideoUrl { get; set; }
    }
}
