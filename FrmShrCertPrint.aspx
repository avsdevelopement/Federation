﻿<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmShrCertPrint.aspx.cs" Inherits="FrmShrCertPrint" %>

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
            <div class="portlet box green">
                <div class="portlet-title">
                    <div class="caption">
                        Shares Certificate
                                <asp:Literal ID="LtrlHeading" runat="server"></asp:Literal>
                    </div>
                </div>
                <div class="portlet-body">
                    <div id="Depositdiv" runat="server">
                        <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-12">
                                <div class="col-md-8">
                                    <asp:RadioButtonList ID="rbtnRptType" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbtnRptType_SelectedIndexChanged"   AutoPostBack="true">
                                        <asp:ListItem Text="Shares Certificate" Value="1" />
                                        <asp:ListItem Text="Sanchit Certificate" Value="2" />
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-12">
                                <label class="control-label col-md-1">Branch Code<span class="required"></span></label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="Txtfrmbrcd" onkeypress="javascript:return isNumber (event)" OnTextChanged="Txtfrmbrcd_TextChanged" AutoPostBack="true" CssClass="form-control" TabIndex="1" runat="server" />
                                </div>
                            </div>
                        </div>
                        <div class="row" style="margin: 7px 0 7px 0" runat="server" id="Div_DEP">
                            <div class="col-lg-12">
                                <label class="control-label col-md-1">Division : <span class="required">* </span></label>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="DdlRecDiv" CssClass="form-control" runat="server" Enabled="True" OnSelectedIndexChanged="DdlRecDiv_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                                <label class="control-label col-md-2" style="width: 120px">Department : <span class="required">*  </span></label>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="DdlRecDept" CssClass="form-control" Enabled="True" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-12">
                                <label class="control-label col-md-1">From A/C <span class="required">*</span></label>
                                <div class="col-md-1">
                                    <asp:TextBox ID="TxtFAcc" OnTextChanged="TxtFAcc_TextChanged" Placeholder="From A/C No." onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="TxtFAccName" OnTextChanged="TxtFAccName_TextChanged" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <label class="control-label col-md-1">To A/C <span class="required">*</span></label>
                                <div class="col-md-1">
                                    <asp:TextBox ID="TxtTAcc" OnTextChanged="TxtTAcc_TextChanged" Placeholder="To A/C No." onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="TxtTAccName" OnTextChanged="TxtTAccName_TextChanged" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="row" style="margin: 7px 0 7px 0">
                        <div class="col-lg-12">
                            <label class="control-label col-md-1">From Date <span class="required"></span></label>
                            <div class="col-lg-2">
                                <asp:TextBox ID="TxtFDate" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" onchange="checkdate(this)"></asp:TextBox>
                                <asp:TextBoxWatermarkExtender ID="TxtDate_WatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtFDate">
                                </asp:TextBoxWatermarkExtender>
                                <asp:CalendarExtender ID="TxtDate_CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtFDate">
                                </asp:CalendarExtender>
                            </div>
                            <label class="control-label col-md-1">To Date <span class="required"></span></label>
                            <div class="col-md-2">
                                <asp:TextBox ID="TxtTDate" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" onchange="checkdate(this)"></asp:TextBox>
                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtTDate">
                                </asp:TextBoxWatermarkExtender>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtTDate">
                                </asp:CalendarExtender>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="margin: 7px 0 7px 0; padding-top: 10px; text-align: center">
                        <div class="col-lg-12">
                            <div class="col-md-6">
                                <asp:Button ID="BtnPrint" runat="server" Text="Report" CssClass="btn btn-success" OnClick="BtnPrint_Click" OnClientClick="javascript:return validate();" TabIndex="12" />
                                <asp:Button ID="BtnClear" runat="server" Text="Clear" CssClass="btn btn-success" OnClick="BtnClear_Click" TabIndex="13" />
                                <asp:Button ID="BtnExit" runat="server" Text="Exit" CssClass="btn btn-success" OnClick="BtnExit_Click" TabIndex="14" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

