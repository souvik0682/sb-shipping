<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LocWiseWiseLineSummary.aspx.cs" Inherits="DSR.WebApp.Reports.LocWiseWiseLineSummary"
    MasterPageFile="~/Site.Master" Title=":: Shipping :: Location Wise Line Summary" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Scripts/Common.js" type="text/javascript"></script>
    <link href="../Styles/DSR.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        function validateData() {
            if (document.getElementById('<%=txtFromDt.ClientID %>').value == '') {
                alert('Please enter from date');
                return false;
            }
            if (document.getElementById('<%=txtToDt.ClientID %>').value == '') {
                alert('Please enter to date');
                return false;
            }

            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="Server">
<center>
    <div style="padding-top: 10px;">
        <fieldset style="width:784px;height:65px;">
        <table>
            <tr>
                <td class="label" style="padding-right:5px;vertical-align:top;">
                    From Date:
                </td>
                <td style="padding-right:20px;vertical-align:top;">
                    <asp:TextBox ID="txtFromDt" runat="server" CssClass="textbox" Width="80"></asp:TextBox><br />
                    <cc2:CalendarExtender ID="cbeFromDt" runat="server" TargetControlID="txtFromDt" />                    
                </td>
                <td class="label" style="padding-right:5px;vertical-align:top;">
                    To Date:<span class="errormessage">*</span>
                </td>
                <td style="padding-right:20px;vertical-align:top;">
                    <asp:TextBox ID="txtToDt" runat="server" CssClass="textbox" Width="80"></asp:TextBox>
                    <cc2:CalendarExtender ID="cbeToDt" runat="server" TargetControlID="txtToDt" />
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="label" style="padding-right:5px;vertical-align:top;">
                    Location:
                </td>
                <td style="padding-right:20px;vertical-align:top;">
                    <asp:DropDownList ID="ddlLoc" runat="server"></asp:DropDownList>
                </td>            
                <td class="label" style="padding-right:5px;vertical-align:top;">
                    Prospect For:
                </td>
                <td style="padding-right:20px;vertical-align:top;">
                    <asp:DropDownList ID="ddlPros" runat="server"></asp:DropDownList>
                </td>
                <td style="vertical-align:top;"><asp:Button ID="btnShow" runat="server" Text="Show" CssClass="button" OnClientClick="javascript:return validateData();" OnClick="btnShow_Click" /></td>
            </tr>
        </table>
        </fieldset>
        <div style="padding-left:5px;width:800px;">
            <rsweb:ReportViewer ID="rptViewer" runat="server" Width="100%"></rsweb:ReportViewer>        
        </div>
    </div>
</center>
</asp:Content>
