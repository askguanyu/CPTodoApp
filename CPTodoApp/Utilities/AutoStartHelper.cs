using System;
using System.IO;
using IWshRuntimeLibrary;
using File = System.IO.File;

namespace CPTodoApp.Utilities
{
    public static class AutoStartHelper
    {
        private static string GetShortcutPath(string shortcutName)
        {
            string startupPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            return Path.Combine(startupPath, shortcutName);
        }

        public static void EnableAutoStart(string exePath, string shortcutName)
        {
            string shortcutPath = GetShortcutPath(shortcutName);
            var shell = new WshShell();
            var shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);
            shortcut.TargetPath = exePath;
            shortcut.Save();
        }

        public static void DisableAutoStart(string shortcutName)
        {
            string shortcutPath = GetShortcutPath(shortcutName);
            if (File.Exists(shortcutPath))
            {
                File.Delete(shortcutPath);
            }
        }

        public static bool IsAutoStartEnabled(string shortcutName)
        {
            string shortcutPath = GetShortcutPath(shortcutName);
            return File.Exists(shortcutPath);
        }

        public static string GetShortcutTargetPath(string shortcutName)
        {
            string shortcutPath = GetShortcutPath(shortcutName);
            
            if (!File.Exists(shortcutPath))
            {
                return null;
            }

            var shell = new WshShell();
            var shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);
            return shortcut.TargetPath;
        }
    }
}