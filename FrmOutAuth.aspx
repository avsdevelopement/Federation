<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmOutAuth.aspx.cs" Inherits="FrmOutAuth" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function isvalidate() {

            var instrumentno = document.getElementById('<%=TxtInstNo.ClientID%>').value;
            var message = '';

            if (instrumentno == "") {
                message = 'Please Enter Instrument No....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtInstNo.ClientID%>').focus();
                return false;
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="Div2">
                <div class="portlet-title">
                    <div class="caption">
                        Outward Clg Pending Authorization
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab1">
                                        <div class="row" style="margin-bottom: 10px;">
                                            <div id="Div1" class="row" style="margin: 7px 0 7px 0" runat="server">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Type 1<span class="required">* </span></label>
                                                    <div class="col-md-4">
                                                        <asp:RadioButton ID="Rdb_Single" runat="server" Text="Single Pass" AutoPostBack="true" GroupName="TYPE1" Checked="true" OnCheckedChanged="Rdb_Single_CheckedChanged" />
                                                        <asp:RadioButton ID="Rdb_Lot" runat="server" Text="Lot Pass" AutoPostBack="true" GroupName="TYPE1" OnCheckedChanged="Rdb_Lot_CheckedChanged" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="margin: 20px; border-bottom: 1px solid rgba(53, 152, 220, 0.55);"><strong></strong></div>
                                            <div class="row" style="margin: 7px 0px 7px 0px" id="Div_Single" runat="server">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Instrument No : </label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtInstNo" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>

                                                </div>
                                            </div>
                                            <div class="row" style="margin: 7px 0 7px 0" runat="server" id="Div_Lot" visible="false">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-1">From Setno <span class="required"></span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtFSetno" OnTextChanged="TxtFSetno_TextChanged" Placeholder="From Setno" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                    <label class="control-label col-md-1">To Setno<span class="required"></span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtTSetno" Placeholder="To Setno" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="margin: 7px 0 7px 0">

                                                <div class="row" style="margin: 20px; border-bottom: 1px solid rgba(53, 152, 220, 0.55);"><strong></strong></div>
                                                <div class="row" style="margin: 7px 0 7px 0; text-align: center">
                                                    <div class="col-lg-12">
                                                        <asp:Button ID="btnSearch" runat="server" CssClass="btn blue" Text="Submit" OnClick="btnSearch_Click" OnClientClick="Javascript:return isvalidate();" />
                                                        <asp:Button ID="Btn_Submit" runat="server" Text="Submit" CssClass="btn btn-success" OnClick="Btn_Submit_Click" Visible="false" />
                                                        <asp:Button ID="Btn_ClearAll" runat="server" Text="Clear All" CssClass="btn btn-success" />
                                                        <asp:Button ID="Btn_Exit" runat="server" Text="Exit" CssClass="btn btn-success" />
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
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="table-scrollable">
                <div class="table-scrollable" style="height: 200px; overflow-x: scroll; overflow-y: scroll; padding-bottom: 10px;">
                    <asp:Label ID="Lbl_Pass" runat="server" Text="PASSED ENTRIES" BackColor="#66ff99" Font-Bold="true" Font-Size="Medium"></asp:Label>
                    <table class="table table-striped table-bordered table-hover">
                        <thead>
                            <tr>
                                <th>
                                    <asp:GridView ID="grdOwgData" runat="server" AllowPaging="True"
                                        AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                        EditRowStyle-BackColor="#FFFF99"
                                        OnPageIndexChanging="grdOwgData_PageIndexChanging"
                                        PagerStyle-CssClass="pgr" Width="100%">
                                        <Columns>

                                            <asp:TemplateField HeaderText="Set No" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="SET_NO" runat="server" Text='<%# Eval("SET_NO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Scrl" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="SCRL" runat="server" Text='<%# Eval("SCRL") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="AT" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="AT" runat="server" Text='<%# Eval("AT") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Name" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="Name" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="AMOUNT" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="Amount" runat="server" Text='<%# Eval("AMOUNT") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="BANKNAME" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="Bankname" runat="server" Text='<%# Eval("BANKNAME") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Inst No" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="INSTNO" runat="server" Text='<%# Eval("instNO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Maker" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="Date" runat="server" Text='<%# Eval("maker") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Particulars" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="Parti" runat="server" Text='<%# Eval("PARTICULARS") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Authorize" Visible="true">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("setscroll")%>' CommandName="select" OnClick="lnkEdit_Click" class="glyphicon glyphicon-edit"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Cancel" Visible="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Eval("setscroll")%>' CommandName="select" OnClick="lnkDelete_Click" class="glyphicon glyphicon-trash"></asp:LinkButton>
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

