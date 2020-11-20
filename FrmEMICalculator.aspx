<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmEMICalculator.aspx.cs" Inherits="FrmEMICalculator" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function IsValide() {
            var TxtRepayPeriod = document.getElementById('<%=TxtReypayPeriod.ClientID%>').value;
            var ROI = document.getElementById('<%=TxtROI.ClientID%>').value;
            var LoanAmt = document.getElementById('<%=TxtLoanAmt.ClientID%>').value;


            var message = '';

            if (TxtRepayPeriod == "0") {
                //alert("Please Select Prefix.....!!");
                message = 'Please Enter Repay period....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=TxtReypayPeriod.ClientID %>').focus();
                return false;

            }
            if (ROI == "") {
                //alert("Please Select Prefix.....!!");
                message = 'Please Enter Rate of Interest....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=TxtROI.ClientID %>').focus();
                    return false;
                }


            if (LoanAmt == "") {
                    //alert("Please Select Prefix.....!!");
                    message = 'Please Enter Loan Amount....!!\n';
                    $('#alertModal').find('.modal-body p').text(message);
                    $('#alertModal').modal('show')
                    $('#<%=TxtLoanAmt.ClientID %>').focus();
                    return false;
                }


         

        }
    </script>

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
                        EMI Calculator
                        <asp:Literal ID="ltrlHeader" runat="server"></asp:Literal>
                    </div>
                </div>
           
            <div class="portlet-body form">
                <div class="form-horizontal">
                    <div class="form-wizard">
                        <div class="form-body">
                            <div class="tab-content">
                                <div class="tab-pane active" id="tab__blue">
                                    <div style="border: 1px solid #3598dc">
                                         <div class="col-lg-12">
                                            <br />
                                            <label class="control-label col-md-2">Repay Period (in Months)<span class="required">*</span></label>

                                            <div class="col-md-2">
                                                <asp:TextBox ID="TxtReypayPeriod" CssClass="form-control" runat="server" onkeypress="return isNumber(event)"></asp:TextBox>
                                            </div>
                                            <label class="control-label col-md-2">EMI or Monthly Repayment</label>
                                            <div class="col-lg-4">
                                                <asp:TextBox ID="TxtTotalEMIAmt" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                            </div>

                                        </div>
                                        <div class="col-lg-12">
                                            <br />
                                            <label class="control-label col-md-2">Rate of Interest<span class="required">*</span></label>

                                            <div class="col-md-2">
                                                <asp:TextBox ID="TxtROI" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>

                                        </div>
                                        <div class="col-lg-12">
                                            <br />
                                            <label class="control-label col-md-2">Loan Amount<span class="required">*</span></label>

                                            <div class="col-md-2">
                                                <asp:TextBox ID="TxtLoanAmt" CssClass="form-control" runat="server" onkeypress="return isNumber(event)"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-lg-12" style="text-align: center">
                                    <asp:Button ID="Submit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClientClick="Javascript:return IsValide();" OnClick="Submit_Click" />
                                    <asp:Button ID="ClearAll" runat="server" Text="Clear All" CssClass="btn btn-primary" OnClick="ClearAll_Click"/>
                                    &nbsp;<asp:Button ID="Exit" runat="server" Text="Exit" CssClass="btn btn-primary" OnClick="Exit_Click" />
                                    <br />
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
</asp:Content>

