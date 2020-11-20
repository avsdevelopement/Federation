<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAuthorizeCP.aspx.cs" Inherits="FrmAuthorizeCP" %>

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
                        Authorize Screen For Cash Payment
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

                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-2">Product Type : </label>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtProdType" Enabled="false" CssClass="form-control" runat="server" />
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtProdName" Enabled="false" CssClass="form-control" runat="server" />
                                                            </div>
                                                            <label class="control-label col-md-2">Entry Date : </label>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtEntryDate" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-2">A/C Number : </label>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtAccNo" Enabled="false" CssClass="form-control" runat="server" />
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtAccName" Enabled="false" CssClass="form-control" runat="server" />
                                                            </div>
                                                            <label class="control-label col-md-2">Customer : </label>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtCustNo" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-2">A/C Type : </label>
                                                            <div class="col-md-3">
                                                                <asp:TextBox ID="txtAccType" Enabled="false" CssClass="form-control" runat="server" />
                                                            </div>
                                                            <label class="control-label col-md-2" runat="server" visible="false" id="lblJoint">Joint Name </label>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtJointName" Enabled="false" CssClass="form-control" Visible="false" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-2">Spl Inst : </label>
                                                            <div class="col-md-5">
                                                                <asp:TextBox ID="txtspclInst" Enabled="false" CssClass="form-control" runat="server" TextMode="MultiLine" />
                                                            </div>
                                                            <label class="control-label col-md-2">PAN Card : </label>
                                                            <div class="col-md-3">
                                                                <asp:TextBox ID="txtPanCardNo" Enabled="false" CssClass="form-control" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-2">Voucher Type : </label>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtVTypeNo" Enabled="false" CssClass="form-control" runat="server" ></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtVTypeName" Enabled="false" CssClass="form-control" runat="server" ></asp:TextBox>
                                                            </div>
                                                            <label class="control-label col-md-2">Token No : </label>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtTokenNo" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div id="divInstrument" visible="false" runat="server" class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-2">Instrument No : </label>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtInstNo" Enabled="false" CssClass="form-control" runat="server" Text="By Cash" />
                                                            </div>
                                                            <label class="control-label col-md-2">Instrument Date : </label>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtInstDate" Enabled="false" CssClass="form-control" runat="server" Text="By Cash" />
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-2">Narration1 : </label>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtNarration1" Enabled="false" CssClass="form-control" runat="server" />
                                                            </div>
                                                            <label class="control-label col-md-2">Narration2 : </label>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtNarration2" Enabled="false" CssClass="form-control" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-2">Clear Balance : </label>
                                                            <div class="col-md-3">
                                                                <asp:TextBox ID="txtClearBal" Enabled="false" CssClass="form-control" runat="server" />
                                                            </div>
                                                            <label class="control-label col-md-2">Total Balance : </label>
                                                            <div class="col-md-3">
                                                                <asp:TextBox ID="txtTotalBal" Enabled="false" CssClass="form-control" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div id="divPassAmt" visible="false" runat="server" class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-2">Amount : </label>
                                                            <div class="col-md-3">
                                                                <asp:TextBox ID="txtDrAmount" Enabled="false" CssClass="form-control" runat="server" />
                                                            </div>
                                                            <label class="control-label col-md-2">Pass Amount : </label>
                                                            <div class="col-md-3">
                                                                <asp:TextBox ID="txtPassAmount" placeholder="Pass Amount" CssClass="form-control" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div id="divReason" visible="false" runat="server" class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-2">Cancel Reason : <span class="required">* </span></label>
                                                            <div class="col-md-2">
                                                                <asp:DropDownList ID="ddlReason" CssClass="form-control" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>

                                                </asp:TableCell><asp:TableCell ID="Tbl_c2" runat="server" Width="30%" BorderStyle="Solid" BorderWidth="1px">

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

                                                </asp:TableCell></asp:TableRow></asp:Table></div></div></div><div class="form-actions">
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
                                                <asp:Label ID="lblEDate" runat="server" Text='<%# Eval("EDATE","{0:dd/MM/yyyy}") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="SetNo" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSetNo" runat="server" Text='<%# Eval("SetNo") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Particulars1">
                                            <ItemTemplate>
                                                <asp:Label ID="lblParticular" runat="server" Text='<%# Eval("PARTI") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Particulars2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblparticular2" runat="server" Text='<%# Eval("PARTI1") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Cheque/Refrence">
                                            <ItemTemplate>
                                                <asp:Label ID="lblInstNo" runat="server" Text='<%# Eval("INSTNO") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Credit" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCredit" runat="server" Text='<%# Eval("CREDIT") %>'></asp:Label></ItemTemplate><ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="DEBIT">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDebit" runat="server" Text='<%# Eval("DEBIT") %>'></asp:Label></ItemTemplate><ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="BALANCE">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBalance" runat="server" Text='<%# Eval("BALANCE") %>'></asp:Label></ItemTemplate><ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Dr/Cr">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDrCrIndicator" runat="server" Text='<%# Eval("DrCr") %>'></asp:Label></ItemTemplate><ItemStyle CssClass="Th" HorizontalAlign="Left" />
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
                    <button type="button" class="close" data-dismiss="modal">&times;</button><center><h4 class="modal-title" style="color:#ff0000">AVS CORE</h4></center></div><div class="modal-body">
                    <p>
                        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label></p></div><div class="modal-footer">
                    <button id="btnClose" class="btn btn-default" data-dismiss="modal">Close</button></div></div></div></div></asp:Content>