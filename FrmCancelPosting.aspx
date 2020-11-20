<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmCancelPosting.aspx.cs" Inherits="FrmCancelPosting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div id="page-wrapper">

        <div class="panel panel-warning">
            <div class="panel-heading">Cancel Posting </div>
            <div class="panel-body">
                <div class="tab-content">
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
                                <label class="control-label ">Entry Date</label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="TxtEntryDate" Enabled="false" placeholder="Setno" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server" AutoPostBack="true" />
                            </div>
                        </div>
                    </div>



                    <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                        <div class="col-lg-12">

                            <div class="col-md-1">
                                <label class="control-label">Setno</label>
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="TxtSetNo" placeholder="Setno" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server" AutoPostBack="true" />
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-12 text-center">
                            <asp:Button ID="BtnSubmit" runat="server" CssClass="btn btn-success" Text="Submit" OnClientClick="Javascript:return isvalidate();" OnClick="BtnSubmit_Click" />
                            <asp:Button ID="BtnExit" runat="server" CssClass="btn btn-danger" Text="Exit" />
                        </div>
                    </div>
                </div>
            </div>

        </div>

        <div id="DivSumGrid" class="col-md-12" runat="server">
            <div class="table-scrollable" style="width: 100%; height: auto; max-height: 400px; overflow-x: auto; overflow-y: auto">
                <table class="table table-striped table-bordered table-hover" width="100%">
                    <thead>
                        <tr>
                            <th>

                                <asp:GridView ID="Grd_EntryDetail" runat="server" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Setno">
                                            <ItemTemplate>
                                                <asp:Label ID="LblSetno" Style="width: 80px" runat="server" Text='<%#Eval("Setno") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Subgl code">
                                            <ItemTemplate>
                                                <asp:Label ID="LblSubgl" Style="width: 80px" runat="server" Text='<%#Eval("Subglcode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Account No">
                                            <ItemTemplate>
                                                <asp:Label ID="LblAccno" Style="width: 80px" runat="server" Text='<%#Eval("Accno") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Cust Name">
                                            <ItemTemplate>
                                                <asp:Label ID="LblCustname" Style="width: 200px" runat="server" Text='<%#Eval("Custname") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Particulars">
                                            <ItemTemplate>
                                                <asp:Label ID="LblP1" Style="width: 300px" runat="server" Text='<%#Eval("Particulars") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="LblAmt" Style="width: 100px" runat="server" Text='<%#Eval("Amount") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Cr/Dr">
                                            <ItemTemplate>
                                                <asp:Label ID="LblCrDr" Style="width: 50px" runat="server" Text='<%#Eval("Trxtype") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="UserId">
                                            <ItemTemplate>
                                                <asp:Label ID="LblMid" Style="width: 50px" runat="server" Text='<%#Eval("Mid") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatus" Style="width: 100px" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="CheckrId">
                                            <ItemTemplate>
                                                <asp:Label ID="LblCid" Style="width: 100px" runat="server" Text='<%#Eval("Cid") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Cancel">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" Style="width: 100px" runat="server" CommandArgument='<%#Eval("Setno")%>' CommandName="select" class="glyphicon glyphicon-trash" OnClick="lnkDelete_Click"></asp:LinkButton>
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

