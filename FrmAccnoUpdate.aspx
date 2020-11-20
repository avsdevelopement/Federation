<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAccnoUpdate.aspx.cs" Inherits="FrmAccnoUpdate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
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
    </script>
    <script type="text/javascript">
        function IsValidate() {
            var BrCode = document.getElementById('<%=txtBrCode.ClientID%>').value;
            var PrCode = document.getElementById('<%=txtProdCode.ClientID%>').value;
            var AccNo = document.getElementById('<%=txtAccNo.ClientID%>').value;
            var message = '';

            if (BrCode == "") {
                message = 'Enter branch code first...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtBrCode.ClientID%>').focus();
                return false;
            }

            if (PrCode == "") {
                message = 'Enter product code first...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtProdCode.ClientID%>').focus();
                return false;
            }

            if (AccNo == "") {
                message = 'Enter account no first...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtAccNo.ClientID%>').focus();
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="portlet box blue" id="form_wizard_1">
                        <div class="portlet-title">
                            <div class="caption">
                                Account Number Changes
                            </div>
                        </div>
                        <div class="portlet-body form">
                            <div class="form-horizontal">
                                <div class="form-wizard">
                                    <div class="form-body">
                                        <div class="tab-content">
                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-1">Branch Code<span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtBrCode" Enabled="false" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" />
                                                    </div>
                                                    <div class="col-lg-3">
                                                        <asp:DropDownList ID="ddlBrName" CssClass="form-control" OnSelectedIndexChanged="ddlBrName_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <label class="control-label col-md-1">Open Date</label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtAccOpenDate" Enabled="false" runat="server" CssClass="form-control" />
                                                    </div>
                                                    <div id="DivAmount" runat="server" visible="false">
                                                        <label id="LblName1" runat="server" class="control-label col-md-1"></label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtLimitAmt" Enabled="false" runat="server" CssClass="form-control" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-1">Product <span class="required">*</span></label>
                                                    <div class="col-lg-2">
                                                        <asp:TextBox ID="txtProdCode" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" OnTextChanged="txtProdCode_TextChanged" AutoPostBack="true" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="input-icon">
                                                            <i class="fa fa-search"></i>
                                                            <asp:TextBox ID="txtProdName" runat="server" placeholder="Search Product Name" CssClass="form-control" OnTextChanged="txtProdName_TextChanged" AutoPostBack="true" />
                                                            <div id="CustList1" style="height: 200px; overflow-y: scroll;"></div>
                                                            <asp:AutoCompleteExtender ID="AutoProdName" runat="server" TargetControlID="txtProdName" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" ServiceMethod="GetGlWiseName" CompletionListElementID="CustList1" />
                                                        </div>
                                                    </div>
                                                    <label class="control-label col-md-1">Clear Balance</label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtClearBal" Enabled="false" runat="server" CssClass="form-control" />
                                                    </div>
                                                    <div id="DivPeriod" runat="server" visible="false">
                                                        <label id="LblName2" runat="server" class="control-label col-md-1">Period</label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtPeriod" Enabled="false" runat="server" CssClass="form-control" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-1">Acc No<span class="required">*</span></label>
                                                    <div class="col-lg-2">
                                                        <asp:TextBox ID="txtAccNo" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" OnTextChanged="txtAccNo_TextChanged" AutoPostBack="true" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="input-icon">
                                                            <i class="fa fa-search"></i>
                                                            <asp:TextBox ID="txtAccName" runat="server" placeholder="Search Customer Name" CssClass="form-control" OnTextChanged="txtAccName_TextChanged" AutoPostBack="true" />
                                                            <div id="CustList2" style="height: 200px; overflow-y: scroll;"></div>
                                                            <asp:AutoCompleteExtender ID="AutoCustName" runat="server" TargetControlID="txtAccName" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" ServiceMethod="GetProdWiseName" CompletionListElementID="CustList2" />
                                                        </div>
                                                    </div>
                                                    <label class="control-label col-md-1">Acc Status</label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtAccStatus" Enabled="false" runat="server" CssClass="form-control" />
                                                    </div>
                                                    <div id="DivIntRate" runat="server" visible="false">
                                                        <label id="LblName3" runat="server" class="control-label col-md-1">Int Rate</label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtIntRate" Enabled="false" runat="server" CssClass="form-control" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-1">Special Inst </label>
                                                    <div class="col-lg-5">
                                                        <asp:TextBox ID="txtSpecialInst" Enabled="false" CssClass="form-control" runat="server" />
                                                    </div>
                                                    <div id="DivOverDue" runat="server" visible="false">
                                                        <label class="control-label col-md-1">OverDue </label>
                                                        <div class="col-lg-2">
                                                            <asp:TextBox ID="txtOverDue" Enabled="false" CssClass="form-control" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-1">New Acc No<span class="required">*</span></label>
                                                    <div class="col-lg-2">
                                                        <asp:TextBox ID="TxtNewAccNo" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" OnTextChanged="TxtNewAccNo_TextChanged" AutoPostBack="true" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="input-icon">
                                                            <i class="fa fa-search"></i>
                                                            <asp:TextBox ID="TxtNewAccName" runat="server" placeholder="Search Customer Name" CssClass="form-control" OnTextChanged="TxtNewAccName_TextChanged" AutoPostBack="true" />
                                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="TxtNewAccName" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" ServiceMethod="GetProdWiseName" CompletionListElementID="CustList2" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-actions">
                                        <div class="row">
                                            <div class="col-md-offset-3 col-md-9">
                                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn blue" Text="Update AccNo" OnClick="btnSubmit_Click" OnClientClick="Javascript:return IsValidate();" />
                                                <asp:Button ID="btnClear" runat="server" CssClass="btn blue" Text="Clear All" OnClick="btnClear_Click" />
                                                <asp:Button ID="btnExit" runat="server" CssClass="btn btn-primary" Text="Exit" OnClick="btnExit_Click" />
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

