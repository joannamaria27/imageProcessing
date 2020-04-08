using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
    /// Logika interakcji dla klasy Histogramy.xaml
    /// </summary>
    public partial class Histogramy : Window
    {
        MainWindow Sender;
        public Histogramy(object sender)
        {
            InitializeComponent();
            Sender = (MainWindow)sender;
        }
        internal void Show(Bitmap obrazek)
        {
            image2 = obrazek;
            image = image2;
            this.Show();
        }
        public SeriesCollection series1 { get; set; }
        public SeriesCollection series2 { get; set; }

        ChartValues<double> valR = new ChartValues<double>();
        ChartValues<double> valG = new ChartValues<double>();
        ChartValues<double> valB = new ChartValues<double>();
        ChartValues<double> valU = new ChartValues<double>();

        public Bitmap image2;
        public Bitmap image;

        int[,] h;
        int[,] d;

        public void GenerateValues()
        {
            d = RGBHistogram(image2);

            valR.Clear();
            valG.Clear();
            valB.Clear();
            valU.Clear();
            h = RGBHistogram(image);

            for (int i = 0; i < 256; i++)
            {
                valR.Add(h[0, i]);
                valG.Add(h[1, i]);
                valB.Add(h[2, i]);
                valU.Add((h[0, i] + h[1, i] + h[2, i]) / 3);
            }
            image = image2;
        }
        public int[,] RGBHistogram(Bitmap OriginalImage)
        {
            int[,] histogram = new int[3, 256];

            for (int i = 0; i < 256; i++)
            {
                histogram[0, i] = 0;
                histogram[1, i] = 0;
                histogram[2, i] = 0;
            }

            for (int m = 0; m < OriginalImage.Width; m++)
            {
                for (int n = 0; n < OriginalImage.Height; n++)
                {
                    System.Drawing.Color pixel = OriginalImage.GetPixel(m, n);
                    histogram[0, pixel.R]++;
                    histogram[1, pixel.G]++;
                    histogram[2, pixel.B]++;
                }
            }
            return histogram;
        }



        private int[] LUTRozciagniecie(int a, int b)
        {
            int[] result = new int[256];
            //double aa = ;
            for (int i = 0; i < 256; i++)
            {
                if ((int)(255 * Math.Abs(i - a) / (b - a)) > 255)
                {
                    result[i] = 255;
                }
                else
                    result[i] = (int)(255 * Math.Abs(i - a) / (b - a));

            }
            return result;
            // return result;
        }

        public int a;
        public int b;
        public void Rozciagniecie(object sender, RoutedEventArgs e)
        {

            a = 54;
            b = 255;
            RozciągnięcieAiB okno = new RozciągnięcieAiB();
            if (okno.ShowDialog() == true)
            {
                a = okno.a;
                b = okno.b;
            }

            int[] hR = new int[256];
            int[] hG = new int[256];
            int[] hB = new int[256];
            for (int i = 0; i < 256; i++)
            {
                hR[i] = d[0, i];
                hG[i] = d[1, i];
                hB[i] = d[2, i];
            }


            int[] LUTred = LUTRozciagniecie(a, b);
            int[] LUTgreen = LUTRozciagniecie(a, b);
            int[] LUTblue = LUTRozciagniecie(a, b);

            //Przetworz obraz i oblicz nowy histogram
            for (int i = 0; i < 256; i++)
            {
                h[0, i] = 0;
                h[1, i] = 0;
                h[2, i] = 0;
            }

            Bitmap oldBitmap = (Bitmap)image;
            Bitmap newBitmap = new Bitmap(image.Width, image.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    System.Drawing.Color pixel = oldBitmap.GetPixel(x, y);
                    System.Drawing.Color newPixel = System.Drawing.Color.FromArgb(LUTred[pixel.R], LUTgreen[pixel.G], LUTblue[pixel.B]);
                    newBitmap.SetPixel(x, y, newPixel);
                    h[0, newPixel.R]++;
                    h[1, newPixel.G]++;
                    h[2, newPixel.B]++;
                }
            }
            image = newBitmap;
            //MainWindow mw = new MainWindow();
            Sender.AktualizacjaObrazek(image);
            //aktualicaja histogramow
            GenerateValues();
        }




        private int[] LUTWyrownanie(int[] values, int size)
        {
            double minValue = 0;
            for (int i = 0; i < 256; i++)
            {
                if (values[i] != 0)
                {
                    minValue = values[i];
                    break;
                }
            }

            int[] result = new int[256];
            double sum = 0;
            for (int i = 0; i < 256; i++)
            {
                sum += values[i];
                result[i] = (int)(((sum - minValue) / (size - minValue)) * 255.0);
            }

            return result;
        }
        public void Wyrownanie(object sender, RoutedEventArgs e)
        {
            int[] hR = new int[256];
            int[] hG = new int[256];
            int[] hB = new int[256];
            for (int i = 0; i < 256; i++)
            {
                hR[i] = d[0, i];
                hG[i] = d[1, i];
                hB[i] = d[2, i];
            }
            int[] LUTred = LUTWyrownanie(hR, image.Width * image.Height);
            int[] LUTgreen = LUTWyrownanie(hG, image.Width * image.Height);
            int[] LUTblue = LUTWyrownanie(hB, image.Width * image.Height);

            for (int i = 0; i < 256; i++)
            {
                h[0, i] = 0;
                h[1, i] = 0;
                h[2, i] = 0;
            }

            Bitmap oldBitmap = image;
            Bitmap newBitmap = new Bitmap(image.Width, image.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    System.Drawing.Color pixel = oldBitmap.GetPixel(x, y);
                    System.Drawing.Color newPixel = System.Drawing.Color.FromArgb(LUTred[pixel.R], LUTgreen[pixel.G], LUTblue[pixel.B]);
                    newBitmap.SetPixel(x, y, newPixel);
                    h[0, newPixel.R]++;
                    h[1, newPixel.G]++;
                    h[2, newPixel.B]++;
                }
            }
            image = newBitmap;
            //MainWindow mw = new MainWindow();
            Sender.AktualizacjaObrazek(newBitmap);
            //aktualicaja histogramow
            GenerateValues();

        }


        private int[] LUTRozjasnienie(double a)
        {
            int[] result = new int[256];
            int b;
            //System.Drawing.Color c = System.Drawing.Color.FromArgb(r, g, b);
            for (int i = 0; i < 256; i++)
            {
                //   System.Drawing.Color c = System.Drawing.Color.FromArgb(valuesR[i], valuesG[i], valuesB[i]);
                // double cc = Convert.ToDouble(c);
                b = (int)(Math.Pow(i, a));
                if ((b) > 255)
                {
                    result[i] = 255;
                }
                else if ((b) < 0)
                {
                    result[i] = 0;
                }
                else
                {
                    result[i] = b;
                }
            }
            return result;
        }

        public void Rozjasnienie(object sender, RoutedEventArgs e)
        {

            int[] hR = new int[256];
            int[] hG = new int[256];
            int[] hB = new int[256];
            for (int i = 0; i < 256; i++)
            {
                hR[i] = d[0, i];
                hG[i] = d[1, i];
                hB[i] = d[2, i];
            }

            int[] LUTred = LUTRozjasnienie(1.07);
            int[] LUTgreen = LUTRozjasnienie(1.07);
            int[] LUTblue = LUTRozjasnienie(1.07);

            for (int i = 0; i < 256; i++)
            {
                h[0, i] = 0;
                h[1, i] = 0;
                h[2, i] = 0;
            }

            Bitmap oldBitmap = (Bitmap)image;
            Bitmap newBitmap = new Bitmap(image.Width, image.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    System.Drawing.Color pixel = oldBitmap.GetPixel(x, y);
                    System.Drawing.Color newPixel = System.Drawing.Color.FromArgb(LUTred[pixel.R], LUTgreen[pixel.G], LUTblue[pixel.B]);
                    newBitmap.SetPixel(x, y, newPixel);
                    h[0, newPixel.R]++;
                    h[1, newPixel.G]++;
                    h[2, newPixel.B]++;
                }
            }
            image = newBitmap;
            //MainWindow mw = new MainWindow();
            Sender.AktualizacjaObrazek(image);
            //aktualicaja histogramow
            GenerateValues();
        }
        public void Przyciemnienie(object sender, RoutedEventArgs e)
        {
            int[] hR = new int[256];
            int[] hG = new int[256];
            int[] hB = new int[256];

            for (int i = 0; i < 256; i++)
            {
                hR[i] = d[0, i];
                hG[i] = d[1, i];
                hB[i] = d[2, i];
            }

            int[] LUTred = LUTRozjasnienie(0.96);
            int[] LUTgreen = LUTRozjasnienie(0.96);
            int[] LUTblue = LUTRozjasnienie(0.96);

            for (int i = 0; i < 256; i++)
            {
                h[0, i] = 0;
                h[1, i] = 0;
                h[2, i] = 0;
            }

            Bitmap oldBitmap = (Bitmap)image;
            Bitmap newBitmap = new Bitmap(image.Width, image.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    System.Drawing.Color pixel = oldBitmap.GetPixel(x, y);
                    System.Drawing.Color newPixel = System.Drawing.Color.FromArgb(LUTred[pixel.R], LUTgreen[pixel.G], LUTblue[pixel.B]);
                    newBitmap.SetPixel(x, y, newPixel);
                    h[0, newPixel.R]++;
                    h[1, newPixel.G]++;
                    h[2, newPixel.B]++;
                }
            }
            image = newBitmap;
            //MainWindow mw = new MainWindow();
            Sender.AktualizacjaObrazek(image);
            //aktualicaja histogramow
            GenerateValues();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            series1 = new SeriesCollection
                {
                    new LineSeries
                    {
                        Title = "R",
                       //LineSmoothness = 0,
                        //StrokeThickness = 1,                   
                        //PointGeometry = Geometry.Parse("m 2 2 2 4 4 4 4 2"),
                        //Stroke = new SolidColorBrush(Color.FromRgb(116,191,155)),
                        Stroke = new SolidColorBrush(Colors.Red),
                        //PointForeground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(69,93,107)),
                        Values = valR
                    },
                    new LineSeries
                    {
                        Title = "G",
                        //LineSmoothness = 0,
                        //StrokeThickness = 3,                   
                        //PointGeometry = Geometry.Parse("m 2 2 2 4 4 4 4 2"),
                        //Stroke = new SolidColorBrush(Color.FromRgb(116,191,155)),
                        Stroke = new SolidColorBrush(Colors.Green),
                        //PointForeground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(69,93,107)),
                        Values = valG
                    },
                    new LineSeries
                    {
                        Title = "B",
                       // LineSmoothness = 0,
                        //StrokeThickness = 3,                   
                        //PointGeometry = Geometry.Parse("m 2 2 2 4 4 4 4 2"),
                        //Stroke = new SolidColorBrush(Color.FromRgb(116,191,155)),
                        Stroke = new SolidColorBrush(Colors.Blue),
                        //PointForeground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(69,93,107)),
                        Values = valB
                    }
                };
            series2 = new SeriesCollection
                {
                    new LineSeries
                    {
                        Title = "(R+G+B)/3",
                       // LineSmoothness = 0,
                        //StrokeThickness = 3,                   
                        //PointGeometry = Geometry.Parse("m 2 2 2 4 4 4 4 2"),
                        //Stroke = new SolidColorBrush(Color.FromRgb(116,191,155)),
                        Stroke = new SolidColorBrush(Colors.Purple),
                        //PointForeground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(69,93,107)),
                        Values = valU
                    }
                };

            chart1.DataContext = this;
            chart2.DataContext = this;
            GenerateValues();
        }
    }
}


