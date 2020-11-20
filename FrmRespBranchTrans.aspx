<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmRespBranchTrans.aspx.cs" Inherits="FrmRespBranchTrans" %>

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
        function FormatIt(obj) {
            if (obj.value.length == 2)
                obj.value = obj.value + "/";
            if (obj.value.length == 5)
                obj.value = obj.value + "/";
            if (obj.value.length == 11)
                alert("Please enter valid date");
        }

    </script>
    <script type="text/javascript">
        function IsValid() {
            var LoanBrCode = document.getElementById('<%=txtLoanBrCode.ClientID%>').value;
            var DdlCRDR = document.getElementById('<%=DdlCRDR.ClientID%>').value;
            var ddlPMTMode = document.getElementById('<%=ddlPMTMode.ClientID%>').value;
            var TxtPtype = document.getElementById('<%=TxtPtype.ClientID%>').value;
            var TxtAccNo = document.getElementById('<%=TxtAccNo.ClientID%>').value;
            var TxtNarration = document.getElementById('<%=TxtNarration.ClientID%>').value;
            var TxtAmount = document.getElementById('<%=TxtAmount.ClientID%>').value;
            var Len = parseInt(TxtChequeNo.length);
            debugger;

            if (LoanBrCode == "") {
                var message = 'Select branch first...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=ddlLoanBrName.ClientID%>').focus();
                return false;
            }

            if (DdlCRDR == "0") {
                var message = 'Select transaction type...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=DdlCRDR.ClientID%>').focus();
                return false;
            }

            if (ddlPMTMode == "0") {
                var message = 'Select payment mode first...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=ddlPMTMode.ClientID%>').focus();
                return false;
            }

            if (TxtPtype == "") {
                var message = 'Enter product type first...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtPtype.ClientID%>').focus();
                return false;
            }

            if (TxtAccNo == "") {
                var message = 'Enter account number first...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtAccNo.ClientID%>').focus();
                return false;
            }

            if (TxtAmount == "") {
                var message = 'Enter proper amount first...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtAmount.ClientID%>').focus();
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
                        IBT Module - Responding Branch
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">

                            <div class="table-scrollable" style="width: 100%; height: auto; max-height: 150px; overflow-x: auto; overflow-y: auto">
                                <table class="table table-striped table-bordered table-hover" width="100%">
                                    <thead>
                                        <tr>
                                            <th>
                                                <asp:GridView ID="grdResponding" runat="server" AlternatingRowStyle-CssClass="alt" CssClass="mGrid"
                                                    AutoGenerateColumns="false" EditRowStyle-BackColor="#FFFF99"
                                                    DataKeyNames="ID" Width="100%" EmptyDataText="No FD Available for this Customer">
                                                    <Columns>

                                                        <asp:TemplateField HeaderText="Select" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkSelect" Text="Select" CommandName="select" CommandArgument='<%#Eval("ID")%>' OnClick="lnkSelect_Click" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Br Code" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBrCode" runat="server" Text='<%# Eval("BRCD") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Gl Code" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSubGlCode" runat="server" Text='<%# Eval("SubGlCode") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="GlName" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblGlName" runat="server" Text='<%# Eval("GlName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="PARTICULARS" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblParticulars" runat="server" Text='<%# Eval("PARTICULARS2") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Credit" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCredit" runat="server" Text='<%# Eval("Credit") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Debit" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDebit" runat="server" Text='<%# Eval("Debit") %>'></asp:Label>
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

                            <div class="form-body">
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab__blue">

                                        <div class="row" style="margin-top: 5px; margin-bottom: 7px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label ">Payment Mode :<span class="required"> *</span></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="ddlPMTMode" CssClass="form-control" OnSelectedIndexChanged="ddlPMTMode_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                        <asp:ListItem Value="0">-- Select --</asp:ListItem>
                                                        <asp:ListItem Value="3">Cash</asp:ListItem>
                                                        <%--<asp:ListItem Value="5">Cheque</asp:ListItem>--%>
                                                        <asp:ListItem Value="7">Transfer</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">Transaction Type :<span class="required"> *</span></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="DdlCRDR" CssClass="form-control" OnSelectedIndexChanged="DdlCRDR_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                        <asp:ListItem Value="0">-- Select --</asp:ListItem>
                                                        <asp:ListItem Value="1">Credit</asp:ListItem>
                                                        <asp:ListItem Value="2">Debit</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">Activity :<span class="required"> *</span></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="ddlActivity" CssClass="form-control" OnSelectedIndexChanged="ddlActivity_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                        <asp:ListItem Value="0">-- Select --</asp:ListItem>
                                                        <asp:ListItem Value="1">IBT</asp:ListItem>
                                                        <asp:ListItem Value="2">General</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div id="DivCash" runat="server">
                                            <div id="DivBranch" runat="server" visible="false" class="row" style="margin-top: 5px; margin-bottom: 7px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Branch Name :<span class="required"> *</span></label>
                                                    </div>
                                                    <div class="col-lg-3">
                                                        <asp:DropDownList ID="ddlLoanBrName" CssClass="form-control" OnSelectedIndexChanged="ddlLoanBrName_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-lg-2">
                                                        <asp:TextBox ID="txtLoanBrCode" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Working Date</label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtWorkingDate" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                                        <asp:CalendarExtender ID="txtWorkingDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtWorkingDate">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 5px; margin-bottom: 7px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Product Type <span class="required">*</span></label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtPtype" PlaceHolder="Product Code" runat="server" onkeypress="javascript:return isNumber (event)" CssClass="form-control" OnTextChanged="TxtPtype_TextChanged" AutoPostBack="true" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="input-icon">
                                                            <i class="fa fa-search"></i>
                                                            <asp:TextBox ID="TxtPname" Placeholder="Search Product Name" OnTextChanged="TxtPname_TextChanged" AutoPostBack="true" runat="server" CssClass="form-control" />
                                                            <div id="CustList1" style="height: 200px; overflow-y: scroll;"></div>
                                                            <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="TxtPname" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList1" ServiceMethod="GetGlName">
                                                            </asp:AutoCompleteExtender>
                                                        </div>
                                                        <input type="hidden" id="hdfGlCode" runat="server" value="" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 5px; margin-bottom: 7px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Account Number <span class="required">*</span></label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtAccNo" onkeypress="javascript:return isNumber(event)" OnTextChanged="TxtAccNo_TextChanged" AutoPostBack="true" Placeholder="Account Number" runat="server" CssClass="form-control" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="input-icon">
                                                            <i class="fa fa-search"></i>
                                                            <asp:TextBox ID="TxtCustName" OnTextChanged="TxtCustName_TextChanged" AutoPostBack="true" Placeholder="Search Customer Name" runat="server" CssClass="form-control" />
                                                            <div id="CustList2" style="height: 200px; overflow-y: scroll;"></div>
                                                            <asp:AutoCompleteExtender ID="AutoAccname" runat="server" TargetControlID="TxtCustName" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList2" ServiceMethod="GetAccName">
                                                            </asp:AutoCompleteExtender>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-1">
                                                        <label class="control-label ">CustNo</label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtCustNo" CssClass="form-control" Enabled="false" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 5px; margin-bottom: 7px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Balance :<span class="required"> *</span></label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtBalance" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Total Balance :<span class="required"> *</span></label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtTotalBal" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 5px; margin-bottom: 7px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label ">Naration :<span class="required"> *</span></label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="TxtNarration" CssClass="form-control" runat="server" TabIndex="11" PlaceHolder="Narration"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">Amount :<span class="required"> *</span></label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="TxtAmount" CssClass="form-control" runat="server" TabIndex="12" PlaceHolder="Amount" onkeypress="javascript:return isNumber (event)" OnTextChanged="TxtAmount_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 5px; margin-bottom: 7px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label ">Credit Amount :<span class="required"> *</span></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtCRBAL" CssClass="form-control" runat="server" PlaceHolder="Credit" Enabled="false"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">Debit Amount :<span class="required"> *</span></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtDRBAL" CssClass="form-control" onkeyup="FormatIt(this)" runat="server" PlaceHolder="Debit" Enabled="false"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">Diffrence Amount :<span class="required"> *</span></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtDiff" CssClass="form-control" onkeyup="FormatIt(this)" runat="server" PlaceHolder="Diffrence Amount" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>

                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9">
                                        <asp:Button ID="Submit" runat="server" Text="Add" CssClass="btn btn-primary" OnClientClick="javascript:return IsValid()" OnClick="Submit_Click" />
                                        <asp:Button ID="btnClear" runat="server" Text="ClearAll" CssClass="btn btn-primary" OnClick="btnClear_Click" />
                                        <asp:Button ID="btnView" Enabled="false" runat="server" Text="Voucher View" CssClass="btn btn-success" OnClick="btnView_Click" />
                                        <asp:Button ID="btnPost" Enabled="false" runat="server" Text="Post" CssClass="btn btn-success" OnClick="btnPost_Click" />
                                        <asp:Button ID="Exit" runat="server" Text="Exit" CssClass="btn btn-primary" OnClick="Exit_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-lg-12">
            <div class="table-scrollable">
                <table class="table table-striped table-bordered table-hover">
                    <thead>
                        <tr>
                            <th>
                                <asp:GridView ID="grdvoucher" runat="server" AllowPaging="True" OnRowDataBound="grdvoucher_RowDataBound"
                                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                    EditRowStyle-BackColor="#FFFF99"
                                    OnPageIndexChanging="grdvoucher_PageIndexChanging"
                                    PagerStyle-CssClass="pgr" Width="100%">
                                    <Columns>

                                        <asp:TemplateField HeaderText="AT" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="AT" runat="server" Text='<%# Eval("SubGlCode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Ac No" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="ACNO" runat="server" Text='<%# Eval("AccNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Name" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="Name" runat="server" Text='<%# Eval("CustName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="AMOUNT" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="Amount" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="PARTICULARS" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="Particulars" runat="server" Text='<%# Eval("Particulars") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Entry Type" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="CRDR" runat="server" Text='<%# Eval("TrxType") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Delete" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkbtnDelete" runat="server" OnClick="lnkbtnDelete_Click" CommandArgument='<%#Eval("ID")%>' CommandName="select" class="glyphicon glyphicon-trash"></asp:LinkButton>
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
                        <div class="panel-heading">Voucher Details Before Post</div>
                        <div class="panel-body">
                            <div class="col-sm-12">
                                <div class="col-lg-12">

                                    <div class="table-scrollable" style="width: 100%; height: auto; max-height: 300px; overflow-x: auto; overflow-y: auto">
                                        <table class="table table-striped table-bordered table-hover" width="100%">
                                            <thead>
                                                <tr>
                                                    <th>
                                                        <asp:GridView ID="grdVoucherView" runat="server" AlternatingRowStyle-CssClass="alt" CssClass="mGrid" Width="100%"
                                                            AutoGenerateColumns="false" EditRowStyle-BackColor="#FFFF99" EmptyDataText="No Records Available">
                                                            <Columns>

                                                                <asp:TemplateField HeaderText="BrCode" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblBrCd" runat="server" Text='<%# Eval("BrCd") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="EntryDate" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblEDate" runat="server" Text='<%# Eval("EDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="GlCode" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSubGl" runat="server" Text='<%# Eval("SubGl") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Acc No" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAccNo" runat="server" Text='<%# Eval("AccNo") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Particulars1">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblParti1" runat="server" Text='<%# Eval("Parti1") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Particulars2">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblParti2" runat="server" Text='<%# Eval("Parti2") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Amount">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Dr/Cr">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTrxType" runat="server" Text='<%# Eval("TrxType") %>'></asp:Label>
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
                    <h4 class="modal-title">Voucher Details Screen</h4>
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
                                                                    <div class="table-scrollable" style="height: 350px; overflow-x: scroll; overflow-y: scroll; padding-bottom: 10px;">
                                                                        <asp:GridView ID="GrdView" runat="server" AutoGenerateColumns="false" ShowFooter="true" OnRowDataBound="GrdView_RowDataBound">
                                                                            <Columns>

                                                                                <asp:TemplateField HeaderText="VOUCHER NO" Visible="true">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="SETNO" runat="server" Text='<%# Eval("SETNO") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="On Date" Visible="true">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="ENTRYDATE" runat="server" Text='<%# Eval("ENTRYDATE") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="SUBGLCODE" HeaderText="Product Code" />
                                                                                <asp:BoundField DataField="ACCNO" HeaderText="A/C No" />
                                                                                <asp:BoundField DataField="CUSTNAME" HeaderText="Name" />
                                                                                <asp:BoundField DataField="PARTICULARS" HeaderText="Particulars" />

                                                                                <asp:TemplateField HeaderText="Amount" Visible="true">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="AMOUNT" runat="server" Text='<%# Eval("AMOUNT") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Type" Visible="true">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="TYPE" runat="server" Text='<%# Eval("TYPE") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="ACTIVITY" Visible="true">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="ACTIVITY" runat="server" Text='<%# Eval("ACTIVITY") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:BoundField DataField="BRCD" HeaderText="Br. Code" />
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

