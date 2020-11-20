<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmInvMaturity.aspx.cs" Inherits="FrmInvMaturity" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="portlet box blue" id="form_wizard_1">
                        <div class="portlet-title">
                            <div class="caption">
                                Investment Maturity Report
                            </div>
                        </div>
                        <div class="portlet-body form">
                            <div class="form-horizontal">
                                <div class="form-wizard">
                                    <div class="form-body">
                                        <div class="tab-content">
                                            <div class="tab-pane active" id="tab__blue">
                                            </div>
                                            <div style="border: 1px solid #3598dc">
                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-2">
                                                                <label class="control-label">From Date</label>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <asp:TextBox ID="txtFromDate" Width="75%" CssClass="form-control" onkeyup="FormatIt(this)" PlaceHolder="dd/mm/yyyy" runat="server"></asp:TextBox>
                                                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtFromDate">
                                                                </asp:TextBoxWatermarkExtender>
                                                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtFromDate">
                                                                </asp:CalendarExtender>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <label class="control-label">To Date</label>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <asp:TextBox ID="txtToDate" Width="75%" CssClass="form-control" onkeyup="FormatIt(this)" PlaceHolder="dd/mm/yyyy" runat="server"></asp:TextBox>
                                                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtToDate">
                                                                </asp:TextBoxWatermarkExtender>
                                                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtToDate">
                                                                </asp:CalendarExtender>
                                                            </div>
                                                            <div class="col-lg-7">
                                                            </div>
                                                        </div>
                                                    </div>

                                                <div class="row">
                                                    <div class="col-lg-12" style="text-align: left; margin-top: 12px; margin-bottom: 13px; margin-left: 15px;">
                                                        <div class="col-lg-6">
                                                            <asp:Button ID="btnview" runat="server" Text="View" CssClass="btn btn-primary" OnClick="btnview_Click" />
                                                            <asp:Button ID="btnReport" runat="server" Text="Report" CssClass="btn btn-primary" OnClick="btnReport_Click" />
                                                            <asp:Button ID="btnExit" runat="server" Text="Exit" CssClass="btn btn-primary" OnClick="btnExit_Click" />
                                                            <br />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="table-scrollable">
                        <table class="table table-striped table-bordered table-hover">
                            <asp:Label ID="Lbl_Sts" runat="server" Text="Investment Maturity" Style="font-size: medium;"></asp:Label>
                            <thead>
                                <tr>
                                    <th>
                                        <asp:GridView ID="GrdInv" runat="server" ShowFooter="true"
                                            AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                            EditRowStyle-BackColor="#FFFF99" HeaderStyle-HorizontalAlign="Center"
                                            onrowdatabound="GrdInv_RowDataBound"
                                            PagerStyle-CssClass="pgr" Width="100%">
                                            <Columns>
                                                <%--<asp:TemplateField HeaderText="Sr No" ItemStyle-HorizontalAlign="right">
                                                    <ItemTemplate>
                                                        <%#Eval("ID") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Product Code" >
                                                    <ItemTemplate>
                                                        <%#Eval("PRODUCTCODE") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Acc No">
                                                    <ItemTemplate>
                                                        <%#Eval("ACCNO") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date of Inv" ItemStyle-HorizontalAlign="center">
                                                    <ItemTemplate>
                                                        <%#Eval("INVDATE") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Due Date" ItemStyle-HorizontalAlign="center">
                                                    <ItemTemplate>
                                                        <%#Eval("DUEDATE") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Inv Amt" ItemStyle-HorizontalAlign="right">
                                                    <ItemTemplate>
                                                        <%#Eval("INVAMT") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Int Amt" ItemStyle-HorizontalAlign="right">
                                                    <ItemTemplate>
                                                        <%#Eval("INTAMT") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Maturity Amt" ItemStyle-HorizontalAlign="right">
                                                    <ItemTemplate>
                                                        <%#Eval("MATURITYAMT") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Rate" ItemStyle-HorizontalAlign="right">
                                                    <ItemTemplate>
                                                        <%#Eval("RATE") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Period" ItemStyle-HorizontalAlign="right">
                                                    <ItemTemplate>
                                                        <%#Eval("PERIOD") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Closing Bal" ItemStyle-HorizontalAlign="right" FooterStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <%#Eval("CLOSINGBAL") %>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblTotal" runat="server" ></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle CssClass="pgr" />
                                            <SelectedRowStyle BackColor="#66FF99" />
                                            <EditRowStyle BackColor="#FFFF99" />
                                            <AlternatingRowStyle CssClass="alt" />
                                        </asp:GridView>
                                    </th>
                                </tr>
                            </thead>
                        </table>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

