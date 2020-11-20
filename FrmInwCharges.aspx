<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmInwCharges.aspx.cs" Inherits="FrmInwCharges" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script type="text/javascript">
         function validate() {
             var date, brcd, owg_type, type, fromamt, toamt, charges, placcount;
             date = document.getElementById('<% =TxtEffectDate.ClientID%>').value;
            charges = document.getElementById('<% =TxtCharges.ClientID%>').value;
            placcount = document.getElementById('<% =TxtPlacc.ClientID%>').value;
            var message = '';


            if (date == "") {
                // alert("Enter date");
                message = 'Please Enter Date....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=TxtEffectDate.ClientID %>').focus();
                return false;
            }
                     
            if (charges == "") {
                //alert("Enter Charges");
                message = 'Please Enter Charges....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=TxtCharges.ClientID %>').focus();
                return false;
            }
            if (placcount == "") {
                //alert("Enter PL account Number");
                message = 'Please Enter PL account Number....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=TxtPlacc.ClientID %>').focus();
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        Inward Charges
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
                                                    <asp:TextBox ID="TxtEffectDate" CssClass="form-control" onkeyup="FormatIt(this)" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane active" id="Div2">
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-11">
                                                <label class="control-label col-md-3">OWG Type: <span class="required"></span></label>
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="DdlReturnType" runat="server" Enabled="false" CssClass="form-control" AutoPostBack="true" >
                                                        <asp:ListItem Text="--Select type--" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="INWARD RETURN" Value="1" Selected="True"></asp:ListItem>
                                                      </asp:DropDownList>

                                                </div>
                                                 <label class="control-label col-md-2">Last Applied:<span class="required"></span></label>
                                                 <div class="col-md-4">
                                                    <asp:TextBox ID="TxtLastApplyDt" CssClass="form-control"  Enabled="false" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                   
                                    <div class="tab-pane active" id="Div7">
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-11">
                                                <label class="control-label col-md-3">Pl Account No : <span class="required"></span></label>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtPlacc" CssClass="form-control" Enabled="false" runat="server" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-2">Name <span class="required"></span></label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="TxtPLName" CssClass="form-control"  Enabled="false" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane active" id="Div1">
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-11">
                                                <label class="control-label col-md-3">Service Tax<span class="required"></span></label>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtSTax" CssClass="form-control" Enabled="false" runat="server" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-2">Name<span class="required"></span></label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="TxtSTaxName" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                     <div class="tab-pane active" id="Div6">
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-11">
                                                <label class="control-label col-md-3">Charges : <span class="required"></span></label>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtCharges" CssClass="form-control" runat="server"></asp:TextBox>
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
                                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn blue"  OnClientClick="javascript:return validate();" Text="Submit" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="Btn_Clear" runat="server" CssClass="btn blue" Text="Clear All" OnClick="Btn_Clear_Click" />
                                        <asp:Button ID="Btn_Exit" runat="server" CssClass="btn blue" Text="Exit" OnClick="Btn_Exit_Click"  />
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
                                <asp:GridView ID="grdCharged" runat="server" AllowPaging="True" EmptyDataText="Return Entries not Found"
                                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                    EditRowStyle-BackColor="#FFFF99"
                                    PagerStyle-CssClass="pgr" Width="100%">
                                    <Columns>

                                        <asp:TemplateField HeaderText="Effect Date" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="PEFDate" runat="server" Text='<%# Eval("EFFECTDATE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Charges Type">
                                            <ItemTemplate>
                                                <asp:Label ID="POType" runat="server" Text='<%# Eval("CHARGESTYPE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Type Name">
                                            <ItemTemplate>
                                                <asp:Label ID="PType" runat="server" Text='<%# Eval("DESCRIPTION") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Charges" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="PFAMT" runat="server" Text='<%# Eval("CHARGES") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        
                                       <asp:TemplateField HeaderText="PL Acc No">
                                            <ItemTemplate>
                                                <asp:Label ID="PCGLNo" runat="server" Text='<%# Eval("PLACC") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Edit" Visible="true">
                                            <ItemTemplate>
                                                <%--<asp:Button ID="Edit" runat="server" OnClick="Edit_Click" CommandArgument='<%#Eval("OWGID")%>' Text="ED" CssClass="btn-circle" />--%>
                                                <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("OWGID")%>' CommandName="select"  class="glyphicon glyphicon-edit"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Delete" Visible="true">
                                            <ItemTemplate>
                                                <%--<asp:Button ID="Delete" runat="server" OnClick="Delete_Click" CommandArgument='<%#Eval("OWGID")%>' Text="X" CssClass="btn-circle" />--%>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Eval("OWGID")%>' CommandName="select" class="glyphicon glyphicon-trash"></asp:LinkButton>
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

