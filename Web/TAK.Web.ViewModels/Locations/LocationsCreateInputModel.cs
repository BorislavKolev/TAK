namespace TAK.Web.ViewModels.Locations
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class LocationsCreateInputModel
    {
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
