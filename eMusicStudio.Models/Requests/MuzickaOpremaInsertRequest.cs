using System;
using System.Collections.Generic;
using System.Text;

namespace eMusicStudio.Model.Requests
{
    public class MuzickaOpremaInsertRequest
    {
        public string Naziv { get; set; }
        public int VrstaId { get; set; }
        public int NaStanju { get; set; }
        public int Cijena { get; set; }

    }
}
