<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmCDR.aspx.cs" Inherits="FrmCDR" %>

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

        function isNumber(evt) {
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
                                CD Ratio 
                            </div>
                        </div>
                        <div class="portlet-body form">
                            <div class="form-horizontal">
                                <div class="form-wizard">
                                    <div class="form-body">
                                        <div class="tab-content">
                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">

                                                        <label class="control-label col-md-2">Report Type</label>

                                                        <div class="col-md-9">
                                                            <asp:RadioButtonList ID="Rdb_AccType" runat="server" RepeatDirection="Horizontal" CssClass="radio-list" OnSelectedIndexChanged=  "Rdb_AccType_SelectedIndexChanged" AutoPostBack="true">
                                                                <asp:ListItem Text="Summary" Value="1" style="margin: 25px;" Selected="True"> </asp:ListItem>
                                                                <asp:ListItem Text="Details" Value="2" style="margin: 15px;"> </asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </div>
                                                </div>
                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <label class="control-label">Branch Code</label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtBrID" OnTextChanged="TxtBrID_TextChanged" AutoPostBack="true" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtBrName" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <label class="control-label">As On Date</label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtOnDate" CssClass="form-control" onkeyup="FormatIT(this)" PlaceHolder="DD/MM/YYYY" runat="server"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtOnDate">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="form-actions">
                                        <div class="row">
                                            <div class="col-md-offset-3 col-md-9">
                                                <asp:Button ID="BtnReport" runat="server" Text="Report" CssClass="btn btn-primary" OnClick="BtnReport_Click" />
                                                <asp:Button ID="BtnClear" runat="server" Text="Clear All" CssClass="btn btn-primary" OnClick="BtnClear_Click" />
                                                <asp:Button ID="BtnExit" runat="server" Text="Exit" CssClass="btn btn-primary" OnClick="BtnExit_Click" />
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
</asp:Content>

