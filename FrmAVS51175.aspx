<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAVS51175.aspx.cs" Inherits="FrmAVS51175" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.16/css/dataTables.bootstrap4.min.css" />
    <script src="https://cdn.datatables.net/1.10.16/js/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="https://cdn.datatables.net/1.10.16/js/dataTables.bootstrap4.min.js" type="text/javascript"></script>

    <style>
        .form-inline .form-group {
            margin-right: 10px;
        }

        .well-primary {
            color: rgb(255, 255, 255);
            background-color: rgb(66, 139, 202);
            border-color: rgb(53, 126, 189);
        }

        .glyphicon {
            margin-right: 5px;
        }


        inset {
            border-style: inset;
        }

        /*Media Query*/
        @media (min-width: 11px) {

            .input-group {
                margin-top: -8px;
            }
        }

        .example-modal .modal {
            position: relative;
            top: auto;
            bottom: auto;
            right: auto;
            left: auto;
            display: block;
            z-index: 1;
        }


        .example-modal .modal {
            background: transparent !important;
        }
    </style>
    <script type="text/javascript">
        function ShowPopup(title, body) {

            alert(title);

            window.location = "FrmAVS51175.aspx";
        }
    </script>
    <script type="text/javascript">
        function FormatIt(obj) {

            if (obj.value.length == 2) // Day
                obj.value = obj.value + "/";
            if (obj.value.length == 5) // month 
                obj.value = obj.value + "/";
            if (obj.value.length == 11) // year 
            {
                alert("Please Enter valid Date");
                obj.value = "";
            }
        }
    </script>
    <script>
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;
            return true;
        }


        function OnltAlphabets(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return true;

            return false;
        }

        function CheckFirstChar(key, txt) {
            if (key == 32 && txt.value.length <= 0) {
                return false;
            }
            else if (txt.value.length > 0) {
                if (txt.value.charCodeAt(0) == 32) {
                    txt.value = txt.value.substring(1, txt.value.length);
                    return true;
                }
            }
            return true;
        }

    </script>
    <script type="text/javascript">
        function isvalidateForVenMaster() {


            var Venderno = document.getElementById('<%=TxtVendorId.ClientID%>').value;
            var VenderName = document.getElementById('<%=txtVendorName.ClientID%>').value;
            var ProdName = document.getElementById('<%=txtProductNAme.ClientID%>').value;
            var ProdId = document.getElementById('<%=TxtProductId.ClientID%>').value;
            var Rate = document.getElementById('<%=TxtRate.ClientID%>').value;
            var CGST = document.getElementById('<%=TxtCGST.ClientID%>').value;

            var SGST = document.getElementById('<%=txtSGST.ClientID%>').value;
            var SCGSTPRCD = document.getElementById('<%=txtSGstPrcd.ClientID%>').value;
            var CGSTPRCD = document.getElementById('<%=txtCGDTPROCODE.ClientID%>').value;

            var message = '';

            if (Venderno == "") {
                message = 'Please Enter Vendor Number....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtVendorId.ClientID%>').focus();
                return false;
            }

            if (VenderName == "") {
                message = 'Please Enter Vendor Name....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtVendorName.ClientID%>').focus();
                return false;
            }

            if (ProdId == "") {
                message = 'Please Enter Contact Person Name....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtProductId.ClientID%>').focus();
                return false;
            }


            if (ProdName == "") {
                message = 'Please Enter Email Id....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtProductNAme.ClientID%>').focus();
                return false;
            }
            if (Rate == "") {
                message = 'Please Enter Address Line 1....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtRate.ClientID%>').focus();
                return false;
            }
            if (CGST == "") {
                message = 'Please Enter Address Line 2...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtCGST.ClientID%>').focus();
                        return false;
                    }

                    if (SGST == "") {
                        message = 'Please Enter City....!!\n';
                        $('#alertModal').find('.modal-body p').text(message);
                        $('#alertModal').modal('show')
                        document.getElementById('<%=txtSGST.ClientID%>').focus();
                        return false;
                    }
                    if (SCGSTPRCD == "") {
                        message = 'Please Enter Pin Code Number....!!\n';
                        $('#alertModal').find('.modal-body p').text(message);
                        $('#alertModal').modal('show')
                        document.getElementById('<%=txtSGstPrcd.ClientID%>').focus();
                        return false;
                    }
                    if (CGSTPRCD == "") {
                        message = 'Please Enter Pin Code Number....!!\n';
                        $('#alertModal').find('.modal-body p').text(message);
                        $('#alertModal').modal('show')
                        document.getElementById('<%=txtCGDTPROCODE.ClientID%>').focus();
                        return false;
                    }
                }
    </script>
    <style>
        .gridview {
            font-family: Arial;
            background-color: #FFFFFF;
            border: solid 1px #CCCCCC;
            margin-left: 100px;
        }

        .gridViewHeader {
            background-color: #0066CC;
            color: #FFFFFF;
            padding: 4px 50px 4px 4px;
            text-align: left;
            border-width: 0px;
            border-collapse: collapse;
            margin-left: 100px;
        }

        .gridViewRow {
            background-color: #99CCFF;
            color: #000000;
            border-width: 0px;
            border-collapse: collapse;
            margin-left: 100px;
        }

        .gridViewAltRow {
            background-color: #FFFFFF;
            border-width: 0px;
            border-collapse: collapse;
            margin-left: 100px;
        }

        .gridViewSelectedRow {
            background-color: #FFCC00;
            color: #666666;
            border-width: 0px;
            border-collapse: collapse;
            margin-left: 100px;
        }

        .gridViewPager {
            background-color: #0066CC;
            color: #FFFFFF;
            text-align: left;
            padding: 10px;
            margin-left: 100px;
        }
    </style>

    <style>
        .form-inline .form-group {
            margin-right: 10px;
        }

        .well-primary {
            color: rgb(255, 255, 255);
            background-color: rgb(66, 139, 202);
            border-color: rgb(53, 126, 189);
        }

        .glyphicon {
            margin-right: 5px;
        }


        inset {
            border-style: inset;
        }

        /*Media Query*/
        @media (min-width: 1281px) {

            .input-group {
                margin-top: -8px;
            }
        }

        .example-modal .modal {
            position: relative;
            top: auto;
            bottom: auto;
            right: auto;
            left: auto;
            display: block;
            z-index: 1;
        }

        .example-modal .modal {
            background: transparent !important;
        }
    </style>
    <style type="text/css">
        .Background {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
            -webkit-transition: background-color 2s, filter 2s,opacity 2s; /* Safari prior 6.1 */
            transition: background-color 2s, filter 2s,opacity 2s;
        }

        .Popup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 400px;
            height: 350px;
        }

        .lbl {
            font-size: 16px;
            font-style: italic;
            font-weight: bold;
        }

        .row1 {
            font-weight: bold;
        }


        .row2 {
            font-weight: bold;
        }



        tr.row2 td {
            padding-top: 10px;
        }

        tr.row3 td {
            padding-top: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
       <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Always" runat="server">
            <ContentTemplate>



    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        Product Master Details
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div class="portlet-body">
                                        <div class="tab-pane active" id="tab__blue">
                                            <ul class="nav nav-pills">
                                                <li class="pull-right">
                                                    <asp:Label ID="lblStst" runat="server" Text="Activity Perform :" Style="font-weight: bold;"></asp:Label>
                                                    <asp:Label ID="lblActivity" runat="server" Text=""></asp:Label>
                                                </li>
                                                <li>
                                                    <asp:LinkButton ID="lnkAdd" runat="server" Text="a" class="btn btn-default" OnClick="lnkAdd_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-asterisk"></i>Add New</asp:LinkButton>
                                                </li>
                                                <li>
                                                    <asp:LinkButton ID="lnkModify" runat="server" Text="a" class="btn btn-default" OnClick="lnkModify_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-plus-circle"></i>Modify</asp:LinkButton>
                                                </li>
                                                <li>
                                                    <asp:LinkButton ID="LnkAuthorize" runat="server" Text="a" class="btn btn-default" OnClick="LnkAuthorize_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-plus-circle"></i>Authorize</asp:LinkButton>
                                                </li>
                                                <li>
                                                    <asp:LinkButton ID="lnkDelete" runat="server" Text="a" class="btn btn-default" OnClick="lnkDelete_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-plus-circle"></i>Delete</asp:LinkButton>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="portlet-title">
                                            <div class="caption">
                                                <div class="form-actions">


                                                    <div style="border: 1px solid #3598dc">
                                                        <div class="row">
                                                            <div class="col-lg-12">
                                                                <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Product Master Detail's : </strong></div>
                                                            </div>
                                                        </div>
                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-12">
                                                                <div class="col-md-2">
                                                                    <label class="control-label ">Vendor ID.<span class="required">*</span></label>

                                                                </div>
                                                                <div class="col-md-2">
                                                                    <asp:TextBox ID="TxtVendorId" CssClass="form-control" onkeyup="CheckFirstChar(event.keyCode, this);" OnTextChanged="TxtVendorId_TextChanged" onkeydown="return CheckFirstChar(event.keyCode, this);" onkeypress="javascript:return isNumber (event)" runat="server" AutoPostBack="true" TabIndex="1"></asp:TextBox>
                                                                    <div id="Div1" style="height: 200px; overflow-y: scroll;"></div>
                                                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="TxtVendorId"
                                                                        UseContextKey="true"
                                                                        CompletionInterval="1"
                                                                        CompletionSetCount="20"
                                                                        MinimumPrefixLength="1"
                                                                        EnableCaching="true"
                                                                        ServicePath="~/WebServices/Contact.asmx"
                                                                        ServiceMethod="GetVendorName"
                                                                        CompletionListElementID="Div1">
                                                                    </asp:AutoCompleteExtender>
                                                                </div>
                                                                <div class="col-md-2">
                                                                    <label class="control-label">Vendor Name<span class="required">*</span></label>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <asp:TextBox ID="txtVendorName" runat="server" AutoPostBack="true" placeholder="Enter Vendor Name" onkeyup="CheckFirstChar(event.keyCode, this);" OnTextChanged="txtVendorName_TextChanged" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" TabIndex="2"></asp:TextBox>
                                                                    <div id="CustList" style="height: 200px; overflow-y: scroll;"></div>
                                                                    <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="txtVendorName"
                                                                        UseContextKey="true"
                                                                        CompletionInterval="1"
                                                                        CompletionSetCount="20"
                                                                        MinimumPrefixLength="1"
                                                                        EnableCaching="true"
                                                                        ServicePath="~/WebServices/Contact.asmx"
                                                                        ServiceMethod="GetVendorName"
                                                                        CompletionListElementID="CustList">
                                                                    </asp:AutoCompleteExtender>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-12">
                                                                <div class="col-md-2">
                                                                    <label class="control-label">Product Id <span class="required">*</span></label>
                                                                </div>
                                                                <div class="col-md-2">
                                                                    <asp:TextBox ID="TxtProductId" Placeholder="Enter Product Name" runat="server" CssClass="form-control" TabIndex="4" OnTextChanged="TxtProductId_TextChanged" onkeyup="CheckFirstChar(event.keyCode, this);" onkeydown="return CheckFirstChar(event.keyCode, this);" AutoPostBack="true"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-2">
                                                                    <label class="control-label">Product Name <span class="required">*</span></label>

                                                                </div>
                                                                <div class="col-md-4">
                                                                    <asp:TextBox ID="txtProductNAme" Placeholder="Enter  Product Name" OnTextChanged="txtProductNAme_TextChanged" runat="server" CssClass="form-control" onkeyup="CheckFirstChar(event.keyCode, this);" onkeydown="return CheckFirstChar(event.keyCode, this);" TabIndex="4" AutoPostBack="true"></asp:TextBox>
                                                                    <div id="CustList2" style="height: 200px; overflow-y: scroll;"></div>
                                                                    <asp:AutoCompleteExtender ID="AutoProduct" runat="server" TargetControlID="txtProductNAme"
                                                                        UseContextKey="true"
                                                                        CompletionInterval="1"
                                                                        CompletionSetCount="20"
                                                                        MinimumPrefixLength="1"
                                                                        EnableCaching="true"
                                                                        ServicePath="~/WebServices/Contact.asmx"
                                                                        ServiceMethod="GetProductName"
                                                                        CompletionListElementID="CustList2">
                                                                    </asp:AutoCompleteExtender>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-12">
                                                                <div class="col-md-2">
                                                                    <label class="control-label">Rate  </label>
                                                                </div>
                                                                <div class="col-md-2">
                                                                    <asp:TextBox ID="TxtRate" runat="server" placeholder="Enter Rate" CssClass="form-control" onkeyup="CheckFirstChar(event.keyCode, this);" onkeydown="return CheckFirstChar(event.keyCode, this);" TabIndex="27" onkeypress="javascript:return isNumber (event)"></asp:TextBox>

                                                                </div>


                                                            </div>
                                                        </div>
                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-12">
                                                                <div class="col-md-2">
                                                                    <label class="control-label">SGST  </label>
                                                                </div>
                                                                <div class="col-md-2">
                                                                    <asp:TextBox ID="txtSGST" runat="server" placeholder="Enter SGST" CssClass="form-control" onkeyup="CheckFirstChar(event.keyCode, this);" onkeydown="return CheckFirstChar(event.keyCode, this);" TabIndex="27" onkeypress="javascript:return isNumber (event)"></asp:TextBox>

                                                                </div>
                                                                <div class=" col-md-2">
                                                                    <label class="control-label">SGST Product Code </label>
                                                                </div>

                                                                <div class=" col-md-2">
                                                                    <asp:TextBox ID="txtSGstPrcd" runat="server" placeholder="Enter SGST Product Code" CssClass="form-control" onkeyup="CheckFirstChar(event.keyCode, this);" onkeydown="return CheckFirstChar(event.keyCode, this);" TabIndex="27" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                                </div>

                                                            </div>
                                                        </div>
                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-12">
                                                                <div class="col-md-2">
                                                                    <label class="control-label">CGST  </label>
                                                                </div>
                                                                <div class="col-md-2">
                                                                    <asp:TextBox ID="TxtCGST" runat="server" placeholder="Enter CGST" CssClass="form-control" TabIndex="27" onkeyup="CheckFirstChar(event.keyCode, this);" onkeydown="return CheckFirstChar(event.keyCode, this);" onkeypress="javascript:return isNumber (event)"></asp:TextBox>

                                                                </div>
                                                                <div class=" col-md-2">
                                                                    <label class="control-label">CGST Product Code </label>
                                                                </div>

                                                                <div class=" col-md-2">
                                                                    <asp:TextBox ID="txtCGDTPROCODE" runat="server" placeholder="Enter CGST Product Code" CssClass="form-control" onkeyup="CheckFirstChar(event.keyCode, this);" onkeydown="return CheckFirstChar(event.keyCode, this);" TabIndex="27" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                                </div>

                                                            </div>
                                                        </div>

                                                    </div>




                                                    <br />

                                                    <div class="form-actions col-md-12">
                                                        <%-- <div class="row">--%>

                                                        <div class="col-md-1">
                                                            <asp:Button ID="BtnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="BtnSubmit_Click" OnClientClick="Javascript:return IsValide();CheckForFutureDate();IsEmailAddress(txtemailid);" TabIndex="49" />
                                                        </div>
                                                        <div class="col-md-1">
                                                            <asp:Button ID="Btnclear" runat="server" Text="ClearAll" CssClass="btn btn-primary" OnClick="Btnclear_Click" TabIndex="50" />
                                                        </div>
                                                        <div class="col-md-1">
                                                            <asp:Button ID="btnExit1" runat="server" Text="Go To Main Menu" CssClass="btn btn-primary" OnClick="btnExit1_Click" TabIndex="51" />

                                                        </div>
                                                        <div class="col-md-1">
                                                            <asp:Button ID="BtnCancle" runat="server" Text="Cancel" CssClass="btn btn-primary" Visible="false" OnClick="BtnCancle_Click" TabIndex="51" />
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:FileUpload ID="FileUpload1" runat="server" />
                                                             </div>
                                                        <div class="col-md-2">
                                                            <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />
                                                        </div>

                                                    </div>
                                                    <%-- </div>--%>
                                                </div>
                                                <br />
                                                <asp:GridView ID="GridView1" runat="server" OnPageIndexChanging = "PageIndexChanging"    
                                                    AllowPaging="True" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" EditRowStyle-BackColor="#FFFF99"
                                                     PageIndex="10" PageSize="10" PagerStyle-CssClass="pgr" CssClass="mGrid" BorderWidth="1px" BorderColor="#333300" Width="100%"
                                                                                ShowFooter="true"></asp:GridView>
                                                <div class="row" id="Div_grid" runat="server">
                                                    <div class="col-md-12">
                                                        <div class="table-scrollable" style="width: 100%; height: 500px; overflow-x: auto; overflow-y: auto">
                                                            <table class="table table-striped table-bordered table-hover">
                                                                <thead>
                                                                    <tr>
                                                                        <th>
                                                                            <%--<asp:GridView ID="GrdProduct" runat="server" CellPadding="6" CellSpacing="7"
                                                                                    ForeColor="#333333" OnPageIndexChanging="GrdProduct_PageIndexChanging" OnRowCommand="GrdProduct_RowCommand"
                                                                                    PageIndex="10" AutoGenerateColumns="False" CssClass="mGrid"  BorderWidth="1px" OnSelectedIndexChanged="GrdProduct_SelectedIndexChanged"
                                                                                    BorderColor="#333300" Width="100%" AllowPaging="True"
                                                                                    ShowFooter="true">--%>
                                                                            <asp:GridView ID="GrdProduct" runat="server" CellPadding="6" CellSpacing="7"
                                                                                ForeColor="#333333" OnPageIndexChanged="GrdProduct_SelectedIndexChanged" OnPageIndexChanging="GrdProduct_PageIndexChanging"
                                                                                AllowPaging="True" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" EditRowStyle-BackColor="#FFFF99"
                                                                                PageIndex="10" PageSize="10" PagerStyle-CssClass="pgr" CssClass="mGrid" BorderWidth="1px" BorderColor="#333300" Width="100%"
                                                                                ShowFooter="true">
                                                                                <AlternatingRowStyle BackColor="White" />
                                                                                <Columns>

                                                                                    <asp:TemplateField HeaderText="VENDOR ID" Visible="true">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="VENDORID" runat="server" Text='<%# Eval("VENDORID") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="VENDOR NAME" Visible="true">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="VENDERNAME" runat="server" Text='<%# Eval("VENDERNAME ") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="PRODUCT ID." Visible="true">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="PRODID" runat="server" Text='<%# Eval("PRODID ") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="PRODUCT NAME" Visible="true">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="PRODNAME" runat="server" Text='<%# Eval("PRODNAME") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="RATE" Visible="true">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="RATE" runat="server" Text='<%# Eval("RATE ") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="SGST" Visible="true">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="SGST" runat="server" Text='<%# Eval("SGST ") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="CGST" Visible="true">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="CGST" runat="server" Text='<%# Eval("CGST ") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="STATUS" Visible="true">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="STATUS" runat="server" Text='<%# Eval("STATUS") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="DATE" Visible="true">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="ENTRYDATE" runat="server" Text='<%# Eval("ENTRYDATE") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="LOGINCODE" Visible="true">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="LOGINCODE" runat="server" Text='<%# Eval("LOGINCODE") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <%--                                                                                            <asp:TemplateField HeaderText="Authorize" Visible="true">
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton ID="LnkAutorise" runat="server"  CommandArgument='<%#Eval("PRODID")%>' CommandName="Select" OnClick="LnkAutorise_Click"   class="glyphicon glyphicon-pencil" ></asp:LinkButton>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>--%>

                                                                                    <%-- <asp:TemplateField HeaderText="Delete" Visible="true">
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton ID="lnkDelete1" runat="server" CommandName="select" class="glyphicon glyphicon-trash" OnClick="lnkDelete1_Click" ></asp:LinkButton>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                                                                            </asp:TemplateField>--%>

                                                                                    <asp:TemplateField HeaderText="View" Visible="true">
                                                                                        <ItemTemplate>
                                                                                            <asp:LinkButton ID="lnkView" runat="server" CommandArgument='<%#Eval("PRODID")+","+ Eval("VENDORID")%>' CommandName="select" OnClick="lnkView_Click" class="glyphicon glyphicon-pencil"></asp:LinkButton>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="Th" HorizontalAlign="Center" />
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
    

</ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnUpload" />
            </Triggers>
        </asp:UpdatePanel>
</asp:Content>

