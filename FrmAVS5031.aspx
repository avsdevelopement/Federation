<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAVS5031.aspx.cs" Inherits="FrmAVS5031" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <script>
         function isNumber(evt) {
             var iKeyCode = (evt.which) ? evt.which : evt.keyCode
             if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                 return false;
             return true;
         }
</script> 
       <script type="text/javascript">
           function isvalidate() {


               var ddlHeading = document.getElementById('<%=ddlHeading.ClientID%>').value;
               var TxtBAlign = document.getElementById('<%=TxtBAlign.ClientID%>').value;
               var TxtAAlign = document.getElementById('<%=TxtAAlign.ClientID%>').value;
               var TxtCNO = document.getElementById('<%=TxtCNO.ClientID%>').value;
             
               var message = '';

               if (ddlHeading == "0") {
                   message = 'Please Select Heading...!!\n';
                   $('#alertModal').find('.modal-body p').text(message);
                   $('#alertModal').modal('show')
                   document.getElementById('<%=ddlHeading.ClientID%>').focus();
                   return false;
               }

               if (TxtBAlign == "") {
                   message = 'Please Enter Before Align....!!\n';
                   $('#alertModal').find('.modal-body p').text(message);
                   $('#alertModal').modal('show')
                   document.getElementById('<%=TxtBAlign.ClientID%>').focus();
                   return false;
               }

               if (TxtAAlign == "") {
                   message = 'Please Enter After Align....!!\n';
                   $('#alertModal').find('.modal-body p').text(message);
                   $('#alertModal').modal('show')
                   document.getElementById('<%=TxtAAlign.ClientID%>').focus();
                   return false;
               }

               if (TxtCNO == "") {
                   message = 'Please Enter CNo....!!\n';
                   $('#alertModal').find('.modal-body p').text(message);
                   $('#alertModal').modal('show')
                   document.getElementById('<%=TxtCNO.ClientID%>').focus();
                   return false;
               }
           }
       </script>
        <script type="text/javascript">
            function isvalidateCo() {


                var ddlheading1 = document.getElementById('<%=ddlheading1.ClientID%>').value;
                var TxtRowNo = document.getElementById('<%=TxtRowNo.ClientID%>').value;
                var TxtColNo = document.getElementById('<%=TxtColNo.ClientID%>').value;

               var message = '';

               if (ddlheading1 == "0") {
                   message = 'Please Select Heading...!!\n';
                   $('#alertModal').find('.modal-body p').text(message);
                   $('#alertModal').modal('show')
                   document.getElementById('<%=ddlheading1.ClientID%>').focus();
                   return false;
               }

                if (TxtRowNo == "") {
                   message = 'Please Enter Row Number....!!\n';
                   $('#alertModal').find('.modal-body p').text(message);
                   $('#alertModal').modal('show')
                   document.getElementById('<%=TxtRowNo.ClientID%>').focus();
                   return false;
               }

                if (TxtColNo == "") {
                   message = 'Please Enter Column Number....!!\n';
                   $('#alertModal').find('.modal-body p').text(message);
                   $('#alertModal').modal('show')
                   document.getElementById('<%=TxtColNo.ClientID%>').focus();
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
                        Passbook Setting Parameter
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div class="portlet-body">
                                          <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <div class="col-md-3"></div>
                                                <div class="col-md-6">
                                                    <asp:radiobuttonlist ID="ddlSelection" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSelection_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                        <asp:ListItem Text="Cover Setting" Value="0" Selected="True" style="padding-right:20px;"></asp:ListItem>
                                                        <asp:ListItem Text="Transaction Setting" Value="1"></asp:ListItem>
                                                    </asp:radiobuttonlist>
                                                   </div>
                                            </div>
                                        </div>
                                        <div runat="server" id="div_Cover">
                                             <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label">Heading : </label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="ddlheading1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlheading1_SelectedIndexChanged" ToolTip="Lookupform1 1100" CssClass="form-control" TabIndex="1"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label">Row Number : </label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtRowNo" onkeypress="javascript:return isNumber(event)" placeholder="Row No." runat="server" CssClass="form-control" TabIndex="2"></asp:TextBox>
                                                </div>
                                                 <div class="col-md-2"></div>
                                                <div class="col-md-2">
                                                    <label class="control-label">Column Number : </label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtColNo" onkeypress="javascript:return isNumber(event)" placeholder="Column No" runat="server" CssClass="form-control" TabIndex="3"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                             <div class="row">
                                            <div class="col-lg-12" style="text-align: center">
                                                <asp:Button ID="BtnSub" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="BtnSub_Click" TabIndex="5" OnClientClick="javascript:return isvalidateCo();"/>
                                                <asp:Button ID="BtnMod" runat="server" Text="Modify" CssClass="btn btn-primary" OnClick="BtnMod_Click" Visible="false" TabIndex="6" OnClientClick="javascript:return isvalidateCo();"/>
                                                <asp:Button ID="BtnDel" runat="server" Text="Delete" CssClass="btn btn-primary" OnClick="BtnDel_Click" Visible="false" TabIndex="7" OnClientClick="javascript:return isvalidateCo();"/>
                                            </div>
                                        </div>
                                        </div>
                                        <div runat="server" id="div_trans">
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label">Heading : </label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="ddlHeading" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlHeading_SelectedIndexChanged" ToolTip="Lookupform1 1099" CssClass="form-control" TabIndex="8"></asp:DropDownList>
                                                </div>
                                                 <div class="col-md-1"></div>
                                                 <div class="col-md-2">
                                                    <label class="control-label">GlCode : </label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtGlcode" onkeypress="javascript:return isNumber(event)" placeholder="GLCODE" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label">Before Align : </label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtBAlign" onkeypress="javascript:return isNumber(event)" placeholder="Before align" runat="server" CssClass="form-control" TabIndex="9"></asp:TextBox>
                                                </div>
                                                 <div class="col-md-2"></div>
                                                <div class="col-md-2">
                                                    <label class="control-label">After Align : </label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtAAlign" onkeypress="javascript:return isNumber(event)" placeholder="After align" runat="server" CssClass="form-control" TabIndex="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label">Cno : </label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtCNO" onkeypress="javascript:return isNumber(event)" placeholder="Cno" runat="server" CssClass="form-control" TabIndex="11"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-12" style="text-align: center">
                                                <asp:Button ID="BtnADD" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="BtnADD_Click" TabIndex="12" OnClientClick="javascript:return isvalidate();"/>
                                                <asp:Button ID="BtnModify" runat="server" Text="Modify" CssClass="btn btn-primary" OnClick="BtnModify_Click" Visible="false" TabIndex="13" OnClientClick="javascript:return isvalidate();"/>
                                                <asp:Button ID="BtnDelete" runat="server" Text="Delete" CssClass="btn btn-primary" OnClick="BtnDelete_Click" Visible="false" TabIndex="14" OnClientClick="javascript:return isvalidate();"/>
                                            </div>
                                        </div></div>
                                        
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
     <div class="row" runat="server" id="div_passbk" visible="false">
        <div class="col-lg-12">
            <div class="table-scrollable" style="border: none">
                <table class="table table-striped table-bordered table-hover">
                    <thead>
                        <tr>
                            <th>
                                <asp:GridView ID="GrdPassbook" runat="server" AllowPaging="True"
                                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                    EditRowStyle-BackColor="#FFFF99"
                                    OnPageIndexChanging="GrdPassbook_PageIndexChanging"
                                    PageIndex="10" PageSize="25"
                                    PagerStyle-CssClass="pgr" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="ID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Heading">
                                            <ItemTemplate>
                                                <asp:Label ID="Heading" runat="server" Text='<%# Eval("Heading") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Before Align">
                                            <ItemTemplate>
                                                <asp:Label ID="BRows" runat="server" Text='<%# Eval("BRows") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="After Align">
                                            <ItemTemplate>
                                                <asp:Label ID="Arows" runat="server" Text='<%# Eval("Arows") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Column Status">
                                            <ItemTemplate>
                                                <asp:Label ID="ColumnStatus" runat="server" Text='<%# Eval("ColumnStatus") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Entry Date">
                                            <ItemTemplate>
                                                <asp:Label ID="EntryDate" runat="server" Text='<%# Eval("EntryDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="GLCODE">
                                            <ItemTemplate>
                                                <asp:Label ID="glcode" runat="server" Text='<%# Eval("glcode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="CNO">
                                            <ItemTemplate>
                                                <asp:Label ID="Cno" runat="server" Text='<%# Eval("Cno") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Add">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkAdd" runat="server" CommandName="select" OnClick="lnkAdd_Click" class="glyphicon glyphicon-plus"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Modify">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkSelect" runat="server" CommandArgument='<%#Eval("ID")%>' CommandName="select" OnClick="lnkSelect_Click" class="glyphicon glyphicon-edit"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Eval("ID")%>' CommandName="select" OnClick="lnkDelete_Click" class="glyphicon glyphicon-trash"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Center" />
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
      <div id="Div_covergrd" class="row" runat="server" visible="false">
        <div class="col-lg-12">
            <div class="table-scrollable" style="border: none">
                <table class="table table-striped table-bordered table-hover">
                    <thead>
                        <tr>
                            <th>
                                <asp:GridView ID="grdCover" runat="server" AllowPaging="True"
                                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                    EditRowStyle-BackColor="#FFFF99"
                                    OnPageIndexChanging="grdCover_PageIndexChanging"
                                    PageIndex="10" PageSize="25"
                                    PagerStyle-CssClass="pgr" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="ID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Heading">
                                            <ItemTemplate>
                                                <asp:Label ID="Heading" runat="server" Text='<%# Eval("Heading") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Row Number">
                                            <ItemTemplate>
                                                <asp:Label ID="BRows" runat="server" Text='<%# Eval("PRows") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Column Number">
                                            <ItemTemplate>
                                                <asp:Label ID="Arows" runat="server" Text='<%# Eval("PColumn") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Column Status">
                                            <ItemTemplate>
                                                <asp:Label ID="ColumnStatus" runat="server" Text='<%# Eval("ColumnStatus") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Entry Date">
                                            <ItemTemplate>
                                                <asp:Label ID="EntryDate" runat="server" Text='<%# Eval("EntryDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Add">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkAdd1" runat="server" CommandName="select" OnClick="lnkAdd1_Click" class="glyphicon glyphicon-plus"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Modify">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkSelect1" runat="server" CommandArgument='<%#Eval("ID")%>' CommandName="select" OnClick="lnkSelect1_Click" class="glyphicon glyphicon-edit"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete1" runat="server" CommandArgument='<%#Eval("ID")%>' CommandName="select" OnClick="lnkDelete1_Click" class="glyphicon glyphicon-trash"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Center" />
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

