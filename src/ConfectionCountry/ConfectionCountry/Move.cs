// Copyright (c) 2019 Andrew Vardeman.  Published under the MIT license.
// See license.txt in the ConfectionCountry distribution or repository for the
// full text of the license.

namespace ConfectionCountry
{
    public class Move
    {
        public string Name { get; }
        public int Position { get; }

        public bool Fly { get; }

        public Move(string name, int position, bool fly)
        {
            Name = name;
            Position = position;
            Fly = fly;
        }
    }
}
