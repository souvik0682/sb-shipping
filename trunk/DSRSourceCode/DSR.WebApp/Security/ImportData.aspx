<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportData.aspx.cs" Inherits="DSR.WebApp.Security.ImportData" MasterPageFile="~/Site.Master" Title=":: DSR::Import Data" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Scripts/Common.js" type="text/javascript"></script>
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
    <div style="width:950px;">        
        <fieldset style="width:100%;">
            <legend>Upload Ship Soft Data</legend>
            <table>
                <tr>
                    <td>
                        Select File: <asp:FileUpload ID="fuShipSoft" runat="server" />
                    </td>
                    <td><asp:Button ID="btnImport" runat="server" Text="Import" Width="100px" OnClick="btnImport_Click" /></td>
                </tr>
            </table>
        </fieldset>
        <fieldset id="fsList" runat="server" style="width:100%;min-height:100px;">
            <legend>Ship Soft Data</legend>
            <div>
                <table>
                    <tr>
                        <td style="padding-right:5px;">Select Type:</td>
                        <td style="padding-right:30px;">
                            <asp:RadioButtonList ID="rblTag" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblTag_SelectedIndexChanged">
                                <asp:ListItem Value="1" Text="Tagged"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Untagged"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td style="padding-right:5px;">Select Customer:</td>
                        <td><asp:DropDownList ID="ddlCust" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCust_SelectedIndexChanged"><asp:ListItem Value="0" Text="--Select--"></asp:ListItem></asp:DropDownList><asp:RequiredFieldValidator ID="rfvCust" runat="server" CssClass="errormessage" ControlToValidate="ddlCust" InitialValue="0" ValidationGroup="Save"></asp:RequiredFieldValidator></td>
                    </tr>
                </table>
            </div>
            <div style="float:right;padding-bottom:5px;">
                Results Per Page:<asp:DropDownList ID="ddlPaging" runat="server" Width="50px" AutoPostBack="true" 
                        OnSelectedIndexChanged="ddlPaging_SelectedIndexChanged">
                        <asp:ListItem Text="10" Value="10" />
                        <asp:ListItem Text="30" Value="30" />
                        <asp:ListItem Text="50" Value="50" />
                        <asp:ListItem Text="100" Value="100" />
                    </asp:DropDownList>&nbsp;&nbsp;
            </div><br />
            <div>
                <asp:GridView ID="gvwData" runat="server" AutoGenerateColumns="false" AllowPaging="true" BorderStyle="None" BorderWidth="0" OnPageIndexChanging="gvwData_PageIndexChanging" OnRowDataBound="gvwData_RowDataBound" OnRowCommand="gvwData_RowCommand" Width="100%">
                    <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
                    <PagerStyle CssClass="gridviewpager" />
                    <EmptyDataRowStyle CssClass="gridviewemptydatarow" />
                    <EmptyDataTemplate>No Data Found</EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField>
                            <HeaderStyle CssClass="gridviewheader" />
                            <ItemStyle CssClass="gridviewitem" Width="3%" />    
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSel" runat="server" />
                                <asp:HiddenField ID="hdnId" runat="server" />
                            </ItemTemplate>                                
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Location">
                            <HeaderStyle CssClass="gridviewheader" />
                            <ItemStyle CssClass="gridviewitem" Width="9%" />                                       
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Prospect">
                            <HeaderStyle CssClass="gridviewheader" />
                            <ItemStyle CssClass="gridviewitem" Width="7%" />                                       
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Vessel Voyage">
                            <HeaderStyle CssClass="gridviewheader" />
                            <ItemStyle CssClass="gridviewitem" Width="13%" />                                       
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderStyle CssClass="gridviewheader" />
                            <ItemStyle CssClass="gridviewitem" Width="25%" />
                            <HeaderTemplate><asp:LinkButton ID="lnkHShipper" runat="server" CommandName="Sort" CommandArgument="ShipperName" Text="Shipper Name"></asp:LinkButton></HeaderTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Port">
                            <HeaderStyle CssClass="gridviewheader" />
                            <ItemStyle CssClass="gridviewitem" Width="9%" />                                       
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TEU">
                            <HeaderStyle CssClass="gridviewheader" />
                            <ItemStyle CssClass="gridviewitem" Width="4%" />                                       
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="FEU">
                            <HeaderStyle CssClass="gridviewheader" />
                            <ItemStyle CssClass="gridviewitem" Width="4%" />                                       
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderStyle CssClass="gridviewheader" />
                            <ItemStyle CssClass="gridviewitem" Width="26%" /> 
                            <HeaderTemplate><asp:LinkButton ID="lnkHCust" runat="server" CommandName="Sort" CommandArgument="CustName" Text="Customer"></asp:LinkButton></HeaderTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div style="padding-top:10px;">
                <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Save" OnClick="btnSave_Click" />&nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Cancel" OnClick="btnCancel_Click" />
            </div>
        </fieldset>
    </div>
    </center>
</asp:Content>