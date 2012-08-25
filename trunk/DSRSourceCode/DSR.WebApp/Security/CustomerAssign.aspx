<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerAssign.aspx.cs" Inherits="DSR.WebApp.Security.CustomerAssign" MasterPageFile="~/Site.Master" Title=":: DSR :: Customer Assign" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Scripts/Common.js" type="text/javascript"></script>
    <link href="../Styles/DSR.css" rel="stylesheet" type="text/css" />
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
    <div id="headercaption">CUSTOMER ASSIGN</div>
    <center>
    <div style="width:850px;">        
        <fieldset style="width:100%;">
            <legend>Assign Customer</legend>
            <asp:UpdatePanel ID="upAdd" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td>Existing User:</td>
                            <td colspan="4"><asp:DropDownList ID="ddlExisting" runat="server"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>New User:</td>
                            <td style="padding-right:10px;">
                                <asp:DropDownList ID="ddlNew" runat="server"></asp:DropDownList>
                            </td>
                            <td>Type:</td>
                            <td colspan="2">
                                <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                    <asp:ListItem Value="P" Text="Permanent"></asp:ListItem>
                                    <asp:ListItem Value="T" Text="Temporary"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblFromDt" runat="server" Text="Start Date:"></asp:Label></td>
                            <td style="padding-right:10px;">
                                <asp:TextBox ID="txtStartDt" runat="server"></asp:TextBox>
                                <cc1:calendarextender ID="cbeStartDt" TargetControlID="txtStartDt" 
                                    runat="server" />
                                <br />
                                <span id="spnStartDt" runat="server" class="errormessage" style="display: none;">Start Date can't be blank</span>
                            </td>
                            <td style="width:70px;"><asp:Label ID="lblEndDt" runat="server" Text="End Date"></asp:Label></td>
                            <td style="width:150px;padding-right:10px;">
                                <asp:TextBox ID="txtEndDt" runat="server"></asp:TextBox>
                                <cc1:calendarextender ID="cbeEndDt" TargetControlID="txtEndDt" runat="server" />
                                <br />
                                <span id="spnEndDt" runat="server" class="errormessage" style="display: none;">End Date can't be blank</span>
                            </td>
                            <td>
                                <asp:HiddenField ID="hdnId" runat="server" />
                                <asp:Button ID="btnAdd" runat="server" Text="Add New User" Width="130px" OnClick="btnAdd_Click" />
                            </td>
                        </tr>
                    </table>                            
                </ContentTemplate>
            </asp:UpdatePanel>
        </fieldset>
        <asp:UpdateProgress ID="uProgressList" runat="server" AssociatedUpdatePanelID="upList">
            <ProgressTemplate>
                <div class="progress">
                    <div id="image"><img src="../../Images/PleaseWait.gif" alt="" /></div>
                    <div id="text">Please Wait...</div>
                </div>
            </ProgressTemplate>        
        </asp:UpdateProgress>
        <fieldset id="fsList" runat="server" style="width:100%;min-height:100px;">
            <legend>Assigned Customer</legend>
            <div style="float:right;padding-bottom:5px;">                
                Results Per Page:<asp:DropDownList ID="ddlPaging" runat="server" Width="50px" AutoPostBack="true" 
                    OnSelectedIndexChanged="ddlPaging_SelectedIndexChanged">
                    <asp:ListItem Text="10" Value="10" />
                    <asp:ListItem Text="30" Value="30" />
                    <asp:ListItem Text="50" Value="50" />
                    <asp:ListItem Text="100" Value="100" />
                </asp:DropDownList>&nbsp;&nbsp;            
            </div>
            <div>
                <asp:UpdatePanel ID="upList" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlPaging" EventName="SelectedIndexChanged" />
                    </Triggers>
                    <ContentTemplate>
                        <asp:GridView ID="gvwList" runat="server" AutoGenerateColumns="false" AllowPaging="true" BorderStyle="None" BorderWidth="0" OnPageIndexChanging="gvwList_PageIndexChanging" OnRowDataBound="gvwList_RowDataBound" OnRowCommand="gvwList_RowCommand" Width="100%">
                        <PagerSettings Mode="NumericFirstLast" Position="Bottom" />
                        <PagerStyle CssClass="gridviewpager" />
                        <EmptyDataRowStyle CssClass="gridviewemptydatarow" />
                        <EmptyDataTemplate>No Data Found</EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderText="Sl#">
                                <HeaderStyle CssClass="gridviewheader" />
                                <ItemStyle CssClass="gridviewitem" Width="5%" />                                    
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="New User">
                                <HeaderStyle CssClass="gridviewheader" />
                                <ItemStyle CssClass="gridviewitem" Width="25%" />                    
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Existing User">
                                <HeaderStyle CssClass="gridviewheader" />
                                <ItemStyle CssClass="gridviewitem" Width="25%" />                    
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Permanent / Temporary">
                                <HeaderStyle CssClass="gridviewheader" />
                                <ItemStyle CssClass="gridviewitem" Width="15%" />                    
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Start Date">
                                <HeaderStyle CssClass="gridviewheader" />
                                <ItemStyle CssClass="gridviewitem" Width="10%" />                    
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="End Date">
                                <HeaderStyle CssClass="gridviewheader" />
                                <ItemStyle CssClass="gridviewitem" Width="10%" />                    
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderStyle CssClass="gridviewheader" />
                                <ItemStyle CssClass="gridviewitem" Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />                                    
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnEdit" runat="server" CommandName="EditData" ImageUrl="~/Images/edit.png" Height="16" Width="16" />
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