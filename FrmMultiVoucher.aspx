<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmMultiVoucher.aspx.cs" Inherits="FrmMultiVoucher" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46) {
                return false;
            }
            return true;
        }
    </script>
    <script type="text/javascript">
        function FormatIt(obj) {
            if (obj.value.length == 2)
                obj.value = obj.value + "/";
            if (obj.value.length == 5)
                obj.value = obj.value + "/";
            if (obj.value.length == 11)
                alert("Please enter valid date");
        }

    </script>
    <script type="text/javascript">
        function isvalidate() {
            var DdlCRDR = document.getElementById('<%=DdlCRDR.ClientID%>').value;
            var TxtPtype = document.getElementById('<%=TxtPtype.ClientID%>').value;
            var TxtAccNo = document.getElementById('<%=TxtAccNo.ClientID%>').value;
            var GlCode = document.getElementById('<%=hdfGlCode.ClientID%>').value;
            var TxtChequeNo = document.getElementById('<%=TxtChequeNo.ClientID%>').value;
            var TxtChequeDate = document.getElementById('<%=TxtChequeDate.ClientID%>').value;
            var TxtNarration = document.getElementById('<%=TxtNarration.ClientID%>').value;
            var TxtAmount = document.getElementById('<%=TxtAmount.ClientID%>').value;
            var Len = parseInt(TxtChequeNo.length);
            debugger;

            if (DdlCRDR == "0") {
                var message = 'Select Transaction Type...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=DdlCRDR.ClientID%>').focus();
                return false;
            }

            if (TxtPtype == "") {
                var message = 'Enter product Type...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtPtype.ClientID%>').focus();
                return false;
            }

            if (TxtAccNo == "") {
                var message = 'Enter Account Number...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtAccNo.ClientID%>').focus();
                return false;
            }

            if (GlCode > 99) {
                if (TxtChequeNo == "") {
                    var message = 'Enter Cheque Number...!!\n';
                    $('#alertModal').find('.modal-body p').text(message);
                    $('#alertModal').modal('show')
                    document.getElementById('<%=TxtChequeNo.ClientID%>').focus();
                    return false;
                }

                if (TxtChequeDate == "") {
                    var message = 'Enter Cheque Date...!!\n';
                    $('#alertModal').find('.modal-body p').text(message);
                    $('#alertModal').modal('show')
                    document.getElementById('<%=TxtChequeDate.ClientID%>').focus();
                    return false;
                }
            }

            if (TxtAmount == "") {
                var message = 'Enter Amount First...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtAmount.ClientID%>').focus();
                return false;
            }
        }
    </script>
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
                            <div class="tab-pane active" id="tab__blue">

                                <div class="row" style="margin: 7px 0 7px 0; margin-top: 20px;">
                                    <div class="col-lg-12">
                                        <label class="control-label col-md-2">Transfer Type :</label>
                                        <div class="col-md-10">
                                            <asp:RadioButtonList ID="rbtnTransferType" OnSelectedIndexChanged="rbtnTransferType_SelectedIndexChanged" AutoPostBack="true" RepeatDirection="Horizontal" runat="server" TabIndex="1">
                                                <asp:ListItem Text="Transfer" Selected="True" Value="T" />
                                                <asp:ListItem Text="Cheque" Value="C" />
                                            </asp:RadioButtonList>

                                        </div>
                                    </div>
                                </div>

                                <div class="row" style="margin: 7px 0 7px 0; margin-top: 20px;">
                                    <div class="col-lg-12">
                                        <label class="control-label col-md-2">Transaction Type :</label>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="DdlCRDR" CssClass="form-control" OnSelectedIndexChanged="DdlCRDR_SelectedIndexChanged" AutoPostBack="true" runat="server" TabIndex="3">
                                                <asp:ListItem Value="0">-- Select --</asp:ListItem>
                                                <asp:ListItem Value="1">Credit</asp:ListItem>
                                                <asp:ListItem Value="2">Debit</asp:ListItem>
                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                </div>

                                <div class="row" style="margin: 7px 0 7px 0; margin-top: 20px;">
                                    <div class="col-lg-12">
                                        <label class="control-label col-md-2">Credit Amount :</label>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="TxtCRBAL" CssClass="form-control" runat="server" PlaceHolder="Credit" Enabled="false"></asp:TextBox>
                                        </div>
                                        <label class="control-label col-md-2">Debit Amount :</label>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="TxtDRBAL" CssClass="form-control" onkeyup="FormatIt(this)" runat="server" PlaceHolder="Debit" Enabled="false"></asp:TextBox>
                                        </div>
                                        <label class="control-label col-md-2">Diffrence Amount :</label>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="TxtDiff" CssClass="form-control" onkeyup="FormatIt(this)" runat="server" PlaceHolder="Diffrence Amount" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="row" style="margin: 7px 0 7px 0; margin-top: 20px;">
                                    <div class="col-lg-12">
                                        <label class="control-label col-md-2">Product Code : <span class="required">* </span></label>
                                        <div class="col-md-6">
                                            <asp:TextBox ID="TxtPtype" runat="server" onkeypress="javascript:return isNumber (event)" PlaceHolder="Product Code" Style="Width: 20%; height: 33px; border: 1px solid #c0c1c1;" OnTextChanged="TxtPtype_TextChanged" AutoPostBack="true" TabIndex="5"></asp:TextBox>&nbsp;&nbsp;
                                            <asp:TextBox ID="TxtPname" runat="server" PlaceHolder="Product Name" OnTextChanged="TxtPname_TextChanged" Style="width: 61%; height: 33px; margin-left: -3px; border: 1px solid #c0c1c1;" AutoPostBack="true" TabIndex="6"></asp:TextBox>
                                            <div id="CustList" style="height: 200px; overflow-y: scroll;"></div>
                                            <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="TxtPname"
                                                UseContextKey="true"
                                                CompletionInterval="1"
                                                CompletionSetCount="20"
                                                MinimumPrefixLength="1"
                                                EnableCaching="true"
                                                ServicePath="~/WebServices/Contact.asmx"
                                                ServiceMethod="GetGlName" CompletionListElementID="CustList">
                                            </asp:AutoCompleteExtender>
                                        </div>
                                        <input type="hidden" id="hdfGlCode" runat="server" value="" />
                                        <div class="control-label col-md-2">
                                            <label class="control-label ">CustNo</label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="txtCustNo" CssClass="form-control" Enabled="false" runat="server" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="margin: 7px 0 7px 0; margin-top: 20px;">
                                    <div class="col-lg-12">
                                        <label class="control-label col-md-2">Account No : <span class="required">* </span></label>
                                        <div class="col-md-6">
                                            <asp:TextBox ID="TxtAccNo" runat="server" PlaceHolder="Account No" onkeypress="javascript:return isNumber (event)" Style="Width: 20%; height: 33px; border: 1px solid #c0c1c1;" OnTextChanged="TxtAccNo_TextChanged" AutoPostBack="true" TabIndex="7"></asp:TextBox>&nbsp;&nbsp;
                                            <asp:TextBox ID="TxtCustName" runat="server" PlaceHolder="Account Holder Name" Style="width: 61%; height: 33px; margin-left: -3px; border: 1px solid #c0c1c1;" OnTextChanged="TxtCustName_TextChanged" AutoPostBack="true" TabIndex="8"></asp:TextBox>
                                            <div id="CustList2" style="height: 200px; overflow-y: scroll;"></div>
                                            <asp:AutoCompleteExtender ID="AutoAccname" runat="server" TargetControlID="TxtCustName"
                                                UseContextKey="true"
                                                CompletionInterval="1"
                                                CompletionSetCount="20"
                                                MinimumPrefixLength="1"
                                                EnableCaching="true"
                                                ServicePath="~/WebServices/Contact.asmx"
                                                ServiceMethod="GetAccName" CompletionListElementID="CustList2">
                                            </asp:AutoCompleteExtender>
                                            <asp:LinkButton ID="LnkVerify" runat="server" OnClick="LnkVerify_Click">Verify Signature</asp:LinkButton>
                                        </div>
                                        <label class="control-label col-md-2">PAN Card: </label>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="txtPan" placeholder="PAN Card" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="margin: 7px 0 7px 0">

                                    <div class="col-lg-12">
                                        <label class="control-label col-md-2">Mobile 1 </label>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="TxtMobile1" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
                                        <label class="control-label col-md-2">Mobile 2 </label>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="TxtMobile2" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                        </div>

                                    </div>

                                </div>
                                <div class="row" style="margin: 7px 0 7px 0; margin-top: 20px;">
                                    <div class="col-lg-12">
                                        <label class="control-label col-md-2">Balance : </label>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="TxtBalance" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                        <label class="control-label col-md-2">Total Balance : </label>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="TxtTotalBal" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                        <label class="control-label col-md-2">Amount :<span class="required">* </span></label>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="TxtAmount" CssClass="form-control" runat="server" PlaceHolder="Amount" onkeypress="javascript:return isNumber (event)" OnTextChanged="TxtAmount_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        </div>

                                    </div>
                                </div>

                                <div id="divCheque" runat="server" visible="false">
                                    <div class="row" style="margin: 7px 0 7px 0; margin-top: 20px;">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Instrument No. :</label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="TxtChequeNo" CssClass="form-control" runat="server" PlaceHolder="Instrument No" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                            </div>
                                            <label class="control-label col-md-2">Instrument Date :</label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="TxtChequeDate" CssClass="form-control" onkeyup="FormatIt(this)" runat="server" PlaceHolder="Instrument Date" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row" style="margin: 7px 0 7px 0; margin-top: 20px;">
                                    <div class="col-lg-12">
                                        <label class="control-label col-md-2">Naration :</label>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="TxtNarration" CssClass="form-control" runat="server" PlaceHolder="Narration"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2"></div>
                                        <div class="col-md-4">
                                            <asp:Table ID="Tbl_Photo" runat="server">
                                                <asp:TableRow ID="Rw_Ph1" runat="server">
                                                    <asp:TableCell ID="TblCell1" runat="server">
                                                        <asp:Label ID="Label3" runat="server" Text="Signature" Style="font-weight: bold"></asp:Label>
                                                        <img id="Img1" runat="server" style="height: 50%; width: 95%; border: 1px solid #000000; padding: 5px" />
                                                    </asp:TableCell>
                                                    <asp:TableCell ID="TblCell2" runat="server">
                                                        <asp:Label ID="Label4" runat="server" Text="Photo" Style="font-weight: bold"></asp:Label>
                                                        <img id="Img2" runat="server" style="height: 50%; width: 100%; border: 1px solid #000000; padding: 5px" />
                                                    </asp:TableCell>
                                                </asp:TableRow>

                                            </asp:Table>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-12" style="text-align: center">
                                        <asp:Button ID="BtnMobUpld" runat="server" CssClass="btn blue" Text="Mobile Update" OnClick="BtnMobUpld_Click" />
                                        <asp:Button ID="Submit" runat="server" Text="Add" CssClass="btn btn-primary" OnClientClick="javascript:return isvalidate()" OnClick="Submit_Click" TabIndex="13" />
                                        <asp:Button ID="btnClear" runat="server" Text="ClearAll" CssClass="btn btn-primary" OnClick="btnClear_Click" TabIndex="14" />
                                        <asp:Button ID="btnPost" Enabled="false" runat="server" Text="Post" CssClass="btn btn-success" OnClick="btnPost_Click" TabIndex="15" />
                                        <asp:Button ID="Exit" runat="server" Text="Exit" CssClass="btn btn-primary" OnClick="Exit_Click" TabIndex="16" />
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
                        <thead>
                            <tr>
                                <th>
                                    <asp:GridView ID="grdvoucher" runat="server" AllowPaging="True" OnRowDataBound="grdvoucher_RowDataBound"
                                        AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                        EditRowStyle-BackColor="#FFFF99"
                                        OnPageIndexChanging="grdvoucher_PageIndexChanging"
                                        PagerStyle-CssClass="pgr" Width="100%">
                                        <Columns>

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

                                            <asp:TemplateField HeaderText="Delete" Visible="true">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbtnDelete" runat="server" OnClick="lnkbtnDelete_Click" CommandArgument='<%#Eval("ID")%>' CommandName="select" class="glyphicon glyphicon-trash"></asp:LinkButton>
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
        <div class="col-lg-12">
            <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #0A23F9">Transaction Details  : </strong></div>
        </div>
        <div class="row">
        <div class="col-lg-12">
            <div class="table-scrollable">
                <table class="table table-striped table-bordered table-hover">
                    <thead>
                        <tr>
                            <th>
                                <asp:GridView ID="grdMultiRct" runat="server" AllowPaging="True"
                                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                    EditRowStyle-BackColor="#FFFF99" OnSelectedIndexChanged= "grdMultiRct_SelectedIndexChanged"
                                    OnPageIndexChanging= "grdMultiRct_PageIndexChanging"
                                    PagerStyle-CssClass="pgr" Width="100%">
                                    <Columns>

                                        <asp:TemplateField HeaderText="VOUCHER NO" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="SET_NO" runat="server" Text='<%# Eval("SETNO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="PRODUCT TYPE" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="AT" runat="server" Text='<%# Eval("AT") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="ACC No" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="ACNO" runat="server" Text='<%# Eval("ACNO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="CUST NAME" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="Name" runat="server" Text='<%# Eval("CUSTNAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="AMOUNT" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="Amount" runat="server" Text='<%# Eval("AMOUNT") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="NARRATION" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="Particulars" runat="server" Text='<%# Eval("PARTICULARS") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="MAKER" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="MAKER" runat="server" Text='<%# Eval("MAKER") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Receipt" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkPrintReceipt" runat="server"  CommandArgument='<%# Eval("SETNO") %>' CommandName="select" class="glyphicon glyphicon-plus" OnClick= "LnkPrintReceipt_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <%--<asp:TemplateField HeaderText="Authorize" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="select" class="glyphicon glyphicon-plus"></asp:LinkButton>                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                                     <%--   <asp:TemplateField HeaderText="Dens" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDens" runat="server" OnClick="lnkDens_Click" CommandArgument='<%#Eval("Dens")%>' CommandName="select" class="glyphicon glyphicon-plus"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
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

    <div id="VOUCHERVIEW" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Account Details Screen</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="portlet box green" id="Div1">
                                <div class="portlet-title">
                                    <div class="caption">
                                        Voucher View
                                    </div>
                                </div>
                                <div class="portlet-body form">
                                    <div class="form-horizontal">
                                        <div class="form-wizard">
                                            <div class="form-body">
                                                <div class="tab-content">
                                                    <div class="tab-pane active" id="Div2">
                                                        <div class="row" style="margin-bottom: 10px;">
                                                            <div class="row" style="margin: 7px 0 7px 0">
                                                                <div class="col-lg-12" style="height: 50%">
                                                                    <div class="table-scrollable" style="height: 350px; overflow-x: scroll; overflow-y: scroll; padding-bottom: 10px;">
                                                                        <asp:GridView ID="GrdView" runat="server" AutoGenerateColumns="false" OnRowDataBound="GrdView_RowDataBound">
                                                                            <Columns>

                                                                                <asp:TemplateField HeaderText="VOUCHER NO " Visible="true">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="SETNO" runat="server" Text='<%# Eval("SETNO") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="ON DATE " Visible="true">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="ENTRYDATE" runat="server" Text='<%# Eval("ENTRYDATE") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="SUBGLCODE " HeaderText="Product Code" />
                                                                                <asp:BoundField DataField="ACCNO " HeaderText="A/C No" />
                                                                                <asp:BoundField DataField="CUSTNAME " HeaderText="Name" />
                                                                                <asp:BoundField DataField="PARTICULARS " HeaderText="Particulars" />

                                                                                <asp:TemplateField HeaderText="AMOUNT " Visible="true">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="AMOUNT" runat="server" Text='<%# Eval("AMOUNT") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="TYPE " Visible="true">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="TYPE" runat="server" Text='<%# Eval("TYPE") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="ACTIVITY " Visible="true">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="ACTIVITY" runat="server" Text='<%# Eval("ACTIVITY") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:BoundField DataField="BRCD " HeaderText="Br. Code" />
                                                                                <asp:BoundField DataField="STAGE " HeaderText="Status" />
                                                                                <asp:BoundField DataField="LOGINCODE " HeaderText="User Code" />
                                                                                <asp:BoundField DataField="MID " HeaderText="Maker ID" />
                                                                                <asp:BoundField DataField="CID " HeaderText="Checker ID" />
                                                                            </Columns>
                                                                            <PagerStyle CssClass="pgr"></PagerStyle>
                                                                            <SelectedRowStyle BackColor="#66FF99" />
                                                                            <EditRowStyle BackColor="#FFFF99"></EditRowStyle>
                                                                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                                                        </asp:GridView>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>

                                                </div>
                                                <div class="row">

                                                    <div class="col-md-6">

                                                        <asp:Button ID="Btn_Exit" runat="server" Text="Exit" CssClass="btn btn-primary" data-dismiss="modal" />
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
    </div>
       <div id="CNTCT" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg" style="width: 50%">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title" style="text-align: center; font: 100; font-family: Verdana; font-size: larger; font-style: italic">Contact Add Screen</h4>
                </div>
                <div class="modal-body">
                    <div class="row">

                        <div class="col-md-12">

                            <div class="portlet box blue">
                                <div class="portlet-title">
                                    <div class="caption">
                                        Contact Details
                                    </div>
                                </div>
                                <div class="portlet-body form">
                                    <!--<form action="#" class="form-horizontal" id="submit_form" method="POST">-->
                                    <div class="form-horizontal">
                                        <div class="form-wizard">
                                            <div class="form-body">
                                                <table>
                                                    <tr>
                                                        <td style="width: 80%">
                                                            <div class="tab-content">
                                                                <div id="Div3">
                                                                </div>
                                                                <div class="tab-pane active" id="Div4">
                                                                    <asp:Table ID="Table1" runat="server">
                                                                        <asp:TableRow ID="TableRow1" runat="server" Style="width: 300px">
                                                                            <asp:TableCell ID="TableCell1" runat="server" Style="width: 2000px">

                                                                                <div class="row" style="margin: 7px 0 7px 0">
                                                                                    <div class="col-lg-12">
                                                                                        <label class="control-label col-md-2">Customer No :</label>

                                                                                        <div class="col-md-4">
                                                                                            <asp:TextBox ID="TxtCustno1" placeholder="Enter Customer No" CssClass="form-control" onkeypress="javascript:return isNumber (event)" runat="server" Enabled="false"></asp:TextBox>

                                                                                        </div>

                                                                                        <label class="control-label col-md-2">Brcd : </label>
                                                                                        <div class="col-md-4">
                                                                                            <asp:TextBox ID="TxtBrcd1" placeholder="Enter Brcd" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" Enabled="false"></asp:TextBox>
                                                                                        </div>

                                                                                    </div>
                                                                                </div>
                                                                                <div class="row" style="margin: 7px 0 7px 0">
                                                                                    <div class="col-lg-12">
                                                                                        <label class="control-label col-md-2">Mobile No.1 :</label>

                                                                                        <div class="col-md-4">
                                                                                            <asp:TextBox ID="TxtMob1" placeholder="Enter Mobile No" CssClass="form-control" onkeypress="javascript:return isNumber (event)" runat="server" MaxLength="10"></asp:TextBox>

                                                                                        </div>

                                                                                        <label class="control-label col-md-2">Mobile No.2 : </label>
                                                                                        <div class="col-md-4">
                                                                                            <asp:TextBox ID="TxtMob2" placeholder="Enter Mobile No" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" MaxLength="10"></asp:TextBox>
                                                                                        </div>

                                                                                    </div>
                                                                                </div>

                                                                                <div class="row" style="margin: 7px 0 7px 0">
                                                                                    <div class="col-lg-12">
                                                                                        <label class="control-label" style="color: red">SMS to be send on Mobile No.1</label>

                                                                                    </div>
                                                                                </div>
                                                                            </asp:TableCell>


                                                                        </asp:TableRow>
                                                                    </asp:Table>
                                                                </div>

                                                            </div>
                                                        </td>

                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                        <div class="form-actions">
                                            <div class="row">
                                                <div class="col-md-offset-3 col-md-9">
                                                    <asp:Button ID="BtnModlUpdate" OnClick="BtnModlUpdate_Click" runat="server" Text="Submit" CssClass="btn btn-success" />
                                                    <asp:Button ID="BtnModal_CloseCP" Text="Close" runat="server" CssClass="btn btn-success" data-dismiss="modal" />
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <!--</form>-->
                            </div>


                        </div>

                    </div>

                </div>

            </div>
        </div>
    </div>
</asp:Content>

