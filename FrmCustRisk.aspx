<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmCustRisk.aspx.cs" Inherits="FrmCustRisk" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="portlet box blue" id="form_wizard_1">
                        <div class="portlet-title">
                            <div class="caption">
                                Customer Report
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
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label ">Select Risk Type</label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:DropDownList ID="Ddl_RiskType" runat="server" OnTextChanged="Ddl_RiskType_TextChanged">
                                                                <asp:ListItem Text="--Select Type--" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="Low Risk" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="Meduim Risk" Value="2"></asp:ListItem>
                                                                <asp:ListItem Text="High Risk" Value="3"></asp:ListItem>
                                                                <asp:ListItem Text="All Type" Value="4"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">
                                                        <div class="col-lg-12">
                                                          
                                                                    <asp:Button ID="Btn_Submit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="Btn_Submit_Click"/>
                                                                    <asp:Button ID="Btn_Exit" runat="server" Text="Exit" CssClass="btn btn-primary"/>
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

