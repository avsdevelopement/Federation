<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmITaxReport.aspx.cs" Inherits="FrmITaxReport" %>

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
                        Income Tax Report
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
                                                            <asp:RadioButtonList ID="Rdeatils" OnSelectedIndexChanged="Rdeatils_SelectedIndexChanged" AutoPostBack ="true" RepeatDirection="Horizontal" style="width : 380px; " runat="server">
                                                                <asp:ListItem Text="Details" Value ="1" />
                                                                <asp:ListItem Text="Summary" Value ="2"  />
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </div>
                                                 </div>
                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label">Branch Code</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtBrID" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label">From Date <span class="required"></span></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtFDate" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <asp:CalendarExtender ID="TxtDate_CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtFDate">
                                                    </asp:CalendarExtender>
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label">To Date <span class="required"></span></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtTDate" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtTDate">
                                                    </asp:CalendarExtender>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label">Member Type <span class="required">* </span></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="DdlActivity" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-8"></div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label">Select Type <span class="required">* </span></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="DdlSActivity" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-lg-12" style="text-align: center">
                                        <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-primary" Text="Print Report" OnClick="btnPrint_Click" OnClientClick="Javascript:return Validate();" />
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

