<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmDepositeGLCreation.aspx.cs" Inherits="FrmDepositeGLCreation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function isvalidate() {

            var DepCode, DepType, ReportName, PayBal, bal, amt;
            DepCode = document.getElementById('<%=txtDepCode.ClientID%>').value;
            DepType = document.getElementById('<%=txtDepType.ClientID%>').value;
            ReportName = document.getElementById('<%=txtRepName.ClientID%>').value;
            var message = '';

            if (DepCode == "") {
                message = 'Please Enter Deposite Code....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtDepCode.ClientID%>').focus();
                return false;
            }

            if (DepType == "") {
                message = 'Please Enter Deposite Type....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtDepType.ClientID%>').focus();
                return false;
            }

            if (ReportName == "") {
                message = 'Please Enter Report Name....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtRepName.ClientID%>').focus();
                return false;
            }
        }
    </script>
     <script>
         function isNumber(evt) {
             evt = (evt) ? evt : window.event;
             var charCode = (evt.which) ? evt.which : evt.keyCode;
             if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                 return false;
             }
             return true;
         }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        Deposite GL Creation
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div id="error">
                                    </div>
                                    <div class="tab-pane active" id="tab1">
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-11">
                                                <label class="control-label col-md-3">Deposite Code : <span class="required">* </span></label>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtDepCode" CssClass="form-control" OnTextChanged="txtDepCode_TextChanged" AutoPostBack="true" runat="server" Enabled="false"/>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-11">
                                                <label class="control-label col-md-3">Deposite Type : <span class="required">* </span></label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtDepType" CssClass="form-control" runat="server" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-11">
                                                <label class="control-label col-md-3">Group/Category : <span class="required">* </span></label>
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="ddlCategory" CssClass="form-control" runat="server"> </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-11">
                                                <label class="control-label col-md-3">Report Name : <span class="required">* </span></label>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtRepName" CssClass="form-control"  runat="server" />
                                                </div>
                                                <div class="col-md-3">
                                                    <span class="required">Enter Short Name</span>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-11">
                                                <label class="control-label col-md-3">Status : <span class="required">* </span></label>
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="ddlStatus" CssClass="form-control" runat="server">
                                                        <asp:ListItem Text="-Select-"/>
                                                        <asp:ListItem Text="Active" Value="1"/>
                                                        <asp:ListItem Text="Deactive" value="3"/>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                    </div>

                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9">
                                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn blue" Text="SUBMIT" OnClick="btnSubmit_Click" OnClientClick="Javascript:return isvalidate();" />
                                        <asp:Button ID="btnModify" runat="server" CssClass="btn blue" Text="UPDATE" OnClick="btnModify_Click" OnClientClick="Javascript:return isvalidate();" />
                                        <asp:Button ID="btnDelete" runat="server" CssClass="btn blue" Text="DELETE" OnClick="btnDelete_Click"  />
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
                                        <asp:GridView ID="grdDepositGL" runat="server" AllowPaging="True" AlternatingRowStyle-CssClass="alt" CssClass="mGrid"
                                            EditRowStyle-BackColor="#FFFF99" OnPageIndexChanging="grdDepositGL_PageIndexChanging" PagerStyle-CssClass="pgr" Width="100%">
                                            <%--<Columns>

                                                <asp:TemplateField HeaderText="Glcode" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="gid" runat="server" Text='<%# Eval("") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Subglcode">
                                                    <ItemTemplate>
                                                        <asp:Label ID="subgl" runat="server" Text='<%# Eval("") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Gl Group" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="gGrp" runat="server" Text='<%# Eval("") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="GL Name" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="glname" runat="server" Text='<%# Eval("") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Opening Balance" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="bal" runat="server" Text='<%# Eval("") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Type" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="CRDR" runat="server" Text='<%# Eval("") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>--%>
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
                    <!--</form>-->
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
    </div>
</asp:Content>

