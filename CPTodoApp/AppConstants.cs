using System.Windows.Forms;
using CPTodoApp.Utilities;

namespace CPTodoApp
{
    internal static class AppConstants
    {
        public const int HotKeyId = 0x3333;
        public const string ShortcutName = "CPTodoApp.lnk";
        public const string AppTitle = "CPTodo";
        public const string AppName = "CPTodoApp";
        public const string StickyNotesWindowName = "Sticky Notes";
        public const string TrayMenuShow = "Show";
        public const string TrayMenuExit = "Exit";
        public const string TrayMenuVersionFormat = "Ver: {0}";
        public const string LogDivider = "--------";
        public const string LogDateFormat = "o";
        public const int AutomationDelayMs = 100;
        public const Keys HotKey = Keys.D;
        public const GlobalHotKey.KeyModifiers HotKeyModifiers = GlobalHotKey.KeyModifiers.Ctrl | GlobalHotKey.KeyModifiers.Alt;
    }
}