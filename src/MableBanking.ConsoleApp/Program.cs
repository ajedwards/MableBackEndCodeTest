using MableBanking.Domain.Services;

namespace Microsoft.Extensions.DependencyInjection;

class Program
{
    static void Main(string[] args)
    {
        // Set up dependency injection
        var services = new ServiceCollection()
            .AddMableBanking();

        var serviceProvider = services.BuildServiceProvider();

        // Resolve services
        var accountService = serviceProvider.GetRequiredService<IAccountService>();
        var transferService = serviceProvider.GetRequiredService<ITransferService>();

        string accountsFilePath = args.Length > 0 ? args[0] : "../../data/mable_account_balance.csv";
        string transfersFilePath = args.Length > 1 ? args[1] : "../../data/mable_transfer.csv";

        // Load accounts
        Console.WriteLine($"Loading accounts from {accountsFilePath}...");
        accountService.LoadAccountsFromFile(accountsFilePath);

        var accounts = accountService.GetAllAccounts();
        Console.WriteLine($"- Loaded {accounts.Count()} accounts.\n");

        // Process transfers
        Console.WriteLine($"Processing transfers from {transfersFilePath}...");
        var results = transferService.ProcessTransfersFromFile(transfersFilePath);

        // Display transfer results
        foreach (var result in results)
        {
            Console.WriteLine($"- Transfer from {result.FromAccount} to {result.ToAccount}: {result.Message ?? "Succeeded"}");
        }

        // Display final balances
        Console.WriteLine("\nFinal account balances:");
        foreach (var account in accounts)
        {
            Console.WriteLine($"- Account {account.AccountNumber} has a balance of {account.Balance}");
        }
    }
}
