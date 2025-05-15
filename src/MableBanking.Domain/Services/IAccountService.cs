using MableBanking.Domain.Models;

namespace MableBanking.Domain.Services;

/// <summary>
/// Interface for account-related operations.
/// </summary>
public interface IAccountService
{
    /// <summary>
    /// Gets the details of a specific account.
    /// </summary>
    /// <param name="accountNumber">The account number.</param>
    /// <returns>The account with the specified account number, or null if not found.</returns>
    Account? GetAccount(AccountNumber accountNumber);

    /// <summary>
    /// Gets the details of all accounts.
    /// </summary>
    /// <returns>An enumerable collection of all accounts.</returns>
    IEnumerable<Account> GetAllAccounts();

    /// <summary>
    /// Loads account data from a file.
    /// </summary>
    /// <param name="filePath">The path to the file containing account data.</param>
    /// <remarks>
    /// This method reads account data from the specified file and populates the account list.
    /// The file should be in a format that can be parsed into <see cref="Account"/> objects.
    /// </remarks>
    /// <returns>A task representing the asynchronous operation.</returns>
    void LoadAccountsFromFile(string filePath);
}
