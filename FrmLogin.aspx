<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmLogin.aspx.cs" Inherits="FrmLogin" %>

<!DOCTYPE html>

<!--[if IE 8]> <html lang="en" class="ie8 no-js"> <![endif]-->
<!--[if IE 9]> <html lang="en" class="ie9 no-js"> <![endif]-->
<!--[if !IE]><!-->
<html lang="en">
<!--<![endif]-->
<!-- BEGIN HEAD -->
<head>
    <meta charset="utf-8" />
    <title>Avs In-so Tech | Login Form </title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1.0" name="viewport" />
    <meta http-equiv="Content-type" content="text/html; charset=utf-8">
    <meta content="" name="description" />
    <meta content="" name="author" />
    <!-- BEGIN GLOBAL MANDATORY STYLES -->
    <link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&subset=all" rel="stylesheet" type="text/css" />
    <link href="global/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="global/plugins/simple-line-icons/simple-line-icons.min.css" rel="stylesheet" type="text/css" />
    <link href="global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="global/plugins/uniform/css/uniform.default.css" rel="stylesheet" type="text/css" />
    <!-- END GLOBAL MANDATORY STYLES -->
    <!-- BEGIN PAGE LEVEL STYLES -->
    <link href="global/plugins/select2/select2.css" rel="stylesheet" type="text/css" />
    <link href="admin/pages/css/login-soft.css" rel="stylesheet" type="text/css" />
    <!-- END PAGE LEVEL SCRIPTS -->
    <!-- BEGIN THEME STYLES -->
    <link href="global/css/components.css" id="style_components" rel="stylesheet" type="text/css" />
    <link href="global/css/plugins.css" rel="stylesheet" type="text/css" />
    <link href="admin/layout/css/layout.css" rel="stylesheet" type="text/css" />
    <link id="style_color" href="admin/layout/css/themes/darkblue.css" rel="stylesheet" type="text/css" />
    <link href="admin/layout/css/custom.css" rel="stylesheet" type="text/css" />
    <!-- END THEME STYLES -->
    <link rel="shortcut icon" href="favicon.ico" />
    <link rel="stylesheet" href="global/css/style.css" media="screen" type="text/css" />
    <!-- New Design Files start-->
    <link href="includes/css/newstyle.css" rel="stylesheet" />
    <!-- New Design Files end -->
