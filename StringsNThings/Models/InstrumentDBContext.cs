using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StringsNThings.Models
{
    public class InstrumentDBContext : ApplicationDbContext
    {
        public DbSet<Instrument> OtherInstruments { get; set; }
        public DbSet<BrassInstrument> BrassInstruments { get; set; }
        public DbSet<KeyboardInstrument> KeyboardInstruments { get; set; }
        public DbSet<WoodwindInstrument> WoodwindInstruments { get; set; }
        public DbSet<StringInstrument> StringInstruments { get; set; }
        public DbSet<PercussionInstrument> PercussionInstruments { get; set; }

    }
}