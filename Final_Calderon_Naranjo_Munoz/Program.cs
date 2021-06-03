using System;
using System.IO;

namespace Final_Calderon_Naranjo_Munoz
{
    class Program
    {
        static void Main(string[] args)
        {
            Program filtroSobelG7 = new Program();

            filtroSobelG7.init();

            for (int i = 0; i <= 11; i++)
            {
                int num = i+1;
                String path = "../../../../BMPImages/homer" + num + ".bmp";

                filtroSobelG7.leerImagen(path, num);
            }

        }

        protected static sbyte[][] kernel;

        private void init()
        {
            kernel = new sbyte[][] { new sbyte[] {-1, 0 ,1}, new sbyte[] { -2, 0, 2 }, new sbyte[] { -1, 0, 1 } };
        }


        static byte[] filteringAlgorithmXYIJ2(byte[] data)
        {
            int offset = 1078;
            int n = (int)Math.Sqrt(data.Length - offset);
            byte[,] C = new byte[n, n];
            for (int x = 1; x < n - 1; x++) // starts at offset + n because image upper limit of the image is ignored
            {
                for (int y = 1; y < n - 1; y++) // starts at 1 because
                {
                    for (int i = 0; i < kernel.Length; i++)
                    {
                        for (int j = 0; j < kernel[i].Length; j++)
                        {
                            int row = x + i - 1;
                            int col = y + j - 1;

                            C[x, y] = (byte)(C[x, y] + data[offset + row * n + col] * kernel[i][j]);
                        }
                    }
                }
            }
          
            int co = 0;
            for (int x = 0; x < n; x++) // starts at offset + n because image upper limit of the image is ignored
            {
                for (int y = 0; y < n; y++) // starts at 1 because
                {
                    data[offset + co] = C[x, y];
                    co++;
                }
            }

            return data;
        }

        protected void leerImagen(String path, int i)
        {
            // Load file meta data with FileInfo
            FileInfo fileInfo = new FileInfo(path);
            Console.WriteLine(fileInfo.Length);
            // The byte[] to save the data in
            byte[] data = new byte[fileInfo.Length];
            // Load a filestream and put its content into the byte[]

            using (FileStream fs = fileInfo.OpenRead())
            {
                fs.Read(data, 0, data.Length);
            }
            byte[] imagenConFiltro = filteringAlgorithmXYIJ2(data);

            FileInfo output = new FileInfo("../../../../BMPImages/Out" + i + ".bmp");
            using (FileStream fs = output.OpenWrite())
            {
                fs.Write(data, 0, data.Length);
            }
        }
    }
}
