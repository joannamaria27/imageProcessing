using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Logika interakcji dla klasy RozciągnięcieAiB.xaml
    /// </summary>
    public partial class RozciągnięcieAiB : Window
    {
        public int a;
        public int b;
        public RozciągnięcieAiB( )
        {
            InitializeComponent();
            

        }

        private void Zatwierdz(object sender, RoutedEventArgs e)
        {

            this.DialogResult = true;

            if ((!(int.TryParse(txtAnswer1.Text, out a))) || !(int.TryParse(txtAnswer3.Text, out a)))
            {
                a = 54;
                b = 255;
            }




        }
        private void Anuluj(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}



/*private void Wpisanie(object sender, RoutedEventArgs e)
{
    //Sender.calculateLUT(txtAnswer1.Text, txtAnswer3.Text);
}*/
/*private void btnDialogOk_Click(object sender, RoutedEventArgs e)
{
    this.DialogResult = true;
}
*/

/*
        public string Answer1
        {
            get { return txtAnswer1.Text; }
        }
        public string Answer3
        {
            get { return txtAnswer3.Text; }
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
{
    Regex regex = new Regex("[^0-9]+");
    e.Handled = regex.IsMatch(e.Text);
}
        private void Window_ContentRendered(object sender, EventArgs e)
        {
            txtAnswer1.SelectAll();
            txtAnswer1.Focus();

        }
        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            if (DialogResult == true)
            {
                // Update fonts
                this.txtAnswer1.Text = Answer1;
                this.txtAnswer3.Text = Answer3;
            }

        }*/



