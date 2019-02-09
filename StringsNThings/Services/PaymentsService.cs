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


        public async Task AddToCart(int instrumentId, string UserB)
        {
            if (instrumentId != 0 && UserB != null)
            {
                Instrument instrument = db.Instruments.First(x => x.Id == instrumentId);

                var cartItem = new CartItem
                {
                    UserId = UserB,
                    InstrumentId = instrumentId,
                    Instrument = instrument
                };

                db.Carts.Add(cartItem);
                await db.SaveChangesAsync();
            }
        }

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


        public async Task DiscardCartItem(int InstrumentId, string userB)
        {
            var cart = db.Carts.FirstOrDefault(x => x.UserId == userB && x.InstrumentId == InstrumentId);

            db.Carts.Remove(cart);
            await db.SaveChangesAsync();
        }


        public async Task Checkout(string userB)
        {
            var list = db.Carts.Where(x => x.UserId == userB).ToArray();
            foreach (var item in list)
                await ProcessPayment(item.InstrumentId, userB);
        }

        public async Task EmptyCart(string userB)
        {
            db.Carts.RemoveRange(db.Carts.Where(x => x.UserId == userB));
            await db.SaveChangesAsync();
        }
    }
}