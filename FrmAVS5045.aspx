<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAVS5045.aspx.cs" Inherits="FrmAVS5045" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function Validate() {
            var brcd = document.getElementById('<%=TxtBrcd.ClientID%>').value;
            var Message = document.getElementById('<%=TxtMsg.ClientID%>').value;
            var activity = document.getElementById('<%=TxtActivity.ClientID%>').value;
            var Parameter = document.getElementById('<%=ddlPara.ClientID%>').value;

            var message = '';

            if (brcd == "") {
                message = 'Please Enter Branch Code...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtBrcd.ClientID%>').focus();
                return false;
            }
            if (Message == "") {
                message = 'Please Enter Message...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtMsg.ClientID%>').focus();
                return false;
            }
            if (activity == "") {
                message = 'Please Enter Activity...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtActivity.ClientID%>').focus();
                return false;
            }
            if (Parameter == "0") {
                message = 'Please Select Parameter Value...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=ddlPara.ClientID%>').focus();
                return false;
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-md-12">
        <div class="portlet box green" id="Div1">
            <div class="portlet-title">
                <div class="caption">
                    SMS Parameter
                </div>
            </div>
            <div class="portlet-body form">
                <div class="form-horizontal">
                    <div class="form-wizard">
                        <div class="form-body">
                            <div style="border: 1px solid #3598dc">
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab1" runat="server">
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Brcd:<span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtBrcd" CssClass="form-control" PlaceHolder="Brcd" runat="server" OnTextChanged="TxtBrcd_TextChanged" AutoPostBack="true" TabIndex="1"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtBrcdName" CssClass="form-control" PlaceHolder="Branch Name" runat="server" Enabled="false" TabIndex="2"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-2">Message:<span class="required">*</span></label>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtMsg" CssClass="form-control" PlaceHolder="Message" runat="server" AutoPostBack="true" TabIndex="3"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Activity:<span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtActivity" CssClass="form-control" PlaceHolder="Activity" runat="server" AutoPostBack="true" TabIndex="4"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-2">Parameter:<span class="required">*</span></label>
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="ddlPara" CssClass="form-control" AutoPostBack="true" runat="server" TabIndex="5">
                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="N"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-10">
                                                <div class="col-md-3">
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:Button ID="BtnSubmit" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="BtnSubmit_Click" OnClientClick="javascript: return Validate();" TabIndex="6" />
                                                    <asp:Button ID="BtnModify" runat="server" CssClass="btn btn-primary" Text="Modify" OnClick="BtnModify_Click" OnClientClick="javascript: return Validate();" Visible="false" TabIndex="7" />
                                                    <asp:Button ID="BtnAuthorise" runat="server" CssClass="btn btn-primary" Text="Authorise" OnClick="BtnAuthorise_Click" OnClientClick="javascript: return Validate();" Visible="false" TabIndex="8" />
                                                    <asp:Button ID="BtnDelete" runat="server" CssClass="btn btn-primary" Text="Delete" OnClick="BtnDelete_Click" OnClientClick="javascript: return Validate();" Visible="false" TabIndex="9" />
                                                    <asp:Button ID="BtnClear" runat="server" CssClass="btn btn-primary" Text="Clear" OnClick="BtnClear_Click" TabIndex="10" />
                                                    <asp:Button ID="BtnExit" runat="server" CssClass="btn btn-primary" Text="Exit" OnClick="BtnExit_Click" TabIndex="11" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            
                               <div class="row" style="margin: 7px 0 7px 0">
                        <div class="col-md-12">
                            <div class="table-scrollable">
                                <table class="table table-striped table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th>
                                                <asp:GridView ID="GrdSMSPara" runat="server" AllowPaging="True" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                    EditRowStyle-BackColor="#FFFF99" PagerStyle-CssClass="pgr" Width="100%">
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

                                                        <asp:TemplateField HeaderText="Message">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMsg" runat="server" Text='<%# Eval("Message") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Activity">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAct" runat="server" Text='<%# Eval("Activity") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Parameter">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblParam" runat="server" Text='<%# Eval("Parameter") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="BRCD">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBrcd" runat="server" Text='<%# Eval("BRCD") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Add New" runat="server">
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

