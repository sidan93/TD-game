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

        public CommonBullet(RenderTarget RenderTarget2D, Vector2 position, DemoTime time = null) :
            base(RenderTarget2D, "Bullet01.png", new Size2F(4, 4), position, time)
        {
            _speed = 6;
            _lifeTime = 3;
        }

        public override void Draw(DemoTime time)
        {
            base.Draw(time);
        }

        public override void Update(DemoTime time)
        {
            base.Update(time);
            if (_isArrive || TimeOfDeath(time))
                this.Destroy();
        }
    }
}
