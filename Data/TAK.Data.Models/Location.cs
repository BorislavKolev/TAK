namespace TAK.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using TAK.Data.Common.Models;

    public class Location : BaseDeletableModel<int>
    {
        public Location()
        {
            this.LocationPictures = new HashSet<LocationPicture>();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public string Adress { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Website { get; set; }

        public string FacebookPage { get; set; }

        public string InstagramPage { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public ICollection<LocationPicture> LocationPictures { get; set; }
    }
}
