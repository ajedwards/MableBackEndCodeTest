using System.Text;
using FluentAssertions;
using MableBanking.ConsoleApp.Services;
using MableBanking.Infrastructure.DataAccess;
using MableBanking.Infrastructure.Repositories;
using NSubstitute;

namespace MableBanking.ConsoleApp.Specs
{
    public class DescribeBankingService
    {
        const string AccountFilePath = "path/to/accounts/csv/file";
        const string TransferFilePath = "path/to/transfers/csv/file";

        [Fact]
        public void ItShouldLoadAccountsAndProcessTransfersCorrectly()
        {
            var accountRepository = new InMemoryAccountRepository();
            var fileSystem = GivenValidAccountAndTransferFiles();

            // Load accounts from the file
            var accountService = new AccountService(accountRepository, new SimpleCsvFileReader(fileSystem));
            accountService.LoadAccountsFromFile(AccountFilePath);

            // Process transfers from the file
            var transferService = new TransferService(accountRepository, new SimpleCsvFileReader(fileSystem));
            var results = transferService.ProcessTransfersFromFile(TransferFilePath);

            // Assert that the transfers were processed correctly
            results.Should().NotBeNull();
            results.Should().AllSatisfy(result => result.IsSuccess.Should().BeTrue());
        }

        [Fact]
        public void ItShouldCalculateFinalBalancesCorrectly()
        {
            var accountRepository = new InMemoryAccountRepository();
            var fileSystem = GivenValidAccountAndTransferFiles();

            // Load accounts from the file
            var accountService = new AccountService(accountRepository, new SimpleCsvFileReader(fileSystem));
            accountService.LoadAccountsFromFile(AccountFilePath);

            // Process transfers from the file
            var transferService = new TransferService(accountRepository, new SimpleCsvFileReader(fileSystem));
            transferService.ProcessTransfersFromFile(TransferFilePath);

            // Assert that the final balances are correct
            accountService.GetAccount("1111234522226789")!.Balance.Should().Be(4820.50M); // 5000 - 500 + 320.50
            accountService.GetAccount("1111234522221234")!.Balance.Should().Be(9974.40M); // 10000 - 25.60
            accountService.GetAccount("2222123433331212")!.Balance.Should().Be(1550.00M); // 550 + 1000
            accountService.GetAccount("1212343433335665")!.Balance.Should().Be(1725.60M); // 1200 + 500 + 25.60
            accountService.GetAccount("3212343433335755")!.Balance.Should().Be(48679.50M); // 50000 - 1000 - 320.50
        }

        [Fact]
        public void ItShouldHandleMixedSuccessfulAndFailedTransfers()
        {
            var accountRepository = new InMemoryAccountRepository();
            var fileSystem = GivenValidAccountFileAndInvalidTransferFile();

            // Load accounts from the file
            var accountService = new AccountService(accountRepository, new SimpleCsvFileReader(fileSystem));
            accountService.LoadAccountsFromFile(AccountFilePath);

            // Process transfers from the file
            var transferService = new TransferService(accountRepository, new SimpleCsvFileReader(fileSystem));
            var results = transferService.ProcessTransfersFromFile(TransferFilePath);

            // Assert that the transfers were processed correctly
            results.Should().NotBeNull();
            results.Should().HaveCount(4);
        }

        [Fact]
        public void ItShouldNotTransferMoneyIfItWillPutAccountBalanceBelowZero()
        {
            var accountRepository = new InMemoryAccountRepository();
            var fileSystem = GivenValidAccountFileAndInvalidTransferFile();

            // Load accounts from the file
            var accountService = new AccountService(accountRepository, new SimpleCsvFileReader(fileSystem));
            accountService.LoadAccountsFromFile(AccountFilePath);

            // Process transfers from the file
            var transferService = new TransferService(accountRepository, new SimpleCsvFileReader(fileSystem));
            var results = transferService.ProcessTransfersFromFile(TransferFilePath);

            // Assert that the transfers were processed correctly
            var failedTransfers = results.Where(result => !result.IsSuccess).ToList();
            failedTransfers.Should().HaveCount(1);
            failedTransfers[0].Message
                .Should().Be($"Account {failedTransfers[0].FromAccount} has insufficient funds to withdraw {failedTransfers[0].Amount}.");
        }

        private static IFileSystem GivenValidAccountFileAndInvalidTransferFile()
        {
            // Set up the test data for valid accounts and invalid transfers
            const string AccountFileContent =
                "1111234522226789,5000.00\n" +
                "1111234522221234,10000.00\n" +
                "2222123433331212,550.00\n" +
                "1212343433335665,1200.00\n" +
                "3212343433335755,50000.00\n";

            const string TransferFileContent =
                "1111234522226789,1212343433335665,500.00\n" +
                "3212343433335755,2222123433331212,1000.00\n" +
                "3212343433335755,1111234522226789,320.50\n" +
                "1111234522221234,1212343433335665,10001.00\n";

            // Create a stream for the account data
            var accountFileStream = new MemoryStream(Encoding.UTF8.GetBytes(AccountFileContent));
            var accountFileReader = new StreamReader(accountFileStream);

            // Create a stream for the transfer data
            var transferFileStream = new MemoryStream(Encoding.UTF8.GetBytes(TransferFileContent));
            var transferFileReader = new StreamReader(transferFileStream);

            // Mock the file system to return the temporary files
            var mockFileSystem = Substitute.For<IFileSystem>();
            mockFileSystem.StreamReader(AccountFilePath).Returns(accountFileReader);
            mockFileSystem.StreamReader(TransferFilePath).Returns(transferFileReader);

            return mockFileSystem;
        }

        private static IFileSystem GivenValidAccountAndTransferFiles()
        {
            // Set up the test data for valid accounts and transfers
            const string AccountFileContent =
                "1111234522226789,5000.00\n" +
                "1111234522221234,10000.00\n" +
                "2222123433331212,550.00\n" +
                "1212343433335665,1200.00\n" +
                "3212343433335755,50000.00\n";

            const string TransferFileContent =
                "1111234522226789,1212343433335665,500.00\n" +
                "3212343433335755,2222123433331212,1000.00\n" +
                "3212343433335755,1111234522226789,320.50\n" +
                "1111234522221234,1212343433335665,25.60\n";

            // Create a stream for the account data
            var accountFileStream = new MemoryStream(Encoding.UTF8.GetBytes(AccountFileContent));
            var accountFileReader = new StreamReader(accountFileStream);

            // Create a stream for the transfer data
            var transferFileStream = new MemoryStream(Encoding.UTF8.GetBytes(TransferFileContent));
            var transferFileReader = new StreamReader(transferFileStream);

            // Mock the file system to return the temporary files
            var mockFileSystem = Substitute.For<IFileSystem>();
            mockFileSystem.StreamReader(AccountFilePath).Returns(accountFileReader);
            mockFileSystem.StreamReader(TransferFilePath).Returns(transferFileReader);

            return mockFileSystem;
        }
    }
}
