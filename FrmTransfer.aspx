<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmTransfer.aspx.cs" Inherits="FrmTransfer" %>

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
        function isvalidate() {
            // var pcode, accno, disbustamt, procode, accNo, Amt;
            var pcode = document.getElementById('<%=TxtPtype.ClientID%>').value;
            var accno = document.getElementById('<%=TxtAccNo.ClientID%>').value;
            var procode = document.getElementById('<%=TxtCRPType.ClientID%>').value;
            var accNo = document.getElementById('<%=TxtCRAccNo.ClientID%>').value;


            if (pcode == "") {
                alert("Please enter Product Code...!!!!")
                document.getElementById('<%=TxtPtype.ClientID%>').focus();
                return false;
            }

            if (procode == "") {
                alert("Please enter  product code...!!! ")
                document.getElementById('<%=TxtCRPType.ClientID%>').focus();
                return false;
            }

        }

    </script>
    <script type="text/javascript">
        function FormatIt(obj) {
            if (obj.value.length == 2)
                obj.value = obj.value + "/";
            if (obj.value.length == 5)
                obj.value = obj.value + "/";
            if (obj.value.length == 11)
                alert("Please enter valid date");
        }


    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-md-12">
        <div class="portlet box blue" id="form_wizard_1">
            <div class="portlet-title">
                <div class="caption">
                    Transfer
                </div>
            </div>
            <div class="portlet-body form">
                <div class="form-horizontal">
                    <div class="form-wizard">
                        <div class="form-body">
                            <div class="tab-content">
                                <div class="tab-pane active" id="tab__blue">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #0f98ff">Debit Account Information : </strong></div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin: 7px 0 7px 0; margin-top: 20px;">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Product Code : <span class="required">* </span></label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="TxtGLCD" Style="Width: 30px; height: 33px; border: 1px solid #c0c1c1;" runat="server" TabIndex="1"></asp:TextBox>
                                                <asp:TextBox ID="TxtPtype" runat="server" onkeypress="javascript:return isNumber (event)" PlaceHolder="Product Code" Style="Width: 20%; height: 33px; border: 1px solid #c0c1c1;" OnTextChanged="TxtPtype_TextChanged" AutoPostBack="true" TabIndex="2"></asp:TextBox>&nbsp;&nbsp;
                                                    <asp:TextBox ID="TxtPname" runat="server" PlaceHolder="Product Name" OnTextChanged="TxtPname_TextChanged" Style="width: 63%; height: 33px; margin-left: -3px; border: 1px solid #c0c1c1;" AutoPostBack="true" TabIndex="3"></asp:TextBox>
                                                <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="TxtPname"
                                                    UseContextKey="true"
                                                    CompletionInterval="1"
                                                    CompletionSetCount="20"
                                                    MinimumPrefixLength="1"
                                                    EnableCaching="true"
                                                    ServicePath="~/WebServices/Contact.asmx"
                                                    ServiceMethod="GetGlName">
                                                </asp:AutoCompleteExtender>
                                            </div>
                                            <label class="control-label col-md-2">Account No : <span class="required">* </span></label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="TxtAccNo" runat="server" PlaceHolder="Account No" onkeypress="javascript:return isNumber (event)" Style="Width: 33%; height: 33px; border: 1px solid #c0c1c1;" OnTextChanged="TxtAccNo_TextChanged" AutoPostBack="true" TabIndex="4"></asp:TextBox>&nbsp;&nbsp;
                                                    <asp:TextBox ID="TxtCustName" runat="server" PlaceHolder="Account Holder Name" Style="width: 61%; height: 33px; margin-left: -3px; border: 1px solid #c0c1c1;" OnTextChanged="TxtCustName_TextChanged" AutoPostBack="true" TabIndex="5"></asp:TextBox>
                                                <asp:AutoCompleteExtender ID="AutoAccname" runat="server" TargetControlID="TxtCustName"
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
                                    <div class="row" id="DIVTR" runat="server" visible="true">
                                        <div class="col-lg-12">
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);">
                                                        <strong style="color: #0f98ff">
                                                            <br />
                                                            Credit Account Information : </strong>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="margin: 7px 0 7px 0; margin-top: 20px;">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Product Code : <span class="required">* </span></label>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="TxtCRGLCD" runat="server" Style="Width: 30px; height: 33px; border: 1px solid #c0c1c1;" TabIndex="6"></asp:TextBox>
                                                        <asp:TextBox ID="TxtCRPType" runat="server" onkeypress="javascript:return isNumber (event)" PlaceHolder="Product Code" Style="Width: 20%; height: 33px; border: 1px solid #c0c1c1;" AutoPostBack="true" OnTextChanged="TxtCRPType_TextChanged" TabIndex="7"></asp:TextBox>&nbsp;&nbsp;
                                                    <asp:TextBox ID="TxtCRPTName" runat="server" PlaceHolder="Product Name" Style="width: 63%; height: 33px; margin-left: -3px; border: 1px solid #c0c1c1;" AutoPostBack="true" OnTextChanged="TxtCRPTName_TextChanged" TabIndex="8"></asp:TextBox>
                                                        <asp:AutoCompleteExtender ID="autoproduct" runat="server" TargetControlID="TxtCRPTName"
                                                            UseContextKey="true"
                                                            CompletionInterval="1"
                                                            CompletionSetCount="20"
                                                            MinimumPrefixLength="1"
                                                            EnableCaching="true"
                                                            ServicePath="~/WebServices/Contact.asmx"
                                                            ServiceMethod="GetGlName">
                                                        </asp:AutoCompleteExtender>
                                                    </div>
                                                    <label class="control-label col-md-2">Account No : <span class="required">* </span></label>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="TxtCRAccNo" runat="server" PlaceHolder="Account No" onkeypress="javascript:return isNumber (event)" Style="Width: 33%; height: 33px; border: 1px solid #c0c1c1;" OnTextChanged="TxtCRAccNo_TextChanged" AutoPostBack="true" TabIndex="9"></asp:TextBox>&nbsp;&nbsp;
                                                    <asp:TextBox ID="TxtCRACName" runat="server" PlaceHolder="Account Holder Name" Style="width: 61%; height: 33px; margin-left: -3px; border: 1px solid #c0c1c1;" OnTextChanged="TxtCRACName_TextChanged" AutoPostBack="true" TabIndex="10"></asp:TextBox>
                                                        <br />
                                                        <asp:AutoCompleteExtender ID="Autocrname" runat="server" TargetControlID="TxtCRACName"
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

                                            <div class="row" style="margin: 7px 0 7px 0; margin-top: 20px;">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Instrument No. :</label>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="TxtChequeNo" CssClass="form-control" runat="server" TabIndex="11" PlaceHolder="Instrument No"></asp:TextBox>
                                                    </div>
                                                    <label class="control-label col-md-2">Instrument Date :</label>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="TxtChequeDate" CssClass="form-control" onkeyup="FormatIt(this)" runat="server" TabIndex="12" PlaceHolder="Instrument Date"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin: 7px 0 7px 0; margin-top: 20px;">
                                                <div class="col-lg-12">

                                                    <label class="control-label col-md-2">
                                                        Naration : <span class="required">* </span>
                                                    </label>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TxtNarration" runat="server" PlaceHolder="Naration" Style="Width: 100%; height: 33px; border: 1px solid #c0c1c1;" TabIndex="13"></asp:TextBox>
                                                    </div>
                                                    <label class="control-label col-md-2">
                                                        Amount : <span class="required">* </span>
                                                    </label>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="TxtCRAmount" runat="server" AutoPostBack="true" onkeypress="javascript:return isNumber (event)" PlaceHolder="Amount" Style="width: 30%; height: 33px; border: 1px solid #c0c1c1;" TabIndex="14"></asp:TextBox>
                                                        <%-- <asp:Button ID="AddMore" runat="server" Text="Add More" CssClass="btn btn-success" OnClick="AddMore_Click" />--%>
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <asp:Button ID="SubmitTemp" runat="server" Text="SUBMIT" CssClass="btn btn-primary" OnClick="SubmitTemp_Click" OnClientClick="javascript:return isvalidate()" TabIndex="15" />
                                                </div>
                                                <br />
                                                <br />



                                            </div>
                                            <div class="table-scrollable">
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
    <div class="row">
        <div class="col-lg-12">
            <div class="table-scrollable">
                <table class="table table-striped table-bordered table-hover">
                    <thead>
                        <tr>
                            <th>
                                <asp:GridView ID="grdTransfer" runat="server" AllowPaging="True"
                                    AlternatingRowStyle-CssClass="alt" EditRowStyle-BackColor="#FFFF99"
                                    PageIndex="10" PageSize="25"
                                    PagerStyle-CssClass="pgr" Width="100%">
                                    <Columns>


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

