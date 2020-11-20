<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmCancelConfirm.aspx.cs" Inherits="FrmCancelConfirm" %>

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
    <!-- END GLOBAL MANDATORY STYLES -->
</head>
<body>
    <form id="form1" runat="server">
    <div class="row" style="margin:10px 10px 10px 10px">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        Cancel Voucher
                    </div>
                </div>
                <div class="portlet-body form">
                    <!--<form action="#" class="form-horizontal" id="submit_form" method="POST">-->
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">

                                <div class="tab-content">                                                                       
                                    <div class="tab-pane active" id="tab1">
                                        <div class="row" style="margin: 7px 0px 7px 0px">
                                            <div class="col-md-12">
                                                <div class="col-md-2">
                                                    <asp:Label ID="lblVoucherNo" runat="server" Text="Voucher No:"></asp:Label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtVoucherNo" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>                                              
                                                </div>
                                                <div class="col-md-2" >
                                                    <asp:Label ID="lblPrdType" runat="server" Text="Product Type:"></asp:Label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtPrdType" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:Label ID="lblAccNo" runat="server" Text="Account No :"></asp:Label>
                                                </div>
                                                <div class="col-md-2" >
                                                    <asp:TextBox ID="TxtAccNo" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
                                                </div> 
                                                <div class="col-md-2">
                                                    <asp:Label ID="lblInsNo" runat="server" Text="Instrument No:"></asp:Label>
                                                </div>
                                                 <div class="col-md-2" >
                                                    <asp:TextBox ID="TxtInsNo" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
                                                </div>   
                                                <div class="col-md-2">
                                                    <asp:Label ID="lblInsDate" runat="server" Text="Instrument date :"></asp:Label>
                                                </div>
                                                <div class="col-md-2" >
                                                    <asp:TextBox ID="TxtInsDate" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
                                                </div>                                             
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #fa4e0d"></strong></div>
                                            </div>
                                        </div>                                      

                                        
                                                                  
                                                                               
                                        <div class="row" style="margin:7px 0 7px 0">
                                            <div class="col-lg-11">
                                            <label class="control-label col-md-3">Cust Name : <span class="required">* </span></label>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="TxtCustName" CssClass="form-control" runat="server" Enabled="False"></asp:TextBox>
                                            </div>
                                                <label class="control-label col-md-2">Amount : <span class="required"></span></label>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="TxtAmount" CssClass="form-control" runat="server" Enabled="False"></asp:TextBox>
                                            </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin:7px 0 7px 0">
                                            <div class="col-lg-11">
                                            
                                            <label class="control-label col-md-3">Naration : <span class="required">* </span></label>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="TxtNarration" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                            </div>

                                            <label class="control-label col-md-2">Maker : <span class="required">* </span></label>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="TxtMaker" CssClass="form-control" runat="server" Enabled="False"></asp:TextBox>
                                            </div>
                                            </div>
                                        </div>
                                                                                 
                                    </div>

                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9">
                                        <asp:Button ID="btnDelete" runat="server" CssClass="btn blue" Text="Confirm Delete" OnClick="btnDelete_Click"/>
                                        <asp:Button ID="Exit" runat="server" CssClass="btn btn-success" Text="Exit" OnClick="Exit_Click"/>
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
