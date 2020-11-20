<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmCashPayAuthDo.aspx.cs" Inherits="FrmCashPayAuthDo" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>AVS In-So-Tech</title>
     <!-- BEGIN GLOBAL MANDATORY STYLES -->
    <link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&subset=all" rel="stylesheet" type="text/css">
    <link href="global/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css">
    <link href="global/plugins/simple-line-icons/simple-line-icons.min.css" rel="stylesheet" type="text/css">
    <link href="global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css">
    <link href="global/plugins/uniform/css/uniform.default.css" rel="stylesheet" type="text/css">
    <link href="global/plugins/bootstrap-switch/css/bootstrap-switch.min.css" rel="stylesheet" type="text/css" />
    <link href="global/css/components-rounded.css" rel="stylesheet"  type="text/css" />
    <!-- END GLOBAL MANDATORY STYLES -->    
</head>
<body>   
    <form id="form1" runat="server">    
        <div class="row" style="margin:10px 10px 10px 10px">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        Cash Payment Authorization
                    </div>
                </div>
                <div class="portlet-body form">
                    <!--<form action="#" class="form-horizontal" id="submit_form" method="POST">-->
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">

                                <div class="tab-content">                                    
                                    <div id="error">
                                    </div>
                                    <div class="tab-pane active" id="tab1">
                                        <div class="row" style="margin:7px 0 7px 0">
                                            <div class="col-lg-11">                                                
                                                <label class="control-label col-md-3">Entry Date : <span class="required">* </span></label>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtEntrydate" CssClass="form-control" runat="server" Enabled="False"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-2">Set No : <span class="required"  Visible="false"></span></label>
                                                <div class="col-md-3" Visible="false">
                                                    <asp:TextBox ID="txtsetno" CssClass="form-control" runat="server" ReadOnly="True"></asp:TextBox>
                                                    <%--<asp:TextBox ID="TextBox1" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtsetno_TextChanged" ReadOnly="True"></asp:TextBox>--%>
                                                </div>
                                            </div>
                                        </div>

                                         <div class="row" style="margin:7px 0 7px 0">
                                            <div class="col-lg-11">
                                                <label class="control-label col-md-3">Account Type : <span class="required">* </span></label>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtProcode" CssClass="form-control" runat="server" ReadOnly="True" ></asp:TextBox>
                                                    <%--<asp:TextBox ID="TextBox1" CssClass="form-control" runat="server"  AutoPostBack="true" OnTextChanged="TxtProcode_TextChanged" ></asp:TextBox>--%>
                                                </div>                                                
                                                <div class="col-md-5">
                                                   <asp:TextBox ID="TxtProName" CssClass="form-control" runat="server" Enabled="False"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin:7px 0 7px 0">
                                            <div class="col-lg-11">
                                                <label class="control-label col-md-3">Account No : <span class="required">* </span></label>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtAccNo" CssClass="form-control" runat="server" Enabled="False"></asp:TextBox>                                                
                                                    <%--<asp:TextBox ID="TextBox1" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="TxtAccNo_TextChanged"></asp:TextBox>--%>                                                
                                                </div>                                                
                                                <div class="col-md-5">
                                                    <asp:TextBox ID="TxtAccName" CssClass="form-control" runat="server" Enabled="False" ></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                                                               
                                        <div class="row" style="margin:7px 0 7px 0">
                                            <div class="col-lg-11">
                                            <label class="control-label col-md-3">Naration : <span class="required">* </span></label>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtnaration" CssClass="form-control" runat="server" Text="To Cash" Enabled="False"></asp:TextBox>
                                            </div>
                                                <label class="control-label col-md-2">Naration 2 : <span class="required"></span></label>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtnaration1" CssClass="form-control" runat="server" TabIndex="7" Enabled="False"></asp:TextBox>
                                            </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin:7px 0 7px 0">
                                            <div class="col-lg-11">
                                            
                                            <label class="control-label col-md-3">Balance : <span class="required">* </span></label>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtBalance" CssClass="form-control" runat="server" TabIndex="-1" ReadOnly="true"></asp:TextBox>
                                            </div>

                                            <label class="control-label col-md-2">Amount : <span class="required">* </span></label>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtamountt" CssClass="form-control" runat="server"  TabIndex="8" Enabled="False"></asp:TextBox>
                                            </div>
                                            </div>
                                        </div>
                                                                                 
                                    </div>

                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9">
                                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn blue" Text="Authorize" OnClick="btnSubmit_Click" OnClientClick="Javascript:return isvalidate();"/>
                                        <%--<asp:Button ID="Button1" runat="server" CssClass="btn blue" Text="Authorize" OnClick="btnSubmit_Click"/>--%>
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
    </form>
    


</body>
</html>
