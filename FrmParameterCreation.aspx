<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/CBSMaster.master" CodeFile="FrmParameterCreation.aspx.cs" Inherits="FrmParameterCreation" %>

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
                        Parameter Creation
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
                                                <label class="control-label col-md-3">List Field : <span class="required">* </span></label>
                                                <div class="col-md-3">
                                                     <asp:TextBox ID="txtlistfd" CssClass="form-control" runat="server" MaxLength="12"   ></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-11">
                                                <label class="control-label col-md-3">List Values : <span class="required">* </span></label>
                                                
                                                <div class="col-md-3">

                                                    <asp:TextBox ID="txtlistvalue" CssClass="form-control" runat="server" MaxLength="15"   ></asp:TextBox>
                                                    <%--<asp:TextBox ID="TextBox1" CssClass="form-control" runat="server"  AutoPostBack="true" OnTextChanged="TxtProcode_TextChanged" ></asp:TextBox>--%>
                                                </div>
                                                <div class="col-md-5">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-11">
                                                <label class="control-label col-md-3">Branch Code : <span class="required">* </span></label>
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="ddlbank" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlbank_SelectedIndexChanged" >
                                                    </asp:DropDownList>
                                                    <%--<asp:TextBox ID="TextBox1" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="TxtAccNo_TextChanged"></asp:TextBox>--%>
                                                </div>
                                                <div class="col-md-5">
                                                    <asp:TextBox ID="txtBranchcode" CssClass="form-control" MaxLength="8" Enabled= "false" runat="server" AutoPostBack="true"  TabIndex="4" onkeypress="javascript:return isNumber (event)" ></asp:TextBox>
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
            <div class="table-scrollable" style="width: 100%; height: 350px; overflow-x: auto; overflow-y: auto">
                <table class="table table-striped table-bordered table-hover">
                    <thead>
                        <tr>
                            <th>
                                <asp:GridView ID="grdparameter" runat="server"
                                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                    EditRowStyle-BackColor="#FFFF99"
                                    Width="100%" OnSelectedIndexChanged="grdparameter_SelectedIndexChanged">
                                    <Columns>

                                        <asp:TemplateField HeaderText="List Field" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lbllistfield" runat="server" CssClass="GridCell" Text='<%# Eval("LISTFIELD") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="List Value" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lbllistvalues" runat="server" CssClass="GridCell" Text='<%# Eval("LISTVALUE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Branch Id" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lbbranch" runat="server" CssClass="GridCell" Text='<%# Eval("BRCD") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Select" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkselect" runat="server" Text="Select" CommandName="select" OnClick="lnkselect_Click" CommandArgument='<%#Eval("LISTFIELD")%>' class="glyphicon glyphicon-plus" ></asp:LinkButton>                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Delete" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" CommandArgument='<%#Eval("LISTFIELD")%>' OnClick="lnkDelete_Click" CommandName="select" class="glyphicon glyphicon-trash" ></asp:LinkButton>                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
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