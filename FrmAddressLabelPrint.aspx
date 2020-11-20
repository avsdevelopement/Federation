<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAddressLabelPrint.aspx.cs" Inherits="FrmAddressLabelPrint" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-md-12">
        <div class="portlet box green" id="Div1">
            <div class="portlet-title">
                <div class="caption">
                    Address Label Printing
                </div>
            </div>
            <div class="portlet-body form">
                <div class="form-horizontal">
                    <div class="form-wizard">
                        <div class="form-body">
                            <div class="tab-content">
                                <div class="tab-pane active" id="tab1">

                                    <div style="border: 1px solid #3598dc">
                                        <div class="row" style="margin-bottom: 10px;">
                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2"></div>

                                                </div>
                                            </div>
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-1">From A/C:<span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtFMemNo" placeholder="From Accno" CssClass="form-control" AutoPostBack="true" runat="server" OnTextChanged="TxtFMemNo_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtFMemName" placeholder="From Account Name" OnTextChanged="TxtFMemName_TextChanged" AutoPostBack="true"   CssClass="form-control" runat="server"></asp:TextBox>
                                                     <div id="CustList0" style="height: 200px; overflow-y: scroll;"></div>
                                                           <asp:AutoCompleteExtender ID="AutoMemberName" runat="server" TargetControlID="TxtFMemName" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" ServiceMethod="GetMemberNames" CompletionListElementID="CustList0">
                                                            </asp:AutoCompleteExtender>
                                                    <%--<asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="TxtFMemName"
                                                        UseContextKey="true"
                                                        CompletionInterval="1"
                                                        CompletionSetCount="20"
                                                        MinimumPrefixLength="1"
                                                        EnableCaching="true"
                                                        ServicePath="~/WebServices/Contact.asmx"
                                                        ServiceMethod="GetMemberNames" CompletionListElementID="Div3">
                                                    </asp:AutoCompleteExtender>--%>
                                                </div>
                                                <label class="control-label col-md-1">To A/C:<span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtTMemNo" placeholder="To Accno" CssClass="form-control" AutoPostBack="true" runat="server" OnTextChanged="TxtTMemNo_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtTMemName" placeholder="To Account Name" CssClass="form-control" AutoPostBack="true"  OnTextChanged="TxtTMemName_TextChanged" runat="server"></asp:TextBox>
                                                      <div id="Div2" style="height: 200px; overflow-y: scroll;"></div>
                                                           <asp:AutoCompleteExtender ID="CustList01" runat="server" TargetControlID="TxtTMemName" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" ServiceMethod="GetMemberNames" CompletionListElementID="Div2">
                                                            </asp:AutoCompleteExtender>
                                                   <%-- <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="TxtTMemName"
                                                        UseContextKey="true"
                                                        CompletionInterval="1"
                                                        CompletionSetCount="20"
                                                        MinimumPrefixLength="1"
                                                        EnableCaching="true"
                                                        ServicePath="~/WebServices/Contact.asmx"
                                                        ServiceMethod="GetMemberNames" CompletionListElementID="Div2">
                                                    </asp:AutoCompleteExtender>--%>
                                                </div>
                                            </div>
                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <div runat="server" id="FDT">
                                                        <div class="col-md-2">
                                                            <label class="control-label">As On Date</label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="TxtFDate" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                            <asp:TextBoxWatermarkExtender ID="TxtFDT_WatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtFDate">
                                                            </asp:TextBoxWatermarkExtender>
                                                            <asp:CalendarExtender ID="TxtFDT_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtFDate">
                                                            </asp:CalendarExtender>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-10">
                                                    <div class="col-md-4">
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-primary" Text="Print" OnClick="btnPrint_Click" OnClientClick="Javascript:return Validate();" />
                                                        <asp:Button ID="BtnClear" runat="server" CssClass="btn btn-primary" Text="Clear" OnClick="BtnClear_Click" />
                                                        <asp:Button ID="BtnExit" runat="server" CssClass="btn btn-primary" Text="Exit" OnClick="BtnExit_Click" />
                                                    </div>
                                                    <div class="col-md-5">
                                                    </div>
                                                </div>
                                            </div>
                                            </>
                                        </div>
                                    </div>
                                </div>
                            </div>
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
</asp:Content>

