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
using WpfStokTakip2011.Model;

namespace WpfStokTakip2011
{
    /// <summary>
    /// Interaction logic for frmFiyatGör.xaml
    /// </summary>
    public partial class frmFiyatGör : Window
    {
        StokTakip2011Entities dc = new StokTakip2011Entities();
        public frmFiyatGör()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            textBox1.Focus();
        }

        private void textBox1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                e.Handled = true;
            }

        }

        private void textBox1_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            e.Handled = true;
           
            var v = dc.Ürünler.Where(p => p.BarkodNo == textBox1.Text).SingleOrDefault();

            if (v != null)
            {
                txtFiyat.Text = v.SatışFiyat.ToString();
                txtÜrünAdı.Text = v.ÜrünAdı;
                textBox1.Focus();
                textBox1.SelectAll();

            }
            else
            {
                txtFiyat.Text = "";
                txtÜrünAdı.Text = "";
            }

            
            
        }
    }
}
