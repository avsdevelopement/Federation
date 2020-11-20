<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmTrialBalance.aspx.cs" Inherits="FrmTrialBalance" %>

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
    </script>
    <script>
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
            <div class="portlet box blue" id="Div1">
                <div class="portlet-title">
                    <div class="caption">
                        Trail Balance
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
                                            <div class="col-md-12">
                                                <div class="col-md-6">
                                                    <asp:RadioButton ID="RBTDeatils" OnCheckedChanged="RBTDeatils_CheckedChanged" AutoPostBack="true" runat="server" Text="Details Wise" />
                                                    <asp:RadioButton ID="RBTSummary" OnCheckedChanged="RBTSummary_CheckedChanged" AutoPostBack="true" runat="server" Text="Summary Wise" />
                                                    <asp:RadioButton ID="RBTDeatils_M" OnCheckedChanged="RBTDeatils_M_CheckedChanged" AutoPostBack="true" runat="server" Text="Marathi Details" />
                                                    <asp:RadioButton ID="RBTSummary_M" OnCheckedChanged="RBTSummary_M_CheckedChanged" AutoPostBack="true" runat="server" Text="Marathi Summary" />
                                                </div>

                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0; padding-top: 15px;">
                                            <div class="col-md-6">
                                                <div class="col-md-6">
                                                    <asp:RadioButton ID="rdbAll" runat="server" Text="As On" GroupName="AS" OnCheckedChanged="rdbAll_CheckedChanged" AutoPostBack="true"></asp:RadioButton>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:RadioButton ID="rdbSpecific" runat="server" Text="From To" GroupName="AS" AutoPostBack="true" OnCheckedChanged="rdbSpecific_CheckedChanged"></asp:RadioButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-12">
                                        <div class="col-md-1">
                                            <label class="control-label">Branch Code</label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="TxtBrID" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <div runat="server" id="FDT">
                                                <div class="col-md-1">
                                                    <label class="control-label">From Date</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtFDate" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <asp:TextBoxWatermarkExtender ID="TxtFDT_WatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtFDate">
                                                    </asp:TextBoxWatermarkExtender>
                                                    <asp:CalendarExtender ID="TxtFDT_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtFDate">
                                                    </asp:CalendarExtender>
                                                </div>
                                            </div>

                                            <div runat="server" id="TDT">
                                                <div class="col-md-1">
                                                    <label class="control-label">To Date</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtTDate" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtTDate">
                                                    </asp:TextBoxWatermarkExtender>
                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtTDate">
                                                    </asp:CalendarExtender>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-md-12">
                                            <div class="col-md-6">
                                                <asp:RadioButton ID="rdbcode" runat="server" GroupName="CG" Text="Code Wise" />
                                                <asp:RadioButton ID="rdbName" runat="server" GroupName="CG" Text="Name Wise" />
                                            </div>

                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-lg-12" style="text-align: left; margin-top: 12px; margin-bottom: 13px; margin-left: 15px;">
                                            <div class="col-lg-6">
                                                <asp:Button ID="Submit" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="Submit_Click" OnClientClick="Javascript:return Validate();" />
                                                <asp:Button ID="ReportV" runat="server" CssClass="btn btn-primary" Text="Trail Balance Report" OnClick="ReportV_Click" />
                                                <asp:Button ID="TextReport" runat="server" CssClass="btn btn-primary" Text="Download Text Report" OnClick="TextReport_Click" />
                                                <br />
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
                                <asp:GridView ID="GrdEmployeeDetails" DataKeyNames="ID"
                                    runat="server" AutoGenerateColumns="False" Width="100%"
                                    CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" ShowFooter="True" PageIndex="10" PageSize="25"
                                    OnRowDataBound="GrdEmployeeDetails_RowDataBound" ShowHeader="False" OnRowCreated="GrdEmployeeDetails_RowCreated" OnPageIndexChanging="GrdEmployeeDetails_PageIndexChanging"
                                    EditRowStyle-BackColor="#FFFF99" EmptyDataText="Record Not Found">
                                    <Columns>
                                        <asp:BoundField DataField="CNO" HeaderText="Id" Visible="false" ItemStyle-Width="60">
                                            <ItemStyle Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="GLCode" ItemStyle-Width="60">
                                            <ItemStyle Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="subglcode" ItemStyle-Width="60">
                                            <ItemStyle Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="GlName" ItemStyle-Width="80">
                                            <ItemStyle Width="80px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="OPBAL" ItemStyle-Width="60" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right">
                                            <ItemStyle CssClass="Th" HorizontalAlign="Right" Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CR" ItemStyle-Width="60" DataFormatString="{0:N2}">
                                            <ItemStyle CssClass="Th" HorizontalAlign="Right" Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DR" ItemStyle-Width="60" DataFormatString="{0:N2}">
                                            <ItemStyle CssClass="Th" HorizontalAlign="Right" Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Credit" ItemStyle-Width="80" DataFormatString="{0:N2}">
                                            <ItemStyle CssClass="Th" HorizontalAlign="Right" Width="80px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Debit" ItemStyle-Width="60" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right">
                                            <ItemStyle CssClass="Th" HorizontalAlign="Right" Width="60px" />
                                        </asp:BoundField>
                                    </Columns>
                                    <PagerStyle CssClass="pgr"></PagerStyle>
                                    <SelectedRowStyle BackColor="#66FF99" />
                                    <EditRowStyle BackColor="#FFFF99"></EditRowStyle>
                                    <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                </asp:GridView>
                            </th>
                        </tr>
                    </thead>
                </table>
            </div>
        </div>
    </div>
</asp:Content>

