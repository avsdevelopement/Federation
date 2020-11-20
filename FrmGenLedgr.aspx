<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmGenLedgr.aspx.cs" Inherits="FrmGenLedgr" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <%--GENERAL LEDGER--%>
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box green">
                <div class="portlet-title">
                    <div class="caption">
                        General Ledger
                        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                    </div>
                    <div class="tools">
                        <a href="javascript:;" class="collapse" data-original-title="" title=""></a>
                        <a href="#portlet-config" data-toggle="modal" class="config" data-original-title="" title=""></a>
                    </div>
                </div>

                <div class="portlet-body">

                    <div class="row" style="margin: 7px 0 7px 0; margin-top: 20px;">
                        <div class="col-lg-12">
                            <label class="control-label col-md-2"  style="text-align:right">From Date<span class="required"></span></label>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtfrmdate" CssClass="form-control" PlaceHolder="dd/mm/yyyy" runat="server"></asp:TextBox>
                                <asp:TextBoxWatermarkExtender ID="TxtDate_WatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtfrmdate">
                                                            </asp:TextBoxWatermarkExtender>
                                                            <asp:CalendarExtender ID="TxtDate_CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtfrmdate">
                                                            </asp:CalendarExtender>
                            </div>
                            <label class="control-label col-md-1"  style="text-align:right">To Date<span class="required"></span></label>
                            <div class="col-md-2">
                                <asp:TextBox ID="Txttodate" CssClass="form-control" PlaceHolder="dd/mm/yyyy" runat="server"></asp:TextBox>
                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="Txttodate">
                                    </asp:TextBoxWatermarkExtender>
                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="Txttodate">
                                    </asp:CalendarExtender>
                            </div>
                        </div>
                    </div>

                    <div class="row" style="margin: 7px 0 7px 0;" id="Cash" runat="server">
                        <div class="col-lg-12">
                            <label class="control-label col-md-2" style="text-align:right">GL Code :<span class="required"></span></label>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtgl" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtglname" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row" style="margin: 7px 0 7px 0;" id="Div1" runat="server">
                        <div class="col-lg-12">
                            <label class="control-label col-md-2"  style="text-align:right">Product code :<span class="required"></span></label>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtsgl" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtsglname" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                   
                    <div>
                        <hr style="margin: 10px 0px 10px 0px" />
                    </div>

                    <div class="row" style="margin: 07px 0 1px 0">
                        <div class="col-lg-12">
                            <div class="col-md-2">
                            </div>
                            <div class="col-md-2">
                                <asp:Button ID="BtnGLDayly" runat="server" CssClass="btn green" Text="GL Summary Daily" OnClick ="BtnGLDayly_Click" OnClientClick="javascript:return validate();" />
                            </div>
                            <div class="col-md-2">
                                <asp:Button ID="BtnGLMonthly" runat="server" CssClass="btn green" Text="GL Summary Monthly" OnClick="BtnGLMonthly_Click" OnClientClick="javascript:return validate();" />    
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

