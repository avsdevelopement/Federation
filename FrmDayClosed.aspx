<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmDayClosed.aspx.cs" Inherits="FrmDayClosed" %>

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

    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="Div1">
                <div class="portlet-title">
                    <div class="caption">
                        Branch Handover / Day Close
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab1">

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <div class="col-lg-3">
                                                </div>
                                                <asp:RadioButton ID="rbtnBranchHandover" Text="Branch Handover" GroupName="BH" OnCheckedChanged="rbtnBranchHandover_CheckedChanged" runat="server" AutoPostBack="true" />
                                                <asp:RadioButton ID="rbtnDayClose" Text="Day Close" GroupName="BH" OnCheckedChanged="rbtnDayClose_CheckedChanged" runat="server" AutoPostBack="true" />
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Branch Name  <span class="required">*</span></label>
                                                <div class="col-lg-4">
                                                    <asp:DropDownList ID="ddlBrName" CssClass="form-control" OnSelectedIndexChanged="ddlBrName_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-lg-2">
                                                    <asp:TextBox ID="txtBrCode" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Working Date  <span class="required">*</span></label>
                                                <div class="col-lg-2">
                                                    <asp:TextBox ID="txtWorkDate" Enabled="false" onkeyup="FormatIt(this);" onkeypress="javascript:return isNumber(event)" placeholder="DD/MM/YYYY" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <asp:CalendarExtender ID="txtWorkDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtWorkDate">
                                                    </asp:CalendarExtender>
                                                </div>
                                                <label class="control-label col-md-2">Open Date </label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtOpenDate" Enabled="false" onkeyup="FormatIt(this);" onkeypress="javascript:return isNumber(event)" placeholder="DD/MM/YYYY" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <asp:CalendarExtender ID="txtOpenDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtOpenDate">
                                                    </asp:CalendarExtender>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9">
                                        <asp:Button ID="btnHandover" Visible="false" runat="server" CssClass="btn btn-primary" Text="Branch Handover" UseSubmitBehavior="false" OnClick="btnHandover_Click" OnClientClick="this.disabled='true';this.value='Please Wait'"/>
                                        <asp:Button ID="btnDayClose" Visible="false" runat="server" CssClass="btn btn-primary" Text="Day Close" UseSubmitBehavior="false" OnClick="btnDayClose_Click" OnClientClick="this.disabled='true';this.value='Please Wait'"/>
                                        <asp:Button ID="btnExit" runat="server" CssClass="btn btn-primary" Text="Exit" UseSubmitBehavior="false" OnClick="btnExit_Click" OnClientClick="this.disabled='true';this.value='Please Wait'"/>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="divgrd" class="row" runat="server">
        <div class="row" style="margin: 7px 0 7px 0">
            <div class="col-lg-12">
                <label class="control-label col-md-6">Unpass Details (Authorise First) </label>
            </div>
        </div>
        <div class="col-lg-12">
            <div class="table-scrollable">
                <table class="table table-striped table-bordered table-hover">
                    <thead>
                        <tr>
                            <th>
                                <asp:GridView ID="GrdBranchH" runat="server" AllowPaging="True" AlternatingRowStyle-CssClass="alt"
                                    AutoGenerateColumns="true" EditRowStyle-BackColor="#FFFF99" PageIndex="10" PageSize="25" PagerStyle-CssClass="pgr" Width="100%" OnPageIndexChanging="GrdBranchH_PageIndexChanging">
                                    <Columns>
                                        <%--<asp:TemplateField HeaderText="Select" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkSelect" runat="server" CommandArgument='<%#Eval("AFLAG")%>' CommandName="select" OnClick="lnkSelect_Click" class="glyphicon glyphicon-check"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
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

