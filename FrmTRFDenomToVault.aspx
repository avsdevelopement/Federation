<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmTRFDenomToVault.aspx.cs" Inherits="FrmTRFDenomToVault" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%--<script type="text/javascript">
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
            var NTS2000 = document.getElementById('<%=txtTCnt1.ClientID%>').value;
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
            var NTS1000 = document.getElementById('<%=txtTCnt2.ClientID%>').value;
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
            var NTS500 = document.getElementById('<%=txtTCnt3.ClientID%>').value;
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

        function NT100(obj) {
            var NTS100 = document.getElementById('<%=txtTCnt4.ClientID%>').value;
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
            var NTS50 = document.getElementById('<%=txtTCnt5.ClientID%>').value;
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
            var NTS20 = document.getElementById('<%=txtTCnt6.ClientID%>').value;
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
            var NTS10 = document.getElementById('<%=txtTCnt7.ClientID%>').value;
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
            var NTS5 = document.getElementById('<%=txtTCnt8.ClientID%>').value;
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
            var NTS2 = document.getElementById('<%=txtTCnt9.ClientID%>').value;
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
            var NTS1 = document.getElementById('<%=txtTCnt10.ClientID%>').value;
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
            var CNS = document.getElementById('<%=txtCoinTake1.ClientID%>').value;
            var CNSAVLBL = document.getElementById('<%=txtVaultCoins1.ClientID%>').value;
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
            var NT100 = document.getElementById('<%=txtTAmt4.ClientID%>').value || 0;
            var NT50 = document.getElementById('<%=txtTAmt5.ClientID%>').value || 0;
            var NT20 = document.getElementById('<%=txtTAmt6.ClientID%>').value || 0;
            var NT10 = document.getElementById('<%=txtTAmt7.ClientID%>').value || 0;
            var NT5 = document.getElementById('<%=txtTAmt8.ClientID%>').value || 0;
            var NT2 = document.getElementById('<%=txtTAmt9.ClientID%>').value || 0;
            var NT1 = document.getElementById('<%=txtTAmt10.ClientID%>').value || 0;
            var CNS = document.getElementById('<%=txtCoinTake1.ClientID%>').value || 0;

            var SUM = parseInt(NT2000) + parseInt(NT1000) + parseInt(NT500) + parseInt(NT100) + parseInt(NT50) + parseInt(NT20) + parseInt(NT10) + parseInt(NT5) + parseInt(NT2) + parseInt(NT1) + parseInt(CNS);
            if (!isNaN(SUM)) {
                document.getElementById('<%=txtBalTake.ClientID%>').value = SUM;
            }
        }
    </script>

    <script type="text/javascript">
        //FOR TAKE AMOUNT fron station to vault
        function NT20001(obj) {
            var NTS2000 = document.getElementById('<%=txtCnt1.ClientID%>').value;
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
            var NTS1000 = document.getElementById('<%=txtCnt2.ClientID%>').value;
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
            var NTS500 = document.getElementById('<%=txtCnt3.ClientID%>').value;
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

        function NT1001(obj) {
            var NTS100 = document.getElementById('<%=txtCnt4.ClientID%>').value;
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
            var NTS50 = document.getElementById('<%=txtCnt5.ClientID%>').value;
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
            var NTS20 = document.getElementById('<%=txtCnt6.ClientID%>').value;
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
            var NTS10 = document.getElementById('<%=txtCnt7.ClientID%>').value;
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
            var NTS5 = document.getElementById('<%=txtCnt8.ClientID%>').value;
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
            var NTS2 = document.getElementById('<%=txtCnt9.ClientID%>').value;
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
            var NTS1 = document.getElementById('<%=txtCnt10.ClientID%>').value;
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
            var CNS = document.getElementById('<%=txtCgive.ClientID%>').value;
            var CNSAVLBL = document.getElementById('<%=txtCAvlbl.ClientID%>').value;
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
            var NT100 = document.getElementById('<%=txtAmt4.ClientID%>').value || 0;
            var NT50 = document.getElementById('<%=txtAmt5.ClientID%>').value || 0;
            var NT20 = document.getElementById('<%=txtAmt6.ClientID%>').value || 0;
            var NT10 = document.getElementById('<%=txtAmt7.ClientID%>').value || 0;
            var NT5 = document.getElementById('<%=txtAmt8.ClientID%>').value || 0;
            var NT2 = document.getElementById('<%=txtAmt9.ClientID%>').value || 0;
            var NT1 = document.getElementById('<%=txtAmt10.ClientID%>').value || 0;
            var CNS = document.getElementById('<%=txtCgive.ClientID%>').value || 0;

            var SUM = parseInt(NT2000) + parseInt(NT1000) + parseInt(NT500) + parseInt(NT100) + parseInt(NT50) + parseInt(NT20) + parseInt(NT10) + parseInt(NT5) + parseInt(NT2) + parseInt(NT1) + parseInt(CNS);
            if (!isNaN(SUM)) {
                document.getElementById('<%=txtBalGive.ClientID%>').value = SUM;
            }
        }
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<div class="row">
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
                                                <div class="col-md-1">
                                                    <label class="control-label ">BranchCode:</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="ddlBrCode" CssClass="form-control" runat="server"></asp:DropDownList>
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">User/Station No:</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtStationNo" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">TRF To Vault No:</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtTRFNo" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 20px; border-bottom: 1px solid rgba(53, 152, 220, 0.55);"><strong></strong></div>
                             
                                    <asp:Panel ID="Panel1" CssClass="Panel" style="margin-left:auto;margin-right:auto;" GroupingText="Denomination Table (This Station)" runat="server" Width="100%" Height="280px">                
                                        <div class="DivScroll" style="margin-left:auto;margin-right:auto;height:210px;width:900px;">

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-6">
                                                        <label class="control-label">Available Denomination</label>
                                                    </div>
                                                    <div class="col-md-6">                                                      
                                                            <label class="control-label ">Give Denomination</label>                                                        
                                                    </div>
                                                </div>
                                            </div>

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
                                                        <label class="control-label ">Count</label>
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
                                                        <asp:TextBox ID="txtDm1" Text="2000" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAvlblC1" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtCnt1" onkeyup="NT20001()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAmt1" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtDm2" Text="1000" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAvlblC2" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtCnt2" onkeyup="NT10001()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAmt2" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtDm3" Text="500" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAvlblC3" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtCnt3" onkeyup="NT5001()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAmt3" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtDm4" Text="100" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAvlblC4" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtCnt4" onkeyup="NT1001()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAmt4" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtDm5" Text="50" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAvlblC5" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtCnt5" onkeyup="NT501()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAmt5" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtDm6" Text="20" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAvlblC6" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtCnt6" onkeyup="NT201()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAmt6" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtDm7" Text="10" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAvlblC7" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtCnt7" onkeyup="NT101()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAmt7" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtDm8" Text="5" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAvlblC8" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtCnt8" onkeyup="NT51()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAmt8" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtDm9" Text="2" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAvlblC9" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtCnt9" onkeyup="NT21()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAmt9" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtDm10" Text="1" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAvlblC10" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtCnt10" onkeyup="NT11()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAmt10" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </asp:Panel>
                                    
                                    <div class="row" style="margin: 20px; border-bottom: 1px solid rgba(53, 152, 220, 0.55);"><strong></strong></div>

                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-1">
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">Coins Give :</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtCgive" onkeyup="TakeCns()" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-1">
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">Coins Available :</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtCAvlbl" CssClass="form-control" runat="server" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-1">
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">SolidNotes Give :</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtSGive" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-1">
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">SolidNotes Available :</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtSNAvlbl" CssClass="form-control" runat="server" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-1">
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">Balance Give :</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtBalGive" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-1">
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">Avlble CashBalance :</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtAvlblCBal" CssClass="form-control" runat="server" />
                                                </div>
                                            </div>
                                        </div>

                                    <div class="row" style="margin: 20px; border-bottom: 1px solid rgba(53, 152, 220, 0.55);"><strong></strong></div>
                                    
                                    <asp:Panel ID="Panel2" CssClass="Panel" style="margin-left:auto;margin-right:auto;" GroupingText="Available Denomination (User Selected Vault)" runat="server" Width="90%" Height="180px">     
                                        <div class="DivScroll2" style="margin-left:auto;margin-right:auto;height:140px;width:700px;" >

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TextBox27" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TextBox28" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="TextBox30" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TextBox31" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TextBox42" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="TextBox43" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TextBox44" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TextBox45" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="TextBox46" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TextBox47" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TextBox48" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="TextBox49" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TextBox50" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TextBox51" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="TextBox52" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TextBox53" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TextBox54" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="TextBox55" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TextBox56" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TextBox57" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="TextBox58" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </asp:Panel>

                                    <div class="row" style="margin: 20px; border-bottom: 1px solid rgba(53, 152, 220, 0.55);"><strong></strong></div>

                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label ">Vault Coins :</label>
                                                </div>
                                                <div class="col-md-2">
                                                  <asp:TextBox ID="txtVaultC" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">Vault Solid Notes :</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtVSNts" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">Vault Balance :</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtVBal" CssClass="form-control" runat="server" />
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-5 col-md-7">
                                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn blue" Text="OK" OnClientClick="Javascript:return isvalidate();" />
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
                                                <div class="col-md-1">
                                                    <label class="control-label ">BranchCode:</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="ddlBrCode1" CssClass="form-control" runat="server"></asp:DropDownList>
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">User/Station No:</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtStnNo1" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">TRF From Vault No:</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtTRFFrom" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 20px; border-bottom: 1px solid rgba(53, 152, 220, 0.55);"><strong></strong></div>
                             
                                    <asp:Panel ID="Panel3" CssClass="Panel" style="margin-left:auto;margin-right:auto;" GroupingText="Denomination Table (User Selected Value)" runat="server" Width="100%" Height="280px">                
                                        <div class="DivScroll" style="margin-left:auto;margin-right:auto;height:210px;width:900px;">

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-6">
                                                        <label class="control-label">Available Denomination</label>
                                                    </div>
                                                    <div class="col-md-6">                                                      
                                                            <label class="control-label ">Take Denomination</label>                                                        
                                                    </div>
                                                </div>
                                            </div>

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
                                                        <label class="control-label ">Count</label>
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
                                                        <asp:TextBox ID="txtDenom1" Text="2000" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAvlblCnt1" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtTCnt1" onkeyup="NT2000()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtTAmt1" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtDenom2" Text="1000" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAvlblCnt2" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtTCnt2" onkeyup="NT1000()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtTAmt2" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtDenom3" Text="500" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAvlblCnt3" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtTCnt3" onkeyup="NT500()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtTAmt3" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtDenom4" Text="100" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAvlblCnt4" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtTCnt4" onkeyup="NT100()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtTAmt4" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtDenom5" Text="50" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAvlblCnt5" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtTCnt5" onkeyup="NT50()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtTAmt5" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtDenom6" Text="20" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAvlblCnt6" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtTCnt6" onkeyup="NT20()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtTAmt6" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtDenom7" Text="10" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAvlblCnt7" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtTCnt7" onkeyup="NT10()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtTAmt7" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtDenom8" Text="5" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAvlblCnt8" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtTCnt8" onkeyup="NT5()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtTAmt8" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtDenom9" Text="2" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAvlblCnt9" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtTCnt9" onkeyup="NT2()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtTAmt9" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtDenom10" Text="1" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtAvlblCnt10" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtTCnt10" onkeyup="NT1()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtTAmt10" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </asp:Panel>
                                    
                                    <div class="row" style="margin: 20px; border-bottom: 1px solid rgba(53, 152, 220, 0.55);"><strong></strong></div>

                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-1">
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">Coins Take :</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtCoinTake1" onkeyup="TakeCoins()" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-1">
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">Vault Coins :</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtVaultCoins1" CssClass="form-control" runat="server" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-1">
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">SolidNotes Take :</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtSolidNtsTake" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-1">
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">Vault SolidNotes :</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtVaultSolidNts" CssClass="form-control" runat="server" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-1">
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">Balance Take :</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtBalTake" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-1">
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">Vault CashBalance :</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtVaultCashBal" CssClass="form-control" runat="server" />
                                                </div>
                                            </div>
                                        </div>

                                    <div class="row" style="margin: 20px; border-bottom: 1px solid rgba(53, 152, 220, 0.55);"><strong></strong></div>
                                    
                                    <asp:Panel ID="Panel4" CssClass="Panel" style="margin-left:auto;margin-right:auto;" GroupingText="Denomination (This Station)" runat="server" Width="90%" Height="180px">     
                                        <div class="DivScroll2" style="margin-left:auto;margin-right:auto;height:140px;width:700px;" >

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TextBox89" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TextBox90" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="TextBox91" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TextBox92" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TextBox93" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="TextBox94" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TextBox95" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TextBox96" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="TextBox97" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TextBox98" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TextBox99" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="TextBox100" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TextBox101" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TextBox102" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="TextBox103" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TextBox104" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TextBox105" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="TextBox106" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TextBox107" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TextBox108" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="TextBox109" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </asp:Panel>
                                    
                                    <div class="row" style="margin: 20px; border-bottom: 1px solid rgba(53, 152, 220, 0.55);"><strong></strong></div>

                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label ">Station Coins :</label>
                                                </div>
                                                <div class="col-md-2">
                                                  <asp:TextBox ID="txtStationCoins" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">Station SolidNotes :</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtSolidNotes" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">Station Balance :</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtStationBal" CssClass="form-control" runat="server" />
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-5 col-md-7">
                                        <asp:Button ID="btnOkFrom" OnClick="btnOkFrom_Click" runat="server" CssClass="btn blue" Text="OK" OnClientClick="Javascript:return isvalidate();" />
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
    </div>--%>

</asp:Content>

