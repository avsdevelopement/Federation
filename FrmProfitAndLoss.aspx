<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmProfitAndLoss.aspx.cs" Inherits="FrmProfitAndLoss" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function FormatIt(obj)
        {
            if (obj.value.length == 2) // Day
                obj.value = obj.value + "/";
            if (obj.value.length == 5) // month 
                obj.value = obj.value + "/";
            if (obj.value.length == 11) // year 
                alert("Please Enter valid Date");
        }

        function Validate()
        {
            var TxtFDT = document.getElementById('<%=TxtFDT.ClientID%>').value;

            if (TxtFDT == "")
            {
                alert("Please Enter Date........!!");
                return false;
            }
        }

        function UserConfirmation()
        {
            if (confirm("Are you sure you want to download?"))
            {
                __doPostBack(TextReport, '');
            }

            return false;
        }
    </script>
    <script>
        function isNumber(evt)
        {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;
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
                                Profit And Loss
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

                                                <div style="border: 1px solid #3598dc">
                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-2">
                                                                <label class="control-label">Branch Code</label>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="TxtBrID" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>

                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtCurrentLang"  CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin: 7px 0 7px 0" align="center">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-8">
                                                                <asp:RadioButtonList ID="RBSel" Style="width: 800px;" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="RBSel_SelectedIndexChanged" AutoPostBack="true">
                                                                    <asp:ListItem Value="1">&nbsp;As On Date</asp:ListItem>
                                                                    <asp:ListItem Value="2" style="padding-left: 10px">&nbsp;N Form PL</asp:ListItem>
                                                                    <asp:ListItem Value="3" style="padding-left: 10px">&nbsp;Income / Expenture</asp:ListItem>
                                                                    <asp:ListItem Value="4" style="padding-left: 10px">&nbsp;Admin Expenses</asp:ListItem>
                                                                    <asp:ListItem Value="5" style="padding-left: 10px">&nbsp;PL Marathi</asp:ListItem>
                                                                    <asp:ListItem Value="6" style="padding-left: 10px">&nbsp;N Form PL Marathi</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div runat="server" id="divOnDate" visible="false">
                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-12">
                                                                <div class="col-md-2">
                                                                    <label class="control-label">As On Date</label>
                                                                </div>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="TxtFDT" Width="75%" CssClass="form-control" onkeyup="FormatIt(this)" PlaceHolder="dd/mm/yyyy" runat="server"></asp:TextBox>
                                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtFDT">
                                                                    </asp:CalendarExtender>
                                                                </div>

                                                                <div class="col-lg-7">
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-lg-12" style="text-align: left; margin-top: 12px; margin-bottom: 13px; margin-left: 15px;">
                                                                <div class="col-lg-6">
                                                                    <asp:Button ID="Submit" runat="server" Text="Show" CssClass="btn btn-primary" OnClick="Submit_Click" />
                                                                    <asp:Button ID="Report" runat="server" Text="Profit Loss Report" OnClick="Report_Click" CssClass="btn btn-primary" />
                                                                    <asp:Button ID="TextReport" runat="server" CssClass="btn btn-primary" Text="Download Text Report" OnClick="TextReport_Click"
                                                                        OnClientClick="return UserConfirmation();" />
                                                                    <br />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div runat="server" id="divDate" visible="false">
                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-12">
                                                                <div class="col-md-2">
                                                                    <label class="control-label">From Date</label>
                                                                </div>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="txtFromDate" Width="75%" CssClass="form-control" onkeyup="FormatIt(this)" PlaceHolder="dd/mm/yyyy" runat="server"></asp:TextBox>
                                                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtFromDate">
                                                                    </asp:TextBoxWatermarkExtender>
                                                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtFromDate">
                                                                    </asp:CalendarExtender>
                                                                </div>
                                                                <div class="col-md-2">
                                                                    <label class="control-label">To Date</label>
                                                                </div>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="txtToDate" Width="75%" CssClass="form-control" onkeyup="FormatIt(this)" PlaceHolder="dd/mm/yyyy" runat="server"></asp:TextBox>
                                                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtToDate">
                                                                    </asp:TextBoxWatermarkExtender>
                                                                    <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtToDate">
                                                                    </asp:CalendarExtender>
                                                                </div>
                                                                <div class="col-lg-7">
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-lg-12" style="text-align: left; margin-top: 12px; margin-bottom: 13px; margin-left: 15px;">
                                                                <div class="col-lg-6">
                                                                    <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-primary" OnClick="btnShow_Click" />
                                                                    <asp:Button ID="btnBal" runat="server" Text="Profit Loss Report" OnClick="btnBal_Click" CssClass="btn btn-primary" />
                                                                    <asp:Button ID="btnDown" runat="server" CssClass="btn btn-primary" Text="Download Text Report" OnClick="btnDown_Click"
                                                                        OnClientClick="return UserConfirmation();" />
                                                                    <br />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                    <div style="border: 1px solid #3598dc">
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12" style="height: 50%">
                                                <div class="table-scrollable" style="height: 350px; overflow-x: scroll; overflow-y: scroll; padding-bottom: 10px;">
                                                    <table class="table table-striped table-bordered table-hover">
                                                        <thead>
                                                            <tr>
                                                                <th>
                                                                    <asp:GridView ID="GrdProfitLoss" runat="server" AllowPaging="True"
                                                                        AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                                        EditRowStyle-BackColor="#FFFF99"
                                                                        OnPageIndexChanging="GrdProfitAndLoss_PageIndexChanging"
                                                                        PageIndex="10" PageSize="25"
                                                                        PagerStyle-CssClass="pgr" Width="100%">
                                                                        <Columns>

                                                                            <asp:TemplateField HeaderText="ID" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="ID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Product Code">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSubGlCode" runat="server" Text='<%# Eval("ESUBGL") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Product Name">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblGlName" runat="server" Text='<%# Eval("EGLNM") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Amount" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblOpBal" runat="server" Text='<%# Eval("EAMOUNT") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Product Code" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCrBal" runat="server" Text='<%# Eval("ISUBGL") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Product Name">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblDrBal" runat="server" Text='<%# Eval("IGLNM") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Amount">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblClBal" runat="server" Text='<%# Eval("IAMOUNT") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="Th" HorizontalAlign="Right" />
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
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:HiddenField ID="hdnFlag" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

