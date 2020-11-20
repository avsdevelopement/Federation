<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmInwordAuthoDo.aspx.cs" Inherits="frmInwordAuthoDo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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

            var entrydate, setno, pcode, accno, acctype, opertype, particular, instrutype, bnkcode, brchcode, instruNo, instruDate, instruAmt;
            var message = '';

            entrydate = document.getElementById('<%=TxtEntrydate.ClientID%>').value;
            setno = document.getElementById('<%=txtsetno.ClientID%>').value;
            pcode = document.getElementById('<%=TxtProcode.ClientID%>').value;
            accno = document.getElementById('<%=TxtAccNo.ClientID%>').value;
            acctype = document.getElementById('<%=txtAccTypeid.ClientID%>').value;
            opertype = document.getElementById('<%=txtOpTypeId.ClientID%>').value;
            particular = document.getElementById('<%=txtpartic.ClientID%>').value;
            instrutype = document.getElementById('<%=ddlinsttype.ClientID%>').value;
            bnkcode = document.getElementById('<%=txtbankcd.ClientID%>').value;
            brchcode = document.getElementById('<%=txtbrnchcd.ClientID%>').value;
            instruNo = document.getElementById('<%=txtinstno.ClientID%>').value;
            instruDate = document.getElementById('<%=txtinstdate.ClientID%>').value;
            instruAmt = document.getElementById('<%=txtinstamt.ClientID%>').value;

            if (entrydate == "") {
                message = 'Please Enter entry date ....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                entrydate = document.getElementById('<%=TxtEntrydate.ClientID%>').focus();
                return false;
            }
            if (setno == "") {
                message = 'Please Enter set No ....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                setno = document.getElementById('<%=txtsetno.ClientID%>').focus();
                return false;
            }
            if (pcode == "") {
                message = 'Please Enter product code ....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                pcode = document.getElementById('<%=TxtProcode.ClientID%>').focus();
                return false;
            }

            if (accno == "") {
                message = 'Please Enter Account No....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                accno = document.getElementById('<%=TxtAccNo.ClientID%>').focus();
                return false;
            }

            if (acctype == "") {
                message = 'Please Enter account type....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                acctype = document.getElementById('<%=txtAccTypeid.ClientID%>').focus();
                return false;
            }

            if (opertype == "") {
                message = 'Please Enter operation type....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                opertype = document.getElementById('<%=txtOpTypeId.ClientID%>').focus();
                return false;
            }
            if (particular == "") {
                message = 'Please Enter particular....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                particular = document.getElementById('<%=txtpartic.ClientID%>').focus();
                return false;
            }

            if (instrutype == "0") {
                message = 'Please Enter instrument type ....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                instrutype = document.getElementById('<%=ddlinsttype.ClientID%>').focus();
                return false;
            }

            if (bnkcode == "") {
                message = 'Please Enter Bank Code ....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                bnkcode = document.getElementById('<%=txtbankcd.ClientID%>').focus();
                return false;
            }

            if (brchcode == "") {
                message = 'Please Enter branch code ....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                brchcode = document.getElementById('<%=txtbrnchcd.ClientID%>').focus();
                return false;
            }

            if (instruNo == "") {
                message = 'Please Enter instruments No ....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                instruNo = document.getElementById('<%=txtinstno.ClientID%>').focus();
                return false;
            }

            if (instruDate == "") {
                message = 'Please Enter instrument date ....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                instruDate = document.getElementById('<%=txtinstdate.ClientID%>').focus();
                return false;
            }
            if (instruAmt == "") {
                message = 'Please Enter instrument amount....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                instruAmt = document.getElementById('<%=txtinstamt.ClientID%>').focus();
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
                            Authorization
                        </div>
                    </div>
                    <div class="portlet-body form">
                        <div class="form-horizontal">
                            <div class="form-wizard">
                                <div class="form-body">

                                    <div class="tab-content">
                                        <div class="tab-pane active" id="tab1">

                                            <asp:Table ID="tbl_MainWindow" runat="server">
                                                <asp:TableRow ID="tbl_Row1" runat="server">

                                                    <asp:TableCell ID="tbl_Col1" runat="server" Width="70%">

                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-12">
                                                                <div class="col-md-3">
                                                                    <label class="control-label">Entry Date : <span class="required">* </span></label>
                                                                </div>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="TxtEntrydate" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-3">
                                                                    <label class="control-label">Set No : <span class="required">* </span></label>
                                                                </div>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="txtsetno" CssClass="form-control" runat="server" AutoPostBack="true" ReadOnly="True"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-12">
                                                                <div class="col-md-3">
                                                                    <label class="control-label">Product Code : <span class="required">* </span></label>
                                                                </div>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="TxtProcode" CssClass="form-control" runat="server" AutoPostBack="true" ReadOnly="True"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <asp:TextBox ID="TxtProName" CssClass="form-control" runat="server" ReadOnly="True"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-12">
                                                                <div class="col-md-3">
                                                                    <label class="control-label">Account No : <span class="required">* </span></label>
                                                                </div>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="TxtAccNo" CssClass="form-control" runat="server" AutoPostBack="true" ReadOnly="True"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <asp:TextBox ID="TxtAccName" CssClass="form-control" runat="server" ReadOnly="True"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-12">
                                                                <div class="col-md-3">
                                                                    <label class="control-label">Account Type : <span class="required">* </span></label>
                                                                </div>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="txtAccTypeid" CssClass="form-control" runat="server" AutoPostBack="true" Enabled="False"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <asp:TextBox ID="TxtAccTypeName" CssClass="form-control" runat="server" Enabled="False"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-12">
                                                                <div class="col-md-3">
                                                                    <label class="control-label">Operation Type : <span class="required">* </span></label>
                                                                </div>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="txtOpTypeId" CssClass="form-control" runat="server" AutoPostBack="true" Enabled="False"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <asp:TextBox ID="TxtOpTypeName" CssClass="form-control" runat="server" Enabled="False"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-12">
                                                                <div class="col-md-3">
                                                                    <label class="control-label">Particulars : <span class="required">* </span></label>
                                                                </div>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="txtpartic" CssClass="form-control" runat="server" Text="By Clg" ReadOnly="True"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-3">
                                                                    <label class="control-label">Instrument Type<span class="required">* </span></label>
                                                                </div>
                                                                <div class="col-md-3">
                                                                    <asp:DropDownList ID="ddlinsttype" runat="server" CssClass="form-control" Enabled="False"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-12">
                                                                <div class="col-md-3">
                                                                    <label class="control-label">Bank Code : <span class="required">* </span></label>
                                                                </div>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="txtbankcd" CssClass="form-control" runat="server" AutoPostBack="true" ReadOnly="True"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <asp:TextBox ID="txtbnkdname" CssClass="form-control" runat="server" Enabled="False"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-12">
                                                                <div class="col-md-3">
                                                                    <label class="control-label">Branch Code : <span class="required">* </span></label>
                                                                </div>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="txtbrnchcd" CssClass="form-control" runat="server" AutoPostBack="true" ReadOnly="True"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <asp:TextBox ID="txtbrnchcdname" CssClass="form-control" runat="server" AutoPostBack="true" Enabled="False"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-12">
                                                                <div class="col-md-3">
                                                                    <label class="control-label">Instrument No : <span class="required">* </span></label>
                                                                </div>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="txtinstno" CssClass="form-control" runat="server" ReadOnly="True"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-3">
                                                                    <label class="control-label">Instrument Date:<span class="required">* </span></label>
                                                                </div>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="txtinstdate" CssClass="form-control" runat="server" ReadOnly="True"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-12">
                                                                <div class="col-md-3">
                                                                    <label class="control-label">Instrument Amount : <span class="required">* </span></label>
                                                                </div>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="txtinstamt" CssClass="form-control" runat="server" ReadOnly="True"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                         <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-12">
                                                                <label class="control-label col-md-3">Clear Bal</label>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="txtBalance" CssClass="form-control" runat="server" TabIndex="-1" ReadOnly="true" PlaceHolder="Balance"></asp:TextBox>
                                                                </div>

                                                                <label class="control-label col-md-2">Total Bal</label>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="TxtTotalBal" CssClass="form-control" runat="server" TabIndex="-1" ReadOnly="true" PlaceHolder="Balance"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </asp:TableCell>

                                                    <asp:TableCell ID="tbl_Col2" runat="server" Width="30%">

                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-12">
                                                                <asp:Label ID="Label1" runat="server" Text="Signature" Style="font-weight: bold"></asp:Label>
                                                            </div>
                                                        </div>

                                                        <div id="Div1" runat="server" class="row" style="margin: 7px 0 7px 0">
                                                            <div id="Div2" runat="server" class="col-lg-12">
                                                                <img id="ImageSign" runat="server" style="height: 70%; width: 70%; border: 1px solid #000000; padding: 5px" />
                                                            </div>
                                                        </div>

                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-12">
                                                                <asp:Label ID="Label2" runat="server" Text="Photo" Style="font-weight: bold"></asp:Label>
                                                            </div>
                                                        </div>

                                                        <div id="Div3" runat="server" class="row" style="margin: 7px 0 7px 0">
                                                            <div id="Div4" runat="server" class="col-lg-12">
                                                                <img id="ImagePhoto" runat="server" style="height: 70%; width: 70%; border: 1px solid #000000; padding: 5px" />
                                                            </div>
                                                        </div>

                                                    </asp:TableCell>

                                                </asp:TableRow>
                                            </asp:Table>

                                        </div>
                                    </div>
                                </div>
                                <div class="form-actions">
                                    <div class="row">
                                        <div class="col-md-offset-3 col-md-9">
                                            <asp:Button ID="btnAuthorize" runat="server" CssClass="btn blue" Text="Authorize" OnClick="btnAuthorize_Click" OnClientClick="Javascript:return isvalidate();" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
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