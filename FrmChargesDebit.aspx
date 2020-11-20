<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmChargesDebit.aspx.cs" Inherits="FrmChargesDebit" %>

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

        function IsValid() {
            var PrCode = document.getElementById('<%=txtGl.ClientID%>').value;
            var AccNo = document.getElementById('<%=txtAccNo.ClientID%>').value;
            var Activity = document.getElementById('<%=ddlActivity.ClientID%>').value;
            var Charges = document.getElementById('<%=ddlCharges.ClientID%>').value;
            var Amount = document.getElementById('<%=txtAmount.ClientID%>').value;
            var Narration = document.getElementById('<%=txtNarration.ClientID%>').value;
            var message = '';

            if (PrCode == "") {
                window.alert("Enter product number first ...!!");
                document.getElementById('<%=txtGl.ClientID%>').focus();
                return false;
            }
            if (AccNo == "") {
                window.alert("Enter account number first ...!!");
                document.getElementById('<%=txtAccNo.ClientID%>').focus();
                return false;
            }
            if (Activity == "0") {
                window.alert("Select Activity first ...!!");
                document.getElementById('<%=ddlActivity.ClientID%>').focus();
                return false;
            }
            if (Charges == "0") {
                window.alert("Select Charges Type first ...!!");
                document.getElementById('<%=ddlCharges.ClientID%>').focus();
                return false;
            }
            if (Amount == "") {
                window.alert("Enter Amount first ...!!");
                document.getElementById('<%=txtAmount.ClientID%>').focus();
                return false;
            }
            if (Narration == "") {
                window.alert("Enter Narration first ...!!");
                document.getElementById('<%=txtNarration.ClientID%>').focus();
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
                        Charges Outstanding
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">

                                    <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2" style="width: 120px">Charges Type : </label>
                                            <div class="col-md-1"></div>
                                            <div class="col-md-8">
                                                <asp:RadioButtonList ID="rbtnEntryType" OnSelectedIndexChanged="rbtnEntryType_SelectedIndexChanged" AutoPostBack="true" RepeatDirection="Horizontal" runat="server" Style="width: 250px;">
                                                    <asp:ListItem Text="Receivable" Selected="True" Value="R" />
                                                    <%--<asp:ListItem Text="Debit" Value="D" />--%>
                                                    <asp:ListItem Text="Cancel" Value="C" />
                                                </asp:RadioButtonList>

                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2" style="width: 120px">Product Code : <span class="required">*</span></label>
                                            <div class="col-md-1">
                                                <asp:TextBox ID="txtGl" onkeypress="javascript:return isNumber(event)" CssClass="form-control" Placeholder="Product Code" runat="server" AutoPostBack="true" OnTextChanged="txtGl_TextChanged"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtGlName" CssClass="form-control" Placeholder="Product Name" runat="server" Enabled="false"></asp:TextBox>
                                            </div>
                                            <label class="control-label col-md-2" style="width: 120px">Account No : <span class="required">*</span></label>
                                            <div class="col-md-1">
                                                <asp:TextBox ID="txtAccNo" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server" Placeholder="AccNo" OnTextChanged="txtAccNo_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="input-icon">
                                                    <i class="fa fa-search"></i>
                                                    <asp:TextBox ID="txtAccName" CssClass="form-control" Placeholder="Search account name wise" OnTextChanged="txtAccName_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                                    <div id="CustList2" style="height: 200px; overflow-y: scroll;"></div>
                                                    <asp:AutoCompleteExtender ID="AutoAccname" runat="server" TargetControlID="txtAccName" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                        MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList2" ServiceMethod="GetAccName">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div id="divActivity" visible="true" runat="server" class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2" style="width: 120px">Select Activity : <span class="required">*</span></label>
                                            <div class="col-md-4">
                                                <asp:DropDownList CssClass="form-control" ID="ddlActivity" OnSelectedIndexChanged="ddlActivity_SelectedIndexChanged" AutoPostBack="true" runat="Server">
                                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                    <asp:ListItem Value="1">Charges</asp:ListItem>
                                                    <asp:ListItem Value="2">Reverse Charges</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <label class="control-label col-md-2" style="width: 120px">Select Chrges : <span class="required">*</span></label>
                                            <div class="col-md-4">
                                                <asp:DropDownList CssClass="form-control" ID="ddlCharges" runat="Server" OnSelectedIndexChanged="ddlCharges_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div id="divVoucher" visible="true" runat="server" class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2" style="width: 120px">Amount : <span class="required">*</span></label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtAmount" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber(event)" Placeholder="Amount"></asp:TextBox>
                                            </div>
                                            <label class="control-label col-md-2" style="width: 120px">Voucher Type : <span class="required">*</span></label>
                                            <div class="col-md-4">
                                                <asp:DropDownList ID="ddlVoucher" CssClass="form-control" runat="Server" OnSelectedIndexChanged="ddlVoucher_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                    <asp:ListItem Value="1">Cash</asp:ListItem>
                                                    <asp:ListItem Value="2">Transfer</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div id="divCancel" visible="false" runat="server" class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2" style="width: 120px">Select Chrges : <span class="required">*</span></label>
                                            <div class="col-md-4">
                                                <asp:DropDownList ID="ddlCharges1" runat="Server" CssClass="form-control" OnSelectedIndexChanged="ddlCharges1_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                            </div>
                                            <label class="control-label col-md-2" style="width: 120px">Amount : <span class="required">*</span></label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtAmount1" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber(event)" Placeholder="Amount"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div id="Transfer" visible="false" runat="server">
                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2" style="width: 120px">Product Code : <span class="required">*</span></label>
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="txtProdType1" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber(event)" AutoPostBack="true" OnTextChanged="txtProdType1_TextChanged" PlaceHolder="Product Type"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="input-icon">
                                                        <i class="fa fa-search"></i>
                                                        <asp:TextBox ID="txtProdName1" CssClass="form-control" PlaceHolder="Search product name wise" OnTextChanged="txtProdName1_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                                        <div id="CustList3" style="height: 200px; overflow-y: scroll;"></div>
                                                        <asp:AutoCompleteExtender ID="autoglname1" runat="server" TargetControlID="txtProdName1" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                            MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList3" ServiceMethod="GetGlName">
                                                        </asp:AutoCompleteExtender>
                                                    </div>
                                                </div>
                                                <label class="control-label col-md-2" style="width: 120px">Account No : <span class="required">*</span></label>
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="TxtAccNo1" CssClass="form-control" PlaceHolder="ID" onkeypress="javascript:return isNumber(event)" runat="server" AutoPostBack="true" OnTextChanged="TxtAccNo1_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="input-icon">
                                                        <i class="fa fa-search"></i>
                                                        <asp:TextBox ID="TxtAccName1" CssClass="form-control" PlaceHolder="Search account name wise" runat="server" AutoPostBack="true" OnTextChanged="TxtAccName1_TextChanged"></asp:TextBox>
                                                        <div id="CustList4" style="height: 200px; overflow-y: scroll;"></div>
                                                        <asp:AutoCompleteExtender ID="AutoAccname1" runat="server" TargetControlID="TxtAccName1" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                            MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList4" ServiceMethod="GetAccName">
                                                        </asp:AutoCompleteExtender>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div id="DivAmount" visible="false" runat="server">
                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2" style="width: 120px">Naration : </label>
                                                <div class="col-md-5">
                                                    <asp:TextBox ID="txtNarration" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-1">Amount : <span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtCrAmount" placeholder="Credit Amount" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9">
                                        <asp:Button ID="btnInsert" runat="server" CssClass="btn blue" Text="Submit" OnClick="btnInsert_Click" OnClientClick="Javascript:return IsValid();" />
                                        <asp:Button ID="btnCancel" runat="server" CssClass="btn blue" Text="Cancel" OnClick="btnCancel_Click" Visible="false" />
                                        <asp:Button ID="btnUpdate" runat="server" CssClass="btn blue" Text="Modify" OnClick="btnUpdate_Click" Visible="false" />
                                        <asp:Button ID="BtnAddNew" runat="server" CssClass="btn blue" Text="Add New" OnClick="BtnAddNew_Click" Visible="false" />
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

    <div class="row" style="margin: 7px 0 7px 0">
        <div class="col-lg-12" style="height: 100%">
            <div class="table-scrollable" style="height: 400px; overflow-x: scroll; overflow-y: scroll; padding-bottom: 10px; width: 100%">
                <table class="table table-striped table-bordered table-hover" width="100%">
                    <thead>
                        <tr>
                            <th>
                                <asp:GridView ID="grdBal" runat="server" CssClass="tabbable-line" HeaderStyle-BackColor="#1e9275" HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Center"
                                    DataKeyNames="ID" AutoGenerateColumns="false" Width="100%" OnRowDataBound="grdBal_RowDataBound" ShowFooter="true">
                                    <Columns>
                                        
                                        <asp:TemplateField HeaderText="Select" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkSelect" Text="Select" CommandName="select" CommandArgument='<%#Eval("ID")%>' OnClick="lnkSelect_Click" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Entry Date" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEntryDate" runat="server" Text='<%# Eval("EntryDate") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                Total:
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Product Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGlName" runat="server" Text='<%# Eval("GlName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate></FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Acc No" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAccountNo" runat="server" Text='<%# Eval("AccountNo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate></FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Narration">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNarration" runat="server" Text='<%# Eval("Narration") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate></FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Debit" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDebit" runat="server" Text='<%#Eval("Debit") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblDrTotal" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Credit" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCredit" runat="server" Text='<%#Eval("Credit") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblCrTotal" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="TrxType" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTrxType" runat="server" Text='<%# Eval("TrxType") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate></FooterTemplate>
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

</asp:Content>

