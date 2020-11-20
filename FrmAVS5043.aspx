<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAVS5043.aspx.cs" Inherits="FrmAVS5043" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-md-12">
        <div class="portlet box green" id="Div1">
            <div class="portlet-title">
                <div class="caption">
                    Dividend Transfer Master
                </div>
            </div>
            <div class="portlet-body form">
                <div class="form-horizontal">
                    <div class="form-wizard">
                        <div class="form-body">
                            <div style="border: 1px solid #3598dc">
                            <div class="tab-content">
                                <div class="tab-pane active" id="tab1">
                                    <div class="row" style="margin-bottom: 10px;">
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Member No:<span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtMemNo" CssClass="form-control" PlaceHolder="Mem No" runat="server" OnTextChanged="TxtMemNo_TextChanged" AutoPostBack="true" TabIndex="1"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="TxtMemName" CssClass="form-control" PlaceHolder="Member Name" runat="server" OnTextChanged="TxtMemName_TextChanged" AutoPostBack="true" TabIndex="2"></asp:TextBox>
                                                      <asp:AutoCompleteExtender ID="automemname" runat="server" TargetControlID="TxtMemName"
                                                    UseContextKey="true"
                                                    CompletionInterval="1"
                                                    CompletionSetCount="20"
                                                    MinimumPrefixLength="1"
                                                    EnableCaching="true"
                                                    ServicePath="~/WebServices/Contact.asmx"
                                                    ServiceMethod="GetMemNamesdash">
                                                </asp:AutoCompleteExtender>
                                                </div>
                                            </div>
                                        </div>  </div>
                                              <div class="row">
                                    <div class="col-lg-12">
                                        <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31); padding-bottom: 8px;"><strong>Transfer A/C : </strong></div>
                                    </div>
                                </div>
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Brcd:<span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtBrcd" CssClass="form-control" PlaceHolder="Brcd" runat="server" OnTextChanged="TxtBrcd_TextChanged" AutoPostBack="true" TabIndex="3"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="TxtBrcdName" CssClass="form-control" PlaceHolder="Branch Name" runat="server" Enabled="false" TabIndex="4"></asp:TextBox>
                                                    </div></div></div>


                                                 <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                  <label class="control-label col-md-2">Prod cd:<span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtPcd" CssClass="form-control" PlaceHolder="Prod Cd" runat="server" OnTextChanged="TxtPcd_TextChanged" AutoPostBack="true" TabIndex="5"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="TxtPcdName" CssClass="form-control" PlaceHolder="Product Name" runat="server" OnTextChanged="TxtPcdName_TextChanged" AutoPostBack="true" TabIndex="6"></asp:TextBox>
                                                     <div id="CustList" style="height: 200px; overflow-y: scroll;"></div>
                                                                <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="TxtPcdName"
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
                                                <label class="control-label col-md-2">Account No:<span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtAccno" CssClass="form-control" PlaceHolder="Account No" runat="server" OnTextChanged="TxtAccno_TextChanged" AutoPostBack="true" TabIndex="7"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="TxtAccName" CssClass="form-control" PlaceHolder="Account Name" runat="server" OnTextChanged="TxtAccName_TextChanged" AutoPostBack="true" TabIndex="8"></asp:TextBox>
                                                      <div id="CustList2" style="height: 200px; overflow-y: scroll;"></div>
                                                                <asp:AutoCompleteExtender ID="AutoAccname" runat="server" TargetControlID="TxtAccName"
                                                                    UseContextKey="true"
                                                                    CompletionInterval="1"
                                                                    CompletionSetCount="20"
                                                                    MinimumPrefixLength="1"
                                                                    EnableCaching="true"
                                                                    ServicePath="~/WebServices/Contact.asmx"
                                                                    ServiceMethod="GetAccName" CompletionListElementID="CustList2">
                                                                </asp:AutoCompleteExtender>
                                                    </div>
                                                </div>
                                               </div>


                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-10">
                                                <div class="col-md-3">
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:Button ID="BtnSubmit" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="BtnSubmit_Click" TabIndex="9" />
                                                    <asp:Button ID="BtnModify" runat="server" CssClass="btn btn-primary" Text="Modify" OnClick="BtnModify_Click" Visible="false" TabIndex="10" />
                                                    <asp:Button ID="BtnAuthorise" runat="server" CssClass="btn btn-primary" Text="Authorise" OnClick="BtnAuthorise_Click" Visible="false" TabIndex="11" />
                                                    <asp:Button ID="BtnDelete" runat="server" CssClass="btn btn-primary" Text="Cancel" OnClick="BtnDelete_Click" Visible="false" TabIndex="12" />
                                                     <asp:Button ID="BtnClear" runat="server" CssClass="btn btn-primary" Text="Clear" OnClick="BtnClear_Click" TabIndex="13" />
                                                    <asp:Button ID="BtnExit" runat="server" CssClass="btn btn-primary" Text="Exit" OnClick="BtnExit_Click" TabIndex="14" />
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
    </div></div>
         <div class="col-md-12">
                        <div class="table-scrollable">
                            <table class="table table-striped table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th>
                                            <asp:GridView ID="GrdDivTrf" runat="server" AllowPaging="True" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                EditRowStyle-BackColor="#FFFF99" PagerStyle-CssClass="pgr" Width="100%" OnPageIndexChanged="GrdDivTrf_PageIndexChanged">
                                                <Columns>
                                                     <asp:TemplateField HeaderText="ID">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSrno" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="SRNO" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                       <asp:TemplateField HeaderText="CREDIT SUBGL">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDirName" runat="server" Text='<%# Eval("CRSUBGL") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                     <asp:TemplateField HeaderText="CREDIT ACCNO">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCustno" runat="server" Text='<%# Eval("CRACCNO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                     <asp:TemplateField HeaderText="DEBIT ACCNO">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDaccno" runat="server" Text='<%# Eval("DRACCNO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                     <asp:TemplateField HeaderText="BRCD">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPost" runat="server" Text='<%# Eval("BRCD") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Add New" Visible="true" runat="server">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkAdd" runat="server" OnClick="lnkAdd_Click" class="glyphicon glyphicon-plus"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="Modify" Visible="true" runat="server">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkMod" runat="server" CommandArgument='<%#Eval("ID")%>' OnClick="lnkMod_Click" class="glyphicon glyphicon-pencil"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="Delete" Visible="true" runat="server">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDel" runat="server" CommandArgument='<%#Eval("ID")%>' OnClick="lnkDel_Click" class="glyphicon glyphicon-trash"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Authorise" Visible="true" runat="server">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkAuth" runat="server" CommandArgument='<%#Eval("ID")%>' OnClick="lnkAuth_Click" class="glyphicon glyphicon-check"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerStyle CssClass="pgr" />
                                                <SelectedRowStyle BackColor="#66FF99" />
                                                <EditRowStyle BackColor="#FFFF99" />
                                                <AlternatingRowStyle CssClass="alt" />
                                            </asp:GridView>
                                        </th>
                                    </tr>
                                </thead>
                            </table>
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

