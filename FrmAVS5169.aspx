<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAVS5169.aspx.cs" Inherits="FrmAVS5169" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
        function IsValide() {
            var ddlprefix = document.getElementById('<%=txtcstno.ClientID%>').value;
            var message = '';

            if (txtcstno == "") {
                //alert("Please Select Prefix.....!!");
                message = 'Please Enter Customer number....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=txtcstno.ClientID %>').focus();
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
    </script>
    <style type="text/css">
        .btn {
            text-decoration: none;
            border: 1px solid #000;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        Account Master Patch
                        <asp:Literal ID="ltrlHeader" runat="server"></asp:Literal>
                    </div>
                </div>
                <div class="portlet-body form">
                    <!--<form action="#" class="form-horizontal" id="submit_form" method="POST">   -->
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab__blue">
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Product Type <span class="required">*</span></label>

                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txttype" CssClass="form-control" runat="server" onkeypress="return isNumber(event)" OnTextChanged="txttype_TextChanged" AutoPostBack="true" TabIndex="1"></asp:TextBox>
                                                </div>
                                                <div class="col-lg-4">
                                                    <asp:TextBox ID="txttynam" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txttynam_TextChanged" TabIndex="2"></asp:TextBox>
                                                    <div id="Custlist2" style="height: 200px; overflow-y: scroll;"></div>
                                                    <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="txttynam"
                                                        UseContextKey="true"
                                                        CompletionInterval="1"
                                                        CompletionSetCount="20"
                                                        MinimumPrefixLength="1"
                                                        EnableCaching="true"
                                                        ServicePath="~/WebServices/Contact.asmx"
                                                        ServiceMethod="GetGlName" CompletionListElementID="Custlist2">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                            </div>
                                        </div>

                                        <div id="DIVACC" runat="server" class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">AccountNo</label>
                                                <div class="col-lg-2">
                                                    <asp:TextBox ID="txtaccno" runat="server" CssClass="form-control" onkeypress="javascript:return isNumber (event)" OnTextChanged="txtaccno_TextChanged" AutoPostBack="true" TabIndex="5"></asp:TextBox>
                                                </div>
                                                <div class="col-lg-4">
                                                    <asp:TextBox ID="TxtAccName" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="TxtAccName_TextChanged" TabIndex="6"></asp:TextBox>
                                                    <div id="CustList" style="height: 200px; overflow-y: scroll;"></div>
                                                    <asp:AutoCompleteExtender ID="AutoAccname" runat="server" TargetControlID="TxtAccName"
                                                        UseContextKey="true"
                                                        CompletionInterval="1"
                                                        CompletionSetCount="20"
                                                        MinimumPrefixLength="1"
                                                        EnableCaching="true"
                                                        ServicePath="~/WebServices/Contact.asmx"
                                                        ServiceMethod="GetAccName" CompletionListElementID="CustList">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc"></strong></div>
                                            </div>
                                        </div>

                                        <div runat="server" id="TblDiv_MainWindow">
                                            <div id="Depositdiv" runat="server">
                                                <div style="border: 1px solid #3598dc">
                                                    <asp:Table ID="Table1" runat="server">
                                                        <asp:TableRow ID="TableRow1" runat="server" Style="width: 300px">
                                                            <asp:TableCell ID="TableCell1" runat="server" Style="width: 2000px">

                                                                <div class="row" style="margin: 7px 0 7px 0" runat="server" id="CUSTNODIV">
                                                                    <div class="col-lg-12">
                                                                        <label class="control-label col-md-2">Customer No<span class="required">*</span></label>
                                                                        <div class="col-md-2">
                                                                            <asp:TextBox ID="txtcstno" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" TabIndex="3"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-lg-4">
                                                                            <asp:TextBox ID="txtname" runat="server" CssClass="form-control" TabIndex="4"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="row" style="margin: 7px 0 7px 0">
                                                                    <div class="col-lg-12">
                                                                        <label class="control-label col-md-2">Account Type <span class="required">*</span></label>
                                                                        <div class="col-md-2">
                                                                            <asp:TextBox ID="txtAccType" CssClass="form-control" runat="server" OnTextChanged="txtAccType_TextChanged" AutoPostBack="true" onkeypress="javascript:return isNumber (event)" TabIndex="7"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-md-4">
                                                                            <asp:DropDownList ID="Ddlacctype" runat="server" CssClass="form-control" OnSelectedIndexChanged="Ddlacctype_SelectedIndexChanged" AutoPostBack="true" EnableViewState="true" TabIndex="8"></asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="row" style="margin: 7px 0 7px 0">
                                                                    <div class="col-lg-12">
                                                                        <label class="control-label col-md-2">Opening Date</label>
                                                                        <div class="col-md-2">
                                                                            <asp:TextBox ID="txtodate" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber(event)" placeholder="DD/MM/YYYY" CssClass="form-control" runat="server" TabIndex="9"></asp:TextBox>
                                                                            <asp:CalendarExtender ID="TxtFDT_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtodate">
                                                                            </asp:CalendarExtender>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="row" style="margin: 7px 0 7px 0">
                                                                    <div class="col-lg-12">
                                                                        <label class="control-label col-md-2">Mode of Operation<span class="required">*</span></label>
                                                                        <div class="col-md-2">
                                                                            <asp:TextBox ID="txtmopr" CssClass="form-control" runat="server" OnTextChanged="txtmopr_TextChanged" AutoPostBack="true" TabIndex="10"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-lg-4">
                                                                            <asp:DropDownList ID="DdlModeofOpr" runat="server" CssClass="form-control" OnSelectedIndexChanged="DdlModeofOpr_SelectedIndexChanged" AutoPostBack="true" EnableViewState="true" TabIndex="11"></asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="row" style="margin: 7px 0 7px 0">
                                                                    <div class="col-lg-12">
                                                                        <label class="control-label col-md-2">Incase of Minor  <span class="required"></span></label>
                                                                        <div class="col-md-2">
                                                                            <label for="chkYes">
                                                                                <asp:RadioButton ID="chkYes" runat="server" Text="Yes" OnCheckedChanged="chkYes_CheckedChanged" GroupName="YN" name="minor" AutoPostBack="true" TabIndex="12" />
                                                                            </label>
                                                                            <label for="chkNo">
                                                                                <asp:RadioButton ID="chkNo" runat="server" Text="No" GroupName="YN" name="minor" OnCheckedChanged="chkNo_CheckedChanged" AutoPostBack="true" TabIndex="13" />
                                                                            </label>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="row" style="margin: 7px 0 7px 0" id="dvminor" runat="server" visible="false">
                                                                    <div class="col-lg-12">
                                                                        <label class="control-label col-md-2">CustomerID </label>
                                                                        <div class="col-md-2">
                                                                            <asp:TextBox ID="txtcustid" CssClass="form-control" runat="server" OnTextChanged="txtcustid_TextChanged" AutoPostBack="true" TabIndex="14"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-md-4">
                                                                            <asp:TextBox ID="txtacconam" CssClass="form-control" OnTextChanged="txtacconam_TextChanged" AutoPostBack="true" runat="server" TabIndex="15"></asp:TextBox>
                                                                            <div id="Custlist1" style="height: 200px; overflow-y: scroll;"></div>
                                                                            <asp:AutoCompleteExtender ID="AutoCompleteExtenderminor" runat="server" TargetControlID="txtacconam"
                                                                                UseContextKey="true"
                                                                                CompletionInterval="1"
                                                                                CompletionSetCount="20"
                                                                                MinimumPrefixLength="1"
                                                                                EnableCaching="true"
                                                                                ServicePath="~/WebServices/Contact.asmx"
                                                                                ServiceMethod="GetCustNames" CompletionListElementID="Custlist1">
                                                                            </asp:AutoCompleteExtender>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="row" style="margin: 7px 0 7px 0" id="dvmname" runat="server" visible="false">
                                                                    <div class="col-lg-12">
                                                                        <label class="control-label col-md-2">Relation:</label>
                                                                        <div class="col-md-4">
                                                                            <asp:RadioButton ID="rdbmom" OnCheckedChanged="rdbmom_CheckedChanged" runat="server" Text="Mother" GroupName="MDG" TabIndex="16"></asp:RadioButton>
                                                                            <asp:RadioButton ID="rdbdad" OnCheckedChanged="rdbdad_CheckedChanged" runat="server" Text="Father" GroupName="MDG" TabIndex="17"></asp:RadioButton>
                                                                            <asp:RadioButton ID="rdbguard" OnCheckedChanged="rdbguard_CheckedChanged" runat="server" Text="Gaurdian" GroupName="MDG" TabIndex="18"></asp:RadioButton>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="row" style="margin: 7px 0 7px 0" id="dvmdate" runat="server" visible="false">
                                                                    <div class="col-lg-12">
                                                                        <label class="control-label col-md-2">Minor's Date of Birth</label>
                                                                        <div class="col-md-2">
                                                                            <asp:TextBox ID="txtmdate" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber(event)" placeholder="DD/MM/YYYY" CssClass="form-control" MaxLength="10" runat="server" OnTextChanged="txtmdate_TextChanged" AutoPostBack="true" TabIndex="19"></asp:TextBox>
                                                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtmdate">
                                                                            </asp:CalendarExtender>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="row" style="margin: 7px 0 7px 0">
                                                                    <div class="col-lg-12">
                                                                        <label class="control-label col-md-2">Ref Cust No.</label>
                                                                        <div class="col-md-2">
                                                                            <asp:TextBox ID="Txtrefcustno" CssClass="form-control" runat="server" placeholder="Ref Cust No" onkeypress="javascript:return isNumber (event)" OnTextChanged="Txtrefcustno_TextChanged" AutoPostBack="true" TabIndex="20"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-lg-4">
                                                                            <asp:TextBox ID="txtrefcustname" runat="server" CssClass="form-control" placeholder="Ref Cust Name" AutoPostBack="true" OnTextChanged="txtrefcustname_TextChanged" TabIndex="21"></asp:TextBox>
                                                                            <div id="Div1" style="height: 200px; overflow-y: scroll;"></div>
                                                                            <asp:AutoCompleteExtender ID="autocustname1" runat="server" TargetControlID="txtrefcustname"
                                                                                UseContextKey="true"
                                                                                CompletionInterval="1"
                                                                                CompletionSetCount="20"
                                                                                MinimumPrefixLength="1"
                                                                                EnableCaching="true"
                                                                                ServicePath="~/WebServices/Contact.asmx"
                                                                                ServiceMethod="GetCustNames" CompletionListElementID="Div1">
                                                                            </asp:AutoCompleteExtender>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="row" style="margin: 7px 0 7px 0">
                                                                    <div class="col-lg-12">
                                                                        <label class="control-label col-md-2">Executive/Agent No.</label>
                                                                        <div class="col-md-2">
                                                                            <asp:TextBox ID="TxtAgentNo" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber(event)" placeholder="Agent No" AutoPostBack="true" OnTextChanged="TxtAgentNo_TextChanged" TabIndex="22"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-lg-4">
                                                                            <asp:TextBox ID="TxtAgentname" runat="server" CssClass="form-control" AutoPostBack="true" placeholder="Agent Name" OnTextChanged="TxtAgentname_TextChanged" TabIndex="23"></asp:TextBox>
                                                                            <div id="Div2" style="height: 200px; overflow-y: scroll;"></div>
                                                                            <asp:AutoCompleteExtender ID="autoagentname" runat="server" TargetControlID="TxtAgentname"
                                                                                UseContextKey="true"
                                                                                CompletionInterval="1"
                                                                                CompletionSetCount="20"
                                                                                MinimumPrefixLength="1"
                                                                                EnableCaching="true"
                                                                                ServicePath="~/WebServices/Contact.asmx"
                                                                                ServiceMethod="GetDDSAcc" CompletionListElementID="Div2">
                                                                            </asp:AutoCompleteExtender>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div id="Div_nominee" runat="server" visible="false">
                                                                    <div class="row">
                                                                        <div class="col-lg-12">
                                                                            <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">[Nominee Details] : </strong></div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="row" style="margin-bottom: 10px;">
                                                                        <div class="col-lg-12">
                                                                            <div class="col-md-1">
                                                                                <label class="control-label">1.</label>

                                                                            </div>
                                                                            <div class="col-md-4">
                                                                                <asp:TextBox ID="txtNomName" CssClass="form-control" placeholder="Nominee Full Name" runat="server" TabIndex="24"></asp:TextBox>
                                                                            </div>
                                                                            <div class="col-md-1">
                                                                                <label class="control-label">Relation </label>
                                                                            </div>
                                                                            <div class="col-md-2">
                                                                                <asp:DropDownList ID="ddlRelation" Width="130px" runat="server" CssClass="form-control" TabIndex="25"></asp:DropDownList>
                                                                            </div>
                                                                            <div class="col-md-1">
                                                                                <label class="control-label">BirthDate</label>
                                                                            </div>
                                                                            <div class="col-md-2">
                                                                                <asp:TextBox ID="txtBithdate" onkeyup="FormatIt(this)" CssClass="form-control" MaxLength="10" onkeypress="javascript:return isNumber(event)" placeholder="DD/MM/YYYY" runat="server" TabIndex="26"></asp:TextBox>
                                                                                <asp:CalendarExtender ID="TxtDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtBithdate">
                                                                                </asp:CalendarExtender>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="row" style="margin-bottom: 10px;">
                                                                        <div class="col-md-12">
                                                                            <div class="col-md-1">
                                                                                <label class="control-label">2.</label>
                                                                            </div>
                                                                            <div class="col-md-4">
                                                                                <asp:TextBox ID="txtnom2" CssClass="form-control" placeholder="Nominee Full Name" runat="server" TabIndex="27"></asp:TextBox>
                                                                            </div>
                                                                            <div class="col-md-1">
                                                                                <label class="control-label">Relation </label>
                                                                            </div>
                                                                            <div class="col-md-2">
                                                                                <asp:DropDownList ID="ddlrelation2" Width="130px" runat="server" CssClass="form-control" TabIndex="28"></asp:DropDownList>
                                                                            </div>
                                                                            <div class="col-md-1">
                                                                                <label class="control-label">BirthDate</label>
                                                                            </div>
                                                                            <div class="col-md-2">
                                                                                <asp:TextBox ID="txtdob2" onkeyup="FormatIt(this)" MaxLength="10" onkeypress="javascript:return isNumber(event)" CssClass="form-control" placeholder="DD/MM/YYYY" runat="server" TabIndex="29"></asp:TextBox>
                                                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtdob2">
                                                                                </asp:CalendarExtender>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div id="Div_Joint" runat="server" visible="false">
                                                                    <div class="row">
                                                                        <div class="col-lg-12">
                                                                            <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">[Joint Account Details] : </strong></div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-2"></div>

                                                                    <div class="row" style="margin-bottom: 10px;">
                                                                        <div class="col-lg-12">
                                                                            <div class="col-md-1">
                                                                                <label class="control-label">1</label>
                                                                            </div>
                                                                            <div class="col-md-1">
                                                                                <asp:TextBox ID="txtJointCust1" CssClass="form-control" placeholder="Cust No" AutoPostBack="true" OnTextChanged="txtJointCust1_TextChanged" runat="server" TabIndex="30"></asp:TextBox>
                                                                            </div>
                                                                            <div class="col-md-4">
                                                                                <asp:TextBox ID="txtJointFName" CssClass="form-control" placeholder="Full Name" runat="server" TabIndex="31"></asp:TextBox>
                                                                            </div>
                                                                            <div class="col-md-1">
                                                                                <label class="control-label">Relation </label>
                                                                            </div>
                                                                            <div class="col-md-2">
                                                                                <asp:DropDownList ID="ddlreljoint" Width="130px" runat="server" CssClass="form-control" TabIndex="32"></asp:DropDownList>
                                                                            </div>
                                                                            <div class="col-md-1">
                                                                                <label class="control-label">BirthDate</label>
                                                                            </div>
                                                                            <div class="col-md-2">
                                                                                <asp:TextBox ID="TxtDOB" placeholder="DD/MM/YYYY" CssClass="form-control" MaxLength="10" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" runat="server" AutoPostBack="true" TabIndex="33"></asp:TextBox>
                                                                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtDOB">
                                                                                </asp:CalendarExtender>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row" style="margin-bottom: 10px;">
                                                                        <div class="col-lg-12">
                                                                            <div class="col-md-1">
                                                                                <label class="control-label">2</label>
                                                                            </div>
                                                                            <div class="col-md-1">
                                                                                <asp:TextBox ID="txtJointCust2" CssClass="form-control" placeholder="Cust No" AutoPostBack="true" OnTextChanged="txtJointCust2_TextChanged" runat="server" TabIndex="34"></asp:TextBox>
                                                                            </div>
                                                                            <div class="col-md-4">
                                                                                <asp:TextBox ID="txtjname2" CssClass="form-control" placeholder="Full Name" runat="server" TabIndex="35"></asp:TextBox>
                                                                            </div>
                                                                            <div class="col-md-1">
                                                                                <label class="control-label">Relation </label>
                                                                            </div>
                                                                            <div class="col-md-2">
                                                                                <asp:DropDownList ID="ddlrelationj2" Width="130px" runat="server" CssClass="form-control" TabIndex="36"></asp:DropDownList>
                                                                            </div>
                                                                            <div class="col-md-1">
                                                                                <label class="control-label">BirthDate</label>
                                                                            </div>
                                                                            <div class="col-md-2">
                                                                                <asp:TextBox ID="txtjbirth2" CssClass="form-control" MaxLength="10" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" placeholder="Birth Date" runat="server" AutoPostBack="true" TabIndex="37"></asp:TextBox>
                                                                                <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtjbirth2">
                                                                                </asp:CalendarExtender>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </asp:TableCell>

                                                        </asp:TableRow>
                                                    </asp:Table>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-12" style="text-align: center">
                                                <asp:Button ID="Submit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" OnClientClick="Javascript:return IsValide();" TabIndex="38" />
                                                <asp:Button ID="Clear" runat="server" Text="Clear All" CssClass="btn btn-primary" OnClick="Clear_Click" TabIndex="39" />
                                                <asp:Button ID="Exit" runat="server" Text="Exit" CssClass="btn btn-primary" OnClick="Exit_Click" TabIndex="40" />
                                            </div>
                                        </div>

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
            <div class="table-scrollable" style="width: 100%; height: 500px; overflow-x: auto; overflow-y: auto">
                <table class="table table-striped table-bordered table-hover">
                    <thead>
                        <tr>
                            <th>
                                <asp:GridView ID="GrdAcc" runat="server" CellPadding="6" CellSpacing="7" ForeColor="#333333" PageIndex="5"
                                    AutoGenerateColumns="False" CssClass="mGrid" BorderWidth="1px" BorderColor="#333300" Width="100%" ShowFooter="true">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>

                                        <asp:TemplateField HeaderText="SUBGLCODE" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="SUBGLCODE" runat="server" Text='<%# Eval("SUBGLCODE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="ACCNO" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="ACCNO" runat="server" Text='<%# Eval("ACCNO ") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="CUSTNAME" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="CUSTNAME" runat="server" Text='<%# Eval("CUSTNAME ") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="MakerName" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="MakerName" runat="server" Text='<%# Eval("MakerName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Modify" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkModify" runat="server" CommandArgument='<%#Eval("SUBGLCODE")+","+Eval("ACCNO")%>' CommandName="select" class="glyphicon glyphicon-pencil" OnClick="LnkModify_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Delete" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Eval("SUBGLCODE")+","+Eval("ACCNO")%>' CommandName="select" class="glyphicon glyphicon-trash" OnClick="lnkDelete_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Authorise" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkAutorise" runat="server" CommandArgument='<%#Eval("SUBGLCODE")+","+Eval("ACCNO")%>' CommandName="select" class="glyphicon glyphicon-pencil" OnClick="LnkAutorise_Click"></asp:LinkButton>
                                            </ItemTemplate>
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
    </div>

</asp:Content>

