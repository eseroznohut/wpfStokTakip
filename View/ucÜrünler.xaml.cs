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
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Data.Objects.DataClasses;
using System.ComponentModel;
using WpfStokTakip2011.Model;
using System.Windows.Threading;
using System.Threading;

namespace WpfStokTakip2011.View
{

    public partial class ucÜrünler : UserControl
    {
        StokTakip2011Entities dc = new StokTakip2011Entities();
        ListCollectionView view1;
  

        public ucÜrünler()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            birimColum.ItemsSource = dc.Birim;
            kategoriColumn.ItemsSource = dc.ÜrünKategori;

            view1 = (ListCollectionView)CollectionViewSource.GetDefaultView(dc.Ürünler.ToList());

            DataGrid1.ItemsSource = view1;
  
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dc.SaveChanges();
        }

       
        private void DataGrid1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Kayıtİşlemleri(false);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           
            Kayıtİşlemleri(true);

        }

        private void Kayıtİşlemleri(bool isNew)
        {
            string Kullanıcı = "Gökmen Yılmaz";

            Ürünler v = isNew ? (Ürünler)view1.AddNew() : (Ürünler)view1.CurrentItem;

            DataGrid1.ScrollIntoView(v);

            frmÜrün f = new frmÜrün();
            f.Kategoriler = dc.ÜrünKategori.ToList();
            f.Birimler = dc.Birim.ToList();

            f.Ürün = v;
            f.ShowDialog();
            
            this.Opacity = 0.4;
            f.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            if (f.DialogResult == true)
            {
                if (isNew) 
                {
                    v.Güncelleyen = Kullanıcı;
                    view1.CommitNew(); 

                    dc.AddToÜrünler(v);
                }
                else 
                {
                    v.Güncelleyen = Kullanıcı;
                    view1.CommitEdit();
                }

                dc.SaveChanges();
               
            }
            else
            {
                if (isNew) view1.CancelNew();else EntityOperations.SingleEntityCancel(v, dc);
            }
            this.Opacity = 1;
        }

        bool AynıBarkodVarMı(string BarkodNo)
        {
            var x=dc.Ürünler.Where(p => p.BarkodNo == BarkodNo).SingleOrDefault();
            if (x != null) return true; else return false;
        }
        private void txtAra_TextChanged(object sender, TextChangedEventArgs e)
        {
            view1.Filter = p => ((Ürünler)p).ÜrünAdı.ToUpper().Contains(txtAra.Text);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Ürünler x = (Ürünler)view1.CurrentItem;
            
            dc.Ürünler.DeleteObject(x);
            view1.Remove(x);

            dc.SaveChanges();

            view1.MoveCurrentToPrevious();
           
        }

        List<Ürünler> l = new List<Ürünler>();
        DispatcherTimer _time;
        
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            _time = new DispatcherTimer();
            _time.Interval = new TimeSpan(0, 0, 0, 0, 100);
            _time.Tick += new EventHandler(_time_Tick);
            _time.Start();


            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);

            DataGrid1.Opacity = .5;

            bw.RunWorkerAsync();
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DataGrid1.ItemsSource = l;
            DataGrid1.Opacity = 1;
            _time.Stop();
            lblYükle.Content = "";
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(1000);
            l.AddRange(dc.Ürünler.ToList());
        }

        int i = 0;
        void _time_Tick(object sender, EventArgs e)
        {
            i++;
            if (Math.Pow((-1), i) > 0)
            {
                lblYükle.Content = "Yükleniyor";
            }
            else
            {
                lblYükle.Content = "";
            }

        }
    }
}
