<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CallTypeWiseDailyRpt.aspx.cs" Inherits="DSR.WebApp.Reports.CallTypeWiseDailyRpt"
    MasterPageFile="~/Site.Master" Title=":: Shipping :: Call Type Wise Daily Report" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
        <fieldset style="width:964px;height:70px;">
        <table>
            <tr>
                <td class="label" style="padding-right:5px;vertical-align:top;">
                    From Date:<span class="errormessage">*</span>
                </td>
                <td style="padding-right:20px;vertical-align:top;">
                    <asp:TextBox ID="txtFromDt" runat="server" CssClass="textbox" Width="80"></asp:TextBox>
                    <cc2:CalendarExtender ID="cbeFromDt" runat="server" TargetControlID="txtFromDt" />
                </td>
                <td class="label" style="padding-right:5px;vertical-align:top;">
                    To Date:<span class="errormessage">*</span>
                </td>
                <td style="padding-right:20px;vertical-align:top;">
                    <asp:TextBox ID="txtToDt" runat="server" CssClass="textbox" Width="80"></asp:TextBox>
                    <cc2:CalendarExtender ID="cbeToDt" runat="server" TargetControlID="txtToDt" />
                </td>
                <td class="label" style="padding-right:5px;vertical-align:top;">
                    Location:
                </td>
                <td style="padding-right:20px;vertical-align:top;">
                    <asp:DropDownList ID="ddlLoc" runat="server"></asp:DropDownList>
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="label" style="padding-right:5px;vertical-align:top;">
                    Sales Person:
                </td>
                <td style="padding-right:20px;vertical-align:top;">
                    <asp:DropDownList ID="ddlSales" runat="server"></asp:DropDownList>
                </td>
                <td class="label" style="padding-right:5px;vertical-align:top;">
                    Call Type:
                </td>
                <td style="padding-right:20px;vertical-align:top;">
                    <asp:DropDownList ID="ddlType" runat="server"></asp:DropDownList>
                </td>
                <td colspan="2"></td>
                <td style="vertical-align:top;">
                    <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="button" OnClientClick="javascript:return validateData();" OnClick="btnShow_Click" />
                </td>
            </tr>
        </table>
        </fieldset>
        <div style="padding-left:5px;width:980px;">
            <rsweb:ReportViewer ID="rptViewer" runat="server" Width="100%"></rsweb:ReportViewer>        
        </div>
    </div>
</center>
</asp:Content>
