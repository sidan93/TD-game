﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using System.Drawing;
using System.Drawing.Imaging;

using SharpDX;
using SharpDX.DXGI;
using SharpDX.Direct2D1;

using SharpDX.Samples;

using AlphaMode = SharpDX.Direct2D1.AlphaMode;
using Bitmap = SharpDX.Direct2D1.Bitmap;
using PixelFormat = SharpDX.Direct2D1.PixelFormat;
using System.IO;
using SharpDX.Multimedia;
using SharpDX.MediaFoundation;

namespace TD
{
    static class Helpers
    {
        public static Bitmap LoadFromFile(RenderTarget renderTarget, string file)
        {
            file = "Assets/img/" + file;
            // Loads from file using System.Drawing.Image
            using (var bitmap = (System.Drawing.Bitmap)System.Drawing.Image.FromFile(file))
            {
                var sourceArea = new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height);
                var bitmapProperties = new BitmapProperties(new PixelFormat(Format.R8G8B8A8_UNorm, AlphaMode.Premultiplied));
                var size = new Size2(bitmap.Width, bitmap.Height);

                // Transform pixels from BGRA to RGBA
                int stride = bitmap.Width * sizeof(int);
                using (var tempStream = new DataStream(bitmap.Height * stride, true, true))
                {
                    // Lock System.Drawing.Bitmap
                    var bitmapData = bitmap.LockBits(sourceArea, ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

                    // Convert all pixels 
                    for (int y = 0; y < bitmap.Height; y++)
                    {
                        int offset = bitmapData.Stride * y;
                        for (int x = 0; x < bitmap.Width; x++)
                        {
                            // Not optimized 
                            byte B = Marshal.ReadByte(bitmapData.Scan0, offset++);
                            byte G = Marshal.ReadByte(bitmapData.Scan0, offset++);
                            byte R = Marshal.ReadByte(bitmapData.Scan0, offset++);
                            byte A = Marshal.ReadByte(bitmapData.Scan0, offset++);
                            int rgba = R | (G << 8) | (B << 16) | (A << 24);
                            tempStream.Write(rgba);
                        }

                    }
                    bitmap.UnlockBits(bitmapData);
                    tempStream.Position = 0;

                    return new Bitmap(renderTarget, size, tempStream, stride, bitmapProperties);
                }
            }
        }

        public static bool InRange(Vector2 obj, Vector2 target, double range)
        {
            return Range(obj, target) < range*range;
        }
        public static double Range(Vector2 obj, Vector2 target)
        {
            return Range(obj, target.X, target.Y);
        }
        public static double Range(Vector2 obj, float X, float Y)
        {
            return Range(obj.X, obj.Y, X, Y);
        }
        public static double Range(float X1, float Y1, float X2, float Y2)
        {
            return (X1 - X2) * (X1 - X2) + (Y1 - Y2) * (Y1 - Y2);
        }

        public static void DecodeAudioToWav(string fileName)
        {
            fileName = "Assets/sounds/" + fileName;
            var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            var audioDecoder = new AudioDecoder(stream);

            var outputWavStream = new FileStream(fileName + ".wav", FileMode.Create, FileAccess.Write);

            var wavWriter = new WavWriter(outputWavStream);

            // Write the WAV file
            wavWriter.Begin(audioDecoder.WaveFormat);

            // Decode the samples from the input file and output PCM raw data to the WAV stream.
            wavWriter.AppendData(audioDecoder.GetSamples());

            // Close the wav writer.
            wavWriter.End();

            audioDecoder.Dispose();
            outputWavStream.Close();
            stream.Close();
        }
    
    }
        
}
