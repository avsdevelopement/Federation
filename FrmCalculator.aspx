<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmCalculator.aspx.cs" Inherits="FrmCalculator" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="row">
        <div class="col-md-12">
            <div class="portlet box green">
                <div class="portlet-title">
                    <div class="caption">
                        Deposit Calculator
                                <asp:Literal ID="LtrlHeading" runat="server"></asp:Literal>
                    </div>
                    <div class="tools">
                        <a href="javascript:;" class="collapse" data-original-title="" title=""></a>
                        <a href="#portlet-config" data-toggle="modal" class="config" data-original-title="" title=""></a>
                    </div>
                </div>

                <div class="portlet-body">
                    <div class="row" style="margin: 7px 0 7px 0;">
                        <div class="col-lg-12">
                            <label class="control-label col-md-2">Product Code : <span class="required">* </span></label>
                            <div class="col-md-2">
                                <asp:DropDownList ID="ddlProdCode" runat="server" CssClass="form-control" OnTextChanged="ddlProdCode_TextChanged" AutoPostBack="true" >
                                    </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-12">
                                <label class="control-label col-md-2">Account Type : <span class="required">* </span></label>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlAccType" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-12">
                                <label class="control-label col-md-2">Deposit Date: <span class="required"></span></label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="dtDeposDate" onkeyup="FormatIt(this)" CssClass="form-control" PlaceHolder="dd/mm/yyyy" runat="server" Enabled="False"></asp:TextBox>
                                </div>
                                <label class="control-label col-md-2">Interest Payout: <span class="required">* </span></label>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlIntrestPay" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-12">
                                <label class="control-label col-md-2">Deposit Amount : <span class="required"></span></label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="TxtDepoAmt" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" AutoPostBack="true" OnTextChanged="TxtDepoAmt_TextChanged" ></asp:TextBox>
                                </div>
                                <label class="control-label col-md-2">Period : <span class="required"></span></label>
                                <div class="col-md-2" style="margin-right: -24px;">
                                    <asp:DropDownList ID="ddlduration" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="M">Months</asp:ListItem>
                                        <asp:ListItem Value="D">Days</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <asp:TextBox ID="TxtPeriod" CssClass="form-control" PlaceHolder="Period" runat="server" AutoPostBack="true" OnTextChanged="TxtPeriod_TextChanged" Style="width: 77px;"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-12">
                                <label class="control-label col-md-2">Rate : <span class="required"></span></label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="TxtRate" CssClass="form-control" runat="server" OnTextChanged="TxtRate_TextChanged" AutoPostBack="true" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                </div>
                                <label class="control-label col-md-2">Interest Amount : <span class="required"></span></label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="TxtIntrest" CssClass="form-control" runat="server" Enabled="false" ></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row" style="margin: 7px 0 7px 0">
                            <div class="col-lg-12">
                                <label class="control-label col-md-2">Maturity Amount :<span class="required" ></span></label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="TxtMaturity" CssClass="form-control" runat="server" Enabled="false" ></asp:TextBox>
                                </div>
                                <label class="control-label col-md-2">Due Date :<span class="required" ></span></label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="DtDueDate" CssClass="form-control" PlaceHolder="dd/mm/yyyy" Enabled="false" runat="server" ></asp:TextBox>
                                </div>
                            </div>
                        </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

