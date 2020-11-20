<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmVoucherPA.aspx.cs" Inherits="FrmVoucherPA" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AVS In-So-Tech</title>
    <!-- BEGIN GLOBAL MANDATORY STYLES -->
    <link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&subset=all" rel="stylesheet" type="text/css">
    <link href="global/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css">
    <link href="global/plugins/simple-line-icons/simple-line-icons.min.css" rel="stylesheet" type="text/css">
    <link href="global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css">
    <link href="global/plugins/uniform/css/uniform.default.css" rel="stylesheet" type="text/css">
    <link href="global/plugins/bootstrap-switch/css/bootstrap-switch.min.css" rel="stylesheet" type="text/css" />
    <link href="global/css/components-rounded.css" rel="stylesheet" type="text/css" />
    <!-- END GLOBAL MANDATORY STYLES -->
</head>
<body>
    <form id="form1" runat="server">
        <div class="row" style="margin: 10px 10px 10px 10px">
            <div class="col-md-12">
                <div class="portlet box blue" id="form_wizard_1">
                    <div class="portlet-title">
                        <div class="caption">
                            Voucher Authorisation
                        </div>
                    </div>
                    <div class="portlet-body form">
                        <!--<form action="#" class="form-horizontal" id="submit_form" method="POST">-->
                        <div class="form-horizontal">
                            <div class="form-wizard">
                                <div class="form-body">
                                    <table>
                                        <tr>
                                            <td style="width: 80%">
                                                <div class="tab-content">
                                                    <div class="tab-pane active" id="tab1">
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
                                                                    <asp:TextBox ID="TxtSetno" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
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
                                                                    <asp:TextBox ID="TxtEntrydate" CssClass="form-control" runat="server" Enabled="False"></asp:TextBox>
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

                                                    </div>

                                                </div>
                                                <div class="row" align="center">
                                                    <div class="col-md-offset-3 col-md-9">
                                                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn blue" Text="Authorize" OnClick="btnSubmit_Click" AutoPostBack="true" />
                                                        <%--<asp:Button ID="PhotoSign" runat="server" CssClass="btn btn-success" Text="Photo & Sign" OnClick="PhotoSign_Click" />--%>
                                                    </div>
                                                </div>
                                            </td>
                                            <td style="width: 20%">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td>
                                                            <div>
                                                                <asp:Label ID="Label12" runat="server" Text="Signature" Style="font-weight: bold"></asp:Label>
                                                            </div>
                                                            <%--<div style="width:100%;height:100%;padding:5px" onclick="ShowUpload(1)" runat="server" id="DivSign" align="center">--%>

                                                            <img id="Img7" runat="server" style="height: 100%; width: 100%; border: 1px solid #000000; padding: 5px" />

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div>
                                                                <asp:Label ID="Label13" runat="server" Text="Photo" Style="font-weight: bold"></asp:Label>
                                                            </div>

                                                            <%--<div style="width:100%;height:100%;padding:5px" onclick="ShowUpload(2)" runat="server" id="Div2" align="center">--%>
                                                            <img id="Img8" runat="server" style="height: 140%; width: 100%; border: 1px solid #000000; padding: 5px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>

                                </div>
                                <div class="form-actions">
                                </div>
                            </div>
                        </div>
                        <!--</form>-->
                    </div>
                </div>
            </div>

            <div class="col-md-12">

                <div class="table-scrollable" style="width: 100%; height: auto; max-height: 300px; overflow-x: auto; overflow-y: auto">
                    <table class="table table-striped table-bordered table-hover" width="100%">
                        <thead>
                            <tr>
                                <th>
                                    <asp:GridView ID="grdAccStatement" runat="server" AlternatingRowStyle-CssClass="alt" CssClass="mGrid" Width="100%"
                                        AutoGenerateColumns="false" EditRowStyle-BackColor="#FFFF99" DataKeyNames="id" EmptyDataText="No Records Available">
                                        <Columns>

                                            <asp:TemplateField HeaderText="EntryDate" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEDate" runat="server" Text='<%# Eval("EDATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="SetNo" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSetNo" runat="server" Text='<%# Eval("SetNo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Particulars1">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblParticular" runat="server" Text='<%# Eval("PARTI") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Particulars2">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblparticular2" runat="server" Text='<%# Eval("PARTI1") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Cheque/Refrence">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInstNo" runat="server" Text='<%# Eval("INSTNO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Credit" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCredit" runat="server" Text='<%# Eval("CREDIT") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="DEBIT">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDebit" runat="server" Text='<%# Eval("DEBIT") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="BALANCE">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBalance" runat="server" Text='<%# Eval("BALANCE") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Dr/Cr">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDrCrIndicator" runat="server" Text='<%# Eval("DrCr") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="Th" HorizontalAlign="Left" />
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
        <asp:HiddenField ID="hdnRow" runat="server" Value="0" />
    </form>
</body>
</html>
