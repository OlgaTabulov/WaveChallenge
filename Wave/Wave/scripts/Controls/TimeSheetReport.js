var Wave = Wave || {};

$(document).ready(function () {
    
    DisplayMessage('', true);
    GetReport();

});

function GetReport() {
    var params = {};
    CallAjax(Wave.WebServicesPath + 'GetTimeSheetsReport', params, GetTimeSheetsReport_OnSuccess);

}

function GetTimeSheetsReport_OnSuccess(data, status) {
    if (status == 'success') {
        $("#reportDiv").html('');

        $(data).find("TimesheetReportItemView").each(function () {           
            var item = {
                EmployeeId: $(this).find("EmployeeId").text(),
                PayPeriod: $(this).find("PayPeriod").text(),
                AmountPaid: $(this).find("AmountPaid").text(),
            };
            $("#reportTemplate").tmpl(item).appendTo("#reportDiv");

        });
    }
    else {
        DisplayMessage(data, false);
    }
}