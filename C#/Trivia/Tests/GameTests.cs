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
    public void Given_QuestionIs_Correctly_Answered_Response_Should_Be_Exactly_Correct()
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

    public Game CreateSut()
    {
        return new Game();
    }
}