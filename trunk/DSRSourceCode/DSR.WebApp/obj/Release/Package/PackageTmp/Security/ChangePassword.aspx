<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="DSR.WebApp.Security.ChangePassword" MasterPageFile="~/Site.Master" Title=":: DSR :: Change Password" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="Server">
    <div id="headercaption">CHANGE PASSWORD</div>
    <center>
        <fieldset style="width:400px;">
            <legend>Change Password</legend>
            <table border="0" cellpadding="3" cellspacing="3">
                <tr>
                    <td width="150" class="labelcaption">Old Password:<span class="errormessage">*</span></td>
                    <td>
                        <asp:TextBox ID="txtOldPwd" MaxLength="10" runat="server" ToolTip="Old Password" TextMode="Password" CssClass="textbox"></asp:TextBox><br />
                        <asp:RequiredFieldValidator ID="rfvOldPwd" runat="server" CssClass="errormessage" ControlToValidate="txtOldPwd" Display="Dynamic" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td width="150" class="labelcaption">New Password:<span class="errormessage">*</span></td>
                    <td>
                        <asp:TextBox ID="txtNewPwd" MaxLength="10" runat="server" ToolTip="New Password" TextMode="Password" CssClass="textbox"></asp:TextBox><br />
                        <asp:RequiredFieldValidator ID="rfvNewPwd" runat="server" CssClass="errormessage" ControlToValidate="txtNewPwd" Display="Dynamic" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                        <cc1:PasswordStrength ID="txtNewPwd_PasswordStrength" runat="server" Enabled="True" TargetControlID="txtNewPwd"></cc1:PasswordStrength>
                    </td>
                </tr>
                <tr>
                    <td width="150" class="labelcaption">Confirm Password:<span class="errormessage">*</span></td>
                    <td>
                        <asp:TextBox ID="txtRePwd" MaxLength="10" runat="server" ToolTip="Re-type Password" TextMode="Password" CssClass="textbox"></asp:TextBox><br />
                        <asp:RequiredFieldValidator ID="rfvRePwd" runat="server" CssClass="errormessage" ControlToValidate="txtRePwd" Display="Dynamic" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="cvRePwd" runat="server" ControlToCompare="txtNewPwd" ControlToValidate="txtRePwd" CssClass="errormessage" ValidationGroup="Submit"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="2"><asp:Button ID="btnChangePwd" runat="server" Text="Submit" OnClick="btnChangePwd_Click" ValidationGroup="Submit" /></td>
                </tr>
            </table>
        </fieldset>
    </center>
</asp:Content>