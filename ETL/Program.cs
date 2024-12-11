using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using ETL.Models;
using ETL.Services;

var csvPath = "yourCsvPath.csv";
var duplicatesPath = "yourDuplicatesPath.csv";
var connectionString =
    "yourConnectionString";

var transformer = new DataTransformer();
var loader = new DataBaseLoader(connectionString);

Console.WriteLine("Extracting data...");
var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
{
    TrimOptions = TrimOptions.Trim, // Trim all text fields
    IgnoreBlankLines = true, // Ignore blank lines
};
using var streamReader = new StreamReader(csvPath);
using var csvReader = new CsvReader(streamReader, csvConfig);
csvReader.Context.RegisterClassMap<TripMap>();
var records = csvReader.GetRecords<Trip>().ToList();

Console.WriteLine("Transforming data...");
var transformedData = transformer.TransformData(records, duplicatesPath);

Console.WriteLine("Loading data to database...");
loader.BulkInsert(transformedData);

Console.WriteLine("ETL Process Completed!");