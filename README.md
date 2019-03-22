# Name List and Quicksort Homework Solution

C# console application which reads name/ID pairs from a CSV file, sorts them in ascending or descending order, and prints the sorted list.

It makes use of the Quicksort sorting algorithm to sort the pairs.

## Running

You can run the program from the command line using the following commands from the root directory:

```
cd NameList
dotnet restore
dotnet run

# sort the provided sample CSV file in ascending order
dotnet run ../names.csv

# or in descending order
dotnet run -d ../names.csv
```

## Testing

You can run unit tests by running the following commands from the root directory:

```
cd NameList.Tests
dotnet restore
dotnet test
```
