<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmLoanODSummary.aspx.cs" Inherits="FrmLoanODSummary" %>

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
                        Loan Overdue List
                                <asp:Literal ID="LtrlHeading" runat="server"></asp:Literal>
                    </div>
                </div>
                <div class="portlet-body">
                    <div id="Depositdiv" runat="server">
                        <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-12">
                                <label class="control-label col-md-2">A/C Type</label>
                                <div class="col-md-9">
                                    <asp:RadioButtonList ID="rbtnType" runat="server" RepeatDirection="Horizontal" CssClass="radio-list">
                                        <asp:ListItem Text="Normal A/c" Value="1" style="margin: 15px;" Selected="True" />
                                        <asp:ListItem Text="Court Files A/C" Value="2" style="margin: 25px;" />
                                        <asp:ListItem Text="All A/C's" Value="3" style="margin: 25px;" />
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-12">
                                <label class="control-label col-md-2">From Branch<span class="required"></span></label>
                                <div class="col-md-1">
                                    <asp:TextBox ID="Txtfrmbrcd" onkeypress="javascript:return isNumber (event)" OnTextChanged="Txtfrmbrcd_TextChanged" AutoPostBack="true" CssClass="form-control" TabIndex="1" runat="server" />
                                </div>

                                <label class="control-label col-md-2">To Branch<span class="required"></span></label>
                                <div class="col-md-1">
                                    <asp:TextBox ID="Txttobrcd" onkeypress="javascript:return isNumber (event)" OnTextChanged="Txttobrcd_TextChanged" AutoPostBack="true" CssClass="form-control" TabIndex="2" runat="server" />
                                </div>
                                <label class="control-label col-md-2">AsOnDate<span class="required"></span></label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="TxtAsonDate" TabIndex="3" MaxLength="10" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="dd/MM/yyyy" runat="server"></asp:TextBox>
                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtAsonDate">
                                    </asp:TextBoxWatermarkExtender>
                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtAsonDate">
                                    </asp:CalendarExtender>
                                </div>
                            </div>
                        </div>

                        <%--  <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-12">
                               <%-- <label class="control-label col-md-2">All/Selected <span class="required"></span></label>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="DdlAlS" TabIndex="3" runat="server" CssClass="form-control" OnSelectedIndexChanged="DdlAlS_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                        <asp:ListItem Value="A">All</asp:ListItem>
                                        <asp:ListItem Value="S">Selected</asp:ListItem>
                                    </asp:DropDownList>
                                </div>--%>
                        <%--   <div class="col-md-2">
                                    </div>
                           
                            </div>
                        </div>--%>

                        <%--<div id="div_prd" runat="server" visible="false" class="row" style="margin: 7px 0 7px 0">--%>
                        <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-12">
                                <label class="control-label col-md-2">From ProductCode <span class="required"></span></label>
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
                                <label class="control-label col-md-2">To ProductCode <span class="required"></span></label>
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
                                <label class="control-label col-md-2">From Account <span class="required"></span></label>
                                <div class="col-md-1">
                                    <asp:TextBox ID="TxtFaccno" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="No" OnTextChanged="TxtFaccno_TextChanged" AutoPostBack="true" TabIndex="8" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="TxtFaccname" CssClass="form-control" placeholder="Account Name" OnTextChanged="TxtFaccname_TextChanged" AutoPostBack="true" TabIndex="9" runat="server"></asp:TextBox>
                                    <div id="CustList3" style="height: 200px; overflow-y: scroll;"></div>
                                    <asp:AutoCompleteExtender ID="AutoAccname" runat="server" TargetControlID="TxtFaccname"
                                        UseContextKey="true"
                                        CompletionInterval="1"
                                        CompletionSetCount="20"
                                        MinimumPrefixLength="1"
                                        EnableCaching="true"
                                        ServicePath="~/WebServices/Contact.asmx"
                                        ServiceMethod="GetAccName" CompletionListElementID="CustList3">
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
                    </div>

                    <div class="row" style="margin: 7px 0 7px 0; text-align: center">
                        <div class="col-lg-12">
                            <div class="col-md-6">
                                <asp:Button ID="BtnPrint" runat="server" Text="Report" CssClass="btn btn-success" OnClick="BtnPrint_Click" OnClientClick="javascript:return validate();" TabIndex="12" />
                                <asp:Button ID="BtnClear" runat="server" Text="Clear" CssClass="btn btn-success" OnClick="BtnClear_Click" TabIndex="13" />
                                <asp:Button ID="BtnExit" runat="server" Text="Exit" CssClass="btn btn-success" OnClick="BtnExit_Click" TabIndex="14" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%-- </div>--%>
</asp:Content>

