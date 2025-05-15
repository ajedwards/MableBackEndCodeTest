using MableBanking.Domain.Exceptions;

namespace MableBanking.Domain.Models;

/// <summary>
/// Represents a bank account.
/// </summary>
public class Account
{
    public AccountNumber AccountNumber { get; init; }

    public decimal Balance { get; private set; }

    public Account(string accountNumber, decimal balance)
    {
        ArgumentNullException.ThrowIfNull(accountNumber);
        // ArgumentOutOfRangeException.ThrowIfNegative(balance); // Probable requirement but not specified

        AccountNumber = accountNumber;
        Balance = balance;
    }

    public bool CanWithdraw(decimal amount)
    {
        return Balance >= amount;
    }

    public void Deposit(decimal amount)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(amount);

        Balance += amount;
    }

    public void Withdraw(decimal amount)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(amount);

        if (!CanWithdraw(amount))
        {
            throw new InsufficientFundsException(
                $"Account {AccountNumber} has insufficient funds to withdraw {amount}.");
        }

        Balance -= amount;
    }
}
