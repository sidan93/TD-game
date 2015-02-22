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
using TD.Enums;


namespace TD.Towers
{   
    class SingleTower : CommonTower
    {
        public SingleTower(RenderTarget RenderTarget2D, Vector2 position) :
            base(RenderTarget2D, position)
        {
            _range = 400;
            LoadFromFile("Tower01.png");

            _type = ETowers.SingleTower;
        }
    }
}
