using FluentAssertions;
using MableBanking.Domain.Models;

namespace MableBanking.Domain.UnitTests;

public class TransferTests
{
    [Fact]
    public void Constructor_ShouldInitializeTransfer()
    {
        var transfer = new Transfer("1234567890123456", "6543210987654321", 50);

        transfer.Amount.Should().Be(50);
        transfer.FromAccount.Value.Should().Be("1234567890123456");
        transfer.ToAccount.Value.Should().Be("6543210987654321");
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentException_WhenToAccountIsSameAsFromAccount()
    {
        Action construction = () => new Transfer("1234567890123456", "1234567890123456", 50);

        construction.Should().Throw<ArgumentException>()
            .WithParameterName("toAccount")
            .WithMessage("Cannot transfer to the same account.*");
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentNullException_WhenFromAccountIsNull()
    {
        Action construction = () => new Transfer(null!, "6543210987654321", 50);

        construction.Should().Throw<ArgumentNullException>()
            .WithParameterName("fromAccount");
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentNullException_WhenToAccountIsNull()
    {
        Action construction = () => new Transfer("1234567890123456", null!, 50);

        construction.Should().Throw<ArgumentNullException>()
            .WithParameterName("toAccount");
    }
}