</head>
<!-- END HEAD -->
<!-- BEGIN BODY -->
        <script type="text/javascript">
        function preventBack() { window.history.forward(1); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
    </script>
<body class="login">
    <div class="login_wrapper">
    <!-- BEGIN LOGIN -->
    <div class="login_content">
        <!-- BEGIN LOGIN FORM -->
        <form id="Form1" class="login-form" runat="server" method="post" autocomplete="off">
            <h3 class="form-title">Login</h3>


            <div class="alert alert-danger display-hide">
                <button class="close" data-close="alert"></button>
                <span>Enter any username and password. </span>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <!--ie8, ie9 does not support html5 placeholder, so we just show field title for that-->
                        <label class="control-label visible-ie8 visible-ie9">Username</label>
                        <div class="input-icon">
                            <i class="fa fa-user"></i>
                            <input class="form-control placeholder-no-fix" type="text" id="Userid" runat="server" autocomplete="off" style="text-transform: uppercase" placeholder="Username" name="username" />
                        </div>
                    </div>

                </div>
                <div class="col-md-12">
                    <div class="form-group">
                        <label class="control-label visible-ie8 visible-ie9">Password</label>
                        <div class="input-icon">
                            <i class="fa fa-lock"></i>
                            <input class="form-control placeholder-no-fix" type="password" id="pass" runat="server" autocomplete="off" style="text-transform: uppercase" placeholder="Password" name="password" />
                        </div>
                    </div>

                </div>
            </div>
            <div class="row">
               
                <div class="col-md-12">
                    <div class="form-group text-center">
                        <asp:Button ID="Login" runat="server" Text="Login" class="btn btn2 sizehalf lightblue" OnClick="Login_Click" />
                        <%--<button class="btn  style3" value="Login" type="button" id="Login" runat="server" onserverclick="Login_Click"/>--%>
                    </div>
                </div>
                <div class="col-md-12">
                    <marquee direction="left" onmouseover="this.stop();" onmouseout="this.start();" >
                    <asp:Label ID="Lbl_Error" Text="" runat="server" style="color:#F00; font-size:13px;"></asp:Label>

                    </marquee>
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
                <asp:HiddenField ID="hdnAddress" runat="server" />

        </form>
        <!-- END LOGIN FORM -->
        <!-- BEGIN FORGOT PASSWORD FORM -->
        <form class="forget-form" action="index.html" method="post" style="display:none;">
            <h3>Forget Password ?</h3>
            <p>
                Enter your e-mail address below to reset your password.
            </p>
            <div class="form-group">
                <div class="input-icon">
                    <i class="fa fa-envelope"></i>
                    <input class="form-control placeholder-no-fix" type="text" autocomplete="off" placeholder="Email" name="email" />
                </div>
            </div>
            <div class="form-actions">
                <button type="button" id="back-btn" class="btn">
                    <i class="m-icon-swapleft"></i>Back
                </button>
                <button type="submit" class="btn blue pull-right">
                    Submit <i class="m-icon-swapright m-icon-white"></i>
                </button>
            </div>
        </form>
        <!-- END FORGOT PASSWORD FORM -->
        <!-- BEGIN REGISTRATION FORM -->
        <!-- END REGISTRATION FORM -->
    </div>
    <!-- END LOGIN -->
    <!-- BEGIN COPYRIGHT -->
    <div class="logincopyright"> 2016 &copy; AVS In-So Tech.</div>
        </div>
    <!-- END COPYRIGHT -->
    <!-- BEGIN JAVASCRIPTS(Load javascripts at bottom, this will reduce page load time) -->
    <!-- BEGIN CORE PLUGINS -->
    <!--[if lt IE 9]>
<script src="global/plugins/respond.min.js"></script>
<script src="global/plugins/excanvas.min.js"></script> 
<![endif]-->
    <script src="global/plugins/jquery.min.js" type="text/javascript"></script>
    <script src="global/plugins/jquery-migrate.min.js" type="text/javascript"></script>
    <script src="global/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="global/plugins/jquery.blockui.min.js" type="text/javascript"></script>
    <script src="global/plugins/uniform/jquery.uniform.min.js" type="text/javascript"></script>
    <script src="global/plugins/jquery.cokie.min.js" type="text/javascript"></script>
    <!-- END CORE PLUGINS -->
    <!-- BEGIN PAGE LEVEL PLUGINS -->
    <script src="global/plugins/jquery-validation/js/jquery.validate.min.js" type="text/javascript"></script>
    <script src="global/plugins/backstretch/jquery.backstretch.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="global/plugins/select2/select2.min.js"></script>
    <!-- END PAGE LEVEL PLUGINS -->
    <!-- BEGIN PAGE LEVEL SCRIPTS -->
    <script src="global/scripts/metronic.js" type="text/javascript"></script>
    <script src="admin/layout/scripts/layout.js" type="text/javascript"></script>
    <script src="admin/layout/scripts/demo.js" type="text/javascript"></script>
    <script src="admin/pages/scripts/login-soft.js" type="text/javascript"></script>
    <!-- END PAGE LEVEL SCRIPTS -->
    <!--<script>
        jQuery(document).ready(function () {
            Metronic.init(); // init metronic core components
            Layout.init(); // init current layout
            Login.init();
            Demo.init();
            // init background slide images
            $.backstretch([
             "admin/pages/media/bg/6.jpg",
            ], {
                fade: 1000,
                duration: 8000
            }
         );
        });
    </script>-->
    <!-- END JAVASCRIPTS -->
</body>
<!-- END BODY -->
</html>
