<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmFundingEffect.aspx.cs" Inherits="FrmFundingEffect" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-title">
        <h1>Funding Effect</h1>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="table-scrollable" style="width: 100%; height: 500px; overflow-x: auto; overflow-y: auto">
                <table class="table table-striped table-bordered table-hover">
                    <thead>
                        <tr>
                            <th>
                                <asp:GridView ID="grdOwgData" runat="server"
                                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                    EditRowStyle-BackColor="#FFFF99"
                                    Width="100%">
                                    <Columns>
                                        <%--OW.BANK_CODE,OW.BRANCH_CODE,OW.INSTRU_TYPE,OW.INSTRUDATE,OW.ACC_TYPE,OW.OPRTN_TYPE--%>
                                        <asp:TemplateField HeaderText="Status" Visible="true" HeaderStyle-BackColor="#ccffcc">
                                            <ItemTemplate>
                                                <asp:Label ID="Fund_Status" runat="server" Text='<%# Eval("Fund_Status") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Bank Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="Lbl_BankCode" runat="server" Text='<%# Eval("BANK_CODE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Branch Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="Lbl_BranchCode" runat="server" Text='<%# Eval("BRANCH_CODE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Intr Type" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="Lbl_InstruType" runat="server" Text='<%# Eval("INSTRU_TYPE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="INSTRUDATE" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="Lbl_InstruDate" runat="server" Text='<%# Eval("INSTRUDATE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ACC_TYPE" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="Lbl_AccType" runat="server" Text='<%# Eval("ACC_TYPE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="OPRTN_TYPE" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="Lbl_OprType" runat="server" Text='<%# Eval("OPRTN_TYPE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Set No" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="SET_NO" runat="server" Text='<%# Eval("SET_NO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Scrl" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="SCRL" runat="server" Text='<%# Eval("SCRL") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="AT" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="AT" runat="server" Text='<%# Eval("AT") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="AN" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAccNo" runat="server" Text='<%# Eval("AC") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Name" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="Name" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="AMOUNT" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="Amount" runat="server" Text='<%# Eval("AMOUNT") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="BANKNAME" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="Bankname" runat="server" Text='<%# Eval("BANKNAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Inst No" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="INSTNO" runat="server" Text='<%# Eval("instNO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Maker" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="Date" runat="server" Text='<%# Eval("maker") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Particulars" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="Parti" runat="server" Text='<%# Eval("PARTICULARS") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
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
        <div class="row">
            <div class="col-lg-12">
                <div class="col-lg-3">
                    <asp:Button ID="Fundding" runat="server" Text="Fundding Effect" OnClick="Fundding_Click" CssClass="btn btn-primary" />
                </div>
                <div class="col-lg-3">
                    <asp:CheckBox ID="Chk_IsReturnDone" runat="server" Text="Is Return process Done ?" style="font-size:larger; font-style:italic;" />
                </div>

            </div>
        </div>

    </div>
</asp:Content>

