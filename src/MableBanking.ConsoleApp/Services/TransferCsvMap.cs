using CsvHelper.Configuration;
using MableBanking.Domain.Models;

namespace MableBanking.ConsoleApp.Services;

public class TransferCsvMap : ClassMap<Transfer>
{
    public TransferCsvMap()
    {
        Parameter("fromAccount").Index(0);
        Parameter("toAccount").Index(1);
        Parameter("amount").Index(2);
    }
}
