<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAVS51182.aspx.cs" Inherits="FrmAVS51182" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function isvalidate() {
            var Sec, FBrcd, TBrcd, FSubgl, TSubgl, FAcc, TAcc;
            FBrcd = document.getElementById('<%=TxtFBrID.ClientID%>').value;
            FSubgl = document.getElementById('<%=txtFPrCode.ClientID%>').value;
            FAcc = document.getElementById('<%=TxtFACID.ClientID%>').value;
            TAcc = document.getElementById('<%=TxtTACID.ClientID%>').value;
            var message = '';

            if (FBrcd == "") {
                message = 'Enter From Branch....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtFBrID.ClientID%>').focus();
                return false;
            }
            if (FSubgl == "") {
                message = 'Enter From Product Code....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtFPrCode.ClientID%>').focus();
                return false;
            }
            if (FAcc == "") {
                message = 'Enter From AccNo...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtFACID.ClientID%>').focus();
                return false;
            }
            if (TAcc == "") {
                message = 'Enter To AccNo...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtTACID.ClientID%>').focus();
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
                        Active / Non Active Member List
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
                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">A/C Type</label>
                                            <div class="col-md-9">
                                                <asp:RadioButtonList ID="rbtnType" runat="server" RepeatDirection="Horizontal" CssClass="radio-list">
                                                    <asp:ListItem Text="Active Member" Value="1" style="margin: 15px;" Selected="True" />
                                                    <asp:ListItem Text="Non Active Member" Value="2" style="margin: 25px;" />
                                                    <asp:ListItem Text="All Member" Value="3" style="margin: 25px;" />
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                    <div style="border: 1px solid #3598dc">
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <div class="col-md-1">
                                                    <label class="control-label ">Branch Code</label>
                                                </div>
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="TxtFBrID" Placeholder="Branch" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" OnTextChanged="TxtFBrID_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtFBrName" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <div class="col-md-1">
                                                    <label class="control-label ">Product Code</label>
                                                </div>
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="txtFPrCode" Placeholder="Product Code" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" OnTextChanged="txtFPrCode_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtFPrName" Placeholder="Product Name"  CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                                                    <div id="CustList1" style="height: 200px; overflow-y: scroll;"></div>
                                                    <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="txtFPrName"
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
                                                <div class="col-md-1">
                                                    <label class="control-label ">From Member</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtFACID" Placeholder="From Account" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-md-1">
                                                    <label class="control-label ">To Member</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtTACID" Placeholder="To Account" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <div class="col-md-1">
                                                    <label class="control-label">Shares Amount</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtShrAmt" Placeholder="Shares Amount" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <div class="col-md-1">
                                                    <label class="control-label ">Deposit Amount</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtDepAmt" Placeholder="Deposit Amount" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-md-1">
                                                    <label class="control-label ">Deposit Period</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtDPPeriod"  Placeholder="Deposit Period" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <div class="col-md-1">
                                                    <label class="control-label ">Loan Amount</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtLoanAmt" Placeholder="Loan Amount"  onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                                 <div class="col-md-1">
                                                    <label class="control-label ">Loan Period</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtLnvPeriod" Placeholder="Loan Period" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <div class="col-md-1">
                                                    <label class="control-label ">Attendance Yr.</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtAttendance"  Placeholder="Attendance Yr."  onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-md-1">
                                                    <label class="control-label ">Atte Minimum.</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtAtteMin"  Placeholder="Attendance Min."  onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <div class="col-md-1">
                                                    <label class="control-label ">AsOnDate</label>
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
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row" style="margin: 7px 0 7px 0; text-align: center">
                                <div class="row">
                                    <div class="col-lg-12" style="text-align: center">
                                        <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-primary" Text="Report Print" OnClick="btnPrint_Click" OnClientClick="Javascript:return isvalidate();" />
                                        <asp:Button ID="btnClear" runat="server" CssClass="btn btn-primary" Text="Clear" OnClick="btnClear_Click" OnClientClick="Javascript:return validate();" />
                                        <asp:Button ID="btnExit" runat="server" CssClass="btn btn-primary" Text="Exit" OnClick="btnExit_Click" OnClientClick="Javascript:return validate();" />
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
