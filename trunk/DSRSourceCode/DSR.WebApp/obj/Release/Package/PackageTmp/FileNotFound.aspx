<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileNotFound.aspx.cs" Inherits="DSR.WebApp.FileNotFound" MasterPageFile="~/Blank.Master" Title=":: DSR :: File Not Found" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="Server">
    <div>
        <div class="dvheader">
            <h3>
                The page cannot be found</h3>
        </div>
        <div id="dvbody">
            The page you are looking for might have been removed, had its name changed, or is
            temporarily unavailable.
        </div>
        <div id="dvfooter" style="padding-top: 15px;">
            HTTP Error 404 - File or directory not found.
        </div>
    </div>
</asp:Content>