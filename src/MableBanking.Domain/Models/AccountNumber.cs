namespace MableBanking.Domain.Models;

/// <summary>
/// Represents an account number.
/// </summary>
public record AccountNumber
{
    public static implicit operator string(AccountNumber accountNumber) => accountNumber.Value;

    public static implicit operator AccountNumber(string value) => new(value);

    public string Value { get; init; }

    public AccountNumber(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);

        if (!IsValidAccountNumber(value))
        {
            throw new ArgumentException("Account number must be 16 digits.", nameof(value));
        }

        Value = value;
    }

    public override string ToString() => Value;

    private static bool IsValidAccountNumber(string value)
    {
        return value.Length == 16 && value.All(char.IsDigit);
    }
}
