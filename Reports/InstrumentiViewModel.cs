using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reports
{
    public class InstrumentiViewModel
    {
        public string Naziv { get; set; }
        public int BrojRezervacija { get; set; }
        public decimal Zarada { get; set; }
        public static List<InstrumentiViewModel> Get()
        {
            return new List<InstrumentiViewModel> { };
        }
    }
}