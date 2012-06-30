<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageLocation.aspx.cs" Inherits="DSR.WebApp.Security.ManageLocation" MasterPageFile="~/Blank.Master" Title=":: DSR:: Manage Location" %>
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
    <div>
        <div id="headercaption">Manage Location</div>
        <div style="padding:5px;">
            <fieldset style="width:300px;">
                <legend>Search Location</legend>
                <asp:RadioButtonList ID="rblCriteria" runat="server" CssClass="radiobutton" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1" Text="Abbreviation"></asp:ListItem>
                    <asp:ListItem Value="2" Text="Location"></asp:ListItem>
                </asp:RadioButtonList>
                <asp:TextBox ID="txtCriteria" runat="server" CssClass="textbox"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" Width="100px" OnClick="btnSearch_Click" />
            </fieldset>
        </div>
        <asp:UpdateProgress ID="uProgressDPU" runat="server" AssociatedUpdatePanelID="upLoc">
            <ProgressTemplate>
                <div class="progress">
                    <div id="image">
                        <img src="../../Images/PleaseWait.gif" alt="" /></div>
                    <div id="text">
                        Please Wait...</div>
                </div>
            </ProgressTemplate>        
        </asp:UpdateProgress>
        <fieldset id="fsList" runat="server" style="width:710px;min-height:100px;">
        <legend>Location List</legend>
        <div style="float:right;padding-right:10px;padding-bottom:5px;"><asp:Button ID="btnAdd" runat="server" Text="Add New Location" CssClass="button" Width="130px" OnClick="btnAdd_Click" /></div>
        <asp:UpdatePanel ID="upLoc" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
            </Triggers>
            <ContentTemplate>
                <asp:GridView ID="gvwLoc" runat="server" AutoGenerateColumns="false" AllowPaging="true" BorderStyle="None" BorderWidth="0" OnPageIndexChanging="gvwLoc_PageIndexChanging" OnRowDataBound="gvwLoc_RowDataBound" OnRowCommand="gvwLoc_RowCommand" Width="700px">
                    <PagerSettings Mode="NumericFirstLast" Position="Bottom" />
                    <PagerStyle CssClass="gridviewpager" />
                    <EmptyDataRowStyle CssClass="gridviewemptydatarow" />
                    <EmptyDataTemplate>No User(s) Found</EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText="Sl #">
                            <HeaderStyle CssClass="gridviewheader" />
                            <ItemStyle CssClass="gridviewitem" Width="5%" />                                    
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderStyle CssClass="gridviewheader" />
                            <ItemStyle CssClass="gridviewitem" Width="15%" />    
                            <HeaderTemplate><asp:LinkButton ID="lnkHAbbr" runat="server" CommandName="Sort" CommandArgument="Abbr" Text="Abbr"></asp:LinkButton></HeaderTemplate>                                
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderStyle CssClass="gridviewheader" />
                            <ItemStyle CssClass="gridviewitem" Width="25%" />    
                            <HeaderTemplate><asp:LinkButton ID="lnkHLoc" runat="server" CommandName="Sort" CommandArgument="Location" Text="Location"></asp:LinkButton></HeaderTemplate>                                
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="City & Pin">
                            <HeaderStyle CssClass="gridviewheader" />
                            <ItemStyle CssClass="gridviewitem" Width="20%" />                                       
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Location Manager">
                            <HeaderStyle CssClass="gridviewheader" />
                            <ItemStyle CssClass="gridviewitem" Width="25%" />                                       
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderStyle CssClass="gridviewheader" />
                            <ItemStyle CssClass="gridviewitem" Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />                                    
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEdit" runat="server" CommandName="Edit" ImageUrl="~/Images/edit_icon.png" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderStyle CssClass="gridviewheader" />
                            <ItemStyle CssClass="gridviewitem" Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />                                    
                            <ItemTemplate>
                                <asp:ImageButton ID="btnRemove" runat="server" CommandName="Remove" ImageUrl="~/Images/trash_icon.gif" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
        </fieldset>
    </div>
</asp:Content>