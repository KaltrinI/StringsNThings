
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using StringsNThings.Models;
using StringsNThings.Services;

namespace StringsNThings.Controllers
{
    public class InstrumentsController : Controller
    {
        private IInstrumentServices instrumentService = new InstrumentServices();
        // GET: Instruments
        public async Task<ActionResult> Index()
        {
            return View(await instrumentService.GetAllInstruments());
        }

        // GET: Instruments/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instrument instrument = await instrumentService.GetInstrumentDetails(id.Value); 
            if (instrument == null)
            {
                return HttpNotFound();
            }
            return View(instrument);
        }

        // GET: Instruments/Create
        [Authorize(Roles ="User,Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Instruments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Category,Description,Price")] Instrument instrument)
        {
            if (ModelState.IsValid)
            {
                await instrumentService.AddInstrument(instrument);
                return RedirectToAction("Index");
            }

            return View(instrument);
        }

        // GET: Instruments/Edit/5
        [Authorize(Roles ="User,Administrator")]
        public async Task<ActionResult> Edit(int? id)
        {
            

            if (id == null || !await AccessCheck())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instrument instrument = await instrumentService.GetInstrumentDetails(id.Value); 
            if (instrument == null)
            {
                return HttpNotFound();
            }
            return View(instrument);
        }

        // POST: Instruments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User,Administrator")]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Category,Description,Price")] Instrument instrument)
        {
           
            if (ModelState.IsValid|| !await AccessCheck())
            {
                await instrumentService.ModifyInstrumentInfo(instrument);
                return RedirectToAction("Index");
            }
            return View(instrument);
        }

        // GET: Instruments/Delete/5
        [Authorize(Roles = "User,Administrator")]
        public async Task<ActionResult> Delete(int? id)
        {
            
            if (id == null || !await AccessCheck())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instrument instrument = await instrumentService.GetInstrumentDetails(id.Value);
            if (instrument == null)
            {
                return HttpNotFound();
            }

            return View(instrument);
        }

        // POST: Instruments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User,Administrator")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Instrument instrument = await instrumentService.GetInstrumentDetails(id);
            if(!await AccessCheck(instrument))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            return RedirectToAction("Index");
        }

        private async Task<bool> AccessCheck(Instrument i=null)
        {
            if (i.UserId != User.Identity.Name)
                return false;
            return true;
        }
    }
}
