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

using TD.Common;
using TD.Factory;
using TD.Enums;


namespace TD.Player
{
    // Класс для работы с действиями игрока
    class PlayerActions
    {
        EPlayerActions _playerActions;
        public EPlayerActions playerActions
        {
            get
            {
                return _playerActions;
            }
        }

        // Параметры
        public Vector2 position;
        public ETowers tower;

        public PlayerActions()
            : this(EPlayerActions.None)
        {
        }
        public PlayerActions(EPlayerActions action)
        {
            _playerActions = action;
        }
    }
}
