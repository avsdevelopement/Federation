<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmStandingInst.aspx.cs" Inherits="FrmStandingInst" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function isvalidate() {
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
                                Standing Instruction Details 
                            </div>
                        </div>
                        <div class="portlet-body form">
                            <div class="form-horizontal">
                                <div class="form-wizard">
                                    <div class="form-body">
                                        <div class="tab-content">
                                            <div class="tab-pane active" id="tab__blue">
                                            </div>
                                            <!-- mycode start  -->
                                            <div style="border: 1px solid #3598dc">


                                                <div class="row" style="margin: 20px; border-bottom: 1px solid rgba(53, 152, 220, 0.55);"><strong>Debit account details:</strong> </div>

                                                <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label ">SI No.</label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="TxtSINo" CssClass="form-control" placeholder="SI No." runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-6">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin-bottom: 10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label">Account Type</label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:DropDownList ID="ddlAccType" runat="server" CssClass="form-control">                                                                
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="col-md-2">
                                                            <label class="control-label">Customer No.</label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="TxtCustNo" CssClass="form-control" placeholder="Customer No." runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row" style="margin-bottom: 10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label">Account No</label>
                                                        </div>
                                                        <div class="col-md-5">
                                                            <asp:TextBox ID="TxtAccno" runat="server" Style="Width: 75px; height: 33px; border: 1px solid #89c4f4;"></asp:TextBox>
                                                            <asp:TextBox ID="TxtAccName" runat="server" Style="width: 200px; height: 33px; border: 1px solid #89c4f4;"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-4">
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row" style="margin: 20px; border-bottom: 1px solid rgba(53, 152, 220, 0.55);"><strong>Credit account details:</strong> </div>

                                                <div class="row" style="margin-bottom: 10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label">Account Type</label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:DropDownList ID="ddlTAccType" runat="server" CssClass="form-control">                                                              
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="col-md-2">
                                                            <label class="control-label">Customer No.</label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="TxtTCustNo" CssClass="form-control" placeholder="Customer No." runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row" style="margin-bottom: 10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label">Account No</label>
                                                        </div>
                                                        <div class="col-md-5">
                                                            <asp:TextBox ID="TxtTAccno" runat="server" Style="Width: 75px; height: 33px; border: 1px solid #89c4f4;"></asp:TextBox>
                                                            <asp:TextBox ID="TxtTAccName" runat="server" Style="width: 200px; height: 33px; border: 1px solid #89c4f4;"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-4">
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row" style="margin: 20px; border-bottom: 1px solid rgba(53, 152, 220, 0.55);"><strong>Process Details:</strong> </div>
                                                <div class="row" style="margin-bottom: 10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label">Amount</label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtAmt" CssClass="form-control" placeholder="Amount" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <label class="control-label">From Date</label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtFDate" CssClass="form-control"  placeholder="dd/MM/yyyyy" runat="server"></asp:TextBox>
                                                               <asp:TextBoxWatermarkExtender ID="TxtDate_WatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtFDate">
                                                            </asp:TextBoxWatermarkExtender>
                                                            <asp:CalendarExtender ID="TxtDate_CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtFDate">
                                                            </asp:CalendarExtender>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <label class="control-label">To Date</label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtToDate" CssClass="form-control" placeholder="dd/MM/yyyy"  runat="server"></asp:TextBox> 
                                                               <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtToDate">
                                                            </asp:TextBoxWatermarkExtender>
                                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtToDate">
                                                            </asp:CalendarExtender>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin-bottom: 10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label">Transfer Date</label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtTFDate" CssClass="form-control" placeholder="dd/MM/yyyy"  runat="server"></asp:TextBox> 
                                                               <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtTFDate">
                                                            </asp:TextBoxWatermarkExtender>
                                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtTFDate">
                                                            </asp:CalendarExtender>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <label class="control-label">Previous Date</label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtPRDate" CssClass="form-control" placeholder="dd/MM/yyyy" runat="server"></asp:TextBox> 
                                                               <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtPRDate">
                                                            </asp:TextBoxWatermarkExtender>
                                                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtPRDate">
                                                            </asp:CalendarExtender>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <label class="control-label">Account Status</label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:DropDownList ID="ddlAccSTS" runat="server" CssClass="form-control">                                                                
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin-bottom: 10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label">Total EMI</label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtEMI" CssClass="form-control" placeholder="Total EMI" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <label class="control-label">Pay</label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:RadioButtonList ID="RdbPay" runat="server" RepeatDirection="Horizontal" CssClass="radio-list pull-right">
                                                                <asp:ListItem Text="Daily" Value="D" style="margin: 5px;"> </asp:ListItem>
                                                                <asp:ListItem Text="Monthly" Value="M" style="margin: 5px;"> </asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin-bottom: 10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label">Transfer Day</label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtTFDay" CssClass="form-control" placeholder="Transfer Day" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <label class="control-label">Remark</label>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:TextBox ID="TxtRemark" CssClass="form-control" placeholder="Remark" runat="server"></asp:TextBox>
                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="row" style="margin-bottom: 10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-lg-10">
                                                            <asp:Button ID="Submit" runat="server" Text="Submit" CssClass="btn btn-primary" />                                                            
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- mycode end  -->
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

