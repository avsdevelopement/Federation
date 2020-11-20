<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmBlank.aspx.cs" Inherits="FrmBlank" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function NewWindow() {
            document.forms[0].target = '_blank';
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-md-12">
        <marquee direction="left" onmouseover="this.stop();" onmouseout="this.start();">
                    <asp:Label ID="Lbl_Show" Text="" runat="server" style="color:#F00; font-size:18px;" scrollamount="30" ></asp:Label></marquee>
         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="GridDetails" />
        </Triggers>
        <ContentTemplate>
            <div id="page-wrapper">
                <div class="panel panel-warning">
                    <div class="panel-heading" align="center" style="color: black; font-weight: bold; font-size: larger">
                        New Release 
                      <asp:Label ID="lblname" runat="server" Style="font-weight: bold; font-size:large"></asp:Label>
                    </div>
                     <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                        <div class="col-md-12">
                            <asp:GridView ID="GridDetails" runat="server" CssClass="pager" AutoGenerateColumns="false" Width="100%" Style="color: white; font-weight: bold; font-size: medium"
                                DataKeyNames="id" AllowPaging="true" OnPageIndexChanging="GridDetails_PageIndexChanging" PageSize="15">
                                <PagerSettings Mode="NextPrevious" Position="Bottom" PreviousPageText="मागील" NextPageText="पुढील" />
                                <Columns>
                                    <asp:TemplateField HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center" Visible="false" HeaderStyle-HorizontalAlign="Left">

                                        <ItemTemplate>
                                            <asp:Label ID="LblId" runat="server" Style="color: black; font-weight: bold; font-size: small" Text='<%#Eval("Id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="4%" ItemStyle-HorizontalAlign="Center" Visible="false" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            SrNo
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="LblSrno" runat="server" Style="color: black; font-weight: bold; font-size: small" Text='<%#Eval("SrNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="6%" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown" ItemStyle-HorizontalAlign="Left" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            Date
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Lbldate" runat="server" Style="color: black; font-size: small" Text='<%#Eval("Date","{0:dd.MM.yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            Menu
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="LblMenu" runat="server" Style="color: black; font-weight: bold; font-size: small" Text='<%#Eval("Menu") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            Activity
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="LblActivity" runat="server" Style="color: black; font-weight: bold; font-size: small" Text='<%#Eval("Activity") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="50%" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            Requirement
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="LblRequ" runat="server" Style="color: black; font-weight: bold; font-size: small" Text='<%#Eval("Requirement") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="8%" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            Requirement By
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="LblReqBy" runat="server" Style="color: black; font-weight: bold; font-size: small" Text='<%#Eval("RequirementBy") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderStyle-BackColor="PeachPuff" HeaderStyle-ForeColor="Brown" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            User Manual
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LnkDisplay" runat="server" OnClientClick="return NewWindow();" OnClick="LnkDisplay_Click" CommandArgument='<%#Eval("Id")%>' CssClass="glyphicon glyphicon-plus"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <%--    <PagerStyle  />--%>
                            </asp:GridView>
                        </div>
                    </div>
                    </div>
                 
                </div>
                <div class="panel-body">
                    <div id="User" runat="server" visible="false">
                        <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-md-12">
                                <label class="control-label col-md-1">Date:<span class="required"> *</span> </label>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddlDate" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlDate_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                     </div>
                  
                 <asp:HiddenField ID="hdnRow" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
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
                    <button class="btn btn-default">Close</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
