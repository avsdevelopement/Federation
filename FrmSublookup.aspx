<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/CBSMaster.master" CodeFile="FrmSublookup.aspx.cs" Inherits="FrmSublookup" %>

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
                        Sub Lookup Master
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
                                                <label class="control-label col-md-3">LookUp No : <span class="required">* </span></label>
                                                <div class="col-md-3">

                                                    <asp:TextBox ID="txtlno" CssClass="form-control" runat="server" MaxLength="8" AutoPostBack= "true" TabIndex="2" onkeypress="javascript:return isNumber (event)" OnTextChanged="txtlno_TextChanged" ></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-11">
                                                <label class="control-label col-md-3">Lookup Type : <span class="required">* </span></label>
                                                
                                                <div class="col-md-3">

                                                    <asp:TextBox ID="txtltype" CssClass="form-control" runat="server" AutoPostBack="true" MaxLength="20"  TabIndex="2" ></asp:TextBox>
                                                    <%--<asp:TextBox ID="TextBox1" CssClass="form-control" runat="server"  AutoPostBack="true" OnTextChanged="TxtProcode_TextChanged" ></asp:TextBox>--%>
                                                </div>
                                                <div class="col-md-5">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-11">
                                                <label class="control-label col-md-3">Description : <span class="required">* </span></label>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtdesc" CssClass="form-control" MaxLength="50" runat="server" AutoPostBack="true"  ></asp:TextBox>
                                                    <%--<asp:TextBox ID="TextBox1" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="TxtAccNo_TextChanged"></asp:TextBox>--%>
                                                </div>
                                                <div class="col-md-5">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-11">
                                                <label class="control-label col-md-2">&nbsp;</label></div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-11">

                                                <div class="col-md-3">
                                                </div>

                                                <div class="col-md-3">
                                                </div>
                                            </div>
                                        </div>

                                    </div>

                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9">
                                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn blue" Text="Submit"  OnClientClick="Javascript:return isvalidate();" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnModify" runat="server" CssClass="btn blue" Text="UPDATE"  OnClientClick="Javascript:return isvalidate();" OnClick="btnModify_Click" />
                                        <asp:Button ID="btnDelete" runat="server" CssClass="btn blue" Text="DELETE" OnClick="btnDelete_Click"   />
                                     <asp:Button ID="BtnExit" runat="server" CssClass="btn blue" Text="Exit" OnClick="BtnExit_Click" />
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
                                <asp:GridView ID="Grdcrdrdetailes" runat="server" AllowPaging="True"
                                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                    EditRowStyle-BackColor="#FFFF99"
                                    OnPageIndexChanging=  "Grdcrdrdetailes_PageIndexChanging"
                                    PagerStyle-CssClass="pgr" Width="100%" OnSelectedIndexChanged="Grdcrdrdetailes_SelectedIndexChanged">
                                    <Columns>

                                         <asp:TemplateField HeaderText="Select" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkselect" runat="server" Text="Select" OnClick= "lnkselect_Click"  CommandArgument='<%#Eval("LNO")%>' CommandName="select" class="glyphicon glyphicon-plus"></asp:LinkButton>                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="List Number" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="LNO" runat="server" Text='<%# Eval("LNO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="LTYPE" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="LTYPE" runat="server" Text='<%# Eval("LTYPE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="DESCRIPTION" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="DESCRIPTION" runat="server" Text='<%# Eval("DESCRIPTION") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Select" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" OnClick="lnkDelete_Click" CommandArgument='<%#Eval("LNO")%>'  CommandName="select" class="glyphicon glyphicon-plus"></asp:LinkButton>                                                
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