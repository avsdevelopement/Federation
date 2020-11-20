<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CBSMaster.master" CodeFile="FrmEMIEnquiry.aspx.cs" Inherits="FrmEMIEnquiry" %>

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
                        EMI Enquiry
                    </div>
                </div>
                <div class="portlet-body form">
                    <!--<form action="#" class="form-horizontal" id="submit_form" method="POST">-->
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">

                                <div class="tab-content">
                                    <div id="error">
                                    </div>
                                    <div class="tab-pane active" id="tab1">


                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-11">
                                                <label class="control-label col-md-3">Loan Amount : <span class="required">* </span></label>

                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtLoanAmt" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" PlaceHolder="Loan Amount"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-11">
                                                <label class="control-label col-md-3">Annual Interest</label>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtAnnualINT" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" PlaceHolder="Annual Interest"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-1">Repay Amt</label>

                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtRepayAmt" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" PlaceHolder="Repay Amount" OnTextChanged="TxtRepayAmt_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-11">
                                                <label class="control-label col-md-3">Period</label>
                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="DDl_PeriodType" runat="server" CssClass="form-control" OnTextChanged="DDl_PeriodType_TextChanged" AutoPostBack="true">
                                                        <asp:ListItem Text="Months" Value="M" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Years" Value="Y"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="TxtPeriodY" CssClass="form-control" runat="server" PlaceHolder="In Years"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0" runat="server" visible="false">
                                            <div class="col-lg-11">
                                                <label class="control-label col-md-3">Payments per Year</label>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtNoofPay" CssClass="form-control" runat="server" PlaceHolder="No. of Payment"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-11">
                                                <label class="control-label col-md-3">Start date</label>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtSDate" CssClass="form-control" PlaceHolder="dd/mm/yyyy" runat="server" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                    <asp:TextBoxWatermarkExtender ID="TxtFDate_WatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtSDate">
                                                    </asp:TextBoxWatermarkExtender>
                                                    <asp:CalendarExtender ID="TxtFDate_CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtSDate">
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
                                        <%--<button type="button" class="btn blue" >Submit</button>--%>
                                        <%--OnClientClick="javascript:return validate();"--%>
                                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn blue" Text="Submit" OnClick="btnSubmit_Click" OnClientClick="javascript:return validate();" />
                                        <asp:Button ID="BtnReport" runat="server" CssClass="btn blue" Text="Report" OnClick="BtnReport_Click" OnClientClick="javascript:return validate();" />

                                        <asp:Button ID="Exit" runat="server" CssClass="btn blue" Text="Exit" OnClick="Exit_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--</form>-->
                </div>
            </div>
        </div>
    </div>

    <div class="table-scrollable" style="width: 100%; height: 500px; overflow-x: auto; overflow-y: auto">
        <table class="table table-striped table-bordered table-hover">
            <thead>
                <tr>
                    <th>
                        <asp:GridView ID="GrdLoanInfo" runat="server"
                            AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                            EditRowStyle-BackColor="#FFFF99"
                            Width="100%">
                            <Columns>

                                <asp:TemplateField HeaderText="PMT No">
                                    <ItemTemplate>
                                        <asp:Label ID="ID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Payment Date" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="PAY_DATE" runat="server" Text='<%# Eval("PAY_DATE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Beginning Balance" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="BEG_BAL" runat="server" Text='<%# Eval("BEG_BAL") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Scheduled Payment">
                                    <ItemTemplate>
                                        <asp:Label ID="SCHEDULE_PAY" runat="server" Text='<%# Eval("SCHEDULE_PAY") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Principal">
                                    <ItemTemplate>
                                        <asp:Label ID="PRINCIPAL" runat="server" Text='<%# Eval("PRINCIPAL") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Interest">
                                    <ItemTemplate>
                                        <asp:Label ID="INTEREST" runat="server" Text='<%# Eval("INTEREST") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Pay">
                                    <ItemTemplate>
                                        <asp:Label ID="TOTAL_PAY" runat="server" Text='<%# Eval("TOTAL_PAY") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ending Balance">
                                    <ItemTemplate>
                                        <asp:Label ID="END_BAL" runat="server" Text='<%# Eval("END_BAL") %>'></asp:Label>
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

</asp:Content>

