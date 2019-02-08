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
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Payment
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult> ProcessPayment(int id, string userB)
        {
            if(id == null || userB == null )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            await paymentService.ProcessPayment(id, userB);
            return RedirectToAction("Index","Instruments");
        }
        [Authorize(Roles = "User,Admin")]

        public async Task<ActionResult> ViewCart(string UserId)
        {
            if(UserId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var list = await paymentService.ViewCart(UserId);
            return View(list);
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User,Administrator")]
        public async Task<ActionResult> DiscardCartItem(Instrument i, string id)
        {
            if(i==null || id==null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            await paymentService.DiscardCartItem(i, id);
            return View();
        }


        [HttpGet]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User,Administrator")]
        public async Task<ActionResult> EmptyCart(string id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            await paymentService.EmptyCart(id);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User,Administrator")]
        public async Task<ActionResult> Checkout (string UserId)
        {
            if (UserId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            await paymentService.Checkout(UserId);
            return View();
        }
        
    }
}