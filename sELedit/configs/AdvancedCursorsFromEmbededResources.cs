using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sELedit
{
    public class AdvancedCursorsFromEmbededResources
    {
        [DllImport("user32.dll")]
        static extern IntPtr CreateIconFromResource(byte[] presbits, uint dwResSize, bool fIcon, uint dwVer);

        public static Cursor Create(byte[] resource)
        {
            IntPtr myNew_Animated_hCursor;
            myNew_Animated_hCursor = CreateIconFromResource(resource, (uint)resource.Length, false, 0x00030000);
            return new Cursor(myNew_Animated_hCursor);
        }
    }
}
