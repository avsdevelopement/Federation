<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CBSMaster.master" CodeFile="FrmInv_RegRpt.aspx.cs" Inherits="FrmInv_RegRpt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>

        function FormatIt(obj) {
            if (obj.value.length == 2) //DAY
                obj.value = obj.value + "/";
            if (obj.value.length == 5) //MONTH
                obj.value = obj.value + "/";
            if (obj.value.length == 11) //YEAR
                alert("Enter Valid Date!....");
        }
    </script>
    <script>
        function dateLen(dt) {
            var dt1 = dt + '';
            if (dt1.length == 1)
                dt1 = '0' + dt;

            return dt1;
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
                                Investment Register Report
                            </div>
                        </div>
                        <div class="portlet-body form">
                            <div class="form-horizontal">
                                <div class="form-wizard">
                                    <div class="form-body">
                                        <div class="tab-content">
                                            <div style="border: 1px solid #3598dc">
                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-6" align="center">
                                                            <asp:RadioButtonList ID="RdblSelection" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"  OnSelectedIndexChanged= "RdblSelection_SelectedIndexChanged">
                                                                <asp:ListItem Value="0" style="margin: 10px">All</asp:ListItem>
                                                                <asp:ListItem Value="1" style="margin: 10px">Specific</asp:ListItem>
                                                                <asp:ListItem Value="2" style="margin: 10px">Group wise</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label">As On Date:</label>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtfromdate" CssClass="form-control" type="text" PlaceHolder="dd/mm/yyyy" runat="server" onkeyup="FormatIt(this)"></asp:TextBox>
                                                            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtfromdate">
                                                            </asp:TextBoxWatermarkExtender>
                                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtfromdate">
                                                            </asp:CalendarExtender>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row" style="margin: 7px 0 7px 0" id="DivShow" runat="server" visible="false">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label">Prod Code:</label>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtfdepositgl" Placeholder=" Enter Deposit GL " runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>

                                                    </div>
                                                </div>
                                                 <div class="row" style="margin: 7px 0 7px 0" id="DivGroup" runat="server" visible="false">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label">Inv Group:</label>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <asp:DropDownList ID="ddlInv" runat="server" CssClass="form-control">

                                                            </asp:DropDownList>
                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12" align="center">
                                                     <div class="col-md-12">
                                                             <asp:Button ID="BtnPrint" runat="server" CssClass="btn btn-primary" Text="View" OnClick= "BtnPrint_Click" />
                                                            <asp:Button ID="BtnReport" runat="server" CssClass="btn btn-primary" Text="Report" OnClick=  "BtnReport_Click" />
                                                            <asp:Button ID="BtnExit" runat="server" CssClass="btn btn-primary" Text="Exit" OnClick= "BtnExit_Click" />
                                                           </div>
                                                        <div class="col-md-5">
                                                        </div>
                                                    </div>
                                                </div>
                                                <asp:HiddenField ID="hdnFlag" runat="server" />
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
                        <table class="table table-striped table-bordered table-hover">
                            <asp:Label ID="Lbl_Sts" runat="server" Text="Investment Reg" Style="font-size: medium;"></asp:Label>
                            <thead>
                                <tr>
                                    <th>
                                        <asp:GridView ID="GrdInv" runat="server" ShowFooter="true"
                                            AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                            EditRowStyle-BackColor="#FFFF99" HeaderStyle-HorizontalAlign="Center"
                                            onrowdatabound="GrdInv_RowDataBound"
                                            PagerStyle-CssClass="pgr" Width="100%">
                                            <Columns>
                                                <%--<asp:TemplateField HeaderText="Sr No" ItemStyle-HorizontalAlign="right">
                                                    <ItemTemplate>
                                                        <%#Eval("ID") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Product Code" >
                                                    <ItemTemplate>
                                                        <%#Eval("PRODUCTCODE") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Acc No">
                                                    <ItemTemplate>
                                                        <%#Eval("ACCNO") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date of Inv" ItemStyle-HorizontalAlign="center">
                                                    <ItemTemplate>
                                                        <%#Eval("INVDATE") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Inv Amt" ItemStyle-HorizontalAlign="right">
                                                    <ItemTemplate>
                                                        <%#Eval("INVAMT") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Int Amt" ItemStyle-HorizontalAlign="right">
                                                    <ItemTemplate>
                                                        <%#Eval("INTAMT") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Maturity Amt" ItemStyle-HorizontalAlign="right">
                                                    <ItemTemplate>
                                                        <%#Eval("MATURITYAMT") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Rate" ItemStyle-HorizontalAlign="right">
                                                    <ItemTemplate>
                                                        <%#Eval("RATE") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Period" ItemStyle-HorizontalAlign="right">
                                                    <ItemTemplate>
                                                        <%#Eval("PERIOD") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Closing Bal" ItemStyle-HorizontalAlign="right" FooterStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <%#Eval("CLOSINGBAL") %>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblTotal" runat="server" ></asp:Label>
                                                    </FooterTemplate>
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

