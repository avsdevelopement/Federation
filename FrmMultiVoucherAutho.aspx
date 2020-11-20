<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmMultiVoucherAutho.aspx.cs" Inherits="FrmMultiVoucherAutho" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="portlet box blue" id="form_wizard_1">
        <div class="portlet-title">
            <div class="caption">
                Multiple Voucher
            </div>
        </div>
        <div class="portlet-body form">
            <div class="form-horizontal">
                <div class="form-wizard">
                    <div class="form-body">
                        <div class="tab-content">
                            <div class="row" style="margin: 7px 0 7px 0; margin-top: 20px;">
                                <div class="col-lg-12">
                                    <label class="control-label col-md-2">Trf Type</label>
                                    <div class="col-md-10">
                                        <asp:RadioButtonList ID="Rdb_PassType" AutoPostBack="true" RepeatDirection="Horizontal" runat="server" OnTextChanged="Rdb_PassType_TextChanged" TabIndex="1">
                                            <asp:ListItem Text="Single Passing" Selected="True" Value="1" />
                                            <asp:ListItem Text="Lot Passing" Value="2" />
                                        </asp:RadioButtonList>

                                    </div>
                                </div>
                            </div>
                            <asp:Table ID="TblDiv_MainWindow" runat="server">
                                <asp:TableRow ID="Tbl_R1" runat="server">
                                    <asp:TableCell ID="Tbl_c1" runat="server" Width="50%" BorderStyle="Solid" BorderWidth="1px">
                                        <div class="tab-pane active" id="tab__blue">



                                            <div class="row" style="margin: 7px 0 7px 0;" runat="server" id="Div_LotPassing">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Total Cr. Amount<span class="required">* </span></label>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="Txt_MTCrAmt" Enabled="false" runat="server" CssClass="form-control" AutoPostBack="true" BackColor="#ffccff"></asp:TextBox>
                                                    </div>
                                                   <label class="control-label col-md-2">Total Dr. Amount<span class="required">* </span></label>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="Txt_MTDrAmt" Enabled="false" runat="server" CssClass="form-control" AutoPostBack="true" BackColor="#ccffcc"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div runat="server" id="Div_SinglePassing">
                                                <div class="row" style="margin: 7px 0 7px 0; margin-top: 20px;">
                                                    <div class="col-lg-12">
                                                        <label class="control-label col-md-2">Trf Type</label>
                                                        <div class="col-md-10">
                                                            <asp:RadioButtonList ID="rbtnTransferType" AutoPostBack="true" RepeatDirection="Horizontal" runat="server" TabIndex="1">
                                                                <asp:ListItem Text="Transfer" Selected="True" Value="T" />
                                                                <asp:ListItem Text="Cheque" Value="C" />
                                                            </asp:RadioButtonList>

                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row" style="margin: 7px 0 7px 0; margin-top: 20px;">
                                                    <div class="col-lg-12">
                                                        <label class="control-label col-md-2">Trx Type</label>
                                                        <div class="col-md-4">
                                                            <asp:DropDownList ID="DdlCRDR" CssClass="form-control" AutoPostBack="true" runat="server" TabIndex="3">
                                                                <asp:ListItem Value="0">-- Select --</asp:ListItem>
                                                                <asp:ListItem Value="1">Credit</asp:ListItem>
                                                                <asp:ListItem Value="2">Debit</asp:ListItem>
                                                            </asp:DropDownList>

                                                        </div>
                                                        <label class="control-label col-md-2">CustNo</label>

                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtCustNo" CssClass="form-control" Enabled="false" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row" style="margin: 7px 0 7px 0; margin-top: 20px;">
                                                    <div class="col-lg-12">
                                                        <label class="control-label col-md-1">Cr Amt</label>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="TxtCRBAL" CssClass="form-control" runat="server" PlaceHolder="Credit" Enabled="false"></asp:TextBox>
                                                        </div>
                                                        <label class="control-label col-md-1">Dr Amt</label>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="TxtDRBAL" CssClass="form-control" onkeyup="FormatIt(this)" runat="server" PlaceHolder="Debit" Enabled="false"></asp:TextBox>
                                                        </div>
                                                        <label class="control-label col-md-1">Diff Amt</label>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="TxtDiff" CssClass="form-control" onkeyup="FormatIt(this)" runat="server" PlaceHolder="Diffrence Amount" Enabled="false"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row" style="margin: 7px 0 7px 0; margin-top: 20px;">
                                                    <div class="col-lg-12">
                                                        <label class="control-label col-md-2">Prd Cd.<span class="required"></span></label>
                                                        <div class="col-md-6">
                                                            <asp:TextBox ID="TxtPtype" runat="server" onkeypress="javascript:return isNumber (event)" PlaceHolder="Product Code" Style="Width: 20%; height: 33px; border: 1px solid #c0c1c1;" AutoPostBack="true" TabIndex="5"></asp:TextBox>&nbsp;&nbsp;

                                                        <asp:TextBox ID="TxtPname" runat="server" PlaceHolder="Product Name" Style="width: 61%; height: 33px; margin-left: -3px; border: 1px solid #c0c1c1;" AutoPostBack="true" TabIndex="6"></asp:TextBox>

                                                        </div>


                                                    </div>
                                                </div>
                                                <div class="row" style="margin: 7px 0 7px 0; margin-top: 20px;">
                                                    <div class="col-lg-12">
                                                        <label class="control-label col-md-2">A/C No.<span class="required">* </span></label>
                                                        <div class="col-md-6">
                                                            <asp:TextBox ID="TxtAccNo" runat="server" PlaceHolder="Account No" onkeypress="javascript:return isNumber (event)" Style="Width: 20%; height: 33px; border: 1px solid #c0c1c1;" AutoPostBack="true" TabIndex="7"></asp:TextBox>&nbsp;&nbsp;
                                            <asp:TextBox ID="TxtCustName" runat="server" PlaceHolder="Account Holder Name" Style="width: 61%; height: 33px; margin-left: -3px; border: 1px solid #c0c1c1;" AutoPostBack="true" TabIndex="8"></asp:TextBox>


                                                        </div>
                                                    </div>
                                                </div>


                                                <div id="divCheque" runat="server">
                                                    <div class="row" style="margin: 7px 0 7px 0; margin-top: 20px;">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-2">Instr No.</label>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="TxtChequeNo" CssClass="form-control" runat="server" TabIndex="9" PlaceHolder="Instrument No" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                            </div>
                                                            <label class="control-label col-md-2">Instr Dt.</label>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="TxtChequeDate" CssClass="form-control" onkeyup="FormatIt(this)" runat="server" TabIndex="10" PlaceHolder="Instrument Date" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row" style="margin: 7px 0 7px 0; margin-top: 20px;">
                                                    <div class="col-lg-12">
                                                        <label class="control-label col-md-2">Narr</label>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="TxtNarration" CssClass="form-control" runat="server" TabIndex="11" PlaceHolder="Narration"></asp:TextBox>
                                                        </div>
                                                        <label class="control-label col-md-2">Pass Amt :<span class="required"></span></label>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="TxtPassAmount" CssClass="form-control" runat="server" TabIndex="12" PlaceHolder="Amount" onkeypress="javascript:return isNumber (event)" AutoPostBack="true"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-12" style="text-align: center">
                                                        <asp:Button ID="Submit" runat="server" Text="Add" CssClass="btn btn-primary" OnClientClick="javascript:return isvalidate()" TabIndex="13" />
                                                        <asp:Button ID="btnClear" runat="server" Text="ClearAll" CssClass="btn btn-primary" TabIndex="14" />
                                                        <asp:Button ID="btnPost" Enabled="false" runat="server" Text="Post" CssClass="btn btn-success" TabIndex="15" />
                                                        <asp:Button ID="Exit" runat="server" Text="Exit" CssClass="btn btn-primary" TabIndex="16" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:TableCell>
                                    <asp:TableCell ID="Tbl_c2" runat="server" Width="50%" BorderStyle="Solid" BorderWidth="1px">
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Limit<span class="required">* </span></label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="TxtLLimit" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-2">StDt<span class="required"></span></label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="TxtLSancDt" CssClass="form-control" runat="server" AutoPostBack="true" ReadOnly="True"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">IntRt<span class="required">* </span></label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="TxtLIntRate" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-2">DueDt<span class="required"></span></label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="TxtLDueDt" CssClass="form-control" runat="server" AutoPostBack="true" ReadOnly="True"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">LstIntdt<span class="required">* </span></label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="TxtLLastIntDate" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-2">Instamt<span class="required"></span></label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="TxtLInstAmt" CssClass="form-control" runat="server" AutoPostBack="true" ReadOnly="True"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0">
                                        </div>
                                        <div class="col-lg-12">
                                            <div class="col-md-12" style="border-bottom: 2px solid #3598dc;"><strong style="color: #3598dc"></strong></div>
                                        </div>
                                        <div class="col-lg-12">
                                            <asp:Table ID="Tbl_Photo" runat="server">
                                                <asp:TableRow ID="Rw_Ph1" runat="server">
                                                    <asp:TableCell ID="TblCell1" runat="server">
                                                        <asp:Label ID="Label1" runat="server" Text="Signature" Style="font-weight: bold"></asp:Label>
                                                        <img id="Img1" runat="server" style="height: 70%; width: 70%; border: 1px solid #000000; padding: 5px" />
                                                    </asp:TableCell>
                                                    <asp:TableCell ID="TblCell2" runat="server">
                                                        <asp:Label ID="Label2" runat="server" Text="Photo" Style="font-weight: bold"></asp:Label>
                                                        <img id="Img2" runat="server" style="height: 70%; width: 70%; border: 1px solid #000000; padding: 5px" />
                                                    </asp:TableCell>
                                                </asp:TableRow>

                                            </asp:Table>

                                        </div>
                                        <div class="col-lg-12">
                                            <div class="col-md-12" style="border-bottom: 2px solid #3598dc;"><strong style="color: #3598dc"></strong></div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-3">Balance<span class="required"></span></label>
                                                <div class="col-md-5">
                                                    <asp:TextBox ID="TxtIwcTotal" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-3">UnClr Bal<span class="required"></span></label>
                                                <div class="col-md-5">
                                                    <asp:TextBox ID="TxtOwcTotal" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-3">Tot Bal<span class="required"></span></label>
                                                <div class="col-md-5">
                                                    <asp:TextBox ID="TxtOwcRTotal" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="table-scrollable" style="width: 100%; height: 500px; overflow-x: auto; overflow-y: auto">
                     <table class="table table-striped table-bordered table-hover">
                        <thead>
                            <tr>
                                <th>
                                    <asp:GridView ID="grdvoucher" runat="server"
                                        AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                        EditRowStyle-BackColor="#FFFF99"
                                        PagerStyle-CssClass="pgr" Width="100%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="ID" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="ID" runat="server" Text='<%# Eval("SCROLLNO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="AT" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="AT" runat="server" Text='<%# Eval("SubGlCode") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Ac No" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="ACNO" runat="server" Text='<%# Eval("AccNo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Name" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="Name" runat="server" Text='<%# Eval("CustName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="AMOUNT" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="Amount" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="PARTICULARS" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="Particulars" runat="server" Text='<%# Eval("Particulars2") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Entry Type" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="CRDR" runat="server" Text='<%# Eval("TrxType") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Authorize" Visible="true">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="Lnk_MAutho" runat="server" CommandArgument='<%#Eval("SCROLLNO")%>' CommandName="select" class="glyphicon glyphicon-trash"></asp:LinkButton>
                                                </ItemTemplate>
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
    <div id="alertModal" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <center><h4 class="modal-title" style="color:#ff0000">AVS CORE</h4></center>
                </div>
                <div class="modal-body">
                    <p></p>
                    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                </div>
                <div class="modal-footer">
                    <button class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>


</asp:Content>

