using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Leap;
using System.Drawing;
using System.Windows.Forms;

namespace TwitterFind
{

    class LeapAsMouse
    {

        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        public static void EnableCursorLeap(Frame frame)
        {
            var coordinate = GetNormalizedXandY(frame);
            if ((int)frame.Fingers[0].TipVelocity.Magnitude <= 25) return;
            SetCursorPos((int)coordinate.X, (int)coordinate.Y);
            if (frame.Fingers.Count != 2) return;
            mouse_event(0x0002 | 0x0004, 0, (int)coordinate.X, (int)coordinate.Y, 0);
        }

        private static Point GetNormalizedXandY(Frame frame)
        {
            var interactionBox = frame.InteractionBox;
            var vector = interactionBox.NormalizePoint(frame.Fingers.Frontmost.StabilizedTipPosition);

            var width = SystemInformation.VirtualScreen.Width;
            var height = SystemInformation.VirtualScreen.Height;
            var x = (vector.x * width);
            var y = height - (vector.y * height);

            return new Point() { X = (int)x, Y = (int)y };
        }

    }
}
