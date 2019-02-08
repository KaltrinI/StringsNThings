using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StringsNThings.Models
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionId { get; set; }
        public string ClientId { get; set; }
        [ForeignKey("Instrument")]
        public int InstrumendId { get; set; }
        public virtual Instrument Instrument { get; set; }
        public double Amount { get; set; }
    }
}