<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAVS51177.aspx.cs" Inherits="FrmAVS51177" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

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

            window.location = "FrmAVS51177.aspx";
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
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        Opening Stock
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
                                                    <div class="row">

                                                        <div id="DIVPer" runat="server" style="border: 1px solid #3598dc">
                                                            <div></div>
                                                            <div class="row">
                                                                <div class="col-lg-12">
                                                                    <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Stock Detail's : </strong></div>
                                                                </div>
                                                            </div>
                                                            <div></div>
                                                            <br />

                                                            <div class="row" style="margin: 9px 0 9px 0">
                                                                <div class="col-lg-12">
                                                                    <div class="col-md-1">
                                                                        <asp:Label ID="Label9" Width="120px" Text="BRCD" runat="server">  </asp:Label>
                                                                    </div>
                                                                    <div class="col-md-3">
                                                                        <asp:TextBox ID="txtBRCD" Enabled="false" Width="200px" MaxLength="10" onkeyup="CheckFirstChar(event.keyCode, this);" onkeydown="return CheckFirstChar(event.keyCode, this);" onkeypress="javascript:return isNumber(event)" runat="server" placeholder="Enter Quantity" CssClass="form-control" TabIndex="27"></asp:TextBox>

                                                                    </div>


                                                                </div>
                                                            </div>
                                                            <div class="row" style="margin: 9px 0 9px 0">
                                                                <div class="col-lg-12">
                                                                    <div class="col-md-1">
                                                                        <asp:Label ID="Label00" runat="server" Text="Vendor ID"></asp:Label>
                                                                    </div>
                                                                    <div class="col-lg-3">
                                                                        <asp:TextBox ID="txtVendorID" Width="200px" Placeholder="Enter Vendor ID" onkeyup="CheckFirstChar(event.keyCode, this);" OnTextChanged="txtVendorID_TextChanged" onkeydown="return CheckFirstChar(event.keyCode, this);" onkeypress="javascript:return isNumber(event)" runat="server" CssClass="form-control" TabIndex="4" AutoPostBack="true"></asp:TextBox>
                                                                    </div>

                                                                    <div class="col-md-1">
                                                                        <asp:Label ID="Label5" runat="server" Width="120px" Text="Vendor Name"></asp:Label>
                                                                    </div>
                                                                    <div class="col-lg-3">
                                                                        <asp:TextBox ID="txtVendorName" Placeholder="Enter Vendor Name" onkeyup="CheckFirstChar(event.keyCode, this);" OnTextChanged="txtVendorName_TextChanged" onkeydown="return CheckFirstChar(event.keyCode, this);" onkeypress="javascript:return OnltAlphabets(event)" runat="server" CssClass="form-control" TabIndex="4" AutoPostBack="true"></asp:TextBox>
                                                                        <div id="CustList2" style="height: 200px; overflow-y: scroll;"></div>
                                                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtVendorName"
                                                                            UseContextKey="true"
                                                                            CompletionInterval="1"
                                                                            CompletionSetCount="20"
                                                                            MinimumPrefixLength="1"
                                                                            EnableCaching="true"
                                                                            ServicePath="~/WebServices/Contact.asmx"
                                                                            ServiceMethod="GetVendorName"
                                                                            CompletionListElementID="CustList2">
                                                                        </asp:AutoCompleteExtender>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row" style="margin: 9px 0 9px 0">
                                                                <div class="col-lg-12">
                                                                    <div class="col-lg-1">
                                                                        <label class="control-label ">Product ID.</label>
                                                                    </div>
                                                                    <div class="col-md-3">
                                                                        <asp:TextBox ID="txtProductID" Width="200px" CssClass="form-control"  onkeypress="javascript:return isNumber (event)" runat="server" placeholder="Enter Product Id" AutoPostBack="true" TabIndex="1" OnTextChanged="txtProductID_TextChanged"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-md-1">

                                                                        <asp:Label ID="Label4" Width="120px" runat="server" Text="Product Name"></asp:Label>
                                                                    </div>
                                                                    <div class="col-lg-3">
                                                                        <asp:TextBox ID="txtProductName" runat="server" AutoPostBack="true" OnTextChanged="txtProductName_TextChanged" onkeyup="CheckFirstChar(event.keyCode, this);" onkeydown="return CheckFirstChar(event.keyCode, this);" placeholder="Enter Product Name" onkeypress="javascript:return OnltAlphabets(event)" CssClass="form-control" TabIndex="2"></asp:TextBox>
                                                                        <div id="CustList" style="height: 200px; overflow-y: scroll;"></div>
                                                                        <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="txtProductName"
                                                                            UseContextKey="true"
                                                                            CompletionInterval="1"
                                                                            CompletionSetCount="20"
                                                                            MinimumPrefixLength="1"
                                                                            EnableCaching="true"
                                                                            ServicePath="~/WebServices/Contact.asmx"
                                                                            ServiceMethod="GetProductName"
                                                                            CompletionListElementID="CustList">
                                                                        </asp:AutoCompleteExtender>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            
                                                            <div class="row" style="margin: 9px 0 9px 0">
                                                                <div class="col-lg-12">
                                                                    <div class="col-md-1">
                                                                        <asp:Label ID="Label1" Width="120px" Text="Quantity" runat="server">  </asp:Label>
                                                                    </div>
                                                                    <div class="col-md-3">
                                                                        <asp:TextBox ID="txtQuantity" AutoPostBack="true" Width="200px" MaxLength="10" OnTextChanged="txtQuantity_TextChanged" onkeyup="CheckFirstChar(event.keyCode, this);" onkeydown="return CheckFirstChar(event.keyCode, this);" onkeypress="javascript:return isNumber(event)" runat="server" placeholder="Enter Quantity" CssClass="form-control" TabIndex="27"></asp:TextBox>

                                                                    </div>
                                                                    <div class=" col-md-1">
                                                                        <label class="control-label">RATE  </label>
                                                                    </div>

                                                                    <div class=" col-md-3">
                                                                        <asp:TextBox ID="txtRate" runat="server" Enabled="false" CssClass="form-control" onkeyup="CheckFirstChar(event.keyCode, this);" onkeydown="return CheckFirstChar(event.keyCode, this);" TabIndex="27"></asp:TextBox>
                                                                    </div>

                                                                </div>
                                                            </div>


                                                            <div class="row" style="margin: 9px 0 9px 0">
                                                                <div class="col-lg-12">
                                                                    <div class="col-md-1">
                                                                        <asp:Label ID="Label6" Width="120px" Text="SGST %" runat="server">  </asp:Label>
                                                                    </div>
                                                                    <div class="col-md-3">
                                                                        <asp:TextBox ID="txtSGSTPercent" Width="200px" MaxLength="10" Enabled="false" onkeyup="CheckFirstChar(event.keyCode, this);" onkeydown="return CheckFirstChar(event.keyCode, this);" onkeypress="javascript:return isNumber(event)" runat="server" placeholder="Enter Quantity" CssClass="form-control" TabIndex="27"></asp:TextBox>

                                                                    </div>
                                                                    <div class=" col-md-1">

                                                                        <asp:Label ID="Label2" Width="120px" Text="SGST Amount " runat="server">  </asp:Label>
                                                                    </div>

                                                                    <div class=" col-md-3">
                                                                        <asp:TextBox ID="txtSGSTAmount" runat="server" Enabled="false" CssClass="form-control" onkeyup="CheckFirstChar(event.keyCode, this);" onkeydown="return CheckFirstChar(event.keyCode, this);" TabIndex="27"></asp:TextBox>
                                                                    </div>

                                                                </div>
                                                            </div>


                                                            <div class="row" style="margin: 9px 0 9px 0">
                                                                <div class="col-lg-12">
                                                                    <div class="col-md-1">
                                                                        <asp:Label ID="Label7" Width="120px" Text="CGST %" runat="server">  </asp:Label>
                                                                    </div>
                                                                    <div class="col-md-3">
                                                                        <asp:TextBox ID="txtCGSTPercent" Width="200px" Enabled="false" MaxLength="10" onkeyup="CheckFirstChar(event.keyCode, this);" onkeydown="return CheckFirstChar(event.keyCode, this);" onkeypress="javascript:return isNumber(event)" runat="server" placeholder="Enter Quantity" CssClass="form-control" TabIndex="27"></asp:TextBox>

                                                                    </div>
                                                                    <div class=" col-md-1">

                                                                        <asp:Label ID="Label3" Width="120px" Text="CGST Amount" runat="server">  </asp:Label>
                                                                    </div>

                                                                    <div class=" col-md-3">
                                                                        <asp:TextBox ID="txtCGSTAmount" runat="server" Enabled="false" CssClass="form-control" onkeyup="CheckFirstChar(event.keyCode, this);" onkeydown="return CheckFirstChar(event.keyCode, this);" TabIndex="27"></asp:TextBox>
                                                                    </div>

                                                                </div>
                                                            </div>



                                                            <div class="row" style="margin: 9px 0 9px 0">
                                                                <div class="col-lg-12">
                                                                    <div class="col-md-1">
                                                                        <asp:Label ID="Label8" Width="120px" Text="Total Amount" runat="server">  </asp:Label>
                                                                    </div>
                                                                    <div class="col-md-3">
                                                                        <asp:TextBox ID="txtTotalAmount" Enabled="false" Width="200px" MaxLength="10" onkeyup="CheckFirstChar(event.keyCode, this);" onkeydown="return CheckFirstChar(event.keyCode, this);" onkeypress="javascript:return isNumber(event)" runat="server" placeholder="Enter Quantity" CssClass="form-control" TabIndex="27"></asp:TextBox>

                                                                    </div>


                                                                </div>
                                                            </div>

                                                            <div class="form-actions">
                                                                <div class="row">

                                                                    <div class="col-md-offset-3 col-md-9">
                                                                        <asp:Button ID="BtnVenderMasterAdd" runat="server" OnClick="BtnVenderMasterAdd_Click" Text="Save" CssClass="btn btn-primary" OnClientClick="Javascript:return IsValide();CheckForFutureDate();IsEmailAddress(txtemailid);" TabIndex="49" />
                                                                        <%--<asp:Button ID="BtnVenderMasterMod" runat="server" Text="Modify" CssClass="btn btn-primary" OnClick="BtnVenderMasterMod_Click" OnClientClick="Javascript:return IsValide();CheckForFutureDate();IsEmailAddress(txtemailid);" TabIndex="49" />
                                                                        --%><asp:Button ID="Button2" runat="server" Text="ClearAll" CssClass="btn btn-primary" TabIndex="50" />
                                                                        <asp:Button ID="BTNEXIT1" runat="server" Text="Go To Main Menu" CssClass="btn btn-primary" TabIndex="51" OnClick="BTNEXIT1_Click" />
                                                                        <asp:Button ID="BtnDelete2" runat="server" Text="Delete" CssClass="btn btn-primary" Visible="false" TabIndex="51" />
                                                                        <asp:Button ID="BtnCancle" runat="server" Text="Cancel" CssClass="btn btn-primary" Visible="true" TabIndex="51" OnClick="BtnCancle_Click" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <br />

                                                        <br />

                                                        <br />


                                                        <br />
                                                        <div class="row" id="Div_grid" runat="server">
                                                            <div class="col-md-12">
                                                                <div class="table-scrollable" style="width: 100%; height: 500px; overflow-x: auto; overflow-y: auto">
                                                                    <table class="table table-striped table-bordered table-hover">
                                                                        <thead>
                                                                            <tr>
                                                                                <th>
                                                                                  <%--  <asp:GridView ID="GrdStock" runat="server" CellPadding="6" CellSpacing="7"
                                                                                            ForeColor="#333333" OnPageIndexChanging="GrdStock_PageIndexChanging" OnRowCommand="GrdStock_RowCommand"
                                                                                            PageIndex="15" AutoGenerateColumns="False" CssClass="mGrid" BorderWidth="1px" OnSelectedIndexChanged="GrdStock_SelectedIndexChanged"
                                                                                            BorderColor="#333300" Width="100%" AllowPaging="True"
                                                                                            ShowFooter="true">--%>
                                                                                         <asp:GridView ID="GrdStock" runat="server" CellPadding="6" CellSpacing="7"
                                                                                         ForeColor="#333333" OnPageIndexChanged="GrdStock_SelectedIndexChanged" OnPageIndexChanging="GrdStock_PageIndexChanging"
                                                                                                AllowPaging="True" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" EditRowStyle-BackColor="#FFFF99"
                                                                                                PageIndex="10" PageSize="10" PagerStyle-CssClass="pgr" CssClass="mGrid" BorderWidth="1px" BorderColor="#333300" Width="100%"
                                                                                                ShowFooter="true">
                                                                                            <AlternatingRowStyle BackColor="White" />
                                                                                            <Columns>
                                                                                                 <asp:TemplateField HeaderText="BRCD" Visible="true">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="BRCD" runat="server" Text='<%# Eval("BRCD") %>' ></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                                
                                                                                                <asp:TemplateField HeaderText="VENDOR ID" Visible="true">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="VENDORID" runat="server"  Text='<%# Eval("VENDORID") %>'></asp:Label>
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
                                                                                                    <asp:Label ID="PRODNAME" runat="server" Text='<%# Eval("UNITCOST") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                                 <asp:TemplateField HeaderText="Quantity" Visible="true">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="QTY" runat="server" Text='<%# Eval("QTY ") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="RATE" Visible="true">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="RATE" runat="server" Text='<%# Eval("UNITCOST ") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                               
                                                                                                <asp:TemplateField HeaderText="GST" Visible="true">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="GSTAMT" runat="server" Text='<%# Eval("SGSTAMT ") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="AMOUNT" Visible="true">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="TOTALAMT" runat="server" Text='<%# Eval("AMOUNT") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="DATE" Visible="true">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="ENTRYDATE" runat="server" Text='<%# Eval("ENTRYDATE") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="STATUS" Visible="true">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="STATUS" runat="server" Text='<%# Eval("STATUS") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                  <asp:TemplateField HeaderText="LOGINCODE" Visible="true">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="LOGINCODE" runat="server" Text='<%# Eval("LOGINCODE ") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>

                                                                                              <%--  <asp:TemplateField HeaderText="Authorize" Visible="true">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:LinkButton ID="LnkAutorise" runat="server" CommandArgument='<%#Eval("BRCD")+","+ Eval("PRODID")+","+Eval("VENDORID")%>' CommandName="Select" OnClick="LnkAutorise_Click" class="glyphicon glyphicon-pencil"></asp:LinkButton>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>--%>



                                                                                                <asp:TemplateField HeaderText="Veiw" Visible="true">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:LinkButton ID="lnkView" runat="server" CommandArgument='<%#Eval("BRCD")+","+ Eval("PRODID")+","+Eval("VENDORID")%>' CommandName="select" OnClick="lnkView_Click" class="glyphicon glyphicon-pencil"></asp:LinkButton>
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
        </div>
    </div>
</asp:Content>

