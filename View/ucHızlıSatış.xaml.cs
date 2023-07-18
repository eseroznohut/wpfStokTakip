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
using System.Collections.ObjectModel;
using System.Windows.Media.Animation;
using WpfStokTakip2011.Model;

namespace WpfStokTakip2011.View
{
    /// <summary>
    /// Interaction logic for frmStokHareket.xaml
    /// </summary>
    public partial class ucHızlıSatış : UserControl
    {
        public event Action BeginLoad;
        StokTakip2011Entities dc = new StokTakip2011Entities();
        ObservableCollection<StokHareket> liste;

        public decimal  Toplam { get; set; }

      

        public ucHızlıSatış()
        {
           
            InitializeComponent();
            if (BeginLoad != null)
            {
                BeginLoad();
            }
        }

        Fiş yeniFiş;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            liste = new ObservableCollection<StokHareket>();
            DataGrid1.ItemsSource = liste;
            yeniFiş= new Fiş();
           
           


        }


     
        
        private void txtBarkod_KeyUp(object sender, KeyEventArgs e)
        {


            TextBox t = (sender as TextBox);
            var ürün = dc.Ürünler.Where(p => p.BarkodNo == t.Text).SingleOrDefault();

            if (ürün != null)
            {
               
                StokHareket s = new StokHareket();
                s.GÇ_Tipi = cboİşlemTipi.SelectedIndex == 0 ? "Ç" : "G";
                s.SatışFiyat  = ürün.SatışFiyat;
                s.AlışFiyat = ürün.AlışFiyat;
                s.Miktar = double.Parse(txtAdet.Text);
                s.ÜrünId = ürün.ÜrünId;
                s.Tarih = DateTime.Now;
                s.KullanıcıTc = "Gökmen";
                s.ToplamSatışTutar= ürün.SatışFiyat * (decimal)s.Miktar;
                s.ToplamAlışTutar = ürün.AlışFiyat * (decimal)s.Miktar;

                double adetKontrol=0;
                if (cboİşlemTipi.SelectedIndex == 0)
                {
                    adetKontrol = ürün.KalanMiktar - s.Miktar;

                   
                    if (adetKontrol <0)
                    {
                        string uyarı = string.Format("Depoda Yeterli Ürün Bulunmamaktadır.\r  Depoda Bulunan Miktardan {0} Adet Fazla  Girdiniz.", adetKontrol * (-1));
                        MessageBox.Show(uyarı);
                        return;
                    }
                   
                    ürün.KalanMiktar -= s.Miktar;
                }
                else
                {
                    ürün.KalanMiktar += s.Miktar;
                }
                
              
                liste.Add(s);
                yeniFiş.StokHareket.Add(s);

                dc.Fiş.AddObject(yeniFiş);

                

                var d = liste.Sum(p => p.ToplamSatışTutar);
                txtToplam.Text = d.ToString("N") + " TL";
                
                this.Toplam = d;
                
                txtBarkod.Text = "";
                txtAdet.Text = "1";

                if (adetKontrol == 0)
                {
                    string uyarı = string.Format("Depodaki son ürünü sattınız");
                    MessageBox.Show(uyarı);
                    return;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            frmÖdeme f = new frmÖdeme();
            f.ÖdemeFiş = yeniFiş;

            f.ÖdemeFiş.ToplamTutar = this.Toplam;
            f.ÖdemeFiş.FişTarihi = DateTime.Now;
            
            f.ShowDialog();

            if (f.DialogResult != true)
                return;
            
            dc.SaveChanges();
            yeniFiş = new Fiş();

           
            dc = new StokTakip2011Entities();
            liste = new ObservableCollection<StokHareket>();
            DataGrid1.ItemsSource = liste;
            txtToplam.Text = "";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
            StokHareket s = DataGrid1.SelectedItem as StokHareket;
            if (s!=null)
            {
                liste.Remove(s);
                dc.StokHareket.DeleteObject(s);
                var d = liste.Sum(p => p.ToplamSatışTutar);
                txtToplam.Text = d.ToString("N");
            }
     
        }


        private void DataGrid1_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString(); 
        }

       
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key==Key.F2)
            {
                MessageBox.Show("f2");
                e.Handled = true;
            }
        }

        private void mnuÜrünler_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            yeniFiş = new Fiş();
            dc = new StokTakip2011Entities();
            liste = new ObservableCollection<StokHareket>();
            DataGrid1.ItemsSource = liste;
            txtToplam.Text = "";
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            frmFiyatGör f = new frmFiyatGör();
           
            Window rootWindow = Application.Current.MainWindow  as Window;
            rootWindow.Opacity = 0.4;

            f.ShowDialog();

            rootWindow.Opacity =1;
        }

        private void txtAdet_GotFocus(object sender, RoutedEventArgs e)
        {
           
        }

        private void txtAdet_MouseEnter(object sender, MouseEventArgs e)
        {
            
        }

        private void txtBarkod_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


      
    }
}
