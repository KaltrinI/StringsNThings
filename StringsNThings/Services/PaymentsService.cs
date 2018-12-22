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

        public async Task ProcessPayment(Instrument i, string userB)
        {
            var transaction = new Transaction
            {
                UserB = userB,
                UserS = i.UserId,
                Amount = i.Price,
                InstrumentId = i.Id
            };

            if (db.Instruments.Find(i).Quantity > 0)
            {
                db.Transactions.Add(transaction);
                db.Instruments.First(ins => ins.Id == i.Id).Quantity--;

                await db.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<CartItem>> ViewCart(string UserId)
        {

            var list = db.Carts.Where(x => x.UserId == UserId).ToArray();

            return list;
        }


        public async Task DiscardCart(Instrument i,string id)
        {
            var cart = db.Carts.Where(x => x.UserId == id && x.instrument == i).FirstOrDefault();

            db.Carts.Remove(cart);
            await db.SaveChangesAsync();
        }


        public async Task Checkout(string UserId)
        {
            var list = db.Carts.Where(x => x.UserId == UserId).ToArray();
            foreach (var item in list)
                await ProcessPayment(item.instrument, UserId);
        }

       
    }
}