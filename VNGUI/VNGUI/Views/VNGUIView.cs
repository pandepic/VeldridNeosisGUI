﻿using System;
using Noesis;

namespace VeldridNGUI
{
    public abstract class VNGUIView : IDisposable
    {
        private Veldrid.InputSnapshot _prevInputSnapshot;

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
        protected abstract void InternalRender();

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

            CreateView();
            SetSize(width, height);

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

        public void HandleInput(Veldrid.InputSnapshot snapshot)
        {
            if (_prevInputSnapshot == null)
            {
                _prevInputSnapshot = snapshot;
                return;
            }

            var mouseX = (int)snapshot.MousePosition.X;
            var mouseY = (int)snapshot.MousePosition.Y;
            var prevMouseX = (int)_prevInputSnapshot.MousePosition.X;
            var prevMouseY = (int)_prevInputSnapshot.MousePosition.Y;

            foreach (var mouseEvent in snapshot.MouseEvents)
            {
                if (mouseEvent.Down)
                {
                    var button = GetNoesisMouseButton(mouseEvent.MouseButton);

                    if (button.HasValue)
                        View.MouseButtonDown(mouseX, mouseY, button.Value);
                }
                else
                {
                    var button = GetNoesisMouseButton(mouseEvent.MouseButton);

                    if (button.HasValue)
                        View.MouseButtonUp(mouseX, mouseY, button.Value);
                }
            }

            if (prevMouseX != mouseX || prevMouseY != mouseY)
                View.MouseMove(mouseX, mouseY);

            if (snapshot.WheelDelta != 0)
                View.MouseWheel(mouseX, mouseY, (int)snapshot.WheelDelta);

            _prevInputSnapshot = snapshot;
        }

        public Noesis.MouseButton? GetNoesisMouseButton(Veldrid.MouseButton button)
        {
            switch (button)
            {
                case Veldrid.MouseButton.Left:
                    return MouseButton.Left;

                case Veldrid.MouseButton.Right:
                    return MouseButton.Right;

                case Veldrid.MouseButton.Middle:
                    return MouseButton.Middle;

                case Veldrid.MouseButton.Button1:
                    return MouseButton.XButton1;

                case Veldrid.MouseButton.Button2:
                    return MouseButton.XButton2;
            }

            return null;
        }
    }
}
