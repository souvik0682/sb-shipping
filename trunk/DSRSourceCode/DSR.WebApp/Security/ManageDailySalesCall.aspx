<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageDailySalesCall.aspx.cs" Inherits="DSR.WebApp.Security.ManageDailySalesCall" MasterPageFile="~/Site.Master" Title=":: DSR:: Manage Daily Sales Call" %>
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
    <div id="headercaption">MANAGE DAILY SALES CALL</div>
    <center>
    <div style="width:850px;">        
        <asp:UpdateProgress ID="uProgressDSC" runat="server" AssociatedUpdatePanelID="upDSC">
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
            <legend>Call List</legend>
            <div style="float:right;padding-bottom:5px;">
                Results Per Page:<asp:DropDownList ID="ddlPaging" runat="server" Width="50px" AutoPostBack="true" 
                        OnSelectedIndexChanged="ddlPaging_SelectedIndexChanged">
                        <asp:ListItem Text="10" Value="10" />
                        <asp:ListItem Text="30" Value="30" />
                        <asp:ListItem Text="50" Value="50" />
                        <asp:ListItem Text="100" Value="100" />
                    </asp:DropDownList>&nbsp;&nbsp;
                <asp:Button ID="btnAdd" runat="server" Text="Add New Call" Width="130px" OnClick="btnAdd_Click" />
            </div><br /><br />
            <div>
                <asp:UpdatePanel ID="upDSC" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlPaging" EventName="SelectedIndexChanged" />
                    </Triggers>
                    <ContentTemplate>
                        <asp:GridView ID="gvwDSC" runat="server" AutoGenerateColumns="false" AllowPaging="true" BorderStyle="None" BorderWidth="0" OnPageIndexChanging="gvwDSC_PageIndexChanging" OnRowDataBound="gvwDSC_RowDataBound" OnRowCommand="gvwDSC_RowCommand" Width="100%">
                            <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
                            <PagerStyle CssClass="gridviewpager" />
                            <EmptyDataRowStyle CssClass="gridviewemptydatarow" />
                            <EmptyDataTemplate>No Area(s) Found</EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="Sl#">
                                    <HeaderStyle CssClass="gridviewheader" />
                                    <ItemStyle CssClass="gridviewitem" Width="10%" />                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Call Date">
                                    <HeaderStyle CssClass="gridviewheader" />
                                    <ItemStyle CssClass="gridviewitem" Width="15%" />                                        
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Customer">
                                    <HeaderStyle CssClass="gridviewheader" />
                                    <ItemStyle CssClass="gridviewitem" Width="20%" />                                        
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Call Type">
                                    <HeaderStyle CssClass="gridviewheader" />
                                    <ItemStyle CssClass="gridviewitem" Width="15%" />                                        
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Prospect For">
                                    <HeaderStyle CssClass="gridviewheader" />
                                    <ItemStyle CssClass="gridviewitem" Width="15%" />                                        
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Next Call Date">
                                    <HeaderStyle CssClass="gridviewheader" />
                                    <ItemStyle CssClass="gridviewitem" Width="15%" />                                        
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