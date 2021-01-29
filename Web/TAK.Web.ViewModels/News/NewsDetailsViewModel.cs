namespace TAK.Web.ViewModels.News
{
    using System;
    using System.Collections.Generic;

    using Ganss.XSS;
    using TAK.Data.Models;
    using TAK.Services.Mapping;

    public class NewsDetailsViewModel : IMapFrom<NewsPost>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string LatinTitle { get; set; }

        public string SanitizedContent { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }

        public DateTime CreatedOn { get; set; }

        public ICollection<string> Urls { get; set; }
    }
}
