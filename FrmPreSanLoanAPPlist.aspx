<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmPreSanLoanAPPlist.aspx.cs" Inherits="FrmPreSanLoanAPPlist" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script>
        function isNumber(evt)
        {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
            {
                return false;
            }
            return true;
        }

        function FormatIt(obj)
        {
            if (obj.value.length == 2) // Day
                obj.value = obj.value + "/";
            if (obj.value.length == 5) // month 
                obj.value = obj.value + "/";
            if (obj.value.length == 11) // year 
                alert("Please Enter valid Date");
        }
    </script>

    <script type="text/javascript">

        function ToBrCode(obj)
        {
            debugger;
            var FrBrCode = document.getElementById('<%=ddlFromBrName.ClientID%>').value || 0;

            if (parseFloat(ToBrCode) < parseFloat(FrBrCode))
            {
                message = 'Select to branch greter than from branch...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                return false;
            }
        }

        function ToPrCode(obj)
        {
            debugger;

            if (parseFloat(ToPrCode) < parseFloat(FrPrCode))
            {
                message = 'Enter to product code greter than from product code...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                return false;
            }
        }

        function ToSancLimit(obj)
        {
            debugger;

            if (parseFloat(ToLimit) < parseFloat(FrLimit))
            {
                message = 'Enter to limit greter than from limit...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                return false;
            }
        }

        function FromSancDate(obj)
        {
            debugger;
            var FrSancDate = document.getElementById('<%=txtFromSancDate.ClientID%>').value || 0;

            var frdate = FrSancDate.substring(0, 2);
            var frmonth = FrSancDate.substring(3, 5);
            var fryear = FrSancDate.substring(6, 10);
            var frmyDate = new Date(fryear, frmonth - 1, frdate);

            var wdate = WorkingDate.substring(0, 2);
            var wmonth = WorkingDate.substring(3, 5);
            var wyear = WorkingDate.substring(6, 10);
            var wmyDate = new Date(wyear, wmonth - 1, wdate);

            if (frmyDate > wmyDate)
            {
                message = 'From date not allow greter than working date...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtFromSancDate.ClientID%>').focus();
                return false;
            }
        }

        function ToSancDate(obj)
        {
            debugger;
            var FrSancDate = document.getElementById('<%=txtFromSancDate.ClientID%>').value || 0;
            var ToSancDate = document.getElementById('<%=txtToSancDate.ClientID%>').value || 0;

            var frdate = FrSancDate.substring(0, 2);
            var frmonth = FrSancDate.substring(3, 5);
            var fryear = FrSancDate.substring(6, 10);
            var frmyDate = new Date(fryear, frmonth - 1, frdate);

            var todate = ToSancDate.substring(0, 2);
            var tomonth = ToSancDate.substring(3, 5);
            var toyear = ToSancDate.substring(6, 10);
            var tomyDate = new Date(toyear, tomonth - 1, todate);

            var wdate = WorkingDate.substring(0, 2);
            var wmonth = WorkingDate.substring(3, 5);
            var wyear = WorkingDate.substring(6, 10);
            var wmyDate = new Date(wyear, wmonth - 1, wdate);

            if (tomyDate > wmyDate)
            {
                message = 'To date not allow greter than working date...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtToSancDate.ClientID %>').value = "";
                document.getElementById('<%=txtToSancDate.ClientID%>').focus();
                return false;
            }
            else if (tomyDate < frmyDate)
            {
                message = 'Enter to Sanction date greter than from sanction date...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtToSancDate.ClientID %>').value = "";
                document.getElementById('<%=txtToSancDate.ClientID%>').focus();
                return false;
            }
    }
    </script>

    <script type="text/javascript">
        function IsValidate() {

            var FrBrCode = document.getElementById('<%=txtFrBranch.ClientID%>').value;
            var FrSancDate = document.getElementById('<%=txtFromSancDate.ClientID%>').value;
            var ToSancDate = document.getElementById('<%=txtToSancDate.ClientID%>').value;

            var message = '';

            if (FrBrCode == "") {
                message = 'Select from branch first...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtFrBranch.ClientID%>').focus();
                return false;
            }
            if (FrSancDate == "") {
                message = 'Enter from sanction date first...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtFromSancDate.ClientID%>').focus();
                return false;
            }

            if (ToSancDate == "") {
                message = 'Enter to sanction date first...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtToSancDate.ClientID%>').focus();
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
                        Loan Application Report
                    </div>
                </div>
                <div class="portlet-body form">

                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div id="error">
                                    </div>
                                    <div class="tab-pane active" id="tab1">
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <asp:RadioButtonList ID="Rdeatils" RepeatDirection="Horizontal" Style="width: 380px;" runat="server">
                                                        <asp:ListItem Text="Pre Loan Sanction" Value="1" selected="True"/>
                                                        <asp:ListItem Text="Disburstment" Value="2" />
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label ">Select Branch<span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="ddlFromBrName" CssClass="form-control" OnSelectedIndexChanged="ddlFromBrName_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtFrBranch" Enabled="false" CssClass="form-control" runat="server" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label ">From Date<span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtFromSancDate" onblur="FromSancDate()" MaxLength="10" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="DD/MM/YYYY" runat="server"></asp:TextBox>
                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtFromSancDate">
                                                    </asp:CalendarExtender>
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">To Date<span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtToSancDate" onblur="ToSancDate()" MaxLength="10" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="DD/MM/YYYY" runat="server"></asp:TextBox>
                                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtToSancDate">
                                                    </asp:CalendarExtender>
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
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn blue" Text="SUBMIT" OnClick="btnSubmit_Click" OnClientClick="Javascript:return IsValidate();" />
                                     <asp:Button ID="BtnClear" runat="server" CssClass="btn blue" Text="Clear" OnClick="BtnClear_Click" TabIndex="13" />
                                     <asp:Button ID="BtnBack" runat="server" CssClass="btn blue" Text="Back" OnClick="BtnBack_Click" TabIndex="14" />
                                </div>
                            </div>
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
                    <p>
                        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                    </p>

                </div>
                <div class="modal-footer">
                    <button id="btnClose" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

