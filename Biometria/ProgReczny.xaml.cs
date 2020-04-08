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
    /// Logika interakcji dla klasy ProgReczny.xaml
    /// </summary>
    public partial class ProgReczny : Window
    {
        public int a;
        public ProgReczny()
        {
            InitializeComponent();
           
        }

        private void Zatwierdz(object sender, RoutedEventArgs e)
        {

            this.DialogResult = true;

            if(!(int.TryParse(txtAnswer1.Text, out a)))
            {
                a = 120;
            }
            this.Close();

        }
        private void Anuluj(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
