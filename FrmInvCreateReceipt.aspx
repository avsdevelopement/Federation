<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmInvCreateReceipt.aspx.cs" Inherits="FrmInvCreateReceipt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script>
        function IsValid() {
            debugger;
            var txtProdCode = document.getElementById('<%=txtProdCode.ClientID%>').value;
            var txtAccNo = document.getElementById('<%=txtAccNo.ClientID%>').value;
            var ddlIntrestPay = document.getElementById('<%=ddlIntrestPay.ClientID%>').value;
            var txtDepAmt = document.getElementById('<%=txtDepAmt.ClientID%>').value;
            var ddlDuration = document.getElementById('<%=ddlDuration.ClientID%>').value;
            var txtPeriod = document.getElementById('<%=txtPeriod.ClientID%>').value;
            var txtDueDate = document.getElementById('<%=txtDueDate.ClientID%>').value;

            var message = '';

            if (txtProdCode == "") {
                message = 'Please Enter Product Type...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtProdCode.ClientID%>').focus();
                return false;
            }

            if (txtAccNo == "") {
                message = 'Please Enter Account Number...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtAccNo.ClientID%>').focus();
                return false;
            }

            if (ddlIntrestPay == "0") {
                message = 'Please Select Interest PayOut...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=ddlIntrestPay.ClientID%>').focus();
                return false;
            }

            if (txtDepAmt == "") {
                message = 'Please Enter Deposite Amount...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtDepAmt.ClientID%>').focus();
                return false;
            }

            if (ddlDuration == "0") {
                message = 'Please Select Period...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=ddlDuration.ClientID%>').focus();
                return false;
            }

            if (txtPeriod == "") {
                message = 'Please Enter Peroid...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtPeriod.ClientID%>').focus();
                return false;
            }

            if (txtDueDate == "") {
                message = 'Please Enter Due Date...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtDueDate.ClientID%>').focus();
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
                        Investment Receipt 
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="row" style="margin-top: 5px;">
                                    <div class="col-lg-12">
                                        <div class="col-md-2">
                                            <label class="control-label">Product Code : <span class="required">*</span></label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="txtProdCode" CssClass="form-control" Placeholder="Product Code" onkeypress="javascript:return isNumber (event)" runat="server" OnTextChanged="txtProdCode_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-icon">
                                                <i class="fa fa-search"></i>
                                                <asp:TextBox ID="txtProdName" CssClass="form-control" Placeholder="Search Product Name" runat="server" AutoPostBack="true" OnTextChanged="txtProdName_TextChanged"></asp:TextBox>
                                                <div id="CustList1" style="height: 200px; overflow-y: scroll;"></div>
                                                <asp:AutoCompleteExtender ID="AutoGlName" runat="server" TargetControlID="txtProdName" UseContextKey="true"
                                                    CompletionInterval="1" CompletionSetCount="20" MinimumPrefixLength="1" EnableCaching="true"
                                                    ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList1" ServiceMethod="GetGlName">
                                                </asp:AutoCompleteExtender>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row" style="margin-top: 5px;">
                                    <div class="col-lg-12">
                                        <div class="col-md-2">
                                            <label class="control-label">Account No : <span class="required">*</span></label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="txtAccNo" CssClass="form-control" onkeypress="javascript:return isNumber (event)" PlaceHolder="Account Number" runat="server" AutoPostBack="true" OnTextChanged="txtAccNo_TextChanged"></asp:TextBox>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="input-icon">
                                                <i class="fa fa-search"></i>
                                                <asp:TextBox ID="txtAccName" CssClass="form-control" Placeholder="Customer Name" runat="server" AutoPostBack="true" OnTextChanged="txtAccName_TextChanged"></asp:TextBox>
                                                <%--<div id="CustList2" style="height: 200px; overflow-y: scroll;"></div>
                                                <asp:AutoCompleteExtender ID="AutoAccName" runat="server" TargetControlID="txtAccName" UseContextKey="true"
                                                    CompletionInterval="1" CompletionSetCount="20" MinimumPrefixLength="1" EnableCaching="true"
                                                    ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList2" ServiceMethod="GetAccName">
                                                </asp:AutoCompleteExtender>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <label class="control-label">Cust No : <span class="required">*</span></label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="txtCustNo" CssClass="form-control" PlaceHolder="Customer Number" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="row" style="margin-top: 5px;">
                                    <div class="col-lg-12">
                                       <%-- <div class="col-md-2">//Dhanya Shetty //01/07/2017
                                            <label class="control-label">Account Type : <span class="required">*</span></label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="ddlAccType" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>--%>
                                        <div class="col-md-2">
                                            <label class="control-label">As Of Date : <span class="required">*</span></label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="txtDepDate" onkeyup="FormatIt(this)" CssClass="form-control" PlaceHolder="DD/MM/YYYY" runat="server"></asp:TextBox>
                                            <asp:CalendarExtender ID="TxtDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtDepDate">
                                            </asp:CalendarExtender>
                                        </div>
                                        <div class="col-md-2">
                                            <label class="control-label">Interest Payout : <span class="required">*</span></label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="ddlIntrestPay" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="row" style="margin-top: 5px;">
                                    <div class="col-lg-12">
                                        <div class="col-md-2">
                                            <label class="control-label">Deposit Amount : <span class="required">*</span></label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="txtDepAmt" CssClass="form-control" PlaceHolder="Deposit Amount" runat="server" onkeypress="javascript:return isNumber (event)" AutoPostBack="true" OnTextChanged="txtDepAmt_TextChanged"></asp:TextBox>
                                        </div>
                                        <div class="col-md-1">
                                            <label class="control-label">Period : <span class="required">*</span></label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="ddlDuration" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="M">Months</asp:ListItem>
                                                <asp:ListItem Value="D">Days</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-1">
                                            <asp:TextBox ID="txtPeriod" CssClass="form-control" PlaceHolder="Period" runat="server" AutoPostBack="true" OnTextChanged="txtPeriod_TextChanged" Style="width: 77px;"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <label class="control-label">Interest Rate : <span class="required">*</span></label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="txtIntRate" CssClass="form-control"  runat="server" AutoPostBack="true"  PlaceHolder="Interest Rate" OnTextChanged="txtIntRate_TextChanged" runat="server" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="row" style="margin-top: 5px;">
                                    <div class="col-lg-12">
                                        <div class="col-md-2">
                                            <label class="control-label">Interest Amount : <span class="required">*</span></label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="txtIntAmt" CssClass="form-control" PlaceHolder="Interest Amount" runat="server" ></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <label class="control-label">Maturity Amount : <span class="required">*</span></label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="txtMaturityAmt" CssClass="form-control" PlaceHolder="Maturity Amount" runat="server" ></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <label class="control-label">Due Date : <span class="required">*</span></label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="txtDueDate" CssClass="form-control" PlaceHolder="DD/MM/YYYY" runat="server" ></asp:TextBox>
                                            <asp:CalendarExtender ID="TxtFDT_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtDueDate">
                                            </asp:CalendarExtender>
                                        </div>
                                    </div>
                                </div>

                               <%-- <div class="row" style="margin-top: 5px;">
                                    <div class="col-lg-12">
                                        <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #0A23F9">Interest Transfer Account : </strong></div>
                                    </div>
                                </div>

                                <div class="row" style="margin-top: 5px;">
                                    <div class="col-lg-12">
                                        <div class="col-md-2">
                                            <label class="control-label ">Product Code :<span class="required"> *</span></label>
                                        </div>
                                        <div class="col-md-1">
                                            <asp:TextBox ID="txtIntProdCode" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtIntProdCode_TextChanged" PlaceHolder="Product Code"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="input-icon">
                                                <i class="fa fa-search"></i>
                                                <asp:TextBox ID="txtIntProdName" runat="server" CssClass="form-control" placeholder="Search Product Name" AutoPostBack="true" OnTextChanged="txtIntProdName_TextChanged"></asp:TextBox>
                                                <div id="CustList3" style="height: 200px; overflow-y: scroll;"></div>
                                                <asp:AutoCompleteExtender ID="IntAutoGlName" runat="server" TargetControlID="txtIntProdName" UseContextKey="true"
                                                    CompletionInterval="1" CompletionSetCount="20" MinimumPrefixLength="1" EnableCaching="true"
                                                    ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList3" ServiceMethod="GetGlName">
                                                </asp:AutoCompleteExtender>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <label class="control-label ">Account Number :<span class="required"> *</span></label>
                                        </div>
                                        <div class="col-md-1">
                                            <asp:TextBox ID="txtIntAccNo" CssClass="form-control" onkeypress="javascript:return isNumber (event)" PlaceHolder="Account Number" runat="server" AutoPostBack="true" OnTextChanged="txtIntAccNo_TextChanged"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="input-icon">
                                                <i class="fa fa-search"></i>
                                                <asp:TextBox ID="txtIntAccName" CssClass="form-control" PlaceHolder="Search Customer Name" OnTextChanged="txtIntAccName_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                                <div id="CustList4" style="height: 200px; overflow-y: scroll;"></div>
                                                <asp:AutoCompleteExtender ID="IntAutoAccName" runat="server" TargetControlID="txtIntAccName" UseContextKey="true"
                                                    CompletionInterval="1" CompletionSetCount="20" MinimumPrefixLength="1" EnableCaching="true"
                                                    ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList4" ServiceMethod="GetAccName">
                                                </asp:AutoCompleteExtender>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row" style="margin-top: 5px;">
                                    <div class="col-lg-12">
                                        <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #0A23F9">Principle Transfer Account : </strong></div>
                                    </div>
                                </div>

                                <div class="row" style="margin-top: 5px;">
                                    <div class="col-lg-12">
                                        <div class="col-md-2">
                                            <label class="control-label ">Product Code :<span class="required"> *</span></label>
                                        </div>
                                        <div class="col-md-1">
                                            <asp:TextBox ID="txtPrinProdCode" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtPrinProdCode_TextChanged" PlaceHolder="Product Code"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="input-icon">
                                                <i class="fa fa-search"></i>
                                                <asp:TextBox ID="txtPrinProdName" runat="server" PlaceHolder="Search Product Name" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtPrinProdName_TextChanged"></asp:TextBox>
                                                <div id="CustList5" style="height: 200px; overflow-y: scroll;"></div>
                                                <asp:AutoCompleteExtender ID="PrinAutoGlName" runat="server" TargetControlID="txtPrinProdName" UseContextKey="true"
                                                    CompletionInterval="1" CompletionSetCount="20" MinimumPrefixLength="1" EnableCaching="true"
                                                    ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList5" ServiceMethod="GetGlName">
                                                </asp:AutoCompleteExtender>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <label class="control-label ">Account Number :<span class="required"> *</span></label>
                                        </div>
                                        <div class="col-md-1">
                                            <asp:TextBox ID="txtPrinAccNo" CssClass="form-control" onkeypress="javascript:return isNumber (event)" PlaceHolder="Account Number" runat="server" AutoPostBack="true" OnTextChanged="txtPrinAccNo_TextChanged"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="input-icon">
                                                <i class="fa fa-search"></i>
                                                <asp:TextBox ID="txtPrinAccName" CssClass="form-control" PlaceHolder="Search Customer Name" OnTextChanged="txtPrinAccName_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                                <div id="CustList6" style="height: 200px; overflow-y: scroll;"></div>
                                                <asp:AutoCompleteExtender ID="PrinAutoAccName" runat="server" TargetControlID="txtPrinAccName" UseContextKey="true"
                                                    CompletionInterval="1" CompletionSetCount="20" MinimumPrefixLength="1" EnableCaching="true"
                                                    ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList6" ServiceMethod="GetAccName">
                                                </asp:AutoCompleteExtender>
                                            </div>
                                        </div>
                                    </div>
                                </div>--%>

                                <div class="row" style="margin-top: 5px;">
                                    <div class="col-lg-12">
                                        <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #0A23F9">Payment  : </strong></div>
                                    </div>
                                </div>

                                <div class="row" style="margin-top: 5px;">
                                    <div class="col-lg-12">
                                        <div class="col-md-2">
                                            <label class="control-label">Payment Mode <span class="required">*</span></label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="ddlPayType" OnSelectedIndexChanged="ddlPayType_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div id="Transfer" visible="false" runat="server">
                                    <div class="row" style="margin-top: 5px;">
                                        <div class="col-lg-12">
                                            <div class="col-md-2">
                                                <label class="control-label ">Product Code :<span class="required"> *</span></label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtTrfProdCode" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtProdType1_TextChanged" PlaceHolder="Product Code"></asp:TextBox>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="input-icon">
                                                    <i class="fa fa-search"></i>
                                                    <asp:TextBox ID="txtTrfProdName" CssClass="form-control" PlaceHolder="Search Product Name" OnTextChanged="txtProdName1_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                                    <div id="CustList7" style="height: 200px; overflow-y: scroll;"></div>
                                                    <asp:AutoCompleteExtender ID="TrfAutoGlName" runat="server" TargetControlID="txtTrfProdName" UseContextKey="true"
                                                        CompletionInterval="1" CompletionSetCount="20" MinimumPrefixLength="1" EnableCaching="true"
                                                        ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList7" ServiceMethod="GetGlName">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin-top: 5px;" runat="server">
                                        <div class="col-lg-12">
                                            <div class="col-md-2">
                                                <label class="control-label ">Acc No / Name:<span class="required"> *</span></label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtTrfAccNo" CssClass="form-control" PlaceHolder="Account Number" runat="server" AutoPostBack="true" OnTextChanged="txtAccNo1_TextChanged"></asp:TextBox>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="input-icon">
                                                    <i class="fa fa-search"></i>
                                                    <asp:TextBox ID="txtTrfAccName" CssClass="form-control" PlaceHolder="Search Customer Name" runat="server" AutoPostBack="true" OnTextChanged="txtAccName1_TextChanged"></asp:TextBox>
                                                    <div id="CustList8" style="height: 200px; overflow-y: scroll;"></div>
                                                    <asp:AutoCompleteExtender ID="TrfAutoAccName" runat="server" TargetControlID="txtTrfAccName" UseContextKey="true"
                                                        CompletionInterval="1" CompletionSetCount="20" MinimumPrefixLength="1" EnableCaching="true"
                                                        ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList8" ServiceMethod="GetAccName">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <label class="control-label ">Balance:<span class="required"> *</span></label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtBalance" CssClass="form-control" PlaceHolder="Balance" runat="server" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div id="DivCheque" visible="false" runat="server">
                                    <div class="row" style="margin-top: 5px;" runat="server">
                                        <div class="col-lg-12">
                                            <div class="col-md-2">
                                                <label class="control-label ">Instrument No:<span class="required"> *</span></label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtChequeNo" placeholder="CHEQUE NUMBER" MaxLength="6" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <label class="control-label ">Instrument Date:<span class="required"> *</span></label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtChequeDate" placeholder="CHEQUE DATE" CssClass="form-control" onkeyup="FormatIt(this)" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div id="DivAmount" visible="false" runat="server">
                                    <div class="row" style="margin-top: 5px;" runat="server">
                                        <div class="col-lg-12">
                                            <div class="col-md-2">
                                                <label class="control-label ">Naration : <span class="required">*</span></label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtNarration" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <label class="control-label ">Amount : <span class="required">*</span></label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtAmount" placeholder="DEBIT AMOUNT" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn blue" OnClientClick="Javascript:return IsValid();" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btmModify" runat="server" Text="Modify" CssClass="btn blue"  OnClick="btmModify_Click" Visible="false" />
                                         <asp:Button ID="btnClear" runat="server" Text="Clear All" CssClass="btn blue" OnClick="btnClear_Click" />
                                        <asp:Button ID="btnExit" runat="server" Text="Exit" CssClass="btn blue" OnClick="btnExit_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                    </div>
                    <div class="col-md-12">
                        <div class="table-scrollable">
                        </div>
                    </div>
                     <div class="col-md-12">
                        <div class="table-scrollable">
                            <table class="table table-striped table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th>
                                            <asp:GridView ID="grdInvRectrans" runat="server"  AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                EditRowStyle-BackColor="#FFFF99"  PagerStyle-CssClass="pgr" Width="100%">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="Select" Visible="true" runat="server">
                                                        <ItemTemplate>
                                                         <asp:LinkButton ID="lnkEdit" runat="server" CommandName='<%# Eval("SubGLCode") %>' CommandArgument='<%#Eval("Id")+","+ Eval("SubGLCOde")+","+Eval("CustACCNO")%>'  OnClick="lnkEdit_Click" class="glyphicon glyphicon-plus"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="SUBGLCode" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBankCode" runat="server" Text='<%# Eval("SubGLCOde") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="A/C No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBranchCode" runat="server" Text='<%# Eval("CustACCNO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Bank Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBankName" runat="server" Text='<%# Eval("BankName") %>'></asp:Label>
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
    <asp:HiddenField ID="hdnAcc" runat="server" />
    <asp:HiddenField ID="hdnreceipt" runat="server" />
</asp:Content>

