using System;
using Noesis;
using NoesisApp;

namespace VNGUI
{
    public class VN
    {
        public static bool IsLoggingEnabled { set; get; } = true;
        public static View MainView { get; private set; }

        public static void Init(string licenceName, string licenceKey)
        {
            Log("Initializing NoesisGUI");

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


            GUI.Init(licenceName, licenceKey);

            SetProviders();
            //SetFonts();

            LoadView();

            GUI.LoadApplicationResources("Theme/NoesisTheme.DarkBlue.xaml");

        }

        public static void Dispose()
        {
            MainView.Renderer.Shutdown();
            //MainView.Reset();
            GUI.Shutdown();
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

        public static void Update(double dt)
        {
            // TODO: check if everything was initialized?

            MainView.Update(dt);
        }

        public static void Draw()
        {
            bool wasUpdated = MainView.Renderer.UpdateRenderTree();
            //if(wasUpdated)
            {
                MainView.Renderer.RenderOffscreen();

                MainView.Renderer.Render();
            }
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

        private static void LoadView()
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
