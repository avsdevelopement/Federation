<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmMemberPassbook.aspx.cs" Inherits="FrmMemberPassbook" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function isNumber(evt)
        {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>
    <script type="text/javascript">
        function FormatIt(obj) {
            if (obj.value.length == 2) // Day
                obj.value = obj.value + "/";
            if (obj.value.length == 5) // month 
                obj.value = obj.value + "/";
            if (obj.value.length == 11) // year 
                alert("Please Enter valid Date");
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        Member PassBook Report
                        <asp:Literal ID="ltrlHeader" runat="server"></asp:Literal>
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
                                                <div class="col-md-2">
                                                    <asp:RadioButtonList ID="Rdb_Type" RepeatDirection="Horizontal" Style="width: 380px;" runat="server" OnSelectedIndexChanged="Rdb_Type_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Value="1" Text="All" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="Specific"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-1">
                                                    <label class="control-label ">Branch Code</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtBrID" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server" TabIndex="1"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0" runat="server" id="Div_DEP">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-1">Division : <span class="required">* </span></label>
                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="DdlRecDiv" CssClass="form-control" runat="server" Enabled="True" OnSelectedIndexChanged="DdlRecDiv_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </div>
                                                <label class="control-label col-md-2" style="width: 120px">Department : <span class="required">*  </span></label>
                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="DdlRecDept" CssClass="form-control" Enabled="True" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-1">
                                                    <label>From Cust No<span class="required"></span></label>
                                                </div>
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="txtCustNo" CssClass="form-control" Placeholder="Cust No" onkeypress="javascript:return isNumber (event)" runat="server" OnTextChanged="txtCustNo_TextChanged" AutoPostBack="true" Style="width: 70px;" TabIndex="2"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtCustName" CssClass="form-control" Placeholder="Customer Name" OnTextChanged="txtCustName_TextChanged" AutoPostBack="true" runat="server" Style="width: 360px;" />
                                                    <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="txtCustName"
                                                        UseContextKey="true"
                                                        CompletionInterval="1"
                                                        CompletionSetCount="20"
                                                        MinimumPrefixLength="1"
                                                        EnableCaching="true"
                                                        ServicePath="~/WebServices/Contact.asmx"
                                                        ServiceMethod="GetCustNames">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                                <div class="col-md-1">
                                                    <asp:Label ID="Lbltocust" runat="server" Text="To Cust No"></asp:Label>
                                                </div>
                                                <div class="col-md-1" runat="server" id="Div_To">
                                                    <asp:TextBox ID="TxtToCustno" CssClass="form-control" Placeholder="To Cust No" onkeypress="javascript:return isNumber (event)" runat="server" OnTextChanged="TxtToCustno_TextChanged" AutoPostBack="true" Style="width: 70px;"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4" runat="server" id="Div_ToName">
                                                    <asp:TextBox ID="TxtToCustName" CssClass="form-control" Placeholder="To Customer Name" OnTextChanged="TxtToCustName_TextChanged" AutoPostBack="true" runat="server" Style="width: 360px;" />
                                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="TxtToCustName"
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
                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-1">
                                                    <label class="control-label">From Date <span class="required"></span></label>
                                                </div>
                                                <div class="col-lg-2">
                                                    <asp:TextBox ID="TxtFDate" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <asp:TextBoxWatermarkExtender ID="TxtDate_WatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtFDate">
                                                    </asp:TextBoxWatermarkExtender>
                                                    <asp:CalendarExtender ID="TxtDate_CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtFDate">
                                                    </asp:CalendarExtender>
                                                </div>
                                                <label class="control-label col-md-2">To Date <span class="required"></span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtTDate" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtTDate">
                                                    </asp:TextBoxWatermarkExtender>
                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtTDate">
                                                    </asp:CalendarExtender>
                                                </div>
                                                <div class="col-md-2">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-lg-12" style="text-align: center">
                                        <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-primary" Text="Print Report" OnClick="btnPrint_Click" OnClientClick="Javascript:return Validate();" />
                                        <asp:Button ID="btnprint1" runat="server" CssClass="btn btn-primary" Text="Print new Report" OnClick="btnprint1_Click" OnClientClick="Javascript:return Validate();" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

