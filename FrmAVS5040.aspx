<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAVS5040.aspx.cs" Inherits="FrmAVS5040" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function IsValid() {
            debugger;
            var txtbrname = document.getElementById('<%=txtbrname.ClientID%>').value;
            var txtappdate = document.getElementById('<%=txtappdate.ClientID%>').value;
            var TXtCustNo = document.getElementById('<%=TXtCustNo.ClientID%>').value;
            var txtResignvouchar = document.getElementById('<%=txtResignvouchar.ClientID%>').value;


            var message = '';

            if (txtbrname == "") {
                message = 'Please Enter Br NAme ...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtbrname.ClientID%>').focus();
                return false;
            }

            if (txtappdate == "") {
                message = 'Please Enter Application Date...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtappdate.ClientID%>').focus();
                return false;
            }
            if (TXtCustNo == "") {
                message = 'Please Enter Cust No...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TXtCustNo.ClientID%>').focus();
                return false;
            }
            if (txtResignvouchar == "") {
                message = 'Please Enter Resignation Vouchar...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtResignvouchar.ClientID%>').focus();
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
         var popup;
         function getCurrentIndex(row) {

          //   alert("hello");
             var rowData = row.parentNode.parentNode;
             var rowIndex = rowData.rowIndex;

            //  alert(rowIndex);
             document.getElementById("<%=hdnRowIndexForPayable.ClientID %>").value = rowIndex;

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row ownformwrap">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        Retirment Vouchar
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="colmn-wrap colmn-wrap-full">
                                    <div class="clmn clmn-1">
                                        <div class="row" style="margin-top: 5px;">
                                            <div class="col-lg-12">
                                                <h3 class="heading">Application Details  :</h3>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 5px;">
                                            <div class="col-lg-12 no-padding margin-top-bot-5">
                                                <div class="col-md-5">
                                                    <label class="control-label">Branch Name : <span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-7">
                                                    <asp:TextBox ID="txtbrname" CssClass="form-control" Placeholder="Product Code" onkeypress="javascript:return isNumber (event)" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 no-padding margin-top-bot-5">
                                                <div class="col-md-5">
                                                    <label class="control-label">Application Date : <span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-7">
                                                    <div class="input-icon">
                                                        <asp:TextBox ID="txtappdate" CssClass="form-control" onkeyup="FormatIt(this)" PlaceHolder="DD/MM/YYYY" onkeypress="javascript:return isNumber (event)" runat="server"></asp:TextBox>
                                                        <asp:CalendarExtender ID="txtappdate_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtappdate">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clmn clmn-2">
                                        <div class="row" style="margin-top: 5px;">
                                            <div class="col-lg-12">
                                                <h3 class="heading">Personal Details  : </h3>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 5px;">
                                            <div class="col-lg-12 no-padding margin-top-bot-5">
                                                <div class="col-md-3" style="padding-right: 0">
                                                    <label class="control-label">Cust No : <span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-3" style="padding-right: 0">
                                                    <asp:TextBox ID="TXtCustNo" CssClass="form-control" onkeypress="javascript:return isNumber (event)" PlaceHolder="Cust Number" runat="server" AutoPostBack="true" OnTextChanged="TXtCustNo_TextChanged"></asp:TextBox>

                                                </div>

                                                <%-- <div class="col-md-2">
                                            <asp:TextBox ID="TXtCustNo" CssClass="form-control" Placeholder="Cust Number"  runat="server" AutoPostBack="true" OnTextChanged="TXtCustNo_TextChanged"></asp:TextBox>
                                        </div>--%>
                                                <div class="col-md-6">
                                                    <div class="input-icon">
                                                        <i class="fa fa-search"></i>
                                                        <asp:TextBox ID="txtcustName" CssClass="form-control" Placeholder="Customer Name" runat="server"></asp:TextBox>
                                                        <div id="CustList" style="height: 200px; overflow-y: scroll;"></div>
                                                        <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="txtcustName"
                                                            UseContextKey="true"
                                                            CompletionInterval="1"
                                                            CompletionSetCount="20"
                                                            MinimumPrefixLength="1"
                                                            EnableCaching="true"
                                                            ServicePath="~/WebServices/Contact.asmx"
                                                            ServiceMethod="GetCustNames"
                                                            CompletionListElementID="CustList">
                                                        </asp:AutoCompleteExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 no-padding margin-top-bot-5">
                                                <div class="col-md-6">
                                                    <label class="control-label">Resign No : <span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtResignvouchar" CssClass="form-control" PlaceHolder="Reg vouchar" runat="server" TabIndex="1"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="clmn clmn-3">
                                        <div class="row" style="margin-top: 5px;">
                                            <div class="col-lg-12">
                                                <h3 class="heading">Resignation Reason  : </h3>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 5px;">
                                            <div class="col-lg-12 no-padding margin-top-bot-5">
                                                <div class="col-md-6">
                                                    <label class="control-label">Customer Since Year : <span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="input-icon">
                                                        <asp:TextBox ID="txtdatediff" CssClass="form-control" PlaceHolder=" DAte Since Customer" ReadOnly="true" onkeypress="javascript:return isNumber (event)" runat="server"></asp:TextBox>

                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 no-padding margin-top-bot-5">
                                                <div class="col-md-6">
                                                    <label class="control-label">Resign Reason : <span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtResignReason" CssClass="form-control" Placeholder="Resign Reason" runat="server" TabIndex="2"></asp:TextBox>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <div style="border-bottom: 5px solid rgba(128, 128, 128, 0.31); margin-top: 5px;"><strong style="color: black">Total Amount</strong></div>
                                <asp:Table ID="TblDiv_MainWindow" runat="server" CssClass="noborder fullwidth">
                                    <asp:TableRow ID="Tbl_R1" runat="server">
                                        <asp:TableCell ID="Tbl_c1" runat="server" Width="30%" Style="vertical-align: top;">
                                            <div class="row" style="margin-top: 5px;">
                                                <div class="col-lg-12">
                                                    <h3 class="heading">Payble Amount  :</h3>
                                                </div>
                                            </div>
                                            <div class="row" style="margin-top: 5px;">
                                                <div class="col-lg-9">
                                                    <div id="divStandard" runat="server" align="left" visible="true">
                                                        <asp:GridView ID="grdstandard" runat="server" AutoGenerateColumns="false" CssClass="noborder fullwidth" OnRowDataBound="grdstandard_RowDataBound">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText=" " ItemStyle-Width="20px">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="Agentcollect" runat="server" Text="" Style="width: 100px;" Checked="true" OnCheckedChanged="Agentcollect_CheckedChanged" AutoPostBack="true" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Sr.No" ItemStyle-Width="20px">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="TxtSrno" Enable="false" CssClass="form-control" runat="server" Text='<%#Eval("SRNO") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="SubglCode" ItemStyle-Width="50px">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtsubglcode" onkeypress="javascript:return isNumber(event)" OnTextChanged="txtsubglcode_TextChanged" AutoPostBack="true" CssClass="form-control" MaxLength="4" Width="60px" runat="server" Text='<%#Eval("SUBGLCODE") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="GLNAME" ItemStyle-Width="70px">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtGLNAME" Width="200px" onkeypress="javascript:return isNumber(event)" AutoPostBack="true" CssClass="form-control" runat="server" Text='<%#Eval("GLNAME") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Acc No" ItemStyle-Width="70px">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="TxtSname" CssClass="form-control" Width="90px" runat="server" Text='<%#Eval("ACCNO") %>' />

                                                                    </ItemTemplate>

                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Amount" ItemStyle-Width="70px">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="TxtSDeduction" onkeypress="javascript:return isNumber(event)" Width="90px" OnTextChanged="TxtSDeduction_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server" Text='<%#Eval("AMOUNT") %>' ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" />
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <div style="padding: 0 0 5px 0">
                                                                            <asp:Label ID="Lbl_TotalLimit" runat="server" />
                                                                            <%--<asp:TextBox ID="TxtSubS" Width="120px" runat="server" ReadOnly="true" />--%>
                                                                        </div>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Int" ItemStyle-Width="20px">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtIntAmt" CssClass="form-control" OnTextChanged="txtIntAmt_TextChanged" Text='<%#Eval("INTAMT") %>' Width="90px" runat="server" AutoPostBack="true" />

                                                                    </ItemTemplate>

                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Calculate" ItemStyle-Width="20px">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton CssClass="btn btn-primary " runat="server" Text="Int" CommandName="Select"  ID="btnPayableIntCalculate" OnClientClick="getCurrentIndex(this)" OnClick="btnPayableIntCalculate_Click"  CommandArgument='<%#Eval("SUBGLCODE")+"," +Eval("ACCNO")%>' ></asp:LinkButton>

                                                                    </ItemTemplate>

                                                                </asp:TemplateField>

                                                            </Columns>
                                                        </asp:GridView>
                                                        <div class="pad_top10 text-right">
                                                            <asp:Button ID="Btnstandard" CssClass="btn btn-primary" runat="server" Text="Add New Row" OnClick="Btnstandard_Click" />
                                                            <div class="col-md-1"></div>
                                                            <asp:Label ID="LblSubS" Text="Sub Total" runat="server" />
                                                            <asp:TextBox ID="TxtSubS" Width="100px" runat="server" ClientIDMode="Static" BackColor="#99ff66" ReadOnly="false" />
                                                            <asp:HiddenField ID="hdnRowIndexForPayable" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                        </asp:TableCell><asp:TableCell ID="TableCell1" runat="server" CssClass="form-control" Width="45%" Style="vertical-align: top;">
                                            <div class="row" style="margin-top: 5px;">
                                                <div class="col-lg-12">
                                                    <h3 class="heading">Receivable Amount  : </h3>
                                                </div>
                                            </div>
                                            <div class="row" style="margin-top: 5px;">
                                                <div class="col-lg-12">
                                                    <div id="div1" runat="server" align="right" visible="true">
                                                        <asp:GridView ID="grdRecivable" runat="server" AutoGenerateColumns="false" CssClass="noborder fullwidth" OnRowDataBound="grdRecivable_RowDataBound">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText=" " ItemStyle-Width="20px">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="Agent" runat="server" Text="" OnCheckedChanged="Agent_CheckedChanged" Checked="true" AutoPostBack="true" Style="width: 100px;" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Sr.No" ItemStyle-Width="20px">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="TxtSrno" Enable="false" CssClass="form-control" runat="server" Text='<%#Eval("SRNO") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="SubglCode" ItemStyle-Width="60px">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtsubglcode" onkeypress="javascript:return isNumber(event)" OnTextChanged="txtsubglcode_TextChanged1" AutoPostBack="true" CssClass="form-control" MaxLength="4" Width="60px" runat="server" Text='<%#Eval("SUBGLCODE") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="GLNAME" ItemStyle-Width="70px">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtGLNAME" onkeypress="javascript:return isNumber(event)" Width="200px" AutoPostBack="true" CssClass="form-control" runat="server" Text='<%#Eval("GLNAME") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Acc No" ItemStyle-Width="70px">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="TxtSname" CssClass="form-control" Width="90px" runat="server" Text='<%#Eval("ACCNO") %>' />

                                                                    </ItemTemplate>

                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Amount" ItemStyle-Width="70px">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="TxtSDeduction" onkeypress="javascript:return isNumber(event)" CssClass="form-control" Width="90px" runat="server" AutoPostBack="true" Text='<%#Eval("AMOUNT") %>' OnTextChanged="txtLoanInt_TextChanged" />
                                                                    </ItemTemplate>

                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Int" ItemStyle-Width="30px">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtLoanInt" CssClass="form-control" Width="80px" runat="server" AutoPostBack="true" Text='<%#Eval("INTAMT") %>' OnTextChanged="txtLoanInt_TextChanged" />

                                                                    </ItemTemplate>

                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Calculate" ItemStyle-Width="20px">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnRecIntCalculate" CssClass="btn btn-primary " CommandName="select" runat="server" Text="Int"  OnClick="btnRecIntCalculate_Click" CommandArgument='<%#Eval("SUBGLCODE")+"," +Eval("ACCNO")%>'></asp:LinkButton>

                                                                    </ItemTemplate>

                                                                </asp:TemplateField>

                                                            </Columns>
                                                        </asp:GridView>
                                                        <div class="pad_top10 text-right">
                                                            <asp:Button ID="BtnAddNew" CssClass="btn btn-primary" runat="server" Text="Add New Row" OnClick="BtnAddNew_Click" />
                                                            <asp:Label ID="Label1" Text="Sub Total" runat="server" />
                                                            <asp:TextBox ID="txtRsub" Width="90px" runat="server" BackColor="#99ff66" ReadOnly="true" onchange="CalculateFinalAmount()" />
                                                        </div>
                                                    </div>
                                                </div>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </div>
                            <div class="colmn-wrap colmn-wrap-full">
                                <div class="clmn clmn-1">
                                </div>
                                <div class="clmn clmn-2">
                                </div>
                            </div>
                            <div class="row" style="margin-top: 5px;">
                                <div class="col-lg-12">
                                    <h3 class="heading">Member Welfare Fund : </h3>
                                </div>
                            </div>
                            <div class="row" style="margin-top: 5px;">
                                <div class="col-lg-12">
                                    <div class="col-md-1">
                                        <label class="control-label">Glcode <span class="required"></span></label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtMglcode" CssClass="form-control" Placeholder="Product Code" onkeypress="javascript:return isNumber (event)" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1">
                                        <label class="control-label">SubGlcode <span class="required"></span></label>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="input-icon">
                                            <asp:TextBox ID="txtMsub" CssClass="form-control" PlaceHolder="Subglcode" onkeypress="javascript:return isNumber (event)" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-1">
                                        <label class="control-label">Amount <span class="required"></span></label>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="input-icon">
                                            <asp:TextBox ID="txtMAount" CssClass="form-control" PlaceHolder="Amount" onkeypress="javascript:return isNumber (event)" ClientIDMode="Static" onchange="CalculateFinalAmount()" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="margin-top: 5px;">
                                <div class="col-lg-12">
                                    <h3 class="heading">Payment Type  : </h3>
                                </div>
                            </div>
                            <div class="row" style="margin: 7px 0 7px 0;">
                                <div class="col-lg-12">
                                    <label class="control-label col-md-2">Payment Mode : <span class="required">* </span></label>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddlPayMode" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlPayMode_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0" Text="--Select--">
                                            </asp:ListItem>
                                            <asp:ListItem Value="1" Text="Cash">
                                            </asp:ListItem>
                                            <asp:ListItem Value="2" Text="Transfer">
                                            </asp:ListItem>
                                            <asp:ListItem Value="3" Text="Cheque">
                                            </asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2"></div>

                                    <asp:Label ID="lblpayable" runat="server" Text="Net Paid Amount" class="control-label col-md-2"></asp:Label><div class="col-md-2">
                                        <asp:TextBox ID="TxtPayAmt" runat="server" onkeyup="Sum()" PlaceHolder="payeble Amount" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div id="DivTransfer" runat="server" visible="false">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #21fa09">Transfer Information : </strong></div>
                                    </div>
                                </div>
                                <div class="row" style="margin: 7px 0 7px 0;">
                                    <div class="col-lg-12">
                                        <label class="control-label col-md-2">Product Type : <span class="required">* </span></label>
                                        <div class="col-md-1">
                                            <asp:TextBox ID="TxtPType" runat="server" onkeypress="javascript:return isNumber (event)" PlaceHolder="Product Code" OnTextChanged="TxtPType_TextChanged" CssClass="form-control" AutoPostBack="true">
                                            </asp:TextBox>&nbsp;&nbsp;
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="TxtPTName" runat="server" PlaceHolder="Product Name" CssClass="form-control" OnTextChanged="TxtPTName_TextChanged" AutoPostBack="true"></asp:TextBox><div id="CustList3" style="height: 200px; overflow-y: scroll;"></div>
                                            <asp:AutoCompleteExtender ID="Autoprd4" runat="server" TargetControlID="TxtPTName"
                                                UseContextKey="true"
                                                CompletionInterval="1"
                                                CompletionSetCount="20"
                                                MinimumPrefixLength="1"
                                                EnableCaching="true"
                                                ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList3"
                                                ServiceMethod="GetGlName">
                                            </asp:AutoCompleteExtender>
                                        </div>
                                        <label class="control-label col-md-2">Account No : <span class="required">* </span></label>
                                        <div class="col-md-1">
                                            <asp:TextBox ID="TxtTAccNo" runat="server" PlaceHolder="Account No" onkeypress="javascript:return isNumber (event)" OnTextChanged="TxtTAccNo_TextChanged" CssClass="form-control" AutoPostBack="true"></asp:TextBox>&nbsp;&nbsp;
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="TxtTAName" runat="server" PlaceHolder="Account Holder Name" AutoPostBack="true" OnTextChanged="TxtTAName_TextChanged" CssClass="form-control"></asp:TextBox><div id="CustList4" style="height: 200px; overflow-y: scroll;"></div>
                                            <asp:AutoCompleteExtender ID="Autoaccname4" runat="server" TargetControlID="TxtTAName"
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
                                    <div id="Transfer2" visible="false" runat="server">
                                        <div id="Transfer3" class="row" style="margin: 07px 0 7px 0;" runat="server">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Instrument No :<span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtChequeNo" placeholder="CHEQUE NUMBER" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" MaxLength="6">
                                                    </asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-2">Instrument Date :<span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtChequeDate" placeholder="CHEQUE DATE" CssClass="form-control" onkeyup="FormatIt(this)" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="form-actions">
                            <div class="row">
                                <div class="col-md-offset-3 col-md-9">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn blue" OnClick="btnSubmit_Click" OnClientClick="Javascript:return IsValid();" />
                                    <asp:Button ID="BtnCloseAcc" runat="server" Text="Close All Acc" CssClass="btn blue" OnClick="BtnCloseAcc_Click" />
                                    <asp:Button ID="BtnAddNew_1" runat="server" Text="Advice Print" CssClass="btn blue" OnClick="BtnAddNew_1_Click" />
                                    <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn blue" OnClick="btnClear_Click" />
                                    <asp:Button ID="btnexit" runat="server" Text="Exit" CssClass="btn blue" OnClick="btnexit_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-12">
                </div>
                <div class="col-md-12">
                    <div class="table-scrollable">
                    </div>
                </div>
            </div>
            <div style="border: 1px solid #3598dc">
                <div class="portlet-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <h3 class="heading">InDirect Liabilities-To Surity : </h3>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                        <div class="col-lg-12">
                            <asp:GridView ID="GrdInDirectLiab" runat="server" AllowPaging="True" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                EditRowStyle-BackColor="#FFFF99" OnPageIndexChanging="GrdInDirectLiab_PageIndexChanging" OnRowDataBound="GrdInDirectLiab_RowDataBound"
                                PagerStyle-CssClass="pgr" Width="100%" ShowFooter="true" CssClass="noborder">
                                <Columns>

                                    <asp:TemplateField HeaderText="SrNo" Visible="true" FooterText="Sub Total" FooterStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSrNo1" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Brcd" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBrcd" runat="server" Text='<%# Eval("BRCD") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Prod Code" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPrdcd" runat="server" Text='<%# Eval("subglcode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Accno" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAccno" runat="server" Text='<%# Eval("accno") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Custno" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCUSTNO" runat="server" Text='<%# Eval("CUSTNO") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Account Holder Name" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCUSTNAME" runat="server" Text='<%# Eval("CUSTNAME") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Opening Dt" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOPdt" runat="server" Text='<%# Eval("OPENINGDATE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Due Dt" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblduedt" runat="server" Text='<%# Eval("DUEDATE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sanction Amt" Visible="true" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsancamtIn" runat="server" Text='<%# Eval("LIMIT") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <div style="padding: 0 0 5px 0">
                                                <asp:Label ID="Lbl_TotalLimitIn" runat="server" />
                                            </div>
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Outstanding bal" Visible="true" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lbloutbalIn" runat="server" Text='<%# Eval("BALANCE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <div style="padding: 0 0 5px 0">
                                                <asp:Label ID="Lbl_TotalBalIn" runat="server" />
                                            </div>
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="OD Amt" Visible="true" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblodamtIn" runat="server" Text='<%# Eval("ODAMT") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <div style="padding: 0 0 5px 0">
                                                <asp:Label ID="Lbl_TotalODIn" runat="server" />
                                            </div>
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="No of Inst" Visible="true" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblnoofinst" runat="server" Text='<%# Eval("TOTALINST") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle CssClass="pgr" />
                                <HeaderStyle BackColor="#ffce9d" />
                                <FooterStyle BackColor="#bbffff" />
                                <SelectedRowStyle BackColor="#66FF99" />
                                <EditRowStyle BackColor="#FFFF99" />
                                <AlternatingRowStyle CssClass="alt" />
                            </asp:GridView>
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
                    <button type="button" class="close" data-dismiss="modal">&times;</button><center><h4 class="modal-title" style="color:#ff0000">AVS CORE</h4></center>
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
    <asp:HiddenField ID="hdnAcc" runat="server" />
    <asp:HiddenField ID="hdnreceipt" runat="server" />
    <script>

        function CalculateFinalAmount() {
            var totalPayableAmt = 0, totalRecAmt = 0, netPaidAmt = 0;
            //alert("hello");


            totalPayableAmt = Number(document.getElementById('<%=TxtSubS.ClientID%>').value);
            // alert(totalPayableAmt);
            totalRecAmt = Number(document.getElementById('<%= txtRsub.ClientID%>').value);
            // alert(totalRecAmt);

            netPaidAmt = Number(document.getElementById('<%=txtMAount.ClientID%>').value);
            // alert(netPaidAmt);


            var finalAmount = (totalPayableAmt - totalRecAmt) + netPaidAmt;

            // alert(finalAmount);

            document.getElementById('<%=TxtPayAmt.ClientID%>').value = Number(finalAmount);

            // alert("hello");
        }
    </script>

    <script type="text/javascript">

        $("#TxtSubS").on("input", function (e) {
            var input = $(this);
            var val = input.val();
            alert("hello");

            if (input.data("lastval") != val) {
                input.data("lastval", val);

                //your change action goes here 
                console.log(val);
            }

        });
        function tSpeedValue() {


            alert("hi");
            var at = txt.value;


            alert(at);


        }
    </script>
</asp:Content>
