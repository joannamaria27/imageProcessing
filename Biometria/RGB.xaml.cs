using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Biometria
{
    /// <summary>
    /// Logika interakcji dla klasy RGB.xaml
    /// </summary>
    public partial class RGB : Window
    {
        MainWindow Sender;
        public RGB(object sender)
        {
            InitializeComponent();
            Sender = (MainWindow)sender;
        }
        private void WyborKoloru(object sender, RoutedEventArgs e)
        {
            pickerColor.SelectedColor = pickerColor.SelectedColor;
            Color kol = (Color)pickerColor.SelectedColor;
            
            Sender.Zmiana(kol,_x,_y);
            this.Close();

        }
        int _y;
        int _x;
        internal void Show(Color kolor, int x, int y)
        {
            pickerColor.SelectedColor = kolor;
            _x = x;
            _y = y;
            this.Show();

        }
    }
}
