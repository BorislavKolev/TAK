namespace TAK.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using CloudinaryDotNet;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using TAK.Data.CloudinaryHelper;
    using TAK.Data.Models;
    using TAK.Services.Data.Contracts;
    using TAK.Web.ViewModels.Locations;

    public class LocationsController : BaseController
    {
        private const int ItemsPerPage = 6;

        private readonly ILocationsService locationsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly Cloudinary cloudinary;

        public LocationsController(ILocationsService locationsService, UserManager<ApplicationUser> userManager, Cloudinary cloudinary)
        {
            this.locationsService = locationsService;
            this.userManager = userManager;
            this.cloudinary = cloudinary;
        }

        public IActionResult All(string filter, int page = 1)
        {
            this.ViewData["CurrentFilter"] = filter;

            var viewModel = new LocationsListViewModel();

            var count = this.locationsService.GetLocationsCount();

            viewModel.PagesCount = (int)Math.Ceiling((double)count / ItemsPerPage);
            var locations = this.locationsService.GetAll<LocationsListItemViewModel>(ItemsPerPage, (page - 1) * ItemsPerPage);

            if (!string.IsNullOrEmpty(filter))
            {
                locations = locations.Where(l => l.Type == filter);
            }

            viewModel.Locations = locations;

            if (viewModel.PagesCount == 0)
            {
                viewModel.PagesCount = 1;
            }

            viewModel.CurrentPage = page;

            return this.View(viewModel);
        }

        public IActionResult ByName(string name)
        {
            var locationViewModel = this.locationsService.GetByName<LocationDetailsViewModel>(name);

            if (locationViewModel == null)
            {
                return this.NotFound();
            }

            var urls = this.locationsService.GetPictureUrls(locationViewModel.Id);
            locationViewModel.Urls = urls;
            var perks = this.locationsService.GetPerks(locationViewModel.Id);
            locationViewModel.LocationPerks = perks;

            return this.View(locationViewModel);
        }

        [Authorize]
        public IActionResult Create()
        {
            var viewModel = new LocationsCreateInputModel();

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAsync(LocationsCreateInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            var imageUrls = await CloudinaryExtension.UploadMultipleAsync(this.cloudinary, input.Pictures);

            int locationId = await this.locationsService.CreateAsync(input.Name, input.Description, input.Adress, input.PhoneNumber, input.Email, input.Website, input.FacebookPage, input.InstagramPage, user.Id, input.MapLink, input.Perks, input.Type, imageUrls);

            return this.RedirectToAction("ByName", new { name = input.Name });
        }
    }
}
