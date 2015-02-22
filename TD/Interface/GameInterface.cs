using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SharpDX.Direct2D1;
using SharpDX.Samples;

using TD.Common;
using TD.Interface.Components;
using SharpDX;
using SharpDX.DirectWrite;

namespace TD.Interface
{
    class GameInterface : IOutObject
    {
        protected RenderTarget RenderTarget2D;
        protected SharpDX.DirectWrite.Factory _factoryDWrite;
        protected SolidColorBrush _brushChat;

        Chat _chat;

        public GameInterface(RenderTarget RenderTarget2D, Size2 resolution, SharpDX.DirectWrite.Factory FactoryDWrite)
        {
            this.RenderTarget2D = RenderTarget2D;
            _factoryDWrite = FactoryDWrite;
            _brushChat = new SolidColorBrush(RenderTarget2D, Color4.White);

            _chat = new Chat(RenderTarget2D, FactoryDWrite, _brushChat, new Vector2(200, resolution.Height - 100), new Size2F(400, 200));
        }

        public void Draw(DemoTime time)
        {
            _chat.Draw(time);
        }

        public void Update(DemoTime time)
        {
            _chat.Update(time);
            _chat.AddMessage("Andrey", time.ElapseTime.ToString() + " Тут должно перенестись на новую строку и не налезть  ");
        }
    }
}
