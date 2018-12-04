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
        Task ProcessPayment(Instrument i, int userB);
        Task<IEnumerable<CartItem>> ViewCart(int UserId);
        Task Checkout(int UserId);
        Task DiscardCart(Instrument i, int UserId);
    }
}
