using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace TravelSupportSystem.Views
{
    /// <summary>
    /// Interaction logic for TransportWindow.xaml
    /// </summary>
    public partial class TransportWindow : Window
    {
        public TransportWindow()
        {
            InitializeComponent();
        }

        private void OpenLink(string url)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }

        private void BtnPlane_Click(object sender, RoutedEventArgs e)
        {
            OpenLink("https://www.vietnamairlines.com/vi-vn/lotusmiles-offers?utm_source=NVO&utm_medium=GGL-Search&utm_campaign=vna_xuyensuot&utm_term=ffp-vn-26&utm_content=VI&gad_source=1&gad_campaignid=22707067610&gbraid=0AAAAA_YdzAi_vHnHj9fa5qZZFCXzrcqxO&gclid=EAIaIQobChMI4-XaxurCkgMVkuwWBR15Yz2qEAAYASAAEgKBrvD_BwE");
        }

        private void BtnBus_Click(object sender, RoutedEventArgs e)
        {
            OpenLink("https://futabus.vn/");
        }

        private void BtnTrain_Click(object sender, RoutedEventArgs e)
        {
            OpenLink("https://dsvn.vn/#/");
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            new Home().Show();
            this.Close();
        }
    }
}
