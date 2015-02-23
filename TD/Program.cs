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

using Character = TD.Player.Player;
using TD.Factory;
using TD.Core;
using TD.Interface;
using MainMenu = TD.Interface.MainMenu;
using SharpDX.XAudio2;

namespace TD
{
    /// <summary>
    /// This sample demonstrates how to load a Direct2D1 bitmap from a file.
    /// This method will be part of a future version of SharpDX API.
    /// </summary>
    public class Program : Direct2D
    {
        Bitmap _infoGamePanel;

        Character _myCharacter;
        
        BuildingsFactory _buildingsFactory;
        PlayersFactory _playersFactory;

        double _timeLastDraw;       // Время последней отрисовки
        double _timeLastUpdate;     // Время последнего Update

        static Size2 RESOLUTION = new Size2(900, 600);

        GameState gameState;
        MainMenu mainMenu;
        GameInterface GameInterface;
        
        protected override void Initialize(DemoConfiguration demoConfiguration)
        {
            base.Initialize(demoConfiguration);
            _infoGamePanel = Helpers.LoadFromFile(RenderTarget2D, "gameInfo.png");

            GameInterface = new GameInterface(RenderTarget2D, RESOLUTION, FactoryDWrite);

            _buildingsFactory = new BuildingsFactory(RenderTarget2D, GameInterface);
            _myCharacter = new Character(RenderTarget2D, _buildingsFactory, "Серафим");
            _playersFactory = new PlayersFactory(RenderTarget2D, GameInterface, _myCharacter);

           

            _timeLastDraw = 0;
            _timeLastUpdate = 0;

            gameState = new GameState();
            mainMenu = new MainMenu(RenderTarget2D, RESOLUTION);
            //SoundsManager.init();
            
        }   

        protected override void Draw(DemoTime time)
        {
            base.Draw(time);

            if (gameState.State == EGameState.MainMenu)
            {
                mainMenu.Draw(time);
                return;
            }

            if (gameState.State == EGameState.Game)
            {

                base.Draw(time);
                RenderTarget2D.Clear(Color4.Black);

                if (time.ElapseTime < 2)
                    RenderTarget2D.DrawBitmap(_infoGamePanel,
                        new SharpDX.RectangleF(RESOLUTION.Width / 2 - _infoGamePanel.Size.Width / 2, RESOLUTION.Height / 2 - _infoGamePanel.Size.Height / 2, _infoGamePanel.Size.Width, _infoGamePanel.Size.Height),
                        1.0f, BitmapInterpolationMode.Linear);
                
                _buildingsFactory.Draw(time);
                _myCharacter.Draw(time);

                GameInterface.Draw(time);
                _timeLastDraw = time.ElapseTime;

                return;
            }
            
        }
        
        protected override void Update(DemoTime time)
        {
            base.Update(time);

            if (gameState.State == EGameState.MainMenu)
            {
                mainMenu.Update(time);
                return;
            }

            if (gameState.State == EGameState.Game)
            {
                _buildingsFactory.Update(time, _myCharacter.Position);
                _myCharacter.Update(time);
                _timeLastUpdate = time.ElapseTime;

                GameInterface.Update(time);

                return;
            }
        }

        protected override void MouseClick(MouseEventArgs e)
        {
            base.MouseClick(e);
            if (e.Button == MouseButtons.Right)
                _myCharacter.MoveTo(new Vector2(e.X, e.Y));
            if (e.Button == MouseButtons.Left)
                _myCharacter.SetTower(new Vector2(e.X, e.Y));
        }

        protected override void MouseMove(MouseEventArgs e)
        {
            base.MouseMove(e);
            _buildingsFactory.MouseMove(e.X, e.Y);
            _myCharacter.MouseMove(e.X, e.Y);
            
        }

        protected override void KeyDown(KeyEventArgs e)
        {
            base.KeyDown(e);
            GameInterface.KeyDown(e);
        }

        static void Main(string[] args)
        {
            Program program = new Program();
            program.Run(new DemoConfiguration("SharpDX Bitmap Demo", RESOLUTION.Width, RESOLUTION.Height));
        }
    }

}
