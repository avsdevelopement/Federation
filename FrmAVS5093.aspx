<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAVS5093.aspx.cs" Inherits="FrmAVS5093" %>

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
        function Validate() {
            debugger;
            var Custno, S_Custno, Accno, PriAmt, Status, Subgl, Accno;

            Custno = document.getElementById('<%=TxtCustno.ClientID%>').value;
            S_Custno = document.getElementById('<%=TxtS_Custno.ClientID%>').value;
            PriAmt = document.getElementById('<%=TxtPriAmt.ClientID%>').value;
            Status = document.getElementById('<%=Ddl_Status.ClientID%>').value;
            Subgl = document.getElementById('<%=TxtLoaneeSubgl.ClientID%>').value;
            Accno = document.getElementById('<%=TxtLoaneeAccno.ClientID%>').value;


            if (Custno == "") {
                alert("Enter Custno..!!");
                return false;
            }
            if (S_Custno == "") {
                alert("Surety Custno cannot be empty..!!");
                return false;
            }
            if (PriAmt == "") {
                alert("Principle Amount cannot be empty..!!");
                return false;
            }
            if (Status == "0") {
                alert("Status cannot be empty..!!");
                return false;
            }
            if (Subgl == "") {
                alert("Loanee Subglcode cannot be empty,Select Account from Loan Accounts Grid..!!");
                return false;
            }
            if (Accno == "") {
                alert("Loanee Account number cannot be empty,Select Account from Loan Accounts Grid..!!");
                return false;
            }

        }
    </script>
    <script type="text/javascript">
        function Validate1() {
            debugger;
            var Custno, Subgl, Accno;

            Custno = document.getElementById('<%=TxtCustno.ClientID%>').value;
            Subgl = document.getElementById('<%=TxtLoaneeSubgl.ClientID%>').value;
            Accno = document.getElementById('<%=TxtLoaneeAccno.ClientID%>').value;

            if (Custno == "") {
                alert("Enter Custno..!!");
                return false;
            }
            if (Subgl == "") {
                alert("Loanee Subglcode cannot be empty,Select Account from Loan Accounts Grid..!!");
                return false;
            }
            if (Accno == "") {
                alert("Loanee Account number cannot be empty,Select Account from Loan Accounts Grid..!!");
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
                        Surety Deduction
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">

                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-1">Custno <span class="required">*</span></label>
                                            <div class="col-md-1">
                                                <asp:TextBox ID="TxtCustno" OnTextChanged="TxtCustno_TextChanged" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="TxtCustname" OnTextChanged="TxtCustname_TextChanged" CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                                                <div id="RecNames" style="height: 200px; overflow-y: scroll;"></div>
                                                <asp:AutoCompleteExtender ID="AutoCustName" runat="server" TargetControlID="TxtCustname"
                                                    UseContextKey="true"
                                                    CompletionInterval="1"
                                                    CompletionSetCount="20"
                                                    MinimumPrefixLength="1"
                                                    EnableCaching="true"
                                                    ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="RecNames"
                                                    ServiceMethod="GetCustNames">
                                                </asp:AutoCompleteExtender>
                                            </div>
                                            <label class="control-label col-md-1">Rec Divi</label>
                                            <div class="col-md-1">
                                                <asp:TextBox ID="TxtRecDiv" Enabled="false" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" />
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="TxtRecDivName" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-1">Loanee Subgl </label>
                                            <div class="col-md-1">
                                                <asp:TextBox ID="TxtLoaneeSubgl" Enabled="false" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" />
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="TxtLoaneeSubglname" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>

                                            <label class="control-label col-md-1">Rec Dept </label>
                                            <div class="col-md-1">
                                                <asp:TextBox ID="TxtRecDept" Enabled="false" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" />
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="TxtRecDeptName" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">

                                            <label class="control-label col-md-1">Loanee Accno </label>
                                            <div class="col-md-1">
                                                <asp:TextBox ID="TxtLoaneeAccno" Enabled="false" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" />
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="TxtLoaneeAccName" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-1">
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Button ID="BtnSubmit1" OnClick="BtnSubmit1_Click" runat="server" CssClass="btn blue" Text="Surety Details" OnClientClick="Javascript:return Validate1();" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #fa4e0d">Surety Details</strong></div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-1">Surety Custno </label>
                                            <div class="col-md-1">
                                                <asp:TextBox ID="TxtS_Custno" Enabled="false" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" />
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="TxtS_Custname" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <label class="control-label col-md-1">Member No </label>
                                            <div class="col-md-1">
                                                <asp:TextBox ID="TxtS_MemNo" Enabled="false" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" />
                                            </div>

                                        </div>
                                    </div>
                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                        </div>
                                    </div>
                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-1">Operation Type </label>
                                            <div class="col-md-3">
                                                <asp:RadioButtonList ID="Rdb_Type" runat="server" RepeatDirection="Horizontal" Width="200px">
                                                    <asp:ListItem Text="Insert" Value="I" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Modify" Value="M"></asp:ListItem>
                                                    <asp:ListItem Text="Cancel" Value="C"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-1">PriAmt<span class="required">* </span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="TxtPriAmt" onkeyup="FormatIt(this)" CssClass="form-control" runat="server" />
                                            </div>
                                            <label class="control-label col-md-1">Int Amt<span class="required">* </span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="TxtIntAmt" onkeyup="FormatIt(this)" CssClass="form-control" runat="server" />
                                            </div>
                                            <label class="control-label col-md-1">Status<span class="required">* </span></label>
                                            <div class="col-md-2">
                                                <asp:DropDownList ID="Ddl_Status" runat="server" AutoPostBack="true" CssClass="form-control">
                                                    <asp:ListItem Text="-Select-" Value="0" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Start" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Stop" Value="2"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <div class="col-md-1">
                                                <asp:Button ID="btnSubmit" OnClick="btnSubmit_Click" runat="server" CssClass="btn blue" Text="Submit" OnClientClick="Javascript:return Validate();" />
                                            </div>
                                            <div class="col-md-1">
                                                <asp:Button ID="btnClear" runat="server" CssClass="btn blue" Text="Clear All" OnClick="btnClear_Click" />
                                            </div>
                                        </div>
                                    </div>

                                    <%--Brcd	Custno	CustName	Subglcode	GlName	Accno--%>
                                    <div id="DivLoanAccount" runat="server" class="col-md-12">
                                        <div class="table-scrollable" style="width: 100%; height: auto; max-height: 150px; overflow-x: auto; overflow-y: auto">
                                            <table class="table table-striped table-bordered table-hover" width="100%">
                                                <asp:Label ID="LblLn_Header" runat="server" Text="Loan Accounts" Style="font-size: medium;"></asp:Label>
                                                <thead>
                                                    <tr>
                                                        <th>
                                                            <asp:GridView ID="GridLoanAccno" runat="server" AlternatingRowStyle-CssClass="alt" CssClass="mGrid"
                                                                AutoGenerateColumns="false" EditRowStyle-BackColor="#FFFF99" Width="100%" OnRowUpdating="GridLoanAccno_RowUpdating">
                                                                <Columns>

                                                                    <asp:TemplateField HeaderText="Select" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="LnkSelectLn" runat="server" CommandName="Update" class="glyphicon glyphicon-plus"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Brcd" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Lblln_Brcd" runat="server" Text='<%# Eval("Brcd") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Custno" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Lblln_Custno" runat="server" Text='<%# Eval("Custno") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="CustName" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="LblLn_CustName" runat="server" Text='<%# Eval("CustName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Subglcode">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="LblLn_Subglcode" runat="server" Text='<%# Eval("Subglcode") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="GlName">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="LblLn_GlName" runat="server" Text='<%# Eval("GlName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Accno">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="LblLn_Accno" runat="server" Text='<%# Eval("Accno") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
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


                                    <%--Custno	Custname	Subglcode	Glname	Accno	Surety_Custno	Surety_Custname	Surety_Date	Brcd--%>
                                    <div id="DivSureties" runat="server" class="col-md-12">
                                        <div class="table-scrollable" style="width: 100%; height: auto; max-height: 150px; overflow-x: auto; overflow-y: auto">
                                            <table class="table table-striped table-bordered table-hover" width="100%">
                                                <asp:Label ID="Label1" runat="server" Text="Loan Surieties" Style="font-size: medium;"></asp:Label>
                                                <thead>
                                                    <tr>
                                                        <th>
                                                            <asp:GridView ID="GridSurities" runat="server" AlternatingRowStyle-CssClass="alt" CssClass="mGrid"
                                                                AutoGenerateColumns="false" EditRowStyle-BackColor="#FFFF99" Width="100%" OnRowUpdating="GridSurities_RowUpdating">
                                                                <Columns>

                                                                    <asp:TemplateField HeaderText="Select" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="LnkSelectLns" runat="server" CommandName="Update" class="glyphicon glyphicon-plus"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Custno" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Lbllns_Custno" runat="server" Text='<%# Eval("Custno") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Custname" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Lbllns_Custname" runat="server" Text='<%# Eval("Custname") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Subglcode" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="LblLns_Subglcode" runat="server" Text='<%# Eval("Subglcode") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Glname">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="LblLns_Glname" runat="server" Text='<%# Eval("Glname") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Accno">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="LblLns_Accno" runat="server" Text='<%# Eval("Accno") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Surety_Custno">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="LblLns_Surety_Custno" runat="server" Text='<%# Eval("Surety_Custno") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Surety_Custname" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Lbllns_Surety_Custname" runat="server" Text='<%# Eval("Surety_Custname") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Surety_MemNo">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="LblLns_Surety_MemNo" runat="server" Text='<%# Eval("Surety_MemNo") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Surety_Date" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Lbllns_Surety_Date" runat="server" Text='<%# Eval("Surety_Date") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Brcd" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Lbllns_Brcd" runat="server" Text='<%# Eval("Brcd") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
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


                                    <%--Id	Custno	Custname	Subglcode	Glname	Accno PriAmt Intramt	Surety_Custno	Surety_Custname	Start_Date	End_Date	Brcd	Remark--%>
                                    <div id="DivSuretyDeduct" runat="server" class="col-md-12">
                                        <div class="table-scrollable" style="width: 100%; height: auto; max-height: 150px; overflow-x: auto; overflow-y: auto">
                                            <table class="table table-striped table-bordered table-hover" width="100%">
                                                <asp:Label ID="Lbl_SuretyDeduct" runat="server" Text="Loan Surety Deduct" Style="font-size: medium;"></asp:Label>
                                                <thead>
                                                    <tr>
                                                        <th>
                                                            <asp:GridView ID="GridSuretyDeduct" runat="server" AlternatingRowStyle-CssClass="alt" CssClass="mGrid"
                                                                AutoGenerateColumns="false" EditRowStyle-BackColor="#FFFF99" Width="100%" OnRowUpdating="GridSuretyDeduct_RowUpdating">
                                                                <Columns>

                                                                    <asp:TemplateField HeaderText="Select" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="LnkSelectLnsd" runat="server" CommandName="Update" class="glyphicon glyphicon-plus"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Id" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Lbllnsd_Id" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Custno" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Lbllnsd_Custno" runat="server" Text='<%# Eval("Custno") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Subglcode" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="LblLnsd_Subglcode" runat="server" Text='<%# Eval("Subglcode") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Glname">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="LblLnsd_Glname" runat="server" Text='<%# Eval("Glname") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Accno">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="LblLnsd_Accno" runat="server" Text='<%# Eval("Accno") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="PriAmt">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="LblLnsd_PriAmt" runat="server" Text='<%# Eval("PriAmt") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Int Amt">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="LblLnsd_Intramt" runat="server" Text='<%# Eval("Intramt") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Surety_Custno">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="LblLnsd_Surety_Custno" runat="server" Text='<%# Eval("Surety_Custno") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Surety_Custname" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Lbllnsd_Surety_Custname" runat="server" Text='<%# Eval("Surety_Custname") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Surety_S_Memno" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Lbllnsd_Surety_S_Memno" runat="server" Text='<%# Eval("S_Memno") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Start Date" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Lbllnsd_Start_Date" runat="server" Text='<%# Eval("Start_Date") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="End Date" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Lbllnsd_End_Date" runat="server" Text='<%# Eval("End_Date") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Brcd" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Lbllnsd_Brcd" runat="server" Text='<%# Eval("Brcd") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Remark" Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Lbllnsd_Remark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
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





                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-md-12">
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
                </div>
            </div>
        </div>
    </div>
</asp:Content>

