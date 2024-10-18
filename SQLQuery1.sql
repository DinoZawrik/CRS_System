CREATE TABLE [dbo].[Flights] (
    [FlightNumber]    NVARCHAR (100)    NOT NULL,
    [DepartureCity]   NVARCHAR (100)    NULL,
    [DestinationCity] NVARCHAR (100)    NULL,
    [DepartureTime]   DATETIME        NULL,
    [ArrivalTime]     DATETIME        NULL,
    [Price]           DECIMAL (10, 2) NULL,
    PRIMARY KEY CLUSTERED ([FlightNumber] ASC)
);
