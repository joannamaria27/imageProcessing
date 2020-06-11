
using Accord.Imaging.Filters;
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

        #region Ładowanie_i_zapis
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

        #endregion

        #region Powiekszenie
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

        #endregion

        #region Zmiana_pixeli

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
                RGB.Text = "RGB: " + c + " (" + c.R + ", " + c.G + ", " + c.B + ")";
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
                RGB.Text = "RGB: " + c + " (" + c.R + ", " + c.G + ", " + c.B + ")";
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
                RGB.Text = "RGB: " + c + " (" + c.R + ", " + c.G + ", " + c.B + ")";
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

            byte[] ColorData = { k.A, k.R, k.G, k.B };

            target.WritePixels(new Int32Rect(x, y, 1, 1), ColorData, 4, 0);
            obrazek.Source = target;
        }

        #endregion

        #region ZamianaBitmap
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
        public BitmapImage ConvertBitmapImage(System.Drawing.Bitmap bitmap)
        {
            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();

            return image;
        }

        #endregion

        #region Histogramy

        public System.Drawing.Bitmap b;
        private void HistogramyWyswietl(object sender, RoutedEventArgs e)
        {
            try
            {
                BitmapImage source = obrazek_2.Source as BitmapImage;
                System.Drawing.Bitmap b = BitmapImage2DBitmap(source);

                Histogramy histogramy = new Histogramy(this);
                histogramy.Show(b);
            }
            catch (Exception)
            {
                MessageBox.Show("Brak obrazka!", "Histogramy", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        public void AktualizacjaObrazek(System.Drawing.Bitmap image)
        {
            Histogramy histogramy = new Histogramy(this);
            obrazek.Source = ConvertBitmapImage(image);
        }
        #endregion

        #region Binaryzacja
        private void Szarosc(System.Drawing.Bitmap gmp)
        {
            BitmapImage source = obrazek_2.Source as BitmapImage;
            System.Drawing.Bitmap bmp = BitmapImage2DBitmap(source);
            System.Drawing.Color p;
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    p = bmp.GetPixel(x, y);

                    int a = p.A;
                    int r = p.R;
                    int g = p.G;
                    int b = p.B;
                    int avg = (r + g + b) / 3;

                    bmp.SetPixel(x, y, System.Drawing.Color.FromArgb(a, avg, avg, avg));
                }
            }
            obrazek_2.Source = ConvertBitmapImage(bmp);
            //obrazek.Source = ConvertBitmapImage(bmp);
        }

        private void SkalaSzarosci(object sender, RoutedEventArgs e)
        {
            BitmapImage source = obrazek_2.Source as BitmapImage;
            System.Drawing.Bitmap b = BitmapImage2DBitmap(source);
            Szarosc(b);
        }

        private void BinazyzacjaReczna(object sender, RoutedEventArgs e)
        {

            int prog = 120;
            ProgReczny okno = new ProgReczny();
            if (okno.ShowDialog() == true)
                prog = okno.a;

            BitmapImage source = obrazek_2.Source as BitmapImage;
            System.Drawing.Bitmap b = BitmapImage2DBitmap(source);
            Szarosc(b);
            if (b != null)
            {
                System.Drawing.Color curColor;
                int kolor;

                for (int i = 0; i < b.Width; i++)
                {
                    for (int j = 0; j < b.Height; j++)
                    {
                        curColor = b.GetPixel(i, j);
                        kolor = curColor.R;

                        if (kolor > prog)
                            kolor = 255;
                        else
                            kolor = 0;
                        b.SetPixel(i, j, System.Drawing.Color.FromArgb(kolor, kolor, kolor));
                    }
                }
                obrazek.Source = ConvertBitmapImage(b);
            }
        }
        private void BinaryzacjaLokalna(object sender, RoutedEventArgs e)
        {

            BitmapImage source = obrazek_2.Source as BitmapImage;
            System.Drawing.Bitmap b = BitmapImage2DBitmap(source);
            Szarosc(b);
            if (b != null)
            {
                System.Drawing.Color curColor;
                int kolor;
                double k = -0.5;
                int otoczenie = 5;
                Niblack okno = new Niblack();
                if (okno.ShowDialog() == true)
                {
                    k = okno.k;
                    otoczenie = okno.otoczenie;
                }
                int[,] prog = new int[b.Width, b.Height];

                prog = NiblackProg(otoczenie, k, b);
                for (int i = 0; i < b.Width; i++)
                {
                    for (int j = 0; j < b.Height; j++)
                    {
                        curColor = b.GetPixel(i, j);
                        kolor = curColor.R;

                        if (kolor > prog[i, j])
                            kolor = 255;
                        else
                            kolor = 0;

                        b.SetPixel(i, j, System.Drawing.Color.FromArgb(kolor, kolor, kolor));
                    }
                }
                obrazek.Source = ConvertBitmapImage(b);
            }
        }

        private int[,] NiblackProg(int wielkoscot, double k, System.Drawing.Bitmap b)
        {
            int l = 0;
            int otoczenie = wielkoscot / 2;
            int[,] prog = new int[b.Width, b.Height];
            double[] liczby = new double[wielkoscot * wielkoscot];
            for (int i = 0; i < b.Width; i++)
            {
                for (int j = 0; j < b.Height; j++)
                {
                    double suma = 0;
                    double z = 0;
                    l = 0;
                    for (int g = 0; g < wielkoscot * wielkoscot; g++)
                    {
                        liczby[g] = 0;

                    }
                    for (int aa = i - otoczenie; aa < i + otoczenie; aa++)
                    {
                        for (int ee = j - otoczenie; ee < j + otoczenie; ee++)
                        {
                            if ((aa < 0) || (ee < 0)) continue;
                            if ((aa >= b.Width) || (ee >= b.Height)) continue;
                            System.Drawing.Color pixel = b.GetPixel(aa, ee);
                            suma += pixel.R;
                            liczby[l] = pixel.R;
                            l++;
                        }
                    }
                    double sredna = suma / (l);
                    for (int oo = 0; oo < l; oo++)
                    {
                        z += (liczby[oo] - sredna) * (liczby[oo] - sredna);
                    }
                    double odchStd = (Math.Sqrt((z / (l))));

                    prog[i, j] = (int)(sredna + k * odchStd);
                }
            }
            return prog;
        }
        private void BinaryzacjaAutomatyczna(object sender, RoutedEventArgs e)
        {

            BitmapImage source = obrazek_2.Source as BitmapImage;
            System.Drawing.Bitmap b = BitmapImage2DBitmap(source);
            Szarosc(b);

            if (b != null)
            {
                System.Drawing.Color curColor;
                int kolor = 0;
                int prog;
                prog = ProgOtsu(b);


                for (int i = 0; i < b.Width; i++)
                {
                    for (int j = 0; j < b.Height; j++)
                    {

                        curColor = b.GetPixel(i, j);
                        kolor = curColor.R;

                        if (kolor > prog)
                        {
                            kolor = 255;
                        }
                        else
                            kolor = 0;
                        b.SetPixel(i, j, System.Drawing.Color.FromArgb(kolor, kolor, kolor));

                    }
                }

                obrazek.Source = ConvertBitmapImage(b);
            }
        }

        private int ProgOtsu(System.Drawing.Bitmap b)
        {
            int[] histogram = new int[256];

            for (int m = 0; m < b.Width; m++)
            {
                for (int n = 0; n < b.Height; n++)
                {
                    System.Drawing.Color pixel = b.GetPixel(m, n);
                    histogram[pixel.R]++;

                }
            }

            long[] pob = new long[256];
            long[] pt = new long[256];


            for (int t = 0; t < 256; t++)
            {
                for (int t1 = 0; t1 <= t; t1++)
                {
                    pob[t] += histogram[t1];
                }

                for (int t1 = t + 1; t1 < 256; t1++)
                {
                    pt[t] += histogram[t1];
                }
            }

            double[] srOb = new double[256];
            double[] srT = new double[256];

            for (int t = 0; t < 256; t++)
            {
                for (int k = 0; k <= t; k++)
                {
                    srOb[t] += (k * histogram[k]);// / pob[t];

                }
                for (int k = t + 1; k < 256; k++)
                {

                    srT[t] += (k * histogram[k]);/// pt[t];
                }
            }

            for (int t = 0; t < 256; t++)
            {
                if (pob[t] != 0)
                    srOb[t] = srOb[t] / pob[t];
                if (pt[t] != 0)
                    srT[t] = srT[t] / pt[t];
            }


            double[] wariancjaMiedzy = new double[256];
            double maks = 0;

            for (int t = 0; t < 256; t++)
            {

                wariancjaMiedzy[t] = pob[t] * pt[t] * (srOb[t] - srT[t]) * (srOb[t] - srT[t]);
                //(pob[t] * Math.Pow(warOb[t], 2)) + (pt[t] * Math.Pow(warT[t], 2));
            }

            maks = 0;
            int x = 0;
            for (int w = 0; w < 256; w++)
            {
                if (wariancjaMiedzy[w] > maks)
                {
                    maks = wariancjaMiedzy[w];
                    x = w;
                }
            }

            return x;

        }


        #endregion

        #region Filtry

        private void FiltrWlasny(object sender, RoutedEventArgs e)
        {
            BitmapImage source = obrazek_2.Source as BitmapImage;
            System.Drawing.Bitmap b = BitmapImage2DBitmap(source);
            int[,] maska = new int[3, 3] { { 1, 2, 1 }, { 0, 0, 0 }, { -1, -2, -1 } };
            int podzielnik = 1;
            FiltrWlasny okno = new FiltrWlasny();
            if (okno.ShowDialog() == true)
            {
                maska = okno.maska;
                podzielnik = okno.podzielnik;
            }
            b = Filtrowanie(maska, podzielnik, b);
        }
        private void FiltrRozmywajacy(object sender, RoutedEventArgs e)
        {
            BitmapImage source = obrazek_2.Source as BitmapImage;
            System.Drawing.Bitmap b = BitmapImage2DBitmap(source);
            int[,] maska = new int[3, 3] { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } };
            b = Filtrowanie(maska, 9, b);

        }

        private void FiltrPrewitta(object sender, RoutedEventArgs e)
        {
            BitmapImage source = obrazek_2.Source as BitmapImage;
            System.Drawing.Bitmap b = BitmapImage2DBitmap(source);
            int[,] maskaHoryzontalna = new int[3, 3] { { 1, 1, 1 }, { 0, 0, 0 }, { -1, -1, -1 } };
            int[,] maskaWertykalna = new int[3, 3] { { 1, 0, -1 }, { 1, 0, -1 }, { 1, 0, -1 } };

            int[,,] pixelePoFiltracji1 = FiltrowanieOdczytPixeli(maskaHoryzontalna, 1, b);
            int[,,] pixelePoFiltracji2 = FiltrowanieOdczytPixeli(maskaWertykalna, 1, b);

            b = FiltrowanieUaktualnienieObrazu(pixelePoFiltracji1, b);
            b = FiltrowanieUaktualnienieObrazu(pixelePoFiltracji2, b);
        }
        private void FiltrSobela(object sender, RoutedEventArgs e)
        {
            BitmapImage source = obrazek_2.Source as BitmapImage;
            System.Drawing.Bitmap b = BitmapImage2DBitmap(source);
            int[,] maskapozioma = new int[3, 3] { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
            int[,] maskapionowa = new int[3, 3] { { 1, 2, 1 }, { 0, 0, 0 }, { -1, -2, -1 } };

            int[,,] pixelePoFiltracji1 = FiltrowanieOdczytPixeli(maskapozioma, 1, b);
            int[,,] pixelePoFiltracji2 = FiltrowanieOdczytPixeli(maskapionowa, 1, b);

            b = FiltrowanieUaktualnienieObrazu(pixelePoFiltracji1, b);
            b = FiltrowanieUaktualnienieObrazu(pixelePoFiltracji2, b);
        }

        private void FiltrLaplacea(object sender, RoutedEventArgs e)
        {
            BitmapImage source = obrazek_2.Source as BitmapImage;
            System.Drawing.Bitmap b = BitmapImage2DBitmap(source);
            int[,] maska = new int[3, 3] { { 0, -1, 0 }, { -1, 5, -1 }, { 0, -1, 0 } };
            b = Filtrowanie(maska, 1, b);
        }

        private void FiltrWykrywajacyNarozniki(object sender, RoutedEventArgs e)
        {
            BitmapImage source = obrazek_2.Source as BitmapImage;
            System.Drawing.Bitmap b = BitmapImage2DBitmap(source);
            int[,] maska0 = new int[3, 3] { { 1, 1, 1 }, { 1, -2, -1 }, { 1, -1, -1 } };
            int[,] maska1 = new int[3, 3] { { 1, 1, 1 }, { -1, -2, 1 }, { 1, -1, 1 } };
            int[,] maska2 = new int[3, 3] { { 1, -1, -1 }, { 1, -2, -1 }, { 1, 1, 1 } };
            int[,] maska3 = new int[3, 3] { { -1, -1, 1 }, { -1, -2, 1 }, { 1, 1, 1 } };

            int[,,] pixelePoFiltracji1 = FiltrowanieOdczytPixeli(maska0, 1, b);
            int[,,] pixelePoFiltracji2 = FiltrowanieOdczytPixeli(maska1, 1, b);
            int[,,] pixelePoFiltracji3 = FiltrowanieOdczytPixeli(maska2, 1, b);
            int[,,] pixelePoFiltracji4 = FiltrowanieOdczytPixeli(maska3, 1, b);

            b = FiltrowanieUaktualnienieObrazu(pixelePoFiltracji1, b);
            b = FiltrowanieUaktualnienieObrazu(pixelePoFiltracji2, b);
            b = FiltrowanieUaktualnienieObrazu(pixelePoFiltracji3, b);
            b = FiltrowanieUaktualnienieObrazu(pixelePoFiltracji4, b);
        }


        //filtrowanie na kilku maskach
        private int[,,] FiltrowanieOdczytPixeli(int[,] maska, int dzielnik, System.Drawing.Bitmap b)
        {
            int dlugosc = 1;
            int[,,] pixelergb = new int[3, b.Width, b.Height];
            if (b != null)
            {
                System.Drawing.Color kolorZm;
                int[] kolor = new int[3];

                int[,] nowePixeleR = new int[b.Width, b.Height];
                int[,] nowePixeleG = new int[b.Width, b.Height];
                int[,] nowePixeleB = new int[b.Width, b.Height];
                for (int x = dlugosc; x < b.Width - dlugosc; x++)
                {
                    for (int y = dlugosc; y < b.Height - dlugosc; y++)
                    {

                        int[] nowyKolor = new int[3];

                        int a1 = 1, a2 = 1;
                        for (int i = -1; i <= 1; i++)
                        {
                            for (int j = -1; j <= 1; j++)
                            {
                                kolorZm = b.GetPixel(x + i, y + j);
                                nowyKolor[0] += maska[a1 + i, a2 + j] * kolorZm.R;
                                nowyKolor[1] += maska[a1 + i, a2 + j] * kolorZm.G;
                                nowyKolor[2] += maska[a1 + i, a2 + j] * kolorZm.B;
                            }
                        }

                        nowyKolor[0] /= dzielnik;
                        nowyKolor[1] /= dzielnik;
                        nowyKolor[2] /= dzielnik;

                        if (nowyKolor[0] >= 255) nowyKolor[0] = 255;
                        if (nowyKolor[1] >= 255) nowyKolor[1] = 255;
                        if (nowyKolor[2] >= 255) nowyKolor[2] = 255;

                        if (nowyKolor[0] <= 0) nowyKolor[0] = 0;
                        if (nowyKolor[1] <= 0) nowyKolor[1] = 0;
                        if (nowyKolor[2] <= 0) nowyKolor[2] = 0;

                        nowePixeleR[x, y] = nowyKolor[0];
                        nowePixeleG[x, y] = nowyKolor[1];
                        nowePixeleB[x, y] = nowyKolor[2];
                        pixelergb[0, x, y] = nowyKolor[0];
                        pixelergb[1, x, y] = nowyKolor[0];
                        pixelergb[2, x, y] = nowyKolor[0];
                    }
                }
            }
            return pixelergb;
        }

        private System.Drawing.Bitmap FiltrowanieUaktualnienieObrazu(int[,,] maska, System.Drawing.Bitmap b)
        {
            int dlugosc = 1;
            if (b != null)
            {
                for (int x = dlugosc; x < b.Width - dlugosc; x++)
                {
                    for (int y = dlugosc; y < b.Height - dlugosc; y++)
                    {
                        b.SetPixel(x, y, System.Drawing.Color.FromArgb(maska[0, x, y], maska[1, x, y], maska[2, x, y]));
                    }
                }
                obrazek.Source = ConvertBitmapImage(b);
            }
            return b;
        }



        //filtrowanie poprzez jedną maskę
        private System.Drawing.Bitmap Filtrowanie(int[,] maska, int dzielnik, System.Drawing.Bitmap b)
        {
            int dlugosc = 1;

            if (b != null)
            {
                System.Drawing.Color kolorZm;
                int[] kolor = new int[3];
                int[,] nowePixeleR = new int[b.Width, b.Height];
                int[,] nowePixeleG = new int[b.Width, b.Height];
                int[,] nowePixeleB = new int[b.Width, b.Height];
                for (int x = dlugosc; x < b.Width - dlugosc; x++)
                {
                    for (int y = dlugosc; y < b.Height - dlugosc; y++)
                    {

                        int[] nowyKolor = new int[3];

                        int a1 = 1, a2 = 1;
                        for (int i = -1; i <= 1; i++)
                        {
                            for (int j = -1; j <= 1; j++)
                            {
                                kolorZm = b.GetPixel(x + i, y + j);
                                nowyKolor[0] += maska[a1 + i, a2 + j] * kolorZm.R;
                                nowyKolor[1] += maska[a1 + i, a2 + j] * kolorZm.G;
                                nowyKolor[2] += maska[a1 + i, a2 + j] * kolorZm.B;
                            }
                        }

                        nowyKolor[0] /= dzielnik;
                        nowyKolor[1] /= dzielnik;
                        nowyKolor[2] /= dzielnik;

                        if (nowyKolor[0] >= 255) nowyKolor[0] = 255;
                        if (nowyKolor[1] >= 255) nowyKolor[1] = 255;
                        if (nowyKolor[2] >= 255) nowyKolor[2] = 255;

                        if (nowyKolor[0] <= 0) nowyKolor[0] = 0;
                        if (nowyKolor[1] <= 0) nowyKolor[1] = 0;
                        if (nowyKolor[2] <= 0) nowyKolor[2] = 0;

                        nowePixeleR[x, y] = nowyKolor[0];
                        nowePixeleG[x, y] = nowyKolor[1];
                        nowePixeleB[x, y] = nowyKolor[2];
                    }
                }

                for (int x = dlugosc; x < b.Width - dlugosc; x++)
                {
                    for (int y = dlugosc; y < b.Height - dlugosc; y++)
                    {
                        b.SetPixel(x, y, System.Drawing.Color.FromArgb(nowePixeleR[x, y], nowePixeleG[x, y], nowePixeleB[x, y]));
                    }
                }
                obrazek.Source = ConvertBitmapImage(b);
            }
            return b;
        }

        private void FiltrKuwahara(object sender, RoutedEventArgs e)
        {
            BitmapImage source = obrazek_2.Source as BitmapImage;
            System.Drawing.Bitmap b = BitmapImage2DBitmap(source);

            b = FiltrowanieKuwahara(b);
        }

        private System.Drawing.Bitmap FiltrowanieKuwahara(System.Drawing.Bitmap b)
        {
            int dlugoscMaski = 5;

            if (b != null)
            {
                int[,] nowePixeleR = new int[b.Width, b.Height];
                int[,] nowePixeleG = new int[b.Width, b.Height];
                int[,] nowePixeleB = new int[b.Width, b.Height];

                for (int x = dlugoscMaski / 2; x < b.Width - dlugoscMaski / 2; x++)
                {
                    for (int y = dlugoscMaski / 2; y < b.Height - dlugoscMaski / 2; y++)
                    {
                        System.Drawing.Color pixel;

                        //region1
                        int[] srednia1region = new int[3];
                        int m = 0;
                        int[,] liczby1 = new int[3, 9];
                        int[] wariancja1region = new int[3];
                        for (int k = -2; k <= 0; k++)
                        {
                            for (int l = -2; l <= 0; l++)
                            {
                                pixel = b.GetPixel(x + k, y + l);
                                srednia1region[0] += pixel.R;
                                srednia1region[1] += pixel.G;
                                srednia1region[2] += pixel.B;
                                liczby1[0, m] = pixel.R;
                                liczby1[1, m] = pixel.G;
                                liczby1[2, m] = pixel.B;
                                m++;
                            }
                        }
                        srednia1region[0] /= 9;
                        srednia1region[1] /= 9;
                        srednia1region[2] /= 9;
                        int[] z1 = new int[3];
                        for (int i = 0; i < 9; i++)
                        {
                            z1[0] += (srednia1region[0] - liczby1[0, i]) * (srednia1region[0] - liczby1[0, i]);
                            z1[1] += (srednia1region[1] - liczby1[1, i]) * (srednia1region[1] - liczby1[1, i]);
                            z1[2] += (srednia1region[2] - liczby1[2, i]) * (srednia1region[2] - liczby1[2, i]);
                        }
                        wariancja1region[0] = z1[0] / 9;
                        wariancja1region[1] = z1[1] / 9;
                        wariancja1region[2] = z1[2] / 9;

                        //region2
                        int[] srednia2region = new int[3];
                        m = 0;
                        int[,] liczby2 = new int[3, 9];
                        int[] wariancja2region = new int[3];
                        for (int k = 0; k <= 2; k++)
                        {
                            for (int l = -2; l <= 0; l++)
                            {
                                pixel = b.GetPixel(x + k, y + l);
                                srednia2region[0] += pixel.R;
                                srednia2region[1] += pixel.G;
                                srednia2region[2] += pixel.B;
                                liczby2[0, m] = pixel.R;
                                liczby2[1, m] = pixel.G;
                                liczby2[2, m] = pixel.B;
                                m++;
                            }
                        }
                        srednia2region[0] /= 9;
                        srednia2region[1] /= 9;
                        srednia2region[2] /= 9;
                        int[] z2 = new int[3];
                        for (int i = 0; i < 9; i++)
                        {
                            z2[0] += (srednia2region[0] - liczby2[0, i]) * (srednia2region[0] - liczby2[0, i]);
                            z2[1] += (srednia2region[1] - liczby2[1, i]) * (srednia2region[1] - liczby2[1, i]);
                            z2[2] += (srednia2region[2] - liczby2[2, i]) * (srednia2region[2] - liczby2[2, i]);
                        }
                        wariancja2region[0] = z2[0] / 9;
                        wariancja2region[1] = z2[1] / 9;
                        wariancja2region[2] = z2[2] / 9;

                        //region3
                        int[] srednia3region = new int[3];
                        m = 0;
                        int[,] liczby3 = new int[3, 9];
                        int[] wariancja3region = new int[3];
                        for (int k = -2; k <= 0; k++)
                        {
                            for (int l = 0; l <= 2; l++)
                            {
                                pixel = b.GetPixel(x + k, y + l);
                                srednia3region[0] += pixel.R;
                                srednia3region[1] += pixel.G;
                                srednia3region[2] += pixel.B;
                                liczby3[0, m] = pixel.R;
                                liczby3[1, m] = pixel.G;
                                liczby3[2, m] = pixel.B;
                                m++;
                            }
                        }
                        srednia3region[0] /= 9;
                        srednia3region[1] /= 9;
                        srednia3region[2] /= 9;
                        int[] z3 = new int[3];
                        for (int i = 0; i < 9; i++)
                        {
                            z3[0] += (srednia3region[0] - liczby3[0, i]) * (srednia3region[0] - liczby3[0, i]);
                            z3[1] += (srednia3region[1] - liczby3[1, i]) * (srednia3region[1] - liczby3[1, i]);
                            z3[2] += (srednia3region[2] - liczby3[2, i]) * (srednia3region[2] - liczby3[2, i]);
                        }
                        wariancja3region[0] = z3[0] / 9;
                        wariancja3region[1] = z3[1] / 9;
                        wariancja3region[2] = z3[2] / 9;

                        //region4
                        int[] srednia4region = new int[3];
                        m = 0;
                        int[,] liczby4 = new int[3, 9];
                        int[] wariancja4region = new int[3];
                        for (int k = 0; k <= 2; k++)
                        {
                            for (int l = 0; l <= 2; l++)
                            {
                                pixel = b.GetPixel(x + k, y + l);
                                srednia4region[0] += pixel.R;
                                srednia4region[1] += pixel.G;
                                srednia4region[2] += pixel.B;
                                liczby4[0, m] = pixel.R;
                                liczby4[1, m] = pixel.G;
                                liczby4[2, m] = pixel.B;
                                m++;
                            }
                        }
                        srednia4region[0] /= 9;
                        srednia4region[1] /= 9;
                        srednia4region[2] /= 9;
                        int[] z4 = new int[3];
                        for (int i = 0; i < 9; i++)
                        {
                            z4[0] += (srednia4region[0] - liczby4[0, i]) * (srednia4region[0] - liczby4[0, i]);
                            z4[1] += (srednia4region[1] - liczby4[1, i]) * (srednia4region[1] - liczby4[1, i]);
                            z4[2] += (srednia4region[2] - liczby4[2, i]) * (srednia4region[2] - liczby4[2, i]);
                        }
                        wariancja4region[0] = z4[0] / 9;
                        wariancja4region[1] = z4[1] / 9;
                        wariancja4region[2] = z4[2] / 9;

                        int r = 0, g = 0, bb = 0;
                        int minR = wariancja1region[0];
                        if (wariancja2region[0] < minR) { minR = wariancja2region[0]; r = 1; }
                        if (wariancja3region[0] < minR) { minR = wariancja3region[0]; r = 2; }
                        if (wariancja4region[0] < minR) { minR = wariancja4region[0]; r = 3; }

                        int minG = wariancja1region[1];
                        if (wariancja2region[1] < minG) { minG = wariancja2region[0]; g = 1; }
                        if (wariancja3region[1] < minG) { minG = wariancja3region[1]; g = 2; }
                        if (wariancja4region[1] < minG) { minG = wariancja4region[1]; g = 3; }

                        int minB = wariancja1region[2];
                        if (wariancja2region[2] < minB) { minB = wariancja2region[2]; bb = 1; }
                        if (wariancja3region[2] < minB) { minB = wariancja3region[0]; bb = 2; }
                        if (wariancja4region[2] < minB) { minB = wariancja4region[2]; bb = 3; }

                        if (r == 0) nowePixeleR[x, y] = srednia1region[0];
                        if (r == 1) nowePixeleR[x, y] = srednia2region[0];
                        if (r == 2) nowePixeleR[x, y] = srednia3region[0];
                        if (r == 3) nowePixeleR[x, y] = srednia4region[0];

                        if (g == 0) nowePixeleG[x, y] = srednia1region[1];
                        if (g == 1) nowePixeleG[x, y] = srednia2region[1];
                        if (g == 2) nowePixeleG[x, y] = srednia3region[1];
                        if (g == 3) nowePixeleG[x, y] = srednia4region[1];

                        if (bb == 0) nowePixeleB[x, y] = srednia1region[2];
                        if (bb == 1) nowePixeleB[x, y] = srednia2region[2];
                        if (bb == 2) nowePixeleB[x, y] = srednia3region[2];
                        if (bb == 3) nowePixeleB[x, y] = srednia4region[2];

                    }
                }

                for (int x = dlugoscMaski / 2; x < b.Width - dlugoscMaski / 2; x++)
                {
                    for (int y = dlugoscMaski / 2; y < b.Height - dlugoscMaski / 2; y++)
                    {
                        b.SetPixel(x, y, System.Drawing.Color.FromArgb(nowePixeleR[x, y], nowePixeleG[x, y], nowePixeleB[x, y]));
                    }
                }

                obrazek.Source = ConvertBitmapImage(b);
            }
            return b;
        }

        private void FiltrMaciezowy3x3(object sender, RoutedEventArgs e)
        {
            BitmapImage source = obrazek_2.Source as BitmapImage;
            System.Drawing.Bitmap b = BitmapImage2DBitmap(source);
            b = FiltrowanieMedianowe(3, b);
        }
        private void FiltrMaciezowy5x5(object sender, RoutedEventArgs e)
        {
            BitmapImage source = obrazek_2.Source as BitmapImage;
            System.Drawing.Bitmap b = BitmapImage2DBitmap(source);
            b = FiltrowanieMedianowe(5, b);
        }
        private System.Drawing.Bitmap FiltrowanieMedianowe(int rozmiarMaski, System.Drawing.Bitmap b)
        {
            int dlugosc = rozmiarMaski * rozmiarMaski;
            int pol = rozmiarMaski / 2;
            if (b != null)
            {
                System.Drawing.Color kolorWejsciowy;
                int[,] nowePixeleR = new int[b.Width, b.Height];
                int[,] nowePixeleG = new int[b.Width, b.Height];
                int[,] nowePixeleB = new int[b.Width, b.Height];

                for (int x = 0 + pol; x < b.Width - pol; x++)
                {
                    for (int y = 0 + pol; y < b.Height - pol; y++)
                    {
                        int[] tableR = new int[dlugosc];
                        int[] tableG = new int[dlugosc];
                        int[] tableB = new int[dlugosc];
                        int o = 0;
                        for (int i = x - pol; i <= x + pol; i++)
                        {
                            for (int j = y - pol; j <= y + pol; j++)
                            {
                                kolorWejsciowy = b.GetPixel(i, j);
                                tableR[o] = kolorWejsciowy.R;
                                tableG[o] = kolorWejsciowy.G;
                                tableB[o] = kolorWejsciowy.B;
                                o++;
                            }
                        }

                        Array.Sort(tableR);
                        Array.Sort(tableG);
                        Array.Sort(tableB);
                        int medianValueR = tableR[dlugosc / 2];
                        int medianValueG = tableG[dlugosc / 2];
                        int medianValueB = tableB[dlugosc / 2];
                        nowePixeleR[x, y] = medianValueR;
                        nowePixeleG[x, y] = medianValueG;
                        nowePixeleB[x, y] = medianValueB;
                    }
                }
                for (int x = 0 + pol; x < b.Width - pol; x++)
                {
                    for (int y = 0 + pol; y < b.Height - pol; y++)
                    {
                        b.SetPixel(x, y, System.Drawing.Color.FromArgb(nowePixeleR[x, y], nowePixeleG[x, y], nowePixeleB[x, y]));
                    }
                }
                obrazek.Source = ConvertBitmapImage(b);
            }
            return b;
        }

        private void ZamkniecieMorfologiczne(object sender, RoutedEventArgs e)
        {
            BitmapImage source = obrazek_2.Source as BitmapImage;
            System.Drawing.Bitmap b = BitmapImage2DBitmap(source);
            morphologicalClosure morphologic = new morphologicalClosure();
            int[,] maska = new int[3, 3] { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } };
            b = morphologic.FilterM(maska, b);
            obrazek.Source = ConvertBitmapImage(b);
        }

        #endregion

    }
}




