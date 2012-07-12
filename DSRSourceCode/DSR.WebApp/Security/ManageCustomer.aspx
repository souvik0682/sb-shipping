<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageCustomer.aspx.cs" Inherits="DSR.WebApp.Security.ManageCustomer" MasterPageFile="~/Site.Master" Title=":: DSR:: Manage Customer" %>
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
    <div id="headercaption">MANAGE CUSTOMER</div>
    <center>
    <div style="width:850px;">        
        <fieldset style="width:100%;">
            <legend>Search Customer</legend>
            <table>
                <tr>
                    <td>
                        <asp:TextBox ID="txtLoc" runat="server" CssClass="watermark" ForeColor="#747862"></asp:TextBox>
                        <cc1:TextBoxWatermarkExtender ID="txtWMELoc" runat="server" TargetControlID="txtLoc" WatermarkText="Type Location" WatermarkCssClass="watermark"></cc1:TextBoxWatermarkExtender>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCustName" runat="server" CssClass="watermark" ForeColor="#747862"></asp:TextBox>
                        <cc1:TextBoxWatermarkExtender ID="txtWMECust" runat="server" TargetControlID="txtCustName" WatermarkText="Type Customer Name" WatermarkCssClass="watermark"></cc1:TextBoxWatermarkExtender>
                    </td>
                    <td>
                        <asp:TextBox ID="txtGrComp" runat="server" CssClass="watermark" ForeColor="#747862"></asp:TextBox>
                        <cc1:TextBoxWatermarkExtender ID="txtWMEGr" runat="server" TargetControlID="txtGrComp" WatermarkText="Type Group Company" WatermarkCssClass="watermark"></cc1:TextBoxWatermarkExtender>
                    </td>
                    <td><asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" Width="100px" OnClick="btnSearch_Click" /></td>
                </tr>
            </table>
        </fieldset>
        <asp:UpdateProgress ID="uProgressCust" runat="server" AssociatedUpdatePanelID="upCust">
            <ProgressTemplate>
                <div class="progress">
                    <div id="image">
                        <img src="../../Images/PleaseWait.gif" alt="" /></div>
                    <div id="text">
                        Please Wait...</div>
                </div>
            </ProgressTemplate>        
        </asp:UpdateProgress>
        <fieldset id="fsList" runat="server" style="width:100%;min-height:100px;">
            <legend>Customer List</legend>
            <div style="float:right;padding-bottom:5px;">
                Results Per Page:<asp:DropDownList ID="ddlPaging" runat="server" Width="50px" AutoPostBack="true" 
                        OnSelectedIndexChanged="ddlPaging_SelectedIndexChanged">
                        <asp:ListItem Text="15" Value="15" />
                        <asp:ListItem Text="30" Value="30" />
                        <asp:ListItem Text="50" Value="50" />
                        <asp:ListItem Text="100" Value="100" />
                    </asp:DropDownList>&nbsp;&nbsp;
                <asp:Button ID="btnAdd" runat="server" Text="Add New Customer" CssClass="button" Width="130px" OnClick="btnAdd_Click" />
            </div><br /><br />
            <div>
                <asp:UpdatePanel ID="upCust" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="ddlPaging" EventName="SelectedIndexChanged" />
                    </Triggers>
                    <ContentTemplate>
                        <asp:GridView ID="gvwCust" Width="100%" runat="server" AutoGenerateColumns="false"
                            AllowPaging="false" BorderStyle="None" BorderWidth="0" OnRowDataBound="gvwCust_RowDataBound" OnRowCommand="gvwCust_RowCommand" OnPageIndexChanging="gvwCust_PageIndexChanging">
                            <PagerSettings Mode="NumericFirstLast" />
                            <PagerStyle CssClass="gridviewpager" />
                            <EmptyDataRowStyle CssClass="gridviewemptydatarow" />
                            <EmptyDataTemplate>
                                No customer(s) found
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="Sl#">
                                    <HeaderStyle CssClass="gridviewheader" />
                                    <ItemStyle CssClass="gridviewitem" Width="5%" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderStyle CssClass="gridviewheader" />
                                    <ItemStyle CssClass="gridviewitem" Width="20%" />
                                    <HeaderTemplate><asp:LinkButton ID="lnkHLoc" runat="server" Text="Location" CommandName="Sort" CommandArgument="Location"></asp:LinkButton></HeaderTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderStyle CssClass="gridviewheader" />
                                    <ItemStyle CssClass="gridviewitem" Width="25%" />      
                                    <HeaderTemplate><asp:LinkButton ID="lnkHName" runat="server" Text="Customer Name" CommandName="Sort" CommandArgument="CustName"></asp:LinkButton></HeaderTemplate>              
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Group Company">
                                    <HeaderStyle CssClass="gridviewheader" />
                                    <ItemStyle CssClass="gridviewitem" Width="20%" />                          
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Executive Name">
                                    <HeaderStyle CssClass="gridviewheader" />
                                    <ItemStyle CssClass="gridviewitem" Width="20%" />                          
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderStyle CssClass="gridviewheader" />
                                    <ItemStyle CssClass="gridviewitem" Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />                                    
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEdit" runat="server" CommandName="Edit" ImageUrl="~/Images/edit.png" Height="16" Width="16" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderStyle CssClass="gridviewheader" />
                                    <ItemStyle CssClass="gridviewitem" Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />                                    
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnRemove" runat="server" CommandName="Remove" ImageUrl="~/Images/remove.png" Height="16" Width="16" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </fieldset>
    </div>
    </center>
</asp:Content>