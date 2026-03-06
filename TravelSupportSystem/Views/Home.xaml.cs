using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;

namespace TravelSupportSystem.Views
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        public Home()
        {
            InitializeComponent();
        }

        private void BtnPlanner_Click(object sender, RoutedEventArgs e)
        {
            new TravelPlanner().Show();
            this.Close();
        }

        private void BtnSettings_Click(object sender, RoutedEventArgs e)
        {
            new Settings().Show();
            this.Close();
        }

        private void BtnMap_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://www.google.com/maps/search/du+lich+da+nang",
                UseShellExecute = true
            });
        }

        private void BtnTransport_Click(object sender, RoutedEventArgs e)
        {
            new TransportWindow().Show();
        }

        private void BtnAccommodation_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Chức năng này hiện không khả dụng", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            new DangNhap().Show();
            this.Close();
        }
    }
}
