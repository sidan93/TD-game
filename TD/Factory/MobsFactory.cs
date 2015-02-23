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

using TD.Common;
using TD.Interface;

namespace TD.Factory
{
    class MobsFactory : CommonFactory
    {
        List<CommonMob> mobs;

        public MobsFactory(RenderTarget RenderTarget2D, GameInterface GameInterface) :
            base(RenderTarget2D, GameInterface)
        
        {
        }
    }
}
