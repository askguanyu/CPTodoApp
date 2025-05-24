using System;
using System.Windows.Forms;

namespace CPTodoApp
{
    public static class UILogger
    {
        public static void Log(this TextBoxBase textBox, string message)
        {
            if (textBox == null || string.IsNullOrWhiteSpace(message))
            {
                return;
            }

            try
            {
                textBox.AppendText(AppConstants.LogDivider + Environment.NewLine);
                textBox.AppendText($"[{DateTimeOffset.Now.ToString(AppConstants.LogDateFormat)}]{Environment.NewLine}");
                textBox.AppendText(message + Environment.NewLine);
            }
            catch
            {
            }
        }
    }
}