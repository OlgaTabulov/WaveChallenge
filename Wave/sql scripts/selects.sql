select * from Timesheet

select EmployeeId, PayPeriodStart, sum(AmountPaid) as AmountPaid
from Timesheet
group by EmployeeId, PayPeriodStart
order by EmployeeId, PayPeriodStart

--delete from Timesheet