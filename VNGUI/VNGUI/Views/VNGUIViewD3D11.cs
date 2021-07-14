using System;
using System.Reflection;

namespace VeldridNGUI
{
    public class VNGUIViewD3D11 : VNGUIView, IDisposable
    {
        private readonly Assembly _veldridAssembly;
        private readonly Type _d3d11CommandListType;
        private readonly MethodInfo _flushViewportsMethod;

        private Vortice.Direct3D11.ID3D11Device _d3d11Device;
        private Vortice.Direct3D11.ID3D11DeviceContext _d3d11DeviceContext;
        private Noesis.RenderDeviceD3D11 _renderDevice;
        private Veldrid.CommandList _commandList;

        public override void Dispose()
        {
            if (_disposed)
                return;

            base.Dispose();

            _commandList?.Dispose();
        }

        ~VNGUIViewD3D11()
        {
            Dispose();
        }

        internal VNGUIViewD3D11(Veldrid.GraphicsDevice graphicsDevice)
        {
            GraphicsDevice = graphicsDevice;

            _veldridAssembly = Assembly.Load(new AssemblyName("Veldrid"));
            _d3d11CommandListType = _veldridAssembly.GetType("Veldrid.D3D11.D3D11CommandList");
            _flushViewportsMethod = _d3d11CommandListType.GetMethod("FlushViewports", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        protected override void InternalInit()
        {
            _d3d11Device = new Vortice.Direct3D11.ID3D11Device(GraphicsDevice.GetD3D11Info().Device);
            _d3d11DeviceContext = _d3d11Device.ImmediateContext;

            _renderDevice = new Noesis.RenderDeviceD3D11(_d3d11DeviceContext.NativePointer);
            View.Renderer.Init(_renderDevice);

            CreateCommandList();
        }

        protected override void InternalRender()
        {
            _commandList.Begin();
            _commandList.SetFramebuffer(GraphicsDevice.SwapchainFramebuffer);
            _commandList.SetFullViewports();
            _flushViewportsMethod.Invoke(_commandList, null);
        }

        private void CreateCommandList()
        {
            _commandList = GraphicsDevice.ResourceFactory.CreateCommandList();

            var d3d11GDContextField = _d3d11CommandListType.GetField("_context", BindingFlags.NonPublic | BindingFlags.Instance);
            var d3d11GDContext1Field = _d3d11CommandListType.GetField("_context1", BindingFlags.NonPublic | BindingFlags.Instance);

            d3d11GDContextField.SetValue(_commandList, _d3d11DeviceContext.Device.ImmediateContext);
            d3d11GDContext1Field.SetValue(_commandList, _d3d11DeviceContext.Device.ImmediateContext.QueryInterfaceOrNull<Vortice.Direct3D11.ID3D11DeviceContext1>());
        }
    }
}
