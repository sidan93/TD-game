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
using RectangleF = SharpDX.RectangleF;
using Point = SharpDX.Point;
using Character = TD.Player.Player;

using TD.Enums;
using TD.Interface;
using TD.Common;

namespace TD.Factory
{
    class PlayersFactory : CommonFactory
    {
        Character _mainPlayer;
        List<Character> _players;

        public PlayersFactory(RenderTarget RenderTarget2D, GameInterface GameInterface, Character mainPlayer) :
            base(RenderTarget2D, GameInterface)
        {
            _mainPlayer = mainPlayer;
            _players = new List<Character>();

            // Подпишем игрока на наведение мыши
            _mainPlayer.eventMouseOver += _playerEventMouseOver;
        }

        // Событие наведение мыши на игрока
        private void _playerEventMouseOver(Character player)
        {
            GameInterface.InfoHero(player);
        }
    }
}
