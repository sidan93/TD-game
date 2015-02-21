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
using TD.Towers;
using TD.Enums;

namespace TD.Factory
{
    class BuildingsFactory 
    {
        List<CommonTower> _towers;
        RenderTarget RenderTarget2D;

        public BuildingsFactory(RenderTarget RenderTarget2D)
        {
            this.RenderTarget2D = RenderTarget2D;
            _towers = new List<CommonTower>();
        }

        public void Draw(DemoTime time)
        {
            _towers.ForEach(delegate(CommonTower Tower)
            {
                Tower.Draw(time);
            });
        }

        public void Update(DemoTime time, Vector2 position)
        {
            _towers.ForEach(delegate(CommonTower Tower)
            {
                Tower.Update(time);
            });

            _towers.ForEach(delegate(CommonTower Tower)
            {
                Tower.Fire(time, position);
            });
        }

        // Уставить башню
        public bool SetTower(ETowers tower, Vector2 position)
        {
            CommonTower outTower = null;
            if (tower == ETowers.SingleTower)
                outTower = new SingleTower(RenderTarget2D, position);

            if (tower == ETowers.CrazyTower)
                outTower = new CrazyTower(RenderTarget2D, position);

            if (outTower != null)
                _towers.Add(outTower);

            return true;
        }

        public void MouseMove(int X, int Y)
        {
            foreach (var item in _towers)
            {
                item.MouseMove(X, Y);
            }
        }
    }
}
