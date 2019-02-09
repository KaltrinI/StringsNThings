using StringsNThings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringsNThings.Services
{
    interface IPaymentService
    {
        Task AddToCart(int i, string userB);
        Task ProcessPayment(int i, string userB);
        Task<IEnumerable<CartItem>> ViewCart(string UserId);
        Task Checkout(string userB);
        Task DiscardCartItem(int InstrumentId, string userB);
        Task EmptyCart(string userB);
    }
}
