<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAVS5033.aspx.cs" Inherits="FrmAVS5033" %>

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
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        Investment Data Entry
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">


                                    <div style="border: 1px solid #3598dc">
                                        <div class="row" style="margin-top: 10px">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label">Investment Type <span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="ddlInvestment" runat="server" CssClass="form-control"></asp:DropDownList>
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
                                                        <asp:TextBox ID="txtBankNo" placeholder="Product Code" OnTextChanged="txtBankNo_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
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
                                                        <asp:TextBox ID="txtAC" placeholder="Account No" CssClass="form-control" runat="server"  onkeypress="javascript:return isNumber (event)" OnTextChanged="txtAC_TextChanged" AutoPostBack="true"/>
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

                                        <div class="row" style="margin-top: 10px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label">Receipt No <span class="required">*</span></label>
                                                </div>
                                                <div class="col-lg-2">
                                                    <asp:TextBox ID="txtReceiptNo" onkeypress="javascript:return isNumber (event)" placeholder="Receipt Number" CssClass="form-control" runat="server" />
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
                                                    <asp:TextBox ID="txtBResNo" onkeypress="javascript:return isNumber (event)" placeholder="Board Resolution No" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label">Board Meeting Date </label>
                                                </div>
                                                <div class="col-lg-2">
                                                    <asp:TextBox ID="txtBMeetDate" onkeyup="FormatIt(this);" onkeypress="javascript:return isNumber(event)" placeholder="Board Meeting Date" CssClass="form-control" runat="server" />
                                                    <asp:CalendarExtender ID="txtBMeetDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtBMeetDate">
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
                                                 <div class="col-md-2">
                                                    <label class="control-label">Closing Bal <span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtClBal" CssClass="form-control" runat="server"></asp:TextBox>
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
                                                    <asp:TextBox ID="txtIntProdCode" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtIntProdCode_TextChanged" PlaceHolder="Product Code"></asp:TextBox>
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
                                                    <asp:TextBox ID="txtPrinProdCode" CssClass="form-control" onkeypress="javascript:return isNumber (event)" runat="server" AutoPostBack="true" OnTextChanged="txtPrinProdCode_TextChanged" PlaceHolder="Product Code"></asp:TextBox>
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
                                                    <asp:TextBox ID="txtPrinAccNo" CssClass="form-control" onkeypress="javascript:return isNumber (event)" PlaceHolder="Account Number" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="input-icon">
                                                        <i class="fa fa-search"></i>
                                                        <asp:TextBox ID="txtPrinAccName" Style="text-transform: uppercase" CssClass="form-control" PlaceHolder="Search Customer Name" runat="server"></asp:TextBox>
                                                        <%-- <div id="CustList6" style="height: 200px; overflow-y: scroll;"></div>
                                                                <asp:AutoCompleteExtender ID="PrinAutoAccName" runat="server" TargetControlID="txtPrinAccName" UseContextKey="true"
                                                                    CompletionInterval="1" CompletionSetCount="20" MinimumPrefixLength="1" EnableCaching="true"
                                                                    ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList6" ServiceMethod="GetAccName">
                                                                </asp:AutoCompleteExtender>--%>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 5px;">
                                            <div class="col-lg-12">
                                                <%-- <div class="col-md-2">//Dhanya Shetty //01/07/2017
                                            <label class="control-label">Account Type : <span class="required">*</span></label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="ddlAccType" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>--%>
                                                <div class="col-md-2">
                                                    <label class="control-label">As Of Date : <span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtDepDate" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" PlaceHolder="DD/MM/YYYY" runat="server"></asp:TextBox>
                                                    <asp:CalendarExtender ID="TxtDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtDepDate">
                                                    </asp:CalendarExtender>
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label">Interest Payout : <span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="ddlIntrestPay" onkeypress="javascript:return isNumber (event)" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 5px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label">Deposit Amount : <span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtDepAmt" CssClass="form-control" PlaceHolder="Deposit Amount" runat="server" onkeypress="javascript:return isNumber (event)" AutoPostBack="true" OnTextChanged="txtDepAmt_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-md-1">
                                                    <label class="control-label">Period : <span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="ddlDuration" runat="server" CssClass="form-control">
                                                        <asp:ListItem Value="M">Months</asp:ListItem>
                                                        <asp:ListItem Value="D">Days</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="txtPeriod" CssClass="form-control" onkeypress="javascript:return isNumber (event)" PlaceHolder="Period" runat="server" AutoPostBack="true" OnTextChanged="txtPeriod_TextChanged" Style="width: 77px;"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label">Interest Rate : <span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtIntRate" CssClass="form-control" runat="server" AutoPostBack="true" PlaceHolder="Interest Rate" OnTextChanged="txtIntRate_TextChanged"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin-top: 5px;">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label">Interest Amount : <span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtIntAmt" CssClass="form-control" onkeypress="javascript:return isNumber (event)" PlaceHolder="Interest Amount" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label">Maturity Amount : <span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtMaturityAmt" CssClass="form-control" onkeypress="javascript:return isNumber (event)" PlaceHolder="Maturity Amount" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label">Due Date : <span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtDueDate" CssClass="form-control" onkeypress="javascript:return isNumber (event)" PlaceHolder="DD/MM/YYYY" runat="server"  onkeyup="FormatIt(this)"></asp:TextBox>
                                                    <asp:CalendarExtender ID="CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtDueDate">
                                                    </asp:CalendarExtender>
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
                                        <asp:Button ID="btnCreate" runat="server" Text="Submit" CssClass="btn blue" OnClick="btnCreate_Click" OnClientClick="Javascript:return IsValid();" />
                                        <asp:Button ID="btnModify" runat="server" Text="Modify" CssClass="btn blue" OnClick="btnModify_Click" OnClientClick="Javascript:return IsValid();" Visible="false" />
                                          <asp:Button ID="BtnClear" runat="server" Text="Clear" CssClass="btn blue" OnClick="BtnClear_Click"/>
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
                                                EditRowStyle-BackColor="#FFFF99" PagerStyle-CssClass="pgr" Width="100%" OnPageIndexChanging="grdInvAccMaster_PageIndexChanging">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="Select" Visible="true" runat="server">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName='<%# Eval("SubGLCode") %>' CommandArgument='<%#Eval("Id")%>' OnClick="lnkEdit_Click2" class="glyphicon glyphicon-plus"></asp:LinkButton>
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
                     <div class="col-md-12">
                        <div class="table-scrollable">
                            <table class="table table-striped table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th>
                                            <asp:GridView ID="grdGlname" runat="server" AllowPaging="True" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                EditRowStyle-BackColor="#FFFF99" PagerStyle-CssClass="pgr" Width="100%" OnPageIndexChanging="grdGlname_PageIndexChanging">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="Select" Visible="true" runat="server">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName='<%# Eval("SUBGLCODE")+"-"+Eval("GLNAME") %>' CommandArgument='<%#Eval("SUBGLCODE")+"-"+Eval("GLNAME")%>' OnClick="lnkEdit_Click1" class="glyphicon glyphicon-plus"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="SUBGLCode">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBankCode" runat="server" Text='<%# Eval("SUBGLCODE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Bank Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBankName" runat="server" Text='<%# Eval("GLNAME") %>'></asp:Label>
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
</asp:Content>

