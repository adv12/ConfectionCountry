// Copyright (c) 2019 Andrew Vardeman.  Published under the MIT license.
// See license.txt in the ConfectionCountry distribution or repository for the
// full text of the license.

using System.Collections.Generic;

namespace ConfectionCountry
{
    public class Batch
    {
        public Game TemplateGame { get; private set; }

        public Game CurrentGame { get; private set; }

        public int NumberOfGames { get; set; } = 1000;

        public List<Game> CompletedGames { get; } = new List<Game>();

        public bool Complete => CompletedGames.Count >= NumberOfGames;

        private int[] _winCounts = new int[] { 0, 0, 0, 0 };

        public Batch(Game templateGame)
        {
            TemplateGame = templateGame;
        }

        public void Next()
        {
            if (CompletedGames.Count < NumberOfGames)
            {
                CurrentGame = new Game(TemplateGame);
                CurrentGame.PlayToEnd();
                _winCounts[CurrentGame.Players.IndexOf(CurrentGame.Winner)]++;
                CompletedGames.Add(CurrentGame);
            }
        }

        public int GetWinCount(Player p)
        {
            if (CurrentGame == null)
            {
                return 0;
            }
            return _winCounts[CurrentGame.Players.IndexOf(p)];
        }

    }
}
