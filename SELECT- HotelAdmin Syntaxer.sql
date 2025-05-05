USE HotelAdminDB
GO
--Shows all rooms,customers,bookings and invoices
SELECT * FROM Rooms
SELECT * FROM Customers
SELECT  * FROM Bookings
SELECT * FROM Invoices

--Find room for 2 guests
SELECT * FROM Rooms
Where Capacity = 2

--Find paid invoices
SELECT * FROM Invoices
Where IsPaid > 0

--Find bookings between March and April
SELECT * FROM Bookings
WHERE StartDate >= '2025-03-01' AND EndDate <= '2025-04-30'

--Find customers whose name start with S
SELECT * FROM Customers
WHERE Name LIKE 'S%'

--Find available rooms between March and April
SELECT * FROM Rooms
WHERE RoomId NOT IN(
SELECT RoomId FROM Bookings
WHERE StartDate >= '2025-03-1' AND EndDate <= '2025-04-30')

--Show me sum of total paid invocies in this year
SELECT SUM(TotalAmount) AS TotalPaidInvoices
FROM Invoices
WHERE IsPaid = 1 AND YEAR(DueDate) = 2025




