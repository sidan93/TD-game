using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using TD.Interface.Components;
using TD.Common;
using TD.Player;

using SharpDX.Direct2D1;
using SharpDX;
using SharpDX.Samples;
using SharpDX.DirectWrite;

namespace TD.Interface.Components
{
    class InfoHeroPanel : CommonComponent
    {

        string _playerPosition;
        string _playerSpeed;
        string _playerName;

        public InfoHeroPanel(RenderTarget RenderTarget2D, SharpDX.DirectWrite.Factory Factory_, SolidColorBrush Brush, Vector2 position, Size2F size) :
            base(RenderTarget2D, Factory_, Brush, "chat01.png", position, size)
        {
            _margin = new Vector4(5, 5, 5, 5);
            _textFormat.WordWrapping = WordWrapping.NoWrap;
        }

        public override void Draw(DemoTime time)
        {
            if (!_visible)
                return;

            base.Draw(time);

            var start = getPosition(Vector2.Zero);
            var size = getSize();
            RenderTarget2D.DrawText("ID героя: " + 0, _textFormat, new RectangleF(start.X, start.Y, size.Width, size.Height), _brush);
            start = getPosition(new Vector2(0, _lineHeight));
            RenderTarget2D.DrawText("Имя героя: " + _playerName, _textFormat, new RectangleF(start.X, start.Y, size.Width, size.Height), _brush);
            start = getPosition(new Vector2(0, _lineHeight * 2));
            RenderTarget2D.DrawText("Тип героя: " + 0, _textFormat, new RectangleF(start.X, start.Y, size.Width, size.Height), _brush);
            start = getPosition(new Vector2(0, _lineHeight * 3));
            RenderTarget2D.DrawText("Позиция: " + _playerPosition, _textFormat, new RectangleF(start.X, start.Y, size.Width, size.Height), _brush);
            start = getPosition(new Vector2(0, _lineHeight * 4));
            RenderTarget2D.DrawText("Скорость перемещения: " + _playerSpeed, _textFormat, new RectangleF(start.X, start.Y, size.Width, size.Height), _brush);
        }

        public void Show(Player.Player player)
        {
            base.Show();

            _playerPosition = player.Position.ToString();
            _playerSpeed = player.Speed.ToString();
            _playerName = player.Name;
        }
    }
}
