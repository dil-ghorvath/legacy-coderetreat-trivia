using System.IO;
using System;
using System.Threading.Tasks;
using VerifyXunit;
using Trivia;
using Xunit;

namespace Tests;

public class ApprovalTests
{
    public StringWriter output;

    [Fact]
    public Task ApprovalTest()
    {
        var sut = CreateSut();

        var rockQuestion = sut.CreateRockQuestion(1);

        return VerifyXunit.Verifier.Verify(rockQuestion);
    }

    [Fact]
    public Task AddPlayersApproval()
    {
        var sut = CreateSut();

        sut.Add("Chet");

        return VerifyXunit.Verifier.Verify(output.ToString());
    }

    [Fact]
    public Task AddTwoPlayersApproval()
    {
        var sut = CreateSut();

        sut.Add("Chet");
        sut.Add("Pat");

        return VerifyXunit.Verifier.Verify(output.ToString());
    }

    [Fact]
    public Task AddSevenPlayersApproval()
    {
        var sut = CreateSut();

        for (var i = 0; i < 7; i++)
        {
            sut.Add("Player" + i);
        }

        return VerifyXunit.Verifier.Verify(output.ToString());
    }

    [Fact]
    public Task RollApproval()
    {
        var sut = CreateSut();

        sut.Add("Chet");
        sut.Roll(1);

        return VerifyXunit.Verifier.Verify(output.ToString());
    }

    [Fact]
    public Task WasCorrectlyAnwseredApproval()
    {
        var sut = CreateSut();

        sut.Add("Chet");
        sut.WasCorrectlyAnswered();

        return VerifyXunit.Verifier.Verify(output.ToString());
    }

    [Fact]
    public Task WrongAnswerApproval()
    {
        var sut = CreateSut();

        sut.Add("Chet");
        sut.WrongAnswer();

        return VerifyXunit.Verifier.Verify(output.ToString());
    }

    [Fact]
    public Task IsInPenaltyBoxApproval()
    {
        var sut = CreateSut();

        sut.Add("Chet");
        sut.WrongAnswer();
        sut.Roll(2);

        return VerifyXunit.Verifier.Verify(output.ToString());
    }

    public Game CreateSut()
    {
        output = new StringWriter();
        Console.SetOut(output);
        return new Game();
    }
}