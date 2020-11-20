<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmOwgCharges.aspx.cs" Inherits="FrmOwgCharges" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
        function validate() {
            var date, brcd, owg_type, type, fromamt, toamt, charges, placcount;
            date = document.getElementById('<% =txteffect.ClientID%>').value;
            owg_type = document.getElementById('<% =txtotype.ClientID%>').value;
            type = document.getElementById('<% =txttype.ClientID%>').value;
            fromamt = document.getElementById('<% =txtamt.ClientID%>').value;
            toamt = document.getElementById('<% =txttoamt.ClientID%>').value;
            charges = document.getElementById('<% =txtcharges.ClientID%>').value;
            placcount = document.getElementById('<% =txtaccno.ClientID%>').value;
            var message = '';


            if (date == "") {
               // alert("Enter date");
                message = 'Please Enter Date....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=txteffect.ClientID %>').focus();
                return false;
            }
            if (owg_type == "") {
                // alert("Enter owgtype");
                message = 'Please Enter owgtype....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=txtotype.ClientID %>').focus();
                return false;
            }
            if (type == "") {
                //alert("Enter owgtype");
                message = 'Please Enter Type....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=txttype.ClientID %>').focus();
                return false;
            }
            if (fromamt == "") {
                //alert("Enter from amout");
                message = 'Please Enter Amount....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=txtamt.ClientID %>').focus();
                return false;
            }
            if (toamt == "") {
                // alert("Enter To amount");
                message = 'Please Enter To amount....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=txttoamt.ClientID %>').focus();
            return false;
        }
        if (charges == "") {
            //alert("Enter Charges");
            message = 'Please Enter Charges....!!\n';
            $('#alertModal').find('.modal-body p').text(message);
            $('#alertModal').modal('show')
            $('#<%=txtcharges.ClientID %>').focus();
            return false;
        }
        if (placcount == "") {
            //alert("Enter PL account Number");
            message = 'Please Enter PL account Number....!!\n';
            $('#alertModal').find('.modal-body p').text(message);
            $('#alertModal').modal('show')
            $('#<%=txtaccno.ClientID %>').focus();
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
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        Outward Charges
                    </div>
                </div>
                <div class="portlet-body form">
                    <!--<form action="#" class="form-horizontal" id="submit_form" method="POST">-->
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div class="alert alert-success display-none">
                                        <button class="close" data-dismiss="alert"></button>
                                        Your form validation is successful!
                                    </div>
                                    <div class="tab-pane active" id="tab1">
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-11">
                                                <label class="control-label col-md-3">Effect Date : <span class="required"></span></label>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txteffect" CssClass="form-control" onkeyup="FormatIt(this)" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane active" id="Div2">

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-11">
                                                <label class="control-label col-md-3">OWG Type: <span class="required"></span></label>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtotype" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="tab-pane active" id="Div3">

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-11">
                                                <label class="control-label col-md-3">Type : <span class="required"></span></label>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txttype" CssClass="form-control" runat="server" OnTextChanged="txttype_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtTName" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="tab-pane active" id="Div4">
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-11">
                                                <label class="control-label col-md-3">From amount: <span class="required"></span></label>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtamt" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="tab-pane active" id="Div5">
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-11">
                                                <label class="control-label col-md-3">To amount: <span class="required"></span></label>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txttoamt" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="tab-pane active" id="Div6">
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-11">
                                                <label class="control-label col-md-3">Charges : <span class="required"></span></label>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtcharges" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane active" id="Div7">
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-11">
                                                <label class="control-label col-md-3">Pl Account No : <span class="required"></span></label>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtaccno" CssClass="form-control" runat="server" OnTextChanged="txtaccno_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtAccName" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9">
                                        <%--<button type="button" class="btn blue" >Submit</button>--%>
                                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn blue" OnClick="btnSubmit_Click" OnClientClick="javascript:return validate();" Text="Submit" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--</form>-->
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
                                <asp:GridView ID="grdCharged" runat="server" AllowPaging="True"
                                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                    EditRowStyle-BackColor="#FFFF99"
                                    OnPageIndexChanging="grdCharged_PageIndexChanging"
                                    PagerStyle-CssClass="pgr" Width="100%">
                                    <Columns>

                                        <asp:TemplateField HeaderText="Effect Date" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="PEFDate" runat="server" Text='<%# Eval("effect_date","{0:dd/MM/yyyy}") %>'></asp:Label>
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

                                        <asp:TemplateField HeaderText="Type">
                                            <ItemTemplate>
                                                <asp:Label ID="PType" runat="server" Text='<%# Eval("type") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="From Amount" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="PFAMT" runat="server" Text='<%# Eval("from_amount") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="To Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="PTAMT" runat="server" Text='<%# Eval("to_amt") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Charegs">
                                            <ItemTemplate>
                                                <asp:Label ID="PSTS" runat="server" Text='<%# Eval("charges") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="PL Acc No">
                                            <ItemTemplate>
                                                <asp:Label ID="PCGLNo" runat="server" Text='<%# Eval("pl_accno") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Add New" Visible="true">
                                            <ItemTemplate>
                                                <%--<asp:Button ID="Addnew" runat="server" OnClick="Addnew_Click" Text="+" CssClass="btn-circle"  />                                                     --%>
                                                <asp:LinkButton ID="lnkAddNew" runat="server" CommandName="select" OnClick="lnkAddNew_Click" class="glyphicon glyphicon-plus" ViewStateMode="Enabled"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Edit" Visible="true">
                                            <ItemTemplate>
                                                <%--<asp:Button ID="Edit" runat="server" OnClick="Edit_Click" CommandArgument='<%#Eval("OWGID")%>' Text="ED" CssClass="btn-circle" />--%>
                                                <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("OWGID")%>' CommandName="select" OnClick="lnkEdit_Click" class="glyphicon glyphicon-edit"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Delete" Visible="true">
                                            <ItemTemplate>
                                                <%--<asp:Button ID="Delete" runat="server" OnClick="Delete_Click" CommandArgument='<%#Eval("OWGID")%>' Text="X" CssClass="btn-circle" />--%>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Eval("OWGID")%>' CommandName="select" OnClick="lnkDelete_Click" class="glyphicon glyphicon-trash"></asp:LinkButton>
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
</asp:Content>

