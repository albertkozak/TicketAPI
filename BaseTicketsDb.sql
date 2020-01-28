CREATE TABLE Venue(
	[venue_name] [varchar](20) PRIMARY KEY,
	[capacity] [int] NULL
	)
	GO
	
CREATE TABLE Section(
	[section_id] int IDENTITY PRIMARY KEY,
	[section_name] [varchar](20) NOT NULL,
	[venue_name] [varchar](20)
	)
GO
ALTER TABLE Section ADD FOREIGN KEY([venue_name])
REFERENCES Venue ([venue_name])
GO
CREATE TABLE [Row](
	[row_id] int IDENTITY PRIMARY KEY,
	[row_name] [varchar](20) NOT NULL,
	[section_id] int
	)
GO
ALTER TABLE Row ADD FOREIGN KEY([section_id])
REFERENCES Section ([section_id])
GO
CREATE TABLE Seat(
	[seat_id] int IDENTITY PRIMARY KEY,
	[price] MONEY,
	[row_id] int,
	)
	GO
	ALTER TABLE Seat ADD FOREIGN KEY([row_id])
REFERENCES [Row] ([row_id])
GO

CREATE TABLE [Event](
	[event_id] int IDENTITY PRIMARY KEY,
	[event_name] varchar(20) NOT NULL,
	[venue_name] [varchar](20),
	)
	GO
	ALTER TABLE Event ADD FOREIGN KEY([venue_name])
REFERENCES [Venue] ([venue_name])
GO

CREATE Table EventSeat(
[event_seat_id] int IDENTITY PRIMARY KEY,
[seat_id] int NOT NULL,
[event_id] int NOT NULL,
[event_seat_price] MONEY
)
GO
ALTER TABLE EventSeat ADD FOREIGN KEY([seat_id]) REFERENCES [Seat]([seat_id])
ALTER TABLE EventSeat ADD FOREIGN KEY([event_id]) REFERENCES [Event]([event_id])
GO

CREATE Table TicketPurchase(
[purchase_id] int NOT NULL PRIMARY KEY,
[payment_method] varchar(20) NOT NULL,
[payment_amount] MONEY NOT NULL,
[confirmation_code] varchar(20)
)
GO

CREATE Table TicketPurchaseSeat(
[purchase_id] int NOT NULL,
[event_seat_id] int NOT NULL,
[seat_subtotal] MONEY
)
GO
ALTER TABLE TicketPurchaseSeat ADD FOREIGN KEY([purchase_id]) REFERENCES [TicketPurchase]([purchase_id])
ALTER TABLE TicketPurchaseSeat ADD FOREIGN KEY([event_seat_id]) REFERENCES [EventSeat]([event_seat_id])
ALTER TABLE TicketPurchaseSeat ADD PRIMARY KEY([event_seat_id], [purchase_id])

GO
