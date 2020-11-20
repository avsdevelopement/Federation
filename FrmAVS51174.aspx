<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAVS51174.aspx.cs" Inherits="FrmAVS51174" %>

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

            window.location = "FrmAVS51174.aspx";
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


            var Venderno = document.getElementById('<%=TxtVenderno.ClientID%>').value;
            var VenderName = document.getElementById('<%=TxtVenderName.ClientID%>').value;
            var ComName = document.getElementById('<%=txtComName.ClientID%>').value;
            var MobileNumber = document.getElementById('<%=txtMobileNumber.ClientID%>').value;
            var EmailId = document.getElementById('<%=txtEmailId.ClientID%>').value;
            var AddLin1 = document.getElementById('<%=txtAddline1.ClientID%>').value;
            var AddLin2 = document.getElementById('<%=txtAddline2.ClientID%>').value;

            var ParmCity = document.getElementById('<%=txtParmCity.ClientID%>').value;
            var Pin = document.getElementById('<%=txtPin.ClientID%>').value;
            var State = document.getElementById('<%=ddlParmState.ClientID%>').value;
            var message = '';

            if (Venderno == "") {
                message = 'Please Enter Vendor Number....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtVenderno.ClientID%>').focus();
                return false;
            }

            if (VenderName == "") {
                message = 'Please Enter Vendor Name....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtVenderName.ClientID%>').focus();
                        return false;
                    }

                    if (ComName == "") {
                        message = 'Please Enter Contact Person Name....!!\n';
                        $('#alertModal').find('.modal-body p').text(message);
                        $('#alertModal').modal('show')
                        document.getElementById('<%=txtComName.ClientID%>').focus();
                        return false;
                    }

                    if (MobileNumber == "") {
                        message = 'Please Enter Mobile No....!!\n';
                        $('#alertModal').find('.modal-body p').text(message);
                        $('#alertModal').modal('show')
                        document.getElementById('<%=txtMobileNumber.ClientID%>').focus();
                        return false;
                    }

                    if (EmailId == "") {
                        message = 'Please Enter Email Id....!!\n';
                        $('#alertModal').find('.modal-body p').text(message);
                        $('#alertModal').modal('show')
                        document.getElementById('<%=txtEmailId.ClientID%>').focus();
                        return false;
                    }
                    if (AddLin1 == "") {
                        message = 'Please Enter Address Line 1....!!\n';
                        $('#alertModal').find('.modal-body p').text(message);
                        $('#alertModal').modal('show')
                        document.getElementById('<%=txtAddline1.ClientID%>').focus();
                        return false;
                    }
                    if (AddLin2 == "") {
                        message = 'Please Enter Address Line 2...!!\n';
                        $('#alertModal').find('.modal-body p').text(message);
                        $('#alertModal').modal('show')
                        document.getElementById('<%=txtAddline2.ClientID%>').focus();
                        return false;
                    }
                    if (State == "0") {
                        message = 'Please Select State....!!\n';
                        $('#alertModal').find('.modal-body p').text(message);
                        $('#alertModal').modal('show')
                        document.getElementById('<%=ddlParmState.ClientID%>').focus();
                        return false;
                    }
                    if (ParmCity == "") {
                        message = 'Please Enter City....!!\n';
                        $('#alertModal').find('.modal-body p').text(message);
                        $('#alertModal').modal('show')
                        document.getElementById('<%=txtParmCity.ClientID%>').focus();
                        return false;
                    }
                    if (Pin == "") {
                        message = 'Please Enter Pin Code Number....!!\n';
                        $('#alertModal').find('.modal-body p').text(message);
                        $('#alertModal').modal('show')
                        document.getElementById('<%=txtPin.ClientID%>').focus();
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
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        Vendor Master Details
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
                                                                    <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Personal Detail's : </strong></div>
                                                                </div>
                                                            </div>
                                                            <div></div>
                                                            <br />
                                                            <div class="row" style="margin: 9px 0 9px 0">
                                                                <div class="col-lg-12">
                                                                    <div class="col-lg-1">
                                                                        <label class="control-label ">Vendor ID.</label>
                                                                    </div>
                                                                    <div class="col-md-3">
                                                                        <asp:TextBox ID="TxtVenderno" Width="200px" onkeyup="CheckFirstChar(event.keyCode, this);" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" onkeypress="javascript:return isNumber (event)" runat="server" OnTextChanged="TxtVenderno_TextChanged" AutoPostBack="true" TabIndex="1"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-md-1">

                                                                        <asp:Label ID="Label4" Width="120px" runat="server" Text="Vendor Name"></asp:Label>
                                                                    </div>
                                                                    <div class="col-lg-3">
                                                                        <asp:TextBox ID="TxtVenderName" runat="server" onkeyup="CheckFirstChar(event.keyCode, this);" onkeydown="return CheckFirstChar(event.keyCode, this);" AutoPostBack="true" placeholder="Enter Vendor Name" onkeypress="javascript:return OnltAlphabets(event)" CssClass="form-control" OnTextChanged="TxtVenderName_TextChanged" TabIndex="2"></asp:TextBox>
                                                                        <div id="CustList" style="height: 200px; overflow-y: scroll;"></div>
                                                                        <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="TxtVenderName"
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

                                                                     <div class="col-lg-3">
                                                                    <asp:Button ID="BtnUpdateGST" Visible="false" runat="server" Text="UpdateGST_No" CssClass="btn btn-primary" OnClick="BtnUpdateGST_Click"  TabIndex="11" />
                                                                   </div>
                                                                </div>
                                                            </div>

                                                            <div class="row" style="margin: 9px 0 9px 0">
                                                                <div class="col-lg-12">
                                                                    <div class="col-md-1">
                                                                        <asp:Label ID="Label00" Width="120px" runat="server" Text="Person Name"></asp:Label>
                                                                    </div>
                                                                    <div class="col-lg-4">
                                                                        <asp:TextBox ID="txtComName" Placeholder="Enter Contact Person Name" onkeyup="CheckFirstChar(event.keyCode, this);" onkeydown="return CheckFirstChar(event.keyCode, this);" onkeypress="javascript:return OnltAlphabets(event)" runat="server" CssClass="form-control" TabIndex="3" OnTextChanged="txtComName_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                    </div>
                                                                     <div class="col-md-1">
                                                                        <asp:Label ID="lblgst" Visible="false" Width="120px" runat="server" Text="GST No."></asp:Label>
                                                                    </div>
                                                                    <div class="col-lg-2">
                                                                        <asp:TextBox ID="txtGSTNO" Visible="false" Placeholder="Enter GST No." onkeyup="CheckFirstChar(event.keyCode, this);" onkeydown="return CheckFirstChar(event.keyCode, this);"  runat="server" CssClass="form-control" TabIndex="3"  AutoPostBack="true"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row" style="margin: 9px 0 9px 0">
                                                                <div class="col-lg-12">
                                                                    <div class="col-md-1">
                                                                        <asp:Label ID="Label1" Width="120px" Text="Mobile Number" runat="server">  </asp:Label>
                                                                    </div>
                                                                    <div class="col-md-3">
                                                                        <asp:TextBox ID="txtMobileNumber" Width="220px" MaxLength="10" OnTextChanged="txtMobileNumber_TextChanged" onkeyup="CheckFirstChar(event.keyCode, this);" onkeydown="return CheckFirstChar(event.keyCode, this);" onkeypress="javascript:return isNumber(event)" runat="server" placeholder="Enter Mobile Number" CssClass="form-control" TabIndex="4"></asp:TextBox>

                                                                    </div>
                                                                    <div class=" col-md-1">
                                                                        <label class="control-label">Email Id  </label>
                                                                    </div>

                                                                    <div class=" col-md-3">
                                                                        <asp:TextBox ID="txtEmailId" runat="server" placeholder="Enter Email Id" CssClass="form-control" onkeyup="CheckFirstChar(event.keyCode, this);" onkeydown="return CheckFirstChar(event.keyCode, this);" TabIndex="5"></asp:TextBox>
                                                                    </div>

                                                                </div>
                                                            </div>
                                                        </div>

                                                        <br />
                                                        <div class="row">
                                                            <div class="col-lg-12">
                                                                <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Address Detail's : </strong></div>
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <div style="border: 1px solid #3598dc">

                                                            <div id="divParAdd" runat="server">
                                                                <div style="border: 1px solid #3598dc">
                                                                    <br />
                                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                                        <div class="col-lg-12">
                                                                            <div class="col-md-1">

                                                                                <asp:Label ID="Label2" Width="120px" Text="Address Line1" runat="server">  </asp:Label>
                                                                            </div>
                                                                            <div class="col-md-3">
                                                                                <asp:TextBox ID="txtAddline1" placeholder="Enter Address  " onkeyup="FullAddr();" onkeydown="return CheckFirstChar(event.keyCode, this);" runat="server" CssClass="form-control" TabIndex="6" MaxLength="60"></asp:TextBox>
                                                                            </div>
                                                                            <div class="col-md-1">
                                                                                <asp:Label ID="Label3" Width="120px" Text="Address Line2" runat="server">  </asp:Label>
                                                                            </div>
                                                                            <div class="col-md-3">
                                                                                <asp:TextBox ID="txtAddline2" placeholder="Enter Address  " onkeyup="FullAddr();" onkeydown="return CheckFirstChar(event.keyCode, this);" runat="server" CssClass="form-control" TabIndex="7" MaxLength="60"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                                        <div class="col-lg-12">

                                                                            <div class="col-md-1">
                                                                                <label class="control-label">State </label>
                                                                            </div>
                                                                            <div class="col-md-3">
                                                                                <asp:DropDownList ID="ddlParmState" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlParmState_SelectedIndexChanged" AutoPostBack="true" EnableViewState="true" TabIndex="8"></asp:DropDownList>
                                                                            </div>

                                                                            <div class="col-md-1">
                                                                                <label class="control-label">City </label>
                                                                            </div>
                                                                            <div class="col-md-2">
                                                                                <asp:TextBox ID="txtParmCity" CssClass="form-control" onkeyup="CheckFirstChar(event.keyCode, this);" onkeydown="return CheckFirstChar(event.keyCode, this);" onkeypress="javascript:return OnltAlphabets(event)" placeholder="City" runat="server" TabIndex="9"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                                        <div class="col-lg-12">

                                                                            <div class="col-md-1">
                                                                                <label class="control-label">Pin Code </label>
                                                                            </div>

                                                                            <div class="col-md-2">
                                                                                <asp:TextBox ID="txtPin" placeholder="Enter Pin Code" onkeyup="CheckFirstChar(event.keyCode, this);" onkeydown="return CheckFirstChar(event.keyCode, this);" onkeypress="javascript:return isNumber(event)" runat="server" CssClass="form-control" TabIndex="10" MaxLength="6"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <br />

                                                        <div class="form-actions">
                                                            <div class="row">

                                                                <div class="col-md-offset-3 col-md-9">
                                                                    <asp:Button ID="BtnVenderMasterAdd" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="BtnVenderMaster_Click" OnClientClick="Javascript:return IsValide();CheckForFutureDate();IsEmailAddress(txtemailid);" TabIndex="11" />
                                                                    <%--<asp:Button ID="BtnVenderMasterMod" runat="server" Text="Modify" CssClass="btn btn-primary" OnClick="BtnVenderMasterMod_Click" OnClientClick="Javascript:return IsValide();CheckForFutureDate();IsEmailAddress(txtemailid);" TabIndex="49" />
                                                                    --%><asp:Button ID="btnClearAll" runat="server" Text="ClearAll" CssClass="btn btn-primary" TabIndex="12" onclick="btnClearAll_Click"/>
                                                                    <asp:Button ID="BTNeXIT1" runat="server" Text="Go To Main Menu" CssClass="btn btn-primary" OnClick="BTNeXIT1_Click" TabIndex="13" />
                                                                     <asp:Button ID="BtnDelete2" runat="server" Text="Delete" CssClass="btn btn-primary" Visible="false" OnClick="BtnDelete2_Click" TabIndex="14" />
                                                                     <asp:Button ID="BtnCancle" runat="server" Text="Cancel" CssClass="btn btn-primary" Visible="false" OnClick="BtnCancle_Click" TabIndex="15" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <div class="row" id="Div_grid" runat="server">
                                                            <div class="col-md-12">
                                                                <div class="table-scrollable" style="width: 100%; height: 500px; overflow-x: auto; overflow-y: auto">
                                                                    <table class="table table-striped table-bordered table-hover">
                                                                        <thead>
                                                                            <tr>
                                                                                <th>
                                                                                   <%-- <asp:GridView ID="GrdAcc" runat="server" CellPadding="6" CellSpacing="7"
                                                                                        ForeColor="#333333" OnPageIndexChanging="GrdAcc_PageIndexChanging" OnRowCommand="GrdAcc_RowCommand"
                                                                                        PageIndex="15" AutoGenerateColumns="False"  BorderWidth="1px" AutoPostback="true"
                                                                                        BorderColor="#333300" Width="100%" OnSelectedIndexChanged="GrdAcc_SelectedIndexChanged" AllowPaging="True" 
                                                                                        ShowFooter="true">
                                                                                        <AlternatingRowStyle BackColor="White" />--%>
                                                                                     <asp:GridView ID="GrdAcc" runat="server" CellPadding="6" CellSpacing="7"
                                                                                                ForeColor="#333333" OnPageIndexChanged="GrdAcc_SelectedIndexChanged" OnPageIndexChanging="GrdAcc_PageIndexChanging"
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
                                                                                            <asp:TemplateField HeaderText="MOBILE NO." Visible="true">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="MOBNO" runat="server" Text='<%# Eval("MOBNO ") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="LOGINCODE" Visible="true">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="LOGINCODE" runat="server" Text='<%# Eval("LOGINCODE") %>'></asp:Label>
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
                                                                                           <%-- <asp:TemplateField HeaderText="Authorize" Visible="true">
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton ID="LnkAutorise" runat="server"  CommandArgument='<%#Eval("VENDORID")%>' CommandName="Select"  class="glyphicon glyphicon-pencil" OnClick="LnkAutorise_Click"></asp:LinkButton>
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
                                                                                                    <asp:LinkButton ID="lnkView" runat="server" CommandArgument='<%#Eval("VENDORID")%>' OnClick="lnkView_Click" CommandName="select" class="glyphicon glyphicon-pencil"></asp:LinkButton>
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

