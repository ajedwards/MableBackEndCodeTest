using FluentAssertions;
using MableBanking.Domain.Models;
using MableBanking.Infrastructure.Repositories;

namespace MableBanking.Infrastructure.UnitTests;

public class InMemoryAccountRepositoryTests
{
    [Fact]
    public void AddAccount_ShouldAddAccount()
    {
        var account = new Account("1234567890123456", 1000);

        var repository = new InMemoryAccountRepository();
        repository.AddAccount(account);

        repository.GetAccount("1234567890123456").Should().NotBeNull();
    }

    [Fact]
    public void AddAccount_ShouldThrowArgumentException_WhenAccountAlreadyExists()
    {
        var account = new Account("1234567890123456", 1000);

        var repository = new InMemoryAccountRepository();
        repository.AddAccount(account);

        Action addingAccountAgain = () => repository.AddAccount(account);

        addingAccountAgain.Should().Throw<ArgumentException>()
            .WithParameterName("account")
            .WithMessage("Account 1234567890123456 already exists.*");
    }

    [Fact]
    public void AddAccount_ShouldThrowArgumentNullException_WhenAccountIsNull()
    {
        var repository = new InMemoryAccountRepository();

        Action addingNullAccount = () => repository.AddAccount(null!);

        addingNullAccount.Should().Throw<ArgumentNullException>()
            .WithParameterName("account");
    }

    [Fact]
    public void GetAccount_ShouldReturnAccount_WhenAccountExists()
    {
        var account = new Account("1234567890123456", 1000);

        var repository = new InMemoryAccountRepository();

        repository.AddAccount(account);
        var result = repository.GetAccount("1234567890123456");

        result.Should().NotBeNull();
        result.Should().Be(account);
    }

    [Fact]
    public void GetAccount_ShouldReturnNull_WhenAccountDoesNotExist()
    {
        var repository = new InMemoryAccountRepository();

        var result = repository.GetAccount("1234567890123456");

        result.Should().BeNull();
    }

    [Fact]
    public void GetAllAccounts_ShouldReturnAllAccounts()
    {
        var account1 = new Account("1234567890123456", 1000);
        var account2 = new Account("6543210987654321", 2000);

        var repository = new InMemoryAccountRepository();

        repository.AddAccount(account1);
        repository.AddAccount(account2);
        var result = repository.GetAllAccounts();

        result.Should().HaveCount(2);
        result.Should().Contain(account1);
        result.Should().Contain(account2);
    }

    [Fact]
    public void UpdateAccount_ShouldUpdateExistingAccount()
    {
        var account = new Account("1234567890123456", 1000);

        var repository = new InMemoryAccountRepository();
        repository.AddAccount(account);

        account.Deposit(500);
        repository.UpdateAccount(account);

        var updatedAccount = repository.GetAccount("1234567890123456");
        updatedAccount.Should().NotBeNull();
        updatedAccount!.Balance.Should().Be(1500);
    }

    [Fact]
    public void UpdateAccount_ShouldThrowArgumentException_WhenAccountDoesNotExist()
    {
        var account = new Account("1234567890123456", 1000);

        var repository = new InMemoryAccountRepository();

        Action updatingUnknownAccount = () => repository.UpdateAccount(account);

        updatingUnknownAccount.Should().Throw<ArgumentException>()
            .WithParameterName("account")
            .WithMessage("Account 1234567890123456 does not exist.*");
    }
}
