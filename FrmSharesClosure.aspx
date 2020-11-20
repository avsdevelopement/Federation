<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmSharesClosure.aspx.cs" Inherits="FrmSharesClosure" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        Share Application Closure
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">

                                    <div style="border: 1px solid #3598dc">
                                        <div class="portlet-body">
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Member Information : </strong></div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Application Type :<span class="required"> *</span></label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:DropDownList ID="ddlAppType" OnSelectedIndexChanged="ddlAppType_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control" >
                                                            <asp:ListItem Text="-- Select --" Value="" />
                                                            <asp:ListItem Text="Share Transfer" Value="" />
                                                            <asp:ListItem Text="Share Closure" Value="" />
                                                        </asp:DropDownList>  
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <label class="control-label ">MemberNo :<span class="required"> *</span></label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtMemberNo" onkeypress="javascript:return isNumber(event)" OnTextChanged="txtMemberNo_TextChanged" AutoPostBack="true" Placeholder="Member No" runat="server" CssClass="form-control" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtMemberName" Placeholder="Member Name" OnTextChanged="txtMemberName_TextChanged" AutoPostBack="true" runat="server" CssClass="form-control" />
                                                        <asp:AutoCompleteExtender ID="AutoMemName" runat="server" TargetControlID="txtMemberName"
                                                            UseContextKey="true"
                                                            CompletionInterval="1"
                                                            CompletionSetCount="20"
                                                            MinimumPrefixLength="1"
                                                            EnableCaching="true"
                                                            ServicePath="~/WebServices/Contact.asmx"
                                                            ServiceMethod="GetGlName">
                                                        </asp:AutoCompleteExtender>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label class="control-label ">AppNo :</label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtAppNo" Placeholder="Application No" runat="server" CssClass="form-control" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <label class="control-label ">NoOfShare :</label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtNoOfShare" onkeypress="javascript:return isNumber(event)" Placeholder="No Of Sahres" runat="server" CssClass="form-control" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtTotShrValue" Placeholder="Total Share value" runat="server" CssClass="form-control" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Certification No :</label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:DropDownList ID="ddlCertNo" runat="server" CssClass="form-control" ></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <label class="control-label ">From Share No :</label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtFromShr" runat="server" CssClass="form-control" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label class="control-label ">To Share No :</label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtToShr" runat="server" CssClass="form-control" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Share Value :</label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtShrValue" runat="server" CssClass="form-control" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Remark :</label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtRemark" TextMode="MultiLine" runat="server" CssClass="form-control" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div style="border: 1px solid #3598dc">
                                        <div class="portlet-body">
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Payment Mode : </strong></div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <label class="control-label ">Payment Type :<span class="required"> *</span></label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:DropDownList ID="ddlPayType" OnSelectedIndexChanged="ddlPayType_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div id="Transfer" visible="false" runat="server">

                                                <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label ">Product Code :<span class="required"> *</span></label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtProdType1" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtProdType1_TextChanged" PlaceHolder="Product Type"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtProdName1" CssClass="form-control" PlaceHolder="Product Name" OnTextChanged="txtProdName1_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                                            <asp:AutoCompleteExtender ID="autoglname1" runat="server" TargetControlID="txtProdName1"
                                                                UseContextKey="true"
                                                                CompletionInterval="1"
                                                                CompletionSetCount="20"
                                                                MinimumPrefixLength="1"
                                                                EnableCaching="true"
                                                                ServicePath="~/WebServices/Contact.asmx"
                                                                ServiceMethod="GetGlName">
                                                            </asp:AutoCompleteExtender>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div id="Div1" class="row" style="margin-top: 12px; margin-bottom: 10px;" runat="server">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label ">Acc No / Name:<span class="required"> *</span></label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtAccNo1" CssClass="form-control" PlaceHolder="ID" runat="server" AutoPostBack="true" OnTextChanged="TxtAccNo1_TextChanged"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="TxtAccName1" CssClass="form-control" PlaceHolder="Customer Name" runat="server" AutoPostBack="true" OnTextChanged="TxtAccName1_TextChanged"></asp:TextBox>
                                                            <asp:AutoCompleteExtender ID="AutoAccname1" runat="server" TargetControlID="TxtAccName1"
                                                                UseContextKey="true"
                                                                CompletionInterval="1"
                                                                CompletionSetCount="20"
                                                                MinimumPrefixLength="1"
                                                                EnableCaching="true"
                                                                ServicePath="~/WebServices/Contact.asmx"
                                                                ServiceMethod="GetAccName">
                                                            </asp:AutoCompleteExtender>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <label class="control-label ">Balance:<span class="required"> *</span></label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtBalance" CssClass="form-control" PlaceHolder="Balance" runat="server" Enabled="false"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div id="Transfer1" visible="false" runat="server">
                                                <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label ">Instrument No. :</label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="TxtChequeNo" placeholder="CHEQUE NUMBER" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <label class="control-label ">Instrument Date :</label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="TxtChequeDate" placeholder="CHEQUE DATE" CssClass="form-control" onkeyup="FormatIt(this)" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div id="DivAmount" visible="false" runat="server">
                                                <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label ">Naration : <span class="required">*</span></label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtNarration" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <label class="control-label ">Amount : <span class="required">*</span></label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtAmount" placeholder="DEBIT AMOUNT" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" Enabled="false"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>

                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9">
                                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn blue" Text="Submit" OnClick="btnSubmit_Click" OnClientClick="Javascript:return IsValide();" />
                                        <asp:Button ID="btnClear" Text="Clear All" runat="server" CssClass="btn blue" OnClick="btnClear_Click" />
                                        <asp:Button ID="btnExit" Text="Exit" runat="server" CssClass="btn blue" OnClick="btnExit_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
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

