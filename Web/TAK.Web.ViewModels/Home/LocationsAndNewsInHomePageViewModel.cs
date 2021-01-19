namespace TAK.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using TAK.Web.ViewModels.Locations;
    using TAK.Web.ViewModels.News;

    public class LocationsAndNewsInHomePageViewModel
    {
        public IEnumerable<LocationsListItemViewModel> Locations { get; set; }

        public IEnumerable<NewsListItemViewModel> News { get; set; }
    }
}
