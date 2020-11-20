<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmSetPWD.aspx.cs" Inherits="FrmSetPWD" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript">
        function CheckPass() {
            var TxtPass = document.getElementById('<%=txtEnterpwd.ClientID%>').value;
            var TXtCPass = document.getElementById('<%=txtconfirmpwd.ClientID%>').value;
            var logincode = document.getElementById('<%=txtlogincode.ClientID%>').value;
            var message = '';

            if (logincode == "") {
                // alert("please enter your login code.....!!!");
                message = 'Please Enter your login code....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtlogincode.ClientID%>').focus();
                return false;

            }

            if (TxtPass == "") {
                // alert("please enter new password....!!");
                message = 'Please Enter new password....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtEnterpwd.ClientID%>').focus();
                return false;
            }

            if (TXtCPass == "") {
                //alert("please enter confirmed password....!!");
                message = 'Please Enter confirmed password....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtconfirmpwd.ClientID%>').focus();
                return false;
            }

            if (TxtPass != TXtCPass) {
                //  alert("Please enter Proper Password.........!!");
                message = 'Please Enter Proper Password....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                return false;
            }
        }
    </script>
    <style type="text/css">
        .blue {
            height: 26px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="row">
                <div class="col-md-12">
                    <div class="portlet box blue" id="form_wizard_1">
                        <div class="portlet-title">
                            <div class="caption">
                                Assign Password
                            </div>
                        </div>
                        <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-12">
                                <label class="control-label col-md-2">Please enter login code:<span class="required"></span></label>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtlogincode" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                </div>
                                <div class="col-md-7">
                                </div>
                            </div>
                        </div>

                        <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-12">
                                <label class="control-label col-md-2">Enter password:<span class="required"></span></label>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtEnterpwd" CssClass="form-control" TextMode="Password"  placeholder="Enter Password" runat="server" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                </div>
                                <div class="col-md-7">
                                </div>
                            </div>
                        </div>

                        <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-12">
                                <label class="control-label col-md-2">Confirm Password:<span class="required"></span></label>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtconfirmpwd" CssClass="form-control" TextMode="Password"  placeholder="Confirm Password" runat="server" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                </div>
                                <div class="col-md-7">
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-offset-3 col-md-9">
                                <asp:Button ID="btncreate" runat="server" CssClass="btn blue" Text="Submit" OnClick="btncreate_Click" OnClientClick="Javascript:return CheckPass();" />                              
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
