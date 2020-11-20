<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAVS5029.aspx.cs" Inherits="FrmAVS5029" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function FormatIt(obj) {

            if (obj.value.length == 2) // Day
                obj.value = obj.value + "/";
            if (obj.value.length == 5) // month 
                obj.value = obj.value + "/";
            if (obj.value.length == 11) // year 
            {
                alert("Please Enter valid Date");
                obj.value = "";
            }
        }
    </script>
    <script>
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;
            return true;
        }
    </script>
    <script type="text/javascript">
        function isvalidate() {


            var Custno = document.getElementById('<%=TxtCustno.ClientID%>').value;
            var empno = document.getElementById('<%=TxtEmpNo.ClientID%>').value;
            var divno = document.getElementById('<%=TxtDivNO.ClientID%>').value;
            var offno = document.getElementById('<%=TxtOffNo.ClientID%>').value;
            var desig = document.getElementById('<%=ddlDesig.ClientID%>').value;
            var doj = document.getElementById('<%=TxtDOJ.ClientID%>').value;
            var period = document.getElementById('<%=TxtRetPeriod.ClientID%>').value;
            var retiredt = document.getElementById('<%=TxtRetireDt.ClientID%>').value;
            var message = '';

            if (Custno == "") {
                message = 'Please Enter Customer Number....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtCustno.ClientID%>').focus();
                return false;
            }

            if (empno == "") {
                message = 'Please Enter Employee No....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtEmpNo.ClientID%>').focus();
                return false;
            }

            if (divno == "") {
                message = 'Please Enter Division Number....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtDivNO.ClientID%>').focus();
                return false;
            }

            if (offno == "") {
                message = 'Please Enter Office No....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtOffNo.ClientID%>').focus();
                return false;
            }
            if (desig == "0") {
                message = 'Please Select Designation....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=ddlDesig.ClientID%>').focus();
                return false;
            }
            if (doj == "DD/MM/YYYY") {
                message = 'Please Enter Date of Joining....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtDOJ.ClientID%>').focus();
                return false;
            }
            if (period == "") {
                message = 'Please Enter Period....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtRetPeriod.ClientID%>').focus();
                return false;
            }
            if (retiredt == "DD/MM/YYYY") {
                message = 'Please Enter Retirement Date....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtRetireDt.ClientID%>').focus();
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
                        Employer Details
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div class="portlet-body">
                                        <div class="tab-pane active" id="tab__blue">
                                            <ul class="nav nav-pills">
                                                <li class="pull-right">
                                                    <asp:Label ID="lblStst" runat="server" Text="Activity Perform :" Style="font-weight: bold;"></asp:Label>
                                                    <asp:Label ID="lblActivity" runat="server" Text=""></asp:Label>
                                                </li>
                                            </ul>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label">Customer No.<span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtCustno" runat="server" placeholder="Custno" onkeypress="javascript:return isNumber(event)" AutoPostBack="true" OnTextChanged="TxtCustno_TextChanged" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="TxtCustname" placeholder="Customer name" runat="server" AutoPostBack="true" OnTextChanged="TxtCustname_TextChanged" CssClass="form-control" TabIndex="2"></asp:TextBox>
                                                    <div id="Custlist1" style="height: 200px; overflow-y: scroll;"></div>
                                                    <asp:AutoCompleteExtender ID="AutoCompleteExtenderminor" runat="server" TargetControlID="TxtCustname"
                                                        UseContextKey="true"
                                                        CompletionInterval="1"
                                                        CompletionSetCount="20"
                                                        MinimumPrefixLength="1"
                                                        EnableCaching="true"
                                                        ServicePath="~/WebServices/Contact.asmx"
                                                        ServiceMethod="GetCustNames" CompletionListElementID="Custlist1">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label">CPF No.<span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtEmpNo" placeholder="CPF No" runat="server" CssClass="form-control" onkeypress="javascript:return isNumber(event)" TabIndex="3"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                    <div id="div_Main" runat="server" visible="false">
                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <label class="control-label">Division : <span class="required">*</span></label>
                                                    </div>
                                                    <%--  <div class="col-md-5">
                                                        <asp:DropDownList ID="ddlDivision" runat="server" TabIndex="4" CssClass="form-control" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                    </div>--%>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtDivNO" runat="server" onkeypress="javascript:return isNumber(event)" AutoPostBack="true" OnTextChanged="TxtDivNO_TextChanged" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="TxtDivName" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="TxtDivName_TextChanged"></asp:TextBox>
                                                        <div id="Div1" style="height: 200px; overflow-y: scroll;"></div>
                                                        <asp:AutoCompleteExtender ID="autodivname" runat="server" TargetControlID="TxtDivName" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                            MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" ServiceMethod="GetDivName" CompletionListElementID="Div1">
                                                        </asp:AutoCompleteExtender>
                                                    </div>
                                                     <div class="col-md-2">
                                                    <label class="control-label">SAP No.</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtSAPNo" placeholder="SAP No" runat="server" CssClass="form-control" MaxLength="25" TabIndex="3"></asp:TextBox>
                                                    <%--<asp:TextBox ID="txtSAPNo" placeholder="SAP No" runat="server" MaxLength="25" CssClass="form-control" onkeypress="javascript:return isNumber(event)" TabIndex="3"></asp:TextBox>--%>
                                                </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <label class="control-label">Office : <span class="required">*</span></label>
                                                    </div>
                                                    <%-- <div class="col-md-5">
                                                        <asp:DropDownList ID="ddlOffic" runat="server" TabIndex="5" CssClass="form-control" OnSelectedIndexChanged="ddlOffic_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                    </div>--%>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtOffNo" runat="server" onkeypress="javascript:return isNumber(event)" AutoPostBack="true" OnTextChanged="TxtOffNo_TextChanged" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="TxtOffName" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="TxtOffName_TextChanged"></asp:TextBox>
                                                        <div id="Div3" style="height: 200px; overflow-y: scroll;"></div>
                                                        <asp:AutoCompleteExtender ID="autooffcname" runat="server" TargetControlID="TxtOffName" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                            MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" ServiceMethod="GetOffcName" CompletionListElementID="Div3">
                                                        </asp:AutoCompleteExtender>
                                                    </div>
                                                     <div class="col-md-2">
                                                    <label class="control-label">Member Account Type<span class="required">*</span></label>
                                                </div>
                                               <div class="col-md-2">
                                                            <asp:DropDownList ID="ddlCustType" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCustType_SelectedIndexChanged" AutoPostBack="true"  TabIndex="14">
                                                            </asp:DropDownList>
                                                        </div>
                                                </div>
                                            </div>
                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <label class="control-label">Designation : <span class="required">*</span></label>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:DropDownList ID="ddlDesig" TabIndex="6" runat="server" AutoPostBack="true" CssClass="form-control" ToolTip="Lookupform1 1101"></asp:DropDownList>
                                                        <%--<asp:TextBox ID="TxtDesig" placeholder="Designation" TabIndex="6" runat="server" AutoPostBack="true" CssClass="form-control"></asp:TextBox>--%>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:Button ID="BtnAdDesig" runat="server" Text="Add Designation" CssClass="btn btn-secondary" OnClick="BtnAdDesig_Click" />
                                                    </div>
                                                    <div class="col-md-2">
                                                    <label class="control-label">BloodGroup<span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtBloodGrp" placeholder="Blood Group" runat="server" CssClass="form-control" TabIndex="3"></asp:TextBox>
                                                </div>
                                                </div>
                                            </div>


                                         <%--  <div id="div_Main" runat="server" visible="false">--%>
                                         <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label">Age<span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtAge" runat="server" placeholder="Age" onkeypress="javascript:return isNumber(event)" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                </div>
                                                <div class="col-md-1">
                                                    <label class="control-label" style="width:100px">Email Id<span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtEmailId" placeholder="Email Id" runat="server" CssClass="form-control" TabIndex="2"></asp:TextBox>
                                                 </div>
                                                <div class="col-md-2">
                                                    <label class="control-label">AdharCard No<span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtAdhar" placeholder="AdharCard No" runat="server" CssClass="form-control" TabIndex="3"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                               <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label">Member Mobile No<span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtmemno" runat="server" MaxLength="12" placeholder="Member Mobile No" onkeypress="javascript:return isNumber(event)" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                </div>
                                                <div class="col-md-1">
                                                    <label class="control-label" style="width:180px">Nominee Mob No<span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtNominee" placeholder="Nominee MobNo" MaxLength="10" runat="server" CssClass="form-control" TabIndex="2"></asp:TextBox>
                                                 </div>
                                              <div class="col-md-2">
                                                    <label class="control-label">Pancard No<span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtPancard" placeholder="PanCard No" MaxLength="10" runat="server" CssClass="form-control" TabIndex="2"></asp:TextBox>
                                                 </div>
                                            </div>
                                        </div>
                                      
                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <label class="control-label">Date Of Birth : <span class="required">*</span></label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtDOJ" onkeyup="FormatIt(this)" TabIndex="7" onkeypress="javascript:return isNumber(event)" placeholder="dd/mm/yyyy" CssClass="form-control" runat="server" OnTextChanged="TxtDOJ_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtDOJ">
                                                        </asp:TextBoxWatermarkExtender>
                                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtDOJ">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                    <label class="control-label col-md-3">Retirement Period(Yr) : <span class="required">*</span></label>
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="TxtRetPeriod" placeholder="Period" TabIndex="8" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server" OnTextChanged="TxtRetPeriod_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label class="control-label">Date Of Retirement : <span class="required">*</span></label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtRetireDt" onkeyup="FormatIt(this)" TabIndex="9" onkeypress="javascript:return isNumber(event)" placeholder="dd/mm/yyyy" CssClass="form-control" runat="server" OnTextChanged="TxtRetireDt_TextChanged" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtRetireDt">
                                                        </asp:TextBoxWatermarkExtender>
                                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtRetireDt">
                                                        </asp:CalendarExtender>
                                                    </div>

                                                </div>
                                            </div>
                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <label class="control-label">Date Of Member : </label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtConfDt" onkeyup="FormatIt(this)" TabIndex="10" onkeypress="javascript:return isNumber(event)" placeholder="dd/mm/yyyy" CssClass="form-control" runat="server"></asp:TextBox>
                                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtConfDt">
                                                        </asp:TextBoxWatermarkExtender>
                                                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtConfDt">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Bank Details : </strong></div>
                                                </div>
                                            </div>

                                            <div id="divInsert" runat="server">
                                                <asp:GridView ID="grdInsert" runat="server" AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr.No">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtSrNo" Enable="false" CssClass="form-control" runat="server" Text='<%#Container.DataItemIndex+1 %>' Width="50px" Enabled="false" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Bank Name">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="TxtBnkName" CssClass="form-control" runat="server" Style="text-transform: uppercase" placeholder="Bank Name" Width="450px" Text='<%#Eval("bankname") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Branch Name">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="TxtBrnchName" CssClass="form-control" runat="server" Style="text-transform: uppercase" placeholder="Branch Name" Text='<%#Eval("branchname") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Account No.">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="TxtAccNo" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber(event)" placeholder="Account No" MaxLength="16" Text='<%#Eval("Accno") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="IFSC Code">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="TxtIFSC" CssClass="form-control" runat="server" Style="text-transform: uppercase" placeholder="IFSC Code" Text='<%#Eval("IFSCCode") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                </asp:GridView>
                                                <asp:Button ID="btnAddNewRow" CssClass="btn btn-primary" runat="server" Text="Add New Row" OnClick="btnAddNewRow_Click" />
                                            </div>

                                        </div>

                                        <div id="divSuretyStart" runat="server" visible="false">
                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                <div class="col-lg-12">
                                                    <div class="col-md-2">
                                                        <label class="control-label">Rec Start/Stop : <span class="required">*</span></label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:DropDownList ID="ddlOption" CssClass="form-control" runat="server">
                                                            <asp:ListItem Text="Start" Value="1" />
                                                            <asp:ListItem Text="Stop" Value="2" />
                                                        </asp:DropDownList>
                                                    </div>
                                                    <label class="control-label col-md-2">Principle : </label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtPAmt" onkeypress="javascript:return isNumber (event)" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <label class="control-label col-md-2">Interest : </label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtIntAmt" onkeypress="javascript:return isNumber (event)" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-12">
                                        <asp:Button ID="BtnAddNew" runat="server" Text="Add new" CssClass="btn blue" OnClick="BtnAddNew_Click" TabIndex="15" />
                                        <asp:Button ID="BtnAddNew_1" runat="server" Text="Add Oth Recovery" CssClass="btn blue" OnClick="BtnAddNew_1_Click" TabIndex="16" />
                                        <asp:Button ID="Submit" runat="server" Text="Submit" CssClass="btn blue" OnClick="Submit_Click" OnClientClick="Javascript:return isvalidate();" TabIndex="17" Visible="false" />
                                        <asp:Button ID="BtnModify" runat="server" Text="Modify" CssClass="btn blue" OnClick="BtnModify_Click" OnClientClick="Javascript:return isvalidate();" TabIndex="18" Visible="false" />
                                        <asp:Button ID="BtnDelete" runat="server" Text="Delete" CssClass="btn blue" OnClick="BtnDelete_Click" TabIndex="19" Visible="false" />
                                        <asp:Button ID="BtnAuthorise" runat="server" Text="Authorise" CssClass="btn blue" OnClick="BtnAuthorise_Click" TabIndex="20" Visible="false" />
                                        <asp:Button ID="Clear" runat="server" Text="Clear All" CssClass="btn blue" OnClick="Clear_Click" TabIndex="21" Visible="false" />
                                        <asp:Button ID="Exit" runat="server" Text="Close" CssClass="btn blue" OnClick="Exit_Click" TabIndex="22" Visible="false" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="row" runat="server" id="div_GridVw">
        <div class="col-lg-12">
            <div class="table-scrollable" style="border: none">
                <table class="table table-striped table-bordered table-hover">
                    <thead>
                        <tr>
                            <th>
                                <asp:GridView ID="GrdEmpDetails" runat="server" AllowPaging="True"
                                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                    EditRowStyle-BackColor="#FFFF99"
                                    PageIndex="10" PageSize="25"
                                    PagerStyle-CssClass="pgr" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Brcd">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBRCD" runat="server" Text='<%# Eval("BRCD") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CustNo">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCUSTNO" runat="server" Text='<%# Eval("CUSTNO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Division">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDiv" runat="server" Text='<%# Eval("DIV") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Office">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOffc" runat="server" Text='<%# Eval("OFFC") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDESIGNATION" runat="server" Text='<%# Eval("DESIGNATION") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Date Of Birth">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDOB" runat="server" Text='<%# Eval("DOB") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Retirement Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDOR" runat="server" Text='<%# Eval("DOR","{0:dd/MM/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Modify">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkSelect" runat="server" CommandArgument='<%#Eval("ID")%>' CommandName="select" OnClick="lnkSelect_Click" class="glyphicon glyphicon-edit"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Eval("ID")%>' CommandName="select" OnClick="lnkDelete_Click" class="glyphicon glyphicon-trash"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Authorize">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkAuthorize" runat="server" CommandArgument='<%#Eval("ID")%>' CommandName="select" OnClick="lnkAuthorize_Click" class="glyphicon glyphicon-check"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="View">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkView11" runat="server" CommandArgument='<%#Eval("ID")%>' onclick="lnkView11_Click" CommandName="select" class="glyphicon glyphicon-check"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Center" />
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

    <div class="row" id="divSurety" runat="server" visible="false">
        <div class="col-md-12">
            <asp:Label ID="lblSurety" runat="server" Text="From Surety : " BackColor="#66ccff" Font-Bold="true" Font-Size="Medium"></asp:Label>
            <div class="table-scrollable" style="height: auto; max-height: 300px; overflow-x: auto; overflow-y: auto">
                <table class="table table-striped table-bordered table-hover" width="100%">
                    <thead>
                        <tr>
                            <th>
                                <asp:GridView ID="GrdFromSurity" runat="server" AllowPaging="True" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                    EditRowStyle-BackColor="#FFFF99" PagerStyle-CssClass="pgr" Width="100%">
                                    <HeaderStyle BackColor="#66ccff" Font-Bold="true" Font-Size="Medium"></HeaderStyle>
                                    <Columns>

                                        <asp:TemplateField HeaderText="SrNo" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSrNo1" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Prod Code" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPRODCODE" runat="server" Text='<%# Eval("LOANGLCODE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Accno" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLOANACCNO" runat="server" Text='<%# Eval("LOANACCNO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Custno" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCUSTNO" runat="server" Text='<%# Eval("MemberNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Account Holder Name" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCUSTNAME" runat="server" Text='<%# Eval("CUSTNAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Loan Amount" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLIMIT" runat="server" Text='<%# Eval("LIMIT") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Loan Date" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSANSSIONDATE" runat="server" Text='<%# Eval("SANSSIONDATE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Closing Date" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCLOSINGDATE" runat="server" Text='<%# Eval("CLOSINGDATE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Balance" Visible="false" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBALANCE" runat="server" Text='<%# Eval("BALANCE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="OverDue Amount" Visible="false" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOVERDUEDATE" runat="server" Text='<%# Eval("OVERDUE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="View">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkView" runat="server" CommandArgument='<%#Eval("LOANGLCODE")+"-"+Eval("LOANACCNO")+"-"+Eval("MemberNo")%>' CommandName="select" OnClick="lnkView_Click" class="glyphicon glyphicon-edit"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle CssClass="pgr" />
                                    <HeaderStyle BackColor="#ffce9d" />
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

    <div id="ADDESIGNATION" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Add Designation Screen</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="portlet box green" id="Div2">
                                <div class="portlet-title">
                                    <div class="caption">
                                        Add Designation
                                    </div>
                                </div>
                                <div class="portlet-body form">
                                    <div class="form-horizontal">
                                        <div class="form-wizard">
                                            <div class="form-body">
                                                <div class="tab-content">
                                                    <div class="tab-pane active" id="tab1">
                                                        <div class="row" style="margin-bottom: 10px;">
                                                            <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                                <div class="col-lg-12">

                                                                    <div class="col-md-4">
                                                                        <label class="control-label">Add Designation : <span class="required">*</span></label>
                                                                    </div>
                                                                    <div class="col-md-7">
                                                                        <asp:TextBox ID="TxtAddDesig" Placeholder="Add Designation" runat="server" CssClass="form-control" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-actions">
                                                    <div class="row">
                                                        <div class="col-md-offset-3 col-md-12">
                                                            <asp:Button ID="Btn_Save" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="Btn_Save_Click" />
                                                            <asp:Button ID="Btn_Exit" runat="server" Text="Exit" CssClass="btn btn-primary" data-dismiss="modal" />
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

