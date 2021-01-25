namespace TAK.Web.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;
    using TAK.Services.Data.Contracts;
    using TAK.Web.ViewModels;
    using TAK.Web.ViewModels.Home;
    using TAK.Web.ViewModels.Locations;
    using TAK.Web.ViewModels.News;

    public class HomeController : BaseController
    {
        private readonly ILocationsService locationsService;
        private readonly INewsService newsService;

        public HomeController(ILocationsService locationsService, INewsService newsService)
        {
            this.locationsService = locationsService;
            this.newsService = newsService;
        }

        public IActionResult Index()
        {
            var viewModel = new LocationsAndNewsInHomePageViewModel();

            var locations = this.locationsService.GetRandomLocations<LocationsListItemViewModel>(4);
            var news = this.newsService.GetLast<NewsListItemViewModel>(4);

            viewModel.Locations = locations;
            viewModel.News = news;

            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }

        public IActionResult HttpError(int statusCode)
        {
            if (statusCode == 404)
            {
                return this.View("404NotFound");

            }
            else
            {
                return this.Error();
            }
        }
    }
}
