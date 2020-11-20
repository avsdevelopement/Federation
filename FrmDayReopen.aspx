<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmDayReopen.aspx.cs" Inherits="FrmDayReopen" %>
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

    <%--<script type="text/javascript">
        //For Check Disb Date.. Not allow grether than working date
        function MaxDate() {
            debugger;
            var txtWorkingDate = document.getElementById('<%=txtWorkingDate.ClientID%>').value;
            var txtBackDate = document.getElementById('<%=txtBackDate.ClientID%>').value;
            var MaxBackDate = document.getElementById('<%=MaxBackDate.ClientID%>').value;

            var date = MaxBackDate.substring(0, 2);
            var month = MaxBackDate.substring(3, 5);
            var year = MaxBackDate.substring(6, 10);
            var myDate = new Date(year, month - 1, date);

            var date1 = txtWorkingDate.substring(0, 2);
            var month1 = txtWorkingDate.substring(3, 5);
            var year1 = txtWorkingDate.substring(6, 10);
            var myDate1 = new Date(year1, month1 - 1, date1);

            var date2 = txtBackDate.substring(0, 2);
            var month2 = txtBackDate.substring(3, 5);
            var year2 = txtBackDate.substring(6, 10);
            var myDate2 = new Date(year2, month2 - 1, date2);

            if (myDate2 > myDate1) {
                message = 'Not Allow For Future Date...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtBackDate.ClientID%>').focus();
                ClearDate();
                return false;
            }

            if (txtBackDate == "") {
                return false;
            }

            if (myDate2 < myDate) {
                message = 'Not Allow For More Than One Back Date...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtBackDate.ClientID%>').focus();
                ClearDate();
                return false;
            }
        }

        function ClearDate() {
            document.getElementById("<%= txtBackDate.ClientID %>").value = '';
        }
    </script>--%>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        Day Re-Open Activity
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
                                    <div class="col-md-offset-3 col-md-9">
                                        <asp:Button ID="btnReOpen" runat="server" CssClass="btn blue" Text="Re-Open Day" OnClick="btnReOpen_Click" OnClientClick="Javascript:return IsValid();" />
                                        <asp:Button ID="btnOpen" Visible="false" runat="server" CssClass="btn blue" Text="Open Day" OnClick="btnOpen_Click" OnClientClick="Javascript:return IsValid();" />
                                        <asp:Button ID="BtnExit" runat="server" CssClass="btn blue" Text="Exit" OnClick="BtnExit_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                    </div>
                </div>
            </div>
            <div id="DivUserLog" class="row" runat="server" visible="false">
                <div class="col-lg-12">
                    <div class="table-scrollable">
                        <table class="table table-striped table-bordered table-hover">
                            <thead>
                                <tr>
                                    <th>
                                        <asp:GridView ID="GrdUserLogin" runat="server" AllowPaging="True" AlternatingRowStyle-CssClass="alt"
                                            AutoGenerateColumns="true" EditRowStyle-BackColor="#FFFF99" PageIndex="10" PageSize="25" PagerStyle-CssClass="pgr" Width="100%">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Select" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkSelect" runat="server" CommandArgument='<%#Eval("AFLAG")%>' CommandName="select" OnClick="lnkSelect_Click" class="glyphicon glyphicon-check"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </th>
                                </tr>
                            </thead>
                        </table>
                    </div>
                </div>
            </div>
            <div id="DivReopen" class="row" runat="server">
                <div class="col-lg-12">
                    <div class="table-scrollable">
                        <table class="table table-striped table-bordered table-hover">
                            <thead>
                                <tr>
                                    <th>
                                        <asp:GridView ID="grdOpenDay" runat="server" AllowPaging="True" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                            EditRowStyle-BackColor="#FFFF99" OnPageIndexChanging="grdOpenDay_PageIndexChanging" PagerStyle-CssClass="pgr" Width="100%">
                                            <Columns>

                                                <%--<asp:TemplateField HeaderText="Select" Visible="true" runat="server">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkbtnSelect" runat="server" CommandName="select" CommandArgument='<%#Eval("DayBeginDate")%>' OnClick="lnkbtnSelect_Click" class="glyphicon glyphicon-plus"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>

                                                <asp:TemplateField HeaderText="SrNo">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Day Re-Opened">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDayBeginDate" runat="server" Text='<%# Eval("DayBeginDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Opened By">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDayOpBy" runat="server" Text='<%# Eval("DayOpBy") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="HandOver By">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDayHandBy" runat="server" Text='<%# Eval("DayHandBy") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Closed By">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDayClosedBy" runat="server" Text='<%# Eval("DayClosedBy") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Opened Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSystemDate" runat="server" Text='<%# Eval("SystemDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                            <PagerStyle CssClass="pgr" />
                                            <SelectedRowStyle BackColor="#66FF99" />
                                            <EditRowStyle BackColor="#FFFF99" />
                                            <AlternatingRowStyle CssClass="alt" />
                                        </asp:GridView>
                                    </th>
                                </tr>
                            </thead>
                        </table>
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

