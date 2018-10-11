using StringsNThings.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StringsNThings.Services
{
    public interface IInstrumentServices
    {
        List<Instrument> GetAllInstruments();
        void AddInstrument(Instrument instrument);
        Task<Instrument> GetInstrumentDetails(int id, string type);
        Task<List<Instrument>> GetInstrumentsByType(string type);
        void DeleteInstrument(Instrument instrument);
        void ModifyInstrumentInfo(Instrument instrument);
    }
}