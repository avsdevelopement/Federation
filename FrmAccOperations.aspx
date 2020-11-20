<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAccOperations.aspx.cs" Inherits="FrmAccOperations" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">
        function isNumber(evt) {
        var iKeyCode = (evt.which) ? evt.which : evt.keyCode
        if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
            return false;
        return true;
    }
</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                       Account Operations
                    </div>
                </div>
                 <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div id="error">
                                    </div>
                                    <div class="tab-pane active" id="tab1">
                                        <div class="row" style="margin-bottom: 10px;">
                                            <div class="col-lg-6">
                                                <div class="col-md-3">
                                                    <asp:RadioButtonList ID="Rdb_No" runat="server" RepeatDirection="Horizontal" Style="width: 800px;" OnSelectedIndexChanged="Rdb_No_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Text="Account Open/Close" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Deposit Lean Mark Cancel" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="Customer Open/Close" Value="3"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>
                                     </div>
                                </div>

                                 <div class="row" id="Div_ACC" runat="server">
                               <div class="row" style="margin-bottom: 10px;">
                                     <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                                <label class="control-label col-md-1">BRCD <span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtBRCD"  onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" OnTextChanged="TxtBRCD_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtBRCDName"  Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                              </div>
                                                     </div>

                                    <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-1">Prd Code<span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtPRD" Placeholder="Product Code" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" OnTextChanged="TxtPRD_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtPRDName"  Placeholder="Name" CssClass="form-control" OnTextChanged="TxtPRDName_TextChanged" runat="server" AutoPostBack="true"></asp:TextBox>
                                                    <div id="CustList" style="height: 200px; overflow-y: scroll;"></div>
                                                    <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="TxtPRDName"
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
                                                <label class="control-label col-md-1">Ac No<span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtAC" Placeholder="Account No" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" OnTextChanged="TxtAC_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtACName" Placeholder="Name" CssClass="form-control" OnTextChanged="TxtACName_TextChanged1" runat="server" AutoPostBack="true"></asp:TextBox>
                                                    <div id="AccList" style="height: 200px; overflow-y: scroll;"></div>
                                                    <asp:AutoCompleteExtender ID="autoglnameA" runat="server" TargetControlID="TxtACName"
                                                        UseContextKey="true"
                                                        CompletionInterval="1"
                                                        CompletionSetCount="20"
                                                        MinimumPrefixLength="1"
                                                        EnableCaching="true"
                                                        ServicePath="~/WebServices/Contact.asmx"
                                                        ServiceMethod="GetAccName" CompletionListElementID="AccList">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                                 <div class="col-md-2">
                                                    <asp:TextBox ID="TxtAcStatus" Placeholder="Status"  CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                                                 </div>
                                                 <div class="col-md-2">
                                                <asp:TextBox ID="TxtBalance" Placeholder="Balance"  CssClass="form-control"  runat="server" AutoPostBack="true"></asp:TextBox>
                                                     </div>
                                                </div>
                                        </div>

                                   <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-1">Status <span class="required">*</span></label>
                                                <div class="col-lg-2">
                                                    <asp:DropDownList ID="DDLStatus" CssClass="form-control"  AutoPostBack="true" runat="server">
                                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                     <asp:ListItem Value="Open">Open</asp:ListItem>
                                                    <asp:ListItem Value="Close">Close</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                 </div>
                                            </div>
                                   </div>
                                     </div>



                                <div class="row" id="Div_DEPOSIT" runat="server">
                                 <div class="row" style="margin-bottom: 10px;"> 
                                      <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                                <label class="control-label col-md-1">BRCD<span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtDBRCD"  onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" OnTextChanged="TxtDBRCD_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtDBRCDname"  Enabled="false"  CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                              </div>
                                                     </div>

                                      <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-1">Prd Code<span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtDPRD" Placeholder="Product Code" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" OnTextChanged="TxtDPRD_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtDPRDName"  Placeholder="Name" CssClass="form-control" OnTextChanged="TxtDPRDName_TextChanged" runat="server" AutoPostBack="true"></asp:TextBox>
                                                    <div id="DPRD" style="height: 200px; overflow-y: scroll;"></div>
                                                    <asp:AutoCompleteExtender ID="autoglnameDP" runat="server" TargetControlID="TxtDPRDName"
                                                        UseContextKey="true"
                                                        CompletionInterval="1"
                                                        CompletionSetCount="20"
                                                        MinimumPrefixLength="1"
                                                        EnableCaching="true"
                                                        ServicePath="~/WebServices/Contact.asmx"
                                                        ServiceMethod="getglname" CompletionListElementID="DPRD">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                            </div>
                                        </div>

                                      <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-1">Ac No<span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtAcNoD" Placeholder="Account No" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" OnTextChanged="TxtAcNoD_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtAcNameD" Placeholder="Name" CssClass="form-control" OnTextChanged="TxtAcNameD_TextChanged" runat="server" AutoPostBack="true"></asp:TextBox>
                                                    <div id="AccD2" style="height: 200px; overflow-y: scroll;"></div>
                                                    <asp:AutoCompleteExtender ID="autoglnameAC2" runat="server" TargetControlID="TxtAcNameD"
                                                        UseContextKey="true"
                                                        CompletionInterval="1"
                                                        CompletionSetCount="20"
                                                        MinimumPrefixLength="1"
                                                        EnableCaching="true"
                                                        ServicePath="~/WebServices/Contact.asmx"
                                                        ServiceMethod="GetAccName" CompletionListElementID="AccD2">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                                 <div class="col-md-2">
                                                    <asp:TextBox ID="TxtStatusD" Placeholder="Status"  CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                                </div>
                                        </div>

                                      <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-1">Status <span class="required">*</span></label>
                                                <div class="col-lg-2">
                                                    <asp:DropDownList ID="DDLStatusD" CssClass="form-control"  AutoPostBack="true" runat="server">
                                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                     <asp:ListItem Value="Open">Open</asp:ListItem>
                                                    <asp:ListItem Value="Close">Close</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                 </div>
                                            </div> 
                        </div>
                        </div>



 <div class="row" id="Div_CUST" runat="server">
                                 <div class="row" style="margin-bottom: 10px;"> 
                                      <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-1">Status <span class="required">*</span></label>
                                                <div class="col-lg-2">
                                                    <asp:DropDownList ID="DDLstatusc" CssClass="form-control"  AutoPostBack="true" runat="server">
                                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                     <asp:ListItem Value="Open">Open</asp:ListItem>
                                                    <asp:ListItem Value="Close">Close</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                 </div>
                                            </div>  
                                            <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                                <label class="control-label col-md-1">BRCD<span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtBRCDC"  onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" OnTextChanged="TxtBRCDC_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtBRCDNameC"  Enabled="false"  CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                              </div>
                                </div>

                                      <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-12">
                                                                <label class="control-label col-md-1">Customer No<span class="required">*</span></label>
                                                                <div class="col-md-2">
                                                                    <asp:TextBox ID="Txtcstno" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" OnTextChanged="Txtcstno_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                </div>
                                                                <div class="col-lg-3">
                                                                    <asp:TextBox ID="TxtCname" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="TxtCname_TextChanged"></asp:TextBox>
                                                                    <div id="CustList3" style="height: 200px; overflow-y: scroll;"></div>
                                                                    <asp:AutoCompleteExtender ID="autocustname" runat="server" TargetControlID="TxtCname"
                                                                        UseContextKey="true"
                                                                        CompletionInterval="1"
                                                                        CompletionSetCount="20"
                                                                        MinimumPrefixLength="1"
                                                                        EnableCaching="true"
                                                                        ServicePath="~/WebServices/Contact.asmx"
                                                                        ServiceMethod="GetCustNames" CompletionListElementID="CustList3">
                                                                    </asp:AutoCompleteExtender>
                                                                </div>
                                                                     </div>
                                                        </div>
</div>
     </div> -

 <div class="row" id="Div_Buttons" runat="server">
                                    <div class="row" style="margin: 7px 0 7px 0; text-align:center">
                                            <div class="col-lg-12">
                                                <div class="col-md-6">
                                             <asp:Button ID="BtnSubmit" runat="server" Text="Submit" CssClass="btn btn-success" OnClick="BtnSubmit_Click" OnClientClick="javascript:return validate();" />
                                                   <asp:Button ID="BtnClear" runat="server" Text="Clear" CssClass="btn btn-success" OnClick="BtnClear_Click" OnClientClick="javascript:return validate();" />
                                                    <asp:Button ID="BtnExit" runat="server" Text="Exit" CssClass="btn btn-success" OnClick="BtnExit_Click" OnClientClick="javascript:return validate();" />
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

