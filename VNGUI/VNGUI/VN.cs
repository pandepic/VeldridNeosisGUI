﻿using System;
using Noesis;
using NoesisApp;

namespace VNGUI
{
    public class VN
    {
        public static bool IsLoggingEnabled { set; get; } = true;
        private static View MainView;

        public static void Init(string licenceName, string licenceKey)
        {
            Log("Initializing NoesisGUI");

            GUI.Init(licenceName, licenceKey);

            SetProviders();
            SetFonts();

            LoadView();

            GUI.LoadApplicationResources("Theme/NoesisTheme.DarkBlue.xaml");

        }

        public static void SetRenderDevice(RenderDevice device)
        {
            if (MainView == null)
            {
                Log("MainView can't be null. Make sure to call VN.Init() first");

                throw new NullReferenceException();
            }

            MainView.Renderer.Init(device);
        }

        public static void Dispose()
        {
            GUI.Shutdown();
        }

        private static void SetProviders()
        {
            //GUI.SetXamlProvider();
            //GUI.SetFontProvider();
            //GUI.SetTextureProvider();

            Application.SetThemeProviders(new LocalXamlProvider(), new LocalFontProvider(), new LocalTextureProvider());
        }

        private static void SetFonts()
        {
            string[] fonts = { "Fonts/#PT Root UI", "Arial", "Segoe UI Emoji" };
            GUI.SetFontFallbacks(fonts);
            GUI.SetFontDefaultProperties(15.0f, FontWeight.Normal, FontStretch.Normal, FontStyle.Normal);
        }

        private static void LoadView()
        {
            FrameworkElement xaml = (FrameworkElement)GUI.LoadXaml("Reflections.xaml");
            MainView = GUI.CreateView(xaml);

            MainView.SetFlags(RenderFlags.PPAA | RenderFlags.LCD);
            MainView.SetSize(1024, 768);
        }
        private static void Log(string format, params object[] args)
        {
            if (IsLoggingEnabled)
            {
                Console.WriteLine(format, args);
            }
        }

        #region Events
        public static void OnWindowResized(int width, int height)
        {
            MainView.SetSize(width, height);
        }
        #endregion
    }
}
