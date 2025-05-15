using FluentAssertions;
using MableBanking.Domain.Models;

namespace MableBanking.Domain.UnitTests;

public class TransferResultTests
{
    [Fact]
    public void Failure_ShouldReturnUnsuccessfulResult()
    {
        var transfer = new Transfer("1234567890123456", "6543210987654321", 50);

        var result = TransferResult.Failure(transfer, "Transfer failed");

        result.IsSuccess.Should().BeFalse();
        result.Message.Should().Be("Transfer failed");
        result.Transfer.Should().Be(transfer);
    }

    [Fact]
    public void Failure_ShouldThrowArgumentNullException_WhentransferIsNull()
    {
        Action failure = () => TransferResult.Failure(null!, "Transfer failed");

        failure.Should().Throw<ArgumentNullException>()
            .WithParameterName("transfer");
    }

    [Fact]
    public void Success_ShouldReturnSuccessfulResult()
    {
        var transfer = new Transfer("1234567890123456", "6543210987654321", 50);

        var result = TransferResult.Success(transfer);

        result.IsSuccess.Should().BeTrue();
        result.Message.Should().BeNull();
        result.Transfer.Should().Be(transfer);
    }

    [Fact]
    public void Success_ShouldThrowArgumentNullException_WhentransferIsNull()
    {
        Action success = () => TransferResult.Success(null!);

        success.Should().Throw<ArgumentNullException>()
            .WithParameterName("transfer");
    }
}
