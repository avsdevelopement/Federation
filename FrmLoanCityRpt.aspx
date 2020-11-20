<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmLoanCityRpt.aspx.cs" Inherits="FrmLoanCityRpt" %>

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
                        Loan Report city / Acc Close/ city Wise
                    </div>
                </div>

                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                     <div class="row" style="margin: 7px 0 7px 0">
                                    <div class="col-lg-12">
                                        <div class="col-md-2">
                                            <label class="control-label">Branch Code</label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="TxtBrID" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                         </div>
                                    <div id="amount" runat="server" visible="false">
                                     <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <div runat="server" id="Div2">
                                                <div class="col-md-2">
                                                    <label class="control-label">From Amount</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtfromamt" placeholder="From Amt"  onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div runat="server" id="Div3">
                                                <div class="col-md-2">
                                                    <label class="control-label">To Amount</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txttoamt" placeholder="To Amount"  onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                        </div>
                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <div runat="server" id="FDT">
                                                <div class="col-md-2">
                                                    <label class="control-label">From Date</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtFDate" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <asp:TextBoxWatermarkExtender ID="TxtFDT_WatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtFDate">
                                                    </asp:TextBoxWatermarkExtender>
                                                    <asp:CalendarExtender ID="TxtFDT_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtFDate">
                                                    </asp:CalendarExtender>
                                                </div>
                                            </div>

                                            <div runat="server" id="TDT">
                                                <div class="col-md-2">
                                                    <label class="control-label">To Date</label>
                                                </div>
                                                <div class="col-md-3">
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
                                            <div class="col-md-12">
                                                <asp:RadioButton ID="rbnclose" runat="server" GroupName="CG" AutoPostBack="true" Text="Close Acc" OnCheckedChanged="rbnclose_CheckedChanged" />
                                                <asp:RadioButton ID="rbncity" runat="server" GroupName="CG" AutoPostBack="true" Text="City Wise" OnCheckedChanged="rbncity_CheckedChanged" />
                                                 <asp:RadioButton ID="rbnamount" runat="server" GroupName="CG" AutoPostBack="true" OnCheckedChanged="rbnamount_CheckedChanged" Text="Amount Wise" />
                                                 
                                            </div>

                                        </div>
                                    </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-12" style="text-align: center; margin-top: 12px; margin-bottom: 13px; margin-left: 15px;">
                                            <div class="col-lg-6">
                                                <asp:Button ID="Submit" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick=  "Submit_Click" OnClientClick="Javascript:return Validate();" />
                                                <asp:Button ID="btnclear" runat="server" CssClass="btn btn-primary" Text="Clear" OnClick=  "btnclear_Click" />
                                                <asp:Button ID="btnExit" runat="server" CssClass="btn btn-primary" Text="Exit" OnClick=  "btnExit_Click" />
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
                                    OnRowDataBound=  "GrdEmployeeDetails_RowDataBound" ShowHeader="False" OnRowCreated=  "GrdEmployeeDetails_RowCreated" OnPageIndexChanging=  "GrdEmployeeDetails_PageIndexChanging"
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
