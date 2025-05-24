using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CPTodoApp.Utilities
{
    public static class ClipboardHelper
    {
        public static string GetCleanedClipboardText()
        {
            var input = Clipboard.GetText()?.Trim() ?? "";
            string tempText = input.Replace(@"\r\n", "  ").Replace(@"\n", "  ").Replace(System.Environment.NewLine, "  ");
            return Regex.Replace(tempText, @"\s{2,}", " ");
        }
    }
}