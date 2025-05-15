@echo off

REM This script is used to test the MableBanking service.
REM It builds the project and then runs the unit tests and behaviour specificiations.

dotnet test .\tests\MableBanking.Domain.UnitTests\
dotnet test .\tests\MableBanking.Infrastructure.UnitTests\
dotnet test .\tests\MableBanking.ConsoleApp.Specs\
