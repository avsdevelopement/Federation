<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAppliedInterest.aspx.cs" Inherits="FrmAppliedInterest" %>

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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="Btn_TextReport" />
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="portlet box blue" id="form_wizard_1">
                        <div class="portlet-title">
                            <div class="caption">
                                Applied Interest Report
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

                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label ">Branch Code<span class="required">*</span></label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:DropDownList ID="ddlBrName" CssClass="form-control" OnSelectedIndexChanged="ddlBrName_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtBrCode" Enabled="false" CssClass="form-control" runat="server" />
                                                        </div>
                                                        <div class="col-md-2">
                                                            <label class="control-label ">Product Code:<span class="required">*</span></label>
                                                        </div>
                                                        <div class="col-md-1">
                                                            <asp:TextBox ID="txtPrCode" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtPrCode_TextChanged" PlaceHolder="Product Code"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="input-icon">
                                                                <i class="fa fa-search"></i>
                                                                <asp:TextBox ID="txtPrName" CssClass="form-control" PlaceHolder="Search Product Name" OnTextChanged="txtPrName_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                                                <div id="CustList1" style="height: 200px; overflow-y: scroll;"></div>
                                                                <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="txtPrName" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                    MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList1" ServiceMethod="GetGlName">
                                                                </asp:AutoCompleteExtender>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label ">As On Date<span class="required">*</span></label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtAsOnDate" MaxLength="10" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="DD/MM/YYYY" runat="server"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtAsOnDate">
                                                            </asp:CalendarExtender>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <label class="control-label ">Calculation Type<span class="required">*</span></label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:DropDownList ID="ddlCalType" CssClass="form-control" runat="server">
                                                                <asp:ListItem Text="--Select--" Value="0" />
                                                                <asp:ListItem Text="Receivable" Value="1" />
                                                                <asp:ListItem Text="Capitalise" Value="2" />
                                                            </asp:DropDownList>
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
                                            <asp:Button ID="btnSubmit" runat="server" CssClass="btn blue" Text="SUBMIT" OnClick="btnSubmit_Click" OnClientClick="Javascript:return IsValidate();" />
                                        <asp:Button ID="Btn_TextReport" runat="server" CssClass="btn blue" Text="Text Report" OnClick="Btn_TextReport_Click" OnClientClick="Javascript:return IsValidate();" />

                                        </div>
                                         <div class="col-md-offset-3 col-md-9">
                                            
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
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

