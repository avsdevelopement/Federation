<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAVS5142.aspx.cs" Inherits="FrmAVS5142" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="Div18">
                <div class="portlet-title">
                    <div class="caption">
                        Voucher Authorisation
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">

                                    <div class="row" style="margin-top: 10px; margin-bottom: 5px;">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2" style="width: 120px">Branch Code : <span class="required">*</span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtBrCode" Placeholder="Branch Code" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                            </div>
                                            <label class="control-label col-md-2" style="width: 120px">Entry Date : <span class="required">*</span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtEDate" Placeholder="Entry Date" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin-top: 10px; margin-bottom: 5px;">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2" style="width: 120px">From SetNo : <span class="required">*</span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtFSetNo" Placeholder="From SetNo" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <label class="control-label col-md-2" style="width: 120px">To SetNo : <span class="required">*</span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtTSetNo" Placeholder="To SetNo" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-12">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn blue" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnAuthorize" runat="server" Text="Authorize" CssClass="btn blue" OnClick="btnAuthorize_Click" />
                                        <asp:Button ID="btnClear" runat="server" CssClass="btn blue" Text="ClearAll" OnClick="btnClear_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>

    <div class="row" id="divVouchers" runat="server" visible="false">
        <div class="col-md-12">
            <div class="table-scrollable" style="height: auto; max-height: 400px; overflow-x: auto; overflow-y: auto">
                <asp:Label ID="Label1" runat="server" Text="Need To Authorize : " BackColor="#66ccff" Font-Bold="true" Font-Size="Medium"></asp:Label>
                <table class="table table-striped table-bordered table-hover" width="100%">
                    <thead>
                        <tr>
                            <th>
                                <asp:GridView ID="grdVoucher" runat="server" CssClass="mGrid" CellPadding="6" CellSpacing="7" PageIndex="5" ForeColor="#333333"
                                    AutoGenerateColumns="False" BorderWidth="1px" BorderColor="#333300" Width="100%" ShowFooter="true">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>

                                        <asp:TemplateField HeaderText="BrCode" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBrCode1" runat="server" Text='<%# Eval("BrCd") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="EntryDate" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEntryDate1" runat="server" Text='<%# Eval("EntryDate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Set No" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSetNo1" runat="server" Text='<%# Eval("SetNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="SubGlCode" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSubGlCode1" runat="server" Text='<%# Eval("SubGlCode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="AccNo" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAccNo1" runat="server" Text='<%# Eval("AccNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="PartiCulars" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPartiCulars1" runat="server" Text='<%# Eval("PartiCulars") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Debit" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDebit1" runat="server" Text='<%# Eval("DEBIT") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Credit" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCredit1" runat="server" Text='<%# Eval("CREDIT") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Maker" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMaker1" runat="server" Text='<%# Eval("Maker") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <FooterStyle BackColor="#ccffcc" Font-Bold="True" ForeColor="Black" HorizontalAlign="Right" BorderStyle="None" />
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

    <div class="row" id="divUnTallySet" runat="server" visible="false">
        <div class="col-md-12">
            <div class="table-scrollable" style="height: auto; max-height: 400px; overflow-x: auto; overflow-y: auto">
                <asp:Label ID="lblVoucher" runat="server" Text="Voucher Not Tally : " BackColor="#66ccff" Font-Bold="true" Font-Size="Medium"></asp:Label>
                <table class="table table-striped table-bordered table-hover" width="100%">
                    <thead>
                        <tr>
                            <th>
                                <asp:GridView ID="grdUnTallySet" runat="server" CssClass="mGrid" CellPadding="6" CellSpacing="7" PageIndex="5" ForeColor="#333333"
                                    AutoGenerateColumns="False" BorderWidth="1px" BorderColor="#333300" Width="100%" ShowFooter="true">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>

                                        <asp:TemplateField HeaderText="Br Code" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBrCode" runat="server" Text='<%# Eval("BrCode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="EntryDate" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEntryDate" runat="server" Text='<%# Eval("EntryDate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Set No" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSetNo" runat="server" Text='<%# Eval("SetNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Debit" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDebit" runat="server" Text='<%# Eval("Debit") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Credit" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCredit" runat="server" Text='<%# Eval("Credit") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Difference" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDifference" runat="server" Text='<%# Eval("Difference") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <FooterStyle BackColor="#ccffcc" Font-Bold="True" ForeColor="Black" HorizontalAlign="Right" BorderStyle="None" />
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

</asp:Content>

