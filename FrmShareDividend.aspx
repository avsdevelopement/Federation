<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmShareDividend.aspx.cs" Inherits="FrmShareDividend" %>

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
        function Validate() {
            var MemberNo = document.getElementById('<%=txtMemberNo.ClientID%>').value;

            if (MemberNo == "") {
                var message = 'Enter member number first...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtMemberNo.ClientID%>').focus();
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
                        Share Dividend Transfer
                    </div>
                </div>
                <div class="portlet-body form">

                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div id="error">
                                    </div>
                                    <div class="tab-pane active" id="tab1">

                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:RadioButtonList ID="rbtnType" runat="server" OnSelectedIndexChanged="rbtnType_SelectedIndexChanged" AutoPostBack="true" RepeatDirection="Horizontal" Style="width: 400px;">
                                                        <asp:ListItem Text="Specific" Value="1" Selected="True" />
                                                        <asp:ListItem Text="Multiple" Value="2" />
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>

                                        <div id="divSpecific" runat="server">
                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-12">
                                                    <div class="col-lg-2">
                                                        <label class="control-label">Member Number<span class="required">*</span></label>
                                                    </div>
                                                    <div class="col-lg-2">
                                                        <asp:TextBox ID="txtMemberNo" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" OnTextChanged="txtMemberNo_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="input-icon">
                                                            <i class="fa fa-search"></i>
                                                            <asp:TextBox ID="txtMemberName" runat="server" placeholder="Search Customer Name" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtMemberName_TextChanged"></asp:TextBox>
                                                            <div id="CustList0" style="height: 200px; overflow-y: scroll;"></div>
                                                            <asp:AutoCompleteExtender ID="AutoMemberName" runat="server" TargetControlID="txtMemberName" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" ServiceMethod="GetMemberNames" CompletionListElementID="CustList0">
                                                            </asp:AutoCompleteExtender>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-2">
                                                        <label class="control-label">Customer Number<span class="required">*</span></label>
                                                    </div>
                                                    <div class="col-lg-2">
                                                        <asp:TextBox ID="txtCustNo" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div id="divMultiple" visible="false" runat="server">
                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-12">
                                                    <div class="col-lg-2">
                                                        <label class="control-label">Rec Division<span class="required">*</span></label>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <asp:DropDownList ID="ddlDivision" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" AutoPostBack="true" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-12">
                                                    <div class="col-lg-2">
                                                        <label class="control-label">Rec Department<span class="required">*</span></label>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <asp:DropDownList ID="ddlDepartment" CssClass="form-control" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-12">
                                                    <div class="col-lg-2">
                                                        <label class="control-label">Prod Code<span class="required">*</span></label>
                                                    </div>
                                                    <div class="col-lg-2">
                                                        <asp:TextBox ID="txtProdCode" CssClass="form-control" OnTextChanged="txtProdCode_TextChanged" AutoPostBack="true" runat="server" />
                                                    </div>
                                                    <div class="col-lg-4">
                                                        <div class="input-icon">
                                                            <i class="fa fa-search"></i>
                                                            <asp:TextBox ID="txtProdName" CssClass="form-control" PlaceHolder="Search Product Name" OnTextChanged="txtProdName_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                                            <div id="CustList1" style="height: 200px; overflow-y: scroll;"></div>
                                                            <asp:AutoCompleteExtender ID="AutoGlName" runat="server" TargetControlID="txtProdName" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList1" ServiceMethod="GetGlWiseName">
                                                            </asp:AutoCompleteExtender>
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
                                    <div class="col-md-offset-3 col-md-9">
                                        <asp:Button ID="btnShow" runat="server" CssClass="btn blue" Text="Show" OnClick="btnShow_Click" OnClientClick="Javascript:return Validate();" />
                                        <asp:Button ID="BtnView" CssClass="btn blue" runat="server" Text="StatementView" OnClick="BtnView_Click" Visible="false" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-12">
                    </div>
                    <div id="Div1" runat="server" class="col-md-12">
                        <div class="table-scrollable" style="width: 100%; height: auto; max-height: 400px; overflow-x: auto; overflow-y: auto">
                            <table class="table table-striped table-bordered table-hover" width="100%">
                                <thead>
                                    <tr>
                                        <th>
                                            <asp:GridView ID="GrdStatementSum" runat="server" AlternatingRowStyle-CssClass="alt" CssClass="mGrid"
                                                AutoGenerateColumns="false" EditRowStyle-BackColor="#FFFF99"
                                                Width="100%" EmptyDataText="No Records available for this customer">
                                                <Columns>

                                                    <%--REFBRCD	REFBRNAME	BRCD	entrydate	SETNO	PARTICULAR1	PARTICULAR2	CHECQUENO	GLCODE	SUBGLCODE	ACCNO	Payable	Paid	BALANCE	CRDR--%>

                                                    <asp:TemplateField HeaderText="BRCD" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_GSBRCD" runat="server" Text='<%# Eval("BRCD") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Glcode" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_GSGlcode" runat="server" Text='<%# Eval("GLCODE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Prod Code" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_GSSubGlCode" runat="server" Text='<%# Eval("SUBGLCODE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Acc Number" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_GSAccNo" runat="server" Text='<%# Eval("ACCNO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Cust Name" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_GSName" runat="server" Text='<%# Eval("CUSTNAME") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Payable">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_GSCredit" runat="server" Text='<%# Eval("Payable") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Paid">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_GSBalance" runat="server" Text='<%# Eval("Paid") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="BALANCE">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_GSBalance1" runat="server" Text='<%# Eval("BALANCE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CR/DR">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbl_GSBalance2" runat="server" Text='<%# Eval("CRDR") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="View Details">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="Lnk_ClickView" runat="server" CommandName="select" CommandArgument='<%#Eval("BRCD")+"_"+ Eval("SUBGLCODE")+"_"+Eval("ACCNO")%>' class="glyphicon glyphicon-plus" OnClick="Lnk_ClickView_Click"></asp:LinkButton>
                                                            <%--<asp:LinkButton ID="LnkViewDetails" runat="server" CommandName="select" CommandArgument='<%#Eval("BRCD")+"_"+ Eval("SUBGLCODE")+"_"+Eval("ACCNO")%>' class="glyphicon glyphicon-plus" OnClick="LnkViewDetails_Click" ></asp:LinkButton>--%>
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
                    <div id="DivGrd1" runat="server" class="col-md-12">
                        <div class="table-scrollable" style="width: 100%; height: auto; max-height: 150px; overflow-x: auto; overflow-y: auto">
                            <table class="table table-striped table-bordered table-hover" width="100%">
                                <thead>
                                    <tr>
                                        <th>
                                            <asp:GridView ID="grdShareDividend" runat="server" AlternatingRowStyle-CssClass="alt" CssClass="mGrid"
                                                AutoGenerateColumns="false" EditRowStyle-BackColor="#FFFF99"
                                                DataKeyNames="id" Width="100%" EmptyDataText="No Records available for this customer">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="Select" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chk" Checked="true" runat="server" onclick="Check_Click(this)" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Product Type" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGlName" runat="server" Text='<%# Eval("GlName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Prod Code" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSubGlCode" runat="server" Text='<%# Eval("SubGlCode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Acc Number" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAccNo" runat="server" Text='<%# Eval("AccNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBalance" runat="server" Text='<%# Eval("Balance") %>'></asp:Label>
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

                    <div id="DivPaymentType" visible="false" runat="server">
                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                            <div class="col-lg-12">
                                <div class="col-md-2">
                                    <label class="control-label ">Payment Type : <span class="required">*</span></label>
                                </div>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="ddlPaymentType" CssClass="form-control" runat="Server" OnSelectedIndexChanged="ddlPaymentType_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                        <asp:ListItem Value="1">Cash</asp:ListItem>
                                        <asp:ListItem Value="2">Transfer</asp:ListItem>
                                        <asp:ListItem Value="4">Cheque</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div id="Transfer" visible="false" runat="server">
                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                            <div class="col-lg-12">
                                <div class="col-md-2">
                                    <label class="control-label ">Branch Code :<span class="required"> *</span></label>
                                </div>
                                <div class="col-md-1">
                                    <asp:TextBox ID="txtPayBrCode" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtPayBrCode_TextChanged"></asp:TextBox>
                                </div>
                                <div class="col-md-5">
                                    <asp:TextBox ID="txtPayBrName" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                            <div class="col-lg-12">
                                <div class="col-md-2">
                                    <label class="control-label ">Product Code :<span class="required"> *</span></label>
                                </div>
                                <div class="col-md-1">
                                    <asp:TextBox ID="txtPayProdType" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtPayProdType_TextChanged" PlaceHolder="Product Type"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <div class="input-icon">
                                        <i class="fa fa-search"></i>
                                        <asp:TextBox ID="txtPayProdName" CssClass="form-control" PlaceHolder="Search Product Name" OnTextChanged="txtPayProdName_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                        <div id="CustList2" style="height: 200px; overflow-y: scroll;"></div>
                                        <asp:AutoCompleteExtender ID="AutoPayGlName" runat="server" TargetControlID="txtPayProdName" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                            MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList2" ServiceMethod="GetGlName">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <label class="control-label ">Account No:<span class="required"> *</span></label>
                                </div>
                                <div class="col-md-1">
                                    <asp:TextBox ID="TxtPayAccNo" CssClass="form-control" PlaceHolder="ID" runat="server" AutoPostBack="true" OnTextChanged="TxtPayAccNo_TextChanged"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <div class="input-icon">
                                        <i class="fa fa-search"></i>
                                        <asp:TextBox ID="TxtPayAccName" CssClass="form-control" PlaceHolder="Search Customer Name" runat="server" AutoPostBack="true" OnTextChanged="TxtPayAccName_TextChanged"></asp:TextBox>
                                        <div id="CustList3" style="height: 200px; overflow-y: scroll;"></div>
                                        <asp:AutoCompleteExtender ID="AutoPayAccName" runat="server" TargetControlID="TxtPayAccName" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                            MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList3" ServiceMethod="GetAccName">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div id="Transfer1" visible="false" runat="server">
                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                <div class="col-lg-12">
                                    <div class="col-md-2">
                                        <label class="control-label ">Instrument No. :</label>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="TxtChequeNo" placeholder="CHEQUE NUMBER" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <label class="control-label ">Instrument Date :</label>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="TxtChequeDate" CssClass="form-control" PlaceHolder="DD/MM/YYYY" onkeyup="FormatIt(this);" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>

                    <div id="DivAmount" visible="false" runat="server">
                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                            <div class="col-lg-12">
                                <div class="col-md-2">
                                    <label class="control-label ">Naration :</label>
                                </div>
                                <div class="col-md-5">
                                    <asp:TextBox ID="txtNarration" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label class="control-label ">Amount : <span class="required">*</span></label>
                                </div>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtCrAmount" placeholder="Credit Amount" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                        <div class="col-md-12">
                        </div>
                    </div>

                    <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                        <div class="col-md-12">
                            <div class="col-md-2">
                            </div>
                            <div class="col-md-2">
                                <asp:Button ID="btnSubmit" CssClass="btn blue" runat="server" Text="Submit" OnClick="btnSubmit_Click" Visible="false" />
                            </div>
                        </div>
                    </div>

                    <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                        <div class="col-md-12">
                        </div>
                    </div>

                </div>
            </div>
        </div>

        <div class="col-md-12">
            <div style="border: 1px solid #3598dc">
                <div class="portlet-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Direct Liabilities : </strong></div>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                        <div class="col-lg-12">
                            <asp:GridView ID="GrdDirectLiab" runat="server" AllowPaging="True" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                EditRowStyle-BackColor="#FFFF99" PagerStyle-CssClass="pgr" Width="100%" ShowFooter="true">
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
                </div>
            </div>
        </div>

        <div class="col-md-12">
            <div style="border: 1px solid #3598dc">
                <div class="portlet-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">InDirect Liabilities-To Surety : </strong></div>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                        <div class="col-lg-12">
                            <asp:GridView ID="GrdInDirectLiab" runat="server" AllowPaging="True" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                EditRowStyle-BackColor="#FFFF99" PagerStyle-CssClass="pgr" Width="100%" ShowFooter="true">
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
                                    <asp:TemplateField HeaderText="Custno" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCUSTNO" runat="server" Text='<%# Eval("CUSTNO") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Account Holder Name" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCUSTNAME" runat="server" Text='<%# Eval("CUSTNAME") %>'></asp:Label>
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
                                            <asp:Label ID="lblsancamtIn" runat="server" Text='<%# Eval("LIMIT") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <div style="padding: 0 0 5px 0">
                                                <asp:Label ID="Lbl_TotalLimitIn" runat="server" />
                                            </div>
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Outstanding bal" Visible="true" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lbloutbalIn" runat="server" Text='<%# Eval("BALANCE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <div style="padding: 0 0 5px 0">
                                                <asp:Label ID="Lbl_TotalBalIn" runat="server" />
                                            </div>
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="OD Amt" Visible="true" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblodamtIn" runat="server" Text='<%# Eval("ODAMT") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <div style="padding: 0 0 5px 0">
                                                <asp:Label ID="Lbl_TotalODIn" runat="server" />
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
                </div>
            </div>
        </div>

        <div class="col-md-12">
            <div style="border: 1px solid #3598dc">
                <div class="portlet-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Other Account Details : </strong></div>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                        <div class="col-lg-12">
                            <asp:GridView ID="grdAccDetails" runat="server" AllowPaging="True" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                EditRowStyle-BackColor="#FFFF99" OnPageIndexChanging="grdAccDetails_PageIndexChanging"
                                PagerStyle-CssClass="pgr" Width="100%">
                                <Columns>

                                    <asp:TemplateField HeaderText="SrNo" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSrNo1" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="GL Code" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGLCODE" runat="server" Text='<%# Eval("GLCODE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Prod Code" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSUBGLCODE" runat="server" Text='<%# Eval("SUBGLCODE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Product Name" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGLNAME" runat="server" Text='<%# Eval("GLNAME") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="A/C No" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblACCNO" runat="server" Text='<%# Eval("ACCNO") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Remark" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCUSTNAME" runat="server" Text='<%# Eval("CUSTNAME") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="A/C Status" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAcStatus" runat="server" Text='<%# Eval("ACC_STATUS") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Balance" Visible="true" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBALANCE" runat="server" Text='<%# Eval("BALANCE") %>'></asp:Label>
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
                </div>
            </div>
        </div>

    </div>

    <div class="modal fade bs-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" style="margin-left: 10%; width:80%; padding-right: 211px; padding-top: 106px">
        <div class="modal-dialog modal-lg" role="document" style="width: 96%">
            <div class="modal-content" style="border: 5px solid #4dbfc0;">
                <div class="inner_top">
                    <div class="panel panel-default">
                        <div class="panel-heading">Account Statement</div>
                        <div class="panel-body">
                            <div class="col-sm-12">
                                <div class="col-lg-12">

                                    <div class="table-scrollable" style="width: 100%; height: auto; max-height: 300px; overflow-x: auto; overflow-y: auto">
                                        <table class="table table-striped table-bordered table-hover">
                                            <thead>
                                                <tr>
                                                    <th>
                                                        <asp:GridView ID="grdAccStatement" runat="server" AlternatingRowStyle-CssClass="alt" CssClass="mGrid" Width="100%"
                                                            AutoGenerateColumns="false" EditRowStyle-BackColor="#FFFF99">
                                                            <Columns>
                                                                <%--BRCD	entrydate	SETNO	PARTICULAR1	PARTICULAR2	CHECQUENO	GLCODE	SUBGLCODE	ACCNO	Payable	Paid	BALANCE	CRDR--%>
                                                                <asp:TemplateField HeaderText="Ref BRCD" Visible="true" HeaderStyle-BackColor="#99ff99">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Lbl_GSRefBRCD" runat="server" Text='<%# Eval("REFBRCD") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Ref Name" Visible="true"  HeaderStyle-BackColor="#99ff99">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Lbl_GSRefBRCDName" runat="server" Text='<%# Eval("REFBRNAME") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="EntryDate" Visible="true"  HeaderStyle-BackColor="#99ff99">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblEDate" runat="server" Text='<%# Eval("entrydate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Glname " Visible="true"  HeaderStyle-BackColor="#99ff99">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblGlname" runat="server" Text='<%# Eval("GLNAME") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="SetNo" Visible="true"  HeaderStyle-BackColor="#99ff99">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSetNo" runat="server" Text='<%# Eval("SETNO") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Particulars1"  HeaderStyle-BackColor="#99ff99">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblParticular" runat="server" Text='<%# Eval("PARTICULAR1") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Particulars2"  HeaderStyle-BackColor="#99ff99">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblparticular2" runat="server" Text='<%# Eval("PARTICULAR2") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Cheque/Refrence"  HeaderStyle-BackColor="#99ff99">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblInstNo" runat="server" Text='<%# Eval("CHECQUENO") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Payable" Visible="true"  HeaderStyle-BackColor="#ffff00">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCredit" runat="server" Text='<%# Eval("Payable") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Paid"  HeaderStyle-BackColor="#ffff00">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDebit" runat="server" Text='<%# Eval("Paid") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Balance"  HeaderStyle-BackColor="#99ff99">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblBalance" runat="server" Text='<%# Eval("Balance") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Dr/Cr"  HeaderStyle-BackColor="#99ff99">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDrCrIndicator" runat="server" Text='<%# Eval("CRDR") %>'></asp:Label>
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

    <div id="alertModal" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <center><h4 class="modal-title" style="color:#ff0000">AVS CORE</h4></center>
                </div>
                <div class="modal-body">
                    <p>
                        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                    </p>

                </div>
                <div class="modal-footer">
                    <button id="btnClose" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>


</asp:Content>

