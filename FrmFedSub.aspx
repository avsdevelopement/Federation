<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmFedSub.aspx.cs" Inherits="FrmFedSub" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>

    <style>
        .gvwCasesPager a {
            margin-left: 5px;
            margin-right: 5px;
        }
    </style>
    <script type="text/javascript">
         
        <%-- Only Numbers --%>
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;
            return true;
        }
    </script>
    <script>

        function FormatIt(obj) {
            if (obj.value.length == 2) //DAY
                obj.value = obj.value + "/";
            if (obj.value.length == 5) //MONTH
                obj.value = obj.value + "/";
            if (obj.value.length == 11) //YEAR
                alert("Enter Valid Date!....");
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="TXTCNO" />
        </Triggers>
        <ContentTemplate>

            <div class="container-fluid">
                <br />
                <div class="panel panel-primary">
                    <div class="panel-heading" style="background-color: #4d88ff;">Federation Subscription</div>
                    <div class="panel-body">
                        <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-1">
                                <asp:Label ID="Label5" runat="server" Text="Member Type"></asp:Label>
                            </div>
                            <div class="col-lg-2">
                                <asp:DropDownList ID="ddlMemberType" CssClass="form-control" ClientIDMode="Static" AutoPostBack="true"
                                    runat="server" OnSelectedIndexChanged="ddlPaymentMode_SelectedIndexChanged">
                                    <asp:ListItem Value="4" Selected="True">Housing</asp:ListItem>
                                    <asp:ListItem Value="44">Promises-Nominal</asp:ListItem>
                                    <asp:ListItem Value="45">Housing Time</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-1">
                                <asp:Label ID="LBLCNO" runat="server" Text="Mem No"></asp:Label>
                            </div>
                            <div class="col-lg-1">
                                <asp:TextBox ID="TXTCNO" placeholder="Mem No." onkeypress="javascript:return isNumber (event)" OnTextChanged="TXTCNO_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server" />
                            </div>
                            <div class="col-lg-4">
                                <%-- <asp:TextBox ID="TXTCNAME" runat="server" Height="30px" Width="400px" placeholder="Customer Name"></asp:TextBox>--%>
                                <asp:TextBox ID="TXTCNAME" Enabled="false" placeholder="Customer Name" CssClass="form-control" runat="server" />
                            </div>

                            <div class="col-lg-1">
                                <asp:Label ID="LBLBAL" runat="server" Text="Balance"></asp:Label>
                            </div>
                            <div class="col-lg-2">
                                <%-- <asp:TextBox ID="TXTBAL3" runat="server" Height="30px" Width="150px"></asp:TextBox>--%>
                                <asp:TextBox ID="TXTBAL3" ReadOnly="true" placeholder="Balance" CssClass="form-control" runat="server" />
                            </div>
                        </div>


                        <div class="row" style="margin: 7px 0 7px 0">

                            <div class="col-lg-1">
                                <asp:Label ID="LBLPAYMOD" runat="server" Text="Pay Mode"></asp:Label>
                            </div>

                            <div class="col-lg-1">
                                <asp:DropDownList ID="ddlPaymentMode" Style="width: 100px" CssClass="form-control" ClientIDMode="Static" AutoPostBack="true"
                                    runat="server" OnSelectedIndexChanged="ddlPaymentMode_SelectedIndexChanged">
                                    <asp:ListItem Value="3" Selected="True">Cash</asp:ListItem>
                                    <asp:ListItem Value="5">Cheque</asp:ListItem>
                                    <asp:ListItem Value="7">Online</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-2" style="text-align: right">
                                <asp:Label ID="Label1" runat="server" Text="Bank Prod Code"></asp:Label>
                            </div>
                            <div class="col-lg-1">
                                <asp:TextBox ID="txtDSubGlCode" AutoPostBack="true" onkeypress="javascript:return isNumber (event)" OnTextChanged="txtDSubGlCode_TextChanged" placeholder="Bank Product Code" CssClass="form-control" runat="server" />
                            </div>
                            <%-- <div class="col-lg-1">
                                <asp:Label ID="Label2" runat="server" Text="Debit Prod Name"></asp:Label>
                            </div>--%>
                            <div class="col-lg-3">
                                <asp:TextBox ID="txtDProdName" placeholder="Debit Product Name" AutoPostBack="true" OnTextChanged="txtDProdName_TextChanged" CssClass="form-control" runat="server" />
                                <asp:AutoCompleteExtender ID="autoBankname" runat="server" TargetControlID="txtDProdName"
                                    UseContextKey="true"
                                    CompletionInterval="1"
                                    CompletionSetCount="20"
                                    MinimumPrefixLength="1"
                                    EnableCaching="true"
                                    ServicePath="~/WebServices/Contact.asmx"
                                    ServiceMethod="GetGlNamef7">
                                </asp:AutoCompleteExtender>
                            </div>
                            <div class="col-lg-4">
                                <%--<asp:TextBox ID="TXTNARR" runat="server" Height="30PX" Width="300px" placeholder="Narration"></asp:TextBox>--%>
                                <asp:TextBox ID="TXTNARR" ClientIDMode="Static" placeholder="Narration" CssClass="form-control" runat="server" />
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-lg-1">
                                <label>From </label>
                            </div>

                            <div class="col-lg-2">
                                <asp:TextBox ID="txtFromPeriod" onkeyup="FormatIt(this);" onkeypress="javascript:return isNumber(event)" placeholder="dd/mm/yyyy" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtFromPeriod">
                                </asp:TextBoxWatermarkExtender>
                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtFromPeriod">
                                </asp:CalendarExtender>
                            </div>
                            <div class="col-lg-1">
                                <label>To </label>
                            </div>

                            <div class="col-lg-2">
                                <asp:TextBox ID="txtToPeriod" onkeyup="FormatIt(this);" onkeypress="javascript:return isNumber(event)" placeholder="dd/mm/yyyy" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtToPeriod">
                                </asp:TextBoxWatermarkExtender>
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtToPeriod">
                                </asp:CalendarExtender>
                            </div>
                            <div id="instrumentDetails1" runat="server">
                                <div class="col-lg-1">
                                    <asp:Label ID="LBLCHNO" runat="server" Text="Cheque No"></asp:Label>
                                </div>

                                <div class="col-lg-2">
                                    <asp:TextBox ID="Txtchno" onkeypress="javascript:return isNumber (event)" ClientIDMode="Static" placeholder="Cheque Number" CssClass="form-control" runat="server" />
                                </div>
                                <div class="col-lg-1">
                                    <asp:Label ID="Label3" runat="server" Text="Cheque Date"></asp:Label>
                                </div>
                                <div class="col-lg-2">
                                    <asp:TextBox ID="txtChequeDate" onkeyup="FormatIt(this)" ClientIDMode="Static" placeholder="DD/MM/YYYY" CssClass="form-control" runat="server" />
                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtChequeDate">
                                    </asp:TextBoxWatermarkExtender>
                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtChequeDate">
                                    </asp:CalendarExtender>
                                </div>
                            </div>
                        </div>

                        <div id="instrumentDetails2" runat="server">
                            <div class="row" style="margin: 7px 0 7px 0">
                                <div class="col-lg-1">
                                    <asp:LinkButton ID="lnkBank" OnClick="lnkBank_Click" runat="server" Text="Bank"></asp:LinkButton>
                                </div>
                                <div class="col-lg-1">
                                    <%--<asp:TextBox ID="txtBankCode" ClientIDMode="Static" runat="server" OnTextChanged="txtBankCode_TextChanged" AutoPostBack="true"
                                        Height="30PX" Width="120px"
                                        placeholder="Bank Code"></asp:TextBox>--%>
                                    <asp:TextBox ID="txtBankCode" onkeypress="javascript:return isNumber (event)" ClientIDMode="Static" AutoPostBack="true" placeholder="Bank Code" OnTextChanged="txtBankCode_TextChanged" CssClass="form-control" runat="server" />
                                </div>
                                <%-- <div class="col-lg-1">
                                    <asp:Label ID="Label6" runat="server" Text="Bank Name"></asp:Label>
                                </div>--%>

                                <div class="col-lg-4">
                                    <%--  <asp:TextBox ID="txtBankName" ClientIDMode="Static" runat="server"
                                        Height="30PX" Width="250px" Enabled="false"
                                        placeholder="Bank Name"></asp:TextBox>--%>
                                    <asp:TextBox ID="txtBankName" ClientIDMode="Static" placeholder="Bank Name" CssClass="form-control" runat="server" />
                                </div>
                                <div class="col-lg-1">
                                    <asp:Label ID="Label2" runat="server" Text="Branch"></asp:Label>
                                </div>
                                <div class="col-lg-1">
                                    <%--<asp:TextBox ID="txtBranchCode" ClientIDMode="Static" runat="server" OnTextChanged="txtBranchCode_TextChanged" AutoPostBack="true"
                                        Height="30PX" Width="120px"
                                        placeholder="Branch Code"></asp:TextBox>--%>
                                    <asp:TextBox ID="txtBranchCode" onkeypress="javascript:return isNumber (event)" ClientIDMode="Static" placeholder="Branch Code" AutoPostBack="true" OnTextChanged="txtBranchCode_TextChanged" CssClass="form-control" runat="server" />
                                </div>
                                <%--  <div class="col-lg-1">
                                <asp:Label ID="Label7" runat="server" Text="Branch Name"></asp:Label>
                            </div>--%>
                                <div class="col-lg-4">
                                    <%--      <asp:TextBox ID="txtBranchName" ClientIDMode="Static" runat="server"
                                        Height="30PX" Width="250px" MaxLength="6" Enabled="false"
                                        placeholder="Branch Name"></asp:TextBox>--%>
                                    <asp:TextBox ID="txtBranchName" ClientIDMode="Static" placeholder="Branch Name" AutoPostBack="true" CssClass="form-control" runat="server" />
                                </div>

                            </div>
                        </div>




                        <div class="row" style="margin: 7px 0 7px 0">

                            <div class="col-lg-1">
                                <asp:Label ID="LBLGST" runat="server" Text="GST"></asp:Label>
                            </div>

                            <div class="col-lg-1">
                                <asp:Label ID="LBLPRCO" runat="server" Text="Prod Code"></asp:Label>
                            </div>

                            <div class="col-lg-2">
                                <asp:Label ID="LBLPRNAME" runat="server" Text="Product Name"></asp:Label>
                            </div>

                            <div class="col-lg-1">
                                <asp:Label ID="LBLCGST" runat="server" Text="SGST"></asp:Label>
                            </div>

                            <div class="col-lg-1">
                                <asp:Label ID="LBLSGST" runat="server" Text="CGST"></asp:Label>
                            </div>

                            <div class="col-lg-1">
                                <asp:Label ID="LBLRATE" runat="server" Text="Rate"></asp:Label>
                            </div>

                            <div class="col-lg-1">
                                <asp:Label ID="LBLAMT" runat="server" Text="Amount"></asp:Label>
                            </div>

                            <div class="col-lg-1">
                                <asp:Label ID="LBLSGSTAMT" runat="server" Text="SGST Amt"></asp:Label>
                            </div>

                            <div class="col-lg-1">
                                <asp:Label ID="LBLCGSTAMT" runat="server" Text="CGST Amt"></asp:Label>
                            </div>

                            <div class="col-lg-1">
                                <asp:Label ID="LBLTGST" runat="server" Text="Total GST"></asp:Label>
                            </div>

                            <div class="col-lg-1">
                                <asp:Label ID="LBLTOTAL" runat="server" Text="Total"></asp:Label>
                            </div>

                        </div>

                        <div class="row" style="margin: 7px 0 7px 0">

                            <div class="col-lg-1">
                                <asp:DropDownList ID="DDL1" CssClass="form-control" runat="server" AutoPostBack="true"
                                    OnSelectedIndexChanged="DDL1_SelectedIndexChanged">
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="col-lg-1">
                                <asp:TextBox ID="TXTPRC" onkeypress="javascript:return isNumber (event)" CssClass="form-control" OnTextChanged="TXTPRC_TextChanged" AutoPostBack="true"
                                    runat="server"></asp:TextBox>
                            </div>


                            <div class="col-lg-2">
                                <asp:TextBox ID="TXTPRNAME" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="col-lg-1">
                                <asp:TextBox ID="TXTSGST" onkeypress="javascript:return isNumber (event)" runat="server" AutoPostBack="true" OnTextChanged="TotalAmount" CssClass="form-control" Style="text-align: right;">0</asp:TextBox>
                            </div>
                            <div class="col-lg-1">
                                <asp:TextBox ID="TXTCGST" onkeypress="javascript:return isNumber (event)" runat="server" AutoPostBack="true" OnTextChanged="TotalAmount" CssClass="form-control" Style="text-align: right;">0</asp:TextBox>
                            </div>



                            <div class="col-lg-1">
                                <asp:TextBox ID="TXTRATE" onkeypress="javascript:return isNumber (event)" Enabled="false" runat="server" CssClass="form-control" Style="text-align: right;">0</asp:TextBox>
                            </div>

                            <div class="col-lg-1">
                                <asp:TextBox ID="TXTAMT" onkeypress="javascript:return isNumber (event)" runat="server" OnTextChanged="TXTAMT_TextChanged" AutoPostBack="true"
                                    CssClass="form-control">0</asp:TextBox>
                            </div>

                            <div class="col-lg-1">
                                <asp:TextBox ID="TXTSGSTAMT" onkeypress="javascript:return isNumber (event)" runat="server" AutoPostBack="true" OnTextChanged="TotalAmount" CssClass="form-control" Style="text-align: right;">0</asp:TextBox>
                            </div>

                            <div class="col-lg-1">
                                <asp:TextBox ID="TXTCGSTAMT" onkeypress="javascript:return isNumber (event)" runat="server" AutoPostBack="true" OnTextChanged="TotalAmount" CssClass="form-control" Style="text-align: right;">0</asp:TextBox>
                            </div>

                            <div class="col-lg-1">
                                <asp:TextBox ID="TXTTGST" onkeypress="javascript:return isNumber (event)" runat="server"  Enabled="false" CssClass="form-control" Style="text-align: right;">0</asp:TextBox>
                            </div>

                            <div class="col-lg-1">
                                <asp:TextBox ID="TXTTTL" onkeypress="javascript:return isNumber (event)" runat="server" Enabled="false" CssClass="form-control" Style="text-align: right;">0</asp:TextBox>
                            </div>
                            <div class="col-lg-1">
                            </div>

                        </div>


                        <div class="row" style="margin: 7px 0 7px 0">

                            <div class="col-lg-1">
                                <asp:DropDownList ID="DDL2" AutoPostBack="true" OnSelectedIndexChanged="DDL2_SelectedIndexChanged" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                    <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="col-lg-1">
                                <asp:TextBox ID="TXTPRCODE2" onkeypress="javascript:return isNumber (event)" OnTextChanged="TXTPRCODE2_TextChanged"
                                    AutoPostBack="true" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-lg-2">
                                <asp:TextBox ID="TXTPRNAME2" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="col-lg-1">
                                <asp:TextBox ID="TXTSGST2" onkeypress="javascript:return isNumber (event)" runat="server" AutoPostBack="true" OnTextChanged="TotalAmount" CssClass="form-control" Style="text-align: right;">0</asp:TextBox>
                            </div>

                            <div class="col-lg-1">
                                <asp:TextBox ID="TXTCGST2" onkeypress="javascript:return isNumber (event)" runat="server" AutoPostBack="true" OnTextChanged="TotalAmount" CssClass="form-control" Style="text-align: right;">0</asp:TextBox>
                            </div>
                            <div class="col-lg-1">
                                <asp:TextBox ID="TXTRATE2" onkeypress="javascript:return isNumber (event)" runat="server" Enabled="false" CssClass="form-control" Style="text-align: right;">0</asp:TextBox>
                            </div>

                            <div class="col-lg-1">
                                <asp:TextBox ID="TXTAMT2" onkeypress="javascript:return isNumber (event)" OnTextChanged="TXTAMT2_TextChanged" AutoPostBack="true" runat="server" CssClass="form-control">0</asp:TextBox>
                            </div>

                            <div class="col-lg-1">
                                <asp:TextBox ID="TXTTTSGST2" onkeypress="javascript:return isNumber (event)" AutoPostBack="true" runat="server" OnTextChanged="TotalAmount" CssClass="form-control" Style="text-align: right;">0</asp:TextBox>
                            </div>
                            <div class="col-lg-1">
                                <asp:TextBox ID="TXTTTCGST2" onkeypress="javascript:return isNumber (event)" AutoPostBack="true" runat="server" OnTextChanged="TotalAmount" CssClass="form-control" Style="text-align: right;">0</asp:TextBox>
                            </div>
                            <div class="col-lg-1">
                                <asp:TextBox ID="TTLGST2" onkeypress="javascript:return isNumber (event)" runat="server" Enabled="false" CssClass="form-control" Style="text-align: right;">0</asp:TextBox>
                            </div>
                            <div class="col-lg-1">
                                <asp:TextBox ID="TTLAMT2" onkeypress="javascript:return isNumber (event)" runat="server" Enabled="false" CssClass="form-control" Style="text-align: right;">0</asp:TextBox>
                            </div>
                        </div>

                        <div class="row" style="margin: 7px 0 7px 0">

                            <div class="col-lg-9"></div>
                            <div class="col-lg-1">
                                <%-- <asp:Label ID="LBLAMT2" runat="server" Text="Amount"></asp:Label>--%>
                                <label class="control-label col-lg-1">Amount </label>
                            </div>

                            <div class="col-lg-2">
                                <asp:TextBox ID="TXTAMT3" onkeypress="javascript:return isNumber (event)" runat="server" Enabled="false" CssClass="form-control" Style="text-align: right;">0</asp:TextBox>
                            </div>
                        </div>

                        <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-9">
                            </div>

                            <div class="col-lg-1">
                                <%--  <asp:Label ID="LBLSGST2" runat="server" Text="SGST"></asp:Label>--%>
                                <label class="control-label col-lg-1">SGST</label>
                            </div>

                            <div class="col-lg-2">
                                <asp:TextBox ID="TXTSGST3" onkeypress="javascript:return isNumber (event)" runat="server" Enabled="false" CssClass="form-control" Style="text-align: right;">0</asp:TextBox>
                            </div>
                        </div>

                        <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-9">
                            </div>

                            <div class="col-lg-1">
                                <%-- <asp:Label ID="LBLCGST2" runat="server" Text="CGST"></asp:Label>--%>
                                <label class="control-label col-lg-1">CGST</label>
                            </div>

                            <div class="col-lg-2">
                                <asp:TextBox ID="TXTCGST3" onkeypress="javascript:return isNumber (event)" runat="server" Enabled="false" CssClass="form-control" Style="text-align: right;">0</asp:TextBox>
                            </div>
                        </div>

                        <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-9">
                            </div>

                            <div class="col-lg-1">
                                <%--<asp:Label ID="LBLGRTTL" runat="server" Text="Grand Total"></asp:Label>--%>
                                <label class="control-label col-lg-1">Grand Total</label>
                            </div>

                            <div class="col-lg-2">
                                <asp:TextBox ID="TXTGRTTL3" onkeypress="javascript:return isNumber (event)" runat="server" Enabled="false" Style="text-align: right;" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-5">
                            </div>

                            <asp:Button ID="BTNSUBMIT" runat="server" OnClick="BTNSUBMIT_Click" class="btn-primary" Text="Submit" Width="10%" Height="40px"
                                Style="font-size: 18px;" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                  <asp:Button ID="BTNCANCEL" runat="server" class="btn-primary" Text="Cancel" Width="10%" Height="40px" Style="font-size: 18px;" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                  <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" class="btn-primary" Text="Statement" Width="10%" Height="40px" Style="font-size: 18px;" />


                        </div>

                        <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-md-12">
                                <asp:GridView runat="server" ID="grdCustomerStatement"
                                    AutoGenerateColumns="false" AllowPaging="true" OnPageIndexChanging="grdCustomerStatement_PageIndexChanging"
                                    Width="100%" OnRowCommand="grdCustomerStatement_RowCommand">
                                    <PagerStyle CssClass="gvwCasesPager" />
                                    <Columns>
                                        <asp:BoundField DataField="SRNO" HeaderText="SRNO" />
                                        <asp:BoundField DataField="ENTRYDATE" HeaderText="ENTRYDATE" />
                                        <asp:BoundField DataField="BRCD" HeaderText="BRCD" />
                                        <asp:BoundField DataField="SUBGLCODE" HeaderText="SUBGLCODE" />
                                        <asp:BoundField DataField="ACCNO" HeaderText="ACCNO" />
                                        <asp:BoundField DataField="PARTICULARS" HeaderText="PARTICULARS" />
                                        <asp:BoundField DataField="DEBIT" HeaderText="DEBIT" />
                                        <asp:BoundField DataField="CREDIT" HeaderText="CREDIT" />
                                        <asp:BoundField DataField="BAL" HeaderText="BAL" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkRemove" Text="Receipt" runat="server" CommandName="Select" CommandArgument='<%# Eval("ID") %>' />
                                            </ItemTemplate>

                                        </asp:TemplateField>


                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </ContentTemplate>

    </asp:UpdatePanel>

</asp:Content>

