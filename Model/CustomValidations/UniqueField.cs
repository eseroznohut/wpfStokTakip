using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace WpfStokTakip2011.Model.CustomValidations
{
    class UniqueField : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            string barkodNo=(string)value;

           

            StokTakip2011Entities dc = new StokTakip2011Entities();
            var v = dc.Ürünler.Where(c => c.BarkodNo == barkodNo).SingleOrDefault();

            if (v == null)
            {
                return true;
            }
            else
                if (v.Güncelleyen != null)
                {
                    return true;
                }
                else
                    return false;



        }

    }
}
