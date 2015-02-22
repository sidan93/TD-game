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
using System.Windows.Forms;

namespace TD.Interface
{
    class GameInterface : IOutObject
    {
        protected RenderTarget RenderTarget2D;
        protected SharpDX.DirectWrite.Factory _factoryDWrite;
        protected SolidColorBrush _brushChat;

        Chat _chat;
        InfoPanel _infoTower;

        public GameInterface(RenderTarget RenderTarget2D, Size2 resolution, SharpDX.DirectWrite.Factory FactoryDWrite)
        {
            this.RenderTarget2D = RenderTarget2D;
            _factoryDWrite = FactoryDWrite;
            _brushChat = new SolidColorBrush(RenderTarget2D, Color4.White);

            _chat = new Chat(RenderTarget2D, FactoryDWrite, _brushChat, new Vector2(200, resolution.Height - 100), new Size2F(400, 200));
            _infoTower = new InfoPanel(RenderTarget2D, FactoryDWrite, _brushChat, new Vector2(resolution.Width - 75, resolution.Height - 100), new Size2F(150, 200));
        }

        public void Draw(DemoTime time)
        {
            _chat.Draw(time);
            _infoTower.Draw(time);
        }

        public void Update(DemoTime time)
        {
            _chat.Update(time);
            _infoTower.Update(time);
        }

        public void KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Oemtilde)
            {
                _chat.Toggle();
                return;
            }

            // При нажатии на Ентер надо открыть чат, если он закрыт
            if (e.KeyCode == Keys.Enter && !_chat.Visible)
            {
                _chat.Show(true);
                return;
            }

            if (_chat.Visible)
            {
                _chat.KeyDown(e);
                return;
            }
        }
        
        public void InfoTower(CommonTower tower)
        {
            if (tower.isMouseOver)
                _infoTower.Show(tower);
            else _infoTower.Hide();
        }
    }
}
