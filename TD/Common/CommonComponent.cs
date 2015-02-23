using System;
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


namespace TD.Common
{
    class CommonComponent : CommonObject
    {
        protected TextFormat _textFormat;
        protected SharpDX.DirectWrite.Factory _factoryDWrite;
        protected SolidColorBrush _brush;

        protected float _lineHeight;

        // Внешний вид контрола
        protected uint _textSize = 16;
        protected Vector4 _margin;
        protected bool _visible;
        public bool Visible { get { return _visible; } }
    
        
        public CommonComponent(RenderTarget RenderTarget2D, SharpDX.DirectWrite.Factory Factory_, SolidColorBrush Brush, string Texture, Vector2 position, Size2F size) :
            base(RenderTarget2D, Texture, position, size)
        {

            _factoryDWrite = Factory_;
            _textFormat = new TextFormat(_factoryDWrite, "Gabriola", _textSize);
            _brush = Brush;
            RenderTarget2D.TextAntialiasMode = TextAntialiasMode.Cleartype;

            // Высота строки для текста
            _lineHeight = _textFormat.FontSize + 9;

            _visible = false;
            _margin = Vector4.Zero;
        }

        public void Show()
        {
            _visible = true;
        }

        public void Hide()
        {
            _visible = false;
        }

        public void Toggle()
        {
            _visible = !_visible;
        }

        public virtual void KeyDown(KeyEventArgs e)
        {

        }

        // Получить глобальные координаты с учетом отступов и offset
        protected Vector2 getPosition(Vector2 offset)
        {
            return new Vector2(_position.X - _size.Width / 2 + _margin.W + offset.X, _position.Y - _size.Height / 2 + _margin.X + offset.Y);
        }

        // Получить возможную ширину с учетом отступов
        protected Size2F getSize()
        {
            return new Size2F(_size.Width - _margin.Y - _margin.W, _size.Height - _margin.X - _margin.Z);
        }

    }
}
