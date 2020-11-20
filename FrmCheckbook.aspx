<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmCheckbook.aspx.cs" Inherits="FrmCheckbook" %>

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

            var payordNo, setno, payordamt, acctype, custno, accno, name, payordissue, reasonissue;
            var message = '';
            payordNo = document.getElementById('<%=txtpay.ClientID%>').value;
            setno = document.getElementById('<%=txtset.ClientID%>').value;
            payordamt = document.getElementById('<%=txtpayamt.ClientID%>').value;
            acctype = document.getElementById('<%=txtacctype.ClientID%>').value;
            custno = document.getElementById('<%=txtcstno.ClientID%>').value;
            accno = document.getElementById('<%=txtaccno.ClientID%>').value;
            name = document.getElementById('<%=txtnam.ClientID%>').value;
            payordissue = document.getElementById('<%=txtpaynam.ClientID%>').value;
            reasonissue = document.getElementById('<%=ddl.ClientID%>').value;


            if (payordNo == "") {
                message = 'Please Enter PayOrderNo ....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtpay.ClientID%>').focus();
                return false;
            }

            if (setno == "") {
                message = 'Please Enter Set No....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtset.ClientID%>').focus();
                return false;
            }

            if (payordamt == "") {
                message = 'Please Enter PayOrder amount ....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtpayamt.ClientID%>').focus();
                return false;
            }

            if (acctype == "") {
                message = 'Please Enter Account Type ....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtacctype.ClientID%>').focus();
                return false;
            }

            if (custno == "") {
                message = 'Please Enter Customer No ....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtcstno.ClientID%>').focus();
                return false;
            }

            if (accno == "") {
                message = 'Please Enter Account No ....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtaccno.ClientID%>').focus();
                return false;
            }

            if (name == "") {
                message = 'Please Enter Name ....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtnam.ClientID%>').focus();
                return false;
            }
            if (payordissue == "") {
                message = 'Please Enter ....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtpaynam.ClientID%>').focus();
                return false;
            }

            if (reasonissue == "0") {
                message = 'Please Enter ....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=ddl.ClientID%>').focus();
                return false;
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
                        Payorder Check  Issue
                    </div>
                </div>
                <div class="portlet-body form">
                    <!--<form action="#" class="form-horizontal" id="submit_form" method="POST">-->
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab__blue">
                                        <ul class="nav nav-pills">
                                            <li>

                                               <asp:LinkButton ID="lnkAdd" runat="server" Text="a" OnClick="lnkAdd_Click" class="btn-circle-top bg-blue-madison"><i class="fa fa-plus-circle"></i>Add New</asp:LinkButton>

                                            </li>

                                            <li>
                                                <asp:LinkButton ID="lnkModify" runat="server" Text="MD" class="btn-circle-top bg-blue-madison" OnClick="lnkModify_Click"><i class="fa fa-pencil-square-o"></i>Modify</asp:LinkButton>
                                            </li>

                                            <li>
                                                <asp:LinkButton ID="lnkDelete" runat="server" Text="DL" class="btn-circle-top bg-blue-madison" OnClick="lnkDelete_Click"><i class="fa fa-times"></i>Delete</asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton ID="lnkAuthorized" runat="server" Text="AT" class="btn-circle-top bg-blue-madison" OnClick="lnkAuthorized_Click"><i class="fa fa-times"></i>AUTHORIZE</asp:LinkButton>
                                            <li>
                                                <asp:Label ID="lblStst" runat="server" Text="Activity Perform :"></asp:Label>
                                                <asp:Label ID="lblActivity" runat="server" Text=""></asp:Label>
                                            </li>
                                        </ul>
                                    </div>


                                    <div style="border: 1px solid #3598dc">
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">PayOrder No</label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtpay" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-1">Set No </label>
                                                <div class="col-lg-3">
                                                    <asp:TextBox ID="txtset" runat="server" CssClass="form-control" onkeypress="javascript:return isNumber (event)" OnTextChanged="txtset_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">PayOrder Amount </label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtpayamt" CssClass="form-control" runat="server" OnTextChanged="txtpayamt_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                                <div class="col-lg-4">
                                                    <asp:TextBox ID="txtpaynam" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>


                                            </div>
                                        </div>


                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Account Type </label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtacctype" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-lg-4">
                                                    <asp:TextBox ID="txtaccnam" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>


                                            </div>
                                        </div>



                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Customer No</label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtcstno" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-1">AccountNo</label>
                                                <div class="col-lg-3">
                                                    <asp:TextBox ID="txtaccno" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>


                                            </div>
                                        </div>



                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Name</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtnam" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>



                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">PayOrder Isssue Name</label>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtissuenam" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>



                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Reason to Issue</label>
                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="ddl" runat="server" CssClass="form-control">
                                                        <asp:ListItem Text="member" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="member" Value="2"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2"></label>
                                                <div class="col-md-3">
                                                    <asp:Button ID="btnissue" runat="server" Text="Issue" OnClick="btnissue_Click" CssClass="btn btn-primary" OnClientClick="Javascript:return isvalidate();" />
                                                    <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn btn-primary" />
                                                </div>


                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Print  <span class="required"></span></label>
                                                <div class="col-md-3">
                                                    <label for="bankchk">
                                                        <asp:RadioButton ID="rdbbc" runat="server" Text="BankCheque" GroupName="print" CssClass="radio-inline" AutoPostBack="true" />
                                                    </label>
                                                    <label for="payorder">
                                                        <asp:RadioButton ID="rdbpo" runat="server" Text="PayOrder" GroupName="print" CssClass="radio-inline" AutoPostBack="true" />
                                                    </label>
                                                    <div class="col-md-2">
                                                        <asp:Button ID="btnprint" runat="server" Text="Print" CssClass="btn btn-primary" OnClick="Submit_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">REfno</label>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtrefno" CssClass="form-control" runat="server"></asp:TextBox>
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
            <!--</form>-->
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

