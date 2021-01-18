namespace TAK.Web.ViewModels.News
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class NewsCreateInputModel
    {
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        public ICollection<IFormFile> Pictures { get; set; }

    }
}
