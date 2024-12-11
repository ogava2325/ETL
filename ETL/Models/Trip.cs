namespace ETL.Models;

public class Trip
{
    public int Id { get; set; }
    public DateTime PickupDate { get; set; }
    public DateTime DropOffDate { get; set; }
    public int PassengerCount { get; set; }
    public double TripDistance { get; set; }
    public string StoreAndFwdFlag { get; set; }
    public int PULocationID { get; set; }
    public int DOLocationID { get; set; }
    public decimal FareAmount { get; set; }
    public decimal TipAmount { get; set; }
}