<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CBSMaster.master" CodeFile="FrmAVS5060.aspx.cs" Inherits="FrmAVS5060" %>

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
    </script>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        RD Excess Amount
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab1">


                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Deposit Information : </strong></div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0;">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Product Code : <span class="required">* </span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtProcode" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="TxtProcode_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="TxtProName" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="TxtProName_TextChanged"></asp:TextBox>
                                                    <div id="CustList" style="height: 200px; overflow-y: scroll;"></div>
                                                    <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="TxtProName"
                                                        UseContextKey="true"
                                                        CompletionInterval="1"
                                                        CompletionSetCount="20"
                                                        MinimumPrefixLength="1"
                                                        EnableCaching="true"
                                                        ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList"
                                                        ServiceMethod="getglname">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                                <label class="control-label col-md-2">PAN Card: </label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtPan" placeholder="PAN Card" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0;">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Account No : <span class="required">* </span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtAccNo" CssClass="form-control" PlaceHolder="Account No" runat="server" AutoPostBack="true" OnTextChanged="txtAccNo_TextChanged"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="TxtAccname" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="TxtAccname_TextChanged"></asp:TextBox>
                                                    <div id="CustList2" style="height: 200px; overflow-y: scroll;"></div>
                                                    <asp:AutoCompleteExtender ID="AutoAccname" runat="server" TargetControlID="TxtAccName"
                                                        UseContextKey="true"
                                                        CompletionInterval="1"
                                                        CompletionSetCount="20"
                                                        MinimumPrefixLength="1"
                                                        EnableCaching="true"
                                                        ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList2"
                                                        ServiceMethod="GetAccName">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                                <label class="control-label col-md-2">Customer : <span class="required"></span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="Txtcustno" CssClass="form-control" PlaceHolder="Cust No" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <%--Div 1--%>
                                        <div id="Depositdiv" runat="server">
                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Deposit Date<span class="required"></span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="dtDeposDate" onkeyup="FormatIt(this)" CssClass="form-control" PlaceHolder="DD/MM/YYYY" runat="server" Enabled="False"></asp:TextBox>
                                                    </div>
                                                    <label class="control-label col-md-1">Int Payout: <span class="required"></span></label>
                                                    <div class="col-md-2">
                                                        <asp:DropDownList ID="ddlIntrestPay" runat="server" CssClass="form-control" Enabled="False">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-1"></div>
                                                    <label class="control-label col-md-2">Interest Amount : <span class="required"></span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtIntrest" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Deposit Amount : <span class="required"></span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtDepoAmt" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" AutoPostBack="true" OnTextChanged="TxtDepoAmt_TextChanged" Enabled="False"></asp:TextBox>
                                                    </div>
                                                    <label class="control-label col-md-1">Period : <span class="required"></span></label>
                                                    <div class="col-md-2">
                                                        <asp:DropDownList ID="ddlduration" runat="server" CssClass="form-control" Enabled="False">
                                                            <asp:ListItem Value="M">Months</asp:ListItem>
                                                            <asp:ListItem Value="D">Days</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="TxtPeriod" CssClass="form-control" PlaceHolder="Period" runat="server" Style="width: 77px;" Enabled="False"></asp:TextBox>
                                                    </div>
                                                    <label class="control-label col-md-2">Rate : <span class="required"></span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtRate" CssClass="form-control" runat="server" Enabled="false" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Maturity Amount :<span class="required"></span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtMaturity" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                    </div>
                                                    <label class="control-label col-md-1">Due Date :<span class="required"></span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="DtDueDate" CssClass="form-control" PlaceHolder="dd/mm/yyyy" runat="server" Enabled="false"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-1"></div>
                                                    <label class="control-label col-md-2">Close Status : <span class="required"></span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtOpenClose" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>


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
                                                    <label class="control-label col-md-2">RD Excess Amount :<span class="required"></span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtRDExcess" CssClass="form-control" runat="server" Enabled="true"></asp:TextBox>
                                                    </div>
                                                    <label class="control-label col-md-1">Remarks :<span class="required"></span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtRemark" CssClass="form-control" PlaceHolder="Remarks" runat="server" Enabled="true"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-1"></div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9">
                                        <asp:Button ID="BtnSubmit" runat="server" CssClass="btn green" Text="Submit" OnClick="BtnSubmit_Click" OnClientClick="javascript:return validate();" />
                                        <asp:Button ID="BtnClear" runat="server" CssClass="btn green" Text="Clear" OnClick="BtnClear_Click" OnClientClick="javascript:return validate();" />
                                        <asp:Button ID="BtnExit" runat="server" CssClass="btn green" Text="Exit" OnClick="BtnExit_Click" OnClientClick="javascript:return validate();" />
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
                    <button type="button" class="close" data-dismiss="modal">&times;</button><center><h4 class="modal-title" style="color:#ff0000">AVS CORE</h4></center>
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
    <div id="VOUCHERVIEW" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">

            <!-- Modal content-->

        </div>
    </div>

    <%--Added by ankita on 26/06/2017--%>
</asp:Content>
