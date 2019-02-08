using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StringsNThings.Models
{
    public class CartItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartId { get; set; }
        [ForeignKey("Instrument")]
        public int InstrumentId { get; set; }
        public virtual Instrument Instrument { get; set; }
        public string UserId { get; set; }

        
    }
}