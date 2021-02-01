namespace TAK.Web.ViewModels.Locations
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    using static TAK.Data.Common.ModelValidations.Location;

    public class LocationsCreateInputModel
    {
        [Required(ErrorMessage = EmptyNameError)]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = EmptyDescriptionError)]
        public string Description { get; set; }

        [Required(ErrorMessage = EmptyPicturesError)]
        public ICollection<IFormFile> Pictures { get; set; }

        public string Adress { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Website { get; set; }

        public string FacebookPage { get; set; }

        public string InstagramPage { get; set; }

        [Required(ErrorMessage = EmptyMapLinkError)]
        public string MapLink { get; set; }

        [Required(ErrorMessage = EmptyPerksError)]
        public string Perks { get; set; }

        [Required(ErrorMessage = EmptyTypeError)]
        public string Type { get; set; }
    }
}
