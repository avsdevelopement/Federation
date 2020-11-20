<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmUserReport.aspx.cs" Inherits="FrmUserReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="js/jquery-1.8.2.min.js"></script>
    <script src="js/RightClickDisabled.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        User Report
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
                                        
                                          

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-md-12">
                                                <div class="col-md-12">
                                                    <asp:RadioButtonList ID="Rdb_Choice" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Rdb_Choice_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="All" Selected="True">All Branches&nbsp&nbsp&nbsp&nbsp</asp:ListItem>
                                                        <asp:ListItem Value="Some">Specific Branche&nbsp&nbsp&nbsp&nbsp</asp:ListItem>
                                                      <%--  <asp:ListItem Value="loggedin">Logged in Users&nbsp&nbsp&nbsp&nbsp</asp:ListItem>
                                                        <asp:ListItem Value="loggedout">Logged out Users&nbsp&nbsp&nbsp&nbsp</asp:ListItem>   ASKED BY DARADE SIR--%>
                                                    </asp:RadioButtonList>
                                                  <%--  <asp:RadioButton ID="Rdb_All" AutoPostBack="true" runat="server" Text="All Branches" OnCheckedChanged="Rdb_All_CheckedChanged" />
                                                    <asp:RadioButton ID="Rdb_Some" AutoPostBack="true" runat="server" Text="Some Branches" OnCheckedChanged="Rdb_Some_CheckedChanged" />--%>
                                                </div>

                                            </div>
                                        </div>
                                         <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-md-12">
                                                  <div class="col-md-12">
                                                <label class="control-label col-md-2">User Status <span class="required">*</span></label>
                                               <asp:RadioButtonList ID="rbnStatus" runat="server" AutoPostBack="true" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="Active" Selected="True">Active&nbsp&nbsp&nbsp&nbsp</asp:ListItem>
                                                        <asp:ListItem Value="Deactive">Deactive&nbsp&nbsp&nbsp&nbsp</asp:ListItem>
                                                        <asp:ListItem Value="All">All&nbsp&nbsp&nbsp&nbsp</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                       </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">From Branch <span class="required">*</span></label>
                                                <div class="col-lg-3">
                                                    <asp:TextBox ID="TxtFBRCD" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-2">To Branch </label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="TxtTBRCD" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                    </div>

                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-4">
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Button ID="Btn_View" runat="server" Text="View" CssClass="btn btn-success" OnClick="Btn_View_Click"/>
                                        <asp:Button ID="Btn_Submit" runat="server" Text="Report" CssClass="btn btn-success" OnClick="Btn_Submit_Click"/>
                                        <asp:Button ID="Btn_ClearAll" runat="server" Text="Clear All" CssClass="btn btn-primary" OnClick="Btn_ClearAll_Click" />
                                        <asp:Button ID="Btn_Exit" runat="server" Text="Exit" CssClass="btn btn-primary" OnClick="Btn_Exit_Click" />
                                    </div>
                                    <div class="col-lg-2">
                                        
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

     <div class="row" style="margin: 7px 0 7px 0">
        <div class="col-lg-12" style="height: 50%">
            <div class="table-scrollable" style="height: 350px; overflow-x: scroll; overflow-y: scroll; padding-bottom: 10px;">
                <table class="table table-striped table-bordered table-hover" width="100%">
                    <thead>
                        <tr>
                            <th>
                                <asp:GridView ID="grdUserReport" runat="server"
                                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                    EditRowStyle-BackColor="#FFFF99"
                                    PagerStyle-CssClass="pgr" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SRNO" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="SRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="USERNAME" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="USERNAME" runat="server" Text='<%# Eval("USERNAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="USERSTATUS">
                                            <ItemTemplate>
                                                <asp:Label ID="USERSTATUS" runat="server" Text='<%# Eval("USERSTATUS") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="USER MOBILE NO">
                                            <ItemTemplate>
                                                <asp:Label ID="USERMOBILENO" runat="server" Text='<%# Eval("Contact") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="BRCD">
                                            <ItemTemplate>
                                                <asp:Label ID="BRCD" runat="server" Text='<%# Eval("BRCD") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                       

                                    </Columns>
                                   
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

</asp:Content>

