<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmHeadTrfTrans.aspx.cs" Inherits="FrmHeadTrfTrans" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function FormatIt(obj) {

            if (obj.value.length == 2) // Day
                obj.value = obj.value + "/";
            if (obj.value.length == 5) // month 
                obj.value = obj.value + "/";
            if (obj.value.length == 11) // year 
                alert("Please Enter valid Date");
        }

        <%-- Only alphabet --%>
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

        <%-- Only Numbers --%>
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box green">
                <div class="portlet-title">
                    <div class="caption">
                        Balance Trf To Shares Or Other Product
                                <asp:Literal ID="LtrlHeading" runat="server"></asp:Literal>
                    </div>
                </div>
                <div class="portlet-body">
                    <div id="Depositdiv" runat="server">
                        <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-12">
                                <label class="control-label col-md-2">Branch<span class="required"></span></label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="Txtfrmbrcd" onkeypress="javascript:return isNumber (event)" OnTextChanged="Txtfrmbrcd_TextChanged" AutoPostBack="true" CssClass="form-control" TabIndex="1" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-12">
                                <label class="control-label col-md-2">Product <span class="required"></span></label>
                                <div class="col-md-1">
                                    <asp:TextBox ID="TxtFprdcode" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="Code" OnTextChanged="TxtFprdcode_TextChanged" TabIndex="4" AutoPostBack="true" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="TxtFprdname" CssClass="form-control" placeholder="Product Name" OnTextChanged="TxtFprdname_TextChanged" AutoPostBack="true" TabIndex="5" runat="server"></asp:TextBox>
                                    <div id="CustList1" style="height: 200px; overflow-y: scroll;"></div>
                                    <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="TxtFprdname"
                                        UseContextKey="true"
                                        CompletionInterval="1"
                                        CompletionSetCount="20"
                                        MinimumPrefixLength="1"
                                        EnableCaching="true"
                                        ServicePath="~/WebServices/Contact.asmx"
                                        ServiceMethod="getglname" CompletionListElementID="CustList1">
                                    </asp:AutoCompleteExtender>
                                </div>
                                <label class="control-label col-md-2">Transfer Product<span class="required"></span></label>
                                <div class="col-md-1">
                                    <asp:TextBox ID="TxtTprdcode" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="Code" OnTextChanged="TxtTprdcode_TextChanged" TabIndex="6" AutoPostBack="true" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="TxtTprdname" CssClass="form-control" placeholder="Product Name" OnTextChanged="TxtTprdname_TextChanged" AutoPostBack="true" TabIndex="7" runat="server"></asp:TextBox>
                                    <div id="CustList2" style="height: 200px; overflow-y: scroll;"></div>
                                    <asp:AutoCompleteExtender ID="autoglname1" runat="server" TargetControlID="TxtTprdname"
                                        UseContextKey="true"
                                        CompletionInterval="1"
                                        CompletionSetCount="20"
                                        MinimumPrefixLength="1"
                                        EnableCaching="true"
                                        ServicePath="~/WebServices/Contact.asmx"
                                        ServiceMethod="getglname" CompletionListElementID="CustList2">
                                    </asp:AutoCompleteExtender>
                                </div>
                            </div>
                        </div>

                        <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-12">
                                <label class="control-label col-md-2">Amt Greater Trf Product <span class="required"></span></label>
                                <div class="col-md-1">
                                    <asp:TextBox ID="TxtTrfprdcode" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="Code" OnTextChanged="TxtTrfprdcode_TextChanged" TabIndex="4" AutoPostBack="true" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="TxtTrfprdname" CssClass="form-control" placeholder="Product Name" OnTextChanged="TxtTrfprdname_TextChanged" AutoPostBack="true" TabIndex="5" runat="server"></asp:TextBox>
                                    <div id="CustList3" style="height: 200px; overflow-y: scroll;"></div>
                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="TxtTrfprdname"
                                        UseContextKey="true"
                                        CompletionInterval="1"
                                        CompletionSetCount="20"
                                        MinimumPrefixLength="1"
                                        EnableCaching="true"
                                        ServicePath="~/WebServices/Contact.asmx"
                                        ServiceMethod="getglname" CompletionListElementID="CustList3">
                                    </asp:AutoCompleteExtender>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-12">
                                <label class="control-label col-md-2">From Account <span class="required"></span></label>
                                <div class="col-md-1">
                                    <asp:TextBox ID="TxtFaccno" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="No" OnTextChanged="TxtFaccno_TextChanged" AutoPostBack="true" TabIndex="8" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="TxtFaccname" CssClass="form-control" placeholder="Account Name" OnTextChanged="TxtFaccname_TextChanged" AutoPostBack="true" TabIndex="9" runat="server"></asp:TextBox>
                                    <div id="CustList5" style="height: 200px; overflow-y: scroll;"></div>
                                    <asp:AutoCompleteExtender ID="AutoAccname" runat="server" TargetControlID="TxtFaccname"
                                        UseContextKey="true"
                                        CompletionInterval="1"
                                        CompletionSetCount="20"
                                        MinimumPrefixLength="1"
                                        EnableCaching="true"
                                        ServicePath="~/WebServices/Contact.asmx"
                                        ServiceMethod="GetAccName" CompletionListElementID="CustList5">
                                    </asp:AutoCompleteExtender>
                                </div>
                                <label class="control-label col-md-2">To Account <span class="required"></span></label>
                                <div class="col-md-1">
                                    <asp:TextBox ID="TxtTaccno" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="No" OnTextChanged="TxtTaccno_TextChanged" AutoPostBack="true" TabIndex="10" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="TxtTaccname" CssClass="form-control" placeholder="Account Name" OnTextChanged="TxtTaccname_TextChanged" AutoPostBack="true" TabIndex="11" runat="server"></asp:TextBox>
                                    <div id="CustList4" style="height: 200px; overflow-y: scroll;"></div>
                                    <asp:AutoCompleteExtender ID="autoAccname1" runat="server" TargetControlID="TxtTaccname"
                                        UseContextKey="true"
                                        CompletionInterval="1"
                                        CompletionSetCount="20"
                                        MinimumPrefixLength="1"
                                        EnableCaching="true"
                                        ServicePath="~/WebServices/Contact.asmx"
                                        ServiceMethod="GetAccName" CompletionListElementID="CustList4">
                                    </asp:AutoCompleteExtender>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-12">
                                <label class="control-label col-md-2">As On Date <span class="required"></span></label>
                                <div class="col-lg-2">
                                    <asp:TextBox ID="TxtFDate" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" onchange="checkdate(this)"></asp:TextBox>
                                    <asp:TextBoxWatermarkExtender ID="TxtDate_WatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtFDate">
                                    </asp:TextBoxWatermarkExtender>
                                    <asp:CalendarExtender ID="TxtDate_CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtFDate">
                                    </asp:CalendarExtender>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-12">
                                <label class="control-label col-md-2">Amount</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="TxtAmt" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="margin: 7px 0 7px 0; padding-top: 10px; text-align: center">
                            <div class="col-lg-12">
                                <div class="col-md-6">
                                    <asp:Button ID="BtnPrint" runat="server" Text="Trail Report" CssClass="btn btn-success" OnClick="BtnPrint_Click" OnClientClick="javascript:return validate();" TabIndex="12" />
                                    <asp:Button ID="BtnPost" runat="server" CssClass="btn btn-success" Text="Post" OnClick="BtnPost_Click" OnClientClick="Javascript:return isvalidate();" />
                                    <asp:Button ID="BtnClear" runat="server" Text="Clear" CssClass="btn btn-success" OnClick="BtnClear_Click" TabIndex="13" />
                                    <asp:Button ID="BtnExit" runat="server" Text="Exit" CssClass="btn btn-success" OnClick="BtnExit_Click" TabIndex="14" />
                                </div>
                            </div>
                        </div>
                    </div>
            </div>
        </div>
    </div>
</asp:Content>

