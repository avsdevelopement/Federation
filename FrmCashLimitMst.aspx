<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmCashLimitMst.aspx.cs" Inherits="FrmCashLimitMst" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        CASH LIMIT MASTER
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
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-md-12">
                                                <label class="control-label col-md-1">Prod Code:<span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtPrd" Placeholder="Product Code" CssClass="form-control" runat="server" OnTextChanged="TxtPrd_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtProdName" CssClass="form-control" runat="server" AutoPostBack="true" Placeholder="Product Name" OnTextChanged="TxtProdName_TextChanged"></asp:TextBox>
                                                    <div id="ProdList" style="height: 200px; overflow-y: scroll;"></div>
                                                    <asp:AutoCompleteExtender ID="autoFglname" runat="server" TargetControlID="TxtProdName"
                                                        UseContextKey="true"
                                                        CompletionInterval="1"
                                                        CompletionSetCount="20"
                                                        MinimumPrefixLength="1"
                                                        EnableCaching="true"
                                                        ServicePath="~/WebServices/Contact.asmx"
                                                        ServiceMethod="getglname" CompletionListElementID="ProdList">
                                                    </asp:AutoCompleteExtender>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-1">Effective Date: <span class="required"></span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtEffectDate" CssClass="form-control" PlaceHolder="dd/mm/yyyy" runat="server" onkeyup="FormatIt(this)"></asp:TextBox>
                                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtEffectDate">
                                                    </asp:TextBoxWatermarkExtender>
                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtEffectDate">
                                                    </asp:CalendarExtender>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-1">Limit: <span class="required"></span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtLimit" CssClass="form-control" PlaceHolder="Limit" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 20px; border-bottom: 1px solid rgba(53, 152, 220, 0.55);"><strong></strong></div>
                                        <div class="row" style="margin: 7px 0 7px 0; text-align: center">
                                            <div class="col-lg-12">
                                                <div class="col-md-6">

                                                    <asp:Button ID="Btn_Submit" runat="server" Text="Submit" CssClass="btn btn-success" OnClick="Btn_Submit_Click" />
                                                    <asp:Button ID="Btn_Modify" runat="server" Text="Modify" CssClass="btn btn-success" OnClick="Btn_Modify_Click" Visible="false" />
                                                    <asp:Button ID="Btn_Delete" runat="server" Text="Delete" CssClass="btn btn-success" OnClick="Btn_Delete_Click" Visible="false" />
                                                    <asp:Button ID="Btn_ClearAll" runat="server" Text="Clear All" CssClass="btn btn-success" OnClick="Btn_ClearAll_Click" />
                                                    <asp:Button ID="Btn_Exit" runat="server" Text="Exit" CssClass="btn btn-success" OnClick="Btn_Exit_Click" />
                                                    
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 10px;"><strong></strong></div>
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12" style="height: 50%">
                                                <div class="table-scrollable" style="height: auto; overflow-y: scroll; padding-bottom: 10px;">
                                                    <table class="table table-striped table-bordered table-hover" width="100%">
                                                        <thead>
                                                            <tr>
                                                                <th>
                                                                    <asp:GridView ID="grdCashLimit" runat="server"
                                                                        AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                                        EditRowStyle-BackColor="#FFFF99"
                                                                        PagerStyle-CssClass="pgr" Width="100%" OnSelectedIndexChanged="grdCashLimit_SelectedIndexChanged">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="SRNO" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="serial" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>


                                                                            <asp:TemplateField HeaderText="ID" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="ID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="SUBGLCODE">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="SUBGLCODE" runat="server" Text='<%# Eval("SUBGLCODE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="EFFECTIVEDATE">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="EFFECTIVEDATE" runat="server" Text='<%# Eval("EFFECTIVEDATE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="LIMIT">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="LIMIT" runat="server" Text='<%# Eval("LIMIT") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Add new" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkAdd" runat="server" CommandName="select" OnClick="lnkAdd_Click" CommandArgument='<%#Eval("ID")%>' class="glyphicon glyphicon-plus"></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                             <asp:TemplateField HeaderText="Modify" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkModify" runat="server" CommandName="select" OnClick="lnkModify_Click" CommandArgument='<%#Eval("ID")%>' class="glyphicon glyphicon-plus"></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Delete" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkDelete" runat="server" class="glyphicon glyphicon-trash" CommandArgument='<%#Eval("ID")%>' OnClick="lnkDelete_Click" CommandName="select"></asp:LinkButton>
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

