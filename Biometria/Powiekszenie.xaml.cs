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
    /// Logika interakcji dla klasy Powiekszenie.xaml
    /// </summary>
    public partial class Powiekszenie : Window
    {
        public Powiekszenie()
        {
            InitializeComponent();
        }
        internal void Show(Image obrazek)
        {
                obrazek_powiekszenie.Source = obrazek.Source;
                this.Show();
           

        }
        private void Zamknij(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
