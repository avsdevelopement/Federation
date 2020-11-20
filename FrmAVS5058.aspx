<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAVS5058.aspx.cs" Inherits="FrmAVS5058" %>

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
                        <label id="lblheading" runat="server"></label>
                        <asp:Literal ID="ltrlHeader" runat="server"></asp:Literal>
                    </div>
                </div>
                <div id="Div1" class="portlet-body form" runat="server">
                    <!--<form action="#" class="form-horizontal" id="submit_form" method="POST">   -->
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab__blue">
                                        <div class="row" runat="server" id="div_main">
                                            <div class="col-lg-12">
                                                <div class="col-md-4"></div>
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="ddlMainMenu" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlMainMenu_SelectedIndexChanged">
                                                        <asp:ListItem Value="0" Selected="True">--Select--</asp:ListItem>
                                                        <asp:ListItem Value="1">Customer Open Report</asp:ListItem>
                                                        <asp:ListItem Value="2">A/Cs without MobileNo</asp:ListItem>
                                                        <asp:ListItem Value="3">A/Cs without Loan Limit</asp:ListItem>
                                                        <asp:ListItem Value="4">A/Cs without Deposit Info</asp:ListItem>
                                                        <asp:ListItem Value="5">A/Cs without Customer Master</asp:ListItem>
                                                        <asp:ListItem Value="6">A/Cs without Surity</asp:ListItem>
                                                        <asp:ListItem Value="7">A/Cs with 0 balance</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div id="div_brcd" class="row" runat="server" visible="false">
                                            <div id="div_type" class="row" runat="server" visible="false">
                                                <div class="col-lg-12">
                                                    <div class="col-md-6">
                                                        <asp:RadioButtonList ID="rdbType" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1">Loan Info</asp:ListItem>
                                                            <asp:ListItem Value="2">Deposit Info</asp:ListItem>
                                                            <asp:ListItem Value="3">Shares Info</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="margin: 7px 0 7px 0; margin-top: 20px;">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">From BRCD:<span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtFbrcd" runat="server" placeholder="From Brcd" TabIndex="1" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <label class="control-label col-md-2">To BRCD:<span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxttBrcd" runat="server" placeholder="To Brcd" TabIndex="2" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="margin: 7px 0 7px 0; margin-top: 20px;">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">From Date:<span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtFdate" runat="server" placeholder="dd/mm/yyyy" TabIndex="3" CssClass="form-control" AutoPostBack="true" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                        <asp:CalendarExtender ID="TxtDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtFdate">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                    <label class="control-label col-md-2">To Date:<span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtTdate" runat="server" placeholder="dd/mm/yyyy" TabIndex="4" CssClass="form-control" AutoPostBack="true" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtTdate">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="margin: 7px 0 7px 0; text-align: center">
                                                <div class="col-lg-12">
                                                    <div class="col-md-6">

                                                        <asp:Button ID="Btnreport" runat="server" Text="Report" CssClass="btn btn-success" OnClick="Btnreport_Click" />
                                                        <asp:Button ID="BtnClose" runat="server" Text="Close" CssClass="btn btn-success" OnClick="BtnClose_Click" />
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

</asp:Content>

