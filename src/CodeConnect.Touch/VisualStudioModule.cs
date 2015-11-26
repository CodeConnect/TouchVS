using EnvDTE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CodeConnect.Touch
{
    /// <summary>
    /// Exposes Visual Studio APIs to this package
    /// </summary>
    public static class VisualStudioModule
    {
        private static DTE envDte;
        public static SolidColorBrush ForegroundBrush { get; private set; }
        public static SolidColorBrush BackgroundBrush { get; private set; }
        public static SolidColorBrush BackgroundHighlightTransparentBrush { get; private set; }
        public static SolidColorBrush BackgroundTransparentBrush { get; private set; }
        public static SolidColorBrush HyperlinkBrush { get; private set; }

        public static void Initialize(DTE dte)
        {
            envDte = dte;
            initializeBrushes();
        }

        public static void ExecuteCommand(string commandName, string commandArgs = "")
        {
            var test = commandName;
            try
            {
                envDte.ExecuteCommand(commandName, commandArgs);
            }
            catch (Exception ex)
            {
                envDte.StatusBar.Text = $"Error executing {commandName}";
            }
        }

        private static void initializeBrushes()
        {
            const string CATEGORY_FONTS_AND_COLORS = "FontsAndColors";
            const string PAGE_TEXT_EDITOR = "TextEditor";
            var fontsAndColors = envDte.Properties[CATEGORY_FONTS_AND_COLORS, PAGE_TEXT_EDITOR];
            var fontsAndColorsItems = (FontsAndColorsItems)fontsAndColors.Item(nameof(FontsAndColorsItems)).Object;

            var backgroundBrush = GetBrush(fontsAndColorsItems.Item("Plain Text").Background);
            backgroundBrush.Freeze();
            var foregroundBrush = GetBrush(fontsAndColorsItems.Item("Plain Text").Foreground);
            foregroundBrush.Freeze();
            var backgroundTransparentBrush = GetBrush(fontsAndColorsItems.Item("Plain Text").Background, 210);
            backgroundBrush.Freeze();
            var backgroundHighlightTransparentBrush = GetBrush(fontsAndColorsItems.Item("urlformat").Foreground, 170);
            foregroundBrush.Freeze();
            var hyperlinkBrush = GetBrush(fontsAndColorsItems.Item("urlformat").Foreground); // also "HTML Element Name"
            hyperlinkBrush.Freeze();

            BackgroundBrush = backgroundBrush;
            ForegroundBrush = foregroundBrush;
            BackgroundTransparentBrush = backgroundTransparentBrush;
            BackgroundHighlightTransparentBrush = backgroundHighlightTransparentBrush;
            HyperlinkBrush = hyperlinkBrush;
        }

        private static SolidColorBrush GetBrush(uint color, byte opacity = 255)
        {
            int intColor = Convert.ToInt32(color);
            return new SolidColorBrush(Color.FromArgb(
                    opacity,
                    (byte)((intColor) & 0xFF),
                    (byte)((intColor >> 8) & 0xFF),
                    (byte)((intColor >> 16) & 0xFF)));
        }

    }

}
