<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmShareParameter.aspx.cs" Inherits="FrmShareParameter" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;

            return true;
        }
    </script>
    <script type="text/javascript">
        function IsValid() {

            var txtShrValue = document.getElementById('<%=txtShrValue.ClientID%>').value;
            var txtShrFrom = document.getElementById('<%=txtShrFrom.ClientID%>').value;
            var txtNoOfShr = document.getElementById('<%=txtNoOfShr.ClientID%>').value;
            var txtShrPrCode = document.getElementById('<%=txtShrPrCode.ClientID%>').value;
            var txtEntryFee = document.getElementById('<%=txtEntryFee.ClientID%>').value;
            var txtOther1 = document.getElementById('<%=txtOther1.ClientID%>').value;
            var txtSavProd = document.getElementById('<%=txtSavProd.ClientID%>').value;
            var txtOther2 = document.getElementById('<%=txtOther2.ClientID%>').value;
            var message = '';

            if (txtShrValue == "") {
                message = 'Enter Shares Value...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=txtShrValue.ClientID %>').focus();
                return false;
            }

            if (txtShrFrom == "") {
                message = 'Enter Shares From...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=txtShrFrom.ClientID %>').focus();
                return false;
            }

            if (txtNoOfShr == "") {
                message = 'Enter No Of Share...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=txtNoOfShr.ClientID %>').focus();
                return false;
            }

            if (txtShrPrCode == "") {
                message = 'Enter Share Product Code...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=txtShrPrCode.ClientID %>').focus();
                return false;
            }

            if (txtEntryFee == "") {
                message = 'Enter Entry Fees Product Code...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=txtEntryFee.ClientID %>').focus();
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
                                Shares Parameter Details 
                            </div>
                        </div>
                        <div class="portlet-body form">
                            <div class="form-horizontal">
                                <div class="form-wizard">
                                    <div class="form-body">
                                        <div class="tab-content">
                                            <div class="tab-pane active" id="tab__blue">
                                                <ul class="nav nav-pills">

                                                    <li>
                                                        <asp:LinkButton ID="lnkAdd" runat="server" Text="a" class="btn btn-default" OnClick="lnkAdd_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-asterisk"></i>Add New</asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton ID="lnkModify" runat="server" Text="a" class="btn btn-default" OnClick="lnkModify_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-plus-circle"></i>Modify</asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton ID="lnkDelete" runat="server" Text="MD" class="btn btn-default" OnClick="lnkDelete_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-pencil-square-o"></i>Delete</asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton ID="lnkAuthorized" runat="server" Text="a" class="btn btn-default" OnClick="lnkAuthorized_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-arrows"></i>Authorise</asp:LinkButton>
                                                    </li>
                                                    <li class="pull-right">
                                                        <asp:Label ID="lblStst" runat="server" Text="Activity Perform :" Style="font-weight: bold;"></asp:Label>
                                                        <asp:Label ID="lblActivity" runat="server" Text=""></asp:Label>
                                                    </li>
                                                </ul>
                                            </div>

                                            <div style="border: 1px solid #3598dc">
                                                <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label">Shares Value Amount<span class="required">*</span></label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtShrValue" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="Default Shares Value" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <label class="control-label">Shares From <span class="required">*</span></label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtShrFrom" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="Shares From" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <label class="control-label">Number Of Shares <span class="required">*</span></label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtNoOfShr" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="Default No Of Shares" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label">Entrance Amount<span class="required">*</span></label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtEnterenceFee" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="Default Enterence Fee" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <label class="control-label">Welfare Amount</label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtWelfareFee" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="Default Welfare Fee" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <label class="control-label">Welfare(Loan) Amount</label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtWelLoanFee" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="Default Welfare(Loan) Fee" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row" style="margin-bottom: 10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label">Shares Prod Code <span class="required">*</span></label>
                                                        </div>
                                                        <div class="col-md-1">
                                                            <asp:TextBox ID="txtShrPrCode" OnTextChanged="txtShrPrCode_TextChanged" AutoPostBack="true" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="Shares GL" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="input-icon">
                                                                <i class="fa fa-search"></i>
                                                                <asp:TextBox ID="txtShrProdName" OnTextChanged="txtShrProdName_TextChanged" AutoPostBack="true" CssClass="form-control" placeholder="Shares Product Name" runat="server"></asp:TextBox>
                                                                <div id="CustList1" style="height: 200px; overflow-y: scroll;"></div>
                                                                <asp:AutoCompleteExtender ID="AutoGlName1" runat="server" TargetControlID="txtShrProdName" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                    MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList1" ServiceMethod="GetGlName">
                                                                </asp:AutoCompleteExtender>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <label class="control-label">Enterance Prod Code<span class="required">*</span></label>
                                                        </div>
                                                        <div class="col-md-1">
                                                            <asp:TextBox ID="txtEntryFee" OnTextChanged="txtEntryFee_TextChanged" AutoPostBack="true" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="Enterance Fee GL" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="input-icon">
                                                                <i class="fa fa-search"></i>
                                                                <asp:TextBox ID="txtEntryProdName" OnTextChanged="txtEntryProdName_TextChanged" AutoPostBack="true" CssClass="form-control" placeholder="Enterance Product Name" runat="server"></asp:TextBox>
                                                                <div id="CustList2" style="height: 200px; overflow-y: scroll;"></div>
                                                                <asp:AutoCompleteExtender ID="AutoGlName2" runat="server" TargetControlID="txtEntryProdName" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                    MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList2" ServiceMethod="GetGlName">
                                                                </asp:AutoCompleteExtender>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row" style="margin-bottom: 10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label">Saving Prod Code </label>
                                                        </div>
                                                        <div class="col-md-1">
                                                            <asp:TextBox ID="txtSavProd" OnTextChanged="txtSavProd_TextChanged" AutoPostBack="true" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="Saving GL" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="input-icon">
                                                                <i class="fa fa-search"></i>
                                                                <asp:TextBox ID="txtSavProdName" OnTextChanged="txtSavProdName_TextChanged" AutoPostBack="true" CssClass="form-control" placeholder="Saving Product Name" runat="server"></asp:TextBox>
                                                                <div id="CustList3" style="height: 200px; overflow-y: scroll;"></div>
                                                                <asp:AutoCompleteExtender ID="AutoGlName3" runat="server" TargetControlID="txtSavProdName" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                    MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList3" ServiceMethod="GetGlName">
                                                                </asp:AutoCompleteExtender>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <label class="control-label">Welfare Prod Code</label>
                                                        </div>
                                                        <div class="col-md-1">
                                                            <asp:TextBox ID="txtOther1" OnTextChanged="txtOther1_TextChanged" AutoPostBack="true" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="Welfare GL" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="input-icon">
                                                                <i class="fa fa-search"></i>
                                                                <asp:TextBox ID="txtWelProdName" OnTextChanged="txtWelProdName_TextChanged" AutoPostBack="true" CssClass="form-control" placeholder="Welfare Product Name" runat="server"></asp:TextBox>
                                                                <div id="CustList4" style="height: 200px; overflow-y: scroll;"></div>
                                                                <asp:AutoCompleteExtender ID="AutoGlName4" runat="server" TargetControlID="txtWelProdName" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                    MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList4" ServiceMethod="GetGlName">
                                                                </asp:AutoCompleteExtender>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row" style="margin-bottom: 10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label">Printing Prod Code</label>
                                                        </div>
                                                        <div class="col-md-1">
                                                            <asp:TextBox ID="txtOther2" OnTextChanged="txtOther2_TextChanged" AutoPostBack="true" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="Printing GL" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="input-icon">
                                                                <i class="fa fa-search"></i>
                                                                <asp:TextBox ID="txtPrinProdName" OnTextChanged="txtPrinProdName_TextChanged" AutoPostBack="true" CssClass="form-control" placeholder="Printing and Stationary Product Name" runat="server"></asp:TextBox>
                                                                <div id="CustList5" style="height: 200px; overflow-y: scroll;"></div>
                                                                <asp:AutoCompleteExtender ID="AutoGlName5" runat="server" TargetControlID="txtPrinProdName" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                    MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList5" ServiceMethod="GetGlName">
                                                                </asp:AutoCompleteExtender>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <label class="control-label">Service Charge Prod Code </label>
                                                        </div>
                                                        <div class="col-md-1">
                                                            <asp:TextBox ID="txtServiceChg" OnTextChanged="txtServiceChg_TextChanged" AutoPostBack="true" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="Servise GL" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="input-icon">
                                                                <i class="fa fa-search"></i>
                                                                <asp:TextBox ID="txtServiceProdName" OnTextChanged="txtServiceProdName_TextChanged" AutoPostBack="true" CssClass="form-control" placeholder="Service Charge Product Name" runat="server"></asp:TextBox>
                                                                <div id="CustList6" style="height: 200px; overflow-y: scroll;"></div>
                                                                <asp:AutoCompleteExtender ID="AutoGlName6" runat="server" TargetControlID="txtServiceProdName" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                    MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList6" ServiceMethod="GetGlName">
                                                                </asp:AutoCompleteExtender>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row" style="margin-bottom: 10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label">Welfare(Loan) Prod Code</label>
                                                        </div>
                                                        <div class="col-md-1">
                                                            <asp:TextBox ID="txtMemWelfare" OnTextChanged="txtMemWelfare_TextChanged" AutoPostBack="true" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="Member Welfare GL" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="input-icon">
                                                                <i class="fa fa-search"></i>
                                                                <asp:TextBox ID="txtWelLoanProdName" OnTextChanged="txtWelLoanProdName_TextChanged" AutoPostBack="true" CssClass="form-control" placeholder="Welfare(Loan) Product Name" runat="server"></asp:TextBox>
                                                                <div id="CustList7" style="height: 200px; overflow-y: scroll;"></div>
                                                                <asp:AutoCompleteExtender ID="AutoGlName7" runat="server" TargetControlID="txtWelLoanProdName" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                    MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList7" ServiceMethod="GetGlName">
                                                                </asp:AutoCompleteExtender>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <label class="control-label">ShareSuspence Prod Code</label>
                                                        </div>
                                                        <div class="col-md-1">
                                                            <asp:TextBox ID="txtShrSuspence" OnTextChanged="txtShrSuspence_TextChanged" AutoPostBack="true" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="Member Welfare GL" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="input-icon">
                                                                <i class="fa fa-search"></i>
                                                                <asp:TextBox ID="txtShrSusName" OnTextChanged="txtShrSusName_TextChanged" AutoPostBack="true" CssClass="form-control" placeholder="Welfare(Loan) Product Name" runat="server"></asp:TextBox>
                                                                <div id="CustList8" style="height: 200px; overflow-y: scroll;"></div>
                                                                <asp:AutoCompleteExtender ID="AutoGlName8" runat="server" TargetControlID="txtWelLoanProdName" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                    MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList8" ServiceMethod="GetGlName">
                                                                </asp:AutoCompleteExtender>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row" style="margin-bottom: 10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label">Death Charge </label>
                                                        </div>
                                                        <div class="col-md-1">
                                                            <asp:TextBox ID="txtOther3" OnTextChanged="txtOtherPrCode3_TextChanged" AutoPostBack="true" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="input-icon">
                                                                <i class="fa fa-search"></i>
                                                                <asp:TextBox ID="txtOtherPrName3" OnTextChanged="txtOtherPrName3_TextChanged" AutoPostBack="true" CssClass="form-control" placeholder="OtherCharge1 Product Name" runat="server"></asp:TextBox>
                                                                <div id="CustList12" style="height: 200px; overflow-y: scroll;"></div>
                                                                <asp:AutoCompleteExtender ID="AutoGlName12" runat="server" TargetControlID="txtOtherPrName3" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                    MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList12" ServiceMethod="GetGlName">
                                                                </asp:AutoCompleteExtender>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <label class="control-label">Mem Asst S.A Fees </label>
                                                        </div>
                                                        <div class="col-md-1">
                                                            <asp:TextBox ID="txtOther4" OnTextChanged="txtOtherPrCode4_TextChanged" AutoPostBack="true" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="input-icon">
                                                                <i class="fa fa-search"></i>
                                                                <asp:TextBox ID="txtOtherPrName4" OnTextChanged="txtOtherPrName4_TextChanged" AutoPostBack="true" CssClass="form-control" placeholder="OtherCharge2 Product Name" runat="server"></asp:TextBox>
                                                                <div id="CustList13" style="height: 200px; overflow-y: scroll;"></div>
                                                                <asp:AutoCompleteExtender ID="AutoGlName13" runat="server" TargetControlID="txtOtherPrName4" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                    MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList13" ServiceMethod="GetGlName">
                                                                </asp:AutoCompleteExtender>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row" style="margin-bottom: 10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label">M.A Subscrption </label>
                                                        </div>
                                                        <div class="col-md-1">
                                                            <asp:TextBox ID="txtOther5" OnTextChanged="txtOtherPrCode5_TextChanged" AutoPostBack="true" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="input-icon">
                                                                <i class="fa fa-search"></i>
                                                                <asp:TextBox ID="txtOtherPrName5" OnTextChanged="txtOtherPrName5_TextChanged" AutoPostBack="true" CssClass="form-control" placeholder="OtherCharge3 Product Name" runat="server"></asp:TextBox>
                                                                <div id="CustList14" style="height: 200px; overflow-y: scroll;"></div>
                                                                <asp:AutoCompleteExtender ID="AutoGlName14" runat="server" TargetControlID="txtOtherPrName5" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                    MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList14" ServiceMethod="GetGlName">
                                                                </asp:AutoCompleteExtender>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin-bottom: 10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label">Dividend yr Prod Cd 1</label>
                                                        </div>
                                                        <div class="col-md-1">
                                                            <asp:TextBox ID="Txtdiv1" OnTextChanged="Txtdiv1_TextChanged" AutoPostBack="true" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="dividend GL" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="input-icon">
                                                                <i class="fa fa-search"></i>
                                                                <asp:TextBox ID="Txtdiv1name" OnTextChanged="Txtdiv1name_TextChanged" AutoPostBack="true" CssClass="form-control" placeholder="dividend Product Name" runat="server"></asp:TextBox>
                                                                <div id="CustList9" style="height: 200px; overflow-y: scroll;"></div>
                                                                <asp:AutoCompleteExtender ID="AutoGlName9" runat="server" TargetControlID="Txtdiv1name" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                    MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList9" ServiceMethod="GetGlName">
                                                                </asp:AutoCompleteExtender>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <label class="control-label">Dividend yr Prod Cd 2</label>
                                                        </div>
                                                        <div class="col-md-1">
                                                            <asp:TextBox ID="Txtdiv2" OnTextChanged="Txtdiv2_TextChanged" AutoPostBack="true" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="dividend GL" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="input-icon">
                                                                <i class="fa fa-search"></i>
                                                                <asp:TextBox ID="Txtdiv2name" OnTextChanged="Txtdiv2name_TextChanged" AutoPostBack="true" CssClass="form-control" placeholder="dividend Product Name" runat="server"></asp:TextBox>
                                                                <div id="CustList10" style="height: 200px; overflow-y: scroll;"></div>
                                                                <asp:AutoCompleteExtender ID="AutoGlName10" runat="server" TargetControlID="Txtdiv2name" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                    MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList10" ServiceMethod="GetGlName">
                                                                </asp:AutoCompleteExtender>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row" style="margin-bottom: 10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label">Dividend yr Prod Cd 3</label>
                                                        </div>
                                                        <div class="col-md-1">
                                                            <asp:TextBox ID="Txtdiv3" OnTextChanged="Txtdiv3_TextChanged" AutoPostBack="true" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="dividend GL" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="input-icon">
                                                                <i class="fa fa-search"></i>
                                                                <asp:TextBox ID="Txtdiv3name" OnTextChanged="Txtdiv3name_TextChanged" AutoPostBack="true" CssClass="form-control" placeholder="dividend Product Name" runat="server"></asp:TextBox>
                                                                <div id="CustList11" style="height: 200px; overflow-y: scroll;"></div>
                                                                <asp:AutoCompleteExtender ID="AutoGlName11" runat="server" TargetControlID="Txtdiv3name" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                    MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList11" ServiceMethod="GetGlName">
                                                                </asp:AutoCompleteExtender>
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
                                                <asp:Button ID="Submit" runat="server" Text="Submit" CssClass="btn blue" OnClick="Submit_Click" OnClientClick="Javascript:return IsValid();" />
                                                <asp:Button ID="Exit" runat="server" Text="Exit" CssClass="btn blue" OnClick="Exit_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                            </div>
                            <div class="col-md-12">
                                <div class="table-scrollable">
                                    <table class="table table-striped table-bordered table-hover">
                                        <thead>
                                            <tr>
                                                <th>
                                                    <asp:GridView ID="grdMaster" runat="server" AllowPaging="True"
                                                        AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                        EditRowStyle-BackColor="#FFFF99"
                                                        OnPageIndexChanging="grdMaster_PageIndexChanging"
                                                        PagerStyle-CssClass="pgr" Width="100%">
                                                        <Columns>

                                                            <asp:TemplateField HeaderText="Select" Visible="true" runat="server">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName="select" CommandArgument='<%#Eval("Id")%>' OnClick="lnkEdit_Click" class="glyphicon glyphicon-plus"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Share Value" Visible="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSHR_VALUE" runat="server" Text='<%# Eval("SHR_VALUE") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="No Of Share">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNO_OF_SHARES" runat="server" Text='<%# Eval("NO_OF_SHARES") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Share GL">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSHARES_GL" runat="server" Text='<%# Eval("SHARES_GL") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Entry Fee GL">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblENT_FEE" runat="server" Text='<%# Eval("ENTRY_GL") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Saving GL">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSAVING_GL" runat="server" Text='<%# Eval("SAVING_GL") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Other Fee1 GL">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOTHERS_1" runat="server" Text='<%# Eval("OTHERS_GL1") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Other Fee2 GL">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOTHERS_2" runat="server" Text='<%# Eval("OTHERS_GL2") %>'></asp:Label>
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
        </ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>

