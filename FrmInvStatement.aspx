<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmInvStatement.aspx.cs" Inherits="FrmInvStatement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function isNumber(evt)
        {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }

        function FormatIt(obj)
        {
            if (obj.value.length == 2) // Day
                obj.value = obj.value + "/";
            if (obj.value.length == 5) // month 
                obj.value = obj.value + "/";
            if (obj.value.length == 11) // year 
                alert("Please Enter valid Date");
        }
    </script>

    <script type="text/javascript">
        function IsValidate()
        {
            var BrCode = document.getElementById('<%=txtBrCode.ClientID%>').value;
            var PrCode = document.getElementById('<%=txtProdCode.ClientID%>').value;
            var AccNo = document.getElementById('<%=txtAccNo.ClientID%>').value;
            var FrDate = document.getElementById('<%=txtFromDate.ClientID%>').value;
            var ToDate = document.getElementById('<%=txtToDate.ClientID%>').value;
            var message = '';

            if (BrCode == "")
            {
                message = 'Enter branch code first...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtBrCode.ClientID%>').focus();
                return false;
            }

            if (PrCode == "")
            {
                message = 'Enter product code first...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtProdCode.ClientID%>').focus();
                return false;
            }

            if (AccNo == "")
            {
                message = 'Enter account no first...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtAccNo.ClientID%>').focus();
                return false;
            }

            if (FrDate == "")
            {
                message = 'Enter from date first...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtFromDate.ClientID%>').focus();
                return false;
            }

            if (ToDate == "")
            {
                message = 'Enter to date first...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtToDate.ClientID%>').focus();
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
                               Investment Statement View
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
                                                    <label class="control-label col-md-1">Product No<span class="required">*</span></label>
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
                                                    <label class="control-label col-md-1">Clear Bal</label>
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
                                                    <label class="control-label col-md-1">Account No<span class="required">*</span></label>
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
                                                    <label class="control-label col-md-1">UnClear Bal</label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtUnClearBal" Enabled="false" runat="server" CssClass="form-control" />
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
                                                    <label class="control-label col-md-1">From Date<span class="required">*</span></label>
                                                    <div class="col-lg-2">
                                                        <asp:TextBox ID="txtFromDate" MaxLength="10" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="DD/MM/YYYY" runat="server" />
                                                    </div>
                                                    <label class="control-label col-md-1">To Date<span class="required">*</span></label>
                                                    <div class="col-lg-2">
                                                        <asp:TextBox ID="txtToDate" MaxLength="10" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="DD/MM/YYYY" runat="server" />
                                                    </div>
                                                    <label class="control-label col-md-1">Acc Status</label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtAccStatus" Enabled="false" runat="server" CssClass="form-control" />
                                                    </div>
                                                    <div id="DivMaturity" runat="server" visible="false">
                                                        <label id="LblName4" runat="server" class="control-label col-md-1"></label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtInstallmentAmt" Enabled="false" runat="server" CssClass="form-control" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-12">
                                                    <div class="col-lg-1"></div>
                                                    <div id="DivOverDue" runat="server" visible="false">
                                                        <label class="control-label col-md-1">OverDue </label>
                                                        <div class="col-lg-2">
                                                            <asp:TextBox ID="txtOverDue" Enabled="false" CssClass="form-control" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div id="DivReceipt" runat="server" visible="false">
                                                        <label class="control-label col-md-1">Receipt No </label>
                                                        <div class="col-lg-2">
                                                            <asp:TextBox ID="txtRecNo" CssClass="form-control" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-actions">
                                        <div class="row">
                                            <div class="col-md-offset-3 col-md-9">
                                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn blue" Text="Submit" OnClick="btnSubmit_Click" OnClientClick="Javascript:return IsValidate();" />
                                                <asp:Button ID="btnLazerReport" runat="server" CssClass="btn blue" Text="Lazer" OnClick="btnLazerReport_Click" OnClientClick="Javascript:return IsValidate();" />
                                                <asp:Button ID="btnClear" runat="server" CssClass="btn blue" Text="Clear All" OnClick="btnClear_Click" />
                                                <asp:Button ID="btnExit" runat="server" CssClass="btn btn-primary" Text="Exit" OnClick="btnExit_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                            </div>
                        </div>
                    </div>
                </div>

                <div id="divSavingStmt" class="col-md-12" runat="server" visible="false">
                    <div class="table-scrollable" style="width: 100%; height: auto; max-height: 500px; overflow-x: auto; overflow-y: auto">
                        <table class="table table-striped table-bordered table-hover" width="100%">
                            <thead>
                                <tr>
                                    <th>
                                        <asp:GridView ID="grdSaving" runat="server" AlternatingRowStyle-CssClass="alt" CssClass="mGrid"
                                            ShowFooter="true" AutoGenerateColumns="false" EditRowStyle-BackColor="#FFFF99" Width="100%">
                                            <Columns>

                                                <asp:TemplateField HeaderStyle-Width="30" HeaderText="View" runat="server">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkSelect1" runat="server" CommandName="select" CommandArgument='<%#Eval("BrCode")+"_"+ Eval("EntryDate")+"_"+ Eval("SetNo")%>' OnClick="lnkSelect1_Click" class="glyphicon glyphicon-check"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-Width="100px" HeaderText="EntryDate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEntryDate1" runat="server" Text='<%# Eval("EntryDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-Width="50px" HeaderText="SetNo">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSetNo1" runat="server" Text='<%# Eval("SetNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-Width="400px" HeaderText="Particular">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblParticular1" runat="server" Text='<%# Eval("Particular") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-Width="100px" HeaderText="Credit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPCredit1" runat="server" Text='<%# Eval("PCredit") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="Label1" Text="lblPCreditTotal1" runat="server" />
                                                    </FooterTemplate>
                                                    <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                    <FooterStyle CssClass="Th" HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-Width="100px" HeaderText="Debit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPDebit1" runat="server" Text='<%# Eval("PDebit") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblPDebitTotal1" Text="lblPCreditTotal" runat="server" />
                                                    </FooterTemplate>
                                                    <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                    <FooterStyle CssClass="Th" HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-Width="100px" HeaderText="Balance">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPBalance1" runat="server" Text='<%# Eval("PBalance") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-Width="30px" HeaderText="DrCr">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPDrCr1" runat="server" Text='<%# Eval("PDrCr") %>'></asp:Label>
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

                <div id="divStmtGrd" class="col-md-12" runat="server" visible="false">
                    <div class="table-scrollable" style="width: 100%; height: auto; max-height: 500px; overflow-x: auto; overflow-y: auto">
                        <table class="table table-striped table-bordered table-hover" width="100%">
                            <thead>
                                <tr>
                                    <th>
                                        <asp:GridView ID="grdStmtGrd" runat="server" AlternatingRowStyle-CssClass="alt" CssClass="mGrid" ShowFooter="true"
                                            OnDataBound="grdStmtGrd_DataBound" AutoGenerateColumns="false" EditRowStyle-BackColor="#FFFF99" Width="100%">
                                            <Columns>

                                                <asp:TemplateField HeaderStyle-Width="30" HeaderText="View" runat="server">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkSelect2" runat="server" CommandName="select" CommandArgument='<%#Eval("BrCode")+"_"+ Eval("EntryDate")+"_"+ Eval("SetNo")%>' OnClick="lnkSelect2_Click" class="glyphicon glyphicon-check"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-Width="100px" HeaderText="EntryDate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEntryDate2" runat="server" Text='<%# Eval("EntryDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-Width="50px" HeaderText="SetNo">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSetNo2" runat="server" Text='<%# Eval("SetNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-Width="200px" HeaderText="Particular">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblParticular2" runat="server" Text='<%# Eval("Particular") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-Width="100px" HeaderText="Credit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPCredit2" runat="server" Text='<%# Eval("PCredit") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblPCreditTotal2" Text="lblPCreditTotal" runat="server" />
                                                    </FooterTemplate>
                                                    <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                    <FooterStyle CssClass="Th" HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-Width="100px" HeaderText="Debit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPDebit2" runat="server" Text='<%# Eval("PDebit") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblPDebitTotal2" Text="lblPCreditTotal" runat="server" />
                                                    </FooterTemplate>
                                                    <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                    <FooterStyle CssClass="Th" HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-Width="100px" HeaderText="Balance">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPBalance2" runat="server" Text='<%# Eval("PBalance") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-Width="20px" HeaderText="DrCr">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPDrCr2" runat="server" Text='<%# Eval("PDrCr") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-Width="100px" HeaderText="Credit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblICredit2" runat="server" Text='<%# Eval("ICredit") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblICreditTotal2" Text="lblPCreditTotal" runat="server" />
                                                    </FooterTemplate>
                                                    <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                    <FooterStyle CssClass="Th" HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-Width="100px" HeaderText="Debit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIDebit2" runat="server" Text='<%# Eval("IDebit") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblIDebitTotal2" Text="lblPCreditTotal" runat="server" />
                                                    </FooterTemplate>
                                                    <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                    <FooterStyle CssClass="Th" HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-Width="100px" HeaderText="Balance">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIBalance2" runat="server" Text='<%# Eval("IBalance") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-Width="20px" HeaderText="DrCr">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIDrCr2" runat="server" Text='<%# Eval("IDrCr") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-Width="100px" HeaderText="Total Balance">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTotalBalance2" runat="server" Text='<%# Eval("TotalBalance") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-Width="20px" HeaderText="DrCr">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTDrCr2" runat="server" Text='<%# Eval("PDrCr") %>'></asp:Label>
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

                <div class="modal fade bs-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" style="margin-left: 1%; width: 98%">
                    <div class="modal-dialog modal-lg" role="document" style="width: 96%">
                        <div class="modal-content" style="border: 5px solid #4dbfc0;">
                            <div class="inner_top">
                                <div class="panel panel-default">
                                    <div class="panel-heading">Voucher Details</div>
                                    <div class="panel-body">
                                        <div class="col-sm-12">
                                            <div class="col-lg-12">

                                                <div class="table-scrollable" style="width: 100%; height: auto; max-height: 300px; overflow-x: auto; overflow-y: auto">
                                                    <table class="table table-striped table-bordered table-hover" width="100%">
                                                        <thead>
                                                            <tr>
                                                                <th>
                                                                    <asp:GridView ID="grdVoucherDetails" runat="server" AlternatingRowStyle-CssClass="alt" CssClass="mGrid" Width="100%"
                                                                        AutoGenerateColumns="false" EditRowStyle-BackColor="#FFFF99" ShowFooter="true" EmptyDataText="No Records Available">
                                                                        <Columns>

                                                                            <asp:TemplateField HeaderStyle-Width="100" HeaderText="EntryDate">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblVEntryDate" runat="server" Text='<%# Eval("EntryDate") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderStyle-Width="80" HeaderText="SetNo">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblVSetNo" runat="server" Text='<%# Eval("SetNo") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderStyle-Width="80" HeaderText="GlCode">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblVGlCode" runat="server" Text='<%# Eval("GlCode") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderStyle-Width="80" HeaderText="Prod Code">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblVSubGlCode" runat="server" Text='<%# Eval("SubGlCode") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderStyle-Width="80" HeaderText="AccNo" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblVAccNo" runat="server" Text='<%# Eval("AccNo") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderStyle-Width="200" HeaderText="Particulars">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lvlVParti" runat="server" Text='<%# Eval("Parti") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderStyle-Width="100" HeaderText="InstNo">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblVInstNo" runat="server" Text='<%# Eval("InstNo") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderStyle-Width="100" HeaderText="InstDate">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblVInstDate" runat="server" Text='<%# Eval("InstDate") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderStyle-Width="100" HeaderText="Credit">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblVCredit" runat="server" Text='<%# Eval("Credit") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderStyle-Width="100" HeaderText="Debit">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblVDebit" runat="server" Text='<%# Eval("Debit") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderStyle-Width="80" HeaderText="Stage">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblVStage" runat="server" Text='<%# Eval("Stage") %>'></asp:Label>
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

                <table class="table table-striped table-bordered table-hover">
                    <thead>
                        <tr>
                            <th>
                                <asp:GridView ID="GRdMonthly" runat="server" AllowPaging="True" Visible="false"
                                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                    EditRowStyle-BackColor="#FFFF99"
                                    OnPageIndexChanging="GRdMonthly_PageIndexChanging"
                                    PageIndex="10" PageSize="25"
                                    PagerStyle-CssClass="pgr" Width="100%">
                                    <Columns>

                                        <asp:TemplateField HeaderText="Year" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="Year" runat="server" Text='<%# Eval("Year") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Month" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="Month" runat="server" Text='<%# Eval("Month") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="GlName">
                                            <ItemTemplate>
                                                <asp:Label ID="GlName" runat="server" Text='<%# Eval("GlName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Acc No">
                                            <ItemTemplate>
                                                <asp:Label ID="ACCNO" runat="server" Text='<%# Eval("ACCNO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="OpeningBal">
                                            <ItemTemplate>
                                                <asp:Label ID="OpeningBal" runat="server" Text='<%# Eval("OpeningBal") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Credit">
                                            <ItemTemplate>
                                                <asp:Label ID="Credit" runat="server" Text='<%# Eval("Credit") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Debit" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="Debit" runat="server" Text='<%# Eval("Debit") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="ClosingBal">
                                            <ItemTemplate>
                                                <asp:Label ID="ClosingBal" runat="server" Text='<%# Eval("ClosingBal") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Right" />
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

