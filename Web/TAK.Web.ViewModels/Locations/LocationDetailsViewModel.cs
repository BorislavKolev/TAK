namespace TAK.Web.ViewModels.Locations
{
    using System.Collections.Generic;

    using TAK.Data.Models;
    using TAK.Services.Mapping;

    public class LocationDetailsViewModel : IMapFrom<Location>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Adress { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Website { get; set; }

        public string FacebookPage { get; set; }

        public string InstagramPage { get; set; }

        public string MapLink { get; set; }

        public ICollection<string> LocationPerks { get; set; }

        public string Type { get; set; }

        public ICollection<string> Urls { get; set; }
    }
}
