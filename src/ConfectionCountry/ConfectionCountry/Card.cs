// Copyright (c) 2019 Andrew Vardeman.  Published under the MIT license.
// See license.txt in the CollectionCountry distribution or repository for the
// full text of the license.

namespace ConfectionCountry
{
    public class Card
    {
        public SpaceType Type { get; }

        public bool IsDouble { get; }

        public Card(SpaceType type, bool isDouble)
        {
            Type = type;
            IsDouble = isDouble;
        }
    }
}
