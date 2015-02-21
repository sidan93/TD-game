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
using RectangleF = SharpDX.RectangleF;

using TD.Common;

namespace TD.Bullet
{
    class FastBullet : CommonBullet
    {
        public FastBullet(RenderTarget RenderTarget2D, Vector2 position, DemoTime time) :
            base(RenderTarget2D, position, time)
        {
            _speed = 18;
        }

    }
}
