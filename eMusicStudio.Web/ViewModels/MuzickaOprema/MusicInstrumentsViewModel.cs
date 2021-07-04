using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eMusicStudio.Web.ViewModels.MuzickaOprema
{
    public class MusicInstrumentsViewModel
    {
        public List<Model.MuzickaOprema> muzickaOprema { get; set; }
        public List<SelectListItem> Vrste { get; set; }
        public int? VrstaId { get; set; }
        public bool isAdmin { get; set; }


    }
}
