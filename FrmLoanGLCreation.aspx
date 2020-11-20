<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmLoanGLCreation.aspx.cs" Inherits="FrmLoanGLCreation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
        function isvalidate() {

            var DepCode, DepType, ReportName, LoanAmt, IntRate, Peroid, AccNo1, AccNo2, AccNo3, AccNo4, AccNo5, AccNo6, AccNo7, AccNo8, AccNo9;
            DepCode = document.getElementById('<%=txtLoanCode.ClientID%>').value;
        DepType = document.getElementById('<%=txtLoanType.ClientID%>').value;
        ReportName = document.getElementById('<%=txtRepName.ClientID%>').value;
        LoanAmt = document.getElementById('<%=txtLoanAmt.ClientID%>').value;
        IntRate = document.getElementById('<%=txtIntRate.ClientID%>').value;
        Peroid = document.getElementById('<%=txtPeriod.ClientID%>').value;
       AccNo1 = document.getElementById('<%=TxtIRCode.ClientID%>').value;
       AccNo2 = document.getElementById('<%=TxtIRName.ClientID%>').value;
        AccNo3 = document.getElementById('<%=TxtPenCode.ClientID%>').value;
        AccNo4 = document.getElementById('<%=TxtPenCode.ClientID%>').value;
      
        var message = '';

        if (DepCode == "") {
            message = 'Please Enter Deposite Code....!!\n';
            $('#alertModal').find('.modal-body p').text(message);
            $('#alertModal').modal('show')
            document.getElementById('<%=txtLoanCode.ClientID%>').focus();
                return false;
            }

            if (DepType == "") {
                message = 'Please Enter Deposite Type....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtLoanType.ClientID%>').focus();
                return false;
            }

            if (ReportName == "") {
                message = 'Please Enter Report Name....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtRepName.ClientID%>').focus();
                return false;
            }

            if (LoanAmt == "") {
                message = 'Please Enter Loan Amount....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtLoanAmt.ClientID%>').focus();
                    return false;
                }

                if (IntRate == "") {
                    message = 'Please Enter Int Rate....!!\n';
                    $('#alertModal').find('.modal-body p').text(message);
                    $('#alertModal').modal('show')
                    document.getElementById('<%=txtIntRate.ClientID%>').focus();
                return false;
            }

            if (period == "") {
                message = 'Please Enter Period....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtPeriod.ClientID%>').focus();
                return false;
            }

            if (AccNo1 == "") {
                message = 'Please Enter Value....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtIRCode.ClientID%>').focus();
                return false;
            }

            if (AccNo2 == "") {
                message = 'Please Enter Value....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtIRName.ClientID%>').focus();
                return false;
            }

            if (AccNo3 == "") {
                message = 'Please Enter Value....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtPenCode.ClientID%>').focus();
                return false;
            }

            if (AccNo4 == "") {
                message = 'Please Enter Value....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtPenName.ClientID%>').focus();
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
    </script>
    <script type="text/javascript">
        function IRName() {
            document.getElementById('<%=TxtIRName.ClientID%>').value="INTEREST ON " + document.getElementById('<%=txtLoanType.ClientID%>').value;
            document.getElementById('<%=TxtPenName.ClientID%>').value="PENAL INT ON " + document.getElementById('<%=txtLoanType.ClientID%>').value;
            
        }
    </script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        Loan GL Master
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
                                            <div class="col-lg-11">
                                                <label class="control-label col-md-3">Loan Code : <span class="required">* </span></label>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtLoanCode" OnTextChanged="txtLoanCode_TextChanged" Enabled="false" AutoPostBack="true" CssClass="form-control" runat="server" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-11">
                                                <label class="control-label col-md-3">Loan Name : <span class="required">* </span></label>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtLoanType" style="text-transform: uppercase" onkeyup="IRName();" CssClass="form-control" runat="server" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-11">
                                                <label class="control-label col-md-3">Category : <span class="required">* </span></label>
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="ddlCategory" CssClass="form-control" runat="server"></asp:DropDownList>
                                                </div>
                                                <label class="control-label col-md-2">Int Type : <span class="required">* </span></label>
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="ddlIntType" CssClass="form-control" runat="server">
                                                        <asp:ListItem Text="Outstanding" Value="1" />
                                                        <asp:ListItem Text="Principle" Value="2" />
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-2"></div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-11">
                                                <div class="col-md-3">
                                                </div>
                                                <div class="col-md-3">
                                                </div>
                                                <div class="col-md-2">
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtIntAmount" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber(event)" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-11">
                                                <label class="control-label col-md-3">Loan Amount : <span class="required">* </span></label>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtLoanAmt" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber(event)" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-11">
                                                <label class="control-label col-md-3">Report Name : <span class="required">* </span></label>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtRepName" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-3">
                                                    <span class="required">Enter short Name </span>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-11">
                                                <label class="control-label col-md-3">Interest Rate : <span class="required">* </span></label>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtIntRate" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server" />
                                                </div>
                                                <label class="control-label col-md-3">Period : <span class="required">* </span></label>
                                                   <div class="col-md-3">
                                                    <asp:TextBox ID="txtPeriod" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server" />
                                                </div>
                                             </div>
                                        </div>
                                       <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-11">
                                                <label class="control-label col-md-3">Intrest on code: <span class="required">* </span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtIRCode" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtIRName" style="text-transform: uppercase"  CssClass="form-control" runat="server" />

                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-11">
                                                <label class="control-label col-md-3">Penal Interest code : <span class="required">* </span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtPenCode" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtPenName" style="text-transform: uppercase" CssClass="form-control" runat="server" />

                                                </div>
                                            </div>
                                        </div>
                                        <%--<div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-11">
                                                <label class="control-label col-md-3">Intrest Acc No : <span class="required">* </span></label>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TextBox1" onkeypress="javascript:return isNumber(event)"  CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TextBox2" OnTextChanged="TextBox2_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TextBox3" CssClass="form-control" runat="server" />
                                                </div>
                                            </div>
                                        </div>--%>

                                        <%--<div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-11">
                                                <label class="control-label col-md-3">Interest Acc No : <span class="required">* </span></label>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TextBox4" onkeypress="javascript:return isNumber(event)"  CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TextBox5" OnTextChanged="TextBox5_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TextBox6" CssClass="form-control" runat="server" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-11">
                                                <label class="control-label col-md-3">Penal Acc No : <span class="required">* </span></label>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TextBox7" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TextBox8" OnTextChanged="TextBox8_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TextBox9" CssClass="form-control" runat="server" />
                                                </div>
                                            </div>
                                        </div>--%>
                                    </div>

                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9">
                                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn blue" Text="Submit" OnClick="btnSubmit_Click" OnClientClick="Javascript:return isvalidate();" />
                                        <asp:Button ID="btnModify" runat="server" CssClass="btn blue" Text="UPDATE" OnClick="btnModify_Click" OnClientClick="Javascript:return isvalidate();" />
                                        <asp:Button ID="btnDelete" runat="server" CssClass="btn blue" Text="DELETE" OnClick="btnDelete_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                    </div>
                    <div class="col-md-12">
                        <table class="table table-striped table-bordered table-hover" width="100%">
                            <thead>
                                <tr>
                                    <th>
                                        <asp:GridView ID="grdLoanGL" runat="server" AllowPaging="True" AlternatingRowStyle-CssClass="alt" CssClass="mGrid"
                                            EditRowStyle-BackColor="#FFFF99" OnPageIndexChanging="grdLoanGL_PageIndexChanging" PagerStyle-CssClass="pgr" Width="100%">
                                            <%--<Columns>

                                                <asp:TemplateField HeaderText="Glcode" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="gid" runat="server" Text='<%# Eval("") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Subglcode">
                                                    <ItemTemplate>
                                                        <asp:Label ID="subgl" runat="server" Text='<%# Eval("") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Gl Group" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="gGrp" runat="server" Text='<%# Eval("") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="GL Name" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="glname" runat="server" Text='<%# Eval("") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Opening Balance" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="bal" runat="server" Text='<%# Eval("") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Type" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="CRDR" runat="server" Text='<%# Eval("") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>--%>
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

