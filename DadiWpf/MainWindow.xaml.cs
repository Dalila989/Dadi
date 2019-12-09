using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DadiWpf
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random random = new Random();


        public MainWindow()
        {
            InitializeComponent();
            btn_lancia.Focus();


        }

        private void btn_lancia_Click(object sender, RoutedEventArgs e)
        {
            Tiro();

        }

        private void Tiro()
        {
            var t1 = Task.Factory.StartNew(() =>
            {
                return Lancia();
            });


            var t2 = Task.Factory.StartNew(() =>
            {
                return Lancia();
            });



            var t3 = Task.Factory.StartNew(() =>
            {
                Task.WaitAll(t1, t2);
                int num3 = t1.Result + t2.Result;
                Dispatcher.Invoke(() =>

                img1.Source = new BitmapImage(new Uri($"Images/dado{t1.Result}.jpg", UriKind.Relative)));

                Dispatcher.Invoke(() =>
                 img2.Source = new BitmapImage(new Uri($"Images/dado{t2.Result}.jpg", UriKind.Relative)));

           
                Dispatcher.Invoke(() =>
                txt_totale.Text = Convert.ToString(num3));
                return num3;
            });
        }

        private int Lancia()
        {

            return random.Next(1, 7);
        }

        private void btn_lancia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.Tiro();
            }
        }
    }
}
