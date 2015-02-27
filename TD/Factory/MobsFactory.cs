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
        List<CommonMob> _mobs;

        double _lastCreate;

        public MobsFactory(RenderTarget RenderTarget2D, GameInterface GameInterface) :
            base(RenderTarget2D, GameInterface)
        
        {
            _mobs = new List<CommonMob>();
        }

        public override void Draw(DemoTime time)
        {
            base.Draw(time);
        
            foreach(var mob in _mobs)
            {
                mob.Draw(time);
            }
        }

        public override void Update(DemoTime time)
        {
            base.Update(time);

            if (time.ElapseTime - _lastCreate > 1)
            {
                var newMob = new CommonMob(RenderTarget2D);
                var rand = new Random();
                newMob.MoveTo(new Vector2(rand.Next(0, 1200), rand.Next(0, 900)));
                _mobs.Add(newMob);
                _lastCreate = time.ElapseTime;
            }
               
            foreach (var mob in _mobs)
            {
                mob.Update(time);
            }
        }
    }
}
