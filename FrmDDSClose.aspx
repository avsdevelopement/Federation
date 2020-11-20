<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmDDSClose.aspx.cs" Inherits="FrmDDSClose" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
        function MINUS() {

            var PROVI = document.getElementById('<%=Txt_Provi.ClientID%>').value;
            var IC = document.getElementById('<%=TxtCalcInt.ClientID%>').value;
            var Hidden_FL = document.getElementById('<%=Hdn_ParRevProvi.ClientID%>').value;
            var Result;

            if (Hidden_FL != "N") {
                Result = parseFloat(IC) - parseFloat(PROVI);
            }
            else {
                Result = parseFloat(IC);
            }
            if (!isNaN(Result)) {

                document.getElementById('<%=TxtINTC.ClientID%>').value = Result;

            }
        }
    </script>
    <script type="text/javascript">
        function Sum() {

            var IC = document.getElementById('<%=TxtINTC.ClientID%>').value;
            var PAR = document.getElementById('<%=Txtpartpay.ClientID%>').value;
            var RES;

            RES = parseFloat(IC) + parseFloat(PAR);
            if (!isNaN(Result)) {
                document.getElementById('<%=TxtPayAmt.ClientID%>').value = Result;
            }
        }

    </script>
    <style>
        .zoom {
            transition: transform .3s; /* Animation */
            width: 200px;
            height: 200px;
            margin: 0 auto;
        }

            .zoom:hover {
                transform: scale(1.5); /* (150% zoom - Note: if the zoom is too large, it will go outside of the viewport) */
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box green">
                <div class="portlet-title">
                    <div class="caption">
                        DDS Closure
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
                            <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Customer Information : </strong></div>
                        </div>
                    </div>
                    <div class="row" style="margin: 7px 0 7px 0;">
                        <div class="col-md-12" style="height: 28px">
                            <label class="control-label col-md-2">Agent Code:<span class="required">* </span></label>
                            <div class="col-md-1">
                                <asp:TextBox ID="TxtAGCD" runat="server" onkeypress="javascript:return isNumber (event)" PlaceHolder="Agent Code" OnTextChanged="TxtAGCD_TextChanged" AutoPostBack="true" CssClass="form-control"></asp:TextBox>&nbsp;&nbsp;                               
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="TxtAGName" runat="server" PlaceHolder="Agent Name" OnTextChanged="TxtAGName_TextChanged" AutoPostBack="true" CssClass="form-control"></asp:TextBox>
                                <div id="CustList4" style="height: 200px; overflow-y: scroll;"></div>
                                <asp:AutoCompleteExtender ID="autoAgname" runat="server" TargetControlID="TxtAGName"
                                    UseContextKey="true"
                                    CompletionInterval="1"
                                    CompletionSetCount="20"
                                    MinimumPrefixLength="1"
                                    EnableCaching="true"
                                    ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList4"
                                    ServiceMethod="GetAgNameDDSCL">
                                </asp:AutoCompleteExtender>
                            </div>
                            <label class="control-label col-md-2">Account No:<span class="required">* </span></label>
                            <div class="col-md-1">
                                <asp:TextBox ID="TxtAccNo" runat="server" PlaceHolder="Account No" onkeypress="javascript:return isNumber (event)" OnTextChanged="TxtAccNo_TextChanged" AutoPostBack="true" CssClass="form-control"></asp:TextBox>&nbsp;&nbsp;
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="TxtAccName" runat="server" PlaceHolder="Account Holder Name" OnTextChanged="TxtAccName_TextChanged" AutoPostBack="true" CssClass="form-control"></asp:TextBox>
                                <div id="CustList5" style="height: 200px; overflow-y: scroll;"></div>
                                <asp:AutoCompleteExtender ID="AutoAcc" runat="server" TargetControlID="TxtAccName"
                                    UseContextKey="true"
                                    CompletionInterval="1"
                                    CompletionSetCount="20"
                                    MinimumPrefixLength="1"
                                    EnableCaching="true"
                                    CompletionListElementID="CustList5"
                                    ServicePath="~/WebServices/Contact.asmx"
                                    ServiceMethod="GetDDSAcc">
                                </asp:AutoCompleteExtender>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="margin: 7px 0 7px 0;">
                        <div class="col-lg-12">
                            <label class="control-label col-md-2">Account Status<span class="required">* </span></label>
                            <div class="col-md-1">
                                <asp:TextBox ID="TxtAccSTS" runat="server" onkeypress="javascript:return isNumber (event)" PlaceHolder="Acc Status" CssClass="form-control"></asp:TextBox>&nbsp;&nbsp;                                                    
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="TxtAccSTSName" runat="server" PlaceHolder="Acc Type Name" CssClass="form-control"></asp:TextBox>
                            </div>
                            <label class="control-label col-md-2">Acc Type:<span class="required">*</span></label>
                            <div class="col-md-1">
                                <asp:TextBox ID="TxtAccType" runat="server" PlaceHolder="Acc Type" onkeypress="javascript:return isNumber (event)" CssClass="form-control"></asp:TextBox>&nbsp;&nbsp;
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="TxtAccTName" runat="server" PlaceHolder="Acc Type Name" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="margin: 7px 0 7px 0;">
                        <div class="col-lg-12" style="height: 28px">
                            <label class="control-label col-md-2">Opening Date:</label>
                            <div class="col-md-2">
                                <asp:TextBox ID="TxtOpeningDate" runat="server" PlaceHolder="Openig Date" Enabled="false" CssClass="form-control"></asp:TextBox>&nbsp;&nbsp;                                                                                                            
                            </div>
                            <label class="control-label col-md-1">Period:</label>
                            <div class="col-md-1">
                                <asp:TextBox ID="TxtPeriod" runat="server" PlaceHolder="Period" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </div>
                            <label class="control-label col-md-2">Closing Date:</label>
                            <div class="col-md-2">
                                <asp:TextBox ID="TxtCLDate" runat="server" PlaceHolder="Closing Date" Enabled="false" CssClass="form-control"></asp:TextBox>&nbsp;&nbsp;                                                                                                            
                                <asp:LinkButton ID="LnkVerify" runat="server" OnClick="LnkVerify_Click">Verify Signature</asp:LinkButton>
                            </div>
                            <div class="col-md-2">
                                <asp:Table ID="Tbl_Photo" runat="server">
                                    <asp:TableRow ID="Rw_Ph1" runat="server">
                                        <asp:TableCell ID="TblCell1" runat="server">
                                            <asp:Label ID="Label5" runat="server" Text="Signature" Style="font-weight: bold"></asp:Label>
                                            <div class="zoom" style="height: 100%; width: 100%">
                                                <img id="Img1" runat="server" style="height: 100%; width: 100%; border: 1px solid #000000; padding: 5px" />
                                            </div>
                                        </asp:TableCell>
                                        <asp:TableCell ID="TblCell2" runat="server">
                                            <asp:Label ID="Label6" runat="server" Text="Photo" Style="font-weight: bold"></asp:Label>
                                            <div class="zoom" style="height: 100%; width: 100%">
                                                <img id="Img2" runat="server" style="height: 100%; width: 100%; border: 1px solid #000000; padding: 5px" />
                                            </div>
                                        </asp:TableCell>
                                    </asp:TableRow>

                                </asp:Table>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="margin: 7px 0 7px 0;">
                        <div class="col-lg-12" style="height: 28px">
                            <label class="control-label col-md-2">Special Instrunction:</label>
                            <div class="col-md-2">
                                <asp:TextBox ID="TxtSplINSt" runat="server" onkeypress="javascript:return isNumber (event)" PlaceHolder="Spl Intruncation" CssClass="form-control"></asp:TextBox>&nbsp;&nbsp;                                                    
                            </div>

                            <label class="control-label col-md-1">Last Int Dt</label>
                            <div class="col-md-2">
                                <asp:TextBox ID="TxtLastIntDate" Enabled="false" runat="server" onkeypress="javascript:return isNumber (event)" PlaceHolder="Last Int Date" CssClass="form-control"></asp:TextBox>&nbsp;&nbsp;                                                    
                            </div>
                            <label class="control-label col-md-1">Closing Bal<span class="required">* </span></label>
                            <div class="col-md-2">
                                <asp:TextBox ID="TxtCBal" Enabled="false" runat="server" PlaceHolder="Closing Balance" onkeypress="javascript:return isNumber (event)" CssClass="form-control"></asp:TextBox>&nbsp;&nbsp;                                                    
                            </div>
                        </div>
                    </div>
                    <div class="row" style="margin: 7px 0 7px 0;">
                        <div class="col-lg-12" style="height: 28px">
                            <label class="control-label col-md-2">Payment Type<span class="required">* </span></label>
                            <div class="col-md-4">
                                <asp:RadioButton ID="rdbfull" runat="server" GroupName="FP" Text="Full payment" OnCheckedChanged="rdbfull_CheckedChanged" AutoPostBack="true" />
                                <asp:RadioButton ID="rdbpart" runat="server" GroupName="FP" Text="Part payment" OnCheckedChanged="rdbpart_CheckedChanged" AutoPostBack="true" />
                            </div>
                            <label class="control-label col-md-2">Part Payment AMT<span class="required">* </span></label>
                            <div class="col-md-2">
                                <asp:TextBox ID="Txtpartpay" runat="server" onkeyup="Sum()" OnTextChanged="Txtpartpay_TextChanged" AutoPostBack="true" PlaceHolder="Payment AMT" onkeypress="javascript:return isNumber (event)" CssClass="form-control"></asp:TextBox>&nbsp;&nbsp;                                                    
                            </div>
                        </div>
                    </div>
                    <div class="row" style="margin: 7px 0 7px 0;">
                        <div class="col-lg-12" style="height: 28px">
                            <label class="control-label col-md-2">Activity :<span class="required">* </span></label>
                            <div class="col-md-2">
                                <asp:DropDownList ID="ddlActivity" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlActivity_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                    <%-- <asp:ListItem Value="1" Text="Part Withdrawal"></asp:ListItem>--%>
                                    <asp:ListItem Value="2" Text="Premature Closure"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="Maturity Closure"></asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="col-md-2"></div>
                            <label class="control-label col-md-2">Customer:<span class="required"></span></label>
                            <div class="col-md-2">
                                <asp:TextBox ID="Txtcustno" CssClass="form-control" PlaceHolder="Cust No" runat="server" Enabled="false"></asp:TextBox>
                            </div>
                            <%--  <div class="col-md-4">
                                </div>--%>
                        </div>
                    </div>
                    <div class="row" style="margin: 7px 0 7px 0;">
                        <div class="col-lg-12" style="height: 28px">
                            <label class="control-label col-md-2">Daily Amt :<span class="required">* </span></label>
                            <div class="col-md-2">
                                <asp:TextBox ID="TxtDailyAmt" CssClass="form-control" PlaceHolder="Daily Amt" runat="server" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="col-md-2"></div>
                            <label class="control-label col-md-2">PAN Card: </label>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtPan" placeholder="PAN Card" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row" runat="server" id="Div_LoanDetails" visible="false">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #fa4e0d">Loan Details</strong></div>
                            </div>
                        </div>

                        <div class="row" style="margin: 7px 0 7px 0;">
                            <div class="col-md-12" style="height: 28px">
                                <label class="control-label col-md-2">Loan Code<span class="required">* </span></label>
                                <div class="col-md-1">
                                    <asp:TextBox ID="TxtLoanSubglcode" Enabled="false" runat="server" onkeypress="javascript:return isNumber (event)" PlaceHolder="Agent Code" AutoPostBack="true" CssClass="form-control"></asp:TextBox>&nbsp;&nbsp;                               
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="TxtLoanSubglName" Enabled="false" runat="server" AutoPostBack="true" CssClass="form-control"></asp:TextBox>

                                </div>
                                <label class="control-label col-md-2">Account No:<span class="required">* </span></label>
                                <div class="col-md-1">
                                    <asp:TextBox ID="TxtLoanAccno" Enabled="false" runat="server" onkeypress="javascript:return isNumber (event)" AutoPostBack="true" CssClass="form-control"></asp:TextBox>&nbsp;&nbsp;
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="TxtLoanCustName" Enabled="false" runat="server" AutoPostBack="true" CssClass="form-control"></asp:TextBox>

                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc"></strong></div>
                            </div>
                        </div>
                        <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-12">
                                <label class="control-label col-md-2">Loan Sanction Amt </label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtSancAmount" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                                <label class="control-label col-md-1">Balance </label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtLoanBal" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                                <label class="control-label col-md-2">Last Int Date </label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="TxtLoanLastIntdt" CssClass="form-control" PlaceHolder="DD/MM/YYYY" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-12">
                                <label class="control-label col-md-2">Days </label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtDays" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                                <label class="control-label col-md-1">Intr Amt.</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtLoanInt" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                                <label class="control-label col-md-2">Total Balance </label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtTotLoanBal" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-12">
                                <div class="col-md-1">
                                    <asp:Button ID="btnLoanInt" Text="Post Int" runat="server" CssClass="btn btn-primary" OnClick="btnLoanInt_Click" />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-lg-12">
                                <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc"></strong></div>
                            </div>
                        </div>



                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #fa4e0d">Deduction : </strong></div>
                        </div>
                    </div>
                    <div class="row" style="margin: 7px 0 7px 0;">
                        <div class="col-lg-12">
                            <asp:Label ID="Lbl_Provision" runat="server" Text="Provision(B)" class="control-label col-md-2"></asp:Label>
                            <div class="col-md-2">
                                <asp:TextBox ID="Txt_Provi" runat="server" Text="0" onkeyup=" MINUS()" PlaceHolder="Provision" OnTextChanged="Txt_Provi_TextChanged" AutoPostBack="true" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>

                            <div class="col-md-2"></div>
                            <asp:Label ID="Label2" runat="server" Text="Calculated Intr.(A)" class="control-label col-md-2"></asp:Label>
                            <div class="col-md-2">
                                <asp:TextBox ID="TxtCalcInt" runat="server" Text="0" PlaceHolder="Interest" onkeyup="MINUS();Sum()" AutoPostBack="true" CssClass="form-control" OnTextChanged="TxtCalcInt_TextChanged"></asp:TextBox>
                            </div>

                        </div>
                    </div>
                    <div class="row" style="margin: 7px 0 7px 0;">
                        <div class="col-lg-12">

                            <asp:Label ID="lblpreCommission" runat="server" Text="Premature Commission:" class="control-label col-md-2"></asp:Label>
                            <div class="col-md-1">
                                <asp:TextBox ID="TxtPreMC" runat="server" PlaceHolder="Commission" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="TxtPreMCAMT" runat="server" PlaceHolder="Commission" OnTextChanged="TxtPreMCAMT_TextChanged" AutoPostBack="true" CssClass="form-control"></asp:TextBox>
                            </div>
                            <label class="control-label col-md-2">Oth Rec / Admin Chg <span class="required">* </span></label>
                            <div class="col-md-2">
                                <asp:TextBox ID="TxtServCHRS" runat="server" PlaceHolder="Other Receipts" OnTextChanged="TxtServCHRS_TextChanged" AutoPostBack="true" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="margin: 7px 0 7px 0;">
                        <div class="col-lg-12">
                            <label class="control-label col-md-2">Payable Intr (A-B) : <span class="required">* </span></label>
                            <div class="col-md-2">
                                <asp:TextBox ID="TxtINTC" runat="server" onkeyup="Sum()" PlaceHolder="Interest" OnTextChanged="TxtINTC_TextChanged" AutoPostBack="true" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>

                            <div class="col-md-2"></div>
                            <asp:Label ID="Label1" runat="server" Text="GST Amount" class="control-label col-md-2"></asp:Label>
                            <div class="col-md-2">
                                <asp:TextBox ID="TxtGST" runat="server" PlaceHolder="GST" ToolTip="18 % of Commission + Other Receipts" CssClass="form-control" OnTextChanged="TxtGST_TextChanged" AutoPostBack="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="margin: 7px 0 7px 0;">
                        <div class="col-lg-12">
                            <label class="control-label col-md-2">Payment Mode : <span class="required">* </span></label>
                            <div class="col-md-2">
                                <asp:DropDownList ID="ddlPayMode" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlPayMode_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Cash"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Transfer"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="Cheque"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2"></div>

                            <asp:Label ID="lblpayable" runat="server" Text="Amount" class="control-label col-md-2"></asp:Label>
                            <div class="col-md-2">
                                <asp:TextBox ID="TxtPayAmt" runat="server" onkeyup="Sum()" PlaceHolder="payeble Amount" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div id="DivTransfer" runat="server" visible="false">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #21fa09">Transfer Information : </strong></div>
                            </div>
                        </div>
                        <div class="row" style="margin: 7px 0 7px 0;">
                            <div class="col-lg-12">
                                <label class="control-label col-md-2">Product Type : <span class="required">* </span></label>
                                <div class="col-md-1">
                                    <asp:TextBox ID="TxtPType" runat="server" onkeypress="javascript:return isNumber (event)" PlaceHolder="Product Code" CssClass="form-control" OnTextChanged="TxtPType_TextChanged" AutoPostBack="true"></asp:TextBox>&nbsp;&nbsp;
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="TxtPTName" runat="server" PlaceHolder="Product Name" CssClass="form-control" OnTextChanged="TxtPTName_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    <div id="CustList" style="height: 200px; overflow-y: scroll;"></div>
                                    <asp:AutoCompleteExtender ID="AutoPName" runat="server" TargetControlID="TxtPTName"
                                        UseContextKey="true"
                                        CompletionInterval="1"
                                        CompletionSetCount="20"
                                        MinimumPrefixLength="1"
                                        EnableCaching="true"
                                        ServicePath="~/WebServices/Contact.asmx"
                                        ServiceMethod="GetGlName" CompletionListElementID="CustList">
                                    </asp:AutoCompleteExtender>
                                </div>
                                <label class="control-label col-md-2">Account No : <span class="required">* </span></label>
                                <div class="col-md-1">
                                    <asp:TextBox ID="TxtTAccNo" runat="server" PlaceHolder="Account No" onkeypress="javascript:return isNumber (event)" CssClass="form-control" OnTextChanged="TxtTAccNo_TextChanged" AutoPostBack="true"></asp:TextBox>&nbsp;&nbsp;
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="TxtTAName" runat="server" PlaceHolder="Account Holder Name" CssClass="form-control" OnTextChanged="TxtTAName_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    <div id="CustList2" style="height: 200px; overflow-y: scroll;">
                                        <asp:AutoCompleteExtender ID="AutoAccname" runat="server" TargetControlID="TxtTAName"
                                            UseContextKey="true"
                                            CompletionInterval="1"
                                            CompletionSetCount="20"
                                            MinimumPrefixLength="1"
                                            EnableCaching="true"
                                            ServicePath="~/WebServices/Contact.asmx"
                                            ServiceMethod="GetAccName" CompletionListElementID="CustList2">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="margin: 7px 0 7px 0;">
                            <div class="col-lg-12">
                                <label class="control-label col-md-2">Narration <span class="required">* </span></label>
                                <div class="col-md-4">
                                    <asp:TextBox ID="TxtTrfNarration" runat="server" onkeypress="javascript:return isNumber (event)" PlaceHolder="Transfer Narration" CssClass="form-control" AutoPostBack="true" OnTextChanged="TxtTrfNarration_TextChanged"></asp:TextBox>&nbsp;&nbsp;
                                </div>
                                <div id="divLoan" visible="false" runat="server">
                                    <label class="control-label col-md-2">Loan amount <span class="required">* </span></label>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtLoanTotAmt" CssClass="form-control" placeholder="Loan Amount" runat="server" OnTextChanged="txtLoanTotAmt_TextChanged" AutoPostBack="true" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row" id="DIV_INSTRUMENT" runat="server" style="margin: 7px 0 7px 0;">
                            <div class="col-md-12">
                                <label class="control-label col-md-2">Instrument No : <span class="required">* </span></label>
                                <div class="col-md-4">
                                    <asp:TextBox ID="TxtInstNo" runat="server" onkeypress="javascript:return isNumber (event)" MaxLength="6" PlaceHolder="Instrument No" CssClass="form-control"></asp:TextBox>&nbsp;&nbsp;                                                   
                                </div>
                                <label class="control-label col-md-2">Instrument Date : <span class="required">* </span></label>
                                <div class="col-md-4">
                                    <asp:TextBox ID="TxtInstDate" AutoPostBack="true" OnTextChanged="TxtInstDate_TextChanged" runat="server" PlaceHolder="DD/MM/YYYY" CssClass="form-control" onkeyup="FormatIt(this);"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #21fa09"></strong></div>
                        </div>
                    </div>
                    <div class="row" style="margin: 7px 0 7px 0;">
                        <div class="col-lg-12" align="center">
                            <asp:Button ID="BtnCashPost" runat="server" Text="SUBMIT" CssClass="btn btn-primary" OnClick="BtnCashPost_Click" />
                            <asp:Button ID="BtnClearALL" runat="server" Text="Clear All" CssClass="btn btn-primary" OnClick="BtnClearALL_Click" />
                            <asp:Button ID="BtnExit" runat="server" Text="EXIT" CssClass="btn btn-primary" OnClick="BtnExit_Click" />
                            <asp:Button ID="Btn_Printsummary" runat="server" Text="Summary Report" CssClass="btn btn-primary" OnClick="Btn_Printsummary_Click" />
                            <asp:Button ID="PostEntry" runat="server" Text="Post Entry" OnClientClick="Javascript:return Isvalide();" CssClass="btn btn-primary" OnClick="PostEntry_Click" Visible="false" />
                            <asp:Button ID="Btn_AccountStatement" runat="server" Text="Acc Statement" CssClass="btn btn-primary" OnClick="Btn_AccountStatement_Click" />
                        </div>

                    </div>
                </div>
            </div>
            <table class="table table-striped table-bordered table-hover">
                <thead>
                    <tr>
                        <th>
                            <asp:GridView ID="GrdEntryDate" runat="server" AllowPaging="True"
                                AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                EditRowStyle-BackColor="#FFFF99"
                                OnPageIndexChanging="GrdEntryDate_PageIndexChanging"
                                PageIndex="10" PageSize="25"
                                PagerStyle-CssClass="pgr" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="Gl Code" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="GL" runat="server" Text='<%# Eval("GL") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Product GlCode">
                                        <ItemTemplate>
                                            <asp:Label ID="SGL" runat="server" Text='<%# Eval("SUBGL") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Account No" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="ACCNO" runat="server" Text='<%# Eval("ACCNO") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Account Name" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="accname" runat="server" Text='<%# Eval("ACCNAME") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="INSTNO" runat="server" Text='<%# Eval("AMT") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Type">
                                        <ItemTemplate>
                                            <asp:Label ID="TXRTYPE" runat="server" Text='<%# Eval("TXRTYPE") %>'></asp:Label>
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

            <div class="row">
                <div class="col-md-12">
                    <div class="table-scrollable" style="width: 100%; height: 350px; overflow-x: auto; overflow-y: auto">
                        <table class="table table-striped table-bordered table-hover">
                            <thead>
                                <tr>
                                    <th>
                                        <asp:GridView ID="GridCalcu" runat="server" AutoGenerateColumns="false" ShowFooter="true" FooterStyle-HorizontalAlign="Right" FooterStyle-BackColor="#ffff66" OnRowDataBound="GridCalcu_RowDataBound">
                                            <Columns>

                                                <%--Glcode,Subglcode,Accno,ID,DATE_MON,Sch_ENDBAL,END_BAL,Sch_PRODUCT,PRODUCT,ONBALANCE,INTR_AMT,FINAL_INT,Rate--%>

                                                <asp:TemplateField HeaderText="Sr No" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="Th" HorizontalAlign="center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Agent Code" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Subglcode" runat="server" Text='<%# Eval("Subglcode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="Th" HorizontalAlign="center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="A/C No." Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Accno" runat="server" Text='<%# Eval("Accno") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="Th" HorizontalAlign="center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="On Month" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="DATE_MON" runat="server" Text='<%# Eval("DATE_MON") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sch_Month End Bal" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Sch_ENDBAL" runat="server" Text='<%# Eval("Sch_ENDBAL") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Month End Bal" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="END_BAL" runat="server" Text='<%# Eval("END_BAL") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="Th" HorizontalAlign="right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Sch_PRODUCT" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Sch_PRODUCT" runat="server" Text='<%# Eval("Sch_PRODUCT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="Th" HorizontalAlign="right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Product" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="PRODUCT" runat="server" Text='<%# Eval("PRODUCT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="Th" HorizontalAlign="right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="ONBALANCE" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ONBALANCE" runat="server" Text='<%# Eval("ONBALANCE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="Th" HorizontalAlign="right" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Int. Amount" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="INTR_AMT" runat="server" Text='<%# Eval("INTR_AMT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="Th" HorizontalAlign="right" />
                                                    <FooterTemplate>
                                                        <asp:Label ID="LblIntSum" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Rate of Int" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Rate" runat="server" Text='<%# Eval("Rate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="Th" HorizontalAlign="right" />
                                                </asp:TemplateField>


                                            </Columns>
                                            <PagerStyle CssClass="pgr"></PagerStyle>
                                            <SelectedRowStyle BackColor="#66FF99" />
                                            <EditRowStyle BackColor="#FFFF99"></EditRowStyle>
                                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                        </asp:GridView>
                                    </th>
                                </tr>
                            </thead>
                        </table>
                    </div>
                </div>
            </div>
            <div id="divStmtGrd" class="col-md-12" runat="server" visible="false">
                <div class="table-scrollable" style="width: 100%; height: auto; max-height: 500px; overflow-x: auto; overflow-y: auto">
                    <table class="table table-striped table-bordered table-hover" width="100%">
                        <thead>
                            <tr>
                                <th>
                                    <asp:GridView ID="grdStmtGrd" runat="server" AlternatingRowStyle-CssClass="alt" CssClass="mGrid" ShowFooter="true"
                                        OnDataBound="grdStmtGrd_DataBound" AutoGenerateColumns="false" EditRowStyle-BackColor="#FFFF99" Width="100%">
                                        <Columns>

                                            <asp:TemplateField HeaderStyle-Width="100px" HeaderText="EntryDate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEntryDate" runat="server" Text='<%# Eval("EntryDate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="50px" HeaderText="SetNo">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSetNo" runat="server" Text='<%# Eval("SetNo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="200px" HeaderText="Particular">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblParticular" runat="server" Text='<%# Eval("Particular") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="100px" HeaderText="Credit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPCredit" runat="server" Text='<%# Eval("PCredit") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblPCreditTotal" Text="lblPCreditTotal" runat="server" />
                                                </FooterTemplate>
                                                <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                <FooterStyle CssClass="Th" HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="100px" HeaderText="Debit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPDebit" runat="server" Text='<%# Eval("PDebit") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblPDebitTotal" Text="lblPCreditTotal" runat="server" />
                                                </FooterTemplate>
                                                <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                <FooterStyle CssClass="Th" HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="100px" HeaderText="Balance">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPBalance" runat="server" Text='<%# Eval("PBalance") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="20px" HeaderText="DrCr">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPDrCr" runat="server" Text='<%# Eval("PDrCr") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="100px" HeaderText="Credit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblICredit" runat="server" Text='<%# Eval("ICredit") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblICreditTotal" Text="lblPCreditTotal" runat="server" />
                                                </FooterTemplate>
                                                <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                <FooterStyle CssClass="Th" HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="100px" HeaderText="Debit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIDebit" runat="server" Text='<%# Eval("IDebit") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblIDebitTotal" Text="lblPCreditTotal" runat="server" />
                                                </FooterTemplate>
                                                <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                <FooterStyle CssClass="Th" HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="100px" HeaderText="Balance">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIBalance" runat="server" Text='<%# Eval("IBalance") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="20px" HeaderText="DrCr">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIDrCr" runat="server" Text='<%# Eval("IDrCr") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="100px" HeaderText="Total Balance">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTotalBalance" runat="server" Text='<%# Eval("TotalBalance") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="20px" HeaderText="DrCr">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPDrCr" runat="server" Text='<%# Eval("PDrCr") %>'></asp:Label>
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
    <div class="modal fade bs-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" style="margin-left: 1%; width: 100%; height: 90%" id="divPhoto1" runat="server">
        <div class="modal-dialog modal-lg" role="document" style="width: 90%; height: 100%">
            <div class="modal-content" style="border: 5px solid #4dbfc0; height: 100%">
                <div class="inner_top" style="height: 100%">
                    <div class="panel panel-default" style="height: 100%">
                        <div class="panel-heading">Photo Signature View</div>
                        <div class="panel-body" style="height: 100%">
                            <div class="col-sm-12" style="height: 100%">

                                <div class="col-lg-12" style="height: 100%">


                                    <div class="table-responsive" style="height: 90%">
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-11">
                                                <label class="control-label col-md-3">Product Type : <span class="required">* </span></label>

                                                <div class="col-md-1">

                                                    <asp:TextBox ID="txtProd" PLACEHOLDER="PRODUCT TYPE" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                                    <%--<asp:TextBox ID="TextBox1" CssClass="form-control" runat="server"  AutoPostBack="true" OnTextChanged="TxtProcode_TextChanged" ></asp:TextBox>--%>
                                                </div>
                                                <div class="col-md-5">
                                                    <asp:TextBox ID="txtProdCode" placeholder="PRODUCT NAME" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-11">
                                                <label class="control-label col-md-3">Account Number : <span class="required">* </span></label>
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="txtaccno1" placeholder="ACCOUNT NO" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                                    <%--<asp:TextBox ID="TextBox1" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="TxtAccNo_TextChanged"></asp:TextBox>--%>
                                                </div>
                                                <div class="col-md-5">
                                                    <asp:TextBox ID="txtaccname1" placeholder="ACCOUNT NAME" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>

                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtcust" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="height: 75%; padding-top: 5px">
                                            <div class="col-lg-11" style="height: 100%">
                                                <div style="height: 75%;">
                                                    <table style="height: 100%; width: 100%;">
                                                        <tr>
                                                            <td style="width: 40%;" align="center">
                                                                <div>
                                                                    <asp:Label ID="lbl1" runat="server" Text="Signature" Style="font-weight: bold"></asp:Label>
                                                                    <%--<div style="width:100%;height:100%;padding:5px" onclick="ShowUpload(1)" runat="server" id="DivSign" align="center">--%>
                                                                </div>
                                                                <img id="image1" runat="server" style="height: 40%; width: 50%; border: 1px solid #000000; padding: 5px" />

                                                                <%-- </div>--%>
                                                            </td>
                                                            <td style="width: 20%"></td>
                                                            <td style="width: 40%" align="center">
                                                                <div>
                                                                    <asp:Label ID="Label4" runat="server" Text="Photo" Style="font-weight: bold"></asp:Label>
                                                                </div>
                                                                <%--<div style="width:100%;height:100%;padding:5px" onclick="ShowUpload(2)" runat="server" id="Div2" align="center">--%>
                                                                <img id="image2" runat="server" style="height: 40%; width: 50%; border: 1px solid #000000; padding: 5px" />
                                                                <%-- </div>--%>
                                                                       
                                                            </td>

                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                            <div class="row" align="center">
                                                <div class="col-lg-11">
                                                    <%--<asp:Button ID="btnChange" runat="server" Text="Change" CssClass="btn blue" OnClick="btnChange_Click" />--%>

                                                    <asp:Button ID="btnPrev" runat="server" Text="Previous" CssClass="btn blue" OnClick="btnPrev_Click" />
                                                    <asp:Button ID="Button1" runat="server" Text="Exit" CssClass="btn btn-primary" data-dismiss="modal" />
                                                    <asp:Button ID="btnNext" runat="server" Text="Next" CssClass="btn blue" OnClick="btnNext_Click" />
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
    <asp:HiddenField ID="hdnRow" runat="server" Value="0" />
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
        <asp:HiddenField Id="Hdn_ParRevProvi" runat="server" />
    </div>
</asp:Content>

