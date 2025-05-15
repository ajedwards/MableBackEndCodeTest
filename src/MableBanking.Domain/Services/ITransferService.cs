using MableBanking.Domain.Models;

namespace MableBanking.Domain.Services;

/// <summary>
/// Interface for transfer-related operations.
/// </summary>
public interface ITransferService
{
    /// <summary>
    /// Processes a single transfer between two accounts.
    /// </summary>
    /// <param name="transfer">The transfer details.</param>
    /// <returns>The result of the transfer operation.</returns>
    TransferResult ProcessTransfer(Transfer transfer);

    /// <summary>
    /// Processes a batch of transfers between accounts.
    /// </summary>
    /// <param name="filePath">The path to the file containing transfer data.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    IEnumerable<TransferResult> ProcessTransfersFromFile(string filePath);
}
