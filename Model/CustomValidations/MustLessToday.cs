using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace WpfStokTakip2011.Model
{
    public class MustLessToday : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            DateTime dt = DateTime.Parse(value.ToString());

            if (dt > DateTime.Now)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
