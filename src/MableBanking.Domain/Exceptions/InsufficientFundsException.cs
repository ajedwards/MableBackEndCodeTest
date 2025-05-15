namespace MableBanking.Domain.Exceptions;

/// <summary>
/// Exception thrown when an account has insufficient funds for a withdrawal.
/// </summary>
/// <remarks>
/// This exception is thrown when an attempt is made to withdraw an amount greater than the available balance.
/// It is a custom exception that inherits from <see cref="ApplicationException"/>.
/// </remarks>
[Serializable]
public class InsufficientFundsException(string? message) : ApplicationException(message);
