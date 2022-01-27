CREATE PROCEDURE Sp_Transaction
AS
SELECT tr.TransactionID AS "Transaction ID", ROW_NUMBER() OVER(PARTITION BY tr.TransactionID ORDER BY tr.TransactionID ASC) AS [No]
,ct.CategoryName AS "Item Category", it.ItemName AS "Item Name", it.ItemPrice AS "Item Price", ItemQty AS "Quantity", (ItemQty * ItemPrice) AS "Sub Total", br.BranchLocation AS "Branch Location", TransactionDate AS "Date", CashierName AS "Cashier Name"
FROM "TransactionDetail" td,
"Transaction" tr,
Item it,
Payment pa,
Cashier ca,
Category ct,
Branch br
WHERE br.BranchID = ca.BranchID AND td.TransactionNum = tr.TransactionNum AND td.ItemID = it.ItemID AND tr.CashierID = ca.CashierID AND tr.PaymentID = pa.PaymentID AND ct.CategoryID = it.CategoryID