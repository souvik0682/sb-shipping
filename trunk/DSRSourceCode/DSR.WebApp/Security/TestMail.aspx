<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestMail.aspx.cs" Inherits="DSR.WebApp.Security.TestMail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Recipient:<asp:TextBox ID="TextBox1" runat="server" Width="300"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="Send Mail" OnClick="Button1_Click" />
        <asp:Button ID="Button2" runat="server" Text="Send Mail (Default Credential)" OnClick="Button2_Click" />
    </div>
    </form>
</body>
</html>
