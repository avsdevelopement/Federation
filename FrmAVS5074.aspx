<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAVS5074.aspx.cs" Inherits="FrmAVS5074" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <script type="text/javascript" language="javascript">
         function ConfirmOnDelete() {
             if (confirm("Are you want to Confirm?") == true)
                 return true;
             else
                 return false;
         }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row ownformwrap">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1" style="margin-bottom: 0;">
                <div class="portlet-title">
                    <div class="caption">
                        Customer Transfer
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="row" style="margin-bottom: 5px;">
                                    <div class="col-lg-12">
                                        <div class="col-md-1">
                                            <label class="control-label ">Cust No<span class="required"></span></label>
                                        </div>
                                        <div class="col-md-1">
                                            <asp:TextBox ID="txtCustNo" ReadOnly="true" runat="server" CssClass="form-control" TabIndex="1" />
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtCustName" ReadOnly="true" runat="server" CssClass="form-control" TabIndex="2" />
                                        </div>

                                    </div>
                                </div>
                                <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                    <div class="col-lg-12">
                                        <div class="col-md-1">
                                            <label class="control-label ">From BRCD<span class="required"></span></label>
                                        </div>
                                        <div class="col-md-1">
                                            <asp:TextBox ID="txtFBrcd" ReadOnly="true" runat="server" CssClass="form-control" TabIndex="3" />
                                        </div>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="txtFBrcdName" ReadOnly="true" runat="server" CssClass="form-control" TabIndex="4" />
                                        </div>
                                        <div class="col-md-1"></div>
                                        <div class="col-md-1">
                                            <label class="control-label ">To BRCD<span class="required"></span></label>
                                        </div>
                                        <div class="col-md-1">
                                            <asp:TextBox ID="txtTBrcd" runat="server" CssClass="form-control" TabIndex="5" OnTextChanged="txtTBrcd_TextChanged" AutoPostBack="true" />
                                        </div>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="txtTBrcdName" runat="server" CssClass="form-control" TabIndex="6" />
                                        </div>
                                    </div>
                                </div>

                               <!--region for getting same account no-->
                                             <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                    <div class="col-lg-12">
                                        <div class="col-md-3">
                                            <label class="control-label "> Want Same Account No<span class="required"></span></label>
                                        </div>
                                     
                                        <div class="col-md-2">
                                           <asp:RadioButton GroupName="rdbsameaccgrp"  runat="server" ID="rdbsamey" Text="Yes"/>
                                            <asp:RadioButton GroupName="rdbsameaccgrp"  runat="server" ID="rdbsamen" Text="No" Checked="true"/>
                                        </div>
                                      
                                      
                                     
                                      
                                    </div>
                                </div>


                                
                               <!--region for CASHCR and TRFCR-->
                                

                                 <!--region for total account show-->
                                <div class="row" id="Div_grid" runat="server">
        <div class="col-md-12">
            <div class="table-scrollable" style="width: 100%; overflow-x: auto; overflow-y: auto">
                <table class="table table-striped table-bordered table-hover">
                    <thead>
                        <tr>
                            <th>
                                <asp:GridView ID="GrdDisp" runat="server" CellPadding="6" CellSpacing="7"
                                    ForeColor="#333333" PageIndex="5" AutoGenerateColumns="False" CssClass="mGrid" BorderWidth="1px"
                                    BorderColor="#333300" Width="100%" ShowFooter="true">
                                    <AlternatingRowStyle BackColor="White" />

                                    <Columns>
                                  

                                        <asp:TemplateField HeaderText="BRCD" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBRCD" runat="server" Text='<%# Eval("BRCD") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="GLCODE" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="GLCODE" runat="server" Text='<%# Eval("GLCODE ") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                               


                                        <asp:TemplateField HeaderText="Acc No" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblaccno" runat="server" Text='<%# Eval("AccNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Cust Name" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcustname" runat="server" Text='<%# Eval("CustName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Balance" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblbalance" runat="server" Text='<%# Eval("Balance") %>'></asp:Label>
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
                           


                                <div class="row" margin-bottom: 5px;">
                                    <div class="col-lg-12">
                                        <div class="col-md-12">
                                            <div class="col-md-9" align="center">
                                                <asp:Button ID="btnTransfer" runat="server" CssClass="btn btn-primary" OnClick="btnTransfer_Click" Text="Transfer Customer"  OnClientClick="return ConfirmOnDelete();"/>
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
    </div>
</asp:Content>

