using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data.Objects.DataClasses;
using System.Data.Objects;

namespace WpfStokTakip2011
{

    public class EntityOperations
    {
        public static  void  SingleEntityCancel<TEntity>(TEntity entity,ObjectContext dc)
        {
            if (entity == null) return;
  
            var entry = dc.ObjectStateManager.GetObjectStateEntry(((IEntityWithKey)entity).EntityKey);

            for (int i = 0; i < entry.OriginalValues.FieldCount; i++)
            {
                entry.CurrentValues.SetValue(i, entry.OriginalValues[i]);
            }
            entry.AcceptChanges();
           
        }
    
    }

}
