using FluentAssertions;
using MableBanking.Domain.Exceptions;
using MableBanking.Domain.Models;

namespace MableBanking.Domain.UnitTests;

public class AccountTests
{
    [Fact]
    public void Constructor_ShouldInitializeAccount()
    {
        var account = new Account("1234567890123456", 100);

        account.AccountNumber.Value.Should().Be("1234567890123456");
        account.Balance.Should().Be(100);
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentNullException_WhenAccountNumberIsNull()
    {
        Action construction = () => new Account(null!, 100);

        construction.Should().Throw<ArgumentNullException>()
            .WithParameterName("accountNumber");
    }

    [Fact]
    public void CanWithdraw_ShouldReturnTrue_WhenBalanceIsGreaterThanOrEqualToAmount()
    {
        var account = new Account("1234567890123456", 100); // Start with a balance of 100

        var canWithdraw = account.CanWithdraw(50); // 50 is less than 100

        canWithdraw.Should().BeTrue();
    }

    [Fact]
    public void CanWithdraw_ShouldReturnFalse_WhenBalanceIsLessThanAmount()
    {
        var account = new Account("1234567890123456", 100); // Start with a balance of 100

        var canWithdraw = account.CanWithdraw(150); // 150 is greater than 100

        canWithdraw.Should().BeFalse();
    }

    [Fact]
    public void Deposit_ShouldIncreaseBalanceByAmount()
    {
        // Arrange
        var account = new Account("1234567890123456", 100); // Start with a balance of 100

        // Act
        account.Deposit(50);

        // Assert
        account.Balance.Should().Be(150);
    }

    [Fact]
    public void Deposit_ShouldThrowArgumentOutOfRangeException_WhenAmountIsNegative()
    {
        var account = new Account("1234567890123456", 100); // Start with a balance of 100

        var deposit = () => account.Deposit(-50);

        deposit.Should().Throw<ArgumentOutOfRangeException>()
            .WithParameterName("amount")
            .WithMessage("amount ('-50') must be a non-negative value.*");
    }

    [Fact]
    public void Withdraw_ShouldDecreaseBalanceByAmount()
    {
        var account = new Account("1234567890123456", 100); // Start with a balance of 100

        account.Withdraw(50);

        account.Balance.Should().Be(50);
    }

    [Fact]
    public void Withdraw_ShouldThrowArgumentOutOfRangeException_WhenAmountIsNegative()
    {
        var account = new Account("1234567890123456", 50); // Start with a balance of 50

        var withdrawal = () => account.Withdraw(-10);

        withdrawal.Should().Throw<ArgumentOutOfRangeException>()
            .WithParameterName("amount")
            .WithMessage("amount ('-10') must be a non-negative value.*");
    }

    [Fact]
    public void Withdraw_ShouldThrowInsufficientFundsException_WhenAmountIsMoreThanBalance()
    {
        var account = new Account("1234567890123456", 40); // Start with a balance of 40

        var withdrawal = () => account.Withdraw(50); // 50 is greater than 40

        withdrawal.Should().Throw<InsufficientFundsException>()
            .WithMessage("Account 1234567890123456 has insufficient funds to withdraw 50.");
    }
}
