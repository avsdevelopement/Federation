<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmExchDenom.aspx.cs" Inherits="FrmExchDenom" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type='text/css'>
        .DivScroll
        {
            overflow-x: hidden;
            overflow-y: auto;
            background-color:lightgray;
            width:1000px;
            height:220px;
        }
    </style>
    <script type="text/javascript">
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
    <script type="text/javascript">
        //FOR TAKE AMOUNT
        function NTS2000(obj) {
            var NTS2000 = document.getElementById('<%=txtTCnt1.ClientID%>').value;
            var Result = parseInt(NTS2000) * 2000;
            if (!isNaN(Result)) {
                document.getElementById('<%=txtTAmt1.ClientID %>').value = Result;
                TakeTotal();
                return true;
            }
        }

        function NTS1000(obj) {
            var NTS1000 = document.getElementById('<%=txtTCnt2.ClientID%>').value;
            var Result = parseInt(NTS1000) * 1000;
            if (!isNaN(Result)) {
                document.getElementById('<%=txtTAmt2.ClientID %>').value = Result;
                TakeTotal();
                return true;
            }
        }

        function NTS500(obj) {
            var NTS500 = document.getElementById('<%=txtTCnt3.ClientID%>').value;
            var Result = parseInt(NTS500) * 500;
            if (!isNaN(Result)) {
                document.getElementById('<%=txtTAmt3.ClientID %>').value = Result;
                TakeTotal();
                return true;
            }
        }

        function NTS100(obj) {
            var NTS100 = document.getElementById('<%=txtTCnt4.ClientID%>').value;
            var Result = parseInt(NTS100) * 100;
            if (!isNaN(Result)) {
                document.getElementById('<%=txtTAmt4.ClientID %>').value = Result;
                TakeTotal();
                return true;
            }
        }

        function NTS50(obj) {
            var NTS50 = document.getElementById('<%=txtTCnt5.ClientID%>').value;
            var Result = parseInt(NTS50) * 50;
            if (!isNaN(Result)) {
                document.getElementById('<%=txtTAmt5.ClientID %>').value = Result;
                TakeTotal();
                return true;
            }
        }

        function NTS20(obj) {
            var NTS20 = document.getElementById('<%=txtTCnt6.ClientID%>').value;
            var Result = parseInt(NTS20) * 20;
            if (!isNaN(Result)) {
                document.getElementById('<%=txtTAmt6.ClientID %>').value = Result;
                TakeTotal();
                return true;
            }
        }

        function NTS10(obj) {
            var NTS10 = document.getElementById('<%=txtTCnt7.ClientID%>').value;
            var Result = parseInt(NTS10) * 10;
            if (!isNaN(Result)) {
                document.getElementById('<%=txtTAmt7.ClientID %>').value = Result;
                TakeTotal();
                return true;
            }
        }

        function NTS5(obj) {
            var NTS5 = document.getElementById('<%=txtTCnt8.ClientID%>').value;
            var Result = parseInt(NTS5) * 5;
            if (!isNaN(Result)) {
                document.getElementById('<%=txtTAmt8.ClientID %>').value = Result;
                TakeTotal();
                return true;
            }
        }

        function NTS2(obj) {
            var NTS2 = document.getElementById('<%=txtTCnt9.ClientID%>').value;
            var Result = parseInt(NTS2) * 2;
            if (!isNaN(Result)) {
                document.getElementById('<%=txtTAmt9.ClientID %>').value = Result;
                TakeTotal();
                return true;
            }
        }

        function NTS1(obj) {
            var NTS1 = document.getElementById('<%=txtTCnt10.ClientID%>').value;
            var Result = parseInt(NTS1) * 1;
            if (!isNaN(Result)) {
                document.getElementById('<%=txtTAmt10.ClientID %>').value = Result;
                TakeTotal();
                return true;
            }
        }
    </Script>
    <script type="text/javascript">
        //FOR GIVE AMOUNT
        function NT2000(obj) {
            var NTS2000 = document.getElementById('<%=txtGCnt1.ClientID%>').value;
            var NTSAVLBL = document.getElementById('<%=txtavlbl1.ClientID%>').value;
            if (parseInt(NTS2000) <= parseInt(NTSAVLBL)) {
                var Result = parseInt(NTS2000) * 2000;
                if (!isNaN(Result)) {
                    document.getElementById('<%=txtGAmt1.ClientID %>').value = Result;
                    GiveTotal();
                    return true;
                }
            }
            else {
                var message = 'Notes Not Available....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtGCnt1.ClientID%>').focus();
                return false;
            }
        }

        function NT1000(obj) {
            var NTS1000 = document.getElementById('<%=txtGCnt2.ClientID%>').value;
            var NTSAVLBL = document.getElementById('<%=txtavlbl2.ClientID%>').value;

            if (parseInt(NTS1000) <= parseInt(NTSAVLBL)) {
                var Result = parseInt(NTS1000) * 1000;
                if (!isNaN(Result)) {
                    document.getElementById('<%=txtGAmt2.ClientID %>').value = Result;
                    GiveTotal();
                    return true;
                }
            }
            else {
                var message = 'Notes Not Available....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtGCnt2.ClientID%>').focus();
                return false;
            }
        }

        function NT500(obj) {
            var NTS500 = document.getElementById('<%=txtGCnt3.ClientID%>').value;
            var NTSAVLBL = document.getElementById('<%=txtavlbl3.ClientID%>').value;

            if (parseInt(NTS500) <= parseInt(NTSAVLBL)) {
                var Result = parseInt(NTS500) * 500;
                if (!isNaN(Result)) {
                    document.getElementById('<%=txtGAmt3.ClientID %>').value = Result;
                    GiveTotal();
                    return true;
                }
            }
            else {
                var message = 'Notes Not Available....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtGCnt3.ClientID%>').focus();
                return false;
            }
        }

        function NT100(obj) {
            var NTS100 = document.getElementById('<%=txtGCnt4.ClientID%>').value;
            var NTSAVLBL = document.getElementById('<%=txtavlbl4.ClientID%>').value;

            if (parseInt(NTS100) <= parseInt(NTSAVLBL)) {
                var Result = parseInt(NTS100) * 100;
                if (!isNaN(Result)) {
                    document.getElementById('<%=txtGAmt4.ClientID %>').value = Result;
                    GiveTotal();
                    return true;
                }
            }
            else {
                var message = 'Notes Not Available....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtGCnt4.ClientID%>').focus();
                return false;
            }
        }

        function NT50(obj) {
            var NTS50 = document.getElementById('<%=txtGCnt5.ClientID%>').value;
            var NTSAVLBL = document.getElementById('<%=txtavlbl5.ClientID%>').value;

            if (parseInt(NTS50) <= parseInt(NTSAVLBL)) {
                var Result = parseInt(NTS50) * 50;
                if (!isNaN(Result)) {
                    document.getElementById('<%=txtGAmt5.ClientID %>').value = Result;
                    GiveTotal();
                    return true;
                }
            }
            else {
                var message = 'Notes Not Available....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtGCnt5.ClientID%>').focus();
                return false;
            }
        }

        function NT20(obj) {
            var NTS20 = document.getElementById('<%=txtGCnt6.ClientID%>').value;
            var NTSAVLBL = document.getElementById('<%=txtavlbl6.ClientID%>').value;

            if (parseInt(NTS20) <= parseInt(NTSAVLBL)) {
                var Result = parseInt(NTS20) * 20;
                if (!isNaN(Result)) {
                    document.getElementById('<%=txtGAmt6.ClientID %>').value = Result;
                    GiveTotal();
                    return true;
                }
            }
            else {
                var message = 'Notes Not Available....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtGCnt6.ClientID%>').focus();
                return false;
            }
        }

        function NT10(obj) {
            var NTS10 = document.getElementById('<%=txtGCnt7.ClientID%>').value;
            var NTSAVLBL = document.getElementById('<%=txtavlbl7.ClientID%>').value;

            if (parseInt(NTS10) <= parseInt(NTSAVLBL)) {
                var Result = parseInt(NTS10) * 10;
                if (!isNaN(Result)) {
                    document.getElementById('<%=txtGAmt7.ClientID %>').value = Result;
                    GiveTotal();
                    return true;
                }
            }
            else {
                var message = 'Notes Not Available....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtGCnt7.ClientID%>').focus();
                return false;
            }
        }

        function NT5(obj) {
            var NTS5 = document.getElementById('<%=txtGCnt8.ClientID%>').value;
            var NTSAVLBL = document.getElementById('<%=txtavlbl8.ClientID%>').value;

            if (parseInt(NTS5) <= parseInt(NTSAVLBL)) {
                var Result = parseInt(NTS5) * 5;
                if (!isNaN(Result)) {
                    document.getElementById('<%=txtGAmt8.ClientID %>').value = Result;
                    GiveTotal();
                    return true;
                }
            }
            else {
                var message = 'Notes Not Available....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtGCnt8.ClientID%>').focus();
                return false;
            }
        }

        function NT2(obj) {
            var NTS2 = document.getElementById('<%=txtGCnt9.ClientID%>').value;
            var NTSAVLBL = document.getElementById('<%=txtavlbl9.ClientID%>').value;

            if (parseInt(NTS2) <= parseInt(NTSAVLBL)) {
                var Result = parseInt(NTS2) * 2;
                if (!isNaN(Result)) {
                    document.getElementById('<%=txtGAmt9.ClientID %>').value = Result;
                    GiveTotal();
                    return true;
                }
            }
            else {
                var message = 'Notes Not Available....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtGCnt9.ClientID%>').focus();
                return false;
            }
        }

        function NT1(obj) {
            var NTS1 = document.getElementById('<%=txtGCnt10.ClientID%>').value;
            var NTSAVLBL = document.getElementById('<%=txtavlbl10.ClientID%>').value;

            if (parseInt(NTS1) <= parseInt(NTSAVLBL)) {
                var Result = parseInt(NTS1) * 1;
                if (!isNaN(Result)) {
                    document.getElementById('<%=txtGAmt10.ClientID %>').value = Result;
                    GiveTotal();
                    return true;
                }
            }
            else {
                var message = 'Notes Not Available....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtGCnt10.ClientID%>').focus();
                return false;
            }
        }
    </script>
    <script type="text/javascript">
        //FOR TAKE TOTAL
        function TakeTotal() {
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
            var CNS = document.getElementById('<%=txtCTake.ClientID%>').value || 0;

            var SUM = parseInt(NT2000) + parseInt(NT1000) + parseInt(NT500) + parseInt(NT100) + parseInt(NT50) + parseInt(NT20) + parseInt(NT10) + parseInt(NT5) + parseInt(NT2) + parseInt(NT1) + parseInt(CNS);
            if (!isNaN(SUM)) {
                document.getElementById('<%=txtTBal.ClientID%>').value = SUM;
                document.getElementById('<%=txtSetTotal.ClientID%>').value = SUM + ' CR';
            }
        }
    </script>
    <script type="text/javascript">
        //FOR GIVE TOTAL
        function GiveTotal() {
            debugger;
            var NT2000 = document.getElementById('<%=txtGAmt1.ClientID%>').value || 0;
            var NT1000 = document.getElementById('<%=txtGAmt2.ClientID%>').value || 0;
            var NT500 = document.getElementById('<%=txtGAmt3.ClientID%>').value || 0;
            var NT100 = document.getElementById('<%=txtGAmt4.ClientID%>').value || 0;
            var NT50 = document.getElementById('<%=txtGAmt5.ClientID%>').value || 0;
            var NT20 = document.getElementById('<%=txtGAmt6.ClientID%>').value || 0;
            var NT10 = document.getElementById('<%=txtGAmt7.ClientID%>').value || 0;
            var NT5 = document.getElementById('<%=txtGAmt8.ClientID%>').value || 0;
            var NT2 = document.getElementById('<%=txtGAmt9.ClientID%>').value || 0;
            var NT1 = document.getElementById('<%=txtGAmt10.ClientID%>').value || 0;
            var CNS = document.getElementById('<%=txtCGive.ClientID%>').value || 0;

            var SUM = parseInt(NT2000) + parseInt(NT1000) + parseInt(NT500) + parseInt(NT100) + parseInt(NT50) + parseInt(NT20) + parseInt(NT10) + parseInt(NT5) + parseInt(NT2) + parseInt(NT1) + parseInt(CNS);
            if (!isNaN(SUM)) {
                document.getElementById('<%=txtGBal.ClientID%>').value = SUM;
                document.getElementById('<%=txtSetTotal.ClientID%>').value = SUM + ' DR';
            }
        }
    </script>
    <script type="text/javascript">
        //FOR GIVE COINS
        function GiveCoin() {
            var CNS = document.getElementById('<%=txtCGive.ClientID%>').value;
            var AVLBL = document.getElementById('<%=txtCAvlbl.ClientID%>').value;
            var TBAL = document.getElementById('<%=txtGBal.ClientID%>').value;
            debugger;
            if (parseInt(CNS) <= parseInt(AVLBL)) {
                GiveTotal();
                return true;
            }
            else {
                var message = 'Only ' + AVLBL + ' Coins Available....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtCGive.ClientID%>').focus();
                return false;
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        EXCHANGE OF DENOMINATION
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
                                                    <label class="control-label ">SetTotal:</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtSetTotal" CssClass="form-control" runat="server" ></asp:TextBox>
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:TextBox ID="txtName" CssClass="form-control" runat="server" ></asp:TextBox>
                                                </div>
                                           </div>
                                        </div>

                                        <div class="row" style="margin: 20px; border-bottom: 1px solid rgba(53, 152, 220, 0.55);"><strong></strong></div>
                                        <div class="DivScroll">
                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-1">
                                                    <label class="control-label ">Dens</label>
                                                </div>
                                                <div class="col-md-1">
                                                   <label class="control-label ">Available</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">Take Count</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <label class="control-label ">Total Amount</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">Give Count</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <label class="control-label ">Give Amount</label>
                                                </div>
                                           </div>
                                        </div>

                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="txtDens1" Text="2000" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-1">
                                                   <asp:TextBox ID="txtavlbl1" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtTCnt1" onkeyup="NTS2000()" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtTAmt1" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtGCnt1" onkeyup="NT2000()" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtGAmt1" CssClass="form-control" runat="server" />
                                                </div>
                                           </div>
                                        </div>

                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="txtDens2" Text="1000" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-1">
                                                   <asp:TextBox ID="txtavlbl2" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtTCnt2" onkeyup="NTS1000()" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtTAmt2" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtGCnt2" onkeyup="NT1000()" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtGAmt2" CssClass="form-control" runat="server" />
                                                </div>
                                           </div>
                                        </div>

                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="txtDens3" Text="500" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-1">
                                                   <asp:TextBox ID="txtavlbl3" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtTCnt3" onkeyup="NTS500()" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtTAmt3" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtGCnt3" onkeyup="NT500()" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtGAmt3" CssClass="form-control" runat="server" />
                                                </div>
                                           </div>
                                        </div>

                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="txtDens4" Text="100" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-1">
                                                   <asp:TextBox ID="txtavlbl4" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtTCnt4" onkeyup="NTS100()" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtTAmt4" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtGCnt4" onkeyup="NT100()" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtGAmt4" CssClass="form-control" runat="server" />
                                                </div>
                                           </div>
                                        </div>
                                        
                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="txtDens5" Text="50" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-1">
                                                   <asp:TextBox ID="txtavlbl5" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtTCnt5" onkeyup="NTS50()" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtTAmt5" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtGCnt5" onkeyup="NT50()" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtGAmt5" CssClass="form-control" runat="server" />
                                                </div>
                                           </div>
                                        </div>

                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="txtDens6" Text="20" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-1">
                                                   <asp:TextBox ID="txtavlbl6" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtTCnt6" onkeyup="NTS20()" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtTAmt6" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtGCnt6" onkeyup="NT20()" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtGAmt6" CssClass="form-control" runat="server" />
                                                </div>
                                           </div>
                                        </div>

                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="txtDens7" Text="10" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-1">
                                                   <asp:TextBox ID="txtavlbl7" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtTCnt7" onkeyup="NTS10()" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtTAmt7" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtGCnt7" onkeyup="NT10()" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtGAmt7" CssClass="form-control" runat="server" />
                                                </div>
                                           </div>
                                        </div>

                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="txtDens8" Text="5" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-1">
                                                   <asp:TextBox ID="txtavlbl8" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtTCnt8" onkeyup="NTS5()" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtTAmt8" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtGCnt8" onkeyup="NT5()" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtGAmt8" CssClass="form-control" runat="server" />
                                                </div>
                                           </div>
                                        </div>

                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="txtDens9" Text="2" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-1">
                                                   <asp:TextBox ID="txtavlbl9" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtTCnt9" onkeyup="NTS2()" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtTAmt9" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtGCnt9" onkeyup="NT2()" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtGAmt9" CssClass="form-control" runat="server" />
                                                </div>
                                           </div>
                                        </div>

                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="txtDens10" Text="1" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-1">
                                                   <asp:TextBox ID="txtavlbl10" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtTCnt10" onkeyup="NTS1()" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtTAmt10" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtGCnt10" onkeyup="NT1()" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtGAmt10" CssClass="form-control" runat="server" />
                                                </div>
                                           </div>
                                        </div>
                                        
                                        </div>    <%--Div Scroll End Here--%>
                                        <div class="row" style="margin: 20px; border-bottom: 1px solid rgba(53, 152, 220, 0.55);"><strong></strong></div>
                                        
                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-1">
                                                    
                                                </div>
                                                <div class="col-md-2">
                                                   <label class="control-label ">Coins Avlbl :</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtCAvlbl" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-1">
                                                    
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">Coins Take :</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtCTake" onkeyup="TakeTotal()" CssClass="form-control" runat="server" />
                                                </div>
                                           </div>
                                        </div>

                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-1">
                                                    
                                                </div>
                                                <div class="col-md-2">
                                                  
                                                </div>
                                                <div class="col-md-3">
                                                    
                                                </div>
                                                <div class="col-md-1">
                                                    
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">Coins Give :</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtCGive" onkeyup="GiveCoin()" CssClass="form-control" runat="server" />
                                                </div>
                                           </div>
                                        </div>

                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-1">
                                                    
                                                </div>
                                                <div class="col-md-2">
                                                   <label class="control-label ">SolidNotes Avlbl :</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtSNAvlbl" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-1">
                                                    
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">SolidNotes Take :</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtSNTake" CssClass="form-control" runat="server" />
                                                </div>
                                           </div>
                                        </div>

                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-1">
                                                    
                                                </div>
                                                <div class="col-md-2">
                                                  
                                                </div>
                                                <div class="col-md-3">
                                                    
                                                </div>
                                                <div class="col-md-1">
                                                    
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">SolidNotes Give :</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtSNGive" CssClass="form-control" runat="server" />
                                                </div>
                                           </div>
                                        </div>

                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-1">
                                                    
                                                </div>
                                                <div class="col-md-2">
                                                   <label class="control-label ">Balance Avlbl :</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtBalAvlbl" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-1">
                                                    
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">Take Balance :</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtTBal" CssClass="form-control" runat="server" />
                                                </div>
                                           </div>
                                        </div>

                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-1">
                                                    
                                                </div>
                                                <div class="col-md-2">
                                                  
                                                </div>
                                                <div class="col-md-3">
                                                    
                                                </div>
                                                <div class="col-md-1">
                                                    
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">Give Balance :</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtGBal" CssClass="form-control" runat="server" />
                                                </div>
                                           </div>
                                        </div>

                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label ">Token No :</label>
                                                </div>
                                                <div class="col-md-2">
                                                  <asp:TextBox ID="txtTNo" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">Instrument No :</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtInstNo" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">Instrument Date :</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtInstDate" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber(event)" placeholder="DD/MM/YYYY" CssClass="form-control" runat="server" />
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtInstDate" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$"
                                                        ErrorMessage="Invalid date format." ForeColor="Red" ValidationGroup="Group1" />
                                                    <asp:CalendarExtender ID="txtInstDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtInstDate">
                                                    </asp:CalendarExtender>
                                                </div>
                                            </div>
                                        </div>
                                    
                                    </div>
                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-5 col-md-7">
                                        <asp:Button ID="btnSubmit" OnClick="btnSubmit_Click" runat="server" CssClass="btn blue" Text="OK"  OnClientClick="Javascript:return isvalidate();" />
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

