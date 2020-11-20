<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmRelizedUser.aspx.cs" Inherits="FrmRelizedUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="portlet box blue" id="form_wizard_1">
                        <div class="portlet-title">
                            <div class="caption">
                                Realized User
                            </div>
                        </div>
                        <div class="portlet-body form">
                            <div class="form-horizontal">
                                <div class="form-wizard">
                                    <div class="form-body">
                                        <div class="tab-content">
                                            <div class="tab-pane active" id="tab__blue">                                                
                                            </div>
                                            <div style="border: 1px solid #3598dc">
                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12" style="margin-top: 15px;">
                                                        <div class="col-md-2">
                                                            <label class="control-label">User Id</label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtUserId" runat="server" CssClass="form-control" ></asp:TextBox>                                                            
                                                        </div>
                                                        <div class ="col-md-5">
                                                            <asp:Button ID="Realized" runat="server" OnClick="Realized_Click" Text="Realized" CssClass="btn btn-primary" />
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

