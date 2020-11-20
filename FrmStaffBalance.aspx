<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmStaffBalance.aspx.cs" Inherits="FrmStaffBalance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        Staff Balance Report
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
                                                <label class="control-label col-md-2">As On Date</label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtAsOnDate" Enabled="true" onkeyup="FormatIt(this);" onkeypress="javascript:return isNumber(event)" placeholder="DD/MM/YYYY" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Branch Name  <span class="required">*</span></label>
                                                <div class="col-lg-4">
                                                    <asp:DropDownList ID="DdlBrName" CssClass="form-control" OnTextChanged="DdlBrName_TextChanged" AutoPostBack="true" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-lg-2">
                                                    <asp:TextBox ID="TxtBrcd" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <asp:CheckBox ID="ChkChange" runat="server" Text="Change Report Codes" OnCheckedChanged="ChkChange_CheckedChanged" AutoPostBack="true" />
                                                </div>
                                            </div>


                                    </div>
                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="row" style="margin: 7px 0 7px 0; margin-top: 20px;">
                                    <div class="col-md-offset-3">
                                        <asp:Button ID="BtnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" TabIndex="2" />
                                        <asp:Button ID="BtnReport" runat="server" Text="Report" CssClass="btn btn-primary" TabIndex="3" OnClick="BtnReport_Click" />
                                        <asp:Button ID="BtnExit" runat="server" Text="Exit" CssClass="btn btn-primary" TabIndex="4" OnClick="BtnExit_Click" />
                                    </div>
                                   
                                </div>
                            </div>
                            <div id="DivGlData" runat="server" >
                                    <asp:GridView ID="GrdInsert" runat="server" AutoGenerateColumns="false" >
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr.No">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtSrNo" Enable="false" CssClass="form-control" runat="server" Text='<%#Container.DataItemIndex+1 %>' Enabled="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                                                                        
                                            <asp:TemplateField HeaderText="Shares">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS1" Text='<%# Eval("S1") %>'  CssClass="form-control" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Saving">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS2" Text='<%# Eval("S2") %>' CssClass="form-control" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Subgl 3">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS3" Text='<%# Eval("S3") %>' CssClass="form-control" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Subgl 4">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS4" Text='<%# Eval("S4") %>' CssClass="form-control" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Subgl 5">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS5" Text='<%# Eval("S5") %>' CssClass="form-control" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Subgl 6">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS6" Text='<%# Eval("S6") %>' CssClass="form-control" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Subgl 7">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS7" Text='<%# Eval("S7") %>' CssClass="form-control" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Subgl 8">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS8" Text='<%# Eval("S8") %>' CssClass="form-control" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Subgl 9">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS9" Text='<%# Eval("S9") %>' CssClass="form-control" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Subgl 10">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS10" Text='<%# Eval("S10") %>' CssClass="form-control" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:Button ID="BtnUpdate" CssClass="btn btn-primary" runat="server" Text="Update Codes" OnClick="BtnUpdate_Click"/>
                                    </div>
                        </div>
                    </div>
                    <div class="col-md-12">
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

