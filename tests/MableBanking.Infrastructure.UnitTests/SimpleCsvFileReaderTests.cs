using System.Text;
using CsvHelper.Configuration;
using FluentAssertions;
using MableBanking.Infrastructure.DataAccess;
using NSubstitute;

namespace MableBanking.Infrastructure.UnitTests;

public class SimpleCsvFileReaderTests
{
    [Fact]
    public void ReadFile_ShouldReturnListOfRecords_WhenFileExists()
    {
        var filePath = "test.csv";
        var fileStream = new MemoryStream(Encoding.UTF8.GetBytes("1,2,3\n4,5,6"));

        var mockFileSystem = Substitute.For<IFileSystem>();
        mockFileSystem.StreamReader(filePath).Returns(new StreamReader(fileStream));

        var csvFileReader = new SimpleCsvFileReader(mockFileSystem);

        var result = csvFileReader.ReadCsv<CsvRow, CsvRowMap>(filePath);

        result.Should().NotBeNull();
        result.Should().ContainInOrder([
            new CsvRow { Column1 = "1", Column2 = "2", Column3 = "3" },
            new CsvRow { Column1 = "4", Column2 = "5", Column3 = "6" }
        ]);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void ReadFile_ShouldThrowArgumentException_WhenFilePathIsEmptyOrWhitespace(string filePath)
    {
        var mockFileSystem = Substitute.For<IFileSystem>();
        var csvFileReader = new SimpleCsvFileReader(mockFileSystem);

        Action readingEmptyFilePath = () => csvFileReader.ReadCsv<CsvRow, CsvRowMap>(filePath);

        readingEmptyFilePath.Should().Throw<ArgumentException>()
            .WithParameterName("filePath")
            .WithMessage("The value cannot be an empty string or composed entirely of whitespace.*");
    }

    [Fact]
    public void ReadFile_ShouldThrowArgumentNullException_WhenFilePathIsNull()
    {
        string? filePath = null;

        var mockFileSystem = Substitute.For<IFileSystem>();
        var csvFileReader = new SimpleCsvFileReader(mockFileSystem);

        Action readingNullFilePath = () => csvFileReader.ReadCsv<CsvRow, CsvRowMap>(filePath!);

        readingNullFilePath.Should().Throw<ArgumentNullException>()
            .WithParameterName("filePath");
    }

    [Fact]
    public void ReadFile_ShouldThrowFileNotFoundException_WhenFileDoesNotExist()
    {
        var filePath = "nonexistent.csv";

        var mockFileSystem = Substitute.For<IFileSystem>();
        mockFileSystem.StreamReader(filePath).Returns((StreamReader)null!);

        var csvFileReader = new SimpleCsvFileReader(mockFileSystem);

        Action readingNonExistentFile = () => csvFileReader.ReadCsv<CsvRow, CsvRowMap>(filePath);

        readingNonExistentFile.Should().Throw<FileNotFoundException>()
            .WithMessage($"File not found: {filePath}");
    }

    private record CsvRow
    {
        public string? Column1 { get; set; }
        public string? Column2 { get; set; }
        public string? Column3 { get; set; }
    }

    private class CsvRowMap : ClassMap<CsvRow>
    {
        public CsvRowMap()
        {
            Map(m => m.Column1).Index(0);
            Map(m => m.Column2).Index(1);
            Map(m => m.Column3).Index(2);
        }
    }
}
