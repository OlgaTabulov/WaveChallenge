using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class TimesheetReportItemSql
    {
        public int EmployeeId { get; set; }
        public DateTime PayPeriodStart { get; set; }
        public Decimal AmountPaid { get; set; }
    }
}
