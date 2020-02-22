// Copyright (c) 2019 Andrew Vardeman.  Published under the MIT license.
// See license.txt in the ConfectionCountry distribution or repository for the
// full text of the license.

using System.Collections.Generic;

namespace ConfectionCountry
{
    public class Turn
    {
        public Player Player { get; set; }
        public int StartPosition { get; set; }
        public Card Card { get; set; }
        public List<Move> Moves { get; } = new List<Move>();
        public bool Missed => Card == null;

        public Turn(Player player, int startPosition, Card card)
        {
            Player = player;
            StartPosition = startPosition;
            Card = card;
        }
    }
}
