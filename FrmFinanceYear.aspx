<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmFinanceYear.aspx.cs" Inherits="FrmFinanceYear" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div id="page-wrapper">
        <div class="row">
            <div class="col-md-12">
                <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                    <div class="col-lg-12">
                    </div>
                </div>
                <div class="portlet box blue" id="form_wizard_1">
                    <div class="portlet-title">
                        <div class="caption">
                            Financial Year
                        </div>
                    </div>
                    <div class="portlet-body form">
                        <div class="form-horizontal">
                            <div class="form-wizard">
                                <div class="form-body">
                                    <div class="tab-content">
                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-3">
                                                    <asp:RadioButtonList ID="Rdb_EntryType" runat="server" RepeatDirection="Horizontal" Style="width: 800px;">
                                                        <asp:ListItem Text="In Thousand" Value="1" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="In Lacs" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="In Crore" Value="3"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label ">From Date<span class="required"> *</span></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtFDate" onkeyup="FormatIt(this);" onkeypress="javascript:return isNumber(event)" placeholder="DD/MM/YYYY" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <asp:CalendarExtender ID="txtAsOnDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtFDate">
                                                    </asp:CalendarExtender>
                                                </div>
                                                <%--<div class="col-md-2">
                                                    <label class="control-label ">To Date<span class="required"> *</span></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtTDate" onkeyup="FormatIt(this);" onkeypress="javascript:return isNumber(event)" placeholder="DD/MM/YYYY" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtTDate">
                                                    </asp:CalendarExtender>
                                                </div>--%>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="col-md-2">
                                        </div>
                                        <div class="col-md-1">
                                            <asp:Button ID="btnReport" runat="server" CssClass="btn blue" Text="Show" OnClientClick="Javascript:return isvalidate();" OnClick="btnReport_Click"  />
                                        </div>
                                        <%-- <div class="col-md-2">
                                            <asp:Button ID="btnReportMapping" runat="server" CssClass="btn blue" Text="Report Mapping" />
                                        </div>--%>
                                        <div class="col-md-2">
                                            <asp:Button ID="btnPrint" runat="server" CssClass="btn blue" Text="Report Print" OnClientClick="Javascript:return isvalidate();" OnClick="btnPrint_Click"   />
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Button ID="btnBack" runat="server" CssClass="btn blue" Text="Back" OnClick="btnBack_Click"  />
                                         </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">

                <div class="table-scrollable" style="width: 100%; height: 350px; overflow-x: auto; overflow-y: auto">
                    <asp:Label ID="Lbl_Finan" runat="server" Text="Financial Year" BackColor="#ffccff" Font-Bold="true" Font-Size="Small"></asp:Label>
                    <asp:Table ID="Tbl_1" runat="server" Style="width: 33%;" class="table table-striped table-bordered table-hover">
                        <asp:TableRow>
                            <asp:TableCell ID="Tcell1" runat="server">
                                <thead>
                                    <tr>
                                        <th>
                                            <asp:GridView ID="GridFinance" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                EditRowStyle-BackColor="#FFFF99"
                                                PagerStyle-CssClass="pgr" Width="100%" runat="server">
                                                <Columns>

                                                    <%--SrNo	BranchCode	BranchName	Deposit	Loans--%>
                                                    <asp:TemplateField HeaderText="SrNo" Visible="true" HeaderStyle-BackColor="#99ccff">
                                                        <ItemTemplate>
                                                            <asp:Label ID="SrNo" runat="server" Text='<%# Eval("SrNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="BRCD" HeaderStyle-BackColor="#99ccff">
                                                        <ItemTemplate>
                                                            <asp:Label ID="BranchCode" runat="server" Text='<%# Eval("BranchCode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Br. Name" HeaderStyle-BackColor="#99ccff">
                                                        <ItemTemplate>
                                                            <asp:Label ID="BranchName" runat="server" Text='<%# Eval("BranchName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Deposit" HeaderStyle-BackColor="#99ccff">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Deposit" runat="server" Text='<%# Eval("Deposit") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Loans" HeaderStyle-BackColor="#99ccff">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Loans" runat="server" Text='<%# Eval("Loans") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CD RATIO" HeaderStyle-BackColor="#99ccff">
                                                        <ItemTemplate>
                                                            <asp:Label ID="CDRATIO" runat="server" Text='<%# Eval("CDRatio") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <%-- <asp:TemplateField HeaderText="Edit" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkSelect" runat="server" CommandArgument='<%#Eval("ACNO")%>' CommandName="select" OnClick="lnkSelect_Click" class="glyphicon glyphicon-check"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                </Columns>
                                                <PagerStyle CssClass="pgr" />
                                                <SelectedRowStyle BackColor="#66FF99" />
                                                <EditRowStyle BackColor="#FFFF99" />
                                                <AlternatingRowStyle CssClass="alt" />
                                            </asp:GridView>

                                        </th>
                                    </tr>
                                </thead>

                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </div>

            </div>
            <div class="col-lg-12">
                <div class="table-scrollable" style="width: 100%; height: 350px; overflow-x: auto; overflow-y: auto">
                    <asp:Label ID="Lbl_DepLon" runat="server" Text="Deposit & Loan" BackColor="#ffccff" Font-Bold="true" Font-Size="Small"></asp:Label>
                    <asp:Table ID="Table1" runat="server" Style="width: 33%;" class="table table-striped table-bordered table-hover">
                        <asp:TableRow>

                            <asp:TableCell ID="TableCell1" runat="server">
                                <thead>
                                    <tr>
                                        <th>
                                            <asp:GridView ID="GridDL" runat="server" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                EditRowStyle-BackColor="#FFFF99"
                                                PagerStyle-CssClass="pgr" Width="100%">
                                                <Columns>

                                                    <%--SrNo	BranchCode	BranchName	Deposit	Loans--%>
                                                    <asp:TemplateField HeaderText="SrNo" Visible="true" HeaderStyle-BackColor="#ffff99">
                                                        <ItemTemplate>
                                                            <asp:Label ID="SrNo" runat="server" Text='<%# Eval("SrNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="BRCD" HeaderStyle-BackColor="#ffff99">
                                                        <ItemTemplate>
                                                            <asp:Label ID="BranchCode" runat="server" Text='<%# Eval("BranchCode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="BranchName" HeaderStyle-BackColor="#ffff99">
                                                        <ItemTemplate>
                                                            <asp:Label ID="BranchName" runat="server" Text='<%# Eval("BranchName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Deposit" HeaderStyle-BackColor="#ffff99">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Deposit" runat="server" Text='<%# Eval("Deposit") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Loans" HeaderStyle-BackColor="#ffff99">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Loans" runat="server" Text='<%# Eval("Loans") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CD RATIO" HeaderStyle-BackColor="#ffff99">
                                                        <ItemTemplate>
                                                            <asp:Label ID="CDRATIO" runat="server" Text='<%# Eval("CDRatio") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <%-- <asp:TemplateField HeaderText="Edit" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkSelect" runat="server" CommandArgument='<%#Eval("ACNO")%>' CommandName="select" OnClick="lnkSelect_Click" class="glyphicon glyphicon-check"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                </Columns>
                                                <PagerStyle CssClass="pgr" />
                                                <SelectedRowStyle BackColor="#66FF99" />
                                                <EditRowStyle BackColor="#FFFF99" />
                                                <AlternatingRowStyle CssClass="alt" />
                                            </asp:GridView>

                                        </th>
                                    </tr>
                                </thead>

                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </div>
            </div>
            <div class="col-lg-12">
                <div class="table-scrollable" style="width: 100%; height: 350px; overflow-x: auto; overflow-y: auto">
                    <asp:Label ID="Label2" runat="server" Text="Calculation" BackColor="#ffccff" Font-Bold="true" Font-Size="Small"></asp:Label>
                    <asp:Table ID="Table2" runat="server" Style="width: 33%;">
                        <asp:TableRow>

                            <asp:TableCell ID="TableCell2" runat="server">
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </div>
            </div>

        </div>
        <div class="row">
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

</asp:Content>

