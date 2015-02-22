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
        Bitmap _bitmap;
        Character _player;
        BuildingsFactory _buildingsFactory;

        double _timeLastDraw;       // Время последней отрисовки
        double _timeLastUpdate;     // Время последнего Update
        double _rateSweep;          // Частота перерисовки экрана за 1 сек
        double _rateUpdate;         // Частота пересчета данных за 1 сек

        static Size2 RESOLUTION = new Size2(900, 600);

        GameState gameState;
        MainMenu mainMenu;
        GameInterface GameInterface;
        
        protected override void Initialize(DemoConfiguration demoConfiguration)
        {
            base.Initialize(demoConfiguration);
            _bitmap = Helpers.LoadFromFile(RenderTarget2D, "001.jpg");

            GameInterface = new GameInterface(RenderTarget2D, RESOLUTION, FactoryDWrite);

            _buildingsFactory = new BuildingsFactory(RenderTarget2D, GameInterface);
            _player = new Character(RenderTarget2D, _buildingsFactory);

            _timeLastDraw = 0;
            _timeLastUpdate = 0;
            _rateSweep = 60;
            _rateUpdate = 100;

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
                //RenderTarget2D.DrawBitmap(_bitmap, 1.0f, BitmapInterpolationMode.Linear);
                RenderTarget2D.Clear(Color4.Black);

                _buildingsFactory.Draw(time);
                _player.Draw(time);

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
                _buildingsFactory.Update(time, _player.Position);
                _player.Update(time);
                _timeLastUpdate = time.ElapseTime;

                GameInterface.Update(time);

                return;
            }
        }

        protected override void MouseClick(MouseEventArgs e)
        {
            base.MouseClick(e);
            if (e.Button == MouseButtons.Right)
                _player.MoveTo(new Vector2(e.X, e.Y));
            if (e.Button == MouseButtons.Left)
                _player.SetTower(new Vector2(e.X, e.Y));
        }

        protected override void MouseMove(MouseEventArgs e)
        {
            base.MouseMove(e);
            _buildingsFactory.MouseMove(e.X, e.Y);
            
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
