<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmCancelEntryMain.aspx.cs" Inherits="FrmCancelEntryMain" %>
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
            if (obj.value.length == 2)//date
                obj.value = obj.value + "/";
            if (obj.value.length == 5)//month
                obj.value = obj.value + "/";
            if (obj.value.length == 11)//month
                alert("Please enter valid date!...");

        }

    </script>
    <style>
        #table2 td {
            border: solid thin black;
        }

        #td1 td {
            border: solid thin black;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        Cancel Entry
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab__blue">
                                        <div class="row" style="margin-bottom: 10px;">
                                            <div class="col-lg-6">
                                                <div class="col-md-3">
                                                    <asp:RadioButtonList ID="Rdb_EntryType" runat="server" RepeatDirection="Horizontal" Style="width: 800px;" Visible="false">
                                                        <asp:ListItem Text="Other Entry Cancel" Value="1" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Deposit Entry Cancel" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="Loan Inst Cancel" Value="3"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0px 7px 0px">
                                            <div class="col-md-12">
                                                <div class="col-md-1" style="width: 70px">
                                                    <asp:Label ID="lblVN" runat="server" Text="Voucher No"></asp:Label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtSetNo" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-md-1" style="width: 120px">
                                                    <asp:Label ID="lblPrdCD" runat="server" Text="Entry Date"></asp:Label>
                                                </div>
                                                <div class="col-md-2" style="width: 150px">
                                                    <asp:TextBox ID="TxtEntryDate" runat="server" placeholder="Product Code" CssClass="form-control"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #fa4e0d"></strong></div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0px 7px 0px">
                                            <div class="col-lg-offset-1" style="text-align: center">
                                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn blue" Text="Submit" OnClientClick="Javascript:return isvalidate();" OnClick="btnSubmit_Click" />
                                                &nbsp;<asp:Button ID="Exit" OnClick="Exit_Click" runat="server" CssClass="btn blue" Text="Exit" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="table-scrollable" style="width: 100%; height: 500px; overflow-x: auto; overflow-y: auto">
                                                    <table class="table table-striped table-bordered table-hover">
                                                        <thead>
                                                            <tr>
                                                                <th>
                                                                    <asp:GridView ID="grdShow" runat="server"
                                                                        AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                                        EditRowStyle-BackColor="#FFFF99" Width="100%">
                                                                        <Columns>

                                                                            <asp:TemplateField HeaderText="VOUCHER NO" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="SET_NO" runat="server" Text='<%# Eval("SETNO") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="A/C TYPE" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="SUBGLCODE" runat="server" Text='<%# Eval("SUBGLCODE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="A/C Name" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="GLNAME" runat="server" Text='<%# Eval("GLNAME") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="A/C NO" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="ACCNO" runat="server" Text='<%# Eval("ACCNO") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="CUST NAME" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="CUSTNAME" runat="server" Text='<%# Eval("CUSTNAME") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="PARTICULARS" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="PARTICULARS" runat="server" Text='<%# Eval("PARTICULARS") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Cr AMOUNT" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="CREDIT" runat="server" Text='<%# Eval("CREDIT") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Dr AMOUNT" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="DEBIT" runat="server" Text='<%# Eval("DEBIT") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="MID" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="MID" runat="server" Text='<%# Eval("MID") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="ENTRYDATE" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="ENTRYDATE" runat="server" Text='<%# Eval("ENTRYDATE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="MAKER" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="LOGINCODE" runat="server" Text='<%# Eval("LOGINCODE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Cancel" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Eval("ScrollNo")%>' CommandName="select" class="glyphicon glyphicon-trash" OnClick="lnkDelete_Click"></asp:LinkButton>
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
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="DDSCLOSE" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg" style="width: 80%">
            <!-- Modal content-->
            <div class="modal-content" style="width: 100%">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">DDS Closure Authorization</h4>
                </div>
                <div class="modal-body">

                    <div class="row">
                        <div class="col-md-12">
                            <div class="portlet box green">
                                <div class="portlet-title">
                                    <div class="caption">
                                        DDS Closure
                                <asp:Literal ID="LtrlHeading" runat="server"></asp:Literal>
                                    </div>
                                    <div class="tools">
                                        <a href="javascript:;" class="collapse" data-original-title="" title=""></a>
                                        <a href="#portlet-config" data-toggle="modal" class="config" data-original-title="" title=""></a>
                                    </div>
                                </div>

                                <div class="portlet-body">
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="vertical-align: top; width: 50%">
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Customer Information : </strong></div>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin: 7px 0 7px 0; margin-top: 10px;">
                                                    <div class="col-md-12" style="height: 33px">
                                                        <label class="control-label col-md-4">Agent Code : <span class="required"></span></label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="TxtDDSAGCD" Enabled="false" runat="server" onkeypress="javascript:return isNumber (event)" PlaceHolder="Agent Code" AutoPostBack="true" Style="width: 100%; height: 33px;" CssClass="form-control" ReadOnly="true"></asp:TextBox>&nbsp;&nbsp;                               
                                                   

                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="row" style="margin: 7px 0 7px 0; margin-top: 10px;">
                                                    <div class="col-md-12">
                                                        <label class="control-label col-md-4"><span class="required"></span></label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="TxtDDSAGName" Enabled="false" runat="server" PlaceHolder="Agent Name" CssClass="form-control" AutoPostBack="true" Style="width: 100%; height: 33px;" ReadOnly="true"></asp:TextBox>
                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="row" style="margin: 7px 0 7px 0; margin-top: 10px;">
                                                    <div class="col-md-12" style="height: 33px">
                                                        <label class="control-label col-md-4">Account No : <span class="required"></span></label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="TxtDDSAccNo" Enabled="false" runat="server" PlaceHolder="Account No" onkeypress="javascript:return isNumber (event)" AutoPostBack="true" Style="width: 100%; height: 33px;" CssClass="form-control" ReadOnly="true"></asp:TextBox>&nbsp;&nbsp;
                                 
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin: 7px 0 7px 0; margin-top: 10px;">
                                                    <div class="col-md-12">
                                                        <label class="control-label col-md-4"><span class="required"></span></label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="TxtDDSAccName" runat="server" Enabled="false" PlaceHolder="Account Holder Name" AutoPostBack="true" Style="width: 100%; height: 33px;" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin: 7px 0 7px 0; margin-top: 10px;">
                                                    <div class="col-lg-12" style="width: 100%; height: 33px">
                                                        <label class="control-label col-md-4">Opening Date: </label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="TxtDDSOpeningDate" runat="server" PlaceHolder="Openig Date" Enabled="false" Style="width: 100%; height: 33px;" CssClass="form-control" ReadOnly="true"></asp:TextBox>&nbsp;&nbsp;                                                                                                            
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin: 7px 0 7px 0; margin-top: 10px;">
                                                    <div class="col-lg-12" style="height: 33px">
                                                        <label class="control-label col-md-4">Deposit Amount </label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="txtDepositAmt" runat="server" PlaceHolder="Deposit Amount" Enabled="false" Style="width: 100%; height: 33px;" CssClass="form-control" ReadOnly="true"></asp:TextBox>&nbsp;&nbsp;                                                                                                            
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                            <td style="width: 50%">
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Payment Details : </strong></div>
                                                    </div>
                                                </div>
                                                <table id="table2" style="width: 70%; border: 1px solid #000000; border-collapse: collapse; margin-top: 10px" align="center">
                                                    <tr>
                                                        <td style="width: 70%">&nbsp;&nbsp;&nbsp;<asp:Label ID="Label1" runat="server" Text="Closing Balance :"></asp:Label>
                                                        </td>
                                                        <td style="width: 30%" align="right">
                                                            <asp:Label ID="TxtDDSCBal" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                                            <%--<asp:TextBox ID="TxtDDSCBal" ReadOnly="true" Enabled="false" runat="server" PlaceHolder="Closing Balance" onkeypress="javascript:return isNumber (event)" Style="height: 33px; border: 1px solid #c0c1c1;"></asp:TextBox>&nbsp;&nbsp;--%>                                                    
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;&nbsp;&nbsp;
                                                            <asp:Label ID="Label2" runat="server" Text="Int Amount :"></asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="TxtDDSCalcInt" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                                            <%--<asp:TextBox ID="TxtDDSCalcInt" ReadOnly="true" Enabled="false" runat="server" Text="0" PlaceHolder="Interest" onkeyup="MINUS();Sum()" AutoPostBack="true" Style="height: 33px; border: 1px solid #c0c1c1;"></asp:TextBox>--%>
                                                        </td>
                                                    </tr>
                                                    <tr style="background-color: #66FF99">
                                                        <td>&nbsp;&nbsp;&nbsp;<asp:Label ID="lblTotal" runat="server" Text="Total Amount :"></asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="TxtDDSPayAmt" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                                            <%--<asp:TextBox ID="TxtDDSPayAmt" ReadOnly="true" Enabled="false" runat="server" Text="0" PlaceHolder="Total Amount" onkeyup="MINUS();Sum()" AutoPostBack="true" Style="height: 33px; border: 1px solid #c0c1c1;"></asp:TextBox>--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;&nbsp;&nbsp;
                                                            <asp:Label ID="lblpreCommission" runat="server" Text="Commission :"></asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="TxtDDSPreMCAMT" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                                            <%--<asp:TextBox ID="TxtDDSPreMCAMT" ReadOnly="true" Enabled="false" runat="server" PlaceHolder="Commission" Style="height: 33px; border: 1px solid #c0c1c1;"></asp:TextBox>--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;&nbsp;&nbsp;
                                                            <asp:Label ID="lblAdmin" runat="server" Text="Admin/Chg :"></asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="TxtDDSServCHRS" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                                            <%--<asp:TextBox ID="TxtDDSServCHRS" ReadOnly="true" Enabled="false" runat="server" PlaceHolder="Admin/Chg" Style="height: 33px; border: 1px solid #c0c1c1;"></asp:TextBox>--%>
                                                        </td>
                                                    </tr>
                                                    <tr style="background-color: #66FF99">
                                                        <td>&nbsp;&nbsp;&nbsp;<asp:Label ID="lblTDeduction" runat="server" Text="Total Deduction :"></asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="txtDeduction" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                                            <%--<asp:TextBox ID="txtDeduction" ReadOnly="true" Enabled="false" runat="server" PlaceHolder="Total Deduction" Style="height: 33px; border: 1px solid #c0c1c1;"></asp:TextBox>--%>
                                                        </td>
                                                    </tr>

                                                </table>
                                                <table style="width: 70%; margin-top: 10px;" align="center">
                                                    <tr style="background-color: #66FF99">
                                                        <td style="width: 70%">&nbsp;&nbsp;&nbsp;<asp:Label ID="lblNetPay" runat="server" Text="Net Payable :"></asp:Label>
                                                        </td>
                                                        <td style="width: 30%" align="right">
                                                            <asp:Label ID="txtNet" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                </table>


                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);">
                                                            <strong style="color: #3598dc">
                                                                <label id="lblTitle" runat="server"></label>
                                                            </strong>
                                                        </div>
                                                    </div>
                                                </div>
                                                <table id="td1" style="width: 85%; border: 1px solid #000000; border-collapse: collapse; margin-top: 10px; margin-bottom: 10px" align="center">
                                                    <tr>
                                                        <td style="width: 100%" colspan="2">&nbsp;&nbsp;&nbsp;<asp:Label ID="lblTrf" runat="server" Style="font-weight: bold"></asp:Label>
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td style="width: 30%">&nbsp;&nbsp;&nbsp;<asp:Label ID="lblProdCode" runat="server" Text="Product Code :"></asp:Label>
                                                        </td>
                                                        <td style="width: 70%">&nbsp;&nbsp;&nbsp;<asp:Label ID="TxtDDSPType" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;&nbsp;&nbsp;<asp:Label ID="Label3" runat="server" Text="Product Name :"></asp:Label>
                                                        </td>
                                                        <td>&nbsp;&nbsp;&nbsp;<asp:Label ID="TxtDDSPTName" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;&nbsp;&nbsp;<asp:Label ID="lblAccNo" runat="server" Text="Acc No :"></asp:Label>
                                                        </td>
                                                        <td>&nbsp;&nbsp;&nbsp;<asp:Label ID="TxtDDSTAccNo" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;&nbsp;&nbsp;<asp:Label ID="Label4" runat="server" Text="Acc Name :"></asp:Label>
                                                        </td>
                                                        <td>&nbsp;&nbsp;&nbsp;<asp:Label ID="TxtDDSTAName" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="center">
                                                <asp:Button ID="btnModal_CancelInst" runat="server" OnClick="btnModal_CancelInst_Click" Text="Cancel Entry" CssClass="btn btn-success" />
                                                <asp:Button ID="BtnModal_CloseInst" Text="Close" runat="server" CssClass="btn btn-success" data-dismiss="modal" />
                                                <asp:Button ID="btn_Statement" runat="server" OnClick="btn_Statement_Click" Text="Statement View" CssClass="btn btn-success" />
                                            </td>
                                        </tr>
                                        <%--   <tr>
                                            <td colspan="2" align="center" style="padding-top: 10px">
                                                <asp:Button ID="btn_LoanDetails" runat="server" OnClick="btn_LoanDetails_Click" Text="Loan Acc Details" CssClass="btn btn-success" />
                                                <asp:Button ID="btn_PhotoSign" Text="Photo & Sign View" runat="server" CssClass="btn btn-success" OnClick="btn_PhotoSign_Click" />
                                                <asp:Button ID="btn_Statement" runat="server" OnClick="btn_Statement_Click" Text="Statement View" CssClass="btn btn-success" />
                                               <asp:Button ID="btn_JoinName" Text="Join Name" runat="server" CssClass="btn btn-success" OnClick="btn_JoinName_Click" />
                                            </td>
                                        </tr>--%>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="Div_UNCLEARBAL" runat="server">
                        <div class="col-lg-12">
                            <div class="table-scrollable">
                                <asp:Label ID="Lbl_UnClear" runat="server" Text="Account Statement" Style="text-align: center; font-family: Verdana; font-size: medium;" Font-Bold="true"></asp:Label>
                                <table class="table table-striped table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th>
                                                <asp:GridView ID="GridView1" runat="server" AlternatingRowStyle-CssClass="alt" CssClass="mGrid" Width="100%"
                                                    AutoGenerateColumns="false" EditRowStyle-BackColor="#FFFF99" DataKeyNames="id" EmptyDataText="No Records Available">
                                                    <Columns>

                                                        <asp:TemplateField HeaderText="EntryDate" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEDate" runat="server" Text='<%# Eval("EDATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="SetNo" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSetNo" runat="server" Text='<%# Eval("SetNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Particulars1">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblParticular" runat="server" Text='<%# Eval("PARTI") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Particulars2">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblparticular2" runat="server" Text='<%# Eval("PARTI1") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Cheque/Refrence">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblInstNo" runat="server" Text='<%# Eval("INSTNO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Credit" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCredit" runat="server" Text='<%# Eval("CREDIT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="DEBIT">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDebit" runat="server" Text='<%# Eval("DEBIT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="BALANCE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBalance" runat="server" Text='<%# Eval("BALANCE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Dr/Cr">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDrCrIndicator" runat="server" Text='<%# Eval("DrCr") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="Th" HorizontalAlign="Left" />
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
    </div>

    <%--<div id="DDSCLOSE" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">


            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Cancel Screen</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="portlet box green">
                                <div class="portlet-title">
                                    <div class="caption">
                                        DDS Closure
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
                                            <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Customer Information : </strong></div>
                                        </div>
                                    </div>
                                    <div id="DivDebit" runat="server">
                                        <div class="row" style="margin: 7px 0 7px 0; margin-top: 10px;">
                                            <div class="col-md-12">
                                                <label class="control-label col-md-2">Agent Code : <span class="required">* </span></label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="TxtDDSAGCD" Enabled="false" runat="server" onkeypress="javascript:return isNumber (event)" PlaceHolder="Agent Code" AutoPostBack="true" Style="Width: 33%; height: 33px; border: 1px solid #c0c1c1;"></asp:TextBox>&nbsp;&nbsp;                               
                                                    <asp:TextBox ID="TxtDDSAGName" Enabled="false" runat="server" PlaceHolder="Agent Name" AutoPostBack="true" Style="Width: 61%; height: 33px; border: 1px solid #c0c1c1;"></asp:TextBox>

                                                </div>
                                                <label class="control-label col-md-2">Account No : <span class="required">* </span></label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="TxtDDSAccNo" Enabled="false" runat="server" PlaceHolder="Account No" onkeypress="javascript:return isNumber (event)" AutoPostBack="true" Style="Width: 33%; height: 33px; border: 1px solid #c0c1c1;"></asp:TextBox>&nbsp;&nbsp;
                                
                                    <asp:TextBox ID="TxtDDSAccName" runat="server" Enabled="false" PlaceHolder="Account Holder Name" AutoPostBack="true" Style="Width: 61%; height: 33px; border: 1px solid #c0c1c1;"></asp:TextBox>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0; margin-top: 10px;">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Account Status: <span class="required">* </span></label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="TxtDDSAccSTS" Enabled="false" runat="server" onkeypress="javascript:return isNumber (event)" PlaceHolder="Acc Status" Style="Width: 33%; height: 33px; border: 1px solid #c0c1c1;"></asp:TextBox>&nbsp;&nbsp;                                                    
                                    <asp:TextBox ID="TxtDDSAccSTSName" runat="server" Enabled="false" PlaceHolder="Acc Type Name" Style="width: 61%; height: 33px; margin-left: -3px; border: 1px solid #c0c1c1;"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-2">Acc Type : <span class="required">*</span></label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="TxtDDSAccType" Enabled="false" runat="server" PlaceHolder="Acc Type" onkeypress="javascript:return isNumber (event)" Style="Width: 33%; height: 33px; border: 1px solid #c0c1c1;"></asp:TextBox>&nbsp;&nbsp;
                                    <asp:TextBox ID="TxtDDSAccTName" runat="server" Enabled="false" PlaceHolder="Acc Type Name" Style="width: 61%; height: 33px; margin-left: -3px; border: 1px solid #c0c1c1;"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0; margin-top: 10px;">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Opening Date: </label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtDDSOpeningDate" runat="server" PlaceHolder="Openig Date" Enabled="false" Style="height: 33px; border: 1px solid #c0c1c1;"></asp:TextBox>&nbsp;&nbsp;                                                                                                            
                                                </div>
                                                <label class="control-label col-md-1">Period :</label>
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="TxtDDSPeriod" runat="server" PlaceHolder="Period" Enabled="false" Style="width: 100%; height: 33px; border: 1px solid #c0c1c1;"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-2">Closing Date :</label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="TxtDDSCLDate" runat="server" PlaceHolder="Closing Date" Enabled="false" Style="Width: 33%; height: 33px; border: 1px solid #c0c1c1;"></asp:TextBox>&nbsp;&nbsp;                                                                                                            
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0; margin-top: 0px;">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Special Instrunction: </label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="TxtDDSSplINSt" Enabled="false" runat="server" onkeypress="javascript:return isNumber (event)" PlaceHolder="Spl Intruncation" Style="width: 85%; height: 33px; border: 1px solid #c0c1c1;"></asp:TextBox>&nbsp;&nbsp;                                                    
                                                </div>
                                                <label class="control-label col-md-2">Closing Balance : <span class="required">* </span></label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="TxtDDSCBal" Enabled="false" runat="server" PlaceHolder="Closing Balance" onkeypress="javascript:return isNumber (event)" Style="Width: 33%; height: 33px; border: 1px solid #c0c1c1;"></asp:TextBox>&nbsp;&nbsp;                                                    
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0;">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Payment Type <span class="required">* </span></label>
                                                <div class="col-md-4">
                                                    <asp:RadioButton ID="rdbDDSfull" Enabled="false" runat="server" GroupName="FP" Text="Full payment" AutoPostBack="true" />
                                                    <asp:RadioButton ID="rdbDDSpart" Enabled="false" runat="server" GroupName="FP" Text="Part payment" AutoPostBack="true" />
                                                </div>
                                                <label class="control-label col-md-2">Part Payment AMT : <span class="required">* </span></label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="TxtDDSpartpay" Enabled="false" runat="server" onkeyup="Sum()" AutoPostBack="true" PlaceHolder="Payment AMT" onkeypress="javascript:return isNumber (event)" Style="Width: 33%; height: 33px; border: 1px solid #c0c1c1;"></asp:TextBox>&nbsp;&nbsp;                                                    
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0; margin-top: 10px;">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Activity :<span class="required">* </span></label>
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="ddlDDSActivity" runat="server" CssClass="form-control" AutoPostBack="true" Enabled="false">
                                                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                        <%-- <asp:ListItem Value="1" Text="Part Withdrawal"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="Premature Closure"></asp:ListItem>
                                                        <asp:ListItem Value="3" Text="Maturity Closure"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>

                                                <div class="col-md-4">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #fa4e0d">Deduction : </strong></div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin: 7px 0 7px 0; margin-top: 20px;">
                                        <div class="col-lg-12">
                                            <asp:Label ID="Lbl_Provision" runat="server" Text="Provision(B)" class="control-label col-md-2"></asp:Label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="TxtDDS_Provi" runat="server" Text="0" onkeyup=" MINUS()" PlaceHolder="Provision" AutoPostBack="true" Style="Width: 33%; height: 33px; border: 1px solid #c0c1c1;" Enabled="false"></asp:TextBox>
                                            </div>
                                            <asp:Label ID="Label3" runat="server" Text="Calculated Intr.(A)" class="control-label col-md-2"></asp:Label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="TextBox1" Enabled="false" runat="server" Text="0" PlaceHolder="Interest" onkeyup="MINUS();Sum()" AutoPostBack="true" Style="Width: 33%; height: 33px; border: 1px solid #c0c1c1;"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="row" style="margin: 7px 0 7px 0;">
                                        <div class="col-lg-12">

                                            <asp:Label ID="Label4" runat="server" Text="Premature Commission :" class="control-label col-md-2"></asp:Label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="TxtDDSPreMC" Enabled="false" runat="server" PlaceHolder="Commission" Style="Width: 33%; height: 33px; border: 1px solid #c0c1c1;"></asp:TextBox>
                                                <asp:TextBox ID="TextBox2" Enabled="false" runat="server" PlaceHolder="Commission" AutoPostBack="true" Style="Width: 33%; height: 33px; border: 1px solid #c0c1c1;"></asp:TextBox>
                                            </div>
                                            <label class="control-label col-md-2">Other Receipts : <span class="required">* </span></label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="TextBox3" Enabled="false" runat="server" PlaceHolder="Other Receipts" AutoPostBack="true" Style="Width: 33%; height: 33px; border: 1px solid #c0c1c1;"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin: 7px 0 7px 0; margin-top: 20px;">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Payable Intr (A-B) : <span class="required">* </span></label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="TxtDDSINTC" Enabled="false" runat="server" onkeyup="Sum()" PlaceHolder="Interest" AutoPostBack="true" Style="Width: 33%; height: 33px; border: 1px solid #c0c1c1;"></asp:TextBox>
                                            </div>
                                            <asp:Label ID="lblpayable" runat="server" Text="Amount" class="control-label col-md-2"></asp:Label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="TextBox4" Enabled="false" runat="server" onkeyup="Sum()" PlaceHolder="payeble Amount" Style="Width: 33%; height: 33px; border: 1px solid #c0c1c1;"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin: 7px 0 7px 0; margin-top: 20px;">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Payment Mode : <span class="required">* </span></label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="TxtDDSPayMode" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>

                                            </div>
                                            <asp:Label ID="Label5" runat="server" Text="Outstanding Charges" class="control-label col-md-2"></asp:Label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="TxtDDSOutSTD" Enabled="false" runat="server" PlaceHolder="Outstanding Charges" Style="Width: 33%; height: 33px; border: 1px solid #c0c1c1;"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="DivTransfer" runat="server">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #21fa09">Transfer Information : </strong></div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0; margin-top: 20px;">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Product Type : <span class="required">* </span></label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="TextBox5" Enabled="false" runat="server" onkeypress="javascript:return isNumber (event)" PlaceHolder="Product Code" Style="Width: 33%; height: 33px; border: 1px solid #c0c1c1;" AutoPostBack="true"></asp:TextBox>&nbsp;&nbsp;
                                    <asp:TextBox ID="TxtDDSPTName" runat="server" Enabled="false" PlaceHolder="Product Name" Style="Width: 61%; height: 33px; border: 1px solid #c0c1c1;" AutoPostBack="true"></asp:TextBox>

                                                </div>
                                                <label class="control-label col-md-2">Account No : <span class="required">* </span></label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="TextBox6" Enabled="false" runat="server" PlaceHolder="Account No" onkeypress="javascript:return isNumber (event)" Style="Width: 33%; height: 33px; border: 1px solid #c0c1c1;" AutoPostBack="true"></asp:TextBox>&nbsp;&nbsp;
                                    <asp:TextBox ID="TxtDDSTAName" Enabled="false" runat="server" PlaceHolder="Account Holder Name" Style="width: 61%; height: 33px; margin-left: -3px; border: 1px solid #c0c1c1;"></asp:TextBox>

                                                </div>

                                            </div>
                                        </div>
                                        <div class="row" id="DIV_INSTRUMENT" runat="server" style="margin: 7px 0 7px 0; margin-top: 20px;">
                                            <div class="col-md-12">
                                                <label class="control-label col-md-2">Instrument No : <span class="required">* </span></label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="TxtDDSInstNo" Enabled="false" runat="server" onkeypress="javascript:return isNumber (event)" PlaceHolder="Instrument No" CssClass="form-control"></asp:TextBox>&nbsp;&nbsp;                                                   
                                                </div>
                                                <label class="control-label col-md-2">Instrument Date : <span class="required">* </span></label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="TxtDDSInstDate" Enabled="false" runat="server" PlaceHolder="Instrument Date" CssClass="form-control" onkeyup="FormatIt(this);CheckForFutureDate()"></asp:TextBox>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #21fa09"></strong></div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin: 7px 0 7px 0; margin-top: 20px;">
                                        <div class="col-lg-12">
                                            <div class="row">
                                                <div class="col-md-offset-3 col-md-9">
                                                    <asp:Button ID="Button1" runat="server" OnClick="btnModal_CancelInst_Click" Text="Cancel Entry" CssClass="btn btn-success" />
                                                    <asp:Button ID="Button2" Text="Close" runat="server" CssClass="btn btn-success" data-dismiss="modal" />
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
    </div>--%>

    <div id="LOANINST" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg" style="width: 80%">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Cancel Screen</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="portlet box blue" id="Div2">
                                <div class="portlet-title">
                                    <div class="caption">
                                        LOAN INSTALLMENT
                                    </div>
                                </div>
                                <div class="portlet-body form">
                                    <div class="form-horizontal">
                                        <div class="form-wizard">

                                            <div class="form-body">
                                                <div class="tab-content">

                                                    <div style="border: 1px solid #3598dc">
                                                        <div class="portlet-body">
                                                            <div class="row">
                                                                <div class="col-lg-12">
                                                                    <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Account Information : </strong></div>
                                                                </div>
                                                            </div>

                                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                                <div class="col-lg-12">
                                                                    <div class="col-md-2">
                                                                        <label class="control-label ">Prod Type:</label>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <asp:TextBox ID="txtProdType" Enabled="false" Placeholder="Product Type" runat="server" CssClass="form-control" />
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <asp:TextBox ID="txtProdName" Enabled="false" Placeholder="Product Name" runat="server" CssClass="form-control" />
                                                                    </div>
                                                                    <div class="col-md-1">
                                                                        <label class="control-label ">Acc\No:</label>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <asp:TextBox ID="txtAccNo" Enabled="false" Placeholder="Account Number" runat="server" CssClass="form-control" />
                                                                    </div>
                                                                    <div class="col-md-3">
                                                                        <asp:TextBox ID="txtAccName" Enabled="false" Placeholder="Account Name" runat="server" CssClass="form-control" />
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                                <div class="col-lg-12">
                                                                    <div class="col-md-2">
                                                                        <label class="control-label ">Cust Number :</label>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <asp:TextBox ID="txtCustNo" Enabled="false" Placeholder="Customer Number" runat="server" CssClass="form-control" />
                                                                    </div>
                                                                    <div class="col-md-1">
                                                                        <label class="control-label ">Status</label>
                                                                    </div>
                                                                    <div class="col-md-1">
                                                                        <asp:TextBox ID="txtAccStatus" Enabled="false" runat="server" CssClass="form-control" />
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <asp:DropDownList ID="ddlAccStatus" Enabled="false" CssClass="form-control" runat="server"></asp:DropDownList>
                                                                    </div>
                                                                    <div class="col-md-1">
                                                                        <label class="control-label ">Amount:<span class="required">*</span></label>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <asp:TextBox ID="txtDepositeAmt" Enabled="false" placeholder="Installment Amount" CssClass="form-control" runat="server" />
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>

                                                    <div style="border: 1px solid #3598dc">
                                                        <div class="portlet-body">
                                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                                <div class="col-lg-12">
                                                                    <div class="col-md-2" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Head Name</strong></div>
                                                                    <div class="col-md-2" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Paid Amount</strong></div>
                                                                    <div class="col-md-2" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Head Name</strong></div>
                                                                    <div class="col-md-2" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Paid Amount</strong></div>
                                                                    <div class="col-md-2" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Head Name</strong></div>
                                                                    <div class="col-md-2" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Paid Amount</strong></div>
                                                                </div>
                                                            </div>

                                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                                <div class="col-lg-12">
                                                                    <div class="col-md-2">
                                                                        <label class="control-label ">Principle :</label>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <asp:TextBox ID="txtPrinAmt" Enabled="false" Placeholder="Amount" CssClass="form-control" runat="server" />
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <label class="control-label ">Interest :</label>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <asp:TextBox ID="txtIntAmt" Enabled="false" Placeholder="Amount" CssClass="form-control" runat="server" />
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <label class="control-label ">Penal Interest :</label>
                                                                    </div>

                                                                    <div class="col-md-2">
                                                                        <asp:TextBox ID="txtPIntAmt" Enabled="false" Placeholder="Amount" CssClass="form-control" runat="server" />
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                                <div class="col-lg-12">
                                                                    <div class="col-md-2">
                                                                        <label class="control-label ">Int Recievable :</label>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <asp:TextBox ID="txtIntRecAmt" Enabled="false" Placeholder="Amount" CssClass="form-control" runat="server" />
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <label class="control-label ">Notice Charges :</label>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <asp:TextBox ID="txtNotChrgAmt" Enabled="false" Placeholder="Amount" CssClass="form-control" runat="server" />
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <label class="control-label ">Service Chrges :</label>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <asp:TextBox ID="txtSerChrgAmt" Enabled="false" Placeholder="Amount" CssClass="form-control" runat="server" />
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                                <div class="col-lg-12">
                                                                    <div class="col-md-2">
                                                                        <label class="control-label ">Court Charge :</label>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <asp:TextBox ID="txtCrtChrgAmt" Enabled="false" Placeholder="Amount" CssClass="form-control" runat="server" />
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <label class="control-label ">Sur Charge :</label>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <asp:TextBox ID="txtSurChrgAmt" Enabled="false" Placeholder="Amount" CssClass="form-control" runat="server" />
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <label class="control-label ">Other Charge :</label>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <asp:TextBox ID="txtOtherChrgAmt" Enabled="false" Placeholder="Amount" CssClass="form-control" runat="server" />
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                                <div class="col-lg-12">
                                                                    <div class="col-md-2">
                                                                        <label class="control-label ">Bank Charge :</label>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <asp:TextBox ID="txtBankChrgAmt" Enabled="false" Placeholder="Amount" CssClass="form-control" runat="server" />
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <label class="control-label ">Insurance :</label>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <asp:TextBox ID="txtInsuranceAmt" Enabled="false" Placeholder="Amount" CssClass="form-control" runat="server" />
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>

                                                    <div style="border: 1px solid #3598dc">
                                                        <div class="portlet-body">
                                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                                <div class="col-lg-12">
                                                                    <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Payment Mode : </strong></div>
                                                                </div>
                                                            </div>

                                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                                <div class="col-lg-12">
                                                                    <div class="col-md-2">
                                                                        <label class="control-label ">Payment Type :<span class="required"> *</span></label>
                                                                    </div>
                                                                    <div class="col-md-3">
                                                                        <asp:DropDownList ID="ddlPayType" Enabled="false" runat="server" CssClass="form-control">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div id="Transfer" visible="false" runat="server">

                                                                <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                                    <div class="col-lg-12">
                                                                        <div class="col-md-2">
                                                                            <label class="control-label ">Product Code :<span class="required"> *</span></label>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <asp:TextBox ID="txtProdType1" Enabled="false" CssClass="form-control" runat="server" PlaceHolder="Product Type"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-md-5">
                                                                            <asp:TextBox ID="txtProdName1" Enabled="false" CssClass="form-control" PlaceHolder="Product Name" runat="server"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                                    <div class="col-lg-12">
                                                                        <div class="col-md-2">
                                                                            <label class="control-label ">Acc No / Name:<span class="required"> *</span></label>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <asp:TextBox ID="TxtAccNo1" Enabled="false" CssClass="form-control" PlaceHolder="Account Number" runat="server"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-md-5">
                                                                            <asp:TextBox ID="TxtAccName1" Enabled="false" CssClass="form-control" PlaceHolder="Customer Name" runat="server"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div id="Transfer1" visible="false" runat="server">
                                                                <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                                    <div class="col-lg-12">
                                                                        <div class="col-md-2">
                                                                            <label class="control-label ">Instrument No. :</label>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <asp:TextBox ID="TxtInstNo" Enabled="false" placeholder="Instrument Number" CssClass="form-control" runat="server"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-md-2">
                                                                            <label class="control-label ">Instrument Date :</label>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <asp:TextBox ID="TxtInstDate" Enabled="false" placeholder="Instrument Date" CssClass="form-control" runat="server"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div id="Div1" runat="server">
                                                                <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                                    <div class="col-lg-12">
                                                                        <div class="col-md-2">
                                                                            <label class="control-label ">Naration : <span class="required">*</span></label>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <asp:TextBox ID="txtNarration" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                                                        </div>
                                                                        <div class="col-md-2">
                                                                            <label class="control-label ">Amount : <span class="required">*</span></label>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <asp:TextBox ID="txtAmount" Enabled="false" placeholder="Debit amount" CssClass="form-control" runat="server"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>

                                            <div class="form-actions">
                                                <div class="row">
                                                    <div class="col-md-offset-3 col-md-9">
                                                        <asp:Button ID="btnCancelInst" runat="server" OnClick="btnModal_CancelInst_Click" Text="Cancel Entry" CssClass="btn btn-success" />
                                                        <asp:Button ID="btnClose" Text="Close" runat="server" CssClass="btn btn-success" data-dismiss="modal" />
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

    <div id="CASHP" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg" style="width: 80%">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title" style="text-align: center; font: 100; font-family: Verdana; font-size: larger; font-style: italic">Cancel Screen</h4>
                </div>
                <div class="modal-body">
                    <div class="row">

                        <div class="col-md-12">

                            <div class="portlet box blue">
                                <div class="portlet-title">
                                    <div class="caption">
                                        Cash Payment
                                    </div>
                                </div>
                                <div class="portlet-body form">
                                    <!--<form action="#" class="form-horizontal" id="submit_form" method="POST">-->
                                    <div class="form-horizontal">
                                        <div class="form-wizard">
                                            <div class="form-body">

                                                <div class="tab-content">
                                                    <div id="error">
                                                    </div>
                                                    <div class="tab-pane active" id="tab1">
                                                        <asp:Table ID="Table1" runat="server">
                                                            <asp:TableRow ID="TableRow1" runat="server" Style="width: 300px">
                                                                <asp:TableCell ID="TableCell1" runat="server" Style="width: 2000px">
                                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                                        <div class="col-lg-11">
                                                                            <label class="control-label col-md-3">Entry Date : <span class="required">* </span></label>
                                                                            <div class="col-md-3">
                                                                                <asp:TextBox ID="TxtEntrydateCP" CssClass="form-control" runat="server"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                                        <div class="col-lg-11">
                                                                            <label class="control-label col-md-3">Product Type : <span class="required">* </span></label>

                                                                            <div class="col-md-3">
                                                                                <asp:TextBox ID="TxtProcodeCP" placeholder="PRODUCT TYPE" CssClass="form-control" runat="server" AutoPostBack="true" TabIndex="2" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                                                <%--<asp:TextBox ID="TextBox1" CssClass="form-control" runat="server"  AutoPostBack="true" OnTextChanged="TxtProcode_TextChanged" ></asp:TextBox>--%>
                                                                            </div>
                                                                            <div class="col-md-5">
                                                                                <asp:TextBox ID="TxtProNameCP" placeholder="PRODUCT NAME" CssClass="form-control" runat="server" AutoPostBack="true" TabIndex="3"></asp:TextBox>
                                                                                <div id="CustList" style="height: 200px; overflow-y: scroll;"></div>
                                                                                <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="TxtProNameCP"
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

                                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                                        <div class="col-lg-11">
                                                                            <label class="control-label col-md-3">Account No : <span class="required">* </span></label>
                                                                            <div class="col-md-3">
                                                                                <asp:TextBox ID="TxtAccNoCP" placeholder="ACCOUNT NUMBER" CssClass="form-control" runat="server" AutoPostBack="true" TabIndex="4" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                                                <%--<asp:TextBox ID="TextBox1" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="TxtAccNo_TextChanged"></asp:TextBox>--%>
                                                                            </div>
                                                                            <div class="col-md-5">
                                                                                <asp:TextBox ID="TxtAccNameCP" placeholder="ACCOUNT NAME" CssClass="form-control" runat="server" AutoPostBack="true" TabIndex="5"></asp:TextBox>
                                                                                <div id="CustList2" style="height: 200px; overflow-y: scroll;"></div>
                                                                                <asp:AutoCompleteExtender ID="AutoAccname" runat="server" TargetControlID="TxtAccNameCP"
                                                                                    UseContextKey="true"
                                                                                    CompletionInterval="1"
                                                                                    CompletionSetCount="20"
                                                                                    MinimumPrefixLength="1"
                                                                                    EnableCaching="true"
                                                                                    ServicePath="~/WebServices/Contact.asmx"
                                                                                    ServiceMethod="GetAccName" CompletionListElementID="CustList2">
                                                                                </asp:AutoCompleteExtender>
                                                                            </div>
                                                                            <%--   <asp:LinkButton ID="LnkVerify" runat="server" OnClick="LnkVerify_Click">Verify Signature</asp:LinkButton>--%>
                                                                        </div>
                                                                    </div>

                                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                                        <div class="col-lg-11">
                                                                            <label class="control-label col-md-3">Naration : <span class="required">* </span></label>
                                                                            <div class="col-md-3">
                                                                                <asp:TextBox ID="txtnarationCP" CssClass="form-control" runat="server" Text="To Cash" Enabled="false"></asp:TextBox>
                                                                            </div>
                                                                            <label class="control-label col-md-2">Naration 2 : <span class="required"></span></label>
                                                                            <div class="col-md-3">
                                                                                <asp:TextBox ID="txtnaration1CP" placeholder="NARATION 2" CssClass="form-control" runat="server" TabIndex="6"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                                        <div class="col-lg-11">

                                                                            <label class="control-label col-md-3">Clear Balance : <span class="required">* </span></label>
                                                                            <div class="col-md-3">
                                                                                <asp:TextBox ID="txtBalanceCP" placeholder="OLD BALANCE" CssClass="form-control" runat="server" TabIndex="-1" ReadOnly="true"></asp:TextBox>
                                                                            </div>
                                                                            <label class="control-label col-md-2">Amount : <span class="required">* </span></label>
                                                                            <div class="col-md-3">
                                                                                <asp:TextBox ID="TxtAmountCP" Style="background-color: lightgreen" placeholder="DEBIT AMOUNT" CssClass="form-control" runat="server" TabIndex="8" onkeypress="javascript:return isNumber (event)" AutoPostBack="true"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                                        <div class="col-lg-11">

                                                                            <label class="control-label col-md-3">Total Balance : <span class="required">* </span></label>
                                                                            <div class="col-md-3">
                                                                                <asp:TextBox ID="TxtNewBalanceCP" Style="background-color: lightcoral" placeholder="BALANCE" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </asp:TableCell>

                                                                <asp:TableCell ID="TableCell2" runat="server">
                                                                    <div id="DIVPHOTO" runat="server" class="row" style="margin: 7px 0 7px 0">
                                                                        <div class="col-lg-11">
                                                                            <div class="modal-body" style="margin-left: auto; margin-right: auto; text-align: center;">
                                                                                <p></p>
                                                                                <asp:Label ID="LblPhotoandSign" runat="server" Text="View" Style="text-align: center; color: black; font-size: 26px; font-weight: bold;"></asp:Label>
                                                                                <div class="zoom_img">
                                                                                    <asp:Image ID="imgPopup" runat="server" Width="150px" Height="150px" />
                                                                                    <asp:Image ID="imgSignPopup" runat="server" Width="150px" Height="150px" />
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
                                        </div>
                                        <div class="form-actions">
                                            <div class="row">
                                                <div class="col-md-offset-3 col-md-9">
                                                    <div class="col-md-offset-3 col-md-9">
                                                        <asp:Button ID="Btn_CpCancel" runat="server" OnClick="btnModal_CancelInst_Click" Text="Cancel Entry" CssClass="btn btn-success" />
                                                        <asp:Button ID="Btn_CPClose" Text="Close" runat="server" CssClass="btn btn-success" data-dismiss="modal" />
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                    <!--</form>-->
                                </div>


                            </div>

                            <div class="col-md-12">

                                <div class="table-scrollable" style="width: 100%; height: auto; max-height: 150px; overflow-x: auto; overflow-y: auto">
                                    <table class="table table-striped table-bordered table-hover" width="100%">
                                        <thead>
                                            <tr>
                                                <th>
                                                    <asp:GridView ID="grdAccStatement" runat="server" AlternatingRowStyle-CssClass="alt" CssClass="mGrid" Width="100%"
                                                        AutoGenerateColumns="false" EditRowStyle-BackColor="#FFFF99" DataKeyNames="id" EmptyDataText="No Records Available">
                                                        <Columns>

                                                            <asp:TemplateField HeaderText="EntryDate" Visible="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEDate" runat="server" Text='<%# Eval("EDATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="SetNo" Visible="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSetNo" runat="server" Text='<%# Eval("SetNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Particulars1">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblParticular" runat="server" Text='<%# Eval("PARTI") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Particulars2">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblparticular2" runat="server" Text='<%# Eval("PARTI1") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Cheque/Refrence">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblInstNo" runat="server" Text='<%# Eval("INSTNO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Credit" Visible="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCredit" runat="server" Text='<%# Eval("CREDIT") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="DEBIT">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDebit" runat="server" Text='<%# Eval("DEBIT") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="BALANCE">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBalance" runat="server" Text='<%# Eval("BALANCE") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Dr/Cr">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDrCrIndicator" runat="server" Text='<%# Eval("DrCr") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="Th" HorizontalAlign="Left" />
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
        </div>
    </div>

    <div id="CASHR" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg" style="width: 80%">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title" style="text-align: center; font-family: Verdana; font-size: medium; font-style: italic">Cancel Screen</h4>
                </div>
                <div class="modal-body">
                    <div class="row">

                        <div class="col-md-12">
                            <div class="portlet box blue" id="Div7">
                                <div class="portlet-title">
                                    <div class="caption">
                                        Cash Receipt
                                    </div>
                                </div>
                                <div class="portlet-body form">
                                    <!--<form action="#" class="form-horizontal" id="submit_form" method="POST">-->
                                    <div class="form-horizontal">
                                        <div class="form-wizard">
                                            <div class="form-body">

                                                <div class="tab-content">
                                                    <div id="Div8">
                                                    </div>
                                                    <div class="tab-pane active" id="Div9">
                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-11">
                                                                <label class="control-label col-md-3">Entry Date : <span class="required">* </span></label>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="TxtEntrydateCR" CssClass="form-control" runat="server"></asp:TextBox>
                                                                    <asp:TextBoxWatermarkExtender ID="TxtFDT_WatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtEntrydateCR">
                                                                    </asp:TextBoxWatermarkExtender>
                                                                    <asp:CalendarExtender ID="TxtFDT_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtEntrydateCR">
                                                                    </asp:CalendarExtender>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-11">
                                                                <label class="control-label col-md-3">Product Type : <span class="required">* </span></label>

                                                                <div class="col-md-3">

                                                                    <asp:TextBox ID="TxtProcodeCR" PLACEHOLDER="PRODUCT TYPE" CssClass="form-control" runat="server" AutoPostBack="true" TabIndex="2" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                                    <%--<asp:TextBox ID="TextBox1" CssClass="form-control" runat="server"  AutoPostBack="true" OnTextChanged="TxtProcode_TextChanged" ></asp:TextBox>--%>
                                                                </div>
                                                                <div class="col-md-5">
                                                                    <asp:TextBox ID="TxtProNameCR" placeholder="PRODUCT NAME" CssClass="form-control" runat="server" AutoPostBack="true" TabIndex="3"></asp:TextBox>

                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-11">
                                                                <label class="control-label col-md-3">Account Number : <span class="required">* </span></label>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="TxtAccnoCR" placeholder="ACCOUNT NO" CssClass="form-control" runat="server" AutoPostBack="true" TabIndex="4" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                                    <%--<asp:TextBox ID="TextBox1" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="TxtAccNo_TextChanged"></asp:TextBox>--%>
                                                                </div>
                                                                <div class="col-md-5">
                                                                    <asp:TextBox ID="TxtAccNameCR" placeholder="ACCOUNT NAME" CssClass="form-control" runat="server" AutoPostBack="true" TabIndex="5"></asp:TextBox>

                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-11">
                                                                <label class="control-label col-md-3">Naration : <span class="required">* </span></label>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="TxtNarrationCR" CssClass="form-control" runat="server" Text="By Cash" Enabled="false"></asp:TextBox>
                                                                </div>
                                                                <label class="control-label col-md-2">Naration 2 : <span class="required"></span></label>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="TxtNarration2CR" placeholder="NARATION 2" CssClass="form-control" runat="server" TabIndex="6"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-11">

                                                                <label class="control-label col-md-3">Clear Balance : <span class="required">* </span></label>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="TxtOldBalCR" placeholder="OLD BALANCE" CssClass="form-control" runat="server" TabIndex="-1" ReadOnly="true"></asp:TextBox>
                                                                </div>

                                                                <label class="control-label col-md-2">Amount : <span class="required">* </span></label>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="TxtAmountCR" placeholder="CREDIT AMOUNT" CssClass="form-control" runat="server" TabIndex="9" onkeypress="javascript:return isNumber (event)" AutoPostBack="true"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-11">

                                                                <label class="control-label col-md-3">Total Balance : <span class="required">* </span></label>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="TxtNewBalCR" placeholder="BALANCE" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>

                                                </div>
                                            </div>
                                            <div class="form-actions">
                                                <div class="row">
                                                    <div class="col-md-offset-3 col-md-9">
                                                        <div class="col-md-offset-3 col-md-9">
                                                            <asp:Button ID="Btn_CRCancel" runat="server" OnClick="btnModal_CancelInst_Click" Text="Cancel Entry" CssClass="btn btn-success" />
                                                            <asp:Button ID="Btn_CRClose" Text="Close" runat="server" CssClass="btn btn-success" data-dismiss="modal" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!--</form>-->
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12">

                            <div class="table-scrollable" style="width: 100%; height: auto; max-height: 150px; overflow-x: auto; overflow-y: auto">
                                <table class="table table-striped table-bordered table-hover" width="100%">
                                    <thead>
                                        <tr>
                                            <th>
                                                <asp:GridView ID="grdAccStat" runat="server" AlternatingRowStyle-CssClass="alt" CssClass="mGrid" Width="100%"
                                                    AutoGenerateColumns="false" EditRowStyle-BackColor="#FFFF99" DataKeyNames="id" EmptyDataText="No Records Available">
                                                    <Columns>

                                                        <asp:TemplateField HeaderText="EntryDate" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEDate" runat="server" Text='<%# Eval("EDATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="SetNo" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSetNo" runat="server" Text='<%# Eval("SetNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Particulars1">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblParticular" runat="server" Text='<%# Eval("PARTI") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Particulars2">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblparticular2" runat="server" Text='<%# Eval("PARTI1") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Cheque/Refrence">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblInstNo" runat="server" Text='<%# Eval("INSTNO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Credit" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCredit" runat="server" Text='<%# Eval("CREDIT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="DEBIT">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDebit" runat="server" Text='<%# Eval("DEBIT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="BALANCE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBalance" runat="server" Text='<%# Eval("BALANCE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="Th" HorizontalAlign="Right" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Dr/Cr">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDrCrIndicator" runat="server" Text='<%# Eval("DrCr") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="Th" HorizontalAlign="Left" />
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
    </div>

    <div id="TDCLOSE" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg" style="width: 80%">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Cancel Screen</h4>
                </div>
                <div class="modal-body">


                    <div class="row">
                        <div class="col-md-12">
                            <div class="portlet box green">
                                <div class="portlet-title">
                                    <div class="caption">
                                        Deposit Closure
                                <asp:Literal ID="Literal1" runat="server"></asp:Literal>
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
                                            <label class="control-label col-md-2">Deposit Type<span class="required">* </span></label>
                                            <div class="col-md-4">
                                                <asp:RadioButton ID="rdbTDMature" runat="server" Text="Mature" AutoPostBack="true" GroupName="mat" Enabled="false" />
                                                <asp:RadioButton ID="rdbTDPreMature" runat="server" Text="PreMature" AutoPostBack="true" GroupName="mat" Enabled="false" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin: 7px 0 7px 0;">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Product Code<span class="required">* </span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="TxtTDProcode" Enabled="false" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="TxtTDProName" Enabled="false" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin: 7px 0 7px 0;">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Account No<span class="required">* </span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="TxtTDAccno" Enabled="false" CssClass="form-control" PlaceHolder="Account No" runat="server" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="TxtTDAccName" Enabled="false" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>

                                            </div>
                                            <label class="control-label col-md-2">Customer<span class="required"></span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="TxtTDCustno" CssClass="form-control" PlaceHolder="Cust No" runat="server" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div id="Depositdiv" runat="server">
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Deposit Date: <span class="required"></span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtTDDepoDate" onkeyup="FormatIt(this)" CssClass="form-control" PlaceHolder="DD/MM/YYYY" runat="server" Enabled="False"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-2">Interest Payout: <span class="required"></span></label>
                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="ddlTDIntrestPay" runat="server" CssClass="form-control" Enabled="False">
                                                    </asp:DropDownList>
                                                </div>
                                                <label class="control-label col-md-2">Interest Amount : <span class="required"></span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtTDIntrest" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Deposit Amount : <span class="required"></span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtTDDepoAmt" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" AutoPostBack="true" Enabled="False"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-2">Period : <span class="required"></span></label>
                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="ddlTDduration" runat="server" CssClass="form-control" Enabled="False">
                                                        <asp:ListItem Value="M">Months</asp:ListItem>
                                                        <asp:ListItem Value="D">Days</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="TxtTDPeriod" CssClass="form-control" PlaceHolder="Period" runat="server" Style="width: 77px;" Enabled="False"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-1">Rate : <span class="required"></span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtTDRate" CssClass="form-control" runat="server" Enabled="false" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Maturity Amount :<span class="required"></span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtTDMaturity" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-2">Due Date :<span class="required"></span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtTDDueDate" CssClass="form-control" PlaceHolder="dd/mm/yyyy" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-2">Close Status : <span class="required"></span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtTDOpenClose" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc"></strong></div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Principal Payable : <span class="required"></span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtTDPrincPaybl" CssClass="form-control" runat="server" Enabled="false" BackColor="#cccccc"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-2">Interest Payable : <span class="required"></span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtTDIntrestPaybl" CssClass="form-control" runat="server" Enabled="false" BackColor="#cccccc"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="row" style="margin: 07px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Int Applied : <span class="required"></span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtTDInterestNew" CssClass="form-control" runat="server" value="0" Enabled="false" BackColor="#ffcccc"></asp:TextBox>
                                                </div>
                                                <asp:Label ID="Lbl_AdminChr" runat="server" class="control-label col-md-2" Text="Admin Charges :"></asp:Label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtTDAdminCharges" CssClass="form-control" runat="server" value="0" Enabled="false" BackColor="#ffcccc"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0;" id="PWI" runat="server" visible="false">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Transfer Type : <span class="required">* </span></label>
                                                <div class="col-md-4">
                                                    <asp:RadioButton ID="RdbPI" runat="server" Text="Principle With INT" GroupName="mat" AutoPostBack="true" />
                                                    <asp:RadioButton ID="RdbP" runat="server" Text="Only Principle" GroupName="mat" AutoPostBack="true" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0" runat="server" id="SB">
                                            <div class="col-lg-12">
                                                <asp:Label ID="lblpenalrate" runat="server" class="control-label col-md-2" Text="SB int"></asp:Label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtTDSbintrest" CssClass="form-control" runat="server" value="0" Enabled="false" BackColor="#ffcccc"></asp:TextBox>
                                                </div>
                                                <label id="Lbl_Commi" runat="server" class="control-label col-md-2">Comission :<span class="required"></span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtTDCommission" CssClass="form-control" runat="server" value="0" Enabled="false" BackColor="#ffcccc"></asp:TextBox>

                                                </div>
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="TxtTDCROI" AutoPostBack="true" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                </div>

                                                <div id="Div11" runat="server" visible="false">
                                                    <asp:Label ID="lblpenal" runat="server" class="control-label col-md-2" Text="SB int rate"></asp:Label>
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="txt1" CssClass="form-control" runat="server" value="0" Enabled="false"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="Txt2" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">TDS Provision : <span class="required"></span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtTDTDSP" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-2">TDS Paid : <span class="required"></span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtTDTDSPaid" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc"></strong></div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Loan Sanction Amt </label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtTDSancAmount" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-1">Balance </label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtTDLoanBal" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-2">Last Int Date </label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtTDLastIntDate" CssClass="form-control" PlaceHolder="DD/MM/YYYY" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Days </label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtTDDays" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-1">Interest </label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtTDLoanInt" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-2">Total Balance </label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtTDTotLoanBal" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                                <div class="col-md-1">
                                                    <asp:Button ID="btnTDLoanInt" Visible="false" Text="Post Int" runat="server" CssClass="btn btn-primary" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc"></strong></div>
                                            </div>
                                        </div>


                                        <div class="row" style="margin: 7px 0 7px 0" runat="server" visible="false" id="DIVINT">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Saving Acc No / Name:<span class="required"></span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtTDINTAcc" CssClass="form-control" PlaceHolder="ID" runat="server" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="TxtTDIntAccName" CssClass="form-control" PlaceHolder="Customer Name" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-md-12">
                                                <label class="control-label col-md-2">Closure Type : <span class="required">* </span></label>
                                                <div class="col-md-4">
                                                    <asp:RadioButton ID="RdbTDClose" runat="server" Checked="true" Text="Close" AutoPostBack="true" GroupName="CR" />
                                                    <asp:RadioButton ID="RdbTDRenew" runat="server" Text="Renew" AutoPostBack="true" GroupName="CR" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Select Payment Type : <span class="required"></span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtTDPayMode" CssClass="form-control" PlaceHolder="Pay Mode" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-2">Total Payable Amount</label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtTDPayAmnt" CssClass="form-control" PlaceHolder="Total Payable" runat="server" Enabled="false" BackColor="#cccccc"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div id="Div12" visible="true" runat="server">
                                            <div id="Div13" class="row" style="margin: 07px 0 7px 0;" runat="server">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Product Code :<span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtTDPCode1" Enabled="false" CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                                                    </div>

                                                    <div id="Div14" class="col-md-4" runat="server">
                                                        <asp:TextBox ID="TxtTDPName1" Enabled="false" CssClass="form-control" PlaceHolder="Product Name" AutoPostBack="true" runat="server"></asp:TextBox>

                                                    </div>
                                                </div>
                                            </div>

                                            <div id="Div15" class="row" style="margin: 07px 0 7px 0;" runat="server">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Acc No : <span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtTDAccno1" Enabled="false" CssClass="form-control" PlaceHolder="ID" runat="server" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="TxtTDAccName1" Enabled="false" CssClass="form-control" PlaceHolder="Customer Name" runat="server" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="DivNarrattion" class="row" style="margin: 07px 0 7px 0;" runat="server">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Narration<span class="required">*</span></label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="TxtTNarration" CssClass="form-control" runat="server" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>
                                        <div id="Transfer2" visible="false" runat="server">
                                            <div id="Div16" class="row" style="margin: 07px 0 7px 0;" runat="server">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Instrument No :<span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtTDChequeNo" placeholder="CHEQUE NUMBER" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                    </div>
                                                    <label class="control-label col-md-2">Instrument Date :<span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtTDChequeDate" placeholder="CHEQUE DATE" CssClass="form-control" onkeyup="FormatIt(this)" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div>
                                            <hr style="margin: 10px 0px 10px 0px" />
                                        </div>
                                        <div class="row" style="margin: 07px 0 1px 0">
                                            <div class="col-lg-12">
                                                <div class="col-md-offset-3 col-md-9">
                                                    <asp:Button ID="Btn_TDCancel" runat="server" OnClick="btnModal_CancelInst_Click" Text="Cancel Entry" CssClass="btn btn-success" />
                                                    <asp:Button ID="Btn_TDClose" Text="Close" runat="server" CssClass="btn btn-success" data-dismiss="modal" />
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

    <div id="MULTITRANSFER" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg" style="width: 80%">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Cancel Screen</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="portlet box green">
                                <div class="portlet-title">
                                    <div class="caption">
                                        Multiple Transfer
                                <asp:Literal ID="Literal2" runat="server"></asp:Literal>
                                    </div>
                                    <div class="tools">
                                        <a href="javascript:;" class="collapse" data-original-title="" title=""></a>
                                        <a href="#portlet-config" data-toggle="modal" class="config" data-original-title="" title=""></a>
                                    </div>
                                </div>

                                <div class="portlet-body">
                                    <div class="row" style="margin: 7px 0 7px 0;">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Total Cr. Amount<span class="required">* </span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="Txt_MTCrAmt" Enabled="false" runat="server" CssClass="form-control" AutoPostBack="true" BackColor="#ffccff"></asp:TextBox>
                                            </div>
                                            <div class="col-md-4">
                                            </div>
                                            <label class="control-label col-md-2">Total Dr. Amount<span class="required">* </span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="Txt_MTDrAmt" Enabled="false" runat="server" CssClass="form-control" AutoPostBack="true" BackColor="#ccffcc"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-actions">
                                        <div class="row">
                                            <div class="col-md-offset-3 col-md-9">
                                                <div class="col-md-offset-3 col-md-9">
                                                    <asp:Button ID="Btn_MTCancel" runat="server" OnClick="btnModal_CancelInst_Click" Text="Cancel Entry" CssClass="btn btn-success" />
                                                    <asp:Button ID="Btn_Close" Text="Close" runat="server" CssClass="btn btn-success" data-dismiss="modal" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="table-scrollable" style="width: 100%; height: auto; max-height: 500px; overflow-x: auto; overflow-y: auto">
                    <table class="table table-striped table-bordered table-hover">
                        <thead>
                            <tr>
                                <th>
                                    <asp:GridView ID="GrdMultiTransfer" runat="server" AlternatingRowStyle-CssClass="alt" CssClass="mGrid" Width="100%"
                                        AutoGenerateColumns="false" EditRowStyle-BackColor="#FFFF99" EmptyDataText="No Records Available">
                                        <Columns>

                                            <asp:TemplateField HeaderText="SetNo" Visible="true" HeaderStyle-BackColor="#66ccff">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSetNo" runat="server" Text='<%# Eval("SETNO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="EntryDate" Visible="true" HeaderStyle-BackColor="#66ccff">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEDate" runat="server" Text='<%# Eval("ENTRYDATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="CustNo" Visible="true" HeaderStyle-BackColor="#66ccff">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCustNo" runat="server" Text='<%# Eval("CUSTNO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Product Code" HeaderStyle-BackColor="#66ccff">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSubglcode" runat="server" Text='<%# Eval("SUBGLCODE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="GLName" HeaderStyle-BackColor="#66ccff">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGlname" runat="server" Text='<%# Eval("GLNAME") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Accno" HeaderStyle-BackColor="#66ccff">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAccNo" runat="server" Text='<%# Eval("ACCNO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="CustName" Visible="true" HeaderStyle-BackColor="#66ccff">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCustname" runat="server" Text='<%# Eval("CUSTNAME") %>'></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Particulars" HeaderStyle-BackColor="#66ccff">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblParticulars" runat="server" Text='<%# Eval("PARTICULARS") %>'></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Particulars2" HeaderStyle-BackColor="#66ccff">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPArticulars2" runat="server" Text='<%# Eval("PARTICULARS2") %>'></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Credit" HeaderStyle-BackColor="#ffccff">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCredit" runat="server" Text='<%# Eval("CREDIT") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Debit" HeaderStyle-BackColor="#ccffcc">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDebit" runat="server" Text='<%# Eval("DEBIT") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="PmtMode" HeaderStyle-BackColor="#66ccff">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPmtMode" runat="server" Text='<%# Eval("PMTMODE") %>'></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="BRCD" HeaderStyle-BackColor="#66ccff">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblbrcd" runat="server" Text='<%# Eval("BRCD") %>'></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="MID" HeaderStyle-BackColor="#66ccff">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMID" runat="server" Text='<%# Eval("MID") %>'></asp:Label>
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

    <div id="AgentCommision" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg" style="width: 80%">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Cancel Screen</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="portlet box green">
                                <div class="portlet-title">
                                    <div class="caption">
                                        Agent Commission
                                <asp:Literal ID="Literal3" runat="server"></asp:Literal>
                                    </div>
                                    <div class="tools">
                                        <a href="javascript:;" class="collapse" data-original-title="" title=""></a>
                                        <a href="#portlet-config" data-toggle="modal" class="config" data-original-title="" title=""></a>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-12">
                                <label class="control-label col-md-3">Agent Code <span class="required">*</span></label>
                                <div class="col-lg-2">
                                    <asp:TextBox ID="txtAgCode" placeholder="AGENT CODE" Enabled="false" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtAgName" placeholder="AGENT NAME" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <asp:Label ID="CustNo" runat="server" class="control-label col-md-1"></asp:Label>
                            </div>
                        </div>
                        <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-12">
                                <label class="control-label col-md-3">From date <span class="required">*</span></label>
                                <div class="col-lg-3">
                                    <asp:TextBox ID="txtFDate" onkeyup="FormatIt(this)" Enabled="false" CssClass="form-control" PlaceHolder="DD/MM/YYYY" runat="server" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                </div>
                                <label class="control-label col-md-3">To date <span class="required">*</span></label>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtTDate" Enabled="false" onkeyup="FormatIt(this)" CssClass="form-control" PlaceHolder="DD/MM/YYYY" runat="server" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-12">
                                <label class="control-label col-md-3">Total Collection</label>
                                <div class="col-lg-3">
                                    <asp:TextBox ID="txttcoll" Enabled="false" CssClass="form-control" runat="server" />
                                </div>
                                <label class="control-label col-md-3">Commision Collection </label>
                                <div class="col-lg-3">
                                    <asp:TextBox ID="txtTotColl" onkeypress="javascript:return isNumber (event)" Enabled="false" CssClass="form-control" runat="server" />
                                </div>

                            </div>
                        </div>
                        <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-12">
                                <label class="control-label col-md-3">Commission % </label>
                                <div class="col-lg-3">
                                    <asp:TextBox ID="txtCommision" Enabled="false" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" />
                                </div>
                                <label class="control-label col-md-3" id="lblCommision" runat="server">Commission Amt </label>
                                <div class="col-lg-3">
                                    <asp:TextBox ID="txtCommAmt" Enabled="false" CssClass="form-control" runat="server" ToolTip="1004" />
                                </div>


                            </div>
                        </div>
                        <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-12">
                                <label class="control-label col-md-3">TDS % </label>
                                <div class="col-lg-3">
                                    <asp:TextBox ID="txtTDDeduction" Enabled="false" CssClass="form-control" runat="server" />
                                </div>
                                <label class="control-label col-md-3" id="lblTDS" runat="server">TDS</label>
                                <div class="col-lg-3">
                                    <asp:TextBox ID="txtTdAmt" Enabled="false" CssClass="form-control" runat="server" ToolTip="1007" />
                                </div>
                            </div>
                        </div>
                        <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-12">
                                <label class="control-label col-md-3">Agent Sec % </label>
                                <div class="col-lg-3">
                                    <asp:TextBox ID="TxtAGCDSEC" Enabled="false" CssClass="form-control" runat="server" />
                                </div>
                                <label class="control-label col-md-3" id="lblSec" runat="server">Agent Security </label>
                                <div class="col-lg-3">
                                    <asp:TextBox ID="TxtAgentSec" Enabled="false" CssClass="form-control" runat="server" ToolTip="1005" />
                                </div>

                            </div>
                        </div>
                        <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-12">
                                <div class="col-lg-3">
                                </div>
                                <div class="col-lg-3">
                                </div>
                                <label class="control-label col-md-3" id="lblPF" runat="server">AMC </label>
                                <div class="col-lg-3">
                                    <asp:TextBox ID="TXtAGCDAMC" Enabled="false" CssClass="form-control" ToolTip="1008" runat="server" />
                                </div>

                            </div>
                        </div>
                        <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-12">
                                <label class="control-label col-md-3">Travelling Exp %</label>
                                <div class="col-lg-3">
                                    <asp:TextBox ID="txttrev" Enabled="false" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" />
                                </div>
                                <label class="control-label col-md-3" id="lblTrav" runat="server">Travelling Exp </label>
                                <div class="col-lg-3">
                                    <asp:TextBox ID="txttravelexp" Enabled="false" onkeypress="javascript:return isNumber (event)" CssClass="form-control" ToolTip="1006" runat="server" />
                                </div>

                            </div>
                        </div>
                        <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-12">
                                <label class="control-label col-md-3">Net Commision </label>
                                <div class="col-lg-3">
                                    <asp:TextBox ID="txtNetCommision" Enabled="false" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" />
                                </div>
                            </div>
                        </div>
                        <div class="form-actions">
                            <div class="row">
                                <div class="col-md-offset-3 col-md-9">
                                    <div class="col-md-offset-3 col-md-9">
                                        <asp:Button ID="BtnAgCancel" runat="server" OnClick="btnModal_BtnAgCancel_Click" Text="Cancel Entry" CssClass="btn btn-success" />
                                        <asp:Button ID="ExitAg" Text="Close" runat="server" CssClass="btn btn-success" data-dismiss="modal" />
                                    </div>
                                </div>
                            </div>
                        </div>


                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="MobileMultiPost" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg" style="width:80%">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Mobile MultiPosting Cancel Entry</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="portlet box green">
                                <div class="portlet-title">
                                     <h3 class="modal-title" style="color:white" >Mobile MultiPosting Cancel Entry</h3>
                                    
                                </div>

                                <div class="portlet-body">
                                    <div class="row" style="margin: 7px 0 7px 0;">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Total Cr. Amount<span class="required">* </span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtCredit" Enabled="false" runat="server" CssClass="form-control" AutoPostBack="true" BackColor="#ffccff"></asp:TextBox>
                                            </div>
                                            <div class="col-md-4">
                                            </div>
                                            <label class="control-label col-md-2">Total Dr. Amount<span class="required">* </span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtdebit" Enabled="false" runat="server" CssClass="form-control" AutoPostBack="true" BackColor="#ccffcc"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-actions">
                                        <div class="row">
                                            <div class="col-md-offset-3 col-md-9">
                                                <div class="col-md-offset-3 col-md-9">
                                                  
                                                    <asp:Button ID="btnModal_CancelMulti" runat="server" OnClick= "btnModal_CancelMulti_Click" Text="Cancel Entry" CssClass="btn btn-success" />
                                                    <asp:Button ID="Close" Text="Close" runat="server" CssClass="btn btn-success" data-dismiss="modal" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="table-scrollable" style="width: 100%; height: auto; max-height: 500px; overflow-x: auto; overflow-y: auto">
                    <table class="table table-striped table-bordered table-hover">
                        <thead>
                            <tr>
                                <th>
                                    <asp:GridView ID="GrdMultipost" runat="server" AlternatingRowStyle-CssClass="alt" CssClass="mGrid" Width="100%"
                                        AutoGenerateColumns="false" EditRowStyle-BackColor="#FFFF99" EmptyDataText="No Records Available">
                                        <Columns>

                                            <asp:TemplateField HeaderText="EntryDate" Visible="true" HeaderStyle-BackColor="#66ccff">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEntryDate" runat="server" Text='<%# Eval("EntryDate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="ReceiptNo" Visible="true" HeaderStyle-BackColor="#66ccff">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSetNo" runat="server" Text='<%# Eval("SetNo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Pr Code" HeaderStyle-BackColor="#66ccff">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSubGlCode" runat="server" Text='<%# Eval("SubGlCode") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="GL Name" HeaderStyle-BackColor="#66ccff">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGlName" runat="server" Text='<%# Eval("GlName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Acc No" HeaderStyle-BackColor="#66ccff">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAccNo" runat="server" Text='<%# Eval("AccNo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="CustName" Visible="true" HeaderStyle-BackColor="#66ccff">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCustName" runat="server" Text='<%# Eval("CustName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Particulars" HeaderStyle-BackColor="#66ccff">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblParticulars" runat="server" Text='<%# Eval("Parti1") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Credit" HeaderStyle-BackColor="#ffccff">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCredit" runat="server" Text='<%# Eval("Credit") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Debit" HeaderStyle-BackColor="#ccffcc">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDebit" runat="server" Text='<%# Eval("Debit") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="Th" HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="PmtMode" HeaderStyle-BackColor="#66ccff">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPmtMode" runat="server" Text='<%# Eval("PmtMode") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Mid" HeaderStyle-BackColor="#66ccff">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMid" runat="server" Text='<%# Eval("Mid") %>'></asp:Label>
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

