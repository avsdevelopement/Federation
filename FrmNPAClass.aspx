<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmNPAClass.aspx.cs" Inherits="FrmNPAClass" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script>
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;

            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <div class="col-md-12">
        <div class="portlet box green" id="Div1">
            <div class="portlet-title">
                <div class="caption">
                    NPA CLASSES
                </div>
            </div>
            <div class="portlet-body form">
                <div class="form-horizontal">
                    <div class="form-wizard">
                        <div class="form-body">
                            <div class="tab-content">
                                <div class="tab-pane active" id="tab1">
                                    <div class="row" style="margin-bottom: 10px;">
                                      
                                      <div class="row" style="margin: 10px;"><strong></strong></div>
                                         <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">GRCODE:<span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtGrcde" placeholder="GRCODE" runat="server" CssClass="form-control number" TabIndex="1"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-2">Asset:<span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtAsset" runat="server" CssClass="form-control" placeholder="Asset" TabIndex="2"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">GFrom:<span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtGfrom" placeholder="GFrom" runat="server" CssClass="form-control number" TabIndex="3"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-2">GTo:<span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtGto" runat="server" CssClass="form-control number" placeholder="GTo"  TabIndex="4"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                         <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Provision Secured:<span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtProvSec" placeholder="Prov Secured" runat="server" CssClass="form-control number"  TabIndex="5"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-2">Provision Unsecured:<span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtProvUnsec" runat="server" CssClass="form-control number" placeholder="Prov Unsecured"  TabIndex="6"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Interest Secured:<span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtIntSec" placeholder="Int Secured" runat="server" CssClass="form-control number" TabIndex="7"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-2">Interest Unsecured:<span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtIntUnsec" runat="server" CssClass="form-control number" placeholder="Int Unsecured" TabIndex="8"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 20px; border-bottom: 1px solid rgba(53, 152, 220, 0.55);"><strong></strong></div>
                                         <div class="row" style="margin: 7px 0 7px 0; text-align: center">
                                            <div class="col-lg-12">
                                                <div class="col-md-6">

                                                    <asp:Button ID="BtnSubmit" runat="server" CssClass="btn green" Text="Submit" OnClick="BtnSubmit_Click"/>
                                    <asp:Button ID="BtnModify" runat="server" CssClass="btn green" Text="Modify" OnClick="BtnModify_Click" Visible="false" />
                                    <asp:Button ID="BtnDelete" runat="server" CssClass="btn green" Text="Delete" OnClick="BtnDelete_Click" Visible="false" />
                                                    <asp:Button ID="BtnExit" runat="server" CssClass="btn green" Text="Exit" OnClick="BtnExit_Click" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
        <div class="col-lg-12">
            <div class="table-scrollable">
                <table class="table table-striped table-bordered table-hover" width="100%">
                    <thead>
                        <tr>
                            <th>
                                <asp:GridView ID="grdNPA" runat="server"
                                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" AllowPaging="True"
                                    EditRowStyle-BackColor="#FFFF99" PageIndex="10" PageSize="25" OnPageIndexChanging="grdNPA_PageIndexChanging"
                                    PagerStyle-CssClass="pgr" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SRNO">
                                            <ItemTemplate>
                                                <asp:Label ID="SRNO" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="GRCODE" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="GRCODE" runat="server" Text='<%# Eval("GRCODE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="ASSET">
                                            <ItemTemplate>
                                                <asp:Label ID="ASSET" runat="server" Text='<%# Eval("ASSET") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="GFROM">
                                            <ItemTemplate>
                                                <asp:Label ID="GFROM" runat="server" Text='<%# Eval("GFROM") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="GTO">
                                            <ItemTemplate>
                                                <asp:Label ID="GTO" runat="server" Text='<%# Eval("GTO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="PROVSECURED">
                                            <ItemTemplate>
                                                <asp:Label ID="PROVSECURED" runat="server" Text='<%# Eval("PROVSECURED") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    
                                        <asp:TemplateField HeaderText="PROVUNSECURED ">
                                            <ItemTemplate>
                                                <asp:Label ID="PROVUNSECURED" runat="server" Text='<%# Eval("PROVUNSECURED") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="INTSECURED">
                                            <ItemTemplate>
                                                <asp:Label ID="INTSECURED" runat="server" Text='<%# Eval("INTSECURED") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="INTUNSECURED">
                                            <ItemTemplate>
                                                <asp:Label ID="INTUNSECURED" runat="server" Text='<%# Eval("INTUNSECURED") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Add New" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkAdd" runat="server" CommandName="select" CommandArgument='<%#Eval("ID")%>' OnClick="lnkAdd_Click" class="glyphicon glyphicon-plus"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Modify" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit" runat="server" CommandName="select" CommandArgument='<%#Eval("ID")%>' OnClick="lnkEdit_Click" class="glyphicon glyphicon-plus"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Delete" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="select" CommandArgument='<%#Eval("ID")%>' OnClick="lnkDelete_Click" class="glyphicon glyphicon-plus"></asp:LinkButton>
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
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
          </div>
    
</asp:Content>

