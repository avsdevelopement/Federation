<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAuthorizeLoan.aspx.cs" Inherits="FrmAuthorizeLoan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script>
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }

        function FormatIt(obj) {
            if (obj.value.length == 2) // Day
                obj.value = obj.value + "/";
            if (obj.value.length == 5) // month 
                obj.value = obj.value + "/";
            if (obj.value.length == 11) // year 
                alert("Please Enter valid Date");
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        Authorize Screen For Loan
                    </div>
                </div>
                <div class="portlet-body form">

                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab1">

                                        <div style="border: 1px solid #3598dc">
                                            <div class="portlet-body">
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Voucher Details :</strong></div>
                                                    </div>
                                                </div>

                                                <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                    <div class="col-lg-12">
                                                        <label class="control-label col-md-1">Voucher No</label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtVoucherNo" CssClass="form-control" ReadOnly="true" runat="server" />
                                                        </div>
                                                        <label class="control-label col-md-1">Scroll No</label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtScrollNo" CssClass="form-control" ReadOnly="true" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <asp:Table ID="TblDiv_MainWindow" runat="server">
                                            <asp:TableRow ID="Tbl_R1" runat="server">
                                                <asp:TableCell ID="Tbl_c1" runat="server" Width="70%" BorderStyle="Solid" BorderWidth="1px">


                                                    <div style="border: 1px solid #3598dc">
                                                        <div class="portlet-body">
                                                            <div class="row">
                                                                <div class="col-lg-12">
                                                                    <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Account Information : </strong></div>
                                                                </div>
                                                            </div>

                                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                                <div class="col-lg-12">
                                                                    <div class="col-md-2">
                                                                        <label class="control-label ">Prod Type:</label>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <asp:TextBox ID="txtProdType" Enabled="false" Placeholder="Product Type" runat="server" CssClass="form-control" />
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <asp:TextBox ID="txtProdName" Enabled="false" Placeholder="Product Name" runat="server" CssClass="form-control" />
                                                                    </div>
                                                                    <div class="col-md-1">
                                                                        <label class="control-label ">Acc\No:</label>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <asp:TextBox ID="txtAccNo" Enabled="false" Placeholder="Account Number" runat="server" CssClass="form-control" />
                                                                    </div>
                                                                    <div class="col-md-3">
                                                                        <asp:TextBox ID="txtAccName" Enabled="false" Placeholder="Account Name" runat="server" CssClass="form-control" />
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                                <div class="col-lg-12">
                                                                    <div class="col-md-2">
                                                                        <label class="control-label ">Cust Number :</label>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <asp:TextBox ID="txtCustNo" Enabled="false" Placeholder="Customer Number" runat="server" CssClass="form-control" />
                                                                    </div>
                                                                    <div class="col-md-1">
                                                                        <label class="control-label ">Status</label>
                                                                    </div>
                                                                    <div class="col-md-1">
                                                                        <asp:TextBox ID="txtAccStatus" Enabled="false" runat="server" CssClass="form-control" />
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <asp:DropDownList ID="ddlAccStatus" Enabled="false" CssClass="form-control" runat="server"></asp:DropDownList>
                                                                    </div>
                                                                    <div class="col-md-1">
                                                                        <label class="control-label ">Amount:<span class="required">*</span></label>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <asp:TextBox ID="txtDrAmount" ReadOnly="true" placeholder="Installment Amount" CssClass="form-control" runat="server" />
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>

                                                    <div style="border: 1px solid #3598dc">
                                                        <div class="portlet-body">
                                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                                <div class="col-lg-12">
                                                                    <div class="col-md-2" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Head Name</strong></div>
                                                                    <div class="col-md-2" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Paid Amount</strong></div>
                                                                    <div class="col-md-2" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Head Name</strong></div>
                                                                    <div class="col-md-2" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Paid Amount</strong></div>
                                                                    <div class="col-md-2" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Head Name</strong></div>
                                                                    <div class="col-md-2" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Paid Amount</strong></div>
                                                                </div>
                                                            </div>

                                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                                <div class="col-lg-12">
                                                                    <div class="col-md-2">
                                                                        <label class="control-label ">Principle :</label>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <asp:TextBox ID="txtPrinAmt" Enabled="false" Placeholder="Amount" CssClass="form-control" runat="server" />
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <label class="control-label ">Interest :</label>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <asp:TextBox ID="txtIntAmt" Enabled="false" Placeholder="Amount" CssClass="form-control" runat="server" />
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <label class="control-label ">Penal Interest :</label>
                                                                    </div>

                                                                    <div class="col-md-2">
                                                                        <asp:TextBox ID="txtPIntAmt" Enabled="false" Placeholder="Amount" CssClass="form-control" runat="server" />
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                                <div class="col-lg-12">
                                                                    <div class="col-md-2">
                                                                        <label class="control-label ">Int Recievable :</label>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <asp:TextBox ID="txtIntRecAmt" Enabled="false" Placeholder="Amount" CssClass="form-control" runat="server" />
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <label class="control-label ">Notice Charges :</label>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <asp:TextBox ID="txtNotChrgAmt" Enabled="false" Placeholder="Amount" CssClass="form-control" runat="server" />
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <label class="control-label ">Service Chrges :</label>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <asp:TextBox ID="txtSerChrgAmt" Enabled="false" Placeholder="Amount" CssClass="form-control" runat="server" />
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                                <div class="col-lg-12">
                                                                    <div class="col-md-2">
                                                                        <label class="control-label ">Court Charge :</label>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <asp:TextBox ID="txtCrtChrgAmt" Enabled="false" Placeholder="Amount" CssClass="form-control" runat="server" />
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <label class="control-label ">Sur Charge :</label>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <asp:TextBox ID="txtSurChrgAmt" Enabled="false" Placeholder="Amount" CssClass="form-control" runat="server" />
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <label class="control-label ">Other Charge :</label>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <asp:TextBox ID="txtOtherChrgAmt" Enabled="false" Placeholder="Amount" CssClass="form-control" runat="server" />
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                                <div class="col-lg-12">
                                                                    <div class="col-md-2">
                                                                        <label class="control-label ">Bank Charge :</label>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <asp:TextBox ID="txtBankChrgAmt" Enabled="false" Placeholder="Amount" CssClass="form-control" runat="server" />
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <label class="control-label ">Insurance :</label>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <asp:TextBox ID="txtInsuranceAmt" Enabled="false" Placeholder="Amount" CssClass="form-control" runat="server" />
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>

                                                    <div style="border: 1px solid #3598dc">
                                                        <div class="portlet-body">
                                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                                <div class="col-lg-12">
                                                                    <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Payment Mode : </strong></div>
                                                                </div>
                                                            </div>

                                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                                <div class="col-lg-12">
                                                                    <div class="col-md-2">
                                                                        <label class="control-label ">Payment Type :<span class="required"> *</span></label>
                                                                    </div>
                                                                    <div class="col-md-3">
                                                                        <asp:DropDownList ID="ddlPayType" Enabled="false" runat="server" CssClass="form-control">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div id="divTransfer" visible="false" runat="server">

                                                                <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                                    <div class="col-lg-12">
                                                                        <div class="col-md-2">
                                                                            <label class="control-label ">Product Code :<span class="required"> *</span></label>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <asp:TextBox ID="txtProdType1" Enabled="false" CssClass="form-control" runat="server" PlaceHolder="Product Type"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-md-5">
                                                                            <asp:TextBox ID="txtProdName1" Enabled="false" CssClass="form-control" PlaceHolder="Product Name" runat="server"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                                    <div class="col-lg-12">
                                                                        <div class="col-md-2">
                                                                            <label class="control-label ">Acc No / Name:<span class="required"> *</span></label>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <asp:TextBox ID="TxtAccNo1" Enabled="false" CssClass="form-control" PlaceHolder="Account Number" runat="server"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-md-5">
                                                                            <asp:TextBox ID="TxtAccName1" Enabled="false" CssClass="form-control" PlaceHolder="Customer Name" runat="server"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div id="divInstrment" visible="false" runat="server">
                                                                <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                                    <div class="col-lg-12">
                                                                        <div class="col-md-2">
                                                                            <label class="control-label ">Instrument No. :</label>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <asp:TextBox ID="TxtInstNo" Enabled="false" placeholder="Instrument Number" CssClass="form-control" runat="server"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-md-2">
                                                                            <label class="control-label ">Instrument Date :</label>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <asp:TextBox ID="TxtInstDate" Enabled="false" placeholder="Instrument Date" CssClass="form-control" runat="server"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div id="Div1" runat="server">
                                                                <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                                    <div class="col-lg-12">
                                                                        <div class="col-md-2">
                                                                            <label class="control-label ">Naration : <span class="required">*</span></label>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <asp:TextBox ID="txtNarration" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                                                        </div>
                                                                        <div id="divPassAmt" visible="false" runat="server">
                                                                            <label class="control-label col-md-2">Pass Amount : </label>
                                                                            <div class="col-md-3">
                                                                                <asp:TextBox ID="txtPassAmount" placeholder="Pass Amount" CssClass="form-control" runat="server" />
                                                                            </div>
                                                                        </div>
                                                                        <div id="divReason" visible="false" runat="server">
                                                                            <label class="control-label col-md-2">Cancel Reason : <span class="required">* </span></label>
                                                                            <div class="col-md-3">
                                                                                <asp:DropDownList ID="ddlReason" CssClass="form-control" runat="server" />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div runat="server" class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                        </div>
                                                    </div>

                                                </asp:TableCell>
                                                <asp:TableCell ID="Tbl_c2" runat="server" Width="30%" BorderStyle="Solid" BorderWidth="1px">

                                                    <asp:Table ID="Tbl_Photo" runat="server">
                                                        <asp:TableRow ID="Rw_Ph1" runat="server">
                                                            <asp:TableCell ID="TblCell1" runat="server">
                                                                <asp:Label ID="Label1" runat="server" Text="Signature" Style="font-weight: bold"></asp:Label>
                                                                <img id="Img1" runat="server" style="height: 80%; width: 100%; border: 1px solid #000000; padding: 5px" />
                                                                <asp:Label ID="Label2" runat="server" Text="Photo" Style="font-weight: bold"></asp:Label>
                                                                <img id="Img2" runat="server" style="height: 80%; width: 100%; border: 1px solid #000000; padding: 5px" />
                                                            </asp:TableCell>
                                                        </asp:TableRow>
                                                    </asp:Table>

                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </div>
                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9">
                                        <asp:Button ID="btnAuthorize" Visible="false" runat="server" Text="Authorize" CssClass="btn btn-primary" OnClick="btnAuthorize_Click" />
                                        <asp:Button ID="btnCancel" Visible="false" runat="server" Text="Cancel Voucher" CssClass="btn btn-primary" OnClick="btnCancel_Click" />
                                        <asp:Button ID="btnExit" runat="server" Text="Exit" CssClass="btn btn-primary" OnClick="btnExit_Click" />
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

    <div id="alertModal" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button><center><h4 class="modal-title" style="color:#ff0000">AVS CORE</h4></center>
                </div>
                <div class="modal-body">
                    <p>
                        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                    </p>
                </div>
                <div class="modal-footer">
                    <button id="btnClose" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdnamount" runat="server" />
</asp:Content>

