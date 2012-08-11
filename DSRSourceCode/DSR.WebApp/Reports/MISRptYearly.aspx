<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MISRptYearly.aspx.cs" Inherits="DSR.WebApp.Reports.MISRptYearly"
    MasterPageFile="~/Site.Master" Title=":: Shipping :: Yearly MIS Report" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="Server">
<center>
    <div style="padding-top: 10px;">
        <fieldset style="width:964px;height:35px;">
        <table>
            <tr>
                <td class="label" style="padding-right:5px;vertical-align:top;">
                    Year:
                </td>
                <td style="padding-right:20px;vertical-align:top;">
                    <asp:DropDownList ID="ddlYear" runat="server"></asp:DropDownList>
                </td>
                <td class="label" style="padding-right:5px;vertical-align:top;">
                    Location:
                </td>
                <td style="padding-right:20px;vertical-align:top;">
                    <asp:DropDownList ID="ddlLoc" runat="server">
                        <asp:ListItem Value="0" Text="All"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="vertical-align:top;"><asp:Button ID="btnShow" runat="server" Text="Show" CssClass="button" OnClick="btnShow_Click" /></td>
            </tr>
        </table>
        </fieldset>
        <div style="padding-left:5px;width:980px;">
            <rsweb:ReportViewer ID="rptViewer" runat="server" Width="100%"></rsweb:ReportViewer>        
        </div>
    </div>
</center>
</asp:Content>
