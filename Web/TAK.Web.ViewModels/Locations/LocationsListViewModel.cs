namespace TAK.Web.ViewModels.Locations
{
    using System.Collections.Generic;

    using TAK.Data.Models;
    using TAK.Services.Mapping;

    public class LocationsListViewModel : IMapFrom<Location>
    {
        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public IEnumerable<LocationsListItemViewModel> Locations { get; set; }
    }
}
