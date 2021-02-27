using Backgammon.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Backgammon
{
    static class Program
    {
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //MessageBox.Show("Aaaaaaaaa");
            Process[] processes = Process.GetProcessesByName("Backgammon");
            if (processes.Length == 1)
            {
                //Settings.Default.Reset();
                if (Settings.Default.IsFirstRun)
                {
                    Settings.Default.Upgrade();
                    Settings.Default.IsFirstRun = false;
                    Settings.Default.Save();
                }
                if ((Settings.Default.User == null || Settings.Default.User == "") && (Settings.Default.Password == null || Settings.Default.Password == ""))
                {
                    Application.Run(new LogInForm());
                }
                else
                {
                    Application.Run(new LogInForm(Settings.Default.User, Settings.Default.Password));
                }
            }
            else
            {
                ShowWindow(processes[0].MainWindowHandle, 9);
                SetForegroundWindow(processes[0].MainWindowHandle);
            }
            //Application.Run(new OfflineMainForm());
        }
    }
}
