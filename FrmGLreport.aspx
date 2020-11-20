<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmGLreport.aspx.cs" Inherits="FrmGLreport" %>

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
                              Genral Ledger Entry
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
                                                
                                                
                                                
                                                <div class="row" style="margin: 7px 0 7px 0; margin-top:14px;">
                                                    <div class="col-lg-12">

                                                      <div class="col-md-2">
                                                        <label class="control-label">GL Type</label>
                                                       </div>
                                                        <div class="col-md-4">
                                                           <asp:DropDownList ID="DropDownList3" runat="server" CssClass="form-control">
                                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="Miss" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="Mrs" Value="2"></asp:ListItem>
                                                                <asp:ListItem Text="Mr" Value="3"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                       <div class="col-md-2">
                                                        <label class="control-label">GL Code</label>
                                                       </div>
                                                        <div class="col-md-4">
                                                         <asp:TextBox ID="TextBox12" CssClass="form-control" runat="server"  placeholder="GL Code" AutoPostBack="true"></asp:TextBox>
                                                        </div>
                                                      </div>
                                                </div>
                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">

                                                      <div class="col-md-2">
                                                        <label class="control-label">GL Name</label>
                                                       </div>
                                                        <div class="col-md-4">
                                                         <asp:TextBox ID="TextBox1" CssClass="form-control" runat="server" placeholder="GL Name" AutoPostBack="true"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-2">
                                                        <label class="control-label">Sub Account</label>
                                                       </div>
                                                        <div class="col-md-4">
                                                         <asp:DropDownList ID="DropDownList4" runat="server" CssClass="form-control">
                                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="Miss" Value="Yes"></asp:ListItem>
                                                                <asp:ListItem Text="Mrs" Value="No"></asp:ListItem>
                                                                
                                                            </asp:DropDownList>
                                                        </div>
                                                      </div>
                                                </div>
                                                
                                                
                                               
                                                 <div class="row" style="margin: 7px 0 7px 0; margin-bottom:12px;">
                                                    <div class="col-lg-12">
                                                      <div class="col-md-2">
                                                        <asp:Button ID="Submit" runat="server" Text="Submit" CssClass="btn btn-primary"/>
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


