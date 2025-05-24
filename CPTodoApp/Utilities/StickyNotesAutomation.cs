using System;
using System.Threading;
using System.Windows.Automation;
using System.Windows.Automation.Text;
using System.Windows.Forms;

namespace CPTodoApp.Utilities
{
    public static class StickyNotesAutomation
    {
        private const string StickyNotesAppTitle = "Sticky Notes";

        public static bool SendTextAndClearClipboard(string text, Action<string> log = null)
        {
            try
            {
                var stickyNotesWindow = AutomationElement.RootElement
                    .FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, StickyNotesAppTitle));

                if (stickyNotesWindow == null)
                {
                    log?.Invoke("Cannot find AutomationElement.");
                    return false;
                }

                var textBox = stickyNotesWindow
                    .FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Edit));

                if (textBox == null)
                {
                    log?.Invoke("Cannot find text box.");
                    return false;
                }

                if (textBox.TryGetCurrentPattern(TextPattern.Pattern, out object patternObject))
                {
                    var pattern = patternObject as TextPattern;
                    pattern?.DocumentRange.Select();
                    Thread.Sleep(100);

                    textBox.SetFocus();
                    SendKeys.SendWait("{END}");
                    Thread.Sleep(100);

                    textBox.SetFocus();
                    SendKeys.SendWait("{RIGHT}");
                    Thread.Sleep(100);

                    textBox.SetFocus();
                    SendKeys.SendWait("{ENTER}");
                    Thread.Sleep(100);

                    textBox.SetFocus();
                    SendKeys.SendWait("{ENTER}");
                    Thread.Sleep(100);

                    textBox.SetFocus();
                    SendKeys.SendWait("- ");
                    Thread.Sleep(100);
                    SendKeys.SendWait("^v");
                    Thread.Sleep(100);

                    Clipboard.Clear();
                    log?.Invoke($"Sent text to Sticky Notes and clear Clipboard:{Environment.NewLine}{text}");
                    return true;
                }
                else
                {
                    log?.Invoke("Cannot find TextPattern.");
                    return false;
                }
            }
            catch (Exception e)
            {
                log?.Invoke(e.ToString());
                return false;
            }
        }
    }
}