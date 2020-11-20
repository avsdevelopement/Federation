<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAVS5028.aspx.cs" Inherits="FrmAVS5028" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function isNumber(evt)
        {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;
            return true;
        }
    </script>
    <script type="text/javascript">
        function FormatIt(obj)
        {
            if (obj.value.length == 2)
                obj.value = obj.value + "/";
            if (obj.value.length == 5)
                obj.value = obj.value + "/";
            if (obj.value.length == 11)
                alert("Please enter valid date");
        }
    </script>

    <script type="text/javascript">
        function IsValidation() {
            var TxtPtype = document.getElementById('<%=TxtPtype.ClientID%>').value;
            var TxtAppNo = document.getElementById('<%=TxtAppNo.ClientID%>').value;
            var TxtCustno = document.getElementById('<%=TxtCustno.ClientID%>').value;
            var TxtMemNo = document.getElementById('<%=TxtMemNo.ClientID%>').value;
            var TxtLoanSanc = document.getElementById('<%=TxtLoanSanc.ClientID%>').value;
            var TxtBondNo = document.getElementById('<%=TxtBondNo.ClientID%>').value;
            var TxtNetPaid = document.getElementById('<%=TxtNetPaid.ClientID%>').value;
            var ddlPayType = document.getElementById('<%=ddlPayType.ClientID%>').value;

            if (TxtPtype == "")
            {
                var message = 'Enter product code first...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtPtype.ClientID%>').focus();
                return false;
            }

            if (TxtAppNo == "")
            {
                var message = 'Enter application first...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtAppNo.ClientID%>').focus();
                return false;
            }

            if (TxtCustno == "")
            {
                var message = 'Enter customer number first...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtCustno.ClientID%>').focus();
                return false;
            }

            if (TxtMemNo == "")
            {
                var message = 'Enter member number first...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtMemNo.ClientID%>').focus();
                return false;
            }

            if (TxtLoanSanc == "")
            {
                var message = 'Enter loan sanction amount first...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtLoanSanc.ClientID%>').focus();
                return false;
            }

            if (TxtBondNo == "")
            {
                var message = 'Enter bond number first...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtBondNo.ClientID%>').focus();
                return false;
            }

            if (TxtNetPaid == "")
            {
                var message = 'Enter net paid amount first...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtNetPaid.ClientID%>').focus();
                return false;
            }

            if (ddlPayType == "")
            {
                var message = 'Select proper payment mode first...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=ddlPayType.ClientID%>').focus();
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="Div1">
                <div class="portlet-title">
                    <div class="caption">
                        Loan Bond Issue
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab1">
                                        <asp:Table ID="tbl_MainWindow" runat="server">
                                            <asp:TableRow ID="tbl_Row1" runat="server">

                                                <asp:TableCell ID="tbl_Col1" runat="server" Width="70%" BorderStyle="Solid" BorderWidth="1px">

                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Application Details : </strong></div>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-2" style="padding-left: 0px;">
                                                                <label class="control-label " style="padding-left: 0px">Product Code:<span class="required">*</span></label>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="TxtPtype" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" OnTextChanged="TxtPtype_TextChanged" PlaceHolder="Product Code" AutoPostBack="true"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="input-icon">
                                                                    <i class="fa fa-search"></i>
                                                                    <asp:TextBox ID="TxtPname" runat="server" CssClass="form-control" AutoPostBack="true" PlaceHolder="Product Name" OnTextChanged="TxtPname_TextChanged"></asp:TextBox>
                                                                    <div id="CustList" style="height: 200px; overflow-y: scroll;"></div>
                                                                    <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="TxtPname"
                                                                        UseContextKey="true"
                                                                        CompletionInterval="1"
                                                                        CompletionSetCount="20"
                                                                        MinimumPrefixLength="1"
                                                                        EnableCaching="true"
                                                                        ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList"
                                                                        ServiceMethod="GetGlName">
                                                                    </asp:AutoCompleteExtender>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-2" style="padding-left: 0px;">
                                                                <label class="control-label ">App No:<span class="required">*</span></label>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="TxtAppNo" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" PlaceHolder="Application No" AutoPostBack="true" OnTextChanged="TxtAppNo_TextChanged"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <label class="control-label ">Branch:<span class="required">*</span></label>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="TxtBrcd" Enabled="false" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" OnTextChanged="TxtBrcd_TextChanged" PlaceHolder="Branch Code" AutoPostBack="true"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="TxtBrcdName" Enabled="false" runat="server" CssClass="form-control" PlaceHolder="Branch Name"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-2" style="padding-left: 0px;">
                                                                <label class="control-label ">CustNo:<span class="required">*</span></label>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="TxtCustno" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" PlaceHolder="Customer No" OnTextChanged="TxtCustno_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="TxtCustname" runat="server" CssClass="form-control" PlaceHolder="Customer Name" ReadOnly="true"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <label class="control-label ">MemberNo:<span class="required">*</span></label>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="TxtMemNo" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" PlaceHolder="Member No"></asp:TextBox>
                                                            </div>
                                                        </div>

                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Direct Liabilities : </strong></div>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin-top: 12px; margin-bottom: 10px; height: 100px; margin-left: 5px; width: 95%; overflow: auto">
                                                        <div class="col-lg-12">
                                                            <asp:GridView ID="GrdDirectLiab" runat="server" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                                EditRowStyle-BackColor="#FFFF99"
                                                                PagerStyle-CssClass="pgr" Width="100%" ShowFooter="true">
                                                                <Columns>

                                                                    <asp:TemplateField HeaderText="SrNo" Visible="true" FooterText="Sub Total" FooterStyle-Font-Bold="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSrNo1" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Brcd" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblBrcd" runat="server" Text='<%# Eval("BRCD") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Prod Code" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPrdcd" runat="server" Text='<%# Eval("subglcode") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Accno" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblAccno" runat="server" Text='<%# Eval("accno") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Opening Dt" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblOPdt" runat="server" Text='<%# Eval("OPENINGDATE") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Due Dt" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblduedt" runat="server" Text='<%# Eval("DUEDATE") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Sanction Amt" Visible="true" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblsancamt" runat="server" Text='<%# Eval("LIMIT") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <div style="padding: 0 0 5px 0">
                                                                                <asp:Label ID="Lbl_TotalLimit" runat="server" />
                                                                            </div>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Installment Amt" Visible="true" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblInstAmt" runat="server" Text='<%# Eval("INSTALLMENT") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <div style="padding: 0 0 5px 0">
                                                                                <asp:Label ID="Lbl_TotaliNSTALL" runat="server" />
                                                                            </div>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Outstanding bal" Visible="true" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbloutbal" runat="server" Text='<%# Eval("BALANCE") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <div style="padding: 0 0 5px 0">
                                                                                <asp:Label ID="Lbl_TotalBal" runat="server" />
                                                                            </div>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="OD Amt" Visible="true" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblodamt" runat="server" Text='<%# Eval("ODAMT") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <div style="padding: 0 0 5px 0">
                                                                                <asp:Label ID="Lbl_TotalOD" runat="server" />
                                                                            </div>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="No of Inst" Visible="true" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblnoofinst" runat="server" Text='<%# Eval("TOTALINST") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                </Columns>
                                                                <PagerStyle CssClass="pgr" />
                                                                <HeaderStyle BackColor="#ffce9d" />
                                                                <FooterStyle BackColor="#bbffff" />
                                                                <SelectedRowStyle BackColor="#66FF99" />
                                                                <EditRowStyle BackColor="#FFFF99" />
                                                                <AlternatingRowStyle CssClass="alt" />
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">From Surity : </strong></div>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin-top: 12px; margin-bottom: 10px; margin-left: 5px; height: 100px; width: 95%; overflow: auto">
                                                        <div class="col-lg-12">
                                                            <asp:GridView ID="GrdFromSurity" runat="server" AllowPaging="True" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                                EditRowStyle-BackColor="#FFFF99"
                                                                PagerStyle-CssClass="pgr" Width="100%">
                                                                <Columns>

                                                                    <asp:TemplateField HeaderText="SrNo" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSrNo1" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Prod Code" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPRODCODE" runat="server" Text='<%# Eval("LOANGLCODE") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Accno" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblLOANACCNO" runat="server" Text='<%# Eval("LOANACCNO") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Custno" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCUSTNO" runat="server" Text='<%# Eval("MemberNo") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Account Holder Name" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCUSTNAME" runat="server" Text='<%# Eval("CUSTNAME") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Loan Amount" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblLIMIT" runat="server" Text='<%# Eval("LIMIT") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Loan Date" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSANSSIONDATE" runat="server" Text='<%# Eval("SANSSIONDATE") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Balance" Visible="false" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblBALANCE" runat="server" Text='<%# Eval("BALANCE") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="OverDue Amount" Visible="false" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblOVERDUEDATE" runat="server" Text='<%# Eval("OVERDUE") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                </Columns>
                                                                <PagerStyle CssClass="pgr" />
                                                                <HeaderStyle BackColor="#ffce9d" />
                                                                <SelectedRowStyle BackColor="#66FF99" />
                                                                <EditRowStyle BackColor="#FFFF99" />
                                                                <AlternatingRowStyle CssClass="alt" />
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                    <%--   <div class="row">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Personal Details : </strong></div>
                                                        </div>
                                                    </div>--%>

                                                    <%--<div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-2">
                                                                <label class="control-label ">CustNo:<span class="required"> *</span></label>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="TxtCustno" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" PlaceHolder="Customer No"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="TxtCustname" runat="server" CssClass="form-control" PlaceHolder="Customer Name" ReadOnly="true"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <label class="control-label ">MemberNo:<span class="required"> *</span></label>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="TxtMemNo" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" PlaceHolder="Member No"></asp:TextBox>
                                                            </div>
                                                            <%--<div class="col-md-3">
                                                    <asp:TextBox ID="txtMemmName" runat="server" CssClass="form-control"  PlaceHolder="Member Name" ReadOnly="true"></asp:TextBox>
                                                </div>--%>

                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Loan Disbursement : </strong></div>
                                                        </div>
                                                    </div>

                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-2" style="padding-left: 0px;">
                                                                <label class="control-label ">LoanSanction:<span class="required">*</span></label>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <asp:TextBox ID="TxtLoanSanc" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" PlaceHolder="Sanction Amount"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <label class="control-label ">BondNo:<span class="required">*</span></label>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="TxtBondNo" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" PlaceHolder="Bornd No"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-2" style="padding-left: 0px;">
                                                                <label class="control-label ">Deduction:<span class="required">*</span></label>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <asp:TextBox ID="TxtDeduct" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" PlaceHolder="Deduction"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <label class="control-label ">NetPaid:<span class="required">*</span></label>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <asp:TextBox ID="TxtNetPaid" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" PlaceHolder="Net Paid" Enabled="false"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Payment Mode : </strong></div>
                                                        </div>
                                                    </div>

                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-2" style="padding-left: 0px;">
                                                                <label class="control-label ">Payment Type :<span class="required">*</span></label>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:DropDownList ID="ddlPayType" OnSelectedIndexChanged="ddlPayType_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div id="Transfer" visible="false" runat="server">

                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-12">
                                                                <div class="col-md-2" style="padding-left: 0px;">
                                                                    <label class="control-label ">Product Code :<span class="required">*</span></label>
                                                                </div>
                                                                <div class="col-md-2">
                                                                    <asp:TextBox ID="txtProdType1" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtProdType1_TextChanged" PlaceHolder="Product Type"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <div class="input-icon">
                                                                        <i class="fa fa-search"></i>
                                                                        <asp:TextBox ID="txtProdName1" CssClass="form-control" PlaceHolder="Search Product Name" OnTextChanged="txtProdName1_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                                                        <div id="CustList3" style="height: 200px; overflow-y: scroll;"></div>
                                                                        <asp:AutoCompleteExtender ID="autoglname1" runat="server" TargetControlID="txtProdName1" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                            MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList3" ServiceMethod="GetGlName">
                                                                        </asp:AutoCompleteExtender>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-12">
                                                                <div class="col-md-2" style="padding-left: 0px;">
                                                                    <label class="control-label ">Account No:<span class="required">*</span></label>
                                                                </div>
                                                                <div class="col-md-2">
                                                                    <asp:TextBox ID="TxtAccNo1" CssClass="form-control" PlaceHolder="ID" runat="server" AutoPostBack="true" OnTextChanged="TxtAccNo1_TextChanged"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <div class="input-icon">
                                                                        <i class="fa fa-search"></i>
                                                                        <asp:TextBox ID="TxtAccName1" CssClass="form-control" PlaceHolder="Search Customer Name" runat="server" AutoPostBack="true" OnTextChanged="TxtAccName1_TextChanged"></asp:TextBox>
                                                                        <div id="CustList4" style="height: 200px; overflow-y: scroll;"></div>
                                                                        <asp:AutoCompleteExtender ID="AutoAccname1" runat="server" TargetControlID="TxtAccName1" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                            MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList4" ServiceMethod="GetAccName">
                                                                        </asp:AutoCompleteExtender>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-2">
                                                                    <label class="control-label ">Balance:<span class="required">*</span></label>
                                                                </div>
                                                                <div class="col-md-2">
                                                                    <asp:TextBox ID="txtBalance" CssClass="form-control" PlaceHolder="Balance" runat="server" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div id="Transfer1" visible="false" runat="server">
                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-12">
                                                                <div class="col-md-2" style="padding-left: 0px;">
                                                                    <label class="control-label ">Instrument No. :</label>
                                                                </div>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="TxtChequeNo" MaxLength="6" placeholder="CHEQUE NUMBER" OnTextChanged="TxtChequeNo_TextChanged"  CssClass="form-control" runat="server" AutoPostBack="true" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-3">
                                                                    <label class="control-label ">Instrument Date :</label>
                                                                </div>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="TxtChequeDate" placeholder="CHEQUE DATE" CssClass="form-control" onkeyup="FormatIt(this)" runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div id="DivAmount" visible="false" runat="server">
                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-12">
                                                                <div class="col-md-2" style="padding-left: 0px;">
                                                                    <label class="control-label ">Naration : <span class="required">*</span></label>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <asp:TextBox ID="txtNarration" CssClass="form-control" runat="server"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-2">
                                                                    <label class="control-label ">Amount : <span class="required">*</span></label>
                                                                </div>
                                                                <div class="col-md-2">
                                                                    <asp:TextBox ID="txtAmount" placeholder="DEBIT AMOUNT" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <%--<div class="row">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Other bank deduction : </strong></div>
                                                        </div>
                                                    </div>--%>

                                                    <%--<div id="Div2" class="row" runat="server">
                                                        <div class="col-lg-12">
                                                            <div class="table-scrollable" style="border: none">
                                                                <table class="table table-striped table-bordered table-hover">
                                                                    <thead>
                                                                        <tr>
                                                                            <th>
                                                                                <asp:GridView ID="GrsInfo" runat="server" AllowPaging="True"
                                                                                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                                                    EditRowStyle-BackColor="#FFFF99"
                                                                                    PageIndex="10" PageSize="25"
                                                                                    PagerStyle-CssClass="pgr" Width="100%" ShowHeader="false" ShowFooter="false">
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="Account No" Visible="true">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="EDATE" runat="server" Text=" "></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Loan glcode" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lglcd" runat="server" Text=""></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="Loan Type">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="PARTI" runat="server" Text=""></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="Sanction Amount">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="INSTNO" runat="server" Text=" "></asp:Label>
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
                                                    </div>--%>
                                                </asp:TableCell>

                                                <asp:TableCell ID="tbl_Col2" runat="server" Width="30%" BorderStyle="Solid" BorderWidth="1px">

                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Credit Details : </strong></div>
                                                        </div>
                                                    </div>

                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <div class="table-scrollable" style="border: none">
                                                                <table class="table table-striped table-bordered table-hover">
                                                                    <thead>
                                                                        <tr>
                                                                            <th>
                                                                                <asp:GridView ID="grdCredit" runat="server" AllowPaging="True" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                                                    EditRowStyle-BackColor="#FFFF99" OnPageIndexChanging="GrsLoanInfo_PageIndexChanging"
                                                                                    PageIndex="10" PageSize="25" PagerStyle-CssClass="pgr" Width="100%">
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="PrCode" Visible="true">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblPrCode1" runat="server" Text='<%# Eval("SubGlCode") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="Amount" Visible="true">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblAmount1" runat="server" Text='<%# Eval("CREDIT") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </th>
                                                                        </tr>
                                                                    </thead>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Debit Details : </strong></div>
                                                        </div>
                                                    </div>

                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <div class="table-scrollable" style="border: none">
                                                                <table class="table table-striped table-bordered table-hover">
                                                                    <thead>
                                                                        <tr>
                                                                            <th>
                                                                                <asp:GridView ID="grdDebit" runat="server" AllowPaging="True" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                                                    EditRowStyle-BackColor="#FFFF99" OnPageIndexChanging="GrsLoanInfo_PageIndexChanging"
                                                                                    PageIndex="10" PageSize="25" PagerStyle-CssClass="pgr" Width="100%">
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="PrCode" Visible="true">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblPrCode2" runat="server" Text='<%# Eval("SubGlCode") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="Amount" Visible="true">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblAmount2" runat="server" Text='<%# Eval("DEBIT") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </th>
                                                                        </tr>
                                                                    </thead>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Transaction Details : </strong></div>
                                                        </div>
                                                    </div>

                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-4">
                                                                <label class="control-label ">Total Credit</label>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="txtCrAmount" Enabled="false" CssClass="form-control" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-4">
                                                                <label class="control-label ">Total Debit</label>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="txtDrAmount" Enabled="false" CssClass="form-control" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>

                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12" style="text-align: center;">
                                <br />
                                <asp:Button ID="BtnAllVoucher" runat="server" CssClass="btn blue" Text="ALL Voucher Create" OnClick="BtnAllVoucher_Click" />
                                <asp:Button ID="BtnBondCr" runat="server" CssClass="btn blue" Text="Bond Create" OnClick="BtnBondCr_Click" />
                                <asp:Button ID="BtnSurCr" runat="server" CssClass="btn blue" Text="Surity Create" OnClick="BtnSurCr_Click" />
                                <asp:Button ID="BtnVouchCr" runat="server" CssClass="btn blue" Text="Voucher Create" OnClick="BtnVouchCr_Click" OnClientClick="Javascript:return IsValidation()" />
                                <asp:Button ID="BtnPost" runat="server" CssClass="btn blue" Text="Post" OnClick="BtnPost_Click" Enabled="false" />
                                <asp:Button ID="Exit" runat="server" CssClass="btn blue" Text="Close" OnClick="Exit_Click" />
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
    <div class="row" runat="server" id="div_GridVw" visible="false">
        <div class="col-lg-12">
            <div class="table-scrollable" style="border: none">
                <table class="table table-striped table-bordered table-hover">
                    <thead>
                        <tr>
                            <th>
                                <asp:GridView ID="GrsLoanInfo" runat="server" AllowPaging="True"
                                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                    EditRowStyle-BackColor="#FFFF99"
                                    OnPageIndexChanging="GrsLoanInfo_PageIndexChanging"
                                    PageIndex="10" PageSize="25"
                                    PagerStyle-CssClass="pgr" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Account No" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="EDATE" runat="server" Text='<%# Eval("CUSTACCNO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Loan glcode" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lglcd" runat="server" Text='<%# Eval("LOANGLCODE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Loan Type">
                                            <ItemTemplate>
                                                <asp:Label ID="PARTI" runat="server" Text='<%# Eval("GLNAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sanction Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="INSTNO" runat="server" Text='<%# Eval("LIMIT") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sanction Date">
                                            <ItemTemplate>
                                                <asp:Label ID="sdt" runat="server" Text='<%# Eval("SANSSIONDATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate Of Int" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="CREDIT" runat="server" Text='<%# Eval("INTRATE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Installment">
                                            <ItemTemplate>
                                                <asp:Label ID="DEBIT" runat="server" Text='<%# Eval("INSTALLMENT") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DueDATE">
                                            <ItemTemplate>
                                                <asp:Label ID="BALANCE" runat="server" Text='<%# Eval("DUEDATE","{0:dd/MM/yyyy}") %>'></asp:Label>
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
            </div>
        </div>
    </div>
    <div class="col-md-12" runat="server" id="div_grd1" visible="false">
        <div class="row">
            <div class="col-lg-12">
                <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);">
                    <strong style="color: #3598dc">
                        <asp:Label ID="LblGrdName" runat="server"></asp:Label></strong>
                </div>
            </div>
        </div>
        <table class="table table-striped table-bordered table-hover" width="100%">
            <thead>
                <tr>
                    <th>
                        <asp:GridView ID="GrdLoanSurity1" runat="server" AllowPaging="True" AlternatingRowStyle-CssClass="alt" CssClass="mGrid" AutoGenerateColumns="false"
                            EditRowStyle-BackColor="#FFFF99" OnPageIndexChanging="GrdLoanSurity1_PageIndexChanging" PagerStyle-CssClass="pgr" Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText="Br Code" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="BRCD" runat="server" Text='<%# Eval("BRCD") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Prod Code">
                                    <ItemTemplate>
                                        <asp:Label ID="SubGlCode" runat="server" Text='<%# Eval("SubGlCode") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="A/C No">
                                    <ItemTemplate>
                                        <asp:Label ID="AccNo" runat="server" Text='<%# Eval("AccNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cust No">
                                    <ItemTemplate>
                                        <asp:Label ID="CustNo" runat="server" Text='<%# Eval("CustNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Loan Type">
                                    <ItemTemplate>
                                        <asp:Label ID="LnType" runat="server" Text='<%# Eval("LnType") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sr. NO">
                                    <ItemTemplate>
                                        <asp:Label ID="LnSrNo" runat="server" Text='<%# Eval("LnSrNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sr. Name">
                                    <ItemTemplate>
                                        <asp:Label ID="LnSrName" runat="server" Text='<%# Eval("LnSrName") %>'></asp:Label>
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
    <div class="row">
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
                                                <asp:Label ID="Particulars" runat="server" Text='<%# Eval("Particulars2") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Entry Type" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="CRDR" runat="server" Text='<%# Eval("TrxType") %>'></asp:Label>
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

    <div class="row">
                                            <div class="col-md-12">
                                                <div class="table-scrollable" style="width: 100%; height: 500px; overflow-x: auto; overflow-y: auto">
                                                    <table class="table table-striped table-bordered table-hover">
                                                        <thead>
                                                            <tr>
                                                                <th>
                                                                    <asp:GridView ID="grdShow" runat="server"
                                                                        AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                                        EditRowStyle-BackColor="#FFFF99" Width="100%">
                                                                        <Columns>

                                                                            <asp:TemplateField HeaderText="VOUCHER NO" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="SET_NO" runat="server" Text='<%# Eval("SETNO") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="A/C TYPE" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="SUBGLCODE" runat="server" Text='<%# Eval("SUBGLCODE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="A/C Name" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="GLNAME" runat="server" Text='<%# Eval("GLNAME") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="A/C NO" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="ACCNO" runat="server" Text='<%# Eval("ACCNO") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="CUST NAME" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="CUSTNAME" runat="server" Text='<%# Eval("CUSTNAME") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="PARTICULARS" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="PARTICULARS" runat="server" Text='<%# Eval("PARTICULARS") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Cr AMOUNT" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="CREDIT" runat="server" Text='<%# Eval("CREDIT") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Dr AMOUNT" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="DEBIT" runat="server" Text='<%# Eval("DEBIT") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="MID" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="MID" runat="server" Text='<%# Eval("MID") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="ENTRYDATE" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="ENTRYDATE" runat="server" Text='<%# Eval("ENTRYDATE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="MAKER" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="LOGINCODE" runat="server" Text='<%# Eval("LOGINCODE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                              <asp:TemplateField HeaderText="Cancel" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Eval("ScrollNo")%>' CommandName="select" class="glyphicon glyphicon-trash" OnClick="lnkDelete_Click"></asp:LinkButton>
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

