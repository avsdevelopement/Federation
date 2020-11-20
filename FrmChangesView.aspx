<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmChangesView.aspx.cs" Inherits="FrmChangesView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <div class="col-md-12">
        <div class="portlet box green" id="Div1">
            <div class="portlet-title">
                <div class="caption">
                   Issue Details
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
                                          <div id="Div2" class="row" style="margin: 7px 0 7px 0" runat="server">
                                                    <div class="col-lg-12">
                                                        <label class="control-label col-md-2">Remark:<span class="required">*</span></label>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtRemark" CssClass="form-control" placeholder="Remark" runat="server" AutoPostBack="true"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                        <div id="Div3" class="row" style="margin: 7px 0 7px 0" runat="server">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            </div>
                                                        <div class="col-md-2">
                                                          <asp:CheckBox ID="ChkSts" runat="server" AutoPostBack="true" Text="Is Active" OnCheckedChanged="ChkSts_CheckedChanged" />
                                                        </div>
                                                    </div>
                                                </div>

                                              <div class="row" style="margin: 20px; border-bottom: 1px solid rgba(53, 152, 220, 0.55);"><strong></strong></div>
                                        <div class="row" style="margin: 7px 0 7px 0; text-align: center">
                                            <div class="col-lg-12">
                                        
                                        <asp:Button ID="Btn_submit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="Btn_submit_Click"/>
                                                
                                                 <asp:Button ID="Btn_Exit" runat="server" Text="Exit" CssClass="btn btn-primary" OnClick="Btn_Exit_Click"/>
                                                
                                    </div>
                                            
                                  
                                </div>
                                       
                                      
                                         <div class="row" style="margin: 7px 0 7px 0">
        <div class="col-lg-12" style="height: 50%">
            <div class="table-scrollable" style="height:250px;overflow-y: scroll; padding-bottom: 10px;">
                <table class="table table-striped table-bordered table-hover" width="100%">
                    <thead>
    
                        <tr>
                            <th>
                                <asp:GridView ID="grdChanges" runat="server"
                                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                    EditRowStyle-BackColor="#FFFF99"
                                    PagerStyle-CssClass="pgr" Width="100%" OnSelectedIndexChanged="grdChanges_SelectedIndexChanged">
                                    <Columns>
                                          <asp:TemplateField HeaderText="SRNO" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="serial" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="PID" Visible="TRUE">
                                            <ItemTemplate>
                                                <asp:Label ID="PID" runat="server" Text='<%# Eval("PID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="REMARK" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="REMARK" runat="server" Text='<%# Eval("REMARK") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="STATUS">
                                            <ItemTemplate>
                                                <asp:Label ID="STATUS" runat="server" Text='<%# Eval("STATUS") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Add New" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkAdd" runat="server" CommandName="select" OnClick="lnkAdd_Click" CommandArgument='<%#Eval("PID")%>' class="glyphicon glyphicon-plus"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Modify" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit" runat="server" CommandName="select" OnClick="lnkEdit_Click" CommandArgument='<%#Eval("PID")%>' class="glyphicon glyphicon-plus"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                          <asp:TemplateField HeaderText="Delete" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="select" OnClick="lnkDelete_Click" CommandArgument='<%#Eval("PID")%>' class="glyphicon glyphicon-plus"></asp:LinkButton>
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

