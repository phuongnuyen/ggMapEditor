﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ggMapEditor
{
    public static class BitmapImageExtensions
    {
        // Compare 2 images
        public static bool IsEqual(this BitmapSource img1, BitmapSource img2)
        {
            if (img1 == null || img2 == null)
                return false;
            return img1.ToBytes().SequenceEqual(img2.ToBytes());
        }
        public static byte[] ToBytes(this BitmapSource img)
        {
            byte[] data = new byte[] { };
            if (img != null)
            {
                var encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(img));
                using (MemoryStream ms = new MemoryStream())
                {
                    encoder.Save(ms);
                    data = ms.ToArray();
                }
            }
            return data;
        }

        public static RenderTargetBitmap MergeImage(this List<BitmapSource> imgs)
        {
            if (imgs == null)
                return null;

            BitmapFrame frame1 = BitmapFrame.Create(imgs[0]);
            int width = frame1.PixelWidth;
            int height = frame1.PixelHeight;

            int mergeImgWidth = 0;
            int mergeImgHeight = 0;

            DrawingVisual drVisual = new DrawingVisual();
            using (DrawingContext drContext = drVisual.RenderOpen())
            {
                for (int i = 0; i < imgs.Count; i++)
                {
                    BitmapFrame frame = BitmapFrame.Create(imgs[i]);
                    drContext.DrawImage(frame, new Rect(i*width + 1, 0, width, height));
                    mergeImgWidth = (i + 1) * width;
                    mergeImgHeight = height;
                }
            }
            RenderTargetBitmap bmp = new RenderTargetBitmap(mergeImgWidth, mergeImgHeight, 96, 96, PixelFormats.Pbgra32);
            bmp.Render(drVisual);
            return bmp;
        }
        public static RenderTargetBitmap MergeImageDifference(this List<BitmapSource> imgs)
        {
            List<BitmapSource> listDif = new List<BitmapSource>();
            listDif.Add(imgs[0]);
            for (int i = 1; i < imgs.Count(); i++)
            {
                bool isExist = false;
                for (int k = 0; k < listDif.Count(); k++)
                    if (imgs[i].IsEqual(imgs[k]))
                    {
                        isExist = true;
                        break;
                    }
                if (!isExist)
                    listDif.Add(imgs[i]);
            }
            return MergeImage(listDif);
        }

        public static BitmapImage ToBitmapImage(this RenderTargetBitmap rtBitmap)
        {
            MemoryStream stream = new MemoryStream();
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(rtBitmap));
            encoder.QualityLevel = 80;
            encoder.Save(stream);

            BitmapImage bm = new BitmapImage();
            bm.BeginInit();
            bm.StreamSource = stream;
            bm.EndInit();

            return bm;
        }
        public static BitmapImage ToBitmapImage(this CroppedBitmap crImg)
        {
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(crImg));
            encoder.QualityLevel = 80;
            BitmapImage bm = new BitmapImage();

            using (MemoryStream stream = new MemoryStream())
            {
                encoder.Save(stream);
                stream.Seek(0, SeekOrigin.Begin);
                bm.BeginInit();
                bm.CacheOption = BitmapCacheOption.OnLoad;
                bm.StreamSource = stream;
                bm.EndInit();
            }
            return bm;
        }



        public static void SaveImage(this RenderTargetBitmap img, string filePath)
        {
            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(img));
            var fileStream = new FileStream(filePath, FileMode.Create);
            using (fileStream)
            {
                encoder.Save(fileStream);
            }
        }



        public static List<BitmapSource> CropImage(BitmapImage img, int cellWidth = 32, int cellHeight = 32)
        {
            List<BitmapSource> bmSources = new List<BitmapSource>();
            if (img != null && cellWidth > 0 && cellHeight > 0)
            {
                int width = img.PixelWidth;
                int height = img.PixelHeight;

                for (int i = 0; i < height - cellHeight - 1; i += cellHeight + 1)
                    for (int k = 0; k < width - cellWidth - 1; k += cellWidth + 1)
                    {
                        Int32Rect rect = new Int32Rect(k, i, cellWidth, cellHeight);
                        CroppedBitmap croppedBitmap = new CroppedBitmap(img, rect);

                        bool isExist = false;
                        for (int m = 0; m < bmSources.Count; m++)
                                if (croppedBitmap.IsEqual(bmSources[m]))
                                {
                                    isExist = true;
                                    break;
                                }
                        if (!isExist)
                        bmSources.Add(croppedBitmap);
                    }                
            }
            return bmSources;
        }
    }
}
