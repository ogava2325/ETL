using CsvHelper.Configuration;

namespace ETL.Models;

public class TripMap : ClassMap<Trip>
{
    public TripMap()
    {
        Map(m => m.PickupDate).Name("tpep_pickup_datetime")
            .TypeConverterOption.NullValues(string.Empty).Default(DateTime.MinValue);

        Map(m => m.DropOffDate).Name("tpep_dropoff_datetime")
            .TypeConverterOption.NullValues(string.Empty).Default(DateTime.MinValue);

        Map(m => m.PassengerCount).Name("passenger_count")
            .TypeConverterOption.NullValues(string.Empty).Default(0);

        Map(m => m.TripDistance).Name("trip_distance")
            .TypeConverterOption.NullValues(string.Empty).Default(0.0);

        Map(m => m.StoreAndFwdFlag).Name("store_and_fwd_flag")
            .TypeConverterOption.NullValues(string.Empty).Default("N");

        Map(m => m.PULocationID).Name("PULocationID")
            .TypeConverterOption.NullValues(string.Empty).Default(0);

        Map(m => m.DOLocationID).Name("DOLocationID")
            .TypeConverterOption.NullValues(string.Empty).Default(0);

        Map(m => m.FareAmount).Name("fare_amount")
            .TypeConverterOption.NullValues(string.Empty).Default(0m);

        Map(m => m.TipAmount).Name("tip_amount")
            .TypeConverterOption.NullValues(string.Empty).Default(0m);
    }
}