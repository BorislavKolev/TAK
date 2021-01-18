namespace TAK.Web.ViewModels.News
{
    using System;

    using TAK.Data.Models;
    using TAK.Services.Mapping;

    public class NewsListItemViewModel : IMapFrom<NewsPost>
    {
        public string Title { get; set; }

        public string LatinTitle { get; set; }

        public string ImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public string NewsPostUrl => $"/News/{this.LatinTitle.Replace(' ', '-')}";
    }
}
