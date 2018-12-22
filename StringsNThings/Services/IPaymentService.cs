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
        Task ProcessPayment(Instrument i, string userB);
        Task<IEnumerable<CartItem>> ViewCart(string UserId);
        Task Checkout(string UserId);
        Task DiscardCart(Instrument i, string UserId);
    }
}
