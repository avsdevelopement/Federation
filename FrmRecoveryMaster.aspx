<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmRecoveryMaster.aspx.cs" Inherits="FrmRecoveryMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div id="page-wrapper">

        <div class="panel panel-warning">
            <div class="panel-heading">Master Modify</div>
            <div class="panel-body">
                <div class="tab-content">
                    <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                        <div class="col-lg-12">
                            <asp:RadioButtonList ID="Rdb_RecType" runat="server" RepeatDirection="Horizontal" Width="400px" AutoPostBack="true">
                                <asp:ListItem Value="S" Text="Send" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="A" Text="Post"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                        <div class="col-lg-12">
                            <div class="col-md-1">
                                <label class="control-label ">Brcd</label>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlBrCode" CssClass="form-control" Enabled="false" runat="server" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-1">
                                <label class="control-label ">Custno</label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="TxtCustno" Enabled="true" OnTextChanged="TxtCustno_TextChanged" placeholder="Cust No" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server" AutoPostBack="true" />
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-12 text-center">
                            <asp:Button ID="BtnSubmit" runat="server" CssClass="btn btn-success" Text="Update All" OnClientClick="Javascript:return isvalidate();" OnClick="BtnSubmit_Click" />
                            <asp:Button ID="BtnExit" runat="server" CssClass="btn btn-danger" Text="Exit" />
                        </div>
                    </div>

                </div>
            </div>

        </div>


        <div class="row" id="DivSumGrid" runat="server">
            <div class="col-md-12">
                <div class="table-scrollable" style="width: 100%; height: 500px; overflow-x: auto; overflow-y: auto">
                    <table class="table table-striped table-bordered table-hover">
                        <thead>
                            <tr>
                                <th>
                                    <%--Entrydate	Glcode	Subglcode	Accno	Custname	Amount	Particulars	Particulars2	Trxtype	Setno	Paymast	Mid	Cid	Stage CommandArgument='<%#Eval("Custno")+","+Eval("MM")+","+Eval("YYYY")%>'--%>
                                    <asp:GridView ID="Grd_MasterCust" runat="server" AutoGenerateColumns="false" OnRowUpdating="Grd_MasterCust_RowUpdating">
                                        <Columns>

                                            <asp:TemplateField HeaderText="Modify">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="Lnk_ConfirmModify" runat="server" class="glyphicon glyphicon-pencil" CommandName="Update" CommandArgument='<%#Eval("Custno")+","+Eval("MM")+","+Eval("YYYY")%>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                           
                                            <asp:TemplateField HeaderText="Stage">
                                                <ItemTemplate>
                                                    <asp:Label ID="Lbl_Stage" runat="server" Text='<%# Eval("Stage") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sr.No">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtSrNo" Enable="false" Style="width: 80px" CssClass="form-control" runat="server" Text='<%#Container.DataItemIndex+1 %>' Enabled="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="CustNo">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtCustNo" Text='<%# Eval("Custno") %>' Style="width: 80px" CssClass="form-control" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="CustName">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtCustName" Text='<%# Eval("CustName") %>' CssClass="form-control" Style="width: 200px" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="PostDate">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtPostDate" Text='<%# Eval("POSTDATE") %>' Style="width: 110px" CssClass="form-control" runat="server" Enabled="false"  />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PostSetno">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtPostSetno" Text='<%# Eval("POSTSETNO") %>' Style="width: 80px" CssClass="form-control" runat="server" Enabled="false"  />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Month">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtMonth" Text='<%# Eval("MM") %>' Style="width: 80px" CssClass="form-control" runat="server" Enabled="false"  />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Year">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtYear" Text='<%# Eval("YYYY") %>' Style="width: 80px" CssClass="form-control" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtStatus" Text='<%# Eval("Status") %>' Style="width: 80px" CssClass="form-control" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField HeaderText="Total Bal">
                                                <ItemTemplate>
                                                    <asp:Label ID="Lbl_RowTotal" runat="server" Text='<%# Eval("Total") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <div style="padding: 0 0 5px 0">
                                                        <asp:Label ID="Lbl_SumTotal" runat="server"></asp:Label>
                                                    </div>
                                                </FooterTemplate>
                                            </asp:TemplateField>--%>
                                        </Columns>
                                        <FooterStyle BackColor="#ccffcc" Font-Bold="True" ForeColor="Black" HorizontalAlign="Right" BorderStyle="None" />
                                    </asp:GridView>
                                    

                                </th>
                            </tr>
                        </thead>
                    </table>
                </div>
            </div>
        </div>
    </div>

    <asp:HiddenField ID="hdnCustNo" runat="server" Value="0" />

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

</asp:Content>

