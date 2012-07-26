<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEditArea.aspx.cs" Inherits="DSR.WebApp.Security.AddEditArea" MasterPageFile="~/Site.Master" Title=":: DSR :: Add / Edit Area" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Scripts/Common.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="Server">
    <div id="headercaption">ADD / EDIT AREA</div>
    <center>
        <fieldset style="width:400px;">
            <legend>Add / Edit Area</legend>
            <table border="0" cellpadding="3" cellspacing="3">
                <tr>
                    <td style="width:140px;">Area Name:<span class="errormessage1">*</span></td>
                    <td>
                        <asp:TextBox ID="txtName" runat="server" MaxLength="50" Width="250"></asp:TextBox><br />
                        <asp:RequiredFieldValidator ID="rfvName" runat="server" CssClass="errormessage" ControlToValidate="txtName" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Location:</td>
                    <td>
                        <asp:DropDownList ID="ddlLoc" runat="server"><asp:ListItem Value="0" Text="--Select--"></asp:ListItem></asp:DropDownList><br />
                        <asp:RequiredFieldValidator ID="rfvLoc" runat="server" CssClass="errormessage" ControlToValidate="ddlLoc" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="width:140px;">Pin Code:<span class="errormessage1">*</span></td>
                    <td>
                        <asp:TextBox ID="txtPin" runat="server" MaxLength="10" Width="250"></asp:TextBox><br />
                        <asp:RequiredFieldValidator ID="rfvPin" runat="server" CssClass="errormessage" ControlToValidate="txtPin" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                    </td>
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