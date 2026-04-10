using System.Globalization;

namespace ColorPicker
{
    internal class MediaFactory
    {
        /// <summary>
        /// 创建颜色
        /// </summary>
        /// <param name="hexCode">hex颜色值</param>
        /// <returns>返回颜色</returns>
        public static Color ToColor(string hexCode)
        {
            if (string.IsNullOrEmpty(hexCode))
            {
                return Colors.White;
            }

            // Remove the # if it exists.
            var hex = hexCode.TrimStart('#');

            // Create the colour that we will work on.
            var colour = new Color();

            // If we are working with the shorter hex colour codes, duplicate each character as per the
            // spec https://www.w3.org/TR/2001/WD-css3-color-20010305#colorunits
            // (From E3F to EE33FF)
            if (hex.Length is 3 or 4)
            {
                var longHex = "";

                // For each character in the short hex code add two to the long hex code.
                foreach (var t in hex)
                {
                    longHex += t;
                    longHex += t;
                }

                // the short hex is now the long hex.
                hex = longHex;
            }

            try
            {
                const NumberStyles hexStyle = NumberStyles.HexNumber;
                // We should be working with hex codes that are 6 or 8 characters long.
                if (hex.Length is 6)
                {
                    // Create a constant of the style we want (I don't want to type NumberStyles.HexNumber 4
                    // more times.)
                    colour.R = byte.Parse(hex[..2], hexStyle);
                    colour.G = byte.Parse(hex.Substring(2, 2), hexStyle);
                    colour.B = byte.Parse(hex.Substring(4, 2), hexStyle);

                    // We are done, return the parsed colour.
                    colour.A = 255;
                    return colour;
                }

                if (hex.Length is 8)
                {
                    // Create a constant of the style we want (I don't want to type NumberStyles.HexNumber 4
                    // more times.)

                    // Parse Red, Green and Blue from each pair of characters.
                    colour.A = byte.Parse(hex[..2], hexStyle);
                    colour.R = byte.Parse(hex.Substring(2, 2), hexStyle);
                    colour.G = byte.Parse(hex.Substring(4, 2), hexStyle);
                    colour.B = byte.Parse(hex.Substring(6, 2), hexStyle);

                    // We are done, return the parsed colour.
                    return colour;
                }
            }
            catch
            {
                return Colors.White;
            }

            return Colors.White;
        }
        
    }
}