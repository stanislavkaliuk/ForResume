using System;

namespace UnityEngine
{
    public static class Extensions
    {
        public static T[,] To2DimArray<T>(this T[] array,int width, int height)
        {
            if (array.Length != width * height)
            {
                throw new IndexOutOfRangeException();
            }
            T[,] res = new T[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    res[i, j] = array[j * height + i];
                }
            }

            return res;
        }
    }
}