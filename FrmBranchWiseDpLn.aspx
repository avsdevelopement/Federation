<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmBranchWiseDpLn.aspx.cs" Inherits="FrmBranchWiseDpLn" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function isvalidate() {

            var AsOnDate = document.getElementById('<%=txtAsOnDate.ClientID%>').value;

            var message = '';

            if (BrCode == "0") {
                message = 'Select branch code first...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                return false;
            }

            if (AsOnDate == "") {
                message = 'Enter from date first...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtAsOnDate.ClientID%>').focus();
                return false;
            }
        }


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
    <contenttemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="portlet box blue" id="form_wizard_1">
                        <div class="portlet-title">
                            <div class="caption">
                                Branch Wise Deposit / Loans List
                            </div>
                        </div>
                        <div class="portlet-body form">
                            <div class="form-horizontal">
                                <div class="form-wizard">
                                    <div class="form-body">
                                        <div class="tab-content">
                                            <div class="tab-pane active" id="tab__blue">
                                            </div>
                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:RadioButtonList ID="Rdb_EntryType" runat="server" RepeatDirection="Horizontal" Style="width: 350px;">
                                                            <asp:ListItem Text="In Thousand" Value="1000" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="In Lacs" Value="100000"></asp:ListItem>
                                                            <asp:ListItem Text="In Crore" Value="10000000"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <label class="control-label">Product Type :<span class="required"> *</span></label>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:RadioButtonList ID="rbtnProdType" runat="server" RepeatDirection="Horizontal" Style="width: 250px;">
                                                            <asp:ListItem Text="Deposit" Value="1" />
                                                            <asp:ListItem Text="Loan" Value="2" />
                                                            <asp:ListItem Text="Loan Details" Value="3" />
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Branch Code<span class="required"> *</span></label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtBrID" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <label class="control-label ">As On Date :<span class="required"> *</span></label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtAsOnDate" onkeyup="FormatIt(this);" onkeypress="javascript:return isNumber(event)" placeholder="DD/MM/YYYY" CssClass="form-control" runat="server"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtAsOnDate">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:CheckBox ID="cnkPrevMonth" Text="With Prev Month" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 15px; margin-bottom: 5px;">
                                                <div class="col-lg-12 text-center">
                                                    <div class="col-md-6">
                                                        <asp:Button ID="btnReport" runat="server" CssClass="btn btn-danger" Text="Print  Report" OnClick="btnReport_Click" OnClientClick="Javascript:return isvalidate();" />
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="row">

                                        <div id="alertModal" class="modal fade">
                                            <div class="modal-dialog">
                                                <div class="modal-content">
                                                    <div class="modal-header">
                                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                        <center><h4 class="modal-title" style="color:#ff0000">AVS Company</h4></center>
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

                                    </div>


                                </div>
                            </div>
                        </div>
                    </div>
                </div>
</asp:Content>

