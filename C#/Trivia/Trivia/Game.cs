using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Game
    {
        private readonly List<Player> _players = new();
        private readonly LinkedList<string> _popQuestions = new();
        private readonly LinkedList<string> _scienceQuestions = new();
        private readonly LinkedList<string> _sportsQuestions = new();
        private readonly LinkedList<string> _rockQuestions = new();

        private int _currentPlayer;

        public Game()
        {
            for (var i = 0; i < 50; i++)
            {
                _popQuestions.AddLast("Pop Question " + i);
                _scienceQuestions.AddLast("Science Question " + i);
                _sportsQuestions.AddLast("Sports Question " + i);
                _rockQuestions.AddLast(CreateRockQuestion(i));
            }
        }

        public string CreateRockQuestion(int index)
        {
            return "Rock Question " + index;
        }

        public bool IsPlayable()
        {
            return _players.Count >= 2;
        }

        public bool Add(string playerName)
        {
            var player = new Player(playerName);
            _players.Add(player);

            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + _players.Count);
            return true;
        }

        public void Roll(int roll)
        {
            var player = _players[_currentPlayer];
            Console.WriteLine(player.Name + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            if (player.InPenaltyBox)
            {
                if (roll % 2 != 0)
                {
                    player.IsGettingOutOfPenaltyBox = true;
                    Console.WriteLine(player.Name + " is getting out of the penalty box");

                    MovePlayer(player, roll);
                    DisplayQuestion();
                }
                else
                {
                    Console.WriteLine(player.Name + " is not getting out of the penalty box");
                    player.IsGettingOutOfPenaltyBox = false;
                }
            }
            else
            {
                MovePlayer(player, roll);
                DisplayQuestion();
            }
        }

        public bool WrongAnswer()
        {
            var player = _players[_currentPlayer];
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(player.Name + " was sent to the penalty box");
            player.InPenaltyBox = true;

            _currentPlayer++;
            if (_currentPlayer == _players.Count) _currentPlayer = 0;
            return true;
        }

        public bool WasCorrectlyAnswered()
        {
            var player = _players[_currentPlayer];

            if (player.InPenaltyBox)
            {
                if (player.IsGettingOutOfPenaltyBox)
                {
                    return AnswerQuestionCorrectly(player);
                }
                else
                {
                    _currentPlayer++;
                    if (_currentPlayer == _players.Count) _currentPlayer = 0;
                    return true;
                }
            }
            else
            {
                return AnswerQuestionCorrectly(player);
            }
        }

        private void DisplayQuestion()
        {
            Console.WriteLine("The category is " + CurrentCategory());
            AskQuestion();
        }

        private void MovePlayer(Player player, int roll)
        {
            player.Place += roll;
            if (player.Place > 11) player.Place -= 12;

            Console.WriteLine(player.Name + "'s new location is " + player.Place);
        }

        private void AskQuestion()
        {
            if (CurrentCategory() == "Pop")
            {
                Console.WriteLine(_popQuestions.First());
                _popQuestions.RemoveFirst();
            }
            if (CurrentCategory() == "Science")
            {
                Console.WriteLine(_scienceQuestions.First());
                _scienceQuestions.RemoveFirst();
            }
            if (CurrentCategory() == "Sports")
            {
                Console.WriteLine(_sportsQuestions.First());
                _sportsQuestions.RemoveFirst();
            }
            if (CurrentCategory() == "Rock")
            {
                Console.WriteLine(_rockQuestions.First());
                _rockQuestions.RemoveFirst();
            }
        }

        private string CurrentCategory()
        {
            var categoryMap = new Dictionary<int, string>
            {
                { 0, "Pop" }, { 4, "Pop" }, { 8, "Pop" },
                { 1, "Science" }, { 5, "Science" }, { 9, "Science" },
                { 2, "Sports" }, { 6, "Sports" }, { 10, "Sports" }
            };

            return categoryMap.GetValueOrDefault(_players[_currentPlayer].Place, "Rock");
        }

        private bool AnswerQuestionCorrectly(Player player)
        {
            Console.WriteLine("Answer was corrent!!!!");
            player.Purse++;
            Console.WriteLine(player.Name + " now has " + player.Purse + " Gold Coins.");

            var winner = DidPlayerWin(player);
            _currentPlayer++;
            if (_currentPlayer == _players.Count) _currentPlayer = 0;

            return winner;
        }

        private bool DidPlayerWin(Player player)
        {
            return player.Purse != 6;
        }
    }
}
