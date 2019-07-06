// Copyright (c) 2019 Andrew Vardeman.  Published under the MIT license.
// See license.txt in the CollectionCountry distribution or repository for the
// full text of the license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
