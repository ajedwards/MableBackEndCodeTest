using Microsoft.Extensions.DependencyInjection;
using MableBanking.Domain.Services;
using MableBanking.Infrastructure.DataAccess;
using MableBanking.Infrastructure.Repositories;
using MableBanking.ConsoleApp.Services;

internal static class DependencyInjection
{
    public static ServiceCollection AddMableBanking(this ServiceCollection services)
    {
        services
            .AddSingleton<IAccountRepository, InMemoryAccountRepository>()
            .AddScoped<IAccountService, AccountService>()
            .AddScoped<ICsvFileReader, SimpleCsvFileReader>()
            .AddScoped<IFileSystem, FileSystemWrapper>()
            .AddScoped<ITransferService, TransferService>();

        return services;
    }
}
