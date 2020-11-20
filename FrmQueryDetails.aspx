<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="FrmQueryDetails.aspx.cs" Inherits="FrmQueryDetails" %>

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
</head>

      
<body>
    <form id="form1" runat="server">
    <div class="col-md-12">
        <div class="portlet box green" id="Div1">
            <div class="portlet-title">
                <div class="caption">
                   Issue Details
                </div>
            </div>
            <div class="portlet-body form">
                <div class="form-horizontal">
                    <div class="form-wizard">
                        <div class="form-body">
                            <div class="tab-content">
                                <div class="tab-pane active" id="tab1">
                                    <div class="row" style="margin-bottom: 10px;">
                                      
                                        <div class="row" style="margin: 10px;"><strong></strong></div>
                                       <div class="row" style="margin: 7px 0 7px 0">
        <div class="col-lg-12" style="height: 50%">
            <div class="table-scrollable" style="height:auto;overflow-y: scroll; padding-bottom: 10px;">
                <table class="table table-striped table-bordered table-hover" width="100%">
                    <thead>
                        <b>Pending Work</b>
                        <tr>
                            <th>
                                <asp:GridView ID="grdPending" runat="server"
                                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                    EditRowStyle-BackColor="#FFFF99"
                                    PagerStyle-CssClass="pgr" Width="100%" OnSelectedIndexChanged="grdPending_SelectedIndexChanged">
                                    <Columns>
                                          <asp:TemplateField HeaderText="SRNO" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="serial" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="SRNO" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="SRNO" runat="server" Text='<%# Eval("SRNO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="MODULE RQ" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="MODULERQ" runat="server" Text='<%# Eval("MODULERQ") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="DESCRIPTION">
                                            <ItemTemplate>
                                                <asp:Label ID="QUERYDESC" runat="server" Text='<%# Eval("QUERYDESC") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="LOG DATE">
                                            <ItemTemplate>
                                                <asp:Label ID="ISSUEDATE" runat="server" Text='<%# Eval("ISSUEDATE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                    </Columns>
                                   
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
                                          <div class="row" style="margin: 7px 0 7px 0">
        <div class="col-lg-12" style="height: 50%">
            <div class="table-scrollable" style="height:auto;  overflow-y: scroll; padding-bottom: 10px;">
                <table class="table table-striped table-bordered table-hover" width="100%">
                    <thead>
                        <b>Solved Work</b>
                        <tr>
                            <th>
                                <asp:GridView ID="grdSolved" runat="server"
                                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                    EditRowStyle-BackColor="#FFFF99"
                                    PagerStyle-CssClass="pgr" Width="100%" OnSelectedIndexChanged="grdSolved_SelectedIndexChanged">
                                    <Columns>
                                         <asp:TemplateField HeaderText="SRNO" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="serial" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="SRNO" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="SRNO" runat="server" Text='<%# Eval("SRNO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="MODULE RQ" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="MODULERQ" runat="server" Text='<%# Eval("MODULERQ") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="DESCRIPTION">
                                            <ItemTemplate>
                                                <asp:Label ID="QUERYDESC" runat="server" Text='<%# Eval("QUERYDESC") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="LOG DATE">
                                            <ItemTemplate>
                                                <asp:Label ID="ISSUEDATE" runat="server" Text='<%# Eval("ISSUEDATE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="ASSIGN DATE">
                                            <ItemTemplate>
                                                <asp:Label ID="ASSIGNDATE" runat="server" Text='<%# Eval("currenttime") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="SOLVED DATE">
                                            <ItemTemplate>
                                                <asp:Label ID="SOLVEDDATE" runat="server" Text='<%# Eval("SYSTEMDATE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                   
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
                            <div class="row">
                                 
                                    <div class="col-md-6">
                                        
                                        <asp:Button ID="Btn_Exit" runat="server" Text="Exit" CssClass="btn btn-primary" OnClick="Btn_Exit_Click"/>
                                    </div>
                                  
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        </form>
</body>
    </html>


