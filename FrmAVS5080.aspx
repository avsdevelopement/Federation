<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAVS5080.aspx.cs" Inherits="FrmAVS5080" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        Cancel Entry
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab__blue">

                                        <div class="row" style="margin: 7px 0px 7px 0px">
                                            <div class="col-md-12">
                                                <div class="col-md-1" style="width: 70px">
                                                    <asp:Label ID="lblVN" runat="server" Text="Voucher No"></asp:Label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtSetNo" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-md-1" style="width: 120px">
                                                    <asp:Label ID="lblPrdCD" runat="server" Text="Entry Date"></asp:Label>
                                                </div>
                                                <div class="col-md-2" style="width: 150px">
                                                    <asp:TextBox ID="TxtEntryDate" runat="server" placeholder="Product Code" CssClass="form-control"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #fa4e0d"></strong></div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0px 7px 0px">
                                            <div class="col-lg-offset-1" style="text-align: center">
                                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn blue" Text="Submit" OnClientClick="Javascript:return isvalidate();" OnClick="btnSubmit_Click" />
                                                &nbsp;<asp:Button ID="Exit" OnClick="Exit_Click" runat="server" CssClass="btn blue" Text="Exit" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="table-scrollable" style="width: 100%; height: 500px; overflow-x: auto; overflow-y: auto">
                                                    <table class="table table-striped table-bordered table-hover">
                                                        <thead>
                                                            <tr>
                                                                <th>
                                                                    <asp:GridView ID="grdShow" runat="server"
                                                                        AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                                        EditRowStyle-BackColor="#FFFF99" Width="100%">
                                                                        <Columns>

                                                                            <asp:TemplateField HeaderText="VOUCHER NO" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="SET_NO" runat="server" Text='<%# Eval("SETNO") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="A/C TYPE" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="SUBGLCODE" runat="server" Text='<%# Eval("SUBGLCODE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="A/C Name" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="GLNAME" runat="server" Text='<%# Eval("GLNAME") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="A/C NO" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="ACCNO" runat="server" Text='<%# Eval("ACCNO") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="CUST NAME" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="CUSTNAME" runat="server" Text='<%# Eval("CUSTNAME") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="PARTICULARS" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="PARTICULARS" runat="server" Text='<%# Eval("PARTICULARS") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Cr AMOUNT" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="CREDIT" runat="server" Text='<%# Eval("CREDIT") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Dr AMOUNT" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="DEBIT" runat="server" Text='<%# Eval("DEBIT") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="MID" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="MID" runat="server" Text='<%# Eval("MID") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="ENTRYDATE" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="ENTRYDATE" runat="server" Text='<%# Eval("ENTRYDATE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="MAKER" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="LOGINCODE" runat="server" Text='<%# Eval("LOGINCODE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Cancel" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Eval("ScrollNo")%>' CommandName="select" class="glyphicon glyphicon-trash" OnClick="lnkDelete_Click"></asp:LinkButton>
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
    <div id="CUST_TR" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg" style="width: 80%">
            <!-- Modal content-->
            <div class="modal-content" style="width: 100%">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Customer Transfer Cancel</h4>
                </div>
                <div class="modal-body">
                    <div class="row" style="margin: 7px 0px 7px 0px">
                        <div class="col-md-12">
                            <div class="col-md-2">
                                <asp:Label ID="lblMakerName" runat="server" Text="Maker Name :"></asp:Label>
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="TxtMakName" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <asp:Label ID="lblPCNAME" runat="server" Text="PC Name :"></asp:Label>
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="TxtPCMAC" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <asp:Label ID="lblsetno" runat="server" Text="Set No :"></asp:Label>
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #fa4e0d"></strong></div>
                        </div>
                    </div>

                    <div class="row" style="margin: 7px 0 7px 0">
                        <div class="col-lg-11">
                            <label class="control-label col-md-3">Entry Date : <span class="required">* </span></label>
                            <div class="col-md-3">
                                <asp:TextBox ID="TextBox2" CssClass="form-control" runat="server" Enabled="False"></asp:TextBox>
                            </div>
                            <div class="col-md-3" visible="false">
                            </div>
                        </div>
                    </div>
                    <div class="row" style="margin: 7px 0 7px 0">
                        <div class="col-lg-11">
                            <label class="control-label col-md-3">Product Code: <span class="required">* </span></label>
                            <div class="col-md-3">
                                <asp:TextBox ID="TxtProcode" CssClass="form-control" runat="server" ReadOnly="True"></asp:TextBox>
                                <%--<asp:TextBox ID="TextBox1" CssClass="form-control" runat="server"  AutoPostBack="true" OnTextChanged="TxtProcode_TextChanged" ></asp:TextBox>--%>
                            </div>
                            <div class="col-md-5">
                                <asp:TextBox ID="TxtProName" CssClass="form-control" runat="server" Enabled="False"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row" style="margin: 7px 0 7px 0">
                        <div class="col-lg-11">
                            <label class="control-label col-md-3">Account No : <span class="required">* </span></label>
                            <div class="col-md-3">
                                <asp:TextBox ID="TxtAccNo" CssClass="form-control" runat="server" Enabled="False"></asp:TextBox>
                                <%--<asp:TextBox ID="TextBox1" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="TxtAccNo_TextChanged"></asp:TextBox>--%>
                            </div>
                            <div class="col-md-5">
                                <asp:TextBox ID="TxtAccName" CssClass="form-control" runat="server" Enabled="False"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row" style="margin: 7px 0 7px 0">
                        <div class="col-lg-11">
                            <label class="control-label col-md-3">Naration : <span class="required">* </span></label>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtnaration" CssClass="form-control" runat="server" Text="To Cash" Enabled="False"></asp:TextBox>
                            </div>
                            <label class="control-label col-md-2">Naration 2 : <span class="required"></span></label>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtnaration1" CssClass="form-control" runat="server" TabIndex="7" Enabled="False"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row" style="margin: 7px 0 7px 0">
                        <div class="col-lg-11">

                            <label class="control-label col-md-3">Balance : <span class="required">* </span></label>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtBalance" CssClass="form-control" runat="server" TabIndex="-1" ReadOnly="true"></asp:TextBox>
                            </div>

                            <label class="control-label col-md-2">Amount : <span class="required">* </span></label>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtamountt" CssClass="form-control" runat="server" TabIndex="8" Enabled="False"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row" align="center">
                        <div class="col-md-offset-3 col-md-9">
                            <asp:Button ID="btncancel" runat="server" CssClass="btn blue" Text="Cancel" OnClick="btncancel_Click" AutoPostBack="true" />
                            <%--<asp:Button ID="PhotoSign" runat="server" CssClass="btn btn-success" Text="Photo & Sign" OnClick="PhotoSign_Click" />--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

