<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmRecEmailSend.aspx.cs" Inherits="FrmRecEmailSend" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="Btn_ConfirmSendMail" />
        </Triggers>
        <ContentTemplate>
            <div class="panel panel-warning">
                <div class="panel-heading">Email Sending</div>
                <div class="panel-body">
                    <div class="tab-content">
                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                            <div class="col-lg-12">
                                 <div class="col-md-1">
                                    <label class="control-label ">Subject</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="TxtSubject" CssClass="form-control" runat="server"/>
                                </div>
                                 <div class="col-md-1">
                                    <label class="control-label ">Body</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="TxtBody" CssClass="form-control" runat="server"  TextMode="Multiline"/>
                                </div>

                            </div>
                        </div>
                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                            <div class="col-lg-12">
                                <div class="col-md-1">
                                    <label class="control-label ">EmailID</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="TxtEmailID" CssClass="form-control" runat="server" AutoPostBack="true" />
                                </div>
                                <div class="col-md-1">
                                    <label class="control-label ">Attach File</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:FileUpload ID="F_UploadAttachment" CssClass="gui-input" runat="server" AutoPostBack="true" />
                                </div>

                            </div>
                        </div>
                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                            <div class="col-lg-12">
                                <asp:Button ID="Btn_ConfirmSendMail" runat="server" CssClass="btn btn-primary" OnClick="Btn_ConfirmSendMail_Click" Text="Confirm Send Mail" />
                                <asp:Button ID="Btn_Exit" runat="server" CssClass="btn btn-primary" Text="Exit" />
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <div class="row">
        <div class="col-md-12">
            <div class="table-scrollable" style="width: 100%; height: 500px; overflow-x: auto; overflow-y: auto">
                <table class="table table-striped table-bordered table-hover">

                    <thead>
                        <tr>
                            <th>
                                <asp:GridView ID="GrdShow" runat="server" CellPadding="6" CellSpacing="7"
                                    ForeColor="#333333" AutoGenerateColumns="False" CssClass="mGrid" BorderWidth="1px"
                                    BorderColor="#333300" Width="100%" 
                                    ShowFooter="true">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <%--ID,Brcd,EmailId,EntryDate,Stage,Mid,LOGINCODE--%>
                                        <asp:TemplateField HeaderText="ID" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="ID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Brcd" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="LblBrcd" runat="server" Text='<%# Eval("Brcd") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Email ID" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="LblEmailid" runat="server" Text='<%# Eval("EmailId") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Logincode" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="LblLOGINCODE" runat="server" Text='<%# Eval("LOGINCODE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <FooterStyle BackColor="#ccffcc" Font-Bold="True" ForeColor="Black" HorizontalAlign="Right" BorderStyle="None" />
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

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

