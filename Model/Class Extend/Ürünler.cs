using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using WpfStokTakip2011.Model.CustomValidations;

namespace WpfStokTakip2011.Model
{
    [MetadataType(typeof(ÜrünlerMetadata))]
    partial class Ürünler
    {

      
        internal class ÜrünlerMetadata
	    {
            
            public string MaxSeviye { get; set; }


            [MustLessToday( ErrorMessage="Girilen tarih bugünden büyük olamaz")]
            public DateTime? FiyatGüncellemeTarihi { get; set; }

            [UniqueField(ErrorMessage="Bu ürün barkodu daha önceden kayıtlı")]
            public object BarkodNo { get; set; }
	    }
        
    }
}
