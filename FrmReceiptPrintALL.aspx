<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmReceiptPrintALL.aspx.cs" Inherits="FrmReceiptPrintALL" %>

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
    </script>
    <script type="text/javascript">
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
                        Voucher Printing
                        <asp:Literal ID="ltrlHeader" runat="server"></asp:Literal>
                    </div>
                </div>
                <div class="portlet-body form">

                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab__blue">
                                    </div>
                                    <div style="border: 1px solid #3598dc">
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <asp:RadioButtonList ID="Rdeatils" RepeatDirection="Horizontal" Style="width: 380px;" runat="server">
                                                        <asp:ListItem Text="Receipt 1" Value="1" />
                                                        <asp:ListItem Text="Receipt 2" Value="2" />
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label ">Branch Code</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtBrID" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label">From Date <span class="required"></span></label>
                                                </div>
                                                <div class="col-lg-2">
                                                    <asp:TextBox ID="TxtFDate" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <asp:CalendarExtender ID="TxtDate_CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtFDate">
                                                    </asp:CalendarExtender>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label ">Set No</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtsetno" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                    <div class="col-lg-12" style="text-align: center">
                                        <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-primary" Text="Report Print" OnClick="btnPrint_Click" OnClientClick="Javascript:return Validate();" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div style="border: 1px solid #3598dc">
                            <div class="row" style="margin: 7px 0 7px 0">
                                <div class="col-lg-12" style="height: 50%">
                                    <div class="table-scrollable" style="height: 350px; overflow-x: scroll; overflow-y: scroll; padding-bottom: 10px;">
                                        <asp:GridView ID="GrdView" runat="server" AutoGenerateColumns="false" ShowFooter="true" OnRowDataBound="GrdView_RowDataBound" OnSelectedIndexChanged="GrdView_SelectedIndexChanged">
                                            <Columns>
                                                <asp:TemplateField HeaderText="VOUCHER NO" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="SETNO" runat="server" Text='<%# Eval("SETNO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="On Date" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ENTRYDATE" runat="server" Text='<%# Eval("ENTRYDATE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="SUBGLCODE" HeaderText="Product Code" />
                                                <asp:BoundField DataField="ACCNO" HeaderText="A/C No" />
                                                <asp:BoundField DataField="CUSTNAME" HeaderText="Name" />
                                                <asp:BoundField DataField="PARTICULARS" HeaderText="Particulars" />

                                                <asp:TemplateField HeaderText="Amount" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="AMOUNT" runat="server" Text='<%# Eval("AMOUNT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Type" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="TYPE" runat="server" Text='<%# Eval("TYPE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ACTIVITY" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ACTIVITY" runat="server" Text='<%# Eval("ACTIVITY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="BRCD" HeaderText="Br. Code" />
                                                <asp:BoundField DataField="STAGE" HeaderText="Status" />
                                                <asp:BoundField DataField="LOGINCODE" HeaderText="User Code" />
                                                <asp:TemplateField HeaderText="Maker" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="MID" runat="server" Text='<%# Eval("MID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Checker" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="CID" runat="server" Text='<%# Eval("VID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cancel by" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="VID" runat="server" Text='<%# Eval("CID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Receipt" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LnkPrintReceipt" runat="server" CommandName="select" CommandArgument='<%# Eval("SUBGLCODE")+","+Eval("ACCNO")%>' class="glyphicon glyphicon-plus" OnClick="LnkPrintReceipt_Click"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle CssClass="pgr"></PagerStyle>
                                            <SelectedRowStyle BackColor="#66FF99" />
                                            <EditRowStyle BackColor="#FFFF99"></EditRowStyle>
                                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                        </asp:GridView>
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

