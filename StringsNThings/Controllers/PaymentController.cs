using System.Web.Mvc;
using System.Threading.Tasks;
using StringsNThings.Models;
using StringsNThings.Services;
using System.Net;

namespace StringsNThings.Controllers
{
    public class PaymentController : Controller
    {
        private IPaymentService paymentService = new PaymentsService();
        
        // GET: Payment
        [Authorize(Roles = "User,Administrator")]
        public async Task<ActionResult> ProcessPayment(Instrument i, string userB)
        {
            if(i == null || userB == null || !await AccessCheck())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            await paymentService.ProcessPayment(i, userB);
            return View();
        }
        [Authorize(Roles = "User,Administrator")]

        public async Task<ActionResult> ViewCart(string UserId)
        {
            if(UserId == null || !await AccessCheck())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var list = await paymentService.ViewCart(UserId);
            return View(list);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User,Administrator")]
        public async Task<ActionResult> DiscardCart(Instrument i, string id)
        {
            if(!await AccessCheck())
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            await paymentService.DiscardCart(i, id);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User,Administrator")]
        public async Task<ActionResult> Checkout (string UserId)
        {
            if (UserId == null || !await AccessCheck())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            await paymentService.Checkout(UserId);
            return View();
        }

        private async Task<bool> AccessCheck(Instrument i = null)
        {
            if (i.UserId != User.Identity.Name)
                return false;
            return true;
        }
    }
}