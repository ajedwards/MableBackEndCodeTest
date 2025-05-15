using MableBanking.Domain.Exceptions;
using MableBanking.Domain.Models;
using MableBanking.Domain.Services;
using MableBanking.Infrastructure.DataAccess;
using MableBanking.Infrastructure.Repositories;

namespace MableBanking.ConsoleApp.Services;

public class TransferService(IAccountRepository accountRepository, ICsvFileReader csvFileReader) : ITransferService
{
    private readonly IAccountRepository _accountRepository = accountRepository
        ?? throw new ArgumentNullException(nameof(accountRepository));

    private readonly ICsvFileReader _csvFileReader = csvFileReader
        ?? throw new ArgumentNullException(nameof(csvFileReader));

    public TransferResult ProcessTransfer(Transfer transfer)
    {
        ArgumentNullException.ThrowIfNull(transfer);
        ArgumentException.ThrowIfNullOrWhiteSpace(transfer.FromAccount);
        ArgumentException.ThrowIfNullOrWhiteSpace(transfer.ToAccount);

        try
        {
            // Get the source account from the repository
            var fromAccount = _accountRepository.GetAccount(transfer.FromAccount);
            if (fromAccount == null)
            {
                return TransferResult.Failure(transfer, $"From account {transfer.FromAccount} does not exist.");
            }

            // Get the destination account from the repository
            var toAccount = _accountRepository.GetAccount(transfer.ToAccount);
            if (toAccount == null)
            {
                return TransferResult.Failure(transfer, $"To account {transfer.ToAccount} does not exist.");
            }

            // Transfer the funds
            fromAccount.Withdraw(transfer.Amount);
            toAccount.Deposit(transfer.Amount);

            // Update the accounts in the repository
            _accountRepository.UpdateAccount(fromAccount);
            _accountRepository.UpdateAccount(toAccount);

            return TransferResult.Success(transfer);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            return TransferResult.Failure(transfer, $"Amount out of range: {ex.Message}");

        }
        catch (InsufficientFundsException ex)
        {
            return TransferResult.Failure(transfer, ex.Message);

        }
        catch (Exception ex)
        {
            return TransferResult.Failure(transfer, $"An error occurred: {ex.Message}");
        }
    }

    public IEnumerable<TransferResult> ProcessTransfersFromFile(string filePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);

        var transferResults = new List<TransferResult>();

        // Read transfers from the CSV file
        var transfers = _csvFileReader.ReadCsv<Transfer, TransferCsvMap>(filePath);

        // Process each transfer
        return transfers.Select(ProcessTransfer).ToList();
    }
}
