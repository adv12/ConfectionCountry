// Copyright (c) 2019 Andrew Vardeman.  Published under the MIT license.
// See license.txt in the CollectionCountry distribution or repository for the
// full text of the license.

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ConfectionCountry
{
    public static class Util
    {
        public static string GetBootstrapButtonClass(PlayerColor color)
        {
            switch (color)
            {
                case PlayerColor.Red:
                    return "btn-danger";
                case PlayerColor.Yellow:
                    return "btn-warning";
                case PlayerColor.Green:
                    return "btn-success";
                case PlayerColor.Blue:
                    return "btn-primary";
                default:
                    return "btn-outline-secondary";
            }
        }

        public static string GetBootstrapClasses(PlayerColor color)
        {
            switch (color)
            {
                case PlayerColor.Red:
                    return "bg-danger text-white";
                case PlayerColor.Yellow:
                    return "bg-warning";
                case PlayerColor.Green:
                    return "bg-success text-white";
                case PlayerColor.Blue:
                    return "bg-primary text-white";
                default:
                    return "bg-white";
            }
        }

        public static string GetColorClass(SpaceType type)
        {
            switch (type)
            {
                case SpaceType.Red:
                    return "red";
                case SpaceType.Orange:
                    return "orange";
                case SpaceType.Yellow:
                    return "yellow";
                case SpaceType.Green:
                    return "green";
                case SpaceType.Blue:
                    return "blue";
                case SpaceType.Purple:
                    return "purple";
                default:
                    return "pink";
            }
        }

        public static string GetName(SpecialSpace space)
        {
            return GetName(space.Type);
        }

        public static string GetName(SpaceType type)
        {
            return Enum.GetName(typeof(SpaceType), type);
        }

        public static string GetNameWithSpaces(SpaceType type)
        {
            string name = GetName(type);
            StringBuilder sb = new StringBuilder();
            sb.Append(name[0]);
            for (int i = 1; i < name.Length; i++)
            {
                char cur = name[i];
                if (char.IsUpper(cur))
                {
                    sb.Append(' ');
                }
                sb.Append(cur);
            }
            return sb.ToString();
        }

        public static string GetNameAbbreviation(SpecialSpace space)
        {
            return GetNameAbbreviation(space.Type);
        }

        public static string GetNameAbbreviation(SpaceType type)
        {
            string name = GetName(type);
            return Regex.Replace(name, "[a-z]", "");
        }

        private static SpaceType[] _colors = new SpaceType[] {
            // This is game board order; do not change!
            SpaceType.Red,
            SpaceType.Purple,
            SpaceType.Yellow,
            SpaceType.Blue,
            SpaceType.Orange,
            SpaceType.Green
        };

        public static SpaceType[] Colors => _colors;

        public static bool IsColor(SpaceType type)
        {
            return Array.IndexOf(_colors, type) > -1;
        }
    }
}
