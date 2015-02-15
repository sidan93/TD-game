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
        protected Size2F _size;
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
                float x = value.X - _size.Width / 2;
                float y = value.Y - _size.Height / 2;
                _target.Location = new Vector2(x, y);
            }
        }
        public Vector2 Position
        {
            get
            {
                return _position;
            }
        }

        public CommonObject(RenderTarget RenderTarget2D) 
        {
            this.RenderTarget2D = RenderTarget2D;
        }

        public CommonObject(RenderTarget RenderTarget2D, String _bitmapPath, Size2F size, Vector2 position)
        {
            this.RenderTarget2D = RenderTarget2D;
            _bitmap = Helpers.LoadFromFile(RenderTarget2D, _bitmapPath);
            _size = size;
            _position = position;
            _target.Size = _size;
        }

        public virtual void Draw(DemoTime time)
        {
            if (RenderTarget2D != null)
              RenderTarget2D.DrawBitmap(_bitmap, _target, 1.0f, BitmapInterpolationMode.Linear);
        }

        public virtual void Update(DemoTime time)
        {
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
    }
}
