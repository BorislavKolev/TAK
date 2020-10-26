namespace TAK.Web.Controllers
{
    using System;

    using Microsoft.AspNetCore.Mvc;
    using TAK.Services.Data.Contracts;
    using TAK.Web.ViewModels.Locations;

    public class LocationsController : BaseController
    {
        private const int ItemsPerPage = 6;

        private readonly ILocationsService locationsService;

        public LocationsController(ILocationsService locationsService)
        {
            this.locationsService = locationsService;
        }

        public IActionResult All(string name, int page = 1)
        {
            var viewModel = new LocationsListViewModel();

            var count = this.locationsService.GetLocationsCount();

            viewModel.PagesCount = (int)Math.Ceiling((double)count / ItemsPerPage);
            viewModel.Locations = this.locationsService.GetAll<LocationsListItemViewModel>(ItemsPerPage, (page - 1) * ItemsPerPage);

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
    }
}
