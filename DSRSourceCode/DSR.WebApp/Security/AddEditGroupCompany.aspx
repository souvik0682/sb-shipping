<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEditGroupCompany.aspx.cs" Inherits="DSR.WebApp.Security.AddEditGroupCompany" MasterPageFile="~/Site.Master" Title=":: DSR :: Add / Edit Group Company" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Scripts/Common.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function SetMaxLength(obj, maxLen) {
            return (obj.value.length < maxLen);
        }    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="Server">
    <div id="headercaption">ADD / EDIT GROUP COMPANY</div>
    <center>
        <fieldset style="width:400px;">
            <legend>Add / Edit Group Company</legend>
                <table border="0" cellpadding="3" cellspacing="3">
                <tr>
                    <td valign="top" style="width:140px;">Group Name:<span class="errormessage">*</span></td>
                    <td><asp:TextBox ID="txtName" runat="server" MaxLength="50" Width="250"></asp:TextBox><br /><asp:RequiredFieldValidator ID="rfvName" runat="server" CssClass="errormessage" ControlToValidate="txtName" ValidationGroup="Save" Display="Dynamic"></asp:RequiredFieldValidator></td>
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
                    <td><asp:TextBox ID="txtPhone" runat="server" MaxLength="40" Width="250"></asp:TextBox><br /><asp:RegularExpressionValidator ID="revPhone" runat="server" ControlToValidate="txtPhone" CssClass="errormessage" ValidationGroup="Save" Display="Dynamic"></asp:RegularExpressionValidator></td>
                </tr>
                <tr>
                    <td>Is Active?:</td>
                    <td><asp:CheckBox ID="chkActive" runat="server" /></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Save" OnClick="btnSave_Click" />&nbsp;&nbsp;<asp:Button ID="btnBack" runat="server" CssClass="button" Text="Back" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </center>
</asp:Content>