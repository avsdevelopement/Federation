<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmCashReceiptAuthDo.aspx.cs" Inherits="FrmCashReceiptAuthDo" %>

<!DOCTYPE html>

<html>
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
    <!-- END GLOBAL MANDATORY STYLES -->

    <script type="text/javascript">
        function isvalidate() {

            var entrydate, setno, acctype, accno, nar1, nar2, bal, amt;
            var message = '';

            entrydate = document.getElementById('<%=TxtEntrydate.ClientID%>').value;
            setno = document.getElementById('<%=txtsetno.ClientID%>').value;
            acctype = document.getElementById('<%=TxtProcode.ClientID%>').value;
            accno = document.getElementById('<%=TxtAccNo.ClientID%>').value;
            nar1 = document.getElementById('<%=txtnaration.ClientID%>').value;
            nar2 = document.getElementById('<%=txtnaration1.ClientID%>').value;
            bal = document.getElementById('<%=txtBalance.ClientID%>').value;
            amt = document.getElementById('<%=txtamount.ClientID%>').value;

            if (entrydate == "") {
                message = 'Please Enter Entry Date ....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtEntrydate.ClientID%>').focus();
                return false;
            }

            if (setno == "") {
                message = 'Please Enter Set No ....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtsetno.ClientID%>').focus();
                return false;
            }

            if (acctype == "") {
                message = 'Please Enter Account Type ....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtProcode.ClientID%>').focus();
                return false;
            }

            if (accno == "") {
                message = 'Please Enter Account No ....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtAccNo.ClientID%>').focus();
                return false;
            }

            if (nar1 == "") {
                message = 'Please Enter Naration ....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtnaration.ClientID%>').focus();
                return false;
            }

            if (nar2 == "") {
                message = 'Please Enter Naration1 ....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtnaration1.ClientID%>').focus();
                return false;
            }

            if (bal == "") {
                message = 'Please Enter Balance ....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtBalance.ClientID%>').focus();
                return false;
            }

            if (amt == "") {
                message = 'Please Enter amount ....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtamount.ClientID%>').focus();
                return false;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">

        <div class="row" style="margin: 10px 10px 10px 10px">
            <div class="col-md-12">
                <div class="portlet box blue" id="form_wizard_1">
                    <div class="portlet-title">
                        <div class="caption">
                            Cash Receipt Authorization
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
                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-11">
                                                    <label class="control-label col-md-3">Entry Date : <span class="required">* </span></label>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TxtEntrydate" CssClass="form-control" runat="server" Enabled="False"></asp:TextBox>
                                                    </div>
                                                    <label class="control-label col-md-2">Set No : <span class="required" visible="false"></span></label>
                                                    <div class="col-md-3" visible="false">
                                                        <asp:TextBox ID="txtsetno" CssClass="form-control" runat="server" ReadOnly="True"></asp:TextBox>
                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtsetno_TextChanged" ReadOnly="True"></asp:TextBox>--%>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-11">
                                                    <label class="control-label col-md-3">Account Type : <span class="required">* </span></label>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TxtProcode" CssClass="form-control" runat="server" ReadOnly="True"></asp:TextBox>
                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" runat="server"  AutoPostBack="true" OnTextChanged="TxtProcode_TextChanged" ></asp:TextBox>--%>
                                                    </div>
                                                    <div class="col-md-5">
                                                        <asp:TextBox ID="TxtProName" CssClass="form-control" runat="server" Enabled="False"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-11">
                                                    <label class="control-label col-md-3">Account No : <span class="required">* </span></label>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TxtAccNo" CssClass="form-control" runat="server" Enabled="False"></asp:TextBox>
                                                        <%--<asp:TextBox ID="TextBox1" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="TxtAccNo_TextChanged"></asp:TextBox>--%>
                                                    </div>
                                                    <div class="col-md-5">
                                                        <asp:TextBox ID="TxtAccName" CssClass="form-control" runat="server" Enabled="False"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-11">
                                                    <label class="control-label col-md-3">Naration : <span class="required">* </span></label>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtnaration" CssClass="form-control" runat="server" Text="By Cash" Enabled="False"></asp:TextBox>
                                                    </div>
                                                    <label class="control-label col-md-2">Naration 2 : <span class="required"></span></label>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtnaration1" CssClass="form-control" runat="server" TabIndex="7" Enabled="False"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-11">

                                                    <label class="control-label col-md-3">Balance : <span class="required">* </span></label>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtBalance" CssClass="form-control" runat="server" TabIndex="-1" ReadOnly="true"></asp:TextBox>
                                                    </div>

                                                    <label class="control-label col-md-2">Amount : <span class="required">* </span></label>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtamount" CssClass="form-control" runat="server" TabIndex="8" Enabled="False"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>

                                    </div>
                                </div>
                                <div class="form-actions">
                                    <div class="row">
                                        <div class="col-md-offset-3 col-md-9">
                                            <%--<button type="button" class="btn blue" >Submit</button>--%>
                                            <%--OnClientClick="javascript:return validate();"--%>
                                            <asp:Button ID="btnSubmit" runat="server" CssClass="btn blue" Text="Authorize" OnClick="btnSubmit_Click" OnClientClick="Javascript:return isvalidate();" />
                                            <%--<asp:Button ID="Button1" runat="server" CssClass="btn blue" Text="Submit"  OnClick="SaveOwg" OnClientClick="javascript:return validate();"/>--%>
                                            <%--<asp:Button ID="btnUpdate" runat="server" CssClass="btn blue" Text="Delete" OnClick="UpdateOwg" Visible="false"/>
                                        <asp:Button ID="btnReport" runat="server" CssClass="btn blue" Text="Report" OnClick="btnReport_Click"/>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!--</form>-->
                    </div>
                </div>
            </div>
        </div>

    </form>
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

</body>
</html>
