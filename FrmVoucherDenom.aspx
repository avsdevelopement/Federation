<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmVoucherDenom.aspx.cs" Inherits="FrmVoucherDenom" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function FormatIt(obj) {
            if (obj.value.length == 2) // Day
                obj.value = obj.value + "/";
            if (obj.value.length == 5) // month 
                obj.value = obj.value + "/";
            if (obj.value.length == 11) // year 
                alert("Please Enter valid Date");
        }

        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;

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
                        Voucher Denomination
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
                                                <label class="col-md-2 control-label">Entry Date : <span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtEntryDate" ReadOnly="true" runat="server" placeholder="DD/MM/YYYY" CssClass="form-control" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" ></asp:TextBox>
                                                </div>
                                                <label class="col-md-2 control-label">Voucher No : </label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtSetNo" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtSetNo_TextChanged" onkeypress="javascript:return isNumber (event)" > </asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>

                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9">
                                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn blue" Text="Submit" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnClear" runat="server" CssClass="btn blue" Text="ClearAll" OnClick="btnClear_Click" />
                                        <asp:Button ID="btnExit" runat="server" CssClass="btn blue" Text="Exit" OnClick="btnExit_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="divVoucher" runat="server" class="col-md-12">
            <div class="table-scrollable" style="width: 100%; height: auto; max-height: 150px; overflow-x: auto; overflow-y: auto">
                <table class="table table-striped table-bordered table-hover" width="100%">
                    <thead>
                        <tr>
                            <th>
                                <asp:GridView ID="grdVoucher" runat="server" AlternatingRowStyle-CssClass="alt" CssClass="mGrid"
                                    AutoGenerateColumns="false" EditRowStyle-BackColor="#FFFF99" Width="100%" EmptyDataText="No voucher Available">
                                    <Columns>

                                        <asp:TemplateField HeaderText="Voucher" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSetNo" runat="server" Text='<%# Eval("SetNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Entry Date" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEntryDate" runat="server" Text='<%# Eval("EntryDate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Prod Code" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSubGlcode" runat="server" Text='<%# Eval("SubGlcode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Acc No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAccNo" runat="server" Text='<%# Eval("AccNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Dr/Cr" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCrDr" runat="server" Text='<%# Eval("CrDr") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Maker" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMaker" runat="server" Text='<%# Eval("Maker") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Checker">
                                            <ItemTemplate>
                                                <asp:Label ID="lblChecker" runat="server" Text='<%# Eval("Checker") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Deleted">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDeleted" runat="server" Text='<%# Eval("Deleted") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Dens">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDens" runat="server" CommandArgument='<%#Eval("SetNo")+"_"+Eval("Amount")+"_"+Eval("SubGlcode")+"_"+Eval("AccNo")%>' Text="Select" CommandName="select" OnClick="lnkDens_Click" class="glyphicon glyphicon-check"></asp:LinkButton>
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

