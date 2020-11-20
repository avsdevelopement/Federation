<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmTrialDepositLoansStat.aspx.cs" Inherits="FrmTrialDepositLoansStat" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
    <script>
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;

            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="Div1">
                <div class="portlet-title">
                    <div class="caption">
                        Trail Balance From Date And To Date
                    </div>
                </div>

                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab__blue">
                                    </div>
                                    <div class="col-lg-12">
                                        <div class="col-md-2">
                                            <label class="control-label">Branch Code</label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="TxtBrID" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <div runat="server" id="FDT">
                                                <div class="col-md-2">
                                                    <label class="control-label">From Date</label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="TxtFDate" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <asp:TextBoxWatermarkExtender ID="TxtFDT_WatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtFDate">
                                                    </asp:TextBoxWatermarkExtender>
                                                    <asp:CalendarExtender ID="TxtFDT_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtFDate">
                                                    </asp:CalendarExtender>
                                                </div>
                                            </div>

                                            <div runat="server" id="TDT">
                                                <div class="col-md-2">
                                                    <label class="control-label">To Date</label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="TxtTDate" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtTDate">
                                                    </asp:TextBoxWatermarkExtender>
                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtTDate">
                                                    </asp:CalendarExtender>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-12" style="text-align: left; margin-top: 12px; margin-bottom: 13px; margin-left: 15px;">
                                            <div class="col-lg-6">
                                                <asp:Button ID="ReportV" runat="server" CssClass="btn btn-primary" Text="Deposit & Loans Report" OnClick="ReportV_Click"  />
                                                <br />
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
</asp:Content>

