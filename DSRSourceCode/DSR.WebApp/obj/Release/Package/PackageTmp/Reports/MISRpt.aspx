<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MISRpt.aspx.cs" Inherits="DSR.WebApp.Reports.MISRpt"
    MasterPageFile="~/Site.Master" Title=":: Shipping :: MIS Report List" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="Server">
<center>
    <div style="padding-top: 10px;">
        <fieldset style="width:964px;height:65px;">
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
                <td class="label" style="padding-right:5px;vertical-align:top;">Paremeter:</td>
                <td style="vertical-align:top;">
                    <asp:DropDownList ID="ddlParam" runat="server">
                        <asp:ListItem Value="A" Text="Actual"></asp:ListItem>
                        <asp:ListItem Value="C" Text="Commitment"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="label" style="padding-right:5px;vertical-align:top;">
                    Line:
                </td>
                <td style="padding-right:20px;vertical-align:top;">
                    <asp:DropDownList ID="ddlPros" runat="server"></asp:DropDownList>
                </td>
                <td class="label" style="padding-right:5px;vertical-align:top;">
                    Location:
                </td>
                <td style="padding-right:20px;vertical-align:top;">
                    <asp:DropDownList ID="ddlLoc" runat="server">
                        <asp:ListItem Value="0" Text="All"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td colspan="2" style="vertical-align:top;"><asp:Button ID="btnShow" runat="server" Text="Show" CssClass="button" OnClick="btnShow_Click" /></td>
            </tr>
        </table>
        </fieldset>
        <div style="padding-left:5px;width:980px;">
            <rsweb:ReportViewer ID="rptViewer" runat="server" Width="100%"></rsweb:ReportViewer>        
        </div>
    </div>
</center>
</asp:Content>
