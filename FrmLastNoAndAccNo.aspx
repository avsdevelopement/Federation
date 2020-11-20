<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmLastNoAndAccNo.aspx.cs" Inherits="FrmLastNoAndAccNo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
         <%-- Only Numbers --%>
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
                        LastNo And AccNo
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab1">
                                        <div class="row" style="margin-bottom: 10px;">
                                            <div class="col-lg-6">
                                                <div class="col-md-3">
                                                    <asp:RadioButtonList ID="Rdb_No" runat="server" RepeatDirection="Horizontal" Style="width: 300px;" OnSelectedIndexChanged="Rdb_No_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Text="AccNo Update" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="LastNo Update" Value="2"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>
                                         </div>
                                </div>


                                <div class="row" id="Div_ACCNO" runat="server">
        <div class="row" style="margin-bottom: 10px;">
            <div class="row" style="margin: 7px 0 7px 0">
                <div class="col-lg-12">
                    <label class="control-label col-md-1">BRCD<span class="required">*</span></label>
                    <div class="col-md-2">
                        <asp:TextBox ID="txtbrcdno" Placeholder="No" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" OnTextChanged="txtbrcdno_TextChanged" AutoPostBack="true"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtbrcdname" Placeholder="Name" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="row" style="margin: 7px 0 7px 0">
                <div class="col-lg-12">
                    <label class="control-label col-md-1">PrdCode<span class="required">*</span></label>
                    <div class="col-md-2">
                        <asp:TextBox ID="txtprdcode" Placeholder="Code" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" OnTextChanged="txtprdcode_TextChanged" AutoPostBack="true"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtprdname" Placeholder="Name" CssClass="form-control" runat="server" OnTextChanged="txtprdname_TextChanged" AutoPostBack="true"></asp:TextBox>
                        <div id="CustList" style="height: 200px; overflow-y: scroll;"></div>
                        <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="txtprdname"
                            UseContextKey="true"
                            CompletionInterval="1"
                            CompletionSetCount="20"
                            MinimumPrefixLength="1"
                            EnableCaching="true"
                            ServicePath="~/WebServices/Contact.asmx"
                            ServiceMethod="getglname" CompletionListElementID="CustList">
                        </asp:AutoCompleteExtender>
                    </div>
                </div>
            </div>

            <div class="row" style="margin: 7px 0 7px 0">
                <div class="col-lg-12">
                    <label class="control-label col-md-1">Acc No<span class="required">*</span></label>
                    <div class="col-md-2">
                        <asp:TextBox ID="txtaccno" Placeholder="No" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" OnTextChanged="txtaccno_TextChanged" AutoPostBack="true"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtaccname" Placeholder="Name" CssClass="form-control" runat="server" OnTextChanged="txtaccname_TextChanged" AutoPostBack="true"></asp:TextBox>
                        <div id="acclist" style="height: 200px; overflow-y: scroll;"></div>
                        <asp:AutoCompleteExtender ID="autoacc" runat="server" TargetControlID="txtaccname"
                            UseContextKey="true"
                            CompletionInterval="1"
                            CompletionSetCount="20"
                            MinimumPrefixLength="1"
                            EnableCaching="true"
                            ServicePath="~/WebServices/Contact.asmx"
                            ServiceMethod="GetAccName" CompletionListElementID="acclist">
                        </asp:AutoCompleteExtender>
                    </div>
                </div>
            </div>
        </div>
    </div>


<div class="row" id="Div_LASTNO" runat="server">
        <div class="row" style="margin-bottom: 10px;">
            <div class="col-lg-12">
                <label class="control-label col-md-1">BRCD<span class="required">*</span></label>
                <div class="col-md-2">
                    <asp:TextBox ID="txtBR2no" Placeholder="No" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" OnTextChanged="txtBR2no_TextChanged" AutoPostBack="true"></asp:TextBox>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtBR2name" Placeholder="Name" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>
        </div>
    </div>

    <div class="row" id="Div_Buttons" runat="server">
        <div class="row" style="margin: 7px 0 7px 0; text-align: center">
            <div class="col-lg-12">
                <div class="col-md-6">
                    <asp:Button ID="Btn_Submit" OnClientClick="Javascript:return isvalidate();" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="Btn_Submit_Click" />
                    <asp:Button ID="Btn_Clear" runat="server" Text="Clear" CssClass="btn btn-primary" OnClick="Btn_Clear_Click" />
                    <asp:Button ID="Btn_Exit" runat="server" Text="Exit" CssClass="btn btn-primary" OnClick="Btn_Exit_Click" />
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
</asp:Content>

