using FluentAssertions;
using Trivia;
using Xunit;

namespace Tests;

public class GameUnitTests
{
    [Fact]
    public void When_AtLeastTwoPlayersAdded_IsPlayable_Should_Return_True()
    {
        var sut = CreateSut();
        sut.Add("Chet");
        sut.Add("Pat");

        var result = sut.IsPlayable();

        result.Should().BeTrue();
    }

    [Fact]
    public void When_LessThanTwoPlayersAdded_IsPlayable_Should_Return_False()
    {
        var sut = CreateSut();
        sut.Add("Chet");

        var result = sut.IsPlayable();

        result.Should().BeFalse();
    }

    [Fact]
    public void When_PlayerIsIn()
    {
        var sut = CreateSut();
        sut.Add("Chet");
        sut.WasCorrectlyAnswered();

        
    }

    public Game CreateSut()
    {
        return new Game();
    }
}