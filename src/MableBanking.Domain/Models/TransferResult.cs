namespace MableBanking.Domain.Models;

public record TransferResult
{
    public decimal Amount => Transfer.Amount;

    public AccountNumber FromAccount => Transfer.FromAccount;

    public bool IsSuccess { get; init; }

    public string? Message { get; init; }

    public AccountNumber ToAccount => Transfer.ToAccount;

    public Transfer Transfer { get; init; }

    private TransferResult(Transfer transfer, bool isSuccess, string? message = null)
    {
        ArgumentNullException.ThrowIfNull(transfer);

        IsSuccess = isSuccess;
        Message = message;
        Transfer = transfer;
    }

    public static TransferResult Failure(Transfer transfer, string message)
    {
        return new TransferResult(transfer, false, message);
    }

    public static TransferResult Success(Transfer transfer)
    {
        return new TransferResult(transfer, true);
    }
}
