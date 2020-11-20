<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmPL.aspx.cs" Inherits="FrmPL" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">
    function FormatIt(obj) {
        if (obj.value.length == 2) // Day
            obj.value = obj.value + "/";
        if (obj.value.length == 5) // month 
            obj.value = obj.value + "/";
        if (obj.value.length == 11) // year 
            alert(" Enter valid Date");
        if (obj.value.length == 6) {
            var EnteredDate = obj.value;
            var date = EnteredDate.substring(0, 2);
            var month = EnteredDate.substring(3, 5);
            if (month == "01" || month == "03" || month == "05" || month == "07" || month == "08" || month == "10" || month == "12") {
                if (date < "01" || date > "31") {
                    alert("Enter valid Date");
                    obj.value = "";
                }
            }
            else if (month == "04" || month == "06" || month == "09" || month == "11") {
                if (date < "01" || date > "30") {
                    alert("Enter valid Date");
                    obj.value = "";
                }
            }
            else if (month == "02") {
                if (date < "01" || date > "29") {
                    alert("Enter valid Date");
                    obj.value = "";
                }
            }
            if (month < "01" || month > "12") {
                alert("Enter valid Date");
                obj.value = "";
            }
        }
    }

    function isNumber(evt) {
        //  alert("HELLLO");
        var iKeyCode = (evt.which) ? evt.which : evt.keyCode;
        //  alert(iKeyCode);
        if (iKeyCode == 46)
            return false;
        else if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
            return false;
        return true;
    }

    function CheckFirstChar(key, txt) {
        if (key == 32 && txt.value.length <= 0) {
            return false;
        }
        else if (txt.value.length > 0) {
            if (txt.value.charCodeAt(0) == 32) {
                txt.value = txt.value.substring(1, txt.value.length);
                return true;
            }
        }
        return true;
    }
        </script>




</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  
        <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                      Profit Loss Initialization
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="col-md-12" align="center">
                                            <asp:RadioButtonList ID="rbOption" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbOption_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="1" style="padding:5px">PL Transfer</asp:ListItem>
                                                <asp:ListItem Value="2" style="padding:5px">Dividend Transfer</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" id="Div_SPECIFIC" runat="server" visible="false">
                                    <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Date:<span class="required"></span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtDate"  onkeyup="FormatIt(this);" onkeypress="javascript:return isNumber (event)"    onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" placeholder="dd/MM/yyyy" runat="server"></asp:TextBox>
                                                <asp:TextBoxWatermarkExtender ID="TxtDate_WatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtDate">
                                                </asp:TextBoxWatermarkExtender>
                                                <asp:CalendarExtender ID="TxtDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtDate">
                                                </asp:CalendarExtender>

                                            </div>

                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">PL Transfer A/C:<span class="required"></span></label>

                                            <div class="col-md-1">
                                                <asp:TextBox ID="txtPL" CssClass="form-control" runat="server" OnTextChanged="txtPL_TextChanged" AutoPostBack="true" Width="100px"></asp:TextBox>

                                            </div>
                                            <div class="col-md-6" style="margin-left: 20px">
                                                <asp:TextBox ID="txtName" CssClass="form-control" runat="server" Width="200px"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                        <div class="col-lg-12">
                                            <div class="col-md-6" align="center">
                                                <asp:Button ID="txtReport" CssClass="btn btn-success" runat="server" Text="Report" OnClick="txtReport_Click" />

                                                <asp:Button ID="btnExit" runat="server" CssClass="btn btn-success" Text="Exit" OnClick="btnExit_Click" />

                                                <asp:Button ID="btnPost" runat="server" Text="Transfer" CssClass="btn btn-success" OnClick="btnPost_Click" />
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <div class="row" id="DIV_Div" runat="server" visible="false">
                                    <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Date:<span class="required"></span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtDT" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="dd/MM/yyyy" runat="server"></asp:TextBox>
                                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtDate">
                                                </asp:TextBoxWatermarkExtender>
                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtDT">
                                                </asp:CalendarExtender>

                                            </div>

                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Dividend Product Code:<span class="required"></span></label>

                                            <div class="col-md-1">
                                                <asp:TextBox ID="txtPrdFrm" CssClass="form-control" runat="server" OnTextChanged="txtPrdFrm_TextChanged" AutoPostBack="true" Width="100px"></asp:TextBox>

                                            </div>
                                            <div class="col-md-6" style="margin-left: 20px">
                                                <asp:TextBox ID="txtPrdNameFrm" CssClass="form-control" runat="server" Width="200px"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Dividend Transfer Product Code:<span class="required"></span></label>
                                             <div class="col-md-1">
                                                <asp:TextBox ID="txtBRCD" CssClass="form-control" runat="server" placeholder="BRCD" Width="100px"></asp:TextBox>

                                            </div>
                                            <div class="col-md-1" style="margin-left: 20px">
                                                <asp:TextBox ID="txtDivTo" CssClass="form-control" runat="server" OnTextChanged="txtDivTo_TextChanged" AutoPostBack="true" Width="100px"></asp:TextBox>

                                            </div>
                                            <div class="col-md-6" style="margin-left: 20px">
                                                <asp:TextBox ID="txtDivFrom" CssClass="form-control" runat="server" Width="200px"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                        <div class="col-lg-12">
                                            <div class="col-md-6" align="center">
                                                <asp:Button ID="btnDivReport" CssClass="btn btn-success" runat="server" Text="Report" OnClick="btnDivReport_Click" />

                                                <asp:Button ID="btnDivExit" runat="server" CssClass="btn btn-success" Text="Exit" OnClick="btnExit_Click" />

                                                <asp:Button ID="BtnDivPost" runat="server" Text="Transfer" CssClass="btn btn-success" OnClick="BtnDivPost_Click" />
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                </div>
                                </div>
                            </div>
                        </div>
                    </div>
<%--
    --%>
            </div>
            </div>
</asp:Content>

