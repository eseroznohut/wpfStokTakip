using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Data.Objects.DataClasses;

namespace WpfStokTakip2011
{
    public abstract class ValidationBase : EntityObject,IDataErrorInfo
    {
        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get 
            { 
                return ValidateProp(columnName);
            
            }
        }

         private string  ValidateProp(string propertyName)
           {
                
    			string typePath = string.Format("{0}+{1}Metadata", this.GetType().ToString(), this.GetType().Name).Trim();

                        
                        Type tMetaData = Type.GetType(typePath);
    			if (tMetaData != null)
                {
                    AssociatedMetadataTypeTypeDescriptionProvider tdp = new AssociatedMetadataTypeTypeDescriptionProvider(this.GetType(), tMetaData);
                    TypeDescriptor.AddProviderTransparent(tdp, this.GetType());
                }


                string error = string.Empty;
                var value = GetValue(propertyName);
                var results = new List<ValidationResult>(1);
                var result = Validator.TryValidateProperty(
                    value,
                    new ValidationContext(this, null, null)
                    {
                        MemberName = propertyName
                    },
                    results);

                if (!result)
                {
                    var validationResult = results[0];
                    error = validationResult.ErrorMessage;
                }

                return error;
      
            }

        private object GetValue(string propertyName)
        {
            object value;
         
            var propertyDescriptor = TypeDescriptor.GetProperties(GetType()).Find(propertyName, false);
            value = propertyDescriptor.GetValue(this);
  
            return value;
        }
    
    }
}
