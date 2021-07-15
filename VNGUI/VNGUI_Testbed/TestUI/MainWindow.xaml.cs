#if NOESIS
using Noesis;
using NoesisApp;
using System;
#else
using NoesisApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
#endif

namespace VNGUI_Blend
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

#if NOESIS
        private void InitializeComponent()
        {
            Noesis.GUI.LoadComponent(this, "MainWindow.xaml");
        }

        protected override bool ConnectEvent(object source, string eventName, string handlerName)
        {
            //if (eventName == "OnTitleChange" /*&& handlerName == "RadioButton_Checked"*/)
            /*{
                System.WeakReference wr = new System.WeakReference(this);
                ((Window)source).AddHandler(Window.chan, new RoutedEventHandler((s, e) => { ((MainWindow)wr.Target).RadioButton_Checked(s, e); }));
                return true;
            }*/

            return false;
        }
#endif
    }
}
