<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageUser.aspx.cs" Inherits="DSR.WebApp.Security.ManageUser" MasterPageFile="~/Blank.Master" Title=":: DSR:: Manage User" %>
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
        <div style="padding:5px;">
            <asp:Button ID="btnAdd" runat="server" Text="Add New User" CssClass="button" Height="30px" Width="100px" OnClick="btnAdd_Click" />
        </div>
        <asp:UpdateProgress ID="uProgressDPU" runat="server" AssociatedUpdatePanelID="upUser">
            <ProgressTemplate>
                <div class="progress">
                    <div id="image">
                        <img src="../../Images/PleaseWait.gif" alt="" /></div>
                    <div id="text">
                        Please Wait...</div>
                </div>
            </ProgressTemplate>
        
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="upUser" runat="server">
            <ContentTemplate>
                <asp:GridView ID="gvwUser" runat="server" AutoGenerateColumns="false" AllowPaging="true" BorderStyle="None" BorderWidth="0" OnPageIndexChanging="gvwUser_PageIndexChanging" OnRowDataBound="gvwUser_RowDataBound" OnRowCommand="gvwUser_RowCommand" Width="600px">
                    <PagerSettings Mode="NumericFirstLast" Position="Bottom" />
                    <PagerStyle CssClass="gridviewpager" />
                    <EmptyDataRowStyle CssClass="gridviewemptydatarow" />
                    <EmptyDataTemplate>No User(s) Found</EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText="Sl No.">
                            <HeaderStyle CssClass="gridviewheader" />
                            <ItemStyle CssClass="gridviewitem" Width="5%" />                                    
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderStyle CssClass="gridviewheader" />
                            <ItemStyle CssClass="gridviewitem" Width="17%" />    
                            <HeaderTemplate><asp:LinkButton ID="lnkHName" runat="server" CommandName="Sort" CommandArgument="UserName" Text="User Name"></asp:LinkButton></HeaderTemplate>                                
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderStyle CssClass="gridviewheader" />
                            <ItemStyle CssClass="gridviewitem" Width="17%" />
                            <HeaderTemplate><asp:LinkButton ID="lnkHRole" runat="server" CommandName="Sort" CommandArgument="RoleName" Text="User Role"></asp:LinkButton></HeaderTemplate>                                    
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderStyle CssClass="gridviewheader" />
                            <ItemStyle CssClass="gridviewitem" Width="17%" />           
                            <HeaderTemplate><asp:LinkButton ID="lnkHFN" runat="server" CommandName="Sort" CommandArgument="FirstName" Text="First Name"></asp:LinkButton></HeaderTemplate>                         
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderStyle CssClass="gridviewheader" />
                            <ItemStyle CssClass="gridviewitem" Width="17%" />   
                            <HeaderTemplate><asp:LinkButton ID="lnkHLN" runat="server" CommandName="Sort" CommandArgument="LastName" Text="Last Name"></asp:LinkButton></HeaderTemplate>                                 
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderStyle CssClass="gridviewheader" />
                            <ItemStyle CssClass="gridviewitem" Width="17%" />       
                            <HeaderTemplate><asp:LinkButton ID="lnkHLoc" runat="server" CommandName="Sort" CommandArgument="LocName" Text="Location"></asp:LinkButton></HeaderTemplate>                             
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderStyle CssClass="gridviewheader" />
                            <ItemStyle CssClass="gridviewitem" Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />                                    
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEdit" runat="server" CommandName="Remove" ImageUrl="~/Images/edit_icon.png" />
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
    </div>
</asp:Content>