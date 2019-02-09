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

        //GET : AddToCart
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult> AddToCart(int id, string userB)
        {
            if (id == 0 || userB == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            await paymentService.AddToCart(id, userB);
            return Redirect(this.Request.UrlReferrer.AbsolutePath);

        }

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
        
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult> DiscardCartItem(int InstrumentId, string userB)
        {
            if(InstrumentId == 0 || userB == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            await paymentService.DiscardCartItem(InstrumentId, userB);
            return RedirectToAction("Index", "Instruments");
        }


        [HttpGet]
        
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult> EmptyCart(string userB)
        {
            if (userB == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            await paymentService.EmptyCart(userB);
            return RedirectToAction("Index", "Instruments");
        }

        [HttpGet]
        
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult> Checkout(string userB)
        {
            if (userB == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            await paymentService.Checkout(userB);
            return RedirectToAction("Index", "Instruments");
        }
        
    }
}