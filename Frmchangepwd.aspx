<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="Frmchangepwd.aspx.cs" Inherits="changepwd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function CheckPass() {
            var logincode = document.getElementById('<%=txtlogincode.ClientID%>').value;
            var oldpwd = document.getElementById('<%=txtoldpwd.ClientID%>').value;
            var TxtPass = document.getElementById('<%=txtnewpwd.ClientID%>').value;
            var TXtCPass = document.getElementById('<%=txtconfirmpwd.ClientID%>').value;
            var message = '';

            if (logincode == "") {
                message = 'Please Enter login code....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtlogincode.ClientID%>').focus();
                return false;
            }


            if (oldpwd == "") {
                message = 'Please Enter old password....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtoldpwd.ClientID%>').focus();
                return false;
            }


            if (TxtPass == "") {
                message = 'Please Enter new password....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtnewpwd.ClientID%>').focus();                            
               return false;
            }


            if (TXtCPass == "") {
                message = 'Please Enter confirmed password....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtconfirmpwd.ClientID%>').focus();               
                return false;
            }


            if (TxtPass != TXtCPass) {
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
                     Change Password
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-pane active" id="tab__blue">                                  
                                </div>
                                <div class="tab-content">                               
                                     <div class="row" style="margin:7px 0 7px 0" id="divLogin" runat="server">
                                        <div class="col-md-12">
                                            <label class="control-label col-md-3">Login Code <span class="required">* </span></label>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtlogincode" CssClass="form-control" placeholder="enter the login code" runat="server" AutoPostBack="true" OnTextChanged="txtlogincode_TextChanged"></asp:TextBox>
                                            </div>
                                             <div class="col-md-3">
                                               <asp:DropDownList ID="ddlUname" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlUname_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-6">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin:7px 0 7px 0">
                                        <div class="col-md-12">
                                            <label class="control-label col-md-3">Old Password <span class="required">* </span></label>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtoldpwd" CssClass="form-control" TextMode="Password"  placeholder="Enter Old password" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-6">
                                            </div>
                                        </div>
                                    </div>
                                                                       
                                    <div class="row" style="margin:7px 0 7px 0">
                                        <div class="col-md-12">
                                            <label class="control-label col-md-3">New Password <span class="required">* </span></label>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtnewpwd" CssClass="form-control" TextMode="Password"  placeholder="Enter New password" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-6">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin:7px 0 7px 0">
                                        <div class="col-md-12">
                                            <label class="control-label col-md-3">Confirm Password <span class="required">* </span></label>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtconfirmpwd" CssClass="form-control" TextMode="Password"  placeholder="Confirm password" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-6">
                                            </div>
                                        </div>
                                    </div>    
                                                                                                                                                                                                                                                                                                                                    
                                    <div class="row" style="margin:7px 0 7px 0">
                                        <div class="col-md-12">
                                            <div class="col-md-3">
                                            </div>
                                            <div class="col-md-3">                                         
                                                <asp:Button ID="btnsubmit" runat="server" CssClass="btn blue" Text="Submit" OnClick="btnsubmit_Click" OnClientClick="javascript:return CheckPass();"/>
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
</asp:Content>

