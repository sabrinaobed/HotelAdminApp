USE HotelAdminDB
GO


--Join bookings with customers
SELECT 
Bookings.BookingId,
Customers.Name AS CustomerName,
Customers.Email,
Customers.PhoneNumber,
Bookings.StartDate,
Bookings.EndDate,
Bookings.NumberOfGuests
FROM Bookings
FULL OUTER JOIN Customers ON Bookings.CustomerId = Customers.CustomerId

--Join bookings with invoices
SELECT 
    b.BookingId,
    i.TotalAmount,
    i.IsPaid
FROM Bookings b
INNER JOIN Invoices i ON b.BookingId = i.BookingId;


-- Show all bookings with customer name, room number, and total amount,startDate and endDate
SELECT * FROM Bookings
SELECT * FROM Customers
SELECT * FROM Rooms
SELECT * FROM Invoices

SELECT
Bookings.BookingId,
Customers.Name AS CustomerName,
Rooms.RoomNumber,
Invoices.TotalAmount,
Bookings.StartDate,
Bookings.EndDate
FROM Bookings
INNER JOIN Customers ON Bookings.CustomerId = Customers.CustomerId
INNER JOIN Rooms ON Bookings.RoomId = Rooms.RoomId
INNER JOIN Invoices ON Bookings.BookingId = Invoices.BookingId

--List only paid bookings with customer and invoice info
SELECT  *  FROM  Bookings
SELECT * FROM Customers
SELECT * FROM Invoices

SELECT 
Bookings.BookingId,
Customers.Name AS CustomerName,
Rooms.RoomType,
Invoices.TotalAmount,
Invoices.IsPaid
FROM Bookings
INNER JOIN Customers ON Bookings.CustomerId = Customers.CustomerId
INNER JOIN Rooms ON Bookings.RoomId = Rooms.RoomId
INNER JOIN Invoices ON Bookings.BookingId = Invoices.BookingId
WHERE Invoices.IsPaid = 1


--Join all tables rooms,customers,bookings and invoices 
--FULL OUER JOIN: Returns all records from all tables, with NULLs where no match exists.
SELECT  *  FROM  Bookings
SELECT * FROM Customers
SELECT * FROM Invoices
SELECT * FROM Rooms

SELECT
Customers.CustomerId,
Customers.Name AS CustomerName,
Customers.Email,
Customers.PhoneNumber,

Bookings.BookingId,
Bookings.CustomerId,
Bookings.StartDate,
Bookings.EndDate,
Bookings.NumberOfGuests,

Rooms.RoomId,
Rooms.RoomNumber,
Rooms.RoomType,
Rooms.Capacity,
Rooms.PricePerNight,
Rooms.ExtraBeds,

Invoices.InvoiceId,
Invoices.TotalAmount,
Invoices.IsPaid,
Invoices.DueDate

FROM BOOKINGS
FULL OUTER JOIN Customers ON Bookings.CustomerId = Customers.CustomerId
FULL OUTER JOIN Invoices ON Bookings.BookingId = Invoices.BookingId
FULL OUTER JOIN Rooms ON Bookings.RoomId = Rooms.RoomId

--JOINING ALL TABBLES NOW WITH INNER JOIN: Returns only rows where there is a matching record in all joined tables.
SELECT  *  FROM  Bookings
SELECT * FROM Customers
SELECT * FROM Invoices
SELECT * FROM Rooms

SELECT
Customers.CustomerId,
Customers.Name AS CustomerName,
Customers.Email,
Customers.PhoneNumber,

Bookings.BookingId,
Bookings.CustomerId,
Bookings.StartDate,
Bookings.EndDate,
Bookings.NumberOfGuests,

Rooms.RoomId,
Rooms.RoomNumber,
Rooms.RoomType,
Rooms.Capacity,
Rooms.PricePerNight,
Rooms.ExtraBeds,

Invoices.InvoiceId,
Invoices.TotalAmount,
Invoices.IsPaid,
Invoices.DueDate

FROM BOOKINGS
INNER JOIN Customers ON Bookings.CustomerId = Customers.CustomerId
INNER JOIN Invoices ON Bookings.BookingId = Invoices.BookingId
INNER JOIN Rooms ON Bookings.RoomId = Rooms.RoomId

--LEFT JOIN: Returns all bookings, and includes matching customer, room, and invoice details if they exist.
SELECT  *  FROM  Bookings
SELECT * FROM Customers
SELECT * FROM Invoices
SELECT * FROM Rooms

SELECT
Customers.CustomerId,
Customers.Name AS CustomerName,
Customers.Email,
Customers.PhoneNumber,

Bookings.BookingId,
Bookings.CustomerId,
Bookings.StartDate,
Bookings.EndDate,
Bookings.NumberOfGuests,

Rooms.RoomId,
Rooms.RoomNumber,
Rooms.RoomType,
Rooms.Capacity,
Rooms.PricePerNight,
Rooms.ExtraBeds,

Invoices.InvoiceId,
Invoices.TotalAmount,
Invoices.IsPaid,
Invoices.DueDate

FROM BOOKINGS
LEFT JOIN Customers ON Bookings.CustomerId = Customers.CustomerId
LEFT JOIN Invoices ON Bookings.BookingId = Invoices.BookingId
LEFT JOIN Rooms ON Bookings.RoomId = Rooms.RoomId


--RIGHT JOIN: Returns all customers, invoices, and rooms, even if they have no related booking, and includes booking info only when matched.
SELECT  *  FROM  Bookings
SELECT * FROM Customers
SELECT * FROM Invoices
SELECT * FROM Rooms

SELECT
Customers.CustomerId,
Customers.Name AS CustomerName,
Customers.Email,
Customers.PhoneNumber,

Bookings.BookingId,
Bookings.CustomerId,
Bookings.StartDate,
Bookings.EndDate,
Bookings.NumberOfGuests,

Rooms.RoomId,
Rooms.RoomNumber,
Rooms.RoomType,
Rooms.Capacity,
Rooms.PricePerNight,
Rooms.ExtraBeds,

Invoices.InvoiceId,
Invoices.TotalAmount,
Invoices.IsPaid,
Invoices.DueDate

FROM BOOKINGS
RIGHT JOIN Customers ON Bookings.CustomerId = Customers.CustomerId
RIGHT JOIN Invoices ON Bookings.BookingId = Invoices.BookingId
RIGHT JOIN Rooms ON Bookings.RoomId = Rooms.RoomId






