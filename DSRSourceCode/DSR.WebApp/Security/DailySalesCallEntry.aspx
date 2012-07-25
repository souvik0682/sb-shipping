<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" Title=":: DSR :: Daily Sales Call Entry"
    CodeBehind="DailySalesCallEntry.aspx.cs" Inherits="DSR.WebApp.Security.DailySalesCallEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="container" runat="server">
    <div id="headercaption">
        DAILY SALES CALL ENTRY</div>
    <center>
        <div style="width: 850px;">
            <fieldset style="width: 100%;">
                <legend>Daily Sales Call Entry</legend>
                <asp:UpdatePanel ID="uspSalesCall" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label runat="server" Text="Date" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCallDate" runat="server"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarButtonExtender" TargetControlID="txtCallDate" runat="server" />
                                    <br />
                                    <span id="spnCallDate" runat="server" class="errormessage" style="display: none;">
                                        Call Date can't be blank</span>
                                </td>
                                <td>
                                    <asp:Label runat="server" Text="Call Type" />
                                </td>
                                <td colspan="3">
                                    <asp:DropDownList ID="ddlCallType" runat="server" Width="155px">
                                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                    </asp:DropDownList>
                                    <br />
                                    <span id="spnCallType" runat="server" class="errormessage" style="display: none;">
                                        Call Type can't be blank</span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" Text="Customer" />
                                </td>
                                <td colspan="5">
                                    <asp:DropDownList ID="ddlCustomer" runat="server" Width="240px">
                                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                    </asp:DropDownList>
                                    <br />
                                    <span id="spnCustomer" runat="server" class="errormessage" style="display: none;">
                                        Customer can't be blank</span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" Text="Prospect For" />
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlProspectFor" runat="server" Width="155px">
                                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                    </asp:DropDownList>
                                    <br />
                                    <span id="spnProspect" runat="server" class="errormessage" style="display: none;">
                                        Prospect For can't be blank</span>
                                </td>
                                <td>
                                    <asp:Label runat="server" Text="Next Call" />
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtNextCallDate" runat="server" Width="150px"></asp:TextBox><cc1:CalendarExtender
                                        ID="CalendarExtender1" TargetControlID="txtNextCallDate" runat="server" />
                                        <br />
                                    <span id="spnNextCallDate" runat="server" class="errormessage" style="display: none;"></span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" Text="Remarks" />
                                </td>
                                <td colspan="5">
                                    <asp:TextBox ID="txtRemarks" runat="server" Rows="3" Width="235px" Height="50px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    <table style="margin-left: -6px;">
                                        <tr>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtWeekNo" CssClass="watermark" ForeColor="#747862"
                                                    Width="150" />
                                                <cc1:TextBoxWatermarkExtender ID="txtWMEWeekNo" runat="server" TargetControlID="txtWeekNo"
                                                    WatermarkText="Type Week Number" WatermarkCssClass="watermark">
                                                </cc1:TextBoxWatermarkExtender>
                                                <br />
                                                <asp:Label ID="lblWeek" runat="server" CssClass="errormessage" Height="10px" />
                                            </td>
                                            <td colspan="2">
                                                <asp:TextBox runat="server" ID="txtDestination" CssClass="watermark" ForeColor="#747862"
                                                    Width="210" />
                                                <cc1:TextBoxWatermarkExtender ID="txtWMEDestination" runat="server" TargetControlID="txtDestination"
                                                    WatermarkText="Type Destination" WatermarkCssClass="watermark">
                                                </cc1:TextBoxWatermarkExtender>
                                                <br />
                                                <asp:Label ID="lblDestination" runat="server" CssClass="errormessage" Height="10px" />
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtTEU" CssClass="watermark" ForeColor="#747862"
                                                    Width="150" />
                                                <cc1:TextBoxWatermarkExtender ID="txtWMETEU" runat="server" TargetControlID="txtTEU"
                                                    WatermarkText="Type TEU" WatermarkCssClass="watermark">
                                                </cc1:TextBoxWatermarkExtender>
                                                <br />
                                               <asp:Label ID="lblTEU" runat="server" CssClass="errormessage" Height="10px" />
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtFEU" CssClass="watermark" ForeColor="#747862"
                                                    Width="150" />
                                                <cc1:TextBoxWatermarkExtender ID="txtWMEFEU" runat="server" TargetControlID="txtFEU"
                                                    WatermarkText="Type FEU" WatermarkCssClass="watermark">
                                                </cc1:TextBoxWatermarkExtender>
                                                <br />
                                                <asp:Label ID="lblFEU" runat="server" CssClass="errormessage" Height="10px" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnAddToGrid" runat="server" Text="Add Record" ValidationGroup="Add"
                                                    OnClick="btnAddToGrid_Click" />
                                                     <br />
                                                <asp:Label ID="lblDummy" runat="server" CssClass="errormessage" Height="10px" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    <asp:GridView ID="gvwSalesCall" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                                        BorderStyle="None" BorderWidth="0" Width="100%" OnRowDataBound="gvwSalesCall_RowDataBound"
                                        OnRowCommand="gvwSalesCall_RowCommand">
                                        <PagerSettings Mode="NumericFirstLast" Position="Bottom" />
                                        <PagerStyle CssClass="gridviewpager" />
                                        <EmptyDataRowStyle CssClass="gridviewemptydatarow" />
                                        <EmptyDataTemplate>
                                            No User(s) Found</EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField HeaderText="Week No">
                                                <HeaderStyle CssClass="gridviewheader" />
                                                <ItemStyle CssClass="gridviewitem" Width="5%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderStyle CssClass="gridviewheader" />
                                                <ItemStyle CssClass="gridviewitem" Width="15%" />
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="lnkHName" runat="server" CommandName="Sort" CommandArgument="Destination"
                                                        Text="Deatination"></asp:LinkButton></HeaderTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderStyle CssClass="gridviewheader" />
                                                <ItemStyle CssClass="gridviewitem" Width="13%" />
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="lnkHRole" runat="server" CommandName="Sort" CommandArgument="TEU"
                                                        Text="TEU"></asp:LinkButton></HeaderTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderStyle CssClass="gridviewheader" />
                                                <ItemStyle CssClass="gridviewitem" Width="15%" />
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="lnkHFN" runat="server" CommandName="Sort" CommandArgument="FEU"
                                                        Text="FEU"></asp:LinkButton></HeaderTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderStyle CssClass="gridviewheader" />
                                                <ItemStyle CssClass="gridviewitem" Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnEdit" runat="server" CommandName="EditCommitment" ImageUrl="~/Images/edit.png"
                                                        Height="16" Width="16" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderStyle CssClass="gridviewheader" />
                                                <ItemStyle CssClass="gridviewitem" Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnRemove" runat="server" CommandName="Remove" ImageUrl="~/Images/remove.png"
                                                        Height="16" Width="16" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6" style="margin-left: -6px">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" ValidationGroup="Save" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </fieldset>
            <asp:UpdateProgress ID="uProgressUser" runat="server" AssociatedUpdatePanelID="uspSalesCall">
                <ProgressTemplate>
                    <div class="progress">
                        <div id="image">
                            <img src="../../Images/PleaseWait.gif" alt="" /></div>
                        <div id="text">
                            Please Wait...</div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
    </center>
</asp:Content>
