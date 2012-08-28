<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEditCustomerAssign.aspx.cs" Inherits="DSR.WebApp.Security.AddEditCustomerAssign" MasterPageFile="~/Site.Master" Title=":: DSR :: Add / Edit Customer Assign" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Scripts/Common.js" type="text/javascript"></script>
    <link href="../Styles/DSR.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="Server">
    <div id="headercaption">ADD / EDIT CUSTOMER ASSIGN</div>
    <center>
        <fieldset style="width:400px;">
            <legend>Assign Customer</legend>
            <table border="0" cellpadding="3" cellspacing="3">
                <tr>
                    <td style="width:140px;">Existing User:<span class="errormessage1">*</span></td>
                    <td>
                        <asp:DropDownList ID="ddlExisting" runat="server"></asp:DropDownList><br />
                        <asp:RequiredFieldValidator ID="rfvExisting" runat="server" CssClass="errormessage" ControlToValidate="ddlExisting" InitialValue="0" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>New User:<span class="errormessage1">*</span></td>
                    <td>
                        <asp:DropDownList ID="ddlNew" runat="server"></asp:DropDownList><br />
                        <asp:RequiredFieldValidator ID="rfvNew" runat="server" CssClass="errormessage" ControlToValidate="ddlNew" InitialValue="0" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Type:<span class="errormessage1">*</span></td>
                    <td>
                        <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                            <asp:ListItem Value="P" Text="Permanent"></asp:ListItem>
                            <asp:ListItem Value="T" Text="Temporary"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td><asp:Label ID="lblFromDt" runat="server" Text="Start Date:"></asp:Label><span class="errormessage1">*</span></td>
                    <td>
                        <asp:TextBox ID="txtStartDt" runat="server"></asp:TextBox>
                        <cc1:calendarextender ID="cbeStartDt" TargetControlID="txtStartDt" 
                            runat="server" />
                        <br />
                        <span id="spnStartDt" runat="server" class="errormessage" style="display: none;"></span>
                    </td>
                </tr>
                <tr id="trEndDt" runat="server">
                    <td><asp:Label ID="lblEndDt" runat="server" Text="End Date:"></asp:Label><span class="errormessage1">*</span></td>
                    <td>
                        <asp:TextBox ID="txtEndDt" runat="server"></asp:TextBox>
                        <cc1:calendarextender ID="cbeEndDt" TargetControlID="txtEndDt" runat="server" />
                        <br />
                        <span id="spnEndDt" runat="server" class="errormessage" style="display: none;"></span>
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