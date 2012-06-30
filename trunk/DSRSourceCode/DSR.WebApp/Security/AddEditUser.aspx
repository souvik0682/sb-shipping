<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEditUser.aspx.cs" Inherits="DSR.WebApp.Security.AddEditUser" MasterPageFile="~/Blank.Master" Title=":: DSR :: Add / Edit User" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="Server">
    <fieldset style="width:500px;">
        <legend>Create New User</legend>
    <table border="0" cellpadding="5" cellspacing="5">
        <tr>
            <td>User Name:<span class="errormessage1">*</span></td>
            <td><asp:TextBox ID="txtUserName" runat="server" CssClass="textbox" MaxLength="10"></asp:TextBox></td>
        </tr>
        <tr>
            <td>First Name:<span class="errormessage1">*</span></td>
            <td><asp:TextBox ID="txtFName" runat="server" CssClass="textbox" MaxLength="30"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Last Name:<span class="errormessage1">*</span></td>
            <td><asp:TextBox ID="txtLName" runat="server" CssClass="textbox" MaxLength="30"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Email Id:<span class="errormessage1">*</span></td>
            <td><asp:TextBox ID="txtEmail" runat="server" CssClass="textbox" MaxLength="100"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Role:<span class="errormessage1">*</span></td>
            <td><asp:DropDownList ID="ddlRole" runat="server" CssClass="dropdownlist"></asp:DropDownList></td>
        </tr>
        <tr>
            <td>Location:<span class="errormessage1">*</span></td>
            <td><asp:DropDownList ID="ddlLoc" runat="server" CssClass="dropdownlist"></asp:DropDownList></td>
        </tr>
        <tr>
            <td>Sales Person Type:<span class="errormessage1">*</span></td>
            <td><asp:DropDownList ID="ddlSalesPersonType" runat="server" CssClass="dropdownlist"></asp:DropDownList></td>
        </tr>
        <tr>
            <td>Is Active?:<span class="errormessage1">*</span></td>
            <td><asp:CheckBox ID="chkActive" runat="server" /></td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" OnClick="btnSave_Click" />&nbsp;<asp:Button ID="btnClose" runat="server" CssClass="button" Text="Close" />
            </td>
        </tr>
    </table>
    </fieldset>
</asp:Content>