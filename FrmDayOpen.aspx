<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmDayOpen.aspx.cs" Inherits="FrmDayOpen" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
    <script type="text/javascript">
        function FormatIt(obj) {
            if (obj.value.length == 2) // Day
                obj.value = obj.value + "/";
            if (obj.value.length == 5) // month 
                obj.value = obj.value + "/";
            if (obj.value.length == 11) // year 
                alert("Please Enter valid Date");
        }

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
        function IsValid() {
            var BrCode = document.getElementById('<%=txtBrCode.ClientID%>').value;
            var WorkingDate = document.getElementById('<%=txtWorkDate.ClientID%>').value;
            var OpenDate = document.getElementById('<%=txtOpenDate.ClientID%>').value;
            var message = '';

            if (BrCode == "") {
                message = 'Select branch first ....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=txtBrCode.ClientID %>').focus();
                return false;
            }

            if (WorkingDate == "") {
                message = 'Something wrong working date not show....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=txtBrCode.ClientID %>').focus();
                return false;
            }

            if (OpenDate == "") {
                message = 'Something wrong open date not show ....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=txtBrCode.ClientID %>').focus();
                return false;
            }

        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        Day Open Process
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div id="error">
                                    </div>
                                    <div class="tab-pane active" id="tab1">

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Branch Name  <span class="required">*</span></label>
                                                <div class="col-lg-4">
                                                    <asp:DropDownList ID="ddlBrName" CssClass="form-control" OnSelectedIndexChanged="ddlBrName_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-lg-2">
                                                    <asp:TextBox ID="txtBrCode" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Working Date  <span class="required">*</span></label>
                                                <div class="col-lg-2">
                                                    <asp:TextBox ID="txtWorkDate" Enabled="false" onkeyup="FormatIt(this);" onkeypress="javascript:return isNumber(event)" placeholder="DD/MM/YYYY" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <asp:CalendarExtender ID="txtWorkDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtWorkDate">
                                                    </asp:CalendarExtender>
                                                </div>
                                                <label class="control-label col-md-2">Open Date </label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtOpenDate" Enabled="false" onkeyup="FormatIt(this);" onkeypress="javascript:return isNumber(event)" placeholder="DD/MM/YYYY" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <asp:CalendarExtender ID="txtOpenDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtOpenDate">
                                                    </asp:CalendarExtender>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-3">
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Button ID="DayBegin" runat="server" Text="Day Begin" CssClass="btn btn-primary" OnClick="DayBegin_Click" OnClientClick="Javascript:return IsValid();" />
                                    </div>
                                    <div class="col-lg-2">
                                        <asp:Button ID="btnExist" runat="server" Text="Exit" CssClass="btn btn-success" OnClick="btnExist_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                    </div>
                    <div id="DivUserLog" class="row" runat="server" visible="false">
                        <div class="col-lg-12">
                            <div class="table-scrollable">
                                <table class="table table-striped table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th>
                                                <asp:GridView ID="GrdDayOpen" runat="server" AllowPaging="True" AlternatingRowStyle-CssClass="alt"
                                                    AutoGenerateColumns="true" EditRowStyle-BackColor="#FFFF99" PageIndex="10" PageSize="25" PagerStyle-CssClass="pgr" Width="100%">
                                                    <Columns>
                                                        
                                                    </Columns>
                                                </asp:GridView>
                                            </th>
                                        </tr>
                                    </thead>
                                </table>
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

</asp:Content>

