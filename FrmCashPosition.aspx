<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmCashPosition.aspx.cs" Inherits="FrmCashPosition" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
         <%-- Only Numbers --%>
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;
            return true;
        }
    </script>

    <script type="text/javascript">

        function NTS2000(obj) {
            var NTS2000 = document.getElementById('<%=Txttwothousand.ClientID%>').value || 0;
            var Result = parseInt(NTS2000) * 2000;
            if (!isNaN(Result)) {
                document.getElementById('<%=Txttwothoutotal.ClientID %>').value = Result;
                TakeTotal();
                return true;
            }
        }

        function NTS1000(obj) {
            var NTS1000 = document.getElementById('<%=Txtonethousand.ClientID%>').value || 0;
            var Result = parseInt(NTS1000) * 1000;
            if (!isNaN(Result)) {
                document.getElementById('<%=Txtonethoutotal.ClientID %>').value = Result;
                TakeTotal();
                return true;
            }
        }

        function NTS500(obj) {
            var NTS500 = document.getElementById('<%=Txtfivehundrd.ClientID%>').value || 0;
            var Result = parseInt(NTS500) * 500;
            if (!isNaN(Result)) {
                document.getElementById('<%=Txtfivehuntotal.ClientID %>').value = Result;
                TakeTotal();
                return true;
            }
        }

        function NTS200(obj) {
            var NTS200 = document.getElementById('<%=txtTwoHundred.ClientID%>').value || 0;
            var Result = parseFloat(NTS200) * 200;
            if (!isNaN(Result)) {
                document.getElementById('<%=txtTwoHunTotal.ClientID %>').value = Result;
                TakeTotal();
                return true;
            }
        }

        function NTS100(obj) {
            var NTS100 = document.getElementById('<%=Txthundred.ClientID%>').value || 0;
            var Result = parseInt(NTS100) * 100;
            if (!isNaN(Result)) {
                document.getElementById('<%=Txthundrdtotal.ClientID %>').value = Result;
             TakeTotal();
             return true;
         }
     }

     function NTS50(obj) {
         var NTS50 = document.getElementById('<%=Txtfifty.ClientID%>').value || 0;
            var Result = parseInt(NTS50) * 50;
            if (!isNaN(Result)) {
                document.getElementById('<%=Txtfiftytotal.ClientID %>').value = Result;
                TakeTotal();
                return true;
            }
        }

        function NTS20(obj) {
            var NTS20 = document.getElementById('<%=Txttwenty.ClientID%>').value || 0;
            var Result = parseInt(NTS20) * 20;
            if (!isNaN(Result)) {
                document.getElementById('<%=Txttwntytotal.ClientID %>').value = Result;
                TakeTotal();
                return true;
            }
        }

        function NTS10(obj) {
            var NTS10 = document.getElementById('<%=Txtten.ClientID%>').value || 0;
            var Result = parseInt(NTS10) * 10;
            if (!isNaN(Result)) {
                document.getElementById('<%=Txttentotal.ClientID %>').value = Result;
                TakeTotal();
                return true;
            }
        }

        function NTS5(obj) {
            var NTS5 = document.getElementById('<%=Txtfive.ClientID%>').value || 0;
            var Result = parseInt(NTS5) * 5;
            if (!isNaN(Result)) {
                document.getElementById('<%=Txtfivetotal.ClientID %>').value = Result;
                TakeTotal();
                return true;
            }
        }

        function NTS2(obj) {
            var NTS2 = document.getElementById('<%=Txttwo.ClientID%>').value || 0;
            var Result = parseInt(NTS2) * 2;
            if (!isNaN(Result)) {
                document.getElementById('<%=Txttwototal.ClientID %>').value = Result;
                TakeTotal();
                return true;
            }
        }

        function NTS1(obj) {
            var NTS1 = document.getElementById('<%=Txtone.ClientID%>').value || 0;
            var Result = parseInt(NTS1) * 1;
            if (!isNaN(Result)) {
                document.getElementById('<%=Txtonetotal.ClientID %>').value = Result;
                TakeTotal();
                return true;
            }
        }
    </script>

    <script type="text/javascript">
        //GRAND CASH TOTAL
        function TakeTotal() {
            debugger;
            var NT2000 = document.getElementById('<%=Txttwothoutotal.ClientID%>').value || 0;
            var NT1000 = document.getElementById('<%=Txtonethoutotal.ClientID%>').value || 0;
            var NT500 = document.getElementById('<%=Txtfivehuntotal.ClientID%>').value || 0;
            var NT200 = document.getElementById('<%=txtTwoHunTotal.ClientID%>').value || 0;
            var NT100 = document.getElementById('<%=Txthundrdtotal.ClientID%>').value || 0;
            var NT50 = document.getElementById('<%=Txtfiftytotal.ClientID%>').value || 0;
            var NT20 = document.getElementById('<%=Txttwntytotal.ClientID%>').value || 0;
            var NT10 = document.getElementById('<%=Txttentotal.ClientID%>').value || 0;
            var NT5 = document.getElementById('<%=Txtfivetotal.ClientID%>').value || 0;
            var NT2 = document.getElementById('<%=Txttwototal.ClientID%>').value || 0;
            var NT1 = document.getElementById('<%=Txtonetotal.ClientID%>').value || 0;
            <%--var CNS = document.getElementById('<%=txtCoins.ClientID%>').value || 0;--%>

            var SUM = parseFloat(NT2000) + parseFloat(NT1000) + parseFloat(NT500) + parseFloat(NT200) + parseFloat(NT100) + parseFloat(NT50) + parseFloat(NT20) + parseFloat(NT10) + parseFloat(NT5) + parseFloat(NT2) + parseFloat(NT1);// + parseFloat(CNS);
            if (!isNaN(SUM)) {
                document.getElementById('<%=txtTotalCshBal.ClientID%>').value = SUM;
            }
        }
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        Update Vault Cash
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="portlet-body">
                                <div class="tab-content">

                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-1">Note Type</label>
                                            <label class="control-label col-md-2">No Of Notes</label>
                                            <label class="control-label col-md-2">Total Amount</label>
                                        </div>
                                    </div>

                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-1" id="Lbl2000" runat="server">2000 </label>
                                            <div class="col-md-1">
                                                <label class="control-label" runat="server">*</label>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:TextBox ID="Txttwothousand" onkeypress="javascript:return isNumber (event)" onblur="NTS2000()" OnTextChanged="Txttwothousand_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server" TabIndex="1" />
                                            </div>
                                            <div class="col-md-1">
                                                <label class="control-label" runat="server">=</label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="Txttwothoutotal" Enabled="false" CssClass="form-control" runat="server" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-1" id="Lbl1000" runat="server">1000 </label>
                                            <div class="col-md-1">
                                                <label id="Label1" class="control-label" runat="server">*</label>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:TextBox ID="Txtonethousand" onkeypress="javascript:return isNumber (event)" onblur="NTS1000()" OnTextChanged="Txtonethousand_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server" />
                                            </div>
                                            <div class="col-md-1">
                                                <label id="Label2" class="control-label" runat="server">=</label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="Txtonethoutotal" Enabled="false" CssClass="form-control" runat="server" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-1" id="Lbl500" runat="server">500 </label>
                                            <div class="col-md-1">
                                                <label id="Label11" class="control-label" runat="server">*</label>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:TextBox ID="Txtfivehundrd" onkeypress="javascript:return isNumber (event)" onblur="NTS500()" OnTextChanged="Txtfivehundrd_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server" />
                                            </div>
                                            <div class="col-md-1">
                                                <label id="Label3" class="control-label" runat="server">=</label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="Txtfivehuntotal" Enabled="false" CssClass="form-control" runat="server" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-1" id="Lbl200" runat="server">200 </label>
                                            <div class="col-md-1">
                                                <label id="Label20" class="control-label" runat="server">*</label>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:TextBox ID="txtTwoHundred" onkeypress="javascript:return isNumber (event)" onblur="NTS200()" OnTextChanged="txtTwoHundred_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server" />
                                            </div>
                                            <div class="col-md-1">
                                                <label id="Label22" class="control-label" runat="server">=</label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtTwoHunTotal" Enabled="false" CssClass="form-control" runat="server" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-1" id="Lbl100" runat="server">100 </label>
                                            <div class="col-md-1">
                                                <label id="Label12" class="control-label" runat="server">*</label>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:TextBox ID="Txthundred" onkeypress="javascript:return isNumber (event)" onblur="NTS100()" OnTextChanged="Txthundred_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server" />
                                            </div>
                                            <div class="col-md-1">
                                                <label id="Label4" class="control-label" runat="server">=</label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="Txthundrdtotal" Enabled="false" CssClass="form-control" runat="server" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-1" id="Lbl50" runat="server">50 </label>
                                            <div class="col-md-1">
                                                <label id="Label13" class="control-label" runat="server">*</label>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:TextBox ID="Txtfifty" onkeypress="javascript:return isNumber (event)" onblur="NTS50()" OnTextChanged="Txtfifty_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server" />
                                            </div>
                                            <div class="col-md-1">
                                                <label id="Label5" class="control-label" runat="server">=</label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="Txtfiftytotal" Enabled="false" CssClass="form-control" runat="server" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-1" id="Lbl20" runat="server">20 </label>
                                            <div class="col-md-1">
                                                <label id="Label14" class="control-label" runat="server">*</label>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:TextBox ID="Txttwenty" onkeypress="javascript:return isNumber (event)" onblur="NTS20()" OnTextChanged="Txttwenty_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server" />
                                            </div>
                                            <div class="col-md-1">
                                                <label id="Label6" class="control-label" runat="server">=</label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="Txttwntytotal" Enabled="false" CssClass="form-control" runat="server" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-1" id="Lbl10" runat="server">10 </label>
                                            <div class="col-md-1">
                                                <label id="Label15" class="control-label" runat="server">*</label>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:TextBox ID="Txtten" onkeypress="javascript:return isNumber (event)" onblur="NTS10()" OnTextChanged="Txtten_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server" />
                                            </div>
                                            <div class="col-md-1">
                                                <label id="Label7" class="control-label" runat="server">=</label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="Txttentotal" Enabled="false" CssClass="form-control" runat="server" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-1" id="Lbl5" runat="server">5 </label>
                                            <div class="col-md-1">
                                                <label id="Label16" class="control-label" runat="server">*</label>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:TextBox ID="Txtfive" onkeypress="javascript:return isNumber (event)" onblur="NTS5()" OnTextChanged="Txtfive_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server" />
                                            </div>
                                            <div class="col-md-1">
                                                <label id="Label8" class="control-label" runat="server">=</label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="Txtfivetotal" Enabled="false" CssClass="form-control" runat="server" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-1" id="Lbl2" runat="server">2 </label>
                                            <div class="col-md-1">
                                                <label id="Label17" class="control-label" runat="server">*</label>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:TextBox ID="Txttwo" onkeypress="javascript:return isNumber (event)" onblur="NTS2()" OnTextChanged="Txttwo_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server" />
                                            </div>
                                            <div class="col-md-1">
                                                <label id="Label9" class="control-label" runat="server">=</label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="Txttwototal" Enabled="false" CssClass="form-control" runat="server" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-1" id="Lbl1" runat="server">1 </label>
                                            <div class="col-md-1">
                                                <label id="Label18" class="control-label" runat="server">*</label>
                                            </div>
                                            <div class="col-md-1">
                                                <asp:TextBox ID="Txtone" onkeypress="javascript:return isNumber (event)" onblur="NTS1()" OnTextChanged="Txtone_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server" />
                                            </div>
                                            <div class="col-md-1">
                                                <label id="Label10" class="control-label" runat="server">=</label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="Txtonetotal" Enabled="false" CssClass="form-control" runat="server" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-3" runat="server">Total Cash Balance </label>
                                            <div class="col-md-1">
                                                <label id="Label21" class="control-label" runat="server">=</label>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtTotalCshBal" Enabled="false" CssClass="form-control" runat="server" />
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9">
                                        <asp:Button ID="BtnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="BtnSubmit_Click" OnClientClick="javascript:return validate();" TabIndex="11" />
                                        <asp:Button ID="BtnClear" runat="server" Text="Clear" CssClass="btn btn-primary" OnClick="BtnClear_Click" OnClientClick="javascript:return validate();" TabIndex="12" />
                                        <asp:Button ID="BtnExit" runat="server" Text="Exit" CssClass="btn btn-primary" OnClick="BtnExit_Click" OnClientClick="javascript:return validate();" TabIndex="13" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
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
    </div>

</asp:Content>

