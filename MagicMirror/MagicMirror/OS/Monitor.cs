using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Power;
using System.Runtime.InteropServices;

namespace MagicMirror.OS
{
    public static class Monitor
    {
        public static int WM_SYSCOMMAND = 0x0112;
        public static int SC_MONITORPOWER = 0xF170; //Using the system pre-defined MSDN constants that can be used by the SendMessage() function .


        [DllImport("user32.dll")]
        private static extern int SendMessage(int hWnd, int hMsg, int wParam, int lParam);

        public static void IssueCommand(MONITOR_STATE monitorState)
        {
            SendMessage(0xffff,WM_SYSCOMMAND, SC_MONITORPOWER, (int)monitorState);
        }

    }

    public enum MONITOR_STATE
    {
        On = -1,
        LowPower = 1,
        Off = 2
    }
}
