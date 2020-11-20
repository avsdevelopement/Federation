<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/CBSMaster.master" CodeFile="FrmAccReport.aspx.cs" Inherits="FrmAccReport" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function isvalidate() {

            var TxtFBRCD = document.getElementById('<%=TxtFBRCD.ClientID%>').value;
            var TxtTBRCD = document.getElementById('<%=TxtTBRCD.ClientID%>').value;
            var message = '';


            if (TxtFBRCD == "") {
                message = 'Please Enter From branch ....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=TxtFBRCD.ClientID %>').focus();
                return false;
            }
            if (TxtTBRCD == "") {
                message = 'Please Enter to Branch ....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=TxtTBRCD.ClientID %>').focus();
                return false;
            }

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        Account Open Close
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
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">From Branch <span class="required">*</span></label>
                                                <div class="col-lg-3">
                                                    <asp:TextBox ID="TxtFBRCD" runat="server" Placeholder="From Branch"  CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-2">To Branch </label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="TxtTBRCD" runat="server" Placeholder="To Branch" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">From Date <span class="required">*</span></label>
                                                <div class="col-lg-3">
                                                    <asp:TextBox ID="txtFdate" runat="server" Placeholder=" from date" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-2">To Date </label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtTdate" runat="server" Placeholder=" To Date" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                         <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Product Code <span class="required">*</span></label>
                                                <div class="col-lg-3">
                                                    <asp:TextBox ID="txtpcode" runat="server" Placeholder="Product Code" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-2">Product Name </label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtpname" runat="server" Placeholder="Product Name" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                           <div class="col-lg-12">
                                        <label class="control-label col-md-2"></label>
                                        <div class="col-md-10">
                                            <asp:RadioButtonList ID="rbnacc" OnSelectedIndexChanged= "rbnacc_SelectedIndexChanged" AutoPostBack="true" RepeatDirection="Horizontal" runat="server" Width="400px" TabIndex="1">
                                                <asp:ListItem Text="Open Account" Selected="True" Value="O" />
                                                <asp:ListItem Text="Close Account" Value="C" />
                                            </asp:RadioButtonList>

                                        </div>
                                    </div>          
                                    </div>
                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-4">
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick= "btnSubmit_Click" OnClientClick="Javascript:return isvalidate();" />
                                    </div>
                                    <div class="col-lg-2">
                                        <asp:Button ID="btnexit" runat="server" Text="Exit" CssClass="btn btn-success" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                    </div>
                    <div id="DivUserLog" class="row" runat="server" visible="false">
                        <div class="col-lg-12">
                            <div class="table-scrollable">
                                <table class="table table-striped table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th>
                                                <asp:GridView ID="GrdDayOpen" runat="server" AllowPaging="True" AlternatingRowStyle-CssClass="alt"
                                                    AutoGenerateColumns="true" EditRowStyle-BackColor="#FFFF99" PageIndex="10" PageSize="25" PagerStyle-CssClass="pgr" Width="100%">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Select" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkSelect" runat="server"  CommandName="select" OnClick= "lnkSelect_Click" class="glyphicon glyphicon-check"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
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
        </div>
        <div id="alertModal" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <center><h4 class="modal-title" style="color:#ff0000">AVS Company</h4></center>
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

