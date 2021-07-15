using System;
using System.Diagnostics;
using System.IO;
using Noesis;

namespace VeldridNGUI
{
    public abstract class VNGUIView : IDisposable
    {
        private Veldrid.InputSnapshot _prevInputSnapshot;
        private System.Numerics.Vector2 _prevMousePosition;
        private Stopwatch _stopwatch;

        public View View { get; protected set; }
        public bool IsLoggingEnabled { get; set; }

        public int Width { get; protected set; }
        public int Height { get; protected set; }

        public Veldrid.GraphicsDevice GraphicsDevice { get; protected set; }

        #region IDisposable
        protected bool _disposed = false;

        public virtual void Dispose()
        {
            if (_disposed)
                return;

            View.Renderer.Shutdown();
            _disposed = true;
        }
        #endregion

        protected abstract void InternalInit();
        protected abstract void InternalRender(Veldrid.Framebuffer framebuffer);
        protected abstract void InternalSetView();

        public static VNGUIView CreateInstance(Veldrid.GraphicsDevice graphicsDevice)
        {
            switch (graphicsDevice.BackendType)
            {
                case Veldrid.GraphicsBackend.Direct3D11:
                    return new VNGUIViewD3D11(graphicsDevice);

                case Veldrid.GraphicsBackend.OpenGL:
                    return new VNGUIViewOpenGL(graphicsDevice);
            };

            throw new Exception($"Unsupported backend type {graphicsDevice.BackendType}");
        }

        public void Init(int width, int height, bool loggingEnabled)
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

                Noesis.Error.SetUnhandledCallback((exception) =>
                {
                    // TODO : figure out what to do with this
                    throw exception;
                });
            }

            View = Noesis.GUI.CreateView(null);
            View.SetFlags(RenderFlags.PPAA | RenderFlags.LCD);
            SetSize(width, height);

            InternalInit();
            InternalSetView();

            _stopwatch = Stopwatch.StartNew();
        }

        public void CreateViewFromFile(string path)
        {
            var xaml = File.ReadAllText(path);
            CreateViewFromString(xaml);
        }

        public void CreateViewFromStream(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                var xaml = reader.ReadToEnd();
                CreateViewFromString(xaml);
            }
        }

        public void CreateViewFromString(string xaml)
        {
            var xamlElement = (FrameworkElement)Noesis.GUI.ParseXaml(xaml);

            View = Noesis.GUI.CreateView(xamlElement);
            View.SetFlags(RenderFlags.PPAA | RenderFlags.LCD);

            SetSize(Width, Height);
            InternalSetView();
        }

        public void SetSize(int width, int height)
        {
            Width = width;
            Height = height;
            View.SetSize(Width, Height);
        }

        public void Draw(Veldrid.Framebuffer framebuffer = null)
        {
            if (framebuffer == null)
                framebuffer = GraphicsDevice.SwapchainFramebuffer;

            View.Renderer.UpdateRenderTree();
            View.Renderer.RenderOffscreen();

            InternalRender(framebuffer);

            View.Renderer.Render();
        }

        public void Update()
        {
            View.Update(_stopwatch.Elapsed.TotalSeconds);
        }

        public void HandleInput(Veldrid.InputSnapshot snapshot)
        {
            if (_prevInputSnapshot == null)
            {
                _prevInputSnapshot = snapshot;
                return;
            }

            #region Mouse Input
            var mouseX = (int)snapshot.MousePosition.X;
            var mouseY = (int)snapshot.MousePosition.Y;

            foreach (var mouseEvent in snapshot.MouseEvents)
            {
                var button = VeldridMapping.GetNoesisMouseButton(mouseEvent.MouseButton);

                if (!button.HasValue)
                    continue;

                if (mouseEvent.Down)
                    View.MouseButtonDown(mouseX, mouseY, button.Value);
                else
                    View.MouseButtonUp(mouseX, mouseY, button.Value);
            }

            if (_prevMousePosition != snapshot.MousePosition)
                View.MouseMove(mouseX, mouseY);

            if (snapshot.WheelDelta != 0)
                View.MouseWheel(mouseX, mouseY, (int)snapshot.WheelDelta);
            #endregion

            #region Keyboard Input
            foreach (var keyEvent in snapshot.KeyEvents)
            {
                var key = VeldridMapping.GetNoesisKey(keyEvent.Key);

                if (!key.HasValue)
                    continue;

                if (keyEvent.Down)
                    View.KeyDown(key.Value);
                else
                    View.KeyUp(key.Value);
            }

            foreach (var c in snapshot.KeyCharPresses)
            {
                View.Char(c);
            }
            #endregion
            
            _prevInputSnapshot = snapshot;
            _prevMousePosition = snapshot.MousePosition;
        }
    }
}
