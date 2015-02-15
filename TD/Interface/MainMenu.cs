using System;
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
using RectangleF = SharpDX.RectangleF;

using TD.Common;

namespace TD.Interface
{
    public class MainMenu : IOutObject
    {
        protected RenderTarget RenderTarget2D;

        String _backgroundPath;
        Bitmap _backgroundFile;
        Color4 _backgroundColor;
        RectangleF _outRectangle;

        public MainMenu(RenderTarget RenderTarget2D, Size2 resolution)
        {
            this.RenderTarget2D = RenderTarget2D;
            _backgroundPath = "background01.png";
            _backgroundFile = Helpers.LoadFromFile(RenderTarget2D, _backgroundPath);
            _backgroundColor = new Color4(181.0f / 255, 188.0f / 255, 1, 1);
            _outRectangle = new RectangleF(resolution.Width / 2 - 250, resolution.Height / 2 - 250, 500, 500);
        }

        public void Draw(DemoTime time)
        {
            RenderTarget2D.Clear(_backgroundColor);
            RenderTarget2D.DrawBitmap(_backgroundFile, _outRectangle, 1.0f, BitmapInterpolationMode.Linear);
        }

        public void Update(DemoTime time)
        {
        }
    }
}
