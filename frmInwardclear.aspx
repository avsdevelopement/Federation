<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" CodeFile="~/frmInwardclear.aspx.cs" AutoEventWireup="true" Inherits="frmInwordclear" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="style/customcss.css" rel="stylesheet" />
    <script type="text/javascript">
        function validate() {
            var message = '';

            var date = document.getElementById('<% =TxtEntrydate.ClientID%>').value;
            if (date == "") {
                //alert("Entry date is not present");
                message = 'Entry date is not present ....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<% =TxtEntrydate.ClientID%>').focus();
                return false;
            }
            var Procode = document.getElementById('<% =TxtProcode.ClientID%>').value;
            if (Procode == "") {
                // alert("Enter product code");
                message = 'Enter product code ....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<% =TxtProcode.ClientID%>').focus();
                return false;
            }
            var AccNo = document.getElementById('<% =TxtAccNo.ClientID%>').value;
            if (AccNo == "") {
                // alert("Enter Account Number");
                message = 'Enter Account Number ....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<% =TxtAccNo.ClientID%>').focus();
                return false;
            }

            var partic = document.getElementById('<% =txtpartic.ClientID%>').value;
            if (partic == "") {
                //// alert("Enter Particular");
                message = 'Enter Particular ....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<% =txtpartic.ClientID%>').focus();
                return false;
            }
            var bankcd = document.getElementById('<% =txtbankcd.ClientID%>').value;
            if (bankcd == "") {
                //alert("Enter Bank code");
                message = 'Enter Bank code ....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<% =txtbankcd.ClientID%>').focus();
                return false;
            }
            var brnchcd = document.getElementById('<% =txtbrnchcd.ClientID%>').value;
            if (brnchcd == "") {
                // alert("Enter Branch code");
                message = 'Enter Branch code ....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<% =txtbrnchcd.ClientID%>').focus();
                return false;
            }
            var instno = document.getElementById('<% =txtinstno.ClientID%>').value;
            if (instno == "") {
                //alert("Enter Instrument number");
                message = 'Enter Instrument number ....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<% =txtinstno.ClientID%>').focus();
                return false;
            }
            var instno = document.getElementById('<% =txtinstno.ClientID%>').value;
            if (instno.length < 6 || instno.length > 6) {
                // alert("Enter 6 digit Instrument number");
                message = 'Enter 6 digit Instrument number ....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<% =txtinstno.ClientID%>').focus();
                return false;
            }
            var instdate = document.getElementById('<% =txtinstdate.ClientID%>').value;
            if (instdate == "") {
                // alert("Enter Instrument Date");
                message = 'Enter Instrument Date....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<% =txtinstdate.ClientID%>').focus();
                document.getElementById('<%=txtinstdate.ClientID%>').value = "";
                return false;
            }

            var instamt = document.getElementById('<% =txtinstamt.ClientID%>').value;
            if (instamt == "") {
                //alert("Enter Instrument amount");
                message = 'Enter Instrument amount....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<% =txtinstamt.ClientID%>').focus();
                return false;
            }

        }

    </script>
    <script type="text/javascript">
        function Validinstno() {
            var NOLEN = document.getElementById('<%=txtinstno.ClientID%>').value;

            if (NOLEN.length > 6) {
                message = 'Enter 6 digit Instrument number ....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<% =txtinstno.ClientID%>').focus();
                return false;
            }

        }
    </script>

    <script type="text/javascript">
        function VisibleReturn() {
            debugger;
            document.getElementById('<%=Btn_Return.ClientID %>').style.display = "inherit";
            document.getElementById('<%=btnSubmit.ClientID %>').disabled = true;
            document.getElementById('<%=BtnPostUnpass.ClientID %>').disabled = true;
            document.getElementById('<%=DdlReason.ClientID %>').style.display = "inherit";




        }
    </script>
    <script type="text/javascript">
        function VisibleReject() {
            debugger;
            document.getElementById('<%=Btn_Return.ClientID %>').style.display = "none";
            document.getElementById('<%=btnSubmit.ClientID %>').style.display = "none";
            document.getElementById('<%=BtnPostUnpass.ClientID %>').style.display = "inherit";
            document.getElementById('<%=DdlReason.ClientID %>').style.display = "none";

        }
    </script>

    <script type="text/javascript">
        function FormatIt(obj) {
            if (obj.value.length == 2) // Day
                obj.value = obj.value + "/";
            if (obj.value.length == 5) // month 
                obj.value = obj.value + "/";

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
    </script>

    <style type="text/css">
        .ModalPopupBG {
            background-color: #666699;
            filter: alpha(opacity=50);
            opacity: 0.7;
            text-align: center;
            font-family: 'Adobe Arabic';
            font-size: xx-large;
        }

        .HellowWorldPopup {
            min-width: 200px;
            min-height: 150px;
            background: white;
            text-align: center;
            font-family: 'Adobe Arabic';
            font-size: medium;
            text-decoration-color: brown;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        Inward Clearing
                    </div>
                </div>
                <div class="portlet-body form">
                    <!--<form action="#" class="form-horizontal" id="submit_form" method="POST">-->
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">

                                <div class="tab-content">
                                    <div id="error">
                                    </div>
                                    <div class="tab-pane active" id="tab1">
                                        <div class="row" style="margin: 7px 0 12px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Select </label>
                                                <div class="col-md-3">
                                                    <asp:Button ID="btnAddNew" runat="server" CssClass="btn default" Text="Add new " OnClick="AddNewBtnClicked" AccessKey="1" />
                                                    <asp:Button ID="btnAddMore" runat="server" CssClass="btn default" Text="Add Multiple" OnClick="btnAddMore_Click" Visible="false" />
                                                    <asp:Button ID="btnDelete" runat="server" CssClass="btn default" Text="Cancel" OnClick="DeleteBtnClicked" Visible="false" />
                                                </div>
                                                <asp:Label ID="lblstatus" runat="server" CssClass="control-label col-md-3" Text="Add New" Style="color: blueviolet;"></asp:Label>
                                            </div>
                                        </div>
                                        <asp:Table ID="TblDiv_MainWindow" runat="server">
                                            <asp:TableRow ID="Tbl_R1" runat="server">
                                                <asp:TableCell ID="Tbl_c1" runat="server" Width="50%" BorderStyle="Solid" BorderWidth="1px">

                                                    <div runat="server" id="Div_SubMainWindow">
                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="row" style="margin: 7px 0 12px 0">
                                                                <div class="col-lg-12">
                                                                    <label class="control-label col-md-3">Entry Type<span class="required">* </span></label>
                                                                    <div class="col-md-6">
                                                                        <asp:RadioButton ID="rdbCredit" runat="server" Text="Credit" GroupName="CD" OnCheckedChanged="rdbCredit_CheckedChanged" AutoPostBack="true" />
                                                                        <asp:RadioButton ID="rdbDebit" runat="server" Text="Debit" GroupName="CD" OnCheckedChanged="rdbDebit_CheckedChanged" AutoPostBack="true" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" style="margin: 7px 0 12px 0">
                                                            <div class="col-lg-12">
                                                                <label class="control-label col-md-3">Clg Session<span class="required">* </span></label>
                                                                <div class="col-md-3">
                                                                    <asp:DropDownList ID="Dll_Session" CssClass="form-control" runat="server">
                                                                        <asp:ListItem Text="--Select Session--" Value="0"></asp:ListItem>
                                                                        <asp:ListItem Text="Morning Session" Value="1" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Text="Evening Session" Value="2"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <label class="control-label col-md-3">A/C Sts<span class="required">* </span></label>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="TxtACCStatus" CssClass="form-control" runat="server" AutoPostBack="true" ReadOnly="True"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-12">
                                                                <label class="control-label col-md-3">Entry Dt<span class="required">* </span></label>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="TxtEntrydate" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                                </div>
                                                                <label class="control-label col-md-3">SetNo<span class="required"></span></label>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="txtsetno" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtsetno_TextChanged" ReadOnly="True"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-12">
                                                                <label class="control-label col-md-3">Prd Code<span class="required">* </span></label>

                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="TxtProcode" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="TxtProcode_TextChanged" onkeypress="javascript:return isNumber (event)" PlaceHolder="Product code"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <asp:TextBox ID="TxtProName" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="TxtProName_TextChanged" PlaceHolder="product Name"></asp:TextBox>
                                                                    <div id="CustList1" style="height: 200px; overflow-y: scroll;"></div>
                                                                    <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="TxtProName"
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
                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-12">
                                                                <label class="control-label col-md-3">A/C No<span class="required">* </span></label>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="TxtAccNo" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="TxtAccNo_TextChanged" onkeypress="javascript:return isNumber (event)" PlaceHolder="Account no."></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <asp:TextBox ID="TxtAccName" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="TxtAccName_TextChanged" PlaceHolder="Account Holder name"></asp:TextBox>
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
                                                                    <asp:LinkButton ID="LnkVerify" Visible="false" runat="server" OnClick="LnkVerify_Click">Verify Signature</asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-12">
                                                                <label class="control-label col-md-3">A/C Type<span class="required">* </span></label>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="txtAccTypeid" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="TxtAccntTypeChanged" Enabled="False" PlaceHolder="Account type"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <asp:TextBox ID="TxtAccTypeName" CssClass="form-control" runat="server" Enabled="False" PlaceHolder="Account type name"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-12">
                                                                <label class="control-label col-md-3">Opr Type<span class="required">* </span></label>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="txtOpTypeId" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="TxtOpTypeIdChanged" Enabled="False" PlaceHolder="Operation type"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <asp:TextBox ID="TxtOpTypeName" CssClass="form-control" runat="server" Enabled="False" PlaceHolder="Operation name"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" style="margin: 7px 0 7px 0" runat="server" visible="true">
                                                            <div class="col-lg-12">
                                                                <label class="control-label col-md-3">Bank Code<span class="required">* </span></label>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="txtbankcd" CssClass="form-control" runat="server" OnTextChanged="txtbankcd_TextChanged" AutoPostBack="true" onkeypress="javascript:return isNumber (event)" PlaceHolder="Bank code"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <asp:TextBox ID="txtbnkdname" CssClass="form-control" runat="server" OnTextChanged="txtbnkdname_TextChanged" AutoPostBack="true" PlaceHolder="Bank name"></asp:TextBox>
                                                                    <div id="CustList3" style="height: 200px; overflow-y: scroll;"></div>
                                                                    <asp:AutoCompleteExtender ID="AutoBank" runat="server" TargetControlID="txtbnkdname"
                                                                        UseContextKey="true"
                                                                        CompletionInterval="1"
                                                                        CompletionSetCount="20"
                                                                        MinimumPrefixLength="1"
                                                                        EnableCaching="true"
                                                                        ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList3"
                                                                        ServiceMethod="GetBankName">
                                                                    </asp:AutoCompleteExtender>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" style="margin: 7px 0 7px 0" runat="server" visible="true">
                                                            <div class="col-lg-12">
                                                                <label class="control-label col-md-3">Brcd<span class="required">* </span></label>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="txtbrnchcd" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtbrnchcd_TextChanged" onkeypress="javascript:return isNumber (event)" PlaceHolder="Branch code"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <asp:TextBox ID="txtbrnchcdname" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtbrnchcdname_TextChanged" PlaceHolder="Branch name"></asp:TextBox>
                                                                    <div id="CustList4" style="height: 200px; overflow-y: scroll;"></div>
                                                                    <asp:AutoCompleteExtender ID="AutoBranch" runat="server" TargetControlID="txtbrnchcdname"
                                                                        UseContextKey="true"
                                                                        CompletionInterval="1"
                                                                        CompletionSetCount="20"
                                                                        MinimumPrefixLength="1"
                                                                        EnableCaching="true"
                                                                        ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList4"
                                                                        ServiceMethod="GetBranchName">
                                                                    </asp:AutoCompleteExtender>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-12">
                                                                <label class="control-label col-md-3">Partic<span class="required">* </span></label>
                                                                <div class="col-md-9">
                                                                    <asp:TextBox ID="txtpartic" CssClass="form-control" runat="server" Text=""></asp:TextBox>
                                                                </div>


                                                            </div>
                                                        </div>
                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-12">
                                                                <label class="control-label col-md-3">InstNo.<span class="required">* </span></label>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="txtinstno" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" PlaceHolder="Instrument no." OnTextChanged="txtinstno_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                </div>
                                                                <label class="control-label col-md-2">InstDt.<span class="required">*</span></label>
                                                                <div class="col-md-4">
                                                                    <asp:TextBox ID="txtinstdate" CssClass="form-control" onkeyup="FormatIt(this)" runat="server" OnTextChanged="txtinstdate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-12">
                                                                <label class="control-label col-md-3">Amount<span class="required">* </span></label>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="txtinstamt" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" OnTextChanged="txtinstamt_TextChanged" AutoPostBack="true" PlaceHolder="Instrument Amount"></asp:TextBox>
                                                                </div>
                                                                <label class="control-label col-md-2">TotBal<span class="required">*</span></label>
                                                                <div class="col-md-4">
                                                                    <asp:TextBox ID="TxtTotalBal" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-12">
                                                                <label class="control-label col-md-3">Clear Bal<span class="required">*</span></label>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="TxtBalance" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                        </div>
                                                        <div class="row" runat="server" visible="false">
                                                            <div class="col-lg-12">
                                                                <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Clg Contra </strong></div>
                                                            </div>
                                                        </div>
                                                        <div class="row" style="margin: 7px 0 7px 0" runat="server" visible="false">
                                                            <div class="col-lg-11">
                                                                <label class="control-label col-md-3">Select Bank : <span class="required">* </span></label>
                                                                <div class="col-md-3">
                                                                    <asp:DropDownList ID="ddlBankName" CssClass="form-control" runat="server"></asp:DropDownList>
                                                                </div>
                                                                <label class="control-label col-md-2">Contra Amoount :<span class="required">*</span></label>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="TxtCLGAmount" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div id="DIV_RETRN" class="row" style="margin: 7px 0 7px 0" runat="server" visible="false">
                                                            <div class="col-lg-12">
                                                                <label class="control-label col-md-3">Return Reason<span class="required">* </span></label>
                                                                <div class="col-md-5">

                                                                    <asp:DropDownList ID="DdlReason" runat="server" CssClass="form-control">
                                                                    </asp:DropDownList>

                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-actions">
                                                            <div class="row">
                                                                <div class="col-md-offset-3 col-md-12">
                                                                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn blue" Text="Submit" OnClick="SaveOwg" OnClientClick="javascript:return validate();" />
                                                                    <asp:Button ID="BtnPostUnpass" runat="server" CssClass="btn blue" Text="Reject" OnClick="BtnPostUnpass_Click" OnClientClick="javascript:return validate();" Enabled="false" />
                                                                    <asp:Button ID="Btn_Return" runat="server" CssClass="btn blue" Text="Return" OnClick="Btn_Return_Click" Enabled="false" />
                                                                    <asp:Button ID="btnUpdate" runat="server" CssClass="btn blue" Text="Delete" OnClick="UpdateOwg" Visible="false" />
                                                                    <asp:Button ID="btmClear" runat="server" CssClass="btn blue" Text="Clear All" OnClick="btmClear_Click" />
                                                                    <asp:Button ID="btnReport" runat="server" CssClass="btn blue" Text="Report" OnClick="btnReport_Click" Visible="true" />
                                                                    <asp:Button ID="Exit" runat="server" CssClass="btn blue" Text="Exit" OnClick="Exit_Click" />
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </asp:TableCell>
                                                <asp:TableCell ID="Tbl_c2" runat="server" Width="50%" BorderStyle="Solid" BorderWidth="1px">
                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-2">Limit<span class="required">* </span></label>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="TxtLLimit" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                            </div>
                                                            <label class="control-label col-md-2">StDt<span class="required"></span></label>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="TxtLSancDt" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtsetno_TextChanged" ReadOnly="True"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-2">IntRt<span class="required">* </span></label>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="TxtLIntRate" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                            </div>
                                                            <label class="control-label col-md-2">DueDt<span class="required"></span></label>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="TxtLDueDt" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtsetno_TextChanged" ReadOnly="True"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-2">LstIntdt<span class="required">* </span></label>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="TxtLLastIntDate" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                            </div>
                                                            <label class="control-label col-md-2">Instamt<span class="required"></span></label>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="TxtLInstAmt" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtsetno_TextChanged" ReadOnly="True"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                    </div>
                                                    <div class="col-lg-12">
                                                        <div class="col-md-12" style="border-bottom: 2px solid #3598dc;"><strong style="color: #3598dc"></strong></div>
                                                    </div>
                                                    <div class="col-lg-12">
                                                        <asp:Table ID="Tbl_Photo" runat="server">
                                                            <asp:TableRow ID="Rw_Ph1" runat="server">
                                                                <asp:TableCell ID="TblCell1" runat="server">
                                                                    <asp:Label ID="Label1" runat="server" Text="Signature" Style="font-weight: bold"></asp:Label>
                                                                    <img id="Img1" runat="server" style="height: 70%; width: 70%; border: 1px solid #000000; padding: 5px" />
                                                                </asp:TableCell>
                                                                <asp:TableCell ID="TblCell2" runat="server">
                                                                    <asp:Label ID="Label2" runat="server" Text="Photo" Style="font-weight: bold"></asp:Label>
                                                                    <img id="Img2" runat="server" style="height: 70%; width: 70%; border: 1px solid #000000; padding: 5px" />
                                                                </asp:TableCell>
                                                            </asp:TableRow>

                                                        </asp:Table>
                                                        <%--<table style="width: 100%">
                                                            <tr>
                                                                <td>
                                                                    <div>
                                                                        <asp:Label ID="Label12" runat="server" Text="Signature" Style="font-weight: bold"></asp:Label>
                                                                    </div>
                                                                   

                                                                    <img id="Img1" runat="server" style="height: 70%; width: 70%; border: 1px solid #000000; padding: 5px" />

                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <div>
                                                                        <asp:Label ID="Label13" runat="server" Text="Photo" Style="font-weight: bold"></asp:Label>
                                                                    </div>

                                                                 
                                                                    <img id="Img2" runat="server" style="height: 70%; width: 70%; border: 1px solid #000000; padding: 5px" />
                                                                </td>
                                                            </tr>
                                                        </table>--%>
                                                    </div>
                                                    <div class="col-lg-12">
                                                        <div class="col-md-12" style="border-bottom: 2px solid #3598dc;"><strong style="color: #3598dc"></strong></div>
                                                    </div>
                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-3">Tot IWC<span class="required">* </span></label>
                                                            <div class="col-md-5">
                                                                <asp:TextBox ID="TxtIwcTotal" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-3">Tot OWC<span class="required">* </span></label>
                                                            <div class="col-md-5">
                                                                <asp:TextBox ID="TxtOwcTotal" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-3">Tot OWC-R<span class="required">* </span></label>
                                                            <div class="col-md-5">
                                                                <asp:TextBox ID="TxtOwcRTotal" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-3">Tot IWC-R<span class="required">* </span></label>
                                                            <div class="col-md-5">
                                                                <asp:TextBox ID="TxtIwcRTotal" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-3">Tot IWCUnp<span class="required">* </span></label>
                                                            <div class="col-md-5">
                                                                <asp:TextBox ID="TxtIWCUTotal" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-3">Tot OWCUnp<span class="required">* </span></label>
                                                            <div class="col-md-5">
                                                                <asp:TextBox ID="TxtOWCUTotal" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
        <!--</form>-->
    </div>

    <%--<div class="testtable">
        <table class="fullwidth">
            <thead>
                <tr>
                    <th scope="col">SET</th>
                    <th scope="col">PROCODE</th>
                </tr>
            </thead>
            <tbody style="height:400px;overflow-y:auto;display:block">
                <tr>
                    <td colspan="2">
                       
                    </td>
                    
                </tr>
                

            </tbody>
        </table>
    </div>--%>

    <div class="row">
        <div class="col-md-12">
            <div class="table-scrollable" style="width: 100%; height: 500px; overflow-x: auto; overflow-y: auto">
                <table class="table table-striped table-bordered table-hover">

                    <thead>
                        <tr>
                            <th>
                                <asp:GridView ID="grdCharged" runat="server" CellPadding="6" CellSpacing="7"
                                    ForeColor="#333333" OnPageIndexChanging="grdCharged_PageIndexChanging"
                                    PageIndex="5" AutoGenerateColumns="False" CssClass="mGrid" BorderWidth="1px"
                                    BorderColor="#333300" Width="100%" OnSelectedIndexChanged="grdCharged_SelectedIndexChanged"
                                    OnRowDataBound="grdCharged_RowDataBound" ShowFooter="true">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="SET" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="SET_NO" runat="server" Text='<%# Eval("SET_NO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SCRL" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="SCRL" runat="server" Text='<%# Eval("SCRL") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="AT" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="AT" runat="server" Text='<%# Eval("AT") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="AC" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="AC" runat="server" Text='<%# Eval("AC") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Name" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="Name" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Inst. No." Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="instNO" runat="server" Text='<%# Eval("instNO") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <div style="padding: 0 0 5px 0">
                                                    <asp:Label ID="Lbl_Numofnst" runat="server" />
                                                </div>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="Date" runat="server" Text='<%# Eval("DATE1","{0:dd/MM/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PARTICULARS" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="PARTICULARS" runat="server" Text='<%# Eval("PARTICULARS") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="STAGE" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="STAGE" runat="server" Text='<%# Eval("STAGE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="STATUS" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="STATUS" runat="server" Text='<%# Eval("STATUS") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="AMOUNT" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="Amount" runat="server" Text='<%# Eval("AMOUNT") %>'></asp:Label>

                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                            <FooterTemplate>
                                                <div style="padding: 0 0 5px 0">
                                                    <asp:Label ID="Lbl_Total" runat="server" />
                                                </div>
                                            </FooterTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Modify" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkModify" runat="server" CommandArgument='<%#Eval("SET_NO")%>' CommandName="select" class="glyphicon glyphicon-pencil" OnClick="LnkModify_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Delete" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Eval("SET_NO")%>' CommandName="select" class="glyphicon glyphicon-trash" OnClick="lnkDelete_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <FooterStyle BackColor="#ccffcc" Font-Bold="True" ForeColor="Black" HorizontalAlign="Right" BorderStyle="None" />
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
                    <%--<asp:Button ID="DBtn_Reject" Text="Reject ?" runat="server" OnClick="DBtn_Reject_Click" />
                    <asp:Button ID="DBtn_Return" Text="Return?" runat="server" OnClick="DBtn_Return_Click" />--%>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

