<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAVS5027.aspx.cs" Inherits="FrmAVS5027" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

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

        function IsValide() {
            debugger;
            var LoanProd = document.getElementById('<%=TxtLtype.ClientID%>').value;
            var LoanAmount = document.getElementById('<%=TxtLapply.ClientID%>').value;
            var TotPeriod = document.getElementById('<%=txtTotPeriod.ClientID%>').value;
            var IntRate = document.getElementById('<%=txtIntRate.ClientID%>').value;
            var InstallAmt = document.getElementById('<%=Txtinstll.ClientID%>').value;

            if ((LoanProd == "") || (LoanProd == "0")) {
                window.alert("Enter loan product first ...!!");
                document.getElementById('<%=TxtLtype.ClientID%>').focus();
                return false;
            }
            if ((LoanAmount == "") || (LoanAmount == "0")) {
                window.alert("Enter loan apply amount first ...!!");
                document.getElementById('<%=TxtLapply.ClientID%>').focus();
                return false;
            }
            if ((TotPeriod == "") || (TotPeriod == "0")) {
                window.alert("Enter total period first ...!!");
                document.getElementById('<%=txtTotPeriod.ClientID%>').focus();
                return false;
            }
            if ((IntRate == "") || (IntRate == "0")) {
                window.alert("Enter interest rate first ...!!");
                document.getElementById('<%=txtIntRate.ClientID%>').focus();
                return false;
            }
            if ((InstallAmt == "") || (InstallAmt == "0")) {
                window.alert("Enter installments amount first ...!!");
                document.getElementById('<%=Txtinstll.ClientID%>').focus();
                return false;
            }
        }

           <%-- Only Allow For alphabet --%>
        function onlyAlphabets(e, t) {
            try {
                if (window.event) {
                    var charCode = window.event.keyCode;
                }
                else if (e) {
                    var charCode = e.which;
                }
                else { return true; }
                if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123))
                    return true;
                else
                    return false;
            }
            catch (err) {
                alert(err.Description);
            }
        }

        <%-- Only Allow For Numbers --%>
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;
            return true;
        }
    </script>

    <script type="text/javascript">
        $(function () {
            $('[id*=lstDoc]').multiselect({
                includeSelectAllOption: true
            });
        });
    </script>
    <%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <link href="http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/css/bootstrap.min.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/js/bootstrap.min.js"></script>
    <link href="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/css/bootstrap-multiselect.css" rel="stylesheet" type="text/css" />
    <script src="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/js/bootstrap-multiselect.js" type="text/javascript"></script>--%>
    <script type="text/javascript">
        $(function () {
            $('[id*=lstFruits]').multiselect({
                includeSelectAllOption: true
            });
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="Div1">
                <div class="portlet-title">
                    <div class="caption">
                        Loan Application
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">

                                    <div style="border: 1px solid #3598dc">
                                        <div class="portlet-body">
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Account Information : </strong></div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-1">
                                                        <label class="control-label ">Loan Type<span class="required"></span></label>
                                                    </div>
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="TxtLtype" onkeypress="javascript:return isNumber(event)" AutoPostBack="true" OnTextChanged="TxtLtype_TextChanged" runat="server" CssClass="form-control" TabIndex="1" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <div class="input-icon">
                                                            <i class="fa fa-search"></i>
                                                            <asp:TextBox ID="TxtLname" OnTextChanged="TxtLname_TextChanged" AutoPostBack="true" runat="server" CssClass="form-control" TabIndex="2" />
                                                            <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="TxtLname" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" ServiceMethod="GetGlName">
                                                            </asp:AutoCompleteExtender>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-1">
                                                        <label class="control-label ">Purpose<span class="required"></span></label>
                                                    </div>
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="Txtpur" Enabled="false" runat="server" CssClass="form-control" TabIndex="3" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:DropDownList ID="DDlpurpose" runat="server" CssClass="form-control" OnSelectedIndexChanged="DDlpurpose_SelectedIndexChanged" AutoPostBack="true" TabIndex="4"></asp:DropDownList>
                                                    </div>
                                                    <%--<div class="col-md-3">
                                                        <asp:ListBox ID="lstDoc" runat="server" SelectionMode="Multiple"></asp:ListBox>
                                                    </div>--%>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-12">

                                                    <div class="col-md-1">
                                                        <label class="control-label ">Loan Apply<span class="required"></span></label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtLapply" onkeypress="javascript:return isNumber(event)" runat="server" CssClass="form-control" OnTextChanged="TxtLapply_TextChanged" AutoPostBack="true" TabIndex="5" />
                                                    </div>
                                                    <div class="col-md-1"></div>
                                                    <div class="col-md-1">
                                                        <label class="control-label ">Cust No<span class="required"></span></label>
                                                    </div>
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="txtCustNo" onkeypress="javascript:return isNumber(event)" Enabled="false" runat="server" CssClass="form-control" TabIndex="6" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtCustName" onkeypress="javascript:return isNumber(event)" Enabled="false" runat="server" CssClass="form-control" TabIndex="7" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div style="border: 1px solid #3598dc">
                                        <div class="portlet-body">
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <div class="col-md-10" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Add Surity Details : </strong></div>
                                                    <div class="col-lg-2 right-align">
                                                        <asp:Button ID="btnAddNewRow" CssClass="btn btn-primary addnewbtn" runat="server" Text="Add New Row" OnClick="btnAddNewRow_Click" OnClientClick="javascript:return Confirm();" TabIndex="8" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div id="divInsert" runat="server">
                                                <asp:GridView ID="grdInsert" runat="server" AutoGenerateColumns="false" class="noborder fullwidth">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr.No" ItemStyle-Width="20px">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtSrNo" Enabled="false" CssClass="form-control" runat="server" Text='<%#Container.DataItemIndex+1 %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="MemNo" ItemStyle-Width="100px">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="Txtmemno" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="Txtmemno_TextChanged" Text='<%#Eval("Txtmemno") %>' TabIndex="9" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="CustNo" ItemStyle-Width="100px">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="Txtcustno" onkeypress="javascript:return isNumber(event)" Enabled="false" CssClass="form-control" AutoPostBack="true" runat="server" Text='<%#Eval("Txtcustno") %>' TabIndex="10" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Name" ItemStyle-Width="250px">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="Txtname" CssClass="form-control" AutoPostBack="true" OnTextChanged="Txtname_TextChanged" runat="server" Text='<%#Eval("Txtname") %>' TabIndex="11" />
                                                                <asp:AutoCompleteExtender ID="autocustname" runat="server" TargetControlID="Txtname"
                                                                    UseContextKey="true"
                                                                    CompletionInterval="1"
                                                                    CompletionSetCount="20"
                                                                    MinimumPrefixLength="1"
                                                                    EnableCaching="true"
                                                                    ServicePath="~/WebServices/Contact.asmx"
                                                                    ServiceMethod="GetCustNamesf7">
                                                                </asp:AutoCompleteExtender>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Mem_DT" ItemStyle-Width="100px">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="TxtMemdt" CssClass="form-control" runat="server" Enabled="false" AutoPostBack="true" Text='<%#Eval("Memberdate") %>' TabIndex="12" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Retire_DT" ItemStyle-Width="100px">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="Txtretdate" CssClass="form-control" Enabled="false" runat="server" Text='<%#Eval("DATEOFRET") %>' TabIndex="13" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Loan_Balance" ItemStyle-Width="120px">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="TxtLbal" Enabled="false" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server" AutoPostBack="true" Text='<%#Eval("LoanBal") %>' TabIndex="14" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Rem_Ser" ItemStyle-Width="70px">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="Txtremser" Enabled="false" CssClass="form-control" runat="server" AutoPostBack="true" Text='<%#Eval("REMSERVICE") %>' TabIndex="15" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Mobile" ItemStyle-Width="130px">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtMobileNo" CssClass="form-control" runat="server" Text='<%#Eval("STANDS") %>' TabIndex="16" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>

                                            </div>
                                        </div>
                                    </div>

                                    <div style="border: 1px solid #3598dc">
                                        <div class="portlet-body">
                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-4" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Salary Details : </strong></div>
                                                    <div class="col-md-4" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Loan Eligible</strong></div>
                                                    <div class="col-md-4" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Installment</strong></div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-md-12">
                                                    <label class="control-label col-md-2" style="width: 150px">Gross Salary <span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtGross" runat="server" onkeypress="javascript:return isNumber(event)" CssClass="form-control" TabIndex="17" />
                                                    </div>
                                                    <label class="control-label col-md-2" style="width: 150px">By Membership <span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="Txtmembrship" runat="server" CssClass="form-control" TabIndex="22" />
                                                    </div>
                                                    <label class="control-label col-md-2" style="width: 150px">Repaycapacity <span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="Txtrepay" runat="server" CssClass="form-control" TabIndex="27" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-md-12">
                                                    <label class="control-label col-md-2" style="width: 150px">Net Salary <span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtNet" AutoPostBack="true" runat="server" OnTextChanged="TxtNet_TextChanged" onkeypress="javascript:return isNumber(event)" TabIndex="18" CssClass="form-control" />
                                                    </div>
                                                    <label class="control-label col-md-2" style="width: 150px">By Salary <span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="Txtbysal" runat="server" CssClass="form-control" TabIndex="23" />
                                                    </div>
                                                    <label class="control-label col-md-2" style="width: 150px">Installment Type <span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:DropDownList ID="ddlInstType" runat="server" CssClass="form-control mendatory" OnSelectedIndexChanged="ddlInstType_SelectedIndexChanged" AutoPostBack="true" TabIndex="28">
                                                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                            <asp:ListItem Value="1" Text="Principal"></asp:ListItem>
                                                            <asp:ListItem Value="2" Text="EMI"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-md-12">
                                                    <label class="control-label col-md-2" style="width: 150px">25% <span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="Txttwntyfive" runat="server" CssClass="form-control" TabIndex="19" />
                                                    </div>
                                                    <label class="control-label col-md-2" style="width: 150px">To Be Sanction <span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="Txttosanction" runat="server" onkeypress="javascript:return isNumber(event)" TabIndex="24" CssClass="form-control" />
                                                    </div>
                                                    <label class="control-label col-md-2" style="width: 150px">Installment <span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="Txtinstll" runat="server" onkeypress="javascript:return isNumber(event)" CssClass="form-control" onblur="getTInstallment()" TabIndex="29" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-md-12">
                                                    <label class="control-label col-md-2" style="width: 150px">Sanction By </label>
                                                    <div class="col-md-2">
                                                        <asp:DropDownList ID="ddlSancAuthCd" CssClass="form-control" runat="server" ToolTip="Lookupform1 1097" TabIndex="20">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <label class="control-label col-md-2" style="width: 150px">Period <span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtTotPeriod" runat="server" onkeypress="javascript:return isNumber(event)" CssClass="form-control" TabIndex="25" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-md-12">
                                                    <label class="control-label col-md-2" style="width: 150px">Recom/Ref By </label>
                                                    <div class="col-md-2">
                                                        <asp:DropDownList ID="ddlRecAuthCd" CssClass="form-control" runat="server" ToolTip="Lookupform1 1098" TabIndex="21">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <label class="control-label col-md-2" style="width: 150px">Int Rate <span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtIntRate" runat="server" onkeypress="javascript:return isNumber(event)" CssClass="form-control" TabIndex="26" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div style="border: 1px solid #3598dc">
                                        <div class="portlet-body">
                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">[(A) Previous Loan] : </strong></div>
                                                </div>
                                            </div>

                                            <div id="divPrevious" runat="server" class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <asp:GridView ID="GrdprevLoan" runat="server" AutoGenerateColumns="false" class="noborder fullwidth">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr.No" ItemStyle-Width="20px">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtPSrNo" Enabled="false" CssClass="form-control" runat="server" Text='<%#Container.DataItemIndex+1 %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Prdcd" ItemStyle-Width="70px">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="Txtprcd" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server" Text='<%#Eval("subglcode") %>' TabIndex="30" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="AccNo" ItemStyle-Width="200px">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtAccno" Enabled="false" CssClass="form-control" runat="server" Text='<%#Eval("accno") %>' TabIndex="31" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Prd Name" ItemStyle-Width="200px">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="TxtPname" Enabled="false" CssClass="form-control" runat="server" Text='<%#Eval("GLNAME") %>' TabIndex="32" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Balance" ItemStyle-Width="100px">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="Txtbal" CssClass="form-control" Enabled="false" onkeypress="javascript:return isNumber(event)" runat="server" TabIndex="33" Text='<%#Eval("ODAMT") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Principle" ItemStyle-Width="100px">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="TxtDeduction" onkeypress="javascript:return isNumber(event)" OnTextChanged="TxtDeduction_TextChanged" CssClass="form-control" TabIndex="34" runat="server" AutoPostBack="true" Text='<%#Eval("Amount1") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Interest" ItemStyle-Width="70px">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="TxtInt" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server" Text='<%#Eval("Amount") %>' OnTextChanged="TxtDeduction_TextChanged" AutoPostBack="true" TabIndex="35" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                </asp:GridView>
                                                <div class="pad_top10 pad_bot10 right-align">
                                                    <div class="col-md-3"></div>
                                                    <asp:Label ID="LblSubI" Text="Sub Total" runat="server" />
                                                    <asp:TextBox Width="120px" ID="TxtSubD" runat="server" ReadOnly="true" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div style="border: 1px solid #3598dc">
                                        <div class="portlet-body">
                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-10" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">[(B) Standard] : </strong></div>
                                                    <div class="col-md-2 right-align">
                                                        <asp:Button ID="Btnstandard" CssClass="btn btn-primary addnewbtn" runat="server" Text="Add New Row" OnClick="Btnstandard_Click" TabIndex="36" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div id="divStandard" runat="server" class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <asp:GridView ID="grdstandard" runat="server" AutoGenerateColumns="false" class="noborder fullwidth">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr.No" ItemStyle-Width="20px">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtSSrNo" Enabled="false" CssClass="form-control" runat="server" Text='<%#Container.DataItemIndex+1 %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Prdcd" ItemStyle-Width="70px">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="TxtSprcd" onkeypress="javascript:return isNumber(event)" OnTextChanged="TxtSprcd_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server" Text='<%#Eval("TxtSprcd") %>' TabIndex="37" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="AccNo" ItemStyle-Width="200px">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtAccno" CssClass="form-control" runat="server" Text='<%#Eval("accno") %>' TabIndex="38" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Name" ItemStyle-Width="200px">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="TxtSname" CssClass="form-control" OnTextChanged="TxtSname_TextChanged" runat="server" Text='<%#Eval("TxtSname") %>' TabIndex="39" AutoPostBack="true" />
                                                                <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="TxtSname" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                    MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" ServiceMethod="GetGlNamef7">
                                                                </asp:AutoCompleteExtender>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Per(%)" ItemStyle-Width="200px">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtPer" CssClass="form-control" OnTextChanged="txtPer_TextChanged" AutoPostBack="true" runat="server" Text='<%#Eval("txtPer") %>' TabIndex="40" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Deduction" ItemStyle-Width="100px">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="TxtSDeduction" onkeypress="javascript:return isNumber(event)" OnTextChanged="TxtSDeduction_TextChanged" CssClass="form-control" runat="server" AutoPostBack="true" Text='<%#Eval("TxtSDeduction") %>' TabIndex="41" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                    </Columns>
                                                </asp:GridView>
                                                <div class="pad_top10 pad_bot10 right-align">
                                                    <div class="col-md-1"></div>
                                                    <asp:Label ID="LblSubS" Text="Sub Total" runat="server" />
                                                    <asp:TextBox ID="TxtSubS" Width="120px" runat="server" ReadOnly="true" TabIndex="42" />
                                                </div>
                                            </div>

                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Total deduction :<span class="required">*</span></label>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="TxttotDed" onkeypress="javascript:return isNumber(event)" runat="server" CssClass="form-control" ReadOnly="true" TabIndex="47" /></span>
                                                    </div>
                                                    <label id="LblSubD" runat="server" class="control-label col-md-2">Sub Total :<span class="required">*</span></label>
                                                    <div class="col-md-3 right-align">
                                                        <asp:TextBox ID="TxtSubO" Width="120px" CssClass="form-control" runat="server" ReadOnly="true" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-12 " style="text-align: center;">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Create" CssClass="btn btn-primary" OnClick="btnSubmit_Click" TabIndex="48" OnClientClick="Javascript:return IsValide();" />
                                        <asp:Button ID="btnExit" runat="server" Text="Exit" CssClass="btn btn-primary" TabIndex="49" OnClick="btnExit_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>

    <div class="row" id="Div_grid" runat="server">
        <div class="col-md-12">
            <div class="table-scrollable" style="height: auto; max-height: 200px; overflow-x: auto; overflow-y: auto">
                <table class="table table-striped table-bordered table-hover" width="100%">
                    <thead>
                        <tr>
                            <th>
                                <asp:GridView ID="GrdLoanAp" runat="server" CssClass="mGrid" CellPadding="6" CellSpacing="7" PageIndex="5" ForeColor="#333333"
                                    AutoGenerateColumns="False" BorderWidth="1px" BorderColor="#333300" Width="100%" ShowFooter="true">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="ID" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="Id" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PRDCODE" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="LOANTYPE" runat="server" Text='<%# Eval("LOANTYPE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CUSTNO" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="CUSTNO" runat="server" Text='<%# Eval("CUSTNO ") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="App Date" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="Date" runat="server" Text='<%# Eval("Date ") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sanction Amount" Visible="true" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <asp:Label ID="Amount" runat="server" Text='<%# Eval("sancitonamount ") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="Status" runat="server" Text='<%# Eval("status ") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Modify" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkModify" runat="server" CommandArgument='<%#Eval("Id")+","+ Eval("PRDCODE")+","+Eval("CUSTNO")%>' CommandName="select" class="glyphicon glyphicon-pencil" OnClick="LnkModify_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Authorise" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkAutorise" runat="server" CommandArgument='<%#Eval("Id")+","+ Eval("PRDCODE")+","+Eval("CUSTNO")%>' CommandName="select" class="glyphicon glyphicon-pencil" OnClick="LnkAutorise_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Delete" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Eval("Id")+","+ Eval("PRDCODE")+","+Eval("CUSTNO")%>' CommandName="select" class="glyphicon glyphicon-trash" OnClick="lnkDelete_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Center" />
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

        <div id="DivAddMore" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <center><h4 class="modal-title" style="color:#ff0000">AVS Company</h4></center>
                    </div>
                    <div class="modal-body">
                        <p></p>
                        <asp:Label ID="Label1" runat="server" Text="" Style="color: black"></asp:Label>
                        <div class="row">
                            <div class="col-md-12" align="center">
                                <asp:Button ID="BtnYes" class="btn btn-primary" runat="server" Text="Ok" OnClick="BtnYes_Click"></asp:Button>
                            </div>
                        </div>

                    </div>
                    <div class="modal-footer">
                        <button class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>

                </div>
            </div>
        </div>
    </div>

    <div class="row">
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
    </div>
    <asp:HiddenField ID="hdnApp" runat="server" Value="0" />
</asp:Content>

