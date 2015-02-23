using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using TD.Interface.Components;
using TD.Common;
using TD.Core;

using SharpDX.Direct2D1;
using SharpDX;
using SharpDX.Samples;
using SharpDX.DirectWrite;

namespace TD.Interface.Components
{
    class InfoStatsPanel : CommonComponent
    {
        GameStats GameStats;
        protected SolidColorBrush _brushRed;

        private bool _isRedBrush;
        private double _timeRedBrush;
        private bool _setRedBrush;

        public InfoStatsPanel(RenderTarget RenderTarget2D, SharpDX.DirectWrite.Factory Factory_, SolidColorBrush Brush, GameStats GameStats, Vector2 position, Size2F size) :
            base(RenderTarget2D, Factory_, Brush, "chat01.png", position, size)
        {
            _margin = new Vector4(5, 5, 5, 5);
            _textFormat.WordWrapping = WordWrapping.NoWrap;

            _brushRed = new SolidColorBrush(RenderTarget2D, new Color4(1, 0, 0, 1));
            _isRedBrush = false;
            _setRedBrush = false;
            this.GameStats = GameStats;
        }

        public override void Draw(DemoTime time)
        {
            if (!_visible || GameStats == null)
                return;

            base.Draw(time);

            var drawBrush = _brush;
            if (_isRedBrush)
                drawBrush = _brushRed;
            var start = getPosition(Vector2.Zero);
            var size = getSize();
            RenderTarget2D.DrawText("Деньги: " + GameStats.Money, _textFormat, new RectangleF(start.X, start.Y, size.Width, size.Height), drawBrush);
            start = getPosition(new Vector2(0, _lineHeight));
            RenderTarget2D.DrawText("Дерево: " + GameStats.Woods, _textFormat, new RectangleF(start.X, start.Y, size.Width, size.Height), drawBrush);
        }

        public override void Update(DemoTime time)
        {
            base.Update(time);
        
            if (_setRedBrush)
            {
                _isRedBrush = true;
                _setRedBrush = false;
                _timeRedBrush = time.ElapseTime;
            }

            if (_isRedBrush)
                if (time.ElapseTime - _timeRedBrush > 2)
                    _isRedBrush = false;
            
        }

        public void setRedBrush()
        {
            _setRedBrush = true;
        }
    }
}
