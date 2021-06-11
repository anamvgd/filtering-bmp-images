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
                    int num = i + 1;
                    String path = "../../../../BMPImages/homer" + num + ".bmp";

                    filtroSobelG7.leerImagen(path, num);
                Console.WriteLine(i);
                }

        }

        protected static sbyte[][] kernel;

        private void init()
        {
            kernel = new sbyte[][] { new sbyte[] {-1, 0 ,1}, new sbyte[] { -2, 0, 2 }, new sbyte[] { -1, 0, 1 } };
        }

// Estas dos opciones(xy, yx) se combinan con 3 versiones posibles de indexar la matriz núcleo(ij, ji, unrolling)

        static byte[] filteringAlgorithmXYIJ(byte[] data)
        {
            int offset = 1078;
            int n = (int)Math.Sqrt(data.Length - offset);
            byte[,] changed = new byte[n, n];

            long time1 = DateTime.Now.Ticks;

            for (int x = 1; x < n - 1; x++) 
            {
                for (int y = 1; y < n - 1; y++) 
                {
                    for (int i = 0; i < kernel.Length; i++)
                    {
                        for (int j = 0; j < kernel[i].Length; j++)
                        {
                            int row = x + i - 1;
                            int col = y + j - 1;

                            changed[x, y] = (byte)(changed[x, y] + data[offset + row * n + col] * kernel[i][j]);
                        }
                    }
                }
            }

            long time2 = DateTime.Now.Ticks;
            long finalTime = (time2 - time1) * 100;
            Console.WriteLine(finalTime+"  "+"1");
            int co = 0;
            for (int x = 0; x < n; x++) 
            {
                for (int y = 0; y < n; y++) 
                {
                    data[offset + co] = changed[x, y];
                    co++;
                }
            }

            return data;
        }

        static byte[] filteringAlgorithmYXIJ(byte[] data)
        {
            int offset = 1078;
            int n = (int)Math.Sqrt(data.Length - offset);
            byte[,] changed = new byte[n, n];

            long time1 = DateTime.Now.Ticks;

            for (int y = 1; y < n - 1; y++) 
            {
                for (int x = 1; x < n - 1; x++) 
                {
                    for (int i = 0; i < kernel.Length; i++)
                    {
                        for (int j = 0; j < kernel[i].Length; j++)
                        {
                            int row = x + i - 1;
                            int col = y + j - 1;

                            changed[x, y] = (byte)(changed[x, y] + data[offset + row * n + col] * kernel[i][j]);
                        }
                    }
                }
            }

            long time2 = DateTime.Now.Ticks;
            long finalTime = (time2 - time1) * 100;
            Console.WriteLine(finalTime + "  " + "2");

            int co = 0;
            for (int x = 0; x < n; x++) 
            {
                for (int y = 0; y < n; y++) 
                {
                    data[offset + co] = changed[x, y];
                    co++;
                }
            }

            return data;
        }

        static byte[] filteringAlgorithmXYJI(byte[] data)
        {
            int offset = 1078;
            int n = (int)Math.Sqrt(data.Length - offset);
            byte[,] changed = new byte[n, n];

            long time1 = DateTime.Now.Ticks;

            int kernelMax = 3;
            for (int x = 1; x < n - 1; x++) 
            {
                for (int y = 1; y < n - 1; y++) 
                {
                    for (int j = 0; j < kernelMax; j++)
                    {
                        for (int i = 0; i < kernelMax; i++)
                        {
                            int row = x + i - 1;
                            int col = y + j - 1;

                            changed[x, y] = (byte)(changed[x, y] + data[offset + row * n + col] * kernel[i][j]);
                        }
                    }
                }
            }

            long time2 = DateTime.Now.Ticks;
            long finalTime = (time2 - time1) * 100;
            Console.WriteLine(finalTime + "  " + "3");

            int co = 0;
            for (int x = 0; x < n; x++) // starts at offset + n because image upper limit of the image is ignored
            {
                for (int y = 0; y < n; y++) // starts at 1 because
                {
                    data[offset + co] = changed[x, y];
                    co++;
                }
            }

            return data;
        }

        static byte[] filteringAlgorithmYXJI(byte[] data)
        {
            int offset = 1078;
            int n = (int)Math.Sqrt(data.Length - offset);
            byte[,] changed = new byte[n, n];

            long time1 = DateTime.Now.Ticks;

            for (int y = 1; y < n - 1; y++) // starts at offset + n because image upper limit of the image is ignored
            {
                for (int x = 1; x < n - 1; x++) // starts at 1 because
                {
                    for (int j = 0; j < kernel.Length; j++)
                    {
                        for (int i = 0; i < kernel[0].Length; i++)
                        {
                            int row = x + i - 1;
                            int col = y + j - 1;

                            changed[x, y] = (byte)(changed[x, y] + data[offset + row * n + col] * kernel[i][j]);
                        }
                    }
                }
            }

            long time2 = DateTime.Now.Ticks;
            long finalTime = (time2 - time1) * 100;
            Console.WriteLine(finalTime + "  " + "4");

            int co = 0;
            for (int x = 0; x < n; x++) // starts at offset + n because image upper limit of the image is ignored
            {
                for (int y = 0; y < n; y++) // starts at 1 because
                {
                    data[offset + co] = changed[x, y];
                    co++;
                }
            }

            return data;
        }

        static byte[] filteringAlgorithmXYunrolling(byte[] image)
        {
            int offset = 1078;
            int n = (int)Math.Sqrt(image.Length - offset);
            byte[,] changed = new byte[n, n];

            long time1 = DateTime.Now.Ticks;

            for (int x = 1; x < n - 1; x++) // starts at offset + n because image upper limit of the image is ignored
            {
                for (int y = 1; y < n - 1; y++) // starts at 1 because
                {
                    int r1 = x - 1;
                    int r2 = x;
                    int r3 = x + 1;

                    int c1 = y - 1;
                    int c2 = y;
                    int c3 = y + 1;

                    changed[x, y] = (byte)(changed[x, y] + image[offset + (r1 * n) + c1] * kernel[0][0]);
                    changed[x, y] = (byte)(changed[x, y] + image[offset + (r1 * n) + c2] * kernel[0][1]);
                    changed[x, y] = (byte)(changed[x, y] + image[offset + (r1 * n) + c3] * kernel[0][2]);
                    changed[x, y] = (byte)(changed[x, y] + image[offset + (r2 * n) + c1] * kernel[1][0]);
                    changed[x, y] = (byte)(changed[x, y] + image[offset + (r2 * n) + c2] * kernel[1][1]);
                    changed[x, y] = (byte)(changed[x, y] + image[offset + (r2 * n) + c3] * kernel[1][2]);
                    changed[x, y] = (byte)(changed[x, y] + image[offset + (r3 * n) + c1] * kernel[2][0]);
                    changed[x, y] = (byte)(changed[x, y] + image[offset + (r3 * n) + c2] * kernel[2][1]);
                    changed[x, y] = (byte)(changed[x, y] + image[offset + (r3 * n) + c3] * kernel[2][2]);
                }
            }

            long time2 = DateTime.Now.Ticks;
            long finalTime = (time2 - time1) * 100;
            Console.WriteLine(finalTime + "  " + "5");

            int co = 0;
            for (int x = 0; x < n; x++) // starts at offset + n because image upper limit of the image is ignored
            {
                for (int y = 0; y < n; y++) // starts at 1 because
                {
                    image[offset + co] = changed[x, y];
                    co++;
                }
            }

            return image;
        }

        static byte[] filteringAlgorithmYXunrolling(byte[] image)
        {
            int offset = 1078;
            int n = (int)Math.Sqrt(image.Length - offset);
            byte[,] changed = new byte[n, n];

            long time1 = DateTime.Now.Ticks;

            for (int y = 1; y < n - 1; y++) // starts at offset + n because image upper limit of the image is ignored
            {
                for (int x = 1; x < n - 1; x++) // starts at 1 because
                {
                    int r1 = x - 1;
                    int r2 = x;
                    int r3 = x + 1;

                    int c1 = y - 1;
                    int c2 = y;
                    int c3 = y + 1;

                    changed[x, y] = (byte)(changed[x, y] + image[offset + (r1 * n) + c1] * kernel[0][0]);
                    changed[x, y] = (byte)(changed[x, y] + image[offset + (r1 * n) + c2] * kernel[0][1]);
                    changed[x, y] = (byte)(changed[x, y] + image[offset + (r1 * n) + c3] * kernel[0][2]);
                    changed[x, y] = (byte)(changed[x, y] + image[offset + (r2 * n) + c1] * kernel[1][0]);
                    changed[x, y] = (byte)(changed[x, y] + image[offset + (r2 * n) + c2] * kernel[1][1]);
                    changed[x, y] = (byte)(changed[x, y] + image[offset + (r2 * n) + c3] * kernel[1][2]);
                    changed[x, y] = (byte)(changed[x, y] + image[offset + (r3 * n) + c1] * kernel[2][0]);
                    changed[x, y] = (byte)(changed[x, y] + image[offset + (r3 * n) + c2] * kernel[2][1]);
                    changed[x, y] = (byte)(changed[x, y] + image[offset + (r3 * n) + c3] * kernel[2][2]);
                }
            }

            long time2 = DateTime.Now.Ticks;
            long finalTime = (time2 - time1) * 100;
            Console.WriteLine(finalTime + "  " + "6");

            int co = 0;
            for (int x = 0; x < n; x++) // starts at offset + n because image upper limit of the image is ignored
            {
                for (int y = 0; y < n; y++) // starts at 1 because
                {
                    image[offset + co] = changed[x, y];
                    co++;
                }
            }

            return image;
        }

        protected void leerImagen(String path, int i)
        {
            FileInfo output = null;
            FileInfo output2 = null;
            FileInfo output3 = null;
            FileInfo output4 = null;
            FileInfo output5 = null;
            FileInfo output6 = null;

            for (int z = 0; z < 3; z++)
             {
                
                 FileInfo fileInfo = new FileInfo(path);
                 byte[] data = new byte[fileInfo.Length];

                 using (FileStream fs = fileInfo.OpenRead())
                 {
                     fs.Read(data, 0, data.Length);
                     //fs.Close();
                 }
                 
                 byte[] imagenfilteringAlgorithmXYIJ2 = filteringAlgorithmXYIJ(data);

                 output = new FileInfo("../../../../BMPImages/Out" + i + "_XYIJ" + ".bmp");


                 if (z < 2)
                 {
                     output.Delete();
                     //fileInfo.Delete();
                 }

                 using (FileStream fs = output.OpenWrite())
                 {
                     fs.Write(data, 0, data.Length);
                     fs.Close();
                 }


             }

              for (int m = 0; m < 3; m++)
              {
                  
                  FileInfo fileInfo = new FileInfo(path);
                  byte[] data = new byte[fileInfo.Length];

                  using (FileStream fs = fileInfo.OpenRead())
                  {
                      fs.Read(data, 0, data.Length);
                      //fs.Close();
                  }
               

                    byte[] imagenfilteringAlgorithmYXIJ = filteringAlgorithmYXIJ(data);

                    output2 = new FileInfo("../../../../BMPImages/Out" + i + "_YXIJ" + ".bmp");

                  if (m < 2)
                  {
                      output2.Delete();
                  }

                  using (FileStream fs = output2.OpenWrite())
                        {
                            fs.Write(data, 0, data.Length);
                        }
              }

              for (int n = 0; n < 3; n++)
              {
                   FileInfo fileInfo = new FileInfo(path);
                   byte[] data = new byte[fileInfo.Length];

                   using (FileStream fs = fileInfo.OpenRead())
                   {
                       fs.Read(data, 0, data.Length);
                       //fs.Close();
                   }

                    byte[] imagenfilteringAlgorithmXYJI = filteringAlgorithmXYJI(data);

                    output3 = new FileInfo("../../../../BMPImages/Out" + i + "_XYJI" + ".bmp");

                    if (n < 2){
                      output3.Delete();
                    }
                    
                    using (FileStream fs = output3.OpenWrite())
                    {
                        fs.Write(data, 0, data.Length);
                    }
              }


            for (int b = 0; b < 3; b++)
            {
                FileInfo fileInfo = new FileInfo(path);
                byte[] data = new byte[fileInfo.Length];

                using (FileStream fs = fileInfo.OpenRead())
                {
                    fs.Read(data, 0, data.Length);
                    //fs.Close();
                }

                byte[] imagenfilteringAlgorithmYXJI = filteringAlgorithmYXJI(data);


                output4 = new FileInfo("../../../../BMPImages/Out" + i + "_YXJI" + ".bmp");

                if (b < 2)
                {
                    output4.Delete();
                }

                using (FileStream fs = output4.OpenWrite())
                {
                    fs.Write(data, 0, data.Length);
                }

            }

            for (int c = 0; c < 3; c++)
            {
                FileInfo fileInfo = new FileInfo(path);
                byte[] data = new byte[fileInfo.Length];

                using (FileStream fs = fileInfo.OpenRead())
                {
                    fs.Read(data, 0, data.Length);
                    //fs.Close();
                }


                byte[] imagenfilteringAlgorithmXYunrolling = filteringAlgorithmXYunrolling(data);

                output5 = new FileInfo("../../../../BMPImages/Out" + i + "_XYunrolling" + ".bmp");

                if (c < 2)
                {
                    output5.Delete();
                }

                using (FileStream fs = output5.OpenWrite())
                {
                    fs.Write(data, 0, data.Length);
                }
            }

            for (int d = 0; d < 3; d++)
            {
                FileInfo fileInfo = new FileInfo(path);
                byte[] data = new byte[fileInfo.Length];

                using (FileStream fs = fileInfo.OpenRead())
                {
                    fs.Read(data, 0, data.Length);
                    //fs.Close();
                }
                byte[] imagenfilteringAlgorithmYXunrolling = filteringAlgorithmYXunrolling(data);

                output6 = new FileInfo("../../../../BMPImages/Out" + i + "_YXunrolling" + ".bmp");

                if (d < 2)
                {
                    output6.Delete();
                }

                using (FileStream fs = output6.OpenWrite())
                {
                    fs.Write(data, 0, data.Length);
                }

            }


        }
    }
}
