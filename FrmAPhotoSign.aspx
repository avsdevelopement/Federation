<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmAPhotoSign.aspx.cs" Inherits="FrmAPhotoSign" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
        <div>
            <div class="row" style="width: 65%">
                <div class="col-md-12">
                    <div class="portlet box blue" id="form_wizard_1">
                        <div class="portlet-title">
                            <div class="caption">
                                Photo & Signature
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
                                                <div class="row">
                                                    <div class="col-md-7">
                                                        <div class="col-md-12">
                                                            <br />
                                                            <br />
                                                            <div class="col-md-6" style="height: 40px;">
                                                                <label class="control-label ">Customer NO.</label>
                                                            </div>

                                                            <div class="col-md-6" style="height: 51px;">
                                                                <asp:TextBox ID="Txtcustno" CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                                                            </div>
                                                            <br />
                                                        </div>
                                                        <div class="col-md-12">
                                                            <br />
                                                            <div class="col-md-6" style="height: 40px;">
                                                                <label class="control-label ">Customer Name</label>
                                                            </div>
                                                            <div class="col-md-6" style="height: 51px;">
                                                                <asp:TextBox ID="Txtcustname" CssClass="form-control" runat="server" style="text-transform:uppercase" AutoPostBack="true"></asp:TextBox>
                                                            </div>
                                                        </div>

                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="col-md-3" style="text-align: left">
                                                            <asp:Image ID="IMGSIG" runat="server" Height="150px" Width="150px" />
                                                        </div>
                                                        <div class="col-md-3 pull-right">
                                                            <asp:Image ID="IMGPH" runat="server" Height="150px" Width="150px" />
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <br />
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
    </form>
</body>
</html>
