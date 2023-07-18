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
    /// Interaction logic for frmÖdeme.xaml
    /// </summary>
    public partial class frmÖdeme : Window
    {
        public frmÖdeme()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtToplamTutar.Text = ÖdemeFiş.ToplamTutar.ToString();
            txtÖdemeTutar.Text = ÖdemeFiş.ToplamTutar.ToString();
            txtParaÜstü.Text = "0";
            
        }

        public Fiş ÖdemeFiş { get; set; }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.ÖdemeFiş.ÖdemeTipi = cboÖdemeTipi.Text;
            this.ÖdemeFiş.AlınanTutar = decimal.Parse(txtÖdemeTutar.Text);
            this.DialogResult = true;
        }

        private void txtÖdemeTutar_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtParaÜstü.Text =(decimal.Parse(txtToplamTutar.Text) - decimal.Parse(txtÖdemeTutar.Text)).ToString();
        }

        private void cboÖdemeTipi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboÖdemeTipi.Text == "Car")
                cboCari.Visibility = Visibility.Visible;
            else
                cboCari.Visibility = Visibility.Hidden;
        }

    }
}
