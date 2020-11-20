<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="frmTDAClearCBS2.aspx.cs" Inherits="frmTDAClearCBS2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.value = "";
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";

            if (confirm("Do you want to Maturity Interest for Account, IF Yes Press OK?")) {
                document.getElementById("INPUT").value = "";
                confirm_value.value = "Yes";
            } else {
                document.getElementById("INPUT").value = "";
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
    <script type="text/javascript">
        function Validate() {
            var date = document.getElementById('<% =TxtProcode.ClientID%>').value;
            var AccNo = document.getElementById('<% =txtAccNo.ClientID%>').value;
            var custn = document.getElementById('<% =Txtcustno.ClientID%>').value;
            var DepoAmt = document.getElementById('<% =TxtDepoAmt.ClientID%>').value;
            var Period = document.getElementById('<% =TxtPeriod.ClientID%>').value;
            var Intrest = document.getElementById('<% =TxtIntrest.ClientID%>').value;
            var Maturity = document.getElementById('<% =TxtMaturity.ClientID%>').value;
            var DueDate = document.getElementById('<% =DtDueDate.ClientID%>').value;
            var PayType = document.getElementById('<% =ddlPayType.ClientID%>').value;

            if (date == "") {
                window.alert("Enter Product code ...!!");
                document.getElementById('<% =TxtProcode.ClientID%>').focus();
                return false;
            }
            if (AccNo == "") {
                window.alert("Enter Account number ...!!");
                document.getElementById('<% =txtAccNo.ClientID%>').focus();
                return false;
            }
            if (custn == "") {
                window.alert("Custno not available ...!!");
                document.getElementById('<% =Txtcustno.ClientID%>').focus();
                return false;
            }
            if (DepoAmt == "") {
                window.alert("Deposite amount is not present ...!!");
                document.getElementById('<% =TxtDepoAmt.ClientID%>').focus();
                return false;
            }
            if (Period == "") {
                window.alert("Period is not present ...!!");
                document.getElementById('<% =TxtPeriod.ClientID%>').focus();
                return false;
            }
            if (Intrest == "") {
                window.alert("Interest is not present ...!!");
                document.getElementById('<% =TxtIntrest.ClientID%>').focus();
                return false;
            }
            if (Maturity == "") {
                window.alert("Maturity amount is not present ...!!");
                document.getElementById('<% =TxtMaturity.ClientID%>').focus();
                return false;
            }
            if (DueDate == "") {
                window.alert("Due date not available ...!!");
                document.getElementById('<% =DtDueDate.ClientID%>').focus();
                return false;
            }
            if (PayType == 0) {
                window.alert("Select Payment type ...!!");
                document.getElementById('<% =ddlPayType.ClientID%>').focus();
                return false;
            }
        }
    </script>

    <script type="text/javascript">
        function IsValid() {
            var Procode = document.getElementById('<% =Txtprocode1.ClientID%>').value;
            var AccNo = document.getElementById('<% =TxtAccNo1.ClientID%>').value;

            if (Procode == "") {
                window.alert("Enter loan product code first ...!!");
                document.getElementById('<% =Txtprocode1.ClientID%>').focus();
                return false;
            }
            if (AccNo == "") {
                window.alert("Enter loan account number first ...!!");
                document.getElementById('<% =TxtAccNo1.ClientID%>').focus();
                return false;
            }
        }
    </script>

    <script type="text/javascript">
        function validatecal() {
            var date = document.getElementById('<% =TxtProcode.ClientID%>').value;
            var AccNo = document.getElementById('<% =txtAccNo.ClientID%>').value;

            if (date == "") {
                window.alert("Product Code is not present ...!!");
                document.getElementById('<% =TxtProcode.ClientID%>').focus();
                return false;
            }
            if (AccNo == "") {
                window.alert("Account No is not present ...!!");
                document.getElementById('<% =txtAccNo.ClientID%>').focus();
                return false;
            }
        }
    </script>

    <script>
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;
            return true;
        }
    </script>

    <script type="text/javascript">
        function FormatIt(obj) {
            if (obj.value.length == 2) // Day
                obj.value = obj.value + "/";
            if (obj.value.length == 5) // month 
                obj.value = obj.value + "/";
            if (obj.value.length == 11) // year 
                alert("Please Enter valid Date ...!!");
        }
    </script>
    <script type="text/javascript">
        function Btn_chg() {
            var INew = document.getElementById('<%=TxtInterestNew.ClientID%>').value || 0;

            if (parseFloat(INew) >= 0) {
                document.getElementById('<%=BtnPostInt.ClientID%>').value = "Post Int";
            }
            else {
                document.getElementById('<%=BtnPostInt.ClientID%>').value = "Rev Int";
            }

        }

        function CheckTotalPay() {
            var TotPay = document.getElementById('<%=TxtTotalPayShow.ClientID%>').value || 0;
            var MatAmt = document.getElementById('<%=TxtMaturity.ClientID%>').value || 0;
            var IntApp = document.getElementById('<%=TxtInterestNew.ClientID%>').value || 0;
            var DedAmt;

            if (parseFloat(TotPay) = parseFloat(MatAmt)) {
            }
            else if (parseFloat(TotPay) > parseFloat(MatAmt)) {
                DedAmt = parseFloat(TotPay) - parseFloat(MatAmt);
                document.getElementById('<%=TxtTotalPayShow.ClientID%>').value = MatAmt;
                document.getElementById('<%=TxtInterestNew.ClientID%>').value = -DedAmt;
            }
    }

    function ShowTotalPayable() {
        debugger;
        var PrinPay = document.getElementById('<%=TxtPrincPaybl.ClientID%>').value || 0;
        var IntPay = document.getElementById('<%=TxtIntrestPaybl.ClientID%>').value || 0;
        var IntNew = document.getElementById('<%=TxtInterestNew.ClientID%>').value || 0;
        var SbInt = document.getElementById('<%=TxtSbintrest.ClientID%>').value || 0;
        var TDSAdmin = document.getElementById('<%=TxtAdminCharges.ClientID%>').value || 0;
        var Commi = document.getElementById('<%=TxtCommission.ClientID%>').value || 0;
        var Total = 0, Deduction = 0, Sumation = 0;

        Deduction = parseFloat(Commi) + parseFloat(TDSAdmin);
        Sumation = parseFloat(PrinPay) + parseFloat(IntPay) + parseFloat(IntNew) + parseFloat(SbInt);

        if (!isNaN(Sumation) && !isNaN(Deduction)) {
            document.getElementById('<%=TxtTotalPayShow.ClientID%>').value = Sumation - Deduction;
            }
            CheckTotalPay();
        }

    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%-- DEPOSITE CLOSURE INTEREST --%>
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        Deposit Closure
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab1">

                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Set Info</strong></div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-3">
                                                    <asp:RadioButton ID="Rdb_NewSetno" AccessKey="1" Style="margin: 15px;" OnCheckedChanged="Rdb_NewSetno_CheckedChanged" runat="server" Text="New Setno" AutoPostBack="true" GroupName="Type" Checked="true" />
                                                    <asp:RadioButton ID="Rdb_ExistingSetno" AccessKey="2" OnCheckedChanged="Rdb_ExistingSetno_CheckedChanged" runat="server" Text="Existing Setno" AutoPostBack="true" GroupName="Type" />

                                                </div>
                                                <div id="Div_ExSetno" runat="server" visible="false">
                                                    <label class="control-label col-md-1">Set No<span class="required">* </span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="Txt_ExistSetno" onkeypress="javascript:return isNumber (event)" AutoPostBack="true" CssClass="form-control" runat="server" OnTextChanged="Txt_ExistSetno_TextChanged"></asp:TextBox>
                                                    </div>
                                                    <label class="control-label col-md-1">Trx :<span class="required">* </span></label>
                                                    <div class="col-md-2">
                                                        <asp:DropDownList ID="DdlCrDr" runat="server" CssClass="form-control" OnSelectedIndexChanged="DdlCrDr_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Value="0" Text="--Select--" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Value="1" Text="Credit"></asp:ListItem>
                                                            <asp:ListItem Value="2" Text="Debit"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Deposit Information : </strong></div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-1" style="width: 110px">Deposit Type :<span class="required">*</span></label>
                                                <div class="col-md-4">
                                                    <asp:RadioButton ID="rdbMature" runat="server" Text="Mature" AutoPostBack="true" OnCheckedChanged="rdbMature_CheckedChanged" GroupName="mat" Enabled="false" />
                                                    <asp:RadioButton ID="rdbPreMature" runat="server" Text="PreMature" AutoPostBack="true" OnCheckedChanged="rdbPreMature_CheckedChanged" GroupName="mat" Enabled="false" />
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:RadioButton ID="Rdb_IntPay" runat="server" Text="Interest Pay" AutoPostBack="true" OnCheckedChanged="Rdb_IntPay_CheckedChanged" GroupName="PAY" Enabled="true" ToolTip="For MIS Interest Transfer" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-1" style="width: 110px">Prod Code :<span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtProcode" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="TxtProcode_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="TxtProName" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="TxtProName_TextChanged"></asp:TextBox>
                                                    <div id="CustList" style="height: 200px; overflow-y: scroll;"></div>
                                                    <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="TxtProName"
                                                        UseContextKey="true"
                                                        CompletionInterval="1"
                                                        CompletionSetCount="20"
                                                        MinimumPrefixLength="1"
                                                        EnableCaching="true"
                                                        ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList"
                                                        ServiceMethod="getglname">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                                <label class="control-label col-md-1">Rec Srno<span class="required"></span></label>
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="TxtRecNo" Enabled="false" CssClass="form-control" onkeypress="javascript:return isNumber (event)" PlaceHolder="Receipt No" runat="server" AutoPostBack="true" OnTextChanged="TxtRecNo_TextChanged"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-1">Custno : <span class="required"></span></label>
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="Txtcustno" CssClass="form-control" PlaceHolder="Cust No" runat="server" Enabled="false"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-1" style="width: 110px">Acc No :<span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtAccNo" CssClass="form-control" PlaceHolder="Account No" runat="server" AutoPostBack="true" OnTextChanged="txtAccNo_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="TxtAccname" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="TxtAccname_TextChanged"></asp:TextBox>
                                                    <div id="CustList2" style="height: 200px; overflow-y: scroll;"></div>
                                                    <asp:AutoCompleteExtender ID="AutoAccname" runat="server" TargetControlID="TxtAccName"
                                                        UseContextKey="true"
                                                        CompletionInterval="1"
                                                        CompletionSetCount="20"
                                                        MinimumPrefixLength="1"
                                                        EnableCaching="true"
                                                        ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList2"
                                                        ServiceMethod="GetAccName">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                                <label class="control-label col-md-1">PAN Card: </label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtPan" placeholder="PAN Card" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-1" style="width: 110px">Joint Name :</label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtJointAccName" CssClass="form-control" runat="server" Enabled="False"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-1" style="width: 110px">Acc Type :</label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtAccType" CssClass="form-control" runat="server" Enabled="False"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2" style="width: 40px"></div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-1" style="width: 110px">Deposit Date :<span class="required"></span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="dtDeposDate" onkeyup="FormatIt(this)" CssClass="form-control" PlaceHolder="DD/MM/YYYY" runat="server" Enabled="False"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-1" style="width: 110px">Int Payout :</label>
                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="ddlIntrestPay" runat="server" CssClass="form-control" Enabled="False">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-2" style="width: 40px"></div>
                                                <label class="control-label col-md-2">Int Amt: <span class="required"></span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtIntrest" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-1" style="width: 110px">Deposit Amt :<span class="required"></span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtDepoAmt" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" AutoPostBack="true" OnTextChanged="TxtDepoAmt_TextChanged" Enabled="False"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-1" style="width: 110px">Period :</label>
                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="ddlduration" runat="server" CssClass="form-control" Enabled="False">
                                                        <asp:ListItem Value="M">Months</asp:ListItem>
                                                        <asp:ListItem Value="D">Days</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="TxtPeriod" CssClass="form-control" PlaceHolder="Period" runat="server" Style="width: 77px;" Enabled="False"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-1" style="width: 130px">Int Rate :</label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtRate" CssClass="form-control" runat="server" Enabled="false" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-1" style="width: 110px">Maturity Amt :<span class="required"></span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtMaturity" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-1" style="width: 110px">Due Date :</label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="DtDueDate" CssClass="form-control" PlaceHolder="dd/mm/yyyy" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2" style="width: 40px"></div>
                                                <label class="control-label col-md-2">Close Status : <span class="required"></span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtOpenClose" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc"></strong></div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-1" style="width: 110px">Princ Payable :<span class="required"></span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtPrincPaybl" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-1" style="width: 110px">Int Payable :</label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtIntrestPaybl" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2" style="width: 40px"></div>
                                                <label class="control-label col-md-2">Interest Paid : <span class="required"></span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtIntPaid" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-1" style="width: 110px">Int Applied :<span class="required"></span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtInterestNew" onkeyup="ShowTotalPayable();" onchange="Btn_chg();" CssClass="form-control" runat="server" value="0"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-1" id="Lbl_AdminChr" runat="server" style="width: 110px">TDS/AdmChg :</label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtAdminCharges" onkeyup="ShowTotalPayable();" CssClass="form-control" runat="server" value="0" Enabled="true"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2" style="width: 40px"></div>
                                                <label class="control-label col-md-2">Last Int Date :<span class="required"></span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtTDLastIntDt" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;" id="PWI" runat="server" visible="false">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-1" style="width: 110px">Transfer Type :<span class="required">*</span></label>
                                                <div class="col-md-4">
                                                    <asp:RadioButton ID="RdbPI" runat="server" Text="Principle With INT" GroupName="mat" OnCheckedChanged="RdbPI_CheckedChanged" AutoPostBack="true" />
                                                    <asp:RadioButton ID="RdbP" runat="server" Text="Only Principle" GroupName="mat" OnCheckedChanged="RdbP_CheckedChanged" AutoPostBack="true" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;" runat="server" id="SB">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-1" id="lblpenalrate" runat="server" style="width: 110px">SB Int :<span class="required"></span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtSbintrest" Enabled="true" onkeyup="ShowTotalPayable();" CssClass="form-control" runat="server" value="0"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-1" id="Lbl_Commi" runat="server" style="width: 110px">Commission :</label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtCommission" onkeyup="ShowTotalPayable();" CssClass="form-control" runat="server" value="0" Enabled="true" ToolTip="Calculated on total credit"></asp:TextBox>
                                                </div>
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="TxtCROI" onkeyup="ShowTotalPayable();" OnTextChanged="TxtCROI_TextChanged" PlaceHolder="Commi Rate" AutoPostBack="true" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                                <div id="Div1" runat="server" visible="false">
                                                    <asp:Label ID="lblpenal" runat="server" class="control-label col-md-2" Text="SB int rate"></asp:Label>
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="txt1" CssClass="form-control" runat="server" value="0" Enabled="false"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="Txt2" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-1" style="width: 110px">TDS Provision :<span class="required"></span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtTDSP" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-1" style="width: 110px">TDS Paid :</label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtTDSPaid" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                                <div class="col-md-1">
                                                    <asp:Button ID="BtnPostInt" runat="server" Text="Post INT" CssClass="btn blue" Visible="false" OnClick="BtnPostInt_Click" Width="100px" />
                                                </div>
                                                <div class="col-md-1">
                                                    <asp:Button ID="BtnCalcSbInt" runat="server" Text="Calc SBINT" CssClass="btn blue" Visible="false" OnClick="OnConfirm" Width="100px" />
                                                </div>
                                                <label class="control-label col-md-1">Total Pay<span class="required"></span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtTotalPayShow" BackColor="#99ff99" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <%--Added By Amol--%>
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc"></strong></div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-1" style="width: 110px">Loan SancAmt :</label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtSancAmount" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-1" style="width: 110px">Balance :</label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtLoanBal" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-2">Last Int Date </label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtLastIntDate" CssClass="form-control" PlaceHolder="DD/MM/YYYY" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-1" style="width: 110px">Total Days :</label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtDays" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-1" style="width: 110px">Interest :</label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtLoanInt" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-2">Total Balance </label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtTotLoanBal" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                                <div class="col-md-1">
                                                    <asp:Button ID="btnLoanInt" Visible="false" Text="Post Int" runat="server" CssClass="btn btn-primary" OnClick="btnLoanInt_Click" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc"></strong></div>
                                            </div>
                                        </div>
                                        <%--End Added By Amol--%>

                                        <asp:Table ID="TblDiv_MainWindow" runat="server">
                                            <asp:TableRow ID="Tbl_R1" runat="server">
                                                <asp:TableCell ID="Tbl_c1" runat="server" Width="100%">

                                                    <div class="row" style="margin-top: 5px; margin-bottom: 5px;" runat="server" visible="false" id="DIVINT">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-1">
                                                                <label class="control-label">Saving Acc No / Name:<span class="required"></span></label>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="TxtINTAcc" CssClass="form-control" PlaceHolder="ID" runat="server" AutoPostBack="true" OnTextChanged="TxtINTAcc_TextChanged"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="TxtIntAccName" CssClass="form-control" PlaceHolder="Customer Name" runat="server" Enabled="false"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                        <div class="col-md-12">
                                                            <label class="control-label col-md-1" style="width: 110px">Close Type :<span class="required">*</span></label>
                                                            <div class="col-md-10">
                                                                <asp:RadioButton ID="RdbClose" runat="server" Checked="true" Text="Close" AutoPostBack="true" OnCheckedChanged="RdbClose_CheckedChanged" GroupName="CR" Style="margin: 15px;" />
                                                                <asp:RadioButton ID="RdbRenew" runat="server" Text="Renew" AutoPostBack="true" OnCheckedChanged="RdbRenew_CheckedChanged" GroupName="CR" Style="margin: 15px;" />
                                                                <asp:RadioButton ID="RdbTrfInt" runat="server" Text="Trf Interest" OnCheckedChanged="RdbTrfInt_CheckedChanged" AutoPostBack="true" GroupName="CR" Style="margin: 15px;" ToolTip="Will post only Interest Payable (MIS Only)" />
                                                                <asp:RadioButton ID="RdbMultipleClose" runat="server" Text="Multiple Closure" OnCheckedChanged="RdbMultipleClose_CheckedChanged" AutoPostBack="true" GroupName="CR" Style="margin: 15px;" ToolTip="Multiple TDA Clousre" />
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                        <div class="col-md-12">
                                                            <label class="control-label col-md-1" style="width: 120px">Transfer Type :<span class="required">*</span></label>
                                                            <div class="col-md-10">
                                                                <asp:CheckBox ID="Chk_Multitransfer" AccessKey="3" runat="server" Text="Multiple Transfer" OnCheckedChanged="Chk_Multitransfer_CheckedChanged" AutoPostBack="true" />
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row" style="margin: 7px 0 7px 0; margin-bottom: 5px">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-1" style="width: 120px">Pay Type :<span class="required"></span></label>
                                                            <div class="col-md-2">
                                                                <asp:DropDownList ID="ddlPayType" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPayType_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </div>
                                                            <label class="control-label col-md-2">Total Pay Amount</label>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtPayAmnt" CssClass="form-control" PlaceHolder="Total Payable" runat="server" Enabled="true" OnTextChanged="txtPayAmnt_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row" style="margin: 7px 0 7px 0; margin-bottom: 5px">
                                                        <div id="ABB" visible="false" runat="server">
                                                            <div id="Div2" class="row" style="margin: 07px 0 7px 0;" runat="server">
                                                                <div class="col-lg-12">
                                                                    <label class="control-label col-md-1">Branch Code :<span class="required">*</span></label>
                                                                    <div class="col-md-2">
                                                                        <asp:TextBox ID="txtBrcd" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtBrcd_TextChanged"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <asp:TextBox ID="txtBName" CssClass="form-control" runat="server"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div id="Transfer" visible="false" runat="server">
                                                        <div id="Div3" class="row" style="margin: 07px 0 7px 0;" runat="server">
                                                            <div class="col-lg-12">
                                                                <label class="control-label col-md-1">Product Code :<span class="required">*</span></label>
                                                                <div class="col-md-2">
                                                                    <asp:TextBox ID="Txtprocode1" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="Txtprocode1_TextChanged"></asp:TextBox>
                                                                </div>

                                                                <div id="Div4" class="col-md-4" runat="server">
                                                                    <asp:TextBox ID="Txtglcode" CssClass="form-control" PlaceHolder="Product Name" OnTextChanged="Txtglcode_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                                                    <div id="CustList1" style="height: 200px; overflow-y: scroll;"></div>
                                                                    <asp:AutoCompleteExtender ID="autoglname1" runat="server" TargetControlID="Txtglcode"
                                                                        UseContextKey="true"
                                                                        CompletionInterval="1"
                                                                        CompletionSetCount="20"
                                                                        MinimumPrefixLength="1"
                                                                        EnableCaching="true"
                                                                        ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList1"
                                                                        ServiceMethod="GetGlName">
                                                                    </asp:AutoCompleteExtender>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div id="Div5" class="row" style="margin: 07px 0 7px 0;" runat="server">
                                                            <div class="col-lg-12">
                                                                <label class="control-label col-md-1">Acc No : <span class="required">*</span></label>
                                                                <div class="col-md-2">
                                                                    <asp:TextBox ID="TxtAccNo1" CssClass="form-control" onkeypress="javascript:return isNumber (event)" PlaceHolder="ID" runat="server" AutoPostBack="true" OnTextChanged="TxtAccNo1_TextChanged"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <asp:TextBox ID="TxtCustName1" CssClass="form-control" PlaceHolder="Customer Name" runat="server" AutoPostBack="true" OnTextChanged="TxtCustName1_TextChanged"></asp:TextBox>
                                                                    <div id="CustList3" style="height: 200px; overflow-y: scroll;"></div>
                                                                    <asp:AutoCompleteExtender ID="TrfAccName" runat="server" TargetControlID="TxtCustName1"
                                                                        UseContextKey="true"
                                                                        CompletionInterval="1"
                                                                        CompletionSetCount="20"
                                                                        MinimumPrefixLength="1"
                                                                        EnableCaching="true"
                                                                        ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList3"
                                                                        ServiceMethod="GetAccName">
                                                                    </asp:AutoCompleteExtender>
                                                                </div>
                                                                <div id="divLoan" visible="false" runat="server">
                                                                    <div class="col-md-2">
                                                                        <asp:TextBox ID="txtLoanTotAmt" CssClass="form-control" placeholder="Loan Amount" runat="server" OnTextChanged="txtLoanTotAmt_TextChanged" AutoPostBack="true" />
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <asp:Button ID="btnPayLoan" CssClass="btn btn-primary" OnClientClick="javascript:return IsValid();" OnClick="btnPayLoan_Click" Text="Submit" runat="server" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div id="Div_RecSrno" visible="false" runat="server">
                                                            <div id="Div6" class="row" style="margin: 07px 0 7px 0;" runat="server">
                                                                <div class="col-lg-12">
                                                                    <label class="control-label col-md-1">RecSrno<span class="required">*</span></label>
                                                                    <div class="col-md-2">
                                                                        <asp:TextBox ID="TxtTRecno" CssClass="form-control" onkeypress="javascript:return isNumber (event)" runat="server" AutoPostBack="true" OnTextChanged="TxtTRecno_TextChanged"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div id="DivNarrattion" class="row" style="margin: 07px 0 7px 0;" runat="server">
                                                            <div class="col-lg-12">
                                                                <label class="control-label col-md-1">Narration<span class="required">*</span></label>
                                                                <div class="col-md-6">
                                                                    <asp:TextBox ID="TxtTNarration" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="TxtTNarration_TextChanged"></asp:TextBox>
                                                                </div>

                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div id="Transfer2" visible="false" runat="server">
                                                        <div id="Div7" class="row" style="margin: 07px 0 7px 0;" runat="server">
                                                            <div class="col-lg-12">
                                                                <label class="control-label col-md-1">Instrument No :<span class="required">*</span></label>
                                                                <div class="col-md-2">
                                                                    <asp:TextBox ID="TxtChequeNo" placeholder="CHEQUE NUMBER" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                                </div>
                                                                <label class="control-label col-md-2">Instrument Date :<span class="required">*</span></label>
                                                                <div class="col-md-2">
                                                                    <asp:TextBox ID="TxtChequeDate" placeholder="CHEQUE DATE" CssClass="form-control" onkeyup="FormatIt(this)" runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </asp:TableCell><asp:TableCell ID="TableCell1" runat="server" Width="100%" BorderStyle="Solid" BorderWidth="1px">
                                                    <div class="col-md-6">
                                                        <asp:Table ID="Tbl_Photo" runat="server">
                                                            <asp:TableRow ID="Rw_Ph1" runat="server">
                                                                <asp:TableCell ID="TblCell1" runat="server">
                                                                    <asp:Label ID="Label3" runat="server" Text="Signature" Style="font-weight: bold"></asp:Label>
                                                                    <img id="Img1" runat="server" style="height: 50%; width: 95%; border: 1px solid #000000; padding: 5px" />
                                                                </asp:TableCell>
                                                                <asp:TableCell ID="TblCell2" runat="server">
                                                                    <asp:Label ID="Label4" runat="server" Text="Photo" Style="font-weight: bold"></asp:Label>
                                                                    <img id="Img2" runat="server" style="height: 50%; width: 100%; border: 1px solid #000000; padding: 5px" />
                                                                </asp:TableCell>
                                                            </asp:TableRow>

                                                        </asp:Table>
                                                    </div>
                                                </asp:TableCell></asp:TableRow></asp:Table><div>
                                            <hr style="margin: 10px 0px 10px 0px" />
                                        </div>

                                        <div class="row" style="margin: 07px 0 1px 0">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <asp:Button ID="Btn_Add" runat="server" Text="Add" CssClass="btn green" OnClick="Btn_Add_Click" Enabled="false" />
                                                    <asp:Button ID="Btn_PostM" runat="server" Text="Post" CssClass="btn green" OnClick="Btn_PostM_Click" Enabled="false" />
                                                </div>
                                                <div id="Div_SSubmit" class="col-md-2" runat="server" visible="true">
                                                    <asp:Button ID="BtnSubmit" runat="server" CssClass="btn green" Text="Submit / Close Ac" OnClick="BtnSubmit_Click" OnClientClick="Validate()" />
                                                </div>
                                                <div id="Div_MSubmit" class="col-md-2" runat="server" visible="false">
                                                    <asp:Button ID="BtnMultipleSubmit" runat="server" CssClass="btn green" Text="Multiple Cl.Submit" OnClick="BtnMultipleSubmit_Click" />
                                                </div>

                                                <div class="col-md-2">
                                                    <asp:Button ID="btnView" runat="server" CssClass="btn blue" Text="View TDS" OnClick="btnView_Click" />
                                                    <asp:Button ID="BtnExit" runat="server" CssClass="btn green" Text="Exit" OnClick="BtnExit_Click" Visible="false" />
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:Button ID="Report" runat="server" Text="FD Receipt" CssClass="btn blue" OnClick="Report_Click" />
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:Button ID="ClearData" runat="server" Text="Clear All" CssClass="btn blue" OnClick="ClearData_Click" />
                                                </div>

                                                <div class="col-md-2">
                                                    <asp:Button ID="Btn_AccountStatement" runat="server" Text="Acc Statement" CssClass="btn blue" OnClick="Btn_AccountStatement_Click" />
                                                </div>
                                                <asp:HiddenField ID="Hdn_Amount" runat="server" />
                                                <asp:HiddenField ID="Hdn_SubmittedAmt" runat="server" />
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="table-scrollable">
                                                    <table class="noborder fullwidth">
                                                        <thead>
                                                            <tr>
                                                                <td>
                                                                    <asp:GridView ID="grdvoucher" runat="server" OnRowDataBound="grdvoucher_RowDataBound"
                                                                        AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                                        EditRowStyle-BackColor="#FFFF99"
                                                                        PagerStyle-CssClass="pgr" CssClass="table table-striped table-bordered table-hover  noborder fullwidth">
                                                                        <Columns>

                                                                            <asp:TemplateField HeaderText="Id" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Lbl_VId" runat="server" Text='<%# Eval("Id") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="BRcd" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Lbl_VBrcd" runat="server" Text='<%# Eval("Brcd") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="EntryDate" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Lbl_VEntryDate" runat="server" Text='<%# Eval("EntryDate") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Subglcode" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Lbl_VSubglcode" runat="server" Text='<%# Eval("Subglcode") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="GlName" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Lbl_VGlName" runat="server" Text='<%# Eval("GlName") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Accno" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Lbl_VAccno" runat="server" Text='<%# Eval("Accno") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="CustName" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Lbl_VCustName" runat="server" Text='<%# Eval("CustName") %>'></asp:Label></ItemTemplate></asp:TemplateField><%--A.Id,A.Brcd,A.EntryDate,A.SUbglcode,B.GLNAME GlName,A.Accno,isnull(D.CUSTNAME,B.GLNAME) CustName,A.RECSRNO RecSrno,A.AMount,A.TrxType--%><asp:TemplateField HeaderText="RecSrno" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Lbl_VRecSrno" runat="server" Text='<%# Eval("RecSrno") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="AMount" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Lbl_VAMount" runat="server" Text='<%# Eval("AMount") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="TrxType" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Lbl_VTrxType" runat="server" Text='<%# Eval("TrxType") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Delete" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkbtnDelete" runat="server" OnClick="lnkbtnDelete_Click" CommandArgument='<%#Eval("Id")%>' CommandName="select" class="glyphicon glyphicon-trash"></asp:LinkButton></ItemTemplate></asp:TemplateField></Columns><PagerStyle CssClass="pgr" />
                                                                        <SelectedRowStyle BackColor="#66FF99" />
                                                                        <EditRowStyle BackColor="#FFFF99" />
                                                                        <AlternatingRowStyle CssClass="alt" />
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </thead>
                                                    </table>
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

    <div class="row">
        <div class="col-lg-12">
            <div class="table-scrollable">
                <table class="table table-striped table-bordered table-hover">
                    <asp:Label ID="lBL_gnAME" runat="server" Text="Interest Applied Calculation" Style="font-size: medium;"></asp:Label><thead>
                        <tr>
                            <th>
                                <asp:GridView ID="GridCalculation" runat="server" AllowPaging="True"
                                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                    EditRowStyle-BackColor="#FFFF99"
                                    PageIndex="10" PageSize="25"
                                    PagerStyle-CssClass="pgr" Width="100%" RowStyle-HorizontalAlign="Right">
                                    <Columns>
                                        <asp:BoundField DataField="P" HeaderText="Principal (A)" HeaderStyle-BackColor="LightBlue" />
                                        <asp:BoundField DataField="R" HeaderText="Rate of Interest (B)" HeaderStyle-BackColor="LightBlue" />
                                        <asp:BoundField DataField="PR" HeaderText="Penal ROI  (C)" HeaderStyle-BackColor="LightBlue" />
                                        <asp:BoundField DataField="RPR" HeaderText="D=(B-C)" HeaderStyle-BackColor="LightBlue" />
                                        <asp:BoundField DataField="D" HeaderText="Days Diff" HeaderStyle-BackColor="LightBlue" />
                                        <asp:BoundField DataField="M" HeaderText="Month Diff" HeaderStyle-BackColor="LightBlue" />
                                        <asp:BoundField DataField="T" HeaderText="Total ([ (A) * (D) / 100 / (365 or 12) * (D or M) ]) - IntPayable - IntDebit" HeaderStyle-BackColor="LightGreen" />
                                    </Columns>
                                    <PagerStyle CssClass="pgr" />
                                    <SelectedRowStyle BackColor="#66FF99" />
                                    <EditRowStyle BackColor="#FFFF99" />
                                    <AlternatingRowStyle CssClass="alt" />
                                </asp:GridView>

                                <asp:GridView ID="Grid_MatureCalculation" runat="server" AllowPaging="True"
                                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                    EditRowStyle-BackColor="#FFFF99"
                                    PageIndex="10" PageSize="25"
                                    PagerStyle-CssClass="pgr" Width="100%" RowStyle-HorizontalAlign="Right">
                                    <Columns>
                                        <asp:BoundField DataField="MA" HeaderText="Maturity (A)" HeaderStyle-BackColor="LightBlue" />
                                        <asp:BoundField DataField="PP" HeaderText="Principal Payable (B)" HeaderStyle-BackColor="LightBlue" />
                                        <asp:BoundField DataField="IP" HeaderText="Intr Payable (C)" HeaderStyle-BackColor="LightBlue" />
                                        <asp:BoundField DataField="IPI" HeaderText="(B+C)" HeaderStyle-BackColor="LightBlue" />
                                        <asp:BoundField DataField="TOTAL" HeaderText="Total [ (A) - ( B + C ) ]" HeaderStyle-BackColor="LightGreen" />

                                    </Columns>
                                    <PagerStyle CssClass="pgr" />
                                    <SelectedRowStyle BackColor="#66FF99" />
                                    <EditRowStyle BackColor="#FFFF99" />
                                    <AlternatingRowStyle CssClass="alt" />
                                </asp:GridView>

                                <asp:GridView ID="GridSBInt" runat="server" AllowPaging="True"
                                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                    EditRowStyle-BackColor="#FFFF99"
                                    PageIndex="10" PageSize="25"
                                    PagerStyle-CssClass="pgr" Width="100%" RowStyle-HorizontalAlign="Right">
                                    <Columns>
                                        <asp:BoundField DataField="D" HeaderText="Deposit Amt (A)" HeaderStyle-BackColor="LightBlue" />
                                        <asp:BoundField DataField="R" HeaderText="Rate (B)" HeaderStyle-BackColor="LightBlue" />
                                        <asp:BoundField DataField="DD" HeaderText="Days Diff(C)" HeaderStyle-BackColor="LightBlue" />
                                        <asp:BoundField DataField="L" HeaderText="SB Limit" HeaderStyle-BackColor="LightBlue" />
                                        <asp:BoundField DataField="T" HeaderText="Total [ (A * B) / 100 / 365 * (C) ]" HeaderStyle-BackColor="LightGreen" />

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

    <div class="row">
        <div class="col-lg-12">
            <div class="table-scrollable" style="width: 100%; height: 500px; overflow-x: auto; overflow-y: auto">
                <table class="table table-striped table-bordered table-hover">
                    <asp:Label ID="Label1" runat="server" Text="RD/DP Interest Calculation" Style="font-size: medium;"></asp:Label><thead>
                        <tr>
                            <th>
                                <%--id	AsOnDate	Principle	Intr	Rate	PenalRate--%>

                                <asp:GridView ID="Grid_RDCal" runat="server"
                                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                    EditRowStyle-BackColor="#FFFF99"
                                    Width="100%" RowStyle-HorizontalAlign="Right"
                                    ShowFooter="true">
                                    <Columns>
                                        <asp:BoundField DataField="id" HeaderText="Srno" HeaderStyle-BackColor="LightBlue" />
                                        <asp:BoundField DataField="AsOnDate" HeaderText="On Date" HeaderStyle-BackColor="LightBlue" />
                                        <asp:BoundField DataField="ScheduleCr" HeaderText="ScheduleCr" HeaderStyle-BackColor="LightBlue" />
                                        <asp:BoundField DataField="Credit" HeaderText="Credit" HeaderStyle-BackColor="LightBlue" />
                                        <asp:BoundField DataField="SchedulePri" HeaderText="SchedulePri" HeaderStyle-BackColor="LightBlue" />
                                        <asp:BoundField DataField="IntrOnSchedule" HeaderText="IntrOnSchedule" HeaderStyle-BackColor="LightBlue" />
                                        <asp:BoundField DataField="Principle" HeaderText="Principle" HeaderStyle-BackColor="LightBlue" />
                                        <asp:BoundField DataField="OnBalance" HeaderText="OnBalance" HeaderStyle-BackColor="LightBlue" />
                                        <asp:BoundField DataField="Intr" HeaderText="Interest Amt" HeaderStyle-BackColor="LightBlue" />
                                        <asp:BoundField DataField="Rate" HeaderText="Applied Rate" HeaderStyle-BackColor="LightGreen" />
                                        <asp:BoundField DataField="ActRate" HeaderText="Actual Rate" HeaderStyle-BackColor="LightGreen" />
                                        <asp:BoundField DataField="AfterMatRoi" HeaderText="After Mat ROI" HeaderStyle-BackColor="LightGreen" />
                                        <asp:BoundField DataField="PenalRate" HeaderText="Penal Rate" HeaderStyle-BackColor="LightGreen" />


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

    <%--  <div class="row">
        <div class="col-lg-12">
            <div class="table-scrollable" style="width: 100%; height: 500px; overflow-x: auto; overflow-y: auto">
                <table class="table table-striped table-bordered table-hover">
                    <asp:Label ID="Lbl_Sts" runat="server" Text="Account Statement" Style="font-size: medium;"></asp:Label><thead>
                        <tr>
                            <th>
                                <asp:GridView ID="GrdFDLedger" runat="server" AllowPaging="True"
                                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                    EditRowStyle-BackColor="#FFFF99"
                                    OnPageIndexChanging="GrdFDLedger_PageIndexChanging"
                                    PageIndex="10" PageSize="25"
                                    PagerStyle-CssClass="pgr" Width="100%">
                                    <Columns>

                                        <asp:TemplateField HeaderText="EntryDate" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="EDATE" runat="server" Text='<%# Eval("EDATE","{0:dd/MM/yyyy}") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Particulars">
                                            <ItemTemplate>
                                                <asp:Label ID="PARTI" runat="server" Text='<%# Eval("PARTI") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Cheque/Refrence">
                                            <ItemTemplate>
                                                <asp:Label ID="INSTNO" runat="server" Text='<%# Eval("INSTNO") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Credit" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="CREDIT" runat="server" Text='<%# Eval("CREDIT", "{0:0.00}") %>'></asp:Label></ItemTemplate><ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="DEBIT">
                                            <ItemTemplate>
                                                <asp:Label ID="DEBIT" runat="server" Text='<%# Eval("DEBIT", "{0:0.00}") %>'></asp:Label></ItemTemplate><ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="BALANCE">
                                            <ItemTemplate>
                                                <asp:Label ID="BALANCE" runat="server" Text='<%# Eval("BALANCE", "{0:0.00}") %>'></asp:Label></ItemTemplate><ItemStyle CssClass="Th" HorizontalAlign="Right" />
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
    </div>--%>

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
                                            <asp:Label ID="lblEntryDate" runat="server" Text='<%# Eval("EntryDate") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderStyle-Width="50px" HeaderText="SetNo">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSetNo" runat="server" Text='<%# Eval("SetNo") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderStyle-Width="200px" HeaderText="Particular">
                                        <ItemTemplate>
                                            <asp:Label ID="lblParticular" runat="server" Text='<%# Eval("Particular") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderStyle-Width="100px" HeaderText="Credit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPCredit" runat="server" Text='<%# Eval("PCredit") %>'></asp:Label></ItemTemplate><FooterTemplate>
                                            <asp:Label ID="lblPCreditTotal" Text="lblPCreditTotal" runat="server" />
                                        </FooterTemplate>
                                        <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                        <FooterStyle CssClass="Th" HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="100px" HeaderText="Debit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPDebit" runat="server" Text='<%# Eval("PDebit") %>'></asp:Label></ItemTemplate><FooterTemplate>
                                            <asp:Label ID="lblPDebitTotal" Text="lblPCreditTotal" runat="server" />
                                        </FooterTemplate>
                                        <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                        <FooterStyle CssClass="Th" HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="100px" HeaderText="Balance">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPBalance" runat="server" Text='<%# Eval("PBalance") %>'></asp:Label></ItemTemplate><ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="20px" HeaderText="DrCr">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPDrCr" runat="server" Text='<%# Eval("PDrCr") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderStyle-Width="100px" HeaderText="Credit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblICredit" runat="server" Text='<%# Eval("ICredit") %>'></asp:Label></ItemTemplate><FooterTemplate>
                                            <asp:Label ID="lblICreditTotal" Text="lblPCreditTotal" runat="server" />
                                        </FooterTemplate>
                                        <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                        <FooterStyle CssClass="Th" HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="100px" HeaderText="Debit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIDebit" runat="server" Text='<%# Eval("IDebit") %>'></asp:Label></ItemTemplate><FooterTemplate>
                                            <asp:Label ID="lblIDebitTotal" Text="lblPCreditTotal" runat="server" />
                                        </FooterTemplate>
                                        <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                        <FooterStyle CssClass="Th" HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="100px" HeaderText="Balance">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIBalance" runat="server" Text='<%# Eval("IBalance") %>'></asp:Label></ItemTemplate><ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="20px" HeaderText="DrCr">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIDrCr" runat="server" Text='<%# Eval("IDrCr") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderStyle-Width="100px" HeaderText="Total Balance">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalBalance" runat="server" Text='<%# Eval("TotalBalance") %>'></asp:Label></ItemTemplate><ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="20px" HeaderText="DrCr">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPDrCr" runat="server" Text='<%# Eval("PDrCr") %>'></asp:Label></ItemTemplate></asp:TemplateField></Columns><SelectedRowStyle BackColor="#66FF99" />
                                <EditRowStyle BackColor="#FFFF99" />
                                <AlternatingRowStyle CssClass="alt" />
                            </asp:GridView>
                        </th>
                    </tr>
                </thead>
            </table>
        </div>
    </div>

    <div id="VOUCHERVIEW" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button><h4 class="modal-title">Account Details Screen</h4></div><div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="portlet box green" id="Div8">
                                <div class="portlet-title">
                                    <div class="caption">
                                        Voucher View </div></div><div class="portlet-body form">
                                    <div class="form-horizontal">
                                        <div class="form-wizard">
                                            <div class="form-body">
                                                <div class="tab-content">
                                                    <div class="tab-pane active" id="Div9">
                                                        <div class="row" style="margin-bottom: 10px;">
                                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                                <div class="col-lg-12" style="height: 50%">
                                                                    <div class="table-scrollable" style="height: 350px; overflow-x: scroll; overflow-y: scroll; padding-bottom: 10px;">
                                                                        <asp:GridView ID="GrdView" runat="server" AutoGenerateColumns="false" OnRowDataBound="GrdView_RowDataBound">
                                                                            <Columns>

                                                                                <asp:TemplateField HeaderText="VOUCHER NO " Visible="true">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="SETNO" runat="server" Text='<%# Eval("SETNO") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="ON DATE" Visible="true">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="ENTRYDATE" runat="server" Text='<%# Eval("ENTRYDATE") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:BoundField DataField="SUBGLCODE" HeaderText="Product Code" />
                                                                                <asp:BoundField DataField="ACCNO" HeaderText="A/C No" />
                                                                                <asp:BoundField DataField="CUSTNAME" HeaderText="Name" />
                                                                                <asp:BoundField DataField="PARTICULARS" HeaderText="Particulars" />

                                                                                <asp:TemplateField HeaderText="AMOUNT " Visible="true">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="AMOUNT" runat="server" Text='<%# Eval("AMOUNT") %>'></asp:Label></ItemTemplate><ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="TYPE " Visible="true">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="TYPE" runat="server" Text='<%# Eval("TYPE") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="ACTIVITY" Visible="true">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="ACTIVITY" runat="server" Text='<%# Eval("ACTIVITY") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:BoundField DataField="BRCD" HeaderText="Br. Code" />
                                                                                <asp:BoundField DataField="STAGE" HeaderText="Status" />
                                                                                <asp:BoundField DataField="LOGINCODE" HeaderText="User Code" />
                                                                                <asp:BoundField DataField="MID" HeaderText="Maker ID" />
                                                                                <asp:BoundField DataField="CID" HeaderText="Checker ID" />
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
                                                <asp:Label ID="SET_NO" runat="server" Text='<%# Eval("SETNO") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="PRODUCT TYPE" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="AT" runat="server" Text='<%# Eval("AT") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="ACC No" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="ACNO" runat="server" Text='<%# Eval("ACNO") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="CUST NAME" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="Name" runat="server" Text='<%# Eval("CUSTNAME") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="AMOUNT" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="Amount" runat="server" Text='<%# Eval("AMOUNT") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="NARRATION" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="Particulars" runat="server" Text='<%# Eval("PARTICULARS") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="INSTRUMENT NO" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="Instno" runat="server" Text='<%# Eval("INSTRUMENTNO") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="INSTRUMENT DATE" Visible="true">
                                            <ItemTemplate>

                                                <asp:Label ID="InstDate" runat="server" Text='<%# Eval("INSTRUMENTDATE") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="MAKER" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="MAKER" runat="server" Text='<%# Eval("MAKER") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Voucher" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkPrintReceipt" runat="server" CommandName="select" class="glyphicon glyphicon-plus" OnClick="LnkPrintReceipt_Click"></asp:LinkButton></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Dens" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDens" runat="server" OnClick="lnkDens_Click" CommandArgument='<%#Eval("Dens")%>' CommandName="select" class="glyphicon glyphicon-plus"></asp:LinkButton></ItemTemplate></asp:TemplateField></Columns><PagerStyle CssClass="pgr" />
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
        <div id="DivTDSCalc" class="modal fade" role="dialog">
            <div class="modal-dialog modal-lg" style="width: 90%">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-header-title">TDS Calculation</h4></div><div class="modal-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="portlet-body">
                                    <div class="table-scrollable" style="width: 100%; height: auto; max-height: 300px; overflow-x: auto; overflow-y: auto">
                                        <table class="table table-striped table-bordered table-hover" width="100%">
                                            <thead>
                                                <tr>
                                                    <th>
                                                        <asp:GridView ID="grdAccStatement" runat="server" AlternatingRowStyle-CssClass="alt" CssClass="mGrid" Width="100%"
                                                            AutoGenerateColumns="false" EditRowStyle-BackColor="#FFFF99" EmptyDataText="No Records Available">
                                                            <Columns>
                                                                <%--CurrInterest	AllAccInterest	TotalInterestCalc	RateApplied	TDSDeducted	CalculatedDeductedAmt--%>
                                                                <asp:TemplateField HeaderText="CurrInterest(A)" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCurrInterest" runat="server" Text='<%# Eval("CurrInterest") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="AllAccInterest(B)" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAllAccInterest" runat="server" Text='<%# Eval("AllAccInterest") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="TotalInterestCalc (C)=(A+B)">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTotalInterestCalc" runat="server" Text='<%# Eval("TotalInterestCalc") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="RateApplied (D)">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRateApplied" runat="server" Text='<%# Eval("RateApplied") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="TDSDeducted (E)">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTDSDeducted" runat="server" Text='<%# Eval("TDSDeducted") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="CalculatedDeductedAmt [ (A - E) * (D) /100 ]  " Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblMasterT" runat="server" Text='<%# Eval("CalculatedDeductedAmt") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="TDS Ded (Y/N)" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCalculatedDeductedAmt" runat="server" Text='<%# Eval("T") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Ext TDS Calc" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblExtTDSCalc" runat="server" Text='<%# Eval("ExtTDSCalc") %>'></asp:Label></ItemTemplate></asp:TemplateField></Columns><PagerStyle CssClass="pgr" />

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
                                <div class="portlet-body">
                                    <div class="table-scrollable" style="width: 100%; height: auto; max-height: 300px; overflow-x: auto; overflow-y: auto">
                                        <table class="table table-striped table-bordered table-hover" width="100%">
                                            <thead>
                                                <tr>
                                                    <th>
                                                        <asp:GridView ID="GridAllAcc" runat="server" AlternatingRowStyle-CssClass="alt" CssClass="mGrid" Width="100%"
                                                            AutoGenerateColumns="false" EditRowStyle-BackColor="#FFFF99" EmptyDataText="No Records Available">
                                                            <Columns>
                                                                <%--Id,EntryDate,Glcode,Subglcode,Accno,RecSrno,CrAmount,DrAmount,Particulars,Brcd,
                                                                    RateOfTDA,DueDate,DayDiff,ExtTDSAmt--%>
                                                                <asp:TemplateField HeaderText="EntryDate" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblEntryDate" runat="server" Text='<%# Eval("EntryDate") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Subglcode" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSubglcode" runat="server" Text='<%# Eval("Subglcode") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Accno">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAccno" runat="server" Text='<%# Eval("Accno") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="RecSrno">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRecSrno" runat="server" Text='<%# Eval("RecSrno") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="DrAmount" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDrAmount" runat="server" Text='<%# Eval("DrAmount") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="CrAmount">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCrAmount" runat="server" Text='<%# Eval("CrAmount") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Particulars" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblParticulars" runat="server" Text='<%# Eval("Particulars") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Brcd" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblBrcd" runat="server" Text='<%# Eval("Brcd") %>'></asp:Label></ItemTemplate></asp:TemplateField></Columns><PagerStyle CssClass="pgr" />
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
                                <div class="form-actions">
                                    <div class="row">
                                        <div class="col-md-offset-3 col-md-9">
                                            <div class="col-md-offset-3 col-md-9">
                                                <asp:Button ID="btnClose2" Text="Close" runat="server" CssClass="btn btn-success" data-dismiss="modal" />
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
    <asp:HiddenField ID="hdnValue" runat="server" Value="0" />

</asp:Content>

