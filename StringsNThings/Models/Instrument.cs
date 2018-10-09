using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StringsNThings.Models
{
    public class Instrument
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public double Price{ get; set; }

        public virtual string GetInformation() {
            return String.Format("Name: {0}\nCategory: {1}\nPrice: {2}\nDescription: {3}",Name,Category,Price,Description);
        }
        
    }
}