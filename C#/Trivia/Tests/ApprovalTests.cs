using System.IO;
using System;
using System.Threading.Tasks;
using VerifyXunit;
using Trivia;
using Xunit;

namespace Tests;

public class ApprovalTests
{
    [Fact]
    public Task ApprovalTest()
    {
        var output = new StringWriter();
        Console.SetOut(output);
        var sut = CreateSut();

        var rockQuestion = sut.CreateRockQuestion(1);

        return VerifyXunit.Verifier.Verify(rockQuestion);
    }

    [Fact]
    public Task AddPlayersApproval()
    {
        var output = new StringWriter();
        Console.SetOut(output);
        var sut = CreateSut();

        sut.Add("Chet");

        return VerifyXunit.Verifier.Verify(output.ToString());
    }

    [Fact]
    public Task AddTwoPlayersApproval()
    {
        var output = new StringWriter();
        Console.SetOut(output);
        var sut = CreateSut();

        sut.Add("Chet");
        sut.Add("Pat");

        return VerifyXunit.Verifier.Verify(output.ToString());
    }

    [Fact]
    public Task AddSevenPlayersApproval()
    {
        var output = new StringWriter();
        Console.SetOut(output);
        var sut = CreateSut();

        for (int i = 0; i < 7; i++)
        {
            sut.Add("Player" + i);
        }

        return VerifyXunit.Verifier.Verify(output.ToString());
    }

    [Fact]
    public Task RollApproval()
    {
        var output = new StringWriter();
        Console.SetOut(output);
        var sut = CreateSut();

        sut.Add("Chet");
        sut.Roll(1);

        return VerifyXunit.Verifier.Verify(output.ToString());
    }

    public Game CreateSut()
    {
        return new Game();
    }
}