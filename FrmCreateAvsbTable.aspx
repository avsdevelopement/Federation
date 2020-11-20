<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmCreateAvsbTable.aspx.cs" Inherits="FrmCreateAvsbTable" %>
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

        function isvalidate() {

            var txtBrCode = document.getElementById('<%=txtBrCode.ClientID%>').value;
            var txtFDate = document.getElementById('<%=txtFDate.ClientID%>').value;
            var txtTDate = document.getElementById('<%=txtTDate.ClientID%>').value;
            var UserGrp = document.getElementById('<%=hdnUserGrp.ClientID%>').value;
            var message = '';

            if (txtBrCode == "") {
                message = 'Enter branch code first...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtBrCode.ClientID%>').focus();
                return false;
            }

            if (txtFDate == "") {
                message = 'Enter from date first...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtFDate.ClientID%>').focus();
                return false;
            }

            if (txtTDate == "") {
                message = 'Enter to date first...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtTDate.ClientID%>').focus();
                return false;
            }

            if (UserGrp != "1") {
                message = 'Allow for admin user only...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtBrCode.ClientID%>').focus();
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
                        Admin's Create Table
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab1">
                                        <asp:Table ID="Tbl1" runat="server">
                                            <asp:TableRow ID="Tbl_row1" runat="server">
                                                <asp:TableCell ID="Tbl_Cell1" runat="server" Style="width: 50%;">
                                                    <div class="row" style="margin-bottom: 10px;">
                                                        
                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-12">
                                                                <label class="control-label col-md-2">BRCD <span class="required">*</span></label>
                                                                <div class="col-md-2">
                                                                    <asp:TextBox ID="txtBrCode" Placeholder="Branch Code" Enabled="false" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" OnTextChanged="txtBrCode_TextChanged"  AutoPostBack="true"></asp:TextBox>
                                                                </div>
                                                                <label class="control-label col-md-2">BRCD Name <span class="required">*</span></label>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="txtBrName" Placeholder="Branch Name" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                                                </div>
                                                                <div class="col=md-2">
                                                                    <input type="hidden" id="hdnUserGrp" runat="server" value="" />
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-12">
                                                                <label class="control-label col-md-2">From Date <span class="required">* </span></label>
                                                                <div class="col-md-2">
                                                                    <asp:TextBox ID="txtFDate" CssClass="form-control" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this)" runat="server"></asp:TextBox>
                                                                    <asp:CalendarExtender ID="txtFDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtFDate">
                                                                    </asp:CalendarExtender>
                                                                </div>
                                                                <label class="control-label col-md-2">To Date <span class="required">* </span></label>
                                                                <div class="col-md-2">
                                                                    <asp:TextBox ID="txtTDate" CssClass="form-control" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this)" runat="server"></asp:TextBox>
                                                                    <asp:CalendarExtender ID="txtTDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtTDate">
                                                                    </asp:CalendarExtender>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row" style="margin: 7px 0 7px 0; text-align: center">
                                                            <div class="col-lg-12">
                                                                <div class="col-md-6">
                                                                    <asp:Button ID="Btn_Submit" OnClientClick="Javascript:return isvalidate();" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="Btn_Submit_Click"  />
                                                                    <asp:Button ID="Btn_ClearAll" runat="server" Text="Clear All" CssClass="btn btn-success" OnClick="Btn_ClearAll_Click"  />
                                                                    <asp:Button ID="Btn_Exit" runat="server" Text="Exit" CssClass="btn btn-success" OnClick="Btn_Exit_Click"  />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                        <asp:Table ID="Tbl2" runat="server">
                                        </asp:Table>
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

