<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmSBIntCalcReport.aspx.cs" Inherits="FrmSBIntCalcReport" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        Saving Interest Calculation Report
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <div class="col-md-2">
                                                <asp:RadioButtonList ID="Rdeatils" RepeatDirection="Horizontal" Style="width: 380px;" runat="server">
                                                    <asp:ListItem Text="Details" Value="1" />
                                                    <asp:ListItem Text="Summary" Value="2" />
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Branch Code : <span class="required">*</span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtBrCode" Enabled="false" Placeholder="Branch Code" CssClass="form-control" runat="server" OnTextChanged="txtBrCode_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtBrName" Enabled="false" Placeholder="Branch Name" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Product Code : <span class="required">* </span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtProdType" runat="server" onkeypress="javascript:return isNumber(event)" OnTextChanged="txtProdType_TextChanged" AutoPostBack="true" Placeholder="Product Code" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="input-icon">
                                                    <i class="fa fa-search"></i>
                                                    <asp:TextBox ID="txtProdName" runat="server" OnTextChanged="txtProdName_TextChanged" AutoPostBack="true" Placeholder="Search Product Name" CssClass="form-control"></asp:TextBox>
                                                    <div id="CustList1" style="height: 200px; overflow-y: scroll;"></div>
                                                    <asp:AutoCompleteExtender ID="AutoGlName" runat="server" TargetControlID="txtProdName" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                        MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" ServiceMethod="GetGlWiseName" CompletionListElementID="CustList1">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">From Date <span class="required"></span></label>
                                            <div class="col-lg-2">
                                                <asp:TextBox ID="TxtFDate" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" onchange="checkdate(this)"></asp:TextBox>
                                                <asp:TextBoxWatermarkExtender ID="TxtDate_WatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtFDate">
                                                </asp:TextBoxWatermarkExtender>
                                                <asp:CalendarExtender ID="TxtDate_CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtFDate">
                                                </asp:CalendarExtender>
                                            </div>
                                            <label class="control-label col-md-2">To Date <span class="required"></span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="TxtTDate" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" onchange="checkdate(this)"></asp:TextBox>
                                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtTDate">
                                                </asp:TextBoxWatermarkExtender>
                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtTDate">
                                                </asp:CalendarExtender>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9">
                                        <asp:Button ID="btnPrint" runat="server" CssClass="btn blue" Text="Report" OnClick="btnPrint_Click" />
                                        <asp:Button ID="btnClear" runat="server" Text="Clear All" CssClass="btn blue" OnClick="btnClear_Click" />
                                        <asp:Button ID="btnExit" runat="server" Text="Exit" CssClass="btn blue" OnClick="btnExit_Click" />
                                        <asp:Button ID="BtnParameter" runat="server" Text="Paramter" CssClass="btn blue" OnClick="BtnParameter_Click" />
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

