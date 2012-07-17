<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEditCustomer.aspx.cs" Inherits="DSR.WebApp.Security.AddEditCustomer" MasterPageFile="~/Site.Master" Title=":: DSR :: Add / Edit Customer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
        function SetMaxLength(obj, maxLen) {
            return (obj.value.length < maxLen);
        }    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="Server">
    <div id="headercaption">ADD / EDIT CUSTOMER</div>
    <center>
        <fieldset style="width:95%;">
            <legend>Add / Edit Customer</legend>
            <table border="0" cellpadding="2" cellspacing="0" width="100%">
                <tr>
                    <td style="width:20%;">Group:<span class="errormessage">*</span></td>
                    <td style="width:28%;"><asp:DropDownList ID="ddlGroup" runat="server" CssClass="dropdownlist" TabIndex="1"><asp:ListItem Value="0" Text="--Select--"></asp:ListItem></asp:DropDownList><br /><asp:RequiredFieldValidator ID="rfvGroup" runat="server" CssClass="errormessage" ControlToValidate="ddlGroup" InitialValue="0" ValidationGroup="Save" Display="Dynamic"></asp:RequiredFieldValidator></td>
                    <td style="width:4%;"></td>
                    <td style="width:20%;">Phone1:<span class="errormessage">*</span></td>                
                    <td style="width:28%;"><asp:TextBox ID="txtPhone1" runat="server" CssClass="textbox" MaxLength="50" Width="250" TabIndex="11"></asp:TextBox><br /><asp:RequiredFieldValidator ID="rfvPhone1" runat="server" CssClass="errormessage" ControlToValidate="txtPhone1" ValidationGroup="Save" Display="Dynamic"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="revPhone1" runat="server" ControlToValidate="txtPhone1" CssClass="errormessage" ValidationGroup="Save" Display="Dynamic"></asp:RegularExpressionValidator></td>
                </tr>
                <tr>
                    <td>Location:<span class="errormessage">*</span></td>
                    <td><asp:DropDownList ID="ddlLoc" runat="server" AutoPostBack="true" CssClass="dropdownlist" TabIndex="2" OnSelectedIndexChanged="ddlLoc_SelectedIndexChanged"><asp:ListItem Value="0" Text="---Select---"></asp:ListItem></asp:DropDownList><br /><asp:RequiredFieldValidator ID="rfvLoc" runat="server" CssClass="errormessage" ControlToValidate="ddlLoc" InitialValue="0" ValidationGroup="Save" Display="Dynamic"></asp:RequiredFieldValidator></td>
                    <td></td>
                    <td>Phone2:</td>
                    <td><asp:TextBox ID="txtPhone2" runat="server" CssClass="textbox" MaxLength="50" Width="250" TabIndex="12"></asp:TextBox><br /><asp:RegularExpressionValidator ID="revPhone2" runat="server" ControlToValidate="txtPhone2" CssClass="errormessage" ValidationGroup="Save" Display="Dynamic"></asp:RegularExpressionValidator></td>
                </tr>
                <tr>
                    <td>Pin:</td>
                    <td><asp:TextBox ID="txtPin" runat="server" CssClass="textbox" MaxLength="10" Width="150" TabIndex="3"></asp:TextBox>&nbsp;&nbsp;<asp:Button ID="btnSearch" runat="server" TabIndex="4" Text="Search Area" Height="24" OnClick="btnSearch_Click" /></td>
                    <td></td>
                    <td>Customer Profile:</td>
                    <td><asp:TextBox ID="txtProfile" runat="server" CssClass="textbox" MaxLength="500" TextMode="MultiLine" Rows="5" Width="250" TabIndex="13"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Area:<span class="errormessage">*</span></td>
                    <td><asp:DropDownList ID="ddlArea" runat="server" CssClass="dropdownlist" TabIndex="5"><asp:ListItem Value="0" Text="---Select---"></asp:ListItem></asp:DropDownList><br /><asp:RequiredFieldValidator ID="rfvArea" runat="server" CssClass="errormessage" ControlToValidate="ddlArea" InitialValue="0" ValidationGroup="Save" Display="Dynamic"></asp:RequiredFieldValidator></td>
                    <td></td>
                    <td>Account Of:<span class="errormessage">*</span></td>
                    <td><asp:DropDownList ID="ddlExecutive" runat="server" CssClass="dropdownlist" TabIndex="14"><asp:ListItem Value="0" Text="---Select---"></asp:ListItem></asp:DropDownList><br /><asp:RequiredFieldValidator ID="rfvExecutive" runat="server" CssClass="errormessage" ControlToValidate="ddlExecutive" InitialValue="0" ValidationGroup="Save" Display="Dynamic"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td>Customer Type:<span class="errormessage">*</span></td>
                    <td><asp:DropDownList ID="ddlCustType" runat="server" CssClass="dropdownlist" TabIndex="6"><asp:ListItem Value="0" Text="---Select---"></asp:ListItem></asp:DropDownList><br /><asp:RequiredFieldValidator ID="rfvCustType" runat="server" CssClass="errormessage" ControlToValidate="ddlCustType" InitialValue="0" ValidationGroup="Save" Display="Dynamic"></asp:RequiredFieldValidator></td>
                    <td></td>
                    <td>PAN:</td>
                    <td><asp:TextBox ID="txtPan" runat="server" CssClass="textbox" MaxLength="10" Width="250" TabIndex="15"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Client Scope:<span class="errormessage">*</span></td>
                    <td>
                        <asp:DropDownList ID="ddlCorpLoc" runat="server" CssClass="dropdownlist" TabIndex="7">
                            <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                            <asp:ListItem Value="L" Text="Local"></asp:ListItem>
                            <asp:ListItem Value="C" Text="Corporate"></asp:ListItem>
                        </asp:DropDownList><br />
                        <asp:RequiredFieldValidator ID="rfvCorpLoc" runat="server" CssClass="errormessage" ControlToValidate="ddlCorpLoc" InitialValue="0" ValidationGroup="Save" Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                    <td></td>
                    <td>TAN:</td>
                    <td><asp:TextBox ID="txtTan" runat="server" CssClass="textbox" MaxLength="15" Width="250" TabIndex="16"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Customer Name:<span class="errormessage">*</span></td>
                    <td><asp:TextBox ID="txtName" runat="server" CssClass="textbox" MaxLength="60" Width="250" TabIndex="8"></asp:TextBox><br /><asp:RequiredFieldValidator ID="rfvName" runat="server" CssClass="errormessage" ControlToValidate="txtName" ValidationGroup="Save" Display="Dynamic"></asp:RequiredFieldValidator></td>
                    <td></td>
                    <td>BIN:</td>
                    <td><asp:TextBox ID="txtBin" runat="server" CssClass="textbox" MaxLength="15" Width="250" TabIndex="17"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Address:<span class="errormessage">*</span></td>
                    <td><asp:TextBox ID="txtAddress" runat="server" CssClass="textbox" MaxLength="200" TextMode="MultiLine" Rows="5" Width="250" TabIndex="9"></asp:TextBox><br /><asp:RequiredFieldValidator ID="rfvAddr" runat="server" CssClass="errormessage" ControlToValidate="txtAddress" ValidationGroup="Save" Display="Dynamic"></asp:RequiredFieldValidator></td>
                    <td></td>
                    <td>IEC:</td>
                    <td><asp:TextBox ID="txtIec" runat="server" CssClass="textbox" MaxLength="15" Width="250" TabIndex="18"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>City:</td>
                    <td><asp:TextBox ID="txtCity" runat="server" CssClass="textbox" MaxLength="50" Width="250" TabIndex="10"></asp:TextBox></td>
                    <td></td>
                    <td>Is Active?:</td>
                    <td><asp:CheckBox ID="chkActive" runat="server" TabIndex="19" /></td>
                </tr>
                <tr>
                    <td colspan="2" style="padding-top:20px;"><h3><strong>Contact Person1</strong></h3></td>
                    <td></td>
                    <td colspan="2" style="padding-top:20px;"><h3><strong>Contact Person2</strong></h3></td>
                </tr>
                <tr>
                    <td>Contact Person1:<span class="errormessage">*</span></td>
                    <td><asp:TextBox ID="txtPerson1" runat="server" CssClass="textbox" MaxLength="50" Width="250" TabIndex="20"></asp:TextBox><br /><asp:RequiredFieldValidator ID="rfvPerson1" runat="server" CssClass="errormessage" ControlToValidate="txtPerson1" ValidationGroup="Save" Display="Dynamic"></asp:RequiredFieldValidator></td>
                    <td></td>
                    <td>Contact Person2:</td>
                    <td><asp:TextBox ID="txtPerson2" runat="server" CssClass="textbox" MaxLength="50" Width="250" TabIndex="24"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Contact Designation1:<span class="errormessage">*</span></td>
                    <td><asp:TextBox ID="txtDesig1" runat="server" CssClass="textbox" MaxLength="50" Width="250" TabIndex="21"></asp:TextBox><br /><asp:RequiredFieldValidator ID="rfvDesig1" runat="server" CssClass="errormessage" ControlToValidate="txtDesig1" ValidationGroup="Save" Display="Dynamic"></asp:RequiredFieldValidator></td>
                    <td></td>
                    <td>Contact Designation2:</td>
                    <td><asp:TextBox ID="txtDesig2" runat="server" CssClass="textbox" MaxLength="50" Width="250" TabIndex="25"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Contact Mobile1:<span class="errormessage">*</span></td>
                    <td><asp:TextBox ID="txtContactMob1" runat="server" CssClass="textbox" MaxLength="15" Width="250" TabIndex="22"></asp:TextBox><br /><asp:RequiredFieldValidator ID="rfvContactMob1" runat="server" CssClass="errormessage" ControlToValidate="txtContactMob1" ValidationGroup="Save" Display="Dynamic"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="revContactMob1" runat="server" ControlToValidate="txtContactMob1" CssClass="errormessage" ValidationGroup="Save" Display="Dynamic"></asp:RegularExpressionValidator></td>
                    <td></td>
                    <td>Contact Mobile2:</td>
                    <td><asp:TextBox ID="txtContactMob2" runat="server" CssClass="textbox" MaxLength="15" Width="250" TabIndex="26"></asp:TextBox><br /><asp:RegularExpressionValidator ID="revContactMob2" runat="server" ControlToValidate="txtContactMob2" CssClass="errormessage" ValidationGroup="Save" Display="Dynamic"></asp:RegularExpressionValidator></td>
                </tr>
                <tr>
                    <td>Contact EmailId1:</td>
                    <td><asp:TextBox ID="txtEmail1" runat="server" CssClass="textbox" MaxLength="50" Width="250" TabIndex="23"></asp:TextBox><br /><asp:RegularExpressionValidator ID="revEmail1" runat="server" ControlToValidate="txtEmail1" CssClass="errormessage" ValidationGroup="Save" Display="Dynamic"></asp:RegularExpressionValidator></td>
                    <td></td>
                    <td>Contact EmailId2:</td>
                    <td><asp:TextBox ID="txtEmail2" runat="server" CssClass="textbox" MaxLength="50" Width="250" TabIndex="27"></asp:TextBox><br /><asp:RegularExpressionValidator ID="revEmail2" runat="server" ControlToValidate="txtEmail2" CssClass="errormessage" ValidationGroup="Save" Display="Dynamic"></asp:RegularExpressionValidator></td>
                </tr>
                <tr>
                    <td colspan="5"><asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" ValidationGroup="Save" OnClick="btnSave_Click" TabIndex="28" />&nbsp;&nbsp;<asp:Button ID="btnBack" runat="server" CssClass="button" Text="Back" TabIndex="29" /></td>
                </tr>
            </table>
        </fieldset>
    </center>
</asp:Content>