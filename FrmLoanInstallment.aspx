<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmLoanInstallment.aspx.cs" Inherits="FrmLoanInstallment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
        function IsValide() {

            var txtProdType = document.getElementById('<%=txtProdType.ClientID%>').value;
            var txtAccNo = document.getElementById('<%=txtAccNo.ClientID%>').value;
            var DepositeAmt = document.getElementById('<%=txtDepositeAmt.ClientID%>').value;
            var ddlPayType = document.getElementById('<%=ddlPayType.ClientID%>').value;
            var txtAmount = document.getElementById('<%=txtAmount.ClientID%>').value;
            var message = '';

            if (txtProdType == "") {
                window.alert("Enter Product Type First ...!!");
                document.getElementById('<%=txtProdType.ClientID%>').focus();
                return false;
            }
            if (txtAccNo == "") {
                window.alert("Enter Account Number First ...!!");
                document.getElementById('<%=txtAccNo.ClientID%>').focus();
                return false;
            }
            if (DepositeAmt == "") {
                window.alert("Enter Paid Amount first ...!!");
                document.getElementById('<%=txtDepositeAmt.ClientID%>').focus();
                return false;
            }
            if (ddlPayType == "0") {
                window.alert("Select Payment Mode First ...!!");
                document.getElementById('<%=ddlPayType.ClientID%>').focus();
                return false;
            }
            if (txtAmount == "") {
                window.alert("Select Payment Mode First ...!!");
                document.getElementById('<%=ddlPayType.ClientID%>').focus();
                return false;
            }
        }
    </script>

    <script type="text/javascript">
        function Confirm() {
            debugger;
            var confirm_value = document.createElement("INPUT");
            var PrinPen = document.getElementById('<%=txtPrinPen.ClientID%>').value || 0;
            var IntPen = document.getElementById('<%=txtIntPen.ClientID%>').value || 0;
            var PIntPen = document.getElementById('<%=txtPIntPen.ClientID%>').value || 0;
            var IntRecPen = document.getElementById('<%=txtIntRecPen.ClientID%>').value || 0;
            var NotChrgPen = document.getElementById('<%=txtNotChrgPen.ClientID%>').value || 0;
            var SerChrgPen = document.getElementById('<%=txtSerChrgPen.ClientID%>').value || 0;
            var CrtChrgPen = document.getElementById('<%=txtCrtChrgPen.ClientID%>').value || 0;
            var SurChrgPen = document.getElementById('<%=txtSurChrgPen.ClientID%>').value || 0;
            var OtherChrgPen = document.getElementById('<%=txtOtherChrgPen.ClientID%>').value || 0;
            var BankChrgPen = document.getElementById('<%=txtBankChrgPen.ClientID%>').value || 0;
            var InsurancePen = document.getElementById('<%=txtInsurancePen.ClientID%>').value || 0;

            var Sum = parseFloat(PrinPen) + parseFloat(IntPen) + parseFloat(PIntPen) + parseFloat(IntRecPen) + parseFloat(NotChrgPen) + parseFloat(SerChrgPen) + parseFloat(CrtChrgPen) + parseFloat(SurChrgPen) + parseFloat(OtherChrgPen) + parseFloat(BankChrgPen) + parseFloat(InsurancePen)
            if (parseFloat(SUM) == 0) {
                confirm_value.type = "hidden";
                confirm_value.name = "confirm_value";
                if (confirm("Do you want to close this account?")) {
                    confirm_value.value = "Yes";
                } else {
                    confirm_value.value = "No";
                }
            }
            else {
                confirm_value.value = "No";
            }

            document.forms[0].appendChild(confirm_value);
        }
    </script>

    <script type="text/javascript">
        function PrincipleTot() {
            var PrinPrev = document.getElementById('<%=txtPrinPrev.ClientID%>').value || 0;
            var PrinCurr = document.getElementById('<%=txtPrinCurr.ClientID%>').value || 0;
            var TotAmt = parseFloat(PrinPrev) + parseFloat(PrinCurr);
            document.getElementById('<%=txtPrinTotal.ClientID%>').value = TotAmt;
            var PrinTotal = document.getElementById('<%=txtPrinTotal.ClientID%>').value || 0;
            var PrinAmt = document.getElementById('<%=txtPrinAmt.ClientID%>').value || 0;

            var PaidAmt = parseFloat(PrinTotal) - parseFloat(PrinAmt);
            if (!isNaN(PaidAmt)) {
                document.getElementById('<%=txtPrinPen.ClientID%>').value = PaidAmt;
            }
            DispTotal();
        }

        function InterestTot() {
            var IntPrev = document.getElementById('<%=txtIntPrev.ClientID%>').value || 0;
            var IntCurr = document.getElementById('<%=txtIntCurr.ClientID%>').value || 0;
            var TotAmt = parseFloat(IntPrev) + parseFloat(IntCurr);
            document.getElementById('<%=txtIntTotal.ClientID%>').value = TotAmt;
            var IntTotal = document.getElementById('<%=txtIntTotal.ClientID%>').value || 0;
            var IntAmt = document.getElementById('<%=txtIntAmt.ClientID%>').value || 0;

            var PaidAmt = parseFloat(IntTotal) - parseFloat(IntAmt);
            if (!isNaN(PaidAmt)) {
                document.getElementById('<%=txtIntPen.ClientID%>').value = PaidAmt;
            }
            DispTotal();
        }

        function PenalTot() {
            var PIntPrev = document.getElementById('<%=txtPIntPrev.ClientID%>').value || 0;
            var PIntCurr = document.getElementById('<%=txtPIntCurr.ClientID%>').value || 0;
            var TotAmt = parseFloat(PIntPrev) + parseFloat(PIntCurr);
            document.getElementById('<%=txtPIntTotal.ClientID%>').value = TotAmt;
            var PIntTotal = document.getElementById('<%=txtPIntTotal.ClientID%>').value || 0;
            var PIntAmt = document.getElementById('<%=txtPIntAmt.ClientID%>').value || 0;

            var PaidAmt = parseFloat(PIntTotal) - parseFloat(PIntAmt);
            if (!isNaN(PaidAmt)) {
                document.getElementById('<%=txtPIntPen.ClientID%>').value = PaidAmt;
            }
            DispTotal();
        }

        function IntRecTot() {
            var IntRecPrev = document.getElementById('<%=txtIntRecPrev.ClientID%>').value || 0;
            var IntRecCurr = document.getElementById('<%=txtIntRecCurr.ClientID%>').value || 0;
            var TotAmt = parseFloat(IntRecPrev) + parseFloat(IntRecCurr);
            document.getElementById('<%=txtIntRecTotal.ClientID%>').value = TotAmt;
            var IntRecTotal = document.getElementById('<%=txtIntRecTotal.ClientID%>').value || 0;
            var IntRecAmt = document.getElementById('<%=txtIntRecAmt.ClientID%>').value || 0;

            var PaidAmt = parseFloat(IntRecTotal) - parseFloat(IntRecAmt);
            if (!isNaN(PaidAmt)) {
                document.getElementById('<%=txtIntRecPen.ClientID%>').value = PaidAmt;
            }
            DispTotal();
        }

        function NoticeTot() {
            var NotChrgPrev = document.getElementById('<%=txtNotChrgPrev.ClientID%>').value || 0;
            var NotChrgCurr = document.getElementById('<%=txtNotChrgCurr.ClientID%>').value || 0;
            var TotAmt = parseFloat(NotChrgPrev) + parseFloat(NotChrgCurr);
            document.getElementById('<%=txtNotChrgTotal.ClientID%>').value = TotAmt;
            var NotChrgTotal = document.getElementById('<%=txtNotChrgTotal.ClientID%>').value || 0;
            var NotChrgAmt = document.getElementById('<%=txtNotChrgAmt.ClientID%>').value || 0;

            var PaidAmt = parseFloat(NotChrgTotal) - parseFloat(NotChrgAmt);
            if (!isNaN(PaidAmt)) {
                document.getElementById('<%=txtNotChrgPen.ClientID%>').value = PaidAmt;
            }
            DispTotal();
        }

        function ServiceTot() {
            var SerChrgPrev = document.getElementById('<%=txtPrinPrev.ClientID%>').value || 0;
            var SerChrgCurr = document.getElementById('<%=txtSerChrgCurr.ClientID%>').value || 0;
            var TotAmt = parseFloat(SerChrgPrev) + parseFloat(SerChrgCurr);
            document.getElementById('<%=txtSerChrgTotal.ClientID%>').value = TotAmt;
            var SerChrgTotal = document.getElementById('<%=txtSerChrgTotal.ClientID%>').value || 0;
            var SerChrgAmt = document.getElementById('<%=txtSerChrgAmt.ClientID%>').value || 0;

            var PaidAmt = parseFloat(SerChrgTotal) - parseFloat(SerChrgAmt);
            if (!isNaN(PaidAmt)) {
                document.getElementById('<%=txtSerChrgPen.ClientID%>').value = PaidAmt;
            }
            DispTotal();
        }

        function CourtTot() {
            var CrtChrgPrev = document.getElementById('<%=txtCrtChrgPrev.ClientID%>').value || 0;
            var CrtChrgCurr = document.getElementById('<%=txtCrtChrgCurr.ClientID%>').value || 0;
            var TotAmt = parseFloat(CrtChrgPrev) + parseFloat(CrtChrgCurr);
            document.getElementById('<%=txtCrtChrgTotal.ClientID%>').value = TotAmt;
            var CrtChrgTotal = document.getElementById('<%=txtCrtChrgTotal.ClientID%>').value || 0;
            var CrtChrgAmt = document.getElementById('<%=txtCrtChrgAmt.ClientID%>').value || 0;

            var PaidAmt = parseFloat(CrtChrgTotal) - parseFloat(CrtChrgAmt);
            if (!isNaN(PaidAmt)) {
                document.getElementById('<%=txtCrtChrgPen.ClientID%>').value = PaidAmt;
                TakeTotal();
            }
            DispTotal();
        }

        function SurTot() {
            var SurChrgPrev = document.getElementById('<%=txtSurChrgPrev.ClientID%>').value || 0;
            var SurChrgCurr = document.getElementById('<%=txtSurChrgCurr.ClientID%>').value || 0;
            var TotAmt = parseFloat(SurChrgPrev) + parseFloat(SurChrgCurr);
            document.getElementById('<%=txtSurChrgTotal.ClientID%>').value = TotAmt;
            var SurChrgTotal = document.getElementById('<%=txtSurChrgTotal.ClientID%>').value || 0;
            var SurChrgAmt = document.getElementById('<%=txtSurChrgAmt.ClientID%>').value || 0;

            var PaidAmt = parseFloat(SurChrgTotal) - parseFloat(SurChrgAmt);
            if (!isNaN(PaidAmt)) {
                document.getElementById('<%=txtSurChrgPen.ClientID%>').value = PaidAmt;
            }
            DispTotal();
        }

        function OtherTot() {
            var OtherChrgPrev = document.getElementById('<%=txtOtherChrgPrev.ClientID%>').value || 0;
            var OtherChrgCurr = document.getElementById('<%=txtOtherChrgCurr.ClientID%>').value || 0;
            var TotAmt = parseFloat(OtherChrgPrev) + parseFloat(OtherChrgCurr);
            document.getElementById('<%=txtOtherChrgTotal.ClientID%>').value = TotAmt;
            var OtherChrgTotal = document.getElementById('<%=txtOtherChrgTotal.ClientID%>').value || 0;
            var OtherChrgAmt = document.getElementById('<%=txtOtherChrgAmt.ClientID%>').value || 0;

            var PaidAmt = parseFloat(OtherChrgTotal) - parseFloat(OtherChrgAmt);
            if (!isNaN(PaidAmt)) {
                document.getElementById('<%=txtOtherChrgPen.ClientID%>').value = PaidAmt;
            }
            DispTotal();
        }

        function BankTot() {
            var BankChrgPrev = document.getElementById('<%=txtBankChrgPrev.ClientID%>').value || 0;
            var BankChrgCurr = document.getElementById('<%=txtBankChrgCurr.ClientID%>').value || 0;
            var TotAmt = parseFloat(BankChrgPrev) + parseFloat(BankChrgCurr);
            document.getElementById('<%=txtBankChrgTotal.ClientID%>').value = TotAmt;
            var BankChrgTotal = document.getElementById('<%=txtBankChrgTotal.ClientID%>').value || 0;
            var BankChrgAmt = document.getElementById('<%=txtBankChrgAmt.ClientID%>').value || 0;

            var PaidAmt = parseFloat(BankChrgTotal) - parseFloat(BankChrgAmt);
            if (!isNaN(PaidAmt)) {
                document.getElementById('<%=txtBankChrgPen.ClientID%>').value = PaidAmt;
            }
            DispTotal();
        }

        function InsuranceTot() {
            var InsurancePrev = document.getElementById('<%=txtInsurancePrev.ClientID%>').value || 0;
            var InsuranceCurr = document.getElementById('<%=txtInsuranceCurr.ClientID%>').value || 0;
            var TotAmt = parseFloat(InsurancePrev) + parseFloat(InsuranceCurr);
            document.getElementById('<%=txtInsuranceTotal.ClientID%>').value = TotAmt;
            var InsuranceTotal = document.getElementById('<%=txtInsuranceTotal.ClientID%>').value || 0;
            var InsuranceAmt = document.getElementById('<%=txtInsuranceAmt.ClientID%>').value || 0;

            var PaidAmt = parseFloat(InsuranceTotal) - parseFloat(InsuranceAmt);
            if (!isNaN(PaidAmt)) {
                document.getElementById('<%=txtInsurancePen.ClientID%>').value = PaidAmt;
            }
            DispTotal();
        }
    </script>

    <script type="text/javascript">
        function DispTotal() {
            debugger;
            var PrinPrevAmt = document.getElementById('<%=txtPrinPrev.ClientID%>').value || 0;
            var IntPrevAmt = document.getElementById('<%=txtIntPrev.ClientID%>').value || 0;
            var PIntPrevAmt = document.getElementById('<%=txtPIntPrev.ClientID%>').value || 0;
            var IntRecPrevAmt = document.getElementById('<%=txtIntRecPrev.ClientID%>').value || 0;
            var NotChrgPrevAmt = document.getElementById('<%=txtNotChrgPrev.ClientID%>').value || 0;
            var SerChrgPrevAmt = document.getElementById('<%=txtSerChrgPrev.ClientID%>').value || 0;
            var CrtChrgPrevAmt = document.getElementById('<%=txtCrtChrgPrev.ClientID%>').value || 0;
            var SurChrgPrevAmt = document.getElementById('<%=txtSurChrgPrev.ClientID%>').value || 0;
            var OtherChrgPrevAmt = document.getElementById('<%=txtOtherChrgPrev.ClientID%>').value || 0;
            var BankChrgPrevAmt = document.getElementById('<%=txtBankChrgPrev.ClientID%>').value || 0;
            var InsurancePrevAmt = document.getElementById('<%=txtInsurancePrev.ClientID%>').value || 0;
            var GSTPrevAmt = document.getElementById('<%=txtGSTPrevAmt.ClientID%>').value || 0;

            var SUM = parseFloat(PrinPrevAmt) + parseFloat(IntPrevAmt) + parseFloat(PIntPrevAmt) + parseFloat(IntRecPrevAmt) + parseFloat(NotChrgPrevAmt) + parseFloat(SerChrgPrevAmt) + parseFloat(CrtChrgPrevAmt) + parseFloat(SurChrgPrevAmt) + parseFloat(OtherChrgPrevAmt) + parseFloat(BankChrgPrevAmt) + parseFloat(InsurancePrevAmt) + parseFloat(GSTPrevAmt);
            if (!isNaN(SUM)) {
                document.getElementById('<%=txtTotPrevAmt.ClientID%>').value = SUM;
            }

            var PrinCurrAmt = document.getElementById('<%=txtPrinCurr.ClientID%>').value || 0;
            var IntCurrAmt = document.getElementById('<%=txtIntCurr.ClientID%>').value || 0;
            var PIntCurrAmt = document.getElementById('<%=txtPIntCurr.ClientID%>').value || 0;
            var IntRecCurrAmt = document.getElementById('<%=txtIntRecCurr.ClientID%>').value || 0;
            var NotChrgCurrAmt = document.getElementById('<%=txtNotChrgCurr.ClientID%>').value || 0;
            var SerChrgCurrAmt = document.getElementById('<%=txtSerChrgCurr.ClientID%>').value || 0;
            var CrtChrgCurrAmt = document.getElementById('<%=txtCrtChrgCurr.ClientID%>').value || 0;
            var SurChrgCurrAmt = document.getElementById('<%=txtSurChrgCurr.ClientID%>').value || 0;
            var OtherChrgCurrAmt = document.getElementById('<%=txtOtherChrgCurr.ClientID%>').value || 0;
            var BankChrgCurrAmt = document.getElementById('<%=txtBankChrgCurr.ClientID%>').value || 0;
            var InsuranceCurrAmt = document.getElementById('<%=txtInsuranceCurr.ClientID%>').value || 0;
            var GSTCurrAmt = document.getElementById('<%=txtGSTCurrAmt.ClientID%>').value || 0;

            var SUM = parseFloat(PrinCurrAmt) + parseFloat(IntCurrAmt) + parseFloat(PIntCurrAmt) + parseFloat(IntRecCurrAmt) + parseFloat(NotChrgCurrAmt) + parseFloat(SerChrgCurrAmt) + parseFloat(CrtChrgCurrAmt) + parseFloat(SurChrgCurrAmt) + parseFloat(OtherChrgCurrAmt) + parseFloat(BankChrgCurrAmt) + parseFloat(InsuranceCurrAmt) + parseFloat(GSTCurrAmt);
            if (!isNaN(SUM)) {
                document.getElementById('<%=txtTotCurrAmt.ClientID%>').value = SUM;
            }

            var PrinTotalAmt = document.getElementById('<%=txtPrinTotal.ClientID%>').value || 0;
            var IntTotalAmt = document.getElementById('<%=txtIntTotal.ClientID%>').value || 0;
            var PIntTotalAmt = document.getElementById('<%=txtPIntTotal.ClientID%>').value || 0;
            var IntRecTotalAmt = document.getElementById('<%=txtIntRecTotal.ClientID%>').value || 0;
            var NotChrgTotalAmt = document.getElementById('<%=txtNotChrgTotal.ClientID%>').value || 0;
            var SerChrgTotalAmt = document.getElementById('<%=txtSerChrgTotal.ClientID%>').value || 0;
            var CrtChrgTotalAmt = document.getElementById('<%=txtCrtChrgTotal.ClientID%>').value || 0;
            var SurChrgTotalAmt = document.getElementById('<%=txtSurChrgTotal.ClientID%>').value || 0;
            var OtherChrgTotalAmt = document.getElementById('<%=txtOtherChrgTotal.ClientID%>').value || 0;
            var BankChrgTotalAmt = document.getElementById('<%=txtBankChrgTotal.ClientID%>').value || 0;
            var InsuranceTotalAmt = document.getElementById('<%=txtInsuranceTotal.ClientID%>').value || 0;
            var GSTTotalAmt = document.getElementById('<%=txtGSTTotalAmt.ClientID%>').value || 0;

            var SUM = parseFloat(PrinTotalAmt) + parseFloat(IntTotalAmt) + parseFloat(PIntTotalAmt) + parseFloat(IntRecTotalAmt) + parseFloat(NotChrgTotalAmt) + parseFloat(SerChrgTotalAmt) + parseFloat(CrtChrgTotalAmt) + parseFloat(SurChrgTotalAmt) + parseFloat(OtherChrgTotalAmt) + parseFloat(BankChrgTotalAmt) + parseFloat(InsuranceTotalAmt) + parseFloat(GSTTotalAmt);
            if (!isNaN(SUM)) {
                document.getElementById('<%=txtTotalAmt.ClientID%>').value = SUM;
            }

            var PrinPaidAmt = document.getElementById('<%=txtPrinAmt.ClientID%>').value || 0;
            var IntPaidAmt = document.getElementById('<%=txtIntAmt.ClientID%>').value || 0;
            var PIntPaidAmt = document.getElementById('<%=txtPIntAmt.ClientID%>').value || 0;
            var IntRecPaidAmt = document.getElementById('<%=txtIntRecAmt.ClientID%>').value || 0;
            var NotChrgPaidAmt = document.getElementById('<%=txtNotChrgAmt.ClientID%>').value || 0;
            var SerChrgPaidAmt = document.getElementById('<%=txtSerChrgAmt.ClientID%>').value || 0;
            var CrtChrgPaidAmt = document.getElementById('<%=txtCrtChrgAmt.ClientID%>').value || 0;
            var SurChrgPaidAmt = document.getElementById('<%=txtSurChrgAmt.ClientID%>').value || 0;
            var OtherChrgPaidAmt = document.getElementById('<%=txtOtherChrgAmt.ClientID%>').value || 0;
            var BankChrgPaidAmt = document.getElementById('<%=txtBankChrgAmt.ClientID%>').value || 0;
            var InsurancePaidAmt = document.getElementById('<%=txtInsuranceAmt.ClientID%>').value || 0;
            var GSTAmt = document.getElementById('<%=txtGSTAmt.ClientID%>').value || 0;

            var SUM = parseFloat(PrinPaidAmt) + parseFloat(IntPaidAmt) + parseFloat(PIntPaidAmt) + parseFloat(IntRecPaidAmt) + parseFloat(NotChrgPaidAmt) + parseFloat(SerChrgPaidAmt) + parseFloat(CrtChrgPaidAmt) + parseFloat(SurChrgPaidAmt) + parseFloat(OtherChrgPaidAmt) + parseFloat(BankChrgPaidAmt) + parseFloat(InsurancePaidAmt) + parseFloat(GSTAmt);
            if (!isNaN(SUM)) {
                document.getElementById('<%=txtTotPaidAmt.ClientID%>').value = SUM;
            }

            var PrinPendAmt = document.getElementById('<%=txtPrinPen.ClientID%>').value || 0;
            var IntPendAmt = document.getElementById('<%=txtIntPen.ClientID%>').value || 0;
            var PIntPendAmt = document.getElementById('<%=txtPIntPen.ClientID%>').value || 0;
            var IntRecPendAmt = document.getElementById('<%=txtIntRecPen.ClientID%>').value || 0;
            var NotChrgPendAmt = document.getElementById('<%=txtNotChrgPen.ClientID%>').value || 0;
            var SerChrgPendAmt = document.getElementById('<%=txtSerChrgPen.ClientID%>').value || 0;
            var CrtChrgPendAmt = document.getElementById('<%=txtCrtChrgPen.ClientID%>').value || 0;
            var SurChrgPendAmt = document.getElementById('<%=txtSurChrgPen.ClientID%>').value || 0;
            var OtherChrgPendAmt = document.getElementById('<%=txtOtherChrgPen.ClientID%>').value || 0;
            var BankChrgPendAmt = document.getElementById('<%=txtBankChrgPen.ClientID%>').value || 0;
            var InsurancePendAmt = document.getElementById('<%=txtInsurancePen.ClientID%>').value || 0;
            var GSTTotPendAmt = document.getElementById('<%=txtGSTTotPendAmt.ClientID%>').value || 0;

            var SUM = parseFloat(Math.abs(PrinPendAmt)) + parseFloat(Math.abs(IntPendAmt)) + parseFloat(Math.abs(PIntPendAmt)) + parseFloat(Math.abs(IntRecPendAmt)) + parseFloat(Math.abs(NotChrgPendAmt)) + parseFloat(Math.abs(SerChrgPendAmt)) + parseFloat(Math.abs(CrtChrgPendAmt)) + parseFloat(Math.abs(SurChrgPendAmt)) + parseFloat(Math.abs(OtherChrgPendAmt)) + parseFloat(Math.abs(BankChrgPendAmt)) + parseFloat(Math.abs(InsurancePendAmt)) + parseFloat(Math.abs(GSTTotPendAmt));
            if (!isNaN(SUM)) {
                document.getElementById('<%=txtTotPendAmt.ClientID%>').value = SUM;
            }
        }
    </script>

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
                alert("Please Enter valid Date ...!!");
        }
    </script>

    <style type="text/css">
        .Th {
            padding-right: 5px;
            text-align: right;
        }

        .grdheader {
            position: absolute;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        LOAN INSTALLMENT
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
                                                    <div class="col-md-1" style="width: 160px">
                                                        <label class="control-label ">Product Type :<span class="required"> *</span></label>
                                                    </div>
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="txtProdType" onkeypress="javascript:return isNumber(event)" OnTextChanged="txtProdType_TextChanged" AutoPostBack="true" Placeholder="Product Type" runat="server" CssClass="form-control" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="input-icon">
                                                            <i class="fa fa-search"></i>
                                                            <asp:TextBox ID="txtProdName" Placeholder="Search Product Name" OnTextChanged="txtProdName_TextChanged" AutoPostBack="true" runat="server" CssClass="form-control" />
                                                            <div id="CustList1" style="height: 200px; overflow-y: scroll;"></div>
                                                            <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="txtProdName" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList1" ServiceMethod="GetGlName">
                                                            </asp:AutoCompleteExtender>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-1">
                                                        <label class="control-label ">Acc\No:<span class="required">*</span></label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtAccNo" onkeypress="javascript:return isNumber(event)" OnTextChanged="txtAccNo_TextChanged" AutoPostBack="true" Placeholder="Account Number" runat="server" CssClass="form-control" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="input-icon">
                                                            <i class="fa fa-search"></i>
                                                            <asp:TextBox ID="txtAccName" OnTextChanged="txtAccName_TextChanged" AutoPostBack="true" Placeholder="Search Customer Name" runat="server" CssClass="form-control" />
                                                            <div id="CustList2" style="height: 200px; overflow-y: scroll;"></div>
                                                            <asp:AutoCompleteExtender ID="AutoAccname" runat="server" TargetControlID="txtAccName" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList2" ServiceMethod="GetAccName">
                                                            </asp:AutoCompleteExtender>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-1" style="width: 160px">
                                                        <label class="control-label ">Customer Number :</label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtCustNo" Enabled="false" Placeholder="Customer Number" runat="server" CssClass="form-control" />
                                                    </div>
                                                    <div class="col-md-1">
                                                        <label class="control-label ">Case File :</label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <input type="hidden" id="AccType" runat="server" value="0" />
                                                        <asp:TextBox ID="txtAccType" Enabled="false" runat="server" CssClass="form-control" />
                                                    </div>
                                                    <div class="col-md-1">
                                                        <label class="control-label ">Amount:<span class="required">*</span></label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtDepositeAmt" placeholder="Installment Amount" CssClass="form-control" OnTextChanged="txtDepositeAmt_TextChanged" AutoPostBack="true" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                               <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-1" style="width: 160px">
                                                        <label class="control-label ">Mobile No 1 :</label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtMobileNo1" Enabled="false" Placeholder="Mobile No 1" runat="server" CssClass="form-control" />
                                                    </div>
                                                    <div class="col-md-1">
                                                        <label class="control-label ">Mobile No 2</label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtMobileNo2" Enabled="false" runat="server" PlaceHolder="Mobile No 2" CssClass="form-control" />
                                                    </div>
                                                   
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                    <div style="border: 1px solid #3598dc">
                                        <div class="portlet-body">
                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-1" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31); width: 160px"><strong style="color: #3598dc">Head Name</strong></div>
                                                    <div class="col-md-1" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31); width: 130px"><strong style="color: #3598dc">Previous Amount</strong></div>
                                                    <div class="col-md-1" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31); width: 130px"><strong style="color: #3598dc">Current Amount</strong></div>
                                                    <div class="col-md-1" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31); width: 130px"><strong style="color: #3598dc">Total Amount</strong></div>
                                                    <div class="col-md-1" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31); width: 130px"><strong style="color: #3598dc">Paid Amount</strong></div>
                                                    <div class="col-md-1" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31); width: 130px"><strong style="color: #3598dc">Pend Amount</strong></div>
                                                    <div class="col-md-1" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31); width: 260px"><strong style="color: #3598dc">Loan Account Details</strong></div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-1" style="width: 160px">
                                                        <label id="LblName1" runat="server" class="control-label">Principle :</label>
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtPrinPrev" Enabled="false" Placeholder="Previous Amount" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtPrinCurr" onblur="PrincipleTot()" Enabled="false" Placeholder="Current Amount" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtPrinTotal" Enabled="false" Placeholder="Total Amount" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtPrinAmt" onblur="PrincipleTot()" ForeColor="Green" Placeholder="Amount" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtPrinPen" Enabled="false" Placeholder="Pending Amount" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 120px">
                                                        <label class="control-label ">Acc Status</label>
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <input type="hidden" id="AccStatus" runat="server" value="" />
                                                        <asp:DropDownList ID="ddlAccStatus" Enabled="false" CssClass="form-control" runat="server"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-1" style="width: 160px">
                                                        <label id="LblName2" runat="server" class="control-label">Interest :</label>
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtIntPrev" onkeypress="javascript:return isNumber(event)" Enabled="false" Placeholder="Previous Amount" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtIntCurr" onkeypress="javascript:return isNumber(event)" Enabled="false" onblur="InterestTot()" Placeholder="Current Amount" CssClass="form-control" runat="server" OnTextChanged="GSTCalculate_TextChanged" AutoPostBack="true" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtIntTotal" onkeypress="javascript:return isNumber(event)" Enabled="false" Placeholder="Total Amount" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtIntAmt" onkeypress="javascript:return isNumber(event)" onblur="InterestTot()" ForeColor="Green" Placeholder="Amount" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtIntPen" onkeypress="javascript:return isNumber(event)" Enabled="false" Placeholder="Pending Amount" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 120px">
                                                        <label class="control-label ">Sanction Date</label>
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtAccOpenDate" Enabled="false" placeholder="DD/MM/YYYY" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-1" style="width: 160px">
                                                        <label id="LblName3" runat="server" class="control-label">Penal Interest :</label>
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtPIntPrev" onkeypress="javascript:return isNumber(event)" Enabled="false" Placeholder="Previous Amount" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtPIntCurr" onkeypress="javascript:return isNumber(event)" Enabled="false" onblur="PenalTot()" Placeholder="Current Amount" CssClass="form-control" runat="server" OnTextChanged="GSTCalculate_TextChanged" AutoPostBack="true" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtPIntTotal" onkeypress="javascript:return isNumber(event)" Enabled="false" Placeholder="Total Amount" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtPIntAmt" onkeypress="javascript:return isNumber(event)" onblur="PenalTot()" ForeColor="Green" Placeholder="Amount" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtPIntPen" onkeypress="javascript:return isNumber(event)" Enabled="false" Placeholder="Pending Amount" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 120px">
                                                        <label class="control-label ">Sanction Limit</label>
                                                    </div>
                                                    <div class="col-md-2" style="width: 130px">
                                                        <asp:TextBox ID="txtSancLimit" onkeypress="javascript:return isNumber(event)" Enabled="false" Placeholder="Sanction Limit" runat="server" CssClass="form-control" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-1" style="width: 160px">
                                                        <label id="LblName4" runat="server" class="control-label">Interest Recievable :</label>
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtIntRecPrev" onkeypress="javascript:return isNumber(event)" Enabled="false" Placeholder="Previous Amount" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtIntRecCurr" onkeypress="javascript:return isNumber(event)" Enabled="false" onblur="IntRecTot()" Placeholder="Current Amount" CssClass="form-control" runat="server" OnTextChanged="GSTCalculate_TextChanged" AutoPostBack="true" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtIntRecTotal" onkeypress="javascript:return isNumber(event)" Enabled="false" Placeholder="Total Amount" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtIntRecAmt" onkeypress="javascript:return isNumber(event)" onblur="IntRecTot()" ForeColor="Green" Placeholder="Amount" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtIntRecPen" onkeypress="javascript:return isNumber(event)" Enabled="false" Placeholder="Pending Amount" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 120px">
                                                        <label class="control-label ">Total Period</label>
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtPeriod" Enabled="false" Placeholder="Total Period" runat="server" CssClass="form-control" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-1" style="width: 160px">
                                                        <label id="LblName5" runat="server" class="control-label">Notice Charges :</label>
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtNotChrgPrev" onkeypress="javascript:return isNumber(event)" Enabled="false" Placeholder="Previous Amount" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtNotChrgCurr" onkeypress="javascript:return isNumber(event)" onblur="NoticeTot()" Placeholder="Current Amount" CssClass="form-control" runat="server" OnTextChanged="GSTCalculate_TextChanged" AutoPostBack="true" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtNotChrgTotal" onkeypress="javascript:return isNumber(event)" Enabled="false" Placeholder="Total Amount" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtNotChrgAmt" onkeypress="javascript:return isNumber(event)" onblur="NoticeTot()" ForeColor="Green" Placeholder="Amount" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtNotChrgPen" onkeypress="javascript:return isNumber(event)" Enabled="false" Placeholder="Pending Amount" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 120px">
                                                        <label class="control-label ">Applied Rate</label>
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtAppliedRate" onkeypress="javascript:return isNumber(event)" Enabled="false" Placeholder="Applied Rate" runat="server" CssClass="form-control" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-1" style="width: 160px">
                                                        <label id="LblName6" runat="server" class="control-label">Service Charges :</label>
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtSerChrgPrev" onkeypress="javascript:return isNumber(event)" Enabled="false" Placeholder="Previous Amount" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtSerChrgCurr" onkeypress="javascript:return isNumber(event)" onblur="ServiceTot()" Placeholder="Current Amount" CssClass="form-control" runat="server" OnTextChanged="GSTCalculate_TextChanged" AutoPostBack="true" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtSerChrgTotal" onkeypress="javascript:return isNumber(event)" Enabled="false" Placeholder="Total Amount" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtSerChrgAmt" onkeypress="javascript:return isNumber(event)" onblur="ServiceTot()" ForeColor="Green" Placeholder="Amount" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtSerChrgPen" onkeypress="javascript:return isNumber(event)" Enabled="false" Placeholder="Pending Amount" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 120px">
                                                        <label class="control-label ">Interest Rate</label>
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtIntRate" onkeypress="javascript:return isNumber(event)" Enabled="false" Placeholder="Rate Of Interest" runat="server" CssClass="form-control" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-1" style="width: 160px">
                                                        <label id="LblName7" runat="server" class="control-label">Court Charge :</label>
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtCrtChrgPrev" onkeypress="javascript:return isNumber(event)" Enabled="false" Placeholder="Previous Amount" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtCrtChrgCurr" onkeypress="javascript:return isNumber(event)" onblur="CourtTot()" Placeholder="Current Amount" CssClass="form-control" runat="server" OnTextChanged="GSTCalculate_TextChanged" AutoPostBack="true" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtCrtChrgTotal" onkeypress="javascript:return isNumber(event)" Enabled="false" Placeholder="Total Amount" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtCrtChrgAmt" onkeypress="javascript:return isNumber(event)" onblur="CourtTot()" ForeColor="Green" Placeholder="Amount" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtCrtChrgPen" onkeypress="javascript:return isNumber(event)" Enabled="false" Placeholder="Pending Amount" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 120px">
                                                        <label class="control-label ">Total Days</label>
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtDays" Enabled="false" Placeholder="Total Days Diff" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-1" style="width: 160px">
                                                        <label id="LblName8" runat="server" class="control-label">Sur Charge :</label>
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtSurChrgPrev" onkeypress="javascript:return isNumber(event)" Enabled="false" Placeholder="Previous Amount" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtSurChrgCurr" onkeypress="javascript:return isNumber(event)" onblur="SurTot()" Placeholder="Current Amount" CssClass="form-control" runat="server" OnTextChanged="GSTCalculate_TextChanged" AutoPostBack="true" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtSurChrgTotal" onkeypress="javascript:return isNumber(event)" Enabled="false" Placeholder="Total Amount" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtSurChrgAmt" onkeypress="javascript:return isNumber(event)" onblur="SurTot()" ForeColor="Green" Placeholder="Amount" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtSurChrgPen" onkeypress="javascript:return isNumber(event)" Enabled="false" Placeholder="Pending Amount" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 120px">
                                                        <label class="control-label ">Installment Amt</label>
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtInstAmt" onkeypress="javascript:return isNumber(event)" Enabled="false" Placeholder="Installment Amount" runat="server" CssClass="form-control" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-1" style="width: 160px">
                                                        <label id="LblName9" runat="server" class="control-label">Other Charge :</label>
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtOtherChrgPrev" onkeypress="javascript:return isNumber(event)" Enabled="false" Placeholder="Previous Amount" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtOtherChrgCurr" onkeypress="javascript:return isNumber(event)" onblur="OtherTot()" Placeholder="Current Amount" CssClass="form-control" runat="server" OnTextChanged="GSTCalculate_TextChanged" AutoPostBack="true" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtOtherChrgTotal" onkeypress="javascript:return isNumber(event)" Enabled="false" Placeholder="Total Amount" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtOtherChrgAmt" onkeypress="javascript:return isNumber(event)" onblur="OtherTot()" ForeColor="Green" Placeholder="Amount" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtOtherChrgPen" onkeypress="javascript:return isNumber(event)" Enabled="false" Placeholder="Pending Amount" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 120px">
                                                        <label class="control-label ">Last Int Date</label>
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <input type="hidden" id="WorkDate" runat="server" />
                                                        <asp:TextBox ID="txtLastIntDate" Enabled="false" Placeholder="DD/MM/YYYY" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-1" style="width: 160px">
                                                        <label id="LblName10" runat="server" class="control-label">Bank Charge :</label>
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtBankChrgPrev" onkeypress="javascript:return isNumber(event)" Enabled="false" Placeholder="Previous Amount" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtBankChrgCurr" onkeypress="javascript:return isNumber(event)" onblur="BankTot()" Placeholder="Current Amount" CssClass="form-control" runat="server" OnTextChanged="GSTCalculate_TextChanged" AutoPostBack="true" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtBankChrgTotal" onkeypress="javascript:return isNumber(event)" Enabled="false" Placeholder="Total Amount" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtBankChrgAmt" onkeypress="javascript:return isNumber(event)" onblur="BankTot()" ForeColor="Green" Placeholder="Amount" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtBankChrgPen" onkeypress="javascript:return isNumber(event)" Enabled="false" Placeholder="Pending Amount" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 120px">
                                                        <label class="control-label ">Due Date</label>
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtDueDate" Enabled="false" placeholder="DD/MM/YYYY" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-1" style="width: 160px">
                                                        <label id="LblName11" runat="server" class="control-label">Insurance :</label>
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtInsurancePrev" onkeypress="javascript:return isNumber(event)" Enabled="false" Placeholder="Previous Amount" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtInsuranceCurr" onkeypress="javascript:return isNumber(event)" onblur="InsuranceTot()" Placeholder="Current Amount" CssClass="form-control" runat="server" OnTextChanged="GSTCalculate_TextChanged" AutoPostBack="true" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtInsuranceTotal" onkeypress="javascript:return isNumber(event)" Enabled="false" Placeholder="Total Amount" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtInsuranceAmt" onkeypress="javascript:return isNumber(event)" onblur="InsuranceTot()" ForeColor="Green" Placeholder="Amount" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtInsurancePen" onkeypress="javascript:return isNumber(event)" Enabled="false" Placeholder="Pending Amount" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 120px">
                                                        <label class="control-label ">Total DP</label>
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtTotalDP" onkeypress="javascript:return isNumber(event)" Enabled="false" Placeholder="Draw Power" runat="server" CssClass="form-control" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-1" style="width: 160px">
                                                        <label class="control-label ">GST Amount :</label>
                                                    </div>
                                                    <div class="col-md-2" style="width: 130px">
                                                        <asp:TextBox ID="txtGSTPrevAmt" onkeypress="javascript:return isNumber(event)" Enabled="false" Style="background-color: #E8DAEF" Placeholder="Previous Amount" Text="0" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtGSTCurrAmt" onkeypress="javascript:return isNumber(event)" Style="background-color: #E8DAEF" Placeholder="Current Amount" CssClass="form-control" runat="server" OnTextChanged="txtGSTCurrAmt_TextChanged" AutoPostBack="true" />
                                                    </div>
                                                    <div class="col-md-2" style="width: 130px">
                                                        <asp:TextBox ID="txtGSTTotalAmt" onkeypress="javascript:return isNumber(event)" Style="background-color: #E8DAEF" Placeholder="Total Amount" CssClass="form-control" runat="server" OnTextChanged="txtGSTTotalAmt_TextChanged" AutoPostBack="true" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtGSTAmt" onkeypress="javascript:return isNumber(event)" Style="background-color: #E8DAEF" Placeholder="Amount" CssClass="form-control" runat="server" OnTextChanged="txtGSTTotalAmt_TextChanged" AutoPostBack="true" />
                                                    </div>
                                                    <div class="col-md-2" style="width: 130px">
                                                        <asp:TextBox ID="txtGSTTotPendAmt" onkeypress="javascript:return isNumber(event)" Enabled="false" Style="background-color: #E8DAEF" Placeholder="Pending Amount" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 120px">
                                                        <label class="control-label ">Principle OD</label>
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtTotOverdue" onkeypress="javascript:return isNumber(event)" Enabled="false" Placeholder="O/S Overdue" runat="server" CssClass="form-control" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-1" style="width: 160px">
                                                        <label class="control-label ">Total Amount :</label>
                                                    </div>
                                                    <div class="col-md-2" style="width: 130px">
                                                        <asp:TextBox ID="txtTotPrevAmt" onkeypress="javascript:return isNumber(event)" Enabled="false" Style="background-color: #E8DAEF" Placeholder="Total Amount" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtTotCurrAmt" onkeypress="javascript:return isNumber(event)" Enabled="false" Style="background-color: #E8DAEF" Placeholder="Total Amount" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-2" style="width: 130px">
                                                        <asp:TextBox ID="txtTotalAmt" onkeypress="javascript:return isNumber(event)" Enabled="false" Style="background-color: #E8DAEF" Placeholder="Total Amount" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtTotPaidAmt" onkeypress="javascript:return isNumber(event)" Enabled="false" Style="background-color: #E8DAEF" Placeholder="Total Amount" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-2" style="width: 130px">
                                                        <asp:TextBox ID="txtTotPendAmt" onkeypress="javascript:return isNumber(event)" Enabled="false" Style="background-color: #E8DAEF" Placeholder="Total Amount" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1" style="width: 120px">
                                                        <label class="control-label ">Total O/S</label>
                                                    </div>
                                                    <div class="col-md-1" style="width: 130px">
                                                        <asp:TextBox ID="txtOSOverdue" onkeypress="javascript:return isNumber(event)" Enabled="false" ForeColor="Red" Placeholder="Total O/S Overdue" runat="server" CssClass="form-control" />
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
                                                    <div class="col-md-2">
                                                        <asp:DropDownList ID="ddlPayType" OnSelectedIndexChanged="ddlPayType_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <%--<div class="col-md-2">
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Total O/S After Int :</label>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtTotalOS" Enabled="false" runat="server" CssClass="form-control" />
                                                    </div>--%>
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
                                                            <div class="input-icon">
                                                                <i class="fa fa-search"></i>
                                                                <asp:TextBox ID="txtProdName1" CssClass="form-control" PlaceHolder="Search Product Name" OnTextChanged="txtProdName1_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                                                <div id="CustList3" style="height: 200px; overflow-y: scroll;"></div>
                                                                <asp:AutoCompleteExtender ID="autoglname1" runat="server" TargetControlID="txtProdName1" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                    MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList3" ServiceMethod="GetGlName">
                                                                </asp:AutoCompleteExtender>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label ">Account No:<span class="required"> *</span></label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtAccNo1" CssClass="form-control" PlaceHolder="ID" runat="server" AutoPostBack="true" OnTextChanged="TxtAccNo1_TextChanged"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="input-icon">
                                                                <i class="fa fa-search"></i>
                                                                <asp:TextBox ID="TxtAccName1" CssClass="form-control" PlaceHolder="Search Customer Name" runat="server" AutoPostBack="true" OnTextChanged="TxtAccName1_TextChanged"></asp:TextBox>
                                                                <div id="CustList4" style="height: 200px; overflow-y: scroll;"></div>
                                                                <asp:AutoCompleteExtender ID="AutoAccname1" runat="server" TargetControlID="TxtAccName1" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                    MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList4" ServiceMethod="GetAccName">
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
                                                            <label class="control-label ">Amount : <span class="required">*</span></label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtAmount" placeholder="DEBIT AMOUNT" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" Enabled="false"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <asp:HiddenField ID="Response" runat="server" ClientIDMode="Static" />
                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-1 col-md-12">
                                        <asp:Button ID="BtnViewCoBorrow" runat="server" CssClass="btn blue" Text="View Co-Borrower" OnClick="BtnViewCoBorrow_Click" />

                                        <asp:Button ID="btnView" runat="server" CssClass="btn blue" Text="View" OnClick="btnView_Click" />
                                        <asp:Button ID="btnVoucherView" Visible="false" runat="server" CssClass="btn blue" Text="Voucher View" OnClick="btnVoucherView_Click" OnClientClick="Javascript:return IsValide();" />
                                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn blue" Text="Submit" OnClick="Submit_Click" OnClientClick="Javascript:return IsValide(); CheckConfirm(this);" />
                                        <asp:Button ID="Btn_Loan_Sch" runat="server" CssClass="btn blue" Text="Schedule Report" OnClick="Btn_Loan_Sch_Click" OnClientClick="Javascript:return RepIsValid();" />
                                        <asp:Button ID="btnAccStat" runat="server" CssClass="btn blue" Text="Loan Account Statement" OnClick="btnAccStat_Click" />
                                        <asp:Button ID="btnClear" Text="Clear All" runat="server" CssClass="btn blue" OnClick="btnClear_Click" />
                                        <asp:Button ID="BtnViewDt" runat="server" CssClass="btn blue" Text="Other Acc Details" OnClick="BtnViewDt_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                    </div>
                </div>
            </div>
        </div>
        <div id="alertModal" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <center><h4 class="modal-title" style="color:#ff0000">AVS Company</h4></center>
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
    <div class="modal fade bs-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" style="margin-left: 1%; width: 98%">
        <div class="modal-dialog modal-lg" role="document" style="width: 96%">
            <div class="modal-content" style="border: 5px solid #4dbfc0;">
                <div class="inner_top">
                    <div class="panel panel-default">
                        <div class="panel-heading">Loan Details</div>
                        <div class="panel-body">
                            <div class="col-sm-12">
                                <div class="col-lg-12">
                                    <div class="table-responsive">
                                        <div style="height: 250px; overflow: auto">
                                            <asp:GridView ID="GridRecords" FooterStyle-Font-Bold="true" Style="height: 50%" CssClass="tabbable-line" runat="server" AutoGenerateColumns="false" OnRowDataBound="GridRecords_RowDataBound" ShowFooter="true">
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#1e9275" HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="Th" ItemStyle-Width="50px">
                                                        <HeaderTemplate>
                                                            Sr. No.
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                        <FooterTemplate></FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#1e9275" HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="Th" ItemStyle-Width="100px">
                                                        <HeaderTemplate>
                                                            EDate
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%#Eval("EDate1") %>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            Total Amount:
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" HeaderStyle-BackColor="#1e9275" HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="Th" ItemStyle-Width="250px">
                                                        <HeaderTemplate>
                                                            Credit PR
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%#Eval("Credit_PR") %>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblTCr" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" HeaderStyle-BackColor="#1e9275" HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="Th" ItemStyle-Width="250px">
                                                        <HeaderTemplate>
                                                            Debit PR
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%#Eval("Debit_PR") %>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblTDe" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" HeaderStyle-BackColor="#1e9275" HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="Th" ItemStyle-Width="250px">
                                                        <HeaderTemplate>
                                                            Interest Amt
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%#Eval("InterestAmt") %>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblTIn" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" HeaderStyle-BackColor="#1e9275" HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="Th" ItemStyle-Width="250px">
                                                        <HeaderTemplate>
                                                            Penal Int
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%#Eval("PenalInt") %>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblTPI" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" HeaderStyle-BackColor="#1e9275" HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="Th" ItemStyle-Width="250px">
                                                        <HeaderTemplate>
                                                            Int Receivable
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%#Eval("IntReceivable") %>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblTIR" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" HeaderStyle-BackColor="#1e9275" HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="Th" ItemStyle-Width="250px">
                                                        <HeaderTemplate>
                                                            Notice Charge
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%#Eval("NoticeCharge") %>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblTNC" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" HeaderStyle-BackColor="#1e9275" HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="Th" ItemStyle-Width="250px">
                                                        <HeaderTemplate>
                                                            Service Charge
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%#Eval("ServiceCharge") %>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblTSC" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" HeaderStyle-BackColor="#1e9275" HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="Th" ItemStyle-Width="250px">
                                                        <HeaderTemplate>
                                                            Court Charge
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%#Eval("CourtCharge") %>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblTCC" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" HeaderStyle-BackColor="#1e9275" HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="Th" ItemStyle-Width="250px">
                                                        <HeaderTemplate>
                                                            Ser Charge
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%#Eval("SerCharge") %>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblTSC1" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" HeaderStyle-BackColor="#1e9275" HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="Th" ItemStyle-Width="250px">
                                                        <HeaderTemplate>
                                                            Other Charge
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%#Eval("OtherCharge") %>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblTOC" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" HeaderStyle-BackColor="#1e9275" HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="Th" ItemStyle-Width="250px">
                                                        <HeaderTemplate>
                                                            Insurance
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%#Eval("Insurance") %>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblTI" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" HeaderStyle-BackColor="#1e9275" HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="Th" ItemStyle-Width="250px">
                                                        <HeaderTemplate>
                                                            Bank Charge
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%#Eval("BankCharge") %>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblTBC" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" HeaderStyle-BackColor="#1e9275" HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="Th" ItemStyle-Width="250px">
                                                        <HeaderTemplate>
                                                            Total BALANCE
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%#Eval("TBal") %>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblTTC" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" HeaderStyle-BackColor="#1e9275" HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="Th" ItemStyle-Width="250px">
                                                        <HeaderTemplate>
                                                            CL BALANCE
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%#Eval("BALANCE") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
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
                                                                                <%--     <asp:TemplateField HeaderText="SUBGLCODE " Visible="true">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="SUBGLCODE" runat="server" Text='<%# Eval("SUBGLCODE") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>--%>

                                                                               <%-- <asp:BoundField DataField="SUBGLCODE " HeaderText="Product Code" />--%>
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
    <div id="COBORROWER" class="modal fade" role="dialog" align="center">
        <div class="modal-dialog" align="center">

            <!-- Modal content-->
            <div class="modal-dialog" align="center">

                <div class="modal-body" align="center">
                    <div class="row" align="center" style="width: 54%">
                        <div class="col-md-12" align="center">
                            <div class="portlet box green" id="Div3" align="center">
                                <div class="portlet-title">
                                    <div class="caption">
                                        Co-Borrower Details
                                    </div>
                                </div>
                                <div class="portlet-body form">
                                    <div class="form-horizontal">
                                        <div class="form-wizard">
                                            <div class="form-body">
                                                <div class="tab-content">
                                                    <div class="tab-pane active" id="tab1">
                                                        <div class="row" style="margin-bottom: 10px;">
                                                            <div class="row" style="margin: 7px 0 7px 0">
                                                                <div class="col-lg-12" style="height: 50%">
                                                                    <div class="table-scrollable" style="height: auto; overflow-y: scroll; padding-bottom: 10px;">
                                                                        <table class="table table-striped table-bordered table-hover" width="100%">
                                                                            <thead>

                                                                                <tr>
                                                                                    <th>
                                                                                        <asp:GridView ID="grdcoborrower" runat="server"
                                                                                            AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                                                            EditRowStyle-BackColor="#FFFF99"
                                                                                            PagerStyle-CssClass="pgr" Width="100%">
                                                                                            <Columns>
                                                                                                <asp:TemplateField HeaderText="SRNO" Visible="true">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="srno" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>


                                                                                                <asp:TemplateField HeaderText="Name" Visible="true">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="Name" runat="server" Text='<%# Eval("LnSrName") %>'></asp:Label>
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
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-5">
                                                                </div>
                                                                <div class="col-md-6">

                                                                    <asp:Button ID="BtnExitb" runat="server" Text="Close" CssClass="btn btn-primary" data-dismiss="modal" />
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
                                    EditRowStyle-BackColor="#FFFF99" OnSelectedIndexChanged="grdCashRct_SelectedIndexChanged"
                                    OnPageIndexChanging="grdOwgData_PageIndexChanging"
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
                                                <asp:LinkButton ID="LnkPrintReceipt" runat="server" CommandName="select" class="glyphicon glyphicon-plus" OnClick="LnkPrintReceipt_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <%--<asp:TemplateField HeaderText="Authorize" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="select" class="glyphicon glyphicon-plus"></asp:LinkButton>                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

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
    <div class="col-lg-12">
        <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #0A23F9">Customer Account Details  : </strong></div>
    </div>

    <div id="DivGrd1" runat="server" class="col-md-12" visible="false">
        <div class="table-scrollable" style="width: 100%; height: auto; max-height: 150px; overflow-x: auto; overflow-y: auto">
            <table class="table table-striped table-bordered table-hover" width="100%">
                <thead>
                    <tr>
                        <th>
                            <asp:GridView ID="grdAccDetails" runat="server" AlternatingRowStyle-CssClass="alt" CssClass="mGrid"
                                AutoGenerateColumns="false" EditRowStyle-BackColor="#FFFF99" Width="100%" EmptyDataText="No Accounts Available for this Customer">
                                <Columns>

                                    <asp:TemplateField HeaderText="Gl Code" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSubGlCode" runat="server" Text='<%# Eval("SubGlCode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Gl Name" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGlName" runat="server" Text='<%# Eval("GlName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="A/C Number" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAccNo" runat="server" Text='<%# Eval("AccNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Customer Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCustName" runat="server" Text='<%# Eval("CustName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Open Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOpenDate" runat="server" Text='<%# Eval("OpenDate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Close Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCloseDate" runat="server" Text='<%# Eval("CloseDate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Balance">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBalance" runat="server" Text='<%# Eval("Balance") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="OverDue">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOverDue" runat="server" Text='<%# Eval("OverDue") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Acc Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAccType" runat="server" Text='<%# Eval("AccType") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Acc Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAccStatus" runat="server" Text='<%# Eval("AccStatus") %>'></asp:Label>
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

</asp:Content>

