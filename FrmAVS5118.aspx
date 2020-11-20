<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAVS5118.aspx.cs" Inherits="FrmAVS5118" %>

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
                        PO / DD Issue Screen
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">

                                    <div id="divNewInfo" runat="server" visible="true">
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Select : <span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:Button ID="btnAddNew" runat="server" CssClass="btn default" Text="Add new " OnClick="btnAddNew_Click" AccessKey="1" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div id="divDetailInfo" runat="server" visible="false">
                                        <div style="border: 1px solid #3598dc">
                                            <div class="portlet-body">
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Debit Account Details: </strong></div>
                                                    </div>
                                                </div>

                                                <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:RadioButtonList ID="rbtnType" runat="server" OnSelectedIndexChanged="rbtnType_SelectedIndexChanged" AutoPostBack="true" RepeatDirection="Horizontal" Style="width: 300px;">
                                                                <asp:ListItem Text="DD Issue" Value="DD" Selected="True" />
                                                                <asp:ListItem Text="PO Issue" Value="PO" />
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">
                                                        <label class="control-label col-md-2" style="width: 140px">Debit Prod Type <span class="required">*</span></label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtDrProdType" runat="server" CssClass="form-control" OnTextChanged="txtDrProdType_TextChanged" AutoPostBack="true" onkeypress="javascript:return isNumber(event)" Placeholder="Account Type"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="input-icon">
                                                                <i class="fa fa-search"></i>
                                                                <asp:TextBox ID="txtDrProdName" runat="server" CssClass="form-control" OnTextChanged="txtDrProdName_TextChanged" AutoPostBack="true" Placeholder="Search Product Name" onkeypress="return checkQuote();"></asp:TextBox>
                                                                <div id="CustList1" style="height: 200px; overflow-y: scroll;"></div>
                                                                <asp:AutoCompleteExtender ID="AutoDrGlName" runat="server" TargetControlID="txtDrProdName" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                    MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" ServiceMethod="GetGlWiseName" CompletionListElementID="CustList1">
                                                                </asp:AutoCompleteExtender>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">
                                                        <label class="control-label col-md-2" style="width: 140px">Debit Acc No <span class="required">*</span></label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtDrAccNo" runat="server" CssClass="form-control" OnTextChanged="txtDrAccNo_TextChanged" AutoPostBack="true" onkeypress="javascript:return isNumber(event)" Placeholder="Account Number" />
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="input-icon">
                                                                <i class="fa fa-search"></i>
                                                                <asp:TextBox ID="txtDrAccName" runat="server" CssClass="form-control" OnTextChanged="txtDrAccName_TextChanged" AutoPostBack="true" Placeholder="Search Account Name" onkeypress="return checkQuote();" />
                                                                <div id="CustList2" style="height: 200px; overflow-y: scroll;"></div>
                                                                <asp:AutoCompleteExtender ID="AutoDrAccName" runat="server" TargetControlID="txtDrAccName" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                    MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" ServiceMethod="GetProdWiseName" CompletionListElementID="CustList2">
                                                                </asp:AutoCompleteExtender>
                                                            </div>
                                                        </div>
                                                        <label class="control-label col-md-1">Cust No </label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtDrCustNo" Enabled="false" runat="server" CssClass="form-control" Placeholder="Customer Number" />
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">
                                                        <label class="control-label col-md-2" style="width: 140px">Account Balance </label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtTotBalance" Enabled="false" runat="server" CssClass="form-control" Placeholder="Account Balance" />
                                                        </div>
                                                        <label class="control-label col-md-2">Cheque Amount <span class="required">*</span></label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtChequeAmt" runat="server" CssClass="form-control" Placeholder="Cheque Amount" OnTextChanged="txtChequeAmt_TextChanged" AutoPostBack="true" onkeypress="javascript:return isNumber(event)" />
                                                        </div>
                                                        <label class="control-label col-md-1">Cheque No</label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtChequeNo" runat="server" CssClass="form-control" Placeholder="Cheque Number" onkeypress="javascript:return isNumber(event)" />
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>

                                        <div style="border: 1px solid #3598dc">
                                            <div class="portlet-body">
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Beneficiary Details: </strong></div>
                                                    </div>
                                                </div>

                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">
                                                        <label class="control-label col-md-2" style="width: 140px">Beneficiary Name</label>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtBenefName" runat="server" CssClass="form-control" placeholder="Beneficiary Name" onkeypress="return checkQuote();" />
                                                        </div>
                                                        <label class="control-label col-md-1">Narration</label>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtNarration" runat="server" CssClass="form-control" placeholder="Narration" onkeypress="return checkQuote();" />
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                        
                                        <div style="border: 1px solid #3598dc">
                                            <div class="portlet-body">
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Remittance Details: </strong></div>
                                                    </div>
                                                </div>

                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">
                                                        <label class="control-label col-md-1" style="width: 85px">Br Code <span class="required">*</span></label>
                                                        <div class="col-md-2">
                                                            <asp:DropDownList ID="ddlCrBrName" CssClass="form-control" OnSelectedIndexChanged="ddlCrBrName_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <label class="control-label col-md-1">Prod Type <span class="required">*</span></label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtCrProdType" runat="server" CssClass="form-control" OnTextChanged="txtCrProdType_TextChanged" AutoPostBack="true" Placeholder="Account Type" onkeypress="javascript:return isNumber(event)"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="input-icon">
                                                                <i class="fa fa-search"></i>
                                                                <asp:TextBox ID="txtCrProdName" runat="server" CssClass="form-control" OnTextChanged="txtCrProdName_TextChanged" AutoPostBack="true" Placeholder="Search Product Name" onkeypress="return checkQuote();"></asp:TextBox>
                                                                <div id="CustList3" style="height: 200px; overflow-y: scroll;"></div>
                                                                <asp:AutoCompleteExtender ID="AutoCrGlName" runat="server" TargetControlID="txtCrProdName" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                    MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" ServiceMethod="GetGlWiseName" CompletionListElementID="CustList3">
                                                                </asp:AutoCompleteExtender>
                                                            </div>
                                                        </div>
                                                        <label class="control-label col-md-1">DD NO</label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtDDNo" CssClass="form-control" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                                
                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">
                                                        <label class="control-label col-md-1" style="width: 85px">Charges</label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtCharges" runat="server" CssClass="form-control" placeholder="Charges Amount" OnTextChanged="txtCharges_TextChanged" AutoPostBack="true" onkeypress="javascript:return isNumber(event)" />
                                                        </div>
                                                        <label class="control-label col-md-1">CGST</label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtCGSTChrg" Enabled="false" runat="server" CssClass="form-control" placeholder="CGST Charge" />
                                                        </div>
                                                        <label class="control-label col-md-1">SGST</label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtSGSTChrg" Enabled="false" runat="server" CssClass="form-control" placeholder="SGST Charge" />
                                                        </div>
                                                        <label class="control-label col-md-1">Total Chrg</label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtTotalChrg" Enabled="false" runat="server" CssClass="form-control" placeholder="Total charges" />
                                                        </div>
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
                                        <asp:Button ID="btnSubmit" Visible="false" runat="server" CssClass="btn blue" Text="Submit" OnClick="btnSubmit_Click" OnClientClick="Javascript:return IsValide(); CheckConfirm(this);" />
                                        <asp:Button ID="btnAuthorize" Visible="false" runat="server" CssClass="btn blue" Text="Authorize" OnClick="btnAuthorize_Click" />
                                        <asp:Button ID="btnDelete" Visible="false" runat="server" CssClass="btn blue" Text="Delete" OnClick="btnDelete_Click" />
                                        <asp:Button ID="btnClear" Visible="false" runat="server" CssClass="btn blue" Text="Clear All" OnClick="btnClear_Click" />
                                        <asp:Button ID="btnExit" Visible="false" runat="server" CssClass="btn blue" Text="Exit" OnClick="btnExit_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>

    <div class="row" id="divAuthoDetail" runat="server" visible="true">
        <div class="col-md-12">
            <div class="table-scrollable" style="height: auto; max-height: 200px; overflow-x: auto; overflow-y: auto">
                <asp:Label ID="lblAuthorize" runat="server" Text="Need to Authorize" BackColor="#66ccff" Font-Bold="true" Font-Size="Medium"></asp:Label>
                <table class="table table-striped table-bordered table-hover" width="100%">
                    <thead>
                        <tr>
                            <th>
                                <asp:GridView ID="grdIssued" runat="server" CssClass="mGrid" CellPadding="6" CellSpacing="7" PageIndex="5" ForeColor="#333333"
                                    AutoGenerateColumns="False" BorderWidth="1px" BorderColor="#333300" Width="100%" ShowFooter="true">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>

                                        <asp:TemplateField HeaderStyle-Width="30px" HeaderText="SrNo" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblSrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Width="50px" HeaderText="TransType" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblTransType" runat="server" Text='<%# Eval("TransType") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Width="80px" HeaderText="IssueDate" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblIssueDate" runat="server" Text='<%# Eval("IssueDate") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Width="80px" HeaderText="Prod Code" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblIssueSubGlCode" runat="server" Text='<%# Eval("IssueSubGlCode") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Width="80px" HeaderText="Acc No" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblIssueAccNo" runat="server" Text='<%# Eval("IssueAccNo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Width="100px" HeaderText="Amount" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblChequeAmt" runat="server" Text='<%# Eval("ChequeAmt") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Width="80px" HeaderText="ChequeNo" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblChequeNo" runat="server" Text='<%# Eval("ChequeNo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Width="150px" HeaderText="Benef Name" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblBenefName" runat="server" Text='<%# Eval("BenefName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Width="100px" HeaderText="Authorise" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown" >
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkAutorise" runat="server" CommandArgument='<%#Eval("SrNumber")+","+ Eval("Mid")%>' CommandName="select" class="glyphicon glyphicon-pencil" OnClick="lnkAutorise_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Width="100px" HeaderText="Delete" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Eval("SrNumber")+","+ Eval("Mid")%>' CommandName="select" class="glyphicon glyphicon-trash" OnClick="lnkDelete_Click"></asp:LinkButton>
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

