<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmRDCalculator.aspx.cs" Inherits="FrmRDCalculator" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
      <script type="text/javascript">
          function IsValide() {
              var TxtMonthAmt = document.getElementById('<%=TxtMonthAmt.ClientID%>').value;
              var TxtROI = document.getElementById('<%=TxtROI.ClientID%>').value;         
              var TxtPeriodMonths = document.getElementById('<%=TxtPeriodMonths.ClientID%>').value;


            var message = '';

            if (TxtMonthAmt == "0") {
                //alert("Please Select Prefix.....!!");
                message = 'Please Enter Monthly amount....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=TxtMonthAmt.ClientID %>').focus();
                    return false;

                }
              if (TxtROI == "") {
                    //alert("Please Select Prefix.....!!");
                    message = 'Please Enter Rate of interest (P.A)....!!\n';
                    $('#alertModal').find('.modal-body p').text(message);
                    $('#alertModal').modal('show')
                    $('#<%=TxtROI.ClientID %>').focus();
                return false;
            }


              if (TxtPeriodMonths == "") {
                //alert("Please Select Prefix.....!!");
                message = 'Please Enter Period in months....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=TxtPeriodMonths.ClientID %>').focus();
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        Recurring Deposit Calculator
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
                                            <div class="col-md-12">
                                                <asp:RadioButton ID="RdbCompoundQ" Text="Compound Quaterly" GroupName="GN_Cmp" runat="server" />
                                                <asp:RadioButton ID="RdbCompoundHY" Text="Compound Half Yearly" GroupName="GN_Cmp" runat="server" />
                                                <asp:RadioButton ID="RdbCompoundM" Text="Compound Monthly" GroupName="GN_Cmp" runat="server" />

                                            </div>
                                            <div class="col-lg-12">
                                                <br />
                                                <label class="control-label col-md-2">Monthly Intsallment RS.<span class="required">*</span></label>

                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtMonthAmt" CssClass="form-control" runat="server" onkeypress="return isNumber(event)"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>
                                        <div id="Div1" class="row" style="margin: 7px 0 7px 0" runat="server">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Annual Interest Rate<span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtROI" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-2">Total Maturity</label>
                                                <div class="col-lg-4">
                                                    <asp:TextBox ID="TxtTotalMat" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Period in Months</label>
                                                <div class="col-lg-2">
                                                     <asp:TextBox ID="TxtPeriodMonths" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-2">Total Interest</label>
                                                <div class="col-lg-4">
                                                    <asp:TextBox ID="TxtTotalIntAmt" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
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
                                        &nbsp;<asp:Button ID="Exit" runat="server" Text="Exit" CssClass="btn btn-primary" OnClick="Exit_Click"/>
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
    <!--</form>-->
</asp:Content>

