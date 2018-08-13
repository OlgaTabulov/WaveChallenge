using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities.Entities;
using Entities.CommonEntities;
using System.Data.SqlClient;
using System.Data;

namespace DataAccess
{
    public class TimesheetDA
    {

        public static ResultObject GetReport()
        {
            var toReturn = new ResultObject();
            var parameters = new SqlParameter[0];
            var query = @"select EmployeeId, PayPeriodStart, sum(AmountPaid) as AmountPaid
                        from Timesheet
                        group by EmployeeId, PayPeriodStart
                        order by EmployeeId, PayPeriodStart";
            var sqlResult = DataAccess.SqlSelect<TimesheetReportItemSql>(query, parameters);
            return sqlResult;
        }

        public static ResultView InsertTimesheetBatch(List<TimesheetUploadItem> timesheetUploadList)
        {
            ResultView result = new ResultView() { Success = true };
            //insert one by one to catch exceptions
            foreach(var timesheet in timesheetUploadList)
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("EmployeeId", SqlDbType.Int) { Value = timesheet.EmployeeId });
                parameters.Add(new SqlParameter("TimeEntry", SqlDbType.DateTime) { Value = timesheet.TimeEntry });
                parameters.Add(new SqlParameter("JobGroup", SqlDbType.NVarChar) { Value = timesheet.JobGroup });
                parameters.Add(new SqlParameter("Hours", SqlDbType.Float) { Value = timesheet.Hours });
                parameters.Add(new SqlParameter("AmountPaid", SqlDbType.Decimal) { Value = timesheet.AmountPaid });
                parameters.Add(new SqlParameter("PayPeriodStart", SqlDbType.DateTime) { Value = timesheet.PayPeriodStart });
                parameters.Add(new SqlParameter("ReportId", SqlDbType.Int) { Value = timesheet.ReportId });
                parameters.Add(new SqlParameter("InFileLineNumber", SqlDbType.Int) { Value = timesheet.InFileLineNumber });
                
                var query = @"insert into Timesheet
                            (   [EmployeeId], 
                                [TimeEntry], 
                                [JobGroup], 
                                [Hours], 
                                [AmountPaid], 
                                [PayPeriodStart], 
                                [ReportId], 
                                [InFileLineNumber])
                            values
                            (   @EmployeeId, 
                                @TimeEntry, 
                                @JobGroup, 
                                @Hours, 
                                @AmountPaid, 
                                @PayPeriodStart, 
                                @ReportId, 
                                @InFileLineNumber)
";
                var sqlResult = DataAccess.SqlNonQuery(query, parameters.ToArray());
                if (!sqlResult.Success)
                {
                    result.Success = false;
                    result.Message += "<br/>ReportId: " + timesheet.ReportId + 
                        ", line number: " + timesheet.InFileLineNumber + sqlResult.Message;
                }
            }
            return result;
        }
    }
}
