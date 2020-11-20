<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmRecoveryGeneration.aspx.cs" Inherits="FrmRecoveryGeneration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
        function isvalidate() {

            var Brcode, Mon, Year, RDiv, RCode, DebitC;
            Brcode = document.getElementById('<%=ddlBrCode.ClientID%>').value;
            Mon = document.getElementById('<%=TxtMM.ClientID%>').value;
            Year = document.getElementById('<%=TxtYYYY.ClientID%>').value;
            RDiv = document.getElementById('<%=DdlRecDiv.ClientID%>').value;
            RCode = document.getElementById('<%=DdlRecDept.ClientID%>').value;
            var message = '';

            if (Brcode == "0") {
                message = 'Enter Branch Code....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=ddlBrCode.ClientID%>').focus();
                return false;
            }
            if (Mon == "") {
                message = 'Enter Month....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtMM.ClientID%>').focus();
                return false;
            }
            if (Year == "") {
                message = 'Enter Year....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtYYYY.ClientID%>').focus();
                return false;
            }
            //if (RDiv == "0")
            //{
            //    message = 'Enter Recovery division....!!\n';
            //   $('#alertModal').find('.modal-body p').text(message);
            //    $('#alertModal').modal('show')
            //    document.getElementById('<%=DdlRecDiv.ClientID%>').focus();
            //    return false;
            //}
            //if (RCode == "0")
            //{
            //    message = 'Enter Recovery department....!!\n';
            //    $('#alertModal').find('.modal-body p').text(message);
            //    $('#alertModal').modal('show')
            //    document.getElementById('<%=DdlRecDept.ClientID%>').focus();
            //    return false;
            //}
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

    <div id="page-wrapper">
        <div class="panel panel-primary">
            <div class="panel-heading" style="color: white">Recovery Opeartion </div>
            <div class="panel-body">
                <div class="tab-content">
                    <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                        <div class="col-lg-12">
                            <asp:RadioButtonList ID="Rdb_CreateType" runat="server" RepeatDirection="Horizontal" Width="400px" OnTextChanged="Rdb_CreateType_TextChanged" AutoPostBack="true">
                                <asp:ListItem Value="S" Text="Specific Create" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="A" Text="All Create"></asp:ListItem>
                                <asp:ListItem Value="C" Text="Single Cust Create"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                        <div class="col-lg-12">
                            <div class="col-md-2">
                                <label class="control-label ">Brcd</label>
                            </div>
                            <div class="col-md-4">
                                <asp:DropDownList ID="ddlBrCode" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlBrCode_SelectedIndexChanged" AutoPostBack="true" Enabled="false">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                        <div class="col-lg-12">
                            <div class="col-md-2">
                                <label class="control-label ">MM/YYYY</label>
                            </div>
                            <div class="col-md-1">
                                <asp:TextBox ID="TxtMM" MaxLength="2" placeholder="MM" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server" OnTextChanged="TxtMM_TextChanged" AutoPostBack="true" />
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="TxtYYYY" MaxLength="4" placeholder="YYYY" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server" OnTextChanged="TxtYYYY_TextChanged" AutoPostBack="true" />
                            </div>
                        </div>
                    </div>
                    <div class="row" runat="server" id="DIV1">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <label class="control-label ">CustNo:</label>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:TextBox ID="txtCustno" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtCustno_TextChanged"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="txtCustName" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                        <div class="col-lg-12">
                            <div class="col-md-2">
                                <label class="control-label ">Rec Div:</label>
                            </div>
                            <div class="col-md-6">
                                <asp:DropDownList ID="DdlRecDiv" CssClass="form-control" runat="server" OnSelectedIndexChanged="DdlRecDiv_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 5px; margin-bottom: 5px;" runat="server" id="Div_Reccode">
                        <div class="col-lg-12">
                            <div class="col-md-2">
                                <label class="control-label ">Rec Dept:</label>
                            </div>
                            <div class="col-md-6">
                                <asp:DropDownList ID="DdlRecDept" CssClass="form-control" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                        <div class="col-lg-12">
                            <div class="col-md-2">
                                <label class="control-label ">Death fund</label>
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="TxtDeathFund" placeholder="Amt" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="TxtDeathFund_TextChanged" />
                            </div>
                            <div class="col-md-1">
                                <label class="control-label">Narration</label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtNarration" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 5px;">
                        <div class="col-lg-12">
                            <div class="col-md-2"></div>
                            <div class="col-md-1">
                                <label class="control-label ">MW</label>
                                <asp:CheckBox ID="chkMW" runat="server" AutoPostBack="true" OnCheckedChanged="chkMW_CheckedChanged" />
                            </div>
                            <div class="col-md-1">
                                <label class="control-label ">US</label>
                                <asp:CheckBox ID="chkUS" runat="server" AutoPostBack="true" OnCheckedChanged="chkUS_CheckedChanged" />
                            </div>
                            <div class="col-md-1">
                                <label class="control-label ">DED_3</label>
                                <asp:CheckBox ID="chkDed3" runat="server" AutoPostBack="true" OnCheckedChanged="chkDed3_CheckedChanged" />
                            </div>
                            <div class="col-md-1">
                                <label class="control-label ">DED_4</label>
                                <asp:CheckBox ID="chkDed4" runat="server" AutoPostBack="true" OnCheckedChanged="chkDed4_CheckedChanged" />
                            </div>
                        </div>
                    </div>
                    <div class="row" id="divtype" runat="server" visible="false" style="margin: 7px 0 7px 0">
                        <div class="col-lg-12">
                            <div class="col-md-2">
                                <label class="control-label ">MW Amount</label>
                            </div>
                            <div class="col-md-1">
                                <asp:TextBox ID="TxtMW" MaxLength="6" placeholder="MM" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server" OnTextChanged="TxtMM_TextChanged" AutoPostBack="true" />
                            </div>
                        </div>
                    </div>
                    <div class="row" id="divtype2" runat="server" visible="false" style="margin: 7px 0 7px 0">
                        <div class="col-lg-12">
                            <div class="col-md-2">
                                <label class="control-label ">US Amount</label>
                            </div>
                            <div class="col-md-1">
                                <asp:TextBox ID="TxtUS" MaxLength="6" placeholder="MM" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server" OnTextChanged="TxtUS_TextChanged" AutoPostBack="true" />
                            </div>
                        </div>
                    </div>
                    <div class="row" id="div2" runat="server" visible="false" style="margin: 7px 0 7px 0">
                        <div class="col-lg-12">
                            <div class="col-md-2">
                                <label class="control-label ">Ded_3 Amount</label>
                            </div>
                            <div class="col-md-1">
                                <asp:TextBox ID="TxDed_3" MaxLength="6" placeholder="MM" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server" OnTextChanged="TxDed_3_TextChanged" AutoPostBack="true" />
                            </div>
                        </div>
                    </div>
                    <div class="row" id="div3" runat="server" visible="false" style="margin: 7px 0 7px 0">
                        <div class="col-lg-12">
                            <div class="col-md-2">
                                <label class="control-label ">Ded_4 Amount</label>
                            </div>
                            <div class="col-md-1">
                                <asp:TextBox ID="TxDed_4" MaxLength="6" placeholder="MM" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server" OnTextChanged="TxDed_4_TextChanged" AutoPostBack="true" />
                            </div>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 5px;">
                        <div class="col-lg-12">
                            <div class="col-md-2"></div>
                            <div class="col-md-4">
                                <label class="control-label ">All Authorize (Div Wise)</label>
                                <asp:CheckBox ID="Chk_AllAutho" runat="server" AutoPostBack="true" OnCheckedChanged="Chk_AllAutho_CheckedChanged" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12 text-center">
                            <asp:Button ID="Btn_CreateAccwise" runat="server" CssClass="btn btn-success" Text="Create Acc wise" OnClick="Btn_CreateAccwise_Click" />
                            <asp:Button ID="BtnCreate" runat="server" CssClass="btn btn-success" Text="Create" OnClick="BtnCreate_Click" OnClientClick="Javascript:return isvalidate();" Visible="false" />
                            <asp:Button ID="BtnModify" runat="server" CssClass="btn btn-success" Text="Modify" OnClick="BtnModify_Click" OnClientClick="Javascript:return isvalidate();" />
                            <asp:Button ID="Btn_Report" runat="server" CssClass="btn btn-success" Text="Report" OnClick="Btn_Report_Click" OnClientClick="Javascript:return isvalidate();" />
                            <asp:Button ID="BtnAuthorize" runat="server" CssClass="btn btn-success" Text="Authorize" OnClick="BtnAuthorize_Click" OnClientClick="Javascript:return isvalidate();" />
                            <asp:Button ID="BtnCancel" Visible="false" runat="server" CssClass="btn btn-success" Text="Cancel Trail" OnClick="BtnCancel_Click" OnClientClick="Javascript:return isvalidate();" />
                            <asp:Button ID="BtnExit" runat="server" CssClass="btn btn-danger" Text="Exit" OnClick="BtnExit_Click" />
                            <asp:Button ID="Btn_ReportNew" runat="server" CssClass="btn btn-success" Text="Office Format" OnClick="Btn_ReportNew_Click" OnClientClick="Javascript:return isvalidate();" />
                        </div>
                    </div>
                </div>

                <!--end of div tag-->

            </div>

        </div>

        <div class="row" id="DivSumGrid" runat="server">
            <div class="col-md-12">
                <div class="table-scrollable" style="width: 100%; overflow-x: auto; overflow-y: auto">
                    <table class="table table-striped table-bordered table-hover">
                        <thead>
                            <tr>
                                <th>
                                    <%--RecDiv	RecCode	Descr	SumAmount--%>

                                    <asp:GridView ID="GrdSumDetails" runat="server" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Division">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblRecDiv" runat="server" Text='<%#Eval("RecDiv") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblRecDept" runat="server" Text='<%#Eval("RecCode") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblName" runat="server" Text='<%#Eval("Descr") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sum Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblSumAmount" runat="server" Text='<%#Eval("SumAmount") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>

                                </th>
                            </tr>
                        </thead>
                    </table>
                </div>
            </div>
        </div>

        <div class="row" id="DivCalculatedData" runat="server">
            <div class="col-md-12">
                <div style="width: 100%; height: 500px; overflow-x: auto; overflow-y: auto">
                    <table class="table table-striped table-bordered table-hover fixhead">
                        <thead>
                            <tr>
                                <th>
                                    <asp:Label ID="Lbl_GridName" Text="" runat="server" Style="font-size: large; color: darkblue; background-color: aqua;"></asp:Label>
                                    <div class="col-md-12">
                                        <asp:Button ID="BtnUpdateAll" CssClass="btn btn-primary" runat="server" Text="Update All" OnClick="BtnUpdateAll_Click" Visible="false" />
                                    </div>

                                    <asp:GridView ID="GrdInsert" runat="server" AutoGenerateColumns="false" OnRowUpdating="GrdInsert_RowUpdating" ShowFooter="true" OnRowDataBound="GrdInsert_RowDataBound">

                                        <Columns>

                                            <asp:TemplateField HeaderText="Confirm Modify">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="Lnk_ConfirmModify" runat="server" class="glyphicon glyphicon-pencil" CommandName="Update" CommandArgument='<%#Eval("Id")%>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Confirm Autho">
                                                <ItemTemplate>
                                                    <%--CommandArgument='<%#Eval("ScrapId")+","+ Eval("UserId")%>'--%>
                                                    <asp:LinkButton ID="Lnk_ConfirmAutho" runat="server" class="glyphicon glyphicon-plus" CommandName="Update" CommandArgument='<%#Eval("Id")+","+Eval("CustNo")+","+Eval("MM")+","+Eval("YYYY")+","+Eval("Total")+","+Eval("Stage")+","+Eval("S9Bal")+","+Eval("S8Bal")+","+Eval("S7Bal")+","+Eval("S6Bal")+","+Eval("S5Bal")+","+Eval("S4Bal")+","+Eval("S3Bal")%>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="Lbl_ID" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Stage">
                                                <ItemTemplate>
                                                    <asp:Label ID="Lbl_Stage" runat="server" Text='<%# Eval("Stage") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sr.No">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtSrNo" Style="width: 80px" CssClass="form-control" runat="server" Text='<%#Container.DataItemIndex+1 %>' Enabled="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="CustNo">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtCustNo" Text='<%# Eval("Custno") %>' CommandArgument='<%#Eval("Custno")%>' Style="width: 80px" CssClass="form-control" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="CustName">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtCustName" Text='<%# Eval("CustName") %>' CssClass="form-control" Style="width: 200px" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <%--  <asp:TemplateField HeaderText="Loan1">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS1" Text='<%# Eval("S1") %>' Enabled="false" Style="width: 80px" CssClass="form-control" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Loan1 Bal">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS1Bal" Text='<%# Eval("S1Bal") %>' Style="width: 80px" CssClass="form-control" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbls1Bal" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Loan1 Inst">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS1Inst" Text='<%# Eval("S1Inst") %>' Style="width: 80px" CssClass="form-control" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbls1Inst" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Loan1 Intr">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS1Intr" Text='<%# Eval("S1Intr") %>' Style="width: 80px" CssClass="form-control" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbls1Intr" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <%--    <asp:TemplateField HeaderText="Loan2">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS2" Text='<%# Eval("S2") %>' Enabled="false" Style="width: 80px" CssClass="form-control" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Loan2Bal">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS2Bal" Text='<%# Eval("S2Bal") %>' Style="width: 80px" CssClass="form-control" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbls2bal" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Loan2Inst">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS2Inst" Text='<%# Eval("S2Inst") %>' Style="width: 80px" CssClass="form-control" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbls2Inst" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Loan2Intr">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS2Intr" Text='<%# Eval("S2Intr") %>' Style="width: 80px" CssClass="form-control" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbls2Intr" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <%--   <asp:TemplateField HeaderText="MON">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS3" Text='<%# Eval("S3") %>' Enabled="false" Style="width: 80px" CssClass="form-control" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="MON Bal">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS3Bal" Text='<%# Eval("S3Bal") %>' Style="width: 80px" CssClass="form-control" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbls3Bal" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:TemplateField HeaderText="MA">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS4" Text='<%# Eval("S4") %>' Enabled="false" Style="width: 80px" CssClass="form-control" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="MA Bal">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS4Bal" Text='<%# Eval("S4Bal") %>' Style="width: 80px" CssClass="form-control" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbls4Bal" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:TemplateField HeaderText="KNT">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS5" Text='<%# Eval("S5") %>' Enabled="false" Style="width: 80px" CssClass="form-control" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="KNT Bal">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS5Bal" Text='<%# Eval("S5Bal") %>' Style="width: 80px" CssClass="form-control" runat="server" Enabled="false" />
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lbls5Bal" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <%--   <asp:TemplateField HeaderText="RD">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS6" Text='<%# Eval("S6") %>' Enabled="false" Style="width: 80px" CssClass="form-control" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="RD Bal">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS6Bal" Text='<%# Eval("S6Bal") %>' Style="width: 80px" CssClass="form-control" runat="server" Enabled="false" />
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lbls6Bal" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:TemplateField HeaderText="DF">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS7" Text='<%# Eval("S7") %>' Enabled="false" Style="width: 80px" CssClass="form-control" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="DF Bal">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS7Bal" Text='<%# Eval("S7Bal") %>' Style="width: 80px" CssClass="form-control" runat="server" Enabled="false" />
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lbls7Bal" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <%--   <asp:TemplateField HeaderText="MW">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS8" Text='<%# Eval("S8") %>' Enabled="false" Style="width: 80px" CssClass="form-control" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="MW Bal">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS8Bal" Text='<%# Eval("S8Bal") %>' Style="width: 80px" CssClass="form-control" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbls8Bal" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:TemplateField HeaderText="US">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS9" Text='<%# Eval("S9") %>' Enabled="false" Style="width: 80px" CssClass="form-control" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="US Bal">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS9Bal" Text='<%# Eval("S9Bal") %>' Style="width: 80px" CssClass="form-control" runat="server" Enabled="false" />
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lbls9Bal" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Surity Amt">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtSurityAmt" Text='<%# Eval("SurityAmt") %>' Style="width: 80px" CssClass="form-control" runat="server" Enabled="false" />
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lblSurityBal" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Month">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblMM" Text='<%#Eval("MM") %>' runat="server" CommandArgument='<%#Eval("MM")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Year">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblYY" Text='<%#Eval("YYYY") %>' runat="server" CommandArgument='<%#Eval("YYYY")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Bal">
                                                <ItemTemplate>
                                                    <asp:Label ID="Lbl_RowTotal" runat="server" Text='<%# Eval("Total") %>' CommandArgument='<%#Eval("Total")%>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <div style="padding: 0 0 5px 0">
                                                        <asp:Label ID="Lbl_SumTotal" runat="server"></asp:Label>
                                                    </div>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="#ccffcc" Font-Bold="True" ForeColor="Black" HorizontalAlign="Right" BorderStyle="None" />
                                    </asp:GridView>

                                    <asp:GridView ID="GridView1009" runat="server" AutoGenerateColumns="false" OnRowUpdating="GridView1009_RowUpdating" ShowFooter="true" OnRowDataBound="GridView1009_RowDataBound">

                                        <Columns>

                                            <asp:TemplateField HeaderText="Confirm Modify">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="Lnk_ConfirmModify" runat="server" class="glyphicon glyphicon-pencil" CommandName="Update" CommandArgument='<%#Eval("Id")%>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Confirm Autho">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="Lnk_ConfirmAutho" runat="server" class="glyphicon glyphicon-plus" CommandName="Update" CommandArgument='<%#Eval("Id")%>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="Lbl_ID" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Stage">
                                                <ItemTemplate>
                                                    <asp:Label ID="Lbl_Stage" runat="server" Text='<%# Eval("Stage") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sr.No">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtSrNo" Style="width: 80px" CssClass="form-control" runat="server" Text='<%#Container.DataItemIndex+1 %>' Enabled="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="CustNo">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtCustNo" Text='<%# Eval("Custno") %>' Style="width: 80px" CssClass="form-control" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="CustName">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtCustName" Text='<%# Eval("CustName") %>' CssClass="form-control" Style="width: 200px" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <%--  <asp:TemplateField HeaderText="Loan1">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS1" Text='<%# Eval("S1") %>' Enabled="false" Style="width: 80px" CssClass="form-control" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Loan1 Bal">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS1Bal" Text='<%# Eval("S1Bal") %>' Style="width: 80px" CssClass="form-control" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbls1Bal" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Loan1 Inst">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS1Inst" Text='<%# Eval("S1Inst") %>' Style="width: 80px" CssClass="form-control" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbls1Inst" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Loan1 Intr">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS1Intr" Text='<%# Eval("S1Intr") %>' Style="width: 80px" CssClass="form-control" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbls1Intr" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <%--    <asp:TemplateField HeaderText="Loan2">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS2" Text='<%# Eval("S2") %>' Enabled="false" Style="width: 80px" CssClass="form-control" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Loan2Bal">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS2Bal" Text='<%# Eval("S2Bal") %>' Style="width: 80px" CssClass="form-control" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbls2bal" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Loan2Inst">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS2Inst" Text='<%# Eval("S2Inst") %>' Style="width: 80px" CssClass="form-control" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbls2Inst" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Loan2Intr">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS2Intr" Text='<%# Eval("S2Intr") %>' Style="width: 80px" CssClass="form-control" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbls2Intr" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Loan3Bal">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS3Bal" Text='<%# Eval("S3Bal") %>' Style="width: 80px" CssClass="form-control" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbls3bal" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Loan3Inst">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS3Inst" Text='<%# Eval("S3Inst") %>' Style="width: 80px" CssClass="form-control" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbls3Inst" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Loan3Intr">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS3Intr" Text='<%# Eval("S3Intr") %>' Style="width: 80px" CssClass="form-control" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbls3Intr" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <%--   <asp:TemplateField HeaderText="MON">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS3" Text='<%# Eval("S3") %>' Enabled="false" Style="width: 80px" CssClass="form-control" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="MON Bal">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS4Bal" Text='<%# Eval("S4Bal") %>' Style="width: 80px" CssClass="form-control" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbls4Bal" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:TemplateField HeaderText="MA">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS4" Text='<%# Eval("S4") %>' Enabled="false" Style="width: 80px" CssClass="form-control" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="MA Bal">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS5Bal" Text='<%# Eval("S5Bal") %>' Style="width: 80px" CssClass="form-control" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbls5Bal" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:TemplateField HeaderText="KNT">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS5" Text='<%# Eval("S5") %>' Enabled="false" Style="width: 80px" CssClass="form-control" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="KNT Bal">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS6Bal" Text='<%# Eval("S6Bal") %>' Style="width: 80px" CssClass="form-control" runat="server" Enabled="false" />
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lbls6Bal" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <%--   <asp:TemplateField HeaderText="RD">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS6" Text='<%# Eval("S6") %>' Enabled="false" Style="width: 80px" CssClass="form-control" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="RD Bal">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS7Bal" Text='<%# Eval("S7Bal") %>' Style="width: 80px" CssClass="form-control" runat="server" Enabled="false" />
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lbls7Bal" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:TemplateField HeaderText="DF">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS7" Text='<%# Eval("S7") %>' Enabled="false" Style="width: 80px" CssClass="form-control" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="DF Bal">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS8Bal" Text='<%# Eval("S8Bal") %>' Style="width: 80px" CssClass="form-control" runat="server" Enabled="false" />
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lbls8Bal" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <%--   <asp:TemplateField HeaderText="MW">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS8" Text='<%# Eval("S8") %>' Enabled="false" Style="width: 80px" CssClass="form-control" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="MW Bal">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS9Bal" Text='<%# Eval("S9Bal") %>' Style="width: 80px" CssClass="form-control" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbls9Bal" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:TemplateField HeaderText="US">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS9" Text='<%# Eval("S9") %>' Enabled="false" Style="width: 80px" CssClass="form-control" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="US Bal">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS10Bal" Text='<%# Eval("S10Bal") %>' Style="width: 80px" CssClass="form-control" runat="server" Enabled="false" />
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lbls10Bal" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Surity Amt">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtSurityAmt" Text='<%# Eval("SurityAmt") %>' Style="width: 80px" CssClass="form-control" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <div style="padding: 0 0 5px 0">
                                                        <asp:Label ID="LblSurityAmt" runat="server"></asp:Label>
                                                    </div>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Bal">
                                                <ItemTemplate>
                                                    <asp:Label ID="Lbl_RowTotal" runat="server" Text='<%# Eval("Total") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <div style="padding: 0 0 5px 0">
                                                        <asp:Label ID="Lbl_SumTotal" runat="server"></asp:Label>
                                                    </div>
                                                </FooterTemplate>
                                            </asp:TemplateField>


                                        </Columns>
                                        <FooterStyle BackColor="#ccffcc" Font-Bold="True" ForeColor="Black" HorizontalAlign="Right" BorderStyle="None" />
                                    </asp:GridView>
                                </th>
                            </tr>
                        </thead>
                    </table>
                </div>
            </div>
        </div>
    </div>

    <asp:HiddenField ID="hdnCustNo" runat="server" Value="0" />

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

