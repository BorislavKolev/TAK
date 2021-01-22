namespace TAK.Web.ViewModels.Locations
{
    using TAK.Data.Models;
    using TAK.Services.Mapping;

    public class LocationDeleteViewModel : IMapFrom<Location>
    {
        public int Id { get; set; }
    }
}
