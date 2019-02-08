
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using StringsNThings.Models;
using StringsNThings.Services;
using System.Collections.Generic;

namespace StringsNThings.Controllers
{
    public class InstrumentsController : Controller
    {
        private IInstrumentServices instrumentService = new InstrumentServices();
        private ApplicationDbContext db = new ApplicationDbContext();
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
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Instruments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Name,Category,Description,Price,Quantity,Image")] Instrument instrument)
        {
            if (ModelState.IsValid)
            {
                await instrumentService.AddInstrument(instrument);
                return RedirectToAction("Index");
            }

            return View(instrument);
        }

        // GET: Instruments/Edit/5
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult> Edit(int? id)
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

        // POST: Instruments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Category,Description,Price,Quantity,Image")] Instrument instrument)
        {
           
            if (ModelState.IsValid)
            {
                await instrumentService.ModifyInstrumentInfo(instrument);
                return RedirectToAction("Index");
            }
            return View(instrument);
        }

        // GET: Instruments/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int? id)
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

        public async Task<ActionResult> GetInstrumentsByType(string type)
        {
            if(type == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            return View(await instrumentService.GetInstrumentsByType(type));
        }
      

        // POST: Instruments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Instrument instrument = db.Instruments.Find(id);
            db.Instruments.Remove(instrument);
            return RedirectToAction("Index");
        }

       
    }
}
