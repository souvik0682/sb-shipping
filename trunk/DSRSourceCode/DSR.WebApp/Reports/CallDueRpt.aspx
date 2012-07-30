<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CallDueRpt.aspx.cs" Inherits="DSR.WebApp.Reports.CallDueRpt"
    MasterPageFile="~/Site.Master" Title=":: Shipping :: Call Due Report" %>

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
        <fieldset style="width:784px;height:40px;">
        <table>
            <tr>
                <td class="label" style="padding-right:5px;vertical-align:top;">
                    From Date:<span class="errormessage">*</span>
                </td>
                <td style="padding-right:20px;vertical-align:top;">
                    <asp:TextBox ID="txtFromDt" runat="server" CssClass="textbox" Width="70"></asp:TextBox><br />
                    <cc2:maskededitextender id="MaskedEditExtender1" runat="server" mask="99/99/9999" masktype="Date" targetcontrolid="txtFromDt" culturename="en-GB"></cc2:maskededitextender>
                    <cc2:maskededitvalidator id="MaskedEditValidator1" runat="server" CssClass="errormessage" controlextender="MaskedEditExtender1" controltovalidate="txtFromDt" display="Dynamic" invalidvalueblurredmessage="*Invalid Date"></cc2:maskededitvalidator>
                </td>
                <td class="label" style="padding-right:5px;vertical-align:top;">
                    To Date:<span class="errormessage">*</span>
                </td>
                <td style="padding-right:20px;vertical-align:top;">
                    <asp:TextBox ID="txtToDt" runat="server" CssClass="textbox" Width="70"></asp:TextBox><br />
                    <cc2:maskededitextender id="MaskedEditExtender2" runat="server" mask="99/99/9999" masktype="Date" targetcontrolid="txtToDt" culturename="en-GB"></cc2:maskededitextender>
                    <cc2:maskededitvalidator id="MaskedEditValidator2" runat="server" CssClass="errormessage" controlextender="MaskedEditExtender1" controltovalidate="txtToDt" display="Dynamic" invalidvalueblurredmessage="*Invalid Date"></cc2:maskededitvalidator>
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
