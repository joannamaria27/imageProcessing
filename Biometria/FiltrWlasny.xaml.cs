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
    /// Logika interakcji dla klasy FiltrWlasny.xaml
    /// </summary>
    public partial class FiltrWlasny : Window
    {
        public int[,] maska = new int[3, 3];
        public int podzielnik;
        public FiltrWlasny()
        {
            InitializeComponent();
        }
        private void Zatwierdz(object sender, RoutedEventArgs e)
        {

            this.DialogResult = true;

            if ((!(int.TryParse(txtAnswer.Text, out podzielnik))) || (!(int.TryParse(txtAnswer1.Text, out maska[0, 0]))) || (!(int.TryParse(txtAnswer2.Text, out maska[0, 1]))) || (!(int.TryParse(txtAnswer3.Text, out maska[0, 2])))
                     || (!(int.TryParse(txtAnswer4.Text, out maska[1, 0]))) || (!(int.TryParse(txtAnswer5.Text, out maska[1, 1]))) || (!(int.TryParse(txtAnswer6.Text, out maska[1, 2])))
                     || (!(int.TryParse(txtAnswer7.Text, out maska[2, 0]))) || (!(int.TryParse(txtAnswer8.Text, out maska[2, 1]))) || (!(int.TryParse(txtAnswer9.Text, out maska[2, 2]))))
            {
                podzielnik = 1;
                maska[0, 0] = 1;
                maska[0, 1] = 2;
                maska[0, 2] = 1;
                maska[1, 0] = 0;
                maska[1, 1] = 0;
                maska[1, 2] = 0;
                maska[2, 0] = -1;
                maska[2, 1] = -2;
                maska[2, 2] = -1;
            }
            
        }
        private void Anuluj(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
