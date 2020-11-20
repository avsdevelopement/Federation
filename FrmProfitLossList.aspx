<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmProfitLossList.aspx.cs" Inherits="FrmProfitLossList" %>

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
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        Profit & Loss Report
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
                                                    <label class="control-label ">From BRCD</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtFBrID" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label">TO BRCD</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtTBrID" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0" align="center">
                                            <div class="col-lg-12">
                                                <div class="col-md-4">
                                                    <asp:RadioButtonList ID="RBSel" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="RBSel_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Value="1">&nbsp;As On Date</asp:ListItem>
                                                        <asp:ListItem Value="2" style="padding-left: 10px">&nbsp;From & To Date</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>
                                        <div runat="server" id="divOnDate" visible="false" class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <label class="control-label">As On Date</label>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TxtFDT" Width="75%" CssClass="form-control" onkeyup="FormatIt(this)" PlaceHolder="dd/mm/yyyy" runat="server"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtFDT">
                                                        </asp:CalendarExtender>
                                                    </div>

                                                    <div class="col-lg-7">
                                                    </div>
                                                </div>
                                        </div>
                                        <div runat="server" id="div1" visible="false" class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <label class="control-label">From Date</label>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TxtFdate" Width="75%" CssClass="form-control" onkeyup="FormatIt(this)" PlaceHolder="dd/mm/yyyy" runat="server"></asp:TextBox>
                                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtFromDate">
                                                        </asp:TextBoxWatermarkExtender>
                                                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtFromDate">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label class="control-label">To Date</label>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TxtTdate" Width="75%" CssClass="form-control" onkeyup="FormatIt(this)" PlaceHolder="dd/mm/yyyy" runat="server"></asp:TextBox>
                                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtToDate">
                                                        </asp:TextBoxWatermarkExtender>
                                                        <asp:CalendarExtender ID="CalendarExtender5" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtToDate">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                </div>
                                        </div>
                                    
                                    <div runat="server" id="divDate" visible="false" class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label">From Date</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtFromDate" Width="75%" CssClass="form-control" onkeyup="FormatIt(this)" PlaceHolder="dd/mm/yyyy" runat="server"></asp:TextBox>
                                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtFromDate">
                                                    </asp:TextBoxWatermarkExtender>
                                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtFromDate">
                                                    </asp:CalendarExtender>
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label">To Date</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtToDate" Width="75%" CssClass="form-control" onkeyup="FormatIt(this)" PlaceHolder="dd/mm/yyyy" runat="server"></asp:TextBox>
                                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtToDate">
                                                    </asp:TextBoxWatermarkExtender>
                                                    <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtToDate">
                                                    </asp:CalendarExtender>
                                                </div>
                                                <div class="col-lg-7">
                                                </div>
                                            </div>

                                        
                                    </div>
                                        <div class="row">
                                            <div class="col-lg-12" style="text-align: left; margin-top: 12px; margin-bottom: 13px; margin-left: 15px;">
                                                <div class="col-lg-6">
                                                    <asp:Button ID="btnShow" runat="server" CssClass="btn btn-primary" Text="Report Print" OnClick="btnPrint_Click" OnClientClick="Javascript:return Validate();" />

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

    <asp:HiddenField ID="hdnFlag" runat="server" />
</asp:Content>

