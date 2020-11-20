<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmBRHAuthorize.aspx.cs" Inherits="FrmBRHAuthorize" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box green">
                <div class="portlet-title">
                    <div class="caption">
                        Branch Handover
                                <asp:Literal ID="LtrlHeading" runat="server"></asp:Literal>
                    </div>
                    <div class="tools">
                        <a href="javascript:;" class="collapse" data-original-title="" title=""></a>
                        <a href="#portlet-config" data-toggle="modal" class="config" data-original-title="" title=""></a>
                    </div>
                </div>

                <div class="portlet-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Branch Handover : </strong></div>
                        </div>
                    </div>
                    <div class="row" style="margin: 7px 0 7px 0;">
                        <div class="col-lg-12">
                            <label class="control-label col-md-2">Authorised : <span class="required">* </span></label>
                            <div class="col-md-6">
                                <asp:RadioButton ID="rdbSingle" runat="server" Text="Single" AutoPostBack="true" OnCheckedChanged="rdbSingle_CheckedChanged" GroupName="SM" />
                                <asp:RadioButton ID="rdbMultiple" runat="server" Text="All" AutoPostBack="true" OnCheckedChanged="rdbMultiple_CheckedChanged" GroupName="SM" />&nbsp;
                                <asp:HyperLink ID="hlkOpen" runat="server" Text="DayOpen" NavigateUrl="~/FrmDayOpen.aspx"></asp:HyperLink>&nbsp;
                                <asp:HyperLink ID="hlkBrHand" runat="server" Text="DayClose" NavigateUrl="~/FrmDayClosed.aspx"></asp:HyperLink>&nbsp;
                                <asp:HyperLink ID="hlkReOpen" runat="server" Text="DayRe-Open" NavigateUrl="~/FrmDayReopen.aspx"></asp:HyperLink>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="margin: 7px 0 7px 0;" id="ALLBTN" runat="server" visible="false">
                        <div class="col-lg-12">
                            <asp:Button ID="BtnAll" runat="server" Text="All Authorise" CssClass="btn btn-primary" OnClick="BtnAll_Click" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="table-scrollable">
                                <table class="table table-striped table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th>
                                                <asp:GridView ID="GrdBranchH" runat="server" AllowPaging="True" AlternatingRowStyle-CssClass="alt"
                                                    AutoGenerateColumns="true" EditRowStyle-BackColor="#FFFF99" PageIndex="10" PageSize="25" PagerStyle-CssClass="pgr" Width="100%" OnPageIndexChanging="GrdBranchH_PageIndexChanging">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Select" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkSelect" runat="server" CommandArgument='<%#Eval("ID")%>' CommandName="select" OnClick="lnkSelect_Click" class="glyphicon glyphicon-check"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
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
</asp:Content>

