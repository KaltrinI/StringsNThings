using StringsNThings.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StringsNThings.Services
{
    public interface IInstrumentServices
    {
        Task<IEnumerable<Instrument>> GetAllInstruments();
        Task AddInstrument(Instrument instrument);
        Task<Instrument> GetInstrumentDetails(int id);
        Task<IEnumerable<Instrument>> GetInstrumentsByType(string type);
        Task DeleteInstrument(Instrument instrument);
        Task ModifyInstrumentInfo(Instrument instrument);
    }
}