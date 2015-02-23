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
using Point = SharpDX.Point;

using TD.Towers;
using TD.Enums;
using TD.Interface;


namespace TD.Common
{
    class CommonFactory : IOutObject
    {
        protected RenderTarget RenderTarget2D;
        protected GameInterface GameInterface;

        public CommonFactory(RenderTarget RenderTarget2D, GameInterface GameInterface)
        {
            this.RenderTarget2D = RenderTarget2D;
            this.GameInterface = GameInterface;
        }

        public virtual void Draw(DemoTime time)
        {

        }

        public virtual void Update(DemoTime time)
        {
        }

        public virtual void MouseMove(int X, int Y)
        {

        }

    }
}
