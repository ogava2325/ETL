using System.Data;
using ETL.Models;
using Microsoft.Data.SqlClient;

namespace ETL.Services;

public class DataBaseLoader(string connectionString)
{
    public void BulkInsert(List<Trip> trips)
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();

        using var bulkCopy = new SqlBulkCopy(connection);
        bulkCopy.DestinationTableName = "Trip";

        var dataTable = CreateTripDataTable(trips);
        bulkCopy.WriteToServer(dataTable);
    }

    private static DataTable CreateTripDataTable(List<Trip> trips)
    {
        var table = new DataTable();
        table.Columns.Add("Id", typeof(int));
        table.Columns.Add("tpep_pickup_datetime", typeof(DateTime));
        table.Columns.Add("tpep_dropoff_datetime", typeof(DateTime));
        table.Columns.Add("passenger_count", typeof(int));
        table.Columns.Add("trip_distance", typeof(double));
        table.Columns.Add("store_and_fwd_flag", typeof(string));
        table.Columns.Add("PULocationID", typeof(int));
        table.Columns.Add("DOLocationID", typeof(int));
        table.Columns.Add("fare_amount", typeof(decimal));
        table.Columns.Add("tip_amount", typeof(decimal));

        foreach (var trip in trips)
        {
            table.Rows.Add(trip.Id, trip.PickupDate, trip.DropOffDate, trip.PassengerCount, trip.TripDistance,
                trip.StoreAndFwdFlag, trip.PULocationID, trip.DOLocationID, trip.FareAmount, trip.TipAmount);
        }

        return table;
    }
}