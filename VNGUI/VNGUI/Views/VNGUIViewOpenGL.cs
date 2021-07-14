using System;
using System.Collections.Generic;
using System.Text;

namespace VeldridNGUI
{
    public class VNGUIViewOpenGL : VNGUIView, IDisposable
    {
        private Noesis.RenderDeviceGL _renderDevice;
        
        public override void Dispose()
        {
            if (_disposed)
                return;

            base.Dispose();
        }

        protected override void InternalInit()
        {
            _renderDevice = new Noesis.RenderDeviceGL();
            View.Renderer.Init(_renderDevice);
        }

        protected override void InternalRender()
        {
        }
    }
}
