<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmHome.aspx.cs" Inherits="FrmHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        Outward clearing 
                    </div>
                </div>
                <div class="portlet-body form">
                    <!--<form action="#" class="form-horizontal" id="submit_form" method="POST">-->
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">

                                <div class="tab-content">
                                    <div class="alert alert-success display-none">
                                        <button class="close" data-dismiss="alert"></button>
                                        Your form validation is successful!
                                    </div>
                                    <div class="tab-pane active" id="tab1">
                                        <div class="form-group">
                                            <label class="control-label col-md-3">Entry Date : <span class="required">* </span></label>
                                            <div class="col-md-3">
                                                <%-- <asp:TextBox ID="txtEntryDate" CssClass="form-control" runat="server"></asp:TextBox--%>
                                                <input class="form-control form-control-inline input-medium date-picker" size="16" type="text" value="">
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-md-3">Set No <span class="required">* </span></label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtSetNo" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-md-3">Confirm Password <span class="required">* </span></label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="TextBox2" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label col-md-3">
                                                Email <span class="required">* </span>
                                            </label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="TextBox3" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9">
                                        <%--<button type="button" class="btn blue" >Submit</button>--%>
                                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn blue" Text="Submit"  OnClick="btnSubmit_Click"/>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--</form>-->
                </div>
            </div>
        </div>
    </div>
</asp:Content>
