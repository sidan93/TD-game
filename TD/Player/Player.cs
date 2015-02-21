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
    class Player : CommonDinamicObject
    {

        BuildingsFactory _BuildingsFactory;

        Queue<PlayerActions> _actions;

        public Player(RenderTarget RenderTarget2D, BuildingsFactory _BuildingsFactory) :
            base(RenderTarget2D, "002.png", new Size2F(20,20), new Vector2(0,0))
        {
            this._BuildingsFactory = _BuildingsFactory;
            _actions = new Queue<PlayerActions>();
            _actions.Enqueue(new PlayerActions());
        }

        public override void Draw(DemoTime time)
        {
            base.Draw(time);
        }

        public override void MoveTo(Vector2 position)
        {
           _actions.Clear();
            
            PlayerActions actionMove = new PlayerActions(EPlayerActions.Move);
            actionMove.position = position;
            _actions.Enqueue(actionMove);

            base.MoveTo(position);
        }

        public override void Update(DemoTime time)
        {
            if (_actions.Count > 0)
            {
                if (_actions.Peek().playerActions == EPlayerActions.None)
                    _actions.Dequeue();
                else if (_actions.Peek().playerActions == EPlayerActions.Move)
                    if (_isArrive)
                    {
                        _actions.Dequeue();
                    }
                    else
                    {
                        _position += _direction * _speed;
                    }
                else if (_actions.Peek().playerActions == EPlayerActions.BuildTower)
                {
                    _BuildingsFactory.SetTower(_actions.Peek().tower, _actions.Peek().position);
                    _actions.Dequeue();
                }
            }
        }


        public void SetTower(Vector2 position)
        {
            MoveTo(position);

            PlayerActions actionBuildTower = new PlayerActions(EPlayerActions.BuildTower);
            actionBuildTower.position = position;
            actionBuildTower.tower = ETowers.SingleTower;

            if (new Random().Next(100) > 50)
                actionBuildTower.tower = ETowers.CrazyTower;

            _actions.Enqueue(actionBuildTower);
        }
    }


    
}
