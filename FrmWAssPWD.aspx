<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmWAssPWD.aspx.cs" Inherits="FrmWAssPWD" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
</asp:Content>
<Asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="Div1">
                <div class="portlet-title">
                    <div class="caption">
                        Assign Password
                       <%-- <asp:Label ID="LblTop" runat="server" Text="Label" CssClass="caption"></asp:Label>--%>
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div id="DIV_IP" class="row" style="margin: 7px 0 7px 0; margin-top: 20px;" runat="server">
                                        <div class="tab-pane active" id="tab__blue">

                                            <div class="row" style="margin: 7px 0 7px 0; margin-top: 20px;">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Login Code <span class="required">*</span></label>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtlogincode" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Enter Password <span class="required">*</span></label>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtEnterpwd" CssClass="form-control" TextMode="Password"  placeholder="Enter Password" runat="server" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Confirm Password <span class="required">*</span></label>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtconfirmpwd" CssClass="form-control" TextMode="Password"  placeholder="Confirm Password" runat="server" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12" >
                                                <div class="col-md-2">
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:Button ID="btncreate" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="btncreate_Click" OnClientClick="Javascript:return CheckPass();" />
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

</Asp:Content>