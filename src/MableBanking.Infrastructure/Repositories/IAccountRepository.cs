using MableBanking.Domain.Models;

namespace MableBanking.Infrastructure.Repositories;

/// <summary>
/// Interface for account repository.
/// </summary>
public interface IAccountRepository
{
    /// <summary>
    /// Adds an account to the repository.
    /// </summary>
    /// <param name="account">The account to add.</param>
    void AddAccount(Account account);

    /// <summary>
    /// Gets an account by its account number.
    /// </summary>
    /// <param name="accountNumber">The account number.</param>
    /// <returns>The account with the specified account number.</returns>
    Account? GetAccount(AccountNumber accountNumber);

    /// <summary>
    /// Gets all accounts in the repository.
    /// </summary>
    /// <returns>An enumerable collection of all accounts.</returns>
    IEnumerable<Account> GetAllAccounts();

    /// <summary>
    /// Updates an account in the repository.
    /// </summary>
    /// <param name="account">The account to update.</param>
    void UpdateAccount(Account account);
}
