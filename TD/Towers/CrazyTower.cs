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
using TD.Core;
using System.Threading;
using System.Threading.Tasks;
using TD.Sounds;

namespace TD.Towers
{
    class CrazyTower : CommonTower
    {
        AudioPlayer _soundShoot;
        public CrazyTower(RenderTarget RenderTarget2D, Vector2 position) :
            base(RenderTarget2D, position)
        {
            _range = 1000;
            _speedFire = 1;
            _bulletType = EBullet.FastBullet;
            LoadFromFile("test.gif");

            _type = ETowers.CrazyTower;
            _soundShoot = new AudioPlayer("shoot01.mp3");
        }

        public override void Fire(DemoTime time, Vector2 position)
        {
            base.Fire(time, position);
            _soundShoot.Play();
        }
    }
}
