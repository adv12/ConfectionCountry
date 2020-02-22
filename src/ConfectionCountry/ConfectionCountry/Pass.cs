// Copyright (c) 2019 Andrew Vardeman.  Published under the MIT license.
// See license.txt in the ConfectionCountry distribution or repository for the
// full text of the license.

namespace ConfectionCountry
{
    public class Pass
    {
        public string Name { get; }

        public int StartPosition { get; }

        public int EndPosition { get; }

        public Pass(string name, int startPosition, int endPosition)
        {
            Name = name;
            StartPosition = startPosition;
            EndPosition = endPosition;
        }
    }
}
