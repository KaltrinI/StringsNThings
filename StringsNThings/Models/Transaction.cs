using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StringsNThings.Models
{
    public class Transaction
    {
        public int UserS { get; set; }
        public int UserB { get; set; }
        public int InstrumentId { get; set; }
        public double Amount { get; set; }
    }
}