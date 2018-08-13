drop table timesheet
go
CREATE TABLE [dbo].[Timesheet]
(
	[TimesheetId] INT NOT NULL PRIMARY KEY identity(1,1), 
    [EmployeeId] INT NOT NULL, 
    [TimeEntry] DATETIME NOT NULL, 
    [JobGroup] NVARCHAR NOT NULL, 
    [Hours] FLOAT NOT NULL, 
    [AmountPaid] DECIMAL NOT NULL, 
    [PayPeriodStart] DATETIME NOT NULL, 
    [ReportId] INT NOT NULL, 
    [InFileLineNumber] INT NULL, 
    [ImportedDate] TIMESTAMP NOT NULL
)
