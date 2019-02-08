using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using StringsNThings.Models;

namespace StringsNThings.Services
{
    public class PaymentsService : IPaymentService
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public async Task ProcessPayment(int instrumentId, string userB)
        {

            var instrument = db.Instruments.First(x => x.Id == instrumentId);

            if (instrument.Quantity > 0)
            {
                var transaction = new Transaction
                {
                    ClientId = userB,
                    Amount = instrument.Price,
                    Instrument = instrument
                };

                db.Transactions.Add(transaction);
                db.Carts.Remove(db.Carts.First(x => x.UserId == userB && x.InstrumentId == instrumentId));
                await db.SaveChangesAsync();
            }
            
        }
        public async Task<IEnumerable<CartItem>> ViewCart(string UserId)
        {
            var list = db.Carts.Where(x => x.UserId == UserId).ToArray();
            return list;
        }


        public async Task DiscardCartItem(Instrument i,string id)
        {
            var cart = db.Carts.FirstOrDefault(x => x.UserId == id && x.InstrumentId == i.Id);

            db.Carts.Remove(cart);
            await db.SaveChangesAsync();
        }


        public async Task Checkout(string UserId)
        {
            var list = db.Carts.Where(x => x.UserId == UserId).ToArray();
            foreach (var item in list)
                await ProcessPayment(item.InstrumentId, UserId);
        }

        public async Task EmptyCart(string id)
        {
            db.Carts.RemoveRange(db.Carts.Where(x => x.UserId == id));
        }
    }
}