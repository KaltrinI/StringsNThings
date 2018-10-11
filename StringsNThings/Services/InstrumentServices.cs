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
        private InstrumentDBContext dBContext;

        public void AddInstrument(Instrument instrument)
        {
            var table = GetInstrumentTable(instrument.Category);
            table.Add(instrument);
            dBContext.SaveChanges();
        }

        
        public void DeleteInstrument(Instrument instrument)
        {
            var table = GetInstrumentTable(instrument.Category);
            table.Remove(instrument);
            dBContext.SaveChanges();
        }

        public List<Instrument> GetAllInstruments()
        {
            var instruments = dBContext.OtherInstruments.ToList();
            instruments.AddRange(dBContext.BrassInstruments.ToList());
            instruments.AddRange(dBContext.WoodwindInstruments.ToList());
            instruments.AddRange(dBContext.KeyboardInstruments.ToList());
            instruments.AddRange(dBContext.PercussionInstruments.ToList());
            instruments.AddRange(dBContext.StringInstruments.ToList());
            return instruments.OrderBy(x => x.Name).ToList();
        }

        public async Task<Instrument> GetInstrumentDetails(int id, string type)
        {
            var table = await GetInstrumentTable(type).ToListAsync();
            var list = new List<Instrument>();
            table.ForEach(x => list.Add((Instrument)x));
            return list.First(a => a.Id == id);
        }

        public async Task<List<Instrument>> GetInstrumentsByType(string type)
        {
            
            var table = await GetInstrumentTable(type).ToListAsync();
            var list = new List<Instrument>();
            table.ForEach(x => list.Add((Instrument)x));
            return list;   
        }

        public void ModifyInstrumentInfo(Instrument instrument)
        {
            throw new NotImplementedException();
        }

        private DbSet GetInstrumentTable(string category)
        {
            switch (category)
            {
                case nameof(BrassInstrument):
                    return dBContext.BrassInstruments;
                case nameof(WoodwindInstrument):
                    return dBContext.WoodwindInstruments;
                case nameof(KeyboardInstrument):
                    return dBContext.KeyboardInstruments;
                case nameof(PercussionInstrument):
                    return dBContext.PercussionInstruments;
                case nameof(StringInstrument):
                    return dBContext.StringInstruments;
                default:
                    return dBContext.OtherInstruments;
            }
        }

    }
}