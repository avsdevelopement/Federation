<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmModifyData.aspx.cs" Inherits="FrmModifyData" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
   
</head>
<body>
    <form id="form1" runat="server">
        <div id="CASHR" class="modal fade" role="dialog">
            <div class="modal-dialog modal-lg" style="width: 70%">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title" style="text-align: center; font-family: Verdana; font-size: medium; font-style: italic">Modification screen</h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">

                            <div class="col-md-12">
                                <div class="portlet box blue" id="Div7">
                                    <div class="portlet-title">
                                        <div class="caption">
                                            Modify Recovery
                                        </div>
                                    </div>

                                    <%--Id	Custno	RecDiv	RecCode	GlCode	Subglcode	Accno	S_Bal	S_Inst	S_Intr	MM	YYYY--%>
                                    <div class="portlet-body form">
                                        <!--<form action="#" class="form-horizontal" id="submit_form" method="POST">-->
                                        <div class="form-horizontal">
                                            <div class="form-wizard">
                                                <asp:GridView ID="Grid_SingleModify" runat="server" AutoGenerateColumns="false" ShowFooter="true" OnRowDataBound="Grid_SingleModify_RowDataBound">

                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Id">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Lbl_ID" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Custno">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="TxtCustno" Enable="false" Style="width: 80px" CssClass="form-control" runat="server" Text='<%#Eval("Custno") %>' Enabled="false" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Name">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="TxtCustName" Text='<%# Eval("Cname") %>' Style="width: 200px" CssClass="form-control" runat="server" Enabled="false" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Rec Div" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="TxtRecDiv" Enable="false" Style="width: 80px" CssClass="form-control" runat="server" Text='<%#Eval("RecDiv") %>' Enabled="false" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Rec Code">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="TxtRecCode" Enable="false" Style="width: 80px" CssClass="form-control" runat="server" Text='<%#Eval("RecCode") %>' Enabled="false" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Glcode">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="TxtGlcode" Enable="false" Style="width: 200px" CssClass="form-control" runat="server" Text='<%#Eval("GlCode") %>' Enabled="false" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Subgl">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="TxtSubgl" Text='<%# Eval("Subglcode") %>' Style="width: 80px" CssClass="form-control" runat="server" Enabled="false" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Accno">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="TxtAccno" Text='<%# Eval("Accno") %>' CssClass="form-control" Style="width: 80px" runat="server" Enabled="false" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Balance">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="TxtBal" Text='<%# Eval("S_Bal") %>' Style="width: 80px" CssClass="form-control" runat="server" Enabled="true" />
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="Lbl_S_Bal" Text="" runat="server"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Inst. Amt">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="TxtInst" Text='<%# Eval("S_Inst") %>' Style="width: 80px" CssClass="form-control" runat="server" Enabled="true" />
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="Lbl_S_Inst" Text="" runat="server"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Intr. amt">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="TxtIntr" Text='<%# Eval("S_Intr") %>' Style="width: 80px" CssClass="form-control" runat="server" Enabled="true" />
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="Lbl_S_Intr" Text="" runat="server"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Month">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="TxtMM" Text='<%# Eval("MM") %>' Style="width: 80px" CssClass="form-control" runat="server" Enabled="false" />
                                                            </ItemTemplate>
                                                            
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Year">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="TxtYYYY" Text='<%# Eval("YYYY") %>' Style="width: 80px" CssClass="form-control" runat="server" Enabled="false" />
                                                            </ItemTemplate>
                                                            
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="form-actions">
                                                <div class="row">
                                                    <div class="col-md-offset-3 col-md-9">
                                                        <asp:Button ID="BtnModal_Modify" runat="server" OnClick="BtnModal_Modify_Click" Text="Modify" CssClass="btn btn-success" />
                                                        <asp:Button ID="BtnModal_Exit" Text="Close" runat="server" CssClass="btn btn-success" OnClick="BtnModal_Exit_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!--</form>-->
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12">

                            <div class="table-scrollable" style="width: 100%; height: auto; max-height: 150px; overflow-x: auto; overflow-y: auto">
                                <table class="table table-striped table-bordered table-hover" width="100%">
                                    <thead>
                                        <tr>
                                            <th>
                                                <asp:GridView ID="grdAccStat" runat="server" AlternatingRowStyle-CssClass="alt" CssClass="mGrid" Width="100%"
                                                    AutoGenerateColumns="false" EditRowStyle-BackColor="#FFFF99" DataKeyNames="id" EmptyDataText="No Records Available">
                                                    <Columns>

                                                        <asp:TemplateField HeaderText="EntryDate" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEDate" runat="server" Text='<%# Eval("EDATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="SetNo" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSetNo" runat="server" Text='<%# Eval("SetNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Particulars1">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblParticular" runat="server" Text='<%# Eval("PARTI") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Particulars2">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblparticular2" runat="server" Text='<%# Eval("PARTI1") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Cheque/Refrence">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblInstNo" runat="server" Text='<%# Eval("INSTNO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Credit" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCredit" runat="server" Text='<%# Eval("CREDIT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="DEBIT">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDebit" runat="server" Text='<%# Eval("DEBIT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="BALANCE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBalance" runat="server" Text='<%# Eval("BALANCE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Dr/Cr">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDrCrIndicator" runat="server" Text='<%# Eval("DrCr") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="Th" HorizontalAlign="Left" />
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


                </div>
            </div>
        </div>
    </form>
</body>
</html>
