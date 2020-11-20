<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmCheckACClose.aspx.cs" Inherits="FrmCheckACClose" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function FormatIt(obj) {
            if (obj.value.length == 2)  //Dd
                obj.value = obj.value + "/";
            if (obj.value.length == 5)  //mm
                obj.value = obj.value + "/";
            if (obj.value.length == 11)  //yyyy
                alert("Please Enter Valid Date");
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
    </script>
    <script type="text/javascript">
        function IsTally() {
            var Clear_bal = document.getElementById('<%=TxtClearBal.ClientID%>').value;
            var Total_bal = document.getElementById('<%=TxtTotalBal.ClientID%>').value;
            var Res;

            Res = Clear_bal - Total_bal;
            if (Res != 0) {
                return false;
            }
            else {
                return true;
            }

        }
    </script>
    <script type="text/javascript">
        function sum() {
            
            var MaxCHR = document.getElementById('<%=TxtMaxC.ClientID%>').value;
            var INTAP = document.getElementById('<%=TxtIntApp.ClientID%>').value;
            var UCC = document.getElementById('<%=TxtUnsedChqChrges.ClientID%>').value;
            var SC = document.getElementById('<%=TxtServChrgs.ClientID%>').value;
            var ECC = document.getElementById('<%=TxtEarlyClose.ClientID%>').value;
            var TST = document.getElementById('<%=TxtTotalTax.ClientID%>').value;
            var TCESS = document.getElementById('<%=TxtTotalCease.ClientID%>').value;
            var TBCTT = document.getElementById('<%=TxtBCCT.ClientID%>').value;
            var TotalBal = document.getElementById('<%=TxtTotalBal.ClientID%>').value;
            var OCHR = document.getElementById('<%=TxtOtherChrgs.ClientID%>').value;
            var CC = document.getElementById('<%=TxtCrInt.ClientID%>').value;
            
            var Result,Result1;

            Result = (parseFloat(TotalBal) + parseFloat(INTAP)) - (parseFloat(MaxCHR) + parseFloat(SC) + parseFloat(ECC) + parseFloat(TST) + parseFloat(TCESS) + parseFloat(TBCTT) + parseFloat(UCC) + parseFloat(OCHR) + parseFloat(CC));//
            //Result1 = 
               
            if (!isNaN(Result)) {
                document.getElementById('<%=TxtFinal.ClientID%>').value = Result;
            }
        }
                                    
    </script>
    <script type="text/javascript">

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
                                Check In A/C Close
                            </div>
                        </div>
                        <div class="portlet-body form">
                            <div class="form-horizontal">
                                <div class="form-wizard">
                                    <div class="form-body">
                                        <div class="tab-content">
                                            <div class="tab-pane active" id="tab__blue">
                                            </div>
                                            <div style="border: 1px solid #3598dc">

                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-12" style="border-bottom: 2px solid #3598dc;"><strong style="color: #3598dc">A/C DETAILS</strong></div>
                                                    </div>
                                                </div>

                                                <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                    <div class="col-lg-12">
                                                        <label class="control-label col-md-2">Product Type <span class="required">* </span></label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtProcode" PLACEHOLDER="PRODUCT TYPE" CssClass="form-control" runat="server" AutoPostBack="true" onkeypress="javascript:return isNumber (event)" Style="Width: 75%; height: 28px; border: 1px solid #c0c1c1;" OnTextChanged="TxtProcode_TextChanged"></asp:TextBox>
                                                            <%--<asp:TextBox ID="TextBox1" CssClass="form-control" runat="server"  AutoPostBack="true" OnTextChanged="TxtProcode_TextChanged" ></asp:TextBox>--%>
                                                        </div>
                                                        <div class="col-md-5">
                                                            <asp:TextBox ID="TxtProName" placeholder="PRODUCT NAME" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="TxtProName_TextChanged"></asp:TextBox>
                                                            <div id="CustList" style="height: 200px; overflow-y: scroll;"></div>
                                                            <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="TxtProName"
                                                                UseContextKey="true"
                                                                CompletionInterval="1"
                                                                CompletionSetCount="20"
                                                                MinimumPrefixLength="1"
                                                                EnableCaching="true"
                                                                ServicePath="~/WebServices/Contact.asmx"
                                                                ServiceMethod="getglname" CompletionListElementID="CustList">
                                                            </asp:AutoCompleteExtender>
                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                    <div class="col-lg-12">
                                                        <label class="control-label col-md-2">Account No.<span class="required">* </span></label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtAccNo" placeholder="ACCOUNT NO" CssClass="form-control" runat="server" AutoPostBack="true" onkeypress="javascript:return isNumber (event)" Style="Width: 75%; height: 28px; border: 1px solid #c0c1c1;" OnTextChanged="TxtAccNo_TextChanged"></asp:TextBox>
                                                            <%--<asp:TextBox ID="TextBox1" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="TxtAccNo_TextChanged"></asp:TextBox>--%>
                                                        </div>
                                                        <div class="col-md-5">
                                                            <asp:TextBox ID="TxtAccName" placeholder="ACCOUNT NAME" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="TxtAccName_TextChanged"></asp:TextBox>
                                                            <div id="CustList2" style="height: 200px; overflow-y: scroll;"></div>
                                                            <asp:AutoCompleteExtender ID="AutoAccname" runat="server" TargetControlID="TxtAccName"
                                                                UseContextKey="true"
                                                                CompletionInterval="1"
                                                                CompletionSetCount="20"
                                                                MinimumPrefixLength="1"
                                                                EnableCaching="true"
                                                                ServicePath="~/WebServices/Contact.asmx"
                                                                ServiceMethod="GetAccName" CompletionListElementID="CustList2">
                                                            </asp:AutoCompleteExtender>
                                                        </div>
                                                    </div>
                                                </div>


                                                <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                    <div class="col-lg-12">
                                                        <label class="control-label col-md-2">Status<span class="required">* </span></label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtACStatus" CssClass="form-control" PlaceHolder="A/C Status" runat="server"></asp:TextBox>
                                                        </div>
                                                        <label class="control-label col-md-2">Open Date<span class="required"></span></label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtOpenDate" CssClass="form-control" onkeyup="FormatIt(this)" PlaceHolder="dd/mm/yyyy" runat="server"></asp:TextBox>
                                                            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtOpenDate">
                                                            </asp:TextBoxWatermarkExtender>
                                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtOpenDate">
                                                            </asp:CalendarExtender>
                                                        </div>
                                                        <label class="control-label col-md-2">Cust No<span class="required">* </span></label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtCustNo" CssClass="form-control" PlaceHolder="Cust No" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-12" style="border-bottom: 2px solid #3598dc;"><strong style="color: #3598dc"></strong></div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-12" style="border-bottom: 2px solid #3598dc;"><strong style="color: #3598dc">BALANCE SUMMARY</strong></div>
                                                    </div>
                                                </div>

                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-11">
                                                        <label class="control-label col-md-3">Clear Balance<span class="required">* </span></label>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="TxtClearBal" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" placeholder="Clear Balance"></asp:TextBox>
                                                        </div>
                                                        <label class="control-label col-md-2">Total Balance <span class="required"></span></label>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="TxtTotalBal" onkeypress="javascript:return isNumber (event)" placeholder="Total Balance" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>


                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-12" style="border-bottom: 2px solid #3598dc;"><strong style="color: #3598dc"></strong></div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-12" style="border-bottom: 1px solid  #3598dc;"><strong style="color: #3598dc">INSTRUMENT DETAILS</strong></div>
                                                    </div>
                                                </div>

                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-11">
                                                        <label class="control-label col-md-3">Start No.<span class="required">* </span></label>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="TxtStartNo" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" placeholder="Start Cheque No"></asp:TextBox>
                                                        </div>
                                                        <label class="control-label col-md-2">End No.<span class="required"></span></label>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="TxtEndNo" onkeypress="javascript:return isNumber (event)" placeholder="End Cheque No" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-11">
                                                        <label class="control-label col-md-3">Book Size<span class="required">* </span></label>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="TxtBoooksize" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" placeholder="Total size"></asp:TextBox>
                                                        </div>
                                                        <label class="control-label col-md-2">Unused Count<span class="required"></span></label>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="TxtBookUnused" onkeypress="javascript:return isNumber (event)" placeholder="Unused No" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-12" style="border-bottom: 2px solid #3598dc;"><strong style="color: #3598dc"></strong></div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-12" style="border-bottom: 1px solid #3598dc;"><strong style="color: #3598dc">INSTRUMENT CHARGES</strong></div>
                                                    </div>
                                                </div>


                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-11">
                                                        <label class="control-label col-md-3">Unused Cheques<span class="required"></span></label>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="TxtUnusedCheque" onkeypress="javascript:return isNumber (event)" placeholder="No. of Unsed Cheque" CssClass="form-control" runat="server" OnTextChanged="TxtUnusedCheque_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        </div>
                                                        <label class="control-label col-md-2">Charges per Cheque<span class="required">* </span></label>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="TxtChrgsCheque" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" placeholder="Charges"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-11">
                                                        <label class="control-label col-md-3">MAX CHARGES<span class="required"></span></label>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="TxtMaxC" Text="0" onkeyup="sum();" placeholder="Max Charges" CssClass="form-control" runat="server"></asp:TextBox>
                                                            <%--<asp:TextBox ID="TxtMaxCharges" Text="0" onkeyup="sum()" placeholder="Max Cahrges" CssClass="form-control" runat="server"></asp:TextBox>--%>
                                                        </div>
                                                        <label class="control-label col-md-2">OTHER CHARGES<span class="required"></span></label>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="TxtOtherChrgs" Text="0" onkeyup="sum();" placeholder="Other Charges" CssClass="form-control" runat="server"></asp:TextBox>

                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-12" style="border-bottom: 2px solid #3598dc;"><strong style="color: #3598dc"></strong></div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-12" style="border-bottom: 1px solid  #3598dc;"><strong style="color: #3598dc">CHARGES APPLIED</strong></div>
                                                    </div>
                                                </div>


                                                <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                    <div class="col-lg-12">
                                                        <label id="Lbl_IntApp" runat="server" class="control-label col-md-2">Interest Applied<span class="required">* </span></label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtIntApp" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" PlaceHolder="Interest" Text="0" Style="Width: 75%; height: 33px; border: 1px solid #c0c1c1;" onkeyup="sum()"></asp:TextBox>
                                                        </div>
                                                        <label id="Lbl_UCC" runat="server" class="control-label col-md-2">Unused Cheque Chrgs<span class="required">* </span></label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtUnsedChqChrges" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" PlaceHolder="Charges" Text="0" Style="Width: 75%; height: 33px; border: 1px solid #c0c1c1;" onkeyup="sum()"></asp:TextBox>
                                                        </div>
                                                        <label class="control-label col-md-2">LAST INT DATE<span class="required">* </span></label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtLastIntDate" CssClass="form-control" runat="server" PlaceHolder="Charges" onkeypress="javascript:return isNumber (event)" Text="0" Style="Width: 75%; height: 33px; border: 1px solid #c0c1c1;"></asp:TextBox>
                                                        </div>
                                                    </div>


                                                    <div class="col-lg-12">
                                                        <label id="Lbl_SC" runat="server" class="control-label col-md-2">Service Charges<span class="required">* </span></label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtServChrgs" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" PlaceHolder="Charges" Text="0" Style="Width: 75%; height: 33px; border: 1px solid #c0c1c1;" onkeyup="sum()"></asp:TextBox>
                                                        </div>
                                                        <label id="Lbl_ECC" runat="server" class="control-label col-md-2">Early Closure Charges<span class="required">* </span></label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtEarlyClose" CssClass="form-control" runat="server" PlaceHolder="Charges" onkeypress="javascript:return isNumber (event)" Text="0" Style="Width: 75%; height: 33px; border: 1px solid #c0c1c1;" onkeyup="sum()"></asp:TextBox>
                                                        </div>
                                                        <label id="Lbl_ST" runat="server" class="control-label col-md-2">Total Service Tax<span class="required">* </span></label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtTotalTax" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" PlaceHolder="Charges" Text="0" Style="Width: 75%; height: 33px; border: 1px solid #c0c1c1;" onkeyup="sum()"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="col-lg-12">
                                                        <label id="Lbl_CA" runat="server" class="control-label col-md-2">Total Cess Amt<span class="required">* </span></label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtTotalCease" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" PlaceHolder="Charges" Text="0" Style="Width: 75%; height: 33px; border: 1px solid #c0c1c1;" onkeyup="sum()"></asp:TextBox>
                                                        </div>
                                                        <label id="Lbl_BC" runat="server" class="control-label col-md-2">Total BCTT Charges<span class="required">* </span></label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtBCCT" CssClass="form-control" runat="server" PlaceHolder="Charges" onkeypress="javascript:return isNumber (event)" Text="0" Style="Width: 75%; height: 33px; border: 1px solid #c0c1c1;" onkeyup="sum()"></asp:TextBox>
                                                        </div>
                                                        <label class="control-label col-md-2">FINAL BALANCE<span class="required">* </span></label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtFinal" CssClass="form-control" Text="0" runat="server" onkeypress="javascript:return isNumber (event)" PlaceHolder="Final Balance" Style="Width: 75%; height: 33px; border: 1px solid #c0c1c1;"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-12" style="border-bottom: 2px solid #3598dc;"><strong style="color: #3598dc"></strong></div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-12" style="border-bottom: 1px solid  #3598dc;"><strong style="color: #3598dc">OTHER DETAILS</strong></div>
                                                    </div>
                                                </div>


                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">
                                                        <label class="control-label col-md-2">Cr.Int (CC)</label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtCrInt" Text="0" onkeyup="sum()" CssClass="form-control" runat="server" placeholder=""></asp:TextBox>
                                                        </div>
                                                        <label class="control-label col-md-2">S.I.</label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtSI" CssClass="form-control" runat="server" placeholder=""></asp:TextBox>
                                                        </div>

                                                    </div>
                                                </div>

                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">
                                                        <label class="control-label col-md-2">Closure Reason </label>
                                                        <div class="col-lg-8">
                                                            <asp:TextBox ID="TxtReason" placeholder="ENTER REASON HERE" runat="server" CssClass="form-control" Style="text-transform: uppercase" TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>


                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-12" style="border-bottom: 2px solid #3598dc;"><strong style="color: #3598dc"></strong></div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-12" style="border-bottom: 1px solid  #3598dc;"><strong style="color: #3598dc">TRANSACTION</strong></div>
                                                    </div>
                                                </div>


                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">
                                                        <label class="control-label col-md-2">Voucher Type<span class="required"></span></label>
                                                        <div class="col-md-4">
                                                            <asp:DropDownList ID="Ddl_type" CssClass="form-control" runat="server" OnSelectedIndexChanged="Ddl_type_SelectedIndexChanged" AutoPostBack="true">
                                                                <asp:ListItem Value="0" Text="--Select Type--"></asp:ListItem>
                                                                <asp:ListItem Value="1" Text="CASH"></asp:ListItem>
                                                                <asp:ListItem Value="2" Text="CHEQUE"></asp:ListItem>
                                                                <asp:ListItem Value="3" Text="TRANSFER"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>



                                                <div id="DIV_TPRDACC" runat="server" class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                    <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-2">Product Type <span class="required">* </span></label>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="TxtTProcode" PLACEHOLDER="PRODUCT TYPE" CssClass="form-control" runat="server" AutoPostBack="true" onkeypress="javascript:return isNumber (event)" Style="Width: 75%; height: 28px; border: 1px solid #c0c1c1;" OnTextChanged="TxtTProcode_TextChanged"></asp:TextBox>
                                                                <%--<asp:TextBox ID="TextBox1" CssClass="form-control" runat="server"  AutoPostBack="true" OnTextChanged="TxtProcode_TextChanged" ></asp:TextBox>--%>
                                                            </div>
                                                            <div class="col-md-5">
                                                                <asp:TextBox ID="TxtTPName" placeholder="PRODUCT NAME" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="TxtTPName_TextChanged"></asp:TextBox>
                                                                <div id="CustList4" style="height: 200px; overflow-y: scroll;"></div>
                                                                <asp:AutoCompleteExtender ID="AutoTGlName" runat="server" TargetControlID="TxtTPName"
                                                                    UseContextKey="true"
                                                                    CompletionInterval="1"
                                                                    CompletionSetCount="20"
                                                                    MinimumPrefixLength="1"
                                                                    EnableCaching="true"
                                                                    ServicePath="~/WebServices/Contact.asmx"
                                                                    ServiceMethod="getglname" CompletionListElementID="CustList4">
                                                                </asp:AutoCompleteExtender>
                                                            </div>

                                                        </div>
                                                    </div>
                                                     <div id="DIV1" runat="server" class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-2">Instrument No.<span class="required">* </span></label>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="TxtInstNo" placeholder="CHEQUE NO" CssClass="form-control" runat="server" AutoPostBack="true" onkeypress="javascript:return isNumber (event)" Style="Width: 75%; height: 28px; border: 1px solid #c0c1c1;"></asp:TextBox>
                                                                <%--<asp:TextBox ID="TextBox1" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="TxtAccNo_TextChanged"></asp:TextBox>--%>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="TxtInstruDate" placeholder="DD/MM/YY" onkeypress="javascript:return isNumber (event)" onkeyup="FormatIt(this)" CssClass="form-control" runat="server"></asp:TextBox>
                                                                </div>
                                                        </div>
                                                    </div>
                                                    <div id="DIV_TACC" runat="server" class="row" style="margin-top: 12px; margin-bottom: 10px;">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-2">Account No.<span class="required">* </span></label>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="TxtTAccno" placeholder="ACCOUNT NO" CssClass="form-control" runat="server" AutoPostBack="true" onkeypress="javascript:return isNumber (event)" Style="Width: 75%; height: 28px; border: 1px solid #c0c1c1;" OnTextChanged="TxtTAccno_TextChanged"></asp:TextBox>
                                                                <%--<asp:TextBox ID="TextBox1" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="TxtAccNo_TextChanged"></asp:TextBox>--%>
                                                            </div>
                                                            <div class="col-md-5">
                                                                <asp:TextBox ID="TxtTAccName" placeholder="ACCOUNT NAME" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="TxtTAccName_TextChanged"></asp:TextBox>
                                                                <div id="Div2" style="height: 200px; overflow-y: scroll;"></div>
                                                                <asp:AutoCompleteExtender ID="AutoTAccName" runat="server" TargetControlID="TxtTAccName"
                                                                    UseContextKey="true"
                                                                    CompletionInterval="1"
                                                                    CompletionSetCount="20"
                                                                    MinimumPrefixLength="1"
                                                                    EnableCaching="true"
                                                                    ServicePath="~/WebServices/Contact.asmx"
                                                                    ServiceMethod="GetAccName" CompletionListElementID="Div2">
                                                                </asp:AutoCompleteExtender>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-actions">
                                                    <div class="row">
                                                        <div class="col-md-offset-3 col-md-9">
                                                            <asp:Button ID="Btn_Submit" runat="server" onkeypress="IsTally();" CssClass="btn blue" Text="Submit" OnClick="Btn_Submit_Click" />
                                                            <asp:Button ID="Btn_Receipt" runat="server" CssClass="btn blue" Text="Receipt" OnClick="Btn_Receipt_Click" Visible="false" />
                                                            <asp:Button ID="Btn_btnClear" runat="server" Text="Clear All" CssClass="btn btn-primary" OnClick="Btn_btnClear_Click" />
                                                            &nbsp;<asp:Button ID="Btn_Exit" runat="server" Text="Exit" CssClass="btn btn-primary" OnClick="Btn_Exit_Click" />
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

        </ContentTemplate>
    </asp:UpdatePanel>
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

