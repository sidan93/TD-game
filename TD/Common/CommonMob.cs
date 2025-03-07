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

namespace TD
{
    class CommonMob : CommonDinamicObject
    {
        public CommonMob(RenderTarget RenderTarget2D) :
            this(RenderTarget2D, new Vector2(0, 0))
        {
         
        }

        public CommonMob(RenderTarget RenderTarget2D, Vector2 position) :
            base(RenderTarget2D, "003.png", position, new Size2F(20, 20))
        {

        }

        public override void Draw(DemoTime time)
        {
            base.Draw(time);
        }

        public override void Update(DemoTime time)
        {
            base.Update(time);
            if (_isArrive)
            {
                var rand = new Random();
                MoveTo(new Vector2(rand.Next(0, 1200), rand.Next(0, 900)));
            }
        }
    }
}
