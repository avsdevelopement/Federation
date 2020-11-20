<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmMenuMaster.aspx.cs" Inherits="FrmMenuMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="portlet box blue" id="form_wizard_1">
                        <div class="portlet-title">
                            <div class="caption">
                                Add New Menu 
                            </div>
                        </div>
                        <div class="portlet-body form">
                            <div class="form-horizontal">
                                <div class="form-wizard">
                                    <div class="form-body">
                                        <div class="tab-content">
                                            <div class="tab-pane active" id="tab__blue">
                                                <ul class="nav nav-pills">

                                                    <li>
                                                        <asp:LinkButton ID="lnkAdd" runat="server" Text="a" class="btn btn-default" OnClick="lnkAdd_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-asterisk"></i>Add New</asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton ID="lnkModify" runat="server" Text="a" class="btn btn-default" OnClick="lnkModify_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-plus-circle"></i>Modify</asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton ID="lnkDelete" runat="server" Text="MD" class="btn btn-default" OnClick="lnkDelete_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-pencil-square-o"></i>Delete</asp:LinkButton>
                                                    </li>
                                                    <li class="pull-right">
                                                        <asp:Label ID="lblStst" runat="server" Text="Activity Perform :" Style="font-weight: bold;"></asp:Label>
                                                        <asp:Label ID="lblActivity" runat="server" Text=""></asp:Label>
                                                    </li>
                                                </ul>
                                            </div>

                                            <div style="border: 1px solid #3598dc">
                                                <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label">MenuId<span class="required">*</span></label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtMenuId" OnTextChanged="txtMenuId_TextChanged" AutoPostBack="true" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="Menu Id" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="txtMenuTitle" OnTextChanged="txtMenuTitle_TextChanged" AutoPostBack="true" CssClass="form-control" placeholder="Menu Title" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row" style="margin-bottom: 10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label">Parent MenuTitle<span class="required">*</span></label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="txtPMenuTitle" CssClass="form-control" placeholder="Parent Menu Title" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <label class="control-label">Page Description<span class="required">*</span></label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="txtDesc" CssClass="form-control" placeholder="Menu Description" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row" style="margin-bottom: 10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label">Page URL<span class="required">*</span></label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="txtUrl" CssClass="form-control" placeholder="Menu URL" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-actions">
                                        <div class="row">
                                            <div class="col-md-offset-3 col-md-9">
                                                <asp:Button ID="Submit" runat="server" Text="Submit" CssClass="btn blue" OnClick="Submit_Click" OnClientClick="Javascript:return IsValid();" />
                                                <asp:Button ID="Exit" runat="server" Text="Exit" CssClass="btn blue" OnClick="Exit_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                            </div>
                            <div class="col-md-12">
                                <div class="table-scrollable">
                                    <table class="table table-striped table-bordered table-hover">
                                        <thead>
                                            <tr>
                                                <th>
                                                    <asp:GridView ID="grdMenuMaster" runat="server" AllowPaging="True"
                                                        AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                        EditRowStyle-BackColor="#FFFF99"
                                                        OnPageIndexChanging="grdMenuMaster_PageIndexChanging"
                                                        PagerStyle-CssClass="pgr" Width="100%">
                                                        <Columns>

                                                            <asp:TemplateField HeaderText="Select" Visible="true" runat="server">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName="select" CommandArgument='<%#Eval("MenuId")%>' OnClick="lnkEdit_Click" class="glyphicon glyphicon-plus"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="SrNo">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Menu ID">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMenuId" runat="server" Text='<%# Eval("MenuId") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Parent MenuId">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblParentMenuId" runat="server" Text='<%# Eval("ParentMenuId") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Menu Title">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMenuTitle" runat="server" Text='<%# Eval("MenuTitle") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Menu Description">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPageDesc" runat="server" Text='<%# Eval("PageDesc") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Menu URL">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPageURL" runat="server" Text='<%# Eval("PageURL") %>'></asp:Label>
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
        </ContentTemplate>

    </asp:UpdatePanel>

</asp:Content>

