# ETL
This program extracts, transforms, and loads (ETL) trip data from a CSV file 
into a SQL Server database. It processes potentially large datasets, ensures data validation, removes duplicates, 
and optimizes data insertion for performance.


## Database Schema

The following SQL query is used to create the Trip table and necessary indexes for efficient data retrieval:


  ```sql
    CREATE TABLE Trip(
      Id INT IDENTITY(1,1) PRIMARY KEY, 
      tpep_pickup_datetime DATETIME NOT NULL,
      tpep_dropoff_datetime DATETIME NOT NULL, 
      passenger_count INT CHECK (passenger_count > 0), 
      trip_distance FLOAT CHECK (trip_distance >= 0), 
      store_and_fwd_flag NVARCHAR(3) NOT NULL CHECK (store_and_fwd_flag IN ('Yes', 'No')),
      PULocationID INT NOT NULL, 
      DOLocationID INT NOT NULL, 
      fare_amount DECIMAL(10, 2) CHECK (fare_amount >= 0),
      tip_amount DECIMAL(10, 2) CHECK (tip_amount >= 0)
    );

    CREATE INDEX idx_PULocationID ON Trip (PULocationID);
    CREATE INDEX idx_PULocationID_tip ON Trip (PULocationID, tip_amount);
    CREATE INDEX idx_trip_distance ON Trip (trip_distance DESC);
    CREATE INDEX idx_pickup_dropoff_time ON Trip (tpep_pickup_datetime, tpep_dropoff_datetime);
  ```

## Output
The program inserted 29,889 rows into the database after cleaning and removing duplicates.

## What to do with 10GB?
1. We can split csv file into smaller ones and process them one by one.
2. Parallel Processing could be used.




