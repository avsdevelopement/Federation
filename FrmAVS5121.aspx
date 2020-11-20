<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAVS5121.aspx.cs" Inherits="FrmAVS5121" %>

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

        function FormatIt(obj) {
            if (obj.value.length == 2) // Day
                obj.value = obj.value + "/";
            if (obj.value.length == 5) // month 
                obj.value = obj.value + "/";
            if (obj.value.length == 11) // year 
                alert("Please Enter valid Date");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        NPA Marking
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">

                                    <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                        <div class="col-lg-12">
                                            <div class="col-md-2">
                                            </div>
                                            <div class="col-md-6">
                                                <asp:RadioButtonList ID="rbtnType" runat="server" OnSelectedIndexChanged="rbtnType_SelectedIndexChanged" AutoPostBack="true" RepeatDirection="Horizontal" Style="width: 300px;">
                                                    <asp:ListItem Text="Normal to NPA" Value="1" Selected="True" />
                                                    <asp:ListItem Text="NPA to Normal" Value="2" />
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Branch Code <span class="required">*</span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtBrCode" Enabled="false" CssClass="form-control" runat="server" />
                                            </div>
                                            <div class="col-md-4">
                                                <asp:DropDownList ID="ddlBrName" CssClass="form-control" OnSelectedIndexChanged="ddlBrName_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Product Type <span class="required">*</span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtProdType" runat="server" CssClass="form-control" onkeypress="javascript:return isNumber(event)" OnTextChanged="txtProdType_TextChanged" AutoPostBack="true" Placeholder="Account Type"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="input-icon">
                                                    <i class="fa fa-search"></i>
                                                    <asp:TextBox ID="txtProdName" runat="server" CssClass="form-control" OnTextChanged="txtProdName_TextChanged" AutoPostBack="true" Placeholder="Search Product Name"></asp:TextBox>
                                                    <div id="CustList1" style="height: 200px; overflow-y: scroll;"></div>
                                                    <asp:AutoCompleteExtender ID="AutoGlName" runat="server" TargetControlID="txtProdName" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                        MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" ServiceMethod="GetGlWiseName" CompletionListElementID="CustList1">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Entry Date <span class="required">*</span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtEntryDate" runat="server" CssClass="form-control" Placeholder="DD/MM/YYYY" onkeypress="javascript:return isNumber(event)"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9">
                                        <%--<asp:Button ID="btnTrail" runat="server" CssClass="btn blue" Text="Trail" OnClick="btnTrail_Click" />--%>
                                        <asp:Button ID="btnApply" runat="server" CssClass="btn blue" Text="Apply" OnClick="btnApply_Click" />
                                        <asp:Button ID="btnExit" runat="server" Text="Exit" CssClass="btn btn-primary" OnClick="btnExit_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="col-lg-12">
        <div class="table-scrollable" style="height: auto; max-height: 500px; overflow-x: auto; overflow-y: auto">
            <table class="table table-striped table-bordered table-hover" width="100%">
                <thead>
                    <tr>
                        <th>
                            <asp:GridView ID="grdTrans" runat="server" AlternatingRowStyle-CssClass="alt" CssClass="mGrid" AutoGenerateColumns="false"
                                ShowFooter="true" EditRowStyle-BackColor="#FFFF99" Width="100%">
                                <Columns>

                                    <asp:TemplateField HeaderStyle-Width="80px" HeaderText="BrCd">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBrCd" runat="server" Text='<%# Eval("BrCd") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="80px" HeaderText="EntryDate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEntryDate" runat="server" Text='<%# Eval("EntryDate") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="80px" HeaderText="GlCode">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGlCode" runat="server" Text='<%# Eval("GlCode") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="80px" HeaderText="SubGlCode">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSubGlCode" runat="server" Text='<%# Eval("SubGlCode") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="200px" HeaderText="GlName">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGlName" runat="server" Text='<%# Eval("GlName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="80px" HeaderText="AccNo">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAccNo" runat="server" Text='<%# Eval("AccNo") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="200px" HeaderText="PartiCulars">
                                        <ItemTemplate>
                                            <asp:Label ID="lblParti" runat="server" Text='<%# Eval("Parti") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="100px" HeaderText="Debit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDebit" runat="server" Text='<%# Eval("Debit") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="100px" HeaderText="Credit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCredit" runat="server" Text='<%# Eval("Credit") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="Th" HorizontalAlign="Right" />
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

</asp:Content>

