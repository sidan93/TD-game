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
using TD;

namespace TD.Player
{
    class Player : CommonDinamicObject
    {
        BuildingsFactory _BuildingsFactory;

        Queue<PlayerActions> _actions;

        // Наведена ли мышь на героя
        protected bool _isMouseOver;
        public bool isMouseOver { get { return _isMouseOver; } }

        protected string _name;
        public string Name { get { return _name; } }

        // Событе наведения или ухода мыши
        public delegate void MethodMouseOver(Player player);
        public event MethodMouseOver eventMouseOver;

        // Событие установки башни
        public delegate void MethodCreateTower(CommonTower tower);
        public event MethodCreateTower eventCreateTower;

        public Player(RenderTarget RenderTarget2D, BuildingsFactory _BuildingsFactory, string name) :
            base(RenderTarget2D, "002.png", new Vector2(0, 0), new Size2F(20, 20))
        {
            this._BuildingsFactory = _BuildingsFactory;
            _actions = new Queue<PlayerActions>();
            _actions.Enqueue(new PlayerActions());
            _name = name;
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
                    var tower = _BuildingsFactory.SetTower(_actions.Peek().tower, _actions.Peek().position);
                    eventCreateTower(tower);
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

        public void MouseMove(int X, int Y)
        {
            bool lastState = _isMouseOver;

            if (Helpers.InRange(_position, new Vector2(X, Y), _size.Height))
                _isMouseOver = true;
            else _isMouseOver = false;

            if (lastState != _isMouseOver)
                eventMouseOver(this);

        }
    }


    
}
