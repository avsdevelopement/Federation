<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmLoanChequeReturn.aspx.cs" Inherits="FrmLoanChequeReturn" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
    <script>
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
        function IsValide() {
            var LoanProdType = document.getElementById('<%=txtLoanProdType.ClientID%>').value;
            var LoanAccNo = document.getElementById('<%=txtLoanAccNo.ClientID%>').value;
            var EntryDate = document.getElementById('<%=txtEntryDate.ClientID%>').value;
            var message = '';


            if (LoanProdType == "") {
                message = 'Enter product code first...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=txtLoanProdType.ClientID %>').focus();
                return false;
            }

            if (LoanAccNo == "") {
                message = 'Enter Account number first...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=txtLoanAccNo.ClientID %>').focus();
                return false;
            }

            if (EntryDate == "") {
                message = 'Enter entry date first...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=txtChequeDate.ClientID %>').focus();
                return false;
            }
        }

        function ValideInst() {
            var ChequeDate = document.getElementById('<%=txtChequeDate.ClientID%>').value;
            var ChequeNo = document.getElementById('<%=txtChequeNo.ClientID%>').value;
            var message = '';

            if (ChequeNo == "") {
                message = 'Enter cheque number first...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=txtChequeNo.ClientID %>').focus();
                return false;
            }

            if (ChequeDate == "") {
                message = 'Enter cheque date first...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=txtChequeDate.ClientID %>').focus();
                return false;
            }
        }

        function IsValidate() {
            var FBranch = document.getElementById('<%=txtFBranch.ClientID%>').value;
            var TBranch = document.getElementById('<%=txtTBranch.ClientID%>').value;
            var FDate = document.getElementById('<%=txtFDate.ClientID%>').value;
            var TDate = document.getElementById('<%=txtTDate.ClientID%>').value;
            var message = '';

            if (FBranch == "") {
                message = 'Enter from branch code first...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=txtFBranch.ClientID %>').focus();
                return false;
            }

            if (TBranch == "") {
                message = 'Enter to branch code first...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=txtTBranch.ClientID %>').focus();
                return false;
            }

            if (FDate == "") {
                message = 'Enter from date first...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=txtFDate.ClientID %>').focus();
                return false;
            }

            if (TDate == "") {
                message = 'Enter to date first...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=txtTDate.ClientID %>').focus();
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
                                Loan Cheque Return Entry
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
                                                        <asp:LinkButton ID="lnkCreate" runat="server" Text="AD" class="btn btn-default" OnClick="lnkCreate_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-asterisk"></i>Create New</asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton ID="lnkAuthorized" runat="server" Text="AT" class="btn btn-default" OnClick="lnkAuthorized_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-arrows"></i>Authorise</asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton ID="lnkCancel" runat="server" Text="DL" class="btn btn-default" OnClick="lnkCancel_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-pencil-square-o"></i>Cancel</asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton ID="lnkReport" runat="server" Text="RPT" class="btn btn-default" OnClick="lnkReport_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-arrows"></i>Report</asp:LinkButton>
                                                    </li>
                                                    <li class="pull-right">
                                                        <asp:Label ID="lblStst" runat="server" Text="Activity Perform :" Style="font-weight: bold;"></asp:Label>
                                                        <asp:Label ID="lblActivity" runat="server" Text=""></asp:Label>
                                                    </li>
                                                </ul>
                                            </div>

                                            <div id="DivReturn" runat="server">
                                                <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label ">Product Type :<span class="required"> *</span></label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtLoanProdType" onkeypress="javascript:return isNumber(event)" OnTextChanged="txtLoanProdType_TextChanged" AutoPostBack="true" Placeholder="Product Type" runat="server" CssClass="form-control" />
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="txtLoanProdName" Placeholder="Search Product Name" OnTextChanged="txtLoanProdName_TextChanged" AutoPostBack="true" runat="server" CssClass="form-control" />
                                                            <div id="CustList1" style="height: 200px; overflow-y: scroll;"></div>
                                                            <asp:AutoCompleteExtender ID="AutoLoanGlname" runat="server" TargetControlID="txtLoanProdName" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList1" ServiceMethod="GetGlName">
                                                            </asp:AutoCompleteExtender>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div id="Div1" class="row" style="margin-top: 5px; margin-bottom: 5px" runat="server">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label ">Acc\No:<span class="required">*</span></label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtLoanAccNo" onkeypress="javascript:return isNumber(event)" OnTextChanged="txtLoanAccNo_TextChanged" AutoPostBack="true" Placeholder="Account Number" runat="server" CssClass="form-control" />
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="txtLoanAccName" OnTextChanged="txtLoanAccName_TextChanged" AutoPostBack="true" Placeholder="Search Account Name" runat="server" CssClass="form-control" />
                                                            <div id="CustList2" style="height: 200px; overflow-y: scroll;"></div>
                                                            <asp:AutoCompleteExtender ID="AutoLoanAccName" runat="server" TargetControlID="txtLoanAccName" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList2" ServiceMethod="GetAccName">
                                                            </asp:AutoCompleteExtender>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <label class="control-label ">Customer No:<span class="required">*</span></label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtLoanCustNo" Enabled="false" Placeholder="Customer Number" runat="server" CssClass="form-control" />
                                                        </div>
                                                    </div>
                                                </div>

                                                <div id="Div2" class="row" style="margin-top: 5px; margin-bottom: 5px" runat="server">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label ">Entry Date:<span class="required"> *</span></label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="txtEntryDate" onkeyup="FormatIt(this);" onkeypress="javascript:return isNumber(event)" CssClass="form-control" PlaceHolder="DD/MM/YYYY" runat="server"></asp:TextBox>
                                                            <asp:CalendarExtender ID="txtEntryDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtChequeDate">
                                                            </asp:CalendarExtender>
                                                        </div>
                                                    </div>
                                                </div>
                                                
                                                <div id="divInst" visible="false" class="row" style="margin-top: 5px; margin-bottom: 5px" runat="server">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label ">Instrument No:<span class="required"> *</span></label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="txtChequeNo" placeholder="Cheque Number" MaxLength="6" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <label class="control-label ">Instrument Date:<span class="required"> *</span></label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="txtChequeDate" onkeyup="FormatIt(this);" onkeypress="javascript:return isNumber(event)" CssClass="form-control" PlaceHolder="DD/MM/YYYY" runat="server"></asp:TextBox>
                                                            <asp:CalendarExtender ID="txtChequeDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtChequeDate">
                                                            </asp:CalendarExtender>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>

                                            <div id="DivReport" runat="server" visible="false">

                                                <div id="Div3" class="row" style="margin-top: 5px; margin-bottom: 5px" runat="server">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label ">From Branch<span class="required"> *</span></label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="txtFBranch" placeholder="From Branch Code" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <label class="control-label ">To Branch<span class="required"> *</span></label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="txtTBranch" placeholder="To Branch Code" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div id="Div4" class="row" style="margin-top: 5px; margin-bottom: 5px" runat="server">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label ">From Date<span class="required"> *</span></label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="txtFDate" onkeyup="FormatIt(this);" onkeypress="javascript:return isNumber(event)" CssClass="form-control" PlaceHolder="DD/MM/YYYY" runat="server"></asp:TextBox>
                                                            <asp:CalendarExtender ID="txtFDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtFDate">
                                                            </asp:CalendarExtender>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <label class="control-label ">To Date<span class="required"> *</span></label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="txtTDate" onkeyup="FormatIt(this);" onkeypress="javascript:return isNumber(event)" CssClass="form-control" PlaceHolder="DD/MM/YYYY" runat="server"></asp:TextBox>
                                                            <asp:CalendarExtender ID="txtTDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtTDate">
                                                            </asp:CalendarExtender>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-actions">
                                        <div class="row">
                                            <div class="col-md-offset-3 col-md-9">
                                                <asp:Button ID="btnSubmit" Visible="false" runat="server" CssClass="btn blue" Text="Submit" OnClick="btnSubmit_Click" OnClientClick="Javascript:return IsValide();" />
                                                <asp:Button ID="btnCreate" Visible="false" runat="server" CssClass="btn blue" Text="Create" OnClick="btnCreate_Click" OnClientClick="Javascript:return ValideInst();" />
                                                <asp:Button ID="btnAuthorise" Visible="false" runat="server" CssClass="btn blue" Text="Authorise" OnClick="btnAuthorise_Click" OnClientClick="Javascript:return ValideInst();" />
                                                <asp:Button ID="btnCancel" Visible="false" runat="server" CssClass="btn blue" Text="Cancel" OnClick="btnCancel_Click" OnClientClick="Javascript:return ValideInst();" />
                                                <asp:Button ID="btnReport" Visible="false" runat="server" CssClass="btn blue" Text="Report" OnClick="btnReport_Click" OnClientClick="Javascript:return IsValidate();" />
                                                <asp:Button ID="btnClear" runat="server" CssClass="btn blue" Text="Clear All" OnClick="btnClear_Click" />
                                                <asp:Button ID="btnExit" runat="server" Text="Exit" CssClass="btn blue" OnClick="btnExit_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div id="DivGrd1" visible="false" runat="server" class="col-md-12">
                                <div class="table-scrollable" style="width: 100%; height: auto; max-height: 250px; overflow-x: auto; overflow-y: auto">
                                    <table class="table table-striped table-bordered table-hover" width="100%">
                                        <thead>
                                            <tr>
                                                <th>
                                                    <asp:GridView ID="grdLoanDDS" runat="server" AlternatingRowStyle-CssClass="alt" CssClass="mGrid"
                                                        AutoGenerateColumns="false" EditRowStyle-BackColor="#FFFF99" Width="100%" EmptyDataText="No Records Available">
                                                        <Columns>

                                                            <asp:TemplateField HeaderText="GL" Visible="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGlCode" runat="server" Text='<%# Eval("GlCode") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="SubGl" Visible="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSubGlCode" runat="server" Text='<%# Eval("SubGlCode") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                              <asp:TemplateField HeaderText="Maker" Visible="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMaker" runat="server" Text='<%# Eval("USERNAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="AccNo" Visible="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAccNo" runat="server" Text='<%# Eval("AccNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="SetNo" Visible="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSetNo" runat="server" Text='<%# Eval("SetNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Particulars" Visible="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblParticulars" runat="server" Text='<%# Eval("Particulars2") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Dr/Cr">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDbCrIndicator" runat="server" Text='<%# Eval("DbCrIndicator") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Instrument No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblInstrumentNo" runat="server" Text='<%# Eval("InstrumentNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Instrument Date" Visible="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEntryDate" runat="server" Text='<%# Eval("InstrumentDate") %>'></asp:Label>
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

