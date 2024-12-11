using System.Globalization;
using CsvHelper;
using ETL.Models;

namespace ETL.Services;

public class DataTransformer
{
    private readonly HashSet<string> _uniqueRecords = new();
    private readonly List<Trip> _duplicateRecords = new();

    public List<Trip> TransformData(IEnumerable<Trip> trips, string filePath)
    {
        var transformedData = new List<Trip>();

        foreach (var trip in trips)
        {
            var pickUpDate = ConvertToUtc(trip.PickupDate);
            var dropOffDate = ConvertToUtc(trip.DropOffDate);

            var recordKey = $"{pickUpDate}|{dropOffDate}|{trip.PassengerCount}";

            if (_uniqueRecords.Contains(recordKey))
            {
                _duplicateRecords.Add(trip);
                continue;
            }

            _uniqueRecords.Add(recordKey);

            transformedData.Add(new Trip
            {
                PickupDate = pickUpDate,
                DropOffDate = dropOffDate,
                PassengerCount = trip.PassengerCount,
                TripDistance = trip.TripDistance,
                StoreAndFwdFlag = trip.StoreAndFwdFlag?.Trim() == "Y" ? "Yes" : "No",
                PULocationID = trip.PULocationID,
                DOLocationID = trip.DOLocationID,
                FareAmount = trip.FareAmount,
                TipAmount = trip.TipAmount
            });
        }

        WriteDuplicatesToFile(filePath);
        return transformedData;
    }

    private void WriteDuplicatesToFile(string filePath)
    {
        using var writer = new StreamWriter(filePath);
        using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

        csv.WriteRecords(_duplicateRecords);
    }

    private static DateTime ConvertToUtc(DateTime estDate)
    {
        var estZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

        return TimeZoneInfo.ConvertTimeToUtc(estDate, estZone);
    }
}