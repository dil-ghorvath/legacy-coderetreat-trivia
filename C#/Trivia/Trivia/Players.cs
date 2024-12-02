using System.Collections.Generic;

namespace Trivia;

public class Players
{
    private readonly List<Player> _players = new List<Player>();

    public void AddPlayer(string playerName)
    {
        _players.Add(new Player(playerName));
    }

    public int HowManyPlayers()
    {
        return _players.Count;
    }

    public Player GetCurrentPlayer(int currentPlayerIndex)
    {
        return _players[currentPlayerIndex];
    }

    public void MoveToNextPlayer(ref int currentPlayerIndex)
    {
        currentPlayerIndex = (currentPlayerIndex + 1) % _players.Count;
    }
}
