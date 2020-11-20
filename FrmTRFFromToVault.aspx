<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmTRFFromToVault.aspx.cs" Inherits="FrmTRFDenomToVault" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
        function isvalidate() {
            var AGCODE;
            AMT = document.getElementById('<%=txtTRFto1.ClientID%>').value;
            var message = '';

            if (AGCODE == "") {
                message = 'Please Enter Vault Number...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtTRFto1.ClientID%>').focus();
                return false;
            }
        }

        function isvalidate1() {
            var AGCODE;
            AMT = document.getElementById('<%=txtTRFTo.ClientID%>').value;
            var message = '';

            if (AGCODE == "") {
                message = 'Please Enter Vault Number...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtTRFTo.ClientID%>').focus();
                return false;
            }
        }

        function FormatIt(obj) {
            if (obj.value.length == 2) // Day
                obj.value = obj.value + "/";
            if (obj.value.length == 5) // month 
                obj.value = obj.value + "/";
            if (obj.value.length == 11) // year 
                alert("Please Enter valid Date");
        }

        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;

            return true;
        }
    </script>
    <style type='text/css'>
        .DivScroll {
            overflow-x: hidden;
            overflow-y: auto;
            background-color: lightgray;
            width: 80%;
            height: 210px;            
        }

        .DivScroll2 {
            overflow-x: hidden;
            overflow-y: auto;
            background-color: lightgray;
            width: 60%;
            height: 180px;            
        }

        .Panel legend {
            font-size:small;
            }
    </style>
    <script type="text/javascript">
        //FOR TAKE AMOUNT
        function NT2000(obj) {
            var NTS2000 = document.getElementById('<%=txtTCnt1.ClientID%>').value || 0;
            var NTSAVLBL = document.getElementById('<%=txtAvlblCnt1.ClientID%>').value;
            if (parseInt(NTS2000) <= parseInt(NTSAVLBL)) {
                var Result = parseInt(NTS2000) * 2000;
                if (!isNaN(Result)) {
                    document.getElementById('<%=txtTAmt1.ClientID %>').value = Result;
                    GiveTotal();
                    return true;
                }
            }
            else {
                var message = 'Notes Not Available....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtTCnt1.ClientID%>').focus();
                return false;
            }
        }

        function NT1000(obj) {
            var NTS1000 = document.getElementById('<%=txtTCnt2.ClientID%>').value || 0;
            var NTSAVLBL = document.getElementById('<%=txtAvlblCnt2.ClientID%>').value;

            if (parseInt(NTS1000) <= parseInt(NTSAVLBL)) {
                var Result = parseInt(NTS1000) * 1000;
                if (!isNaN(Result)) {
                    document.getElementById('<%=txtTAmt2.ClientID %>').value = Result;
                    GiveTotal();
                    return true;
                }
            }
            else {
                var message = 'Notes Not Available....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtTCnt2.ClientID%>').focus();
                return false;
            }
        }

        function NT500(obj) {
            var NTS500 = document.getElementById('<%=txtTCnt3.ClientID%>').value || 0;
            var NTSAVLBL = document.getElementById('<%=txtAvlblCnt3.ClientID%>').value;

            if (parseInt(NTS500) <= parseInt(NTSAVLBL)) {
                var Result = parseInt(NTS500) * 500;
                if (!isNaN(Result)) {
                    document.getElementById('<%=txtTAmt3.ClientID %>').value = Result;
                    GiveTotal();
                    return true;
                }
            }
            else {
                var message = 'Notes Not Available....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtTCnt3.ClientID%>').focus();
                return false;
            }
        }

        function NT200(obj) {
            var NTS200 = document.getElementById('<%=txtTCnt12.ClientID%>').value || 0;
            var NTSAVLBL = document.getElementById('<%=txtAvlblCnt12.ClientID%>').value;

            if (parseInt(NTS200) <= parseInt(NTSAVLBL)) {
                var Result = parseInt(NTS200) * 200;
                if (!isNaN(Result)) {
                    document.getElementById('<%=txtTAmt12.ClientID %>').value = Result;
                    GiveTotal();
                    return true;
                }
            }
            else {
                var message = 'Notes Not Available....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtTCnt12.ClientID%>').focus();
                return false;
            }
        }

        function NT100(obj) {
            var NTS100 = document.getElementById('<%=txtTCnt4.ClientID%>').value || 0;
            var NTSAVLBL = document.getElementById('<%=txtAvlblCnt4.ClientID%>').value;

            if (parseInt(NTS100) <= parseInt(NTSAVLBL)) {
                var Result = parseInt(NTS100) * 100;
                if (!isNaN(Result)) {
                    document.getElementById('<%=txtTAmt4.ClientID %>').value = Result;
                    GiveTotal();
                    return true;
                }
            }
            else {
                var message = 'Notes Not Available....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtTCnt4.ClientID%>').focus();
                return false;
            }
        }

        function NT50(obj) {
            var NTS50 = document.getElementById('<%=txtTCnt5.ClientID%>').value || 0;
            var NTSAVLBL = document.getElementById('<%=txtAvlblCnt5.ClientID%>').value;

            if (parseInt(NTS50) <= parseInt(NTSAVLBL)) {
                var Result = parseInt(NTS50) * 50;
                if (!isNaN(Result)) {
                    document.getElementById('<%=txtTAmt5.ClientID %>').value = Result;
                    GiveTotal();
                    return true;
                }
            }
            else {
                var message = 'Notes Not Available....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtTCnt5.ClientID%>').focus();
                return false;
            }
        }

        function NT20(obj) {
            var NTS20 = document.getElementById('<%=txtTCnt6.ClientID%>').value || 0;
            var NTSAVLBL = document.getElementById('<%=txtAvlblCnt6.ClientID%>').value;

            if (parseInt(NTS20) <= parseInt(NTSAVLBL)) {
                var Result = parseInt(NTS20) * 20;
                if (!isNaN(Result)) {
                    document.getElementById('<%=txtTAmt6.ClientID %>').value = Result;
                    GiveTotal();
                    return true;
                }
            }
            else {
                var message = 'Notes Not Available....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtTCnt6.ClientID%>').focus();
                return false;
            }
        }

        function NT10(obj) {
            var NTS10 = document.getElementById('<%=txtTCnt7.ClientID%>').value || 0;
            var NTSAVLBL = document.getElementById('<%=txtAvlblCnt7.ClientID%>').value;

            if (parseInt(NTS10) <= parseInt(NTSAVLBL)) {
                var Result = parseInt(NTS10) * 10;
                if (!isNaN(Result)) {
                    document.getElementById('<%=txtTAmt7.ClientID %>').value = Result;
                    GiveTotal();
                    return true;
                }
            }
            else {
                var message = 'Notes Not Available....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtTCnt7.ClientID%>').focus();
                return false;
            }
        }

        function NT5(obj) {
            var NTS5 = document.getElementById('<%=txtTCnt8.ClientID%>').value || 0;
            var NTSAVLBL = document.getElementById('<%=txtAvlblCnt8.ClientID%>').value;

            if (parseInt(NTS5) <= parseInt(NTSAVLBL)) {
                var Result = parseInt(NTS5) * 5;
                if (!isNaN(Result)) {
                    document.getElementById('<%=txtTAmt8.ClientID %>').value = Result;
                    GiveTotal();
                    return true;
                }
            }
            else {
                var message = 'Notes Not Available....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtTCnt8.ClientID%>').focus();
                return false;
            }
        }

        function NT2(obj) {
            var NTS2 = document.getElementById('<%=txtTCnt9.ClientID%>').value || 0;
            var NTSAVLBL = document.getElementById('<%=txtAvlblCnt9.ClientID%>').value;

            if (parseInt(NTS2) <= parseInt(NTSAVLBL)) {
                var Result = parseInt(NTS2) * 2;
                if (!isNaN(Result)) {
                    document.getElementById('<%=txtTAmt9.ClientID %>').value = Result;
                    GiveTotal();
                    return true;
                }
            }
            else {
                var message = 'Notes Not Available....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtTCnt9.ClientID%>').focus();
                return false;
            }
        }

        function NT1(obj) {
            var NTS1 = document.getElementById('<%=txtTCnt10.ClientID%>').value || 0;
            var NTSAVLBL = document.getElementById('<%=txtAvlblCnt10.ClientID%>').value;

            if (parseInt(NTS1) <= parseInt(NTSAVLBL)) {
                var Result = parseInt(NTS1) * 1;
                if (!isNaN(Result)) {
                    document.getElementById('<%=txtTAmt10.ClientID %>').value = Result;
                    GiveTotal();
                    return true;
                }
            }
            else {
                var message = 'Notes Not Available....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtTCnt10.ClientID%>').focus();
                return false;
            }
        }

        function TakeCoins(obj) {
            var CNS = document.getElementById('<%=txtCoinTake1.ClientID%>').value || 0;
            var CNSAVLBL = document.getElementById('<%=txtVaultCoins1.ClientID%>').value || 0;
            debugger;
            if (parseInt(CNS) <= parseInt(CNSAVLBL)) {
                if (!isNaN(CNS)) {
                    GiveTotal();
                    return true;
                }
            }
            else {
                var message = 'Only '+CNSAVLBL+' Coins Available....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtTCnt10.ClientID%>').focus();
                return false;
            }
        }
    </Script>
    <script type="text/javascript">
        //FOR GIVE TOTAL
        function GiveTotal() {
            debugger;
            var NT2000 = document.getElementById('<%=txtTAmt1.ClientID%>').value || 0;
            var NT1000 = document.getElementById('<%=txtTAmt2.ClientID%>').value || 0;
            var NT500 = document.getElementById('<%=txtTAmt3.ClientID%>').value || 0;
            var NT200 = document.getElementById('<%=txtTAmt12.ClientID%>').value || 0;
            var NT100 = document.getElementById('<%=txtTAmt4.ClientID%>').value || 0;
            var NT50 = document.getElementById('<%=txtTAmt5.ClientID%>').value || 0;
            var NT20 = document.getElementById('<%=txtTAmt6.ClientID%>').value || 0;
            var NT10 = document.getElementById('<%=txtTAmt7.ClientID%>').value || 0;
            var NT5 = document.getElementById('<%=txtTAmt8.ClientID%>').value || 0;
            var NT2 = document.getElementById('<%=txtTAmt9.ClientID%>').value || 0;
            var NT1 = document.getElementById('<%=txtTAmt10.ClientID%>').value || 0;
            var CNS = document.getElementById('<%=txtCoinTake1.ClientID%>').value || 0;

            var SUM = parseInt(NT2000) + parseInt(NT1000) + parseInt(NT500) + parseFloat(NT200) + parseInt(NT100) + parseInt(NT50) + parseInt(NT20) + parseInt(NT10) + parseInt(NT5) + parseInt(NT2) + parseInt(NT1) + parseInt(CNS);
            if (!isNaN(SUM)) {
                document.getElementById('<%=txtBalTake.ClientID%>').value = SUM;
            }
        }
    </script>

    <script type="text/javascript">
        //FOR TAKE AMOUNT fron station to vault
        function NT20001(obj) {
            var NTS2000 = document.getElementById('<%=txtCnt1.ClientID%>').value || 0;
            var NTSAVLBL = document.getElementById('<%=txtAvlblC1.ClientID%>').value;
            if (parseInt(NTS2000) <= parseInt(NTSAVLBL)) {
                var Result = parseInt(NTS2000) * 2000;
                if (!isNaN(Result)) {
                    document.getElementById('<%=txtAmt1.ClientID %>').value = Result;
                    TakeTotal();
                    return true;
                }
            }
            else {
                var message = 'Notes Not Available....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtCnt1.ClientID%>').focus();
                return false;
            }
        }

        function NT10001(obj) {
            var NTS1000 = document.getElementById('<%=txtCnt2.ClientID%>').value || 0;
            var NTSAVLBL = document.getElementById('<%=txtAvlblC2.ClientID%>').value;

            if (parseInt(NTS1000) <= parseInt(NTSAVLBL)) {
                var Result = parseInt(NTS1000) * 1000;
                if (!isNaN(Result)) {
                    document.getElementById('<%=txtAmt2.ClientID %>').value = Result;
                    TakeTotal();
                    return true;
                }
            }
            else {
                var message = 'Notes Not Available....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtCnt2.ClientID%>').focus();
                return false;
            }
        }

        function NT5001(obj) { 
            var NTS500 = document.getElementById('<%=txtCnt3.ClientID%>').value || 0;
            var NTSAVLBL = document.getElementById('<%=txtAvlblC3.ClientID%>').value;

            if (parseInt(NTS500) <= parseInt(NTSAVLBL)) {
                var Result = parseInt(NTS500) * 500;
                if (!isNaN(Result)) {
                    document.getElementById('<%=txtAmt3.ClientID %>').value = Result;
                    TakeTotal();
                    return true;
                }
            }
            else {
                var message = 'Notes Not Available....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtCnt3.ClientID%>').focus();
                return false;
            }
        }

        function NT2001(obj) {
            var NTS200 = document.getElementById('<%=txtCnt12.ClientID%>').value || 0;
            var NTSAVLBL = document.getElementById('<%=txtAvlblC12.ClientID%>').value;

            if (parseInt(NTS200) <= parseInt(NTSAVLBL)) {
                var Result = parseInt(NTS200) * 200;
                if (!isNaN(Result)) {
                    document.getElementById('<%=txtAmt12.ClientID %>').value = Result;
                    TakeTotal();
                    return true;
                }
            }
            else {
                var message = 'Notes Not Available....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtCnt12.ClientID%>').focus();
                return false;
            }
        }

        function NT1001(obj) {
            var NTS100 = document.getElementById('<%=txtCnt4.ClientID%>').value || 0;
            var NTSAVLBL = document.getElementById('<%=txtAvlblC4.ClientID%>').value;

            if (parseInt(NTS100) <= parseInt(NTSAVLBL)) {
                var Result = parseInt(NTS100) * 100;
                if (!isNaN(Result)) {
                    document.getElementById('<%=txtAmt4.ClientID %>').value = Result;
                    TakeTotal();
                    return true;
                }
            }
            else {
                var message = 'Notes Not Available....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtCnt4.ClientID%>').focus();
                return false;
            }
        }

        function NT501(obj) {
            var NTS50 = document.getElementById('<%=txtCnt5.ClientID%>').value || 0;
            var NTSAVLBL = document.getElementById('<%=txtAvlblC5.ClientID%>').value;

            if (parseInt(NTS50) <= parseInt(NTSAVLBL)) {
                var Result = parseInt(NTS50) * 50;
                if (!isNaN(Result)) {
                    document.getElementById('<%=txtAmt5.ClientID %>').value = Result;
                    TakeTotal();
                    return true;
                }
            }
            else {
                var message = 'Notes Not Available....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtCnt5.ClientID%>').focus();
                return false;
            }
        }

        function NT201(obj) {
            var NTS20 = document.getElementById('<%=txtCnt6.ClientID%>').value || 0;
            var NTSAVLBL = document.getElementById('<%=txtAvlblC6.ClientID%>').value;

            if (parseInt(NTS20) <= parseInt(NTSAVLBL)) {
                var Result = parseInt(NTS20) * 20;
                if (!isNaN(Result)) {
                    document.getElementById('<%=txtAmt6.ClientID %>').value = Result;
                    TakeTotal();
                    return true;
                }
            }
            else {
                var message = 'Notes Not Available....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtCnt6.ClientID%>').focus();
                return false;
            }
        }

        function NT101(obj) {
            var NTS10 = document.getElementById('<%=txtCnt7.ClientID%>').value || 0;
            var NTSAVLBL = document.getElementById('<%=txtAvlblC7.ClientID%>').value;

            if (parseInt(NTS10) <= parseInt(NTSAVLBL)) {
                var Result = parseInt(NTS10) * 10;
                if (!isNaN(Result)) {
                    document.getElementById('<%=txtAmt7.ClientID %>').value = Result;
                    TakeTotal();
                    return true;
                }
            }
            else {
                var message = 'Notes Not Available....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtCnt7.ClientID%>').focus();
                return false;
            }
        }

        function NT51(obj) {
            var NTS5 = document.getElementById('<%=txtCnt8.ClientID%>').value || 0;
            var NTSAVLBL = document.getElementById('<%=txtAvlblC8.ClientID%>').value;

            if (parseInt(NTS5) <= parseInt(NTSAVLBL)) {
                var Result = parseInt(NTS5) * 5;
                if (!isNaN(Result)) {
                    document.getElementById('<%=txtAmt8.ClientID %>').value = Result;
                    TakeTotal();
                    return true;
                }
            }
            else {
                var message = 'Notes Not Available....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtCnt8.ClientID%>').focus();
                return false;
            }
        }

        function NT21(obj) {
            var NTS2 = document.getElementById('<%=txtCnt9.ClientID%>').value || 0;
            var NTSAVLBL = document.getElementById('<%=txtAvlblC9.ClientID%>').value;

            if (parseInt(NTS2) <= parseInt(NTSAVLBL)) {
                var Result = parseInt(NTS2) * 2;
                if (!isNaN(Result)) {
                    document.getElementById('<%=txtAmt9.ClientID %>').value = Result;
                    TakeTotal();
                    return true;
                }
            }
            else {
                var message = 'Notes Not Available....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtCnt9.ClientID%>').focus();
                return false;
            }
        }

        function NT11(obj) {
            var NTS1 = document.getElementById('<%=txtCnt10.ClientID%>').value || 0;
            var NTSAVLBL = document.getElementById('<%=txtAvlblC10.ClientID%>').value;

            if (parseInt(NTS1) <= parseInt(NTSAVLBL)) {
                var Result = parseInt(NTS1) * 1;
                if (!isNaN(Result)) {
                    document.getElementById('<%=txtAmt10.ClientID %>').value = Result;
                    TakeTotal();
                    return true;
                }
            }
            else {
                var message = 'Notes Not Available....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtCnt10.ClientID%>').focus();
                return false;
            }
        }

        function TakeCns(obj) {
            var CNS = document.getElementById('<%=txtCgive.ClientID%>').value || 0;
            var CNSAVLBL = document.getElementById('<%=txtCAvlbl.ClientID%>').value||0;
            debugger;
            if (parseInt(CNS) <= parseInt(CNSAVLBL)) {
                if (!isNaN(CNS)) {
                    TakeTotal();
                    return true;
                }
            }
            else {
                var message = 'Only ' + CNSAVLBL + ' Coins Available....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtCgive.ClientID%>').focus();
                return false;
            }
        }
    </Script>
    <script type="text/javascript">
        //FOR GIVE TOTAL
        function TakeTotal() {
            debugger;
            var NT2000 = document.getElementById('<%=txtAmt1.ClientID%>').value || 0;
            var NT1000 = document.getElementById('<%=txtAmt2.ClientID%>').value || 0;
            var NT500 = document.getElementById('<%=txtAmt3.ClientID%>').value || 0;
            var NT200 = document.getElementById('<%=txtAmt12.ClientID%>').value || 0;
            var NT100 = document.getElementById('<%=txtAmt4.ClientID%>').value || 0;
            var NT50 = document.getElementById('<%=txtAmt5.ClientID%>').value || 0;
            var NT20 = document.getElementById('<%=txtAmt6.ClientID%>').value || 0;
            var NT10 = document.getElementById('<%=txtAmt7.ClientID%>').value || 0;
            var NT5 = document.getElementById('<%=txtAmt8.ClientID%>').value || 0;
            var NT2 = document.getElementById('<%=txtAmt9.ClientID%>').value || 0;
            var NT1 = document.getElementById('<%=txtAmt10.ClientID%>').value || 0;
            var CNS = document.getElementById('<%=txtCgive.ClientID%>').value || 0;

            var SUM = parseInt(NT2000) + parseInt(NT1000) + parseInt(NT500) + parseFloat(NT200) + parseInt(NT100) + parseInt(NT50) + parseInt(NT20) + parseInt(NT10) + parseInt(NT5) + parseInt(NT2) + parseInt(NT1) + parseInt(CNS);
            if (!isNaN(SUM)) {
                document.getElementById('<%=txtBalGive.ClientID%>').value = SUM;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">

        <div id="DIVTRFFROM" class="col-md-12" runat="server" visible="true">
            <div class="portlet box blue" id="Div1">
                <div class="portlet-title">
                    <div class="caption">
                        TRANSFER OF DENOMINATIONS FROM VAULT
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div class="tab-pane active" id="Div2">

                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-1"></div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">Tr From Vault</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtTRFFrom" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-md-1">
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">Tr To Vault</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtTRFTo" OnTextChanged="txtTRFTo_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="DivScroll" style="margin-left:auto;margin-right:auto;height:210px;width:900px;">

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Denomination</label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Count</label>
                                                    </div>
                                                    <div class="col-md-2">
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Count Take</label>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <label class="control-label ">Amount Take</label>
                                                    </div>
                                                    <div class="col-md-1">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtDenom1" Enabled="false" Text="2000" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAvlblCnt1" Enabled="false" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtTCnt1" onblur="NT2000()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtTAmt1" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtDenom2" Enabled="false" Text="1000" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAvlblCnt2" Enabled="false" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtTCnt2" onblur="NT1000()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtTAmt2" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtDenom3" Enabled="false" Text="500" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAvlblCnt3" Enabled="false" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtTCnt3" onblur="NT500()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtTAmt3" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                            
                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtDenom12" Enabled="false" Text="200" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAvlblCnt12" Enabled="false" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtTCnt12" onblur="NT200()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtTAmt12" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtDenom4" Enabled="false" Text="100" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAvlblCnt4" Enabled="false" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtTCnt4" onblur="NT100()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtTAmt4" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtDenom5" Enabled="false" Text="50" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAvlblCnt5" Enabled="false" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtTCnt5" onblur="NT50()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtTAmt5" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtDenom6" Enabled="false" Text="20" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAvlblCnt6" Enabled="false" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtTCnt6" onblur="NT20()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtTAmt6" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtDenom7" Enabled="false" Text="10" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAvlblCnt7" Enabled="false" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtTCnt7" onblur="NT10()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtTAmt7" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtDenom8" Enabled="false" Text="5" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAvlblCnt8" Enabled="false" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtTCnt8" onblur="NT5()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtTAmt8" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtDenom9" Enabled="false" Text="2" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAvlblCnt9" Enabled="false" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtTCnt9" onblur="NT2()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtTAmt9" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtDenom10" Enabled="false" Text="1" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAvlblCnt10" Enabled="false" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtTCnt10" onblur="NT1()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtTAmt10" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                        <%--Div Scroll End Here--%>

                                    <div class="row" style="margin: 20px; border-bottom: 1px solid rgba(53, 152, 220, 0.55);"><strong></strong></div>
                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-1">
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">Vault Coins :</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtVaultCoins1" Enabled="false" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">Coins Take :</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtCoinTake1" onblur="TakeCoins()" CssClass="form-control" runat="server" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-1">
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">Vault SolidNotes :</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtVaultSolidNts" Enabled="false" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">SolidNotes Take :</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtSolidNtsTake" CssClass="form-control" runat="server" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-1">
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">Vault CashBalance :</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtVaultCashBal" Enabled="false" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">Balance Take :</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtBalTake" CssClass="form-control" runat="server" />
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-5 col-md-7">
                                        <asp:Button ID="btnOkFrom" OnClick="btnOkFrom_Click" runat="server" CssClass="btn blue" Text="Submit" OnClientClick="Javascript:return isvalidate1();" />
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

        <div id="DIVTRFTO" class="col-md-12" runat="server" visible="false">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        TRANSFER OF DENOMINATIONS TO VAULT
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab1">

                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-1"></div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">Tr From Vault</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtTRFFrom1" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-md-1">
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">Tr To Vault</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtTRFto1" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="DivScroll" style="margin-left:auto;margin-right:auto;height:210px;width:900px;">

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Denomination</label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Count Available</label>
                                                    </div>
                                                    <div class="col-md-2">
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Count Give</label>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <label class="control-label ">Amount Give</label>
                                                    </div>
                                                    <div class="col-md-1">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtDm1" Enabled="false" Text="2000" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAvlblC1" Enabled="false" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtCnt1" onblur="NT20001()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAmt1" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtDm2" Enabled="false" Text="1000" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAvlblC2" Enabled="false" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtCnt2" onblur="NT10001()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAmt2" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtDm3" Enabled="false" Text="500" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAvlblC3" Enabled="false" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtCnt3" onblur="NT5001()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAmt3" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                            
                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtDm12" Enabled="false" Text="200" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAvlblC12" Enabled="false" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtCnt12" onblur="NT2001()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAmt12" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtDm4" Enabled="false" Text="100" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAvlblC4" Enabled="false" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtCnt4" onblur="NT1001()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAmt4" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtDm5" Enabled="false" Text="50" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAvlblC5" Enabled="false" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtCnt5" onblur="NT501()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAmt5" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtDm6" Enabled="false" Text="20" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAvlblC6" Enabled="false" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtCnt6" onblur="NT201()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAmt6" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtDm7" Enabled="false" Text="10" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAvlblC7" Enabled="false" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtCnt7" onblur="NT101()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAmt7" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtDm8" Enabled="false" Text="5" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAvlblC8" Enabled="false" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtCnt8" onblur="NT51()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAmt8" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtDm9" Enabled="false" Text="2" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAvlblC9" Enabled="false" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtCnt9" onblur="NT21()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAmt9" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtDm10" Enabled="false" Text="1" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAvlblC10" Enabled="false" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtCnt10" onblur="NT11()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAmt10" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                        <%--Div Scroll End Here--%>

                                    <div class="row" style="margin: 20px; border-bottom: 1px solid rgba(53, 152, 220, 0.55);"><strong></strong></div>

                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-1">
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">Coins Available :</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtCAvlbl" Enabled="false" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">Coins Give :</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtCgive" onblur="TakeCns()" CssClass="form-control" runat="server" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-1">
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">SolidNotes Available :</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtSNAvlbl" Enabled="false" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">SolidNotes Give :</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtSGive" CssClass="form-control" runat="server" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-1">
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">Avlble CashBalance :</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtAvlblCBal" Enabled="false" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">Balance Give :</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtBalGive" CssClass="form-control" runat="server" />
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-5 col-md-7">
                                        <asp:Button ID="btnGive" OnClick="btnGive_Click" runat="server" CssClass="btn blue" Text="Submit" OnClientClick="Javascript:return isvalidate();" />
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

</asp:Content>

