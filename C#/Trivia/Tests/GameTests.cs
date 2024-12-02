﻿using System;
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

    public Game CreateSut()
    {
        return new Game();
    }
}