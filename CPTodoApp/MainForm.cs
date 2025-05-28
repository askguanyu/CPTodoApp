using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using CPTodoApp.Utilities;

namespace CPTodoApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.Text = $"{AppConstants.AppTitle} {Application.ProductVersion}";
            this.TopMost = true;
            this.textBoxLog.MaxLength = int.MaxValue;
            this.checkBoxAlwaysOnTop.Checked = this.TopMost;

            CheckAutoStart();
            InitTrayMenu();
            InitTrayIcon();

            this.checkBoxAutoStart.CheckedChanged += CheckBoxAutoStart_CheckedChanged;
            this.checkBoxAlwaysOnTop.CheckedChanged += CheckBoxAlwaysOnTop_CheckedChanged;

            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            this.Load += (s, e) => this.Hide();
        }

        private void CheckBoxAutoStart_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxAutoStart.Checked)
            {
                AutoStartHelper.EnableAutoStart(Application.ExecutablePath, AppConstants.ShortcutName);
                textBoxLog.Log($"Auto-start enabled. {AutoStartHelper.GetShortcutTargetPath(AppConstants.ShortcutName)}");
            }
            else
            {
                AutoStartHelper.DisableAutoStart(AppConstants.ShortcutName);
                textBoxLog.Log("Auto-start disabled.");
            }
        }

        private void CheckBoxAlwaysOnTop_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = checkBoxAlwaysOnTop.Checked;
        }

        private void ShowWindowHandler(object sender, EventArgs e)
        {
            ShowWindow();
        }

        private void ExitApplicationHandler(object sender, EventArgs e)
        {
            trayIcon.Visible = false;
            Application.Exit();
            Environment.Exit(0);
        }

        private void buttonExit_Click(object sender, EventArgs e) => ExitApplicationHandler(sender, e);

        private void buttonHide_Click(object sender, EventArgs e)
        {
            HideWindow();
        }

        private void TrayIcon_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var mi = typeof(NotifyIcon).GetMethod("ShowContextMenu", BindingFlags.Instance | BindingFlags.NonPublic);
                mi?.Invoke(trayIcon, null);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = true;
            HideWindow();
        }

        protected override void WndProc(ref Message m)
        {
            try
            {
                switch (m.Msg)
                {
                    case GlobalHotKey.WM_HOTKEY:
                        if (m.WParam.ToInt32() == AppConstants.HotKeyId)
                            HandleClipboard();
                        break;
                    case GlobalHotKey.WM_CREATE:
                        GlobalHotKey.RegKey(Handle, AppConstants.HotKeyId, AppConstants.HotKeyModifiers, AppConstants.HotKey);
                        break;
                    case GlobalHotKey.WM_DESTROY:
                        GlobalHotKey.UnRegKey(Handle, AppConstants.HotKeyId);
                        break;
                }
            }
            catch (Exception e)
            {
                GlobalHotKey.UnRegKey(Handle, AppConstants.HotKeyId);
                textBoxLog.Log(e.ToString());
            }

            base.WndProc(ref m);
        }

        private void InitTrayMenu()
        {
            trayMenu.Items.Add(AppConstants.TrayMenuShow, null, ShowWindowHandler);
            trayMenu.Items.Add(AppConstants.TrayMenuExit, null, ExitApplicationHandler);
            trayMenu.Items.Add(new ToolStripLabel { Text = string.Format(AppConstants.TrayMenuVersionFormat, Application.ProductVersion), Enabled = false });
        }

        private void InitTrayIcon()
        {
            trayIcon.DoubleClick += ShowWindowHandler;
            trayIcon.MouseUp += TrayIcon_MouseUp;
        }

        private void ShowWindow()
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            textBoxLog.Log("Window shown.");
        }

        private void HideWindow()
        {
            this.Hide();
            this.ShowInTaskbar = false;
            textBoxLog.Log("Window hidden.");
        }

        private void HandleClipboard()
        {
            string text = ClipboardHelper.GetCleanedClipboardText();
            textBoxLog.Log($"Clipboard raw content:{Environment.NewLine}{text}");

            if (!string.IsNullOrWhiteSpace(text))
            {
                text = $"[{DateTimeOffset.Now:M/dd}] {text}";
                Clipboard.SetText(text);
                StickyNotesAutomation.SendTextAndClearClipboard(text, textBoxLog.Log);
            }
        }

        private void CheckAutoStart()
        {
            try
            {
                checkBoxAutoStart.Checked = AutoStartHelper.IsAutoStartEnabled(AppConstants.ShortcutName);
                if (checkBoxAutoStart.Checked)
                {
                    textBoxLog.Log($"Auto-start is enabled. {AutoStartHelper.GetShortcutTargetPath(AppConstants.ShortcutName)}");
                }
            }
            catch (Exception e)
            {
                textBoxLog.Log(e.ToString());
            }
        }
    }
}
