<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmTDACBS2.aspx.cs" Inherits="FrmTDACBS2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46) {
                return false;
            }
            return true;
        }

        function FormatIt(obj) {
            if (obj.value.length == 2) // Day
                obj.value = obj.value + "/";
            if (obj.value.length == 5) // month 
                obj.value = obj.value + "/";
            if (obj.value.length == 11) // year 
                alert("Please Enter valid Date");
        }

        function Check15daysDate(obj) {

            var hddate = document.getElementById('<% =hdentrydt.ClientID%>').value;
            var EnteredDate = obj.value;
            var date = EnteredDate.substring(0, 2);
            var month = EnteredDate.substring(3, 5);
            var year = EnteredDate.substring(6, 10);
            var date1 = hddate.substring(0, 2);
            var month1 = hddate.substring(3, 5);
            var year1 = hddate.substring(6, 10);

            var myDate = new Date(year, month - 1, date);

            var myDate1 = new Date(year1, month1 - 1, date1);
            var today = new Date();
            var timeDiff = Math.abs(myDate1.getTime() - myDate.getTime());
            var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24));
            if (year.length == 4) {
                if (diffDays > 15) {
                    window.alert("User cannot select backdate more than 15 days ...!!");
                    document.getElementById('<%=dtDeposDate.ClientID%>').value = '';
                    document.getElementById('<%=dtDeposDate.ClientID%>').focus();
                    return false;
                }
                else {
                    return true;
                }
            }
        }

        function Check15daysDate1(sender, args) {

            var hddate = document.getElementById('<% =hdentrydt.ClientID%>').value;
            var EnteredDate = sender._selectedDate;
            var date1 = hddate.substring(0, 2);
            var month1 = hddate.substring(3, 5);
            var year1 = hddate.substring(6, 10);

            var today = new Date();
            var myDate1 = new Date(year1, month1 - 1, date1);
            var timeDiff = Math.round(myDate1.getTime() - EnteredDate.getTime());

            var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24));
            if (diffDays > 15) {
                window.alert("User cannot select backdate more than 15 days ...!!");
                document.getElementById('<%=dtDeposDate.ClientID%>').value = '';
                document.getElementById('<%=dtDeposDate.ClientID%>').focus();
                return false;
            }
            else if (EnteredDate > myDate1) {
                window.alert("Inavlid Date. System cannot accept Future Date ...!!");
                document.getElementById('<%=dtDeposDate.ClientID%>').value = '';
                document.getElementById('<%=dtDeposDate.ClientID%>').focus();
                return false;
            }
            else {
                return true;
            }
    }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        Create Deposit
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">

                                    <%-- Radio Buttons --%>
                                    <div runat="server" id="Main" visible="true">
                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-3">Select </label>
                                                <div class="col-md-1">
                                                    <asp:Button ID="btnAddNew" runat="server" CssClass="btn default" Text="Add new " OnClick="btnAddNew_Click" AccessKey="1" />
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:Button ID="BtnExist" runat="server" CssClass="btn default" Text="Existing" OnClick="BtnExist_Click" AccessKey="2" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div runat="server" id="Search" visible="false">

                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2" style="width: 120px">Product Code : <span class="required">*</span></label>
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="TxtSPrdCode" CssClass="form-control" placeholder="Product Code" onkeypress="javascript:return isNumber (event)" AutoPostBack="true" OnTextChanged="TxtSPrdCode_TextChanged" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TXtSPrdname" CssClass="form-control" placeholder="Product Name" runat="server" AutoPostBack="true" OnTextChanged="TXtSPrdname_TextChanged"></asp:TextBox>
                                                    <div id="Div_sprd" style="height: 200px; overflow-y: scroll;"></div>
                                                    <asp:AutoCompleteExtender ID="AutoSPrdcode" runat="server" TargetControlID="TXtSPrdname"
                                                        UseContextKey="true"
                                                        CompletionInterval="1"
                                                        CompletionSetCount="20"
                                                        MinimumPrefixLength="1"
                                                        EnableCaching="true"
                                                        ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="Div_sprd"
                                                        ServiceMethod="GetGlName">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                                <label class="control-label col-md-1">AccNo : <span class="required">*</span></label>
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="TxtSAccno" CssClass="form-control" placeholder="Acc No" onkeypress="javascript:return isNumber (event)" AutoPostBack="true" OnTextChanged="TxtSAccno_TextChanged" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtSaccName" CssClass="form-control" placeholder="Acc Holder Name" runat="server" AutoPostBack="true" OnTextChanged="TxtSaccName_TextChanged"></asp:TextBox>
                                                    <div id="Div_saccno" style="height: 200px; overflow-y: scroll;"></div>
                                                    <asp:AutoCompleteExtender ID="AutoSAccno" runat="server" TargetControlID="TxtSaccName"
                                                        UseContextKey="true"
                                                        CompletionInterval="1"
                                                        CompletionSetCount="20"
                                                        MinimumPrefixLength="1"
                                                        EnableCaching="true"
                                                        ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="Div_saccno"
                                                        ServiceMethod="GetAccName">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                            </div>
                                        </div>

                                    </div>

                                    <%-- Main Area fixed --%>
                                    <div runat="server" id="DIV_Term" visible="false">

                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Deposit Account Details : </strong></div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2" style="width: 120px">Product Code : <span class="required">*</span></label>
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
                                                <label class="control-label col-md-1" style="width: 120px">Account No : <span class="required">* </span></label>
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="txtAccNo" CssClass="form-control" onkeypress="javascript:return isNumber (event)" PlaceHolder="Account No /  Receipt No" runat="server" AutoPostBack="true" OnTextChanged="txtAccNo_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtAccName" CssClass="form-control" Placeholder="Acc Name" runat="server" AutoPostBack="true" OnTextChanged="TxtAccName_TextChanged"></asp:TextBox>
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
                                                <label class="control-label col-md-1">RecSrNo<span class="required"></span></label>
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="TxtRecNo" Enabled="false" CssClass="form-control" placeholder="RecSrNo" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2" style="width: 120px">Account Type : <span class="required">*</span></label>
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="ddlAccType" runat="server" CssClass="form-control" Enabled="False">
                                                    </asp:DropDownList>
                                                </div>
                                                <label class="control-label col-md-1" style="width: 120px">Member Type : <span class="required">* </span></label>
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="ddlOpType" runat="server" CssClass="form-control" Enabled="False">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2" style="width: 120px">AsOnDate : <span class="required">*</span></label>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="dtDeposDate" onkeyup="FormatIt(this);Check15daysDate(this);" CssClass="form-control" PlaceHolder="DD/MM/YYYY" runat="server"></asp:TextBox>
                                                    <asp:CalendarExtender ID="TxtDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="dtDeposDate" OnClientDateSelectionChanged="Check15daysDate1">
                                                    </asp:CalendarExtender>
                                                </div>
                                                <label class="control-label col-md-1" style="width: 120px">Int Payout : <span class="required">* </span></label>
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="ddlIntrestPay" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2" style="width: 120px">Deposit Amt : <span class="required">*</span></label>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtDepoAmt" CssClass="form-control" PlaceHolder="Deposit Amount" MaxLength="8" runat="server" onkeypress="javascript:return isNumber (event)" AutoPostBack="true" OnTextChanged="TxtDepoAmt_TextChanged"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-1" style="width: 120px">Period : <span class="required">* </span></label>
                                                <div class="col-md-2">
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

                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2" style="width: 120px">Interest Rate : <span class="required">*</span></label>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtRate" CssClass="form-control" PlaceHolder="Rate" runat="server" Enabled="false" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-1" style="width: 120px">Interest Amt :</label>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtIntrest" CssClass="form-control" PlaceHolder="Interest Amount" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2" style="width: 120px">Maturity Amt : </label>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtMaturity" CssClass="form-control" PlaceHolder="Maturity Amount" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-1" style="width: 120px">Due Date : </label>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="DtDueDate" CssClass="form-control" PlaceHolder="dd/mm/yyyy" runat="server" Enabled="false"></asp:TextBox>
                                                    <asp:TextBoxWatermarkExtender ID="TxtFDT_WatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="DtDueDate">
                                                    </asp:TextBoxWatermarkExtender>
                                                    <asp:CalendarExtender ID="TxtFDT_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="DtDueDate">
                                                    </asp:CalendarExtender>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2" style="width: 120px">Receipt No : </label>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtReceiptNo" CssClass="form-control" PlaceHolder="Receipt No" runat="server" Enabled="true"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-1" style="width: 120px">Cust No :</label>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="Txtcustno" CssClass="form-control" PlaceHolder="Receipt No" runat="server" Enabled="true"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2" style="width: 120px">TDS Rate : </label>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtTDSRate" CssClass="form-control" PlaceHolder="TDS Rate" runat="server" Enabled="false" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-1" style="width: 120px">TDS Amount : </label>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtTDSAmount" CssClass="form-control" PlaceHolder="TDS" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2" style="width: 120px">Remark : </label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="TxtTDSRemark" CssClass="form-control" PlaceHolder="TDS" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                                <asp:LinkButton ID="Lnk_RedirectModCust" Visible="false" runat="server" Text="Modify Customer" OnClick="Lnk_RedirectModCust_Click"></asp:LinkButton>
                                            </div>
                                        </div>

                                        <div id="Div1" class="row" style="margin-top: 5px; margin-bottom: 5px;" runat="server" visible="false">
                                            <div class="col-lg-12">
                                                <div class="col-md-1">
                                                    <asp:RadioButton ID="Rdb_Trf" Checked="true" runat="server" Text="Transfer" Style="font-style: italic;" OnCheckedChanged="Rdb_Trf_CheckedChanged" AutoPostBack="true" GroupName="TrxType" ToolTip="For Transfer Amount" />
                                                </div>
                                                <div class="col-md-1">
                                                    <asp:RadioButton ID="Rdb_CR" runat="server" Text="By Cash" Style="font-style: italic" OnCheckedChanged="Rdb_CR_CheckedChanged" AutoPostBack="true" GroupName="TrxType" ToolTip="For Cash Receipt" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Interest Transfer Account : </strong></div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2" style="width: 120px">Branch Code :<span class="required">*</span></label>
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="ddlTrfBrName" CssClass="form-control" runat="server" OnTextChanged="ddlTrfBrName_TextChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtTrfBrCode" CssClass="form-control" runat="server" Enabled="false" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2" style="width: 120px">Product Code : <span class="required">*</span></label>
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
                                                <label class="control-label col-md-2" style="width: 120px">Account No : <span class="required">*</span></label>
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

                                    </div>
                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-12">
                                        <asp:Button ID="BtnSubmit" runat="server" CssClass="btn blue" Text="Submit" UseSubmitBehavior="false" OnClick="BtnSubmit_Click" OnClientClick="this.disabled='true';this.value='Please Wait'" />
                                        <asp:Button ID="btnClear" runat="server" CssClass="btn blue" Text="ClearAll" UseSubmitBehavior="false" OnClick="btnClear_Click" OnClientClick="this.disabled='true';this.value='Please Wait'" />
                                        <asp:Button ID="Btn_Exit" Text="Exit" runat="server" CssClass="btn blue" UseSubmitBehavior="false" OnClick="Btn_Exit_Click" OnClientClick="this.disabled='true';this.value='Please Wait'" />
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
                                <asp:GridView ID="grdMaster" runat="server" CssClass="mGrid"
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
                                        <asp:TemplateField HeaderText="Recp. Srno">
                                            <ItemTemplate>
                                                <asp:Label ID="RecSrno" runat="server" Text='<%# Eval("RecSrno") %>'></asp:Label>
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
                                        <asp:TemplateField HeaderText="Receipt No" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="ReceiptNo" runat="server" Text='<%# Eval("RECEIPT_NO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Modify" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit" runat="server" CommandName="select" CommandArgument='<%#Eval("Depositglcode")+","+ Eval("CustAccno")+","+Eval("RecSrno")%>' OnClick="lnkEdit_Click" class="glyphicon glyphicon-plus"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Authorize" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkAuthorize" runat="server" CommandName="select" CommandArgument='<%#Eval("Depositglcode")+","+ Eval("CustAccno")+","+Eval("RecSrno")%>' OnClick="lnkAuthorize_Click" class="glyphicon glyphicon-plus"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="select" CommandArgument='<%#Eval("Depositglcode")+","+ Eval("CustAccno")+","+Eval("RecSrno")%>' OnClick="lnkDelete_Click" class="glyphicon glyphicon-plus"></asp:LinkButton>
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
                                                <asp:Label class="modal-title" Style="text-align: center; font-family: Verdana; font-size: medium; font-style: italic; color: black" runat="server" ID="Lbl_DepositMsg"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="form-actions">
                                            <div class="row">
                                                <div class="col-md-offset-3 col-md-9">
                                                    <asp:Button ID="Btn_Redirect" CssClass="btn btn-success" runat="server" OnClick="Btn_Redirect_Click" Text="" />
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
        <asp:HiddenField ID="hdentrydt" runat="server" />
    </div>

</asp:Content>

