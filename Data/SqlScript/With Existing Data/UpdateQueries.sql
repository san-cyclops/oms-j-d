UPDATE sl SET sl.customertype = td.customertype from dbo.InvSales sl
INNER JOIN dbo.TransactionDet td ON sl.SalesID = td.TransactionDetID;

UPDATE em SET em.employeecode = ltrim(rtrim(em.employeecode)), em.employeename = ltrim(rtrim(em.employeename)) 
from dbo.Employee em


UPDATE sl SET sl.createduser = em.journalname
 FROM dbo.InvSales sl
 INNER JOIN dbo.TransactionDet td ON sl.SalesID = td.TransactionDetID
INNER JOIN CashierPermission em ON em.cashierid = td.CashierID

WHERE sl.IsBackOffice = 0;

UPDATE sl SET sl.modifieduser = em.journalname
 FROM dbo.InvSales sl
 INNER JOIN dbo.TransactionDet td ON sl.SalesID = td.TransactionDetID
INNER JOIN CashierPermission em ON em.cashierid = td.CashierID


WHERE sl.IsBackOffice = 0;