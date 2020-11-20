<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CBSMaster.master" CodeFile="FrmAVS5078.aspx.cs" Inherits="FrmAVS5078" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
     <script>
         function IsValid() {
             debugger;
             var txtProdCode = document.getElementById('<%=txtProdCode.ClientID%>').value;
             var txtaccno = document.getElementById('<%=txtaccno.ClientID%>').value;
             var message = '';

             if (txtProdCode == "") {
                 message = 'Please Enter Product Code ...!!\n';
                 $('#alertModal').find('.modal-body p').text(message);
                 $('#alertModal').modal('show')
                 document.getElementById('<%=txtProdCode.ClientID%>').focus();
                 return false;
             }

             if (txtaccno == "") {
                 message = 'Please Enter AccNo...!!\n';
                 $('#alertModal').find('.modal-body p').text(message);
                 $('#alertModal').modal('show')
                 document.getElementById('<%=txtaccno.ClientID%>').focus();
                 return false;
             }

         }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


     <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        Saving Balance Certificate 
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
                                     
                                        <div id="divProd" runat="server" class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Product Code <span class="required">*</span></label>
                                                <div class="col-lg-2">
                                                    <asp:TextBox ID="txtProdCode" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" OnTextChanged= "txtProdCode_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtProdName" CssClass="form-control" OnTextChanged= "txtProdName_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                                    <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="txtProdName"
                                                        UseContextKey="true"
                                                        CompletionInterval="1"
                                                        CompletionSetCount="20"
                                                        MinimumPrefixLength="1"
                                                        EnableCaching="true"
                                                        ServicePath="~/WebServices/Contact.asmx"
                                                        ServiceMethod="GetGlName">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                            </div>
                                        </div>

                                        <div id="Acno" runat="server" class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">AccNo <span class="required">*</span></label>
                                                <div class="col-lg-2">
                                                    <asp:TextBox ID="txtaccno" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" OnTextChanged= "txtaccno_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtaccname" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged= "txtaccname_TextChanged"></asp:TextBox>
                                                    <div id="CustList4" style="height: 200px; overflow-y: scroll;"></div>
                                                        <asp:AutoCompleteExtender ID="Autoaccname4" runat="server" TargetControlID="txtaccname"
                                                            UseContextKey="true"
                                                            CompletionInterval="1"
                                                            CompletionSetCount="20"
                                                            MinimumPrefixLength="1"
                                                            EnableCaching="true"
                                                            ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList4"
                                                            ServiceMethod="GetAccName">
                                                        </asp:AutoCompleteExtender>
                                                </div>
                                            </div>
                                        </div>
                                     <%--   <div class="row" style="margin-bottom: 10px;">
                                            <div class="col-md-12">
                                                 <div class="col-md-2"></div>
                                                <div class="col-md-4">
                                                    <asp:RadioButtonList ID="Rdb_No" runat="server" RepeatDirection="Horizontal" Style="width: 400px;" AutoPostBack="true">
                                                       <asp:ListItem Text="Blank" Value="1"  ></asp:ListItem>
                                                         <asp:ListItem Text="Pre Printed" Value="2"></asp:ListItem>
                                                     </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>--%>
                                     </div>
                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9">
                                        <asp:Button ID="btnPrint" runat="server" CssClass="btn blue" Text="Print" OnClick= "btnPrint_Click" OnClientClick="Javascript:return IsValid();" />
                                        <asp:Button ID="btnExit" runat="server" CssClass="btn btn-primary" Text="Exit" OnClick= "btnExit_Click" />
                                          <asp:Button ID="BtnClear" runat="server" CssClass="btn btn-primary" Text="Clear" OnClick= "BtnClear_Click" />
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
    </div>
</asp:Content>