<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="AVS5050.aspx.cs" Inherits="AVS5050" %>

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
    <script type="text/javascript">
        function isvalidate() {
            var FBr = document.getElementById('<%=txtBrCode.ClientID%>').value;
            var Fprd = document.getElementById('<%=TxtFPRD.ClientID%>').value;
            var Tprd = document.getElementById('<%=TxtTPRD.ClientID%>').value;
            var Faccno = document.getElementById('<%=TxtFAcc.ClientID%>').value;
            var Taccno = document.getElementById('<%=TxtTAcc.ClientID%>').value;
            var FDate = document.getElementById('<%=txtFDate.ClientID%>').value;
            var TDate = document.getElementById('<%=txtTDate.ClientID%>').value;

            if (FBr == "") {
                alert("Please enter from Branch Code......!!");
                return false;
            }
            if (Fprd == "") {
                alert("Please from Product Type......!!");
                return false;
            }
            if (Tprd == "") {
                alert("Please to Product Type.......!!");
                return false;
            }
            if (Faccno == "") {
                alert("Please from A/C number........!!");
                return false;
            }
            if (Taccno == "") {
                alert("Please to A/C number........!!");
                return false;
            }
            if (FDate == "") {
                alert("Please Enter From Date..........!!");
                return false;
            }
            if (TDate == "") {
                alert("Please Enter To Date..........!!");
                return false;
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="Div1">
                <div class="portlet-title">
                    <div class="caption">
                        DDS Interest Calculation
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab1">

                                        <div class="row" style="margin-bottom: 10px;">
                                            <div class="row" style="margin: 20px; border-bottom: 1px solid rgba(53, 152, 220, 0.55);"><strong></strong></div>
                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Branch Code <span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtBrCode" Placeholder="From BRCD" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" OnTextChanged="TxtFBRCD_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtbrName" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">From Pr Code<span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtFPRD" Placeholder="From Product Code" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" OnTextChanged="TxtFPRD_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                    <label class="control-label col-md-2">To Pr Code<span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtTPRD" Placeholder="To Product Code" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" OnTextChanged="TxtTPRD_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">From A/C <span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtFAcc" Placeholder="From A/C No." onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" OnTextChanged="TxtFAcc_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                    <label class="control-label col-md-2">To A/C <span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtTAcc" Placeholder="To A/C No." onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" OnTextChanged="TxtTAcc_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">From Date : <span class="required">* </span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtFDate" CssClass="form-control" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this)" runat="server"></asp:TextBox>
                                                    </div>
                                                    <label class="control-label col-md-2">To Date : <span class="required">* </span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtTDate" CssClass="form-control" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this)" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-2 col-md-10">
                                        <asp:Button ID="btnCalculate" runat="server" CssClass="btn blue" Text="Calculate" OnClick="btnCalculate_Click" OnClientClick="Javascript:return IsValidate();" />
                                        <asp:Button ID="btnReport" runat="server" CssClass="btn blue" Text="Report" OnClick="btnReport_Click" OnClientClick="Javascript:return IsValidate();" />
                                        <asp:Button ID="btnApplyInt" runat="server" CssClass="btn btn-success" Text="Apply" OnClick="btnApplyInt_Click" OnClientClick="Javascript:return IsValidate();" />
                                        <asp:Button ID="btnClearAll" runat="server" Text="Clear All" CssClass="btn blue" OnClick="btnClearAll_Click" />
                                        <asp:Button ID="btnExit" runat="server" Text="Exit" CssClass="btn blue" OnClick="btnExit_Click" />
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

