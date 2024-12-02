namespace Trivia;

public class Player
{
    public string Name { get; }
    public int Position { get; set; }
    public int Purse { get; set; }
    public bool InPenaltyBox { get; set; }

    public Player(string name)
    {
        Name = name;
        Position = 0;
        Purse = 0;
        InPenaltyBox = false;
    }
}
