using StringsNThings.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace StringsNThings.Services
{
    public class InstrumentServices : IInstrumentServices
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        public async Task AddInstrument(Instrument instrument)
        {
            db.Instruments.Add(instrument);
            await db.SaveChangesAsync();
        }

        public async Task DeleteInstrument(Instrument instrument)
        {
            db.Instruments.Remove(instrument);
            await db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Instrument>> GetAllInstruments()
        {
            return await db.Instruments.ToListAsync();
        }

        public async Task<Instrument> GetInstrumentById(int id)
        {
            return await db.Instruments.FindAsync(id);
        }

        public async Task<Instrument> GetInstrumentDetails(int id)
        {
           return await db.Instruments.FindAsync(id);
        }

  
        public async Task<IEnumerable<Instrument>> GetInstrumentsByType(string type)
        {
            var instruments= await db.Instruments.ToListAsync();
            return instruments.Where(instrument => instrument.Category == type);
        }

        public async Task ModifyInstrumentInfo(Instrument instrument)
        {
            db.Entry(instrument).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

    }
}