using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MyCustomApplicationContext());
        }
    }

    public class MyCustomApplicationContext : ApplicationContext
    {
        private NotifyIcon trayIcon;

        public MyCustomApplicationContext()
        {
            // Initialize Tray Icon
            trayIcon = new NotifyIcon()
            {
                Icon = Icon.FromHandle(Resources.Sloth.GetHicon()),
                ContextMenu = new ContextMenu(new MenuItem[] {
                new MenuItem("Exit", Exit),
                new MenuItem("Load DoNet Dll", LoadDll)
            }),
                Visible = true
            };
        }

        void LoadDll(object sender, EventArgs e)
        {
            try
            {
                System.Reflection.Assembly dll1 = System.Reflection.Assembly.LoadFile(@"C:\\DotNetDll.dll");
                if (dll1 != null)
                {
                    object obj = dll1.CreateInstance("DotNetDll.ShowMessageBoxClass");
                    if (obj != null)
                    {
                        System.Reflection.MethodInfo mi = obj.GetType().GetMethod("ShowMessageBox");
                        mi.Invoke(obj, new object[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        
        void Exit(object sender, EventArgs e)
        {
            // Hide tray icon, otherwise it will remain shown until user mouses over it
            trayIcon.Visible = false;

            Application.Exit();
        }
    }
}
