// Copyright (c) 2019 Andrew Vardeman.  Published under the MIT license.
// See license.txt in the CollectionCountry distribution or repository for the
// full text of the license.

namespace ConfectionCountry
{
    public class SpecialSpace
    {
        public int Position { get; }

        public SpaceType Type { get; }

        public SpecialSpace(int position, SpaceType type)
        {
            Position = position;
            Type = type;
        }
    }
}
