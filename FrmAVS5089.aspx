<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAVS5089.aspx.cs" Inherits="FrmAVS5089" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
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
                        Member Number Change
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">

                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Branch Code : <span class="required">*</span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtBrCode" Enabled="false" CssClass="form-control" runat="server" ></asp:TextBox>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtBrName" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Product Code : <span class="required">*</span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtProdType" runat="server" onkeypress="javascript:return isNumber(event)" OnTextChanged="txtProdType_TextChanged" AutoPostBack="true" Placeholder="Product Code" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="input-icon">
                                                    <i class="fa fa-search"></i>
                                                    <asp:TextBox ID="txtProdName" runat="server" OnTextChanged="txtProdName_TextChanged" AutoPostBack="true" Placeholder="Search Product Name" CssClass="form-control"></asp:TextBox>
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
                                            <label class="control-label col-md-2">New MemberNo : <span class="required">*</span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtAccNo" onkeypress="javascript:return isNumber(event)" OnTextChanged="txtAccNo_TextChanged" AutoPostBack="true" Placeholder="Account Number" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="col-md-4">
                                                <div class="input-icon">
                                                    <i class="fa fa-search"></i>
                                                    <asp:TextBox ID="txtAccName" OnTextChanged="txtAccName_TextChanged" AutoPostBack="true" Placeholder="Search Account Name" runat="server" CssClass="form-control" />
                                                    <div id="CustList2" style="height: 200px; overflow-y: scroll;"></div>
                                                    <asp:AutoCompleteExtender ID="AutoAccname" runat="server" TargetControlID="txtAccName" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                        MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" ServiceMethod="GetProdWiseName" CompletionListElementID="CustList2">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtCustNo" Enabled="false" Placeholder="Customer Number" runat="server" CssClass="form-control" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">From Date : </label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtFrDate" onkeyup="FormatIt(this)" placeholder="DD/MM/YYYY" CssClass="form-control" runat="server" />
                                            </div>
                                            <label class="control-label col-md-2">To Date : </label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtToDate" onkeyup="FormatIt(this)" placeholder="DD/MM/YYYY" CssClass="form-control" runat="server" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin: 7px 0 7px 0" id="DivChangeAcc" runat="server" visible="false">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Old MemberNo : <span class="required">*</span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtChangAccNo" onkeypress="javascript:return isNumber(event)" OnTextChanged="txtChangAccNo_TextChanged" AutoPostBack="true" Placeholder="Account Number" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="col-md-4">
                                                <div class="input-icon">
                                                    <i class="fa fa-search"></i>
                                                    <asp:TextBox ID="txtChangAccName" OnTextChanged="txtChangAccName_TextChanged" AutoPostBack="true" Placeholder="Search Account Name" runat="server" CssClass="form-control" />
                                                    <div id="CustList3" style="height: 200px; overflow-y: scroll;"></div>
                                                    <asp:AutoCompleteExtender ID="AutoAccname1" runat="server" TargetControlID="txtChangAccName" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                        MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" ServiceMethod="GetProdWiseName" CompletionListElementID="CustList3">
                                                    </asp:AutoCompleteExtender>
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
                                        <asp:Button ID="btnChange" runat="server" CssClass="btn blue" Text="Change" OnClick="btnChange_Click" />
                                        <asp:Button ID="btnClear" runat="server" Text="Clear All" CssClass="btn btn-primary" OnClick="btnClear_Click" />
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

    <div class="table-scrollable" style="height: auto; max-height: 400px; overflow-x: auto; overflow-y: auto">
        <table class="table table-striped table-bordered table-hover" width="100%">
            <thead>
                <tr>
                    <th>
                        <asp:GridView ID="grdTransaction" runat="server" AlternatingRowStyle-CssClass="alt" CssClass="mGrid" AutoGenerateColumns="false" EditRowStyle-BackColor="#FFFF99" Width="100%">
                            <Columns>

                                <asp:TemplateField HeaderStyle-Width="80px" HeaderText="EntryDate">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEntryDate" runat="server" Text='<%# Eval("EntryDate") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-Width="100px" HeaderText="Voucher No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblVoucherNo" runat="server" Text='<%# Eval("VoucherNo") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-Width="80px" HeaderText="A/C Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSubGlCode" runat="server" Text='<%# Eval("SubGlCode") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-Width="80px" HeaderText="AccNo">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAccNo" runat="server" Text='<%# Eval("AccNo") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-Width="200px" HeaderText="Cust Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCustName" runat="server" Text='<%# Eval("CustName") %>'></asp:Label>
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

