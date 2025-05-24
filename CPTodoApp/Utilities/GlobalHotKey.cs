using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CPTodoApp.Utilities
{
    public static class GlobalHotKey
    {
        public const int WM_HOTKEY = 0x0312;
        public const int WM_CREATE = 0x0001;
        public const int WM_DESTROY = 0x0002;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, KeyModifiers fsModifiers, Keys vk);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        [Flags]
        public enum KeyModifiers
        {
            None = 0,
            Alt = 1,
            Ctrl = 2,
            Shift = 4,
            WindowsKey = 8
        }

        /// <summary>
        /// Registers a global hotkey. Returns true if successful or already registered, false otherwise.
        /// </summary>
        public static bool RegKey(IntPtr hwnd, int hotKeyId, KeyModifiers keyModifiers, Keys key)
        {
            if (RegisterHotKey(hwnd, hotKeyId, keyModifiers, key))
            {
                return true;
            }

            // 1409: Hotkey already registered
            int error = Marshal.GetLastWin32Error();
            return error == 1409;
        }

        /// <summary>
        /// Unregisters a global hotkey.
        /// </summary>
        public static void UnRegKey(IntPtr hwnd, int hotKeyId)
        {
            UnregisterHotKey(hwnd, hotKeyId);
        }
    }
}
