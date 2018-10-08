using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StringsNThings.Models
{
    public abstract class Instrument
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public double Price{ get; set; }

        public string GetInformation() {
            return String.Format("Name: {0}\nCategory: {1}\nPrice: {2}\nDescription: {3}",Name,Category,Price,Description);
        }
        
    }
}