// Copyright (c) 2019 Andrew Vardeman.  Published under the MIT license.
// See license.txt in the ConfectionCountry distribution or repository for the
// full text of the license.

using System;
using System.Collections.Generic;
using System.Linq;

namespace ConfectionCountry
{
    public class Game
    {

        private static SpecialSpace[] _specialSpaces = new SpecialSpace[]
        {
            new SpecialSpace(8, SpaceType.GingerbreadMan),
            new SpecialSpace(19, SpaceType.CandyCane),
            new SpecialSpace(41, SpaceType.Gumdrop),
            new SpecialSpace(68, SpaceType.Peanut),
            new SpecialSpace(91, SpaceType.Lollipop),
            new SpecialSpace(101, SpaceType.IceCream)
        };
        private static int[] _licorices = new int[] {
            45,
            85,
            116
        };
        private static Pass[] _passes = new Pass[]
        {
            new Pass("Rainbow Trail", 4, 58),
            new Pass("Gundrop Pass", 34, 44)
        };
        private static SpaceType[] _spaces = new SpaceType[134];

        public static SpaceType[] Spaces => (SpaceType[])_spaces.Clone();

        static Game() {
            int currentColorIndex = 0;
            SpecialSpace specialSpace;
            for (int i = 0; i < 133; i++)
            {
                if ((specialSpace = _specialSpaces.FirstOrDefault(s => s.Position == i)) != null)
                {
                    _spaces[i] = specialSpace.Type;
                }
                else
                {
                    _spaces[i] = Util.Colors[currentColorIndex++];
                }
                if (currentColorIndex == Util.Colors.Length)
                {
                    currentColorIndex = 0;
                }
            }
            _spaces[133] = SpaceType.Rainbow;
        }

        public static bool IsSpecialSpace(int position, out SpecialSpace space)
        {
            space = _specialSpaces.FirstOrDefault(s => s.Position == position);
            return space != null;
        }

        public static bool IsLicorice(int position)
        {
            return Array.IndexOf(_licorices, position) != -1;
        }

        public static bool HasPass(int position, out Pass pass)
        {
            pass = _passes.FirstOrDefault(p => p.StartPosition == position);
            return pass != null;
        }




        private List<Card> _deck = new List<Card>();

        private List<Card> _discardPile = new List<Card>();

        private bool[] _missingTurns = new bool[4];

        public List<Player> Players { get; } = new List<Player>();

        public int[] PlayerPositions { get; private set; } = new int[4] { -1, -1, -1, -1 };

        public bool Started { get; private set; } = false;

        public Player PreviousPlayer { get; private set; }

        public List<Turn> PreviousTurns { get; } = new List<Turn>();

        public Player NextNonSkippedPlayer
        {
            get
            {
                if (CurrentTurn == null)
                {
                    return Players[0];
                }
                int currentIndex = Players.IndexOf(CurrentTurn.Player);
                bool[] skips = _missingTurns.Take(Players.Count).ToArray();
                int i = currentIndex;
                while (true)
                {
                    i++;
                    if (i == Players.Count)
                    {
                        i = 0;
                    }
                    if (skips[i])
                    {
                        skips[i] = false;
                    }
                    else
                    {
                        return Players[i];
                    }
                }
            }
        }

        public Turn CurrentTurn { get; private set; }

        public bool SetupValid => Players.Count > 1 && Players.All(p => p.Color != 0 && !string.IsNullOrEmpty(p.Name));

        public Player Winner { get; private set; }

        public bool GameOver => Winner != null;

        public bool Accepted { get; private set; }

        public Game()
        {
            InitializeDeck();
            SetupDefaultPlayers();
        }

        public Game(Game previous)
        {
            InitializeDeck();
            if (previous != null)
            {
                foreach (Player p in previous.Players)
                {
                    Players.Add(new Player(p.Name, p.Color));
                }
            }
            else
            {
                SetupDefaultPlayers();
            }
        }

        private void SetupDefaultPlayers()
        {
            Players.Clear();
            Players.Add(new Player("", PlayerColor.Red));
            Players.Add(new Player("", PlayerColor.Yellow));
        }

        public void InitializeDeck()
        {
            _deck.Clear();
            foreach (SpaceType type in Util.Colors)
            {
                for (int i = 0; i < 6; i++)
                {
                    _deck.Add(new Card(type, false));
                }
                for (int i = 0; i < 4; i++)
                {
                    _deck.Add(new Card(type, true));
                }
            }
            foreach (SpecialSpace specialSpace in _specialSpaces)
            {
                _deck.Add(new Card(specialSpace.Type, false));
            }
            ShuffleDeck();
        }
        
