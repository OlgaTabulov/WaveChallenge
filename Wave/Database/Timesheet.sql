CREATE TABLE [dbo].[Timesheet]
(
	[TimesheetId] INT NOT NULL PRIMARY KEY, 
    [EmployeeId] INT NOT NULL, 
    [TimeEntry] DATETIME NOT NULL, 
    [JobGroup] NVARCHAR NOT NULL, 
    [Hours] INT NOT NULL, 
    [AmountPaid] DECIMAL NOT NULL, 
    [PayPeriodStart] DATETIME NOT NULL, 
    [ReportId] INT NOT NULL, 
    [BatchId] INT NULL, 
    [ImportedDate] TIMESTAMP NOT NULL
)
