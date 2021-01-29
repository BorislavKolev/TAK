namespace TAK.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using TAK.Services.Messaging;
    using TAK.Web.ViewModels.Contacts;

    public class ContactsController : BaseController
    {
        private readonly IEmailSender emailSender;

        public ContactsController(IEmailSender emailSender)
        {
            this.emailSender = emailSender;
        }

        public IActionResult Index(string message)
        {
            var viewModel = new ContactsAlertViewModel();
            viewModel.Message = message;

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(ContactsInputModel inputModel)
        {
            try
            {
                await this.emailSender.SendEmailAsync(inputModel.Email, inputModel.Name, "kolevbv@gmail.com", inputModel.Subject, inputModel.Message);
            }
            catch (ArgumentException ex)
            {
                return this.RedirectToAction("Index", new { message = ex.Message });
            }

            return this.RedirectToAction("Index", new { message = "Вашето съобщение беше изпратено успешно!" });
        }
    }
}