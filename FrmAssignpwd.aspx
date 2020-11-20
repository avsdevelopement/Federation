<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAssignpwd.aspx.cs" Inherits="Assignpwd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function isvalidate() {

            var logincode = document.getElementById('<%=txtlogincode.ClientID%>').value;
            var enterpwd = document.getElementById('<%=txtenterpwd.ClientID%>').value;
            var confpwd = document.getElementById('<%=txtconfirmpwd.ClientID%>').value;
            var message = '';

            if (logincode == "") {
                message = 'Please Enter login code....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtlogincode.ClientID%>').focus();
                return false;
            }

            if (enterpwd == "") {
                message = 'Please Enter password....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtlogincode.ClientID%>').focus();
                return false;
            }

            if (confpwd == "") {
                message = 'Please Enter confirm apwword....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtlogincode.ClientID%>').focus();
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
                     Assign Password
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
                                            <label class="control-label col-md-3">Please enter the login code:</label>
                                            <div class="col-md-3">
                                               <asp:TextBox ID="txtlogincode" CssClass="form-control" placeholder="enter the login code" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-6">
                                            </div>
                                        </div>
                                    </div>
                                                                       
                                    <div class="row" style="margin:7px 0 7px 0">
                                        <div class="col-md-12">
                                            <label class="control-label col-md-3"> Enter Password:</label>
                                            <div class="col-md-3">
                                               <asp:TextBox ID="txtenterpwd" CssClass="form-control" placeholder="enter password" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-6">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin:7px 0 7px 0">
                                        <div class="col-md-12">
                                            <label class="control-label col-md-3"> Confirm Password:</label>
                                            <div class="col-md-3">
                                               <asp:TextBox ID="txtconfirmpwd" CssClass="form-control" placeholder="Confirm password" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-6">
                                            </div>
                                        </div>
                                    </div>    
                                                                                                                                                                                                                                                                                                                                    
                                    <%--<div class="form-actions">--%>
                                        <div class="row">
                                            <div class="col-md-offset-4 col-md-8">                                            
                                                <asp:Button ID="btnsubmit" runat="server" CssClass="btn blue" Text="Submit" OnClientClick="Javascript:return isvalidate();"/>
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

