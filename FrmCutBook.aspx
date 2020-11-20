<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmCutBook.aspx.cs" Inherits="FrmCutBook" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function FormatIT(obj) {
            if (obj.value.length == 2)
                obj.value = obj.value + "/";//DATE
            if (obj.value.length == 5)
                obj.value = obj.value + "/";//month
            if (obj.value.length == 11) //Year
                alert("Enter Valid Date");
        }
    </script>

    <script>
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="portlet box blue" id="form_wizard_1">
                        <div class="portlet-title">
                            <div class="caption">
                                Cut Book Report
                            </div>
                        </div>
                        <div class="portlet-body form">
                            <div class="form-horizontal">
                                <div class="form-wizard">
                                    <div class="form-body">
                                        <div class="tab-content">
                                            <div class="tab-pane active" id="tab__blue">
                                                <div style="border: 1px solid #3598dc">
                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-1">
                                                                <asp:RadioButton ID="rdbAll" runat="server" Text="AsOnDate" GroupName="AS" OnCheckedChanged="rdbAll_CheckedChanged" AutoPostBack="true"></asp:RadioButton>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:RadioButton ID="rdbSpecific" runat="server" Text="DateWise" GroupName="AS" AutoPostBack="true" OnCheckedChanged="rdbSpecific_CheckedChanged"></asp:RadioButton>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <asp:RadioButton ID="rdbAllRecSrno" runat="server" Text="RecSrNo" GroupName="AS" OnCheckedChanged="rdbAll_CheckedChanged" AutoPostBack="true"></asp:RadioButton>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:RadioButton ID="rbdDepWise" runat="server" Text="Div / Dep Wise" GroupName="AS" OnCheckedChanged="rdbAll_CheckedChanged" AutoPostBack="true"></asp:RadioButton>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-9">

                                                        <div class="col-md-2">
                                                            <label class="control-label">Product Type</label>
                                                        </div>
                                                        <div class="col-md-2">

                                                            <asp:TextBox ID="TxtPType" runat="server" Placeholder="Product Type" CssClass="form-control" OnTextChanged="TxtPType_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-5">
                                                            <asp:TextBox ID="TxtProName" placeholder="PRODUCT NAME" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="TxtProName_TextChanged" TabIndex="3"></asp:TextBox>
                                                            <div id="CustList" style="height: 200px; overflow-y: scroll;"></div>
                                                            <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="TxtProName"
                                                                UseContextKey="true"
                                                                CompletionInterval="1"
                                                                CompletionSetCount="20"
                                                                MinimumPrefixLength="1"
                                                                EnableCaching="true"
                                                                ServicePath="~/WebServices/Contact.asmx"
                                                                ServiceMethod="getglname" CompletionListElementID="CustList">
                                                            </asp:AutoCompleteExtender>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-9">
                                                        <div runat="server" id="FDT">
                                                            <div class="col-md-2">
                                                                <label class="control-label">From Date</label>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="TxtFDate" CssClass="form-control" PlaceHolder="dd/mm/yyyy" runat="server" onkeyup="FormatIT(this)"></asp:TextBox>
                                                                <asp:TextBoxWatermarkExtender ID="TxtFDT_WatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtFDate">
                                                                </asp:TextBoxWatermarkExtender>
                                                                <asp:CalendarExtender ID="TxtFDT_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtFDate">
                                                                </asp:CalendarExtender>
                                                            </div>
                                                        </div>

                                                        <div runat="server" id="TDT">
                                                            <div class="col-md-2">
                                                                <label class="control-label">To Date</label>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="TxtTDate" CssClass="form-control" PlaceHolder="dd/mm/yyyy" runat="server" onkeyup="FormatIT(this)"></asp:TextBox>
                                                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtTDate">
                                                                </asp:TextBoxWatermarkExtender>
                                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtTDate">
                                                                </asp:CalendarExtender>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-lg-12" style="text-align: left; margin-top: 12px; margin-bottom: 13px; margin-left: 15px;">
                                                        <div class="col-md-2">
                                                            <asp:Button ID="CustBook" runat="server" Text="Cut Book" OnClick="CustBook_Click" CssClass="btn btn-primary" />
                                                            <br />
                                                        </div>
                                                        <div class="col-lg-6">
                                                            <asp:Button ID="Report" runat="server" Text="Cut Book Report" OnClick="Report_Click" CssClass="btn btn-success" />
                                                            <asp:Button ID="TextReport" runat="server" CssClass="btn btn-primary" Text="Download Text Report" OnClick="TextReport_Click" />
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
                    <div class="table-scrollable" style="height: 450px; overflow-x: scroll; overflow-y: scroll; padding-bottom: 10px;">
                        <table class="table table-striped table-bordered table-hover">
                            <thead>
                                <tr>
                                    <th>
                                        <asp:GridView ID="GrdAccountSTS" runat="server"
                                            AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                            EditRowStyle-BackColor="#FFFF99"
                                            PagerStyle-CssClass="pgr" Width="100%">
                                            <Columns>
                                                <%--<asp:TemplateField HeaderText="Srno" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>--%>

                                                <asp:TemplateField HeaderText="Memmber Type" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="MType" runat="server" Text='<%# Eval("MemType") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Accno">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Accno" runat="server" Text='<%# Eval("Accno") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Cust No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Custno" runat="server" Text='<%# Eval("Custno") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Member Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="MNAme" runat="server" Text='<%# Eval("Custname") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="Th" HorizontalAlign="Left" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="DEBIT Balance">
                                                    <ItemTemplate>
                                                        <asp:Label ID="DEBIT" runat="server" Text='<%# Eval("DR") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Credit Balance" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="CREDIT" runat="server" Text='<%# Eval("CR") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>



