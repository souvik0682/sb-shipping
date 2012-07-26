<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEditLocation.aspx.cs" Inherits="DSR.WebApp.Security.AddEditLocation" MasterPageFile="~/Site.Master" Title=":: DSR :: Add / Edit Location" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Scripts/Common.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function SetMaxLength(obj, maxLen) {
            return (obj.value.length < maxLen);
        }    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="Server">
    <div id="headercaption">ADD / EDIT LOCATION</div>
    <center>
        <fieldset style="width:400px;">
            <legend>Add / Edit Location</legend>
            <table border="0" cellpadding="2" cellspacing="3">
                <tr>
                    <td style="width:140px;">Location Name:<span class="errormessage1">*</span></td>
                    <td><asp:TextBox ID="txtLocName" runat="server" MaxLength="50" Width="250"></asp:TextBox><br /><asp:RequiredFieldValidator ID="rfvName" runat="server" CssClass="errormessage" ControlToValidate="txtLocName" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td>Address:</td>
                    <td><asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" MaxLength="200" Rows="5" Width="250"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>City:</td>
                    <td><asp:TextBox ID="txtCity" runat="server" MaxLength="20" Width="250"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Pin:</td>
                    <td><asp:TextBox ID="txtPin" runat="server" MaxLength="10" Width="250"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Abbreviation:<span class="errormessage1">*</span></td>
                    <td><asp:TextBox ID="txtAbbr" runat="server" MaxLength="3" Width="250"></asp:TextBox><br /><asp:RequiredFieldValidator ID="rfvAbbr" runat="server" CssClass="errormessage" ControlToValidate="txtAbbr" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td>Phone:</td>
                    <td><asp:TextBox ID="txtPhone" runat="server" MaxLength="30" Width="250"></asp:TextBox><asp:RegularExpressionValidator ID="revPhone" runat="server" ControlToValidate="txtPhone" CssClass="errormessage" ValidationGroup="Save" Display="Dynamic"></asp:RegularExpressionValidator></td>
                </tr>
                <tr>
                    <td>Manager:</td>
                    <td><asp:DropDownList ID="ddlManager" runat="server"><asp:ListItem Value="0" Text="--Select--"></asp:ListItem></asp:DropDownList></td>
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