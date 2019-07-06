// Copyright (c) 2019 Andrew Vardeman.  Published under the MIT license.
// See license.txt in the CollectionCountry distribution or repository for the
// full text of the license.

namespace ConfectionCountry
{
    public class Move
    {
        public string Name { get; }
        public int Position { get; }

        public Move(string name, int position)
        {
            Name = name;
            Position = position;
        }
    }
}
