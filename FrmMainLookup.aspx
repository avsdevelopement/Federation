<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CBSMaster.master" CodeFile="FrmMainLookup.aspx.cs" Inherits="FrmMainLookup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

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
    <script type="text/javascript">
        function IsValide() {
            debugger;
            var txtlno, txtltype, txtsrno, txtdesc, txtDESCRIPTIONMAR;
            txtlno = document.getElementById('<%=txtlno.ClientID%>').value;
            txtltype = document.getElementById('<%=txtltype.ClientID%>').value;
            txtsrno = document.getElementById('<%=txtsrno.ClientID%>').value;
            txtdesc = document.getElementById('<%=txtdesc.ClientID%>').value;
            txtDESCRIPTIONMAR = document.getElementById('<%=txtDESCRIPTIONMAR.ClientID%>').value;

            var message = '';

            if (txtlno == "") {
                message = 'Please Enter lno Type...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtlno.ClientID%>').focus();
                return false;
            }

            if (txtltype == "") {
                message = 'Please Enter ltype Type...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtltype.ClientID%>').focus();
                 return false;
             }

             if (txtsrno == "") {
                 message = 'Please Enter srno Type...!!\n';
                 $('#alertModal').find('.modal-body p').text(message);
                 $('#alertModal').modal('show')
                 document.getElementById('<%=txtsrno.ClientID%>').focus();
                 return false;
             }

             if (txtdesc == "") {
                 message = 'Please Enter description Type...!!\n';
                 $('#alertModal').find('.modal-body p').text(message);
                 $('#alertModal').modal('show')
                 document.getElementById('<%=txtdesc.ClientID%>').focus();
                 return false;
             }
            if (txtDESCRIPTIONMAR== "") {
                message = 'Please Enter DescriptionMar....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                 $('#alertModal').modal('show')
                 document.getElementById('<%=txtDESCRIPTIONMAR.ClientID%>').focus();
                 return false;
            }
         }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        Lookup Master
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">

                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Lookup No : <span class="required">* </span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtlno" CssClass="form-control" PlaceHolder="LNO" runat="server" Height="35" MaxLength="8" AutoPostBack="true" onkeypress="javascript:return isNumber (event)" OnTextChanged="txtlno_TextChanged"></asp:TextBox>
                                            </div>
                                            <label class="control-label col-md-2">Ltype : <span class="required">* </span></label>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtltype" CssClass="form-control" PlaceHolder="LTYPE" runat="server" MaxLength="50"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Sr No : <span class="required">* </span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtsrno" CssClass="form-control" PlaceHolder="SRNO" runat="server" MaxLength="8" AutoPostBack="true" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                            </div>
                                            <label class="control-label col-md-2">Description: <span class="required">* </span></label>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtdesc" CssClass="form-control" PlaceHolder="DESCRIPTION" runat="server" MaxLength="50"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Descriptiomar : <span class="required">* </span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtDESCRIPTIONMAR" CssClass="form-control" PlaceHolder="DESCRIPTIONMAR" runat="server" MaxLength="8"></asp:TextBox>
                                            </div>
                                            <label class="control-label col-md-2">Ref No : <span class="required">* </span></label>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="TXTREFNO" CssClass="form-control" runat="server" PlaceHolder="REF NO" MaxLength="50" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="col-md-offset-3 col-md-9">
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn blue" Text="Submit" OnClientClick="Javascript:return IsValide();" OnClick="btnSubmit_Click" />
                                    <asp:Button ID="btnModify" runat="server" CssClass="btn blue" Text="Update" OnClientClick="Javascript:return IsValide();" OnClick="btnModify_Click" />
                                    <asp:Button ID="btnDelete" runat="server" CssClass="btn blue" Text="Delete" OnClick="btnDelete_Click" />
                                    <asp:Button ID="BTNclear" runat="server" CssClass="btn blue" Text="Clear" OnClick="BTNclear_Click" />
                                    <asp:Button ID="btnfinish" runat="server" CssClass="btn blue" Text="New" OnClick="btnfinish_Click" />
                                    <asp:Button ID="BtnExit" runat="server" CssClass="btn blue" Text="Exit" OnClick="BtnExit_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                    </div>
                    <div class="col-lg-12">
                        <div class="table-scrollable">
                            <table class="table table-striped table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th>
                                            <asp:GridView ID="Grdcrdrdetailes" runat="server" AllowPaging="True"
                                                AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                EditRowStyle-BackColor="#FFFF99" PageSize="25"
                                                OnPageIndexChanging="Grdcrdrdetailes_PageIndexChanging"
                                                PagerStyle-CssClass="pgr" Width="100%" OnSelectedIndexChanged="Grdcrdrdetailes_SelectedIndexChanged">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="Select" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LNKSELECT" runat="server" Text="Select" CommandArgument='<%#Eval("LNO")+","+ Eval("SrNo")%>' OnClick="LNKSELECT_Click1" CommandName="select"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="List Number" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="listno" runat="server" Text='<%# Eval("LNO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="List Type" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="listtype" runat="server" Text='<%# Eval("LTYPE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SrNo" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="SrNo" runat="server" Text='<%# Eval("SrNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Description" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Desc" runat="server" Text='<%# Eval("DESCRIPTION") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Edit" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkedit" runat="server" OnClick="lnkedit_Click" CommandArgument='<%#Eval("LNO")+","+ Eval("SrNo")%>' Text="Edit" CommandName="select"></asp:LinkButton>
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
</asp:Content>

