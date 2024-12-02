using System;
using System.CodeDom;
using System.IO;
using Trivia;
using Xunit;

namespace Tests;

public class GameTests
{
    [Fact]
    public void When_PlayerIsAdded_Console_Message_Should_Appear()
    {
        var output = new StringWriter();
        Console.SetOut(output);

        var sut = CreateSut();

        sut.Add("Chet");

        Assert.Contains("Chet was added", output.ToString());
    }

    [Fact]
    public void When_NoPlayerAdded_IsPlayeble_ReturnsFalse()
    {
        var sut = CreateSut();
        var result = sut.IsPlayable();
        Assert.False(result);
    }

    [Fact]
    public void When_TwoPlayersAdded_IsPlayable_Should_ReturnTrue()
    {
        var sut = CreateSut();
        sut.Add("Chet");
        sut.Add("Pat");
        Assert.True(sut.IsPlayable());
    }

    [Fact]
    public void When_TwoPlayersAdded_HowManyPlayers_Should_ReturnTwo()
    {
        var sut = CreateSut();
        sut.Add("Chet");
        sut.Add("Pat");

        Assert.Equal(sut.HowManyPlayers(), 2);
    }

    [Fact]
    public void When_FirstPlayerAnswersSixTimesCorrectly_ShouldWin()
    {
        var sut = CreateSut();
        sut.Add("Chet");
        sut.Add("Pat");
        var playerOneWon = false;
        for (int i = 0; i < 7; i++)
        {
            sut.Roll(1);
            playerOneWon = sut.WasCorrectlyAnswered();
            sut.WrongAnswer();
        }

        Assert.True(playerOneWon);
    }

    [Fact]
    public void When_PlayerRolls_Should_MovePlaces()
    {
        var output = new StringWriter();
        Console.SetOut(output);
        var sut = CreateSut();
        sut.Add("Chet");

        sut.Roll(5);

        Assert.Contains("Chet's new location is 5", output.ToString());
    }

    [Fact]
    public void When_PlayerRollsEven_AndIsInPenaltyBox_ShouldNotMovePlaces()
    {
        var output = new StringWriter();
        Console.SetOut(output);
        var sut = CreateSut();
        sut.Add("Chet");
        sut.WrongAnswer();

        sut.Roll(2);

        Assert.Contains("Chet is not getting out of the penalty box", output.ToString());
    }

    [Fact]
    public void When_PlayerRollsOdd_AndIsInPenaltyBox_AndAnwsersCorrect_ShouldMovePlaces()
    {
        var output = new StringWriter();
        Console.SetOut(output);
        var sut = CreateSut();
        sut.Add("Chet");
        sut.WrongAnswer();

        sut.Roll(3);
        sut.WasCorrectlyAnswered();

        Assert.Contains("Chet's new location is 3", output.ToString());
    }

    [Fact]
    public void When_Player_AnwsersWrong_Should_GoToPenaltyBox()
    { 
        var output = new StringWriter();
        Console.SetOut(output);
        var sut = CreateSut();
        sut.Add("Chet");

        sut.WrongAnswer();

        Assert.Contains("Chet was sent to the penalty box", output.ToString());
    }

    public Game CreateSut()
    {
        return new Game();
    }
}