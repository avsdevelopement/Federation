<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmExcessCasRep.aspx.cs" Inherits="FrmExcessCasRep" %>

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
    <script>
        function isValidate() {

            var fdate = document.getElementById('<%=TxtFDate.ClientID%>').value;
            var tdate = document.getElementById('<%=TxtTDate.ClientID%>').value;
            var tbrcd = document.getElementById('<%=TxtTBankcode.ClientID%>').value;
            var fbrcd = document.getElementById('<%=TxtFBankcode.ClientID%>').value;
            var message = "";

            if (fdate == "") {
                message = 'Please Enter From Date....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=TxtFDate.ClientID %>').focus();
                return false;
            }
            if (tdate == "") {
                message = 'Please Enter To Date....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=TxtTDate.ClientID %>').focus();
                return false;
            }
            if (tbrcd == "") {
                message = 'Please Enter From Branch code....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=TxtTBankcode.ClientID %>').focus();
                return false;
            }
            if (fbrcd == "") {
                message = 'Please Enter To Branch code....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=TxtFBankcode.ClientID %>').focus();
                return false;
            }

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
                                Excess Cash Hold Report
                                
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
                                                            <asp:Button ID="BtnReport" runat="server" Text="Report" CssClass="btn btn-success" OnClick="BtnReport_Click" OnClientClick="Javascript:eturn isValidate();" />
                                                            <asp:Button ID="Exit" runat="server" Text="Exit" CssClass="btn btn-primary" OnClick="Exit_Click" />
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

