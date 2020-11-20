﻿<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmUnoperativeAccountReport.aspx.cs" Inherits="FrmUnoperativeAccountReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<script>
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        Un-Operative Accounts List Report
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
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">As On Date <span class="required">*</span></label>
                                                <div class="col-lg-3">
                                                    <asp:TextBox ID="txtAsOnDate" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber(event)" placeholder="dd/mm/yyyy" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <asp:TextBoxWatermarkExtender ID="TxtDate_WatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtAsOnDate">
                                                    </asp:TextBoxWatermarkExtender>
                                                    <asp:CalendarExtender ID="TxtFDT_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtAsOnDate">
                                                    </asp:CalendarExtender>
                                                </div>
                                           </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Greater Than Month <span class="required">*</span></label>
                                                <div class="col-lg-3">
                                                    <asp:TextBox ID="txtMonth" onkeypress="javascript:return isNumber(event)" placeholder="MM" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Fron Branch <span class="required">*</span></label>
                                                <div class="col-lg-2">
                                                    <asp:TextBox ID="txtFromBr" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-2">To Branch <span class="required">*</span></label>
                                                <div class="col-lg-2">
                                                    <asp:TextBox ID="txtToBr" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9">
                                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn blue" Text="Genarate" OnClick="btnSubmit_Click" OnClientClick="Javascript:return isvalidate();" />
                                        <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-primary" Text="Print" OnClick="btnPrint_Click" OnClientClick="Javascript:return Validate();" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                    </div>
                    <div class="col-md-12">
                        <table class="table table-striped table-bordered table-hover" width="100%">
                            <thead>
                                <tr>
                                    <th>
                                        <asp:GridView ID="grdUnOperativeAccts" runat="server" AllowPaging="True" AlternatingRowStyle-CssClass="alt" CssClass="mGrid" AutoGenerateColumns="false"
                                            EditRowStyle-BackColor="#FFFF99" OnPageIndexChanging="grdUnOperativeAccts_PageIndexChanging" PagerStyle-CssClass="pgr" Width="100%">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr No" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Cust No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="CUSTNO" runat="server" Text='<%# Eval("CUSTNO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Acc No" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ACCNO" runat="server" Text='<%# Eval("ACCNO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Cust Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="CUSTNAME" runat="server" Text='<%# Eval("CUSTNAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Balance">
                                                    <ItemTemplate>
                                                        <asp:Label ID="OPBAL" runat="server" Text='<%# Eval("OPBAL") %>'></asp:Label>
                                                    </ItemTemplate>
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

