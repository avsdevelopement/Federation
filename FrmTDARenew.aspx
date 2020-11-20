<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmTDARenew.aspx.cs" Inherits="FrmTDARenew" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AVS In-So-Tech</title>
    <!-- BEGIN GLOBAL MANDATORY STYLES -->
    <link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&subset=all" rel="stylesheet" type="text/css">
    <link href="global/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css">
    <link href="global/plugins/simple-line-icons/simple-line-icons.min.css" rel="stylesheet" type="text/css">
    <link href="global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css">
    <link href="global/plugins/uniform/css/uniform.default.css" rel="stylesheet" type="text/css">
    <link href="global/plugins/bootstrap-switch/css/bootstrap-switch.min.css" rel="stylesheet" type="text/css" />
    <link href="global/css/components-rounded.css" rel="stylesheet" type="text/css" />
</head>
<script type="text/javascript">
    function sum() {
        var PRI = document.getElementById('<%=TxtACPayAmount.ClientID %>').value;
        var AMT = document.getElementById('<%=TxtWRAMT.ClientID %>').value;
        var result = parseInt(PRI) + parseInt(AMT);
        if (!isNaN(result)) {
            document.getElementById('<%=TxtTotalAmount.ClientID %>').value = result;
        }
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
<script type="text/javascript">
    function sumFD() {
        var PRI = document.getElementById('<%=TxtDiff.ClientID %>').value;
        var AMT = document.getElementById('<%=TxtDepoAmt.ClientID %>').value;
        var result = parseInt(PRI) - parseInt(AMT);
        if (!isNaN(result)) {
            document.getElementById('<%=TxtTotalAmount.ClientID %>').value = result;
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
</script>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <asp:UpdatePanel ID="Updatepanel" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12">
                        <div class="portlet box green">
                            <div class="portlet-title">
                                <div class="caption">
                                    Deposit Renewal
                                <asp:Literal ID="LtrlHeading" runat="server"></asp:Literal>
                                </div>
                                <div class="tools">
                                    <a href="javascript:;" class="collapse" data-original-title="" title=""></a>
                                    <a href="#portlet-config" data-toggle="modal" class="config" data-original-title="" title=""></a>
                                </div>
                            </div>

                            <div class="portlet-body">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Deposit Information : </strong></div>
                                    </div>
                                </div>
                                <div class="row" style="margin: 7px 0 7px 0;">
                                    <div class="col-lg-12">
                                        <label class="control-label col-md-2">Select Type : <span class="required">* </span></label>
                                        <div class="col-md-4">
                                            <asp:RadioButton ID="rdbPWINT" runat="server" Text="Principle With INT" GroupName="mat" AutoPostBack="true" OnCheckedChanged="rdbPWINT_CheckedChanged" />
                                            <asp:RadioButton ID="rdbOP" runat="server" Text="Only Principle" GroupName="mat" AutoPostBack="true" OnCheckedChanged="rdbOP_CheckedChanged" />
                                            <asp:RadioButton ID="rdbWR" runat="server" Text="With Receipt" GroupName="mat" AutoPostBack="true" OnCheckedChanged="rdbWR_CheckedChanged" />
                                        </div>
                                        <label class="control-label col-md-2">Payble Amount : <span class="required">* </span></label>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="TxtACPayAmount" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="margin: 7px 0 7px 0;" runat="server" id="WRPAYTP" visible="false">
                                    <div class="col-md-12">
                                        <label class="control-label col-md-2">Select Payment Type : <span class="required"></span></label>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="ddlPayType" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPayType_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="margin: 7px 0 7px 0;" runat="server" id="DIVWR" visible="false">
                                    <div class="col-lg-12">
                                        <label class="control-label col-md-2">Product Code : <span class="required">* </span></label>
                                        <div class="col-md-1">
                                            <asp:TextBox ID="TxtWRPRCD" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="TxtWRPRCD_TextChanged"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="TxtWRPRName" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="TxtWRPRName_TextChanged"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoWRPRD" runat="server" TargetControlID="TxtWRPRName"
                                                UseContextKey="true"
                                                CompletionInterval="1"
                                                CompletionSetCount="20"
                                                MinimumPrefixLength="1"
                                                EnableCaching="true"
                                                ServicePath="~/WebServices/Contact.asmx"
                                                ServiceMethod="getglname">
                                            </asp:AutoCompleteExtender>
                                        </div>
                                        <label class="control-label col-md-2">Account No :</label>
                                        <div class="col-md-1">
                                            <asp:TextBox ID="TxtWRAccNo" CssClass="form-control" onkeypress="javascript:return isNumber (event)" PlaceHolder="Receipt No" runat="server" AutoPostBack="true" OnTextChanged="TxtWRAccNo_TextChanged"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="TxtWRAccName" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="TxtWRAccName_TextChanged"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoWRAccName" runat="server" TargetControlID="TxtWRAccName"
                                                UseContextKey="true"
                                                CompletionInterval="1"
                                                CompletionSetCount="20"
                                                MinimumPrefixLength="1"
                                                EnableCaching="true"
                                                ServicePath="~/WebServices/Contact.asmx"
                                                ServiceMethod="GetAccName">
                                            </asp:AutoCompleteExtender>
                                        </div>
                                        <label class="control-label col-md-1">Balance :</label>
                                        <div class="col-md-1">
                                            <asp:TextBox ID="TxtBalance" CssClass="form-control" onkeypress="javascript:return isNumber (event)" PlaceHolder="Balace" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" id="AMT" visible="false" runat="server" style="margin: 7px 0 7px 0;">
                                    <div class="col-md-12">
                                        <label class="control-label col-md-2">Amount : <span class="required">* </span></label>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="TxtWRAMT" runat="server" onkeypress="javascript:return isNumber (event)" onkeyup="sum()" Text="0" CssClass="form-control" placeholder="Amount"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Button ID="SubmitR" runat="server" Text="Submit" CssClass="btn btn-success" OnClick="SubmitR_Click" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row" id="AMTDetail" runat="server" visible="false" style="margin: 7px 0 7px 0;">
                                    <div class="col-md-12">
                                        <label class="control-label col-md-2">Total Amount : <span class="required">* </span></label>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="TxtTotalAmount" runat="server" Text="0" CssClass="form-control" placeholder="Amount" Enabled="false"></asp:TextBox>
                                        </div>
                                        <label class="control-label col-md-2">Amount Transfer : <span class="required">* </span></label>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="TxtTransferAMT" runat="server" Text="0" CssClass="form-control" placeholder="Amount" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc"></strong></div>
                                    </div>
                                </div>
                                <div class="row" style="margin: 7px 0 7px 0;">
                                    <div class="col-lg-12">
                                        <label class="control-label col-md-2">Renewal Type : <span class="required">* </span></label>
                                        <div class="col-md-4">
                                            <asp:RadioButton ID="RdbSingle" runat="server" Text="Single" GroupName="SM" AutoPostBack="true" OnCheckedChanged="RdbSingle_CheckedChanged" />
                                            <asp:RadioButton ID="rdbMultiple" runat="server" Text="Multiple" GroupName="SM" AutoPostBack="true" OnCheckedChanged="rdbMultiple_CheckedChanged" />
                                        </div>
                                        <label class="control-label col-md-2">Total Amount : <span class="required">* </span></label>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="TxtAMTCOLL" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                        <label class="control-label col-md-1">Diff : <span class="required">* </span></label>
                                        <div class="col-md-1">
                                            <asp:TextBox ID="TxtDiff" runat="server" CssClass="form-control" Enabled="false" palceholder="DIFFRENCE"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="margin: 7px 0 7px 0;">
                                    <div class="col-lg-12">
                                        <label class="control-label col-md-2">Product Code : <span class="required">* </span></label>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="TxtProcode" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="TxtProcode_TextChanged"></asp:TextBox>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:DropDownList ID="ddlProductType" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlProductType_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <%--Div 1--%>
                                <div id="Depositdiv" runat="server">
                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Deposit Date: <span class="required"></span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="dtDeposDate" onkeyup="FormatIt(this);sumFD()" CssClass="form-control" PlaceHolder="dd/mm/yyyy" runat="server"></asp:TextBox>
                                            </div>
                                            <label class="control-label col-md-2">Interest Payout: <span class="required"></span></label>
                                            <div class="col-md-2">
                                                <asp:DropDownList ID="ddlIntrestPay" runat="server" CssClass="form-control" OnTextChanged="ddlIntrestPay_TextChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Deposit Amount : <span class="required"></span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="TxtDepoAmt" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" AutoPostBack="true" OnTextChanged="TxtDepoAmt_TextChanged"></asp:TextBox>
                                            </div>
                                            <label class="control-label col-md-2">Period : <span class="required"></span></label>
                                            <div class="col-md-2" style="margin-right: -24px;">
                                                <asp:DropDownList ID="ddlduration" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="M">Months</asp:ListItem>
                                                    <asp:ListItem Value="D">Days</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="TxtPeriod" CssClass="form-control" PlaceHolder="Period" runat="server" Style="width: 77px;" AutoPostBack="true" OnTextChanged="TxtPeriod_TextChanged"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Rate : <span class="required"></span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="TxtRate" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                            </div>
                                            <label class="control-label col-md-2">Interest Amount : <span class="required"></span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="TxtIntrest" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Maturity Amount :<span class="required"></span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="TxtMaturity" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                            </div>
                                            <label class="control-label col-md-2">Due Date :<span class="required"></span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="DtDueDate" CssClass="form-control" PlaceHolder="dd/mm/yyyy" runat="server" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Receipt No</label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="TxtReceiptNo" CssClass="form-control" PlaceHolder="Receipt No" runat="server" Enabled="true"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <div class="col-md-3">
                                                <h4><b>Transfer Account</b></h4>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin: 7px 0 7px 0; margin-top: 20px;">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-1" style="width: 165px">Product Code : <span class="required">* </span></label>
                                            <div class="col-md-1">
                                                <asp:TextBox ID="TxtProcode4" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="TxtProcode4_TextChanged"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="TxtProName4" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="TxtProName4_TextChanged"></asp:TextBox>
                                                <asp:AutoCompleteExtender ID="Autoprd4" runat="server" TargetControlID="TxtProName4"
                                                    UseContextKey="true"
                                                    CompletionInterval="1"
                                                    CompletionSetCount="20"
                                                    MinimumPrefixLength="1"
                                                    EnableCaching="true"
                                                    ServicePath="~/WebServices/Contact.asmx"
                                                    ServiceMethod="GetGlName">
                                                </asp:AutoCompleteExtender>
                                            </div>
                                            <label class="control-label col-md-1" style="width: 160px">Account No : <span class="required">* </span></label>
                                            <div class="col-md-1">
                                                <asp:TextBox ID="TxtAccNo4" CssClass="form-control" PlaceHolder="Account No /  Receipt No" runat="server" AutoPostBack="true" OnTextChanged="TxtAccNo4_TextChanged"></asp:TextBox>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="TxtAccName4" CssClass="form-control" Width="270px" Placeholder="Acc Name" runat="server" AutoPostBack="true" OnTextChanged="TxtAccName4_TextChanged"></asp:TextBox>
                                                <asp:AutoCompleteExtender ID="Autoaccname4" runat="server" TargetControlID="TxtAccName4"
                                                    UseContextKey="true"
                                                    CompletionInterval="1"
                                                    CompletionSetCount="20"
                                                    MinimumPrefixLength="1"
                                                    EnableCaching="true"
                                                    ServicePath="~/WebServices/Contact.asmx"
                                                    ServiceMethod="GetAccName">
                                                </asp:AutoCompleteExtender>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin: 07px 0 1px 0">
                                        <div class="col-lg-12">
                                            <div class="col-md-2">
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Button ID="BtnSubmit" runat="server" CssClass="btn green" Text="Submit" OnClientClick="javascript:return validate();" OnClick="BtnSubmit_Click" />
                                            </div>
                                            <div class="col-md-2" id="Div_SPost" runat="server">
                                                <asp:Button ID="PostEntry" runat="server" Visible="false" CssClass="btn blue" Text="Post Deposit" OnClientClick="javascript:return validate();" OnClick="PostEntry_Click" />
                                            </div>
                                            <div class="col-md-2" id="Div_MPost" runat="server" >
                                                <asp:Button ID="BtnPostMultiple" runat="server" OnClick="BtnPostMultiple_Click" Visible="false" CssClass="btn blue" Text="Post Cl.Multiple " OnClientClick="javascript:return validate();"/>
                                            </div>
                                        </div>
                                    </div>
                                </div>
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
                                            <asp:GridView ID="GrdFDLedger" runat="server" AllowPaging="True"
                                                AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                EditRowStyle-BackColor="#FFFF99"
                                                OnPageIndexChanging="GrdFDLedger_PageIndexChanging"
                                                PageIndex="10" PageSize="25"
                                                PagerStyle-CssClass="pgr" Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Product code" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="PRD" runat="server" Text='<%# Eval("SUBGLCODE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="ACCNO">
                                                        <ItemTemplate>
                                                            <asp:Label ID="ACC" runat="server" Text='<%# Eval("ACCNO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="AMOUNT">
                                                        <ItemTemplate>
                                                            <asp:Label ID="AMT" runat="server" Text='<%# Eval("AMOUNT") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="CR/DR">
                                                        <ItemTemplate>
                                                            <asp:Label ID="TRXTYPE" runat="server" Text='<%# Eval("TRXTYPE") %>'></asp:Label>
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
    </form>
</body>
</html>
