<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmVoucherAutho.aspx.cs" Inherits="FrmVoucherAutho" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
       <%-- Only Numbers --%>
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;
            return true; TxtDDSPreMCAMT

        }
        // Reference the textbox and call its focus function
        function focus(evt) {
            var txt = $("#txtName");

            txt.focus();

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        #table1 td {
            border: solid thin black;
        }

        #table2 td {
            border: solid thin black;
        }
    </style>
    <style>
        .zoom {
            transition: transform .3s; /* Animation */
            width: 200px;
            height: 200px;
            margin: 0 auto;
        }

            .zoom:hover {
                transform: scale(1.5); /* (150% zoom - Note: if the zoom is too large, it will go outside of the viewport) */
            }
    </style>
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        Voucher Authorisation
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab__blue">
                                        <div id="Div6" class="row" style="margin: 7px 0 7px 0" runat="server">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Type<span class="required">* </span></label>
                                                <div class="col-md-6">
                                                    <asp:RadioButton ID="Rdb_Single" runat="server" Text="Single Pass" AutoPostBack="true" GroupName="TYPE1" Checked="true" OnCheckedChanged="Rdb_Single_CheckedChanged" />
                                                    <asp:RadioButton ID="Rdb_Lot" runat="server" Text="Lot Pass" AutoPostBack="true" GroupName="TYPE1" OnCheckedChanged="Rdb_Lot_CheckedChanged" />
                                                     <asp:Label ID="LblMsg" runat="server" Text=" [Alert : Lot Passing will not send transaction SMS to customer]" ForeColor="Red" Font-Bold="true"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 20px; border-bottom: 1px solid rgba(53, 152, 220, 0.55);"><strong></strong></div>
                                        <div class="row" style="margin: 7px 0px 7px 0px" id="Div_Single" runat="server">
                                            <div class="col-md-12">
                                                <div class="col-md-1" style="width: 70px">
                                                    <asp:Label ID="lbldate" runat="server" Text="Date :"></asp:Label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtDate" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:TextBoxWatermarkExtender ID="TxtDate_WatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtDate">
                                                    </asp:TextBoxWatermarkExtender>
                                                    <asp:CalendarExtender ID="TxtDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtDate">
                                                    </asp:CalendarExtender>
                                                </div>
                                                <div class="col-md-1" style="width: 120px">
                                                    <asp:Label ID="lblPrdCD" runat="server" Text="Product Code :"></asp:Label>
                                                </div>
                                                <div class="col-md-2" style="width: 150px">
                                                    <asp:TextBox ID="TxtPType" runat="server" placeholder="Product Code" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-md-1" style="width: 70px">
                                                    <asp:Label ID="lblUsers" runat="server" Text="User :"></asp:Label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtUserId" runat="server" placeholder="User" CssClass="form-control" AutoPostBack="true" OnTextChanged="TxtUserId_TextChanged"> </asp:TextBox>
                                                    <asp:AutoCompleteExtender ID="autoUName" runat="server" TargetControlID="TxtUserId"
                                                        UseContextKey="true"
                                                        CompletionInterval="1"
                                                        CompletionSetCount="20"
                                                        MinimumPrefixLength="1"
                                                        EnableCaching="true"
                                                        ServicePath="~/WebServices/Contact.asmx"
                                                        ServiceMethod="GetUserName">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                                <div class="col-md-1">
                                                    <asp:Label ID="lblSetno" runat="server" Text="Set no :"></asp:Label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtSetno" runat="server" placeholder="Set No" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0" runat="server" id="Div_Lot" visible="false">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-1">From Setno <span class="required"></span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtFSetno" OnTextChanged="TxtFSetno_TextChanged" Placeholder="From Setno" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-1">To Setno<span class="required"></span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtTSetno" Placeholder="To Setno" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
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
                                                <asp:Button ID="btnSearch" runat="server" CssClass="btn blue" Text="Submit" OnClick="btnSearch_Click" />
                                                <asp:Button ID="Btn_Submit" runat="server" Text="Submit" CssClass="btn blue" OnClick="Btn_Submit_Click" Visible="false" />
                                                <asp:Button ID="BtnClear" runat="server" CssClass="btn blue" Text="ClearAll" OnClick="BtnClear_Click" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-12">
                                                 <asp:Label ID="Lbl_UnAu" runat="server" Text="List Of UnAuthorized Sets" BackColor="#66ccff" Font-Bold="true" Font-Size="Medium"></asp:Label>
                                                <div class="table-scrollable">
                                                    <table class="table table-striped table-bordered table-hover">
                                                        <thead>
                                                            <tr>
                                                                <th>
                                                                    <asp:GridView ID="grdCashRct" runat="server" AllowPaging="True"
                                                                        AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                                        EditRowStyle-BackColor="#FFFF99" PageSize="25"
                                                                        OnPageIndexChanging="grdCashRct_PageIndexChanging"
                                                                        PagerStyle-CssClass="pgr" Width="100%">
                                                                        <Columns>

                                                                            <asp:TemplateField HeaderText="Reciept No" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="SET_NO" runat="server" Text='<%# Eval("SETNO") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Scroll No" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="AT" runat="server" Text='<%# Eval("Scrollno1") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Product Code" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="SGL" runat="server" Text='<%# Eval("subglcode") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Acc No" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="ACNO" runat="server" Text='<%# Eval("AccNO") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="PARTICULARS" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="Particulars" runat="server" Text='<%# Eval("PARTICULARS") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Credit" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="CR" runat="server" Text='<%# Eval("Credit") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Debit" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="DR" runat="server" Text='<%# Eval("Debit") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Maker" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="MID" runat="server" Text='<%# Eval("LOGINCODE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Authorise" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("Scrollno")%>' CommandName="select" OnClick="lnkEdit_Click" class="glyphicon glyphicon-check"></asp:LinkButton>
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
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="CASHP" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg" style="width: 100%">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title" style="text-align: center; font: 100; font-family: Verdana; font-size: larger; font-style: italic">Authorization Screen</h4>
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
                                                <table>
                                                    <tr>
                                                        <td style="width: 80%">
                                                            <div class="tab-content">
                                                                <div id="error">
                                                                </div>
                                                                <div class="tab-pane active" id="tab1">
                                                                    <asp:Table ID="Table1" runat="server">
                                                                        <asp:TableRow ID="TableRow1" runat="server" Style="width: 300px">
                                                                            <asp:TableCell ID="TableCell1" runat="server" Style="width: 2000px">


                                                                                <div class="row" style="margin: 7px 0 7px 0">
                                                                                    <div class="col-lg-12">
                                                                                        <label class="control-label col-md-2">Product Type :</label>

                                                                                        <div class="col-md-1">
                                                                                            <asp:TextBox ID="TxtProcodeCP" placeholder="PRODUCT TYPE" CssClass="form-control" runat="server" AutoPostBack="true" TabIndex="2" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                                                            <%--<asp:TextBox ID="TextBox1" CssClass="form-control" runat="server"  AutoPostBack="true" OnTextChanged="TxtProcode_TextChanged" ></asp:TextBox>--%>
                                                                                        </div>
                                                                                        <div class="col-md-3">
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
                                                                                        <label class="control-label col-md-2">Entry Date : <span class="required">* </span></label>
                                                                                        <div class="col-md-2">
                                                                                            <asp:TextBox ID="TxtEntrydateCP" CssClass="form-control" runat="server"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="row" style="margin: 7px 0 7px 0">
                                                                                    <div class="col-lg-12">
                                                                                        <label class="control-label col-md-2">Account No :</label>
                                                                                        <div class="col-md-1">
                                                                                            <asp:TextBox ID="TxtAccNoCP" placeholder="ACCOUNT NUMBER" CssClass="form-control" runat="server" AutoPostBack="true" TabIndex="4" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                                                            <%--<asp:TextBox ID="TextBox1" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="TxtAccNo_TextChanged"></asp:TextBox>--%>
                                                                                        </div>
                                                                                        <div class="col-md-3">
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
                                                                                        <label class="control-label col-md-2">Customer:<span class="required"></span></label>
                                                                                        <div class="col-md-2">
                                                                                            <asp:TextBox ID="TextBox6" CssClass="form-control" PlaceHolder="Cust No" runat="server" Enabled="false"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="row" style="margin: 7px 0 7px 0">

                                                                                    <div class="col-lg-12">
                                                                                        <label class="control-label col-md-2">Account Type </label>
                                                                                        <div class="col-md-3">
                                                                                            <asp:TextBox ID="txtAccTypeName" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                                                        </div>
                                                                                        <label class="control-label col-md-2" runat="server" visible="false" id="lbjoint">Joint Name </label>
                                                                                        <div class="col-md-4">
                                                                                            <asp:TextBox ID="TxtJointName" CssClass="form-control" Visible="false" runat="server" Enabled="false"></asp:TextBox>
                                                                                        </div>

                                                                                    </div>
                                                                                </div>

                                                                                <div class="row" style="margin: 7px 0 7px 0">
                                                                                    <div class="col-lg-12">
                                                                                        <label class="control-label col-md-2">Spl Inst. </label>
                                                                                        <div class="col-md-4">
                                                                                            <asp:TextBox ID="TxtSplInst" CssClass="form-control" runat="server" Enabled="false" TextMode="MultiLine"></asp:TextBox>
                                                                                        </div>
                                                                                        <label class="control-label col-md-2">PAN Card: </label>
                                                                                        <div class="col-md-2">
                                                                                            <asp:TextBox ID="txtPan" placeholder="PAN Card" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="row" style="margin: 7px 0 7px 0">
                                                                                    <div class="col-lg-12">
                                                                                        <label class="control-label col-md-2">Voucher Type :</label>
                                                                                        <div class="col-md-2">
                                                                                            <asp:TextBox ID="TxtVoucherTypeno" placeholder="Voucher Type" CssClass="form-control" runat="server" TabIndex="5" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                                                            <%--<asp:TextBox ID="TextBox1" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="TxtAccNo_TextChanged"></asp:TextBox>--%>
                                                                                        </div>
                                                                                        <div class="col-md-3">
                                                                                            <asp:TextBox ID="TxtVouchertype" placeholder="Voucher Type" CssClass="form-control" runat="server" TabIndex="6" Enabled="false"></asp:TextBox>
                                                                                        </div>
                                                                                        <div runat="server" visible="false" id="DIVINSTRNO">
                                                                                            <label class="control-label col-md-2">Instrument No:</label>
                                                                                            <div class="col-md-2">
                                                                                                <asp:TextBox ID="TxtInstruNo" CssClass="form-control" PlaceHolder="Inst No" runat="server" TabIndex="7"></asp:TextBox>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="row" style="margin: 7px 0 7px 0">
                                                                                    <div class="col-lg-12">
                                                                                        <label class="control-label col-md-2">Naration :</label>
                                                                                        <div class="col-md-3">
                                                                                            <asp:TextBox ID="txtnarationCP" CssClass="form-control" runat="server" Text="To Cash" Enabled="false"></asp:TextBox>
                                                                                        </div>
                                                                                        <div class="col-md-1"></div>
                                                                                        <label class="control-label col-md-2">Token No:</label>
                                                                                        <div class="col-md-2">
                                                                                            <asp:TextBox ID="txtToken" placeholder="Token No" CssClass="form-control" runat="server"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="row" style="margin: 7px 0 7px 0">
                                                                                    <div class="col-lg-12">

                                                                                        <label class="control-label col-md-2">Clear Balance :</label>
                                                                                        <div class="col-md-2">
                                                                                            <asp:TextBox ID="txtBalanceCP" placeholder="OLD BALANCE" CssClass="form-control" runat="server" TabIndex="-1" ReadOnly="true"></asp:TextBox>
                                                                                        </div>
                                                                                        <div class="col-md-1"></div>
                                                                                        <div id="Div_Amt" runat="server" visible="false">
                                                                                            <label class="control-label col-md-2">Amount :</label>
                                                                                            <div class="col-md-2">
                                                                                                <asp:TextBox ID="TxtAmountCP" Style="background-color: lightgreen" placeholder="DEBIT AMOUNT" CssClass="form-control" runat="server" TabIndex="8" onkeypress="javascript:return isNumber (event)" AutoPostBack="true"></asp:TextBox>
                                                                                            </div>

                                                                                        </div>
                                                                                        <div runat="server" visible="false" id="Div_Instdate">
                                                                                            <label class="control-label col-md-2">Inst Date<span class="required"></span></label>
                                                                                            <div class="col-md-2">
                                                                                                <asp:TextBox ID="TextBox7" CssClass="form-control" onkeyup="FormatIt(this)" PlaceHolder="Inst Date" runat="server"></asp:TextBox>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row" style="margin: 7px 0 7px 0">
                                                                                    <div class="col-lg-12">

                                                                                        <label class="control-label col-md-2">Total Balance : </label>
                                                                                        <div class="col-md-3">
                                                                                            <asp:TextBox ID="TxtNewBalanceCP" Style="background-color: lightcoral" placeholder="BALANCE" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                                                                        </div>
                                                                                        <div class="col-md-2"></div>
                                                                                        <div class="col-md-1">
                                                                                            <asp:Label ID="Lbl_PassAmountCP" runat="server" Text="Pass Amount:"></asp:Label>
                                                                                            <%--Dhanya Shetty//06/07/2017--%>
                                                                                        </div>
                                                                                        <div class="col-md-2">
                                                                                            <asp:TextBox ID="TxtPassCP" onkeypress="javascript:return isNumber (event)" placeholder="Amount" CssClass="form-control" runat="server"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </asp:TableCell>


                                                                        </asp:TableRow>
                                                                    </asp:Table>
                                                                </div>

                                                            </div>
                                                        </td>
                                                        <td style="width: 20%">
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td>
                                                                        <div>
                                                                            <asp:Label ID="Label12" runat="server" Text="Signature" Style="font-weight: bold"></asp:Label>
                                                                        </div>
                                                                        <%--<div style="width:100%;height:100%;padding:5px" onclick="ShowUpload(1)" runat="server" id="DivSign" align="center">--%>

                                                                        <img id="Img7" runat="server" style="height: 70%; width: 70%; border: 1px solid #000000; padding: 5px" />

                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <div>
                                                                            <asp:Label ID="Label13" runat="server" Text="Photo" Style="font-weight: bold"></asp:Label>
                                                                        </div>

                                                                        <%--<div style="width:100%;height:100%;padding:5px" onclick="ShowUpload(2)" runat="server" id="Div2" align="center">--%>
                                                                        <img id="Img8" runat="server" style="height: 70%; width: 70%; border: 1px solid #000000; padding: 5px" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                        <div class="form-actions">
                                            <div class="row">
                                                <div class="col-md-offset-3 col-md-9">
                                                    <asp:Button ID="BtnModal_AuthorizeCP" OnClick="BtnModal_AuthorizeCP_Click" runat="server" Text="Authorize" CssClass="btn btn-success" />
                                                    <asp:Button ID="btnModal_CancelCP" runat="server" OnClick="btnModal_CancelInst_Click" Text="Cancel Entry" CssClass="btn btn-success" />
                                                    <asp:Button ID="BtnModal_CloseCP" Text="Close" runat="server" CssClass="btn btn-success" data-dismiss="modal" />
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

    <div id="CASHR" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg" style="width: 100%">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title" style="text-align: center; font-family: Verdana; font-size: medium; font-style: italic">Authorization Screen</h4>
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
                                            <table>
                                                <tr>
                                                    <td style="width: 80%">
                                                        <div class="form-body" style="width: 100%">

                                                            <div class="tab-content">
                                                                <div id="Div8">
                                                                </div>
                                                                <div class="tab-pane active" id="Div9">
                                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                                        <div class="col-lg-12">
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
                                                                        <div class="col-lg-12">
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
                                                                        <div class="col-lg-12">
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
                                                                        <div class="col-lg-12">
                                                                            <label class="control-label col-md-3">A/C Type </label>
                                                                            <div class="col-md-3">
                                                                                <asp:TextBox ID="TxtAcctypeCR" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                                            </div>
                                                                            <label class="control-label col-md-2" runat="server" visible="false" id="lbjointCR">Joint Name </label>
                                                                            <div class="col-md-4">
                                                                                <asp:TextBox ID="TxtJoinCR" CssClass="form-control" Visible="false" runat="server" Enabled="false"></asp:TextBox>
                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                                        <div class="col-lg-12">
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
                                                                        <div class="col-lg-12">

                                                                            <label class="control-label col-md-3">Clear Balance : <span class="required">* </span></label>
                                                                            <div class="col-md-3">
                                                                                <asp:TextBox ID="TxtOldBalCR" placeholder="OLD BALANCE" CssClass="form-control" runat="server" TabIndex="-1" ReadOnly="true"></asp:TextBox>
                                                                            </div>
                                                                            <div id="Div1" runat="server" visible="false">
                                                                                <label class="control-label col-md-2">Amount : <span class="required">* </span></label>
                                                                                <div class="col-md-3">
                                                                                    <asp:TextBox ID="TxtAmountCR" Enabled="false" placeholder="CREDIT AMOUNT" CssClass="form-control" runat="server" TabIndex="9" onkeypress="javascript:return isNumber (event)" AutoPostBack="true"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                                        <div class="col-lg-12">

                                                                            <label class="control-label col-md-3">Total Balance : <span class="required">* </span></label>
                                                                            <div class="col-md-3">
                                                                                <asp:TextBox ID="TxtNewBalCR" placeholder="BALANCE" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                                                            </div>
                                                                            <%--Dhanya Shetty//06/07/2017--%>
                                                                            <div class="col-md-2">
                                                                                <asp:Label ID="Lbl_PassAmountCR" runat="server" Text="PassAmount:"></asp:Label>
                                                                            </div>
                                                                            <div class="col-md-4">
                                                                                <asp:TextBox ID="TxtPassCR" onkeypress="javascript:return isNumber (event)" placeholder="Amount" CssClass="form-control" runat="server"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                                        <div class="col-lg-12">
                                                                            <label class="control-label col-md-2">Spl Inst. </label>
                                                                            <div class="col-md-4">
                                                                                <asp:TextBox ID="TxtSplInstCR" CssClass="form-control" runat="server" Enabled="false" TextMode="MultiLine"></asp:TextBox>
                                                                            </div>
                                                                            <label class="control-label col-md-2">PAN Card: </label>
                                                                            <div class="col-md-4">
                                                                                <asp:TextBox ID="txtPanR" placeholder="PAN Card" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                            </div>

                                                        </div>

                                                    </td>
                                                    <td style="width: 20%">
                                                        <table style="width: 100%">
                                                            <tr>
                                                                <td>
                                                                    <div>
                                                                        <asp:Label ID="Label6" runat="server" Text="Signature" Style="font-weight: bold"></asp:Label>
                                                                    </div>
                                                                    <%--<div style="width:100%;height:100%;padding:5px" onclick="ShowUpload(1)" runat="server" id="DivSign" align="center">--%>
                                                                    <div class="zoom" style="height: 100%; width: 100%">
                                                                        <img id="Img1" runat="server" style="height: 70%; width: 70%; border: 1px solid #000000; padding: 5px" />
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <div>
                                                                        <asp:Label ID="Label7" runat="server" Text="Photo" Style="font-weight: bold"></asp:Label>
                                                                    </div>
                                                                    <div class="zoom" style="height: 100%; width: 100%">
                                                                        <%--<div style="width:100%;height:100%;padding:5px" onclick="ShowUpload(2)" runat="server" id="Div2" align="center">--%>
                                                                        <img id="Img2" runat="server" style="height: 70%; width: 70%; border: 1px solid #000000; padding: 5px" />
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>

                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div class="form-actions">
                                            <div class="row">
                                                <div class="col-md-offset-3 col-md-9">
                                                    <asp:Button ID="BtnModal_AuthorizeCR" runat="server" OnClick="BtnModal_AuthorizeCP_Click" Text="Authorize" CssClass="btn btn-success" />
                                                    <asp:Button ID="btnModal_CancelCR" runat="server" OnClick="btnModal_CancelInst_Click" Text="Cancel Entry" CssClass="btn btn-success" />
                                                    <asp:Button ID="BtnModal_ExitCR" Text="Close" runat="server" CssClass="btn btn-success" data-dismiss="modal" />
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

    <div id="LOANINST" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg" style="width: 100%">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Authorization Screen</h4>
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
                                            <table>
                                                <tr>
                                                    <td style="width: 80%">

                                                        <div class="form-body" style="width: 100%">
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
                                                                                <div class="col-md-1">
                                                                                    <label class="control-label ">PrCode</label>
                                                                                </div>
                                                                                <div class="col-md-2">
                                                                                    <asp:TextBox ID="txtProdType" Enabled="false" Placeholder="Product Type" runat="server" CssClass="form-control" />
                                                                                </div>
                                                                                <div class="col-md-3">
                                                                                    <asp:TextBox ID="txtProdName" Enabled="false" Placeholder="Product Name" runat="server" CssClass="form-control" />
                                                                                </div>
                                                                                <div class="col-md-1">
                                                                                    <label class="control-label ">Acc No</label>
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
                                                                                <div class="col-md-1">
                                                                                    <label class="control-label ">CustNo</label>
                                                                                </div>
                                                                                <div class="col-md-2">
                                                                                    <asp:TextBox ID="txtCustNo" Enabled="false" Placeholder="Customer Number" runat="server" CssClass="form-control" />
                                                                                </div>
                                                                                <div class="col-md-1">
                                                                                    <label class="control-label ">Status</label>
                                                                                </div>
                                                                                <div class="col-md-2">
                                                                                    <asp:TextBox ID="txtAccStatus" Enabled="false" CssClass="form-control" runat="server" />
                                                                                </div>
                                                                                <div class="col-md-2">
                                                                                    <asp:DropDownList ID="ddlAccStatus" Enabled="false" CssClass="form-control" runat="server"></asp:DropDownList>
                                                                                </div>
                                                                                <div class="col-md-2">
                                                                                    <label class="control-label ">Deposit Amount</label>
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
                                                                                <div class="col-md-2">
                                                                                    <label class="control-label">PAN Card: </label>
                                                                                </div>
                                                                                <div class="col-md-2">
                                                                                    <asp:TextBox ID="txtPanL" placeholder="PAN Card" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
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
                                                                                <div class="col-md-2">
                                                                                    <asp:DropDownList ID="ddlPayType" Enabled="false" runat="server" CssClass="form-control">
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                                <div class="col-md-2">
                                                                                    <label class="control-label ">Before Authorise Bal</label>
                                                                                </div>
                                                                                <div class="col-md-2">
                                                                                    <asp:TextBox ID="txtBefClbal" Enabled="false" placeholder="Before Authorise" CssClass="form-control" runat="server" />
                                                                                </div>
                                                                                <div class="col-md-2">
                                                                                    <label class="control-label ">After Authorise Bal</label>
                                                                                </div>
                                                                                <div class="col-md-2">
                                                                                    <asp:TextBox ID="txtAftClBal" Enabled="false" placeholder="After Authorise" CssClass="form-control" runat="server" />
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

                                                                        <div runat="server">
                                                                            <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                                                                <div class="col-lg-12">
                                                                                    <div class="col-md-2">
                                                                                        <label class="control-label ">Naration:<span class="required">*</span></label>
                                                                                    </div>
                                                                                    <div class="col-md-2">
                                                                                        <asp:TextBox ID="txtNarration" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                                                                    </div>

                                                                                    <%--Dhanya Shetty//06/07/2017--%>
                                                                                    <div class="col-md-2">
                                                                                        <asp:Label ID="Lbl_PassAmount" runat="server" Text="PassAmount :"></asp:Label>
                                                                                    </div>
                                                                                    <div class="col-md-2">
                                                                                        <asp:TextBox ID="Txtentramt" onkeypress="javascript:return isNumber (event)" placeholder="Amount" CssClass="form-control" runat="server"></asp:TextBox>
                                                                                    </div>

                                                                                    <div id="Div3" runat="server" visible="false">
                                                                                        <div class="col-md-2">
                                                                                            <label class="control-label ">Amount : <span class="required">*</span></label>
                                                                                        </div>
                                                                                        <div class="col-md-2">
                                                                                            <asp:TextBox ID="txtAmount" Enabled="false" placeholder="Debit amount" CssClass="form-control" runat="server"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td style="width: 20%">
                                                        <table style="width: 100%">
                                                            <tr>
                                                                <td>
                                                                    <div>
                                                                        <asp:Label ID="lbl1" runat="server" Text="Signature" Style="font-weight: bold"></asp:Label>
                                                                    </div>
                                                                    <%--<div style="width:100%;height:100%;padding:5px" onclick="ShowUpload(1)" runat="server" id="DivSign" align="center">--%>
                                                                    <div class="zoom" style="height: 100%; width: 100%">
                                                                        <img id="image1" runat="server" style="height: 70%; width: 70%; border: 1px solid #000000; padding: 5px" />
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <div>
                                                                        <asp:Label ID="Label5" runat="server" Text="Photo" Style="font-weight: bold"></asp:Label>
                                                                    </div>

                                                                    <%--<div style="width:100%;height:100%;padding:5px" onclick="ShowUpload(2)" runat="server" id="Div2" align="center">--%>
                                                                    <div class="zoom" style="height: 100%; width: 100%">
                                                                        <img id="image2" runat="server" style="height: 70%; width: 70%; border: 1px solid #000000; padding: 5px" />
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                            <div class="form-actions">
                                                <div class="row">
                                                    <div class="col-md-offset-3 col-md-9">
                                                        <asp:Button ID="BtnModal_AuthorizeInst" runat="server" OnClick="BtnModal_AuthorizeCP_Click" Text="Authorize" CssClass="btn btn-success" />
                                                        <asp:Button ID="btnModal_CancelInst" runat="server" OnClick="btnModal_CancelInst_Click" Text="Cancel Entry" CssClass="btn btn-success" />
                                                        <asp:Button ID="BtnModal_CloseInst" Text="Close" runat="server" CssClass="btn btn-success" data-dismiss="modal" />
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

    <div id="DDSCLOSE" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg" style="width: 100%">

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

                                <div class="portlet-body" style="width: 100%">
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="vertical-align: top; width: 40%">
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Customer Information : </strong></div>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin: 7px 0 7px 0; margin-top: 10px;">
                                                    <div class="col-md-12" style="height: 33px">
                                                        <label class="control-label col-md-4">Agent Code : <span class="required"></span></label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="TxtDDSAGCD" Enabled="false" runat="server" onkeypress="javascript:return isNumber (event)" CssClass="form-control" PlaceHolder="Agent Code" AutoPostBack="true" Style="width: 100%; height: 33px;" ReadOnly="true"></asp:TextBox>&nbsp;&nbsp;                               
                                                   

                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="row" style="margin: 7px 0 7px 0; margin-top: 10px;">
                                                    <div class="col-md-12">
                                                        <label class="control-label col-md-4"><span class="required"></span></label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="TxtDDSAGName" Enabled="false" runat="server" PlaceHolder="Agent Name" AutoPostBack="true" CssClass="form-control" Style="width: 100%; height: 33px;" ReadOnly="true"></asp:TextBox>
                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="row" style="margin: 7px 0 7px 0; margin-top: 10px;">
                                                    <div class="col-md-12" style="height: 33px">
                                                        <label class="control-label col-md-4">Account No : <span class="required"></span></label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="TxtDDSAccNo" Enabled="false" runat="server" PlaceHolder="Account No" CssClass="form-control" onkeypress="javascript:return isNumber (event)" AutoPostBack="true" Style="width: 100%; height: 33px;" ReadOnly="true"></asp:TextBox>&nbsp;&nbsp;
                                 
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin: 7px 0 7px 0; margin-top: 10px;">
                                                    <div class="col-md-12">
                                                        <label class="control-label col-md-4"><span class="required"></span></label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="TxtDDSAccName" runat="server" Enabled="false" CssClass="form-control" PlaceHolder="Account Holder Name" AutoPostBack="true" Style="width: 100%; height: 33px;" ReadOnly="true"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin: 7px 0 7px 0; margin-top: 10px;">
                                                    <div class="col-lg-12" style="width: 100%; height: 33px">
                                                        <label class="control-label col-md-4">Opening Date: </label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="TxtDDSOpeningDate" runat="server" PlaceHolder="Openig Date" CssClass="form-control" Enabled="false" Style="width: 100%; height: 33px;" ReadOnly="true"></asp:TextBox>&nbsp;&nbsp;                                                                                                            
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin: 7px 0 7px 0; margin-top: 10px;">
                                                    <div class="col-lg-12" style="height: 33px">
                                                        <label class="control-label col-md-4">Deposit Amount </label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="txtDepositAmt" runat="server" PlaceHolder="Deposit Amount" CssClass="form-control" Enabled="false" Style="width: 100%; height: 33px;" ReadOnly="true"></asp:TextBox>&nbsp;&nbsp;                                                                                                            
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin: 7px 0 7px 0; margin-top: 10px;">
                                                    <div class="col-lg-12" style="height: 33px">
                                                        <label class="control-label col-md-4">PAN Card: </label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="txtPanDDS" placeholder="PAN Card" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:Label ID="Lbl_PassDDS" runat="server" Text="PassAmount:"></asp:Label>
                                                    <%--Dhanya Shetty//10/07/2017--%>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="TxtPassDDS" onkeypress="javascript:return isNumber (event)" placeholder="Amount" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </td>
                                            <td style="width: 40%">
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Payment Details : </strong></div>
                                                    </div>
                                                </div>
                                                <table id="table1" style="width: 70%; border: 1px solid #000000; border-collapse: collapse; margin-top: 10px" align="center">
                                                    <tr>
                                                        <td style="width: 70%">&nbsp;&nbsp;&nbsp;<asp:Label ID="Label1" runat="server" Text="Closing Balance :"></asp:Label>
                                                        </td>
                                                        <td style="width: 30%" align="right">
                                                            <asp:Label ID="TxtDDSCBal" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;&nbsp;&nbsp;
                                                            <asp:Label ID="Label2" runat="server" Text="Int Amount :"></asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="TxtDDSCalcInt" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr style="background-color: #66FF99">
                                                        <td>&nbsp;&nbsp;&nbsp;<asp:Label ID="lblTotal" runat="server" Text="Total Amount :"></asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="TxtDDSPayAmt" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr style="background-color: #66FF99">
                                                        <td>&nbsp;&nbsp;&nbsp;<asp:Label ID="lblTotalWithdraw" runat="server" Text="Total Withdraw :"></asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="txtTotalWithdraw" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;&nbsp;&nbsp;
                                                            <asp:Label ID="lblpreCommission" runat="server" Text="Commission :"></asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="TxtDDSPreMCAMT" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;&nbsp;&nbsp;
                                                            <asp:Label ID="lblAdmin" runat="server" Text="Admin/Chg :"></asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="TxtDDSServCHRS" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;&nbsp;&nbsp;
                                                            <asp:Label ID="LblGst" runat="server" Text="GST Chg:"></asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="TxtGstAmt" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr style="background-color: #66FF99">
                                                        <td>&nbsp;&nbsp;&nbsp;<asp:Label ID="lblTDeduction" runat="server" Text="Total Deduction :"></asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="txtDeduction" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
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
                                                <table id="table2" style="width: 85%; border: 1px solid #000000; border-collapse: collapse; margin-top: 10px; margin-bottom: 10px" align="center">
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
                                                     <tr>
                                                        <td>&nbsp;&nbsp;&nbsp;<asp:Label ID="Label16" runat="server" Text="Cheque No."></asp:Label>
                                                        </td>
                                                        <td>&nbsp;&nbsp;&nbsp;<asp:Label ID="Lbl_ChqNo" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td>&nbsp;&nbsp;&nbsp;<asp:Label ID="Label18" runat="server" Text="Cheque Dt."></asp:Label>
                                                        </td>
                                                        <td>&nbsp;&nbsp;&nbsp;<asp:Label ID="Lbl_ChqDate" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>

                                            </td>
                                            <td style="width: 20%">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td>
                                                            <div>
                                                                <asp:Label ID="Label8" runat="server" Text="Signature" Style="font-weight: bold"></asp:Label>
                                                            </div>
                                                            <%--<div style="width:100%;height:100%;padding:5px" onclick="ShowUpload(1)" runat="server" id="DivSign" align="center">--%>
                                                            <div class="zoom" style="height: 100%; width: 100%">
                                                                <img id="Img3" runat="server" style="height: 70%; width: 70%; border: 1px solid #000000; padding: 5px" />
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div>
                                                                <asp:Label ID="Label9" runat="server" Text="Photo" Style="font-weight: bold"></asp:Label>
                                                            </div>

                                                            <%--<div style="width:100%;height:100%;padding:5px" onclick="ShowUpload(2)" runat="server" id="Div2" align="center">--%>
                                                            <div class="zoom" style="height: 100%; width: 100%">
                                                                <img id="Img4" runat="server" style="height: 70%; width: 70%; border: 1px solid #000000; padding: 5px" />
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="center">
                                                <asp:Button ID="Btn_DDSAutho" runat="server" OnClick="BtnModal_AuthorizeCP_Click" Text="Authorize" CssClass="btn btn-success" />
                                                <asp:Button ID="Btn_DDSClose" Text="Close" runat="server" CssClass="btn btn-success" data-dismiss="modal" />
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

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Authorization Screen</h4>
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
                                            <asp:Label ID="Label2" runat="server" Text="Calculated Intr.(A)" class="control-label col-md-2"></asp:Label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="TxtDDSCalcInt" Enabled="false" runat="server" Text="0" PlaceHolder="Interest" onkeyup="MINUS();Sum()" AutoPostBack="true" Style="Width: 33%; height: 33px; border: 1px solid #c0c1c1;"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="row" style="margin: 7px 0 7px 0;">
                                        <div class="col-lg-12">

                                            <asp:Label ID="lblpreCommission" runat="server" Text="Premature Commission :" class="control-label col-md-2"></asp:Label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="TxtDDSPreMC" Enabled="false" runat="server" PlaceHolder="Commission" Style="Width: 33%; height: 33px; border: 1px solid #c0c1c1;"></asp:TextBox>
                                                <asp:TextBox ID="TxtDDSPreMCAMT" Enabled="false" runat="server" PlaceHolder="Commission" AutoPostBack="true" Style="Width: 33%; height: 33px; border: 1px solid #c0c1c1;"></asp:TextBox>
                                            </div>
                                            <label class="control-label col-md-2">Other Receipts : <span class="required">* </span></label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="TxtDDSServCHRS" Enabled="false" runat="server" PlaceHolder="Other Receipts" AutoPostBack="true" Style="Width: 33%; height: 33px; border: 1px solid #c0c1c1;"></asp:TextBox>
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
                                                <asp:TextBox ID="TxtDDSPayAmt" Enabled="false" runat="server" onkeyup="Sum()" PlaceHolder="payeble Amount" Style="Width: 33%; height: 33px; border: 1px solid #c0c1c1;"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin: 7px 0 7px 0; margin-top: 20px;">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Payment Mode : <span class="required">* </span></label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="TxtDDSPayMode" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>

                                            </div>
                                            <asp:Label ID="Label1" runat="server" Text="Outstanding Charges" class="control-label col-md-2"></asp:Label>
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
                                                    <asp:TextBox ID="TxtDDSPType" Enabled="false" runat="server" onkeypress="javascript:return isNumber (event)" PlaceHolder="Product Code" Style="Width: 33%; height: 33px; border: 1px solid #c0c1c1;" AutoPostBack="true"></asp:TextBox>&nbsp;&nbsp;
                                    <asp:TextBox ID="TxtDDSPTName" runat="server" Enabled="false" PlaceHolder="Product Name" Style="Width: 61%; height: 33px; border: 1px solid #c0c1c1;" AutoPostBack="true"></asp:TextBox>

                                                </div>
                                                <label class="control-label col-md-2">Account No : <span class="required">* </span></label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="TxtDDSTAccNo" Enabled="false" runat="server" PlaceHolder="Account No" onkeypress="javascript:return isNumber (event)" Style="Width: 33%; height: 33px; border: 1px solid #c0c1c1;" AutoPostBack="true"></asp:TextBox>&nbsp;&nbsp;
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
                                                    <asp:Button ID="Btn_DDSAutho" runat="server" OnClick="BtnModal_AuthorizeCP_Click" Text="Authorize" CssClass="btn btn-success" />
                                                    <asp:Button ID="Btn_DDSClose" Text="Close" runat="server" CssClass="btn btn-success" data-dismiss="modal" />
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

    <div id="TRANSFER" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg" style="width: 80%">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Authorization Screen</h4>
                </div>
                <div class="modal-body">
                </div>
            </div>
        </div>
    </div>

    <div id="TDCLOSE" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg" style="width: 100%">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Authorization Screen</h4>
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
                                    <table>
                                        <tr>
                                            <td style="width: 80%">
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
                                                            <div id="Div4" runat="server" visible="false">
                                                                <label class="control-label col-md-2">Total Payable Amount</label>
                                                                <div class="col-md-2">
                                                                    <asp:TextBox ID="txtTDPayAmnt" CssClass="form-control" PlaceHolder="Total Payable" runat="server" Enabled="false" BackColor="#cccccc"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <%--Dhanya Shetty//06/07/2017--%>
                                                            <div class="col-md-2">
                                                                <asp:Label ID="Lbl_PassAmountDEPC" runat="server" Text="PassAmount :"></asp:Label>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="TxtPassDepc" onkeypress="javascript:return isNumber (event)" placeholder="Amount" CssClass="form-control" runat="server"></asp:TextBox>
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
                                                                <label class="control-label col-md-2">PAN Card: </label>
                                                                <div class="col-md-2">
                                                                    <asp:TextBox ID="txtPanD" placeholder="PAN Card" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
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
                                                                <asp:Button ID="Btn_Modal_TDAAutho" runat="server" OnClick="BtnModal_AuthorizeCP_Click" Text="Authorize" CssClass="btn btn-success" />
                                                                <asp:Button ID="Btn_Modal_TDACancel" runat="server" Text="Cancel Entry" CssClass="btn btn-success" Visible="false" />
                                                                <asp:Button ID="Btn_Modal_TDAExit" Text="Close" runat="server" CssClass="btn btn-success" data-dismiss="modal" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                            <td style="width: 20%">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td>
                                                            <div>
                                                                <asp:Label ID="Label10" runat="server" Text="Signature" Style="font-weight: bold"></asp:Label>
                                                            </div>
                                                            <%--<div style="width:100%;height:100%;padding:5px" onclick="ShowUpload(1)" runat="server" id="DivSign" align="center">--%>
                                                            <div class="zoom" style="height: 100%; width: 100%">
                                                                <img id="Img5" runat="server" style="height: 60%; width: 100%; border: 1px solid #000000; padding: 5px" />
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div>
                                                                <asp:Label ID="Label11" runat="server" Text="Photo" Style="font-weight: bold"></asp:Label>
                                                            </div>

                                                            <%--<div style="width:100%;height:100%;padding:5px" onclick="ShowUpload(2)" runat="server" id="Div2" align="center">--%>
                                                            <div class="zoom" style="height: 100%; width: 100%">
                                                                <img id="Img6" runat="server" style="height: 60%; width: 100%; border: 1px solid #000000; padding: 5px" />
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
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
                    <h4 class="modal-title">Authorization Screen</h4>
                </div>
                <div class="modal-body">
                    <div class="row" style="margin: 7px 0 7px 0;">
                        <div class="col-lg-6">
                            <div class="col-md-2">
                                <asp:Label ID="Label14" runat="server" Text="Set NO :"></asp:Label>
                            </div>

                            <div class="col-md-1">
                                <asp:Label ID="lblset" runat="server"></asp:Label>

                            </div>
                            <div class="col-md-2">
                                <asp:Label ID="Label15" runat="server" Text="Scroll No :"></asp:Label>
                            </div>

                            <div class="col-md-1">

                                <asp:Label ID="lblScroll" runat="server"></asp:Label>

                            </div>
                        </div>
                    </div>
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
                                    <div id="Div5" runat="server" visible="false">
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
                                    </div>
                                    <div class="col-lg-12" visible="false">
                                        <%--Dhanya Shetty//06/07/2017--%>
                                    </div>

                                    <div class="row" style="margin: 7px 0 7px 0; margin-top: 20px;">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Transfer Type :</label>
                                            <div class="col-md-10">
                                                <asp:RadioButtonList ID="rbtnTransferType" RepeatDirection="Horizontal" runat="server" TabIndex="1">
                                                    <asp:ListItem Text="Transfer" Selected="True" Value="T" />
                                                    <asp:ListItem Text="Cheque" Value="C" />
                                                </asp:RadioButtonList>

                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin: 7px 0 7px 0; margin-top: 20px;">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Transaction Type :</label>
                                            <div class="col-md-2">
                                                <asp:DropDownList ID="DdlCRDR" CssClass="form-control" runat="server" TabIndex="3" Enabled="false">
                                                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                                                    <asp:ListItem Value="1">Credit</asp:ListItem>
                                                    <asp:ListItem Value="2">Debit</asp:ListItem>
                                                </asp:DropDownList>

                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin: 7px 0 7px 0; margin-top: 20px;">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Credit Amount :</label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="TxtCRBAL" CssClass="form-control" runat="server" PlaceHolder="Credit" Enabled="false"></asp:TextBox>
                                            </div>
                                            <label class="control-label col-md-2">Debit Amount :</label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="TxtDRBAL" CssClass="form-control" onkeyup="FormatIt(this)" runat="server" PlaceHolder="Debit" Enabled="false"></asp:TextBox>
                                            </div>
                                            <label class="control-label col-md-2">Diffrence Amount :</label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="TxtDiff" CssClass="form-control" onkeyup="FormatIt(this)" runat="server" PlaceHolder="Diffrence Amount" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin: 7px 0 7px 0; margin-top: 20px;">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Product Code : <span class="required">* </span></label>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="TextBox1" runat="server" onkeypress="javascript:return isNumber (event)" PlaceHolder="Product Code" Style="Width: 20%; height: 33px; border: 1px solid #c0c1c1;" TabIndex="5" Enabled="false"></asp:TextBox>&nbsp;&nbsp;
                                            <asp:TextBox ID="TxtPname" runat="server" PlaceHolder="Product Name" Style="width: 61%; height: 33px; margin-left: -3px; border: 1px solid #c0c1c1;" TabIndex="6" Enabled="false"></asp:TextBox>
                                                <%--<div id="Div6" style="height: 200px; overflow-y: scroll;"></div>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="TxtPname"
                                                UseContextKey="true"
                                                CompletionInterval="1"
                                                CompletionSetCount="20"
                                                MinimumPrefixLength="1"
                                                EnableCaching="true"
                                                ServicePath="~/WebServices/Contact.asmx"
                                                ServiceMethod="GetGlName" CompletionListElementID="CustList">
                                            </asp:AutoCompleteExtender>--%>
                                            </div>
                                            <input type="hidden" id="hdfGlCode" runat="server" value="" />
                                            <div class="col-md-1">
                                                <label class="control-label ">CustNo</label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="TextBox2" CssClass="form-control" Enabled="false" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin: 7px 0 7px 0; margin-top: 20px;">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Account No : <span class="required">* </span></label>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="TextBox3" runat="server" PlaceHolder="Account No" onkeypress="javascript:return isNumber (event)" Style="Width: 20%; height: 33px; border: 1px solid #c0c1c1;" TabIndex="7" Enabled="false"></asp:TextBox>&nbsp;&nbsp;
                                            <asp:TextBox ID="TxtCustName" runat="server" PlaceHolder="Account Holder Name" Style="width: 61%; height: 33px; margin-left: -3px; border: 1px solid #c0c1c1;" TabIndex="8" Enabled="false"></asp:TextBox>
                                                <%-- <div id="Div10" style="height: 200px; overflow-y: scroll;"></div>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="TxtCustName"
                                                UseContextKey="true"
                                                CompletionInterval="1"
                                                CompletionSetCount="20"
                                                MinimumPrefixLength="1"
                                                EnableCaching="true"
                                                ServicePath="~/WebServices/Contact.asmx"
                                                ServiceMethod="GetAccName" CompletionListElementID="CustList2">
                                            </asp:AutoCompleteExtender>--%>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin: 7px 0 7px 0; margin-top: 20px;">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Balance : </label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="TxtBalance" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                            </div>
                                            <label class="control-label col-md-4">Total Balance : </label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="TxtTotalBal" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div id="divCheque" runat="server" visible="false">
                                        <div class="row" style="margin: 7px 0 7px 0; margin-top: 20px;">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Instrument No. :</label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="TxtChequeNo" CssClass="form-control" runat="server" TabIndex="9" PlaceHolder="Instrument No" onkeypress="javascript:return isNumber (event)" Enabled="false"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-2">Instrument Date :</label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="TxtChequeDate" CssClass="form-control" onkeyup="FormatIt(this)" runat="server" TabIndex="10" PlaceHolder="Instrument Date" onkeypress="javascript:return isNumber (event)" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin: 7px 0 7px 0; margin-top: 20px;">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Naration :</label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="TextBox4" CssClass="form-control" runat="server" TabIndex="11" PlaceHolder="Narration" Enabled="false"></asp:TextBox>
                                            </div>
                                            <label class="control-label col-md-2">Amount :<span class="required">* </span></label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="TextBox5" CssClass="form-control" runat="server" TabIndex="12" PlaceHolder="Amount" onkeypress="javascript:return isNumber (event)" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin: 7px 0 7px 0; margin-top: 20px;">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">PAN Card: </label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtPanM" placeholder="PAN Card" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin: 7px 0 7px 0; margin-top: 20px;">
                                        <div class="col-lg-12">
                                            <div class="col-md-2">
                                                <asp:Label ID="Lbl_PassAmountMUL" runat="server" Text="PassAmount :"></asp:Label>
                                            </div>

                                            <div class="col-md-2">
                                                <asp:TextBox ID="TxtPassMUL" onkeypress="javascript:return isNumber (event)" placeholder="Amount" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-actions">
                                        <div class="row">
                                            <div class="col-md-offset-3 col-md-9">
                                                <div class="col-md-offset-3 col-md-9">
                                                    <asp:Button ID="Btn_MTAutho" runat="server" OnClick="BtnModal_AuthorizeCP_Click" Text="Authorize" CssClass="btn btn-success" />
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
                <%--<div class="table-scrollable" style="width: 100%; height: auto; max-height: 500px; overflow-x: auto; overflow-y: auto">
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
                                                    <asp:Label ID="lblAccNo1" runat="server" Text='<%# Eval("ACCNO") %>'></asp:Label>
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
                </div>--%>
            </div>
        </div>
    </div>

    <div id="TDRENEWAL" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg" style="width: 95%">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Authorization Screen</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="portlet box green">
                                <div class="portlet-title">
                                    <div class="caption">
                                        term Deposit Renewal
                                <asp:Literal ID="Literal3" runat="server"></asp:Literal>
                                    </div>
                                    <div class="tools">
                                        <a href="javascript:;" class="collapse" data-original-title="" title=""></a>
                                        <a href="#portlet-config" data-toggle="modal" class="config" data-original-title="" title=""></a>
                                    </div>
                                </div>
                                <div class="portlet-body">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Debit Details : </strong></div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin: 7px 0 7px 0; margin-top: 20px;">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-1" style="width: 165px">Product Code : <span class="required">* </span></label>
                                            <div class="col-md-1">
                                                <asp:TextBox ID="TxtRPrdDb" CssClass="form-control" runat="server" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="TxtRPrdDbName" runat="server" CssClass="form-control" AutoPostBack="true" Enabled="false"></asp:TextBox>

                                            </div>
                                            <label class="control-label col-md-1" style="width: 160px">Account No : <span class="required">* </span></label>
                                            <div class="col-md-1">
                                                <asp:TextBox ID="TxtRAccDb" CssClass="form-control" PlaceHolder="Account No /  Receipt No" runat="server" Enabled="false"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="TxtRAccDbName" CssClass="form-control" Width="270px" Placeholder="Acc Name" runat="server" Enabled="false"></asp:TextBox>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin: 7px 0 7px 0; margin-top: 20px;">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-1" style="width: 165px">IntPayable<span class="required">* </span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="TxtRIntPayable" CssClass="form-control" runat="server" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                            </div>

                                            <label class="control-label col-md-1" style="width: 160px">IntApplied<span class="required">* </span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="TxtRIntApplied" CssClass="form-control" PlaceHolder="Account No /  Receipt No" runat="server" Enabled="false"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Credit Details : </strong></div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin: 7px 0 7px 0; margin-top: 20px;">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-1" style="width: 165px">Product Code : <span class="required">* </span></label>
                                            <div class="col-md-1">
                                                <asp:TextBox ID="TxtRPrdCr" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="TxtRPrdCrName" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>

                                            </div>
                                            <label class="control-label col-md-1" style="width: 160px">Account No : <span class="required">* </span></label>
                                            <div class="col-md-1">
                                                <asp:TextBox ID="TxtRAccCr" CssClass="form-control" PlaceHolder="Account No /  Receipt No" runat="server" Enabled="false"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="TxtRAccCrName" CssClass="form-control" Width="270px" Placeholder="Acc Name" runat="server" Enabled="false"></asp:TextBox>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Deposit Information : </strong></div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin: 7px 0 7px 0;">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Select Type : <span class="required">* </span></label>
                                            <div class="col-md-4">
                                                <asp:RadioButton ID="rdbPWINT" runat="server" Text="Principle With INT" GroupName="mat" AutoPostBack="true" />
                                                <asp:RadioButton ID="rdbOP" runat="server" Text="Only Principle" GroupName="mat" AutoPostBack="true" />
                                                <asp:RadioButton ID="rdbWR" runat="server" Text="With Receipt" GroupName="mat" AutoPostBack="true" />
                                            </div>
                                            <label class="control-label col-md-2">Payble Amount : <span class="required">* </span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="TxtRACPayAmount" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin: 7px 0 7px 0;" runat="server" id="DIVWR">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Product Code : <span class="required">* </span></label>
                                            <div class="col-md-1">
                                                <asp:TextBox ID="TxtRIntTrfPRCD" runat="server" CssClass="form-control" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="TxtRIntTrfPrdname" runat="server" CssClass="form-control" AutoPostBack="true" Enabled="false"></asp:TextBox>

                                            </div>
                                            <label class="control-label col-md-2">Account No :</label>
                                            <div class="col-md-1">
                                                <asp:TextBox ID="TxtRIntTrfAccno" CssClass="form-control" onkeypress="javascript:return isNumber (event)" Enabled="false" runat="server" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="TxtRIntTrfAccname" runat="server" CssClass="form-control" AutoPostBack="true" Enabled="false"></asp:TextBox>

                                            </div>
                                            <%-- <label class="control-label col-md-1">Balance :</label>
                                        <div class="col-md-1">
                                            <asp:TextBox ID="Txt" CssClass="form-control" onkeypress="javascript:return isNumber (event)" PlaceHolder="Balace" runat="server" Enabled="false"></asp:TextBox>
                                        </div>--%>
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
                                                <asp:RadioButton ID="RdbSingle" runat="server" Text="Single" GroupName="SM" AutoPostBack="true" />
                                                <asp:RadioButton ID="rdbMultiple" runat="server" Text="Multiple" GroupName="SM" AutoPostBack="true" />
                                            </div>

                                        </div>
                                    </div>

                                    <%--Div 1--%>
                                    <div id="Div17" runat="server">
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Deposit Date: <span class="required"></span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtRDeposDate" CssClass="form-control" PlaceHolder="dd/mm/yyyy" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-2">Interest Payout: <span class="required"></span></label>
                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="DdlRIntPayout" Enabled="false" runat="server" CssClass="form-control" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Deposit Amount : <span class="required"></span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtRDepoAmt" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" Enabled="false"></asp:TextBox>
                                                </div>

                                                <label class="control-label col-md-1">Period : <span class="required"></span></label>
                                                <div class="col-md-2" style="margin-right: -24px;">
                                                    <asp:DropDownList ID="ddlduration" runat="server" CssClass="form-control" Enabled="false">
                                                        <asp:ListItem Value="M">Months</asp:ListItem>
                                                        <asp:ListItem Value="D">Days</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="TxtRPeriod" CssClass="form-control" PlaceHolder="Period" runat="server" Style="width: 77px;" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Rate : <span class="required"></span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtRRate" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-2">Interest Amount : <span class="required"></span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtRIntrest" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Maturity Amount :<span class="required"></span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtRMaturity" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                                <%--Dhanya Shetty//04/05/2017--%>
                                                <div class="col-md-2">
                                                    <asp:Label ID="Lbl_TDFPass" runat="server" Text="Pass Amount:"></asp:Label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtFTDpass" onkeypress="javascript:return isNumber (event)" placeholder="Amount" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-2">Due Date :<span class="required"></span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtRDueDate" CssClass="form-control" PlaceHolder="dd/mm/yyyy" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Receipt No</label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtRReceiptNo" CssClass="form-control" PlaceHolder="Receipt No" runat="server" Enabled="false"></asp:TextBox>
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
                                                    <asp:TextBox ID="TxtRProcode4" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtRProName4" runat="server" CssClass="form-control" AutoPostBack="true" Enabled="false"></asp:TextBox>

                                                </div>
                                                <label class="control-label col-md-1" style="width: 160px">Account No : <span class="required">* </span></label>
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="TxtRAccNo4" CssClass="form-control" PlaceHolder="Account No /  Receipt No" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtRAccName4" CssClass="form-control" Width="270px" Placeholder="Acc Name" runat="server" Enabled="false"></asp:TextBox>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-actions">
                                            <div class="row">
                                                <div class="col-md-offset-3 col-md-9">
                                                    <div class="col-md-offset-3 col-md-9">
                                                        <asp:Button ID="BtnRAuthorise" runat="server" OnClick="BtnModal_AuthorizeCP_Click" Text="Authorize" CssClass="btn btn-success" />
                                                        <asp:Button ID="BtnRClose" Text="Close" runat="server" CssClass="btn btn-success" data-dismiss="modal" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%-- <div class="row">
                    <div class="col-lg-12">
                        <div class="table-scrollable">
                            <table class="table table-striped table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th>
                                            <asp:GridView ID="GrdFDLedger" runat="server" AllowPaging="True"
                                                AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                EditRowStyle-BackColor="#FFFF99"
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
                </div>--%>
                </div>
            </div>
        </div>
    </div>

     <div class="row" id="Div_LotpassingUntally" runat="server" visible="false">
        <div class="col-md-12">
            <div class="table-scrollable" style="width: 100%; height: 500px; overflow-x: auto; overflow-y: auto">
                <asp:Label ID="Lbl_Lotpassinguntally" runat="server" Text="List Of Untally Sets" BackColor="#66ccff" Font-Bold="true" Font-Size="Medium"></asp:Label>
                <table class="table table-striped table-bordered table-hover">
                    <thead>
                        <tr>
                            <th>
                                <asp:GridView ID="GrdLotUntally" runat="server" CellPadding="6" CellSpacing="7"
                                    ForeColor="#333333" 
                                    PageIndex="5" AutoGenerateColumns="False" CssClass="mGrid" BorderWidth="1px"
                                    BorderColor="#333300" Width="100%" 
                                   ShowFooter="true">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                     
                                        <asp:TemplateField HeaderText="BrCode" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="BrCodeL" runat="server" Text='<%# Eval("BrCode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="EntryDate" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="EntryDateL" runat="server" Text='<%# Eval("EntryDate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SetNo" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="SetNoL" runat="server" Text='<%# Eval("SetNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Debit" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="DebitL" runat="server" Text='<%# Eval("Debit") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                           <asp:TemplateField HeaderText="Credit" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="CreditL" runat="server" Text='<%# Eval("Credit") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                   </Columns>
                                    <FooterStyle BackColor="#ccffcc" Font-Bold="True" ForeColor="Black" HorizontalAlign="Right" BorderStyle="None" />
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
                    <button class="btn btn-default">Close</button>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdnscroll" runat="server" />
    <asp:HiddenField ID="hdnRow" runat="server" Value="0" />
    <asp:HiddenField ID="hdnset" runat="server" />
    <asp:HiddenField ID="hdnamount" runat="server" />
</asp:Content>

