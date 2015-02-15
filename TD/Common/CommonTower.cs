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
        protected bool _isSet; // Отвечает установаленна башна или нет
        protected List<CommonBullet> _bullets;

        float _speedFire;       // Количество выстрелов в секунду
        double _lastFire;

        public CommonTower(RenderTarget RenderTarget2D, Vector2 position) :
            base(RenderTarget2D)
        {
            _range = 200;
            _size = new Size2F(30, 30);
            _target.Size = _size;
            _position = position;
            _isSet = false;

            _bullets = new List<CommonBullet>();

            var rand = new Random();
            _speedFire = 3 + rand.Next(-10, 10) / 10.0f;
            _lastFire = 0;
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
            if (Math.Abs(time.ElapseTime - _lastFire) > 1.0f / _speedFire)
            {
                CommonBullet bullet = new CommonBullet(RenderTarget2D, _position);
                bullet.MoveTo(position);
                _bullets.Add(bullet);
                _lastFire = time.ElapseTime;
            }
        }
    }

    enum ETowers
    {
        SingleTower
    }

}
