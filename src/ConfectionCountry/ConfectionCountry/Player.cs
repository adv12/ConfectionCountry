// Copyright (c) 2019 Andrew Vardeman.  Published under the MIT license.
// See license.txt in the CollectionCountry distribution or repository for the
// full text of the license.

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

    }
}
