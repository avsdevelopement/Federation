<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="Frmforgetpwd.aspx.cs" Inherits="forgetpwd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript"> 
        function validate() {

            var loginid = document.getElementById('<%=txtloginId.ClientID%>').value;
            var createpwd =document.getElementById('<%=txtcreatenewpwd.ClientID%>').value;
            var confpwd = document.getElementById('<%=txtconfirmpwd.ClientID%>').value;
            var message = '';

            if (loginid =="") {
                //alert = ("please enter Login code....!!");
                message = 'Please Enter Login code....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtloginId.ClientID%>').focus();
                return false;
            }

            if (createpwd =="") {
                //alert = ("please enter new password....!!");
                message = 'Please Enter new password....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtcreatenewpwd.ClientID%>').focus();
                return false;
            }

            if (confpwd =="") {
               // alert = ("please enter confirmed password....!!");
                message = 'Please Enter confirmed password....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtconfirmpwd.ClientID%>').focus();
                return false;
            }

            if (createpwd != confpwd) {
               // alert("Please Enter Proper Password.........!!");
                message = 'Please Enter Proper Password....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                return false;
         }
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                   Forget Password 
                    </div>
                </div>
                <div class="portlet-body form">
                    <!--<form action="#" class="form-horizontal" id="submit_form" method="POST">-->
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-pane active" id="tab__blue">                                  
                                </div>
                                <div class="tab-content">                                  
                                     <div class="row" style="margin:7px 0 7px 0">
                                        <div class="col-md-12">
                                            <label class="control-label col-md-3">Login Id:</label>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtloginId" CssClass="form-control" placeholder="Enter the login Id" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-6">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin:7px 0 7px 0">
                                        <div class="col-md-12">
                                            <label class="control-label col-md-3">Create New Password:</label>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtcreatenewpwd" CssClass="form-control" TextMode="Password" placeholder="Enter New password" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-6">
                                            </div>
                                        </div>
                                    </div>                                                                                                        
                                    <div class="row" style="margin:7px 0 7px 0">
                                        <div class="col-md-12">
                                            <label class="control-label col-md-3">Confirm Password:</label>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtconfirmpwd" CssClass="form-control" TextMode="Password" placeholder="Confirm password" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-6">
                                            </div>
                                        </div>
                                    </div>                                                                                                                                                                                                                                                                                                                                       
                                    <%--<div class="form-actions">--%>
                                        <div class="row">
                                            <div class="col-md-offset-4 col-md-8">                                            
                                                <asp:Button ID="btnsubmit" runat="server" CssClass="btn blue" Text="Submit" OnClick="btnsubmit_Click" OnClientClick="javascript:return validate()"/>
                                                <%--<asp:Button ID="btnmodify" runat="server" CssClass="btn blue" Text="Modify"/>
                                                <asp:Button ID="btnsuspend" runat="server" CssClass="btn blue" Text="Suspend"/>
                                                <asp:Button ID="btnauthorize" runat="server" CssClass="btn blue" Text="Authirize"/>--%>
                                            </div>
                                        </div>
                                    <%--</div>--%>                                                               
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
</asp:Content>

