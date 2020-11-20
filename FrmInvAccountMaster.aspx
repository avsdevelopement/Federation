<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmInvAccountMaster.aspx.cs" Inherits="FrmInvAccountMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
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

        function IsValid() {
            debugger;
            var InvType = document.getElementById('<%=ddlInvestment.ClientID%>').value;
             var Bankno = document.getElementById('<%=txtBankNo.ClientID%>').value;
            var Receipt = document.getElementById('<%=txtReceiptNo.ClientID%>').value;
            var OpeningDate = document.getElementById('<%=txtOpenDate.ClientID%>').value;
            var CRPrd = document.getElementById('<%=txtIntProdCode.ClientID%>').value;
            var PrPrd = document.getElementById('<%=txtPrinProdCode.ClientID%>').value;
            var Ac = document.getElementById('<%=txtAC.ClientID%>').value;
            var RevAcc = document.getElementById('<%=txtPrinAccNo.ClientID%>').value;

            var message = '';

            if (InvType == "0") {
                message = 'Please select Investment Type...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=ddlInvestment.ClientID%>').focus();
                return false;
            }

            if (Bankno == "") {
                message = 'Please Enter Bank Number...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtBankNo.ClientID%>').focus();
                return false;
            }

            if (Receipt == "") {
                message = 'Please Enter Receipt Number...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtReceiptNo.ClientID%>').focus();
                return false;
            }

            if (OpeningDate == "dd/MM/yyyy") {
                message = 'Please Select Opening Date...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtOpenDate.ClientID%>').focus();
                return false;
            }

            if (CRPrd == "") {
                message = 'Please Enter Credit Product Code...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtIntProdCode.ClientID%>').focus();
                return false;
            }

            if (PrPrd == "") {
                message = 'Please Enter Receivable Product Code...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtPrinProdCode.ClientID%>').focus();
                return false;
            }

            if (Ac == "") {
                message = 'Please Enter Account Number...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtAC.ClientID%>').focus();
                return false;
            }

            if (RevAcc == "") {
                message = 'Please Enter Receivable Account Number...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtPrinAccNo.ClientID%>').focus();
                return false;
            }
        }
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="portlet box blue" id="form_wizard_1">
                        <div class="portlet-title">
                            <div class="caption">
                                Investment Master 
                            </div>
                        </div>
                        <div class="portlet-body form">
                            <div class="form-horizontal">
                                <div class="form-wizard">
                                    <div class="form-body">
                                        <div class="tab-content">
                                            <div class="tab-pane active" id="tab__blue">
                                                <ul class="nav nav-pills">

                                                    <li>
                                                        <asp:LinkButton ID="lnkAdd" runat="server" Text="a" class="btn btn-default" OnClick="lnkAdd_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-asterisk"></i>Add New</asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton ID="lnkExist" runat="server" Text="a" class="btn btn-default" OnClick="lnkExist_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-asterisk"></i>Add Existing</asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton ID="lnkModify" runat="server" Text="a" class="btn btn-default" OnClick="lnkModify_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-plus-circle"></i>Modify</asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton ID="lnkDelete" runat="server" Text="MD" class="btn btn-default" OnClick="lnkDelete_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-pencil-square-o"></i>Delete</asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton ID="lnkAutho" runat="server" Text="a" class="btn btn-default" OnClick="lnkAutho_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-arrows"></i>Authorise</asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton ID="lnkView" runat="server" Text="VW" class="btn btn-default" OnClick="lnkView_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-arrows"></i>View</asp:LinkButton>
                                                    </li>
                                                    <li class="pull-right">
                                                        <asp:Label ID="lblStst" runat="server" Text="Activity Perform :" Style="font-weight: bold;"></asp:Label>
                                                        <asp:Label ID="lblActivity" runat="server" Text=""></asp:Label>
                                                    </li>
                                                </ul>
                                            </div>

                                            <div style="border: 1px solid #3598dc">
                                                 <div class="row" style="margin-top: 10px">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-2">
                                                                <label class="control-label">Investment Type <span class="required">*</span></label>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:DropDownList ID="ddlInvestment" runat="server" CssClass="form-control" ></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                <div id="DivNew" runat="server" visible="true">
                                                    
                                                    <div class="row" style="margin-top: 10px;">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-2">
                                                                <label class="control-label">Product Code<span class="required">*</span></label>
                                                            </div>
                                                            <div class="col-lg-2">
                                                                <asp:TextBox ID="txtBankNo" placeholder="Product Code" Enabled="false" OnTextChanged="txtBankNo_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtBankName" runat="server" OnTextChanged="txtBankName_TextChanged" Style="text-transform: uppercase" autocomplete="off"
                                                                    AutoPostBack="true" placeholder="Product Name" CssClass="form-control"></asp:TextBox>
                                                               <%-- <div id="CustList1" style="height: 200px; overflow-y: scroll;"></div>
                                                                <asp:AutoCompleteExtender ID="AutoGlName" runat="server" TargetControlID="txtBankName" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20" MinimumPrefixLength="1"
                                                                    EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList1" ServiceMethod="GetINVGlName">
                                                                </asp:AutoCompleteExtender>--%>
                                                            </div>
                                                              <div class="col-md-1">
                                                                <label class="control-label">A/C No</label>
                                                            </div>
                                                            <div class="col-lg-2">
                                                                <asp:TextBox ID="txtAC" placeholder="Account No" ReadOnly="true" CssClass="form-control" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>

                                                    

                                                    <div class="row" style="margin-top: 10px;">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-2">
                                                                <label class="control-label">Investment Bank <span class="required">*</span></label>
                                                            </div>
                                                          
                                                           <div class="col-md-4">
                                                                <asp:TextBox ID="txtBank1" Style="text-transform: uppercase" autocomplete="off" runat="server" placeholder="Bank Name" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtBranchName" Style="text-transform: uppercase" autocomplete="off" runat="server" placeholder="Branch Name" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                
                                                </div>
                                                <div id="DivOld" runat="server" visible="false">
                                                    <%--Amruta 31/05/2017--%>
                                                   
                                                    <div id="DivREF" runat="server">
                                                        <div class="row" style="margin-top: 10px;">
                                                            <div class="col-lg-12">
                                                                <div class="col-md-2">
                                                                    <label class="control-label">Product Code <span class="required">*</span></label>
                                                                </div>
                                                                <div class="col-lg-2">
                                                                    <asp:TextBox ID="txtPCode" placeholder="Product Code" Style="text-transform: uppercase" autocomplete="off" CssClass="form-control" runat="server" OnTextChanged="txtPCode_TextChanged" AutoPostBack="true" />
                                                                </div>
                                                                <div class="col-lg-4">
                                                                    <asp:TextBox ID="txtPType" placeholder="Product Name" Style="text-transform: uppercase" autocomplete="off" CssClass="form-control" runat="server" OnTextChanged="txtPType_TextChanged" AutoPostBack="true" />
                                                                    <div id="DivList3" style="height: 200px; overflow-y: scroll;"></div>
                                                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="txtPType" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20" MinimumPrefixLength="1"
                                                                        EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="DivList3" ServiceMethod="GetGlName">
                                                                    </asp:AutoCompleteExtender>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin-top: 10px;">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-2">
                                                                <label class="control-label">Bank Name <span class="required">*</span></label>
                                                            </div>
                                                            
                                                            <div class="col-lg-4">
                                                                <asp:TextBox ID="txtBankName1" placeholder="Bank Name" Style="text-transform: uppercase" autocomplete="off" CssClass="form-control" runat="server" />
                                                                <div id="DivList2" style="height: 200px; overflow-y: scroll;"></div>
                                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtBankName1" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20" MinimumPrefixLength="1"
                                                                    EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="DivList2" ServiceMethod="GetGlName">
                                                                </asp:AutoCompleteExtender>
                                                            </div>
                                                              <div class="col-lg-2">
                                                                <asp:TextBox ID="txtBranch" placeholder="Branch Name" Style="text-transform: uppercase" autocomplete="off" CssClass="form-control" runat="server" />

                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin-top: 10px;">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-2">
                                                                <label class="control-label">Acc No</label>
                                                            </div>
                                                            <div class="col-lg-2">
                                                                <asp:TextBox ID="txtAccNo" placeholder="Account No" ReadOnly="true" CssClass="form-control" runat="server" />
                                                            </div>

                                                          
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row" style="margin-top: 10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label">Receipt No <span class="required">*</span></label>
                                                        </div>
                                                        <div class="col-lg-2">
                                                            <asp:TextBox ID="txtReceiptNo" placeholder="Receipt Number" Enabled="false" CssClass="form-control" runat="server" />
                                                        </div>
                                                        <div class="col-lg-4">
                                                            <asp:TextBox ID="txtReceiptName" Style="text-transform: uppercase" autocomplete="off" placeholder="Enter Name Here" CssClass="form-control" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row" style="margin-top: 10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label">Board Resolution No </label>
                                                        </div>
                                                        <div class="col-lg-2">
                                                            <asp:TextBox ID="txtBResNo" placeholder="Board Resolution No" CssClass="form-control" runat="server" />
                                                        </div>
                                                        <div class="col-md-2">
                                                            <label class="control-label">Board Meeting Date </label>
                                                        </div>
                                                        <div class="col-lg-2">
                                                            <asp:TextBox ID="txtBMeetDate" onkeyup="FormatIt(this);" onkeypress="javascript:return isNumber(event)" placeholder="Board Meeting Date" CssClass="form-control" runat="server" />
                                                            <asp:CalendarExtender ID="txtBMeetDate_CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtBMeetDate">
                                                            </asp:CalendarExtender>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row" style="margin-top: 10px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label">Opening Date <span class="required">*</span></label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtOpenDate" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber(event)" placeholder="DD/MM/YYYY" CssClass="form-control" runat="server" TabIndex="9"></asp:TextBox>
                                                            <asp:CalendarExtender ID="TxtFDT_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtOpenDate">
                                                            </asp:CalendarExtender>
                                                        </div>
                                                    </div>
                                                </div>


                                                <div class="row" style="margin-top: 5px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #0A23F9">INTEREST CREDIT GL : </strong></div>
                                                    </div>
                                                </div>

                                                <div class="row" style="margin-top: 5px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label ">Product Code :<span class="required"> *</span></label>
                                                        </div>
                                                        <div class="col-md-1">
                                                            <asp:TextBox ID="txtIntProdCode" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtIntProdCode_TextChanged" PlaceHolder="Product Code"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="input-icon">
                                                                <i class="fa fa-search"></i>
                                                                <asp:TextBox ID="txtIntProdName" runat="server" CssClass="form-control" placeholder="Search Product Name" AutoPostBack="true" OnTextChanged="txtIntProdName_TextChanged"></asp:TextBox>
                                                                <div id="CustList3" style="height: 200px; overflow-y: scroll;"></div>
                                                                <asp:AutoCompleteExtender ID="IntAutoGlName" runat="server" TargetControlID="txtIntProdName" UseContextKey="true"
                                                                    CompletionInterval="1" CompletionSetCount="20" MinimumPrefixLength="1" EnableCaching="true"
                                                                    ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList3" ServiceMethod="GetGlName">
                                                                </asp:AutoCompleteExtender>
                                                            </div>
                                                        </div>
                                                        <%--<div class="col-md-2">
                                                            <label class="control-label ">Account Number :<span class="required"> *</span></label>
                                                        </div>
                                                        <div class="col-md-1">
                                                            <asp:TextBox ID="txtIntAccNo" CssClass="form-control" onkeypress="javascript:return isNumber (event)" PlaceHolder="Account Number" runat="server" AutoPostBack="true" OnTextChanged="txtIntAccNo_TextChanged"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="input-icon">
                                                                <i class="fa fa-search"></i>
                                                                <asp:TextBox ID="txtIntAccName" CssClass="form-control" PlaceHolder="Search Customer Name" OnTextChanged="txtIntAccName_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                                                <div id="CustList4" style="height: 200px; overflow-y: scroll;"></div>
                                                                <asp:AutoCompleteExtender ID="IntAutoAccName" runat="server" TargetControlID="txtIntAccName" UseContextKey="true"
                                                                    CompletionInterval="1" CompletionSetCount="20" MinimumPrefixLength="1" EnableCaching="true"
                                                                    ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList4" ServiceMethod="GetAccName">
                                                                </asp:AutoCompleteExtender>
                                                            </div>
                                                        </div>--%>
                                                    </div>
                                                </div>

                                                <div class="row" style="margin-top: 5px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #0A23F9">INTEREST RECIVBLE GL: </strong></div>
                                                    </div>
                                                </div>

                                                <div class="row" style="margin-top: 5px;">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label ">Product Code :<span class="required"> *</span></label>
                                                        </div>
                                                        <div class="col-md-1">
                                                            <asp:TextBox ID="txtPrinProdCode" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtPrinProdCode_TextChanged" PlaceHolder="Product Code"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="input-icon">
                                                                <i class="fa fa-search"></i>
                                                                <asp:TextBox ID="txtPrinProdName" runat="server" PlaceHolder="Search Product Name" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtPrinProdName_TextChanged"></asp:TextBox>
                                                                <div id="CustList5" style="height: 200px; overflow-y: scroll;"></div>
                                                                <asp:AutoCompleteExtender ID="PrinAutoGlName" runat="server" TargetControlID="txtPrinProdName" UseContextKey="true"
                                                                    CompletionInterval="1" CompletionSetCount="20" MinimumPrefixLength="1" EnableCaching="true"
                                                                    ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList5" ServiceMethod="GetGlName">
                                                                </asp:AutoCompleteExtender>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <label class="control-label ">Account Number :<span class="required"> *</span></label>
                                                        </div>
                                                        <div class="col-md-1">
                                                            <asp:TextBox ID="txtPrinAccNo" CssClass="form-control" onkeypress="javascript:return isNumber (event)" PlaceHolder="Account Number" runat="server" AutoPostBack="true" OnTextChanged="txtPrinAccNo_TextChanged"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="input-icon">
                                                                <i class="fa fa-search"></i>
                                                                <asp:TextBox ID="txtPrinAccName" Enabled="false" Style="text-transform: uppercase" CssClass="form-control" PlaceHolder="Search Customer Name" OnTextChanged="txtPrinAccName_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                                               <%-- <div id="CustList6" style="height: 200px; overflow-y: scroll;"></div>
                                                                <asp:AutoCompleteExtender ID="PrinAutoAccName" runat="server" TargetControlID="txtPrinAccName" UseContextKey="true"
                                                                    CompletionInterval="1" CompletionSetCount="20" MinimumPrefixLength="1" EnableCaching="true"
                                                                    ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList6" ServiceMethod="GetAccName">
                                                                </asp:AutoCompleteExtender>--%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row" style="margin-top: 10px;">
                                                </div>

                                            </div>

                                        </div>
                                    </div>

                                    <div class="form-actions">
                                        <div class="row" style="margin-top: 1px;">
                                            <div class="col-md-offset-3 col-md-9">
                                                <asp:Button ID="btnCreate" runat="server" Text="Create" CssClass="btn blue" OnClick="btnCreate_Click" OnClientClick="Javascript:return IsValid();" />
                                                <asp:Button ID="btnAdd" runat="server" Text="Add Existing" CssClass="btn blue" OnClick="btnAdd_Click" OnClientClick="Javascript:return IsValid();" />
                                                <asp:Button ID="btnModify" runat="server" Text="Modify" CssClass="btn blue" OnClick="btnModify_Click" OnClientClick="Javascript:return IsValid();" />
                                                <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn blue" OnClick="btnDelete_Click" OnClientClick="Javascript:return IsValid();" />
                                                <asp:Button ID="btnAuthorise" runat="server" Text="Authorise" CssClass="btn blue" OnClick="btnAuthorise_Click" OnClientClick="Javascript:return IsValid();" />
                                                <asp:Button ID="btnClear" runat="server" Text="Clear All" CssClass="btn blue" OnClick="btnClear_Click" />
                                                <asp:Button ID="btnExit" runat="server" Text="Exit" CssClass="btn blue" OnClick="btnExit_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                            </div>
                            <div class="col-md-12">
                                <div class="table-scrollable">
                                    <table class="table table-striped table-bordered table-hover">
                                        <thead>
                                            <tr>
                                                <th>
                                                    <asp:GridView ID="grdInvAccMaster" runat="server" AllowPaging="True" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                        EditRowStyle-BackColor="#FFFF99" OnPageIndexChanging="grdInvAccMaster_PageIndexChanging" PagerStyle-CssClass="pgr" Width="100%">
                                                        <Columns>

                                                            <asp:TemplateField HeaderText="Select" Visible="true" runat="server">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName='<%# Eval("SubGLCode") %>' CommandArgument='<%#Eval("Id")%>' OnClick="lnkEdit_Click" class="glyphicon glyphicon-plus"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="SUBGLCode" Visible="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBankCode" runat="server" Text='<%# Eval("SubGLCOde") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Bank Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBankName" runat="server" Text='<%# Eval("BankName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="A/C No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBranchCode" runat="server" Text='<%# Eval("CustACCNO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="ReceiptNo" Visible="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblReceiptNo" runat="server" Text='<%# Eval("ReceiptNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblReceiptName" runat="server" Text='<%# Eval("ReceiptName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="OpeningDate">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOpeningDate" runat="server" Text='<%# Eval("OpeningDate") %>'></asp:Label>
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
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

