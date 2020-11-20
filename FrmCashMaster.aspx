<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CBSMaster.master" CodeFile="FrmCashMaster.aspx.cs" Inherits="CashMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        Cash Counter Management
                    </div>
                </div>
                <div class="portlet-body form">
                    <!--<form action="#" class="form-horizontal" id="submit_form" method="POST">-->
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">

                                <div class="tab-content">
                                    <div id="error">
                                        <div class="col-lg-11">
                                            <label class="control-label col-md-3">User Code : <span class="required">* </span></label>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtusercode" CssClass="form-control" MaxLength="8" runat="server" AutoPostBack="true" OnTextChanged="txtusercode_TextChanged"></asp:TextBox>
                                            </div>
                                            <div class="col-md-5">
                                                <asp:TextBox ID="txtusername" CssClass="form-control" runat="server" OnTextChanged="txtusername_TextChanged"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane active" id="tab1">
                                        <div class="row" style="margin: 7px 0 7px 0">
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-11">
                                                <label class="control-label col-md-3">Type : <span class="required">* </span></label>
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="ddltype" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddltype_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-5">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-11">
                                                <label class="control-label col-md-3">Cash Debit Limit : <span class="required">* </span></label>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtdebitlimit" CssClass="form-control" Maxlength="7" runat="server" AutoPostBack="true" onkeypress="javascript:return isNumber (event)" OnTextChanged="txtdebitlimit_TextChanged"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-11">
                                                <label class="control-label col-md-3">Cash Credit Limit : <span class="required">* </span></label>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtcreditlimit" CssClass="form-control" Maxlength="7" runat="server" AutoPostBack="true" onkeypress="javascript:return isNumber (event)" OnTextChanged="txtcreditlimit_TextChanged"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>



                                    </div>

                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9">
                                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn blue" Text="SUBMIT" OnClientClick="Javascript:return isvalidate();" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnModify" runat="server" CssClass="btn blue" Text="MODIFY" OnClientClick="Javascript:return isvalidate();" OnClick="btnModify_Click" />
                                        <asp:Button ID="btnDelete" runat="server" CssClass="btn blue" Text="DELETE" OnClick="btnDelete_Click" />
                                        <asp:Button ID="BTNAUTHO" runat="server" CssClass="btn blue" Text="Authorize" OnClick="BTNAUTHO_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--</form>-->
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-lg-12">
            <div class="table-scrollable">
                <table class="table table-striped table-bordered table-hover">
                    <thead>
                        <tr>
                            <th>
                                <asp:GridView ID="grdlimit" runat="server" AllowPaging="True"
                                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                    EditRowStyle-BackColor="#FFFF99"
                                    OnPageIndexChanging="grdlimit_PageIndexChanging"
                                    PagerStyle-CssClass="pgr" Width="100%" OnSelectedIndexChanged="grdlimit_SelectedIndexChanged">
                                    <Columns>

                                        <asp:TemplateField HeaderText="Select" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkSelect" runat="server" OnClick="lnkSelect_Click" Text="select" CommandArgument='<%#Eval("USERCODE")%>' CommandName="select"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="UserCode" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="LBUSERCODE" runat="server" Text='<%# Eval("USERCODE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="User Name" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lbusername" runat="server" Text='<%# Eval("USERNAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TYPE" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="LBTYPE" runat="server" Text='<%# Eval("TYPE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="DEBIT LIMIT" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="LBDEBIT" runat="server" Text='<%# Eval("CASHCREDITLIMIT") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="CREDIT LIMIT" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="LBCREDIT" runat="server" Text='<%# Eval("CASHCREDITLIMIT") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" OnClick="lnkDelete_Click" Text="Delete" CommandArgument='<%#Eval("USERCODE")%>' CommandName="select"></asp:LinkButton>
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
