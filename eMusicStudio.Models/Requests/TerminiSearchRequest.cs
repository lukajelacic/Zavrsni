using System;
using System.Collections.Generic;
using System.Text;

namespace eMusicStudio.Model.Requests
{
    public class TerminiSearchRequest
    {
        public DateTime? Datum { get; set; }
        public bool? Aktivan { get; set; }
    }
}
