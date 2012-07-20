<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportData.aspx.cs" Inherits="DSR.WebApp.Security.ImportData" MasterPageFile="~/Site.Master" Title=":: DSR::Import Data" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="Server">
    <div id="dvAsync" style="padding: 5px; display: none;">
        <div class="asynpanel">
            <div id="dvAsyncClose">
                <img alt="" src="../../Images/Close-Button.bmp" style="cursor: pointer;" onclick="ClearErrorState()" /></div>
            <div id="dvAsyncMessage">
            </div>
        </div>
    </div>
    <div id="headercaption">IMPORT DATA</div>
    <center>
    <div style="width:850px;">        
        <fieldset style="width:100%;">
            <legend>Search Area</legend>
            <table>
                <tr>
                    <td>
                        Select File: <asp:FileUpload ID="fuShipSoft" runat="server" />
                    </td>
                    <td><asp:Button ID="btnImport" runat="server" Text="Search" Width="100px" OnClick="btnImport_Click" /></td>
                </tr>
            </table>
        </fieldset>
        <fieldset id="fsList" runat="server" style="width:100%;min-height:100px;">
            <legend>Area List</legend>
            <div style="float:right;padding-bottom:5px;">
                Results Per Page:<asp:DropDownList ID="ddlPaging" runat="server" Width="50px" AutoPostBack="true" 
                        OnSelectedIndexChanged="ddlPaging_SelectedIndexChanged">
                        <asp:ListItem Text="10" Value="10" />
                        <asp:ListItem Text="30" Value="30" />
                        <asp:ListItem Text="50" Value="50" />
                        <asp:ListItem Text="100" Value="100" />
                    </asp:DropDownList>&nbsp;&nbsp;
            </div><br /><br />
            <div>

            </div>
        </fieldset>
    </div>
    </center>
</asp:Content>