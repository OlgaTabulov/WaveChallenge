<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TimeSheetsUpload.aspx.cs" Inherits="Wave.TimeSheetsUpload" %>
<%@ Register Src="~/Controls/TimeSheetUpload.ascx" TagName="TimeSheetUpload" TagPrefix="uc" %>
<%@ Register Src="~/Controls/TimeSheetReport.ascx" TagName="TimeSheetReport" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="container-flex page-width " style="width: 600px;">
    <h1>Timesheets</h1>

    <uc:TimeSheetUpload ID="ucTimeSheetUpload" runat="server"/>
    <div class="vertical-spacer"> </div>
    <uc:TimeSheetReport ID="ucTimeSheetReport" runat="server"/>
        
</div>
</asp:Content>
