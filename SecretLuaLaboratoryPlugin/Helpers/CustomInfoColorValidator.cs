using System;
using System.Linq;
using System.Text.RegularExpressions;
using static Misc;

namespace LuaLab.Helpers
{
    public static class CustomInfoColorValidator
    {
        public static readonly Regex colorTagRegex = new Regex("<color=([A-Za-z0-9#]*)>", RegexOptions.Compiled);

        public static bool IsValid(string text)
        {
            MatchCollection matches = colorTagRegex.Matches(text);

            if (matches.Count == 0)
            {
                return true;
            }


            bool anyFalse = false;
            foreach (Match match in matches)
            {
                string color = match.Groups[1].Value;

                if (color.StartsWith("#"))
                {
                    anyFalse |= !AllowedColors.Values.Contains(color);
                }
                else
                {
                    anyFalse |= !Enum.TryParse(color, true, out PlayerInfoColorTypes _);
                }
            }

            return !anyFalse;
        }
    }
}
