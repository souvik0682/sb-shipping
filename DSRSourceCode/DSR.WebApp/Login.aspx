<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DSR.WebApp.Login"
    MasterPageFile="~/Blank.Master" Title=":: DSR :: Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        fieldset.login label
        {
            display: block;
        }
    </style>
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
    <div style="float: left; margin: 3% 0% 0% 32%;width:350px;">
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
                <fieldset class="login">
                    <legend>Login Information</legend>
                    <p>
                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="txtUserName">Username:</asp:Label>
                        <asp:TextBox ID="txtUserName" runat="server" CssClass="textbox" MaxLength="10" Width="300px"></asp:TextBox><br />
                        <asp:RequiredFieldValidator ID="rfvName" runat="server" CssClass="errormessage" ControlToValidate="txtUserName"
                            ValidationGroup="Save"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblMsgUsername" runat="server" CssClass="errormessage"></asp:Label>
                    </p>
                    <p>
                        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="txtPassword">Password:</asp:Label>
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="textbox" TextMode="Password"
                            onkeypress="javascript:doClick(event,'container_btnLogin');" MaxLength="10" Width="300px"></asp:TextBox><br />
                        <asp:RequiredFieldValidator ID="rfvPwd" runat="server" CssClass="errormessage" ControlToValidate="txtPassword"
                            ValidationGroup="Save"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblMsgPassword" runat="server" CssClass="errormessage"></asp:Label>
                    </p>
                    <p class="submitButton">
                        <asp:Button ID="btnLogin" runat="server" CssClass="button" Text="Login" ValidationGroup="Save"
                            OnClick="btnLogin_Click" />
                    </p>
                    <p>
                        <asp:Label ID="lblMsg" runat="server" CssClass="errormessage" Visible="false"></asp:Label>
                    </p>
                    <p class="logindisclaimer">
                        Disclaimer:DSR hereby disclaims any and all liability to any individual / organization/ person for any loss or damage caused to them for any action taken on the basis of the general information available on the web site which may be due to omission, clerical errors or for any other reason whatsoever. DSR will not be responsible for any damages your business/ individual may suffer. We make no warranties of any kind, expressed or implied for information we provide. We reserve the right to revise the policies at any time. All end-users of us must adhere to the above policies.
                    </p>
                </fieldset>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
