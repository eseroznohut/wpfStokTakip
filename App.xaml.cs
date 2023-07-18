using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfStokTakip2011
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            EventManager.RegisterClassHandler(typeof(TextBox), UIElement.GotFocusEvent, new RoutedEventHandler(SelectAllText), true);
            EventManager.RegisterClassHandler(typeof(TextBox), UIElement.LostFocusEvent, new RoutedEventHandler(LostFocus), true);


            base.OnStartup(e);
        }
        
        
        // metin kutularında gezerken tümünü seç
        private static void SelectAllText(object sender, RoutedEventArgs e)
        {
            var textBox = e.OriginalSource as TextBox;
            if (textBox != null)
            {
                
                textBox.Background = Brushes.Aqua;
                textBox.SelectAll();
            }
        }

        private static void LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = e.OriginalSource as TextBox;
            if (textBox != null)
                textBox.Background = Brushes.White;
        }
    }


}
