<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAVS5083.aspx.cs" Inherits="FrmAVS5083" %>

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

        function AdminTotal(obj) {
            var Admin1 = document.getElementById('<%=txtAdmin1.ClientID%>').value || 0;
            var Admin2 = document.getElementById('<%=txtAdmin2.ClientID%>').value || 0;
            var Result = parseFloat(Admin1) + parseFloat(Admin2);
            if (!isNaN(Result)) {
                document.getElementById('<%=txtTotalAdmin.ClientID%>').value = Result;
            }
        }

        function GSTTotal(obj) {
            var SGST = document.getElementById('<%=txtSGST.ClientID%>').value || 0;
            var CGST = document.getElementById('<%=txtCGST.ClientID%>').value || 0;
            var Result = parseFloat(SGST) + parseFloat(CGST);
            if (!isNaN(Result)) {
                document.getElementById('<%=txtTotalGST.ClientID%>').value = Result;
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
                        Share Refund
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab__blue">
                                    </div>
                                    <div style="border: 1px solid #3598dc">

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <div class="col-md-12" style="border-bottom: 2px solid #3598dc;"><strong style="color: #3598dc">Member Details</strong></div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Product Code : <span class="required">* </span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtProdType" runat="server" onkeypress="javascript:return isNumber(event)" PlaceHolder="Product Code" OnTextChanged="txtProdType_TextChanged" AutoPostBack="true" TabIndex="5" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtProdName" runat="server" PlaceHolder="Product Name" OnTextChanged="txtProdName_TextChanged" AutoPostBack="true" TabIndex="6" CssClass="form-control"></asp:TextBox>
                                                    <div id="CustList" style="height: 200px; overflow-y: scroll;"></div>
                                                    <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="txtProdName"
                                                        UseContextKey="true"
                                                        CompletionInterval="1"
                                                        CompletionSetCount="20"
                                                        MinimumPrefixLength="1"
                                                        EnableCaching="true"
                                                        ServicePath="~/WebServices/Contact.asmx"
                                                        ServiceMethod="GetGlName" CompletionListElementID="CustList">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Account No : <span class="required">* </span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtAccNo" runat="server" PlaceHolder="Account No" onkeypress="javascript:return isNumber(event)" OnTextChanged="txtAccNo_TextChanged" AutoPostBack="true" TabIndex="7" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtAccName" runat="server" PlaceHolder="Account Holder Name" OnTextChanged="txtAccName_TextChanged" AutoPostBack="true" TabIndex="8" CssClass="form-control"></asp:TextBox>
                                                    <div id="CustList2" style="height: 200px; overflow-y: scroll;"></div>
                                                    <asp:AutoCompleteExtender ID="AutoAccname" runat="server" TargetControlID="txtAccName"
                                                        UseContextKey="true"
                                                        CompletionInterval="1"
                                                        CompletionSetCount="20"
                                                        MinimumPrefixLength="1"
                                                        EnableCaching="true"
                                                        ServicePath="~/WebServices/Contact.asmx"
                                                        ServiceMethod="GetAccName" CompletionListElementID="CustList2">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                                <label class="control-label col-md-1">CustNo : </label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtCustNo" CssClass="form-control" Enabled="false" runat="server" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Balance : </label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtBalance" Enabled="false" CssClass="form-control" runat="server" placeholder="Clear Balance"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-1">Dividend : </label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtDividentBal" Enabled="false" CssClass="form-control" runat="server" placeholder="Divident Balance"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-2">Total Balance : </label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtTotalBalance" Enabled="false" CssClass="form-control" runat="server" placeholder="Total Balance"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Acc Status : </label>
                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="ddlAccStatus" Enabled="false" CssClass="form-control" runat="server"></asp:DropDownList>
                                                </div>
                                                <label class="control-label col-md-1">Open Date : <span class="required"></span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtAccOpen" Enabled="false" CssClass="form-control" onkeyup="FormatIt(this)" PlaceHolder="DD/MM/YYYY" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div id="DivShareDividend" runat="server" class="col-md-12">
                                            <div class="table-scrollable" style="width: 100%; height: auto; max-height: 150px; overflow-x: auto; overflow-y: auto">
                                                <table class="table table-striped table-bordered table-hover" width="100%">
                                                    <thead>
                                                        <tr>
                                                            <th>
                                                                <asp:GridView ID="grdShareDividend" runat="server" AlternatingRowStyle-CssClass="alt" CssClass="mGrid"
                                                                    AutoGenerateColumns="false" EditRowStyle-BackColor="#FFFF99" DataKeyNames="id" Width="100%">
                                                                    <Columns>

                                                                        <asp:TemplateField HeaderText="Select" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chk" Checked="true" runat="server" OnCheckedChanged="chk_CheckedChanged" AutoPostBack="true"/>
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

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <div class="col-md-12" style="border-bottom: 2px solid #3598dc;"><strong style="color: #3598dc"></strong></div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <div class="col-md-12" style="border-bottom: 1px solid  #3598dc;"><strong style="color: #3598dc">Deduction Part</strong></div>
                                            </div>
                                        </div>


                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Admin-1 Charge</label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtAdmin1" Enabled="false" onblur="AdminTotal()" Text="0" CssClass="form-control" runat="server" placeholder="Admin Charge"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-2">Admin-1 Charge</label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtAdmin2" Enabled="false" onblur="AdminTotal()" Text="0" CssClass="form-control" runat="server" placeholder="Admin Change"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-2">Total Admin</label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtTotalAdmin" Enabled="false" Text="0" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">C-GST Charge</label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtCGST" Enabled="false" onblur="GSTTotal()" Text="0" CssClass="form-control" runat="server" placeholder="IGST Charge"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-2">S-GST Charge</label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtSGST" Enabled="false" onblur="GSTTotal()" Text="0" CssClass="form-control" runat="server" placeholder="SGST Charge"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-2">Total GST</label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtTotalGST" Enabled="false" Text="0" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="col-md-12" style="border-bottom: 2px solid #3598dc;"><strong style="color: #3598dc"></strong></div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="col-md-12" style="border-bottom: 1px solid  #3598dc;"><strong style="color: #3598dc">Payment Details</strong></div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
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


                                        <div id="Transfer" visible="false" runat="server">
                                            <div class="row" style="margin: 7px 0 7px 0">
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

                                            <div class="row" style="margin: 7px 0 7px 0">
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
                                                            <div id="CustList3" style="height: 200px; overflow-y: scroll;"></div>
                                                            <asp:AutoCompleteExtender ID="AutoPayGlName" runat="server" TargetControlID="txtPayProdName" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList3" ServiceMethod="GetGlName">
                                                            </asp:AutoCompleteExtender>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-1">
                                                        <label class="control-label ">Acc No :<span class="required"> *</span></label>
                                                    </div>
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="TxtPayAccNo" CssClass="form-control" PlaceHolder="Acc No" runat="server" AutoPostBack="true" OnTextChanged="TxtPayAccNo_TextChanged"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="input-icon">
                                                            <i class="fa fa-search"></i>
                                                            <asp:TextBox ID="TxtPayAccName" CssClass="form-control" PlaceHolder="Search Customer Name" runat="server" AutoPostBack="true" OnTextChanged="TxtPayAccName_TextChanged"></asp:TextBox>
                                                            <div id="CustList4" style="height: 200px; overflow-y: scroll;"></div>
                                                            <asp:AutoCompleteExtender ID="AutoPayAccName" runat="server" TargetControlID="TxtPayAccName" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList4" ServiceMethod="GetAccName">
                                                            </asp:AutoCompleteExtender>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div id="Transfer1" visible="false" runat="server">
                                                <div class="row" style="margin: 7px 0 7px 0">
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
                                            <div class="row" style="margin: 7px 0 7px 0">
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

                                    </div>
                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9">
                                        <asp:Button ID="btnSubmit" runat="server" onkeypress="IsTally();" CssClass="btn blue" Text="Submit" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnClear" runat="server" Text="Clear All" CssClass="btn btn-primary" OnClick="btnClear_Click" />
                                        <asp:Button ID="btnExit" runat="server" Text="Exit" CssClass="btn btn-primary" OnClick="btnExit_Click" />
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