        public void ShuffleDeck()
        {
            _deck.Shuffle();
        }

        public void Start()
        {
            Started = true;
        }

        public int GetPlayerPosition(Player p)
        {
            int index = Players.IndexOf(p);
            return PlayerPositions[index];
        }

        private void SetPlayerPosition(Player p, int position)
        {
            int index = Players.IndexOf(p);
            PlayerPositions[index] = position;
            if (position >= _spaces.Length - 1)
            {
                Winner = p;
            }
        }

        public void Next()
        {
            if (CurrentTurn != null)
            {
                PreviousTurns.Add(CurrentTurn);
            }

            Player player;
            
            if (PreviousPlayer == null)
            {
                player = Players[0];
            }
            else
            {
                int previousPlayerIndex = Players.IndexOf(PreviousPlayer);
                if (previousPlayerIndex == Players.Count - 1)
                {
                    player = Players[0];
                }
                else
                {
                    player = Players[previousPlayerIndex + 1];
                }
            }
            Card card = null;
            if (_missingTurns[Players.IndexOf(player)])
            {
                // Do nothing except clear the flag.  This still gets recorded
                // as a turn to keep bookkeeping cleaner, just with no card and
                // no moves. 
                _missingTurns[Players.IndexOf(player)] = false;
            }
            else
            {
                if (_deck.Count == 0)
                {
                    InitializeDeck();
                }
                card = _deck[_deck.Count - 1];
                _deck.RemoveAt(_deck.Count - 1);
                _discardPile.Add(card);
            }
            int position = GetPlayerPosition(player);
            CurrentTurn = new Turn(player, position, card);
            CalculateMoves(CurrentTurn);
            if (CurrentTurn.Moves.Count > 0 && IsLicorice(CurrentTurn.Moves.Last().Position))
            {
                _missingTurns[Players.IndexOf(player)] = true;
            }
            PreviousPlayer = player;
            if (CurrentTurn.Missed)
            {
                Next(); // Don't stop gameplay for a missed turn.
            }
        }

        private void CalculateMoves(Turn turn)
        {
            int position = turn.StartPosition;
            if (turn.Missed)
            {
                return;
            }
            if (Util.IsColor(turn.Card.Type))
            {
                position = GetPositionOfNext(turn.Card.Type, position);
                if (turn.Card.IsDouble)
                {
                    position = GetPositionOfNext(turn.Card.Type, position);
                }
                turn.Moves.Add(new Move(GetColorSpaceTypeName(turn.Card.Type, turn.Card.IsDouble), position, false));
            }
            else
            {
                foreach (SpecialSpace specialSpace in _specialSpaces)
                {
                    if (specialSpace.Type == turn.Card.Type)
                    {
                        SpecialSpace space = _specialSpaces.FirstOrDefault(s => s.Type == turn.Card.Type);
                        if (space != null)
                        {
                            position = space.Position;
                            turn.Moves.Add(new Move(GetSpecialSpaceTypeName(turn.Card.Type), position, true));
                        }
                    }
                }
            }
            if (HasPass(position, out Pass pass))
            {
                position = pass.EndPosition;
                turn.Moves.Add(new Move(pass.Name, pass.EndPosition, true));
            }
            SetPlayerPosition(turn.Player, position);
        }

        public void PlayToEnd()
        {
            while(!GameOver)
            {
                Next();
            }
        }

        private int GetPositionOfNext(SpaceType spaceType, int startPosition)
        {
            for (int i = startPosition + 1; i < _spaces.Length; i++)
            {
                if (_spaces[i] == spaceType || _spaces[i] == SpaceType.Rainbow)
                {
                    return i;
                }
            }
            return _spaces.Length - 1;
        }

        private string GetColorSpaceTypeName(SpaceType type, bool isDouble)
        {
            string name = isDouble ? "Double " : "";
            return name + Enum.GetName(typeof(SpaceType), type);
        }

        private string GetSpecialSpaceTypeName(SpaceType type)
        {
            switch (type)
            {
                case SpaceType.GingerbreadMan:
                    return "Gingerbread Man";
                case SpaceType.CandyCane:
                    return "Candy Cane";
                case SpaceType.Gumdrop:
                    return "Gumdrop";
                case SpaceType.Peanut:
                    return "Peanut";
                case SpaceType.Lollipop:
                    return "Lollipop";
                case SpaceType.IceCream:
                    return "Ice Cream";
            }
            return "????";
        }

        public void Accept()
        {
            if (GameOver)
            {
                Accepted = true;
            }
        }
    }
}
