<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageUser.aspx.cs" Inherits="DSR.WebApp.Security.ManageUser" MasterPageFile="~/Site.Master" Title=":: DSR:: Manage User" %>
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
    <div id="headercaption">MANAGE USER</div>
    <center>
    <div style="width:850px;">        
        <fieldset style="width:100%;">
            <legend>Search User</legend>
            <table>
                <tr>
                    <td>
                        <asp:TextBox ID="txtUserName" runat="server" CssClass="watermark" ForeColor="#747862"></asp:TextBox>
                        <cc1:TextBoxWatermarkExtender ID="txtWMEUserName" runat="server" TargetControlID="txtUserName" WatermarkText="Type Username" WatermarkCssClass="watermark"></cc1:TextBoxWatermarkExtender>
                    </td>
                    <td>
                        <asp:TextBox ID="txtFName" runat="server" CssClass="watermark" ForeColor="#747862"></asp:TextBox>
                        <cc1:TextBoxWatermarkExtender ID="txtWMEFName" runat="server" TargetControlID="txtFName" WatermarkText="Type First Name" WatermarkCssClass="watermark"></cc1:TextBoxWatermarkExtender>
                    </td>
                    <td><asp:Button ID="btnSearch" runat="server" Text="Search" Width="100px" OnClick="btnSearch_Click" /></td>
                </tr>
            </table>              
        </fieldset>
        <asp:UpdateProgress ID="uProgressUser" runat="server" AssociatedUpdatePanelID="upUser">
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
            <legend>User List</legend>
            <div style="float:right;padding-bottom:5px;">                
                Results Per Page:<asp:DropDownList ID="ddlPaging" runat="server" Width="50px" AutoPostBack="true" 
                    OnSelectedIndexChanged="ddlPaging_SelectedIndexChanged">
                    <asp:ListItem Text="10" Value="10" />
                    <asp:ListItem Text="30" Value="30" />
                    <asp:ListItem Text="50" Value="50" />
                    <asp:ListItem Text="100" Value="100" />
                </asp:DropDownList>&nbsp;&nbsp;            
                <asp:Button ID="btnAdd" runat="server" Text="Add New User" Width="130px" OnClick="btnAdd_Click" />
            </div><br /><br />
            <div>
                <asp:UpdatePanel ID="upUser" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="ddlPaging" EventName="SelectedIndexChanged" />
                    </Triggers>
                    <ContentTemplate>
                        <asp:GridView ID="gvwUser" runat="server" AutoGenerateColumns="false" AllowPaging="true" BorderStyle="None" BorderWidth="0" OnPageIndexChanging="gvwUser_PageIndexChanging" OnRowDataBound="gvwUser_RowDataBound" OnRowCommand="gvwUser_RowCommand" Width="100%">
                        <PagerSettings Mode="NumericFirstLast" Position="Bottom" />
                        <PagerStyle CssClass="gridviewpager" />
                        <EmptyDataRowStyle CssClass="gridviewemptydatarow" />
                        <EmptyDataTemplate>No User(s) Found</EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderText="Sl#">
                                <HeaderStyle CssClass="gridviewheader" />
                                <ItemStyle CssClass="gridviewitem" Width="5%" />                                    
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderStyle CssClass="gridviewheader" />
                                <ItemStyle CssClass="gridviewitem" Width="15%" />    
                                <HeaderTemplate><asp:LinkButton ID="lnkHName" runat="server" CommandName="Sort" CommandArgument="UserName" Text="User Name"></asp:LinkButton></HeaderTemplate>                                
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderStyle CssClass="gridviewheader" />
                                <ItemStyle CssClass="gridviewitem" Width="13%" />
                                <HeaderTemplate><asp:LinkButton ID="lnkHRole" runat="server" CommandName="Sort" CommandArgument="RoleName" Text="User Role"></asp:LinkButton></HeaderTemplate>                                    
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderStyle CssClass="gridviewheader" />
                                <ItemStyle CssClass="gridviewitem" Width="15%" />           
                                <HeaderTemplate><asp:LinkButton ID="lnkHFN" runat="server" CommandName="Sort" CommandArgument="FirstName" Text="First Name"></asp:LinkButton></HeaderTemplate>                         
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderStyle CssClass="gridviewheader" />
                                <ItemStyle CssClass="gridviewitem" Width="15%" />   
                                <HeaderTemplate><asp:LinkButton ID="lnkHLN" runat="server" CommandName="Sort" CommandArgument="LastName" Text="Last Name"></asp:LinkButton></HeaderTemplate>                                 
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderStyle CssClass="gridviewheader" />
                                <ItemStyle CssClass="gridviewitem" Width="13%" />       
                                <HeaderTemplate><asp:LinkButton ID="lnkHLoc" runat="server" CommandName="Sort" CommandArgument="LocName" Text="Location"></asp:LinkButton></HeaderTemplate>                             
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderStyle CssClass="gridviewheader" />
                                <ItemStyle CssClass="gridviewitem" Width="14%" HorizontalAlign="Center" VerticalAlign="Middle" />                                    
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkPwd" runat="server" CommandName="ChangePwd" Text="Reset Password"></asp:LinkButton>                                    
                                </ItemTemplate>
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