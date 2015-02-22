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

using TD.Enums;
using TD.Bullet;

namespace TD.Common
{
    class CommonTower : CommonObject
    {
        protected float _range;
        public float Range
        {
            get
            {
                return _range;
            }
        }

        protected Bitmap _bitmapRange;
        protected bool _drawRange;
        public bool isMouseOver { get { return _drawRange; } }

        public delegate void MethodContainer(CommonTower tower);
        public event MethodContainer eventMouseOver;

        protected bool _isSet; // Отвечает установаленна башна или нет
        protected List<CommonBullet> _bullets;
        protected EBullet _bulletType;

        protected float _speedFire;       // Количество выстрелов в секунду
        // Отбрасываем все что после двух знаков
        public float speedFirePerMinutes { get { return _speedFire * 60; } }
        double _lastFire;

        protected ETowers _type;
        public ETowers Type { get { return _type; } }

        private static uint __commonId;
        private uint _id;
        public uint Id { get { return _id; } }

        public CommonTower(RenderTarget RenderTarget2D, Vector2 position) :
            base(RenderTarget2D)
        {
            _range = 10;
            _size = new Size2F(30, 30);
            _target.Size = _size;
            _position = position;
            _isSet = false;

            _bullets = new List<CommonBullet>();

            var rand = new Random();
            _speedFire = 3 + rand.Next(-10, 10) / 10.0f;
            _lastFire = 0;

            _bitmapRange = Helpers.LoadFromFile(RenderTarget2D, "TowerRange01.png"); ;
            _drawRange = false;

            _bulletType = EBullet.CommonBullet;

            _id = ++__commonId;
        }

        public override void Update(DemoTime time)
        {
            base.Update(time);

            _bullets.ForEach(delegate(CommonBullet Bullet)
            {
                Bullet.Update(time);
            });
        }

        public override void Draw(DemoTime time)
        {
            base.Draw(time);

            if (_drawRange && _bitmapRange != null)
                RenderTarget2D.DrawBitmap(_bitmapRange, new RectangleF(_position.X - _range / 2, Position.Y - _range / 2, _range, _range), 1.0f, BitmapInterpolationMode.Linear);

            _bullets.ForEach(delegate(CommonBullet Bullet)
            {
                Bullet.Draw(time);
            });
        }

        // Установить башню
        public virtual bool setUp(Vector2 position)
        {
            if (_isSet)
            {
                _isSet = true;
                _position = position;
                return true;
            }
            return false;
        }

        public virtual void Fire(DemoTime time, Vector2 position)
        {
            // TODO _range / 2 - magic!?
            if (Math.Abs(time.ElapseTime - _lastFire) > 1.0f / _speedFire && Helpers.InRange(_position, position, _range / 2))
            {
                CommonBullet bullet = null;
                if (_bulletType == EBullet.FastBullet)
                    bullet = new FastBullet(RenderTarget2D, _position, time);
                else
                    bullet = new CommonBullet(RenderTarget2D, _position, time);
                if (bullet != null)
                {
                    bullet.MoveTo(position);
                    _bullets.Add(bullet);
                }
                _lastFire = time.ElapseTime;
            }
        }

        public void MouseMove(int X, int Y)
        {
            var oldState = _drawRange;
            
            if ((_position.X - X) * (_position.X - X) + (_position.Y - Y) * (_position.Y - Y) < _size.Width * _size.Height)
                _drawRange = true;
            else _drawRange = false;

            if (oldState != _drawRange)
                eventMouseOver(this);
        }
    }
}
