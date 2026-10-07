using System.Drawing;
using System.Globalization;

namespace IgniteUI.Blazor.Controls
{
    internal static class ColorPickerColorConverter
    {
        internal static string ToCssColor(Color color)
        {
            return $"#{color.R:X2}{color.G:X2}{color.B:X2}{color.A:X2}";
        }

        internal static Color Parse(string value)
        {
            var color = value.Trim();
            if (color.StartsWith('#'))
            {
                return ParseHex(color);
            }

            if (color.StartsWith("rgb", StringComparison.OrdinalIgnoreCase))
            {
                return ParseRgb(color);
            }

            if (color.StartsWith("hsl", StringComparison.OrdinalIgnoreCase))
            {
                return ParseHsl(color);
            }

            throw new FormatException($"'{value}' is not a color value emitted by IgbColorPicker.");
        }

        private static Color ParseHex(string value)
        {
            var hex = value[1..];
            if (hex.Length is not 6 and not 8 || !uint.TryParse(hex, NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out var parsed))
            {
                throw new FormatException($"'{value}' is not a valid CSS hex color.");
            }

            var red = (byte)(parsed >> (hex.Length == 8 ? 24 : 16));
            var green = (byte)(parsed >> (hex.Length == 8 ? 16 : 8));
            var blue = (byte)(parsed >> (hex.Length == 8 ? 8 : 0));
            var alpha = hex.Length == 8 ? (byte)parsed : byte.MaxValue;
            return Color.FromArgb(alpha, red, green, blue);
        }

        private static Color ParseRgb(string value)
        {
            var parts = GetFunctionParts(value, "rgb");
            if (parts.Length is not 3 and not 4)
            {
                throw new FormatException($"'{value}' is not a valid CSS rgb color.");
            }

            return Color.FromArgb(ParseAlpha(parts, 3), ParseByte(parts[0]), ParseByte(parts[1]), ParseByte(parts[2]));
        }

        private static Color ParseHsl(string value)
        {
            var parts = GetFunctionParts(value, "hsl");
            if (parts.Length is not 3 and not 4)
            {
                throw new FormatException($"'{value}' is not a valid CSS hsl color.");
            }

            var hue = ParseNumber(parts[0]) % 360d;
            if (hue < 0)
            {
                hue += 360d;
            }

            var saturation = ParsePercentage(parts[1]);
            var lightness = ParsePercentage(parts[2]);
            var chroma = (1d - Math.Abs((2d * lightness) - 1d)) * saturation;
            var secondary = chroma * (1d - Math.Abs(((hue / 60d) % 2d) - 1d));
            var match = lightness - (chroma / 2d);
            var (red, green, blue) = hue switch
            {
                < 60d => (chroma, secondary, 0d),
                < 120d => (secondary, chroma, 0d),
                < 180d => (0d, chroma, secondary),
                < 240d => (0d, secondary, chroma),
                < 300d => (secondary, 0d, chroma),
                _ => (chroma, 0d, secondary)
            };

            return Color.FromArgb(ParseAlpha(parts, 3), ToByte(red + match), ToByte(green + match), ToByte(blue + match));
        }

        private static string[] GetFunctionParts(string value, string name)
        {
            var openIndex = value.IndexOf('(');
            if (openIndex != name.Length || !value.EndsWith(')'))
            {
                throw new FormatException($"'{value}' is not a valid CSS {name} color.");
            }

            return value[(openIndex + 1)..^1]
                .Replace("/", " ", StringComparison.Ordinal)
                .Split([' ', ','], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        }

        private static byte ParseByte(string value)
        {
            var number = ParseNumber(value);
            if (number < 0d || number > byte.MaxValue)
            {
                throw new FormatException($"'{value}' is outside the RGB byte range.");
            }

            return ToByte(number / byte.MaxValue);
        }

        private static byte ParseAlpha(string[] parts, int alphaIndex)
        {
            return parts.Length > alphaIndex ? ToByte(ParseNumber(parts[alphaIndex])) : byte.MaxValue;
        }

        private static double ParsePercentage(string value)
        {
            if (!value.EndsWith('%'))
            {
                throw new FormatException($"'{value}' is not a percentage.");
            }

            var percentage = ParseNumber(value[..^1]);
            if (percentage < 0d || percentage > 100d)
            {
                throw new FormatException($"'{value}' is outside the percentage range.");
            }

            return percentage / 100d;
        }

        private static double ParseNumber(string value)
        {
            if (!double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var number))
            {
                throw new FormatException($"'{value}' is not a number.");
            }

            return number;
        }

        private static byte ToByte(double value)
        {
            if (value < 0d || value > 1d)
            {
                throw new FormatException($"'{value}' is outside the alpha range.");
            }

            return (byte)Math.Round(value * byte.MaxValue, MidpointRounding.AwayFromZero);
        }
    }
}
