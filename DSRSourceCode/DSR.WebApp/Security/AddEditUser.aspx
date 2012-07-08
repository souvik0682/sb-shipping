<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEditUser.aspx.cs" Inherits="DSR.WebApp.Security.AddEditUser" MasterPageFile="~/Site.Master" Title=":: DSR :: Add / Edit User" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="Server">
    <div id="headercaption">ADD / EDIT USER</div>
    <fieldset style="width:450px;">
        <legend>Add / Edit User</legend>
        <table border="0" cellpadding="5" cellspacing="5" width="100%">
            <tr>
                <td style="width:150px;">User Name:<span class="errormessage1">*</span></td>
                <td>
                    <asp:TextBox ID="txtUserName" runat="server" MaxLength="10" Width="250"></asp:TextBox><br />
                    <asp:RequiredFieldValidator ID="rfvUserName" runat="server" CssClass="errormessage" ControlToValidate="txtUserName" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>First Name:<span class="errormessage1">*</span></td>
                <td>
                    <asp:TextBox ID="txtFName" runat="server" MaxLength="30" Width="250"></asp:TextBox><br />
                    <asp:RequiredFieldValidator ID="rfvFName" runat="server" CssClass="errormessage" ControlToValidate="txtFName" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Last Name:<span class="errormessage1">*</span></td>
                <td>
                    <asp:TextBox ID="txtLName" runat="server" MaxLength="30" Width="250"></asp:TextBox><br />
                    <asp:RequiredFieldValidator ID="rfvLName" runat="server" CssClass="errormessage" ControlToValidate="txtLName" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Email Id:<span class="errormessage1">*</span></td>
                <td>
                    <asp:TextBox ID="txtEmail" runat="server" MaxLength="100" Width="250"></asp:TextBox><br />
                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" CssClass="errormessage" ControlToValidate="txtEmail" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" CssClass="errormessage" ValidationGroup="Save"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>Role:<span class="errormessage1">*</span></td>
                <td>
                    <asp:DropDownList ID="ddlRole" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRole_SelectedIndexChanged"></asp:DropDownList><br />
                    <asp:RequiredFieldValidator ID="rfvRole" runat="server" CssClass="errormessage" ControlToValidate="ddlRole" InitialValue="0" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Location:<span class="errormessage1">*</span></td>
                <td>
                    <asp:DropDownList ID="ddlLoc" runat="server"></asp:DropDownList><br />
                    <asp:RequiredFieldValidator ID="rfvLoc" runat="server" CssClass="errormessage" ControlToValidate="ddlLoc" InitialValue="0" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Sales Person Type:<span class="errormessage1">*</span></td>
                <td>
                    <asp:DropDownList ID="ddlSalesPersonType" runat="server">
                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                        <asp:ListItem Value="L" Text="Team Lead"></asp:ListItem>
                        <asp:ListItem Value="M" Text="Team Member"></asp:ListItem>
                    </asp:DropDownList><br />
                    <span id="spnType" runat="server" class="errormessage" style="display:none;">Please Select Sales Person Type</span>
                </td>
            </tr>
            <tr>
                <td>Is Active?:<span class="errormessage1">*</span></td>
                <td><asp:CheckBox ID="chkActive" runat="server" /></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />&nbsp;&nbsp;<asp:Button ID="btnBack" runat="server" CssClass="button" Text="Back" OnClientClick="javascript:window.location.href='ManageUser.aspx';return false;"/>
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>