namespace MableBanking.Domain.Models;

/// <summary>
/// Represents a transfer of funds between two accounts.
/// </summary>
public record Transfer
{
    public decimal Amount { get; init; }

    public AccountNumber FromAccount { get; init; }

    public AccountNumber ToAccount { get; init; }

    public Transfer(string fromAccount, string toAccount, decimal amount)
    {
        ArgumentNullException.ThrowIfNull(fromAccount);
        ArgumentNullException.ThrowIfNull(toAccount);
        // ArgumentOutOfRangeException.ThrowIfNegative(amount); // Probable requirement but not specified

        if (fromAccount == toAccount)
        {
            throw new ArgumentException("Cannot transfer to the same account.", nameof(toAccount));
        }

        Amount = amount;
        FromAccount = fromAccount;
        ToAccount = toAccount;
    }
}
