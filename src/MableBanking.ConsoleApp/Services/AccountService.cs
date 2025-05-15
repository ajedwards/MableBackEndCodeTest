using MableBanking.Domain.Models;
using MableBanking.Domain.Services;
using MableBanking.Infrastructure.DataAccess;
using MableBanking.Infrastructure.Repositories;

namespace MableBanking.ConsoleApp.Services;

public class AccountService(IAccountRepository accountRepository, ICsvFileReader csvFileReader) : IAccountService
{
    private readonly IAccountRepository _accountRepository = accountRepository
        ?? throw new ArgumentNullException(nameof(accountRepository));

    private readonly ICsvFileReader _csvFileReader = csvFileReader
        ?? throw new ArgumentNullException(nameof(csvFileReader));

    public Account? GetAccount(AccountNumber accountNumber)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(accountNumber);

        return _accountRepository.GetAccount(accountNumber);
    }

    public IEnumerable<Account> GetAllAccounts()
    {
        return _accountRepository.GetAllAccounts();
    }

    public void LoadAccountsFromFile(string filePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);

        var accounts = _csvFileReader.ReadCsv<Account, AccountCsvMap>(filePath);
        foreach (var account in accounts)
        {
            _accountRepository.AddAccount(account);
        }
    }
}
