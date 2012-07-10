<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="DSR.WebApp.Error" MasterPageFile="~/Blank.Master" Title=":: DSR :: Error" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="Server">
    <div>
        <div id="dvheader">
            <h3>
                <asp:Literal ID="ltlTitle" runat="server">There was a problem completing your request</asp:Literal></h3>
        </div>
        <div id="dvbody">
            <asp:Label ID="lblErrMessage" runat="server" CssClass="errormessage"></asp:Label>
        </div>
        <div class="dvfooter" style="padding-top:15px;">
            <a onclick="history.go(-1);return false;" href="#" title="Back to the previous page" class="hyperlink2">Back to the previous page</a>
        </div>
    </div>
</asp:Content>