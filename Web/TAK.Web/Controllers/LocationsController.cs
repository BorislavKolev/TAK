namespace TAK.Web.Controllers
{
    using System;

    using System.Linq;
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

        public IActionResult All(string filter, int page = 1)
        {
            ViewData["CurrentFilter"] = filter;

            var viewModel = new LocationsListViewModel();

            var count = this.locationsService.GetLocationsCount();

            viewModel.PagesCount = (int)Math.Ceiling((double)count / ItemsPerPage);
            var locations = this.locationsService.GetAll<LocationsListItemViewModel>(ItemsPerPage, (page - 1) * ItemsPerPage);

            if (!String.IsNullOrEmpty(filter))
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
    }
}
