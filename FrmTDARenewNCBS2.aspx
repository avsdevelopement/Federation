<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmTDARenewNCBS2.aspx.cs" Inherits="FrmTDARenewNCBS2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AVS In-So-Tech</title>
    <!-- BEGIN GLOBAL MANDATORY STYLES -->
    <link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&subset=all" rel="stylesheet" type="text/css">
    <link href="global/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css">
    <link href="global/plugins/simple-line-icons/simple-line-icons.min.css" rel="stylesheet" type="text/css">
    <link href="global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css">
    <link href="global/plugins/uniform/css/uniform.default.css" rel="stylesheet" type="text/css">
    <link href="global/plugins/bootstrap-switch/css/bootstrap-switch.min.css" rel="stylesheet" type="text/css" />
    <link href="global/css/components-rounded.css" rel="stylesheet" type="text/css" />
</head>
<script type="text/javascript">
    function sum() {
        var PRI = document.getElementById('<%=TxtAcPayment.ClientID %>').value;
        var AMT = document.getElementById('<%=TxtWrAmount.ClientID %>').value;
        var result = parseInt(PRI) + parseInt(AMT);
        if (!isNaN(result)) {
            document.getElementById('<%=TxtTotalAmount.ClientID %>').value = result;
        }
    }
</script>
<script type="text/javascript">
    function Validate() {
        var R_PrdCode = document.getElementById('<%=TxtProcode.ClientID%>').value;
        var R_DepositDate = document.getElementById('<%=TxtDepositDate.ClientID%>').value;
        var R_IntPayOut = document.getElementById('<%=Ddl_IntPayout.ClientID%>').value;
        var R_DepoAmt = document.getElementById('<%=TxtDepoAmt.ClientID%>').value;
        var R_Period = document.getElementById('<%=TxtPeriod.ClientID%>').value;
        var R_Rate = document.getElementById('<%=TxtRate.ClientID%>').value;
        var R_IntAmt = document.getElementById('<%=TxtIntrest.ClientID%>').value;
        var R_Maturity = document.getElementById('<%=TxtMaturity.ClientID%>').value;
        var R_DueDate = document.getElementById('<%=TxtDueDate.ClientID%>').value;

        if (R_PrdCode == "") {
            alert("Enter Product code..!!");
            return false;
        }
        if (R_DepositDate == "") {
            alert("Enter Deposit date..!!");
            return false;
        }
        if (R_IntPayOut == "0") {
            alert("Select Interest Payout..!!");
            return false;
        }
        if (R_DepoAmt == "") {
            alert("Enter Deposit Amount....!!");
            return false;
        }
        if (R_Period == "") {
            alert("Enter Period..!!");
            return false;
        }
        if (R_IntAmt == "") {
            alert("Ineterst amount cannot be empty..!!");
            return false;
        }
        if (R_Maturity == "") {
            alert("Maturity amount cannot be empty..!!");
            return false;
        }
        if (R_DueDate == "") {
            alert("Due Date cannot be empty..!!");
            return false;
        }

    }
</script>
<script type="text/javascript">
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
    function sumFD() {
        var PRI = document.getElementById('<%=TxtDiff.ClientID %>').value;
        var AMT = document.getElementById('<%=TxtDepoAmt.ClientID %>').value;
        var result = parseInt(PRI) - parseInt(AMT);
        if (!isNaN(result)) {
            document.getElementById('<%=TxtTotalAmount.ClientID %>').value = result;
        }
    }
</script>

<script>
    function isNumber(evt) {
        evt = (evt) ? evt : window.event;
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;
    }
