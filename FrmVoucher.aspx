<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmVoucher.aspx.cs" Inherits="FrmVoucher" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="portlet box blue" id="form_wizard_1">
                        <div class="portlet-title">
                            <div class="caption">
                                Receipt Entry
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
                                                <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label ">Receipt Type :</label>
                                                        </div>
                                                        <div class="col-md-8">
                                                            <asp:RadioButtonList ID="RdbType" runat="server" RepeatDirection="Horizontal" CssClass="radio-list">
                                                                <asp:ListItem Text="Add New Reciept" Value="M" style="margin: 15px;"> </asp:ListItem>
                                                                <asp:ListItem Text="Existing Reciept" Value="F" style="margin: 15px;"> </asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin-bottom: 10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label">Date :</label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="txtDate" CssClass="form-control" type="date" placeholder="GL Code" runat="server" Style="width: 96%"></asp:TextBox> 
                                                            <asp:TextBoxWatermarkExtender ID="TxtDate_WatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtDate">
                                                    </asp:TextBoxWatermarkExtender>
                                                    <asp:CalendarExtender ID="TxtDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtDate">
                                                    </asp:CalendarExtender>
                                                        </div>
                                                        <div class="col-md-2"></div>
                                                        <div class="col-md-2">
                                                            <label class="control-label">Receipt No :</label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="txtReceiptno" CssClass="form-control" type="date" placeholder="Receipt No" runat="server" Style="width: 96%"></asp:TextBox>
                                                             <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtReceiptno">
                                                    </asp:TextBoxWatermarkExtender>
                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtReceiptno">
                                                    </asp:CalendarExtender>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin-bottom: 10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2" style="padding-right: 0px;">
                                                            <label class="control-label" style="padding-right: 0px;">Personal/Non-Personal</label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:RadioButtonList ID="Rdbpersonal" runat="server" RepeatDirection="Horizontal" CssClass="radio-list pull-right">
                                                                <asp:ListItem Text="Personal" Value="M" style="margin: 5px;"> </asp:ListItem>
                                                                <asp:ListItem Text="Non-Personal" Value="F" style="margin: 5px;"> </asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                        <div class="col-md-2"></div>
                                                        <div class="col-md-2">
                                                            <label class="control-label">Transaction Type:</label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:RadioButtonList ID="rdbtransactype" runat="server" RepeatDirection="Horizontal" CssClass="radio-list">
                                                                <asp:ListItem Text="Receipt" Value="Receipt" style="margin: 5px;"> </asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin-bottom: 10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label">Cash/Bank:</label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:RadioButtonList ID="rdbcashbank" runat="server" RepeatDirection="Horizontal" CssClass="radio-list pull-right">
                                                                <asp:ListItem Text="Cash" Value="Cash" style="margin: 5px;"> </asp:ListItem>
                                                                <asp:ListItem Text="PO/Check" Value="Cash" style="margin: 5px;"> </asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                        <div class="col-md-6">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin-bottom: 10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label">Cheque No :</label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="txtcheque" runat="server" Style="Width: 35%; height: 33px; border: 1px solid #89c4f4;"></asp:TextBox>
                                                            <asp:TextBox ID="TxtAgentName" runat="server" Style="width: 60%; height: 33px; border: 1px solid #89c4f4;"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-2"></div>
                                                        <div class="col-md-2">
                                                            <label class="control-label">Bank Code :</label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="txtbankcode" runat="server" Style="Width: 30%; height: 33px; border: 1px solid #89c4f4;"></asp:TextBox>
                                                            <asp:TextBox ID="TextBox8" runat="server" Style="width: 63%; height: 33px; border: 1px solid #89c4f4;"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin-bottom: 10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label">Branch Code  :</label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="txtbrcd" runat="server" Style="Width: 35%; height: 33px; border: 1px solid #89c4f4;"></asp:TextBox>
                                                            <asp:TextBox ID="TextBox6" runat="server" Style="width: 60%; height: 33px; border: 1px solid #89c4f4;"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-2"></div>
                                                        <div class="col-md-2">
                                                            <label class="control-label">Account Type :</label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="txtacctype" runat="server" Style="Width: 30%; height: 33px; border: 1px solid #89c4f4;"></asp:TextBox>
                                                            <asp:TextBox ID="TextBox10" runat="server" Style="width: 64%; height: 33px; border: 1px solid #89c4f4;"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin-bottom: 10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label">Account No :</label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="txtaccno" runat="server" Style="Width: 35%; height: 33px; border: 1px solid #89c4f4;"></asp:TextBox>
                                                            <asp:TextBox ID="TextBox11" runat="server" Style="width: 60%; height: 33px; border: 1px solid #89c4f4;"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-2"></div>
                                                        <div class="col-md-2">
                                                            <label class="control-label">Special Transaction :</label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="txtspectrans" CssClass="form-control" placeholder="Special Transaction" runat="server" Style="width: 95%;"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin-bottom: 10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label">Joint Account Name :</label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="txtjointaccname" CssClass="form-control" placeholder="Joint Account Name" runat="server" Style="width: 96%"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-2"></div>
                                                        <div class="col-md-2">
                                                            <label class="control-label">Narration :</label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="txtnarration" runat="server" Style="Width: 30%; height: 33px; border: 1px solid #89c4f4;"></asp:TextBox>
                                                            <asp:TextBox ID="TextBox16" runat="server" Style="width: 64%; height: 33px; border: 1px solid #89c4f4;"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin-bottom: 10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label">Activity :</label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:DropDownList ID="ddlPrefix" runat="server" CssClass="form-control" Style="width: 96%">
                                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="Unbound" Value="Unbound"></asp:ListItem>
                                                                <asp:ListItem Text="Mrs" Value="2"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-md-1"></div>
                                                        <div class="col-md-1">
                                                            <label class="control-label">Credit :</label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtcredit" CssClass="form-control" placeholder="Credit" runat="server" Style="width: 90%"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-1">
                                                            <label class="control-label">Debit :</label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtdebit" CssClass="form-control" placeholder="Debit" runat="server" Style="width: 92%"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin-bottom: 10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-lg-10">
                                                            <asp:LinkButton ID="LinkButton3" runat="server" Text="MD" class="btn btn-primary">Submit</asp:LinkButton>
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

