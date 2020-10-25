using TAK.Data.Models;
using TAK.Services.Mapping;

namespace TAK.Web.ViewModels.Locations
{
    public class LocationsListItemViewModel : IMapFrom<Location>
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string LocationUrl => $"/Locations/{this.Name.Replace(' ', '-')}";
    }
}
