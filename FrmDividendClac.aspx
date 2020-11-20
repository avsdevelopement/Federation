<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmDividendClac.aspx.cs" Inherits="FrmDividendClac" %>

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
        function FormatIt(obj) {
            if (obj.value.length == 2) // Day
                obj.value = obj.value + "/";
            if (obj.value.length == 5) // month 
                obj.value = obj.value + "/";
            if (obj.value.length == 11) // year 
                alert("Please Enter valid Date");
        }
    </script>
    <script>
        function isvalidate() {

            var FDT, TDT, FBR, TBR, FPRD, TPRD, FAC, TAC, DivPrd, DebitPrdcd, Rate, CalcType;
            FDT = document.getElementById('<%=TxtFDate.ClientID%>').value;
            TDT = document.getElementById('<%=TxtTDate.ClientID%>').value;

            FPRD = document.getElementById('<%=TxtFPRD.ClientID%>').value;


            FAC = document.getElementById('<%=TxtFAcc.ClientID%>').value;
            TAC = document.getElementById('<%=TxtTAcc.ClientID%>').value;

            DivPrd = document.getElementById('<%=TxtDiviProdCode.ClientID%>').value;
            DebitPrdcd = document.getElementById('<%=TxtDebitPrdCode.ClientID%>').value;
            Rate = document.getElementById('<%=TxtDiviRate.ClientID%>').value;
            CalcType = document.getElementById('<%=DdlCalcType.ClientID%>').value;

            var message = '';


            if (FDT == "") {
                message = 'Enter From Date....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtFDate.ClientID%>').focus();
                return false;
            }
            if (TDT == "") {
                message = 'Enter To Date....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtTDate.ClientID%>').focus();
                return false;
            }
            if (FPRD == "") {
                message = 'Enter From Product Code....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtFPRD.ClientID%>').focus();
                return false;
            }


            if (Rate == "") {
                message = 'Enter Rate of interest....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtDiviRate.ClientID%>').focus();
                return false;
            }
            if (CalcType == "") {
                message = 'Enter Calculation type....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=DdlCalcType.ClientID%>').focus();
                return false;
            }


        }
    </script>
    <script type="text/javascript">
        function FromDateValid(Obj) {

            debugger;
            var FromDate = document.getElementById('<%=TxtFDate.ClientID%>').value || 0;
            var WorkingDate = document.getElementById('<%=Hf_WorkingDt.ClientID%>').value;

            var frdate = FromDate.substring(0, 2);
            var frmonth = FromDate.substring(3, 5);
            var fryear = FromDate.substring(6, 10);
            var frmyDate = new Date(fryear, frmonth - 1, frdate);

            var wdate = WorkingDate.substring(0, 2);
            var wmonth = WorkingDate.substring(3, 5);
            var wyear = WorkingDate.substring(6, 10);
            var wmyDate = new Date(wyear, wmonth - 1, wdate);

            if (frmyDate > wmyDate) {
                message = 'From date not allow greater than working date...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtFDate.ClientID %>').value = "";
                document.getElementById('<%=TxtFDate.ClientID%>').focus();
                return false;
            }
        }
    </script>
    <script type="text/javascript">
        function ToDateValid(obj) {
            debugger;
            var FromDate = document.getElementById('<%=TxtFDate.ClientID%>').value || 0;
            var ToDate = document.getElementById('<%=TxtTDate.ClientID%>').value || 0;
            var WorkingDate = document.getElementById('<%=Hf_WorkingDt.ClientID%>').value;

            var frdate = FromDate.substring(0, 2);
            var frmonth = FromDate.substring(3, 5);
            var fryear = FromDate.substring(6, 10);
            var frmyDate = new Date(fryear, frmonth - 1, frdate);

            var todate = ToDate.substring(0, 2);
            var tomonth = ToDate.substring(3, 5);
            var toyear = ToDate.substring(6, 10);
            var tomyDate = new Date(toyear, tomonth - 1, todate);

            var wdate = WorkingDate.substring(0, 2);
            var wmonth = WorkingDate.substring(3, 5);
            var wyear = WorkingDate.substring(6, 10);
            var wmyDate = new Date(wyear, wmonth - 1, wdate);

            if (tomyDate > wmyDate) {
                message = 'To Date greater than working date is invalid...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtFDate.ClientID %>').value = "";
                document.getElementById('<%=TxtTDate.ClientID%>').focus();
                return false;
            }
            else if (tomyDate < frmyDate) {
                message = 'To Date less than from date is invalid...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtTDate.ClientID %>').value = "";
                document.getElementById('<%=TxtTDate.ClientID%>').focus();
                return false;
            }
    }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="Btn_TextReport" />
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <asp:HiddenField ID="Hf_WorkingDt" runat="server" Value="" />
                <div class="col-md-12">
                    <div class="portlet box blue" id="Div1">
                        <div class="portlet-title">
                            <div class="caption">
                                Dividend Calculation
                            </div>
                        </div>
                        <div class="portlet-body form">
                            <div class="form-horizontal">
                                <div class="form-wizard">
                                    <div class="form-body">
                                        <div class="tab-content">
                                            <div class="tab-pane active" id="tab1">
                                                <div class="row" style="margin-bottom: 10px;">
                                                    <div class="col-lg-12">
                                                        <label class="control-label col-md-1">From Dt <span class="required">* </span></label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtFDate" OnBlur="FromDateValid()" CssClass="form-control" onkeyup="FormatIt(this)" runat="server"></asp:TextBox>
                                                            <asp:CalendarExtender ID="TxtAsOnDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtFDate">
                                                            </asp:CalendarExtender>
                                                        </div>
                                                        <label class="control-label col-md-1">To Dt <span class="required">* </span></label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtTDate" OnBlur="ToDateValid()" CssClass="form-control" onkeyup="FormatIt(this)" runat="server"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtTDate">
                                                            </asp:CalendarExtender>
                                                        </div>
                                                    </div>
                                                    </br>
                                            <div class="row" style="margin: 20px; border-bottom: 1px solid rgba(53, 152, 220, 0.55);"><strong></strong></div>
                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-1">PRD Cd.<span class="required">*</span></label>
                                                            <div class="col-md-1">
                                                                <asp:TextBox ID="TxtFPRD" OnTextChanged="TxtFPRD_TextChanged" Placeholder="From Product Code" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <asp:TextBox ID="TxtFPRDName" CssClass="form-control" runat="server" OnTextChanged="TxtFPRDName_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                <div id="CustList" style="height: 200px; overflow-y: scroll;"></div>
                                                                <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="TxtFPRDName"
                                                                    UseContextKey="true"
                                                                    CompletionInterval="1"
                                                                    CompletionSetCount="20"
                                                                    MinimumPrefixLength="1"
                                                                    EnableCaching="true"
                                                                    ServicePath="~/WebServices/Contact.asmx"
                                                                    ServiceMethod="getglname" CompletionListElementID="CustList">
                                                                </asp:AutoCompleteExtender>
                                                            </div>

                                                            <%-- <label class="control-label col-md-1">PRD Cd.<span class="required">*</span></label>
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="TxtTPRD" Placeholder="To Product Code" OnTextChanged="TxtTPRD_TextChanged" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server"  AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TXtTPRDName" CssClass="form-control" runat="server" OnTextChanged="TXtTPRDName_TextChanged" AutoPostBack="true" ></asp:TextBox>
                                                        <div id="Div2" style="height: 200px; overflow-y: scroll;"></div>
                                                        <asp:AutoCompleteExtender ID="autoglname1" runat="server" TargetControlID="TXtTPRDName"
                                                            UseContextKey="true"
                                                            CompletionInterval="1"
                                                            CompletionSetCount="20"
                                                            MinimumPrefixLength="1"
                                                            EnableCaching="true"
                                                            ServicePath="~/WebServices/Contact.asmx"
                                                            ServiceMethod="getglname" CompletionListElementID="Div2">
                                                        </asp:AutoCompleteExtender>
                                                    </div>--%>
                                                        </div>
                                                    </div>

                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-1">From A/C <span class="required">*</span></label>
                                                            <div class="col-md-1">
                                                                <asp:TextBox ID="TxtFAcc" OnTextChanged="TxtFAcc_TextChanged" Placeholder="From A/C No." onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <asp:TextBox ID="TxtFAccName" OnTextChanged="TxtFAccName_TextChanged" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                            <label class="control-label col-md-1">To A/C <span class="required">*</span></label>
                                                            <div class="col-md-1">
                                                                <asp:TextBox ID="TxtTAcc" OnTextChanged="TxtTAcc_TextChanged" Placeholder="To A/C No." onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <asp:TextBox ID="TxtTAccName" OnTextChanged="TxtTAccName_TextChanged" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin: 20px; border-bottom: 1px solid rgba(53, 152, 220, 0.55);"><strong></strong></div>
                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-1">Divi Prdcd<span class="required">*</span></label>
                                                            <div class="col-md-1">
                                                                <asp:TextBox ID="TxtDiviProdCode" OnTextChanged="TxtDiviProdCode_TextChanged" Placeholder="Code" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <asp:TextBox ID="TxtDiviProdName" OnTextChanged="TxtDiviProdName_TextChanged" CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                                                                <div id="Div3" style="height: 200px; overflow-y: scroll;"></div>
                                                                <asp:AutoCompleteExtender ID="AutoGlname3" runat="server" TargetControlID="TxtDiviProdName"
                                                                    UseContextKey="true"
                                                                    CompletionInterval="1"
                                                                    CompletionSetCount="20"
                                                                    MinimumPrefixLength="1"
                                                                    EnableCaching="true"
                                                                    ServicePath="~/WebServices/Contact.asmx"
                                                                    ServiceMethod="getglname" CompletionListElementID="Div3">
                                                                </asp:AutoCompleteExtender>
                                                            </div>
                                                            <label class="control-label col-md-1">Debit Prdcd<span class="required">*</span></label>
                                                            <div class="col-md-1">
                                                                <asp:TextBox ID="TxtDebitPrdCode" OnTextChanged="TxtDebitPrdCode_TextChanged" Placeholder="Code" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <asp:TextBox ID="TxtDebitPrdName" OnTextChanged="TxtDebitPrdName_TextChanged" CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                                                                <div id="Div4" style="height: 200px; overflow-y: scroll;"></div>
                                                                <asp:AutoCompleteExtender ID="AutoGlname4" runat="server" TargetControlID="TxtDebitPrdName"
                                                                    UseContextKey="true"
                                                                    CompletionInterval="1"
                                                                    CompletionSetCount="20"
                                                                    MinimumPrefixLength="1"
                                                                    EnableCaching="true"
                                                                    ServicePath="~/WebServices/Contact.asmx"
                                                                    ServiceMethod="getglname" CompletionListElementID="Div4">
                                                                </asp:AutoCompleteExtender>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-1">Rate<span class="required">*</span></label>
                                                            <div class="col-md-1">
                                                                <asp:TextBox ID="TxtDiviRate" OnTextChanged="TxtDiviRate_TextChanged" Placeholder="Rate" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                                                            </div>
                                                            <label class="control-label col-md-1">Calc Type <span class="required">*</span></label>
                                                            <div class="col-md-2">
                                                                <asp:DropDownList ID="DdlCalcType" OnTextChanged="DdlCalcType_TextChanged" CssClass="form-control" runat="server" AutoPostBack="true"></asp:DropDownList>
                                                            </div>
                                                            <label class="control-label col-md-1">Narration<span class="required">*</span></label>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="TxtNarr" OnTextChanged="TxtNarr_TextChanged" Placeholder="Particulars" CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row" style="margin: 20px; border-bottom: 1px solid rgba(53, 152, 220, 0.55);"><strong></strong></div>
                                                    <div class="row" style="margin: 7px 0 7px 0; text-align: center">
                                                        <div class="col-lg-12">
                                                            <asp:Button ID="Btn_Calculate" runat="server" Text="Calculate" CssClass="btn btn-primary" OnClick="Btn_Calculate_Click" OnClientClick="Javascript:return isvalidate();" />
                                                            <asp:Button ID="Btn_Recalculate" runat="server" Text="Re-Calculate" CssClass="btn btn-primary" OnClick="Btn_Recalculate_Click" />
                                                            <asp:Button ID="Btn_TrailEntry" OnClientClick="Javascript:return isvalidate();" runat="server" Text="Trial Run" CssClass="btn btn-primary" OnClick="Btn_TrailEntry_Click" />
                                                            <asp:Button ID="Btn_ApplyEntry" OnClientClick="Javascript:return isvalidate();" runat="server" Text="Apply Entry" CssClass="btn btn-success" OnClick="Btn_ApplyEntry_Click" />
                                                            <asp:Button ID="Btn_Report" runat="server" Text="Report" CssClass="btn btn-success" OnClick="Btn_Report_Click" OnClientClick="Javascript:return isvalidate();" />
                                                            <asp:Button ID="Btn_ClearAll" runat="server" Text="Clear All" CssClass="btn btn-success" OnClick="Btn_ClearAll_Click" />
                                                            <asp:Button ID="Btn_Exit" runat="server" Text="Exit" CssClass="btn btn-success" OnClick="Btn_Exit_Click" />
                                                            <asp:Button ID="Btn_TextReport" runat="server" Text="Text Report" CssClass="btn btn-success" OnClick="Btn_TextReport_Click" />
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
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="table-scrollable">
                                <table class="table table-striped table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th>
                                                <asp:GridView ID="GrdSDC" runat="server"
                                                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                    EditRowStyle-BackColor="#FFFF99"
                                                    Width="100%">
                                                    <Columns>
                                                        <%--Brcd	CertNo	Subgl	MemNo	CustName	IssueDt	FromDt	ToDate	DayDiff	DividAmt	NoOfShares	TotalHolding	OnBalance	CloseDt	Remarks	Flag--%>
                                                        <asp:TemplateField HeaderText="Brcd">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Brcd" runat="server" Text='<%# Eval("Brcd") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Subgl">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Subgl" runat="server" Text='<%# Eval("Subgl") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="MemNo">
                                                            <ItemTemplate>
                                                                <asp:Label ID="MemNo" runat="server" Text='<%# Eval("MemNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="CustName" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="CustName" runat="server" Text='<%# Eval("CustName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="IssueDt">
                                                            <ItemTemplate>
                                                                <asp:Label ID="IssueDt" runat="server" Text='<%# Eval("IssueDt") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="FromDt">
                                                            <ItemTemplate>
                                                                <asp:Label ID="FromDt" runat="server" Text='<%# Eval("FromDt") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ToDate">
                                                            <ItemTemplate>
                                                                <asp:Label ID="ToDate" runat="server" Text='<%# Eval("ToDate") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DayDiff">
                                                            <ItemTemplate>
                                                                <asp:Label ID="DayDiff" runat="server" Text='<%# Eval("DayDiff") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DividAmt">
                                                            <ItemTemplate>
                                                                <asp:Label ID="DividAmt" runat="server" Text='<%# Eval("DividAmt") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="NoOfShares">
                                                            <ItemTemplate>
                                                                <asp:Label ID="NoOfShares" runat="server" Text='<%# Eval("NoOfShares") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="TotalHolding">
                                                            <ItemTemplate>
                                                                <asp:Label ID="TotalHolding" runat="server" Text='<%# Eval("TotalHolding") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="OnBalance">
                                                            <ItemTemplate>
                                                                <asp:Label ID="OnBalance" runat="server" Text='<%# Eval("OnBalance") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CloseDt">
                                                            <ItemTemplate>
                                                                <asp:Label ID="CloseDt" runat="server" Text='<%# Eval("CloseDt") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Remarks">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Remarks" runat="server" Text='<%# Eval("Remarks") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Remarks">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Remarks" runat="server" Text='<%# Eval("Flag") %>'></asp:Label>
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
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

