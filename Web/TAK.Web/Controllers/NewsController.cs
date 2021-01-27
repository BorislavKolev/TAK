namespace TAK.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using Ganss.XSS;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using NickBuhro.Translit;
    using TAK.Data.CloudinaryHelper;
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

        public IActionResult All(string searchString, string filter, int page = 1)
        {
            this.ViewData["CurrentSearchString"] = searchString;

            var viewModel = new NewsListViewModel();

            var count = this.newsService.GetNewsCount();

            viewModel.PagesCount = (int)Math.Ceiling((double)count / ItemsPerPage);
            var news = this.newsService.GetAll<NewsListItemViewModel>(ItemsPerPage, (page - 1) * ItemsPerPage);

            if (viewModel.PagesCount == 0)
            {
                viewModel.PagesCount = 1;
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                news = news.Where(n => n.Title.ToLower().Contains(searchString.ToLower()) || n.Content.ToLower().Contains(searchString.ToLower()));
            }

            viewModel.News = news;

            viewModel.CurrentPage = page;

            return this.View(viewModel);
        }

        public IActionResult ByName(string name)
        {
            var newsViewModel = this.newsService.GetByName<NewsDetailsViewModel>(name);

            if (newsViewModel == null)
            {
                return this.NotFound();
            }

            var urls = this.newsService.GetPictureUrls(newsViewModel.Id);
            newsViewModel.Urls = urls;

            var sanitizer = new HtmlSanitizer();
            sanitizer.AllowedAttributes.Add("class");
            sanitizer.AllowedAttributes.Add("data-ephox-embed-iri");
            sanitizer.AllowedAttributes.Add("scrolling");
            sanitizer.AllowedAttributes.Add("allowfullscreen");
            sanitizer.AllowedTags.Add("div");
            sanitizer.AllowedTags.Add("iframe");
            sanitizer.AllowedCssProperties.Add("position");
            var sanitizedContent = sanitizer.Sanitize(newsViewModel.Content);

            newsViewModel.SanitizedContent = sanitizedContent;

            return this.View(newsViewModel);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            var viewModel = new NewsCreateInputModel();

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CreateAsync(NewsCreateInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            var imageUrls = await CloudinaryExtension.UploadMultipleAsync(this.cloudinary, input.Pictures);

            string latinTitle = Transliteration.CyrillicToLatin(input.Title, Language.Bulgarian);
            latinTitle = latinTitle.Replace(' ', '-');

            int locationId = await this.newsService.CreateAsync(input.Title, input.Content, user.Id, imageUrls, latinTitle, input.Author);

            return this.RedirectToAction("ByName", new { name = latinTitle });
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id)
        {
            var locationToEdit = await this.newsService.GetViewModelByIdAsync<NewsEditViewModel>(id);

            return this.View(locationToEdit);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(NewsEditViewModel newsPostToEdit)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var imageUrls = new List<string>();

            if (newsPostToEdit.Pictures != null)
            {
                imageUrls = await CloudinaryExtension.UploadMultipleAsync(this.cloudinary, newsPostToEdit.Pictures);
            }

            string latinTitle = Transliteration.CyrillicToLatin(newsPostToEdit.Title, Language.Bulgarian);

            latinTitle = latinTitle.Replace(' ', '-');

            await this.newsService.EditAsync(newsPostToEdit.Title, newsPostToEdit.Content, user.Id, imageUrls, latinTitle, newsPostToEdit.Author, newsPostToEdit.Id);

            return this.RedirectToAction("ByName", new { name = latinTitle });
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Remove(int id)
        {
            var newsPostToDelete = await this.newsService.GetViewModelByIdAsync<NewsDeleteViewModel>(id);

            return this.View(newsPostToDelete);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Remove(NewsDeleteViewModel newsPostToDelete)
        {
            await this.newsService.DeleteByIdAsync(newsPostToDelete.Id);

            return this.RedirectToAction("All");
        }
    }
}
