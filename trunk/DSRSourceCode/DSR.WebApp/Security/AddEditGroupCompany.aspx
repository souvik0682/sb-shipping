<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEditGroupCompany.aspx.cs" Inherits="DSR.WebApp.Security.AddEditGroupCompany" MasterPageFile="~/Site.Master" Title=":: DSR :: Add / Edit Group Company" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
        function SetMaxLength(obj, maxLen) {
            return (obj.value.length < maxLen);
        }    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="Server">
    <div id="headercaption">ADD / EDIT GROUP COMPANY</div>
    <fieldset style="width:400px;">
        <legend>Add / Edit Group Company</legend>
            <table border="0" cellpadding="5" cellspacing="5">
            <tr>
                <td valign="top" style="width:140px;">Group Name:<span class="errormessage">*</span></td>
                <td>
                    <asp:TextBox ID="txtName" runat="server" MaxLength="50" Width="250"></asp:TextBox><br /><asp:RequiredFieldValidator ID="rfvName" runat="server" CssClass="errormessage" ControlToValidate="txtName" ValidationGroup="Save"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Address:</td>
                <td><asp:TextBox ID="txtAddress" runat="server" MaxLength="200" TextMode="MultiLine" Rows="5" Width="250"></asp:TextBox></td>
            </tr>
            <tr>
                <td>City:</td>
                <td><asp:TextBox ID="txtCity" runat="server" MaxLength="50" Width="250"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Pin:</td>
                <td><asp:TextBox ID="txtPin" runat="server" MaxLength="10" Width="250"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Phone:</td>
                <td><asp:TextBox ID="txtPhone" runat="server" MaxLength="40" Width="250"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Is Active?:<span class="errormessage1">*</span></td>
                <td><asp:CheckBox ID="chkActive" runat="server" /></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Save" OnClick="btnSave_Click" />&nbsp;&nbsp;<asp:Button ID="btnBack" runat="server" CssClass="button" Text="Back" OnClientClick="javascript:window.location.href='ManageGroupCompany.aspx';return false;" />
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>