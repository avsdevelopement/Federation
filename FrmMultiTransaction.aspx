<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmMultiTransaction.aspx.cs" Inherits="FrmMultiTransaction" %>

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

        function FormatIt(obj) {
            if (obj.value.length == 2) // Day
                obj.value = obj.value + "/";
            if (obj.value.length == 5) // month 
                obj.value = obj.value + "/";
            if (obj.value.length == 11) // year 
                alert("Please Enter valid Date");
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

    <div class="row ownformwrap">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1" style="margin-bottom: 0;">
                <div class="portlet-title">
                    <div class="caption">
                        Multiple Transaction
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab__blue">

                                        <div class="row" style="margin: 5px 0;">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Payment Mode :<span class="required"> *</span></label>
                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="ddlPMTMode" CssClass="form-control" OnSelectedIndexChanged="ddlPMTMode_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                        <asp:ListItem Value="0">-- Select --</asp:ListItem>
                                                        <asp:ListItem Value="1">Cash</asp:ListItem>
                                                        <asp:ListItem Value="2">Transfer</asp:ListItem>
                                                        <asp:ListItem Value="3">Cheque</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <label class="control-label col-md-2">Transaction Type :<span class="required"> *</span></label>
                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="DdlCRDR" CssClass="form-control" OnSelectedIndexChanged="DdlCRDR_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 5px 0;">
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

                                        <div class="row" style="margin: 5px 0;">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Product Code : <span class="required">* </span></label>

                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtPtype" runat="server" onkeypress="javascript:return isNumber(event)" PlaceHolder="Product Code" OnTextChanged="TxtPtype_TextChanged" AutoPostBack="true" TabIndex="5" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="TxtPname" runat="server" PlaceHolder="Product Name" OnTextChanged="TxtPname_TextChanged" AutoPostBack="true" TabIndex="6" CssClass="form-control"></asp:TextBox>
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

                                        <div class="row" style="margin: 5px 0;">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Account No : <span class="required">* </span></label>

                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtAccNo" runat="server" PlaceHolder="Account No" onkeypress="javascript:return isNumber(event)" OnTextChanged="TxtAccNo_TextChanged" AutoPostBack="true" TabIndex="7" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="TxtCustName" runat="server" PlaceHolder="Account Holder Name" OnTextChanged="TxtCustName_TextChanged" AutoPostBack="true" TabIndex="8" CssClass="form-control"></asp:TextBox>
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
                                                </div>
                                                <label class="control-label col-md-2">PAN Card: </label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtPan" placeholder="PAN Card" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 5px 0;">
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
                                                    <asp:TextBox ID="TxtAmount" CssClass="form-control" runat="server" PlaceHolder="Amount" onkeypress="javascript:return isNumber(event)" OnTextChanged="TxtAmount_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>

                                        <div id="divCheque" runat="server" visible="false">
                                            <div class="row" style="margin: 5px 0;">
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

                                        <div class="row" style="margin: 5px 0;">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Naration :</label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="TxtNarration" CssClass="form-control" runat="server" PlaceHolder="Narration"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2"></div>
                                                <div class="col-md-4">
                                                    <asp:Table ID="Tbl_Photo" runat="server" Width="100%">
                                                        <asp:TableRow ID="Rw_Ph1" runat="server">
                                                            <asp:TableCell ID="TblCell1" runat="server" CssClass="col-md-2">
                                                                <asp:Label ID="Label3" runat="server" Text="Signature" Style="font-weight: bold"></asp:Label>
                                                                <img id="Img1" runat="server" style="height: 50%; width: 95%; border: 1px solid #bfbfbf; padding: 5px" />
                                                            </asp:TableCell>
                                                            <asp:TableCell ID="TblCell2" runat="server" CssClass="col-md-2">
                                                                <asp:Label ID="Label4" runat="server" Text="Photo" Style="font-weight: bold"></asp:Label>
                                                                <img id="Img2" runat="server" style="height: 50%; width: 100%; border: 1px solid #bfbfbf; padding: 5px" />
                                                            </asp:TableCell>
                                                        </asp:TableRow>

                                                    </asp:Table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9">
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
            <div class="row">
                <div class="col-lg-12">
                    <div class="table-scrollable">
                        <table class="noborder fullwidth">
                            <thead>
                                <tr>
                                    <td>
                                        <asp:GridView ID="grdvoucher" runat="server" AllowPaging="True" OnRowDataBound="grdvoucher_RowDataBound"
                                            AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                            EditRowStyle-BackColor="#FFFF99"
                                            OnPageIndexChanging="grdvoucher_PageIndexChanging"
                                            PagerStyle-CssClass="pgr" CssClass="table table-striped table-bordered table-hover  noborder fullwidth">
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
                                    </td>
                                </tr>
                            </thead>
                        </table>
                    </div>
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

</asp:Content>

