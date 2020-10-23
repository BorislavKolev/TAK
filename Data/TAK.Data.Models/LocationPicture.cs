namespace TAK.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using TAK.Data.Common.Models;

    public class LocationPicture : BaseDeletableModel<int>
    {
        [Required]
        public int LocationId { get; set; }

        public virtual Location Location { get; set; }

        [Required]
        public string PictureUrl { get; set; }
    }
}
