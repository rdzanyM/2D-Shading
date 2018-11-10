using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Light
{
    class VectorMap
    {
        int width;
        int height;
        double[,][] map;

        public VectorMap(Bitmap normalMap)
        {
            width = normalMap.Width;
            height = normalMap.Height;
            map = new double[width, height][];
            for(int i = 0; i < width; i++)
            {
                for(int j = 0; j < height; j++)
                {
                    map[i, j] = new double[3];
                    map[i, j][0] = normalMap.GetPixel(i, j).R - 128;
                    map[i, j][1] = normalMap.GetPixel(i, j).G - 128;
                    map[i, j][2] = normalMap.GetPixel(i, j).B - 128;
                }
            }
        }

        public VectorMap()
        {
            width = 1;
            height = 1;
            map = new double[1, 1][];
            map[0, 0] = new double[3];
            map[0, 0][0] = 0;
            map[0, 0][1] = 0;
            map[0, 0][2] = 1;
        }

        public double[] GetVector(int x, int y)
        {
            return map[x % width, y % height];
        }
    }
}
