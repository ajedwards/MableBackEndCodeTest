using CsvHelper.Configuration;
using MableBanking.Domain.Models;

namespace MableBanking.ConsoleApp.Services;

public class AccountCsvMap : ClassMap<Account>
{
    public AccountCsvMap()
    {
        Parameter("accountNumber").Index(0);
        Parameter("balance").Index(1);
    }
}
