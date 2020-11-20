<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmSharesAppliaction.aspx.cs" Inherits="FrmSharesAppliaction" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>

        function FormatIt(obj) {
            if (obj.value.length == 2) //DAY
                obj.value = obj.value + "/";
            if (obj.value.length == 5) //MONTH
                obj.value = obj.value + "/";
            if (obj.value.length == 11) //YEAR
                alert("Enter Valid Date!....");
        }
    </script>
    <script>

        function dateLen(dt) {
            var dt1 = dt + '';
            if (dt1.length == 1)
                dt1 = '0' + dt;

            return dt1;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        Shares Application List
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

                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label ">From Branch<span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="ddlFromBrName" CssClass="form-control" OnSelectedIndexChanged="ddlFromBrName_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtFrBranch" Enabled="false" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">To Branch<span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="ddlToBrName" onblur="ToBrCode()" CssClass="form-control" OnSelectedIndexChanged="ddlToBrName_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtToBranch" Enabled="false" CssClass="form-control" runat="server" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Share Suspence Gl </label>
                                                    </div>
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="txtProdType" onkeypress="javascript:return isNumber(event)" OnTextChanged="txtProdType_TextChanged" AutoPostBack="true" Placeholder="Product Type" runat="server" CssClass="form-control" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="input-icon">
                                                            <i class="fa fa-search"></i>
                                                            <asp:TextBox ID="txtProdName" Placeholder="Search Product Name" OnTextChanged="txtProdName_TextChanged" AutoPostBack="true" runat="server" CssClass="form-control" />
                                                            <div id="CustList1" style="height: 200px; overflow-y: scroll;"></div>
                                                            <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="txtProdName" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList1" ServiceMethod="GetGlName">
                                                            </asp:AutoCompleteExtender>
                                                        </div>
                                                    </div>
                                                </div>
                                        </div>
                                        
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
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

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-11">
                                                <label class="control-label col-md-3">Application Type <span class="required">* </span></label>
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="DdlActivityType" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-11">
                                                <label class="control-label col-md-3">Application Status<span class="required">* </span></label>
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="DdlActivity" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
    </div>
    </div>
    <br />
                                <div class="row">
                                    <div class="col-lg-12" style="text-align: center">
                                        <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-primary" Text="Report Print" OnClick="btnPrint_Click" OnClientClick="Javascript:return Validate();" />
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

