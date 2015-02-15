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


namespace TD.Common
{
    public class CommonDinamicObject : CommonObject
    {

        protected Vector2 _direction;
        protected Vector2 _endPosition;
        protected float _speed;

        public CommonDinamicObject(RenderTarget RenderTarget2D) :
            base(RenderTarget2D)
        {
        }

        public CommonDinamicObject(RenderTarget RenderTarget2D, String _bitmapPath, Size2F size, Vector2 position) :
            base(RenderTarget2D, _bitmapPath, size, position)
        {
            _speed = 3;
        }

        // Получить расстояние от текущего положения до точки следования
        protected float _getLenghtToEnd()
        {
            return (_position - _endPosition).Length();
        }

        /// <summary>
        /// Проверяет достиг ли объект своей конечной точки
        /// </summary>
        protected bool _isArrive
        {
            get
            {
                return _getLenghtToEnd() < 3;
            }
        }

        /// <summary>
        /// Поставить для ебъекта точку, куда надо перемещаться
        /// </summary>
        public virtual void MoveTo(Vector2 position)
        {
            _endPosition = position;
            _direction = position - _position;
            _direction.Normalize();
        }

        public override void Update(DemoTime time)
        {
            base.Update(time);

            if (!_isArrive)
                _position += _direction * _speed;

        }

        protected override void Destroy()
        {
            base.Destroy();
            _endPosition = _position;
            _speed = 0;
            _direction = Vector2.Zero;
        }
    }
}
