<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmClassifiction.aspx.cs" Inherits="FrmClassifiction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-md-12">
        <div class="portlet box blue" id="Div1">
            <div class="portlet-title">
                <div class="caption">
                    Account Statment
                </div>
            </div>
            <div class="portlet-body form">
                <div class="form-horizontal">
                    <div class="form-wizard">
                        <div class="form-body">
                            <div class="tab-content">
                                <div class="tab-pane active" id="tab1">
                                    <div>
                                        <div class="row" style="margin: 7px 0 7px 0;">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Classification Of Deposit : <span class="required">* </span></label>
                                                <div class="col-md-4">
                                                    <asp:RadioButton ID="rdbSummary" runat="server" Text="Summary" GroupName="SD" />
                                                    <asp:RadioButton ID="rdbDetaile" runat="server" Text="Details" GroupName="SD" />
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:Button ID="btnDeposit" runat="server" Text="Deposit" OnClick="btnDeposit_Click" CssClass="btn btn-primary" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0;">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Classification Of Loan : <span class="required">* </span></label>
                                                <div class="col-md-4">
                                                    <asp:RadioButton ID="rdbLSummary" runat="server" Text="Summary" GroupName="LSD" />
                                                    <asp:RadioButton ID="rdbLDetail" runat="server" Text="Details" GroupName="LSD" />
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:Button ID="BtnLoan" runat="server" Text="Loan" OnClick="BtnLoan_Click" CssClass="btn btn-primary" />
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
</asp:Content>

