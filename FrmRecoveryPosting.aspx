<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmRecoveryPosting.aspx.cs" Inherits="FrmRecoveryPosting" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        td.aspNetDisabled {
        background-color:#F7ECEC;
        }

        


    </style>
    <script type="text/javascript">
        function isvalidate() {
            debugger;
            var Brcode, Mon, Year, RDiv, RCode, DebitC;
            Brcode = document.getElementById('<%=ddlBrCode.ClientID%>').value;
            Mon = document.getElementById('<%=TxtMM.ClientID%>').value;
            Year = document.getElementById('<%=TxtYYYY.ClientID%>').value;
            RDiv = document.getElementById('<%=DdlRecDiv.ClientID%>').value;
            RCode = document.getElementById('<%=DdlRecDept.ClientID%>').value;
            DebitC = document.getElementById('<%=TxtDebitCode.ClientID%>').value;

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

            if (RDiv == "0") {
                message = 'Enter Recovery division....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=DdlRecDiv.ClientID%>').focus();
                return false;
            }
            if (RCode == "0") {
                message = 'Enter Recovery department....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=DdlRecDept.ClientID%>').focus();
                return false;
            }
            if (DebitC == "") {
                message = 'Enter Recovery debit account....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtDebitCode.ClientID%>').focus();
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
    </script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="page-wrapper">

        <div class="panel panel-primary">
            <div class="panel-heading">Recovery Posting</div>
            <div class="panel-body">
                <div class="tab-content">
                      <div class="tab-pane active" id="tab1">
                    <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                        <div class="col-lg-12">
                            <div class="col-md-2">
                                <label class="control-label ">Brcd</label>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlBrCode" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlBrCode_SelectedIndexChanged" AutoPostBack="true" Enabled="false">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-6">
                                <asp:RadioButtonList ID="Rdb_PostType" runat="server" RepeatDirection="Horizontal" Width="400px" OnTextChanged="Rdb_PostType_TextChanged" AutoPostBack="true">
                                    <asp:ListItem Value="A" Text="All Posting" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="S" Text="Specific Posting"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 5px; margin-bottom: 5px;" id="DivType" runat="server" visible="false">
                        <div class="col-lg-12">
                             <div class="col-md-2">
                                </div>
                            <div class="col-md-3">
                            </div>
                           <div class="col-md-6">
                                <asp:RadioButtonList ID="Rdb_Type" runat="server" RepeatDirection="Horizontal" Width="400px" OnSelectedIndexChanged="Rdb_Type_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="D" Text="DeptWise" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="C" Text="CustomerWise"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                    </div>
                    <div class="row" runat="server" id="DIV1" visible="false">
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
                                        <asp:TextBox ID="txtCustName" runat="server" CssClass="form-control" OnTextChanged="txtCustName_TextChanged" AutoPostBack="true"></asp:TextBox>

                                        <div id="RecNames" style="height: 200px; overflow-y: scroll;"></div>
                                       <asp:AutoCompleteExtender ID="AutoCustName" runat="server" TargetControlID="txtCustName"
                                                    UseContextKey="true"
                                                    CompletionInterval="1"
                                                    CompletionSetCount="20"
                                                    MinimumPrefixLength="1"
                                                    EnableCaching="true"
                                                    ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="RecNames"                                           
                                                    ServiceMethod="GetCustNames">
                                                </asp:AutoCompleteExtender>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row" runat="server" id="DIV2">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="col-md-12">
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
                        </div>
                    </div>
                    <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                        <div class="col-lg-12">
                            <div class="col-md-2">
                                <label class="control-label ">Rec div:</label>
                            </div>
                            <div class="col-md-6">
                                <asp:DropDownList ID="DdlRecDiv" CssClass="form-control" runat="server" OnSelectedIndexChanged="DdlRecDiv_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                        <div class="col-lg-12">
                            <div class="col-md-2">
                                <label class="control-label ">Rec Dept:</label>
                            </div>
                            <div class="col-md-6">
                                <asp:DropDownList ID="DdlRecDept" CssClass="form-control" runat="server" OnSelectedIndexChanged="DdlRecDept_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                        <div class="col-lg-12">
                            <div class="col-md-2">
                                <label class="control-label ">Debit Code</label>
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="TxtDebitCode" placeholder="Code" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="TxtDebitCode_TextChanged" />
                            </div>
                            <div class="col-md-1">
                                <label class="control-label ">Name</label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="TxtDebitCodeName" OnTextChanged="TxtDebitCodeName_TextChanged" placeholder="Name" CssClass="form-control" runat="server" AutoPostBack="true" />
                                 <div id="CustList" style="height: 200px; overflow-y: scroll;"></div>
                                <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="TxtDebitCodeName"
                                    UseContextKey="true"
                                    CompletionInterval="1"
                                    CompletionSetCount="20"
                                    MinimumPrefixLength="1"
                                    EnableCaching="true"
                                    ServicePath="~/WebServices/Contact.asmx"
                                    ServiceMethod="getglname" CompletionListElementID="CustList">
                                </asp:AutoCompleteExtender>
                            </div>

                            <div class="col-md-1">
                                <label class="control-label ">Total</label>
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="TxtAmountSpe" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server" Enabled="false" />
                            </div>
                        </div>
                    </div>
                       <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                        <div class="col-lg-12">
                            <div class="col-md-2">
                                <label class="control-label ">Cheque No.</label>
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="TxtChequeNo" placeholder="6 digit" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server" MaxLength="6"/>
                            </div>
                            <div class="col-md-1">
                                <label class="control-label ">Cheque Dt.</label>
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="TxtChequeDate" onkeypress="javascript:return isNumber(event)" onkeyup="FormatIt(this)"  MaxLength="10" placeholder="Date" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="TxtChequeDate_TextChanged" />
                            </div>
                                                       
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12 text-center">
                            <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-success" Text="Add More" OnClick="btnAdd_Click" Visible="false" />
                            <asp:Button ID="Btn_Report" runat="server" CssClass="btn btn-success" Text="Report" OnClick="Btn_Report_Click" OnClientClick="Javascript:return isvalidate();" />
                              <asp:Button ID="Btn_ExReport" runat="server" CssClass="btn btn-success" Text="Ex Rec Report" OnClick="Btn_ExReport_Click"/>
                            <asp:Button ID="BtnPost" runat="server" CssClass="btn btn-success" Text="Post" OnClick="BtnPost_Click" OnClientClick="Javascript:return isvalidate();" />
                            <asp:Button ID="BtnPostSpecific" runat="server" CssClass="btn btn-success" Text="Post Specific" OnClick="BtnPostSpecific_Click" Visible="false" OnClientClick="Javascript:return isvalidate();" />

                        </div>
                    </div>

                            </div>
                    <!--end of div tag-->
                </div>
            </div>
        </div>
        <div class="row" id="Div_SpecificPosting" runat="server">
            <div class="col-md-12">
                <div class="table-scrollable" style="width: 100%; height: 500px; overflow-x: auto; overflow-y: auto">
                    <table class="table table-striped table-bordered table-hover" id="tbl_specific">
                        <thead>
                            <tr>
                                <th>
                                    <asp:Label ID="Lbl_GridName" Text="" runat="server" Style="font-size: large; color: darkblue; background-color: aqua;"></asp:Label>
                                  
                                        <asp:Button ID="BtnUpdateAll" CssClass="btn btn-primary" runat="server" Text="Update All" OnClick="BtnUpdateAll_Click" visible="false"/>
                                        <asp:Button ID="BtnSelectAll" CssClass="btn btn-primary" runat="server" Text ="Select All" OnClick="BtnSelectAll_Click" visible="false"/>
                                  
                                    <asp:GridView ID="Grd_SpecificPost" ShowFooter="true" runat="server" AutoGenerateColumns="false" OnRowUpdating="Grd_SpecificPost_RowUpdated" OnRowDataBound="Grd_SpecificPost_RowDataBound">

                                        <Columns>
                                               <asp:TemplateField HeaderText="Specific Select" HeaderStyle-ForeColor="#99ff99">
                                                   
                                                <ItemTemplate >
                                                    <asp:CheckBox ID="Chk_Specific" runat="server"  OnCheckedChanged="Chk_Specific_CheckedChanged" AutoPostBack="true" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           

                                            <asp:TemplateField HeaderText="Modify">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="Lnk_ConfirmModify" runat="server" class="glyphicon glyphicon-pencil" CommandName="Update" CommandArgument='<%#Eval("Id")%>'></asp:LinkButton>
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
                                                    <asp:TextBox ID="txtSrNo" Enable="false" Style="width: 80px" CssClass="form-control" runat="server" Text='<%#Container.DataItemIndex+1 %>' Enabled="false" />
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
                                            <asp:TemplateField HeaderText="S1Bal">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS1Bal" Text='<%# Eval("S1Bal") %>' Style="width: 120px" CssClass="form-control" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="S1Inst">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS1Inst"  Text='<%# Eval("S1Inst") %>' Style="width: 120px" CssClass="form-control" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="S1Intr">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS1Intr" Text='<%# Eval("S1Intr") %>' Style="width: 120px" CssClass="form-control" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--  <asp:TemplateField HeaderText="S2">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS2" Text='<%# Eval("S2") %>' Enabled="false" Style="width: 80px" CssClass="form-control" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="S2Bal">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS2Bal" Text='<%# Eval("S2Bal") %>' Style="width: 120px" CssClass="form-control" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="S2Inst">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS2Inst" Text='<%# Eval("S2Inst") %>' Style="width: 120px" CssClass="form-control" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="S2Intr">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS2Intr" Text='<%# Eval("S2Intr") %>' Style="width: 120px" CssClass="form-control" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:TemplateField HeaderText="MON">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS3" Text='<%# Eval("S3") %>' Enabled="false" Style="width: 80px" CssClass="form-control" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="MON Bal">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS3Bal"  Text='<%# Eval("S3Bal") %>' Style="width: 120px" CssClass="form-control" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--  <asp:TemplateField HeaderText="MA">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS4" Text='<%# Eval("S4") %>' Enabled="false" Style="width: 80px" CssClass="form-control" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="MA Bal">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS4Bal"  Text='<%# Eval("S4Bal") %>' Style="width: 120px" CssClass="form-control" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:TemplateField HeaderText="KNT">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS5" Text='<%# Eval("S5") %>' Enabled="false" Style="width: 80px" CssClass="form-control" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="KNT Bal">
                                                <ItemTemplate> 
                                                    <asp:TextBox ID="TxtS5Bal" Text='<%# Eval("S5Bal") %>' Style="width: 120px" CssClass="form-control" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:TemplateField HeaderText="RD">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS6" Text='<%# Eval("S6") %>' Enabled="false" Style="width: 80px" CssClass="form-control" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="RD Bal">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS6Bal"  Text='<%# Eval("S6Bal") %>' Style="width: 120px" CssClass="form-control" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--   <asp:TemplateField HeaderText="DF">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS7" Text='<%# Eval("S7") %>' Enabled="false" Style="width: 80px" CssClass="form-control" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="DF Bal">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS7Bal" Text='<%# Eval("S7Bal") %>' Style="width: 120px" CssClass="form-control" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField HeaderText="MW">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS8" Text='<%# Eval("S8") %>' Enabled="false" Style="width: 80px" CssClass="form-control" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="MW Bal">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS8Bal" Text='<%# Eval("S8Bal") %>' Style="width: 120px" CssClass="form-control" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:TemplateField HeaderText="US">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS9" Text='<%# Eval("S9") %>' Enabled="false" Style="width: 80px" CssClass="form-control" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="US Bal">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS9Bal"  Text='<%# Eval("S9Bal") %>' Style="width: 120px" CssClass="form-control" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Surity Amt">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtSurityAmt" Text='<%# Eval("SurityAmt") %>' Style="width: 80px" CssClass="form-control" runat="server" Enabled="false" />
                                                </ItemTemplate>

                                              </asp:TemplateField>
                                             <asp:TemplateField HeaderText="MM">
                                                <ItemTemplate>
                                                    <asp:Label ID="Lbl_MM" runat="server" Text='<%# Eval("MM") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="YYYY">
                                                <ItemTemplate>
                                                    <asp:Label ID="Lbl_YYYY" runat="server" Text='<%# Eval("YYYY") %>'></asp:Label>
                                                </ItemTemplate>
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
                                    
                                     <asp:GridView ID="GridView1009" runat="server" AutoGenerateColumns="false" OnRowUpdating="GridView1009_RowUpdating" ShowFooter="true" OnRowDataBound="GridView1009_RowDataBound" >

                                        <Columns>
                                            <asp:TemplateField HeaderText="Specific" HeaderStyle-BackColor="#99ff99">
                                                
                                                <ItemTemplate>
                                             
                                                <asp:CheckBox ID="Chk_Specific1009" runat="server"   OnCheckedChanged="Chk_Specific1009_CheckedChanged" AutoPostBack="true" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Modify" HeaderStyle-BackColor="#99ff99">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="Lnk_ConfirmModify" runat="server" class="glyphicon glyphicon-pencil" CommandName="Update" CommandArgument='<%#Eval("Id")%>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="ID" Visible="false" HeaderStyle-BackColor="#99ff99">
                                                <ItemTemplate>
                                                    <asp:Label ID="Lbl_ID" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Stage" HeaderStyle-BackColor="#99ff99">
                                                <ItemTemplate>
                                                    <asp:Label ID="Lbl_Stage" runat="server" Text='<%# Eval("Stage") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Sr.No" HeaderStyle-BackColor="#99ff99">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtSrNo" Style="width: 80px" CssClass="form-control" runat="server" Text='<%#Container.DataItemIndex+1 %>' Enabled="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="CustNo" HeaderStyle-BackColor="#99ff99">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtCustNo" Text='<%# Eval("Custno") %>' Style="width: 80px" CssClass="form-control" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="CustName" HeaderStyle-BackColor="#99ff99">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtCustName" Text='<%# Eval("CustName") %>' CssClass="form-control" Style="width: 200px" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Loan1 Bal" HeaderStyle-BackColor="#99ff99">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS1Bal" Text='<%# Eval("S1Bal") %>' Style="width: 100px" CssClass="form-control" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbls1Bal" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Loan1 Inst" HeaderStyle-BackColor="#99ff99">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS1Inst" Text='<%# Eval("S1Inst") %>' Style="width: 100px" CssClass="form-control" runat="server"  Enabled="false" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbls1Inst" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Loan1 Intr" HeaderStyle-BackColor="#99ff99">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS1Intr" Text='<%# Eval("S1Intr") %>' Style="width: 100px" CssClass="form-control" runat="server" Enabled="false"  />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbls1Intr" runat="server" HeaderStyle-BackColor="#99ff99"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Loan2Bal" HeaderStyle-BackColor="#99ff99">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS2Bal" Text='<%# Eval("S2Bal") %>' Style="width: 100px" CssClass="form-control" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbls2bal" runat="server" HeaderStyle-BackColor="#99ff99"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Loan2Inst" HeaderStyle-BackColor="#99ff99">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS2Inst" Text='<%# Eval("S2Inst") %>' Style="width: 100px" CssClass="form-control" runat="server" Enabled="false"  />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbls2Inst" runat="server" HeaderStyle-BackColor="#99ff99"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Loan2Intr" HeaderStyle-BackColor="#99ff99">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS2Intr" Text='<%# Eval("S2Intr") %>' Style="width: 100px" CssClass="form-control" runat="server" Enabled="false"  />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbls2Intr" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderText="Loan3Bal" HeaderStyle-BackColor="#99ff99">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS3Bal" Text='<%# Eval("S3Bal") %>' Style="width: 100px" CssClass="form-control" runat="server" Enabled="false" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbls3bal" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Loan3Inst" HeaderStyle-BackColor="#99ff99">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS3Inst" Text='<%# Eval("S3Inst") %>' Style="width: 100px" CssClass="form-control" runat="server" Enabled="false"  />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbls3Inst" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Loan3Intr" HeaderStyle-BackColor="#99ff99">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS3Intr" Text='<%# Eval("S3Intr") %>' Style="width: 100px" CssClass="form-control" runat="server" Enabled="false"  />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbls3Intr" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="MON Bal" HeaderStyle-BackColor="#99ff99">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS4Bal" Text='<%# Eval("S4Bal") %>' Style="width: 100px" CssClass="form-control" runat="server" Enabled="false"  />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbls4Bal" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="MA Bal" HeaderStyle-BackColor="#99ff99">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS5Bal" Text='<%# Eval("S5Bal") %>' Style="width: 100px" CssClass="form-control" runat="server" Enabled="false"  />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbls5Bal" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="KNT Bal" HeaderStyle-BackColor="#99ff99">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS6Bal" Text='<%# Eval("S6Bal") %>' Style="width: 100px" CssClass="form-control" runat="server" Enabled="false"  />
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lbls6Bal" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="RD Bal" HeaderStyle-BackColor="#99ff99">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS7Bal" Text='<%# Eval("S7Bal") %>' Style="width: 100px" CssClass="form-control" runat="server" Enabled="false"  />
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lbls7Bal" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="DF Bal" HeaderStyle-BackColor="#99ff99">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS8Bal" Text='<%# Eval("S8Bal") %>' Style="width: 100px" CssClass="form-control" runat="server" Enabled="false"  />
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lbls8Bal" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="MW Bal" HeaderStyle-BackColor="#99ff99">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS9Bal" Text='<%# Eval("S9Bal") %>' Style="width: 100px" CssClass="form-control" runat="server" Enabled="false"  />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbls9Bal" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="US Bal" HeaderStyle-BackColor="#99ff99">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtS10Bal" Text='<%# Eval("S10Bal") %>' Style="width: 100px" CssClass="form-control" runat="server" Enabled="false"  />
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="lbls10Bal" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                           
                                            <asp:TemplateField HeaderText="MM" HeaderStyle-BackColor="#99ff99">
                                                <ItemTemplate>
                                                    <asp:Label ID="Lbl_MM" runat="server" Text='<%# Eval("MM") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="YYYY" HeaderStyle-BackColor="#99ff99">
                                                <ItemTemplate>
                                                    <asp:Label ID="Lbl_YYYY" runat="server" Text='<%# Eval("YYYY") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Total Bal" HeaderStyle-BackColor="#99ff99">
                                                <ItemTemplate>
                                                    <asp:Label ID="Lbl_RowTotal" runat="server" Text='<%# Eval("Total") %>' ></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="Th" HorizontalAlign="Right" />
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
        <asp:HiddenField ID="hdnCustNo" runat="server" Value="0" />
        <asp:HiddenField ID="hdnRec" runat="server" Value="0" />
    </div>


</asp:Content>

