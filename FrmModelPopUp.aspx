<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmModelPopUp.aspx.cs" Inherits="FrmModelPopUp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="modal fade bs-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" style="margin-left: 1%; width: 98%">
        <div class="modal-dialog modal-lg" role="document" style="width: 96%">
            <div class="modal-content" style="border: 5px solid #4dbfc0;">
                <div class="inner_top">
                    <div class="panel panel-default">
                        <div class="panel-heading">Loan Details</div>
                        <div class="panel-body">
                            <div class="col-sm-12">
                                <div class="col-md-12">

                                    <div class="col-md-12" style="border: 0px solid #ccc;">

                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">

                                            <div class="col-md-2" style="width: 148px">
                                                <label>Customer ID:</label>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:TextBox ID="txtCustId" CssClass="form-control" Placeholder="Cust No" ReadOnly="true" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2" style="width: 176px">

                                                <label class="pull-right">Branch Code & Name:</label>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:TextBox ID="txtBranchCode" CssClass="form-control" Placeholder="Cust Name" ReadOnly="true" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-1">
                                                <label>Account ID:</label>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:TextBox ID="txtAccId" CssClass="form-control" ReadOnly="true" Placeholder="Cust No" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-1">
                                                <label class="pull-right">Phone No.:</label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtPh" ReadOnly="true" CssClass="form-control" runat="server" />
                                            </div>

                                        </div>
                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">


                                            <div class="col-md-2" style="width: 148px">

                                                <label>Account Name:</label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtAcName" ReadOnly="true" CssClass="form-control" Placeholder="Cust Name" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2"></div>
                                            <div class="col-md-4">
                                                <asp:GridView ID="GridGurantor" runat="server" ShowHeader="false" AutoGenerateColumns="false" OnRowDataBound="GridGurantor_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Width="150px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblGurantor" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-Width="300px">
                                                            <ItemTemplate>
                                                                <%#Eval("Nominee") %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>


                                        </div>
                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-md-2" style="width: 148px">
                                                <label>GL Code & Name:</label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="GLcode" ReadOnly="true" CssClass="form-control" runat="server" />
                                            </div>
                                             <div class="col-md-2">
                                                <label>Customer Address:</label>
                                            </div>
                                            <div class="col-md-5">
                                                <asp:TextBox ID="txtCustAddress" ReadOnly="true" CssClass="form-control"
                                                    runat="server" TextMode="MultiLine" Text="Enter Text..." Rows="1" TabIndex="9"
                                                    Columns="20"></asp:TextBox>
                                            </div>
                                           
                                           

                                        </div>
                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                              <div class="col-md-2" style="width: 148px">
                                                <label>city & Pincode:</label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtCity" CssClass="form-control" Placeholder="city" ReadOnly="true" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <label>Loan Amount:</label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtlimit" CssClass="form-control" Placeholder="Loan Amt" ReadOnly="true" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-1">
                                                <label>Start Date:</label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtSD" CssClass="form-control" ReadOnly="true" Placeholder="Start Date" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 5px; margin-bottom: 10px;">

                                            <div class="col-md-2" style="width: 148px">

                                                <label>Int Rate</label>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:TextBox ID="txtIRate" CssClass="form-control" Placeholder="Int Rate" ReadOnly="true" runat="server"></asp:TextBox>
                                            </div>

                                            <div class="col-md-1" style="width: 176px">
                                                <label class="pull-right">Last Int Date:</label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtLD" ReadOnly="true" CssClass="form-control" runat="server" />
                                            </div>
                                            <div class="col-md-1">
                                                <label class="pull-right">End Date:</label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtED" ReadOnly="true" CssClass="form-control" runat="server" />
                                            </div>

                                        </div>




                                    </div>
                                    <hr />
                                </div>
                                <div class="col-lg-12">


                                    <div class="table-responsive">
                                        <div style="height: 250px; overflow: auto">
                                            <asp:GridView ID="GridRecords" FooterStyle-Font-Bold="true" Style="height: 50%" CssClass="tabbable-line" runat="server" AutoGenerateColumns="false" OnRowDataBound="GridRecords_RowDataBound" ShowFooter="true">
                                                <Columns>
                                               
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#1e9275" HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="Th" ItemStyle-Width="50px">
                                                        <HeaderTemplate>
                                                            Sr. No.
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                        <FooterTemplate></FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#1e9275" HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="Th" ItemStyle-Width="100px">
                                                        <HeaderTemplate>
                                                            EDate
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%#Eval("EDate1") %>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            Total Amount:
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" HeaderStyle-BackColor="#1e9275" HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="Th" ItemStyle-Width="250px">
                                                        <HeaderTemplate>
                                                            Credit PR
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%#Eval("Credit_PR") %>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblTCr" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" HeaderStyle-BackColor="#1e9275" HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="Th" ItemStyle-Width="250px">
                                                        <HeaderTemplate>
                                                            Debit PR
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%#Eval("Debit_PR") %>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblTDe" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" HeaderStyle-BackColor="#1e9275" HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="Th" ItemStyle-Width="250px">
                                                        <HeaderTemplate>
                                                            Interest Amt
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%#Eval("InterestAmt") %>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblTIn" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" HeaderStyle-BackColor="#1e9275" HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="Th" ItemStyle-Width="250px">
                                                        <HeaderTemplate>
                                                            Penal Int
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%#Eval("PenalInt") %>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblTPI" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" HeaderStyle-BackColor="#1e9275" HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="Th" ItemStyle-Width="250px">
                                                        <HeaderTemplate>
                                                            Int Receivable
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%#Eval("IntReceivable") %>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblTIR" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" HeaderStyle-BackColor="#1e9275" HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="Th" ItemStyle-Width="250px">
                                                        <HeaderTemplate>
                                                            Notice Charge
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%#Eval("NoticeCharge") %>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblTNC" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" HeaderStyle-BackColor="#1e9275" HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="Th" ItemStyle-Width="250px">
                                                        <HeaderTemplate>
                                                            Service Charge
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%#Eval("ServiceCharge") %>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblTSC" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" HeaderStyle-BackColor="#1e9275" HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="Th" ItemStyle-Width="250px">
                                                        <HeaderTemplate>
                                                            Court Charge
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%#Eval("CourtCharge") %>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblTCC" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" HeaderStyle-BackColor="#1e9275" HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="Th" ItemStyle-Width="250px">
                                                        <HeaderTemplate>
                                                            Ser Charge
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%#Eval("SerCharge") %>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblTSC1" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" HeaderStyle-BackColor="#1e9275" HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="Th" ItemStyle-Width="250px">
                                                        <HeaderTemplate>
                                                            Other Charge
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%#Eval("OtherCharge") %>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblTOC" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" HeaderStyle-BackColor="#1e9275" HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="Th" ItemStyle-Width="250px">
                                                        <HeaderTemplate>
                                                            Insurance
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%#Eval("Insurance") %>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblTI" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" HeaderStyle-BackColor="#1e9275" HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="Th" ItemStyle-Width="250px">
                                                        <HeaderTemplate>
                                                            Bank Charge
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%#Eval("BankCharge") %>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblTBC" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" HeaderStyle-BackColor="#1e9275" HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="Th" ItemStyle-Width="250px">
                                                        <HeaderTemplate>
                                                            Total BALANCE
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%#Eval("TBal") %>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblTTC" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" HeaderStyle-BackColor="#1e9275" HeaderStyle-ForeColor="White" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="Th" ItemStyle-Width="250px">
                                                        <HeaderTemplate>
                                                            CL BALANCE
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%#Eval("BALANCE") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    
                                                </Columns>
                                            </asp:GridView>
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

