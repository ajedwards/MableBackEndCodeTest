namespace MableBanking.Infrastructure.DataAccess;

/// <summary>
/// Interface for file system operations.
/// </summary>
/// <remarks>
/// This interface abstracts the file system operations, allowing for easier testing and mocking.
/// </remarks>
public interface IFileSystem
{
    /// <summary>
    /// Gets a <see cref="StreamReader"/> for the specified file path.
    /// </summary>
    /// <param name="filePath">The path to the file.</param>
    /// <returns>A <see cref="StreamReader"/> for the specified file path.</returns>
    StreamReader StreamReader(string filePath);
}
