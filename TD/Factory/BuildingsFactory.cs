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
using Point = SharpDX.Point;

using TD.Common;
using TD.Towers;
using TD.Enums;
using TD.Interface;

namespace TD.Factory
{
    class BuildingsFactory : CommonFactory
    {
        List<CommonTower> _towers;

        public BuildingsFactory(RenderTarget RenderTarget2D, GameInterface GameInterface) :
            base(RenderTarget2D, GameInterface)
        {
            _towers = new List<CommonTower>();
        }

        public void Draw(DemoTime time)
        {
            base.Draw(time);

            _towers.ForEach(delegate(CommonTower Tower)
            {
                Tower.Draw(time);
            });
        }

        public void Update(DemoTime time, Vector2 position)
        {
            base.Update(time);

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
        public CommonTower SetTower(ETowers tower, Vector2 position)
        {
            CommonTower outTower = null;
            if (tower == ETowers.SingleTower)
                outTower = new SingleTower(RenderTarget2D, position);

            if (tower == ETowers.CrazyTower)
                outTower = new CrazyTower(RenderTarget2D, position);

            if (outTower != null)
            {
                outTower.eventMouseOver += outTower_isMouseOver;
                _towers.Add(outTower);
            }
            return outTower;
        }

        void outTower_isMouseOver(CommonTower tower)
        {
            GameInterface.InfoTower(tower);
        }

        public override void MouseMove(int X, int Y)
        {
            foreach (var item in _towers)
            {
                item.MouseMove(X, Y);
            }
        }

        public override int FactorySize { get { return _towers.Count; } }
    }
}
