using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Veldrid;

namespace VNGUI
{
    public static class VeldridUtil
    {
        public static IntPtr GetD3D11ContextPtr(GraphicsDevice gd)
        {
            var dxDevice = new SharpDX.Direct3D11.Device(gd.GetD3D11Info().Device);
            return dxDevice.ImmediateContext.NativePointer;
        }
    }
}
