<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmDayBook_TZMP.aspx.cs" Inherits="FrmDayBook_TZMP" %>

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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="portlet box blue" id="form_wizard_1">
                        <div class="portlet-title">
                            <div class="caption">
                                Day Book
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
                                                            <label class="control-label">Branch Code</label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtBrID" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">
                                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
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
                                                        </div>
                                                        <div class="col-lg-12">
                                                            <div class="col-md-9">
                                                                <asp:CheckBox ID="CHK_SKIP_INT" runat="server" Text="SKIP_INT AC" Style="width: 100px;" />
                                                                <asp:CheckBox ID="CHK_SKIP_DAILY" runat="server" Text="SKIP_DAILY AC" />
                                                            </div>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <label class="control-label"></label>
                                                        </div>
                                                        <div class="col-lg-12">
                                                            <div class="col-md-offset-3 col-md-9">
                                                                    <asp:Button ID="DayBook" runat="server" Text="Day Book Report" CssClass="btn btn-primary" OnClick="DayBook_Click" />
                                                                    <asp:Button ID="DepositDayBook" runat="server" Text="Deposit Renewal" CssClass="btn btn-primary" OnClick="DepositDayBook_Click" />
                                                                    <asp:Button ID="DPDayBookOP" runat="server" Text="Deposit Opening" CssClass="btn btn-primary" OnClick="DPDayBookOP_Click" />
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
                </div>
            </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

