<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmIWOWCharges.aspx.cs" Inherits="FrmIWOWCharges" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
                        Return Charges
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
                                                    <asp:TextBox ID="TxtEffectDate" CssClass="form-control" onkeyup="FormatIt(this)" runat="server" ></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane active" id="Div2">
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-11">
                                                <label class="control-label col-md-3">Return Type<span class="required"></span></label>
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="DdlReturnType" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="DdlReturnType_TextChanged">
                                                        <asp:ListItem Text="--Select type--" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="INWORD RETURN" Value="1" Selected="True"></asp:ListItem>
                                                          <asp:ListItem Text="OUTWORD RETURN" Value="2"></asp:ListItem>
                                                      </asp:DropDownList>

                                                </div>
                                                 <label class="control-label col-md-2">Last Applied:<span class="required"></span></label>
                                                 <div class="col-md-2">
                                                    <asp:TextBox ID="TxtLastApplyDt" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="tab-pane active" id="Div6" runat="server" visible="false">
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-11">
                                                <label class="control-label col-md-3">Charges : <span class="required"></span></label>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtCharges" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="tab-pane active" id="Div7" runat="server" visible="false">
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-11">
                                                <label class="control-label col-md-3">Pl Account No : <span class="required"></span></label>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtPlacc" CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-2">Name <span class="required"></span></label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="TxtPLName" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane active" id="Div1" runat="server" visible="false">
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-11">
                                                <label class="control-label col-md-3">Service Tax<span class="required"></span></label>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtSTax" CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-2">Name<span class="required"></span></label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="TxtSTaxName" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9">
                                        <asp:Button ID="Btn_TrailRun" runat="server" CssClass="btn blue" OnClientClick="javascript:return validate();" Text="Trial Run" OnClick="Btn_TrailRun_Click"/>
                                        <asp:Button ID="Btn_Apply" runat="server" CssClass="btn blue" Text="Apply Charges" OnClick="Btn_Apply_Click" />
                                         <asp:Button ID="Btn_Report" runat="server" CssClass="btn blue" Text="Report" OnClick="Btn_Report_Click"/>
                                        <asp:Button ID="Btn_Exit" runat="server" CssClass="btn blue" Text="Exit" OnClick="Btn_Exit_Click" />
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
                     <asp:Label ID="Lbl_Tab" runat="server" Text="Trail Run" Font-Bold="true" Font-Size="Medium" Font-Italic="true"></asp:Label>
                    <thead>
                        <tr>
                            <th>
                                <asp:GridView ID="Grd_Trail" runat="server" AllowPaging="True"
                                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                    EditRowStyle-BackColor="#FFFF99"
                                    PagerStyle-CssClass="pgr" Width="100%">
                                    <Columns>
                                        <%--Glcode	Subglcode	SubglName	Accno	AccName	InstrNo	InstrDate	InstrAmt	TotalDrAmt	TotalCrAmt	CrPLAccNo	GLNAME	CrPLAmt	CrPLRate	--%>
                                        <%--<asp:TemplateField HeaderText="Glcode" Visible="true" HeaderStyle-BackColor="#99ccff">
                                            <ItemTemplate>
                                                <asp:Label ID="Glcode" runat="server" Text='<%# Eval("Glcode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Flag" HeaderStyle-BackColor="#99ccff">
                                            <ItemTemplate>
                                                <asp:Label ID="Flag" runat="server" Text='<%# Eval("Flag") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Sbgl" HeaderStyle-BackColor="#99ccff">
                                            <ItemTemplate>
                                                <asp:Label ID="Subglcode" runat="server" Text='<%# Eval("Subglcode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="SubglName" HeaderStyle-BackColor="#99ccff">
                                            <ItemTemplate>
                                                <asp:Label ID="SubglName" runat="server" Text='<%# Eval("SubglName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="A/C No" HeaderStyle-BackColor="#99ccff">
                                            <ItemTemplate>
                                                <asp:Label ID="Accno" runat="server" Text='<%# Eval("Accno") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Cust Name" Visible="true" HeaderStyle-BackColor="#99ccff">
                                            <ItemTemplate>
                                                <asp:Label ID="AccName" runat="server" Text='<%# Eval("AccName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="InstrNo" HeaderStyle-BackColor="#99ccff">
                                            <ItemTemplate>
                                                <asp:Label ID="InstrNo" runat="server" Text='<%# Eval("InstrNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="InstrDt" HeaderStyle-BackColor="#99ccff">
                                            <ItemTemplate>
                                                <asp:Label ID="InstrDate" runat="server" Text='<%# Eval("InstrDate") %>'></asp:Label>
                                            </ItemTemplate>
                                          </asp:TemplateField>

                                        <asp:TemplateField HeaderText="TotalDrAmt" HeaderStyle-BackColor="#ffccff">
                                            <ItemTemplate>
                                                <asp:Label ID="TotalDrAmt" runat="server" Text='<%# Eval("TotalDrAmt") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TotalCrAmt" HeaderStyle-BackColor="#99ffcc">
                                            <ItemTemplate>
                                                <asp:Label ID="TotalCrAmt" runat="server" Text='<%# Eval("TotalCrAmt") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="InstrAmt" HeaderStyle-BackColor="#99ccff">
                                            <ItemTemplate>
                                                <asp:Label ID="InstrAmt" runat="server" Text='<%# Eval("InstrAmt") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Bal" HeaderStyle-BackColor="#99ccff">
                                            <ItemTemplate>
                                                <asp:Label ID="AccBalance" runat="server" Text='<%# Eval("AccBalance") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                          <asp:TemplateField HeaderText="CrPLAccNo" HeaderStyle-BackColor="#99ccff">
                                            <ItemTemplate>
                                                <asp:Label ID="CrPLAccNo" runat="server" Text='<%# Eval("CrPLAccNo") %>'></asp:Label>
                                            </ItemTemplate>
                                         
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="PLGLNAME" HeaderStyle-BackColor="#99ccff">
                                            <ItemTemplate>
                                                <asp:Label ID="PLGLNAME" runat="server" Text='<%# Eval("PLGLNAME") %>'></asp:Label>
                                            </ItemTemplate>
                                           
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="CrPLAmt" HeaderStyle-BackColor="#99ccff">
                                            <ItemTemplate>
                                                <asp:Label ID="CrPLAmt" runat="server" Text='<%# Eval("CrPLAmt") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <%--CrGSTAccNo	Glname	CrGSTAmt	CrGSTRate	TotalChargesApp	AccBalance	Flag--%>
                                         <asp:TemplateField HeaderText="CrGSTAccNo" HeaderStyle-BackColor="#99ccff">
                                            <ItemTemplate>
                                                <asp:Label ID="CrGSTAccNo" runat="server" Text='<%# Eval("CrGSTAccNo") %>'></asp:Label>
                                            </ItemTemplate>
                                      
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="GSTGLNAME" HeaderStyle-BackColor="#99ccff">
                                            <ItemTemplate>
                                                <asp:Label ID="GSTGLNAME" runat="server" Text='<%# Eval("GSTGLNAME") %>'></asp:Label>
                                            </ItemTemplate>
                                         </asp:TemplateField>
                                         <asp:TemplateField HeaderText="CrGSTAmt" HeaderStyle-BackColor="#99ccff">
                                            <ItemTemplate>
                                                <asp:Label ID="CrGSTAmt" runat="server" Text='<%# Eval("CrGSTAmt") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        
                                         
                                        

                                        <%--<asp:TemplateField HeaderText="Add New" Visible="true">
                                            <ItemTemplate>
                                                
                                                <asp:LinkButton ID="lnkAddNew" runat="server" CommandName="select" OnClick="lnkAddNew_Click" class="glyphicon glyphicon-plus" ViewStateMode="Enabled"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Edit" Visible="true">
                                            <ItemTemplate>
                                                
                                                <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("OWGID")%>' CommandName="select" OnClick="lnkEdit_Click" class="glyphicon glyphicon-edit"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Delete" Visible="true">
                                            <ItemTemplate>
                                                
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Eval("OWGID")%>' CommandName="select" OnClick="lnkDelete_Click" class="glyphicon glyphicon-trash"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
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

