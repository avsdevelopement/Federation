<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmUserMaster.aspx.cs" Inherits="UserMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function Validate() {
            var brcode = document.getElementById('<%=txtBranchCode.ClientID%>').value;
            var username = document.getElementById('<%=txtUserName.ClientID%>').value;
            var logincode = document.getElementById('<%=txtLoginCode.ClientID%>').value;
            var usergroup = document.getElementById('<%=ddusergroup.ClientID%>').value;
            var autolock = document.getElementById('<%=ddautolock.ClientID%>').value;
            var multibranch = document.getElementById('<%=ddMultibranchaccess.ClientID%>').value;
            var mobileno = document.getElementById('<%=txtmobile.ClientID%>').value;
            var branchcode = document.getElementById('<%=txtBranchCode.ClientID%>').value;
            var permission = document.getElementById('<%=txtpermission.ClientID%>').value;
            var message = '';

            if (brcode == "") {
                message = 'Please Enter Branch Code....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtBranchCode.ClientID%>').focus();
                return false;
            }
            if (username == "") {
                message = 'Please Enter username....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtUserName.ClientID%>').focus();
                return false;
            }
            if (logincode == "") {
                message = 'Please Enter logincode....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtLoginCode.ClientID%>').focus();
                return false;
            }
            if (usergroup == "0") {
                message = 'Please select an usergroup....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=ddusergroup.ClientID%>').focus();
                return false;
            }

            if (autolock == "0") {
                message = 'Please select an autolock....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=ddautolock.ClientID%>').focus();
                return false;
            }
            if (multibranch == "0") {
                message = 'Please select an multibranch....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=ddMultibranchaccess.ClientID%>').focus();
                return false;
            }

            if (mobileno == "") {
                message = 'Please Enter mobile no....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtmobile.ClientID%>').focus();
                return false;
            }

            if (mobileno.length < 10 || mobileno.length > 10) {
                message = 'Please Enter 10 Digits  Mobile No....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtmobile.ClientID%>').focus();
                return false;
            }

            if (permission == "") {
                message = 'Please Enter permission No....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtmobile.ClientID%>').focus();
                return false;
            }
        }
    </script>
    <script type="text/javascript">
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;

            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        User Master
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-pane active" id="tab__blue">
                                </div>
                                <div id="div_main" runat="server">
                                    <div class="tab-content">

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Branch Code <span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtBranchCode" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="DdlBranchName" Style="width: 61%; height: 33px;" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DdlBranchName_SelectedIndexChanged" CssClass="form-control" TabIndex="2">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-lg-4">
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">User Name <span class="required">*</span></label>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtUserName" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <label class="control-label col-md-2">User login code <span class="required">*</span></label>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtLoginCode" CssClass="form-control" runat="server" OnTextChanged="txtLoginCode_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">User Group <span class="required">*</span></label>
                                            <div class="col-md-3">
                                                <asp:DropDownList ID="ddusergroup" CssClass="form-control" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                            <label class="control-label col-md-2">Auto Lock <span class="required">*</span></label>
                                            <div class="col-md-3">
                                                <asp:DropDownList ID="ddautolock" CssClass="form-control" runat="server">
                                                    <asp:ListItem Text="Select" Value="0" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="N"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Multi Br Access <span class="required">*</span></label>
                                            <div class="col-md-3">
                                                <asp:DropDownList ID="ddMultibranchaccess" CssClass="form-control" runat="server">
                                                    <asp:ListItem Text="Select" Value="0" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="N"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <label class="control-label col-md-2">User Mobile No <span class="required">*</span></label>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtmobile" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" MaxLength="10"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Permission <span class="required">*</span></label>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtpermission" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <div class="col-md-6">
                                                <label class="control-label col-md-2">Cash </label>
                                            </div>
                                            <div class="col-md-6">
                                                <label class="control-label col-md-2">Transfer </label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin: 20px; border-bottom: 1px solid rgba(53, 152, 220, 0.55);"><strong></strong></div>
                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <div class="col-md-1">
                                                <asp:CheckBox ID="chkCashCr" OnCheckedChanged="chkCashCr_CheckedChanged" AutoPostBack="true" CssClass="control-label col-md-1" Text="Cr" runat="server" />
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtCSCredit" Enabled="false" placeholder="Credit Limit" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:CheckBox ID="chkCashDr" OnCheckedChanged="chkCashDr_CheckedChanged" AutoPostBack="true" CssClass="control-label col-md-2" Text="Dr" runat="server" />
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtCSDebit" Enabled="false" placeholder="Debit Limit" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:CheckBox ID="chkTrfCr" OnCheckedChanged="chkTrfCr_CheckedChanged" AutoPostBack="true" CssClass="control-label col-md-1" Text="Cr" runat="server" />
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txttrfCredit" Enabled="false" placeholder="Credit Limit" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:CheckBox ID="chkTrfDr" OnCheckedChanged="chkTrfDr_CheckedChanged" AutoPostBack="true" CssClass="control-label col-md-2" Text="Dr" runat="server" />
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txttrfDebit" Enabled="false" placeholder="Debit Limit" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                        </div>
                                    </div>

                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <div class="col-md-6">
                                                <label class="control-label col-md-2">RTGS </label>
                                            </div>
                                            <div class="col-md-6">
                                                <label class="control-label col-md-2">Inward </label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin: 20px; border-bottom: 1px solid rgba(53, 152, 220, 0.55);"><strong></strong></div>
                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <div class="col-md-1">
                                                <asp:CheckBox ID="chkRTGSCr" OnCheckedChanged="chkRTGSCr_CheckedChanged" AutoPostBack="true" CssClass="control-label col-md-1" Text="Cr" runat="server" />
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtrtgsCredit" Enabled="false" placeholder="Credit Limit" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:CheckBox ID="chkRTGSDr" OnCheckedChanged="chkRTGSDr_CheckedChanged" AutoPostBack="true" CssClass="control-label col-md-2" Text="Dr" runat="server" />
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtrtgsDebit" Enabled="false" placeholder="Debit Limit" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:CheckBox ID="chkInwardCr" OnCheckedChanged="chkInwardCr_CheckedChanged" AutoPostBack="true" CssClass="control-label col-md-1" Text="Cr" runat="server" />
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtIWCredit" Enabled="false" placeholder="Credit Limit" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:CheckBox ID="chkInwardDr" OnCheckedChanged="chkInwardDr_CheckedChanged" AutoPostBack="true" CssClass="control-label col-md-2" Text="Dr" runat="server" />
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtIWDebit" Enabled="false" placeholder="Debit Limit" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <div class="col-md-6">
                                                <label class="control-label col-md-2">ABB </label>
                                            </div>
                                            <div class="col-md-6">
                                                <label class="control-label col-md-2">Outward </label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin: 20px; border-bottom: 1px solid rgba(53, 152, 220, 0.55);"><strong></strong></div>
                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <div class="col-md-1">
                                                <asp:CheckBox ID="chkABBCr" OnCheckedChanged="chkABBCr_CheckedChanged" AutoPostBack="true" CssClass="control-label col-md-1" Text="Cr" runat="server" />
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtABBCredit" Enabled="false" placeholder="Credit Limit" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:CheckBox ID="chkABBDr" OnCheckedChanged="chkABBDr_CheckedChanged" AutoPostBack="true" CssClass="control-label col-md-2" Text="Dr" runat="server" />
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtABBDebit" Enabled="false" placeholder="Debit Limit" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:CheckBox ID="chkOWCr" OnCheckedChanged="chkOWCr_CheckedChanged" AutoPostBack="true" CssClass="control-label col-md-1" Text="Cr" runat="server" />
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtOWCredit" Enabled="false" placeholder="Credit Limit" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:CheckBox ID="chkOWDr" OnCheckedChanged="chkOWDr_CheckedChanged" AutoPostBack="true" CssClass="control-label col-md-2" Text="Dr" runat="server" />
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtOWDebit" Enabled="false" placeholder="Debit Limit" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <div class="col-md-6">
                                                <label class="control-label col-md-2">IBT </label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin: 20px; border-bottom: 1px solid rgba(53, 152, 220, 0.55);"><strong></strong></div>
                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <div class="col-md-1">
                                                <asp:CheckBox ID="chkIBTCr" OnCheckedChanged="chkIBTCr_CheckedChanged" AutoPostBack="true" CssClass="control-label col-md-1" Text="Cr" runat="server" />
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtIBTCredit" Enabled="false" placeholder="Credit Limit" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:CheckBox ID="chkIBTDr" OnCheckedChanged="chkIBTDr_CheckedChanged" AutoPostBack="true" CssClass="control-label col-md-2" Text="Dr" runat="server" />
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtIBTDebit" Enabled="false" placeholder="Debit Limit" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-offset-3 col-md-9">
                                            <asp:Button ID="btncreate" runat="server" CssClass="btn blue" Text="Create" OnClick="btncreate_Click" OnClientClick="javascript:return Validate();" />
                                            <asp:Button ID="btnmodify" runat="server" CssClass="btn blue" Text="Modify" OnClick="btnmodify_Click" OnClientClick="javascript:return Validate();" />
                                            <asp:Button ID="btnsuspend" runat="server" CssClass="btn blue" Text="Suspend" OnClick="btnsuspend_Click" />
                                            <asp:Button ID="btnauthorize" runat="server" CssClass="btn blue" Text="Authorize" OnClick="btnauthorize_Click" />
                                             <asp:Button ID="BtnReport" runat="server" CssClass="btn blue" Text="Report" OnClick= "BtnReport_Click" />
                                        </div>
                                    </div>
                                </div>
                                <div id="div_view" runat="server" visible="false">
                                           <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Permission No<span class="required">*</span></label>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="TxtPermNo" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="TxtPermNo_TextChanged"></asp:TextBox>
                                            </div>
                                             <label class="control-label col-md-2">Username <span class="required">*</span></label>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="TxtUname" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="TxtUname_TextChanged"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                    </div>
                    <div class="col-md-12">
                        <table class="table table-striped table-bordered table-hover" width="100%">
                            <thead>
                                <tr>
                                    <th>
                                        <asp:GridView ID="grduDetails" runat="server" AllowPaging="True" AlternatingRowStyle-CssClass="alt" CssClass="mGrid" AutoGenerateColumns="false"
                                            EditRowStyle-BackColor="#FFFF99" OnPageIndexChanging="grduDetails_PageIndexChanging" PagerStyle-CssClass="pgr" Width="100%">
                                            <Columns>

                                                <asp:TemplateField HeaderText="Select" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkSelect" runat="server" CommandArgument='<%#Eval("PERMISSIONNO")%>' CommandName="select" OnClick="lnkSelect_Click" class="glyphicon glyphicon-check"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Login Code" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LOGINCODE" runat="server" Text='<%# Eval("LOGINCODE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="User Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="USERNAME" runat="server" Text='<%# Eval("USERNAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                              <%--  <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="USERSTATUS" runat="server" Text='<%# Eval("USERSTATUS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>

                                                <asp:TemplateField HeaderText="Mobile No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="USERMOBILENO" runat="server" Text='<%# Eval("USERMOBILENO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Stage">
                                                    <ItemTemplate>
                                                        <asp:Label ID="STAGE" runat="server" Text='<%# Eval("UserStage") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                            <PagerStyle CssClass="pgr" />
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

