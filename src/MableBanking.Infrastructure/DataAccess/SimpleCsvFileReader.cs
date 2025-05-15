using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace MableBanking.Infrastructure.DataAccess;

public class SimpleCsvFileReader(IFileSystem fileSystem) : ICsvFileReader
{
    private readonly IFileSystem _fileSystem = fileSystem
        ?? throw new ArgumentNullException(nameof(fileSystem));

    public IEnumerable<TRow> ReadCsv<TRow, TMap>(string filePath)
        where TRow : class
        where TMap : ClassMap<TRow>
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);

        using var fileStreamReader = _fileSystem.StreamReader(filePath)
            ?? throw new FileNotFoundException($"File not found: {filePath}");

        using var csvReader = new CsvReader(
            fileStreamReader,
            new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
                HasHeaderRecord = false,
            });

        csvReader.Context.RegisterClassMap<TMap>();

        return csvReader.GetRecords<TRow>().ToList();
    }
}
