<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAVS5122.aspx.cs" Inherits="FrmAVS5122" %>

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

        function checkQuote() {
            if (event.keyCode == 39) {
                event.keyCode = 0;
                return false;
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
                        Transfer Head To Head
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">

                                    <div style="border: 1px solid #3598dc">
                                        <div class="portlet-body">
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Transfer From Account Details: </strong></div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:RadioButtonList ID="rbtnType" runat="server" OnSelectedIndexChanged="rbtnType_SelectedIndexChanged" AutoPostBack="true" RepeatDirection="Horizontal" Style="width: 300px;">
                                                                <asp:ListItem Text="Head To Head" Value="P" />
                                                                <asp:ListItem Text="Acc To Acc" Value="A" Selected="True" />
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </div>
                                                </div>

                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">From Prod Type <span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtFProdType" runat="server" CssClass="form-control" OnTextChanged="txtFProdType_TextChanged" AutoPostBack="true" onkeypress="javascript:return isNumber(event)" Placeholder="Account Type"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="input-icon">
                                                            <i class="fa fa-search"></i>
                                                            <asp:TextBox ID="txtFProdName" runat="server" CssClass="form-control" OnTextChanged="txtFProdName_TextChanged" AutoPostBack="true" Placeholder="Search Product Name" onkeypress="return checkQuote();"></asp:TextBox>
                                                            <div id="CustList1" style="height: 200px; overflow-y: scroll;"></div>
                                                            <asp:AutoCompleteExtender ID="AutoFGlName" runat="server" TargetControlID="txtFProdName" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" ServiceMethod="GetGlWiseName" CompletionListElementID="CustList1">
                                                            </asp:AutoCompleteExtender>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div id="divFAccInfo" runat="server" visible="true" class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">From Acc No </label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtFAccNo" runat="server" CssClass="form-control" OnTextChanged="txtFAccNo_TextChanged" AutoPostBack="true" onkeypress="javascript:return isNumber(event)" Placeholder="Account Number" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="input-icon">
                                                            <i class="fa fa-search"></i>
                                                            <asp:TextBox ID="txtFAccName" runat="server" CssClass="form-control" OnTextChanged="txtFAccName_TextChanged" AutoPostBack="true" Placeholder="Search Account Name" onkeypress="return checkQuote();" />
                                                            <div id="CustList2" style="height: 200px; overflow-y: scroll;"></div>
                                                            <asp:AutoCompleteExtender ID="AutoFAccName" runat="server" TargetControlID="txtFAccName" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" ServiceMethod="GetProdWiseName" CompletionListElementID="CustList2">
                                                            </asp:AutoCompleteExtender>
                                                        </div>
                                                    </div>
                                                    <label class="control-label col-md-1">Cust No </label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtFCustNo" Enabled="false" runat="server" CssClass="form-control" Placeholder="Customer Number" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div style="border: 1px solid #3598dc">
                                        <div class="portlet-body">
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Transfer To Account Details: </strong></div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">To Prod Type <span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtTProdType" runat="server" CssClass="form-control" OnTextChanged="txtTProdType_TextChanged" AutoPostBack="true" onkeypress="javascript:return isNumber(event)" Placeholder="Account Type"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="input-icon">
                                                            <i class="fa fa-search"></i>
                                                            <asp:TextBox ID="txtTProdName" runat="server" CssClass="form-control" OnTextChanged="txtTProdName_TextChanged" AutoPostBack="true" Placeholder="Search Product Name" onkeypress="return checkQuote();"></asp:TextBox>
                                                            <div id="CustList3" style="height: 200px; overflow-y: scroll;"></div>
                                                            <asp:AutoCompleteExtender ID="AutoTGlName" runat="server" TargetControlID="txtTProdName" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" ServiceMethod="GetGlWiseName" CompletionListElementID="CustList3">
                                                            </asp:AutoCompleteExtender>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div id="divTAccInfo" runat="server" visible="true" class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">To Account No </label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtTAccNo" runat="server" CssClass="form-control" OnTextChanged="txtTAccNo_TextChanged" AutoPostBack="true" onkeypress="javascript:return isNumber(event)" Placeholder="Account Number" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="input-icon">
                                                            <i class="fa fa-search"></i>
                                                            <asp:TextBox ID="txtTAccName" runat="server" CssClass="form-control" OnTextChanged="txtTAccName_TextChanged" AutoPostBack="true" Placeholder="Search Account Name" onkeypress="return checkQuote();" />
                                                            <div id="CustList4" style="height: 200px; overflow-y: scroll;"></div>
                                                            <asp:AutoCompleteExtender ID="AutoTAccName" runat="server" TargetControlID="txtTAccName" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" ServiceMethod="GetProdWiseName" CompletionListElementID="CustList4">
                                                            </asp:AutoCompleteExtender>
                                                        </div>
                                                    </div>
                                                    <label class="control-label col-md-1">Cust No </label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtTCustNo" Enabled="false" runat="server" CssClass="form-control" Placeholder="Customer Number" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">From Date </label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtFDate" runat="server" CssClass="form-control" onkeyup="FormatIt(this);" Placeholder="DD/MM/YYYY" ></asp:TextBox>
                                                    </div>
                                                    <label class="control-label col-md-1">To Date </label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtTDate" runat="server" CssClass="form-control" onkeyup="FormatIt(this);" Placeholder="DD/MM/YYYY" ></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-12">
                                        <asp:Button ID="btnTransfer" runat="server" CssClass="btn blue" Text="Transfer" OnClick="Transfer_Click" OnClientClick="Javascript:return IsValide(); CheckConfirm(this);" />
                                        <asp:Button ID="btnClear" runat="server" CssClass="btn blue" Text="Clear All" OnClick="btnClear_Click" />
                                        <asp:Button ID="btnExit" runat="server" CssClass="btn blue" Text="Exit" OnClick="btnExit_Click" />
                                    </div>
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
                    <center><h4 class="modal-title" style="color:#ff0000">AVS Company</h4></center>
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

