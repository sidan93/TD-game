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
    interface IOutObject
    {
        void Draw(DemoTime time);
        void Update(DemoTime time);
    }
}
