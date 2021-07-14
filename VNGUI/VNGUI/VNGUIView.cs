using System;
using System.Reflection;
using Noesis;
using NoesisApp;

namespace VeldridNGUI
{
    public abstract class VNGUIView : IDisposable
    {
        public View View { get; protected set; }
        public bool IsLoggingEnabled { get; set; }

        public int Width { get; protected set; }
        public int Height { get; protected set; }

        public Veldrid.GraphicsDevice GraphicsDevice { get; protected set; }

        #region IDisposable
        private bool _disposed = false;

        public virtual void Dispose()
        {
            if (_disposed)
                return;

            View.Renderer.Shutdown();
            _disposed = true;
        }
        #endregion

        protected abstract void InternalInit();
        protected abstract void InternalRender();

        public static VNGUIView CreateInstance(Veldrid.GraphicsBackend backendType)
        {
            switch (backendType)
            {
                case Veldrid.GraphicsBackend.Direct3D11:
                    return new VNGUIViewD3D11();
            };

            throw new Exception($"Unsupported backend type {backendType}");
        }

        public void Init(Veldrid.GraphicsDevice graphicsDevice, int width, int height, bool loggingEnabled)
        {
            IsLoggingEnabled = loggingEnabled;

            if (IsLoggingEnabled)
            {
                Noesis.Log.SetLogCallback((level, channel, message) =>
                {
                    if (channel == "")
                    {
                    // [TRACE] [DEBUG] [INFO] [WARNING] [ERROR]
                    string[] prefixes = new string[] { "T", "D", "I", "W", "E" };
                        string prefix = (int)level < prefixes.Length ? prefixes[(int)level] : " ";
                        Console.WriteLine("[NOESIS/" + prefix + "] " + message);
                    }
                });
            }

            CreateView();
            SetSize(width, height);

            Noesis.GUI.LoadApplicationResources("Theme/NoesisTheme.DarkBlue.xaml");

            GraphicsDevice = graphicsDevice;

            InternalInit();
        }

        private void CreateView()
        {
            Noesis.Grid xaml = (Noesis.Grid)Noesis.GUI.ParseXaml(@"
                    <Grid xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"">
                        <Grid.Background>
                            <LinearGradientBrush StartPoint=""0,0"" EndPoint=""0,1"">
                                <GradientStop Offset=""0"" Color=""#FF123F61""/>
                                <GradientStop Offset=""0.6"" Color=""#FF0E4B79""/>
                                <GradientStop Offset=""0.7"" Color=""#FF106097""/>
                            </LinearGradientBrush>
                        </Grid.Background>
                        <Viewbox>
                            <StackPanel Margin=""50"">
                                <Button Content=""Hello World!"" Margin=""0,30,0,0""/>
                                <Rectangle Height=""5"" Margin=""-10,20,-10,0"">
                                    <Rectangle.Fill>
                                        <RadialGradientBrush>
                                            <GradientStop Offset=""0"" Color=""#40000000""/>
                                            <GradientStop Offset=""1"" Color=""#00000000""/>
                                        </RadialGradientBrush>
                                    </Rectangle.Fill>
                                </Rectangle>
                            </StackPanel>
                        </Viewbox>
                    </Grid>");

            View = Noesis.GUI.CreateView(xaml);
            View.SetFlags(RenderFlags.PPAA | RenderFlags.LCD);
        }

        public void SetSize(int width, int height)
        {
            Width = width;
            Height = height;
            View.SetSize(Width, Height);
        }

        public void Draw()
        {
            View.Renderer.UpdateRenderTree();
            View.Renderer.RenderOffscreen();

            InternalRender();

            View.Renderer.Render();
        }

        public void Update(double totalSeconds)
        {
            View.Update(totalSeconds);
        }
    }

    public class VNGUIViewD3D11 : VNGUIView, IDisposable
    {
        private readonly Assembly _veldridAssembly;
        private readonly Type _d3d11CommandListType;
        private readonly MethodInfo _flushViewportsMethod;

        private SharpDX.Direct3D11.Device _d3d11Device;
        private SharpDX.Direct3D11.DeviceContext _d3d11DeviceContext;
        private Noesis.RenderDeviceD3D11 _renderDevice;
        private Veldrid.CommandList _commandList;

        public override void Dispose()
        {
            base.Dispose();

            _commandList?.Dispose();
        }

        ~VNGUIViewD3D11()
        {
            Dispose();
        }

        internal VNGUIViewD3D11()
        {
            _veldridAssembly = Assembly.Load(new AssemblyName("Veldrid"));
            _d3d11CommandListType = _veldridAssembly.GetType("Veldrid.D3D11.D3D11CommandList");
            _flushViewportsMethod = _d3d11CommandListType.GetMethod("FlushViewports", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        protected override void InternalInit()
        {
            _d3d11Device = new SharpDX.Direct3D11.Device(GraphicsDevice.GetD3D11Info().Device);
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
            d3d11GDContext1Field.SetValue(_commandList, _d3d11DeviceContext.Device.ImmediateContext.QueryInterfaceOrNull<SharpDX.Direct3D11.DeviceContext1>());
        }
    }
}
