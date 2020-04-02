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
        public Histogramy()
        {
            InitializeComponent();
        }

        public SeriesCollection series1 { get; set; }
        public SeriesCollection series2 { get; set; }
        ChartValues<double> valR = new ChartValues<double>();
        ChartValues<double> valG = new ChartValues<double>();
        ChartValues<double> valB = new ChartValues<double>();
        ChartValues<double> valU = new ChartValues<double>();

        static Bitmap image;

        void GenerateValues()
        {
            Random rnd = new Random();

            valR.Clear();
            valG.Clear();
            valB.Clear();
            valU.Clear();
            int[,] hR = RGBHistogram(image);
            int[,] hG = RGBHistogram(image);
            int[,] hB = RGBHistogram(image);

            for (int i = 0; i < 256; i++)
            {
                valR.Add(hR[0, i]);
                valG.Add(hG[1, i]);
                valB.Add(hB[2, i]);
                valU.Add((hR[0, i] + hG[1, i] + hB[2, i]) / 3);
            }
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

        public int[] Histogram(Bitmap OriginalImage)
        {
            int[] histogram = new int[256];

            for (int i = 0; i < 256; i++)
            {
                histogram[i] = 0;
            }

            for (int m = 0; m < OriginalImage.Width; m++)
            {
                for (int n = 0; n < OriginalImage.Height; n++)
                {
                    System.Drawing.Color pixel = OriginalImage.GetPixel(m, n);
                    int gs = (int)((pixel.R * 0.3) + (pixel.G * 0.59) + (pixel.B * 0.11));

                    histogram[gs]++;
                }
            }

            return histogram;

        }
        public Bitmap HistoramEqualization(Bitmap OriginalImage)
        {
            Bitmap image = OriginalImage;

            int[] histogram = this.Histogram(image);
            int pixelCount = image.Width * image.Height;

            for (int m = 0; m < image.Width; m++)
            {
                for (int n = 0; n < image.Height; n++)
                {
                    System.Drawing.Color pixel = image.GetPixel(m, n);
                    int gs = (int)((pixel.R * 0.3) + (pixel.G * 0.59) + (pixel.B * 0.11));

                    Double PSum = 0;

                    for (int i = 0; i < gs + 1; i++)
                    {
                        PSum += Convert.ToDouble(histogram[i]) / pixelCount;
                    }

                    int newgs = Convert.ToInt16(Math.Floor(255 * PSum));

                    image.SetPixel(m, n, System.Drawing.Color.FromArgb(255, newgs, newgs, newgs));
                }
            }
            return image;
        }

        internal void Show(Bitmap obrazek)
        {
            image = obrazek;
            this.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            series1 = new SeriesCollection
                {
                    new LineSeries
                    {
                        Title = "R",
                       // LineSmoothness = 0,
                        //StrokeThickness = 3,                   
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
