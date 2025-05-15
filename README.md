# MableBackEndCodeTest

**Author:** Adrian Edwards (2025-05-15)

## Overview

Simple banking service built as a solution to Mable back end code test. See
`MABLE_BACK_END_CODE_TEST.md` file for details of the challenge.

All code is written in C# for [.NET 9.0](https://dotnet.microsoft.com/en-us/download/dotnet/9.0). Tests are written with [XUnit](https://xunit.net/).

The repository contains `.bat` scripts to easily build, run and test the solution.

## Repository Structure

```
MableBackEndCodeTest/
│
├── README.md       # This documentation file.
├── RUNME.bat       # Batch script to start/run the solution.
├── TESTME.bat      # Batch script to run tests on the solution.
├── /src
│   ├── /MableBanking.ConsoleApp        # Banking service executable
│   ├── /MableBanking.Domain            # Banking domain class library
│   └── /MableBanking.Infrastructure    # File access and persistence
└── /tests
    ├── /MableBanking.ConsoleApp.Specs          # Banking service specifications
    ├── /MableBanking.Domain.UnitTests          # Banking domain unit tests
    └── /MableBanking.Infrastructure.UnitTests  # File access and persistence unit tests
```

## Running the Solution

To execute the solution:

1. Open a command prompt.
2. Navigate to the top level `MableBackEndCodeTest` solution directory.
3. Run the startup batch script:
   ```
   RUNME.bat
   ```

## Testing the Solution

To execute tests:

1. Open a command prompt.
2. Navigate to the top level `MableBackEndCodeTest` solution directory.
3. Run the test batch script:
   `   TESTME.bat`
   Review the output to verify that all tests are passing.

## Additional Information

- There are a lot more edge cases that I would usually unit test, but hopefully the test coverage is reasonable in a code test context.

- [License](https://fluentassertions.com/LICENSE) warnings from the [FluentAssertions](https://fluentassertions.com/) NuGet package are suppressed.
  This code test qualifies as non-commercial use.
  I should probably have just used the [Shouldly](https://docs.shouldly.org/) package instead! 😅
