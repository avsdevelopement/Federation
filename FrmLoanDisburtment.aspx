<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmLoanDisburtment.aspx.cs" Inherits="FrmLoanDisburtment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;
            return true;
        }
    </script>

    <script type="text/javascript">
        //For Check Max Sanction Limit
        function SancLimit(obj) {
            debugger;
            var LimitAmt = document.getElementById('<%=txtLimitAmt.ClientID%>').value;
            var DisbAmt = document.getElementById('<%=TxtDSAmt.ClientID%>').value;
            var ClearAmt = document.getElementById('<%=TxtTotalBal.ClientID%>').value;
            var MaxLimit = document.getElementById('<%=hdnLimitAmt.ClientID%>').value;

            var Sum = parseFloat(DisbAmt) + parseFloat(Math.abs(ClearAmt))
            if (parseFloat(Sum) > parseFloat(LimitAmt)) {
                message = 'Not Allow to Exceed Sanction Amount greater than ' + LimitAmt + ' ...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=TxtDSAmt.ClientID %>').focus();
                ClearLimit();
                return false;
            }
        }

        function ClearLimit() {
            document.getElementById("<%=TxtDSAmt.ClientID %>").value = document.getElementById('<%=hdnLimitAmt.ClientID%>').value;
        }

    </script>

    <script type="text/javascript">
        function IsValidate1() {
            //debugger;
            var PrCode = document.getElementById('<%=TxtPtype.ClientID%>').value;
            var AccNo = document.getElementById('<%=TxtAccNo.ClientID%>').value;

            if (PrCode == "") {
                alert("Enter product code first...!!")
                document.getElementById('<%=TxtPtype.ClientID%>').focus();
                return false;
            }

            if (AccNo == "") {
                alert("Enter account number first....!!")
                document.getElementById('<%=TxtAccNo.ClientID%>').focus();
                return false;
            }
        }

        function IsValidate2() {
            //debugger;
            var PayMode = document.getElementById('<%=ddlPayType.ClientID%>').value;
            var DisAmount = document.getElementById('<%=txtAmount.ClientID%>').value;
            var DisAmount = document.getElementById('<%=txtDiffAmount.ClientID%>').value

            if (PayMode == "0") {
                alert("Select payment mode first...!!")
                document.getElementById('<%=ddlPayType.ClientID%>').focus();
                return false;
            }

            if (DisAmount == "") {
                alert("Enter amount first...!!")
                document.getElementById('<%=txtAmount.ClientID%>').focus();
                return false;
            }
        }

        function IsValidate3() {
            debugger;
            var DisAmount = document.getElementById('<%=txtAmount.ClientID%>').value || 0;
            var DrBal = document.getElementById('<%=txtDrAmount.ClientID%>').value || 0;
            var CrBal = document.getElementById('<%=txtCrAmount.ClientID%>').value || 0;
            var Sum = (parseFloat(DrBal) - parseFloat(CrBal));

            if (parseFloat(DisAmount) > parseFloat(Sum)) {
                alert("Enter proper amount first...!!")
                document.getElementById('<%=txtAmount.ClientID%>').value = Sum;
                document.getElementById('<%=txtAmount.ClientID%>').focus();
                return false;
            }
        }

        function IsValidate4() {
            //debugger;
            var PrCode = document.getElementById('<%=TxtPtype.ClientID%>').value;
            var AccNo = document.getElementById('<%=TxtAccNo.ClientID%>').value;
            var DiffAmt = document.getElementById('<%=txtDiffAmount.ClientID%>').value;

            if (PrCode == "") {
                alert("Enter product code first...!!")
                document.getElementById('<%=TxtPtype.ClientID%>').focus();
                return false;
            }

            if (AccNo == "") {
                alert("Enter account number first....!!")
                document.getElementById('<%=TxtAccNo.ClientID%>').focus();
                return false;
            }

            if (DiffAmt != "0") {
                alert("Amount Difference in Credit and Debit Transaction...!!")
                document.getElementById('<%=txtAmount.ClientID%>').focus();
                return false;
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        LOAN DISBURSEMENT
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">

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
                                                        <label class="control-label ">Product Code :<span class="required">*</span></label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtPtype" runat="server" onkeypress="javascript:return isNumber (event)" PlaceHolder="Product Code" CssClass="form-control" OnTextChanged="TxtPtype_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="input-icon">
                                                            <i class="fa fa-search"></i>
                                                            <asp:TextBox ID="TxtPname" runat="server" PlaceHolder="Search Product Name" CssClass="form-control" AutoPostBack="true" OnTextChanged="TxtPname_TextChanged"></asp:TextBox>
                                                            <div id="CustList0" style="height: 200px; overflow-y: scroll;"></div>
                                                            <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="TxtPname" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" ServiceMethod="GetGlName" CompletionListElementID="CustList0">
                                                            </asp:AutoCompleteExtender>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Account No :<span class="required"> *</span></label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtAccNo" runat="server" PlaceHolder="Account No" onkeypress="javascript:return isNumber (event)" CssClass="form-control" OnTextChanged="TxtAccNo_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="input-icon">
                                                            <i class="fa fa-search"></i>
                                                            <asp:TextBox ID="TxtCustName" runat="server" PlaceHolder="Search Customer Name" CssClass="form-control" AutoPostBack="true" OnTextChanged="TxtCustName_TextChanged"></asp:TextBox>
                                                            <div id="CustList1" style="height: 200px; overflow-y: scroll;"></div>
                                                            <asp:AutoCompleteExtender ID="AutoAccname" runat="server" TargetControlID="TxtCustName" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" ServiceMethod="GetAccName" CompletionListElementID="CustList1">
                                                            </asp:AutoCompleteExtender>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Customer Number </label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtCustNo1" Enabled="false" CssClass="form-control" PlaceHolder="Customer No" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Sanction Limit :<span class="required"> *</span></label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtLimitAmt" Enabled="false" Placeholder="Total Sanction Amount" CssClass="form-control" runat="server" />
                                                        <input type="hidden" id="hdnLimitAmt" runat="server" value="" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Int Rate :</label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtIntRate" Enabled="false" Placeholder="Rate Of Interest" runat="server" CssClass="form-control" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Sanction Date  :</label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtAccOpenDate" Enabled="false" onkeyup="FormatIt(this);" onkeypress="javascript:return isNumber(event)" placeholder="DD/MM/YYYY" CssClass="form-control" runat="server"></asp:TextBox>
                                                        <asp:CalendarExtender ID="txtAccOpenDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtAccOpenDate">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Installment Amount :</label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtInstAmt" Enabled="false" Placeholder="Installment Amount" runat="server" CssClass="form-control" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Total Loan Period :</label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtPeriod" Enabled="false" Placeholder="Period" runat="server" CssClass="form-control" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Due Date :</label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtDueDate" Enabled="false" onkeyup="FormatIt(this);" onkeypress="javascript:return isNumber(event)" placeholder="DD/MM/YYYY" CssClass="form-control" runat="server"></asp:TextBox>
                                                        <asp:CalendarExtender ID="txtDueDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtDueDate">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Clear Balance :<span class="required"> *</span></label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtBalance" runat="server" Placeholder="Clear Balance" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Total Balance :<span class="required"> *</span></label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtTotalBal" runat="server" Placeholder="Un-Clear Balance" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Disbursement Amt :<span class="required"> *</span></label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtDSAmt" onkeyup="SancLimit()" runat="server" PlaceHolder="Disburstment Amount" onkeypress="javascript:return isNumber (event)" CssClass="form-control" OnTextChanged="TxtDSAmt_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Debit Amount</label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtDrAmount" Enabled="false" PlaceHolder="Debit Amount" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Credit Amount</label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtCrAmount" Enabled="false" PlaceHolder="Credit Amount" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Difference Amount </label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtDiffAmount" Enabled="false" PlaceHolder="Difference Amount" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                    <div id="DivPayment" visible="false" runat="server" style="border: 1px solid #3598dc">
                                        <div class="portlet-body">
                                            <div class="row" style="margin-top: 5px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Payment Mode : </strong></div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Payment Type :<span class="required"> *</span></label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:DropDownList ID="ddlPayType" OnSelectedIndexChanged="ddlPayType_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div id="Transfer" visible="false" runat="server">
                                                <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label ">Product Code :<span class="required"> *</span></label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtProdType1" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtProdType1_TextChanged" PlaceHolder="Product Type"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtProdName1" CssClass="form-control" PlaceHolder="Product Name" OnTextChanged="txtProdName1_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                                            <div id="CustList2" style="height: 200px; overflow-y: scroll;"></div>
                                                            <asp:AutoCompleteExtender ID="autoglname1" runat="server" TargetControlID="txtProdName1" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" ServiceMethod="GetGlName" CompletionListElementID="CustList2">
                                                            </asp:AutoCompleteExtender>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label ">Acc No / Name:<span class="required"> *</span></label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtAccNo1" CssClass="form-control" PlaceHolder="ID" runat="server" AutoPostBack="true" OnTextChanged="TxtAccNo1_TextChanged"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="TxtAccName1" CssClass="form-control" PlaceHolder="Customer Name" runat="server" AutoPostBack="true" OnTextChanged="TxtAccName1_TextChanged"></asp:TextBox>
                                                            <div id="CustList3" style="height: 200px; overflow-y: scroll;"></div>
                                                            <asp:AutoCompleteExtender ID="AutoAccname1" runat="server" TargetControlID="TxtAccName1" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" ServiceMethod="GetAccName" CompletionListElementID="CustList3">
                                                            </asp:AutoCompleteExtender>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <label class="control-label ">Customer Number </label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtCustNo2" Enabled="false" CssClass="form-control" PlaceHolder="Customer No" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div id="Transfer1" visible="false" runat="server">
                                                <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label ">Instrument No. :</label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="TxtChequeNo" placeholder="CHEQUE NUMBER" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <label class="control-label ">Instrument Date :</label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="TxtChequeDate" placeholder="CHEQUE DATE" CssClass="form-control" onkeyup="FormatIt(this)" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div id="DivAmount" visible="false" runat="server">
                                                <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label ">Naration : <span class="required">*</span></label>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:TextBox ID="txtNarration" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <label class="control-label ">Disburment Amt : <span class="required">*</span></label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtAmount" placeholder="DEBIT AMOUNT" onblur="IsValidate3()" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClientClick="Javascript:return IsValidate1(); IsValidate2();" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnPost" Enabled="false" runat="server" Text="Post" CssClass="btn btn-success" OnClientClick="Javascript:return IsValidate4();" OnClick="btnPost_Click" />
                                        <asp:Button ID="BtnReceipt" runat="server" Text="Receipt" CssClass="btn btn-success" OnClick="BtnReceipt_Click" />
                                        <asp:Button ID="btnClear" runat="server" CssClass="btn blue" Text="ClearAll" OnClick="btnClear_Click" />
                                        <asp:Button ID="Exit" runat="server" CssClass="btn blue" Text="Exit" OnClick="Exit_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-12">
                    </div>
                    <div class="table-scrollable" style="width: 100%; height: auto; max-height: 150px; overflow-x: auto; overflow-y: auto">
                        <asp:GridView ID="grdvoucher" runat="server" AlternatingRowStyle-CssClass="alt" CssClass="mGrid" AutoGenerateColumns="false"
                            EditRowStyle-BackColor="#FFFF99" DataKeyNames="id" Width="100%" EmptyDataText="No FD Available for this Customer">
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
                    </div>

                    <div class="col-md-12">
                    </div>
                    <div class="table-scrollable" style="width: 100%; height: auto; max-height: 150px; overflow-x: auto; overflow-y: auto">
                        <asp:GridView ID="GrsLoanInfo" runat="server" AllowPaging="True"
                            AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                            EditRowStyle-BackColor="#FFFF99"
                            OnPageIndexChanging="GrsLoanInfo_PageIndexChanging"
                            PageIndex="10" PageSize="25"
                            PagerStyle-CssClass="pgr" Width="100%">
                            <Columns>

                                <asp:TemplateField HeaderText="Product Name">
                                    <ItemTemplate>
                                        <asp:Label ID="PARTI" runat="server" Text='<%# Eval("GLNAME") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Account No" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="EDATE" runat="server" Text='<%# Eval("ACCNO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Account Name" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="Accname" runat="server" Text='<%# Eval("ACCName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="INSTNO" runat="server" Text='<%# Eval("AMT") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="pgr" />
                            <SelectedRowStyle BackColor="#66FF99" />
                            <EditRowStyle BackColor="#FFFF99" />
                            <AlternatingRowStyle CssClass="alt" />
                        </asp:GridView>
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
    </div>
    <%--Added by ankita on 26/06/2017--%>
    <div class="row">
        <div class="col-lg-12">
            <div class="table-scrollable">
                <table class="table table-striped table-bordered table-hover">
                    <thead>
                        <tr>
                            <th>
                                <asp:GridView ID="grdCashRct" runat="server" AllowPaging="True"
                                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                    EditRowStyle-BackColor="#FFFF99"
                                    OnPageIndexChanging="grdOwgData_PageIndexChanging" OnSelectedIndexChanged="grdCashRct_SelectedIndexChanged"
                                    PagerStyle-CssClass="pgr" Width="100%">
                                    <Columns>

                                        <asp:TemplateField HeaderText="VOUCHER NO." Visible="true">
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

                                        <asp:TemplateField HeaderText="INSTRUMENT NO" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="Instno" runat="server" Text='<%# Eval("INSTRUMENTNO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="INSTRUMENT DATE" Visible="true">
                                            <ItemTemplate>

                                                <asp:Label ID="InstDate" runat="server" Text='<%# Eval("INSTRUMENTDATE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MAKER" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="MAKER" runat="server" Text='<%# Eval("MAKER") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Voucher" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkPrintReceipt" runat="server" CommandName="select" class="glyphicon glyphicon-plus" OnClick="LnkPrintReceipt_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Dens" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDens" runat="server" OnClick="lnkDens_Click" CommandArgument='<%#Eval("Dens")%>' CommandName="select" class="glyphicon glyphicon-plus"></asp:LinkButton>
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
</asp:Content>

