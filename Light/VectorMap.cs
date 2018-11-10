﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Light
{
    class NormalMap
    {
        int width;
        int height;
        double[,][] map;

        public NormalMap(Bitmap normalMap)
        {
            width = normalMap.Width;
            height = normalMap.Height;
            map = new double[width, height][];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    map[i, j] = new double[3];
                    double d = normalMap.GetPixel(i, j).B - 128;
                    map[i, j][0] = (normalMap.GetPixel(i, j).R - 128)/d;
                    map[i, j][1] = (normalMap.GetPixel(i, j).G - 128)/d;
                    map[i, j][2] = 1;
                }
            }
        }

        public NormalMap()
        {
            width = 1;
            height = 1;
            map = new double[1, 1][];
            map[0, 0] = new double[3];
            map[0, 0][0] = 0;
            map[0, 0][1] = 0;
            map[0, 0][2] = 1;
        }

        public NormalMap disturb(HeightMap heightMap)
        {
            NormalMap disturbed = new NormalMap();
            disturbed.width = Screen.PrimaryScreen.WorkingArea.Width;
            disturbed.height = Screen.PrimaryScreen.WorkingArea.Height;
            disturbed.map = new double[disturbed.width, disturbed.height][];
            for (int x = 0; x < disturbed.width; x++)
            {
                for (int y = 0; y < disturbed.height; y++)
                {
                    disturbed.map[x, y] = new double[3];
                    double dx = heightMap.GetHeight(x + 1, y) - heightMap.GetHeight(x, y);
                    double dy = heightMap.GetHeight(x, y + 1) - heightMap.GetHeight(x, y);
                    double[] vector = GetVector(x, y);
                    disturbed.map[x, y][0] = vector[0] + dx;
                    disturbed.map[x, y][1] = vector[1] + dy;
                    disturbed.map[x, y][2] = vector[2] - vector[0] * dx - vector[1] * dy;
                }
            }
            return disturbed;
        }

        public double[] GetVector(int x, int y)
        {
            return map[x % width, y % height];
        }
    }

    class HeightMap
    {
        int width;
        int height;
        double[,] map;

        public HeightMap(Bitmap heightMap)
        {
            width = heightMap.Width;
            height = heightMap.Height;
            map = new double[width, height];
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    map[i, j] = heightMap.GetPixel(i,j).GetBrightness();
        }

        public HeightMap()
        {
            width = 1;
            height = 1;
            map = new double[1, 1];
            map[0, 0] = 0;
        }

        public double GetHeight(int x, int y)
        {
            return map[x % width, y % height];
        }
    }

}
