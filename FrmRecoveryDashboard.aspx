<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmRecoveryDashboard.aspx.cs" Inherits="FrmRecoveryDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <div class="panel panel-warning">
        <div class="panel-heading">Dashboard</div>
        <div class="panel-body">
            <div class="tab-content">
                <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                    <div class="col-lg-12">

                        <div class="col-md-1">
                            <label class="control-label ">MM/YYYY</label>
                        </div>
                        <div class="col-md-1">
                            <asp:TextBox ID="TxtMM" MaxLength="2" placeholder="MM" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server" OnTextChanged="TxtMM_TextChanged" AutoPostBack="true" />
                        </div>
                        <div class="col-md-1">
                            <asp:TextBox ID="TxtYYYY" MaxLength="4" placeholder="YYYY" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server" OnTextChanged="TxtYYYY_TextChanged" AutoPostBack="true" />
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
     <div class="row" id="DivSumGrid" runat="server">
            <div class="col-md-12">
                <div class="table-scrollable" style="width: 100%; overflow-x: auto; overflow-y: auto; height:600px">
                    <table class="table table-striped table-bordered table-hover">
                        <thead>
                            <tr>
                                <th>
                                    <%--RecDiv	RecCode	Descr	SumAmount--%>

                                    <asp:GridView ID="GrdSumDetails" runat="server" AutoGenerateColumns="false" OnSelectedIndexChanging="GrdSumDetails_SelectedIndexChanging" >
                                        <Columns>
                                            <asp:TemplateField HeaderText="Division">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblRecDiv" runat="server" Text='<%#Eval("RecDiv") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblRecDept" runat="server" Text='<%#Eval("RecCode") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblName" runat="server" Text='<%#Eval("Descr") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sum Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblSumAmount" runat="server" Text='<%#Eval("SumAmount") %>'></asp:Label>
                                                </ItemTemplate>
                                                 
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Report">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="Lnk_GenReport" runat="server" class="glyphicon glyphicon-plus" CommandName="Select"></asp:LinkButton>
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
    
</asp:Content>

