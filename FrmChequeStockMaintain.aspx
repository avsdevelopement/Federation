<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmChequeStockMaintain.aspx.cs" Inherits="FrmChequeStockMaintain" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script type="text/javascript">
        function isvalidate() {

            var ProdCode = document.getElementById('<%=txtProdCode.ClientID%>').value;
            var NoOfLeave = document.getElementById('<%=txtnoLeaves.ClientID%>').value;

            if (ProdCode == "") {
                var message = 'Please Enter Product Code....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtProdCode.ClientID%>').focus();
                return false;
            }

            if (NoOfLeave == "") {
                var message = 'Please Enter Number Of Leaves....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtnoLeaves.ClientID%>').focus();
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
                        Cheque Stock Maintain
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
                                                    <label class="control-label col-md-2">Product Code <span class="required">*</span></label>
                                                    <div class="col-lg-2">
                                                        <asp:TextBox ID="txtProdCode" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" OnTextChanged="txtProdCode_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtProdName" CssClass="form-control" OnTextChanged="txtProdName_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                                        <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="txtProdName"
                                                                UseContextKey="true"
                                                                CompletionInterval="1"
                                                                CompletionSetCount="20"
                                                                MinimumPrefixLength="1"
                                                                EnableCaching="true"
                                                                ServicePath="~/WebServices/Contact.asmx"
                                                                ServiceMethod="GetGlName">
                                                            </asp:AutoCompleteExtender>
                                                    </div>
                                           </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">No Of Leaves <span class="required">*</span></label>
                                                <div class="col-lg-2">
                                                    <asp:TextBox ID="txtnoLeaves" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" />
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
                                        <asp:Button ID="btnAuthorize" runat="server" CssClass="btn blue" Text="Authorised" OnClick="btnAuthorised_Click" />
                                        <asp:Button ID="btnModify" runat="server" CssClass="btn blue" Text="MODIFY" OnClick="btnModify_Click"  />
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
                                        <asp:GridView ID="grdChequeStock" runat="server" AllowPaging="True" AlternatingRowStyle-CssClass="alt" CssClass="mGrid" AutoGenerateColumns="false"
                                            EditRowStyle-BackColor="#FFFF99" OnPageIndexChanging="grdChequeStock_PageIndexChanging" PagerStyle-CssClass="pgr" Width="100%">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SUB GLCODE" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="SUBGLCODE" runat="server" Text='<%# Eval("SUBGLCODE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="SUB GLNAME">
                                                    <ItemTemplate>
                                                        <asp:Label ID="SUBGLNAME" runat="server" Text='<%# Eval("SUBGLNAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="No of Page">
                                                    <ItemTemplate>
                                                        <asp:Label ID="NOOFLEAVES" runat="server" Text='<%# Eval("NOOFLEAVES") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="EFFECTDATE" runat="server" Text='<%# Eval("EFFECTDATE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Stage">
                                                    <ItemTemplate>
                                                        <asp:Label ID="STAGE" runat="server" Text='<%# Eval("STAGE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Select" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkSelect" runat="server" CommandArgument='<%#Eval("id")%>' CommandName="select" OnClick="lnkSelect_Click" class="glyphicon glyphicon-check"></asp:LinkButton>
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

