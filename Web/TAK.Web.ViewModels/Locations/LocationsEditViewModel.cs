namespace TAK.Web.ViewModels.Locations
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using TAK.Data.Models;
    using TAK.Services.Mapping;

    public class LocationsEditViewModel : IMapFrom<Location>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public ICollection<IFormFile> Pictures { get; set; }

        public string Adress { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Website { get; set; }

        public string FacebookPage { get; set; }

        public string InstagramPage { get; set; }

        public string MapLink { get; set; }

        public string Perks { get; set; }

        public string Type { get; set; }
    }
}
