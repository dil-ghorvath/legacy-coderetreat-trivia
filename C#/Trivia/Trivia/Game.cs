using System;

namespace Trivia
{
    public class Game
    {
        private readonly Players _players = new();
        private readonly Questions _questions = new();

        private int _currentPlayerIndex;
        private bool _currentPlayerIsGettingOutOfPenaltyBox;


        public bool AddPlayer(string playerName)
        {
            _players.AddPlayer(playerName);

            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + _players.HowManyPlayers());
            return true;
        }

        public void Roll(int roll)
        {
            var currentPlayer = _players.GetCurrentPlayer(_currentPlayerIndex);
            Console.WriteLine($"{currentPlayer.Name} is the current player");
            Console.WriteLine($"They have rolled a {roll}");

            if (currentPlayer.InPenaltyBox)
            {
                HandlePenaltyBoxPlayer(roll, currentPlayer);
            }
            else
            {
                MovePlayer(roll, currentPlayer);
            }
        }

        private void HandlePenaltyBoxPlayer(int roll, Player currentPlayer)
        {
            if (roll % 2 != 0)
            {
                _currentPlayerIsGettingOutOfPenaltyBox = true;

                Console.WriteLine(currentPlayer.Name + " is getting out of the penalty box");

                MovePlayer(roll, currentPlayer);
            }
            else
            {
                Console.WriteLine(currentPlayer.Name + " is not getting out of the penalty box");
                _currentPlayerIsGettingOutOfPenaltyBox = false;
            }
        }

        public bool WasCorrectlyAnswered()
        {
            var currentPlayer = _players.GetCurrentPlayer(_currentPlayerIndex);

            return currentPlayer.InPenaltyBox ? HandleCorrectAnswer(currentPlayer) : QuestionWasCorrectlyAnswered(currentPlayer);
        }

        private bool HandleCorrectAnswer(Player currentPlayer)
        {
            if (_currentPlayerIsGettingOutOfPenaltyBox)
            {
                return QuestionWasCorrectlyAnswered(currentPlayer);
            }
            else
            {
                _players.MoveToNextPlayer(ref _currentPlayerIndex);
                return true;
            }
        }

        public bool WrongAnswer()
        {
            var currentPlayer = _players.GetCurrentPlayer(_currentPlayerIndex);
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(currentPlayer.Name + " was sent to the penalty box");
            currentPlayer.InPenaltyBox = true;

            _players.MoveToNextPlayer(ref _currentPlayerIndex);
            return true;
        }

        private void MovePlayer(int roll, Player currentPlayer)
        {
            currentPlayer.Position = (currentPlayer.Position + roll) % 12;

            Console.WriteLine(currentPlayer.Name
                              + "'s new location is "
                              + currentPlayer.Position);
            Console.WriteLine("The category is " + CurrentCategory());
            AskQuestion();
        }

        private void AskQuestion()
        {
            var category = CurrentCategory();
            Console.WriteLine(_questions.GetNextQuestion(category));
        }

        private string CurrentCategory()
        {
            var position = _players.GetCurrentPlayer(_currentPlayerIndex).Position;
            if (position == 0 || position == 4 || position == 8) return "Pop";
            if (position == 1 || position == 5 || position == 9) return "Science";
            if (position == 2 || position == 6 || position == 10) return "Sports";
            return "Rock";
        }

        private bool QuestionWasCorrectlyAnswered(Player currentPlayer)
        {
            Console.WriteLine("Answer was correct!!!!");
            currentPlayer.Purse++;
            Console.WriteLine(currentPlayer.Name
                              + " now has "
                              + currentPlayer.Purse
                              + " Gold Coins.");

            var winner = DidPlayerWin();
            _players.MoveToNextPlayer(ref _currentPlayerIndex);

            return winner;
        }

        private bool DidPlayerWin()
        {
            return _players.GetCurrentPlayer(_currentPlayerIndex).Purse != 6;
        }
    }
}
