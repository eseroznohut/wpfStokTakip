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
using WpfStokTakip2011.View;
using WpfStokTakip2011.Properties;
using System.Windows.Media.Animation;
using System.Threading;

namespace WpfStokTakip2011
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Width = 820;
            this.Height = 600;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Title = KaynakData.ProgramBaşlık;
            contentİçerik.Header = KaynakData.FirmaAdı;
        }

        private void mnuÜrünSatış_Click(object sender, RoutedEventArgs e)
        {
            İçerikAyarla(typeof(ucHızlıSatış), "Hızlı Satış");
        }

        private void mnuÜrünler_Click(object sender, RoutedEventArgs e)
        {
            İçerikAyarla(typeof(ucÜrünler), "Ürünler");
        }

        Thread t;
        public void İçerikAyarla(Type _uc,string _başlık)
        {
            double sol = this.Left + (this.Width - 200) / 2;
            double yukseklik = this.Top + (this.Height - 50) / 2;

           t = new Thread(delegate()
            {
                frmLoading f = new frmLoading();

                f.Left = sol;
                f.Top = yukseklik;
                f.ShowDialog();

            });


            t.SetApartmentState(ApartmentState.STA);
            t.IsBackground = true;
            t.Start();
            
    
            Storyboard sb = this.FindResource("UserFormAnimasyon") as Storyboard;
            sb.Begin();

            ImageBrush img = this.FindResource("BrushZemin") as ImageBrush;
            img.Opacity = 0.2;

           

            UserControl uc = (UserControl)Activator.CreateInstance(_uc);
            uc.Loaded += new RoutedEventHandler(uc_Loaded);

            contentİçerik.Content = uc;
            contentİçerik.Header = _başlık;
            
           
        }

        void uc_Loaded(object sender, RoutedEventArgs e)
        {
            t.Abort();
        }

        private void mnuMavi_Click(object sender, RoutedEventArgs e)
        {
            ResourceDictionary dic = App.LoadComponent(new Uri(@"AppThema/Mavi/MainWindowResource.xaml", UriKind.Relative)) as ResourceDictionary;
            this.Resources = dic;
        }

        private void mnuYeşil_Click(object sender, RoutedEventArgs e)
        {
            ResourceDictionary dic=App.LoadComponent(new Uri(@"AppThema/Yeşil/MainWindowResource.xaml",UriKind.Relative))as ResourceDictionary;
            this.Resources = dic;
        }

    }
}
