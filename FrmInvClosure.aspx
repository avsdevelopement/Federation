<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmInvClosure.aspx.cs" Inherits="FrmInvClosure" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;

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

        function CalculateTotal() {
            var inv, IntProv, IntCalc;
            inv = document.getElementById('<%=TxtInAmt.ClientID%>').value;
            IntProv = document.getElementById('<%=TxtInt.ClientID%>').value;
            IntCalc = document.getElementById('<%=TxtIntCalc.ClientID%>').value;
            document.getElementById('<%=TxtTotalMaturity.ClientID%>').value = parseFloat(document.getElementById('<%=TxtInAmt.ClientID%>').value) + parseFloat(document.getElementById('<%=TxtIntCalc.ClientID%>').value) + parseFloat(document.getElementById('<%=TxtInt.ClientID%>').value);
            document.getElementById('<%=TxtIntPaid.ClientID%>').value = parseFloat(document.getElementById('<%=TxtIntCalc.ClientID%>').value) + parseFloat(document.getElementById('<%=TxtInt.ClientID%>').value);
            document.getElementById('<%=txtNet.ClientID%>').value = (parseFloat(document.getElementById('<%=TxtTotalMaturity.ClientID%>').value) - parseFloat(document.getElementById('<%=TxtTDSDeduct.ClientID%>').value)).toFixed(2);
            document.getElementById('<%=TxtInstPrinci.ClientID%>').value = (parseFloat(document.getElementById('<%=TxtTotalMaturity.ClientID%>').value) - parseFloat(document.getElementById('<%=TxtTDSDeduct.ClientID%>').value)).toFixed(2);
        }

        function CalculateTotal1() {

            var inv, IntProv, IntCalc;
            inv = document.getElementById('<%=TxtInstPrinci.ClientID%>').value;
            IntProv = document.getElementById('<%=txtIntprovision.ClientID%>').value;
            IntCalc = document.getElementById('<%=txtMIntCal.ClientID%>').value;
            document.getElementById('<%=TxtMatValue.ClientID%>').value = parseFloat(document.getElementById('<%=TxtInstPrinci.ClientID%>').value) + parseFloat(document.getElementById('<%=txtMIntCal.ClientID%>').value) + parseFloat(document.getElementById('<%=txtIntprovision.ClientID%>').value);
            document.getElementById('<%=txtMIntPaid.ClientID%>').value = parseFloat(document.getElementById('<%=txtMIntCal.ClientID%>').value) + parseFloat(document.getElementById('<%=txtIntprovision.ClientID%>').value);
            document.getElementById('<%=TextBox13.ClientID%>').value = (parseFloat(document.getElementById('<%=TxtMatValue.ClientID%>').value) - parseFloat(document.getElementById('<%=txtMTDS.ClientID%>').value)).toFixed(2);

        }

        function calculateTDS() {
            var inv = document.getElementById('<%=TxtInAmt.ClientID%>').value;
            var DInt, MInt, day, Month;
            day = document.getElementById('<%=TxtDays.ClientID%>').value;
            if (day != 0)

                DInt = (((parseInt(inv) * parseFloat(document.getElementById('<%=TxtROI.ClientID%>').value)) / 100) / parseInt(365)) * parseInt(day);
            else
                DInt = 0;
            Month = document.getElementById('<%=TxtMonths.ClientID%>').value;
            if (Month != 0)
                MInt = (((parseInt(inv) * parseFloat(document.getElementById('<%=TxtROI.ClientID%>').value)) / 100) / parseInt(12)) * parseInt(Month);
            else
                MInt = 0;
            document.getElementById('<%=TxtIntCalc.ClientID%>').value = parseInt(DInt) + parseInt(MInt);
            document.getElementById('<%=TxtTotalMaturity.ClientID%>').value = parseFloat(document.getElementById('<%=TxtInAmt.ClientID%>').value) + parseFloat(document.getElementById('<%=TxtIntCalc.ClientID%>').value) + parseFloat(document.getElementById('<%=TxtInt.ClientID%>').value);
            document.getElementById('<%=txtNet.ClientID%>').value = (parseFloat(document.getElementById('<%=TxtTotalMaturity.ClientID%>').value) - parseFloat(document.getElementById('<%=TxtTDSDeduct.ClientID%>').value)).toFixed(2);
            document.getElementById('<%=TxtInstPrinci.ClientID%>').value = (parseFloat(document.getElementById('<%=TxtTotalMaturity.ClientID%>').value) - parseFloat(document.getElementById('<%=TxtTDSDeduct.ClientID%>').value)).toFixed(2);
        }

        function CalculateMTDS() {
            var inv = document.getElementById('<%=TxtInstPrinci.ClientID%>').value;
            var DInt, MInt, day, Month;
            day = document.getElementById('<%=TxtCDays.ClientID%>').value;
            if (day != 0)

                DInt = (((parseInt(inv) * parseFloat(document.getElementById('<%=TxtCROI.ClientID%>').value)) / 100) / parseInt(365)) * parseInt(day);
            else
                DInt = 0;
            Month = document.getElementById('<%=TxtCMonths.ClientID%>').value;
            if (Month != 0)
                MInt = (((parseInt(inv) * parseFloat(document.getElementById('<%=TxtCROI.ClientID%>').value)) / 100) / parseInt(12)) * parseInt(Month);
            else
                MInt = 0;
            document.getElementById('<%=txtMIntCal.ClientID%>').value = parseInt(DInt) + parseInt(MInt);
            document.getElementById('<%=TxtMatValue.ClientID%>').value = parseFloat(document.getElementById('<%=TxtInstPrinci.ClientID%>').value) + parseFloat(document.getElementById('<%=txtMIntCal.ClientID%>').value) + parseFloat(document.getElementById('<%=txtIntprovision.ClientID%>').value);
            document.getElementById('<%=TextBox13.ClientID%>').value = (parseFloat(document.getElementById('<%=TxtMatValue.ClientID%>').value) - parseFloat(document.getElementById('<%=txtMTDS.ClientID%>').value)).toFixed(2);
        }
    </script>
    <script>
        function IsValid() {

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div id="DivFilter" runat="server">
        <div class="row">
            <div class="col-md-12">
                <div class="portlet box blue" id="Div5">
                    <div class="portlet-title">
                        <div class="caption">
                            Investment Closure/Renewal
                        </div>
                    </div>
                    <div class="portlet-body form">
                        <div class="form-horizontal">
                            <div class="form-wizard">
                                <div class="form-body">
                                    <div class="tab-content">
                                        <div class="tab-pane active" id="Div6">

                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Prd Code</label>
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="txtInvProd" OnTextChanged="txtInvProd_TextChanged" Placeholder=" Product Code" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtInvPrdNm" OnTextChanged="txtInvPrdNm_TextChanged" Placeholder=" Product Name" CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                                                        <div id="Div7" style="height: 200px; overflow-y: scroll;"></div>
                                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtInvPrdNm"
                                                            UseContextKey="true"
                                                            CompletionInterval="1"
                                                            CompletionSetCount="20"
                                                            MinimumPrefixLength="1"
                                                            EnableCaching="true"
                                                            ServicePath="~/WebServices/Contact.asmx"
                                                            ServiceMethod="GetGlInv" CompletionListElementID="Div7">
                                                        </asp:AutoCompleteExtender>
                                                    </div>
                                                    <label class="control-label col-md-2">A/C No. </label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtInvAc" Placeholder="A/C No." OnTextChanged="txtInvAc_TextChanged" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtInvAcName" Placeholder="A/C Name." CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>


                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-bottom: 10px;">
                                            <div class="row" style="margin: 7px 0 7px 0; text-align: center" align="center">
                                                <div class="col-lg-12">
                                                    <div class="col-md-12">
                                                        <asp:Button ID="btngrid" OnClick="btngrid_Click" runat="server" Text="Submit" CssClass="btn btn-primary" />
                                                        <asp:Button ID="btnclear" OnClick="btnclear_Click" runat="server" Text="Clear All" CssClass="btn btn-success" />
                                                        <asp:Button ID="btnexit" runat="server" Text="Exit" CssClass="btn btn-success" OnClick="btnexit_Click" />
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

    <div class="col-md-12" id="divGrid" runat="server" visible="false">
        <div class="table-scrollable">
            <table class="table table-striped table-bordered table-hover">
                <thead>
                    <tr>
                        <th>
                            <asp:GridView ID="grdInvAccMaster" runat="server" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                EditRowStyle-BackColor="#FFFF99" PagerStyle-CssClass="pgr" Width="100%" OnRowDataBound="grdInvAccMaster_RowDataBound" ShowFooter="true">
                                <Columns>

                                    <asp:TemplateField HeaderText="Select" Visible="true" runat="server">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName='<%# Eval("BankName") %>' CommandArgument='<%#Eval("SubGLCode")+"_"+Eval("CustACCNO")+"_"+Eval("DueDate")%>' OnClick="lnkEdit_Click" class="glyphicon glyphicon-plus"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="PRDCD" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBankCode" runat="server" Text='<%# Eval("SubGLCOde") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="SUBGLCODE">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBankName" runat="server" Text='<%# Eval("BankName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="A/C No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBranchCode" runat="server" Text='<%# Eval("CustACCNO") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="ReceiptNo" ItemStyle-HorizontalAlign="Right" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblReceiptNo" runat="server" Text='<%# Eval("ReceiptNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="OpeningDate" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOpeningDate" runat="server" Text='<%# Eval("OpeningDate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DueDate" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDueDate" runat="server" Text='<%# Eval("DueDate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Closing Balance" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblClosing" runat="server" Text='<%# Eval("ClosingBal") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="total" runat="server"></asp:Label>
                                        </FooterTemplate>
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

    <div id="DivRecords" runat="server" visible="false">
        <div class="row">
            <div class="col-md-12">
                <div class="portlet box blue" id="Div1">
                    <div class="portlet-title">
                        <div class="caption">
                            Investment Closure/Renewal
                        </div>
                    </div>
                    <div class="portlet-body form">
                        <div class="form-horizontal">
                            <div class="form-wizard">
                                <div class="form-body">
                                    <div class="tab-content">
                                        <div class="tab-pane active" id="tab1">
                                            <div class="row" style="margin: 7px 0 7px 0" align="center">
                                                <div class="col-lg-12">
                                                    <asp:RadioButtonList ID="rblType" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblType_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Selected="True" Value="1" style="padding: 5px">Investment Close</asp:ListItem>
                                                        <asp:ListItem Value="2" style="padding: 5px">Investment Renewal</asp:ListItem>
                                                        <asp:ListItem Value="3" style="padding: 5px">Interest Received</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                            <div class="row">

                                                <div class="row" style="margin: 7px 0 7px 14px; border-bottom: 1px solid rgba(53, 152, 220, 0.55);"><strong>Investment</strong></div>

                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">
                                                        <label class="control-label col-md-2">Prd Code</label>
                                                        <div class="col-md-1">
                                                            <asp:TextBox ID="TxtPRDNo" OnTextChanged="TxtPRDNo_TextChanged" Placeholder=" Product Code" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="TxtPRDName" OnTextChanged="TxtPRDName_TextChanged" CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                                                            <div id="CustList" style="height: 200px; overflow-y: scroll;"></div>
                                                            <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="TxtPRDName"
                                                                UseContextKey="true"
                                                                CompletionInterval="1"
                                                                CompletionSetCount="20"
                                                                MinimumPrefixLength="1"
                                                                EnableCaching="true"
                                                                ServicePath="~/WebServices/Contact.asmx"
                                                                ServiceMethod="getglname" CompletionListElementID="CustList">
                                                            </asp:AutoCompleteExtender>
                                                        </div>
                                                        <label class="control-label col-md-2">Int A/C No. </label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtIntAccNo" Enabled="false" Placeholder="A/C No." onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtIntAcName" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>


                                                    </div>
                                                </div>


                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">
                                                        <label class="control-label col-md-2">A/C No.</label>
                                                        <div class="col-md-1">
                                                            <asp:TextBox ID="TxtAccNo" runat="server" CssClass="form-control" OnTextChanged="TxtAccNo_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                            <%--     <asp:TextBox ID="" Placeholder="From A/C No." onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged=""></asp:TextBox>--%>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="TxtAccName" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <%--<asp:DropDownList ID="TxtAccNo" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="TxtAccNo_SelectedIndexChanged"></asp:DropDownList>--%>
                                                            <%--<div id="CustList2" style="height: 200px; overflow-y: scroll;"></div>
                                                        <asp:AutoCompleteExtender ID="AutoAccname" runat="server" TargetControlID="TxtAccName"
                                                            UseContextKey="true"
                                                            CompletionInterval="1"
                                                            CompletionSetCount="20"
                                                            MinimumPrefixLength="1"
                                                            EnableCaching="true"
                                                            ServicePath="~/WebServices/Contact.asmx"
                                                            ServiceMethod="GetInvAccName" CompletionListElementID="CustList2">
                                                        </asp:AutoCompleteExtender>--%>
                                                        </div>
                                                        <label class="control-label col-md-2">Close Type </label>
                                                        <div class="col-md-2">
                                                            <asp:DropDownList ID="Ddl_Remark" runat="server" OnTextChanged="Ddl_Remark_TextChanged" CssClass="form-control" AutoPostBack="true">
                                                                <asp:ListItem Text="--SELECT REMARK--" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="PREMATURE CLOSURE" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="AFTER MATURITY CLOSURE" Value="2"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                    </div>

                                                </div>

                                                <div class="row" style="margin: 7px 0 7px 0" runat="server" id="divtrf">
                                                    <div class="col-lg-12">
                                                        <label class="control-label col-md-2">Trf PrCd </label>
                                                        <div class="col-md-1">
                                                            <asp:TextBox ID="TxtTrfAccNo" OnTextChanged="TxtTrfAccNo_TextChanged" Placeholder="Prod Code" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="TxtTrfAccName" OnTextChanged="TxtTrfAccName_TextChanged" CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                                                            <div id="CustList1" style="height: 200px; overflow-y: scroll;"></div>
                                                            <asp:AutoCompleteExtender ID="autoglnametrf" runat="server" TargetControlID="TxtTrfAccName"
                                                                UseContextKey="true"
                                                                CompletionInterval="1"
                                                                CompletionSetCount="20"
                                                                MinimumPrefixLength="1"
                                                                EnableCaching="true"
                                                                ServicePath="~/WebServices/Contact.asmx"
                                                                ServiceMethod="getglname" CompletionListElementID="CustList1">
                                                            </asp:AutoCompleteExtender>
                                                        </div>
                                                        <label class="control-label col-md-2">Trf A/C No. </label>
                                                        <div class="col-md-1">
                                                            <asp:TextBox ID="txtTrfAcc" OnTextChanged="txtTtrfAcc_TextChanged" Placeholder="A/C No." onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin: 7px 0 7px 0; border-bottom: 1px solid rgba(53, 152, 220, 0.55);"><strong>Previous Certificate Details</strong></div>
                                            <div class="row" style="margin-bottom: 10px;">
                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">

                                                        <label class="control-label col-md-2">Investment Amt</label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtInAmt" Text="0" onkeypress="javascript:return isNumber (event)" PlaceHolder="Investment Amount" CssClass="form-control" runat="server" onblur="CalculateTotal()"></asp:TextBox>
                                                        </div>
                                                        <label class="control-label col-md-2">Cal Type</label>
                                                        <div class="col-md-2">
                                                            <asp:DropDownList ID="Ddl_IntType" OnTextChanged="Ddl_IntType_TextChanged" AutoPostBack="true" runat="server" CssClass="form-control">
                                                                <asp:ListItem Value="0">--select--</asp:ListItem>
                                                                <asp:ListItem Value="1">DD</asp:ListItem>
                                                                <asp:ListItem Value="2">MIS</asp:ListItem>
                                                                <asp:ListItem Value="3">CUM</asp:ListItem>
                                                                <asp:ListItem Value="4">FDS</asp:ListItem>
                                                                <asp:ListItem Value="5">DP</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <label class="control-label col-md-2">Last Int Date</label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtLastDate" onkeyup="FormatIt(this);" onkeypress="javascript:return isNumber(event)" placeholder="Last Int Date" CssClass="form-control" runat="server" />
                                                            <asp:CalendarExtender ID="txtLastDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtLastDate">
                                                            </asp:CalendarExtender>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">
                                                        <label class="control-label col-md-2">Date</label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtDate" onkeyup="FormatIt(this)" OnTextChanged="TxtDate_TextChanged" Placeholder="DD/MM/YYYY" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                                                        </div>
                                                        <label class="control-label col-md-2">Int Provision</label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtInt" Text="0" onkeypress="javascript:return isNumber (event)" PlaceHolder="Int Provision" CssClass="form-control" runat="server" onblur="CalculateTotal()"></asp:TextBox>
                                                        </div>


                                                    </div>
                                                </div>
                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">
                                                        <label class="control-label col-md-2">Rate</label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtROI" Text="0" OnTextChanged="TxtROI_TextChanged" onkeypress="javascript:return isNumber (event)" PlaceHolder="Rate" CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                                                        </div>
                                                        <label class="control-label col-md-2">Int Received</label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtIntCalc" Text="0" onkeypress="javascript:return isNumber (event)" PlaceHolder="Amount" CssClass="form-control" runat="server" onblur="CalculateTotal()"></asp:TextBox>
                                                        </div>
                                                        <label class="control-label col-md-2">Total int Calculation</label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtIntPaid" Text="0" onkeypress="javascript:return isNumber (event)" PlaceHolder="Pay Amount" CssClass="form-control" runat="server" onblur="CalculateTotal()"></asp:TextBox>
                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">
                                                        <label class="control-label col-md-2">Period (M/D)</label>
                                                        <div class="col-md-1">
                                                            <asp:TextBox ID="TxtMonths" Text="0" onkeypress="javascript:return isNumber (event)" PlaceHolder="Months" CssClass="form-control" runat="server" OnTextChanged="TxtROI_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-1">
                                                            <asp:TextBox ID="TxtDays" Text="0" onkeypress="javascript:return isNumber (event)" PlaceHolder="Days" CssClass="form-control" runat="server" OnTextChanged="TxtROI_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        </div>
                                                        <label class="control-label col-md-2">TDS Deduct</label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtTDSDeduct" Text="0" onkeypress="javascript:return isNumber (event)" PlaceHolder="Amount" CssClass="form-control" runat="server" onblur="CalculateTotal()"></asp:TextBox>
                                                        </div>
                                                        <label class="control-label col-md-2">Total</label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtTotalMaturity" Text="0" Enabled="false" onkeypress="javascript:return isNumber (event)" PlaceHolder="Amount" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">
                                                        <label class="control-label col-md-2">Maturity Amt</label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtintProvi" Text="0" onkeypress="javascript:return isNumber (event)" PlaceHolder="Maturity Amt" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                        <label class="control-label col-md-2">TDS to Provi</label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtTDSProvi" Text="0" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" PlaceHolder="Provision"></asp:TextBox>
                                                        </div>
                                                        <label class="control-label col-md-2">Net Amount</label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtNet" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" PlaceHolder="Net Amount"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" id="DivCerti" runat="server" visible="false">
                                                <div class="row" style="margin: 7px 0 7px 14px; border-bottom: 1px solid rgba(53, 152, 220, 0.55);"><strong>Certificate Details (New Investment)</strong></div>
                                                <div class="row" style="margin-bottom: 10px;">
                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">

                                                            <label class="control-label col-md-2">Investment Amount</label>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="TxtInstPrinci" Text="0" Placeholder="Amount" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" AutoPostBack="true" onblur="CalculateTotal1()"></asp:TextBox>
                                                            </div>
                                                            <label class="control-label col-md-2">Cal Type</label>
                                                            <div class="col-md-2">
                                                                <asp:DropDownList ID="ddlMddl" OnTextChanged="ddlMddl_TextChanged" AutoPostBack="true" runat="server" CssClass="form-control">
                                                                    <asp:ListItem Value="0">--select--</asp:ListItem>
                                                                    <asp:ListItem Value="1">DD</asp:ListItem>
                                                                    <asp:ListItem Value="2">MIS</asp:ListItem>
                                                                    <asp:ListItem Value="3">CUM</asp:ListItem>
                                                                    <asp:ListItem Value="4">FDS</asp:ListItem>
                                                                    <asp:ListItem Value="5">DP</asp:ListItem>

                                                                </asp:DropDownList>
                                                            </div>
                                                            <label class="control-label col-md-2">Int Type</label>
                                                            <div class="col-md-2">
                                                                <asp:DropDownList ID="ddlIntType" runat="server" CssClass="form-control"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-2">ASOff Date</label>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="TxtAsOffDate" onkeyup="FormatIt(this)" Placeholder="DD/MM/YYYY" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="TxtAsOffDate_TextChanged"></asp:TextBox>
                                                            </div>
                                                            <label class="control-label col-md-2">Int Provision</label>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtIntprovision" Text="0" onkeypress="javascript:return isNumber (event)" PlaceHolder="Int Provision" CssClass="form-control" runat="server" onblur="CalculateTotal1()"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-2">Int. Rate</label>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="TxtCROI" Text="0" onkeypress="javascript:return isNumber (event)" PlaceHolder="Rate" CssClass="form-control" runat="server" OnTextChanged="ddlMddl_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                            </div>
                                                            <label class="control-label col-md-2">Int Received</label>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtMIntCal" Text="0" onkeypress="javascript:return isNumber (event)" PlaceHolder="Amount" CssClass="form-control" runat="server" onblur="CalculateTotal1()"></asp:TextBox>
                                                            </div>
                                                            <label class="control-label col-md-2">Total Int Calculation</label>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtMIntPaid" Text="0" Enabled="false" PlaceHolder="Pay Amount" CssClass="form-control" runat="server" onblur="CalculateTotal1()"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-2">Period (M/D)</label>
                                                            <div class="col-md-1">
                                                                <asp:TextBox ID="TxtCMonths" Text="0" onkeypress="javascript:return isNumber (event)" PlaceHolder="Months" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="TxtAsOffDate_TextChanged"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-1">
                                                                <asp:TextBox ID="TxtCDays" Text="0" onkeypress="javascript:return isNumber (event)" PlaceHolder="Days" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="TxtAsOffDate_TextChanged"></asp:TextBox>
                                                            </div>
                                                        <label class="control-label col-md-2">TDS Deduct</label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtMTDS" Text="0" onkeypress="javascript:return isNumber (event)" PlaceHolder="Amount" CssClass="form-control" runat="server" onblur="CalculateTotal1()"></asp:TextBox>
                                                        </div>
                                                        <label class="control-label col-md-2">Net Amount</label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TextBox13" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" PlaceHolder="Net Amount" onblur="CalculateTotal1()"></asp:TextBox>
                                                        </div>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-2">Mat. Value</label>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="TxtMatValue" Text="0" onkeypress="javascript:return isNumber (event)" PlaceHolder="Maturity Amt" CssClass="form-control" runat="server" onblur="CalculateTotal1()"></asp:TextBox>
                                                            </div>
                                                            <%-- <label class="control-label col-md-2">Int to Provi</label>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="TextBox11" Text="0" onkeypress="javascript:return isNumber (event)" PlaceHolder="Provision" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>--%>
                                                            <label class="control-label col-md-2">TDS to Provi</label>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="TextBox12" Text="0" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" PlaceHolder="Provision"></asp:TextBox>
                                                            </div>
                                                            <label class="control-label col-md-2">Mature Date</label>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="TxtMatDate" Text="0" onkeyup="FormatIt(this)" Placeholder="DD/MM/YYYY" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="margin-bottom: 10px;">
                                                <div class="row" style="margin: 7px 0 7px 0; text-align: center" align="center">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-12">
                                                            <asp:Button ID="Btn_Voucher_View" runat="server" OnClick="Btn_Voucher_View_Click" Text="Voucher View" CssClass="btn btn-success" />
                                                            <asp:Button ID="Btn_Submit" OnClick="Btn_Submit_Click" OnClientClick="Javascript:return isvalidate();" runat="server" Text="Submit" CssClass="btn btn-primary" />
                                                            <asp:Button ID="Btn_ClearAll" OnClick="Btn_ClearAll_Click" runat="server" Text="Clear All" CssClass="btn btn-success" />
                                                            <asp:Button ID="Btn_Exit" runat="server" Text="Exit" CssClass="btn btn-success" />
                                                            <asp:Button ID="btn_Back" runat="server" OnClick="btn_Back_Click" Text="Back" CssClass="btn btn-success" />
                                                            <asp:Button ID="Btn_Provision" OnClick="Btn_Provision_Click" OnClientClick="Javascript:return isvalidate();" runat="server" Text="Provision Post" CssClass="btn btn-primary" />
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
        <div class="table-scrollable">
            <asp:Label ID="Label1" runat="server" Text="Receipt Details" Style="font-family: Verdana; font-size: medium;" Font-Bold="true"></asp:Label>
            <table class="table table-striped table-bordered table-hover">
                <thead>
                    <tr>
                        <th>
                            <asp:GridView ID="GridCerti" runat="server" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                EditRowStyle-BackColor="#FFFF99" PagerStyle-CssClass="pgr" Width="100%">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Date
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Eval("CertDate") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Amount
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Eval("PrincipleAmt") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Maturity Date
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Eval("MaturityDate") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Bal
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Eval("MainBalance") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Maturity Amt
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Eval("MaturityAmt") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Rate
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Eval("RateOfInt") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Period
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Eval("Months") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </th>
                    </tr>
                </thead>
            </table>
        </div>
    </div>


    <div id="VOUCHERVIEW" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg" style="width: 100%" align="center">

            <!-- Modal content-->
            <div class="modal-content" style="width: 1200px">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Entry Details Screen</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="portlet box green" id="Div2">
                                <div class="portlet-title">
                                    <div class="caption">
                                        Voucher View
                                    </div>
                                </div>
                                <div class="portlet-body form">
                                    <div class="form-horizontal">
                                        <div class="form-wizard">
                                            <div class="form-body">
                                                <div class="tab-content">
                                                    <div class="tab-pane active" id="Div3">
                                                        <div class="row" style="margin-bottom: 10px;">
                                                            <div class="row">
                                                                <div class="col-lg-12">

                                                                    <table class="table table-striped table-bordered table-hover">
                                                                        <thead>
                                                                            <tr>
                                                                                <th>
                                                                                    <asp:GridView ID="GrdVoucherView" runat="server" AllowPaging="True" BorderStyle="Double"
                                                                                        AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                                                        EditRowStyle-BackColor="#FFFF99"
                                                                                        PageIndex="10" PageSize="25"
                                                                                        PagerStyle-CssClass="pgr" Width="100%">
                                                                                        <Columns>
                                                                                            <asp:TemplateField HeaderText="SR NO." HeaderStyle-BackColor="#ff9966">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="ID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>



                                                                                            <asp:TemplateField HeaderText="Subglcode" HeaderStyle-BackColor="#ff9966">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="PRD" runat="server" Text='<%# Eval("SUBGLCODE") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="GLName" HeaderStyle-BackColor="#ff9966">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="GLName" runat="server" Text='<%# Eval("GLNAME") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="A/C No." Visible="true" HeaderStyle-BackColor="#ff9966">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="ACCNO" runat="server" Text='<%# Eval("ACCNO") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>


                                                                                            <asp:TemplateField HeaderText="A/C Name" HeaderStyle-BackColor="#ff9966">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="NAME" runat="server" Text='<%# Eval("NAME") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <%-- <asp:TemplateField HeaderText="Amount" HeaderStyle-BackColor="#ff9966">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="AMOUNT" runat="server" Text='<%# Eval("AMT") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>--%>

                                                                                            <asp:TemplateField HeaderText="CR" HeaderStyle-BackColor="#99ff99">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="CR" runat="server" Text='<%# Eval("CR") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="DR" HeaderStyle-BackColor="#99ff99">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="DR" runat="server" Text='<%# Eval("DR") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>

                                                                                            <asp:TemplateField HeaderText="MID" Visible="true" HeaderStyle-BackColor="#99ff99">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="MID" runat="server" Text='<%# Eval("MID") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="BRCD" Visible="true" HeaderStyle-BackColor="#99ff99">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="BRCD" runat="server" Text='<%# Eval("BRCD") %>'></asp:Label>
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
                                                <div class="row">

                                                    <div class="col-md-6">

                                                        <asp:Button ID="BtnExitView" runat="server" Text="Exit" CssClass="btn btn-primary" data-dismiss="modal" />
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
    <asp:HiddenField ID="hdnacc" runat="server" />
    <asp:HiddenField ID="hdnprd" runat="server" />
    <asp:HiddenField ID="hdnprdname" runat="server" />
</asp:Content>

