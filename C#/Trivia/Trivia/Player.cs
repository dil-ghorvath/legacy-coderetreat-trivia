using System;

namespace Trivia
{
    public class Player
    {
        public string Name { get; set; }
        public int Place { get; set; }
        public int Purse { get; set; }
        public bool InPenaltyBox { get; set; }
        public bool IsGettingOutOfPenaltyBox { get; set; }

        public Player(string name)
        {
            Name = name;
            Place = 0;
            Purse = 0;
            InPenaltyBox = false;
            IsGettingOutOfPenaltyBox = false;
        }
    }
}