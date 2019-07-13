// Copyright (c) 2019 Andrew Vardeman.  Published under the MIT license.
// See license.txt in the CollectionCountry distribution or repository for the
// full text of the license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConfectionCountry
{
    public class Batch
    {
        private Game _lastGame;

        public Game CurrentGame { get; private set; }

        public int NumberOfGames { get; set; }

        public List<Game> CompletedGames { get; } = new List<Game>();

        public Batch(Game lastGame)
        {
            _lastGame = lastGame;
        }

        public void Next()
        {
            if (CompletedGames.Count < NumberOfGames)
            {
                Game tmp = new Game(_lastGame);
                _lastGame = CurrentGame;
                CurrentGame = tmp;
                CurrentGame.PlayToEnd();
                CompletedGames.Add(CurrentGame);
            }
        }

    }
}
