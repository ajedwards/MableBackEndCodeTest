@echo off

REM This script is used to run the MableBanking.ConsoleApp project.
REM It builds the project and then runs the executable with the specified data files.

dotnet build .\src\MableBanking.ConsoleApp\

.\src\MableBanking.ConsoleApp\bin\Debug\net9.0\MableBanking.ConsoleApp.exe .\data\mable_account_balances.csv .\data\mable_transactions.csv
