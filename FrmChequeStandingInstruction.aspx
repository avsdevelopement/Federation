<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmChequeStandingInstruction.aspx.cs" Inherits="FrmChequeStandingInstruction" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<script type="text/javascript">
    function isvalidate() {

        var ProdCode, FromSeries, ToSeries, NoOfLeaf;
        ProdCode = document.getElementById('<%=txtProdCode.ClientID%>').value;
            
    var message = '';

    if (ProdCode == "") {
        message = 'Please Enter Product Code....!!\n';
        $('#alertModal').find('.modal-body p').text(message);
        $('#alertModal').modal('show')
        document.getElementById('<%=txtProdCode.ClientID%>').focus();
                return false;
            }
        }
    </script>
    <script>
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        Cheque Book Request
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
                                            <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Product Code <span class="required">*</span></label>
                                                    <div class="col-lg-2">
                                                        <asp:TextBox ID="txtProdCode" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" OnTextChanged="txtProdCode_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtProdName" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                                    </div>
                                           </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Account Number <span class="required">*</span></label>
                                                    <div class="col-lg-2">
                                                        <asp:TextBox ID="txtAccNo" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" OnTextChanged="txtAccNo_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtAccName" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                                    </div>
                                           </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Cheque Type <span class="required">*</span></label>
                                                <div class="col-lg-8">
                                                    <asp:RadioButton ID="rbtnPersonalised" Text="Personalized" runat="server" OnCheckedChanged="rbtnPersonalised_CheckedChanged" AutoPostBack="true" />
                                                    <asp:RadioButton ID="rbtnBlank" Text="Blank" runat="server" OnCheckedChanged="rbtnBlank_CheckedChanged" AutoPostBack="true"/>
                                                </div>
                                           </div>
                                        </div>

                                        <div id="divSeries" runat="server" class="row" style="margin: 7px 0 7px 0" visible="false">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">From Series <span class="required">*</span></label>
                                                <div class="col-lg-2">
                                                    <asp:TextBox ID="txtFromSeries" onkeypress="javascript:return isNumber (event)" OnTextChanged="txtFromSeries_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-2">To Series <span class="required">*</span></label>
                                                <div class="col-lg-2">
                                                    <asp:TextBox ID="txtToSeries" onkeypress="javascript:return isNumber (event)" OnTextChanged="txtToSeries_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2">
                                                        <asp:Label id="lblSeriesTotal" runat="server" />
                                                </div>
                                           </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">No Of Leaves <span class="required">*</span></label>
                                                <div class="col-lg-3">
                                                    <asp:TextBox ID="txtNoLeaves" OnTextChanged="txtNoLeaves_TextChanged" AutoPostBack="true" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" />
                                                </div>
                                           </div>
                                        </div>

                                    </div>

                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9">
                                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn blue" Text="SUBMIT" OnClick="btnSubmit_Click" OnClientClick="Javascript:return isvalidate();" />
                                        <asp:Button ID="btnAuthorised" Text="AUTHORISE" CssClass="btn blue" runat="server" OnClick="btnAuthorized_Click"/>
                                        <asp:Button ID="btnModify" runat="server" CssClass="btn blue" Text="MODIFY" OnClick="btnModify_Click"  />
                                        <asp:Button ID="btnDelete" runat="server" CssClass="btn blue" Text="DELETE" OnClick="btnDelete_Click"  />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                    </div>
                    <div class="col-md-12">
                        <table class="table table-striped table-bordered table-hover" width="100%">
                            <thead>
                                <tr>
                                    <th>
                                        <asp:GridView ID="grdChequeRequest" runat="server" AllowPaging="True" AutoGenerateColumns="FALSE" AlternatingRowStyle-CssClass="alt" CssClass="mGrid"
                                            EditRowStyle-BackColor="#FFFF99" OnPageIndexChanging="grdChequeRequest_PageIndexChanging" PagerStyle-CssClass="pgr" Width="100%">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SUB GLCODE" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="SUBGLCODE" runat="server" Text='<%# Eval("SUBGLCODE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="SUB GLNAME">
                                                    <ItemTemplate>
                                                        <asp:Label ID="SUBGLNAME" runat="server" Text='<%# Eval("SUBGLNAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Acc umber">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ACCNO" runat="server" Text='<%# Eval("ACCNO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                                                                
                                                <asp:TemplateField HeaderText="From Series" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="FSERIES" runat="server" Text='<%# Eval("FSERIES") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="To Series">
                                                    <ItemTemplate>
                                                        <asp:Label ID="TSERIES" runat="server" Text='<%# Eval("TSERIES") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Type Of Cheque">
                                                    <ItemTemplate>
                                                        <asp:Label ID="CHEQUETYPE" runat="server" Text='<%# Eval("CHEQUETYPE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="No Of Page">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LEAF" runat="server" Text='<%# Eval("LEAF") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="No Of Book" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="CHEQUEBOOK" runat="server" Text='<%# Eval("CHEQUEBOOK") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Total No of Page">
                                                    <ItemTemplate>
                                                        <asp:Label ID="NOOFLEAF" runat="server" Text='<%# Eval("NOOFLEAF") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Select" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkSelect" runat="server" CommandArgument='<%#Eval("id")%>' CommandName="select" OnClick="lnkSelect_Click" class="glyphicon glyphicon-check"></asp:LinkButton>
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
    </div>

</asp:Content>

