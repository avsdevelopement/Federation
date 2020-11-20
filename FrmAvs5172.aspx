<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAvs5172.aspx.cs" Inherits="FrmAvs5172" %>

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

        function onlyAlphabets(e, t) {
            try {
                if (window.event) {
                    var charCode = window.event.keyCode;
                }
                else if (e) {
                    var charCode = e.which;
                }
                else { return true; }
                if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123))
                    return true;
                else
                    return false;
            }
            catch (err) {
                alert(err.Description);
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue">
                <div class="portlet-title">
                    <div class="caption">
                        TD Withdrawal
                        <asp:Literal ID="LtrlHeading" runat="server"></asp:Literal>
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div style="border: 1px solid #3598dc">
                                        <div class="portlet-body">

                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Account Information : </strong></div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin: 7px 0 7px 0;">
                                                <div class="col-lg-12" style="height: 28px">
                                                    <div class="col-md-2">
                                                        <asp:RadioButtonList ID="Rdeatils" RepeatDirection="Horizontal" Style="width: 380px;" runat="server">
                                                            <asp:ListItem Text="Withdrawal" Selected="True" Value="1" />
                                                            <asp:ListItem Text="Close" Value="2" />
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin: 7px 0 7px 0;">
                                                <div class="col-lg-12" style="height: 28px">
                                                    <label class="control-label col-md-2">Product Code:<span class="required">*</span></label>
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="TxtPrd" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" AutoPostBack="true" OnTextChanged="TxtPrd_TextChanged" PlaceHolder="Type"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="input-icon">
                                                            <i class="fa fa-search"></i>
                                                            <asp:TextBox ID="Txtprdname" CssClass="form-control" placeholder="Product Name" OnTextChanged="Txtprdname_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                                            <div id="CustList1" style="height: 200px; overflow-y: scroll;"></div>
                                                            <asp:AutoCompleteExtender ID="AutoGlName1" runat="server" TargetControlID="Txtprdname" UseContextKey="true"
                                                                CompletionInterval="1" CompletionSetCount="20" MinimumPrefixLength="1" EnableCaching="true"
                                                                ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList1" ServiceMethod="GetGlName">
                                                            </asp:AutoCompleteExtender>
                                                        </div>
                                                    </div>
                                                    <label class="control-label col-md-1">Acc No:<span class="required">*</span></label>
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="TxtAccNo" runat="server" PlaceHolder="No" onkeypress="javascript:return isNumber (event)" OnTextChanged="TxtAccNo_TextChanged" AutoPostBack="true" CssClass="form-control"></asp:TextBox>&nbsp;&nbsp;
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="input-icon">
                                                            <i class="fa fa-search"></i>
                                                            <asp:TextBox ID="TxtAccName" runat="server" PlaceHolder="Acc Name" OnTextChanged="TxtAccName_TextChanged" AutoPostBack="true" CssClass="form-control"></asp:TextBox>
                                                            <div id="CustList2" style="height: 200px; overflow-y: scroll;"></div>
                                                            <asp:AutoCompleteExtender ID="AutoAccName1" runat="server" TargetControlID="TxtAccName" UseContextKey="true"
                                                                CompletionInterval="1" CompletionSetCount="20" MinimumPrefixLength="1" EnableCaching="true"
                                                                ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList2" ServiceMethod="GetAccName">
                                                            </asp:AutoCompleteExtender>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin: 7px 0 7px 0;">
                                                <div class="col-lg-12" style="height: 28px">
                                                    <label class="control-label col-md-2">Account Status<span class="required"> </span></label>
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="TxtAccSTS" runat="server" Enabled="false" onkeypress="javascript:return isNumber (event)" PlaceHolder="No" CssClass="form-control"></asp:TextBox>&nbsp;&nbsp;                                                    
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TxtAccSTSName" runat="server" Enabled="false" PlaceHolder="Acc Type Name" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <label class="control-label col-md-1">Open User:<span class="required"></span></label>
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="TxtAccType" runat="server" Enabled="false" PlaceHolder="No" onkeypress="javascript:return isNumber (event)" CssClass="form-control"></asp:TextBox>&nbsp;&nbsp;
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TxtAccTName" runat="server" Enabled="false" PlaceHolder="Open Name" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin: 7px 0 7px 0;">
                                                <div class="col-lg-12" style="height: 28px">
                                                    <label class="control-label col-md-2">Clear Balance:<span class="required"> </span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtCBal" runat="server" PlaceHolder="Clear Balance" Enabled="false" onkeypress="javascript:return isNumber (event)" CssClass="form-control"></asp:TextBox>&nbsp;&nbsp;                                                    
                                                    </div>
                                                    <label class="control-label col-md-1">Intrest Applied:</label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtIntAppld" runat="server" PlaceHolder="Int Applied" Enabled="false" onkeypress="javascript:return isNumber (event)" CssClass="form-control"></asp:TextBox>&nbsp;&nbsp;                                                                                                            
                                                    </div>
                                                    <label class="control-label col-md-1">Total Payable:</label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="Txtpayable" AutoPostBack="true" Enabled="false" runat="server" PlaceHolder="Total Payable" onkeypress="javascript:return isNumber (event)" CssClass="form-control"></asp:TextBox>&nbsp;&nbsp;                                                                                                            
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin: 7px 0 7px 0;">
                                                <div class="col-lg-12" style="height: 28px">
                                                    <label class="control-label col-md-2">Total Balance:<span class="required"> </span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtTotalbal" runat="server" PlaceHolder="Total Balance" Enabled="false" onkeypress="javascript:return isNumber (event)" CssClass="form-control"></asp:TextBox>&nbsp;&nbsp;                                                    
                                                    </div>
                                                    <label class="control-label col-md-1">Opening Date:</label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtOpeningDate" runat="server" PlaceHolder="Opening Date" Enabled="false" CssClass="form-control"></asp:TextBox>&nbsp;&nbsp;                                                                                                            
                                                    </div>
                                                    <div class="col-md-2"></div>
                                                </div>
                                            </div>

                                            <%-- <div class="row" style="margin: 7px 0 7px 0;">
                                                <div class="col-lg-12" style="height: 28px">
                                                    <label class="control-label col-md-2">Charges Per Cheque:</label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtCCheque" runat="server" PlaceHolder="Charge Per Cheque" AutoPostBack="true" OnTextChanged="TxtCCheque_TextChanged" onkeypress="javascript:return isNumber (event)" CssClass="form-control"></asp:TextBox>&nbsp;&nbsp;                                                                                                            
                                                    </div>
                                                    <label class="control-label col-md-2">Unused Cheque:</label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtCunused" runat="server" PlaceHolder="Unused Cheque" OnTextChanged="TxtCunused_TextChanged" AutoPostBack="true" onkeypress="javascript:return isNumber (event)" CssClass="form-control"></asp:TextBox>&nbsp;&nbsp;                                                                                                            
                                                    </div>
                                                    <label class="control-label col-md-2">Unused Cheque Charge:</label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtUnusedChrg" runat="server" Enabled="false" PlaceHolder="Unused chequecharge" CssClass="form-control"></asp:TextBox>&nbsp;&nbsp;                                                                                                            
                                                    </div>
                                                </div>
                                            </div>--%>

                                            <%--<div class="row" style="margin: 7px 0 7px 0;">
                                                <div class="col-lg-12" style="height: 28px">
                                                    <label class="control-label col-md-2">Service Charge:</label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtservChg" runat="server" PlaceHolder="Service Charge" OnTextChanged="TxtservChg_TextChanged" AutoPostBack="true" onkeypress="javascript:return isNumber (event)" CssClass="form-control"></asp:TextBox>&nbsp;&nbsp;                                                                                                            
                                                    </div>
                                                    <label class="control-label col-md-2">Early Closure Charge:</label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxterlyClosure" runat="server" PlaceHolder="Early Closure Charge" OnTextChanged="TxterlyClosure_TextChanged" AutoPostBack="true" onkeypress="javascript:return isNumber (event)" CssClass="form-control"></asp:TextBox>&nbsp;&nbsp;                                                                                                            
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin: 7px 0 7px 0;">
                                                <div class="col-lg-12" style="height: 28px">
                                                    <label class="control-label col-md-2">GST State:</label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtGSTState" runat="server" PlaceHolder="GST State" OnTextChanged="TxtGSTState_TextChanged" Enable="true" AutoPostBack="true" onkeypress="javascript:return isNumber (event)" CssClass="form-control"></asp:TextBox>&nbsp;&nbsp;                                                                                                            
                                                    </div>
                                                    <label class="control-label col-md-2">GST Central:</label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtGstCentral" runat="server" PlaceHolder="GST Central" OnTextChanged="TxtGstCentral_TextChanged" Enable="true" AutoPostBack="true" onkeypress="javascript:return isNumber (event)" CssClass="form-control"></asp:TextBox>&nbsp;&nbsp;                                                                                                            
                                                    </div>
                                                    <label class="control-label col-md-1">Total GST:</label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxttotalGSt" runat="server" PlaceHolder="Total GST" Enabled="false" onkeypress="javascript:return isNumber (event)" CssClass="form-control"></asp:TextBox>&nbsp;&nbsp;                                                                                                            
                                                    </div>
                                                </div>
                                            </div>--%>

                                            <div class="row" style="margin: 7px 0 7px 0;">
                                                <div class="col-lg-12" style="height: 28px">
                                                    <%-- <label class="control-label col-md-2">Total Deduction:</label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="Txtdeductn" runat="server" PlaceHolder="Total Ded" Enabled="false" CssClass="form-control"></asp:TextBox>&nbsp;&nbsp;                                                                                                            
                                                    </div>--%>
                                                    <label class="control-label col-md-2">Withdrawal Amount:</label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtNetBal" runat="server" PlaceHolder="Withdrawal Amount" onkeypress="javascript:return isNumber (event)" AutoPostBack="true" OnTextChanged="TxtNetBal_TextChanged" CssClass="form-control"></asp:TextBox>&nbsp;&nbsp;
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div style="border: 1px solid #3598dc">
                                        <div class="portlet-body">
                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Payment Details : </strong></div>
                                                </div>
                                            </div>
                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Payment Type :<span class="required"> *</span></label>
                                                    <div class="col-md-2">
                                                        <asp:DropDownList ID="ddlPayType" OnSelectedIndexChanged="ddlPayType_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div id="divBranch" visible="false" runat="server">
                                                <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                    <div class="col-lg-12">
                                                        <label class="control-label col-md-2">Branch Name : <span class="required">*</span></label>
                                                        <div class="col-md-2">
                                                            <asp:DropDownList ID="ddlPayBrName" CssClass="form-control" OnSelectedIndexChanged="ddlPayBrName_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <label class="control-label col-md-2">Branch Code : <span class="required">*</span></label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtPayBrCode" Enabled="false" CssClass="form-control" runat="server" PlaceHolder="Branch Code"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div id="Transfer" visible="false" runat="server">
                                                <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                    <div class="col-lg-12">
                                                        <label class="control-label col-md-2">Prod Code <span class="required">*</span></label>
                                                        <div class="col-md-1">
                                                            <asp:TextBox ID="txtProdType1" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtProdType1_TextChanged" PlaceHolder="Product Type"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="input-icon">
                                                                <i class="fa fa-search"></i>
                                                                <asp:TextBox ID="txtProdName1" CssClass="form-control" PlaceHolder="Product Name" OnTextChanged="txtProdName1_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                                                <div id="CustList3" style="height: 200px; overflow-y: scroll;"></div>
                                                                <asp:AutoCompleteExtender ID="AutoGlName2" runat="server" TargetControlID="txtProdName1" UseContextKey="true"
                                                                    CompletionInterval="1" CompletionSetCount="20" MinimumPrefixLength="1" EnableCaching="true"
                                                                    ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList3" ServiceMethod="GetGlName">
                                                                </asp:AutoCompleteExtender>
                                                            </div>
                                                        </div>
                                                        <label class="control-label col-md-1">Acc No<span class="required">*</span></label>
                                                        <div class="col-md-1">
                                                            <asp:TextBox ID="txtAccNo1" onkeypress="javascript:return isNumber(event)" AutoPostBack="true" OnTextChanged="txtAccNo1_TextChanged" runat="server" CssClass="form-control" TabIndex="1" />
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="input-icon">
                                                                <i class="fa fa-search"></i>
                                                                <asp:TextBox ID="txtAccName1" CssClass="form-control" PlaceHolder="Customer Name" runat="server" AutoPostBack="true" OnTextChanged="txtAccName1_TextChanged"></asp:TextBox>
                                                                <div id="CustList4" style="height: 200px; overflow-y: scroll;"></div>
                                                                <asp:AutoCompleteExtender ID="AutoAccName2" runat="server" TargetControlID="txtAccName1" UseContextKey="true"
                                                                    CompletionInterval="1" CompletionSetCount="20" MinimumPrefixLength="1" EnableCaching="true"
                                                                    ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList4" ServiceMethod="GetAccName">
                                                                </asp:AutoCompleteExtender>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div id="Transfer1" visible="false" runat="server">
                                                <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                    <div class="col-lg-12">
                                                        <label class="control-label col-md-2">Instrument No : <span class="required">*</span></label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtChequeNo" placeholder="CHEQUE NUMBER" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" OnTextChanged="TxtChequeNo_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        </div>
                                                        <label class="control-label col-md-2">Instrument Date : <span class="required">*</span></label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtChequeDate" placeholder="CHEQUE DATE" CssClass="form-control" onkeyup="FormatIt(this)" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div id="DivAmount" visible="false" runat="server">
                                                <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                    <div class="col-lg-12">
                                                        <label class="control-label col-md-2">Net Amount : </label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtAmount" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                        </div>
                                                        <label class="control-label col-md-2">Naration : </label>
                                                        <div class="col-md-5">
                                                            <asp:TextBox ID="txtNarration" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-actions">
                                        <div class="row">
                                            <div class="col-md-offset-4 col-md-9">
                                                <asp:Button ID="BtnSubmit" runat="server" Text="Submit" CssClass="btn blue" UseSubmitBehavior="false" OnClick="BtnSubmit_Click" OnClientClick="this.disabled='true';this.value='Please Wait'" />
                                                <%--<asp:Button ID="btnPrint" runat="server" Text="Int Report" CssClass="btn btn-primary" OnClick="btnPrint_Click" />--%>
                                                <asp:Button ID="BtnClear" runat="server" Text="Clear" CssClass="btn blue" OnClick="BtnClear_Click" />
                                                <asp:Button ID="BtnExit" runat="server" Text="Exit" CssClass="btn blue" OnClick="BtnExit_Click" />
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

    <div class="row" id="Entry" runat="server">
        <div class="col-lg-12">
            <div class="table-scrollable">
                <table class="table table-striped table-bordered table-hover" width="100%">
                    <asp:Label ID="Label1" runat="server" Text="Voucher Entry" Width="160px" Style="font-size: medium; text-align: center;"></asp:Label>
                    <thead>
                        <tr>
                            <th>
                                <asp:GridView ID="Grdentry" runat="server" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" AllowPaging="True"
                                    EditRowStyle-BackColor="#FFFF99" PageIndex="10" PageSize="25" PagerStyle-CssClass="pgr" Width="100%" ShowFooter="true">
                                    <Columns>

                                        <asp:TemplateField HeaderText="Report" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="Lnk_ReportView" runat="server" OnClick="Lnk_ReportView_Click" CssClass="glyphicon glyphicon-plus" CommandName='<%# Eval("SubGlCode") %>' CommandArgument='<%#Eval("SetNo")+","+ Eval("Brcd")+","+ Eval("SubGlCode")+","+Eval("Accno")%>' ></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="SrNo">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSrno" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Brcd" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBrcd" runat="server" Text='<%# Eval("Brcd") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="SubGlCode" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSubGlCode" runat="server" Text='<%# Eval("SubGlCode") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Accno" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAccNo" runat="server" Text='<%# Eval("AccNo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="TransDate" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTransDate" runat="server" Text='<%# Eval("EntryDate") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Particular" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblParti" runat="server" Text='<%# Eval("PartiCulars") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Amount" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="SetNo" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSetNo" runat="server" Text='<%# Eval("SetNo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="ScrollNo" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblScrollNo" runat="server" Text='<%# Eval("ScrollNo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Authorise" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkAutorise" runat="server" CommandArgument='<%#Eval("SetNo")+","+ Eval("ScrollNo")+","+ Eval("SubGlCode")+","+Eval("Accno")%>' CommandName="select" class="glyphicon glyphicon-pencil" OnClick="LnkAutorise_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <FooterStyle BackColor="#ccffcc" Font-Bold="True" ForeColor="Black" HorizontalAlign="Right" BorderStyle="None" />
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
                    <button class="btn btn-default">Close</button>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdnscroll" runat="server" />
    <asp:HiddenField ID="hdnRow" runat="server" Value="0" />
    <asp:HiddenField ID="hdnset" runat="server" />
    <asp:HiddenField ID="hdnamount" runat="server" />

</asp:Content>

