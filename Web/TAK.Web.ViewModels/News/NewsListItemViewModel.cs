namespace TAK.Web.ViewModels.News
{
    using System;
    using System.Collections.Generic;

    using TAK.Data.Models;
    using TAK.Data.Models;
    using TAK.Services.Mapping;

    public class NewsListItemViewModel : IMapFrom<NewsPost>
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string ImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public string NewsPostUrl => $"/News/{this.Title.Replace(' ', '-')}";
    }
}
