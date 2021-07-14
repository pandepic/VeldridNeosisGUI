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
        public static CommandList CreateNGUICommandList(ResourceFactory factory, SharpDX.Direct3D11.DeviceContext context)
        {
            var veldridAssembly = Assembly.Load(new AssemblyName("Veldrid"));
            var d3d11CLType = veldridAssembly.GetType("Veldrid.D3D11.D3D11CommandList");

            var commandList = factory.CreateCommandList();

            var d3d11GDContextField = d3d11CLType.GetField("_context", BindingFlags.NonPublic | BindingFlags.Instance);
            var d3d11GDContext1Field = d3d11CLType.GetField("_context1", BindingFlags.NonPublic | BindingFlags.Instance);

            d3d11GDContextField.SetValue(commandList, context.Device.ImmediateContext);
            d3d11GDContext1Field.SetValue(commandList, context.Device.ImmediateContext.QueryInterfaceOrNull<SharpDX.Direct3D11.DeviceContext1>());

            return commandList;
        }

        public static SharpDX.Direct3D11.DeviceContext GetD3D11DeviceContext(CommandList cl)
        {
            var veldridAssembly = Assembly.Load(new AssemblyName("Veldrid"));
            var d3d11CLType = veldridAssembly.GetType("Veldrid.D3D11.D3D11CommandList");

            var d3d11GDContextField = d3d11CLType.GetField("_context", BindingFlags.NonPublic | BindingFlags.Instance);

            var d3d11Context = d3d11GDContextField.GetValue(cl);
            return (SharpDX.Direct3D11.DeviceContext)d3d11Context;
        }

        public static SharpDX.Direct3D11.DeviceContext GetD3D11DeviceContext(GraphicsDevice gd)
        {
            var dxDevice = new SharpDX.Direct3D11.Device(gd.GetD3D11Info().Device);
            return dxDevice.ImmediateContext;
        }

        public static IntPtr GetD3D11ContextPtr(GraphicsDevice gd)
        {
            var dxDevice = new SharpDX.Direct3D11.Device(gd.GetD3D11Info().Device);
            return dxDevice.ImmediateContext.NativePointer;
        }
    }
}
