<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DSR.WebApp.Login"
    MasterPageFile="~/Blank.Master" Title=":: DSR :: Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="Server">
    <div id="dvAsync" style="padding: 5px; display: none;">
        <div class="asynpanel">
            <div id="dvAsyncClose">
                <img alt="" src="../Images/Close-Button.bmp" style="cursor: pointer;" onclick="ClearErrorState()" /></div>
            <div id="dvAsyncMessage">
            </div>
        </div>
    </div>
    <div style="float:left;margin:6% 0% 0% 30%;">
        <fieldset style="width: 400px;">
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <div class="progress">
                        <div id="image">
                            <img src="../Images/PleaseWait.gif" alt="" /></div>
                        <div id="text">
                            Please Wait...</div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table border="0" cellpadding="5" cellspacing="5" width="100%">
                        <tr>
                            <td class="label">
                                Username:
                            </td>
                            <td>
                                <asp:TextBox ID="txtUserName" runat="server" CssClass="textbox" MaxLength="10" Width="250px"></asp:TextBox><br />
                                <asp:RequiredFieldValidator ID="rfvName" runat="server" CssClass="errormessage" ControlToValidate="txtUserName" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                <asp:Label ID="lblMsgUsername" runat="server" CssClass="errormessage"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                Password:
                            </td>
                            <td>
                                <asp:TextBox ID="txtPassword" runat="server" CssClass="textbox" TextMode="Password"
                                    onkeypress="javascript:doClick(event,'container_btnLogin');" MaxLength="10" Width="250px"></asp:TextBox><br />
                                <asp:RequiredFieldValidator ID="rfvPwd" runat="server" CssClass="errormessage" ControlToValidate="txtPassword" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                <asp:Label ID="lblMsgPassword" runat="server" CssClass="errormessage"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-bottom: 15px;" colspan="2">
                                <asp:Label ID="lblMsg" runat="server" CssClass="errormessage" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-bottom: 15px;" colspan="2">
                                <asp:Button ID="btnLogin" runat="server" CssClass="button" Text="Login" ValidationGroup="Save" OnClick="btnLogin_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="logindisclaimer">
                                Disclaimer: 
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </fieldset>
    </div>
</asp:Content>
