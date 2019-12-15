using System;
using System.Collections.Generic;
using Emgu.CV.Structure;
using Emgu.CV;
using System.IO;
using System.Linq;

namespace SS_OpenCV
{
    class ImageClass
    {
        public static void Negative(Image<Bgr, byte> img)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                byte blue, green, red;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels;
                int padding = m.widthStep - m.nChannels * m.width;
                int x, y;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            blue = dataPtr[0];
                            green = dataPtr[1];
                            red = dataPtr[2];

                            dataPtr[0] = (byte)(255 - (int)blue);
                            dataPtr[1] = (byte)(255 - (int)green);
                            dataPtr[2] = (byte)(255 - (int)red);

                            dataPtr += nChan;
                        }

                        dataPtr += padding;
                    }
                }
            }
        }

        public static void ConvertToGray(Image<Bgr, byte> img)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                byte blue, green, red, gray;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels;
                int padding = m.widthStep - m.nChannels * m.width;
                int x, y;

                if (nChan == 3)
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            blue = dataPtr[0];
                            green = dataPtr[1];
                            red = dataPtr[2];

                            gray = (byte)Math.Round(((int)blue + green + red) / 3.0);

                            dataPtr[0] = gray;
                            dataPtr[1] = gray;
                            dataPtr[2] = gray;

                            dataPtr += nChan;
                        }

                        dataPtr += padding;
                    }
                }
            }
        }

        public static void BlueChannel(Image<Bgr, byte> img)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                byte blue;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels;
                int padding = m.widthStep - m.nChannels * m.width;
                int x, y;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            blue = dataPtr[0];
                            dataPtr[1] = blue;
                            dataPtr[2] = blue;

                            dataPtr += nChan;
                        }

                        dataPtr += padding;
                    }
                }
            }
        }

        public static void GreenChannel(Image<Bgr, byte> img)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte green;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels;
                int padding = m.widthStep - m.nChannels * m.width;
                int x, y;

                if (nChan == 3)
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            green = dataPtr[1];
                            dataPtr[0] = green;
                            dataPtr[2] = green;

                            dataPtr += nChan;
                        }

                        dataPtr += padding;
                    }
                }
            }
        }

        public static void RedChannel(Image<Bgr, byte> img)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                byte red;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels;
                int padding = m.widthStep - m.nChannels * m.width;
                int x, y;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            red = dataPtr[2];
                            dataPtr[0] = red;
                            dataPtr[1] = red;

                            dataPtr += nChan;
                        }

                        dataPtr += padding;
                    }
                }
            }
        }

        public static void BrightContrast(Image<Bgr, byte> img, int bright, double contrast)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte blue, green, red;
                double newBlue, newGreen, newRed;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width;
                int x, y;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            blue = dataPtr[0];
                            green = dataPtr[1];
                            red = dataPtr[2];

                            newBlue = bright + contrast * blue;
                            newGreen = bright + contrast * green;
                            newRed = bright + contrast * red;

                            if (newBlue > 255) newBlue = 255;
                            if (newGreen > 255) newGreen = 255;
                            if (newRed > 255) newRed = 255;

                            if (newBlue < 0) newBlue = 0;
                            if (newGreen < 0) newGreen = 0;
                            if (newRed < 0) newRed = 0;

                            dataPtr[0] = (byte)Math.Round(newBlue);
                            dataPtr[1] = (byte)Math.Round(newGreen);
                            dataPtr[2] = (byte)Math.Round(newRed);

                            dataPtr += nChan;
                        }

                        dataPtr += padding;
                    }
                }
            }
        }

        public static void Translation(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, int dx, int dy)
        {
            unsafe
            {
                MIplImage mAux = imgCopy.MIplImage;
                MIplImage mResult = img.MIplImage;

                byte* dataPtrO = (byte*)mAux.imageData.ToPointer(); // pointer to the original image
                byte* dataPtrD = (byte*)mResult.imageData.ToPointer(); // pointer to the duplicate image

                byte blue, green, red;

                int width = img.Width;
                int height = img.Height;
                int nChan = mAux.nChannels; // number of channels - 3
                int w = mAux.width;
                int ws = mAux.widthStep;
                int x, y, xO, yO;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            xO = x - dx;
                            yO = y - dy;

                            if (xO < 0 || xO > width - 1 || yO < 0 || yO > height - 1)
                            {
                                blue = 0;
                                green = 0;
                                red = 0;
                            }
                            else
                            {
                                // absolute addressing
                                blue = (byte)(dataPtrO + yO * ws + xO * nChan)[0];
                                green = (byte)(dataPtrO + yO * ws + xO * nChan)[1];
                                red = (byte)(dataPtrO + yO * ws + xO * nChan)[2];
                            }

                            (dataPtrD + y * ws + x * nChan)[0] = blue;
                            (dataPtrD + y * ws + x * nChan)[1] = green;
                            (dataPtrD + y * ws + x * nChan)[2] = red;
                        }
                    }
                }
            }
        }

        public static void Rotation(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float angle)
        {

            unsafe
            {

                MIplImage mResult = img.MIplImage;
                MIplImage mAux = imgCopy.MIplImage;

                byte* dataPtrO = (byte*)mAux.imageData.ToPointer(); // pointer to the original image
                byte* dataPtrD = (byte*)mResult.imageData.ToPointer(); // pointer to the duplicate image

                byte blue, green, red;

                int width = img.Width;
                int height = img.Height;
                int nChan = mAux.nChannels; // number of channels - 3
                int w = mAux.width;
                int ws = mAux.widthStep;
                int x, y, xO, yO;

                double xCenter = img.Width / 2.0;
                double yCenter = img.Height / 2.0;
                double cosAngle = Math.Cos(angle);
                double sinAngle = Math.Sin(angle);

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            xO = (int)Math.Round((x - xCenter) * cosAngle - (yCenter - y) * sinAngle + xCenter);
                            yO = (int)Math.Round(yCenter - (x - xCenter) * sinAngle - (yCenter - y) * cosAngle);

                            if (xO < 0 || yO < 0 || xO >= width || yO >= height)
                            {
                                blue = 0;
                                green = 0;
                                red = 0;
                            }
                            else
                            {
                                // absolute address
                                blue = (byte)(dataPtrO + yO * ws + xO * nChan)[0];
                                green = (byte)(dataPtrO + yO * ws + xO * nChan)[1];
                                red = (byte)(dataPtrO + yO * ws + xO * nChan)[2];
                            }

                            (dataPtrD + y * ws + x * nChan)[0] = blue;
                            (dataPtrD + y * ws + x * nChan)[1] = green;
                            (dataPtrD + y * ws + x * nChan)[2] = red;
                        }
                    }
                }
            }
        }

        public static void Scale(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float scaleFactor)
        {

            unsafe
            {

                MIplImage mAux = imgCopy.MIplImage;
                MIplImage mResult = img.MIplImage;

                byte* dataPtrO = (byte*)mAux.imageData.ToPointer(); // pointer to the original image
                byte* dataPtrD = (byte*)mResult.imageData.ToPointer(); // pointer to the duplicate image
                byte blue, green, red;

                int width = img.Width;
                int height = img.Height;
                int nChan = mAux.nChannels; // number of channels - 3
                int w = mAux.width;
                int ws = mAux.widthStep;
                int x, y, xO, yO;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            xO = (int)Math.Round(x / scaleFactor);
                            yO = (int)Math.Round(y / scaleFactor);

                            if (xO < 0 || yO < 0 || xO >= width || yO >= height)
                            {
                                blue = 0;
                                green = 0;
                                red = 0;
                            }
                            else
                            {
                                // absolute address
                                blue = (byte)(dataPtrO + yO * ws + xO * nChan)[0];
                                green = (byte)(dataPtrO + yO * ws + xO * nChan)[1];
                                red = (byte)(dataPtrO + yO * ws + xO * nChan)[2];
                            }

                            (dataPtrD + y * ws + x * nChan)[0] = blue;
                            (dataPtrD + y * ws + x * nChan)[1] = green;
                            (dataPtrD + y * ws + x * nChan)[2] = red;
                        }
                    }
                }
            }
        }
        public static void Scale_point_xy(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float scaleFactor, int xCenter, int yCenter)
        {

            unsafe
            {

                MIplImage mAux = imgCopy.MIplImage;
                MIplImage mResult = img.MIplImage;

                byte* dataPtrO = (byte*)mAux.imageData.ToPointer(); // pointer to the original image
                byte* dataPtrD = (byte*)mResult.imageData.ToPointer(); // pointer to the duplicate image    
                byte blue, green, red;

                int width = img.Width;
                int height = img.Height;
                int nChan = mAux.nChannels; // number of channels - 3
                int w = mAux.width;
                int ws = mAux.widthStep;
                int x, y, xO, yO;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            xO = (int)Math.Round((x - width / 2) / scaleFactor + xCenter);
                            yO = (int)Math.Round((y - height / 2) / scaleFactor + yCenter);

                            if (xO < 0 || yO < 0 || xO >= width || yO >= height)
                            {
                                blue = 0;
                                green = 0;
                                red = 0;
                            }
                            else
                            {
                                // absolute address
                                blue = (byte)(dataPtrO + yO * ws + xO * nChan)[0];
                                green = (byte)(dataPtrO + yO * ws + xO * nChan)[1];
                                red = (byte)(dataPtrO + yO * ws + xO * nChan)[2];
                            }

                            (dataPtrD + y * ws + x * nChan)[0] = blue;
                            (dataPtrD + y * ws + x * nChan)[1] = green;
                            (dataPtrD + y * ws + x * nChan)[2] = red;
                        }
                    }
                }
            }
        }

        public static void Mean(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {

            unsafe
            {
                MIplImage mAux = imgCopy.MIplImage;
                MIplImage mResult = img.MIplImage;

                byte* dataPtrO = (byte*)mAux.imageData.ToPointer();
                byte* dataPtrD = (byte*)mResult.imageData.ToPointer();

                int width = img.Width;
                int height = img.Height;
                int nChan = mAux.nChannels;
                int w = mAux.width;
                int ws = mAux.widthStep;
                int padding = ws - nChan * w;
                int x, y;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            if (x == 0 && y == 0) //top left corner
                            {
                                dataPtrD[0] = (byte)(Math.Round(((int)dataPtrO[0] * 4 + (dataPtrO + nChan)[0] * 2 + (dataPtrO + ws)[0] * 2 + (dataPtrO + ws + nChan)[0]) / 9.0));
                                dataPtrD[1] = (byte)(Math.Round(((int)dataPtrO[1] * 4 + (dataPtrO + nChan)[1] * 2 + (dataPtrO + ws)[1] * 2 + (dataPtrO + ws + nChan)[1]) / 9.0));
                                dataPtrD[2] = (byte)(Math.Round(((int)dataPtrO[2] * 4 + (dataPtrO + nChan)[2] * 2 + (dataPtrO + ws)[2] * 2 + (dataPtrO + ws + nChan)[2]) / 9.0));
                            }
                            else if (x == width - 1 && y == 0) //top right corner
                            {
                                dataPtrD[0] = (byte)(Math.Round(((int)dataPtrO[0] * 4 + (dataPtrO - nChan)[0] * 2 + (dataPtrO + ws)[0] * 2 + (dataPtrO + ws - nChan)[0]) / 9.0));
                                dataPtrD[1] = (byte)(Math.Round(((int)dataPtrO[1] * 4 + (dataPtrO - nChan)[1] * 2 + (dataPtrO + ws)[1] * 2 + (dataPtrO + ws - nChan)[1]) / 9.0));
                                dataPtrD[2] = (byte)(Math.Round(((int)dataPtrO[2] * 4 + (dataPtrO - nChan)[2] * 2 + (dataPtrO + ws)[2] * 2 + (dataPtrO + ws - nChan)[2]) / 9.0));
                            }
                            else if (x == 0 && y == height - 1) //bottom left corner
                            {
                                dataPtrD[0] = (byte)(Math.Round(((int)dataPtrO[0] * 4 + (dataPtrO + nChan)[0] * 2 + (dataPtrO - ws)[0] * 2 + (dataPtrO - ws + nChan)[0]) / 9.0));
                                dataPtrD[1] = (byte)(Math.Round(((int)dataPtrO[1] * 4 + (dataPtrO + nChan)[1] * 2 + (dataPtrO - ws)[1] * 2 + (dataPtrO - ws + nChan)[1]) / 9.0));
                                dataPtrD[2] = (byte)(Math.Round(((int)dataPtrO[2] * 4 + (dataPtrO + nChan)[2] * 2 + (dataPtrO - ws)[2] * 2 + (dataPtrO - ws + nChan)[2]) / 9.0));
                            }
                            else if (x == width - 1 && y == height - 1) //bottom right corner
                            {
                                dataPtrD[0] = (byte)(Math.Round(((int)dataPtrO[0] * 4 + (dataPtrO - nChan)[0] * 2 + (dataPtrO - ws)[0] * 2 + (dataPtrO - ws - nChan)[0]) / 9.0));
                                dataPtrD[1] = (byte)(Math.Round(((int)dataPtrO[1] * 4 + (dataPtrO - nChan)[1] * 2 + (dataPtrO - ws)[1] * 2 + (dataPtrO - ws - nChan)[1]) / 9.0));
                                dataPtrD[2] = (byte)(Math.Round(((int)dataPtrO[2] * 4 + (dataPtrO - nChan)[2] * 2 + (dataPtrO - ws)[2] * 2 + (dataPtrO - ws - nChan)[2]) / 9.0));
                            }
                            else if (y == 0) // top margin
                            {
                                dataPtrD[0] = (byte)(Math.Round(((int)dataPtrO[0] * 2 + (dataPtrO - nChan)[0] * 2 + (dataPtrO + nChan)[0] * 2 + (dataPtrO + ws - nChan)[0] + (dataPtrO + ws)[0] + (dataPtrO + ws + nChan)[0]) / 9.0));
                                dataPtrD[1] = (byte)(Math.Round(((int)dataPtrO[1] * 2 + (dataPtrO - nChan)[1] * 2 + (dataPtrO + nChan)[1] * 2 + (dataPtrO + ws - nChan)[1] + (dataPtrO + ws)[1] + (dataPtrO + ws + nChan)[1]) / 9.0));
                                dataPtrD[2] = (byte)(Math.Round(((int)dataPtrO[2] * 2 + (dataPtrO - nChan)[2] * 2 + (dataPtrO + nChan)[2] * 2 + (dataPtrO + ws - nChan)[2] + (dataPtrO + ws)[2] + (dataPtrO + ws + nChan)[2]) / 9.0));
                            }
                            else if (y == height - 1) // bottom margin
                            {
                                dataPtrD[0] = (byte)(Math.Round(((int)dataPtrO[0] * 2 + (dataPtrO - nChan)[0] * 2 + (dataPtrO + nChan)[0] * 2 + (dataPtrO - ws - nChan)[0] + (dataPtrO - ws)[0] + (dataPtrO - ws + nChan)[0]) / 9.0));
                                dataPtrD[1] = (byte)(Math.Round(((int)dataPtrO[1] * 2 + (dataPtrO - nChan)[1] * 2 + (dataPtrO + nChan)[1] * 2 + (dataPtrO - ws - nChan)[1] + (dataPtrO - ws)[1] + (dataPtrO - ws + nChan)[1]) / 9.0));
                                dataPtrD[2] = (byte)(Math.Round(((int)dataPtrO[2] * 2 + (dataPtrO - nChan)[2] * 2 + (dataPtrO + nChan)[2] * 2 + (dataPtrO - ws - nChan)[2] + (dataPtrO - ws)[2] + (dataPtrO - ws + nChan)[2]) / 9.0));
                            }
                            else if (x == 0) // left margin
                            {
                                dataPtrD[0] = (byte)(Math.Round(((int)dataPtrO[0] * 2 + (dataPtrO - ws)[0] * 2 + (dataPtrO + ws)[0] * 2 + (dataPtrO + ws + nChan)[0] + (dataPtrO + nChan)[0] + (dataPtrO - ws + nChan)[0]) / 9.0));
                                dataPtrD[1] = (byte)(Math.Round(((int)dataPtrO[1] * 2 + (dataPtrO - ws)[1] * 2 + (dataPtrO + ws)[1] * 2 + (dataPtrO + ws + nChan)[1] + (dataPtrO + nChan)[1] + (dataPtrO - ws + nChan)[1]) / 9.0));
                                dataPtrD[2] = (byte)(Math.Round(((int)dataPtrO[2] * 2 + (dataPtrO - ws)[2] * 2 + (dataPtrO + ws)[2] * 2 + (dataPtrO + ws + nChan)[2] + (dataPtrO + nChan)[2] + (dataPtrO - ws + nChan)[2]) / 9.0));
                            }
                            else if (x == width - 1) //right margin
                            {
                                dataPtrD[0] = (byte)(Math.Round(((int)dataPtrO[0] * 2 + (dataPtrO - ws)[0] * 2 + (dataPtrO + ws)[0] * 2 + (dataPtrO + ws - nChan)[0] + (dataPtrO - nChan)[0] + (dataPtrO - ws - nChan)[0]) / 9.0));
                                dataPtrD[1] = (byte)(Math.Round(((int)dataPtrO[1] * 2 + (dataPtrO - ws)[1] * 2 + (dataPtrO + ws)[1] * 2 + (dataPtrO + ws - nChan)[1] + (dataPtrO - nChan)[1] + (dataPtrO - ws - nChan)[1]) / 9.0));
                                dataPtrD[2] = (byte)(Math.Round(((int)dataPtrO[2] * 2 + (dataPtrO - ws)[2] * 2 + (dataPtrO + ws)[2] * 2 + (dataPtrO + ws - nChan)[2] + (dataPtrO - nChan)[2] + (dataPtrO - ws - nChan)[2]) / 9.0));
                            }
                            else //center pixels
                            {
                                dataPtrD[0] = (byte)(Math.Round(((int)(dataPtrO - ws - nChan)[0] + (dataPtrO - ws)[0] + (dataPtrO - ws + nChan)[0] + (dataPtrO - nChan)[0] + dataPtrO[0] + (dataPtrO + nChan)[0] + (dataPtrO + ws - nChan)[0] + (dataPtrO + ws)[0] + (dataPtrO + ws + nChan)[0]) / 9.0));
                                dataPtrD[1] = (byte)(Math.Round(((int)(dataPtrO - ws - nChan)[1] + (dataPtrO - ws)[1] + (dataPtrO - ws + nChan)[1] + (dataPtrO - nChan)[1] + dataPtrO[1] + (dataPtrO + nChan)[1] + (dataPtrO + ws - nChan)[1] + (dataPtrO + ws)[1] + (dataPtrO + ws + nChan)[1]) / 9.0));
                                dataPtrD[2] = (byte)(Math.Round(((int)(dataPtrO - ws - nChan)[2] + (dataPtrO - ws)[2] + (dataPtrO - ws + nChan)[2] + (dataPtrO - nChan)[2] + dataPtrO[2] + (dataPtrO + nChan)[2] + (dataPtrO + ws - nChan)[2] + (dataPtrO + ws)[2] + (dataPtrO + ws + nChan)[2]) / 9.0));
                            }

                            dataPtrO += nChan;
                            dataPtrD += nChan;
                        }

                        dataPtrO += padding;
                        dataPtrD += padding;
                    }
                }
            }
        }

        public static void NonUniform(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float[,] matrix, float matrixWeight)
        {

            unsafe
            {
                MIplImage mAux = imgCopy.MIplImage;
                MIplImage mResult = img.MIplImage;

                byte* dataPtrO = (byte*)mAux.imageData.ToPointer();
                byte* dataPtrD = (byte*)mResult.imageData.ToPointer();

                double blue, green, red;

                int width = img.Width;
                int height = img.Height;
                int nChan = mAux.nChannels;
                int w = mAux.width;
                int ws = mAux.widthStep;
                int padding = ws - nChan * w;
                int x, y;

                if (nChan == 3)
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            if (x == 0 && y == 0) //top left corner
                            {
                                blue = Math.Round(((int)(dataPtrO[0] * matrix[0, 0]) + dataPtrO[0] * matrix[0, 1] + (dataPtrO + nChan)[0] * matrix[0, 2] + dataPtrO[0] * matrix[1, 0] + dataPtrO[0] * matrix[1, 1] + (dataPtrO + nChan)[0] * matrix[1, 2] + (dataPtrO + ws)[0] * matrix[2, 0] + (dataPtrO + ws)[0] * matrix[2, 1] + (dataPtrO + ws + nChan)[0] * matrix[2, 2]) / matrixWeight);
                                green = Math.Round(((int)(dataPtrO[1] * matrix[0, 0]) + dataPtrO[1] * matrix[0, 1] + (dataPtrO + nChan)[1] * matrix[0, 2] + dataPtrO[1] * matrix[1, 0] + dataPtrO[1] * matrix[1, 1] + (dataPtrO + nChan)[1] * matrix[1, 2] + (dataPtrO + ws)[1] * matrix[2, 0] + (dataPtrO + ws)[1] * matrix[2, 1] + (dataPtrO + ws + nChan)[1] * matrix[2, 2]) / matrixWeight);
                                red = Math.Round(((int)(dataPtrO[2] * matrix[0, 0]) + dataPtrO[2] * matrix[0, 1] + (dataPtrO + nChan)[2] * matrix[0, 2] + dataPtrO[2] * matrix[1, 0] + dataPtrO[2] * matrix[1, 1] + (dataPtrO + nChan)[2] * matrix[1, 2] + (dataPtrO + ws)[2] * matrix[2, 0] + (dataPtrO + ws)[2] * matrix[2, 1] + (dataPtrO + ws + nChan)[2] * matrix[2, 2]) / matrixWeight);
                            }
                            else if (x == width - 1 && y == 0) //top right corner
                            {
                                blue = Math.Round(((int)((dataPtrO - nChan)[0] * matrix[0, 0]) + dataPtrO[0] * matrix[0, 1] + dataPtrO[0] * matrix[0, 2] + (dataPtrO - nChan)[0] * matrix[1, 0] + dataPtrO[0] * matrix[1, 1] + dataPtrO[0] * matrix[1, 2] + (dataPtrO + ws - nChan)[0] * matrix[2, 0] + (dataPtrO + ws)[0] * matrix[2, 1] + (dataPtrO + ws)[0] * matrix[2, 2]) / matrixWeight);
                                green = Math.Round(((int)((dataPtrO - nChan)[1] * matrix[0, 0]) + dataPtrO[1] * matrix[0, 1] + dataPtrO[1] * matrix[0, 2] + (dataPtrO - nChan)[1] * matrix[1, 0] + dataPtrO[1] * matrix[1, 1] + dataPtrO[1] * matrix[1, 2] + (dataPtrO + ws - nChan)[1] * matrix[2, 0] + (dataPtrO + ws)[1] * matrix[2, 1] + (dataPtrO + ws)[1] * matrix[2, 2]) / matrixWeight);
                                red = Math.Round(((int)((dataPtrO - nChan)[2] * matrix[0, 0]) + dataPtrO[2] * matrix[0, 1] + dataPtrO[2] * matrix[0, 2] + (dataPtrO - nChan)[2] * matrix[1, 0] + dataPtrO[2] * matrix[1, 1] + dataPtrO[2] * matrix[1, 2] + (dataPtrO + ws - nChan)[2] * matrix[2, 0] + (dataPtrO + ws)[2] * matrix[2, 1] + (dataPtrO + ws)[2] * matrix[2, 2]) / matrixWeight);
                            }
                            else if (x == 0 && y == height - 1) //bottom left corner
                            {
                                blue = Math.Round(((int)((dataPtrO - ws)[0] * matrix[0, 0]) + (dataPtrO - ws)[0] * matrix[0, 1] + (dataPtrO - ws + nChan)[0] * matrix[0, 2] + dataPtrO[0] * matrix[1, 0] + dataPtrO[0] * matrix[1, 1] + (dataPtrO + nChan)[0] * matrix[1, 2] + dataPtrO[0] * matrix[2, 0] + dataPtrO[0] * matrix[2, 1] + (dataPtrO + nChan)[0] * matrix[2, 2]) / matrixWeight);
                                green = Math.Round(((int)((dataPtrO - ws)[1] * matrix[0, 0]) + (dataPtrO - ws)[1] * matrix[0, 1] + (dataPtrO - ws + nChan)[1] * matrix[0, 2] + dataPtrO[1] * matrix[1, 0] + dataPtrO[1] * matrix[1, 1] + (dataPtrO + nChan)[1] * matrix[1, 2] + dataPtrO[1] * matrix[2, 0] + dataPtrO[1] * matrix[2, 1] + (dataPtrO + nChan)[1] * matrix[2, 2]) / matrixWeight);
                                red = Math.Round(((int)((dataPtrO - ws)[2] * matrix[0, 0]) + (dataPtrO - ws)[2] * matrix[0, 1] + (dataPtrO - ws + nChan)[2] * matrix[0, 2] + dataPtrO[2] * matrix[1, 0] + dataPtrO[2] * matrix[1, 1] + (dataPtrO + nChan)[2] * matrix[1, 2] + dataPtrO[2] * matrix[2, 0] + dataPtrO[2] * matrix[2, 1] + (dataPtrO + nChan)[2] * matrix[2, 2]) / matrixWeight);
                            }
                            else if (x == width - 1 && y == height - 1) //bottom right corner
                            {
                                blue = Math.Round(((int)((dataPtrO - ws - nChan)[0] * matrix[0, 0]) + (dataPtrO - ws)[0] * matrix[0, 1] + (dataPtrO - ws)[0] * matrix[0, 2] + (dataPtrO - nChan)[0] * matrix[1, 0] + dataPtrO[0] * matrix[1, 1] + dataPtrO[0] * matrix[1, 2] + (dataPtrO - nChan)[0] * matrix[2, 0] + dataPtrO[0] * matrix[2, 1] + dataPtrO[0] * matrix[2, 2]) / matrixWeight);
                                green = Math.Round(((int)((dataPtrO - ws - nChan)[1] * matrix[0, 0]) + (dataPtrO - ws)[1] * matrix[0, 1] + (dataPtrO - ws)[1] * matrix[0, 2] + (dataPtrO - nChan)[1] * matrix[1, 0] + dataPtrO[1] * matrix[1, 1] + dataPtrO[1] * matrix[1, 2] + (dataPtrO - nChan)[1] * matrix[2, 0] + dataPtrO[1] * matrix[2, 1] + dataPtrO[1] * matrix[2, 2]) / matrixWeight);
                                red = Math.Round(((int)((dataPtrO - ws - nChan)[2] * matrix[0, 0]) + (dataPtrO - ws)[2] * matrix[0, 1] + (dataPtrO - ws)[2] * matrix[0, 2] + (dataPtrO - nChan)[2] * matrix[1, 0] + dataPtrO[2] * matrix[1, 1] + dataPtrO[2] * matrix[1, 2] + (dataPtrO - nChan)[2] * matrix[2, 0] + dataPtrO[2] * matrix[2, 1] + dataPtrO[2] * matrix[2, 2]) / matrixWeight);
                            }
                            else if (y == 0) // top margin
                            {
                                blue = Math.Round(((int)((dataPtrO - nChan)[0] * matrix[0, 0]) + dataPtrO[0] * matrix[0, 1] + (dataPtrO + nChan)[0] * matrix[0, 2] + (dataPtrO - nChan)[0] * matrix[1, 0] + dataPtrO[0] * matrix[1, 1] + (dataPtrO + nChan)[0] * matrix[1, 2] + (dataPtrO + ws - nChan)[0] * matrix[2, 0] + (dataPtrO + ws)[0] * matrix[2, 1] + (dataPtrO + ws + nChan)[0] * matrix[2, 2]) / matrixWeight);
                                green = Math.Round(((int)((dataPtrO - nChan)[1] * matrix[0, 0]) + dataPtrO[1] * matrix[0, 1] + (dataPtrO + nChan)[1] * matrix[0, 2] + (dataPtrO - nChan)[1] * matrix[1, 0] + dataPtrO[1] * matrix[1, 1] + (dataPtrO + nChan)[1] * matrix[1, 2] + (dataPtrO + ws - nChan)[1] * matrix[2, 0] + (dataPtrO + ws)[1] * matrix[2, 1] + (dataPtrO + ws + nChan)[1] * matrix[2, 2]) / matrixWeight);
                                red = Math.Round(((int)((dataPtrO - nChan)[2] * matrix[0, 0]) + dataPtrO[2] * matrix[0, 1] + (dataPtrO + nChan)[2] * matrix[0, 2] + (dataPtrO - nChan)[2] * matrix[1, 0] + dataPtrO[2] * matrix[1, 1] + (dataPtrO + nChan)[2] * matrix[1, 2] + (dataPtrO + ws - nChan)[2] * matrix[2, 0] + (dataPtrO + ws)[2] * matrix[2, 1] + (dataPtrO + ws + nChan)[2] * matrix[2, 2]) / matrixWeight);
                            }
                            else if (y == height - 1) // bottom margin
                            {
                                blue = Math.Round(((int)((dataPtrO - ws - nChan)[0] * matrix[0, 0]) + (dataPtrO - ws)[0] * matrix[0, 1] + (dataPtrO - ws + nChan)[0] * matrix[0, 2] + (dataPtrO - nChan)[0] * matrix[1, 0] + dataPtrO[0] * matrix[1, 1] + dataPtrO[0] * matrix[1, 2] + (dataPtrO - nChan)[0] * matrix[2, 0] + dataPtrO[0] * matrix[2, 1] + (dataPtrO + nChan)[0] * matrix[2, 2]) / matrixWeight);
                                green = Math.Round(((int)((dataPtrO - ws - nChan)[1] * matrix[0, 0]) + (dataPtrO - ws)[1] * matrix[0, 1] + (dataPtrO - ws + nChan)[1] * matrix[0, 2] + (dataPtrO - nChan)[1] * matrix[1, 0] + dataPtrO[1] * matrix[1, 1] + dataPtrO[1] * matrix[1, 2] + (dataPtrO - nChan)[1] * matrix[2, 0] + dataPtrO[1] * matrix[2, 1] + (dataPtrO + nChan)[1] * matrix[2, 2]) / matrixWeight);
                                red = Math.Round(((int)((dataPtrO - ws - nChan)[2] * matrix[0, 0]) + (dataPtrO - ws)[2] * matrix[0, 1] + (dataPtrO - ws + nChan)[2] * matrix[0, 2] + (dataPtrO - nChan)[2] * matrix[1, 0] + dataPtrO[2] * matrix[1, 1] + dataPtrO[2] * matrix[1, 2] + (dataPtrO - nChan)[2] * matrix[2, 0] + dataPtrO[2] * matrix[2, 1] + (dataPtrO + nChan)[2] * matrix[2, 2]) / matrixWeight);
                            }
                            else if (x == 0) // left margin
                            {
                                blue = Math.Round(((int)((dataPtrO - ws)[0] * matrix[0, 0]) + (dataPtrO - ws)[0] * matrix[0, 1] + (dataPtrO - ws + nChan)[0] * matrix[0, 2] + dataPtrO[0] * matrix[1, 0] + dataPtrO[0] * matrix[1, 1] + (dataPtrO + nChan)[0] * matrix[1, 2] + (dataPtrO + ws)[0] * matrix[2, 0] + (dataPtrO + ws)[0] * matrix[2, 1] + (dataPtrO + ws + nChan)[0] * matrix[2, 2]) / matrixWeight);
                                green = Math.Round(((int)((dataPtrO - ws)[1] * matrix[0, 0]) + (dataPtrO - ws)[1] * matrix[0, 1] + (dataPtrO - ws + nChan)[1] * matrix[0, 2] + dataPtrO[1] * matrix[1, 0] + dataPtrO[1] * matrix[1, 1] + (dataPtrO + nChan)[1] * matrix[1, 2] + (dataPtrO + ws)[1] * matrix[2, 0] + (dataPtrO + ws)[1] * matrix[2, 1] + (dataPtrO + ws + nChan)[1] * matrix[2, 2]) / matrixWeight);
                                red = Math.Round(((int)((dataPtrO - ws)[2] * matrix[0, 0]) + (dataPtrO - ws)[2] * matrix[0, 1] + (dataPtrO - ws + nChan)[2] * matrix[0, 2] + dataPtrO[2] * matrix[1, 0] + dataPtrO[2] * matrix[1, 1] + (dataPtrO + nChan)[2] * matrix[1, 2] + (dataPtrO + ws)[2] * matrix[2, 0] + (dataPtrO + ws)[2] * matrix[2, 1] + (dataPtrO + ws + nChan)[2] * matrix[2, 2]) / matrixWeight);
                            }
                            else if (x == width - 1) //right margin
                            {
                                blue = Math.Round(((int)((dataPtrO - ws - nChan)[0] * matrix[0, 0]) + (dataPtrO - ws)[0] * matrix[0, 1] + (dataPtrO - ws)[0] * matrix[0, 2] + (dataPtrO - nChan)[0] * matrix[1, 0] + dataPtrO[0] * matrix[1, 1] + dataPtrO[0] * matrix[1, 2] + (dataPtrO + ws - nChan)[0] * matrix[2, 0] + (dataPtrO + ws)[0] * matrix[2, 1] + (dataPtrO + ws)[0] * matrix[2, 2]) / matrixWeight);
                                green = Math.Round(((int)((dataPtrO - ws - nChan)[1] * matrix[0, 0]) + (dataPtrO - ws)[1] * matrix[0, 1] + (dataPtrO - ws)[1] * matrix[0, 2] + (dataPtrO - nChan)[1] * matrix[1, 0] + dataPtrO[1] * matrix[1, 1] + dataPtrO[1] * matrix[1, 2] + (dataPtrO + ws - nChan)[1] * matrix[2, 0] + (dataPtrO + ws)[1] * matrix[2, 1] + (dataPtrO + ws)[1] * matrix[2, 2]) / matrixWeight);
                                red = Math.Round(((int)((dataPtrO - ws - nChan)[2] * matrix[0, 0]) + (dataPtrO - ws)[2] * matrix[0, 1] + (dataPtrO - ws)[2] * matrix[0, 2] + (dataPtrO - nChan)[2] * matrix[1, 0] + dataPtrO[2] * matrix[1, 1] + dataPtrO[2] * matrix[1, 2] + (dataPtrO + ws - nChan)[2] * matrix[2, 0] + (dataPtrO + ws)[2] * matrix[2, 1] + (dataPtrO + ws)[2] * matrix[2, 2]) / matrixWeight);
                            }
                            else //center pixels
                            {
                                blue = Math.Round(((int)((dataPtrO - ws - nChan)[0] * matrix[0, 0]) + (dataPtrO - ws)[0] * matrix[0, 1] + (dataPtrO - ws + nChan)[0] * matrix[0, 2] + (dataPtrO - nChan)[0] * matrix[1, 0] + dataPtrO[0] * matrix[1, 1] + (dataPtrO + nChan)[0] * matrix[1, 2] + (dataPtrO + ws - nChan)[0] * matrix[2, 0] + (dataPtrO + ws)[0] * matrix[2, 1] + (dataPtrO + ws + nChan)[0] * matrix[2, 2]) / matrixWeight);
                                green = Math.Round(((int)((dataPtrO - ws - nChan)[1] * matrix[0, 0]) + (dataPtrO - ws)[1] * matrix[0, 1] + (dataPtrO - ws + nChan)[1] * matrix[0, 2] + (dataPtrO - nChan)[1] * matrix[1, 0] + dataPtrO[1] * matrix[1, 1] + (dataPtrO + nChan)[1] * matrix[1, 2] + (dataPtrO + ws - nChan)[1] * matrix[2, 0] + (dataPtrO + ws)[1] * matrix[2, 1] + (dataPtrO + ws + nChan)[1] * matrix[2, 2]) / matrixWeight);
                                red = Math.Round(((int)((dataPtrO - ws - nChan)[2] * matrix[0, 0]) + (dataPtrO - ws)[2] * matrix[0, 1] + (dataPtrO - ws + nChan)[2] * matrix[0, 2] + (dataPtrO - nChan)[2] * matrix[1, 0] + dataPtrO[2] * matrix[1, 1] + (dataPtrO + nChan)[2] * matrix[1, 2] + (dataPtrO + ws - nChan)[2] * matrix[2, 0] + (dataPtrO + ws)[2] * matrix[2, 1] + (dataPtrO + ws + nChan)[2] * matrix[2, 2]) / matrixWeight);
                            }

                            if (blue > 255) blue = 255;
                            if (green > 255) green = 255;
                            if (red > 255) red = 255;

                            if (blue < 0) blue = 0;
                            if (green < 0) green = 0;
                            if (red < 0) red = 0;

                            dataPtrD[0] = (byte)blue;
                            dataPtrD[1] = (byte)green;
                            dataPtrD[2] = (byte)red;

                            dataPtrO += nChan;
                            dataPtrD += nChan;
                        }

                        dataPtrO += padding;
                        dataPtrD += padding;
                    }
                }
            }
        }
        public static void Sobel(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {

            unsafe
            {
                MIplImage mAux = imgCopy.MIplImage;
                MIplImage mResult = img.MIplImage;

                byte* dataPtrO = (byte*)mAux.imageData.ToPointer();
                byte* dataPtrD = (byte*)mResult.imageData.ToPointer();

                double blue, green, red;

                int width = img.Width;
                int height = img.Height;
                int nChan = mAux.nChannels;
                int w = mAux.width;
                int ws = mAux.widthStep;
                int padding = ws - nChan * w;
                int x, y;

                if (nChan == 3)
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            if (x == 0 && y == 0) //top left corner
                            {
                                blue = Math.Round((double)Math.Abs(dataPtrO[0] + dataPtrO[0] * 2 + (dataPtrO + ws)[0] - (dataPtrO + nChan)[0] - (dataPtrO + nChan)[0] * 2 - (dataPtrO + ws + nChan)[0]) + Math.Abs((dataPtrO + ws)[0] + (dataPtrO + ws)[0] * 2 + (dataPtrO + ws + nChan)[0] - dataPtrO[0] - dataPtrO[0] * 2 - (dataPtrO + nChan)[0]));
                                green = Math.Round((double)Math.Abs(dataPtrO[1] + dataPtrO[1] * 2 + (dataPtrO + ws)[1] - (dataPtrO + nChan)[1] - (dataPtrO + nChan)[1] * 2 - (dataPtrO + ws + nChan)[1]) + Math.Abs((dataPtrO + ws)[1] + (dataPtrO + ws)[1] * 2 + (dataPtrO + ws + nChan)[1] - dataPtrO[1] - dataPtrO[1] * 2 - (dataPtrO + nChan)[1]));
                                red = Math.Round((double)Math.Abs(dataPtrO[2] + dataPtrO[2] * 2 + (dataPtrO + ws)[2] - (dataPtrO + nChan)[2] - (dataPtrO + nChan)[2] * 2 - (dataPtrO + ws + nChan)[2]) + Math.Abs((dataPtrO + ws)[2] + (dataPtrO + ws)[2] * 2 + (dataPtrO + ws + nChan)[2] - dataPtrO[2] - dataPtrO[2] * 2 - (dataPtrO + nChan)[2]));
                            }
                            else if (x == width - 1 && y == 0) //top right corner
                            {
                                blue = Math.Round((double)Math.Abs((dataPtrO - nChan)[0] + (dataPtrO - nChan)[0] * 2 + (dataPtrO + ws - nChan)[0] - dataPtrO[0] - dataPtrO[0] * 2 - (dataPtrO + ws)[0]) + Math.Abs((dataPtrO + ws - nChan)[0] + (dataPtrO + ws)[0] * 2 + (dataPtrO + ws)[0] - (dataPtrO - nChan)[0] - dataPtrO[0] * 2 - dataPtrO[0]));
                                green = Math.Round((double)Math.Abs((dataPtrO - nChan)[1] + (dataPtrO - nChan)[1] * 2 + (dataPtrO + ws - nChan)[1] - dataPtrO[1] - dataPtrO[1] * 2 - (dataPtrO + ws)[1]) + Math.Abs((dataPtrO + ws - nChan)[1] + (dataPtrO + ws)[1] * 2 + (dataPtrO + ws)[1] - (dataPtrO - nChan)[1] - dataPtrO[1] * 2 - dataPtrO[1]));
                                red = Math.Round((double)Math.Abs((dataPtrO - nChan)[2] + (dataPtrO - nChan)[2] * 2 + (dataPtrO + ws - nChan)[2] - dataPtrO[2] - dataPtrO[2] * 2 - (dataPtrO + ws)[2]) + Math.Abs((dataPtrO + ws - nChan)[2] + (dataPtrO + ws)[2] * 2 + (dataPtrO + ws)[2] - (dataPtrO - nChan)[2] - dataPtrO[2] * 2 - dataPtrO[2]));
                            }
                            else if (x == 0 && y == height - 1) //bottom left corner
                            {
                                blue = Math.Round((double)Math.Abs((dataPtrO - ws)[0] + dataPtrO[0] * 2 + dataPtrO[0] - (dataPtrO + nChan - ws)[0] - (dataPtrO + nChan)[0] * 2 - (dataPtrO + nChan)[0]) + Math.Abs(dataPtrO[0] + dataPtrO[0] * 2 + (dataPtrO + nChan)[0] - (dataPtrO - ws)[0] - (dataPtrO - ws)[0] * 2 - (dataPtrO - ws + nChan)[0]));
                                green = Math.Round((double)Math.Abs((dataPtrO - ws)[1] + dataPtrO[1] * 2 + dataPtrO[1] - (dataPtrO + nChan - ws)[1] - (dataPtrO + nChan)[1] * 2 - (dataPtrO + nChan)[1]) + Math.Abs(dataPtrO[1] + dataPtrO[1] * 2 + (dataPtrO + nChan)[1] - (dataPtrO - ws)[1] - (dataPtrO - ws)[1] * 2 - (dataPtrO - ws + nChan)[1]));
                                red = Math.Round((double)Math.Abs((dataPtrO - ws)[2] + dataPtrO[2] * 2 + dataPtrO[2] - (dataPtrO + nChan - ws)[2] - (dataPtrO + nChan)[2] * 2 - (dataPtrO + nChan)[2]) + Math.Abs(dataPtrO[2] + dataPtrO[2] * 2 + (dataPtrO + nChan)[2] - (dataPtrO - ws)[2] - (dataPtrO - ws)[2] * 2 - (dataPtrO - ws + nChan)[2]));
                            }
                            else if (x == width - 1 && y == height - 1) //bottom right corner
                            {
                                blue = Math.Round((double)Math.Abs((dataPtrO - ws - nChan)[0] + (dataPtrO - nChan)[0] * 2 + (dataPtrO - nChan)[0] - (dataPtrO - ws)[0] - dataPtrO[0] * 2 - dataPtrO[0]) + Math.Abs((dataPtrO - nChan)[0] + dataPtrO[0] * 2 + dataPtrO[0] - (dataPtrO - ws - nChan)[0] - (dataPtrO - ws)[0] * 2 - (dataPtrO - ws)[0]));
                                green = Math.Round((double)Math.Abs((dataPtrO - ws - nChan)[1] + (dataPtrO - nChan)[1] * 2 + (dataPtrO - nChan)[1] - (dataPtrO - ws)[1] - dataPtrO[1] * 2 - dataPtrO[1]) + Math.Abs((dataPtrO - nChan)[1] + dataPtrO[1] * 2 + dataPtrO[1] - (dataPtrO - ws - nChan)[1] - (dataPtrO - ws)[1] * 2 - (dataPtrO - ws)[1]));
                                red = Math.Round((double)Math.Abs((dataPtrO - ws - nChan)[2] + (dataPtrO - nChan)[2] * 2 + (dataPtrO - nChan)[2] - (dataPtrO - ws)[2] - dataPtrO[2] * 2 - dataPtrO[2]) + Math.Abs((dataPtrO - nChan)[2] + dataPtrO[2] * 2 + dataPtrO[2] - (dataPtrO - ws - nChan)[2] - (dataPtrO - ws)[2] * 2 - (dataPtrO - ws)[2]));
                            }
                            else if (y == 0) // top margin
                            {
                                blue = Math.Round((double)Math.Abs((dataPtrO - nChan)[0] + (dataPtrO - nChan)[0] * 2 + (dataPtrO + ws - nChan)[0] - (dataPtrO + nChan)[0] - (dataPtrO + nChan)[0] * 2 - (dataPtrO + ws + nChan)[0]) + Math.Abs((dataPtrO + ws - nChan)[0] + (dataPtrO + ws)[0] * 2 + (dataPtrO + ws + nChan)[0] - (dataPtrO - nChan)[0] - dataPtrO[0] * 2 - (dataPtrO + nChan)[0]));
                                green = Math.Round((double)Math.Abs((dataPtrO - nChan)[1] + (dataPtrO - nChan)[1] * 2 + (dataPtrO + ws - nChan)[1] - (dataPtrO + nChan)[1] - (dataPtrO + nChan)[1] * 2 - (dataPtrO + ws + nChan)[1]) + Math.Abs((dataPtrO + ws - nChan)[1] + (dataPtrO + ws)[1] * 2 + (dataPtrO + ws + nChan)[1] - (dataPtrO - nChan)[1] - dataPtrO[1] * 2 - (dataPtrO + nChan)[1]));
                                red = Math.Round((double)Math.Abs((dataPtrO - nChan)[2] + (dataPtrO - nChan)[2] * 2 + (dataPtrO + ws - nChan)[2] - (dataPtrO + nChan)[2] - (dataPtrO + nChan)[2] * 2 - (dataPtrO + ws + nChan)[2]) + Math.Abs((dataPtrO + ws - nChan)[2] + (dataPtrO + ws)[2] * 2 + (dataPtrO + ws + nChan)[2] - (dataPtrO - nChan)[2] - dataPtrO[2] * 2 - (dataPtrO + nChan)[2]));
                            }
                            else if (y == height - 1) // bottom margin
                            {
                                blue = Math.Round((double)Math.Abs((dataPtrO - ws - nChan)[0] + (dataPtrO - nChan)[0] * 2 + (dataPtrO - nChan)[0] - (dataPtrO + nChan - ws)[0] - (dataPtrO + nChan)[0] * 2 - (dataPtrO + nChan)[0]) + Math.Abs((dataPtrO - nChan)[0] + dataPtrO[0] * 2 + (dataPtrO + nChan)[0] - (dataPtrO - ws - nChan)[0] - (dataPtrO - ws)[0] * 2 - (dataPtrO - ws + nChan)[0]));
                                green = Math.Round((double)Math.Abs((dataPtrO - ws - nChan)[1] + (dataPtrO - nChan)[1] * 2 + (dataPtrO - nChan)[1] - (dataPtrO + nChan - ws)[1] - (dataPtrO + nChan)[1] * 2 - (dataPtrO + nChan)[1]) + Math.Abs((dataPtrO - nChan)[1] + dataPtrO[1] * 2 + (dataPtrO + nChan)[1] - (dataPtrO - ws - nChan)[1] - (dataPtrO - ws)[1] * 2 - (dataPtrO - ws + nChan)[1]));
                                red = Math.Round((double)Math.Abs((dataPtrO - ws - nChan)[2] + (dataPtrO - nChan)[2] * 2 + (dataPtrO - nChan)[2] - (dataPtrO + nChan - ws)[2] - (dataPtrO + nChan)[2] * 2 - (dataPtrO + nChan)[2]) + Math.Abs((dataPtrO - nChan)[2] + dataPtrO[2] * 2 + (dataPtrO + nChan)[2] - (dataPtrO - ws - nChan)[2] - (dataPtrO - ws)[2] * 2 - (dataPtrO - ws + nChan)[2]));
                            }
                            else if (x == 0) // left margin
                            {
                                blue = Math.Round((double)Math.Abs((dataPtrO - ws)[0] + dataPtrO[0] * 2 + (dataPtrO + ws)[0] - (dataPtrO + nChan - ws)[0] - (dataPtrO + nChan)[0] * 2 - (dataPtrO + ws + nChan)[0]) + Math.Abs((dataPtrO + ws)[0] + (dataPtrO + ws)[0] * 2 + (dataPtrO + ws + nChan)[0] - (dataPtrO - ws)[0] - (dataPtrO - ws)[0] * 2 - (dataPtrO - ws + nChan)[0]));
                                green = Math.Round((double)Math.Abs((dataPtrO - ws)[1] + dataPtrO[1] * 2 + (dataPtrO + ws)[1] - (dataPtrO + nChan - ws)[1] - (dataPtrO + nChan)[1] * 2 - (dataPtrO + ws + nChan)[1]) + Math.Abs((dataPtrO + ws)[1] + (dataPtrO + ws)[1] * 2 + (dataPtrO + ws + nChan)[1] - (dataPtrO - ws)[1] - (dataPtrO - ws)[1] * 2 - (dataPtrO - ws + nChan)[1]));
                                red = Math.Round((double)Math.Abs((dataPtrO - ws)[2] + dataPtrO[2] * 2 + (dataPtrO + ws)[2] - (dataPtrO + nChan - ws)[2] - (dataPtrO + nChan)[2] * 2 - (dataPtrO + ws + nChan)[2]) + Math.Abs((dataPtrO + ws)[2] + (dataPtrO + ws)[2] * 2 + (dataPtrO + ws + nChan)[2] - (dataPtrO - ws)[2] - (dataPtrO - ws)[2] * 2 - (dataPtrO - ws + nChan)[2]));
                            }
                            else if (x == width - 1) //right margin
                            {
                                blue = Math.Round((double)Math.Abs((dataPtrO - ws - nChan)[0] + (dataPtrO - nChan)[0] * 2 + (dataPtrO + ws - nChan)[0] - (dataPtrO - ws)[0] - dataPtrO[0] * 2 - (dataPtrO + ws)[0]) + Math.Abs((dataPtrO + ws - nChan)[0] + (dataPtrO + ws)[0] * 2 + (dataPtrO + ws)[0] - (dataPtrO - ws - nChan)[0] - (dataPtrO - ws)[0] * 2 - (dataPtrO - ws)[0]));
                                green = Math.Round((double)Math.Abs((dataPtrO - ws - nChan)[1] + (dataPtrO - nChan)[1] * 2 + (dataPtrO + ws - nChan)[1] - (dataPtrO - ws)[1] - dataPtrO[1] * 2 - (dataPtrO + ws)[1]) + Math.Abs((dataPtrO + ws - nChan)[1] + (dataPtrO + ws)[1] * 2 + (dataPtrO + ws)[1] - (dataPtrO - ws - nChan)[1] - (dataPtrO - ws)[1] * 2 - (dataPtrO - ws)[1]));
                                red = Math.Round((double)Math.Abs((dataPtrO - ws - nChan)[2] + (dataPtrO - nChan)[2] * 2 + (dataPtrO + ws - nChan)[2] - (dataPtrO - ws)[2] - dataPtrO[2] * 2 - (dataPtrO + ws)[2]) + Math.Abs((dataPtrO + ws - nChan)[2] + (dataPtrO + ws)[2] * 2 + (dataPtrO + ws)[2] - (dataPtrO - ws - nChan)[2] - (dataPtrO - ws)[2] * 2 - (dataPtrO - ws)[2]));
                            }
                            else //center pixels
                            {
                                blue = Math.Round((double)Math.Abs((dataPtrO - ws - nChan)[0] + (dataPtrO - nChan)[0] * 2 + (dataPtrO + ws - nChan)[0] - (dataPtrO + nChan - ws)[0] - (dataPtrO + nChan)[0] * 2 - (dataPtrO + ws + nChan)[0]) + Math.Abs((dataPtrO + ws - nChan)[0] + (dataPtrO + ws)[0] * 2 + (dataPtrO + ws + nChan)[0] - (dataPtrO - ws - nChan)[0] - (dataPtrO - ws)[0] * 2 - (dataPtrO - ws + nChan)[0]));
                                green = Math.Round((double)Math.Abs((dataPtrO - ws - nChan)[1] + (dataPtrO - nChan)[1] * 2 + (dataPtrO + ws - nChan)[1] - (dataPtrO + nChan - ws)[1] - (dataPtrO + nChan)[1] * 2 - (dataPtrO + ws + nChan)[1]) + Math.Abs((dataPtrO + ws - nChan)[1] + (dataPtrO + ws)[1] * 2 + (dataPtrO + ws + nChan)[1] - (dataPtrO - ws - nChan)[1] - (dataPtrO - ws)[1] * 2 - (dataPtrO - ws + nChan)[1]));
                                red = Math.Round((double)Math.Abs((dataPtrO - ws - nChan)[2] + (dataPtrO - nChan)[2] * 2 + (dataPtrO + ws - nChan)[2] - (dataPtrO + nChan - ws)[2] - (dataPtrO + nChan)[2] * 2 - (dataPtrO + ws + nChan)[2]) + Math.Abs((dataPtrO + ws - nChan)[2] + (dataPtrO + ws)[2] * 2 + (dataPtrO + ws + nChan)[2] - (dataPtrO - ws - nChan)[2] - (dataPtrO - ws)[2] * 2 - (dataPtrO - ws + nChan)[2]));

                            }

                            if (blue > 255) blue = 255;
                            if (green > 255) green = 255;
                            if (red > 255) red = 255;

                            dataPtrD[0] = (byte)(blue);
                            dataPtrD[1] = (byte)(green);
                            dataPtrD[2] = (byte)(red);

                            dataPtrO += nChan;
                            dataPtrD += nChan;
                        }

                        dataPtrO += padding;
                        dataPtrD += padding;
                    }
                }
            }
        }
        public static void Roberts(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {
            unsafe
            {
                MIplImage mAux = imgCopy.MIplImage;
                MIplImage mResult = img.MIplImage;

                byte* dataPtrO = (byte*)mAux.imageData.ToPointer();
                byte* dataPtrD = (byte*)mResult.imageData.ToPointer();

                int blue, green, red;

                int width = img.Width;
                int height = img.Height;
                int nChan = mAux.nChannels;
                int w = mAux.width;
                int ws = mAux.widthStep;
                int padding = ws - nChan * w;
                int x, y;

                if (nChan == 3)
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            if (x == width - 1 && y == height - 1) //bottom right corner
                            {
                                blue = 0;
                                green = 0;
                                red = 0;
                            }
                            else if (y == height - 1) // bottom left corner and bottom margin
                            {
                                blue = 2 * Math.Abs((int)dataPtrO[0] - (dataPtrO + nChan)[0]);
                                green = 2 * Math.Abs((int)dataPtrO[1] - (dataPtrO + nChan)[1]);
                                red = 2 * Math.Abs((int)dataPtrO[2] - (dataPtrO + nChan)[2]);
                            }
                            else if (x == width - 1) //top right corner and right margin
                            {
                                blue = 2 * Math.Abs((int)dataPtrO[0] - (dataPtrO + ws)[0]);
                                green = 2 * Math.Abs((int)dataPtrO[1] - (dataPtrO + ws)[1]);
                                red = 2 * Math.Abs((int)dataPtrO[2] - (dataPtrO + ws)[2]);
                            }
                            else //top left corner, left margin, top margin and center pixels
                            {
                                blue = Math.Abs((int)dataPtrO[0] - (dataPtrO + nChan + ws)[0]) + Math.Abs((int)(dataPtrO + nChan)[0] - (dataPtrO + ws)[0]);
                                green = Math.Abs((int)dataPtrO[1] - (dataPtrO + nChan + ws)[1]) + Math.Abs((int)(dataPtrO + nChan)[1] - (dataPtrO + ws)[1]);
                                red = Math.Abs((int)dataPtrO[2] - (dataPtrO + nChan + ws)[2]) + Math.Abs((int)(dataPtrO + nChan)[2] - (dataPtrO + ws)[2]);
                            }

                            if (blue > 255) blue = 255;
                            if (green > 255) green = 255;
                            if (red > 255) red = 255;

                            dataPtrD[0] = (byte)(blue);
                            dataPtrD[1] = (byte)(green);
                            dataPtrD[2] = (byte)(red);

                            dataPtrO += nChan;
                            dataPtrD += nChan;
                        }

                        dataPtrO += padding;
                        dataPtrD += padding;
                    }
                }
            }
        }
        public static void Diferentiation(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {
            unsafe
            {
                MIplImage mAux = imgCopy.MIplImage;
                MIplImage mResult = img.MIplImage;

                byte* dataPtrO = (byte*)mAux.imageData.ToPointer(); // pointer to the original image
                byte* dataPtrD = (byte*)mResult.imageData.ToPointer(); // pointer to the duplicate image

                int blue, green, red;

                int width = img.Width;
                int height = img.Height;
                int nChan = mAux.nChannels; // number of channels - 3
                int w = mAux.width;
                int ws = mAux.widthStep;
                int padding = ws - nChan * w; // alinhament bytes (padding)
                int x, y;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            if (x == width - 1 && y == height - 1) //bottom right corner
                            {
                                blue = 0;
                                green = 0;
                                red = 0;
                            }
                            else if (y == height - 1) // bottom left corner and bottom margin
                            {
                                blue = Math.Abs((int)dataPtrO[0] - (dataPtrO + nChan)[0]);
                                green = Math.Abs((int)dataPtrO[1] - (dataPtrO + nChan)[1]);
                                red = Math.Abs((int)dataPtrO[2] - (dataPtrO + nChan)[2]);
                            }
                            else if (x == width - 1) //top right corner and right margin
                            {
                                blue = Math.Abs((int)dataPtrO[0] - (dataPtrO + ws)[0]);
                                green = Math.Abs((int)dataPtrO[1] - (dataPtrO + ws)[1]);
                                red = Math.Abs((int)dataPtrO[2] - (dataPtrO + ws)[2]);
                            }
                            else //top left, left margin, top margin and center pixels
                            {
                                blue = Math.Abs((int)dataPtrO[0] - (dataPtrO + nChan)[0]) + Math.Abs((int)dataPtrO[0] - (dataPtrO + ws)[0]);
                                green = Math.Abs((int)dataPtrO[1] - (dataPtrO + nChan)[1]) + Math.Abs((int)dataPtrO[1] - (dataPtrO + ws)[1]);
                                red = Math.Abs((int)dataPtrO[2] - (dataPtrO + nChan)[2]) + Math.Abs((int)dataPtrO[2] - (dataPtrO + ws)[2]);
                            }

                            if (blue > 255)
                                blue = 255;
                            if (green > 255)
                                green = 255;
                            if (red > 255)
                                red = 255;

                            dataPtrD[0] = (byte)(blue);
                            dataPtrD[1] = (byte)(green);
                            dataPtrD[2] = (byte)(red);

                            dataPtrO += nChan;
                            dataPtrD += nChan;
                        }

                        dataPtrO += padding;
                        dataPtrD += padding;
                    }
                }
            }
        }
        public static void Median(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {
            imgCopy.SmoothMedian(3).CopyTo(img);
        }

        public static int[] Histogram_Gray(Emgu.CV.Image<Bgr, byte> img)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                byte blue, green, red, gray;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels;
                int padding = m.widthStep - m.nChannels * m.width;
                int x, y;

                int[] intensity = new int[256];

                if (nChan == 3)
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            blue = dataPtr[0];
                            green = dataPtr[1];
                            red = dataPtr[2];

                            gray = (byte)Math.Round(((int)blue + green + red) / 3.0);

                            intensity[gray]++;
                            dataPtr += nChan;
                        }

                        dataPtr += padding;
                    }
                }

                return intensity;
            }
        }

        public static int[,] Histogram_RGB(Emgu.CV.Image<Bgr, byte> img)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                byte blue, green, red;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels;
                int padding = m.widthStep - m.nChannels * m.width;
                int x, y;

                int[,] intensity = new int[3, 256];

                if (nChan == 3)
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            blue = dataPtr[0];
                            green = dataPtr[1];
                            red = dataPtr[2];

                            intensity[0, blue]++;
                            intensity[1, green]++;
                            intensity[2, red]++;

                            dataPtr += nChan;
                        }

                        dataPtr += padding;
                    }
                }

                return intensity;
            }
        }

        public static int[,] Histogram_All(Emgu.CV.Image<Bgr, byte> img)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                byte blue, green, red, gray;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels;
                int padding = m.widthStep - m.nChannels * m.width;
                int x, y;

                int[,] intensity = new int[4, 256];

                if (nChan == 3)
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            blue = dataPtr[0];
                            green = dataPtr[1];
                            red = dataPtr[2];

                            gray = (byte)Math.Round(((int)blue + green + red) / 3.0);

                            intensity[0, gray]++;
                            intensity[1, blue]++;
                            intensity[2, green]++;
                            intensity[3, red]++;

                            dataPtr += nChan;
                        }

                        dataPtr += padding;
                    }
                }

                return intensity;
            }
        }

        public static void ConvertToBW(Emgu.CV.Image<Bgr, byte> img, int threshold)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                byte blue, green, red, gray;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels;
                int padding = m.widthStep - m.nChannels * m.width;
                int x, y;

                int[,] intensity = new int[4, 256];

                if (nChan == 3)
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            blue = dataPtr[0];
                            green = dataPtr[1];
                            red = dataPtr[2];

                            gray = (byte)Math.Round(((int)blue + green + red) / 3.0);

                            if (gray > threshold)
                            {
                                dataPtr[0] = (byte)255;
                                dataPtr[1] = (byte)255;
                                dataPtr[2] = (byte)255;
                            }
                            else
                            {
                                dataPtr[0] = 0;
                                dataPtr[1] = 0;
                                dataPtr[2] = 0;
                            }

                            dataPtr += nChan;
                        }

                        dataPtr += padding;
                    }
                }
            }
        }

        public static void ConvertToBW_Otsu(Emgu.CV.Image<Bgr, byte> img)
        {
            unsafe
            {
                double variance, maxVariance = 0.0;
                double q1, q2, u1, u2;
                int maxThreshold = 0;

                int[] histogram = Histogram_Gray(img);

                for (int t = 0; t < 255; t++)
                {
                    q1 = 0.0;
                    q2 = 0.0;
                    u1 = 0.0;
                    u2 = 0.0;

                    for (int i = 0; i <= t; i++)
                    {
                        q1 += histogram[i];
                        u1 += i * histogram[i];
                    }

                    for (int j = t + 1; j < 255; j++)
                    {
                        q2 += histogram[j];
                        u2 += j * histogram[j];
                    }

                    u1 /= q1;
                    u2 /= q2;

                    variance = q1 * q2 * (Math.Pow((u1 - u2), 2));

                    if (variance > maxVariance)
                    {
                        maxVariance = variance;
                        maxThreshold = t;
                    }
                }

                ConvertToBW(img, maxThreshold);
            }
        }
        public static void Mean_solutionB(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {
            unsafe
            {
                MIplImage mAux = imgCopy.MIplImage;
                MIplImage mResult = img.MIplImage;

                byte* dataPtrO = (byte*)mAux.imageData.ToPointer();
                byte* dataPtrD = (byte*)mResult.imageData.ToPointer();

                int width = img.Width;
                int height = img.Height;
                int nChan = mAux.nChannels;
                int w = mAux.width;
                int ws = mAux.widthStep;
                int padding = ws - nChan * w;
                int x, y;

                int firstSumB = 0, firstSumG = 0, firstSumR = 0;
                int sumB = 0, sumG = 0, sumR = 0;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            if (x == 0 && y == 0) //top left corner
                            {
                                sumB = ((int)dataPtrO[0] * 4 + (dataPtrO + nChan)[0] * 2 + (dataPtrO + ws)[0] * 2 + (dataPtrO + ws + nChan)[0]);
                                sumG = ((int)dataPtrO[1] * 4 + (dataPtrO + nChan)[1] * 2 + (dataPtrO + ws)[1] * 2 + (dataPtrO + ws + nChan)[1]);
                                sumR = ((int)dataPtrO[2] * 4 + (dataPtrO + nChan)[2] * 2 + (dataPtrO + ws)[2] * 2 + (dataPtrO + ws + nChan)[2]);

                                firstSumB = sumB;
                                firstSumG = sumG;
                                firstSumR = sumR;

                                dataPtrD[0] = (byte)(Math.Round(sumB / 9.0));
                                dataPtrD[1] = (byte)(Math.Round(sumG / 9.0));
                                dataPtrD[2] = (byte)(Math.Round(sumR / 9.0));
                            }
                            else if (x == 1 && y == 0) // top margin pixel after left corner
                            {
                                sumB = sumB - (dataPtrO - nChan)[0] * 2 - (dataPtrO - nChan + ws)[0] + (dataPtrO + nChan)[0] * 2 + (dataPtrO + nChan + ws)[0];
                                sumG = sumG - (dataPtrO - nChan)[1] * 2 - (dataPtrO - nChan + ws)[1] + (dataPtrO + nChan)[1] * 2 + (dataPtrO + nChan + ws)[1];
                                sumR = sumR - (dataPtrO - nChan)[2] * 2 - (dataPtrO - nChan + ws)[2] + (dataPtrO + nChan)[2] * 2 + (dataPtrO + nChan + ws)[2];

                                dataPtrD[0] = (byte)(Math.Round(sumB / 9.0));
                                dataPtrD[1] = (byte)(Math.Round(sumG / 9.0));
                                dataPtrD[2] = (byte)(Math.Round(sumR / 9.0));
                            }
                            else if (x == width - 1 && y == 0) //top right corner
                            {
                                sumB = sumB - (dataPtrO - 2 * nChan)[0] * 2 - (dataPtrO - 2 * nChan + ws)[0] + dataPtrO[0] * 2 + (dataPtrO + ws)[0];
                                sumG = sumG - (dataPtrO - 2 * nChan)[1] * 2 - (dataPtrO - 2 * nChan + ws)[1] + dataPtrO[1] * 2 + (dataPtrO + ws)[1];
                                sumR = sumR - (dataPtrO - 2 * nChan)[2] * 2 - (dataPtrO - 2 * nChan + ws)[2] + dataPtrO[2] * 2 + (dataPtrO + ws)[2];

                                dataPtrD[0] = (byte)(Math.Round(sumB / 9.0));
                                dataPtrD[1] = (byte)(Math.Round(sumG / 9.0));
                                dataPtrD[2] = (byte)(Math.Round(sumR / 9.0));
                            }
                            else if (x == 0 && y == height - 1) //bottom left corner
                            {
                                sumB = firstSumB - (dataPtrO - 2 * ws)[0] * 2 - (dataPtrO - 2 * ws + nChan)[0] + dataPtrO[0] * 2 + (dataPtrO + nChan)[0];
                                sumG = firstSumG - (dataPtrO - 2 * ws)[1] * 2 - (dataPtrO - 2 * ws + nChan)[1] + dataPtrO[1] * 2 + (dataPtrO + nChan)[1];
                                sumR = firstSumR - (dataPtrO - 2 * ws)[2] * 2 - (dataPtrO - 2 * ws + nChan)[2] + dataPtrO[2] * 2 + (dataPtrO + nChan)[2];

                                dataPtrD[0] = (byte)(Math.Round(sumB / 9.0));
                                dataPtrD[1] = (byte)(Math.Round(sumG / 9.0));
                                dataPtrD[2] = (byte)(Math.Round(sumR / 9.0));
                            }
                            else if (x == 1 && y == height - 1) // bottom margin pixel after left bottom corner
                            {
                                sumB = sumB - (dataPtrO - nChan)[0] * 2 - (dataPtrO - nChan - ws)[0] + (dataPtrO + nChan)[0] * 2 + (dataPtrO + nChan - ws)[0];
                                sumG = sumG - (dataPtrO - nChan)[1] * 2 - (dataPtrO - nChan - ws)[1] + (dataPtrO + nChan)[1] * 2 + (dataPtrO + nChan - ws)[1];
                                sumR = sumR - (dataPtrO - nChan)[2] * 2 - (dataPtrO - nChan - ws)[2] + (dataPtrO + nChan)[2] * 2 + (dataPtrO + nChan - ws)[2];

                                dataPtrD[0] = (byte)(Math.Round(sumB / 9.0));
                                dataPtrD[1] = (byte)(Math.Round(sumG / 9.0));
                                dataPtrD[2] = (byte)(Math.Round(sumR / 9.0));
                            }
                            else if (x == width - 1 && y == height - 1) //bottom right corner
                            {
                                sumB = sumB - (dataPtrO - 2 * nChan)[0] * 2 - (dataPtrO - 2 * nChan - ws)[0] + dataPtrO[0] * 2 + (dataPtrO - ws)[0];
                                sumG = sumG - (dataPtrO - 2 * nChan)[1] * 2 - (dataPtrO - 2 * nChan - ws)[1] + dataPtrO[1] * 2 + (dataPtrO - ws)[1];
                                sumR = sumR - (dataPtrO - 2 * nChan)[2] * 2 - (dataPtrO - 2 * nChan - ws)[2] + dataPtrO[2] * 2 + (dataPtrO - ws)[2];

                                dataPtrD[0] = (byte)(Math.Round(sumB / 9.0));
                                dataPtrD[1] = (byte)(Math.Round(sumG / 9.0));
                                dataPtrD[2] = (byte)(Math.Round(sumR / 9.0));
                            }
                            else if (x == 0 && y == 1) //left margin pixel after left top corner
                            {
                                sumB = firstSumB - (dataPtrO - ws)[0] * 2 - (dataPtrO + nChan - ws)[0] + (dataPtrO + ws)[0] * 2 + (dataPtrO + nChan + ws)[0];
                                sumG = firstSumG - (dataPtrO - ws)[1] * 2 - (dataPtrO + nChan - ws)[1] + (dataPtrO + ws)[1] * 2 + (dataPtrO + nChan + ws)[1];
                                sumR = firstSumR - (dataPtrO - ws)[2] * 2 - (dataPtrO + nChan - ws)[2] + (dataPtrO + ws)[2] * 2 + (dataPtrO + nChan + ws)[2];

                                firstSumB = sumB;
                                firstSumG = sumG;
                                firstSumR = sumR;

                                dataPtrD[0] = (byte)(Math.Round(sumB / 9.0));
                                dataPtrD[1] = (byte)(Math.Round(sumG / 9.0));
                                dataPtrD[2] = (byte)(Math.Round(sumR / 9.0));
                            }
                            else if (y == 0) // top margin
                            {
                                sumB = sumB - (dataPtrO - 2 * nChan)[0] * 2 - (dataPtrO - 2 * nChan + ws)[0] + (dataPtrO + nChan)[0] * 2 + (dataPtrO + nChan + ws)[0];
                                sumG = sumG - (dataPtrO - 2 * nChan)[1] * 2 - (dataPtrO - 2 * nChan + ws)[1] + (dataPtrO + nChan)[1] * 2 + (dataPtrO + nChan + ws)[1];
                                sumR = sumR - (dataPtrO - 2 * nChan)[2] * 2 - (dataPtrO - 2 * nChan + ws)[2] + (dataPtrO + nChan)[2] * 2 + (dataPtrO + nChan + ws)[2];

                                dataPtrD[0] = (byte)(Math.Round(sumB / 9.0));
                                dataPtrD[1] = (byte)(Math.Round(sumG / 9.0));
                                dataPtrD[2] = (byte)(Math.Round(sumR / 9.0));
                            }
                            else if (y == height - 1) // bottom margin
                            {
                                sumB = sumB - (dataPtrO - 2 * nChan)[0] * 2 - (dataPtrO - 2 * nChan - ws)[0] + (dataPtrO + nChan)[0] * 2 + (dataPtrO + nChan - ws)[0];
                                sumG = sumG - (dataPtrO - 2 * nChan)[1] * 2 - (dataPtrO - 2 * nChan - ws)[1] + (dataPtrO + nChan)[1] * 2 + (dataPtrO + nChan - ws)[1];
                                sumR = sumR - (dataPtrO - 2 * nChan)[2] * 2 - (dataPtrO - 2 * nChan - ws)[2] + (dataPtrO + nChan)[2] * 2 + (dataPtrO + nChan - ws)[2];

                                dataPtrD[0] = (byte)(Math.Round(sumB / 9.0));
                                dataPtrD[1] = (byte)(Math.Round(sumG / 9.0));
                                dataPtrD[2] = (byte)(Math.Round(sumR / 9.0));
                            }
                            else if (x == 0) // left margin
                            {
                                sumB = firstSumB - (dataPtrO - 2 * ws)[0] * 2 - (dataPtrO + nChan - 2 * ws)[0] + (dataPtrO + ws)[0] * 2 + (dataPtrO + nChan + ws)[0];
                                sumG = firstSumG - (dataPtrO - 2 * ws)[1] * 2 - (dataPtrO + nChan - 2 * ws)[1] + (dataPtrO + ws)[1] * 2 + (dataPtrO + nChan + ws)[1];
                                sumR = firstSumR - (dataPtrO - 2 * ws)[2] * 2 - (dataPtrO + nChan - 2 * ws)[2] + (dataPtrO + ws)[2] * 2 + (dataPtrO + nChan + ws)[2];

                                firstSumB = sumB;
                                firstSumG = sumG;
                                firstSumR = sumR;

                                dataPtrD[0] = (byte)(Math.Round(sumB / 9.0));
                                dataPtrD[1] = (byte)(Math.Round(sumG / 9.0));
                                dataPtrD[2] = (byte)(Math.Round(sumR / 9.0));
                            }
                            else if (x == width - 1) //right margin
                            {
                                sumB = sumB - (dataPtrO - 2 * nChan - ws)[0] - (dataPtrO - 2 * nChan)[0] - (dataPtrO - 2 * nChan + ws)[0] + (dataPtrO - ws)[0] + dataPtrO[0] + (dataPtrO + ws)[0];
                                sumG = sumG - (dataPtrO - 2 * nChan - ws)[1] - (dataPtrO - 2 * nChan)[1] - (dataPtrO - 2 * nChan + ws)[1] + (dataPtrO - ws)[1] + dataPtrO[1] + (dataPtrO + ws)[1];
                                sumR = sumR - (dataPtrO - 2 * nChan - ws)[2] - (dataPtrO - 2 * nChan)[2] - (dataPtrO - 2 * nChan + ws)[2] + (dataPtrO - ws)[2] + dataPtrO[2] + (dataPtrO + ws)[2];

                                dataPtrD[0] = (byte)(Math.Round(sumB / 9.0));
                                dataPtrD[1] = (byte)(Math.Round(sumG / 9.0));
                                dataPtrD[2] = (byte)(Math.Round(sumR / 9.0));
                            }
                            else if (x == 1) // second column
                            {
                                sumB = sumB - (dataPtrO - nChan - ws)[0] - (dataPtrO - nChan)[0] - (dataPtrO - nChan + ws)[0] + (dataPtrO + nChan - ws)[0] + (dataPtrO + nChan)[0] + (dataPtrO + nChan + ws)[0];
                                sumG = sumG - (dataPtrO - nChan - ws)[1] - (dataPtrO - nChan)[1] - (dataPtrO - nChan + ws)[1] + (dataPtrO + nChan - ws)[1] + (dataPtrO + nChan)[1] + (dataPtrO + nChan + ws)[1];
                                sumR = sumR - (dataPtrO - nChan - ws)[2] - (dataPtrO - nChan)[2] - (dataPtrO - nChan + ws)[2] + (dataPtrO + nChan - ws)[2] + (dataPtrO + nChan)[2] + (dataPtrO + nChan + ws)[2];

                                dataPtrD[0] = (byte)(Math.Round(sumB / 9.0));
                                dataPtrD[1] = (byte)(Math.Round(sumG / 9.0));
                                dataPtrD[2] = (byte)(Math.Round(sumR / 9.0));
                            }
                            else //center pixels
                            {
                                sumB = sumB - (dataPtrO - 2 * nChan - ws)[0] - (dataPtrO - 2 * nChan)[0] - (dataPtrO - 2 * nChan + ws)[0] + (dataPtrO + nChan - ws)[0] + (dataPtrO + nChan)[0] + (dataPtrO + nChan + ws)[0];
                                sumG = sumG - (dataPtrO - 2 * nChan - ws)[1] - (dataPtrO - 2 * nChan)[1] - (dataPtrO - 2 * nChan + ws)[1] + (dataPtrO + nChan - ws)[1] + (dataPtrO + nChan)[1] + (dataPtrO + nChan + ws)[1];
                                sumR = sumR - (dataPtrO - 2 * nChan - ws)[2] - (dataPtrO - 2 * nChan)[2] - (dataPtrO - 2 * nChan + ws)[2] + (dataPtrO + nChan - ws)[2] + (dataPtrO + nChan)[2] + (dataPtrO + nChan + ws)[2];

                                dataPtrD[0] = (byte)(Math.Round(sumB / 9.0));
                                dataPtrD[1] = (byte)(Math.Round(sumG / 9.0));
                                dataPtrD[2] = (byte)(Math.Round(sumR / 9.0));
                            }

                            dataPtrO += nChan;
                            dataPtrD += nChan;
                        }

                        dataPtrO += padding;
                        dataPtrD += padding;
                    }
                }
            }
        }
        public static void Mean_solutionC(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, int size) // to do
        {
            unsafe
            {
                MIplImage mAux = imgCopy.MIplImage;
                MIplImage mResult = img.MIplImage;

                byte* dataPtrO = (byte*)mAux.imageData.ToPointer();
                byte* dataPtrD = (byte*)mResult.imageData.ToPointer();

                int width = img.Width;
                int height = img.Height;
                int nChan = mAux.nChannels;
                int w = mAux.width;
                int ws = mAux.widthStep;
                int padding = ws - nChan * w;
                int x, y;

                int firstSumB = 0, firstSumG = 0, firstSumR = 0;
                int sumB = 0, sumG = 0, sumR = 0;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            if (x == 0 && y == 0) //top left corner
                            {
                                sumB = ((int)dataPtrO[0] * 4 + (dataPtrO + nChan)[0] * 2 + (dataPtrO + ws)[0] * 2 + (dataPtrO + ws + nChan)[0]);
                                sumG = ((int)dataPtrO[1] * 4 + (dataPtrO + nChan)[1] * 2 + (dataPtrO + ws)[1] * 2 + (dataPtrO + ws + nChan)[1]);
                                sumR = ((int)dataPtrO[2] * 4 + (dataPtrO + nChan)[2] * 2 + (dataPtrO + ws)[2] * 2 + (dataPtrO + ws + nChan)[2]);

                                firstSumB = sumB;
                                firstSumG = sumG;
                                firstSumR = sumR;

                                dataPtrD[0] = (byte)(Math.Round(sumB / 9.0));
                                dataPtrD[1] = (byte)(Math.Round(sumG / 9.0));
                                dataPtrD[2] = (byte)(Math.Round(sumR / 9.0));
                            }
                            else if (x == 1 && y == 0) // top margin pixel after left corner
                            {
                                sumB = sumB - (dataPtrO - nChan)[0] * 2 - (dataPtrO - nChan + ws)[0] + (dataPtrO + nChan)[0] * 2 + (dataPtrO + nChan + ws)[0];
                                sumG = sumG - (dataPtrO - nChan)[1] * 2 - (dataPtrO - nChan + ws)[1] + (dataPtrO + nChan)[1] * 2 + (dataPtrO + nChan + ws)[1];
                                sumR = sumR - (dataPtrO - nChan)[2] * 2 - (dataPtrO - nChan + ws)[2] + (dataPtrO + nChan)[2] * 2 + (dataPtrO + nChan + ws)[2];

                                dataPtrD[0] = (byte)(Math.Round(sumB / 9.0));
                                dataPtrD[1] = (byte)(Math.Round(sumG / 9.0));
                                dataPtrD[2] = (byte)(Math.Round(sumR / 9.0));
                            }
                            else if (x == width - 1 && y == 0) //top right corner
                            {
                                sumB = sumB - (dataPtrO - 2 * nChan)[0] * 2 - (dataPtrO - 2 * nChan + ws)[0] + dataPtrO[0] * 2 + (dataPtrO + ws)[0];
                                sumG = sumG - (dataPtrO - 2 * nChan)[1] * 2 - (dataPtrO - 2 * nChan + ws)[1] + dataPtrO[1] * 2 + (dataPtrO + ws)[1];
                                sumR = sumR - (dataPtrO - 2 * nChan)[2] * 2 - (dataPtrO - 2 * nChan + ws)[2] + dataPtrO[2] * 2 + (dataPtrO + ws)[2];

                                dataPtrD[0] = (byte)(Math.Round(sumB / 9.0));
                                dataPtrD[1] = (byte)(Math.Round(sumG / 9.0));
                                dataPtrD[2] = (byte)(Math.Round(sumR / 9.0));
                            }
                            else if (x == 0 && y == height - 1) //bottom left corner
                            {
                                sumB = firstSumB - (dataPtrO - 2 * ws)[0] * 2 - (dataPtrO - 2 * ws + nChan)[0] + dataPtrO[0] * 2 + (dataPtrO + nChan)[0];
                                sumG = firstSumG - (dataPtrO - 2 * ws)[1] * 2 - (dataPtrO - 2 * ws + nChan)[1] + dataPtrO[1] * 2 + (dataPtrO + nChan)[1];
                                sumR = firstSumR - (dataPtrO - 2 * ws)[2] * 2 - (dataPtrO - 2 * ws + nChan)[2] + dataPtrO[2] * 2 + (dataPtrO + nChan)[2];

                                dataPtrD[0] = (byte)(Math.Round(sumB / 9.0));
                                dataPtrD[1] = (byte)(Math.Round(sumG / 9.0));
                                dataPtrD[2] = (byte)(Math.Round(sumR / 9.0));
                            }
                            else if (x == 1 && y == height - 1) // bottom margin pixel after left bottom corner
                            {
                                sumB = sumB - (dataPtrO - nChan)[0] * 2 - (dataPtrO - nChan - ws)[0] + (dataPtrO + nChan)[0] * 2 + (dataPtrO + nChan - ws)[0];
                                sumG = sumG - (dataPtrO - nChan)[1] * 2 - (dataPtrO - nChan - ws)[1] + (dataPtrO + nChan)[1] * 2 + (dataPtrO + nChan - ws)[1];
                                sumR = sumR - (dataPtrO - nChan)[2] * 2 - (dataPtrO - nChan - ws)[2] + (dataPtrO + nChan)[2] * 2 + (dataPtrO + nChan - ws)[2];

                                dataPtrD[0] = (byte)(Math.Round(sumB / 9.0));
                                dataPtrD[1] = (byte)(Math.Round(sumG / 9.0));
                                dataPtrD[2] = (byte)(Math.Round(sumR / 9.0));
                            }
                            else if (x == width - 1 && y == height - 1) //bottom right corner
                            {
                                sumB = sumB - (dataPtrO - 2 * nChan)[0] * 2 - (dataPtrO - 2 * nChan - ws)[0] + dataPtrO[0] * 2 + (dataPtrO - ws)[0];
                                sumG = sumG - (dataPtrO - 2 * nChan)[1] * 2 - (dataPtrO - 2 * nChan - ws)[1] + dataPtrO[1] * 2 + (dataPtrO - ws)[1];
                                sumR = sumR - (dataPtrO - 2 * nChan)[2] * 2 - (dataPtrO - 2 * nChan - ws)[2] + dataPtrO[2] * 2 + (dataPtrO - ws)[2];

                                dataPtrD[0] = (byte)(Math.Round(sumB / 9.0));
                                dataPtrD[1] = (byte)(Math.Round(sumG / 9.0));
                                dataPtrD[2] = (byte)(Math.Round(sumR / 9.0));
                            }
                            else if (x == 0 && y == 1) //left margin pixel after left top corner
                            {
                                sumB = firstSumB - (dataPtrO - ws)[0] * 2 - (dataPtrO + nChan - ws)[0] + (dataPtrO + ws)[0] * 2 + (dataPtrO + nChan + ws)[0];
                                sumG = firstSumG - (dataPtrO - ws)[1] * 2 - (dataPtrO + nChan - ws)[1] + (dataPtrO + ws)[1] * 2 + (dataPtrO + nChan + ws)[1];
                                sumR = firstSumR - (dataPtrO - ws)[2] * 2 - (dataPtrO + nChan - ws)[2] + (dataPtrO + ws)[2] * 2 + (dataPtrO + nChan + ws)[2];

                                firstSumB = sumB;
                                firstSumG = sumG;
                                firstSumR = sumR;

                                dataPtrD[0] = (byte)(Math.Round(sumB / 9.0));
                                dataPtrD[1] = (byte)(Math.Round(sumG / 9.0));
                                dataPtrD[2] = (byte)(Math.Round(sumR / 9.0));
                            }
                            else if (y == 0) // top margin
                            {
                                sumB = sumB - (dataPtrO - 2 * nChan)[0] * 2 - (dataPtrO - 2 * nChan + ws)[0] + (dataPtrO + nChan)[0] * 2 + (dataPtrO + nChan + ws)[0];
                                sumG = sumG - (dataPtrO - 2 * nChan)[1] * 2 - (dataPtrO - 2 * nChan + ws)[1] + (dataPtrO + nChan)[1] * 2 + (dataPtrO + nChan + ws)[1];
                                sumR = sumR - (dataPtrO - 2 * nChan)[2] * 2 - (dataPtrO - 2 * nChan + ws)[2] + (dataPtrO + nChan)[2] * 2 + (dataPtrO + nChan + ws)[2];

                                dataPtrD[0] = (byte)(Math.Round(sumB / 9.0));
                                dataPtrD[1] = (byte)(Math.Round(sumG / 9.0));
                                dataPtrD[2] = (byte)(Math.Round(sumR / 9.0));
                            }
                            else if (y == height - 1) // bottom margin
                            {
                                sumB = sumB - (dataPtrO - 2 * nChan)[0] * 2 - (dataPtrO - 2 * nChan - ws)[0] + (dataPtrO + nChan)[0] * 2 + (dataPtrO + nChan - ws)[0];
                                sumG = sumG - (dataPtrO - 2 * nChan)[1] * 2 - (dataPtrO - 2 * nChan - ws)[1] + (dataPtrO + nChan)[1] * 2 + (dataPtrO + nChan - ws)[1];
                                sumR = sumR - (dataPtrO - 2 * nChan)[2] * 2 - (dataPtrO - 2 * nChan - ws)[2] + (dataPtrO + nChan)[2] * 2 + (dataPtrO + nChan - ws)[2];

                                dataPtrD[0] = (byte)(Math.Round(sumB / 9.0));
                                dataPtrD[1] = (byte)(Math.Round(sumG / 9.0));
                                dataPtrD[2] = (byte)(Math.Round(sumR / 9.0));
                            }
                            else if (x == 0) // left margin
                            {
                                sumB = firstSumB - (dataPtrO - 2 * ws)[0] * 2 - (dataPtrO + nChan - 2 * ws)[0] + (dataPtrO + ws)[0] * 2 + (dataPtrO + nChan + ws)[0];
                                sumG = firstSumG - (dataPtrO - 2 * ws)[1] * 2 - (dataPtrO + nChan - 2 * ws)[1] + (dataPtrO + ws)[1] * 2 + (dataPtrO + nChan + ws)[1];
                                sumR = firstSumR - (dataPtrO - 2 * ws)[2] * 2 - (dataPtrO + nChan - 2 * ws)[2] + (dataPtrO + ws)[2] * 2 + (dataPtrO + nChan + ws)[2];

                                firstSumB = sumB;
                                firstSumG = sumG;
                                firstSumR = sumR;

                                dataPtrD[0] = (byte)(Math.Round(sumB / 9.0));
                                dataPtrD[1] = (byte)(Math.Round(sumG / 9.0));
                                dataPtrD[2] = (byte)(Math.Round(sumR / 9.0));
                            }
                            else if (x == width - 1) //right margin
                            {
                                sumB = sumB - (dataPtrO - 2 * nChan - ws)[0] - (dataPtrO - 2 * nChan)[0] - (dataPtrO - 2 * nChan + ws)[0] + (dataPtrO - ws)[0] + dataPtrO[0] + (dataPtrO + ws)[0];
                                sumG = sumG - (dataPtrO - 2 * nChan - ws)[1] - (dataPtrO - 2 * nChan)[1] - (dataPtrO - 2 * nChan + ws)[1] + (dataPtrO - ws)[1] + dataPtrO[1] + (dataPtrO + ws)[1];
                                sumR = sumR - (dataPtrO - 2 * nChan - ws)[2] - (dataPtrO - 2 * nChan)[2] - (dataPtrO - 2 * nChan + ws)[2] + (dataPtrO - ws)[2] + dataPtrO[2] + (dataPtrO + ws)[2];

                                dataPtrD[0] = (byte)(Math.Round(sumB / 9.0));
                                dataPtrD[1] = (byte)(Math.Round(sumG / 9.0));
                                dataPtrD[2] = (byte)(Math.Round(sumR / 9.0));
                            }
                            else if (x == 1) // second column
                            {
                                sumB = sumB - (dataPtrO - nChan - ws)[0] - (dataPtrO - nChan)[0] - (dataPtrO - nChan + ws)[0] + (dataPtrO + nChan - ws)[0] + (dataPtrO + nChan)[0] + (dataPtrO + nChan + ws)[0];
                                sumG = sumG - (dataPtrO - nChan - ws)[1] - (dataPtrO - nChan)[1] - (dataPtrO - nChan + ws)[1] + (dataPtrO + nChan - ws)[1] + (dataPtrO + nChan)[1] + (dataPtrO + nChan + ws)[1];
                                sumR = sumR - (dataPtrO - nChan - ws)[2] - (dataPtrO - nChan)[2] - (dataPtrO - nChan + ws)[2] + (dataPtrO + nChan - ws)[2] + (dataPtrO + nChan)[2] + (dataPtrO + nChan + ws)[2];

                                dataPtrD[0] = (byte)(Math.Round(sumB / 9.0));
                                dataPtrD[1] = (byte)(Math.Round(sumG / 9.0));
                                dataPtrD[2] = (byte)(Math.Round(sumR / 9.0));
                            }
                            else //center pixels
                            {
                                sumB = sumB - (dataPtrO - 2 * nChan - ws)[0] - (dataPtrO - 2 * nChan)[0] - (dataPtrO - 2 * nChan + ws)[0] + (dataPtrO + nChan - ws)[0] + (dataPtrO + nChan)[0] + (dataPtrO + nChan + ws)[0];
                                sumG = sumG - (dataPtrO - 2 * nChan - ws)[1] - (dataPtrO - 2 * nChan)[1] - (dataPtrO - 2 * nChan + ws)[1] + (dataPtrO + nChan - ws)[1] + (dataPtrO + nChan)[1] + (dataPtrO + nChan + ws)[1];
                                sumR = sumR - (dataPtrO - 2 * nChan - ws)[2] - (dataPtrO - 2 * nChan)[2] - (dataPtrO - 2 * nChan + ws)[2] + (dataPtrO + nChan - ws)[2] + (dataPtrO + nChan)[2] + (dataPtrO + nChan + ws)[2];

                                dataPtrD[0] = (byte)(Math.Round(sumB / 9.0));
                                dataPtrD[1] = (byte)(Math.Round(sumG / 9.0));
                                dataPtrD[2] = (byte)(Math.Round(sumR / 9.0));
                            }

                            dataPtrO += nChan;
                            dataPtrD += nChan;
                        }

                        dataPtrO += padding;
                        dataPtrD += padding;
                    }
                }
            }
        }

        public static void Rotation_Bilinear(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float angle) // needs adjusts
        {
            unsafe
            {
                MIplImage mResult = img.MIplImage;
                MIplImage mAux = imgCopy.MIplImage;

                byte* dataPtrO = (byte*)mAux.imageData.ToPointer(); // pointer to the original image
                byte* dataPtrD = (byte*)mResult.imageData.ToPointer(); // pointer to the duplicate image

                byte blue, green, red;

                int width = img.Width;
                int height = img.Height;
                int nChan = mAux.nChannels; // number of channels - 3
                int w = mAux.width;
                int ws = mAux.widthStep;
                int x, y;
                double xO, yO, lowerX, lowerY, upperX, upperY, diffX, diffY;
                double blueLowerX, blueUpperX, greenLowerX, greenUpperX, redLowerX, redUpperX;
                double blueY, greenY, redY;

                double xCenter = img.Width / 2.0;
                double yCenter = img.Height / 2.0;
                double cosAngle = Math.Cos(angle);
                double sinAngle = Math.Sin(angle);

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            xO = (x - xCenter) * cosAngle - (yCenter - y) * sinAngle + xCenter;
                            yO = yCenter - (x - xCenter) * sinAngle - (yCenter - y) * cosAngle;

                            if (xO < 0 || yO < 0 || xO >= width || yO >= height)
                            {
                                blue = 0;
                                green = 0;
                                red = 0;
                            }
                            else
                            {
                                lowerX = Math.Floor(xO);
                                lowerY = Math.Floor(yO);
                                upperX = Math.Ceiling(xO);
                                upperY = Math.Ceiling(yO);

                                diffX = xO - lowerX;
                                diffY = yO - lowerY;

                                if ((int)lowerX == width && (int)lowerY == height) // bottom right cornor
                                {
                                    blueLowerX = (1 - diffX) * (dataPtrO + (int)lowerY * ws + (int)lowerX * nChan)[0] + diffX * (dataPtrO + (int)lowerY * ws + (int)lowerX * nChan)[0];
                                    greenLowerX = (1 - diffX) * (dataPtrO + (int)lowerY * ws + (int)lowerX * nChan)[1] + diffX * (dataPtrO + (int)lowerY * ws + (int)lowerX * nChan)[1];
                                    redLowerX = (1 - diffX) * (dataPtrO + (int)lowerY * ws + (int)lowerX * nChan)[2] + diffX * (dataPtrO + (int)lowerY * ws + (int)lowerX * nChan)[2];

                                    blueUpperX = blueLowerX;
                                    greenUpperX = greenLowerX;
                                    redUpperX = redLowerX;

                                    blueY = (1 - diffY) * blueLowerX + diffY * blueUpperX;
                                    greenY = (1 - diffY) * greenLowerX + diffY * greenUpperX;
                                    redY = (1 - diffY) * redLowerX + diffY * redUpperX;
                                }
                                else if ((int)lowerX == width) // right margin
                                {
                                    blueLowerX = (1 - diffX) * (dataPtrO + (int)lowerY * ws + (int)lowerX * nChan)[0] + diffX * (dataPtrO + (int)lowerY * ws + (int)lowerX * nChan)[0];
                                    greenLowerX = (1 - diffX) * (dataPtrO + (int)lowerY * ws + (int)lowerX * nChan)[1] + diffX * (dataPtrO + (int)lowerY * ws + (int)lowerX * nChan)[1];
                                    redLowerX = (1 - diffX) * (dataPtrO + (int)lowerY * ws + (int)lowerX * nChan)[2] + diffX * (dataPtrO + (int)lowerY * ws + (int)lowerX * nChan)[2];

                                    blueUpperX = (1 - diffX) * (dataPtrO + (int)upperY * ws + (int)lowerX * nChan)[0] + diffX * (dataPtrO + (int)upperY * ws + (int)lowerX * nChan)[0];
                                    greenUpperX = (1 - diffX) * (dataPtrO + (int)upperY * ws + (int)lowerX * nChan)[1] + diffX * (dataPtrO + (int)upperY * ws + (int)lowerX * nChan)[1];
                                    redUpperX = (1 - diffX) * (dataPtrO + (int)upperY * ws + (int)lowerX * nChan)[2] + diffX * (dataPtrO + (int)upperY * ws + (int)lowerX * nChan)[2];

                                    blueY = (1 - diffY) * blueLowerX + diffY * blueUpperX;
                                    greenY = (1 - diffY) * greenLowerX + diffY * greenUpperX;
                                    redY = (1 - diffY) * redLowerX + diffY * redUpperX;
                                }
                                else if ((int)lowerY == height) // bottom margin
                                {
                                    blueLowerX = (1 - diffX) * (dataPtrO + (int)lowerY * ws + (int)lowerX * nChan)[0] + diffX * (dataPtrO + (int)lowerY * ws + (int)upperX * nChan)[0];
                                    greenLowerX = (1 - diffX) * (dataPtrO + (int)lowerY * ws + (int)lowerX * nChan)[1] + diffX * (dataPtrO + (int)lowerY * ws + (int)upperX * nChan)[1];
                                    redLowerX = (1 - diffX) * (dataPtrO + (int)lowerY * ws + (int)lowerX * nChan)[2] + diffX * (dataPtrO + (int)lowerY * ws + (int)upperX * nChan)[2];

                                    blueUpperX = blueLowerX;
                                    greenUpperX = greenLowerX;
                                    redUpperX = redLowerX;

                                    blueY = (1 - diffY) * blueLowerX + diffY * blueUpperX;
                                    greenY = (1 - diffY) * greenLowerX + diffY * greenUpperX;
                                    redY = (1 - diffY) * redLowerX + diffY * redUpperX;
                                }
                                else // all other pixels
                                {
                                    blueLowerX = (1 - diffX) * (dataPtrO + (int)lowerY * ws + (int)lowerX * nChan)[0] + diffX * (dataPtrO + (int)lowerY * ws + (int)upperX * nChan)[0];
                                    greenLowerX = (1 - diffX) * (dataPtrO + (int)lowerY * ws + (int)lowerX * nChan)[1] + diffX * (dataPtrO + (int)lowerY * ws + (int)upperX * nChan)[1];
                                    redLowerX = (1 - diffX) * (dataPtrO + (int)lowerY * ws + (int)lowerX * nChan)[2] + diffX * (dataPtrO + (int)lowerY * ws + (int)upperX * nChan)[2];

                                    blueUpperX = (1 - diffX) * (dataPtrO + (int)upperY * ws + (int)lowerX * nChan)[0] + diffX * (dataPtrO + (int)upperY * ws + (int)upperX * nChan)[0];
                                    greenUpperX = (1 - diffX) * (dataPtrO + (int)upperY * ws + (int)lowerX * nChan)[1] + diffX * (dataPtrO + (int)upperY * ws + (int)upperX * nChan)[1];
                                    redUpperX = (1 - diffX) * (dataPtrO + (int)upperY * ws + (int)lowerX * nChan)[2] + diffX * (dataPtrO + (int)upperY * ws + (int)upperX * nChan)[2];

                                    blueY = (1 - diffY) * blueLowerX + diffY * blueUpperX;
                                    greenY = (1 - diffY) * greenLowerX + diffY * greenUpperX;
                                    redY = (1 - diffY) * redLowerX + diffY * redUpperX;
                                }

                                blue = (byte)Math.Round(blueY);
                                green = (byte)Math.Round(greenY);
                                red = (byte)Math.Round(redY);
                            }

                            (dataPtrD + y * ws + x * nChan)[0] = blue;
                            (dataPtrD + y * ws + x * nChan)[1] = green;
                            (dataPtrD + y * ws + x * nChan)[2] = red;
                        }
                    }
                }
            }
        }

        public static void Scale_Bilinear(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float scaleFactor) // needs adjusts
        {
            unsafe
            {
                MIplImage mResult = img.MIplImage;
                MIplImage mAux = imgCopy.MIplImage;

                byte* dataPtrO = (byte*)mAux.imageData.ToPointer(); // pointer to the original image
                byte* dataPtrD = (byte*)mResult.imageData.ToPointer(); // pointer to the duplicate image

                byte blue, green, red;

                int width = img.Width;
                int height = img.Height;
                int nChan = mAux.nChannels; // number of channels - 3
                int w = mAux.width;
                int ws = mAux.widthStep;
                int x, y;
                double xO, yO, lowerX, lowerY, upperX, upperY, diffX, diffY;
                double blueLowerX, blueUpperX, greenLowerX, greenUpperX, redLowerX, redUpperX;
                double blueY, greenY, redY;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            xO = (int)Math.Round(x / scaleFactor);
                            yO = (int)Math.Round(y / scaleFactor);

                            if (xO < 0 || yO < 0 || xO >= width || yO >= height)
                            {
                                blue = 0;
                                green = 0;
                                red = 0;
                            }
                            else
                            {
                                lowerX = Math.Floor(xO);
                                lowerY = Math.Floor(yO);
                                upperX = Math.Ceiling(xO);
                                upperY = Math.Ceiling(yO);

                                diffX = xO - lowerX;
                                diffY = yO - lowerY;

                                if ((int)lowerX == width && (int)lowerY == height) // bottom right cornor
                                {
                                    blueLowerX = (1 - diffX) * (dataPtrO + (int)lowerY * ws + (int)lowerX * nChan)[0] + diffX * (dataPtrO + (int)lowerY * ws + (int)lowerX * nChan)[0];
                                    greenLowerX = (1 - diffX) * (dataPtrO + (int)lowerY * ws + (int)lowerX * nChan)[1] + diffX * (dataPtrO + (int)lowerY * ws + (int)lowerX * nChan)[1];
                                    redLowerX = (1 - diffX) * (dataPtrO + (int)lowerY * ws + (int)lowerX * nChan)[2] + diffX * (dataPtrO + (int)lowerY * ws + (int)lowerX * nChan)[2];

                                    blueUpperX = blueLowerX;
                                    greenUpperX = greenLowerX;
                                    redUpperX = redLowerX;

                                    blueY = (1 - diffY) * blueLowerX + diffY * blueUpperX;
                                    greenY = (1 - diffY) * greenLowerX + diffY * greenUpperX;
                                    redY = (1 - diffY) * redLowerX + diffY * redUpperX;
                                }
                                else if ((int)lowerX == width) // right margin
                                {
                                    blueLowerX = (1 - diffX) * (dataPtrO + (int)lowerY * ws + (int)lowerX * nChan)[0] + diffX * (dataPtrO + (int)lowerY * ws + (int)lowerX * nChan)[0];
                                    greenLowerX = (1 - diffX) * (dataPtrO + (int)lowerY * ws + (int)lowerX * nChan)[1] + diffX * (dataPtrO + (int)lowerY * ws + (int)lowerX * nChan)[1];
                                    redLowerX = (1 - diffX) * (dataPtrO + (int)lowerY * ws + (int)lowerX * nChan)[2] + diffX * (dataPtrO + (int)lowerY * ws + (int)lowerX * nChan)[2];

                                    blueUpperX = (1 - diffX) * (dataPtrO + (int)upperY * ws + (int)lowerX * nChan)[0] + diffX * (dataPtrO + (int)upperY * ws + (int)lowerX * nChan)[0];
                                    greenUpperX = (1 - diffX) * (dataPtrO + (int)upperY * ws + (int)lowerX * nChan)[1] + diffX * (dataPtrO + (int)upperY * ws + (int)lowerX * nChan)[1];
                                    redUpperX = (1 - diffX) * (dataPtrO + (int)upperY * ws + (int)lowerX * nChan)[2] + diffX * (dataPtrO + (int)upperY * ws + (int)lowerX * nChan)[2];

                                    blueY = (1 - diffY) * blueLowerX + diffY * blueUpperX;
                                    greenY = (1 - diffY) * greenLowerX + diffY * greenUpperX;
                                    redY = (1 - diffY) * redLowerX + diffY * redUpperX;
                                }
                                else if ((int)lowerY == height) // bottom margin
                                {
                                    blueLowerX = (1 - diffX) * (dataPtrO + (int)lowerY * ws + (int)lowerX * nChan)[0] + diffX * (dataPtrO + (int)lowerY * ws + (int)upperX * nChan)[0];
                                    greenLowerX = (1 - diffX) * (dataPtrO + (int)lowerY * ws + (int)lowerX * nChan)[1] + diffX * (dataPtrO + (int)lowerY * ws + (int)upperX * nChan)[1];
                                    redLowerX = (1 - diffX) * (dataPtrO + (int)lowerY * ws + (int)lowerX * nChan)[2] + diffX * (dataPtrO + (int)lowerY * ws + (int)upperX * nChan)[2];

                                    blueUpperX = blueLowerX;
                                    greenUpperX = greenLowerX;
                                    redUpperX = redLowerX;

                                    blueY = (1 - diffY) * blueLowerX + diffY * blueUpperX;
                                    greenY = (1 - diffY) * greenLowerX + diffY * greenUpperX;
                                    redY = (1 - diffY) * redLowerX + diffY * redUpperX;
                                }
                                else // all other pixels
                                {
                                    blueLowerX = (1 - diffX) * (dataPtrO + (int)lowerY * ws + (int)lowerX * nChan)[0] + diffX * (dataPtrO + (int)lowerY * ws + (int)upperX * nChan)[0];
                                    greenLowerX = (1 - diffX) * (dataPtrO + (int)lowerY * ws + (int)lowerX * nChan)[1] + diffX * (dataPtrO + (int)lowerY * ws + (int)upperX * nChan)[1];
                                    redLowerX = (1 - diffX) * (dataPtrO + (int)lowerY * ws + (int)lowerX * nChan)[2] + diffX * (dataPtrO + (int)lowerY * ws + (int)upperX * nChan)[2];

                                    blueUpperX = (1 - diffX) * (dataPtrO + (int)upperY * ws + (int)lowerX * nChan)[0] + diffX * (dataPtrO + (int)upperY * ws + (int)upperX * nChan)[0];
                                    greenUpperX = (1 - diffX) * (dataPtrO + (int)upperY * ws + (int)lowerX * nChan)[1] + diffX * (dataPtrO + (int)upperY * ws + (int)upperX * nChan)[1];
                                    redUpperX = (1 - diffX) * (dataPtrO + (int)upperY * ws + (int)lowerX * nChan)[2] + diffX * (dataPtrO + (int)upperY * ws + (int)upperX * nChan)[2];

                                    blueY = (1 - diffY) * blueLowerX + diffY * blueUpperX;
                                    greenY = (1 - diffY) * greenLowerX + diffY * greenUpperX;
                                    redY = (1 - diffY) * redLowerX + diffY * redUpperX;
                                }

                                blue = (byte)Math.Round(blueY);
                                green = (byte)Math.Round(greenY);
                                red = (byte)Math.Round(redY);
                            }

                            (dataPtrD + y * ws + x * nChan)[0] = blue;
                            (dataPtrD + y * ws + x * nChan)[1] = green;
                            (dataPtrD + y * ws + x * nChan)[2] = red;
                        }
                    }
                }
            }
        }

        public static void Scale_point_xy_Bilinear(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float scaleFactor, int xCenter, int yCenter) // needs adjusts
        {
            unsafe
            {
                MIplImage mResult = img.MIplImage;
                MIplImage mAux = imgCopy.MIplImage;

                byte* dataPtrO = (byte*)mAux.imageData.ToPointer(); // pointer to the original image
                byte* dataPtrD = (byte*)mResult.imageData.ToPointer(); // pointer to the duplicate image

                byte blue, green, red;

                int width = img.Width;
                int height = img.Height;
                int nChan = mAux.nChannels; // number of channels - 3
                int w = mAux.width;
                int ws = mAux.widthStep;
                int x, y;
                double xO, yO, lowerX, lowerY, upperX, upperY, diffX, diffY;
                double blueLowerX, blueUpperX, greenLowerX, greenUpperX, redLowerX, redUpperX;
                double blueY, greenY, redY;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            xO = (int)Math.Round((x - width / 2) / scaleFactor + xCenter);
                            yO = (int)Math.Round((y - height / 2) / scaleFactor + yCenter);

                            if (xO < 0 || yO < 0 || xO >= width || yO >= height)
                            {
                                blue = 0;
                                green = 0;
                                red = 0;
                            }
                            else
                            {
                                lowerX = Math.Floor(xO);
                                lowerY = Math.Floor(yO);
                                upperX = Math.Ceiling(xO);
                                upperY = Math.Ceiling(yO);

                                diffX = xO - lowerX;
                                diffY = yO - lowerY;

                                if ((int)lowerX == width && (int)lowerY == height) // bottom right cornor
                                {
                                    blueLowerX = (1 - diffX) * (dataPtrO + (int)lowerY * ws + (int)lowerX * nChan)[0] + diffX * (dataPtrO + (int)lowerY * ws + (int)lowerX * nChan)[0];
                                    greenLowerX = (1 - diffX) * (dataPtrO + (int)lowerY * ws + (int)lowerX * nChan)[1] + diffX * (dataPtrO + (int)lowerY * ws + (int)lowerX * nChan)[1];
                                    redLowerX = (1 - diffX) * (dataPtrO + (int)lowerY * ws + (int)lowerX * nChan)[2] + diffX * (dataPtrO + (int)lowerY * ws + (int)lowerX * nChan)[2];

                                    blueUpperX = blueLowerX;
                                    greenUpperX = greenLowerX;
                                    redUpperX = redLowerX;

                                    blueY = (1 - diffY) * blueLowerX + diffY * blueUpperX;
                                    greenY = (1 - diffY) * greenLowerX + diffY * greenUpperX;
                                    redY = (1 - diffY) * redLowerX + diffY * redUpperX;
                                }
                                else if ((int)lowerX == width) // right margin
                                {
                                    blueLowerX = (1 - diffX) * (dataPtrO + (int)lowerY * ws + (int)lowerX * nChan)[0] + diffX * (dataPtrO + (int)lowerY * ws + (int)lowerX * nChan)[0];
                                    greenLowerX = (1 - diffX) * (dataPtrO + (int)lowerY * ws + (int)lowerX * nChan)[1] + diffX * (dataPtrO + (int)lowerY * ws + (int)lowerX * nChan)[1];
                                    redLowerX = (1 - diffX) * (dataPtrO + (int)lowerY * ws + (int)lowerX * nChan)[2] + diffX * (dataPtrO + (int)lowerY * ws + (int)lowerX * nChan)[2];

                                    blueUpperX = (1 - diffX) * (dataPtrO + (int)upperY * ws + (int)lowerX * nChan)[0] + diffX * (dataPtrO + (int)upperY * ws + (int)lowerX * nChan)[0];
                                    greenUpperX = (1 - diffX) * (dataPtrO + (int)upperY * ws + (int)lowerX * nChan)[1] + diffX * (dataPtrO + (int)upperY * ws + (int)lowerX * nChan)[1];
                                    redUpperX = (1 - diffX) * (dataPtrO + (int)upperY * ws + (int)lowerX * nChan)[2] + diffX * (dataPtrO + (int)upperY * ws + (int)lowerX * nChan)[2];

                                    blueY = (1 - diffY) * blueLowerX + diffY * blueUpperX;
                                    greenY = (1 - diffY) * greenLowerX + diffY * greenUpperX;
                                    redY = (1 - diffY) * redLowerX + diffY * redUpperX;
                                }
                                else if ((int)lowerY == height) // bottom margin
                                {
                                    blueLowerX = (1 - diffX) * (dataPtrO + (int)lowerY * ws + (int)lowerX * nChan)[0] + diffX * (dataPtrO + (int)lowerY * ws + (int)upperX * nChan)[0];
                                    greenLowerX = (1 - diffX) * (dataPtrO + (int)lowerY * ws + (int)lowerX * nChan)[1] + diffX * (dataPtrO + (int)lowerY * ws + (int)upperX * nChan)[1];
                                    redLowerX = (1 - diffX) * (dataPtrO + (int)lowerY * ws + (int)lowerX * nChan)[2] + diffX * (dataPtrO + (int)lowerY * ws + (int)upperX * nChan)[2];

                                    blueUpperX = blueLowerX;
                                    greenUpperX = greenLowerX;
                                    redUpperX = redLowerX;

                                    blueY = (1 - diffY) * blueLowerX + diffY * blueUpperX;
                                    greenY = (1 - diffY) * greenLowerX + diffY * greenUpperX;
                                    redY = (1 - diffY) * redLowerX + diffY * redUpperX;
                                }
                                else // all other pixels
                                {
                                    blueLowerX = (1 - diffX) * (dataPtrO + (int)lowerY * ws + (int)lowerX * nChan)[0] + diffX * (dataPtrO + (int)lowerY * ws + (int)upperX * nChan)[0];
                                    greenLowerX = (1 - diffX) * (dataPtrO + (int)lowerY * ws + (int)lowerX * nChan)[1] + diffX * (dataPtrO + (int)lowerY * ws + (int)upperX * nChan)[1];
                                    redLowerX = (1 - diffX) * (dataPtrO + (int)lowerY * ws + (int)lowerX * nChan)[2] + diffX * (dataPtrO + (int)lowerY * ws + (int)upperX * nChan)[2];

                                    blueUpperX = (1 - diffX) * (dataPtrO + (int)upperY * ws + (int)lowerX * nChan)[0] + diffX * (dataPtrO + (int)upperY * ws + (int)upperX * nChan)[0];
                                    greenUpperX = (1 - diffX) * (dataPtrO + (int)upperY * ws + (int)lowerX * nChan)[1] + diffX * (dataPtrO + (int)upperY * ws + (int)upperX * nChan)[1];
                                    redUpperX = (1 - diffX) * (dataPtrO + (int)upperY * ws + (int)lowerX * nChan)[2] + diffX * (dataPtrO + (int)upperY * ws + (int)upperX * nChan)[2];

                                    blueY = (1 - diffY) * blueLowerX + diffY * blueUpperX;
                                    greenY = (1 - diffY) * greenLowerX + diffY * greenUpperX;
                                    redY = (1 - diffY) * redLowerX + diffY * redUpperX;
                                }

                                blue = (byte)Math.Round(blueY);
                                green = (byte)Math.Round(greenY);
                                red = (byte)Math.Round(redY);
                            }

                            (dataPtrD + y * ws + x * nChan)[0] = blue;
                            (dataPtrD + y * ws + x * nChan)[1] = green;
                            (dataPtrD + y * ws + x * nChan)[2] = red;
                        }
                    }
                }
            }
        }

        public static double[] ConvertRGBtoHSV(int redValue, int greenValue, int blueValue)
        {
            double hue = 0;
            double saturation;
            double value;

            double red = (double)redValue / 255;
            double green = (double)greenValue / 255;
            double blue = (double)blueValue / 255;

            double max = Math.Max(red, Math.Max(green, blue));
            double min = Math.Min(red, Math.Min(green, blue));

            if (max == 0)
            {
                hue = 0;
            } 
            else if (max == red && green >= blue)
            {
                hue = 60 * ((green - blue) / (max - min));
            } 
            else if (max == red && green < blue)
            {
                hue = 60 * ((green - blue) / (max - min)) + 360;
            } 
            else if (max == green)
            {
                hue = 60 * ((blue - red) / (max - min)) + 120;
            } 
            else if (max == blue)
            {
                hue = 60 * ((red - green) / (max - min)) + 240;
            }

            if (hue < 0) hue += 360;
            saturation = (max == 0) ? 0 : ((max - min) / max);
            value = max;

            double[] hsv = new double[3];
            hsv[0] = hue;
            hsv[1] = saturation * 100;
            hsv[2] = value * 100;

            return hsv;
        }

        public static void ConvertMatrixIntoCSV(int[,] matrix)
        {
            string filePath = @"D:\\FCT_MIEEC\\Mestrado\\4º Ano\\SS\\Prática\\SS_OpenCV_Base\\SS_OpenCV\\bin\\Debug\\tags.csv";
            int width = matrix.GetLength(0);
            int height = matrix.GetLength(1);

            using (StreamWriter outfile = new StreamWriter(filePath))
            {
                for (int y = 0; y < height; y++)
                {
                    string line = "";

                    for (int x = 0; x < width; x++)
                    {
                        line += matrix[x, y].ToString() + ";";
                    }

                    outfile.WriteLine(line);
                }
            }
        }

        public static int[,] ConnectedComponentsAlgorithm(int[,] matrix)
        {
            int width = matrix.GetLength(0);
            int height = matrix.GetLength(1);
            int min;
            int current;
            bool restart = false;
            bool reverse = false;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    current = matrix[x, y];
                    if (current != 0)
                    {
                        if (x == 0)
                        {
                            if (y == 0)
                            {
                                int neigh1 = matrix[x + 1, y];
                                int neigh2 = matrix[x, y + 1];

                                List<int> aux = new List<int> {current};
                                if (neigh1 != 0) aux.Add(neigh1);
                                if (neigh2 != 0) aux.Add(neigh2);

                                min = aux.Min();
                            }
                            else if (y == height - 1)
                            {
                                int neigh1 = matrix[x + 1, y];
                                int neigh2 = matrix[x, y - 1];

                                List<int> aux = new List<int> { current };
                                if (neigh1 != 0) aux.Add(neigh1);
                                if (neigh2 != 0) aux.Add(neigh2);

                                min = aux.Min();
                            }
                            else
                            {
                                int neigh1 = matrix[x + 1, y];
                                int neigh2 = matrix[x, y + 1];
                                int neigh3 = matrix[x, y - 1];

                                List<int> aux = new List<int> { current };
                                if (neigh1 != 0) aux.Add(neigh1);
                                if (neigh2 != 0) aux.Add(neigh2);
                                if (neigh3 != 0) aux.Add(neigh3);

                                min = aux.Min();
                            }
                        }
                        else if (x == width - 1)
                        {
                            if (y == 0)
                            {
                                int neigh1 = matrix[x - 1, y];
                                int neigh2 = matrix[x, y + 1];

                                List<int> aux = new List<int> { current };
                                if (neigh1 != 0) aux.Add(neigh1);
                                if (neigh2 != 0) aux.Add(neigh2);

                                min = aux.Min();
                            }
                            else if (y == height - 1)
                            {
                                int neigh1 = matrix[x - 1, y];
                                int neigh2 = matrix[x, y - 1];

                                List<int> aux = new List<int> { current };
                                if (neigh1 != 0) aux.Add(neigh1);
                                if (neigh2 != 0) aux.Add(neigh2);

                                min = aux.Min();
                            }
                            else
                            {
                                int neigh1 = matrix[x - 1, y];
                                int neigh2 = matrix[x, y + 1];
                                int neigh3 = matrix[x, y - 1];

                                List<int> aux = new List<int> { current };
                                if (neigh1 != 0) aux.Add(neigh1);
                                if (neigh2 != 0) aux.Add(neigh2);
                                if (neigh3 != 0) aux.Add(neigh3);

                                min = aux.Min();
                            }
                        }
                        else
                        {
                            if (y == 0)
                            {
                                int neigh1 = matrix[x - 1, y];
                                int neigh2 = matrix[x + 1, y];
                                int neigh3 = matrix[x, y + 1];

                                List<int> aux = new List<int> { current };
                                if (neigh1 != 0) aux.Add(neigh1);
                                if (neigh2 != 0) aux.Add(neigh2);
                                if (neigh3 != 0) aux.Add(neigh3);

                                min = aux.Min();
                            }
                            else if (y == height - 1)
                            {
                                int neigh1 = matrix[x - 1, y];
                                int neigh2 = matrix[x + 1, y];
                                int neigh3 = matrix[x, y - 1];

                                List<int> aux = new List<int> { current };
                                if (neigh1 != 0) aux.Add(neigh1);
                                if (neigh2 != 0) aux.Add(neigh2);
                                if (neigh3 != 0) aux.Add(neigh3);

                                min = aux.Min();
                            }
                            else
                            {
                                int neigh1 = matrix[x - 1, y];
                                int neigh2 = matrix[x + 1, y];
                                int neigh3 = matrix[x, y - 1];
                                int neigh4 = matrix[x, y + 1];

                                List<int> aux = new List<int> { current };
                                if (neigh1 != 0) aux.Add(neigh1);
                                if (neigh2 != 0) aux.Add(neigh2);
                                if (neigh3 != 0) aux.Add(neigh3);
                                if (neigh4 != 0) aux.Add(neigh4);

                                min = aux.Min();
                            }
                        }

                        if (min < current)
                        {
                            matrix[x, y] = min;
                            reverse = true;
                        }
                    }
                }
            }
                    
            if (reverse)
            {
                for (int x = width - 1; x >= 0; x--)
                {
                    for (int y = height - 1; y >= 0; y--)
                    {
                        current = matrix[x, y];
                        if (current != 0)
                        {
                            if (x == 0)
                            {
                                if (y == 0)
                                {
                                    int neigh1 = matrix[x + 1, y];
                                    int neigh2 = matrix[x, y + 1];

                                    List<int> aux = new List<int> { current };
                                    if (neigh1 != 0) aux.Add(neigh1);
                                    if (neigh2 != 0) aux.Add(neigh2);

                                    min = aux.Min();
                                }
                                else if (y == height - 1)
                                {
                                    int neigh1 = matrix[x + 1, y];
                                    int neigh2 = matrix[x, y - 1];

                                    List<int> aux = new List<int> { current };
                                    if (neigh1 != 0) aux.Add(neigh1);
                                    if (neigh2 != 0) aux.Add(neigh2);

                                    min = aux.Min();
                                }
                                else
                                {
                                    int neigh1 = matrix[x + 1, y];
                                    int neigh2 = matrix[x, y + 1];
                                    int neigh3 = matrix[x, y - 1];

                                    List<int> aux = new List<int> { current };
                                    if (neigh1 != 0) aux.Add(neigh1);
                                    if (neigh2 != 0) aux.Add(neigh2);
                                    if (neigh3 != 0) aux.Add(neigh3);

                                    min = aux.Min();
                                }
                            }
                            else if (x == width - 1)
                            {
                                if (y == 0)
                                {
                                    int neigh1 = matrix[x - 1, y];
                                    int neigh2 = matrix[x, y + 1];

                                    List<int> aux = new List<int> { current };
                                    if (neigh1 != 0) aux.Add(neigh1);
                                    if (neigh2 != 0) aux.Add(neigh2);

                                    min = aux.Min();
                                }
                                else if (y == height - 1)
                                {
                                    int neigh1 = matrix[x - 1, y];
                                    int neigh2 = matrix[x, y - 1];

                                    List<int> aux = new List<int> { current };
                                    if (neigh1 != 0) aux.Add(neigh1);
                                    if (neigh2 != 0) aux.Add(neigh2);

                                    min = aux.Min();
                                }
                                else
                                {
                                    int neigh1 = matrix[x - 1, y];
                                    int neigh2 = matrix[x, y + 1];
                                    int neigh3 = matrix[x, y - 1];

                                    List<int> aux = new List<int> { current };
                                    if (neigh1 != 0) aux.Add(neigh1);
                                    if (neigh2 != 0) aux.Add(neigh2);
                                    if (neigh3 != 0) aux.Add(neigh3);

                                    min = aux.Min();
                                }
                            }
                            else
                            {
                                if (y == 0)
                                {
                                    int neigh1 = matrix[x - 1, y];
                                    int neigh2 = matrix[x + 1, y];
                                    int neigh3 = matrix[x, y + 1];

                                    List<int> aux = new List<int> { current };
                                    if (neigh1 != 0) aux.Add(neigh1);
                                    if (neigh2 != 0) aux.Add(neigh2);
                                    if (neigh3 != 0) aux.Add(neigh3);

                                    min = aux.Min();
                                }
                                else if (y == height - 1)
                                {
                                    int neigh1 = matrix[x - 1, y];
                                    int neigh2 = matrix[x + 1, y];
                                    int neigh3 = matrix[x, y - 1];

                                    List<int> aux = new List<int> { current };
                                    if (neigh1 != 0) aux.Add(neigh1);
                                    if (neigh2 != 0) aux.Add(neigh2);
                                    if (neigh3 != 0) aux.Add(neigh3);

                                    min = aux.Min();
                                }
                                else
                                {
                                    int neigh1 = matrix[x - 1, y];
                                    int neigh2 = matrix[x + 1, y];
                                    int neigh3 = matrix[x, y - 1];
                                    int neigh4 = matrix[x, y + 1];

                                    List<int> aux = new List<int> { current };
                                    if (neigh1 != 0) aux.Add(neigh1);
                                    if (neigh2 != 0) aux.Add(neigh2);
                                    if (neigh3 != 0) aux.Add(neigh3);
                                    if (neigh4 != 0) aux.Add(neigh4);

                                    min = aux.Min();
                                }
                            }

                            if (min < current)
                            {
                                matrix[x, y] = min;
                                restart = true;
                            }
                        }
                    }
                }
            }

            if (restart)
            {
                ConnectedComponentsAlgorithm(matrix);
            }

            return matrix;
        }

        public static Dictionary<int, int> RemoveNoiseTags(int[,] matrix, double noiseThreshold)
        {
            int width = matrix.GetLength(0);
            int height = matrix.GetLength(1);
            Dictionary<int, int> tagsDict = new Dictionary<int, int>();

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (matrix[x, y] != 0)
                    {
                        if (tagsDict.ContainsKey(matrix[x, y]))
                        {
                            tagsDict[matrix[x, y]]++;
                        }
                        else
                        {
                            tagsDict.Add(matrix[x, y], 1);
                        }
                    }
                }
            }

            try
            {
                KeyValuePair<int, int> maxTag = tagsDict.First();
                foreach (KeyValuePair<int, int> tag in tagsDict)
                {
                    if (tag.Value > maxTag.Value) maxTag = tag;
                }

                List<int> keysToRemove = new List<int>();
                foreach (KeyValuePair<int, int> tag in tagsDict)
                {
                    double percentage = ((double)tag.Value / (double)maxTag.Value) * 100;

                    if (percentage <= noiseThreshold)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            for (int y = 0; y < height; y++)
                            {
                                if (matrix[x, y] == tag.Key)
                                    matrix[x, y] = 0;
                            }
                        }
                        keysToRemove.Add(tag.Key);
                    }
                }

                foreach (int key in keysToRemove)
                {
                    tagsDict.Remove(key);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Raised exception: " + e);
            }

            return tagsDict;
        }

        public static int CheckBrigthnessLevel(Image<Bgr, byte> img, int startX, int endX, int startY, int endY)
        {
            unsafe
            {
                MIplImage mAux = img.MIplImage;
                byte* dataPtr = (byte*)mAux.imageData.ToPointer();

                int nChan = mAux.nChannels; // number of channels - 3
                int w = mAux.width;
                int ws = mAux.widthStep;
                int padding = ws - nChan * w;
                int x, y;

                int size = (endX - startX) * (endY - startY);
                int blue, green, red;
                int level = 0;

                if (nChan == 3)
                {
                    for (y = startY; y <= endY; y++)
                    {
                        for (x = startX; x <= endX; x++)
                        {
                            blue = (byte)(dataPtr + y * ws + x * nChan)[0];
                            green = (byte)(dataPtr + y * ws + x * nChan)[1];
                            red = (byte)(dataPtr + y * ws + x * nChan)[2];

                            level += (byte)Math.Round(((int)blue + green + red) / 3.0);
                        }
                    }
                }

                level /= size;
                return level;
            }
        }

        public static void DrawSquare(Image<Bgr, byte> img, int startX, int endX, int startY, int endY)
        {
            unsafe
            {
                int width = img.Width;
                int height = img.Height;
                MIplImage mAux = img.MIplImage;
                int nChan = mAux.nChannels; // number of channels - 3
                int w = mAux.width;
                int ws = mAux.widthStep;
                int padding = ws - nChan * w;
                int x, y;

                byte* dataPtr = (byte*)mAux.imageData.ToPointer();

                for (x = startX - 2; x <= endX + 2; x++)
                {
                    (dataPtr + startY * ws + x * nChan)[0] = 138;
                    (dataPtr + startY * ws + x * nChan)[1] = 240;
                    (dataPtr + startY * ws + x * nChan)[2] = 138;

                    (dataPtr + (startY - 1) * ws + x * nChan)[0] = 138;
                    (dataPtr + (startY - 1) * ws + x * nChan)[1] = 240;
                    (dataPtr + (startY - 1) * ws + x * nChan)[2] = 138;

                    (dataPtr + (startY - 2) * ws + x * nChan)[0] = 138;
                    (dataPtr + (startY - 2) * ws + x * nChan)[1] = 240;
                    (dataPtr + (startY - 2) * ws + x * nChan)[2] = 138;

                    (dataPtr + endY * ws + x * nChan)[0] = 138;
                    (dataPtr + endY * ws + x * nChan)[1] = 240;
                    (dataPtr + endY * ws + x * nChan)[2] = 138;

                    (dataPtr + (endY + 1) * ws + x * nChan)[0] = 138;
                    (dataPtr + (endY + 1) * ws + x * nChan)[1] = 240;
                    (dataPtr + (endY + 1) * ws + x * nChan)[2] = 138;

                    (dataPtr + (endY + 2) * ws + x * nChan)[0] = 138;
                    (dataPtr + (endY + 2) * ws + x * nChan)[1] = 240;
                    (dataPtr + (endY + 2) * ws + x * nChan)[2] = 138;
                }

                for (y = startY - 2; y <= endY + 2; y++)
                {
                    (dataPtr + y * ws + startX * nChan)[0] = 138;
                    (dataPtr + y * ws + startX * nChan)[1] = 240;
                    (dataPtr + y * ws + startX * nChan)[2] = 138;

                    (dataPtr + y * ws + (startX - 1) * nChan)[0] = 138;
                    (dataPtr + y * ws + (startX - 1) * nChan)[1] = 240;
                    (dataPtr + y * ws + (startX - 1) * nChan)[2] = 138;

                    (dataPtr + y * ws + (startX - 2) * nChan)[0] = 138;
                    (dataPtr + y * ws + (startX - 2) * nChan)[1] = 240;
                    (dataPtr + y * ws + (startX - 2) * nChan)[2] = 138;

                    (dataPtr + y * ws + endX * nChan)[0] = 138;
                    (dataPtr + y * ws + endX * nChan)[1] = 240;
                    (dataPtr + y * ws + endX * nChan)[2] = 138;

                    (dataPtr + y * ws + (endX + 1) * nChan)[0] = 138;
                    (dataPtr + y * ws + (endX + 1) * nChan)[1] = 240;
                    (dataPtr + y * ws + (endX + 1) * nChan)[2] = 138;

                    (dataPtr + y * ws + (endX + 2) * nChan)[0] = 138;
                    (dataPtr + y * ws + (endX + 2) * nChan)[1] = 240;
                    (dataPtr + y * ws + (endX + 2) * nChan)[2] = 138;
                }
            }
        }

        public static bool CheckIfCircleSign(Image<Bgr, byte> img, int startX, int endX, int startY, int endY)
        {
            unsafe
            {
                MIplImage mAux = img.MIplImage;
                byte* dataPtrO = (byte*)mAux.imageData.ToPointer();

                int nChan = mAux.nChannels; // number of channels - 3
                int w = mAux.width;
                int ws = mAux.widthStep;
                int padding = ws - nChan * w;

                int blue, green, red;

                int x, y;
                int topRedPixels = 0;
                int bottomRedPixels = 0;
                int halfHeight = (endY - startY) / 2;

                for (y = startY; y <= endY; y++)
                {
                    for (x = startX; x <= endX; x++)
                    {
                        blue = (byte)(dataPtrO + y * ws + x * nChan)[0];
                        green = (byte)(dataPtrO + y * ws + x * nChan)[1];
                        red = (byte)(dataPtrO + y * ws + x * nChan)[2];

                        double[] hsvArray = ConvertRGBtoHSV(red, green, blue);
                        double hue = hsvArray[0];
                        double saturation = hsvArray[1];
                        double value = hsvArray[2];

                        if ((0 <= hue && hue <= 10 || 340 <= hue && hue < 360) &&
                            40 <= saturation && saturation <= 100 &&
                            30 <= value && value <= 100)
                        {
                            if(y <= startY + halfHeight) topRedPixels++;
                            if(y > startY + halfHeight) bottomRedPixels++;
                        }
                    }
                }

                return topRedPixels < (0.8 * bottomRedPixels) || bottomRedPixels < (0.8 * topRedPixels) ? false : true;
            }
        }

        public static string IdentifySignContent(List<Image<Bgr, byte>> segments)
        {
            unsafe
            {
                string content = "";
         
                foreach (Image<Bgr, byte> segment in segments)
                {
                    ConvertToBW_Otsu(segment);
                    MIplImage segDigit = segment.MIplImage;
                    int width = segDigit.width;
                    int height = segDigit.height;

                    int maxMatching = 0;
                    string bestMatch = "";

                    for (int i = 0; i < 10; i++)
                    {
                        int matchingPixels = 0;
                        string imgPath = @"D:\\FCT_MIEEC\\Mestrado\\4º Ano\\SS\\Prática\\SS_OpenCV_Base\\SS_OpenCV\\bin\\Debug\\digitos\\" + i.ToString() + ".png";
                        Image<Bgr, byte> digit = new Image<Bgr, byte>(imgPath);
                        Image<Bgr, byte> resizedDigit = digit.Resize(width, height, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
                        ConvertToBW_Otsu(resizedDigit);
                        MIplImage d = resizedDigit.MIplImage;
                        byte* dataPtrD = (byte*)d.imageData.ToPointer();
                        byte* dataPtrS = (byte*)segDigit.imageData.ToPointer();

                        int nChan = d.nChannels;
                        int padding = d.widthStep - d.nChannels * d.width;

                        if (nChan == 3)
                        {
                            for (int y = 0; y < height; y++)
                            {
                                for(int x = 0; x < width; x++)
                                {
                                    if (dataPtrD[0] == dataPtrS[0] &&
                                        dataPtrD[1] == dataPtrS[1] &&
                                        dataPtrD[2] == dataPtrS[2])
                                    {
                                        matchingPixels++;
                                    }

                                    dataPtrS += nChan;
                                    dataPtrD += nChan;
                                }

                                dataPtrS += padding;
                                dataPtrD += padding;
                            }

                            if (matchingPixels > maxMatching)
                            {
                                maxMatching = matchingPixels;
                                bestMatch = i.ToString();
                            }
                        }
                    }

                    content += bestMatch;
                }

                return content;
            }
        }

        public static Image<Bgr, byte> Signs(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, out List<string[]> limitSign, out List<string[]> warningSign, out List<string[]> prohibitionSign, int level)
        {
            unsafe
            {
                limitSign = new List<string[]>();
                warningSign = new List<string[]>();
                prohibitionSign = new List<string[]>();

                MIplImage mResult = img.MIplImage;
                MIplImage mAux = imgCopy.MIplImage;

                byte* dataPtrO = (byte*)mAux.imageData.ToPointer(); // pointer to the original image
                byte* dataPtrD = (byte*)mResult.imageData.ToPointer(); // pointer to the duplicate image

                int blue, green, red;

                int width = img.Width;
                int height = img.Height;
                int nChan = mAux.nChannels; // number of channels - 3
                int w = mAux.width;
                int ws = mAux.widthStep;
                int padding = ws - nChan * w;
                int x, y;

                int[,] tagMatrix = new int[width, height];
                int currentTag = 1;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            blue = (int)dataPtrO[0];
                            green = (int)dataPtrO[1];
                            red = (int)dataPtrO[2];

                            double[] hsvArray = ConvertRGBtoHSV(red, green, blue);
                            double hue = hsvArray[0];
                            double saturation = hsvArray[1];
                            double value = hsvArray[2];

                            if ((0 <= hue && hue <= 10 || 330 <= hue && hue < 360) &&
                                30 <= saturation && saturation <= 100 &&
                                30 <= value && value <= 100)
                            {
                                tagMatrix[x, y] = currentTag;
                                currentTag++;
                            }
                            else tagMatrix[x, y] = 0;

                            dataPtrO += nChan;
                        }
                        dataPtrO += padding;
                    }
                }

                int[,] propMatrix = ConnectedComponentsAlgorithm(tagMatrix);
                //ConvertMatrixIntoCSV(propMatrix);

                Dictionary<int, int> tagsDict = RemoveNoiseTags(propMatrix, 10.0);
                //ConvertMatrixIntoCSV(propMatrix);

                List<string[]> detectedSigns = new List<string[]>();
                foreach (KeyValuePair<int, int> tag in tagsDict)
                {
                    string[] aux = new string[5];
                    aux[0] = "-1";

                    for (x = 0; x < width; x++)
                    {
                        for (y = 0; y < height; y++)
                        {
                            if (propMatrix[x, y] == tag.Key)
                            {
                                if (aux[1] == null || Convert.ToInt32(aux[1]) > x)
                                {
                                    aux[1] = x.ToString();
                                }

                                if (aux[3] == null || Convert.ToInt32(aux[3]) < x)
                                {
                                    aux[3] = x.ToString();
                                }

                                if (aux[4] == null || Convert.ToInt32(aux[4]) > y)
                                {
                                    aux[4] = y.ToString();
                                }

                                if (aux[2] == null || Convert.ToInt32(aux[2]) < y)
                                {
                                    aux[2] = y.ToString();
                                }
                            }

                        }
                    }

                    detectedSigns.Add(aux);
                }

                foreach (string[] sign in detectedSigns)
                {
                    dataPtrO = (byte*)mAux.imageData.ToPointer();

                    int xm = Convert.ToInt32(sign[1]);
                    int xM = Convert.ToInt32(sign[3]);
                    int ym = Convert.ToInt32(sign[4]);
                    int yM = Convert.ToInt32(sign[2]);
                    DrawSquare(img, xm, xM, ym, yM);

                    // Identify shape of sign here, to avoid extra processing, speeding up the program
                    if (CheckIfCircleSign(img, xm, xM, ym, yM))
                    {
                        // Capturing only the sign interior using percentages of size
                        int innerXm = (int)(xm + 0.125 * (xM - xm));
                        int innerXM = (int)(xM - 0.125 * (xM - xm));
                        int innerYm = (int)(ym + 0.25 * (yM - ym));
                        int innerYM = (int)(yM - 0.25 * (yM - ym));

                        //DrawSquare(img, innerXm, innerXM, innerYm, innerYM);

                        int minValue = 0;
                        int maxValue = 30;
                        int brightness = CheckBrigthnessLevel(img, innerXm, innerXM, innerYm, innerYM);

                        if (brightness > 180)
                        {
                            minValue = 20;
                            maxValue = 60;
                        }

                        currentTag = 1;
                        int[,] digitsMatrix = new int[width, height];

                        for (y = innerYm; y <= innerYM; y++)
                        {
                            for (x = innerXm; x <= innerXM; x++)
                            {
                                blue = (byte)(dataPtrO + y * ws + x * nChan)[0];
                                green = (byte)(dataPtrO + y * ws + x * nChan)[1];
                                red = (byte)(dataPtrO + y * ws + x * nChan)[2];

                                double[] hsvArray = ConvertRGBtoHSV(red, green, blue);
                                double hue = hsvArray[0];
                                double saturation = hsvArray[1];
                                double value = hsvArray[2];

                                if (0 <= hue && hue < 360 &&
                                    0 <= saturation && saturation <= 100 &&
                                    minValue <= value && value <= maxValue)
                                {
                                    digitsMatrix[x, y] = currentTag;
                                }
                                else digitsMatrix[x, y] = 0;

                                currentTag++;
                            }
                        }

                        digitsMatrix = ConnectedComponentsAlgorithm(digitsMatrix);

                        Dictionary<int, int> tagsDigitsDict = RemoveNoiseTags(digitsMatrix, 45);

                        List<Image<Bgr, byte>> segmentedDigits = new List<Image<Bgr, byte>>();
                        foreach (KeyValuePair<int, int> tag in tagsDigitsDict)
                        {
                            string[] aux = new string[4];

                            for (x = 0; x < width; x++)
                            {
                                for (y = 0; y < height; y++)
                                {
                                    if (digitsMatrix[x, y] == tag.Key)
                                    {
                                        if (aux[0] == null || Convert.ToInt32(aux[0]) > x)
                                        {
                                            aux[0] = x.ToString();
                                        }

                                        if (aux[1] == null || Convert.ToInt32(aux[1]) < x)
                                        {
                                            aux[1] = x.ToString();
                                        }

                                        if (aux[2] == null || Convert.ToInt32(aux[2]) > y)
                                        {
                                            aux[2] = y.ToString();
                                        }

                                        if (aux[3] == null || Convert.ToInt32(aux[3]) < y)
                                        {
                                            aux[3] = y.ToString();
                                        }
                                    }

                                }
                            }

                            int startX = Convert.ToInt32(aux[0]);
                            int startY = Convert.ToInt32(aux[2]);
                            int endX = Convert.ToInt32(aux[1]);
                            int endY = Convert.ToInt32(aux[3]);
                            int digitWidth = endX - startX;
                            int digitHeight = endY - startY;

                            // DrawSquare(img, startX, endX, startY, endY);
                            Image<Bgr, byte> segment = img.Copy(new System.Drawing.Rectangle(startX, startY, digitWidth, digitHeight));
                            segmentedDigits.Add(segment);
                        }

                        string signContent = IdentifySignContent(segmentedDigits);
                        sign[0] = signContent;

                        if (sign[0].Contains("0"))
                        {
                            limitSign.Add(sign);
                        }
                        else
                        {
                            sign[0] = "-1";
                            prohibitionSign.Add(sign);
                        }
                    }
                    else warningSign.Add(sign);

                    //Console.WriteLine(sign[0]);
                    //Console.WriteLine(sign[1]);
                    //Console.WriteLine(sign[2]);
                    //Console.WriteLine(sign[3]);
                    //Console.WriteLine(sign[4]);
                }
                return img;
            }
        }
    }
}
