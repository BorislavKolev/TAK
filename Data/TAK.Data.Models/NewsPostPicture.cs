namespace TAK.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using TAK.Data.Common.Models;

    public class NewsPostPicture : BaseDeletableModel<int>
    {
        [Required]
        public int NewsPostId { get; set; }

        public virtual NewsPost NewsPost { get; set; }

        [Required]
        public string PictureUrl { get; set; }
    }
}
