<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CBSMaster.master" CodeFile="FrmBranch.aspx.cs" Inherits="FrmBranch" %>

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
                        Branch
                    </div>
                </div>
                <div class="portlet-body form">
                    <!--<form action="#" class="form-horizontal" id="submit_form" method="POST">-->
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">

                                <div class="tab-content">
                                    <div id="error">
                                    </div>
                                    <div class="tab-pane active" id="tab1">
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-11">
                                                <label class="control-label col-md-3">State : <span class="required">* </span></label>
                                                <div class="col-md-3">

                                                    <asp:DropDownList ID="ddlstate" runat="server" CssClass="form-control" TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddlstate_SelectedIndexChanged">
                                                    </asp:DropDownList>

                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-11">
                                                <label class="control-label col-md-3">District : <span class="required">* </span></label>

                                                <div class="col-md-3">

                                                    <asp:DropDownList ID="ddldistrict" runat="server" CssClass="form-control" TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged">
                                                    </asp:DropDownList>

                                                    <%--<asp:TextBox ID="TextBox1" CssClass="form-control" runat="server"  AutoPostBack="true" OnTextChanged="TxtProcode_TextChanged" ></asp:TextBox>--%>
                                                </div>
                                                <div class="col-md-5">
                                                </div>
                                            </div>
                                            <div class="col-lg-11">
                                                <label class="control-label col-md-3">Zone : <span class="required">* </span></label>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtzone" CssClass="form-control" TabIndex="1" runat="server" MaxLength="5"></asp:TextBox>
                                                  
                                                </div>
                                            </div>

                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-11">
                                                <label class="control-label col-md-3">Bank Code : <span class="required">* </span></label>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtbankcode" CssClass="form-control" MaxLength="8" runat="server" TabIndex="1" AutoPostBack="true" onkeypress="javascript:return isNumber (event)" OnTextChanged="txtbankcode_TextChanged"></asp:TextBox>
                                                    <%--<asp:TextBox ID="TextBox1" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="TxtAccNo_TextChanged"></asp:TextBox>--%>
                                                </div>
                                                <div class="col-md-5">
                                                    <asp:TextBox ID="txtbankname" CssClass="form-control" runat="server" TabIndex="1"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-11">
                                                <label class="control-label col-md-3">Branch Code : <span class="required">* </span></label>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtbranchcode" CssClass="form-control" runat="server" MaxLength="8" AutoPostBack="true"  TabIndex="1" onkeypress="javascript:return isNumber (event)" OnTextChanged="txtbranchcode_TextChanged"></asp:TextBox>
                                                    <%--<asp:TextBox ID="TextBox1" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="TxtAccNo_TextChanged"></asp:TextBox>--%>
                                                </div>
                                                <div class="col-md-5">
                                                    <asp:TextBox ID="txtbranchname" CssClass="form-control" runat="server"  TabIndex="1"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                    </div>

                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9">
                                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn blue" Text="Submit" OnClientClick="Javascript:return isvalidate();" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnModify" runat="server" CssClass="btn blue" Text="UPDATE" OnClientClick="Javascript:return isvalidate();" OnClick="btnModify_Click" />
                                        <asp:Button ID="btnDelete" runat="server" CssClass="btn blue" Text="DELETE" OnClick="btnDelete_Click" />
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
                                <asp:GridView ID="grdBank" runat="server" AllowPaging="True"
                                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                    EditRowStyle-BackColor="#FFFF99"
                                    OnPageIndexChanging="grdBank_PageIndexChanging"
                                    PagerStyle-CssClass="pgr" Width="100%"
                                    OnSelectedIndexChanged="grdBank_SelectedIndexChanged">
                                    <Columns>

                                        <asp:TemplateField HeaderText="Select" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkSelect" OnClick="lnkSelect_Click" runat="server" Text="select" CommandArgument='<%#Eval("ID")%>' CommandName="select" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="BANKCD" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lbBANKCD" runat="server" Text='<%# Eval("BANKCD") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="BRANCHCD" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lbBRCHD" runat="server" Text='<%# Eval("BRANCHCD") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Bank Name" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="DESCR" runat="server" Text='<%# Eval("DESCR") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="District" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lbdist" runat="server" Text='<%# Eval("DISTRICT") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" OnClick="lnkDelete_Click" Text="Delete" CommandArgument='<%#Eval("ID")%>' CommandName="select"></asp:LinkButton>
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



