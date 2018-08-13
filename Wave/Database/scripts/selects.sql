select * from Timesheet

select EmployeeId, PayPeriodStart, sum(AmountPaid) as AmountPaid
from Timesheet
group by EmployeeId, PayPeriodStart