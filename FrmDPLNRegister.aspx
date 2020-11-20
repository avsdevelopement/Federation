<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmDPLNRegister.aspx.cs" Inherits="FrmDPLNRegister" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <style type="text/css">
        .auto-style3 {
            width: 412px;
            height: 45px;
        }

        .auto-style4 {
            width: 412px;
            height: 37px;
        }

        .auto-style5 {
            width: 412px;
            height: 47px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        Loan & Deposit Register
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">

                                <div class="row" style="margin-top: 5px;">
                                    <div class="col-lg-12">
                                        <div class="col-md-2">
                                            <label class="control-label">As On Date : <span class="required">*</span></label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="TxtDate" CssClass="form-control"  runat="server" placeholder="dd/MM/yyyy"></asp:TextBox>
                                            <asp:TextBoxWatermarkExtender ID="TxtDate_WatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtDate">
                                            </asp:TextBoxWatermarkExtender>
                                            <asp:CalendarExtender ID="TxtDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtDate">
                                            </asp:CalendarExtender>
                                        </div>
                                    </div>
                                </div>

                                <div class="row" style="margin-top: 5px;">
                                    <div class="col-lg-12">
                                        <div class="col-md-2">
                                            <label class="control-label">Sub GLCode : <span class="required">*</span></label>
                                        </div>
                                        <%--<div class="col-md-2">
                                            <asp:TextBox ID="TxtGLCD" runat="server" CssClass="form-control" ></asp:TextBox>
                                        </div>--%>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="TxtSubgl" runat="server" CssClass="form-control" OnTextChanged="TxtSubgl_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="TxtSGLName" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                            </div>

                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9">
                                        <asp:Button ID="BtnDSS" runat="server" Text="DDS Report" OnClick="BtnDSS_Click" />
                                        <asp:Button ID="BtnDeposit" runat="server" Text="Deposit Report" OnClick="BtnDeposit_Click" />
                                        <asp:Button ID="BtnLoan" runat="server" Text="Loan Report" OnClick="BtnLoan_Click" />
                                        <asp:Button ID="BtnClose" runat="server" Text="Close" />
                                        <asp:Button ID="BtnLoanR" runat="server" Text="Loan Report" Visible="true" OnClick="BtnLoanR_Click" CssClass="btn btn-primary" />
                                        <asp:Button ID="BtnDepositR" runat="server" Text="Deposit Report" Visible="true" OnClick="BtnDepositR_Click" CssClass="btn btn-primary" />
                                        <asp:Button ID="btnDDSR" runat="server" Text="DDS Report" Visible="true" OnClick="btnDDSR_Click" CssClass="btn btn-primary" />
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
                                        <asp:GridView ID="gridadd" runat="server" OnPageIndexChanging="gridadd_PageIndexChanging" PageIndex="10" PageSize="25" AutoGenerateColumns="false" AllowPaging="True">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SUBGLCODE" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="SUBGLCODE" runat="server" Text='<%# Eval("SUBGLCODE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="CUSTNO">
                                                    <ItemTemplate>
                                                        <asp:Label ID="CUSTNO" runat="server" Text='<%# Eval("CUSTNO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="ACCNO">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ACCNO" runat="server" Text='<%# Eval("ACCNO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="CUSTNAME">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LCUSTNAME" runat="server" Text='<%# Eval("CUSTNAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="LIMIT" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LIMIT" runat="server" Text='<%# Eval("LIMIT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="DUEDATE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="DUEDATE" runat="server" Text='<%# Eval("DUEDATE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="INTRATE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="INTRATE" runat="server" Text='<%# Eval("INTRATE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SANSSIONDATE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="SANSSIONDATE" runat="server" Text='<%# Eval("SANSSIONDATE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="INSTALLMENT">
                                                    <ItemTemplate>
                                                        <asp:Label ID="INSTALLMENT" runat="server" Text='<%# Eval("INSTALLMENT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PERIOD">
                                                    <ItemTemplate>
                                                        <asp:Label ID="PERIOD" runat="server" Text='<%# Eval("PERIOD") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="BALANCE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="BALANCE" runat="server" Text='<%# Eval("BALANCE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </th>
                                </tr>
                            </thead>
                        </table>
                    </div>
                    <div class="col-md-12">
                        <table class="table table-striped table-bordered table-hover" width="100%">
                            <thead>
                                <tr>
                                    <th>
                                        <asp:GridView ID="Griddeposit" OnPageIndexChanging="Griddeposit_PageIndexChanging" runat="server" PageIndex="10" PageSize="25" AutoGenerateColumns="false" AllowPaging="True">
                                            <Columns>
                                                <asp:TemplateField HeaderText="CUSTNO" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="CUSTNO" runat="server" Text='<%# Eval("CUSTNO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="CUSTACCNO">
                                                    <ItemTemplate>
                                                        <asp:Label ID="CUSTACCNO" runat="server" Text='<%# Eval("CUSTACCNO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="CUSTNAME">
                                                    <ItemTemplate>
                                                        <asp:Label ID="DCUSTNAME" runat="server" Text='<%# Eval("CUSTNAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="DEPOSITGLCODE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="DEPOSITGLCODE" runat="server" Text='<%# Eval("DEPOSITGLCODE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="PRNAMT" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="PRNAMT" runat="server" Text='<%# Eval("PRNAMT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="RATEOFINT">
                                                    <ItemTemplate>
                                                        <asp:Label ID="RATEOFINT" runat="server" Text='<%# Eval("RATEOFINT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="OPENINGDATE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="OPENINGDATE" runat="server" Text='<%# Eval("OPENINGDATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DUEDATE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="DUEDATE" runat="server" Text='<%# Eval("DUEDATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PERIOD">
                                                    <ItemTemplate>
                                                        <asp:Label ID="PERIOD" runat="server" Text='<%# Eval("PERIOD") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="INTAMT">
                                                    <ItemTemplate>
                                                        <asp:Label ID="INTAMT" runat="server" Text='<%# Eval("INTAMT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MATURITYAMT">
                                                    <ItemTemplate>
                                                        <asp:Label ID="MATURITYAMT" runat="server" Text='<%# Eval("MATURITYAMT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="BALANCE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="BALANCE" runat="server" Text='<%# Eval("BALANCE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                            </Columns>

                                        </asp:GridView>
                                    </th>
                                </tr>
                            </thead>
                        </table>
                    </div>
                     <div class="col-md-12">
                        <table class="table table-striped table-bordered table-hover" width="100%">
                            <thead>
                                <tr>
                                    <th>
                                        <asp:GridView ID="gridDDS" runat="server" OnPageIndexChanging="gridDDS_PageIndexChanging" ShowFooter="true" 
                                            PageIndex="0" PageSize="25" HeaderStyle-HorizontalAlign="Center" AutoGenerateColumns="false" AllowPaging="True" OnRowDataBound="gridDDS_RowDataBound">
                                            <Columns> 
                                                <asp:TemplateField  HeaderStyle-HorizontalAlign="Center" HeaderText="CUST NO" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="CUSTNO" runat="server" Text='<%# Eval("CUSTNO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        Total
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="CUSTACCNO"  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ACCNO1" runat="server" Text='<%# Eval("ACCNO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField  HeaderStyle-HorizontalAlign="Center"  HeaderText="CUST NAME" HeaderStyle-Width="300px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LCUSTNAME" runat="server" Text='<%# Eval("CUSTNAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>

                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" HeaderText="BALANCE" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="100px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="BALANCE" runat="server" Text='<%# Eval("Closing") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblBal" runat="server"></asp:Label>
                                                    </FooterTemplate>
                                                   
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
                    <p>
                        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                    </p>

                </div>
                <div class="modal-footer">
                    <button id="Button1" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

