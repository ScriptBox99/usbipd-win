﻿// SPDX-FileCopyrightText: 2022 Frans van Dorsselaer
//
// SPDX-License-Identifier: GPL-3.0-only

using System.CommandLine;

namespace UnitTests;

using ExitCode = Program.ExitCode;

[TestClass]
sealed class Parse_wsl_list_Tests
    : ParseTestBase
{
    [TestMethod]
    public void Success()
    {
        var mock = CreateMock();
        mock.Setup(m => m.WslList(false,
            It.IsNotNull<IConsole>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(ExitCode.Success));

        Test(ExitCode.Success, mock, "wsl", "list");
    }

    [TestMethod]
    public void SuccessWithUsbids()
    {
        var mock = CreateMock();
        mock.Setup(m => m.WslList(true,
            It.IsNotNull<IConsole>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(ExitCode.Success));

        Test(ExitCode.Success, mock, "wsl", "list", "--usbids");
    }

    [TestMethod]
    public void Failure()
    {
        var mock = CreateMock();
        mock.Setup(m => m.WslList(false,
            It.IsNotNull<IConsole>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(ExitCode.Failure));

        Test(ExitCode.Failure, mock, "wsl", "list");
    }

    [TestMethod]
    public void Canceled()
    {
        var mock = CreateMock();
        mock.Setup(m => m.WslList(false,
            It.IsNotNull<IConsole>(), It.IsAny<CancellationToken>())).Throws<OperationCanceledException>();

        Test(ExitCode.Canceled, mock, "wsl", "list");
    }

    [TestMethod]
    public void Help()
    {
        Test(ExitCode.Success, "wsl", "list", "--help");
    }

    [TestMethod]
    public void StrayArgument()
    {
        Test(ExitCode.ParseError, "wsl", "list", "stray-argument");
    }
}
