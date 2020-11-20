<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmCashDenom.aspx.cs" Inherits="FrmCashDenom" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style type='text/css'>
        .DivScroll {
            overflow-x: hidden;
            overflow-y: auto;
            background-color: lightgray;
            width: 1000px;
            height: 220px;
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

        //function isvalidate()
        //{
        // var Amt = document.getElementById('<%=txtamount.ClientID%>').value || 0;
        //var tot = document.getElementById('<%=txtTBal.ClientID%>').value || 0;
        //var tot1 = document.getElementById('<%=txtGBal.ClientID%>').value || 0;
        //debugger;
        // if (parseInt(tot) != parseInt(Amt) || parseInt(tot1) != parseInt(Amt))
        // {
        //    var message = 'Deposite Amount Not Match....!!\n';
        //    $('#alertModal').find('.modal-body p').text(message);
        //    $('#alertModal').modal('show')
        //    document.getElementById('<%=btnSubmit.ClientID%>').focus();
        //     return false;
        // }
        //}
    </script>
    <script type="text/javascript">
        //FOR TAKE AMOUNT
        function NTS2000(obj) {
            var NTS2000 = document.getElementById('<%=txtTCnt1.ClientID%>').value || 0;
            var Result = parseInt(NTS2000) * 2000;
            if (!isNaN(Result)) {
                document.getElementById('<%=txtTAmt1.ClientID %>').value = Result;
                TakeTotal();
                return true;
            }
        }

        function NTS1000(obj) {
            var NTS1000 = document.getElementById('<%=txtTCnt2.ClientID%>').value || 0;
            var Result = parseInt(NTS1000) * 1000;
            if (!isNaN(Result)) {
                document.getElementById('<%=txtTAmt2.ClientID %>').value = Result;
                TakeTotal();
                return true;
            }
        }

        function NTS500(obj) {
            var NTS500 = document.getElementById('<%=txtTCnt3.ClientID%>').value || 0;
            var Result = parseInt(NTS500) * 500;
            if (!isNaN(Result)) {
                document.getElementById('<%=txtTAmt3.ClientID %>').value = Result;
                TakeTotal();
                return true;
            }
        }

        function NTS200(obj) {
            var NTS200 = document.getElementById('<%=txtTCnt12.ClientID%>').value || 0;
            var Result = parseInt(NTS200) * 200;
            if (!isNaN(Result)) {
                document.getElementById('<%=txtTAmt12.ClientID %>').value = Result;
                TakeTotal();
                return true;
            }
        }

        function NTS100(obj) {
            var NTS100 = document.getElementById('<%=txtTCnt4.ClientID%>').value || 0;
            var Result = parseInt(NTS100) * 100;
            if (!isNaN(Result)) {
                document.getElementById('<%=txtTAmt4.ClientID %>').value = Result;
                TakeTotal();
                return true;
            }
        }

        function NTS50(obj) {
            var NTS50 = document.getElementById('<%=txtTCnt5.ClientID%>').value || 0;
            var Result = parseInt(NTS50) * 50;
            if (!isNaN(Result)) {
                document.getElementById('<%=txtTAmt5.ClientID %>').value = Result;
                TakeTotal();
                return true;
            }
        }

        function NTS20(obj) {
            var NTS20 = document.getElementById('<%=txtTCnt6.ClientID%>').value || 0;
            var Result = parseInt(NTS20) * 20;
            if (!isNaN(Result)) {
                document.getElementById('<%=txtTAmt6.ClientID %>').value = Result;
                TakeTotal();
                return true;
            }
        }

        function NTS10(obj) {
            var NTS10 = document.getElementById('<%=txtTCnt7.ClientID%>').value || 0;
            var Result = parseInt(NTS10) * 10;
            if (!isNaN(Result)) {
                document.getElementById('<%=txtTAmt7.ClientID %>').value = Result;
                TakeTotal();
                return true;
            }
        }

        function NTS5(obj) {
            var NTS5 = document.getElementById('<%=txtTCnt8.ClientID%>').value || 0;
            var Result = parseInt(NTS5) * 5;
            if (!isNaN(Result)) {
                document.getElementById('<%=txtTAmt8.ClientID %>').value = Result;
                TakeTotal();
                return true;
            }
        }

        function NTS2(obj) {
            var NTS2 = document.getElementById('<%=txtTCnt9.ClientID%>').value || 0;
            var Result = parseInt(NTS2) * 2;
            if (!isNaN(Result)) {
                document.getElementById('<%=txtTAmt9.ClientID %>').value = Result;
                TakeTotal();
                return true;
            }
        }

        function NTS1(obj) {
            var NTS1 = document.getElementById('<%=txtTCnt10.ClientID%>').value || 0;
            var Result = parseInt(NTS1) * 1;
            if (!isNaN(Result)) {
                document.getElementById('<%=txtTAmt10.ClientID %>').value = Result;
                TakeTotal();
                return true;
            }
        }
    </script>
    <script type="text/javascript">
        //FOR GIVE AMOUNT
        function NT2000(obj) {
            var NTS2000 = document.getElementById('<%=txtGCnt1.ClientID%>').value || 0;
            var NTSAVLBL = document.getElementById('<%=txtavlbl1.ClientID%>').value || 0;
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
            var NTS1000 = document.getElementById('<%=txtGCnt2.ClientID%>').value || 0;
            var NTSAVLBL = document.getElementById('<%=txtavlbl2.ClientID%>').value || 0;

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
            var NTS500 = document.getElementById('<%=txtGCnt3.ClientID%>').value || 0;
            var NTSAVLBL = document.getElementById('<%=txtavlbl3.ClientID%>').value || 0;

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

        function NT200(obj) {
            var NTS200 = document.getElementById('<%=txtGCnt12.ClientID%>').value || 0;
            var NTSAVLBL = document.getElementById('<%=txtavlbl12.ClientID%>').value || 0;

            if (parseInt(NTS200) <= parseInt(NTSAVLBL)) {
                var Result = parseInt(NTS200) * 200;
                if (!isNaN(Result)) {
                    document.getElementById('<%=txtGAmt12.ClientID %>').value = Result;
                    GiveTotal();
                    return true;
                }
            }
            else {
                var message = 'Notes Not Available....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtGCnt12.ClientID%>').focus();
                return false;
            }
        }

        function NT100(obj) {
            var NTS100 = document.getElementById('<%=txtGCnt4.ClientID%>').value || 0;
            var NTSAVLBL = document.getElementById('<%=txtavlbl4.ClientID%>').value || 0;

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
            var NTS50 = document.getElementById('<%=txtGCnt5.ClientID%>').value || 0;
            var NTSAVLBL = document.getElementById('<%=txtavlbl5.ClientID%>').value || 0;

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
            var NTS20 = document.getElementById('<%=txtGCnt6.ClientID%>').value || 0;
            var NTSAVLBL = document.getElementById('<%=txtavlbl6.ClientID%>').value || 0;

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
            var NTS10 = document.getElementById('<%=txtGCnt7.ClientID%>').value || 0;
            var NTSAVLBL = document.getElementById('<%=txtavlbl7.ClientID%>').value || 0;

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
            var NTS5 = document.getElementById('<%=txtGCnt8.ClientID%>').value || 0;
            var NTSAVLBL = document.getElementById('<%=txtavlbl8.ClientID%>').value || 0;

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
            var NTS2 = document.getElementById('<%=txtGCnt9.ClientID%>').value || 0;
            var NTSAVLBL = document.getElementById('<%=txtavlbl9.ClientID%>').value || 0;

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
            var NTS1 = document.getElementById('<%=txtGCnt10.ClientID%>').value || 0;
            var NTSAVLBL = document.getElementById('<%=txtavlbl10.ClientID%>').value || 0;

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
            var NT200 = document.getElementById('<%=txtTAmt12.ClientID%>').value || 0;
            var NT100 = document.getElementById('<%=txtTAmt4.ClientID%>').value || 0;
            var NT50 = document.getElementById('<%=txtTAmt5.ClientID%>').value || 0;
            var NT20 = document.getElementById('<%=txtTAmt6.ClientID%>').value || 0;
            var NT10 = document.getElementById('<%=txtTAmt7.ClientID%>').value || 0;
            var NT5 = document.getElementById('<%=txtTAmt8.ClientID%>').value || 0;
            var NT2 = document.getElementById('<%=txtTAmt9.ClientID%>').value || 0;
            var NT1 = document.getElementById('<%=txtTAmt10.ClientID%>').value || 0;
            <%--var CNS = document.getElementById('<%=txtCTake.ClientID%>').value || 0;--%>
            var Amt = document.getElementById('<%=txtamount.ClientID%>').value || 0;

            var SUM = parseInt(NT2000) + parseInt(NT1000) + parseInt(NT500) + parseFloat(NT200) + parseInt(NT100) + parseInt(NT50) + parseInt(NT20) + parseInt(NT10) + parseInt(NT5) + parseInt(NT2) + parseInt(NT1);// + parseInt(CNS);
            //if (parseInt(SUM) <= parseInt(Amt)) { //ankita on 05/08/2017 suggested by amol sir
            if (!isNaN(SUM)) {
                document.getElementById('<%=txtTBal.ClientID%>').value = SUM;
            }
            //}
        }
    </script>
    <script type="text/javascript">
        //FOR GIVE TOTAL
        function GiveTotal() {
            debugger;
            var NT2000 = document.getElementById('<%=txtGAmt1.ClientID%>').value || 0;
            var NT1000 = document.getElementById('<%=txtGAmt2.ClientID%>').value || 0;
            var NT500 = document.getElementById('<%=txtGAmt3.ClientID%>').value || 0;
            var NT200 = document.getElementById('<%=txtGAmt12.ClientID%>').value || 0;
            var NT100 = document.getElementById('<%=txtGAmt4.ClientID%>').value || 0;
            var NT50 = document.getElementById('<%=txtGAmt5.ClientID%>').value || 0;
            var NT20 = document.getElementById('<%=txtGAmt6.ClientID%>').value || 0;
            var NT10 = document.getElementById('<%=txtGAmt7.ClientID%>').value || 0;
            var NT5 = document.getElementById('<%=txtGAmt8.ClientID%>').value || 0;
            var NT2 = document.getElementById('<%=txtGAmt9.ClientID%>').value || 0;
            var NT1 = document.getElementById('<%=txtGAmt10.ClientID%>').value || 0;
            <%--var CNS = document.getElementById('<%=txtCGive.ClientID%>').value || 0;--%>
            var Amt = document.getElementById('<%=txtamount.ClientID%>').value || 0;

            var SUM = parseInt(NT2000) + parseInt(NT1000) + parseInt(NT500) + parseFloat(NT200) + parseInt(NT100) + parseInt(NT50) + parseInt(NT20) + parseInt(NT10) + parseInt(NT5) + parseInt(NT2) + parseInt(NT1);// + parseInt(CNS);
            //if (parseInt(SUM) <= parseInt(Amt)) {//ankita on 05/08/2017 suggested by amol sir
            if (!isNaN(SUM)) {
                document.getElementById('<%=txtGBal.ClientID%>').value = SUM;
            }
            //}
        }
    </script>
    <script type="text/javascript">
        //FOR GIVE COINS
        function GiveCoin() {
            <%--var CNS = document.getElementById('<%=txtCGive.ClientID%>').value || 0;
            var AVLBL = document.getElementById('<%=txtCAvlbl.ClientID%>').value || 0;
            var TBAL = document.getElementById('<%=txtGBal.ClientID%>').value || 0;
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
            }--%>
        }
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        DENOMINATIONS OF CASH
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab1">

                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-1">
                                                    <asp:Button ID="btnAddExisting" Text="AddExisting" OnClick="btnAddExisting_Click" CssClass="btn blue" runat="server" />
                                                </div>
                                                <div id="DivSet" runat="server" visible="false">
                                                    <label class="control-label col-md-1" runat="server">SetNo :</label>
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="txtSetNo" CssClass="form-control" OnTextChanged="txtSetNo_TextChanged" AutoPostBack="true" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="DivScroll">
                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
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
                                                        <asp:TextBox ID="txtDens1" Enabled="false" Text="2000" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="txtavlbl1" Enabled="false" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtTCnt1" onkeypress="javascript:return isNumber (event)" onblur="NTS2000()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtTAmt1" Enabled="false" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtGCnt1" onkeypress="javascript:return isNumber (event)" onblur="NT2000()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtGAmt1" Enabled="false" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="txtDens2" Enabled="false" Text="1000" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="txtavlbl2" Enabled="false" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtTCnt2" onkeypress="javascript:return isNumber (event)" onblur="NTS1000()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtTAmt2" Enabled="false" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtGCnt2" onkeypress="javascript:return isNumber (event)" onblur="NT1000()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtGAmt2" Enabled="false" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="txtDens3" Enabled="false" Text="500" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="txtavlbl3" Enabled="false" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtTCnt3" onkeypress="javascript:return isNumber (event)" onblur="NTS500()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtTAmt3" Enabled="false" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtGCnt3" onkeypress="javascript:return isNumber (event)" onblur="NT500()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtGAmt3" Enabled="false" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                            
                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="txtDens12" Enabled="false" Text="200" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="txtavlbl12" Enabled="false" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtTCnt12" onkeypress="javascript:return isNumber (event)" onblur="NTS200()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtTAmt12" Enabled="false" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtGCnt12" onkeypress="javascript:return isNumber (event)" onblur="NT200()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtGAmt12" Enabled="false" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="txtDens4" Enabled="false" Text="100" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="txtavlbl4" Enabled="false" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtTCnt4" onkeypress="javascript:return isNumber (event)" onblur="NTS100()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtTAmt4" Enabled="false" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtGCnt4" onkeypress="javascript:return isNumber (event)" onblur="NT100()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtGAmt4" Enabled="false" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="txtDens5" Enabled="false" Text="50" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="txtavlbl5" Enabled="false" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtTCnt5" onkeypress="javascript:return isNumber (event)" onblur="NTS50()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtTAmt5" Enabled="false" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtGCnt5" onkeypress="javascript:return isNumber (event)" onblur="NT50()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtGAmt5" Enabled="false" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="txtDens6" Enabled="false" Text="20" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="txtavlbl6" Enabled="false" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtTCnt6" onkeypress="javascript:return isNumber (event)" onblur="NTS20()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtTAmt6" Enabled="false" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtGCnt6" onkeypress="javascript:return isNumber (event)" onblur="NT20()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtGAmt6" Enabled="false" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="txtDens7" Enabled="false" Text="10" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="txtavlbl7" Enabled="false" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtTCnt7" onkeypress="javascript:return isNumber (event)" onblur="NTS10()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtTAmt7" Enabled="false" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtGCnt7" onkeypress="javascript:return isNumber (event)" onblur="NT10()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtGAmt7" Enabled="false" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="txtDens8" Enabled="false" Text="5" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="txtavlbl8" Enabled="false" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtTCnt8" onkeypress="javascript:return isNumber (event)" onblur="NTS5()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtTAmt8" Enabled="false" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtGCnt8" onkeypress="javascript:return isNumber (event)" onblur="NT5()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtGAmt8" Enabled="false" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="txtDens9" Enabled="false" Text="2" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="txtavlbl9" Enabled="false" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtTCnt9" onkeypress="javascript:return isNumber (event)" onblur="NTS2()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtTAmt9" Enabled="false" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtGCnt9" onkeypress="javascript:return isNumber (event)" onblur="NT2()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtGAmt9" Enabled="false" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="txtDens10" Enabled="false" Text="1" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="txtavlbl10" Enabled="false" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtTCnt10" onkeypress="javascript:return isNumber (event)" onblur="NTS1()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtTAmt10" Enabled="false" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtGCnt10" onkeypress="javascript:return isNumber (event)" onblur="NT1()" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtGAmt10" Enabled="false" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <%--Div Scroll End Here--%>

                                        <%--<div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label ">Coins Avlbl :</label>
                                                </div>
                                                <div class="col-md-2">
                                                   <asp:TextBox ID="txtCAvlbl" Enabled="false" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">Coins Take :</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtCTake" onkeypress="javascript:return isNumber (event)" onblur="TakeTotal()" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">Coins Give :</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtCGive" onkeypress="javascript:return isNumber (event)" onblur="GiveCoin()" CssClass="form-control" runat="server" />
                                                </div>
                                           </div>
                                        </div>--%>

                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label ">SoiledNotes Avlbl :</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtSNAvlbl" Enabled="false" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">SoiledNotes Take :</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtSNTake" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">SoiledNotes Give :</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtSNGive" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label ">Balance Avlbl :</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtBalAvlbl" Enabled="false" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">Take Balance :</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtTBal" Enabled="false" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">Give Balance :</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtGBal" Enabled="false" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" />
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-2">
                                        <label class="control-label ">Deposite Amount</label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtamount" CssClass="form-control" runat="server" Enabled="false" />
                                    </div>
                                    <div class="col-md-offset-1 col-md-7">
                                        <asp:Button ID="btnSubmit" OnClick="btnSubmit_Click" runat="server" CssClass="btn blue" Text="OK" OnClientClick="Javascript:return isvalidate();" />
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
