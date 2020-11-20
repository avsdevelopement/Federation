<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmTally_Check.aspx.cs" Inherits="FrmTally_Check" %>

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
            <div class="portlet box blue" id="Div1">
                <div class="portlet-title">
                    <div class="caption">
                        Admin's Tally Tool
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab1">
                                        <asp:Table ID="Tbl1" runat="server">
                                            <asp:TableRow ID="Tbl_row1" runat="server">
                                                <asp:TableCell ID="Tbl_Cell1" runat="server" Style="width: 50%;">
                                                    <div class="row" style="margin-bottom: 10px;">
                                                        <div class="col-lg-6">
                                                            <div class="col-md-3">
                                                                <asp:RadioButtonList ID="Rdb_RepType" runat="server" RepeatDirection="Horizontal" Style="width: 200px;" OnSelectedIndexChanged="Rdb_RepType_SelectedIndexChanged" AutoPostBack="true">
                                                                    <asp:ListItem Text="Summary" Value="1" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="Details" Value="2"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin-bottom: 10px;">
                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-12">
                                                                <label class="control-label col-md-2">From Date <span class="required">* </span></label>
                                                                <div class="col-md-2">
                                                                    <asp:TextBox ID="TxtFDate" CssClass="form-control" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this)" runat="server"></asp:TextBox>
                                                                    <asp:TextBoxWatermarkExtender ID="TxtAsOnDate_Extender2" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtFDate">
                                                                    </asp:TextBoxWatermarkExtender>
                                                                    <asp:CalendarExtender ID="TxtAsOnDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtFDate">
                                                                    </asp:CalendarExtender>
                                                                </div>
                                                                <label class="control-label col-md-2">To Date <span class="required">* </span></label>
                                                                <div class="col-md-2">
                                                                    <asp:TextBox ID="TxtTDate" CssClass="form-control" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this)" runat="server"></asp:TextBox>
                                                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtTDate">
                                                                    </asp:TextBoxWatermarkExtender>
                                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtTDate">
                                                                    </asp:CalendarExtender>
                                                                </div>
                                                            </div>

                                                        </div>
                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-12">
                                                                <label class="control-label col-md-2">BRCD <span class="required">*</span></label>
                                                                <div class="col-md-2">
                                                                    <asp:TextBox ID="TxtFBRCD" Placeholder="From BRCD" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" OnTextChanged="TxtFBRCD_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                </div>
                                                                <label class="control-label col-md-2">BRCD Name <span class="required">*</span></label>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="TxtFBRCDName" CssClass="form-control" runat="server"></asp:TextBox>
                                                                </div>

                                                            </div>
                                                        </div>

                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-12">
                                                                <label class="control-label col-md-2">PRD Cd.<span class="required">*</span></label>
                                                                <div class="col-md-2">
                                                                    <asp:TextBox ID="TxtFPRD" Placeholder="From Product Code" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" OnTextChanged="TxtFPRD_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                </div>
                                                                <label class="control-label col-md-2">PRD Name <span class="required">*</span></label>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="TxtFPRDName" CssClass="form-control" runat="server" OnTextChanged="TxtFPRDName_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                    <div id="CustList" style="height: 200px; overflow-y: scroll;"></div>
                                                                    <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="TxtFPRDName"
                                                                        UseContextKey="true"
                                                                        CompletionInterval="1"
                                                                        CompletionSetCount="20"
                                                                        MinimumPrefixLength="1"
                                                                        EnableCaching="true"
                                                                        ServicePath="~/WebServices/Contact.asmx"
                                                                        ServiceMethod="getglname" CompletionListElementID="CustList">
                                                                    </asp:AutoCompleteExtender>
                                                                </div>


                                                            </div>
                                                        </div>
                                                        <div class="row" style="margin: 20px; border-bottom: 1px solid rgba(53, 152, 220, 0.55);"><strong></strong></div>
                                                        <div class="row" style="margin: 7px 0 7px 0; text-align: center">
                                                            <div class="col-lg-12">
                                                                <div class="col-md-6">
                                                                    <asp:Button ID="Btn_Submit" OnClientClick="Javascript:return isvalidate();" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="Btn_Submit_Click" />
                                                                    <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-primary" Text="Report Print" OnClick="btnPrint_Click"  OnClientClick="Javascript:return Validate();" />
                                                                    <asp:Button ID="Btn_ClearAll" runat="server" Text="Clear All" CssClass="btn btn-success" OnClick="Btn_ClearAll_Click" />
                                                                    <asp:Button ID="Btn_Exit" runat="server" Text="Exit" CssClass="btn btn-success" OnClick="Btn_Exit_Click" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                        <asp:Table ID="Tbl2" runat="server">
                                            <asp:TableRow ID="Tbl2_Row1" runat="server">
                                                <asp:TableCell ID="Tbl2_Cell1" runat="server" class="table-scrollable">
                                                    <thead>
                                                        <tr>
                                                            <th>
                                                                <asp:GridView ID="Grid_Summary" runat="server" OnPageIndexChanging="Grid_Summary_PageIndexChanging"
                                                                     PageSize="25"
                                                                    AutoGenerateColumns="false">
                                                                    <Columns>

                                                                        <asp:TemplateField HeaderText="YEAR" Visible="true" HeaderStyle-BackColor="#99ccff">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="YEAR" runat="server" Text='<%# Eval("Year") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="MONTH" HeaderStyle-BackColor="#99ccff">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="MONTH" runat="server" Text='<%# Eval("Month") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="BRCD" HeaderStyle-BackColor="#99ccff">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="BRCD" runat="server" Text='<%# Eval("BrCD") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="AVSM OP BAL" HeaderStyle-BackColor="#ccccff">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="AVSMOP" runat="server" Text='<%# Eval("AVSM_OP") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="AVSM CR BAL" HeaderStyle-BackColor="#ccccff">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="AVSMCR" runat="server" Text='<%# Eval("AVSM_CR") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="AVSM DR BAL" HeaderStyle-BackColor="#ccccff">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="AVSMDR" runat="server" Text='<%# Eval("AVSM_DR") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="AVSM CL BAL" HeaderStyle-BackColor="#ccccff">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="AVSMCL" runat="server" Text='<%# Eval("AVSM_CL") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="AVSB OP BAL" HeaderStyle-BackColor="#ffcccc">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="AVSBOP" runat="server" Text='<%# Eval("AVSB_OP") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="AVSM CR BAL" HeaderStyle-BackColor="#ffcccc">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="AVSBCR" runat="server" Text='<%# Eval("AVSB_CR") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="AVSB DR BAL" HeaderStyle-BackColor="#ffcccc">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="AVSBDR" runat="server" Text='<%# Eval("AVSB_DR") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="AVSB CL BAL" HeaderStyle-BackColor="#ffcccc">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="AVSBCL" runat="server" Text='<%# Eval("AVSB_CL") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Total Diff" HeaderStyle-BackColor="#66ff33">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="TOTALDIFF" runat="server" Text='<%# Eval("Total_DIff") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <%-- <asp:TemplateField HeaderText="Edit" Visible="true">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkSelect" runat="server" CommandArgument='<%#Eval("ACNO")%>' CommandName="select" OnClick="lnkSelect_Click" class="glyphicon glyphicon-check"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>--%>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                </asp:TableCell>
                                                <asp:TableCell ID="Tbl_Cell2" runat="server" Style="width: 50%" class="table-scrollable">
                                                            <thead>
                                                                <tr>
                                                                    <th>
                                                                        <asp:GridView ID="Grid_Details" runat="server"
                                                                            OnPageIndexChanging="Grid_Details_PageIndexChanging"  PageSize="25"
                                                                            AutoGenerateColumns="false">
                                                                            <Columns>
                                                                                
                                                                                <asp:TemplateField HeaderText="YEAR" Visible="true" HeaderStyle-BackColor="#99ccff">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="YEAR" runat="server" Text='<%# Eval("Year") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="MONTH" HeaderStyle-BackColor="#99ccff">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="MONTH" runat="server" Text='<%# Eval("Month") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="BRCD" HeaderStyle-BackColor="#99ccff">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="BRCD" runat="server" Text='<%# Eval("BrCD") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="GL NAME" HeaderStyle-BackColor="#99ccff">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="GLNAME" runat="server" Text='<%# Eval("GlName") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="AVSM OP BAL" HeaderStyle-BackColor="#ccccff">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="AVSMOP" runat="server" Text='<%# Eval("AVSM_OP") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="AVSM CR BAL" HeaderStyle-BackColor="#ccccff">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="AVSMCR" runat="server" Text='<%# Eval("AVSM_CR") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="AVSM DR BAL" HeaderStyle-BackColor="#ccccff">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="AVSMDR" runat="server" Text='<%# Eval("AVSM_DR") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="AVSM CL BAL" HeaderStyle-BackColor="#ccccff" >
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="AVSMCL" runat="server" Text='<%# Eval("AVSM_CL") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="AVSB OP BAL" HeaderStyle-BackColor="#ffcccc">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="AVSBOP" runat="server" Text='<%# Eval("AVSB_OP") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="AVSM CR BAL" HeaderStyle-BackColor="#ffcccc">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="AVSBCR" runat="server" Text='<%# Eval("AVSB_CR") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="AVSB DR BAL" HeaderStyle-BackColor="#ffcccc">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="AVSBDR" runat="server" Text='<%# Eval("AVSB_DR") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="AVSB CL BAL" HeaderStyle-BackColor="#ffcccc">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="AVSBCL" runat="server" Text='<%# Eval("AVSB_CL") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Total Diff" HeaderStyle-BackColor="#66ff33">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="TOTALDIFF" runat="server" Text='<%# Eval("Total_DIff") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <%-- <asp:TemplateField HeaderText="Edit" Visible="true">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkSelect" runat="server" CommandArgument='<%#Eval("ACNO")%>' CommandName="select" OnClick="lnkSelect_Click" class="glyphicon glyphicon-check"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>--%>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </div>
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

