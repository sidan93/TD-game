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

namespace TD.Interface.Components
{
    class Chat : CommonObject
    {
        protected List<Tuple<string, string>> _messages;

        protected TextFormat _textFormat;
        protected SharpDX.DirectWrite.Factory _factoryDWrite;
        protected SolidColorBrush _brush;

        protected uint _textSize = 16;
        protected Vector4 _margin;
     
        public Chat(RenderTarget RenderTarget2D, SharpDX.DirectWrite.Factory Factory_, SolidColorBrush Brush, Vector2 position, Size2F size) :
            base(RenderTarget2D, "chat01.png", position, size)
        {
            _messages = new List<Tuple<string, string>>();

            _factoryDWrite = Factory_;
            _textFormat = new TextFormat(_factoryDWrite, "Gabriola", _textSize) 
                            { 
                               
                            };

            RenderTarget2D.TextAntialiasMode = TextAntialiasMode.Cleartype;

            _brush = Brush;
            _margin = new Vector4(-5, 0, 10, 5);
        }

        public override void Draw(DemoTime time)
        {
            base.Draw(time);

            float lineHeight = _textFormat.FontSize + 9;
            float width = _size.Width - _margin.Y - _margin.W;
            for (int i = _messages.Count - 1, step = 0; i >= 0; i--)
            {
                var message = _messages[i].Item1 + ": " + _messages[i].Item2;
                var kof = 5.642857142857139; // Коэфциент для 16 кегля
                var lineCount = (int)((message.Count() *  kof) / width + 1);
                if ((step + lineCount) * lineHeight + _margin.X + _margin.Z > _size.Height)
                    break;
                var outRectangle = new RectangleF(_target.Location.X + _margin.W,
                                                  _target.Location.Y + _size.Height - lineHeight * (step + lineCount) - _margin.Z, 
                                                  width,
                                                  lineHeight);
                
                RenderTarget2D.DrawText(message, _textFormat, outRectangle, _brush);
                
                step += lineCount;
            }

        }

        public void AddMessage(string playerName, string messageText)
        {
            _messages.Add(new Tuple<string, string>(playerName, messageText));
        }
    }
}
