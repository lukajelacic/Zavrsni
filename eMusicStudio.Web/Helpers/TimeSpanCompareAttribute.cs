using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eMusicStudio.Web.Helpers
{
    public class TimeSpanCompareAttribute: System.ComponentModel.DataAnnotations.ValidationAttribute
    {
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public TimeSpanCompareAttribute()
        {

        }
        public override bool IsValid(object value)
        {

            return Start > End ;
        }
    }
}
