<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmPrintGlReport.aspx.cs" Inherits="FrmPrintGlReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
       <%--<Triggers>
           <asp:PostBackTrigger ControlID="Btnprint" />
       </Triggers>--%>
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="portlet box blue" id="form_wizard_1">
                        <div class="portlet-title">
                            <div class="caption">
                               PRINT GL-REPORT
                            </div>
                        </div>
                        <div class="portlet-body form">
                            <div class="form-horizontal">
                                <div class="form-wizard">
                                    <div class="form-body">
                                        <div class="tab-content">                                            
                                            <div style="border: 1px solid #3598dc">
                                                  
                                                
                                                                                                           
                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-10">
                                                        <div class="col-md-2">
                                                            <asp:Button ID="Btnprint" runat="server" CssClass="form-control" Text="Print" OnClick="Btnprint_Click"/>
                                                        </div>
                                                        <div class="col-md-5">
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

