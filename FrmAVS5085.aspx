<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAVS5085.aspx.cs" Inherits="FrmAVS5085" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function Validate() {
            var FBRCD = document.getElementById('<%=TxtFBRCD.ClientID%>').value;
             var TBRCD = document.getElementById('<%=TxtTBrcd.ClientID%>').value;
             var TxtFDT = document.getElementById('<%=TxtFDate.ClientID%>').value;
             var TxtTDt = document.getElementById('<%=TxtTDate.ClientID%>').value;

             if (FBRCD == "") {
                 alert("Please enter Branch Code......!!");
                 return false;
             }

             if (TBRCD == "") {
                 alert("Please enter Branch Code......!!");
                 return false;
             }
             if (TxtFDT == "DD/MM/YYYY") {
                 alert("Please Enter From Date........!!");
                 return false;
             }
             if (TxtTDt == "DD/MM/YYYY") {
                 alert("Please Enter To Date..........!!");
                 return false;
             }
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
            <div class="portlet box blue" id="Div1">
                <div class="portlet-title">
                    <div class="caption">
                        Gold Loan Sanction Report
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab1">
                                        <div>
                                            <div class="row" id="Div_Monthly" runat="server">
                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">
                                                        <label class="control-label col-md-2">From Brcd<span class="required"></span></label>
                                                        <div class="col-md-1">
                                                            <asp:TextBox ID="TxtFBRCD" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber(event)" AutoPostBack="true" OnTextChanged="TxtFBRCD_TextChanged"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="TxtFBrname" CssClass="form-control" runat="server" Style="text-transform: uppercase;" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                                        </div>
                                                        <label class="control-label col-md-2">To Brcd<span class="required"></span></label>
                                                        <div class="col-md-1">
                                                            <asp:TextBox ID="TxtTBrcd" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber(event)" AutoPostBack="true" OnTextChanged="TxtTBrcd_TextChanged"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="TxtTBrcdName" CssClass="form-control" runat="server" Style="text-transform: uppercase;" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin: 7px 0 7px 0" runat="server" visible="false">
                                                    <div class="col-lg-12">
                                                        <label class="control-label col-md-2">ProductCode <span class="required"></span></label>
                                                        <div class="col-md-1">
                                                            <asp:TextBox ID="TxtFprdcode" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="Code" OnTextChanged="TxtFprdcode_TextChanged" TabIndex="4" AutoPostBack="true" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="TxtFprdname" CssClass="form-control" placeholder="Product Name" OnTextChanged="TxtFprdname_TextChanged" AutoPostBack="true" TabIndex="5" runat="server"></asp:TextBox>
                                                            <div id="CustList1" style="height: 200px; overflow-y: scroll;"></div>
                                                            <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="TxtFprdname"
                                                                UseContextKey="true"
                                                                CompletionInterval="1"
                                                                CompletionSetCount="20"
                                                                MinimumPrefixLength="1"
                                                                EnableCaching="true"
                                                                ServicePath="~/WebServices/Contact.asmx"
                                                                ServiceMethod="getglname" CompletionListElementID="CustList1">
                                                            </asp:AutoCompleteExtender>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">
                                                        <label class="control-label col-md-2">From Date <span class="required"></span></label>
                                                        <div class="col-lg-2">
                                                            <asp:TextBox ID="TxtFDate" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                            <asp:TextBoxWatermarkExtender ID="TxtDate_WatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtFDate">
                                                            </asp:TextBoxWatermarkExtender>
                                                            <asp:CalendarExtender ID="TxtDate_CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtFDate">
                                                            </asp:CalendarExtender>
                                                        </div>
                                                        <label class="control-label col-md-1">To Date <span class="required"></span></label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtTDate" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtTDate">
                                                            </asp:TextBoxWatermarkExtender>
                                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtTDate">
                                                            </asp:CalendarExtender>
                                                        </div>
                                                              <label class="control-label col-md-2">Current Rate/gm <span class="required"></span></label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtCurRt" placeholder="Current Rate" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                          
                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-md-offset-3 col-md-9">
                                                        <asp:Button ID="Submit" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="Submit_Click" OnClientClick="Javascript:return Validate();" />
                                                        <asp:Button ID="Btn_Exit" runat="server" CssClass="btn btn-primary" Text="Exit" OnClick="Btn_Exit_Click" />
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

    <div id="alertModal" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <center><h4 class="modal-title" style="color:#ff0000">AVS CORE</h4></center>
                </div>
                <div class="modal-body">
                    <p></p>
                    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                </div>
                <div class="modal-footer">
                    <button class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

