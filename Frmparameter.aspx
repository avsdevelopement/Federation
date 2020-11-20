<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="Frmparameter.aspx.cs" Inherits="Frmparameter" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function ISValidate() {
            var TxtEFDT = document.getElementById('<%=TxtEffectDT.ClientID%>').value;
            var OType = document.getElementById('<%=TxtOWGType.ClientID%>').value;
            var ENTDT = document.getElementById('<%=TxtEntryDT.ClientID%>').value;
            var FunDT = document.getElementById('<%=TxtFunDT.ClientID%>').value;
            var CLGLN = document.getElementById('<%=TxtCLGNO.ClientID%>').value;
            var CLGLNAME = document.getElementById('<%=TxtCLGName.ClientID%>').value;
            var RTGLN = document.getElementById('<%=TxtRTGLNo.ClientID%>').value;
            var RTGLName = document.getElementById('<%=TxtRTGLName.ClientID%>').value;

            if (TxtEFDT == "") {
                alert("Please Enter Effect Date......!!!");
                return false;
            }
            if (BRCD == "") {
                alert("Please Enter Branch Code......!!!")
                return false;
            }
            if (OType == "") {
                alert("Please Enter Outward Type......!!!")
                return false;
            }
            if (ENTDT == "") {
                alert("Please Enter Entry Date......!!!")
                return false;
            }
            if (FunDT == "") {
                alert("Please Enter Funding Date......!!!")
                return false;
            }
            if (STS == "0") {
                alert("Please Select The Status ......!!!")
                return false;
            }
            if (CLGLN == "") {
                alert("Please Enter Clearing Gl code......!!!")
                return false;
            }
            if (CLGLNAME == "") {
                alert("Please Enter Clearing Gl Name......!!!")
                return false;
            }
            if (RTGLN == "") {
                alert("Please Enter Return Gl code......!!!")
                return false;
            }
            if (RTGLName == "") {
                alert("Please Enter Return Gl Name......!!!")
                return false;
            }
        }
    </script>
      <script type="text/javascript">
          function FormatIt(obj) {
              if (obj.value.length == 2) // Day
                  obj.value = obj.value + "/";
              if (obj.value.length == 5) // month 
                  obj.value = obj.value + "/";
              if (obj.value.length == 11) // year 
                  alert("Please Enter valid Date");
          }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<asp:UpdatePanel ID="updatepanel1" runat="server">
        <ContentTemplate>--%>
            <div class="row">
                <div class="col-md-12">
                    <div class="portlet box blue" id="form_wizard_1">
                        <div class="portlet-title">
                            <div class="caption">
                                Outward Parameter 
                            </div>
                        </div>

                        <div class="portlet-body form">                            
                            <div class="form-horizontal">
                                <div class="form-wizard">
                                    <div class="form-body">
                                        <div class="row">
                                            <div class="col-lg-10">
                                                <div class="col-lg-2">
                                                    <label class="control-label">Effect date</label>
                                                </div>
                                                <div class="col-lg-3">
                                                    <asp:TextBox ID="TxtEffectDT" runat="server" onkeyup="FormatIt(this)" CssClass="form-control"></asp:TextBox><span class="help-block"></span>
                                                   
                                                </div>
                                                <div class="col-lg-2">
                                                    <label class="control-label">Outward Type</label>
                                                </div>
                                                <div class="col-lg-3">
                                                    <asp:TextBox ID="TxtOWGType" runat="server" CssClass="form-control"></asp:TextBox><span class="help-block"></span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-10">
                                                <div class="col-lg-2">
                                                   <label class="control-label">Entry Day </label>
                                                </div>
                                                <div class="col-lg-3">
                                                    <asp:TextBox ID="TxtEntryDT" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <span class="help-block"></span>
                                                </div>
                                                <div class="col-lg-2">
                                                    <label class="control-label">Funding Day</label>
                                                </div>
                                                <div class="col-lg-3">
                                                    <asp:TextBox ID="TxtFunDT" runat="server" CssClass="form-control"></asp:TextBox><span class="help-block"></span>
                                                </div>
                                            </div>
                                        </div>
                                        
                                        <div class="row">
                                            <div class="col-lg-10">
                                                <div class="col-lg-2">
                                                    <label class="control-label">Clearing GL Code</label>
                                                </div>
                                                <div class="col-lg-3">
                                                    <asp:TextBox ID="TxtCLGNO" runat="server" CssClass="form-control" OnTextChanged="TxtCLGNO_TextChanged" AutoPostBack="true"></asp:TextBox><span class="help-block"></span>
                                                </div>
                                                <div class="col-lg-2">
                                                    <label class="control-label" style="font-size:13px">Clearing GL Name</label>
                                                </div>
                                                <div class="col-lg-3">
                                                    <asp:TextBox ID="TxtCLGName" runat="server" CssClass="form-control"></asp:TextBox><span class="help-block"></span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-10">
                                                <div class="col-lg-2">
                                                    <label class="control-label">Reurn GL Code</label>
                                                </div>
                                                <div class="col-lg-3">
                                                    <asp:TextBox ID="TxtRTGLNo" runat="server" CssClass="form-control" OnTextChanged="TxtRTGLNo_TextChanged" AutoPostBack="true"></asp:TextBox><span class="help-block"></span>
                                                </div>
                                                <div class="col-lg-2">
                                                    <label class="control-label">Return GL Name</label>
                                                </div>
                                                <div class="col-lg-3">
                                                    <asp:TextBox ID="TxtRTGLName" runat="server" CssClass="form-control"></asp:TextBox><span class="help-block"></span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-10" style="text-align: center">
                                                <asp:Button ID="Submit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClientClick="javascript:return ISValidate(); " OnClick="Submit_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="table-scrollable">
                        <table class="table table-striped table-bordered table-hover">
                            <thead>
                                <tr>
                                    <th>
                                        <asp:GridView ID="grdMaster" runat="server" AllowPaging="True"
                                            AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                            EditRowStyle-BackColor="#FFFF99"
                                            OnPageIndexChanging="grdMaster_PageIndexChanging"
                                            PagerStyle-CssClass="pgr" Width="100%">
                                            <Columns>

                                                <asp:TemplateField HeaderText="Effect Date" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="PEFDate" runat="server" Text='<%# Eval("effectdate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Branch Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="PBRCD" runat="server" Text='<%# Eval("brcd") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Outword Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="POType" runat="server" Text='<%# Eval("owg_type") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Entry Date" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="PEDT" runat="server" Text='<%# Eval("entrydate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Funding Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="PFDT" runat="server" Text='<%# Eval("funndingdate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="PSTS" runat="server" Text='<%# Eval("status") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Clearing Gl no">
                                                    <ItemTemplate>
                                                        <asp:Label ID="PCGLNo" runat="server" Text='<%# Eval("clg_gl_no") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Clearing Gl Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="PCGLNm" runat="server" Text='<%# Eval("clg_gl_name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Return Gl no">
                                                    <ItemTemplate>
                                                        <asp:Label ID="PRGLNo" runat="server" Text='<%# Eval("return_gl_no") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Return Gl Namae">
                                                    <ItemTemplate>
                                                        <asp:Label ID="PRGLNm" runat="server" Text='<%# Eval("return_gl_name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Add New" Visible="true">
                                                    <ItemTemplate>
                                                        <%--<asp:Button ID="Addnew" runat="server" OnClick="Addnew_Click" Text="+" CssClass="btn-circle" />--%>
                                                        <asp:LinkButton ID="lnkAddNew" runat="server" CommandName="select" OnClick="lnkAddNew_Click" class="glyphicon glyphicon-plus" ViewStateMode="Enabled"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Edit" Visible="true">
                                                    <ItemTemplate>
                                                        <%--<asp:Button ID="Edit" runat="server" OnClick="Edit_Click" CommandArgument='<%#Eval("OID")%>' Text="ED" CssClass="btn-circle" />--%>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("OID")%>' CommandName="select" OnClick="lnkEdit_Click" class="glyphicon glyphicon-edit"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Delete" Visible="true">
                                                    <ItemTemplate>
                                                        <%--<asp:Button ID="Delete" runat="server" OnClick="Delete_Click" CommandArgument='<%#Eval("OID")%>' Text="X" CssClass="btn-circle" />--%>
                                                        <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Eval("OID")%>' CommandName="select" OnClick="lnkDelete_Click" class="glyphicon glyphicon-trash"></asp:LinkButton>
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
        <%--</ContentTemplate>
    </asp:UpdatePanel>--%>


</asp:Content>

