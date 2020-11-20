<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmCustomerDetails.aspx.cs" Inherits="FrmCustomerDetails" %>

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

        <%-- Only alphabet --%>
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

        <%-- Only Numbers --%>
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;
            return true;
        }
    </script>
       <script type="text/javascript">
           function disbl() {
               document.getElementById("<%=BtnReport.ClientID%>").disabled = true;
           }
          
    </script>
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        Customer Details
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div style="border: 1px solid #3598dc">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Member's Details : </strong></div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                        <div class="col-md-12">
                                            <div class="col-md-1">
                                                <label>Cust No :<span class="required"> *</span></label>

                                            </div>
                                            <div class="col-md-1">
                                                <asp:TextBox ID="txtCustNo" Placeholder="Cust No" onkeypress="javascript:return isNumber (event)" runat="server" OnTextChanged="txtCustNo_TextChanged" AutoPostBack="true" Style="width: 70px;" TabIndex="1"></asp:TextBox>

                                            </div>
                                                 <div class="col-md-3">
                                                <asp:TextBox ID="txtCustName" Placeholder="Customer Name" OnTextChanged="txtCustName_TextChanged" AutoPostBack="true" runat="server" Style="width: 300px;" />
                                                <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="txtCustName"
                                                    UseContextKey="true"
                                                    CompletionInterval="1"
                                                    CompletionSetCount="20"
                                                    MinimumPrefixLength="1"
                                                    EnableCaching="true"
                                                    ServicePath="~/WebServices/Contact.asmx"
                                                    ServiceMethod="GetCustNamesBrcd">
                                                </asp:AutoCompleteExtender>
                                            </div>
                                          
                                            <div class="col-md-1">
                                                <label>Old Cust No :</label>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:TextBox ID="txtOldCustNo" runat="server" Style="width: 90px;" OnTextChanged="txtOldCustNo_TextChanged" AutoPostBack="true"/>
                                            </div>
                                            
                                            <div class="col-md-1">
                                                <label>DOB :</label>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:TextBox ID="txtBOD" runat="server" Style="width: 90px;" />
                                            </div>
                                              <div class="col-md-1">
                                                <label>Member Date:</label>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:TextBox ID="txtOpenDate" runat="server" Style="width: 90px;" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                        <div class="col-md-12">
                                            <div class="col-md-1">
                                                <label>Mem No :<span class="required"> *</span></label>

                                            </div>
                                            <div class="col-md-1">
                                                <asp:TextBox ID="Txtmemno" Placeholder="Mem No" onkeypress="javascript:return isNumber (event)" runat="server" OnTextChanged="Txtmemno_TextChanged" AutoPostBack="true" Style="width: 70px" TabIndex="2" ></asp:TextBox>

                                            </div>
                                       


                                              <div class="col-md-3">
                                                <asp:TextBox ID="TxtMemname" Placeholder="Member Name" OnTextChanged="TxtMemname_TextChanged" AutoPostBack="true" runat="server" Style="width: 300px;" />
                                                <asp:AutoCompleteExtender ID="automemname" runat="server" TargetControlID="TxtMemname"
                                                    UseContextKey="true"
                                                    CompletionInterval="1"
                                                    CompletionSetCount="20"
                                                    MinimumPrefixLength="1"
                                                    EnableCaching="true"
                                                    ServicePath="~/WebServices/Contact.asmx"
                                                    ServiceMethod="GetMemNamesdash">
                                                </asp:AutoCompleteExtender>
                                            </div>
                                          
                                             <div class="col-md-1">
                                                <label>Pan Card No :</label>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:TextBox ID="TxtPan" runat="server" Style="width: 90px;" />
                                            </div>
                                             <div class="col-md-1">
                                                <label>Mobile No :</label>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:TextBox ID="TxtMob" runat="server" Style="width: 90px;" />
                                            </div>
                                              
                                                   <div id="DivDep" runat="server" visible="false">
                                                  
                                            <div class="col-md-1">
                                                    <label>CPF No:</label>
                                                </div>
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="TxtCPFNo" runat="server" Style="width: 90px;" />
                                                </div>
                                            </div>
                                                  </div>
                                        </div>
                                    </div>
                                
                                      <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                        <div class="col-md-12">
                                            <div class="col-md-1">
                                                <label>Permanent Add:</label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtAddress1" runat="server" Style="width: 300px;" TextMode="MultiLine" />
                                            </div>
                                            <div class="col-md-1">
                                                <label class="control-label ">Present Add:</label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtAddress2" runat="server" Style="width: 300px;" TextMode="MultiLine" />
                                            </div>
                                               <div class="col-md-1">
                                                <label>Office Add:</label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="TxtOffcAddr" runat="server" Style="width: 300px;" TextMode="MultiLine" />
                                            </div>
                                        </div>
                                    </div>

                                 

                                    <div id="div_schl" runat="server" visible="false">
                                     <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                        <div class="col-md-12">
                                         <div class="col-md-1">
                                                <label>Div Name:</label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="TxtDivName" runat="server" Style="width: 300px;" />
                                            </div>
                                             <div class="col-md-1">
                                                    <label>Dep Name:</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtSchlNme" runat="server" Style="width: 300px;" />
                                                </div>
                                                 <div class="col-md-1">
                                                    <label>DOR:</label>
                                                </div>
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="TxtDOR" runat="server" Style="width: 90px;" />
                                                </div>
                                                <div class="col-md-1">
                                                    <label>Ret Period:</label>
                                                </div>
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="TxtRetPeriod" runat="server" Style="width: 60px;" />
                                                </div>
                                            </div>
                                           
                                        </div>
                                    </div>
                                 
                              
                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-md-offset-1 col-md-12">
                                             <asp:Button ID="BtnShwClsAcc" runat="server" CssClass="btn btn-primary" Text="Show Closed A/C's" OnClick="BtnShwClsAcc_Click"/>
                                            <asp:Button ID="BtnReport" runat="server" CssClass="btn btn-primary" Text="Report" OnClick="BtnReport_Click"/>
                                            <asp:Button ID="LoanApp" runat="server" CssClass="btn btn-primary" Text="LoanApplication" OnClick="LoanApp_Click" Visible="false" />
                                            <asp:Button ID="BtnMemPassbk" runat="server" CssClass="btn btn-primary" Text="Member Passbook" OnClick="BtnMemPassbk_Click" Visible="false" />
                                            <asp:Button ID="BtnRetireVouch" runat="server" CssClass="btn btn-primary" Text="Retirement Voucher" OnClick="BtnRetireVouch_Click" Visible="false" />
                                            <asp:Button ID="BtnCustTransfer" runat="server" CssClass="btn btn-primary" Text="Customer Transfer" OnClick="BtnCustTransfer_Click" Visible="false" />
                                        </div>
                                    </div>
                                </div>

                                                                <div style="border: 1px solid #3598dc">
                                    <div>
                                        <div class="col-md-12"></div>
                                        <label></label>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="row">
                                                <div class="col-lg-3 col-md-6">
                                                    <div class="panel panel-green">
                                                        <div class="panel-heading">
                                                            <div class="row">
                                                                <div class="col-xs-12 text-right">
                                                                    <div class="huge">
                                                                        <asp:Label ID="lblLoans" runat="server" />
                                                                    </div>
                                                                    <div>Total Loans</div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                     
                                                    </div>
                                                </div>
                                                <div class="col-lg-3 col-md-6">
                                                    <div class="panel panel-purple">
                                                        <div class="panel-heading">
                                                            <div class="row">
                                                                <div class="col-xs-12 text-right">
                                                                    <div class="huge">
                                                                        <asp:Label ID="lblDeposite" runat="server" />
                                                                    </div>
                                                                    <div>Term Deposits</div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                     
                                                    </div>
                                                </div>
                                                <div class="col-lg-3 col-md-6">
                                                    <div class="panel panel-slateblue">
                                                        <div class="panel-heading">
                                                            <div class="row">
                                                                <div class="col-xs-12 text-right">
                                                                    <div class="huge">
                                                                        <asp:Label ID="lblShares" runat="server" />
                                                                    </div>
                                                                    <div>Share Capital</div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    
                                                    </div>
                                                </div>
                                                <div class="col-lg-3 col-md-6">
                                                    <div class="panel panel-voilet">
                                                        <div class="panel-heading">
                                                            <div class="row">
                                                                <div class="col-xs-12 text-right">
                                                                    <div class="huge">
                                                                        <asp:Label ID="lblCASADep" runat="server" />
                                                                    </div>
                                                                    <div>CASA Deposits</div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div style="border: 1px solid #3598dc">
                                    <div class="portlet-body">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Account Details : </strong></div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <asp:GridView ID="grdAccDetails" runat="server" AllowPaging="True" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                    EditRowStyle-BackColor="#FFFF99" OnPageIndexChanging="grdAccDetails_PageIndexChanging" OnRowDataBound="grdAccDetails_RowDataBound"
                                                    PagerStyle-CssClass="pgr" Width="100%">
                                                    <Columns>

                                                        <asp:TemplateField HeaderText="SrNo" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSrNo1" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                         <asp:TemplateField HeaderText="Brcd" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblBrcdA" runat="server" Text='<%# Eval("Brcd") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="GL Code" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblGLCODE" runat="server" Text='<%# Eval("GLCODE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Prod Code" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSUBGLCODE" runat="server" Text='<%# Eval("SUBGLCODE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Product Name" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblGLNAME" runat="server" Text='<%# Eval("GLNAME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="A/C No" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblACCNO" runat="server" Text='<%# Eval("ACCNO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Remark" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCUSTNAME" runat="server" Text='<%# Eval("CUSTNAME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="A/C Status" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAcStatus" runat="server" Text='<%# Eval("ACC_STATUS") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Balance" Visible="true" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBALANCE" runat="server" Text='<%# Eval("BALANCE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        
                                                        <asp:TemplateField HeaderText="DrCr" Visible="true" ItemStyle-HorizontalAlign="center" FooterStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDrCr" runat="server" Text='<%# Eval("DrCr") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Opening Date" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOPDT" runat="server" Text='<%# Eval("OPENINGDATE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Due Date" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDUEDT" runat="server" Text='<%# Eval("DUEDATE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Principle Amt" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPRNAMT" runat="server" Text='<%# Eval("PRNAMT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Interest" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblINT" runat="server" Text='<%# Eval("RATEOFINT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Close">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkClose" runat="server" CommandArgument='<%#Eval("GLCODE")+"-"+Eval("SUBGLCODE")+"-"+Eval("ACCNO")%>' CommandName="select" OnClick="lnkClose_Click" class="glyphicon glyphicon-check"></asp:LinkButton>
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
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div style="border: 1px solid #3598dc">
                                    <div class="portlet-body">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Direct Liabilities : </strong></div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <asp:GridView ID="GrdDirectLiab" runat="server" AllowPaging="True" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                    EditRowStyle-BackColor="#FFFF99" OnPageIndexChanging="GrdDirectLiab_PageIndexChanging" OnRowDataBound="GrdDirectLiab_RowDataBound"
                                                    PagerStyle-CssClass="pgr" Width="100%" ShowFooter="true">
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
                                                                <asp:Label ID="lblsancamt" runat="server" Text='<%# Eval("LIMIT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <div style="padding: 0 0 5px 0">
                                                                    <asp:Label ID="Lbl_TotalLimit" runat="server" />
                                                                </div>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Installment Amt" Visible="true" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblInstAmt" runat="server" Text='<%# Eval("INSTALLMENT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <div style="padding: 0 0 5px 0">
                                                                    <asp:Label ID="Lbl_TotaliNSTALL" runat="server" />
                                                                </div>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Outstanding bal" Visible="true" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbloutbal" runat="server" Text='<%# Eval("BALANCE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <div style="padding: 0 0 5px 0">
                                                                    <asp:Label ID="Lbl_TotalBal" runat="server" />
                                                                </div>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        
                                                        <asp:TemplateField HeaderText="DrCr" Visible="true" ItemStyle-HorizontalAlign="center" FooterStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDrCr_1" runat="server" Text='<%# Eval("DrCr") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="OD Amt" Visible="true" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblodamt" runat="server" Text='<%# Eval("ODAMT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <div style="padding: 0 0 5px 0">
                                                                    <asp:Label ID="Lbl_TotalOD" runat="server" />
                                                                </div>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="No of Inst" Visible="true" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblnoofinst" runat="server" Text='<%# Eval("TOTALINST") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSts" runat="server" Text='<%# Eval("STATUS") %>'></asp:Label>
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
                                <div style="border: 1px solid #3598dc">
                                    <div class="portlet-body">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">From Surety : </strong></div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <asp:GridView ID="GrdFromSurity" runat="server" AllowPaging="True" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                    EditRowStyle-BackColor="#FFFF99" OnPageIndexChanging="GrdFromSurity_PageIndexChanging"
                                                    PagerStyle-CssClass="pgr" Width="100%">
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
                                                                <asp:LinkButton ID="lnkView" runat="server" CommandArgument='<%#Eval("MemberNo")%>' CommandName="select" OnClick="lnkView_Click" class="glyphicon glyphicon-check"></asp:LinkButton>
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
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div style="border: 1px solid #3598dc">
                                    <div class="portlet-body">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">InDirect Liabilities-To Surety : </strong></div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <asp:GridView ID="GrdInDirectLiab" runat="server" AllowPaging="True" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                    EditRowStyle-BackColor="#FFFF99" OnPageIndexChanging="GrdInDirectLiab_PageIndexChanging" OnRowDataBound="GrdInDirectLiab_RowDataBound"
                                                    PagerStyle-CssClass="pgr" Width="100%" ShowFooter="true">
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
                                <%--     <div style="border: 1px solid #3598dc">
                                    <div class="portlet-body">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Dividend Payable : </strong></div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <asp:GridView ID="GrdDividend" runat="server" AllowPaging="True" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                    EditRowStyle-BackColor="#FFFF99" OnPageIndexChanging="GrdDividend_PageIndexChanging"
                                                    PagerStyle-CssClass="pgr" Width="100%" ShowFooter="true">
                                                    <Columns>

                                                        <asp:TemplateField HeaderText="SrNo" Visible="true" FooterText="Sub Total" FooterStyle-Font-Bold="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSrNo1" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Brcd" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBrcd" runat="server" Text='<%# Eval("LOANGLCODE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Prod Code" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBrcd" runat="server" Text='<%# Eval("LOANGLCODE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Accno" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAccno" runat="server" Text='<%# Eval("LOANACCNO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Opening Dt" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOPdt" runat="server" Text='<%# Eval("CUSTNAME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Due Dt" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblduedt" runat="server" Text='<%# Eval("LIMIT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Deposit_Amt" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblsancamt" runat="server" Text='<%# Eval("SANSSIONDATE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Balance" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbloutbal" runat="server" Text='<%# Eval("BALANCE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="OD Amt" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblodamt" runat="server" Text='<%# Eval("OVERDUE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                    <PagerStyle CssClass="pgr" />
                                                    <HeaderStyle BackColor="#67ce00" />
                                                    <FooterStyle BackColor="#00bbf9" />
                                                    <SelectedRowStyle BackColor="#66FF99" />
                                                    <EditRowStyle BackColor="#FFFF99" />
                                                    <AlternatingRowStyle CssClass="alt" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>--%>
                                <%--   <div style="border: 1px solid #3598dc">
                                    <div class="portlet-body">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Other Account Details : </strong></div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <asp:GridView ID="GrdOtherAccDetails" runat="server" AllowPaging="True" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                    EditRowStyle-BackColor="#FFFF99" OnPageIndexChanging="GrdOtherAccDetails_PageIndexChanging" OnRowDataBound="GrdOtherAccDetails_RowDataBound"
                                                    PagerStyle-CssClass="pgr" Width="100%" ShowFooter="true">
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
                                                                <asp:Label ID="lblSubgl" runat="server" Text='<%# Eval("SUBGLCODE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Accno" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAccno" runat="server" Text='<%# Eval("ACCNO") %>'></asp:Label>
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

                                                        <asp:TemplateField HeaderText="Deposit_Amt" Visible="true" ItemStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblsancamt" runat="server" Text='<%# Eval("PRNAMT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <div style="padding: 0 0 5px 0">
                                                                    <asp:Label ID="Lbl_TotalBalDAmt" runat="server" />
                                                                </div>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Balance" Visible="true" ItemStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblbal" runat="server" Text='<%# Eval("BALANCE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                             <FooterTemplate>
                                                                <div style="padding: 0 0 5px 0">
                                                                    <asp:Label ID="Lbl_TotalBalBl" runat="server" />
                                                                </div>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                    <PagerStyle CssClass="pgr" />
                                                    <HeaderStyle BackColor="#67ce00" />
                                                    <FooterStyle BackColor="#00bbf9" />
                                                    <SelectedRowStyle BackColor="#66FF99" />
                                                    <EditRowStyle BackColor="#FFFF99" />
                                                    <AlternatingRowStyle CssClass="alt" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>--%>



                                <%-- <div style="border: 1px solid #3598dc">
                                    <div class="portlet-body">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Loan Details : </strong></div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <asp:GridView ID="grdLoan" runat="server" AllowPaging="True" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                    EditRowStyle-BackColor="#FFFF99" OnPageIndexChanging="grdLoan_PageIndexChanging"
                                                    PagerStyle-CssClass="pgr" Width="100%">
                                                    <Columns>

                                                        <asp:TemplateField HeaderText="SrNo" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSrNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Loan Type" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLoanType" runat="server" Text='<%# Eval("GLNAME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Loan Date" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLoanDate" runat="server" Text='<%# Eval("OPENINGDATE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Bond No" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLoanBondNo" runat="server" Text='<%# Eval("BONDNO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Sanc. Amt" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSancAmt" runat="server" Text='<%# Eval("LIMIT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Balance" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLoanBalance" runat="server" Text='<%# Eval("BALANCE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Rate" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLoanrate" runat="server" Text='<%# Eval("INTRATE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Installment" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLoanInst" runat="server" Text='<%# Eval("INSTALLMENT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Total Inst." Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLoanTotalInst" runat="server" Text='<%# Eval("TOTALINST") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="To Be Paid" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLoanToBePaid" runat="server" Text='<%# Eval("TOBEPAID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="No Of Inst." Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLoanNoOfInst" runat="server" Text='<%# Eval("PERIOD") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="30%" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLoanPerc" runat="server" Text='<%# Eval("PERCENTAGE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Due date" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLoanDueDate" runat="server" Text='<%# Eval("DUEDATE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                    <PagerStyle CssClass="pgr" />
                                                    <SelectedRowStyle BackColor="#66FF99" />
                                                    <EditRowStyle BackColor="#FFFF99" />
                                                    <AlternatingRowStyle CssClass="alt" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div style="border: 1px solid #3598dc">
                                    <div class="portlet-body">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Deposite Details : </strong></div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <asp:GridView ID="grdDeposite" runat="server" AllowPaging="True" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                    EditRowStyle-BackColor="#FFFF99" OnPageIndexChanging="grdDeposite_PageIndexChanging"
                                                    PagerStyle-CssClass="pgr" Width="100%">
                                                    <Columns>

                                                        <asp:TemplateField HeaderText="SrNo" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSrNo1" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Deposite Type" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDepType" runat="server" Text='<%# Eval("GLNAME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Acc No" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDepAccNo" runat="server" Text='<%# Eval("ACCNO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Deposite date" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDepdate" runat="server" Text='<%# Eval("OPENINGDATE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Principle" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDepPrinciple" runat="server" Text='<%# Eval("PRNAMT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Rate" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDeprate" runat="server" Text='<%# Eval("RATEOFINT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Period" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDepPeriod" runat="server" Text='<%# Eval("PERIOD") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Interest" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDepInterest" runat="server" Text='<%# Eval("INTAMT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Maturity Date" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDepMatDate" runat="server" Text='<%# Eval("DUEDATE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Maturity Amt" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDepMatAmt" runat="server" Text='<%# Eval("MATURITYAMT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Balance" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDepBalance" runat="server" Text='<%# Eval("BALANCE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                    <PagerStyle CssClass="pgr" />
                                                    <SelectedRowStyle BackColor="#66FF99" />
                                                    <EditRowStyle BackColor="#FFFF99" />
                                                    <AlternatingRowStyle CssClass="alt" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>--%>
                              <div style="border: 1px solid #3598dc">
                                    <div class="portlet-body">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Nominee Details : </strong></div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <asp:GridView ID="GrdNominee" runat="server" AllowPaging="True" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                    EditRowStyle-BackColor="#FFFF99" OnPageIndexChanging="GrdNominee_PageIndexChanging"
                                                    PagerStyle-CssClass="pgr" Width="100%">
                                                    <Columns>

                                                        <asp:TemplateField HeaderText="SrNo" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSrNo1" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Nominee Name" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblNomineename" runat="server" Text='<%# Eval("NOMINEENAME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                           <asp:TemplateField HeaderText="Brcd" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblNBrcd" runat="server" Text='<%# Eval("BRCD") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Glcode" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Lblngl" runat="server" Text='<%# Eval("GLCODE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Subglcode" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblNsubgl" runat="server" Text='<%# Eval("SUBGLCODE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                         <asp:TemplateField HeaderText="Accno" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblNacc" runat="server" Text='<%# Eval("ACCNO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="DOB" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblNdob" runat="server" Text='<%# Eval("DOB") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Address1" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAddress1" runat="server" Text='<%# Eval("Address1") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Address2" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAddress2" runat="server" Text='<%# Eval("Address2") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Address3" Visible="true" >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAddress3" runat="server" Text='<%# Eval("Address3") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                          <asp:TemplateField HeaderText="City" Visible="true" >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCity" runat="server" Text='<%# Eval("City") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                          <asp:TemplateField HeaderText="PinCode" Visible="true" >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPinCode" runat="server" Text='<%# Eval("PinCode") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                              <asp:TemplateField HeaderText="MobNo" Visible="true" >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMobNo" runat="server" Text='<%# Eval("MobNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                          <asp:TemplateField HeaderText="PanNo" Visible="true" >
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPanNo" runat="server" Text='<%# Eval("PanNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    
                                                       
                                                    </Columns>
                                                    <PagerStyle CssClass="pgr" />
                                                    <HeaderStyle BackColor="#ffce9d" />
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
                    </div>
                    <div class="col-md-12">
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
    <div id="LOANDETAILS" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Loan Details Screen</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="portlet box" id="Div1"  style="background-color:#428bca; border:1px solid #ccc;">
                                <div class="portlet-title">
                                    <div class="caption">
                                        Loan Details of surity
                                    </div>
                                </div>
                                <div class="portlet-body form">
                                    <div class="form-horizontal">
                                        <div class="form-wizard">
                                            <div class="form-body" style="padding:14px">
                                                <div class="tab-content">
                                                    <div class="tab-pane active" id="tab1">
                                                        <div class="row" style="margin-bottom: 10px;">

                                                            <div class="row" style="margin: 10px;"></div>
                                                            <div style="border: 1px solid #3598dc">
                                                                <div class="portlet-body">
                                                                    <div class="row">
                                                                        <div class="col-lg-12">
                                                                            <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);">
                                                                                <strong style="color: #3598dc">Direct Liabilities : </strong></div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                                        <div class="col-lg-12">
                                                                            <asp:GridView ID="GrdDirectLiab1" runat="server" AllowPaging="True" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                                                EditRowStyle-BackColor="#FFFF99" OnPageIndexChanging="GrdDirectLiab_PageIndexChanging" OnRowDataBound="GrdDirectLiab_RowDataBound"
                                                                                PagerStyle-CssClass="pgr" Width="100%" ShowFooter="true">
                                                                                <Columns>

                                                                                    <asp:TemplateField HeaderText="SrNo" Visible="true" FooterText="Sub Total" FooterStyle-Font-Bold="true">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblSrNo11" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="Brcd" Visible="true">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblBrcd1" runat="server" Text='<%# Eval("BRCD") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="Prod Code" Visible="true">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblPrdcd1" runat="server" Text='<%# Eval("subglcode") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="Accno" Visible="true">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblAccno1" runat="server" Text='<%# Eval("accno") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="Opening Dt" Visible="true">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblOPdt1" runat="server" Text='<%# Eval("OPENINGDATE") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="Due Dt" Visible="true">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblduedt1" runat="server" Text='<%# Eval("DUEDATE") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="Sanction Amt" Visible="true" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblsancamt1" runat="server" Text='<%# Eval("LIMIT") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <div style="padding: 0 0 5px 0">
                                                                                                <asp:Label ID="Lbl_TotalLimit1" runat="server" />
                                                                                            </div>
                                                                                        </FooterTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Installment Amt" Visible="true" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblInstAmt1" runat="server" Text='<%# Eval("INSTALLMENT") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <div style="padding: 0 0 5px 0">
                                                                                                <asp:Label ID="Lbl_TotaliNSTALL1" runat="server" />
                                                                                            </div>
                                                                                        </FooterTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Outstanding bal" Visible="true" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbloutbal1" runat="server" Text='<%# Eval("BALANCE") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <div style="padding: 0 0 5px 0">
                                                                                                <asp:Label ID="Lbl_TotalBal1" runat="server" />
                                                                                            </div>
                                                                                        </FooterTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="OD Amt" Visible="true" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblodamt1" runat="server" Text='<%# Eval("ODAMT") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <FooterTemplate>
                                                                                            <div style="padding: 0 0 5px 0">
                                                                                                <asp:Label ID="Lbl_TotalOD1" runat="server" />
                                                                                            </div>
                                                                                        </FooterTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="No of Inst" Visible="true" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblnoofinst1" runat="server" Text='<%# Eval("TOTALINST") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Status" Visible="true">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblSts1" runat="server" Text='<%# Eval("STATUS") %>'></asp:Label>
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

                                                </div>
                                                <div class="row">

                                                    <div class="col-md-6">

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
</asp:Content>

