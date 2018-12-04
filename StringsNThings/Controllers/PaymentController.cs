using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

        public async Task<ActionResult> ProcessPayment(Instrument i, int? userB)
        {
            if(i == null || userB == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            await paymentService.ProcessPayment(i, userB.Value);
            return View();
        }

        public async Task<ActionResult> ViewCart(int? UserId)
        {
            if(UserId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var list = await paymentService.ViewCart(UserId.Value);
            return View(list);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DiscardCart(Instrument i, int? id)
        {
            await paymentService.DiscardCart(i, id.Value);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Checkout (int? UserId)
        {
            if (UserId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            await paymentService.Checkout(UserId.Value);
            return View();
        }
    }
}