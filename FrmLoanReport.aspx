<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmLoanReport.aspx.cs" Inherits="FrmLoanReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>AVS In-So-Tech</title>
    <!-- BEGIN GLOBAL MANDATORY STYLES -->
    <link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&subset=all" rel="stylesheet" type="text/css">
    <link href="global/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css">
    <link href="global/plugins/simple-line-icons/simple-line-icons.min.css" rel="stylesheet" type="text/css">
    <link href="global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css">
    <link href="global/plugins/uniform/css/uniform.default.css" rel="stylesheet" type="text/css">
    <link href="global/plugins/bootstrap-switch/css/bootstrap-switch.min.css" rel="stylesheet" type="text/css" />
    <link href="global/css/components-rounded.css" rel="stylesheet" type="text/css" />
    <!-- END GLOBAL MANDATORY STYLES -->
    <script>
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;

            return true;
        }
    </script>

    <script type="text/javascript">
        function isvalidate() {
            var pcode = document.getElementById('<%=TxtPtype.ClientID%>').value;
            var accno = document.getElementById('<%=TxtAccNo.ClientID%>').value;
            var message = '';

            if (pcode == "") {
                message = 'Please Enter product code....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtPtype.ClientID%>').focus();
                return false;
            }

            if (accno == "") {
                message = 'Please Enter account No....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtPtype.ClientID%>').focus();
                return false;
            }
        }

    </script>
</head>

<body>
    <form id="form1" runat="server">
        <div>
            <div class="row" style="margin: 10px 10px 10px 10px">
                <div class="col-md-12">
                    <div class="portlet box blue" id="form_wizard_1">
                        <div class="portlet-title">
                            <div class="caption">
                                Schedule Report
                            </div>
                        </div>
                        <div class="portlet-body form">
                            <div class="form-horizontal">
                                <div class="form-wizard">
                                    <div class="form-body">

                                        <div class="tab-content">
                                            <div class="row" style="margin: 7px 0 7px 0; margin-top: 20px;">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Product Code : <span class="required">* </span></label>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="TxtPtype" runat="server" Style="Width: 38%; height: 33px; border: 1px solid #c0c1c1;" Enabled="false"></asp:TextBox>
                                                        <asp:TextBox ID="TxtPname" runat="server" Style="width: 61%; height: 33px; margin-left: -3px; border: 1px solid #c0c1c1;" Enabled="false"></asp:TextBox>
                                                    </div>
                                                    <label class="control-label col-md-2">Account No : <span class="required">* </span></label>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="TxtAccNo" runat="server" Style="Width: 38%; height: 33px; border: 1px solid #c0c1c1;" Enabled="false"></asp:TextBox>
                                                        <asp:TextBox ID="TxtCustName" runat="server" Style="width: 61%; height: 33px; margin-left: -3px; border: 1px solid #c0c1c1;" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="margin: 7px 0 7px 0; margin-top: 20px;">
                                                <div class="col-lg-12">
                                                    <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btn btn-primary" OnClick="btnPrint_Click" OnClientClick="Javascript:return isvalidate();" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="portlet-body form">
                                <div class="form-horizontal">
                                    <div class="form-wizard">
                                        <div class="form-body">
                                            <div class="tab-content">
                                                <div class="col-lg-12">
                                                    <div class="table-scrollable">
                                                        <table class="table table-striped table-bordered table-hover">
                                                            <thead>
                                                                <tr>
                                                                    <th>
                                                                        <asp:GridView ID="GrdLoanSchedule" runat="server" AllowPaging="True"
                                                                            AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                                            EditRowStyle-BackColor="#FFFF99"
                                                                            OnPageIndexChanging="GrdLoanSchedule_PageIndexChanging"
                                                                            PageIndex="10" PageSize="25"
                                                                            PagerStyle-CssClass="pgr" Width="100%">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Payment No" Visible="true">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Srno" runat="server" Text='<%# Eval("Srno") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>


                                                                                <asp:TemplateField HeaderText="Payment Date" Visible="true">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="INSTDATE" runat="server" Text='<%# Eval("INSTDATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Begining Balance">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="BALANCE" runat="server" Text='<%# Eval("BALANCE") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Schedule Payment">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Schedule" runat="server" Text='<%# Eval("Schedule") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Principle">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Principle" runat="server" Text='<%# Eval("Principle") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Interest">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="INTEREST" runat="server" Text='<%# Eval("INTEREST") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Ending Balance">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="EBALANCE" runat="server" Text='<%# Eval("EBALANCE") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Cumulative Interest">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="CINTEREST" runat="server" Text='<%# Eval("CINTEREST") %>'></asp:Label>
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
    </form>


</body>
</html>
