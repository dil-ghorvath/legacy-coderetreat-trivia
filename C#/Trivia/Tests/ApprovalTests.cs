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

    public Game CreateSut()
    {
        return new Game();
    }
}