<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmIJRegister.aspx.cs" Inherits="FrmIJRegister" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="col-md-12">
        <div class="portlet box green" id="Div1">
            <div class="portlet-title">
                <div class="caption">
                    I & J Register
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
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                               <div class="col-md-5"></div>
                                                <div class="col-md-6">
                                                    <asp:RadioButtonList ID="RblIJReg" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RblIJReg_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Selected="True" style="padding:15px">I Register</asp:ListItem>
                                                        <asp:ListItem Value="1">J Register</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div> 
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                       <div class="col-md-5"></div>
                                                <div class="col-md-6">
                                                    <asp:RadioButtonList ID="RblType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RblType_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Selected="True" style="padding:20px">Specific</asp:ListItem>
                                                        <asp:ListItem Value="1">All</asp:ListItem>
                                                    </asp:RadioButtonList>
                                        </div> 
                                                </div>
                                            </div>
                                         <div class="row" style="margin: 7px 0 7px 0" runat="server" id="DIV_MEMNAME">
                                            <div class="col-lg-12">
                                               <label class="control-label col-md-2">Account No. <span class="required">*</span></label>
                                               
                                                 <div class="col-md-2">
                                                   <asp:TextBox ID="TxtMemNo"  CssClass="form-control" placeholder="Accno" AutoPostBack="true" runat="server" OnTextChanged="TxtMemNo_TextChanged"></asp:TextBox>
                                                </div>
                                            <div class="col-md-4" runat="server">
                                                   <asp:TextBox ID="TxtMemName"  placeholder="Account Name" CssClass="form-control" runat="server"></asp:TextBox>
                                                       <div id="CustList" style="height: 200px; overflow-y: scroll;"></div>
                                                       <asp:AutoCompleteExtender ID="AutoAccname" runat="server" TargetControlID="TxtMemName"
                                                                       UseContextKey="true"
                                                                       CompletionInterval="1"
                                                                       CompletionSetCount="20"
                                                                       MinimumPrefixLength="1"
                                                                       EnableCaching="true"
                                                                       ServicePath="~/WebServices/Contact.asmx"
                                                                       ServiceMethod="GetAccName" CompletionListElementID="CustList">
                                                       </asp:AutoCompleteExtender>
                                                </div>
                                            </div>
                                        </div>
                                         <div class="row" style="margin: 7px 0 7px 0" id="Div_FT" runat="server" visible="false">
                                            <div class="col-lg-12">
                                                 <label class="control-label col-md-1">From A/C:<span class="required">*</span></label>
                                                <div class="col-md-2">
                                                   <asp:TextBox ID="TxtFMemNo"  placeholder="From Accno" CssClass="form-control" AutoPostBack="true" runat="server" OnTextChanged="TxtFMemNo_TextChanged"></asp:TextBox>
                                                </div>
                                            <div class="col-md-3">
                                                   <asp:TextBox ID="TxtFMemName" placeholder="From Account Name" CssClass="form-control" runat="server"></asp:TextBox>
                                                        <div id="Div3" style="height: 200px; overflow-y: scroll;"></div>
                                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="TxtFMemName"
                                                                        UseContextKey="true"
                                                                        CompletionInterval="1"
                                                                        CompletionSetCount="20"
                                                                        MinimumPrefixLength="1"
                                                                        EnableCaching="true"
                                                                        ServicePath="~/WebServices/Contact.asmx"
                                                                        ServiceMethod="GetAccName" CompletionListElementID="Div3">
                                                        </asp:AutoCompleteExtender>
                                                </div>
                                                <label class="control-label col-md-1">To A/C:<span class="required">*</span></label>
                                                <div class="col-md-2">
                                                   <asp:TextBox ID="TxtTMemNo"  placeholder="To Accno" CssClass="form-control" AutoPostBack="true" runat="server" OnTextChanged="TxtTMemNo_TextChanged"></asp:TextBox>
                                                </div>
                                            <div class="col-md-3">
                                                   <asp:TextBox ID="TxtTMemName" placeholder="To Account Name" CssClass="form-control" runat="server"></asp:TextBox>
                                                        <div id="Div2" style="height: 200px; overflow-y: scroll;"></div>
                                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="TxtTMemName"
                                                                        UseContextKey="true"
                                                                        CompletionInterval="1"
                                                                        CompletionSetCount="20"
                                                                        MinimumPrefixLength="1"
                                                                        EnableCaching="true"
                                                                        ServicePath="~/WebServices/Contact.asmx"
                                                                        ServiceMethod="GetAccName" CompletionListElementID="Div2">
                                                        </asp:AutoCompleteExtender>
                                                </div>
                                                </div>
                                             </div>
                                    
                                         <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-10">
                                                        <div class="col-md-4">
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:Button ID="BtnSubmit" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="BtnSubmit_Click" OnClientClick="Javascript:return IsValide()"/>
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

