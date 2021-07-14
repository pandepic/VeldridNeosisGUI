using Noesis;
using NoesisApp;

namespace VeldridNGUI
{
    public static class VNGUI
    {
        public static void Init(string licenseName, string licenseKey)
        {
            GUI.Init(licenseName, licenseKey);
            SetProviders();
            GUI.LoadApplicationResources("Theme/NoesisTheme.DarkBlue.xaml");
        }

        private static void SetProviders()
        {
            //GUI.SetXamlProvider();
            //GUI.SetFontProvider();
            //GUI.SetTextureProvider();
            
            Application.SetThemeProviders();// (new LocalXamlProvider(), new LocalFontProvider(), new LocalTextureProvider());
        }

        private static void SetFonts()
        {
            string[] fonts = { "Fonts/#PT Root UI", "Arial", "Segoe UI Emoji" };
            GUI.SetFontFallbacks(fonts);
            GUI.SetFontDefaultProperties(15.0f, FontWeight.Normal, FontStretch.Normal, FontStyle.Normal);
        }
    }
}
