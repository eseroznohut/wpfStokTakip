using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;

namespace WpfStokTakip2011.Libs
{
    public class WindowViewBase:Window,INotifyPropertyChanged
    {
        public WindowViewBase ()
	    {
            this.AddHandler(Validation.ErrorEvent, new RoutedEventHandler(this.OnValidationError));
	    }

        int PageErrorCount;
       
        void OnValidationError(object sender, RoutedEventArgs e)
        {
            ValidationErrorEventArgs args = (ValidationErrorEventArgs)e;
            string HataMesajı=args.Error.ErrorContent.ToString();
            
            if (HataMesajı.Contains("correct format"))
                  args.Error.ErrorContent="Veri dönüştürme hatası";

            if (args.Action == ValidationErrorEventAction.Added) PageErrorCount++;
            if (args.Action == ValidationErrorEventAction.Removed) PageErrorCount--;

            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("UygunMu"));

        }

        public bool UygunMu
        {
            get
            {
                return PageErrorCount == 0;
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
