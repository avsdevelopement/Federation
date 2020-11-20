<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmInOut.aspx.cs" Inherits="FrmInOut" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function validate() {
            var ddlType = document.getElementById('<%=DdlType.ClientID%>').value;
            var rdbcr = document.getElementById('<%=rdbCredit.ClientID%>').value;
            var rdbdr = document.getElementById('<%=rdbDebit.ClientID%>').value;
            var ddlbank = document.getElementById('<%=ddlBankName.ClientID%>').value;
            var txtamt = document.getElementById('<%=TxtContraAmt.ClientID%>').value;

            if (ddlType == "0") {
                message = 'Select Clearing Type....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=DdlType.ClientID%>').focus();
                return false;
            }
            if (rdbcr == false && rdbdr == false) {
                message = 'Select Entry Type....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                // document.getElementById('<%=DdlType.ClientID%>').focus();
                return false;
            }
            if (ddlbank == "0") {
                message = 'Select Bank Of Clearing....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=ddlBankName.ClientID%>').focus();
                return false;
            }
            if (txtamt == "") {
                message = 'Enter Clearing Amount ....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtContraAmt.ClientID%>').focus();
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
                        Zone Posting
                    </div>
                </div>
                <div class="portlet-body form">
                    <!--<form action="#" class="form-horizontal" id="submit_form" method="POST">-->
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">

                                <div class="tab-content">
                                    <div id="error">
                                    </div>
                                    <div class="tab-pane active" id="tab1">

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-1">Dr Clg<span class="required"></span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtDrClearing" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-1">Cr Clg<span class="required"></span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtCrClearing" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                </div>

                                                <div class="col-md-4">
                                                    <asp:TextBox ID="TxtMsg" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <asp:Table ID="TblDiv_MainWindow" runat="server">
                                            <asp:TableRow ID="Tbl_R1" runat="server">
                                                <asp:TableCell ID="Tbl_c1" runat="server" Width="50%" BorderStyle="Solid" BorderWidth="1px">

                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="row" style="margin: 7px 0 12px 0">
                                                            <div class="col-lg-12">
                                                                <label class="control-label col-md-2">Type<span class="required">* </span></label>
                                                                <div class="col-md-4">
                                                                    <asp:DropDownList ID="DdlType" CssClass="form-control" runat="server" OnSelectedIndexChanged="DdlType_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                </div>
                                                                <label class="control-label col-md-2">Session<span class="required">* </span></label>
                                                                <div class="col-md-4">
                                                                    <asp:DropDownList ID="Dll_Session" CssClass="form-control" runat="server">
                                                                        <asp:ListItem Text="--Select Session--" Value="0"></asp:ListItem>
                                                                        <asp:ListItem Text="Morning Session" Value="1" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Text="Evening Session" Value="2"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="row" style="margin: 7px 0 12px 0">
                                                            <div class="col-lg-12">
                                                                <label class="control-label col-md-3">Entry Type <span class="required">* </span></label>
                                                                <div class="col-md-5">
                                                                    <asp:RadioButton ID="rdbCredit" runat="server" Text="Credit" GroupName="CD" Enabled="false" />
                                                                    <asp:RadioButton ID="rdbDebit" runat="server" Text="Debit" GroupName="CD" Enabled="false" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Clg Contra </strong></div>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-2">Bank<span class="required"></span></label>
                                                            <div class="col-md-4">
                                                                <asp:DropDownList ID="ddlBankName" CssClass="form-control" runat="server"></asp:DropDownList>
                                                            </div>
                                                            <label class="control-label col-md-2">Ctr Amt<span class="required"></span></label>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="TxtContraAmt" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-2">Diff Amt<span class="required"></span></label>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="TxtDiffAmt" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                            </div>
                                                            <label class="control-label col-md-2">Act Amt<span class="required"></span></label>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="TxtActualAmt" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </asp:TableCell>
                                                <asp:TableCell ID="TableCell1" runat="server" Width="50%" BorderStyle="Solid" BorderWidth="1px">
                                                  <div class="col-lg-12">
                                                <div class="table-scrollable" style="width:580px; height: 250px; overflow-x: auto; overflow-y: auto">
                                                    <table class="table table-striped table-bordered table-hover">
                                                                        <thead>
                                                                            <tr>
                                                                                <th>
                                                                                    <asp:GridView ID="GrdDetails" runat="server" CellPadding="6" CellSpacing="7"
                                                                                        ForeColor="#333333"
                                                                                        PageIndex="5" AutoGenerateColumns="False" CssClass="mGrid" BorderWidth="1px"
                                                                                        BorderColor="#333300" Width="100%"
                                                                                        ShowFooter="true">
                                                                                        <AlternatingRowStyle BackColor="White" />
                                                                                        <Columns>
                                                                                            <asp:TemplateField HeaderText="Subgl" Visible="true" HeaderStyle-BackColor="#99ffcc">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="Subgl" runat="server" Text='<%# Eval("Subgl") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="SubglName" Visible="true" HeaderStyle-BackColor="#99ffcc">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="SubglName" runat="server" Text='<%# Eval("SubglName") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>

                                                                                            <asp:TemplateField HeaderText="IW Sum Amount" Visible="true" HeaderStyle-BackColor="#99ccff">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="IwInstrSumAmt" runat="server" Text='<%# Eval("IwInstrSumAmt") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Iw Instr. Count" Visible="true" HeaderStyle-BackColor="#99ccff">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="IwInstrCount" runat="server" Text='<%# Eval("IwInstrCount") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>

                                                                                            <asp:TemplateField HeaderText="OW Sum Amount" Visible="true" HeaderStyle-BackColor="#ffcc99">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="OwInstrSumAmt" runat="server" Text='<%# Eval("OwInstrSumAmt") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>

                                                                                            <asp:TemplateField HeaderText="Ow Instr. Count" Visible="true" HeaderStyle-BackColor="#ffcc99">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="OwInstrCount" runat="server" Text='<%# Eval("OwInstrCount") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                                <FooterTemplate>
                                                                                                    <div style="padding: 0 0 5px 0">
                                                                                                        <asp:Label ID="Lbl_Numofnst" runat="server" />
                                                                                                    </div>
                                                                                                </FooterTemplate>
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
                                                        
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow ID="Tbl_Row2" runat="server">
                                                <asp:TableCell ID="TableCell2" runat="server" Width="100%" BorderStyle="Solid" BorderWidth="1px">
                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-4">OWCSend(A1) <span class="required"></span></label>
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="TxtOwcSendSum" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-4">IWC Recv(B1)<span class="required"></span></label>
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="TxtIwcRecSum" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-4">(A1-B1)<span class="required"></span></label>
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="TxtClgDiff" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-4">IWC Unclr<span class="required"></span></label>
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="TxtIWCUnclearSum" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:TableCell>


                                                <asp:TableCell ID="TableCell3" runat="server" Width="100%" BorderStyle="Solid" BorderWidth="1px">
                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-4">OWC Return(A2) <span class="required"></span></label>
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="TxtOwcReturnSum" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-4">IWC Return(B2)<span class="required"></span></label>
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="TxtIwcReturnSum" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-4">(A2-B2)<span class="required"></span></label>
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="TxtReturnClgDiff" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-4">OWC Unclr<span class="required"></span></label>
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="TxtOWCUnclearSum" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow ID="TableRow1" runat="server">
                                                <asp:TableCell ID="TableCell4" runat="server" Width="100%" BorderStyle="Solid" BorderWidth="1px">
                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-4">Total IWC<span class="required"></span></label>
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="TxtTotalIWCClg" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-4">Total OWC Today<span class="required"></span></label>
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="TxtTotalOWCClg" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:TableCell>
                                                <asp:TableCell ID="TableCell5" runat="server" Width="100%" BorderStyle="Solid" BorderWidth="1px">
                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-4">Clg House Cr.<span class="required"></span></label>
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="TxtClgHouseHOCr" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-4">Clg House Dr.<span class="required"></span></label>
                                                            <div class="col-md-6">
                                                                <asp:TextBox ID="TxtClgHouseHODr" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </div>
                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9">
                                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn blue" Text="Submit" OnClientClick="javascript:return validate();" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnUpdate" runat="server" CssClass="btn blue" Text="Delete" Visible="false" />
                                        <asp:Button ID="btmClear" runat="server" CssClass="btn blue" Text="Clear All" OnClick="btmClear_Click" />
                                        <asp:Button ID="btnReport" runat="server" CssClass="btn blue" Text="Report" />
                                        <asp:Button ID="Exit" runat="server" CssClass="btn blue" Text="Exit" OnClick="Exit_Click" />
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

