<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmTDA.aspx.cs" Inherits="FrmTDA" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
        function validate() {

            var date = document.getElementById('<% =TxtProcode.ClientID%>').value;
            var message = '';
            if (date == "") {
                //alert("Product Code is not present");
                message = 'Please Enter Product Code....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<% =TxtProcode.ClientID%>').focus();
                return false;
            }

            var AccNo = document.getElementById('<% =txtAccNo.ClientID%>').value;
            if (AccNo == "") {
                //alert("Account No is not present");
                message = 'Please Enter Account No....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<% =txtAccNo.ClientID%>').focus();
                return false;
            }

            var custn = document.getElementById('<% =Txtcustno.ClientID%>').value;
            if (custn == "") {
                //alert("Customer No is not present");
                message = 'Please Enter Customer No....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<% =Txtcustno.ClientID%>').focus();
                return false;
            }

            var AccType = document.getElementById('<% =ddlAccType.ClientID%>').value;
            if (AccType == "0") {
                //alert("Account type is not selected");
                message = 'Please Enter Account type....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<% =ddlAccType.ClientID%>').focus();
                return false;
            }

            var OpType = document.getElementById('<% =ddlOpType.ClientID%>').value;
            if (OpType == "0") {
                //alert("Member type is not selected");
                message = 'Please Enter Member type....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<% =ddlOpType.ClientID%>').focus();
                return false;
            }

            var DepoAmt = document.getElementById('<% =TxtDepoAmt.ClientID%>').value;
            if (DepoAmt == "") {
                //alert("Deposite amount is not present");
                message = 'Please Enter Deposite amount....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<% =TxtDepoAmt.ClientID%>').focus();
                return false;
            }

            if (DepoAmt == 0) {
                //alert("Deposite amount is not present");
                message = 'Please Enter Valid Deposite amount....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<% =TxtDepoAmt.ClientID%>').focus();
                return false;
            }

            var duration = document.getElementById('<% =ddlduration.ClientID%>').value;
            if (duration == "0") {
                // alert("Period is not selected");
                message = 'Please Enter Duration....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<% =ddlduration.ClientID%>').focus();
                return false;
            }

            var Periode = document.getElementById('<% =TxtPeriod.ClientID%>').value;
            if (Periode == "") {
                //alert("Period is not present");
                message = 'Please Enter Period....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<% =TxtPeriod.ClientID%>').focus();
                return false;
            }

            var Rate = document.getElementById('<% =TxtRate.ClientID%>').value;
            if (Rate == "") {
                //alert("Rate is not present");
                message = 'Please Enter Rate....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<% =TxtRate.ClientID%>').focus();
                return false;
            }

            var Intrest = document.getElementById('<% =TxtIntrest.ClientID%>').value;
            if (Intrest == "") {
                //alert("Interest is not present");
                message = 'Please Enter Interest....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<% =TxtIntrest.ClientID%>').focus();
                return false;
            }

            var Maturity = document.getElementById('<% =TxtMaturity.ClientID%>').value;
            if (Maturity == "") {
                // alert("Maturity amount is not present");
                message = 'Please Enter Maturity amount....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<% =TxtMaturity.ClientID%>').focus();
                return false;
            }

            var DueDate = document.getElementById('<% =DtDueDate.ClientID%>').value;
            if (DueDate == "") {
                //alert("Due date is not present");
                message = 'Please Enter Due date....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<% =DtDueDate.ClientID%>').focus();
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
                alert("Please Enter valid Date");
        }

        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue">
                <div class="portlet-title">
                    <div class="caption">
                        <asp:Literal ID="LtrlHeading" runat="server"></asp:Literal>
                    </div>
                    <div class="tools">
                        <a href="javascript:;" class="collapse" data-original-title="" title=""></a>
                        <a href="#portlet-config" data-toggle="modal" class="config" data-original-title="" title=""></a>
                    </div>
                </div>


                <div class="portlet-body">

                    <%-- Radio Buttons --%>
                    <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                        <div runat="server" id="Main" visible="true">
                            <div class="row" style="margin: 7px 0 12px 0">
                                <div class="col-lg-12">
                                    <label class="control-label col-md-3">Select </label>
                                    <div class="col-md-1">
                                        <asp:Button ID="btnAddNew" runat="server" CssClass="btn default" Text="Add new " OnClick="btnAddNew_Click" AccessKey="1" />
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Button ID="BtnExist" runat="server" CssClass="btn default" Text="Existing" OnClick="BtnExist_Click" AccessKey="1" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div runat="server" id="Search" visible="false">
                            <div class="row" style="margin: 7px 0 12px 0">
                                <div class="col-md-2"></div>
                                <div class="col-lg-12">
                                    <label class="control-label col-md-2">Product Code</label>
                                    <div class="col-md-1">
                                        <asp:TextBox ID="TxtSPrdCode" CssClass="form-control" placeholder="Product Code" onkeypress="javascript:return isNumber (event)" AutoPostBack="true" OnTextChanged="TxtSPrdCode_TextChanged" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="TXtSPrdname" CssClass="form-control" placeholder="Product Name" runat="server"></asp:TextBox>
                                    </div>
                                    <label class="control-label col-md-2">Acc No</label>
                                    <div class="col-md-1">
                                        <asp:TextBox ID="TxtSAccno" CssClass="form-control" placeholder="Acc No" onkeypress="javascript:return isNumber (event)" AutoPostBack="true" OnTextChanged="TxtSAccno_TextChanged" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="TxtSaccName" CssClass="form-control" placeholder="Acc Holder Name" runat="server"></asp:TextBox>
                                    </div>

                                </div>




                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: blue"></strong></div>
                        </div>
                    </div>
                    <%-- Main Area fixed --%>
                    <div runat="server" id="DIV_Term" visible="false">
                        <div class="row" style="margin: 7px 0 7px 0; margin-top: 20px;">
                            <div class="col-lg-12">
                                <label class="control-label col-md-1" style="width: 165px">Product Code : <span class="required">* </span></label>
                                <div class="col-md-1">
                                    <asp:TextBox ID="TxtProcode" CssClass="form-control" onkeypress="javascript:return isNumber (event)" runat="server" AutoPostBack="true" OnTextChanged="TxtProcode_TextChanged"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <asp:TextBox ID="TxtProName" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txttynam_TextChanged"></asp:TextBox>
                                    <div id="CustList" style="height: 200px; overflow-y: scroll;"></div>
                                    <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="TxtProName"
                                        UseContextKey="true"
                                        CompletionInterval="1"
                                        CompletionSetCount="20"
                                        MinimumPrefixLength="1"
                                        EnableCaching="true"
                                        ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList"
                                        ServiceMethod="GetGlName">
                                    </asp:AutoCompleteExtender>
                                </div>
                                <label class="control-label col-md-1" style="width: 160px">Account No : <span class="required">* </span></label>
                                <div class="col-md-1">
                                    <asp:TextBox ID="txtAccNo" CssClass="form-control" onkeypress="javascript:return isNumber (event)" PlaceHolder="Account No /  Receipt No" runat="server" AutoPostBack="true" OnTextChanged="txtAccNo_TextChanged"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="TxtAccName" CssClass="form-control" Width="270px" Placeholder="Acc Name" runat="server" AutoPostBack="true" OnTextChanged="TxtAccName_TextChanged"></asp:TextBox>
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
                            </div>
                        </div>
                        <%--Div 1--%>
                        <div id="Depositdiv" runat="server">
                            <div class="row" style="margin: 7px 0 7px 0">
                                <div class="col-lg-12">
                                    <label class="control-label col-md-1" style="width: 165px">Account Type : <span class="required">* </span></label>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="ddlAccType" runat="server" CssClass="form-control" Enabled="False">
                                        </asp:DropDownList>
                                    </div>
                                    <label class="control-label col-md-1" style="width: 160px">Member Type: <span class="required">* </span></label>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="ddlOpType" runat="server" CssClass="form-control" Enabled="False">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="margin: 7px 0 7px 0">
                                <div class="col-lg-12">
                                    <label class="control-label col-md-1" style="width: 165px">As of Date:<span class="required">* </span></label>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="dtDeposDate" onkeyup="FormatIt(this)" CssClass="form-control" PlaceHolder="dd/mm/yyyy" runat="server"></asp:TextBox>
                                        <asp:TextBoxWatermarkExtender ID="TxtDate_WatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="dtDeposDate">
                                        </asp:TextBoxWatermarkExtender>
                                        <asp:CalendarExtender ID="TxtDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="dtDeposDate">
                                        </asp:CalendarExtender>
                                    </div>
                                    <label class="control-label col-md-1" style="width: 160px">Interest Payout: <span class="required">* </span></label>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="ddlIntrestPay" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="row" style="margin: 7px 0 7px 0">
                                <div class="col-lg-12">
                                    <label class="control-label col-md-1" style="width: 165px">Deposit Amount : <span class="required">* </span></label>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="TxtDepoAmt" CssClass="form-control" PlaceHolder="Deposit Amount" runat="server" onkeypress="javascript:return isNumber (event)" AutoPostBack="true" OnTextChanged="TxtDepoAmt_TextChanged"></asp:TextBox>
                                    </div>
                                    <label class="control-label col-md-1" style="width: 160px">Period : <span class="required">* </span></label>
                                    <div class="col-md-2" style="margin-right: -24px;">
                                        <asp:DropDownList ID="ddlduration" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="M">Months</asp:ListItem>
                                            <asp:ListItem Value="D">Days</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="TxtPeriod" CssClass="form-control" PlaceHolder="Period" runat="server" AutoPostBack="true" OnTextChanged="TxtPeriod_TextChanged" Style="width: 77px;"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="row" style="margin: 7px 0 7px 0">
                                <div class="col-lg-12">
                                    <label class="control-label col-md-1" style="width: 165px">Rate :</label>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="TxtRate" CssClass="form-control" PlaceHolder="Rate" runat="server" Enabled="false" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                    </div>
                                    <label class="control-label col-md-1" style="width: 160px">Interest Amount :</label>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="TxtIntrest" CssClass="form-control" PlaceHolder="Interest Amount" runat="server" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="row" style="margin: 7px 0 7px 0">
                                <div class="col-lg-12">
                                    <label class="control-label col-md-1" style="width: 165px">Maturity Amount : </label>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="TxtMaturity" CssClass="form-control" PlaceHolder="Maturity Amount" runat="server" Enabled="false"></asp:TextBox>
                                    </div>
                                    <label class="control-label col-md-1" style="width: 160px">Due Date :</label>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="DtDueDate" CssClass="form-control" PlaceHolder="dd/mm/yyyy" runat="server" Enabled="false"></asp:TextBox>
                                        <asp:TextBoxWatermarkExtender ID="TxtFDT_WatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="DtDueDate">
                                        </asp:TextBoxWatermarkExtender>
                                        <asp:CalendarExtender ID="TxtFDT_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="DtDueDate">
                                        </asp:CalendarExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="margin: 7px 0 7px 0">
                                <div class="col-lg-12">
                                    <label class="control-label col-md-1" style="width: 165px">Receipt No</label>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="TxtReceiptNo" CssClass="form-control" PlaceHolder="Receipt No" runat="server" Enabled="true"></asp:TextBox>
                                    </div>
                                    <label class="control-label col-md-1" style="width: 165px">Cust No</label>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="Txtcustno" CssClass="form-control" PlaceHolder="Receipt No" runat="server" Enabled="true"></asp:TextBox>
                                    </div>

                                </div>
                            </div>

                            <div class="row" style="margin: 7px 0 7px 0">
                                <div class="col-lg-12">
                                    <%--<label class="control-label col-md-1" style="width: 165px">Amount Trx Type</label>--%>
                                    <div class="col-md-3">
                                        <h4><b>Amount Trx Type</b></h4>
                                    </div>
                                    
                                </div>
                            </div>
                            <div class="row" style="margin: 7px 0 7px 0">
                                <div class="col-lg-12">
                                    <div class="col-md-1">
                                     <asp:RadioButton ID="Rdb_Trf" Checked="true" runat="server" Text="Transfer" Style="font-style: italic;" OnCheckedChanged="Rdb_Trf_CheckedChanged" AutoPostBack="true" GroupName="TrxType" ToolTip="For Transfer Amount" />
                                    </div>
                                    <div class="col-md-1">
                                     <asp:RadioButton ID="Rdb_CR" runat="server" Text="By Cash" Style="font-style: italic" OnCheckedChanged="Rdb_CR_CheckedChanged" AutoPostBack="true" GroupName="TrxType" ToolTip="For Cash Receipt"/>
                                    </div>
                                </div>
                            </div>
                        </div>


                        <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-12">
                                <div class="col-md-3">
                                    <h4><b>Int. Transfer Account</b></h4>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="margin: 7px 0 7px 0; margin-top: 20px;">
                            <div class="col-lg-12">
                                <label class="control-label col-md-1" style="width: 165px">Product Code : <span class="required">* </span></label>
                                <div class="col-md-1">
                                    <asp:TextBox ID="TxtProcode4" CssClass="form-control" onkeypress="javascript:return isNumber (event)" runat="server" AutoPostBack="true" OnTextChanged="TxtProcode4_TextChanged"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <asp:TextBox ID="TxtProName4" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="TxtProName4_TextChanged"></asp:TextBox>
                                    <div id="CustList3" style="height: 200px; overflow-y: scroll;"></div>
                                    <asp:AutoCompleteExtender ID="Autoprd4" runat="server" TargetControlID="TxtProName"
                                        UseContextKey="true"
                                        CompletionInterval="1"
                                        CompletionSetCount="20"
                                        MinimumPrefixLength="1"
                                        EnableCaching="true"
                                        ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList3"
                                        ServiceMethod="GetGlName">
                                    </asp:AutoCompleteExtender>
                                </div>
                                <label class="control-label col-md-1" style="width: 160px">Account No : <span class="required">* </span></label>
                                <div class="col-md-1">
                                    <asp:TextBox ID="TxtAccNo4" CssClass="form-control" onkeypress="javascript:return isNumber (event)" PlaceHolder="Account No /  Receipt No" runat="server" AutoPostBack="true" OnTextChanged="TxtAccNo4_TextChanged"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="TxtAccName4" CssClass="form-control" Width="270px" Placeholder="Acc Name" runat="server" AutoPostBack="true" OnTextChanged="TxtAccName4_TextChanged"></asp:TextBox>
                                    <div id="CustList4" style="height: 200px; overflow-y: scroll;"></div>
                                    <asp:AutoCompleteExtender ID="Autoaccname4" runat="server" TargetControlID="TxtAccName4"
                                        UseContextKey="true"
                                        CompletionInterval="1"
                                        CompletionSetCount="20"
                                        MinimumPrefixLength="1"
                                        EnableCaching="true"
                                        ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList4"
                                        ServiceMethod="GetAccName">
                                    </asp:AutoCompleteExtender>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-12">
                                <div class="col-md-2">
                                </div>
                                <div class="col-md-3">
                                    <asp:Button ID="BtnSubmit" runat="server" CssClass="btn blue" Text="Submit" OnClick="BtnSubmit_Click" OnClientClick="javascript:return validate();" />
                                    <asp:Button ID="btnClear" runat="server" CssClass="btn blue" Text="ClearAll" OnClick="btnClear_Click" />
                                    <asp:Button ID="Btn_Exit" Text="Exit" runat="server" CssClass="btn blue" OnClick="Btn_Exit_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </div>





    <div class="row" runat="server" id="Div_grid">
        <div class="col-lg-12">
            <div class="table-scrollable" style="width: 100%; height: 350px; overflow-x: auto; overflow-y: auto">
                <table class="table table-striped table-bordered table-hover">
                    <thead>
                        <tr>
                            <th>
                                <asp:GridView ID="grdMaster" runat="server" AllowPaging="True" CssClass="mGrid"
                                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                    EditRowStyle-BackColor="#FFFF99"
                                    OnPageIndexChanging="grdMaster_PageIndexChanging"
                                    PagerStyle-CssClass="pgr" Width="100%">
                                    <Columns>

                                        <asp:TemplateField HeaderText="Cust No" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="CustNo" runat="server" Text='<%# Eval("CustNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Product Code" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="Depositglcode" runat="server" Text='<%# Eval("Depositglcode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Acc No">
                                            <ItemTemplate>
                                                <asp:Label ID="CustAccno" runat="server" Text='<%# Eval("CustAccno") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="prnamt" runat="server" Text='<%# Eval("prnamt") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="rateofint" runat="server" Text='<%# Eval("rateofint") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="period">
                                            <ItemTemplate>
                                                <asp:Label ID="period" runat="server" Text='<%# Eval("period") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Opening date" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="openingdate" runat="server" Text='<%# Eval("openingdate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Modify" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit" runat="server" CommandName="select" CommandArgument='<%#Eval("Depositglcode")+","+ Eval("CustAccno")%>' OnClick="lnkEdit_Click" class="glyphicon glyphicon-plus"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Authorize" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkAuthorize" runat="server" CommandName="select" CommandArgument='<%#Eval("Depositglcode")+","+ Eval("CustAccno")%>' OnClick="lnkAuthorize_Click" class="glyphicon glyphicon-plus"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="select" CommandArgument='<%#Eval("Depositglcode")+","+ Eval("CustAccno")%>' OnClick="lnkDelete_Click" class="glyphicon glyphicon-plus"></asp:LinkButton>
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
       <div id="DIV_REDIRECT" class="modal fade" role="dialog">
                <div class="modal-dialog modal-lg" style="width: 50%">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title" style="text-align: center; font-family: Verdana; font-size: medium; font-style: italic">AVS CORE</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">

                                <div class="col-md-12">

                                    <div class="portlet-body form">
                                        <!--<form action="#" class="form-horizontal" id="submit_form" method="POST">-->
                                        <div class="form-horizontal">
                                            <div class="form-wizard">
                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">
                                                        <asp:Label class="modal-title" Style="text-align: center; font-family: Verdana; font-size: medium; font-style: italic; color:black" runat="server" ID="Lbl_DepositMsg"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="form-actions">
                                                    <div class="row">
                                                        <div class="col-md-offset-3 col-md-9">
                                                            <asp:Button ID="Btn_Redirect" CssClass="btn btn-success"  runat="server" OnClick="Btn_Redirect_Click" Text="" />
                                                            <asp:Button ID="BtnModal_ExitCR" Text="Close" runat="server" CssClass="btn btn-success" data-dismiss="modal" />
                                                        </div>
                                                    </div>
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
</asp:Content>

