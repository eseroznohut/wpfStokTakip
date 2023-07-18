using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;
using WpfStokTakip2011.Libs;
using WpfStokTakip2011.Model;

namespace WpfStokTakip2011
{

    public partial class frmÜrün : WindowViewBase
    {
        public Ürünler Ürün { get; set; }
        public List<ÜrünKategori> Kategoriler { get; set; }
        public List<Birim> Birimler { get; set; }

        
        public frmÜrün()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = Ürün;
            birimTextBox.ItemsSource = Birimler;
            kategoriTextBox.ItemsSource = Kategoriler;
            button1.DataContext = this;

            

        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void barkodNoTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                e.Handled = true;
                KeyEventArgs e1 = new KeyEventArgs(Keyboard.PrimaryDevice, Keyboard.PrimaryDevice.ActiveSource,
                                    0, Key.Tab);
                e1.RoutedEvent = Keyboard.KeyDownEvent;
                InputManager.Current.ProcessInput(e1);

            }
        }


    }
}
