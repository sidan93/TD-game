﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TD.Interface.Components;
using TD.Common;

using SharpDX.Direct2D1;
using SharpDX;
using SharpDX.Samples;
using SharpDX.DirectWrite;
using System.Windows.Forms;

namespace TD.Interface.Components
{
    class InfoPanel : CommonComponent
    {
        string _towerId;
        string _towerPosition;
        float _towerRange;

        public InfoPanel(RenderTarget RenderTarget2D, SharpDX.DirectWrite.Factory Factory_, SolidColorBrush Brush, Vector2 position, Size2F size) :
            base(RenderTarget2D, Factory_, Brush, "chat01.png", position, size)
        {
            _margin = new Vector4(5, 5, 5, 5);
        }

        public override void Draw(DemoTime time)
        {
            if (!_visible)
                return;

            base.Draw(time);

            var start = getPosition(Vector2.Zero);
            var size = getSize();
            RenderTarget2D.DrawText("ID башни: " + _towerId, _textFormat, new RectangleF(start.X, start.Y, size.Width, size.Height), _brush);
            start = getPosition(new Vector2(0, _lineHeight));
            RenderTarget2D.DrawText("Позиция: " + _towerPosition, _textFormat, new RectangleF(start.X, start.Y, size.Width, size.Height), _brush);
            start = getPosition(new Vector2(0, _lineHeight * 2));
            RenderTarget2D.DrawText("Радиус: " + _towerRange, _textFormat, new RectangleF(start.X, start.Y, size.Width, size.Height), _brush);
        }

        public void Show(CommonTower tower)
        {
            base.Show();

            _towerId = tower.Id.ToString();
            _towerPosition = tower.Position.ToString();
            _towerRange = tower.Range;
        }
    }
}
