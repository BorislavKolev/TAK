namespace TAK.Web.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;
    using TAK.Web.ViewModels;

    public class ContactsController : BaseController
    {
        public ContactsController()
        {
        }

        public IActionResult Index()
        {
            return this.View();
        }
    }
}
