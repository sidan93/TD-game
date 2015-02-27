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
    public class CommonObject : IOutObject
    {
        protected Bitmap _bitmap;
        protected RenderTarget RenderTarget2D;

        protected RectangleF _target;

        private Size2F __size;
        protected Size2F _size
        {
            get { return __size; }
            set
            {
                __size = value;
                _target.Location = new Vector2(-_size.Width / 2, -_size.Height / 2);
                _target.Size = __size;
            }
        }
        
        private Vector2 __position;
        protected Vector2 _position
        {
            get
            {
                return __position;
            }
            set
            {
                __position = value;
                recalcMatrix();
            }
        }
        public Vector2 Position { get { return _position; } }

        protected float _rotation;
        public float Rotation
        {
            get { return _rotation; }
            protected set 
            {
                _rotation = value;
                recalcMatrix();
            }
        }
        private Matrix _resultMatrix;

        protected bool _enabled;

        public CommonObject(RenderTarget RenderTarget2D) 
        {
            this.RenderTarget2D = RenderTarget2D;
            _enabled = true;

            _resultMatrix = Matrix.Identity;
        }

        public CommonObject(RenderTarget RenderTarget2D, String _bitmapPath, Vector2 position, Size2F size)
            : this(RenderTarget2D)
        {
            _bitmap = Helpers.LoadFromFile(RenderTarget2D, _bitmapPath);
            

            _size = size;
            _position = position;
        }

        public virtual void Draw(DemoTime time)
        {
            if (!_enabled)
                return; 

            if (RenderTarget2D != null)
            {
                RenderTarget2D.Transform = _resultMatrix;
                RenderTarget2D.DrawBitmap(_bitmap, _target, 1.0f, BitmapInterpolationMode.Linear);
                RenderTarget2D.Transform = Matrix.Identity;
            }
        }

        public virtual void Update(DemoTime time)
        {
            if (!_enabled)
                return; 
        }

        protected bool LoadFromFile(String _bitmapPath)
        {
            if (_bitmap == null)
            {
                _bitmap = Helpers.LoadFromFile(RenderTarget2D, _bitmapPath);
                return true;
            }
            return false;
        }
        protected virtual void Destroy()
        {
            RenderTarget2D = null;
        }

        protected virtual void recalcMatrix()
        {
            _resultMatrix = Matrix.Multiply(Matrix.RotationZ(Rotation), Matrix.Translation(_position.X, _position.Y, 0));
        }
    }
}
