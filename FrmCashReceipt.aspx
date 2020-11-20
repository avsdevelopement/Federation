<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmCashReceipt.aspx.cs" Inherits="FrmCashReceipt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
        function isvalidate() {

            var entrydate, protype, accno, nar1, nar2, bal, amt;
            entrydate = document.getElementById('<%=TxtEntrydate.ClientID%>').value;
            protype = document.getElementById('<%=TxtProcode.ClientID%>').value;
            accno = document.getElementById('<%=TxtAccNo.ClientID%>').value;
            nar1 = document.getElementById('<%=txtnaration.ClientID%>').value;

            bal = document.getElementById('<%=txtBalance.ClientID%>').value;
            amt = document.getElementById('<%=txtamountt.ClientID%>').value;
            var message = '';

            if (entrydate == "") {
                message = 'Please Enter Entry Date....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtEntrydate.ClientID%>').focus();
                return false;
            }

            if (protype == "") {
                message = 'Please Enter Product Code....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtProcode.ClientID%>').focus();
                return false;
            }

            if (nar1 == "") {
                message = 'Please Enter Naration....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtnaration.ClientID%>').focus();
                return false;
            }

            if (amt == "" || amt == "0") {
                message = 'Please Enter Amount....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtamountt.ClientID%>').focus();
                return false;
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
        function Runbat3() {

            var shell = new ActiveXObject("WScript.Shell");
            var path = '"C:/AVS/ReceiptPRINT.bat"';
            shell.run(path, 1, false);
            Runbat1();
        }
        function Runbat1() {

            var shell = new ActiveXObject("WScript.Shell");
            var path = '"C:/AVS/ReceiptDELETE.bat"';
            shell.run(path, 1, false);
        }
    </script>


      <style>
        /*amruta 19/05/2018*/
        .zoom {
            /*amruta 19/05/2018*/
            transition: transform .3s; /* Animation */
            width: 200px;
            height: 200px;
            margin: 0 auto;
        }

            .zoom:hover {
                /*amruta 19/05/2018*/
                transform: scale(1.5); /* (150% zoom - Note: if the zoom is too large, it will go outside of the viewport) */
            }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
      <%--  <Triggers>
            <asp:PostBackTrigger ControlID="grdCashRct" />
        </Triggers>--%>
        <ContentTemplate>
            <div class="row">

                <div class="col-md-12">
                    <div class="portlet box blue" id="form_wizard_1">
                        <div class="portlet-title">
                            <div class="caption">
                                Cash Receipt
                            </div>
                        </div>
                        <div class="portlet-body form">
                            <!--<form action="#" class="form-horizontal" id="submit_form" method="POST">-->
                            <div class="form-horizontal">
                                <div class="form-wizard">
                                    <div class="form-body">

                                        <div class="tab-content">
                                            <div class="tab-pane active" id="tab1">
                                                <asp:Table ID="TblDiv_MainWindow" runat="server">
                                                    <asp:TableRow ID="Tbl_R1" runat="server">
                                                        <asp:TableCell ID="Tbl_c1" runat="server" Width="70%" BorderStyle="Solid" BorderWidth="1px">

                                                            <div class="row" style="margin: 7px 0 7px 0">
                                                                <div class="col-lg-12">
                                                                    <div class="col-md-5">
                                                                        <asp:RadioButton ID="rbtnNewSet" AccessKey="1" Style="margin: 15px;" OnCheckedChanged="rbtnNewSet_CheckedChanged" runat="server" Text="New Voucher" AutoPostBack="true" GroupName="Type" Checked="true" />
                                                                        <asp:RadioButton ID="rbtnExistSet" AccessKey="2" OnCheckedChanged="rbtnExistSet_CheckedChanged" runat="server" Text="Existing Voucher" AutoPostBack="true" GroupName="Type" />

                                                                    </div>
                                                                    <div id="DivExistSet" runat="server" visible="false">
                                                                        <label class="control-label col-md-2">Set No </label>
                                                                        <div class="col-md-2">
                                                                            <asp:TextBox ID="txtExistSetNo" onkeypress="javascript:return isNumber (event)" AutoPostBack="true" CssClass="form-control" runat="server" OnTextChanged="txtExistSetNo_TextChanged"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="row" style="margin: 7px 0 7px 0">
                                                                <div class="col-lg-12">
                                                                    <label class="control-label col-md-2">Product Type : <span class="required">* </span></label>
                                                                    <div class="col-md-2">
                                                                        <asp:TextBox ID="TxtProcode" PLACEHOLDER="PRODUCT TYPE" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="TxtProcode_TextChanged" TabIndex="1" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-md-4">
                                                                        <asp:TextBox ID="TxtProName" placeholder="PRODUCT NAME" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="TxtProName_TextChanged" TabIndex="2"></asp:TextBox>
                                                                        <div id="CustList" style="height: 200px; overflow-y: scroll;"></div>
                                                                        <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="TxtProName"
                                                                            UseContextKey="true"
                                                                            CompletionInterval="1"
                                                                            CompletionSetCount="20"
                                                                            MinimumPrefixLength="1"
                                                                            EnableCaching="true"
                                                                            ServicePath="~/WebServices/Contact.asmx"
                                                                            ServiceMethod="getglname" CompletionListElementID="CustList">
                                                                        </asp:AutoCompleteExtender>
                                                                    </div>
                                                                    <label class="control-label col-md-2">Entry Date : <span class="required">* </span></label>
                                                                    <div class="col-md-2">
                                                                        <asp:TextBox ID="TxtEntrydate" CssClass="form-control" runat="server"></asp:TextBox>
                                                                        <asp:TextBoxWatermarkExtender ID="TxtFDT_WatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtEntrydate">
                                                                        </asp:TextBoxWatermarkExtender>
                                                                        <asp:CalendarExtender ID="TxtFDT_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtEntrydate">
                                                                        </asp:CalendarExtender>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="row" style="margin: 7px 0 7px 0">
                                                                <div class="col-lg-12">
                                                                    <label class="control-label col-md-2">A/C Number:<span class="required">* </span></label>
                                                                    <div class="col-md-2">
                                                                        <asp:TextBox ID="TxtAccNo" placeholder="ACCOUNT NO" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="TxtAccNo_TextChanged" TabIndex="3" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-md-4">
                                                                        <asp:TextBox ID="TxtAccName" placeholder="ACCOUNT NAME" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="TxtAccName_TextChanged" TabIndex="4"></asp:TextBox>
                                                                        <div id="CustList2" style="height: 200px; overflow-y: scroll;"></div>
                                                                        <asp:AutoCompleteExtender ID="AutoAccname" runat="server" TargetControlID="TxtAccName"
                                                                            UseContextKey="true"
                                                                            CompletionInterval="1"
                                                                            CompletionSetCount="20"
                                                                            MinimumPrefixLength="1"
                                                                            EnableCaching="true"
                                                                            ServicePath="~/WebServices/Contact.asmx"
                                                                            ServiceMethod="GetAccName" CompletionListElementID="CustList2">
                                                                        </asp:AutoCompleteExtender>
                                                                    </div>
                                                                    <label class="control-label col-md-2">Customer : <span class="required"></span></label>
                                                                    <div class="col-md-2">
                                                                        <asp:TextBox ID="Txtcustno" CssClass="form-control" PlaceHolder="Cust No" runat="server" Enabled="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row" style="margin: 7px 0 7px 0">
                                                                <div class="col-lg-12">
                                                                    <label class="control-label col-md-2">Receipt Srno<span class="required"></span></label>
                                                                    <div class="col-md-2">
                                                                        <asp:TextBox ID="TxtRecNo" CssClass="form-control" onkeypress="javascript:return isNumber (event)" PlaceHolder="Receipt No" runat="server" AutoPostBack="true" OnTextChanged="TxtRecNo_TextChanged"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="row" style="margin: 7px 0 7px 0">

                                                                <div class="col-lg-12">
                                                                    <label class="control-label col-md-2">A/C Type </label>
                                                                    <div class="col-md-3">
                                                                        <asp:TextBox ID="txtAccTypeName" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                                    </div>
                                                                    <label class="control-label col-md-2" runat="server" visible="false" id="lbjoint">Joint Name </label>
                                                                    <div class="col-md-4">
                                                                        <asp:TextBox ID="TxtJointName" CssClass="form-control" Visible="false" runat="server" Enabled="false"></asp:TextBox>
                                                                    </div>

                                                                </div>

                                                            </div>
                                                            <div class="row" style="margin: 7px 0 7px 0">
                                                                <div class="col-lg-12">
                                                                    <label class="control-label col-md-2">Pan No. </label>
                                                                    <div class="col-md-3">
                                                                        <asp:TextBox ID="txtPan" placeholder="PAN Card" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                                                    </div>
                                                                    <label class="control-label col-md-2">Aadhar No.</label>
                                                                    <div class="col-md-3">
                                                                        <asp:TextBox ID="TxtAadharNo" placeholder="Aadhar" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row" style="margin: 7px 0 7px 0">

                                                                <div class="col-lg-12">
                                                                    <label class="control-label col-md-2">Mobile 1 </label>
                                                                    <div class="col-md-3">
                                                                        <asp:TextBox ID="TxtMobile1" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                                    </div>
                                                                    <label class="control-label col-md-2">Mobile 2 </label>
                                                                    <div class="col-md-3">
                                                                        <asp:TextBox ID="TxtMobile2" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                                    </div>

                                                                </div>

                                                            </div>

                                                            <div class="row" style="margin: 7px 0 7px 0">
                                                                <div class="col-lg-12">
                                                                    <label class="control-label col-md-2">Spl Inst. </label>
                                                                    <div class="col-md-5">
                                                                        <asp:TextBox ID="TxtSplInst" CssClass="form-control" runat="server" Enabled="false" TextMode="MultiLine"></asp:TextBox>
                                                                    </div>

                                                                </div>
                                                            </div>
                                                            <div class="row" style="margin: 7px 0 7px 0">
                                                                <div class="col-lg-12">

                                                                    <div class="col-md-7">
                                                                    </div>

                                                                </div>
                                                            </div>

                                                            <div class="row" style="margin: 7px 0 7px 0">
                                                                <div class="col-lg-12">
                                                                    <label class="control-label col-md-2">Narration : <span class="required">* </span></label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox ID="txtnaration" CssClass="form-control" runat="server" Text="By Cash" Enabled="True" TabIndex="5"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="row" style="margin: 7px 0 7px 0">
                                                                <div class="col-lg-12">

                                                                    <label class="control-label col-md-2">Clear Balance : <span class="required">* </span></label>
                                                                    <div class="col-md-3">
                                                                        <asp:TextBox ID="txtBalance" placeholder="OLD BALANCE" CssClass="form-control" runat="server" TabIndex="-1" ReadOnly="true"></asp:TextBox>
                                                                    </div>

                                                                    <label class="control-label col-md-2">Amount : <span class="required">* </span></label>
                                                                    <div class="col-md-3">
                                                                        <asp:TextBox ID="txtamountt" placeholder="CREDIT AMOUNT" CssClass="form-control" runat="server" TabIndex="6" onkeypress="javascript:return isNumber (event)" AutoPostBack="true" OnTextChanged="txtamountt_TextChanged"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row" style="margin: 7px 0 7px 0">
                                                                <div class="col-lg-12">

                                                                    <label class="control-label col-md-2">Total Balance : <span class="required">* </span></label>
                                                                    <div class="col-md-3">
                                                                        <asp:TextBox ID="TxtNewBalance" placeholder="BALANCE" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="Tbl_c2" runat="server" Width="30%" BorderStyle="Solid" BorderWidth="1px">
                                                            <div class="row" style="margin: 7px 0 7px 0">
                                                                <div class="col-lg-12">
                                                                    <label class="control-label col-md-5">Start Date : <span class="required">* </span></label>
                                                                    <div class="col-md-5">
                                                                        <asp:TextBox ID="TxtStrtDt" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>

                                                                    </div>

                                                                </div>
                                                            </div>
                                                            <div class="col-lg-12">
                                                                <div class="col-md-12" style="border-bottom: 2px solid #3598dc;"><strong style="color: #3598dc"></strong></div>
                                                            </div>







                                                              <div class="col-lg-12">
                                                        <asp:Table ID="Tbl_Photo" runat="server">
                                                            <asp:TableRow ID="Rw_Ph1" runat="server">
                                                                <asp:TableCell ID="TblCell1" runat="server">
                                                                    <asp:Label ID="Label1" runat="server" Text="Signature" Style="font-weight: bold"></asp:Label>
                                                                    <div class="zoom" style="height: 100%; width: 100%">
                                                                        <img id="Img1" runat="server" style="height: 50%; width: 100%; border: 1px solid #000000; padding: 5px" />
                                                                    </div>
                                                                </asp:TableCell>
                                                                <asp:TableCell ID="TblCell2" runat="server">
                                                                    <asp:Label ID="Label2" runat="server" Text="Photo" Style="font-weight: bold"></asp:Label>
                                                                    <div class="zoom" style="height: 100%; width: 100%">
                                                                        <img id="Img2" runat="server" style="height: 50%; width: 100%; border: 1px solid #000000; padding: 5px" />
                                                                    </div>
                                                                </asp:TableCell>
                                                            </asp:TableRow>

                                                        </asp:Table>

                                                    </div>


                                                           <%-- <div class="col-lg-12">
                                                                <asp:Table ID="Tbl_Photo" runat="server">
                                                                    <asp:TableRow ID="Rw_Ph1" runat="server">
                                                                        <asp:TableCell ID="TblCell1" runat="server">
                                                                            <asp:Label ID="Label1" runat="server" Text="Signature" Style="font-weight: bold"></asp:Label>
                                                                            <div class="zoom" style="height: 100%; width: 100%">
                                                                            <img id="Img1" runat="server" style="height: 50%; width: 100%; border: 1px solid #000000; padding: 5px" />
                                                                                 </div>
                                                                        </asp:TableCell>
                                                                        <asp:TableCell ID="TblCell2" runat="server">
                                                                            <asp:Label ID="Label2" runat="server" Text="Photo" Style="font-weight: bold"></asp:Label>
                                                                              <div class="zoom" style="height: 100%; width: 100%">
                                                                            <img id="Img2" runat="server" style="height: 50%; width: 100%; border: 1px solid #000000; padding: 5px" />
                                                                                    </div>
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
                                                           <%-- </div>--%>
                                                            <div></div>
                                                            <div class="col-lg-12">
                                                                <div class="col-md-12" style="border-bottom: 2px solid #3598dc;"><strong style="color: #3598dc"></strong></div>
                                                            </div>
                                                            <div class="row" style="margin: 7px 0 7px 0">
                                                                <div class="col-lg-12">
                                                                    <label class="control-label col-md-5">Total CR<span class="required">* </span></label>
                                                                    <div class="col-md-7">
                                                                        <asp:TextBox ID="TxtTotCR" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                                    </div>

                                                                </div>
                                                            </div>
                                                            <div class="row" style="margin: 7px 0 7px 0">
                                                                <div class="col-lg-12">
                                                                    <label class="control-label col-md-5">Total CP<span class="required">* </span></label>
                                                                    <div class="col-md-7">
                                                                        <asp:TextBox ID="TxtTotalCP" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row" style="margin: 7px 0 7px 0">
                                                                <div class="col-lg-12">
                                                                    <label class="control-label col-md-5">Tot UnCR<span class="required">* </span></label>
                                                                    <div class="col-md-7">
                                                                        <asp:TextBox ID="TxtTotalUnCr" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row" style="margin: 7px 0 7px 0">
                                                                <div class="col-lg-12">
                                                                    <label class="control-label col-md-5">Tot UnCP<span class="required">* </span></label>
                                                                    <div class="col-md-7">
                                                                        <asp:TextBox ID="TxtTotalUnCp" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                </asp:Table>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-actions">
                                        <div class="row">
                                            <div class="col-md-offset-3 col-md-12">
                                                <asp:Button ID="BtnMobUpld" runat="server" CssClass="btn blue" Text="Mobile Update" OnClick="BtnMobUpld_Click" />
                                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn blue" Text="Submit" OnClick="btnSubmit_Click" OnClientClick="Javascript:return isvalidate();" TabIndex="7" />
                                                <asp:Button ID="btnprint" runat="server" CssClass="btn blue" Text="Print" OnClick="btnprint_Click" Visible="false" />
                                                <asp:Button ID="btnView" runat="server" CssClass="btn blue" Text="ViewStatement" OnClick="btnView_Click" />
                                                <asp:Button ID="Btn_KycUpdate" runat="server" CssClass="btn blue" Text="KYC Update" OnClick="Btn_KycUpdate_Click" />

                                                <asp:Button ID="btnClear" runat="server" CssClass="btn blue" Text="ClearAll" OnClick="btnClear_Click" TabIndex="8" />
                                                <asp:Button ID="Exit" runat="server" CssClass="btn blue" Text="Exit" OnClick="Exit_Click" TabIndex="9" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-lg-12">
                    <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #0A23F9">Customer Account Details  : </strong></div>
                </div>

                <div id="DivGrd1" runat="server" class="col-md-12">
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

                <div class="col-lg-12">
                    <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #0A23F9">Transaction Details  : </strong></div>
                </div>

            </div>

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

            <div class="modal fade bs-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" style="margin-left: 1%; width: 98%">
                <div class="modal-dialog modal-lg" role="document" style="width: 96%">
                    <div class="modal-content" style="border: 5px solid #4dbfc0;">
                        <div class="inner_top">
                            <div class="panel panel-default">
                                <div class="panel-heading">Account Statement</div>
                                <div class="panel-body">
                                    <div class="col-sm-12">
                                        <div class="col-lg-12">

                                            <div class="table-scrollable" style="width: 100%; height: auto; max-height: 300px; overflow-x: auto; overflow-y: auto">
                                                <table class="table table-striped table-bordered table-hover" width="100%">
                                                    <thead>
                                                        <tr>
                                                            <th>
                                                                <asp:GridView ID="grdAccStatement" runat="server" AlternatingRowStyle-CssClass="alt" CssClass="mGrid" Width="100%"
                                                                    AutoGenerateColumns="false" EditRowStyle-BackColor="#FFFF99" DataKeyNames="id" EmptyDataText="No Records Available">
                                                                    <Columns>

                                                                        <asp:TemplateField HeaderText="EntryDate" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblEDate" runat="server" Text='<%# Eval("EDATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="SetNo" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSetNo" runat="server" Text='<%# Eval("SetNo") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Particulars1">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblParticular" runat="server" Text='<%# Eval("PARTI") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Particulars2">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblparticular2" runat="server" Text='<%# Eval("PARTI1") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Cheque/Refrence">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblInstNo" runat="server" Text='<%# Eval("INSTNO") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Credit" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblCredit" runat="server" Text='<%# Eval("CREDIT") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="DEBIT">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDebit" runat="server" Text='<%# Eval("DEBIT") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="BALANCE">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblBalance" runat="server" Text='<%# Eval("BALANCE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Dr/Cr">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDrCrIndicator" runat="server" Text='<%# Eval("DrCr") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass="Th" HorizontalAlign="Left" />
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
                                                                            <div class="table-scrollable" style="height: auto; overflow-y: scroll; padding-bottom: 10px;">
                                                                                <asp:GridView ID="GrdView" runat="server" AutoGenerateColumns="false" OnRowDataBound="GrdView_RowDataBound"
                                                                                    OnSelectedIndexChanged="GrdView_SelectedIndexChanged">
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
                                                                                        <asp:BoundField DataField="SUBGLCODE " HeaderText="Product Code" />
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

            <div id="CNTCT" class="modal fade" role="dialog">
                <div class="modal-dialog modal-lg" style="width: 50%">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title" style="text-align: center; font: 100; font-family: Verdana; font-size: larger; font-style: italic">Contact Add Screen</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">

                                <div class="col-md-12">

                                    <div class="portlet box blue">
                                        <div class="portlet-title">
                                            <div class="caption">
                                                Contact Details
                                            </div>
                                        </div>
                                        <div class="portlet-body form">
                                            <!--<form action="#" class="form-horizontal" id="submit_form" method="POST">-->
                                            <div class="form-horizontal">
                                                <div class="form-wizard">
                                                    <div class="form-body">
                                                        <table>
                                                            <tr>
                                                                <td style="width: 80%">
                                                                    <div class="tab-content">
                                                                        <div id="error">
                                                                        </div>
                                                                        <div class="tab-pane active" id="Div3">
                                                                            <asp:Table ID="Table1" runat="server">
                                                                                <asp:TableRow ID="TableRow1" runat="server" Style="width: 300px">
                                                                                    <asp:TableCell ID="TableCell1" runat="server" Style="width: 2000px">

                                                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                                                            <div class="col-lg-12">
                                                                                                <label class="control-label col-md-2">Customer No :</label>

                                                                                                <div class="col-md-4">
                                                                                                    <asp:TextBox ID="TxtCustno1" placeholder="Enter Customer No" CssClass="form-control" onkeypress="javascript:return isNumber (event)" runat="server" Enabled="false"></asp:TextBox>

                                                                                                </div>

                                                                                                <label class="control-label col-md-2">Brcd : </label>
                                                                                                <div class="col-md-4">
                                                                                                    <asp:TextBox ID="TxtBrcd1" placeholder="Enter Brcd" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" Enabled="false"></asp:TextBox>
                                                                                                </div>

                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                                                            <div class="col-lg-12">
                                                                                                <label class="control-label col-md-2">Mobile No.1 :</label>

                                                                                                <div class="col-md-4">
                                                                                                    <asp:TextBox ID="TxtMob1" placeholder="Enter Mobile No" CssClass="form-control" onkeypress="javascript:return isNumber (event)" runat="server" MaxLength="10"></asp:TextBox>

                                                                                                </div>

                                                                                                <label class="control-label col-md-2">Mobile No.2 : </label>
                                                                                                <div class="col-md-4">
                                                                                                    <asp:TextBox ID="TxtMob2" placeholder="Enter Mobile No" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" MaxLength="10"></asp:TextBox>
                                                                                                </div>

                                                                                            </div>
                                                                                        </div>

                                                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                                                            <div class="col-lg-12">
                                                                                                <label class="control-label" style="color: red">SMS to be send on Mobile No.1</label>

                                                                                            </div>
                                                                                        </div>
                                                                                    </asp:TableCell>


                                                                                </asp:TableRow>
                                                                            </asp:Table>
                                                                        </div>

                                                                    </div>
                                                                </td>

                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                                <div class="form-actions">
                                                    <div class="row">
                                                        <div class="col-md-offset-3 col-md-9">
                                                            <asp:Button ID="BtnModlUpdate" OnClick="BtnModlUpdate_Click" runat="server" Text="Submit" CssClass="btn btn-success" />
                                                            <asp:Button ID="BtnModal_CloseCP" Text="Close" runat="server" CssClass="btn btn-success" data-dismiss="modal" />
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                        <!--</form>-->
                                    </div>


                                </div>

                            </div>

                        </div>

                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

