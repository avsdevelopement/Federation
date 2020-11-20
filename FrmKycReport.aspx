<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmKycReport.aspx.cs" Inherits="FrmKycReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="Report" />
        </Triggers>
        <ContentTemplate>
            <div class="row">
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
                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">
                                                        <label class="control-label col-md-2">Kyc Type<span class="required"></span></label>
                                                        <div class="col-md-3">
                                                            <asp:DropDownList ID="ddlKYCType" runat="server" CssClass="form-control">
                                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="All" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="Completed" Value="2"></asp:ListItem>
                                                                <asp:ListItem Text="In Completed" Value="3"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <label class="control-label col-md-2">Export Report<span class="required"></span></label>
                                                        <div class="col-md-3">
                                                            <asp:DropDownList ID="ddlExportT" runat="server" CssClass="form-control">
                                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="PDF" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="Excel" Value="2"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-md-3" style="text-align: center">
                                                            <asp:Button ID="Report" runat="server" CssClass="btn btn-success" Text="KYC Report" OnClick="Report_Click" />
                                                        </div>
                                                        <div class="col-md-3">
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
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

