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
    /// Logika interakcji dla klasy Niblack.xaml
    /// </summary>
    public partial class Niblack : Window
    {
        public double k;
        public int otoczenie;
        public Niblack()
        {
            InitializeComponent();
        }
        private void Zatwierdz(object sender, RoutedEventArgs e)
        {

            this.DialogResult = true;

            if ((!(double.TryParse(txtAnswer1.Text, out k))) || !(int.TryParse(txtAnswer3.Text, out otoczenie)))
            {
                k = -0.5;
                otoczenie = 5;
            }




        }
        private void Anuluj(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
