using MableBanking.Domain.Models;

namespace MableBanking.Infrastructure.Repositories;

public class InMemoryAccountRepository : IAccountRepository
{
    private readonly Dictionary<AccountNumber, Account> _accounts = [];

    public void AddAccount(Account account)
    {
        ArgumentNullException.ThrowIfNull(account);

        if (_accounts.ContainsKey(account.AccountNumber))
            throw new ArgumentException($"Account {account.AccountNumber} already exists.", nameof(account));

        _accounts[account.AccountNumber] = account;
    }

    public Account? GetAccount(AccountNumber accountNumber)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(accountNumber);

        _accounts.TryGetValue(accountNumber, out var account);

        return account;
    }

    public IEnumerable<Account> GetAllAccounts()
    {
        return _accounts.Values;
    }

    public void UpdateAccount(Account account)
    {
        ArgumentNullException.ThrowIfNull(account);

        if (!_accounts.ContainsKey(account.AccountNumber))
            throw new ArgumentException($"Account {account.AccountNumber} does not exist.", nameof(account));

        _accounts[account.AccountNumber] = account;
    }
}
