using FluentAssertions;
using MableBanking.Domain.Models;

namespace MableBanking.Domain.UnitTests;

public class AccountNumberTests
{
    [Fact]
    public void Constructor_ShouldInitializeAccountNumber()
    {
        var accountNumber = new AccountNumber("1234567890123456");

        accountNumber.Value.Should().Be("1234567890123456");
    }

    [Theory]
    [InlineData("1234567890ABCDEF")]
    [InlineData("123456789012345")]
    [InlineData("123456789012345!")]
    [InlineData("12345678901234567")]
    public void Constructor_ShouldThrowArgumentException_WhenValueIsNot16Digits(string accountNumber)
    {
        Action construction = () => new AccountNumber(accountNumber);

        construction.Should().Throw<ArgumentException>()
            .WithParameterName("value")
            .WithMessage("Account number must be 16 digits.*");
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentNullException_WhenValueIsNull()
    {
        Action construction = () => new AccountNumber(null!);

        construction.Should().Throw<ArgumentNullException>()
            .WithParameterName("value");
    }
}
