<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TimeSheetUpload.ascx.cs" Inherits="Wave.Controls.TimeSheetUpload" %>
<script type="text/javascript" src="../scripts/Controls/TimeSheetUpload.js" ></script>



    <div class="report-headers">
        Upload your report
    </div>
    <div>        
        <input type="file" id="reportFile" accept=".csv" />
        <input type="submit" value="Upload" onclick="return handleSubmit();" />         
    </div>

