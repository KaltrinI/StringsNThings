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
        public int Quantity { get; set; }

        public string Image { get; set; }
        
    }

    public enum InstrumentType {  String, Brass , Keyboard , Percussion};
}