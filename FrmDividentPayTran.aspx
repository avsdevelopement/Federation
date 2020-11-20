<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmDividentPayTran.aspx.cs" Inherits="FrmDividentPayTran" %>

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
                        Divident Transfer
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">

                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2" style="width: 120px">Branch Code :<span class="required">*</span></label>
                                            <div class="col-md-1">
                                                <asp:TextBox ID="txtBrCode" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" OnTextChanged="txtBrCode_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtBrName" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <label class="control-label col-md-1">Pr Code :<span class="required">*</span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtProdCode" runat="server" CssClass="form-control" OnTextChanged="txtProdCode_TextChanged" AutoPostBack="true" onkeypress="javascript:return isNumber(event)" Placeholder="Account Type"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="input-icon">
                                                    <i class="fa fa-search"></i>
                                                    <asp:TextBox ID="txtProdName" runat="server" CssClass="form-control" OnTextChanged="txtProdName_TextChanged" AutoPostBack="true" Placeholder="Search Product Name" onkeypress="return checkQuote();"></asp:TextBox>
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
                                            <label class="control-label col-md-2" style="width: 120px">AsOnDate : <span class="required">* </span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtAsOnDate" onkeyup="FormatIt(this)" placeholder="DD/MM/YYYY" CssClass="form-control" runat="server" />
                                            </div>
                                            <label class="control-label col-md-2">Division : <span class="required">* </span></label>
                                            <div class="col-md-2">
                                                <asp:DropDownList ID="DdlRecDiv" CssClass="form-control" runat="server" OnSelectedIndexChanged="DdlRecDiv_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2" style="width: 120px">Department : <span class="required">* </span></label>
                                            <div class="col-md-2">
                                                <asp:DropDownList ID="DdlRecDept" CssClass="form-control" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                            <label class="control-label col-md-2">Cr GlCode : <span class="required">* </span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtProdCode2" CssClass="form-control" runat="server" />
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-12">
                                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn blue" Text="Submit" Visible="true" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnPost" runat="server" CssClass="btn blue" Text="Post" Visible="false" OnClick="btnPost_Click" />
                                        <asp:Button ID="Btn_Report" runat="server" CssClass="btn blue" Text="Report" Visible="true" OnClick="Btn_Report_Click"/>
                                        <asp:Button ID="btnReport" runat="server" CssClass="btn blue" Text="AGM Report" Visible="true" OnClick="btnReport_Click"/>
                                        <asp:Button ID="btnClear" runat="server" CssClass="btn blue" Text="Clear All" OnClick="btnClear_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="col-md-12">
        <div class="table-scrollable" style="height: auto; max-height: 500px; overflow-x: auto; overflow-y: auto">
            <table class="table table-striped table-bordered table-hover" >
                <thead>
                    <tr>
                        <th>
                            <asp:GridView ID="grdDevident" runat="server" CssClass="mGrid" CellPadding="6" CellSpacing="7" PageIndex="5" ForeColor="#333333"
                                AutoGenerateColumns="False" BorderWidth="1px" BorderColor="#333300" Width="100%" ShowFooter="true">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>

                                    <asp:TemplateField HeaderStyle-Width="20px" HeaderText="Select" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk" runat="server" Checked ="true"  onclick="Check_Click(this)" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="50px" HeaderText="SrNum" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSrNum" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="50px" HeaderText="Gl Code" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGlcode" runat="server" Text='<%# Eval("Glcode") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="50px" HeaderText="Prod Code" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSubglcode" runat="server" Text='<%# Eval("Subglcode") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="80px" HeaderText="Acc No" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAccNo" runat="server" Text='<%# Eval("ACCNO") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="50px" HeaderText="CustNo" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCustNo" runat="server" Text='<%# Eval("Custno") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="200px" HeaderText="Cust Name" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCustName" runat="server" Text='<%# Eval("Custname") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="200px" HeaderText="Description" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDESCR" runat="server" Text='<%# Eval("DESCR") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="150px" HeaderText="Balance" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBalance" runat="server" Text='<%# Eval("Balance") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="150px" HeaderText="IMS Balance" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIMSBalance" runat="server" Text='<%# Eval("IMSBalance") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="150px" HeaderText="AGM Balance" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAGMBalance" runat="server" Text='<%# Eval("AGMBalance") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                </Columns>
                                <FooterStyle BackColor="#ccffcc" Font-Bold="True" ForeColor="Black" HorizontalAlign="Right" BorderStyle="None" />
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

</asp:Content>

