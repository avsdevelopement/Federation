<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmDayClose.aspx.cs" Inherits="FrmDayClose" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
                        Day Close
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
                                        <div class="row" style="margin-bottom: 10px;">
                                            <div class="col-lg-6">
                                                <div class="col-md-3">
                                                    <asp:RadioButtonList ID="Rdb_No" runat="server" RepeatDirection="Horizontal" Style="width: 300px;" OnSelectedIndexChanged="Rdb_No_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Text="All Branch" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Specific Branch" Value="2"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row" id="Div_SPECIFIC" runat="server">
                                    <div class="row" style="margin-bottom: 10px;">
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Branch Name:<span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtBrCode" onkeypress="javascript:return isNumber (event)" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-lg-3">
                                                    <asp:DropDownList ID="ddlBrName" CssClass="form-control" OnSelectedIndexChanged="ddlBrName_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">From Date:<span class="required"></span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtfromdate" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="dd/MM/yyyy" runat="server"></asp:TextBox>
                                                    <asp:TextBoxWatermarkExtender ID="TxtDate_WatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtfromdate">
                                                    </asp:TextBoxWatermarkExtender>
                                                    <asp:CalendarExtender ID="TxtDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtfromdate">
                                                    </asp:CalendarExtender>
                                                </div>
                                                <label class="control-label col-md-1">To Date:<span class="required"></span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txttodate" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="dd/MM/yyyy" runat="server"></asp:TextBox>
                                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txttodate">
                                                    </asp:TextBoxWatermarkExtender>
                                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txttodate">
                                                    </asp:CalendarExtender>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row" id="Div_ALL" runat="server">
                                    <div class="row" style="margin-bottom: 10px;">
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">From Date:<span class="required"></span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtfrmdateS" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="dd/MM/yyyy" runat="server"></asp:TextBox>
                                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtfrmdateS">
                                                    </asp:TextBoxWatermarkExtender>
                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtfrmdateS">
                                                    </asp:CalendarExtender>
                                                </div>
                                                <label class="control-label col-md-1">To Date:<span class="required"></span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txttodateS" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="dd/MM/yyyy" runat="server"></asp:TextBox>
                                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txttodateS">
                                                    </asp:TextBoxWatermarkExtender>
                                                    <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txttodateS">
                                                    </asp:CalendarExtender>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row" id="Div_Buttons" runat="server">
                                    <div class="row" style="margin: 7px 0 7px 0; text-align: center">
                                        <div class="col-lg-12">
                                            <div class="col-md-6">
                                                <asp:Button ID="BtnSubmit" runat="server" Text="View" CssClass="btn btn-success" OnClick="BtnSubmit_Click" OnClientClick="javascript:return validate();" />
                                                <asp:Button ID="btnview" runat="server" Text="Submit" CssClass="btn btn-success" OnClick="btnview_Click" OnClientClick="javascript:return validate();" /> 
                                                <asp:Button ID="BtnClear" runat="server" Text="Clear" CssClass="btn btn-success" OnClick="BtnClear_Click" OnClientClick="javascript:return validate();" />
                                                <asp:Button ID="BtnExit" runat="server" Text="Exit" CssClass="btn btn-success" OnClick="BtnExit_Click" OnClientClick="javascript:return validate();" />
                                                
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




    <div class="row" style="margin: 7px 0 7px 0">
        <div class="col-lg-12" style="height: 50%">
            <div class="table-scrollable" style="height: 350px; overflow-x: scroll; overflow-y: scroll; padding-bottom: 10px; width: 66%">
                <table class="table table-striped table-bordered table-hover" width="100%">
                    <thead>
                        <tr>
                            <th>
                                <asp:GridView ID="grddayactivity" runat="server" CssClass="tabbable-line" HeaderStyle-BackColor="#1e9275" HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Center"
                                    AutoGenerateColumns="false" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr. No." Visible="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Start Date" Visible="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="SD" runat="server" Text='<%# Eval("DayBeginDate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Branch Code" Visible="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="BRCD" runat="server" Text='<%# Eval("BRCD") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Branch Name" Visible="true" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="BRANCHNAME" runat="server" Text='<%# Eval("BRANCHNAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Status" Visible="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="STATUS" runat="server" Text='<%# Eval("STATUS") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Remark" Visible="true" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="TIME" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
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
    <div id="divgrd" class="row" runat="server">
        <div class="col-lg-12">
            <div class="table-scrollable">
                <table class="table table-striped table-bordered table-hover">
                    <thead>
                        <tr>
                            <th>
                                <asp:GridView ID="GrdBranchH" runat="server" AllowPaging="True" AlternatingRowStyle-CssClass="alt"
                                    AutoGenerateColumns="true" EditRowStyle-BackColor="#FFFF99" PageIndex="10" PageSize="25" PagerStyle-CssClass="pgr" Width="100%" OnPageIndexChanging="GrdBranchH_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkSelect" runat="server" CommandArgument='<%#Eval("AFLAG")%>' CommandName="select" OnClick="lnkSelect_Click" class="glyphicon glyphicon-check"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </th>
                        </tr>
                    </thead>
                </table>
            </div>
        </div>
    </div>
</asp:Content>

