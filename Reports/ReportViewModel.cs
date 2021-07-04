using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reports
{
    public class ReportViewModel
    {
        public string ImePrezime { get; set; }
        public string KorisnickoIme { get; set; }
        public int BrojRezervacija { get; set; }
        public decimal Potroseno { get; set; }
        public static List<ReportViewModel> Get()
        {
            return new List<ReportViewModel> { };
        }
    }
}