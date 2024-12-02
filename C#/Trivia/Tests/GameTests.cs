using System.IO;
using System;
using FluentAssertions;
using Trivia;
using Xunit;

namespace Tests;

public class GameTests
{
    [Fact]
    public void Given_QuestionIs_Correctly_Answered_Response_Should_Be_Correct()
    {
        var sut = CreateSut();
        var output = new StringWriter();
        Console.SetOut(output);

        sut.Add("Chet");
        sut.WasCorrectlyAnswered();

        output.ToString().Should().Contain("Answer was correct!");
    }

    [Fact]
    public void Given_More_Than_Six_Players_AreAdded_Should_Work()
    {
        var sut = CreateSut();
        var output = new StringWriter();
        Console.SetOut(output);

        for (var i = 1; i <= 7; i++)
        {
                sut.Add("Chet" + i);

        }
        output.ToString().Should().Contain("They are player number 7");
    }

    [Fact]
    public void Given_OnlyOnePlayer_IsAdded_Game_Should_NotBePlayable()
    {
        var sut = CreateSut();
        var output = new StringWriter();
        Console.SetOut(output);
        sut.Add("Chet");
        Action act = () => sut.Roll(2);

        act.Should().Throw<Exception>().WithMessage("Please add at least 2 players before starting!");
    }

    [Fact]
    public void Given_Add_Method_Invoked_WithNullValue_Should_ThrowException()
    {
        var sut = CreateSut();
        var output = new StringWriter();
        Console.SetOut(output);

        Action act = () => sut.Add(null);

        act.Should().Throw<ArgumentNullException>().WithMessage("Please add a name for the player.");
    }

    public Game CreateSut()
    {
        return new Game();
    }
}