namespace TAK.Web.ViewModels.News
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    using static TAK.Data.Common.ModelValidations.NewsPost;

    public class NewsCreateInputModel
    {
        [Required(ErrorMessage = EmptyTitleError)]
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = EmptyContentError)]
        public string Content { get; set; }

        public string Author { get; set; }

        [Required(ErrorMessage = EmptyPicturesError)]
        public ICollection<IFormFile> Pictures { get; set; }
    }
}
