<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TimeSheetReport.ascx.cs" Inherits="Wave.Controls.TimeSheetReport" %>
<script type="text/javascript" src="../scripts/Controls/TimeSheetReport.js?time" ></script>


    <div>
        <div class="has-error" id="pageError"></div>
        <div class="" id="pageMessage"></div>
    </div>
    <div class="container-flex page-width ">
        <div id="headerDiv">
            <div class="row-flex">
                <div class="col-flex report-headers">Employee Id</div>
                <div class="col-flex report-headers">Pay Period</div>
                <div class="col-flex report-headers">Amount Paid</div>
            </div>
        </div>
        <div id="reportDiv">
        </div>
    </div>
<script type="text/x-jquery-tmpl" id="reportTemplate">
    <div class="row-flex">
        <div class="col-flex">${EmployeeId}</div>
        <div class="col-flex">${PayPeriod}</div>
        <div class="col-flex">${AmountPaid}</div>
    </div>
</script>