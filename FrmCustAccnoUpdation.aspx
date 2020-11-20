<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmCustAccnoUpdation.aspx.cs" Inherits="FrmCustAccnoUpdation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;
            return true;
        }

        function IsValide() {
            var txtFromCustNo = document.getElementById('<%=txtFromCustNo.ClientID%>').value;
            <%--var txtToCustNo = document.getElementById('<%=txtToCustNo.ClientID%>').value; --%>

            if (txtFromCustNo == "") {
                message = 'Enter From Customer Number...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=txtFromCustNo.ClientID %>').focus();
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Contnt2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        Customer No Change
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                    <div class="col-lg-12">
                                        <div class="col-md-2">
                                            <label>Customer No <span class="required">*</span></label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="txtFromCustNo" CssClass="form-control" Placeholder="Old Customer No" onkeypress="javascript:return isNumber (event)" runat="server" OnTextChanged="txtFromCustNo_TextChanged" AutoPostBack="true" TabIndex="1"></asp:TextBox>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtFromCustName" CssClass="form-control" Placeholder="From Customer Name" OnTextChanged="txtFromCustName_TextChanged" AutoPostBack="true" runat="server" />
                                            <asp:AutoCompleteExtender ID="autoglname1" runat="server" TargetControlID="txtFromCustName"
                                                UseContextKey="true"
                                                CompletionInterval="1"
                                                CompletionSetCount="20"
                                                MinimumPrefixLength="1"
                                                EnableCaching="true"
                                                ServicePath="~/WebServices/Contact.asmx"
                                                ServiceMethod="GetCustNames">
                                            </asp:AutoCompleteExtender>
                                        </div>
                                         <label class="control-label col-md-2">Show Closed Accno : <span class="required">* </span></label>
                                        <div class="col-md-2">
                                            <asp:CheckBox ID="CHK_SKIP_STD" runat="server" ></asp:CheckBox>
                                        </div>
                                        <%--<div class="col-lg-2">
                                            <div class="col-md-2">
                                                <asp:CheckBox ID="CHK_SKIP_STD" runat="server" Text="SKIP_STD" Style="width: 100px;" />
                                            </div>
                                        </div>--%>
                                    </div>
                                </div>
                                <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                    <div class="col-lg-12">
                                        <div class="col-md-2">
                                            <label>Branch Code <span class="required">*</span></label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="TxtBRCD" OnTextChanged="TxtBRCD_TextChanged" CssClass="form-control" runat="server" Enabled="false" onkeypress="javascript:return isNumber(event)" AutoPostBack="true"></asp:TextBox>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="TxtBrname" CssClass="form-control" runat="server" Style="text-transform: uppercase;" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                        </div>
                                        <label class="control-label col-md-2">Clear Balance : <span class="required">* </span></label>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="TxtClearBal" placeholder="Clear Balance" CssClass="form-control" runat="server" ReadOnly="true" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                    <div class="col-lg-12">
                                        <div class="col-md-2">
                                            <label>Product Type <span class="required">*</span></label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="txttype" CssClass="form-control" runat="server" onkeypress="return isNumber(event)" Enabled="false" OnTextChanged="txttype_TextChanged" AutoPostBack="true" TabIndex="1"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-4">
                                            <asp:TextBox ID="txttynam" runat="server" CssClass="form-control" AutoPostBack="true" Enabled="false" OnTextChanged="txttynam_TextChanged" TabIndex="2"></asp:TextBox>
                                            <div id="Custlist2" style="height: 200px; overflow-y: scroll;"></div>
                                            <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="txttynam"
                                                UseContextKey="true"
                                                CompletionInterval="1"
                                                CompletionSetCount="20"
                                                MinimumPrefixLength="1"
                                                EnableCaching="true"
                                                ServicePath="~/WebServices/Contact.asmx"
                                                ServiceMethod="GetGlName" CompletionListElementID="Custlist2">
                                            </asp:AutoCompleteExtender>
                                        </div>
                                        <label class="control-label col-md-2">Opening Date <span class="required"></span></label>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="TxtOPDT" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                    <div class="col-lg-12">
                                        <div class="col-md-2">
                                            <label>AccountNo<span class="required">*</span></label>
                                        </div>
                                        <div class="col-lg-2">
                                            <asp:TextBox ID="txtaccno" runat="server" CssClass="form-control" onkeypress="javascript:return isNumber (event)" Enabled="false" OnTextChanged="txtaccno_TextChanged" AutoPostBack="true" TabIndex="5"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-4">
                                            <asp:TextBox ID="TxtAccName" runat="server" CssClass="form-control" AutoPostBack="true" Enabled="false" OnTextChanged="TxtAccName_TextChanged" TabIndex="6"></asp:TextBox>
                                            <div id="CustList" style="height: 200px; overflow-y: scroll;"></div>
                                            <asp:AutoCompleteExtender ID="AutoAccname" runat="server" TargetControlID="TxtAccName"
                                                UseContextKey="true"
                                                CompletionInterval="1"
                                                CompletionSetCount="20"
                                                MinimumPrefixLength="1"
                                                EnableCaching="true"
                                                ServicePath="~/WebServices/Contact.asmx"
                                                ServiceMethod="GetAccName" CompletionListElementID="CustList">
                                            </asp:AutoCompleteExtender>
                                        </div>
                                        <label class="control-label col-md-2">A/C Status<span class="required"></span></label>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="TxtACStatus" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                    <div class="col-lg-12">
                                        <div class="col-md-2">
                                            <label>New Cust No <span class="required">*</span></label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="txtNewCustNo" CssClass="form-control" Placeholder="New Customer No" onkeypress="javascript:return isNumber (event)" runat="server" OnTextChanged="txtNewCustNo_TextChanged" AutoPostBack="true" TabIndex="1"></asp:TextBox>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtNewCustname" CssClass="form-control" Placeholder="From Customer Name" OnTextChanged="txtNewCustname_TextChanged" AutoPostBack="true" runat="server" />
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtNewCustname"
                                                UseContextKey="true"
                                                CompletionInterval="1"
                                                CompletionSetCount="20"
                                                MinimumPrefixLength="1"
                                                EnableCaching="true"
                                                ServicePath="~/WebServices/Contact.asmx"
                                                ServiceMethod="GetCustNames">
                                            </asp:AutoCompleteExtender>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9">
                                        <%--<asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" OnClientClick="Javascript:return IsValide();" TabIndex="4" />--%>
                                        <asp:Button ID="btnUpdate" runat="server" Text="Cust No Change" CssClass="btn btn-primary" OnClick="btnUpdate_Click" OnClientClick="Javascript:return IsValide();" TabIndex="5" />
                                        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-primary" OnClick="btnClear_Click" TabIndex="6" />
                                        <asp:Button ID="btnExit" runat="server" Text="Exit" CssClass="btn btn-primary" OnClick="btnExit_Click" TabIndex="6" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                            </div>
                            <div id="Div1" runat="server" class="col-md-12">
                                <table class="table table-striped table-bordered table-hover" width="100%">
                                    <thead>
                                        <tr>
                                            <th>
                                                <asp:GridView ID="grdCustDetails" runat="server" AllowPaging="True" AlternatingRowStyle-CssClass="alt" CssClass="mGrid"
                                                    AutoGenerateColumns="false" EditRowStyle-BackColor="#FFFF99" OnPageIndexChanging="grdCustDetails_PageIndexChanging"
                                                    PagerStyle-CssClass="pgr" Width="100%" EmptyDataText="Customer Not Exists">
                                                    <Columns>

                                                        <asp:TemplateField HeaderText="Branch Code" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBRCD" runat="server" Text='<%# Eval("BRCD") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Customer No" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCUSTNO" runat="server" Text='<%# Eval("CUSTNO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Customer Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCUSTNAME" runat="server" Text='<%# Eval("CUSTNAME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Old Cust No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOLDCTNO" runat="server" Text='<%# Eval("OLDCTNO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Opening Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOPENINGDATE" runat="server" Text='<%# Eval("OPENINGDATE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Gl Code">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblGLCODE" runat="server" Text='<%# Eval("GLCODE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Product Code">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSUBGLCODE" runat="server" Text='<%# Eval("SUBGLCODE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Stage">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSTAGE" runat="server" Text='<%# Eval("STAGE") %>'></asp:Label>
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
                            <div id="Div2" runat="server" class="col-md-12">
                                <table class="table table-striped table-bordered table-hover" width="100%" style="overflow-x: scroll; overflow-y: scroll; height: 50px;">
                                    <thead>
                                        <tr>
                                            <th>
                                                <asp:GridView ID="GrdVwAcDetails" runat="server" AlternatingRowStyle-CssClass="alt" CssClass="mGrid"
                                                    AutoGenerateColumns="false" EditRowStyle-BackColor="#FFFF99" OnPageIndexChanging="GrdVwAcDetails_PageIndexChanging"
                                                    PagerStyle-CssClass="pgr" Width="100%" EmptyDataText="Customer Not Exists" OnSelectedIndexChanged="GrdVwAcDetails_SelectedIndexChanged">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SrNo" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Branch Code" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBRCD" runat="server" Text='<%# Eval("BRCD") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Branch Name" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBRname" runat="server" Text='<%# Eval("MIDNAME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Gl Code">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblGLCODE" runat="server" Text='<%# Eval("GLCODE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Product Code">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSUBGLCODE" runat="server" Text='<%# Eval("SUBGLCODE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Product Name" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPrdName" runat="server" Text='<%# Eval("GLNAME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Account No" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblACCNO" runat="server" Text='<%# Eval("ACCNO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="CustNo" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCUSTNO" runat="server" Text='<%# Eval("CUSTNO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Opening Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOPENINGDATE" runat="server" Text='<%# Eval("OPENINGDATE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="ACC_STATUS" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblACC_STATUS" runat="server" Text='<%# Eval("ACC_STATUS") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="CLBal" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCLBal" runat="server" Text='<%# Eval("CLBal") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="View" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkView" runat="server" CommandArgument='<%#Eval("BRCD")+","+Eval("SUBGLCODE")+","+Eval("ACCNO")%>' CommandName="select" class="glyphicon glyphicon-pencil" OnClick="lnkView_Click"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="Th" HorizontalAlign="Center" />
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
                    <div class="col-md-12">
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

</asp:Content>

