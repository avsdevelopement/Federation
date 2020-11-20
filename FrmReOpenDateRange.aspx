<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmReOpenDateRange.aspx.cs" Inherits="FrmReOpenDateRange" %>
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

        function IsValid() {

            var txtBrCode = document.getElementById('<%=txtBrCode.ClientID%>').value;
            var txtFDate = document.getElementById('<%=txtFDate.ClientID%>').value;
            var txtTDate = document.getElementById('<%=txtTDate.ClientID%>').value;
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
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="Div1">
                <div class="portlet-title">
                    <div class="caption">
                        Admin's Day Re-Open Between Date
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
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
                                                <label class="control-label col-md-2">Working Date <span class="required">* </span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtFDate" Enabled="false" CssClass="form-control" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this)" runat="server"></asp:TextBox>
                                                    <asp:CalendarExtender ID="txtFDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtFDate">
                                                    </asp:CalendarExtender>
                                                </div>
                                                <label class="control-label col-md-2">Back Date <span class="required">* </span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtTDate" CssClass="form-control" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this)" runat="server"></asp:TextBox>
                                                    <asp:CalendarExtender ID="txtTDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtTDate">
                                                    </asp:CalendarExtender>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>

                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9">
                                        <asp:Button ID="Btn_DayReOpen" Text="Day Re-Open" runat="server" CssClass="btn btn-primary" OnClick="Btn_DayReOpen_Click" OnClientClick="Javascript:return IsValid();" />
                                        <asp:Button ID="Btn_ClearAll" runat="server" Text="Clear All" CssClass="btn btn-success" OnClick="Btn_ClearAll_Click" />
                                        <asp:Button ID="Btn_Exit" runat="server" Text="Exit" CssClass="btn btn-success" OnClick="Btn_Exit_Click" />
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
                                                <asp:GridView ID="GrdDayReOpen" runat="server" AllowPaging="True" AlternatingRowStyle-CssClass="alt"
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

