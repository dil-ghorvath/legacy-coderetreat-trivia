using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Game
    {
        private readonly Players _players = new();
        private readonly Questions _questions = new();

        private int _currentPlayer;
        private bool _isGettingOutOfPenaltyBox;


        public bool Add(string playerName)
        {
            _players.AddPlayer(playerName);

            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + _players.HowManyPlayers());
            return true;
        }

        public void Roll(int roll)
        {
            var currentPlayer = _players.GetCurrentPlayer(_currentPlayer);
            Console.WriteLine(currentPlayer.Name + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            if (currentPlayer.InPenaltyBox)
            {
                if (roll % 2 != 0)
                {
                    _isGettingOutOfPenaltyBox = true;

                    Console.WriteLine(currentPlayer.Name + " is getting out of the penalty box");

                    MovePlayer(roll, currentPlayer);
                }
                else
                {
                    Console.WriteLine(currentPlayer.Name + " is not getting out of the penalty box");
                    _isGettingOutOfPenaltyBox = false;
                }
            }
            else
            {
                MovePlayer(roll, currentPlayer);
            }
        }

        public bool WasCorrectlyAnswered()
        {
            var currentPlayer = _players.GetCurrentPlayer(_currentPlayer);

            if (currentPlayer.InPenaltyBox)
            {
                if (_isGettingOutOfPenaltyBox)
                {
                    return QuestionWasCorrectlyAnswered(currentPlayer);
                }
                else
                {
                    _players.MoveToNextPlayer(ref _currentPlayer);
                    return true;
                }
            }
            else
            {
                return QuestionWasCorrectlyAnswered(currentPlayer);
            }
        }

        public bool WrongAnswer()
        {
            var currentPlayer = _players.GetCurrentPlayer(_currentPlayer);
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(currentPlayer.Name + " was sent to the penalty box");
            currentPlayer.InPenaltyBox = true;

            _players.MoveToNextPlayer(ref _currentPlayer);
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
            var position = _players.GetCurrentPlayer(_currentPlayer).Position;
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
            _players.MoveToNextPlayer(ref _currentPlayer);

            return winner;
        }

        private bool DidPlayerWin()
        {
            return _players.GetCurrentPlayer(_currentPlayer).Purse != 6;
        }
    }

}
