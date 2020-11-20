<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="AVS5039.aspx.cs" Inherits="AVS5039" %>

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
            var txtdiv, txtdept, txtsrno, txtdesc, txtRemarks;
            txtdiv = document.getElementById('<%=txtdiv.ClientID%>').value;
            txtdept = document.getElementById('<%=txtdept.ClientID%>').value;
            txtdesc = document.getElementById('<%=txtdesc.ClientID%>').value;
            txtRemarks = document.getElementById('<%=txtRemarks.ClientID%>').value;

            var message = '';

            if (txtdiv == "") {
                message = 'Please Enter Recovery Div No...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtdiv.ClientID%>').focus();
                return false;
            }

            if (txtdept == "") {
                message = 'Please Enter Dept No...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtdept.ClientID%>').focus();
                return false;
            }
            if (txtdesc == "") {
                message = 'Please Enter Desc ...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtdesc.ClientID%>').focus();
                return false;
            }
           

            if (txtRemarks == "") {
                 message = 'Please Enter Remarks ...!!\n';
                 $('#alertModal').find('.modal-body p').text(message);
                 $('#alertModal').modal('show')
                 document.getElementById('<%=txtRemarks.ClientID%>').focus();
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
                       Recovery Div/Dept Add
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">

                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Recovery Div : <span class="required">* </span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtdiv" CssClass="form-control" PlaceHolder="Recovery Div" runat="server" MaxLength="8" AutoPostBack="true" onkeypress="javascript:return isNumber (event)" OnTextChanged= "txtdiv_TextChanged"></asp:TextBox>
                                            </div>
                                            <label class="control-label col-md-4">Recovery Dept: <span class="required">* </span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtdept" CssClass="form-control" PlaceHolder="Recovery dept" runat="server" MaxLength="50"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Description : <span class="required">* </span></label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtdesc" CssClass="form-control" Style="text-transform: uppercase"  PlaceHolder="Description" runat="server" MaxLength="50" AutoPostBack="true" ></asp:TextBox>
                                            </div>
                                            <label class="control-label col-md-2">Remarks : <span class="required">* </span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtRemarks" CssClass="form-control" PlaceHolder="Remarks" runat="server" MaxLength="50"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">DeleteLag : <span class="required">* </span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtDeleteLag" CssClass="form-control" PlaceHolder="DeleteLag" runat="server" MaxLength="8"></asp:TextBox>
                                            </div>
                                            <label class="control-label col-md-4">Temp Id : <span class="required">* </span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txttempid" CssClass="form-control" runat="server" PlaceHolder="Temp Id" MaxLength="10" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-actions">  
                                <div class="col-md-offset-3 col-md-9">
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn blue" Text="Submit" OnClientClick="Javascript:return IsValide();" OnClick= "btnSubmit_Click" />
                                     <asp:Button ID="btnModify" runat="server" CssClass="btn blue" Text="Modify"  OnClick= "btnModify_Click" />
                                     <asp:Button ID="BtnNew" runat="server" CssClass="btn blue" Text="Create New Div"  OnClick= "BtnNew_Click" />
                                   <%-- <asp:Button ID="btnModify" runat="server" CssClass="btn blue" Text="UPDATE" OnClientClick="Javascript:return IsValide();" OnClick="btnModify_Click" />
                                    <asp:Button ID="btnDelete" runat="server" CssClass="btn blue" Text="DELETE" OnClick="btnDelete_Click" />
                                    <asp:Button ID="BTNclear" runat="server" CssClass="btn blue" Text="Clear" OnClick="BTNclear_Click" />
                                    <asp:Button ID="btnfinish" runat="server" CssClass="btn blue" Text="New" OnClick="btnfinish_Click" />
                                    <asp:Button ID="BtnExit" runat="server" CssClass="btn blue" Text="Exit" OnClick="BtnExit_Click" />--%>
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
                                                AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" OnSelectedIndexChanged="Grdcrdrdetailes_SelectedIndexChanged"
                                                EditRowStyle-BackColor="#FFFF99" PageSize="25" OnPageIndexChanging="Grdcrdrdetailes_PageIndexChanging"
                                                PagerStyle-CssClass="pgr" Width="100%">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="Div No" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="DivNo" runat="server" Text='<%# Eval("RECDIV") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Rec Dept" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="RECCODE" runat="server" Text='<%# Eval("RECCODE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="DESCR" runat="server" Text='<%# Eval("DESCR") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="REMARK" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="REMARK" runat="server" Text='<%# Eval("REMARK") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="DELETELAG" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="DELETELAG" runat="server" Text='<%# Eval("DELETELAG") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="TEMPID" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="TEMPID" runat="server" Text='<%# Eval("TEMPID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Edit" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkedit" runat="server"  OnClick=  "lnkedit_Click"  CommandArgument='<%#Eval("RECDIV")+","+ Eval("RECCODE")%>' Text="MODIFY"  CommandName="SELECT"  class="glyphicon glyphicon"></asp:LinkButton>
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

