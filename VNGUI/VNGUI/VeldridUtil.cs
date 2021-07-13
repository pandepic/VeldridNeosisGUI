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
            var veldridAssembly = Assembly.Load(new AssemblyName("Veldrid"));
            var d3d11GDType = veldridAssembly.GetType("Veldrid.D3D11.D3D11GraphicsDevice");

            var d3d11GDContextField = d3d11GDType.GetFields(
                         BindingFlags.NonPublic |
                         BindingFlags.Instance).Where(f => f.Name == "_immediateContext").First();

            var d3d11Context = d3d11GDContextField.GetValue(gd);
            var d3d11ContextPtr = ((dynamic)d3d11Context).NativePointer;

            return d3d11ContextPtr;
        }
    }
}
