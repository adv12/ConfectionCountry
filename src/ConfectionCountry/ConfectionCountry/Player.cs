// Copyright (c) 2019 Andrew Vardeman.  Published under the MIT license.
// See license.txt in the ConfectionCountry distribution or repository for the
// full text of the license.

using System;

namespace ConfectionCountry
{
    public class Player
    {
        public string Name { get; set; }

        public PlayerColor Color { get; set; }
        public Player()
        {

        }

        public Player(string name, PlayerColor color)
        {
            Name = name;
            Color = color;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }
            Player that = (Player)obj;
            return Equals(this.Name, that.Name) && Equals(this.Color, that.Color);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Color);
        }

    }
}
