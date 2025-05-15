namespace MableBanking.Infrastructure.DataAccess;

public class FileSystemWrapper : IFileSystem
{
    public StreamReader StreamReader(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException("The value cannot be an empty string or composed entirely of whitespace.", nameof(filePath));
        }

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"The file '{filePath}' was not found.", filePath);
        }

        return new StreamReader(filePath);
    }
}
