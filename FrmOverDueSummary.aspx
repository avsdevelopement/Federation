<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmOverDueSummary.aspx.cs" Inherits="FrmOverDueSummary" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12">
                    <div class="portlet box blue" id="Div1">
                        <div class="portlet-title">
                            <div class="caption">
                                 Overdue Summary Report
                            </div>
                        </div>
                        <div class="portlet-body form">
                            <div class="form-horizontal">
                                <div class="form-wizard">
                                    <div class="form-body">
                                         <div class="tab-content">
                                            <div id="error">
                                            </div>
                                            <div class="tab-pane active" id="tab1">
                                                 <div class="row">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #fa4e0d"></strong></div>
                                                    </div>
                                                </div>

                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label">AsOn Date <span class="required">*</span></label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="TxtDate" CssClass="form-control" PlaceHolder="dd/mm/yyyy" runat="server" onkeyup="FormatIT(this)"></asp:TextBox>
                                                            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtDate">
                                                            </asp:TextBoxWatermarkExtender>
                                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtDate">
                                                            </asp:CalendarExtender>
                                                        </div>
                                                    </div>
                                                </div>

                                                <%--   <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">

                                                        <label class="control-label col-md-2">A/C Type</label>

                                                        <div class="col-md-9">
                                                            <asp:RadioButtonList ID="Rdb_AccType" runat="server" RepeatDirection="Horizontal" CssClass="radio-list" OnSelectedIndexChanged="Rdb_AccType_SelectedIndexChanged" AutoPostBack="true">
                                                                <asp:ListItem Text="Only Court File" Value="1" style="margin: 15px;" Selected="True"> </asp:ListItem>
                                                                <asp:ListItem Text="No Court Files" Value="2" style="margin: 25px;"> </asp:ListItem>
                                                                <asp:ListItem Text="All A/C's" Value="3" style="margin: 25px;"> </asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </div>
                                                </div>--%>
                                                 </div>
                                                </div>
                                            </div>

                                     <div class="form-actions">
                                        <div class="row">
                                            <div class="col-md-offset-3 col-md-9">
                                                 <asp:Button ID="Btn_Grid" runat="server" Text="Display" CssClass="btn btn-success" OnClick="Btn_Grid_Click" />
                                                <asp:Button ID="Btn_Report" runat="server" Text="Report" CssClass="btn btn-success" OnClick="Btn_Report_Click" />
                                                <asp:Button ID="Btn_Exit" runat="server" Text="Exit" CssClass="btn btn-success" OnClick="Btn_Exit_Click" />
                                            </div>
                                        </div>
                                    </div>
                                      </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                </div>
             </ContentTemplate>
    </asp:UpdatePanel>

     
            <div class="row">
                <div class="col-md-12">
                    <div class="portlet box blue" id="form_wizard_1">
                        <div class="portlet-body form">
                            <div class="form-horizontal">
                                <div class="form-wizard">
                                    <div class="form-body">
                                        <div class="tab-content">
                                            <div class="portlet-body" style="height: 400px; overflow-y: auto">
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Detail Information : </strong></div>
                                                    </div>
                                                </div>

                                                <asp:GridView ID="grdDetails" runat="server" AllowPaging="True" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                    EditRowStyle-BackColor="#FFFF99" OnPageIndexChanging="grdDetails_PageIndexChanging" PagerStyle-CssClass="pgr" Width="100%" >
                                                    <Columns>

                                                        <asp:TemplateField HeaderText="Sr No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Product Code">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSubGl" runat="server" Text='<%# Eval("LOANGLCODE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Product Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDesc" runat="server" Text='<%# Eval("GLNAME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="No Of A/C">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAccNo" runat="server" Text='<%# Eval("Total_Accounts") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Balance">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Total_Closing") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                         <asp:TemplateField HeaderText="Loan OD Acc No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblODNo" runat="server" Text='<%# Eval("No_ofODAcc") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                         <asp:TemplateField HeaderText="Loan OD Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblODAmt" runat="server" Text='<%# Eval("OD_Amount") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                         <asp:TemplateField HeaderText="Loan OD Interest">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblODInt" runat="server" Text='<%# Eval("Recv_Int") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                     </Columns>
                                                    <PagerStyle CssClass="pgr" />
                                                    <SelectedRowStyle BackColor="#66FF99" />
                                                    <EditRowStyle BackColor="#FFFF99" />
                                                    <AlternatingRowStyle CssClass="alt" />
                                                </asp:GridView>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                            </div>
                        </div>
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
            </div>
       
</asp:Content>

