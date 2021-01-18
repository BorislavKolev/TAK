namespace TAK.Web.Controllers
{
    using System;

    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using TAK.Data.Models;
    using TAK.Services.Data.Contracts;
    using TAK.Web.ViewModels.News;

    public class NewsController : BaseController
    {
        private const int ItemsPerPage = 6;

        private readonly INewsService newsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly Cloudinary cloudinary;

        public NewsController(INewsService newsService, UserManager<ApplicationUser> userManager, Cloudinary cloudinary)
        {
            this.newsService = newsService;
            this.userManager = userManager;
            this.cloudinary = cloudinary;
        }

        public IActionResult All(string filter, int page = 1)
        {
            var viewModel = new NewsListViewModel();

            var count = this.newsService.GetNewsCount();

            viewModel.PagesCount = (int)Math.Ceiling((double)count / ItemsPerPage);
            var news = this.newsService.GetAll<NewsListItemViewModel>(ItemsPerPage, (page - 1) * ItemsPerPage);

            if (viewModel.PagesCount == 0)
            {
                viewModel.PagesCount = 1;
            }

            viewModel.News = news;

            viewModel.CurrentPage = page;

            return this.View(viewModel);
        }
    }
}
