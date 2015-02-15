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

namespace TD.Common
{
    class CommonBullet : CommonDinamicObject
    {
        public CommonBullet(RenderTarget RenderTarget2D) :
            base(RenderTarget2D)
        {
        }
        public CommonBullet(RenderTarget RenderTarget2D, Vector2 position) :
            base(RenderTarget2D, "Bullet01.png", new Size2F(4, 4), position)
        {
            _speed = 6;
        }

        public override void Draw(DemoTime time)
        {
            base.Draw(time);
        }

        public override void Update(DemoTime time)
        {
            base.Update(time);
            if (_isArrive)
                this.Destroy();
        }

    }
}
