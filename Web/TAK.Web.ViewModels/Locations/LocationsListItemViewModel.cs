namespace TAK.Web.ViewModels.Locations
{
    using System.Collections.Generic;

    using TAK.Data.Models;
    using TAK.Data.Models;
    using TAK.Services.Mapping;

    public class LocationsListItemViewModel : IMapFrom<Location>
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string Type { get; set; }

        public string LocationUrl => $"/Locations/{this.Name.Replace(' ', '-')}";
    }
}
