<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEditUser.aspx.cs" Inherits="DSR.WebApp.Security.AddEditUser" MasterPageFile="~/Blank.Master" Title=":: DSR :: Add / Edit User" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="Server">
    <fieldset style="width:410px;">
        <legend>Add / Edit User</legend>
        <table border="0" cellpadding="5" cellspacing="5">
            <tr>
                <td style="width:150px;">User Name:<span class="errormessage1">*</span></td>
                <td><asp:TextBox ID="txtUserName" runat="server" CssClass="textbox" MaxLength="10" Width="250"></asp:TextBox></td>
            </tr>
            <tr>
                <td>First Name:<span class="errormessage1">*</span></td>
                <td><asp:TextBox ID="txtFName" runat="server" CssClass="textbox" MaxLength="30" Width="250"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Last Name:<span class="errormessage1">*</span></td>
                <td><asp:TextBox ID="txtLName" runat="server" CssClass="textbox" MaxLength="30" Width="250"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Email Id:<span class="errormessage1">*</span></td>
                <td><asp:TextBox ID="txtEmail" runat="server" CssClass="textbox" MaxLength="100" Width="250"></asp:TextBox></td>
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
                <td>
                    <asp:DropDownList ID="ddlSalesPersonType" runat="server" CssClass="dropdownlist">
                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                        <asp:ListItem Value="L" Text="Team Lead"></asp:ListItem>
                        <asp:ListItem Value="M" Text="Team Member"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Is Active?:<span class="errormessage1">*</span></td>
                <td><asp:CheckBox ID="chkActive" runat="server" /></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" OnClick="btnSave_Click" />&nbsp;&nbsp;<asp:Button ID="btnBack" runat="server" CssClass="button" Text="Back" OnClientClick="javascript:window.location.href='ManageUser.aspx';return false;"/>
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>