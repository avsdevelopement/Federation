<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmOIRegister.aspx.cs" Inherits="FrmOIRegister" %>

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
                                Report
                                <asp:Label ID="LblReport" runat="server" Text="Label"></asp:Label>
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
                                                <div class="row" style="margin: 7px 0 7px 0; padding-top: 15px;">
                                                    <div class="col-md-6">
                                                        <div class="col-md-6">
                                                            <asp:RadioButton ID="RdbChkDate" GroupName="AS" Text="Date Only" AutoPostBack="true" runat="server" OnCheckedChanged="RdbChkDate_CheckedChanged" />
                                                            <asp:RadioButton ID="RdbChkBank" GroupName="AS" Text="Bank Code Only" AutoPostBack="true" runat="server" OnCheckedChanged="RdbChkBank_CheckedChanged" />
                                                            <asp:RadioButton ID="RdbChkBoth" GroupName="AS" Text="Both" AutoPostBack="true" runat="server" OnCheckedChanged="RdbChkBoth_CheckedChanged" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div id="FTDT" class="row" style="margin: 7px 0 7px 0; padding-top: 15px;" runat="server">
                                                    <div class="col-lg-6">

                                                        <div class="col-md-3">
                                                            <label id="LblFDT" class="control-label">From Date</label>
                                                        </div>
                                                        <div class="col-md-5">
                                                            <asp:TextBox ID="TxtFDate" CssClass="form-control" PlaceHolder="dd/mm/yyyy" runat="server" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                            <asp:TextBoxWatermarkExtender ID="TxtFDate_WatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtFDate">
                                                            </asp:TextBoxWatermarkExtender>
                                                            <asp:CalendarExtender ID="TxtFDate_CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtFDate">
                                                            </asp:CalendarExtender>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6">

                                                        <div class="col-md-3">
                                                            <label id="LblTDT" class="control-label">To Date</label>
                                                        </div>
                                                        <div class="col-md-5">
                                                            <asp:TextBox ID="TxtTDate" CssClass="form-control" PlaceHolder="dd/mm/yyyy" runat="server" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                            <asp:TextBoxWatermarkExtender ID="TxtTDate_WatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtTDate">
                                                            </asp:TextBoxWatermarkExtender>
                                                            <asp:CalendarExtender ID="TxtTDate_CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtTDate">
                                                            </asp:CalendarExtender>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div id="FTBANK" class="row" style="margin: 7px 0 7px 0" runat="server">
                                                    <div class="col-lg-9">

                                                        <div class="col-md-2">
                                                            <label id="LblFB" class="control-label">From Bank</label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtFBankcode" runat="server" Placeholder=" From Bank code" CssClass="form-control" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="TxtFBankname" runat="server" Placeholder="Bank Name" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-9">
                                                        <div class="col-md-2">
                                                            <label id="LblTB" class="control-label">To Bank</label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtTBankcode" runat="server" Placeholder=" To Bank code" CssClass="form-control" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="TxtTBankname" runat="server" Placeholder="Bank Name" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-lg-12" style="text-align: left; margin-top: 12px; margin-bottom: 13px; margin-left: 15px;">
                                                        <div class="col-md-2">
                                                            <asp:Button ID="BtnReport" runat="server" Text="Outward Report" CssClass="btn btn-success" OnClick="BtnReport_Click" />
                                                            <br />
                                                        </div>
                                                        <div class="col-lg-6">
                                                            <asp:Button ID="BtnTextReport" runat="server" CssClass="btn btn-primary" Text="Download Text Report" OnClick="BtnTextReport_Click" />
                                                            <asp:Button ID="Exit" runat="server" Text="Exit" CssClass="btn btn-primary" OnClick="Exit_Click" />
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

