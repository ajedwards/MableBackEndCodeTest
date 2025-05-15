using CsvHelper.Configuration;

namespace MableBanking.Infrastructure.DataAccess;

/// <summary>
/// Interface for reading CSV files.
/// </summary>
public interface ICsvFileReader
{
    /// <summary>
    /// Reads a CSV file and returns the data as a collection of rows.
    /// </summary>
    /// <typeparam name="TRow">The type of the rows in the CSV file.</typeparam>
    /// <typeparam name="TMap">The class map for the CSV file.</typeparam>
    /// <param name="filePath">The path to the CSV file.</param>
    /// <returns>
    /// An enumerable collection of rows of type <typeparamref name="TRow"/>.
    /// </returns>
    IEnumerable<TRow> ReadCsv<TRow, TMap>(string filePath)
        where TRow : class
        where TMap : ClassMap<TRow>;
}
