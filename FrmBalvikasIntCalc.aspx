<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmBalvikasIntCalc.aspx.cs" Inherits="FrmBalvikasIntCalc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
            var FDate = document.getElementById('<%=TxtFromDate.ClientID%>').value;
            var TDate = document.getElementById('<%=TxtToDate.ClientID%>').value;
            var FBr = document.getElementById('<%=TxtBRCD.ClientID%>').value;
            var Fprd = document.getElementById('<%=TxtFPRD.ClientID%>').value;
            var Tprd = document.getElementById('<%=TxtTPRD.ClientID%>').value;
            var Faccno = document.getElementById('<%=TxtFAcc.ClientID%>').value;
            var Taccno = document.getElementById('<%=TxtTAcc.ClientID%>').value;


            if (FBr == "") {
                alert("Please enter from Branch Code......!!");
                return false;
            }

            if (Fprd == "") {
                alert("Please Product Type......!!");
                return false;
            }
            if (TBr == "") {
                alert("Please Enter to Branch Code........!!");
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="Div1">
                <div class="portlet-title">
                    <div class="caption">
                       Balvikas Interest Application
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab1">
                                        <div class="row" style="margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-1">From Date<span class="required">* </span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtFromDate" OnTextChanged="TxtFromDate_TextChanged" AutoPostBack="true" CssClass="form-control" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this)" runat="server"></asp:TextBox>
                                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtFromDate">
                                                    </asp:TextBoxWatermarkExtender>
                                                    <asp:CalendarExtender ID="TxtFromDate_CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtFromDate">
                                                    </asp:CalendarExtender>
                                                </div>
                                                <label class="control-label col-md-1">To Date<span class="required">* </span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtToDate" OnTextChanged="TxtToDate_TextChanged" AutoPostBack="true" CssClass="form-control" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this)" runat="server"></asp:TextBox>
                                                    <asp:TextBoxWatermarkExtender ID="TxtAsOnDate_Extender2" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtToDate">
                                                    </asp:TextBoxWatermarkExtender>
                                                    <asp:CalendarExtender ID="TxtToDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtToDate">
                                                    </asp:CalendarExtender>
                                                </div>
                                            </div>
                                            <br />
                                            <br />
                                            <div class="row" style="margin: 20px; border-bottom: 1px solid rgba(53, 152, 220, 0.55);"><strong></strong></div>
                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-1">BRCD <span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtBRCD" Placeholder="From BRCD" OnTextChanged="TxtBRCD_TextChanged" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TxtBRCDName" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                             <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-1">PRD Cd.<span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtFPRD" Placeholder="From Product Code" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" OnTextChanged="TxtFPRD_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TxtFPRDName" CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                                                        <div id="CustList" style="height: 200px; overflow-y: scroll;"></div>
                                                        <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="TxtFPRDName"
                                                            UseContextKey="true"
                                                            CompletionInterval="1"
                                                            CompletionSetCount="20"
                                                            MinimumPrefixLength="1"
                                                            EnableCaching="true"
                                                            ServicePath="~/WebServices/Contact.asmx"
                                                            ServiceMethod="getglname" CompletionListElementID="CustList">
                                                        </asp:AutoCompleteExtender>
                                                    </div>

                                                    <label class="control-label col-md-1">PRD Cd.<span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtTPRD" Placeholder="To Product Code" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" OnTextChanged="TxtTPRD_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TXtTPRDName" CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                                                        <div id="Div2" style="height: 200px; overflow-y: scroll;"></div>
                                                        <asp:AutoCompleteExtender ID="autoglname1" runat="server" TargetControlID="TXtTPRDName"
                                                            UseContextKey="true"
                                                            CompletionInterval="1"
                                                            CompletionSetCount="20"
                                                            MinimumPrefixLength="1"
                                                            EnableCaching="true"
                                                            ServicePath="~/WebServices/Contact.asmx"
                                                            ServiceMethod="getglname" CompletionListElementID="Div2">
                                                        </asp:AutoCompleteExtender>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-1">From A/C <span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtFAcc" OnTextChanged="TxtFAcc_TextChanged" Placeholder="From A/C No." onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TxtFAccName" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                    <label class="control-label col-md-1">To A/C <span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtTAcc" OnTextChanged="TxtTAcc_TextChanged" Placeholder="To A/C No." onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TxtTAccName" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>

                                                </div>
                                            </div>
                                            
                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <div class="col-lg-12">

                                                        <div class="col-md-2">
                                                            <asp:CheckBox ID="Chk_Skip" Text="Skip Digits" runat="server" />
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="margin: 20px; border-bottom: 1px solid rgba(53, 152, 220, 0.55);"><strong></strong></div>
                                            <div class="row" style="margin: 7px 0 7px 0; text-align: center">
                                                <div class="col-lg-12">
                                                    <div class="col-lg-8">
                                                        <asp:Button ID="BtnCalculate" OnClientClick="Javascript:return isvalidate();" runat="server" Text="Calculate" CssClass="btn btn-primary" OnClick="BtnCalculate_Click"/>
                                                        <asp:Button ID="Btn_Recalculate" runat="server" Text="Recalculate" CssClass="btn btn-primary" OnClick="Btn_Recalculate_Click" />
                                                        <asp:Button ID="Btn_ClearAll" runat="server" Text="Clear All" CssClass="btn btn-success" OnClick="Btn_ClearAll_Click" />

                                                        <asp:Button ID="BtnReportCalc" OnClientClick="Javascript:return isvalidate();" runat="server" Text="Calc Report" CssClass="btn btn-success" OnClick="BtnReportCalc_Click" />
                                                        <asp:Button ID="BtnReportTally" OnClientClick="Javascript:return isvalidate();" runat="server" Text="Tally Check Report" CssClass="btn btn-success" OnClick="BtnReportTally_Click" />
                                                        
                                                    </div>
                                                    <div class="col-lg-2">
                                                        <asp:Button ID="ApplyEntry" OnClientClick="Javascript:return isvalidate();" runat="server" Text="Apply Entry" CssClass="btn btn-success" OnClick="ApplyEntry_Click"/>
                                                        <asp:Button ID="Btn_Exit" runat="server" Text="Exit" CssClass="btn btn-success" />
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

