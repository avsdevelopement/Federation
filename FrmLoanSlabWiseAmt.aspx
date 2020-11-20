<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmLoanSlabWiseAmt.aspx.cs" Inherits="FrmLoanSlabWiseAmt" %>

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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box green">
                <div class="portlet-title">
                    <div class="caption">
                        Classification Of Deposit
                                <asp:Literal ID="LtrlHeading" runat="server"></asp:Literal>
                    </div>
                </div>
                <div class="portlet-body">
                    <div id="Depositdiv" runat="server">

                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                            <div class="col-lg-12">
                                <div class="col-md-2">
                                    <label class="control-label col-md-2 ">Type</label>
                                </div>
                                <div class="col-md-2">
                                    <asp:RadioButtonList ID="rbtnRptType" runat="server" RepeatDirection="Horizontal" Style="width: 250px;">
                                        <asp:ListItem Text="Details" Value="D" />
                                        <asp:ListItem Text="Summary" Value="S" Selected="true" />
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                        </div>

                        <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-12">
                                <label class="control-label col-md-2">Branch<span class="required"></span></label>
                                <div class="col-md-1">
                                    <asp:TextBox ID="Txtfrmbrcd" onkeypress="javascript:return isNumber (event)" OnTextChanged="Txtfrmbrcd_TextChanged"  AutoPostBack="true" CssClass="form-control" TabIndex="1" runat="server" />
                                </div>
                            </div>
                        </div>

                        <%--  <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-12">
                               <%-- <label class="control-label col-md-2">All/Selected <span class="required"></span></label>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="DdlAlS" TabIndex="3" runat="server" CssClass="form-control" OnSelectedIndexChanged="DdlAlS_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                        <asp:ListItem Value="A">All</asp:ListItem>
                                        <asp:ListItem Value="S">Selected</asp:ListItem>
                                    </asp:DropDownList>
                                </div>--%>
                        <%--   <div class="col-md-2">
                                    </div>
                           
                            </div>
                        </div>--%>

                        <%--<div id="div_prd" runat="server" visible="false" class="row" style="margin: 7px 0 7px 0">--%>

                        <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-12">
                                <div class="col-md-2">
                                    <label class="control-label ">Date</label>
                                </div>
                                <div class="col-md-2">
                                    <asp:TextBox ID="TxtAsonDate" onkeyup="FormatIt(this)" MaxLength="10" onkeypress="javascript:return isNumber(event)" CssClass="form-control" placeholder="dd/MM/yyyy" runat="server"></asp:TextBox>
                                    <asp:TextBoxWatermarkExtender ID="TxtFDT_WatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtAsonDate">
                                    </asp:TextBoxWatermarkExtender>
                                    <asp:CalendarExtender ID="TxtFDT_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtAsonDate">
                                    </asp:CalendarExtender>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="margin: 7px 0 7px 0; text-align: center">
                            <div class="col-lg-12">
                                <div class="col-md-6">
                                    <asp:Button ID="BtnPrint" runat="server" Text="Report" CssClass="btn btn-success" OnClick="BtnPrint_Click"  OnClientClick="javascript:return validate();" TabIndex="12" />
                                    <asp:Button ID="BtnExit" runat="server" Text="Exit" CssClass="btn btn-success" OnClick="BtnExit_Click"  TabIndex="14" />
                                </div>
                            </div>
                        </div>
                    </div>
            </div>
        </div>
    </div>
    <%-- </div>--%>
</asp:Content>

