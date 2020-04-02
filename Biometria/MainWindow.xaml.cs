using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Biometria
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            obrazek_2.Source = obrazek.Source;
        }
        private void ZaladujZPliku(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpg;*.bmp;*.gif;*.tif;*.tiff;*.jpeg;)|*.png;*.jpg;*.bmp;*.gif;*.tif;*.tiff;*.jpeg; | All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    Uri fileUri = new Uri(openFileDialog.FileName);
                    obrazek.Source = new BitmapImage(fileUri);
                    obrazek_2.Source = obrazek.Source;
                }
                catch (NotSupportedException)
                {
                    MessageBox.Show("Zły format pliku!", "Wczytywanie z pliku", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
        private void ZapiszBMP(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "ImageBMP";
            saveFileDialog.Filter = "BMP (*.bmp)|*.bmp";
            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    var encoder = new BmpBitmapEncoder();
                    BitmapImage obr = new BitmapImage();

                    encoder.Frames.Add(BitmapFrame.Create((BitmapSource)obrazek.Source));
                    using (var stream = saveFileDialog.OpenFile())
                    {
                        encoder.Save(stream);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Błąd przy zapisie!", "Zapis do pliku", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
        private void ZapiszJPG(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "ImageJPG";
            saveFileDialog.Filter = "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg";
            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    var encoder = new JpegBitmapEncoder();
                    BitmapImage obr = new BitmapImage();

                    encoder.Frames.Add(BitmapFrame.Create((BitmapSource)obrazek.Source));
                    using (var stream = saveFileDialog.OpenFile())
                    {
                        encoder.Save(stream);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Błąd przy zapisie!", "Zapis do pliku", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
        private void ZapiszTIFF(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "ImageTIFF";
            saveFileDialog.Filter = "TIFF (*.tiff)|*.tiff";
            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {

                    var encoder = new TiffBitmapEncoder();
                    BitmapImage obr = new BitmapImage();

                    encoder.Frames.Add(BitmapFrame.Create((BitmapSource)obrazek.Source));
                    using (var stream = saveFileDialog.OpenFile())
                    {
                        encoder.Save(stream);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Błąd przy zapisie!", "Zapis do pliku", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
        private void ZapiszGIF(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "ImageGIF";
            saveFileDialog.Filter = "GIF (*.gif)|*.gif";
            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {

                    var encoder = new GifBitmapEncoder();
                    BitmapImage obr = new BitmapImage();

                    encoder.Frames.Add(BitmapFrame.Create((BitmapSource)obrazek.Source));
                    using (var stream = saveFileDialog.OpenFile())
                    {
                        encoder.Save(stream);
                    }

                }
                catch (Exception eeee)
                {
                    MessageBox.Show(eeee.Message);
                    MessageBox.Show("Błąd przy zapisie!", "Zapis do pliku", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
        private void ZapiszPNG(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "ImagePNG";
            saveFileDialog.Filter = "PNG (*.png)|*.png";
            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    var encoder = new PngBitmapEncoder();
                    BitmapImage obr = new BitmapImage();

                    encoder.Frames.Add(BitmapFrame.Create((BitmapSource)obrazek.Source));
                    using (var stream = saveFileDialog.OpenFile())
                    {
                        encoder.Save(stream);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Błąd przy zapisie!", "Zapis do pliku", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void Powieksz(object sender, RoutedEventArgs e)
        {
            Powiekszenie powiekszenie = new Powiekszenie();
            powiekszenie.Show(obrazek);
        }
        private void Powieksz2(object sender, RoutedEventArgs e)
        {
            Powiekszenie powiekszenie = new Powiekszenie();
            powiekszenie.Show(obrazek_2);
        }

        Color kolor;
        int _x;
        int _y;
        private void WartoscPixeli(object sender, MouseEventArgs e)
        {
            ImageSource imageSource = obrazek.Source;
            BitmapSource bitmapImage = (BitmapSource)imageSource;

            int x = (int)(e.GetPosition(obrazek).X * bitmapImage.PixelWidth / obrazek.ActualWidth);
            int y = (int)(e.GetPosition(obrazek).Y * bitmapImage.PixelHeight / obrazek.ActualHeight);

            Pixel_X.Text = "X: " + (e.GetPosition(obrazek).X * bitmapImage.PixelWidth / obrazek.ActualWidth).ToString();
            Pixel_Y.Text = "Y: " + (e.GetPosition(obrazek).Y * bitmapImage.PixelHeight / obrazek.ActualHeight).ToString();

            Color c;
            if (bitmapImage.Format == PixelFormats.Indexed4)
            {
                byte[] pixels = new byte[1];
                int stride = (bitmapImage.PixelWidth * bitmapImage.Format.BitsPerPixel + 3) / 4;
                bitmapImage.CopyPixels(new Int32Rect(x, y, 1, 1), pixels, stride, 0);
                c = bitmapImage.Palette.Colors[pixels[0] >> 4];
                kwadrat.Fill = new SolidColorBrush(c);
                RGB.Text = "RGB: 4" + c + " (" + c.R + ", " + c.G + ", " + c.B + ")";
                kolor = c;
                _x = x;
                _y = y;

            }
            else if (bitmapImage.Format == PixelFormats.Indexed8)
            {
                byte[] pixels = new byte[1];
                int stride = (bitmapImage.PixelWidth * bitmapImage.Format.BitsPerPixel + 7) / 8;
                bitmapImage.CopyPixels(new Int32Rect(x, y, 1, 1), pixels, stride, 0);
                c = bitmapImage.Palette.Colors[pixels[0]];
                kwadrat.Fill = new SolidColorBrush(c);
                RGB.Text = "RGB: 8" + c + " (" + c.R + ", " + c.G + ", " + c.B + ")";
                kolor = c;
                _x = x;
                _y = y;
            }
            else
            {
                byte[] pixels = new byte[4];
                int stride = (bitmapImage.PixelWidth * bitmapImage.Format.BitsPerPixel + 7) / 8;
                bitmapImage.CopyPixels(new Int32Rect(x, y, 1, 1), pixels, stride, 0);
                c = Color.FromArgb(pixels[3], pixels[2], pixels[1], pixels[0]);
                kwadrat.Fill = new SolidColorBrush(c);
                RGB.Text = "RGB: e" + c + " (" + c.R + ", " + c.G + ", " + c.B + ")";
                kolor = c;
                _x = x;
                _y = y;
            }
        }

        private void ZmianaKoloru(object sender, RoutedEventArgs e)
        {
            RGB kolory = new RGB(this);
            kolory.Show(kolor, _x, _y);
        }

        public void Zmiana(Color k, int x, int y)
        {
            BitmapSource source = obrazek.Source as BitmapSource;
            WriteableBitmap target = new WriteableBitmap(source);
            //int stride = source.PixelWidth * (source.Format.BitsPerPixel + 7) / 8;

            byte[] ColorData = { k.A, k.R, k.G, k.B };
            //if (source.Format == PixelFormats.Indexed8)
            //{ }
            //else
                target.WritePixels(new Int32Rect(x, y, 1, 1), ColorData, 4, 0);
            obrazek.Source = target;
        }

        private System.Drawing.Bitmap BitmapImage2DBitmap(BitmapImage bitmapImage)
        {
            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(outStream);
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(outStream);

                return new System.Drawing.Bitmap(bitmap);
            }
        }

        public System.Drawing.Bitmap b;
        private void HistogramyWyswietl(object sender, RoutedEventArgs e)
        {
            
                
                BitmapImage source = obrazek.Source as BitmapImage;
                System.Drawing.Bitmap b = BitmapImage2DBitmap(source);
                
                Histogramy histogramy = new Histogramy();
                histogramy.Show(b);
            
            
     
        }
    }
}


