<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEditCommitment.aspx.cs" Inherits="DSR.WebApp.Security.AddEditCommitment" MasterPageFile="~/Site.Master" Title=":: DSR :: Add / Edit Commitment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="Server">
    <div id="headercaption">ADD / EDIT Commitment</div>
    <center>
        <fieldset style="width:400px;">
            <legend>Add / Edit Area</legend>
            <table border="0" cellpadding="3" cellspacing="3">
                <tr>
                    <td style="width:140px;">Call:<span class="errormessage1">*</span></td>
                    <td>
                        <asp:DropDownList ID="ddlCall" runat="server"><asp:ListItem Value="0" Text="--Select--"></asp:ListItem></asp:DropDownList><br />
                        <asp:RequiredFieldValidator ID="rfvCall" runat="server" CssClass="errormessage" ControlToValidate="ddlCall" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Week No:<span class="errormessage1">*</span></td>
                    <td>
                        <asp:DropDownList ID="ddlWeek" runat="server"><asp:ListItem Value="0" Text="--Select--"></asp:ListItem></asp:DropDownList><br />
                        <asp:RequiredFieldValidator ID="rfvWeek" runat="server" CssClass="errormessage" ControlToValidate="ddlWeek" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Port:<span class="errormessage1">*</span></td>
                    <td>
                        <asp:DropDownList ID="ddlPort" runat="server"><asp:ListItem Value="0" Text="--Select--"></asp:ListItem></asp:DropDownList><br />
                        <asp:RequiredFieldValidator ID="rfvPort" runat="server" CssClass="errormessage" ControlToValidate="ddlPort" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="width:140px;">TEU:<span class="errormessage1">*</span></td>
                    <td>
                        <asp:TextBox ID="txtTEU" runat="server" MaxLength="10" Width="250"></asp:TextBox><br />
                        <asp:RequiredFieldValidator ID="rfvTEU" runat="server" CssClass="errormessage" ControlToValidate="txtTEU" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="width:140px;">FEU:<span class="errormessage1">*</span></td>
                    <td>
                        <asp:TextBox ID="txtFEU" runat="server" MaxLength="10" Width="250"></asp:TextBox><br />
                        <asp:RequiredFieldValidator ID="rfvFEU" runat="server" CssClass="errormessage" ControlToValidate="txtFEU" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                    </td>
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