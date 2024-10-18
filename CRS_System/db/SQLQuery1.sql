CREATE TABLE Flights (
    FlightNumber VARCHAR(10) PRIMARY KEY,
    DepartureCity VARCHAR(50),
    DestinationCity VARCHAR(50),
    DepartureTime DATETIME,
    ArrivalTime DATETIME,
    Price DECIMAL(10, 2)
);