</script>
    <script type="text/javascript">
        function RefreshAutho() {
            window.opener.location.href = "frmTDAClear.aspx?SUBGLCODE=&ACCNO=";
        }
    </script>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <asp:UpdatePanel ID="Updatepanel" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="BtnSubmit" EventName="Click" />
            </Triggers>
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12">
                        <div class="portlet box green">
                            <div class="portlet-title">
                                <div class="caption">
                                    Deposit Renewal
                                <asp:Literal ID="LtrlHeading" runat="server"></asp:Literal>
                                </div>
                                <div class="tools">
                                    <a href="javascript:;" class="collapse" data-original-title="" title=""></a>
                                    <a href="#portlet-config" data-toggle="modal" class="config" data-original-title="" title=""></a>
                                </div>
                            </div>

                            <div class="portlet-body">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Deposit Information : </strong></div>
                                    </div>
                                </div>
                                <div class="row" style="margin: 7px 0 7px 0;">
                                    <div class="col-lg-12">
                                        <label class="control-label col-md-2">Renewal Type : <span class="required">* </span></label>
                                        <div class="col-md-4">
                                            <asp:RadioButton ID="Rdb_PrinWithInt" runat="server" Text="Principle With INT" GroupName="mat" AutoPostBack="true" OnCheckedChanged="Rdb_PrinWithInt_CheckedChanged" />
                                            <asp:RadioButton ID="Rdb_OnlyPrin" runat="server" Text="Only Principle" GroupName="mat" AutoPostBack="true" OnCheckedChanged="Rdb_OnlyPrin_CheckedChanged" />
                                            <asp:RadioButton ID="Rdb_WithReceipt" runat="server" Text="With Receipt" GroupName="mat" AutoPostBack="true" OnCheckedChanged="Rdb_WithReceipt_CheckedChanged" />
                                        </div>
                                        <label class="control-label col-md-2">Payble Amount : <span class="required">* </span></label>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="TxtAcPayment" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="margin: 7px 0 7px 0;" runat="server" id="WRPAYTP" visible="false">
                                    <div class="col-md-12">
                                        <label class="control-label col-md-2">Select Payment Type : <span class="required"></span></label>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="DdlPayType" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="DdlPayType_TextChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="margin: 7px 0 7px 0;" runat="server" id="DIVWR" visible="false">
                                    <div class="col-lg-12">
                                        <label class="control-label col-md-2">Product Code : <span class="required">* </span></label>
                                        <div class="col-md-1">
                                            <asp:TextBox ID="TxtWrPrdcode" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="TxtWrPrdcode_TextChanged"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="TxtWrPrdName" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="TxtWrPrdName_TextChanged"></asp:TextBox>
                                            <div id="WrDiv" style="height: 200px; overflow-y: scroll;"></div>
                                            <asp:AutoCompleteExtender ID="AutoWRPRD" runat="server" TargetControlID="TxtWrPrdName"
                                                UseContextKey="true"
                                                CompletionInterval="1"
                                                CompletionSetCount="20"
                                                MinimumPrefixLength="1"
                                                EnableCaching="true"
                                                ServicePath="~/WebServices/Contact.asmx"
                                                ServiceMethod="getglname" CompletionListElementID="WrDiv">
                                            </asp:AutoCompleteExtender>
                                        </div>
                                        <label class="control-label col-md-2">Account No :</label>
                                        <div class="col-md-1">
                                            <asp:TextBox ID="TxtWrAccno" CssClass="form-control" onkeypress="javascript:return isNumber (event)" PlaceHolder="Receipt No" runat="server" AutoPostBack="true" OnTextChanged="TxtWrAccno_TextChanged"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="TxtWrAccName" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="TxtWrAccName_TextChanged"></asp:TextBox>
                                            <div id="WrAccno" style="height: 200px; overflow-y: scroll;"></div>
                                            <asp:AutoCompleteExtender ID="AutoWRAccName" runat="server" TargetControlID="TxtWRAccName"
                                                UseContextKey="true"
                                                CompletionInterval="1"
                                                CompletionSetCount="20"
                                                MinimumPrefixLength="1"
                                                EnableCaching="true"
                                                ServicePath="~/WebServices/Contact.asmx"
                                                ServiceMethod="GetAccName" CompletionListElementID="WrAccno">
                                            </asp:AutoCompleteExtender>
                                        </div>
                                        <label class="control-label col-md-1">Balance :</label>
                                        <div class="col-md-1">
                                            <asp:TextBox ID="TxtBalance" OnTextChanged="TxtBalance_TextChanged" CssClass="form-control" onkeypress="javascript:return isNumber (event)" PlaceHolder="Balace" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" id="AMT" visible="false" runat="server" style="margin: 7px 0 7px 0;">
                                    <div class="col-md-12">
                                        <label class="control-label col-md-2">Amount : <span class="required">* </span></label>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="TxtWrAmount" OnTextChanged="TxtWrAmount_TextChanged" runat="server" onkeypress="javascript:return isNumber (event)" onkeyup="sum()" Text="0" CssClass="form-control" AutoPostBack="true" placeholder="Amount"></asp:TextBox>
                                        </div>
                                        <label class="control-label col-md-2">Cheque No. <span class="required">* </span></label>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="TxtInstNumber" OnTextChanged="TxtInstNumber_TextChanged" runat="server" onkeypress="javascript:return isNumber (event)" CssClass="form-control" AutoPostBack="true" placeholder="Cheque No."></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Button ID="Btn_SubmitWr" OnClick="Btn_SubmitWr_Click"  runat="server" Text="Submit" CssClass="btn btn-success" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row" id="AMTDetail" runat="server" visible="false" style="margin: 7px 0 7px 0;">
                                    <div class="col-md-12">
                                        <label class="control-label col-md-2">Total Amount : <span class="required">* </span></label>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="TxtTotalAmount" runat="server" Text="0" CssClass="form-control" placeholder="Amount" Enabled="false"></asp:TextBox>
                                        </div>
                                        <label class="control-label col-md-2">Amount Transfer : <span class="required">* </span></label>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="TxtTransferAMT" runat="server" Text="0" CssClass="form-control" placeholder="Amount" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc"></strong></div>
                                    </div>
                                </div>
                                <div class="row" style="margin: 7px 0 7px 0;">
                                    <div class="col-lg-12">
                                        <label class="control-label col-md-2">Renewal Mode : <span class="required">* </span></label>
                                        <div class="col-md-4">
                                            <asp:RadioButton ID="Rdb_Single" runat="server" Text="Single" GroupName="SM" AutoPostBack="true" OnCheckedChanged="Rdb_Single_CheckedChanged" />
                                            <asp:RadioButton ID="Rdb_Multiple" runat="server" Text="Multiple" GroupName="SM" AutoPostBack="true" OnCheckedChanged="Rdb_Multiple_CheckedChanged" />
                                        </div>
                                        <label class="control-label col-md-2">Total Amount : <span class="required">* </span></label>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="TxtAmtColl" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                        <label class="control-label col-md-1">Diff : <span class="required">* </span></label>
                                        <div class="col-md-1">
                                            <asp:TextBox ID="TxtDiff" runat="server" CssClass="form-control" Enabled="false" palceholder="Diff"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="margin: 7px 0 7px 0;">
                                    <div class="col-lg-12">
                                        <label class="control-label col-md-2">Product Code : <span class="required">* </span></label>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="TxtProcode" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="TxtProcode_TextChanged"></asp:TextBox>
                                        </div>

                                        <div id="Div1" class="col-md-4" runat="server" visible="false">
                                            <asp:DropDownList ID="Ddl_Prdname" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="Ddl_Prdname_TextChanged"></asp:DropDownList>
                                        </div>
                                        <label class="control-label col-md-2">Name : <span class="required">* </span></label>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="TxtPrdName" OnTextChanged="TxtPrdName_TextChanged" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                            <div id="CustList2" style="height: 200px; overflow-y: scroll;"></div>
                                            <asp:AutoCompleteExtender ID="AutoPrdName" runat="server" TargetControlID="TxtPrdName"
                                                UseContextKey="true"
                                                CompletionInterval="1"
                                                CompletionSetCount="20"
                                                MinimumPrefixLength="1"
                                                EnableCaching="true"
                                                ServicePath="~/WebServices/Contact.asmx"
                                                ServiceMethod="getglname" CompletionListElementID="CustList2">
                                            </asp:AutoCompleteExtender>
                                        </div>
                                    </div>
                                </div>
                                <%--Div 1--%>
                                <div id="Depositdiv" runat="server">
                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Deposit Date: <span class="required"></span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="TxtDepositDate" OnTextChanged="TxtDepositDate_TextChanged" AutoPostBack="true" onkeyup="FormatIt(this);sumFD()" CssClass="form-control" PlaceHolder="dd/mm/yyyy" runat="server"></asp:TextBox>
                                            </div>
                                            <label class="control-label col-md-2">Interest Payout: <span class="required"></span></label>
                                            <div class="col-md-2">
                                                <asp:DropDownList ID="Ddl_IntPayout" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Deposit Amount : <span class="required"></span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="TxtDepoAmt" OnTextChanged="TxtDepoAmt_TextChanged" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                            <label class="control-label col-md-2">Period : <span class="required"></span></label>
                                            <div class="col-md-2" style="margin-right: -24px;">
                                                <asp:DropDownList ID="Ddl_PerioDtype" runat="server" CssClass="form-control">

                                                    <asp:ListItem Value="M">Months</asp:ListItem>
                                                    <asp:ListItem Value="D">Days</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="TxtPeriod" OnTextChanged="TxtPeriod_TextChanged" CssClass="form-control" PlaceHolder="Period" runat="server" Style="width: 77px;" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Rate : <span class="required"></span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="TxtRate" OnTextChanged="TxtRate_TextChanged" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                            </div>
                                            <label class="control-label col-md-2">Interest Amount : <span class="required"></span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="TxtIntrest" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Maturity Amount :<span class="required"></span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="TxtMaturity" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                            </div>
                                            <label class="control-label col-md-2">Due Date :<span class="required"></span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="TxtDueDate" CssClass="form-control" PlaceHolder="dd/mm/yyyy" runat="server" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Receipt No</label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="TxtReceiptNo" CssClass="form-control" PlaceHolder="Receipt No" runat="server" Enabled="true"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <div class="col-md-3">
                                                <h4><b>Int. Trf Account</b></h4>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin: 7px 0 7px 0; margin-top: 20px;">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-1" style="width: 165px">Product Code : <span class="required">* </span></label>
                                            <div class="col-md-1">
                                                <asp:TextBox ID="TxtTrfProcode" OnTextChanged="TxtTrfProcode_TextChanged" CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="TxtTrfProName" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                <asp:AutoCompleteExtender ID="Autoprd4" runat="server" TargetControlID="TxtTrfProName"
                                                    UseContextKey="true"
                                                    CompletionInterval="1"
                                                    CompletionSetCount="20"
                                                    MinimumPrefixLength="1"
                                                    EnableCaching="true"
                                                    ServicePath="~/WebServices/Contact.asmx"
                                                    ServiceMethod="GetGlName">
                                                </asp:AutoCompleteExtender>
                                            </div>
                                            <label class="control-label col-md-1" style="width: 160px">Account No : <span class="required">* </span></label>
                                            <div class="col-md-1">
                                                <asp:TextBox ID="TxtTrfAccno" OnTextChanged="TxtTrfAccno_TextChanged" CssClass="form-control" PlaceHolder="Account No /  Receipt No" runat="server" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="TxtTrfAccName" CssClass="form-control" Width="270px" Placeholder="Acc Name" runat="server" AutoPostBack="true"></asp:TextBox>
                                                <asp:AutoCompleteExtender ID="Autoaccname4" runat="server" TargetControlID="TxtTrfAccName"
                                                    UseContextKey="true"
                                                    CompletionInterval="1"
                                                    CompletionSetCount="20"
                                                    MinimumPrefixLength="1"
                                                    EnableCaching="true"
                                                    ServicePath="~/WebServices/Contact.asmx"
                                                    ServiceMethod="GetAccName">
                                                </asp:AutoCompleteExtender>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <div class="col-md-3">
                                                <h4><b>Nominee Details</b></h4>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin: 7px 0 7px 0; margin-top: 20px;">
                                        <div class="col-lg-12">
                                            <div class="col-md-4">
                                                <asp:CheckBox ID="Chk_Modify" runat="server" Text="Modify Nominee details" OnCheckedChanged="Chk_Modify_CheckedChanged" AutoPostBack="true" />
                                            </div>
                                        </div>
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-1" style="width: 165px">Name 1 <span class="required"></span></label>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="TxtNominee_1" CssClass="form-control" PlaceHolder="Nominee 1" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-1">
                                                <label class="control-label">Relation <span class="required"></span></label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:DropDownList ID="ddlRelation_1" Width="130px" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-1">
                                                <label class="control-label">Gender <span class="required"></span></label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:RadioButtonList ID="RdbGender_1" runat="server" RepeatDirection="Horizontal" CssClass="radio-list pull-right">
                                                    <asp:ListItem Text="Male" Value="M" style="margin: 5px;" Selected="True"> </asp:ListItem>
                                                    <asp:ListItem Text="Female" Value="F" style="margin: 5px;"> </asp:ListItem>
                                                    <asp:ListItem Text="Transgender" Value="T" style="margin: 5px"> </asp:ListItem>
                                                </asp:RadioButtonList>

                                            </div>
                                        </div>

                                        <div class="col-lg-12">
                                            <label class="control-label col-md-1" style="width: 165px">Name 2 <span class="required"></span></label>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="TxtNominee_2" CssClass="form-control" PlaceHolder="Nominee 2" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-1">
                                                <label class="control-label">Relation <span class="required"></span></label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:DropDownList ID="ddlRelation_2" Width="130px" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-1">
                                                <label class="control-label">Gender <span class="required"></span></label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:RadioButtonList ID="RdbGender_2" runat="server" RepeatDirection="Horizontal" CssClass="radio-list pull-right">
                                                    <asp:ListItem Text="Male" Value="M" style="margin: 5px;" Selected="True"> </asp:ListItem>
                                                    <asp:ListItem Text="Female" Value="F" style="margin: 5px;"> </asp:ListItem>
                                                    <asp:ListItem Text="Transgender" Value="T" style="margin: 5px"> </asp:ListItem>
                                                </asp:RadioButtonList>

                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin: 07px 0 1px 0">
                                        <div class="col-lg-12">
                                            <div class="col-md-2">
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Button ID="BtnSubmit" OnClick="BtnSubmit_Click" runat="server" CssClass="btn green" Text="Submit" OnClientClick="javascript:return Validate();" />
                                            </div>
                                            <div class="col-md-2" id="Div_SPost" runat="server">
                                                <asp:Button ID="Btn_PostEntry" OnClick="Btn_PostEntry_Click" runat="server" Visible="false" CssClass="btn blue" Text="Post Deposit" OnClientClick="javascript:return validate();" />
                                            </div>
                                            <div class="col-md-2" id="Div_MPost" runat="server">
                                                <asp:Button ID="BtnPostMultiple" runat="server" OnClick="BtnPostMultiple_Click" Visible="false" CssClass="btn blue" Text="Post Cl.Multiple " OnClientClick="javascript:return validate();" />
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
                                            <asp:GridView ID="GrdFDLedger" runat="server" AllowPaging="True"
                                                AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                EditRowStyle-BackColor="#FFFF99"
                                                PageIndex="10" PageSize="25"
                                                PagerStyle-CssClass="pgr" Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Product code" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="PRD" runat="server" Text='<%# Eval("SUBGLCODE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="ACCNO">
                                                        <ItemTemplate>
                                                            <asp:Label ID="ACC" runat="server" Text='<%# Eval("ACCNO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="AMOUNT">
                                                        <ItemTemplate>
                                                            <asp:Label ID="AMT" runat="server" Text='<%# Eval("AMOUNT") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="CR/DR">
                                                        <ItemTemplate>
                                                            <asp:Label ID="TRXTYPE" runat="server" Text='<%# Eval("TRXTYPE") %>'></asp:Label>
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
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="updateProgress" runat="server">
            <ProgressTemplate>
                <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                    <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="global/images/ajax-loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="padding: 10px; position: fixed; top: 45%; left: 50%;" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
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
    </form>
</body>
</html>
