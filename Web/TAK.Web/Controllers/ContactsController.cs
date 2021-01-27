namespace TAK.Web.Controllers
{
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

        public IActionResult Index()
        {
            return this.View();
        }


        [HttpPost]
        public async Task<IActionResult> SendEmail(ContactsInputModel inputModel)
        {
            await this.emailSender.SendEmailAsync(inputModel.Email, inputModel.Name, "kolevbv@gmail.com", inputModel.Subject, inputModel.Content);

            return this.Ok();
        }
    }
}