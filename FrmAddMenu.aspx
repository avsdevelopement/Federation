<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAddMenu.aspx.cs" Inherits="FrmAddMenu" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="Div1">
                <div class="portlet-title">
                    <div class="caption">
                        Menu Details
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab1">

                                        <div class="tab-pane active" id="tab__blue">
                                            <ul class="nav nav-pills">
                                                <li class="pull-right">

                                                    <asp:Label ID="lblStst" runat="server" Text="Activity Perform :" Style="font-weight: bold;"></asp:Label>
                                                    <asp:Label ID="lblActivity" runat="server" Text=""></asp:Label>
                                                </li>
                                                  <li>
                                                        <asp:LinkButton ID="lnkAdd" runat="server" Text="a" class="btn btn-default" OnClick="lnkAdd_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-asterisk"></i>Add New</asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton ID="lnkModify" runat="server" Text="a" class="btn btn-default" OnClick="lnkModify_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-plus-circle"></i>Modify</asp:LinkButton>
                                                    </li>
                                            </ul>
                                        </div>
                                        <div>
                                            <div class="row" style="margin: 7px 0 7px 0" id="div_custno" runat="server">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Menu Id <span class="required">*</span></label>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtMenuId" CssClass="form-control"  runat="server" placeholder="MenuId" OnTextChanged="txtMenuId_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    </div>

                                                     <label class="control-label col-md-2">Menu Title<span class="required">*</span></label>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtParent" CssClass="form-control" AutoPostBack="true"  runat="server" placeholder="Menu Title" OnTextChanged="txtParent_TextChanged"></asp:TextBox>
                                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="txtParent"
                                                            UseContextKey="true"
                                                            CompletionInterval="1"
                                                            CompletionSetCount="20"
                                                            MinimumPrefixLength="1"
                                                            EnableCaching="true"
                                                            ServicePath="~/WebServices/Contact.asmx"
                                                            ServiceMethod="GetMenuForm"
                                                            CompletionListElementID="CustList">
                                                        </asp:AutoCompleteExtender>
                                                    </div>

                                                </div>
                                            </div>

                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Parent Menu Id<span class="required">*</span></label>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="ParentMenuId" CssClass="form-control" AutoPostBack="true" placeholder="Parent MenuId" OnTextChanged="ParentMenuId_TextChanged" runat="server"></asp:TextBox>
                                                     <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="ParentMenuId"
                                                            UseContextKey="true"
                                                            CompletionInterval="1"
                                                            CompletionSetCount="20"
                                                            MinimumPrefixLength="1"
                                                            EnableCaching="true"
                                                            ServicePath="~/WebServices/Contact.asmx"
                                                            ServiceMethod="GetMenutitleID"
                                                            CompletionListElementID="CustList">
                                                        </asp:AutoCompleteExtender>
                                                         </div>
                                                    <label class="control-label col-md-2">Parent Menu Title<span class="required">*</span></label>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtmenutt" CssClass="form-control" placeholder="Menu Title" runat="server" AutoPostBack="true" OnTextChanged="txtmenutt_TextChanged"></asp:TextBox>
                                                        <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="txtmenutt"
                                                            UseContextKey="true"
                                                            CompletionInterval="1"
                                                            CompletionSetCount="20"
                                                            MinimumPrefixLength="1"
                                                            EnableCaching="true"
                                                            ServicePath="~/WebServices/Contact.asmx"
                                                            ServiceMethod="GetMenutitle"
                                                            CompletionListElementID="CustList">
                                                        </asp:AutoCompleteExtender>
                                                    </div>
                                                    <div class="col-md-2">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Page Description<span class="required">*</span></label>
                                                    <div class="col-lg-3">
                                                        <asp:TextBox ID="txtPage" placeholder="Page Description" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                    <label class="control-label col-md-2">FormName<span class="required">*</span></label>

                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtFormName" OnTextChanged="txtFormName_TextChanged" placeholder="Page URL" CssClass="form-control" AutoPostBack="true" runat="server"></asp:TextBox>
                                                        <%-- <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" TargetControlID="txtFormName"
                                                            UseContextKey="true"
                                                            CompletionInterval="1"
                                                            CompletionSetCount="20"
                                                            MinimumPrefixLength="1"
                                                            EnableCaching="true"
                                                            ServicePath="~/WebServices/Contact.asmx"
                                                            ServiceMethod="GetMenutitle"
                                                            CompletionListElementID="CustList">
                                                        </asp:AutoCompleteExtender>--%>
                                                    </div>
                                                    <div class="col-md-2">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                  <%--  <label class="control-label col-md-2">User Group <span class="required">*</span></label>
                                                    <div class="col-md-3">
                                                        <asp:DropDownList ID="ddusergroup" CssClass="form-control" runat="server">
                                                        </asp:DropDownList>
                                                    </div>--%>

                                                  <label class="control-label col-md-2">Status </label>

                                                    <div class="col-lg-3">
                                                        <asp:RadioButtonList ID="RdbStatus" runat="server" RepeatDirection="Horizontal" CssClass="radio-list" TabIndex="19">
                                                            <asp:ListItem Text="Active" Value="1" Selected="True"> </asp:ListItem>
                                                            <asp:ListItem Text="Deactive" Value="3"> </asp:ListItem>

                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-lg-12" style="text-align: center; margin-top: 12px;">
                                               <%--   <asp:Button ID="btnAddNew" runat="server" Text="Create MenuID" CssClass="btn btn-primary" OnClick="btnAddNew_Click" />--%>
                                                <asp:Button ID="Submit" runat="server" Text="Submit" CssClass="btn btn-primary"  OnClick="Submit_Click" OnClientClick="Javascript:return Validate()" />
                                                <asp:Button ID="btnClear" runat="server" Text="Clear All" CssClass="btn btn-primary" OnClick="btnClear_Click" />
                                                <asp:Button ID="Exit" runat="server" Text="Close" CssClass="btn btn-primary" />
                                                 <asp:Button ID="BtnUpdate" runat="server" Text="Update" CssClass="btn btn-primary" OnClick="BtnUpdate_Click" />
                                                 <asp:Button ID="BtnReport" runat="server" Text="Report" CssClass="btn btn-primary" OnClick="BtnReport_Click" />
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

