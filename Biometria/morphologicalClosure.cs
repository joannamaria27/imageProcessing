using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Biometria
{
    class morphologicalClosure
    {
        public Bitmap FilterM(int[,] maska, Bitmap b)//maska 3x3 - same jedynki
        {
            int distance = 1;

            int[,] newPixel = new int[b.Width, b.Height];
            int[,] pixelDilation;
            int[,] pixeleErosion;

            Color koloryOb;
            for (int x = distance; x < b.Width - distance; x++)
            {
                for (int y = distance; y < b.Height - distance; y++)
                {
                    koloryOb = b.GetPixel(x, y);
                    if (koloryOb.R == 0) 
                        newPixel[x, y] = 1;
                    else newPixel[x, y] = 0;
                }
            }

            pixelDilation = newPixel.Clone() as int [,];
            //dilation
            for (int x = distance; x < b.Width - distance; x++)
            {
                for (int y = distance; y < b.Height - distance; y++)
                {
                    if (newPixel[x, y] == 1)
                    {
                        if (newPixel[x + 1, y + 1] != 1 || newPixel[x + 1, y] != 1 || newPixel[x, y + 1] != 1 || newPixel[x + 1, y - 1] != 1 ||
                            newPixel[x - 1, y + 1] != 1 || newPixel[x - 1, y] != 1 || newPixel[x, y - 1] != 1 || newPixel[x - 1, y - 1] != 1)
                            pixelDilation[x, y] = 0;
                    }
                }
            }

            pixeleErosion = pixelDilation.Clone() as int[,];
            //erosion
            for (int x = distance; x < b.Width - distance; x++)
            {
                for (int y = distance; y < b.Height - distance; y++)
                {
                    if (pixelDilation[x, y] == 0)
                    {
                        if (pixelDilation[x + 1, y + 1] != 0 || pixelDilation[x + 1, y] != 0 || pixelDilation[x, y + 1] != 0 || pixelDilation[x + 1, y - 1] != 0 ||
                            pixelDilation[x - 1, y + 1] != 0 || pixelDilation[x - 1, y] != 0 || pixelDilation[x, y - 1] != 0 || pixelDilation[x - 1, y - 1] != 0)
                            pixeleErosion[x, y] = 1;
                    }
                }
            }
            newPixel = new int[b.Width, b.Height];
            for(int i = 0; i< b.Width;i++)
            {
                for (int j = 0; j < b.Height; j++)
                {
                    if(pixeleErosion[i,j] == 1)
                    {
                        newPixel[i, j] = 255;
                    }
                    newPixel[i, j] = 0;
                }
            }

            for (int x = distance; x < b.Width - distance; x++)
                for (int y = distance; y < b.Height - distance; y++)
                    b.SetPixel(x, y, Color.FromArgb(newPixel[x, y], newPixel[x, y], newPixel[x, y]));

            return b;
        }

    }
}
