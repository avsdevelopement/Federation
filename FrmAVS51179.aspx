<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAVS51179.aspx.cs" Inherits="FrmAVS51179" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
      <script src="//code.jquery.com/jquery-1.11.1.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".clsTxtToCalculate").each(function () {
                $(this).keyup(function () {
                    var total = 0;
                    $(".clsTxtToCalculate").each(function () {
                        if (!isNaN(this.value) && this.value.length != 0) {
                            total += parseFloat(this.value);
                        }
                    });

                    alert(total);
                    $('#<%=txtAmount.ClientID%>').html(total.toFixed(2));


            });
        });

        });
    </script>
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

            window.location = "FrmAVS51179.aspx";
        }
    </script>
    <script type="text/javascript">
        var popup;
        function getCurrentIndex(row) {
            var rowData = row.parentNode.parentNode;
            var rowIndex = rowData.rowIndex;

            //  alert(rowIndex);
            document.getElementById("<%=hdnRowIndex.ClientID %>").value = rowIndex;

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


    <script type="text/javascript">
        $(function () {
            $('[id*=lstDoc]').multiselect({
                includeSelectAllOption: true
            });
        });
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

        function EnterToTab() {
            //  var id = ElementID.id;

            //   alert(id);    

            if (event.keyCode == 13) {
                event.keyCode = 9;

            }

            //  alert("hello");

        }

        function EnterToTab1() {


            if (event.keyCode == 13)
                // alert("hello");
                event.keyCode = 9;

            document.getElementById('#grdInsert').focus();
            //document.getElementById('grdInsert').children(0).children(1).children(0).children(0)


        }
    </script>
    <script type="text/javascript">
        $(function () {
            $('[id*=lstFruits]').multiselect
                (
                {
                    includeSelectAllOption: true
                }
            );
        });
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


            var Venderno = document.getElementById('<%=txtUseId.ClientID%>').value;
            var VenderName = document.getElementById('<%=txtFBrcd.ClientID%>').value;
            var PONO = document.getElementById('<%=txtAmount.ClientID%>').value;
            var ENDTRYDATE = document.getElementById('<%=txtEntryDate.ClientID%>').value;


            var message = '';

            if (Venderno == "") {
                message = 'Please Enter Vendor Number....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtUseId.ClientID%>').focus();
                return false;
            }

            if (VenderName == "") {
                message = 'Please Enter Vendor Name....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtFBrcd.ClientID%>').focus();
                return false;
            }

            if (PONO == "") {
                message = 'Please Enter Contact Person Name....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtAmount.ClientID%>').focus();
                return false;
            }


            if (ENDTRYDATE == "") {
                message = 'Please Enter Email Id....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtEntryDate.ClientID%>').focus();
                return false;
            }

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

       <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        Used Master Details
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
                                                 <%-- <li>
                                                    <asp:LinkButton ID="lnkModify" runat="server" Text="a" class="btn btn-default" OnClick="lnkModify_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-plus-circle"></i>Modify</asp:LinkButton>
                                                </li>--%>
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
                                                                <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Used Master Detail's : </strong></div>
                                                            </div>
                                                        </div>
                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-12">
                                                                <div class="col-md-1">
                                                                    <asp:Label ID="Label1"  runat="server" Width="120px" Text="FROM BRCD"></asp:Label>
                                                                </div>
                                                                <div class="col-md-2">
                                                                    <asp:TextBox ID="txtFBrcd" runat="server" placeholder="From BRCD" OnTextChanged="txtFBrcd_TextChanged" Enabled="false"  CssClass="form-control" TabIndex="27" onkeypress="javascript:return isNumber (event)"></asp:TextBox>

                                                                </div>
                                                                <div class=" col-md-1">
                                                                    <label class="control-label">NAME</label>
                                                                </div>

                                                                <div class=" col-md-2">
                                                                    <asp:TextBox ID="txtBrcdName" runat="server"  placeholder="Enetr Brcd Name"  OnTextChanged="txtBrcdName_TextChanged" Width="250px"  AutoPostBack="true"  Enabled="false"  CssClass="form-control" TabIndex="27" ></asp:TextBox>
                                                                <div id="CustList" style="height: 200px; overflow-y: scroll;"></div>
                                                                     <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="txtBrcdName"
                                                                        UseContextKey="true"
                                                                        CompletionInterval="1"
                                                                        CompletionSetCount="20"
                                                                        MinimumPrefixLength="1"
                                                                        EnableCaching="true"
                                                                        ServicePath="~/WebServices/Contact.asmx"
                                                                        ServiceMethod="GetBranch"
                                                                        CompletionListElementID="CustList">
                                                                    </asp:AutoCompleteExtender>
                                                                </div>
                                                              

                                                            </div>
                                                        </div>
                                                         

                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-12">
                                                                <div class="col-md-1">
                                                                    <label class="control-label ">USE ID</label>
                                                                </div>
                                                                <div class="col-md-2">
                                                                    <asp:TextBox ID="txtUseId" placeholder="Used ID" CssClass="form-control" onkeypress="javascript:return isNumber (event)" runat="server" Enabled="false" TabIndex="1"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-1">
                                                                    <asp:Label ID="Label2" runat="server" Text="Entry Date" Width="120px"></asp:Label>
                                                                </div>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="txtEntryDate" runat="server" onblur="WorkingDate()" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this)"  AutoPostBack="true" Enabled="false"  CssClass="form-control"  TabIndex="2"></asp:TextBox>
                                                                   
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div style="border: 1px solid #3598dc">
                                                            <div class="portlet-body">
                                                                <div class="row">
                                                                    <div class="col-lg-12">
                                                                        <div class="col-md-10" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Used</strong></div>
                                                                        <div class="col-lg-2 right-align">
                                                                            <asp:Button ID="btnAddNewRow" CssClass="btn btn-primary addnewbtn" OnClick="btnAddNewRow_Click" runat="server" Text="Add New Row" onkeydown="EnterToTab()" OnClientClick="javascript:return Confirm();" />
                                                                        </div>
                                                                    </div>
                                                                </div>


                                                                <br />
                                                                <div id="divInsert" runat="server">
                                                                    <asp:GridView ID="grdInsert" runat="server" AutoGenerateColumns="false" class="noborder fullwidth" Width="100%">
                                                                        <%-- OnPageIndexChanged="grdInsert_PageIndexChanged" OnPageIndexChanging="grdInsert_PageIndexChanging" OnRowDataBound="grdInsert_RowDataBound"--%>
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="SRNO" ItemStyle-Width="20px">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="SRNO" Enabled="false" CssClass="form-control" runat="server" Text='<%#Container.DataItemIndex+1 %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="VENDORID" ItemStyle-Width="100px">
                                                                                <ItemTemplate>
                                                                                    <%--<asp:TextBox ID="Txtmemno" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="Txtmemno_TextChanged" Text='<%#Eval("Txtmemno") %>' TabIndex="9" />--%>
                                                                                    <asp:TextBox ID="VENDORID" onkeypress="getCurrentIndex(this)" CssClass="form-control" onkeydown="EnterToTab()" runat="server" AutoPostBack="true" Text='<%#Eval("VENDORID") %>' commandArgument="<%# Container.DataItemIndex %>" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="VENDOR NAME" ItemStyle-Width="300px">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="VENDERNAME" Enabled="true" onkeypress="getCurrentIndex(this)" OnTextChanged="VENDERNAME_TextChanged" CssClass="form-control" AutoPostBack="true" runat="server" Text='<%#Eval("VENDERNAME") %>' />
                                                                                    <asp:AutoCompleteExtender ID="Autoglname" runat="server" TargetControlID="VENDERNAME"
                                                                                        UseContextKey="true"
                                                                                        CompletionInterval="1"
                                                                                        CompletionSetCount="20"
                                                                                        MinimumPrefixLength="1"
                                                                                        EnableCaching="true"
                                                                                        ServicePath="~/WebServices/Contact.asmx"
                                                                                        ServiceMethod="GetVendorName">
                                                                                    </asp:AutoCompleteExtender>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="PRODUCTID" ItemStyle-Width="100px">
                                                                                <ItemTemplate>
                                                                                    <%--<asp:TextBox ID="Txtmemno" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="Txtmemno_TextChanged" Text='<%#Eval("Txtmemno") %>' TabIndex="9" />--%>
                                                                                    <asp:TextBox ID="PRODID" onkeypress="getCurrentIndex(this)" CssClass="form-control" onkeydown="EnterToTab()" runat="server" AutoPostBack="true"  Text='<%#Eval("PRODID") %>' commandArgument="<%# Container.DataItemIndex %>" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="PRODUCT NAME" ItemStyle-Width="300px">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="PRODNAME" Enabled="true" onkeypress="getCurrentIndex(this)" CssClass="form-control" OnTextChanged="PRODNAME_TextChanged"  AutoPostBack="true" runat="server" Text='<%#Eval("PRODNAME") %>' />
                                                                                    <asp:AutoCompleteExtender ID="autoprname1" runat="server" TargetControlID="PRODNAME"
                                                                                        UseContextKey="true"
                                                                                        CompletionInterval="1"
                                                                                        CompletionSetCount="20"
                                                                                        MinimumPrefixLength="1"
                                                                                        EnableCaching="true"
                                                                                        ServicePath="~/WebServices/Contact.asmx"
                                                                                        ServiceMethod="GetProductName">
                                                                                    </asp:AutoCompleteExtender>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="QUANTITY" ItemStyle-Width="100px">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="QTY" CssClass="form-control" onkeypress="javascript:return isNumber(event)" AutoPostBack="true" OnTextChanged="QTY_TextChanged" runat="server" Text='<%#Eval("QTY") %>' />

                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="RATE" ItemStyle-Width="100px">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="UNITCOST" CssClass="form-control" onkeypress="javascript:return isNumber(event)" runat="server" Enabled="false" Text='<%#Eval("UNITCOST") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                             <%--<asp:TemplateField HeaderText="GST" ItemStyle-Width="100px">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="GST" CssClass="form-control" onkeypress="javascript:return isNumber(event)" Enabled="false" runat="server" Text='<%#Eval("GST") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>--%>
                                                                              <asp:TemplateField HeaderText="SGSTPER" ItemStyle-Width="100px">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="SGSTPER" CssClass="form-control" onkeypress="javascript:return isNumber(event)" Enabled="false" runat="server" Text='<%#Eval("SGSTPER") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="CGSTPER" ItemStyle-Width="80px">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="CGSTPER" Enabled="false" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server" Text='<%#Eval("CGSTPER") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="SGSTAMT" ItemStyle-Width="100px">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="SGSTAMT" CssClass="form-control" onkeypress="javascript:return isNumber(event)" Enabled="false" runat="server" Text='<%#Eval("SGSTAMT") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="CGSTAMT" ItemStyle-Width="80px">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="CGSTAMT" Enabled="false" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server" Text='<%#Eval("CGSTAMT") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="AMOUNT" ItemStyle-Width="100px">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="AMOUNT" Enabled="false" CssClass="form-control" runat="server" onkeyup="ready()" Text='<%#Eval("AMOUNT") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                        </Columns>
                                                                    </asp:GridView>

                                                                    <asp:HiddenField ID="hdnRowIndex" runat="server" />
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="col-md-9">
                                                                            <%-- <asp:Label Text="Total Amount" runat="server"></asp:Label>--%>
                                                                            <asp:TextBox ID="txtAmount" Style="width: 106px; margin-left: 954px;" Text='<%#Eval("txtAmount") %>' Enabled="false" CssClass="clsTxtToCalculate" runat="server"  />
                                                                        </div>
                                                                    </div>
                                                                  
                                                                </div>

                                                            </div>


                                                            <div class="form-actions">
                                                                <div class="row">

                                                                    <div class="col-md-offset-3 col-md-9">
                                                                        <asp:Button ID="BtnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary"   TabIndex="49" OnClick="BtnSubmit_Click" />
                                                                        <asp:Button ID="Btnclear" runat="server" Text="ClearAll" CssClass="btn btn-primary" TabIndex="50" />
                                                                        <asp:Button ID="btnExit" runat="server" Text="Go To Main Menu" CssClass="btn btn-primary" OnClick="btnExit_Click"  TabIndex="51" />
                                                                        <asp:Button ID="btnCancle" runat="server" Text="Cancle" Visible="false" CssClass="btn btn-primary" TabIndex="51" />
                                                                    </div>
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
                                                                                 <%--   <asp:GridView ID="GrdUse" runat="server" CellPadding="6" CellSpacing="7"
                                                                                        ForeColor="#333333" OnPageIndexChanged="GrdUse_PageIndexChanged" OnPageIndexChanging="GrdUse_PageIndexChanging"
                                                                                        PageIndex="10" PageSize="2" AutoGenerateColumns="False" CssClass="mGrid" BorderWidth="1px" 
                                                                                        BorderColor="#333300" Width="100%" AllowPaging="True"
                                                                                        ShowFooter="true">--%>
                                                                                          <asp:GridView ID="GrdUse" runat="server" CellPadding="6" CellSpacing="7"
                                                                                         ForeColor="#333333" OnPageIndexChanged="GrdUse_PageIndexChanged" OnPageIndexChanging="GrdUse_PageIndexChanging"
                                                                                                AllowPaging="True" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" EditRowStyle-BackColor="#FFFF99"
                                                                                                PageIndex="10" PageSize="10" PagerStyle-CssClass="pgr" CssClass="mGrid" BorderWidth="1px" BorderColor="#333300" Width="100%"
                                                                                                ShowFooter="true">
                                                                                        <AlternatingRowStyle BackColor="White" />
                                                                                        <Columns>
                                                                                            <%-- <asp:TemplateField HeaderText="SRNO" Visible="true">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="SRNO" runat="server" Text='<%# Eval("SRNO") %>' ></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>--%>
                                                                                           <asp:TemplateField HeaderText="USENO" Visible="true">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="USENO" runat="server" Text='<%# Eval("USEDNO") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <%--<asp:TemplateField HeaderText="VENDOR ID" Visible="true">
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
                                                                                                    <asp:Label ID="PRODNAME" runat="server" Text='<%# Eval("UNITCOST") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>--%>
                                                                                            <asp:TemplateField HeaderText="RATE" Visible="true">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="RATE" runat="server" Text='<%# Eval("QTY ") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        
                                                                                           <asp:TemplateField HeaderText="GSTAMT" Visible="true">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="GSTAMT" runat="server" Text='<%# Eval("GSTAMT ") %>'></asp:Label>
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
                                                                                                    <asp:Label ID="LOGINCODE" runat="server" Text='<%# Eval("LOGINCODE") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>

                                                                                       <%--    <asp:TemplateField HeaderText="Authorize" Visible="true">
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton ID="LnkAutorise" runat="server" CommandArgument='<%#Eval("ISSUENO")%>' CommandName="Select"  class="glyphicon glyphicon-pencil"></asp:LinkButton>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>--%>



                                                                                           <asp:TemplateField HeaderText="View" Visible="true">
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton ID="lnkView" runat="server" CommandArgument='<%#Eval("USEDNO")%>' OnClick="lnkView_Click" CommandName="select"  class="glyphicon glyphicon-pencil"></asp:LinkButton>
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

