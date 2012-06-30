<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEditLocation.aspx.cs" Inherits="DSR.WebApp.Security.AddEditLocation" MasterPageFile="~/Blank.Master" Title=":: DSR :: Add / Edit Location" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="Server">
    <fieldset style="width:400px;">
        <legend>Add / Edit Location</legend>
    <table border="0" cellpadding="5" cellspacing="5">
        <tr>
            <td>Location Name:<span class="errormessage1">*</span></td>
            <td><asp:TextBox ID="txtLocName" runat="server" CssClass="textbox" MaxLength="50" Width="250"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Address:</td>
            <td><asp:TextBox ID="txtAddress" runat="server" CssClass="textbox" TextMode="MultiLine" MaxLength="200" Width="250"></asp:TextBox></td>
        </tr>
        <tr>
            <td>City:</td>
            <td><asp:TextBox ID="txtCity" runat="server" CssClass="textbox" MaxLength="20" Width="250"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Pin:</td>
            <td><asp:TextBox ID="txtPin" runat="server" CssClass="textbox" MaxLength="10" Width="250"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Abbreviation:<span class="errormessage1">*</span></td>
            <td><asp:TextBox ID="txtAbbr" runat="server" CssClass="textbox" MaxLength="3" Width="250"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Phone:<span class="errormessage1">*</span></td>
            <td><asp:TextBox ID="txtPhone" runat="server" CssClass="textbox" MaxLength="30" Width="250"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Manager:</td>
            <td><asp:DropDownList ID="ddlManager" runat="server" CssClass="dropdownlist"><asp:ListItem Value="0" Text="--Select--"></asp:ListItem></asp:DropDownList></td>
        </tr>
        <tr>
            <td>Is Active?:<span class="errormessage1">*</span></td>
            <td><asp:CheckBox ID="chkActive" runat="server" /></td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" OnClick="btnSave_Click" />&nbsp;<asp:Button ID="btnBack" runat="server" CssClass="button" Text="Back" OnClientClick="javascript:window.location.href='ManageLocation.aspx';return false;" />
            </td>
        </tr>
    </table>
    </fieldset>
</asp:Content>