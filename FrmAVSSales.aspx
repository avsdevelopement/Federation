<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAVSSales.aspx.cs" Inherits="FrmAVSSales" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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

            window.location = "FrmAVSSales.aspx";
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="portlet box blue" id="form_wizard_1">
                        <div class="portlet-title">
                            <div class="caption">
                                Sales Master Details
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
                                                        <%--   <li>
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
                                                                        <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Sales Master Detail's : </strong></div>
                                                                    </div>
                                                                </div>
                                                                <div class="row" style="margin: 7px 0 7px 0">
                                                                    <div class="col-lg-12">
                                                                        <div class="col-md-2">
                                                                            <asp:Label ID="Label1" runat="server" Width="120px" Text="Member NO"></asp:Label>
                                                                        </div>
                                                                        <div class="col-md-2">
                                                                            <asp:TextBox ID="txtmemno" runat="server" AutoPostBack="true" OnTextChanged="txtmemno_TextChanged" placeholder="Member NO" Enabled="true" CssClass="form-control" TabIndex="1" onkeypress="javascript:return isNumber (event)"></asp:TextBox>

                                                                        </div>
                                                                        <div class=" col-md-2">
                                                                            <label class="control-label">Member Name</label>
                                                                        </div>

                                                                        <div class=" col-md-3">
                                                                            <asp:TextBox ID="txtmembername" runat="server" placeholder="Enetr Member Name" AutoPostBack="true" Enabled="true" CssClass="form-control" TabIndex="2"></asp:TextBox>
                                                                            <%-- <div id="CustList" style="height: 200px; overflow-y: scroll;"></div>
                                                                           <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="txtBrcdName"
                                                                                UseContextKey="true"
                                                                                CompletionInterval="1"
                                                                                CompletionSetCount="20"
                                                                                MinimumPrefixLength="1"
                                                                                EnableCaching="true"
                                                                                ServicePath="~/WebServices/Contact.asmx"
                                                                                ServiceMethod="GetBranch"
                                                                                CompletionListElementID="CustList">
                                                                            </asp:AutoCompleteExtender>--%>
                                                                        </div>
                                                                        <div class="col-md-1">
                                                                            <asp:TextBox ID="txtIssueId" Visible="true" placeholder="Issue ID" CssClass="form-control" onkeypress="javascript:return isNumber (event)" OnTextChanged="txtIssueId_TextChanged" AutoPostBack="true" runat="server" Enabled="true" TabIndex="1"></asp:TextBox>
                                                                        </div>
                                                                         <div class="col-lg-2">
                                                                    <asp:Button ID="BtnUpdateGST" runat="server" Text="ADDGST_No" CssClass="btn btn-primary" OnClick="BtnUpdateGST_Click"  TabIndex="11" />
                                                                   </div>

                                                                    </div>

                                                                </div>
                                                                <div class="row" style="margin: 7px 0 7px 0">
                                                                    <div class="col-lg-12">
                                                                        <div class="col-md-2">
                                                                            <label class="control-label">Non Member Name</label>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <asp:TextBox ID="txtnonmem" runat="server" placeholder="Non Member" Enabled="true" CssClass="form-control" AutoPostBack="true" TabIndex="3" OnTextChanged="txtnonmem_TextChanged" ></asp:TextBox>

                                                                        </div>
                                                                        
                                                                        <div >
                                                                            <asp:TextBox ID="txtnonmember" Visible="false" runat="server" Width="250px" placeholder="Enter Non Member Name" CssClass="form-control" TabIndex="4"></asp:TextBox>
                                                                            <%-- <div id="CustList1" style="height: 200px; overflow-y: scroll;"></div>
                                                                          <asp:AutoCompleteExtender ID="AutoBrcd" runat="server" TargetControlID="txtTBrcdName"
                                                                                UseContextKey="true"
                                                                                CompletionInterval="1"
                                                                                CompletionSetCount="20"
                                                                                MinimumPrefixLength="1"
                                                                                EnableCaching="true"
                                                                                ServicePath="~/WebServices/Contact.asmx"
                                                                                ServiceMethod="GetBranch"
                                                                                CompletionListElementID="CustList1">
                                                                            </asp:AutoCompleteExtender>--%>
                                                                        </div>
                                                                        <div class="col-md-1">
                                                                            <label class="control-label ">Mobile No</label>
                                                                        </div>
                                                                        <div class="col-md-2">
                                                                            <asp:TextBox ID="txtmob" MaxLength="10" placeholder="Enter Mobile No" CssClass="form-control" onkeypress="javascript:return isNumber (event)" runat="server" Enabled="true" TabIndex="5"></asp:TextBox>
                                                                        </div>
                                                                         <div class="col-md-1">
                                                                        <asp:Label ID="lblgst" Visible="false" Width="120px" runat="server" Text="GST No."></asp:Label>
                                                                    </div>
                                                                    <div class="col-lg-2">
                                                                        <asp:TextBox ID="txtGSTNO" Visible="false" Placeholder="Enter GST No." onkeyup="CheckFirstChar(event.keyCode, this);" onkeydown="return CheckFirstChar(event.keyCode, this);"  runat="server" CssClass="form-control" TabIndex="3"  AutoPostBack="true"></asp:TextBox>
                                                                    </div>

                                                                    </div>
                                                                </div>

                                                                <div class="row" style="margin: 7px 0 7px 0">
                                                                    <div class="col-lg-12">
                                                                        
                                                                        <div class="col-md-2">
                                                                            <asp:Label ID="Label2" runat="server"  Width="120px"></asp:Label>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <asp:TextBox ID="txtemail" Visible="false" runat="server" placeholder="Enter Email Id" AutoPostBack="False" Enabled="true" CssClass="form-control" TabIndex="6"></asp:TextBox>

                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div style="border: 1px solid #3598dc">
                                                                    <div class="portlet-body">
                                                                        <div class="row">
                                                                            <div class="col-lg-12">
                                                                                <div class="col-md-10" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Issue </strong></div>
                                                                                <div class="col-lg-2 right-align">
                                                                                    <asp:Button ID="btnAddNewRow" CssClass="btn btn-primary addnewbtn" OnClick="btnAddNewRow_Click" runat="server" Text="Add New Row" onkeydown="EnterToTab()" OnClientClick="javascript:return Confirm();" />
                                                                                </div>
                                                                            </div>
                                                                        </div>


                                                                        <br />
                                                                        <div id="divInsert" runat="server">
                                                                            <asp:GridView ID="grdInsert"  runat="server" AutoGenerateColumns="false" class="noborder fullwidth" Width="100%"
                                                                               ForeColor="#333333" AllowPaging="True" AlternatingRowStyle-CssClass="alt" EditRowStyle-BackColor="#FFFF99"
                                                                                                PageSize="10" PagerStyle-CssClass="pgr" CssClass="mGrid" BorderWidth="1px" BorderColor="#333300" ShowFooter="true">
                                                                                <%-- OnPageIndexChanged="grdInsert_PageIndexChanged" OnPageIndexChanging="grdInsert_PageIndexChanging" OnRowDataBound="grdInsert_RowDataBound"--%>
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="SRNO" ItemStyle-Width="20px">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="SRNO" Enabled="false" CssClass="form-control" runat="server" Text='<%#Container.DataItemIndex+1 %>' />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>


                                                                                 
                                                                                    <asp:TemplateField HeaderText="PRODUCTID" ItemStyle-Width="100px">
                                                                                        <ItemTemplate>
                                                                                            <%--<asp:TextBox ID="Txtmemno" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="Txtmemno_TextChanged" Text='<%#Eval("Txtmemno") %>' TabIndex="9" />--%>
                                                                                            <asp:TextBox ID="PRODID" onkeypress="getCurrentIndex(this)" CssClass="form-control" OnTextChanged="PRODID_TextChanged" onkeydown="EnterToTab()" runat="server" AutoPostBack="true" Text='<%#Eval("PRODID") %>' commandArgument="<%# Container.DataItemIndex %>" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="PRODUCT NAME" ItemStyle-Width="300px">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="PRODNAME" Enabled="true" onkeypress="getCurrentIndex(this)" CssClass="form-control" OnTextChanged="PRODNAME_TextChanged" AutoPostBack="true" runat="server" Text='<%#Eval("PRODNAME") %>' />
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
                                                                                            <asp:TextBox ID="SGSTPER" CssClass="form-control" onkeypress="javascript:return isNumber(event)" Enabled="false" runat="server" Text='<%#Eval("CGSTPER") %>' />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="CGSTPER" ItemStyle-Width="80px">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="CGSTPER" Enabled="false" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server" Text='<%#Eval("SGSTPER") %>' />
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
                                                                                <%--<div class="col-md-9">
                                                                                    <%-- <asp:Label Text="Total Amount" runat="server"></asp:Label>
                                                                                    <asp:TextBox ID="txtAmount" Style="width: 106px; margin-left: 954px;" Text='<%#Eval("txtAmount") %>' Enabled="false" CssClass="clsTxtToCalculate" runat="server" />
                                                                                </div>--%>
                                                                            </div>





                                                                        </div>

                                                                    </div>



                                                                </div>
                                                                <br />
                                                                <div class="row" id="Div_grid" runat="server">
                                                                    <div class="col-md-12">
                                                                        <div class="table-scrollable" style="width: 100%; overflow-x: auto; overflow-y: auto">
                                                                            <table class="table table-striped table-bordered table-hover">
                                                                                <thead>
                                                                                    <tr>
                                                                                        <th>
                                                                                            <asp:GridView ID="GrdIssue" runat="server" CellPadding="6" CellSpacing="7"
                                                                                                ForeColor="#333333" OnPageIndexChanged="GrdIssue_PageIndexChanged" OnPageIndexChanging="GrdIssue_PageIndexChanging"
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
                                                                                                    <asp:TemplateField HeaderText="ISSUENO" Visible="true">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="ISSUENO" runat="server" Text='<%# Eval("ISSUENO") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                  <asp:TemplateField HeaderText="MEMNO" Visible="true">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="MEMNO" runat="server" Text='<%# Eval("MEMNO") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                             <%-- <asp:TemplateField HeaderText="VENDOR NAME" Visible="true">
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
                                                                                                    <%--<asp:TemplateField HeaderText="GST" Visible="true">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="GST" runat="server" Text='<%# Eval("GST ") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>--%>
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
                                                                                                            <asp:LinkButton ID="lnkView" runat="server" CommandArgument='<%#Eval("ISSUENO")%>' OnClick="lnkView_Click" CommandName="select" class="glyphicon glyphicon-pencil"></asp:LinkButton>
                                                                                                        </ItemTemplate>

                                                                                                        <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                                                                                    </asp:TemplateField>
                                                                                                      <asp:TemplateField HeaderText="Report" Visible="true">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:LinkButton ID="lnkreport" runat="server" CommandArgument='<%#Eval("ISSUENO")+"_"+Eval("MEMNO")%>' OnClick="lnkreport_Click" CommandName="reort" class="glyphicon glyphicon-pencil"></asp:LinkButton>
                                                                                                        </ItemTemplate>
                                                                                                          
                                                                                                        <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                                                                                    </asp:TemplateField>

                                                                                                </Columns>
                                                                                                <FooterStyle BackColor="#FFFF99" Font-Bold="True" ForeColor="Black" HorizontalAlign="Right" BorderStyle="None" />

                                                                                                <PagerStyle CssClass="pgr" />
                                                                                                <SelectedRowStyle BackColor="#FFFF99" />
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
                                                            <div style="border: 1px solid #3598dc">
                                                                <div class="row">
                                                                    <div class="col-lg-12">
                                                                        <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Payment Details : </strong></div>
                                                                    </div>
                                                                </div>

                                                                <div id="Div1" class="row" style="margin-top: 5px; margin-bottom: 5px" runat="server">
                                                                    <div class="col-lg-12">
                                                                        <div class="col-md-2">
                                                                            <label class="control-label">Payment Mode <span class="required">*</span></label>
                                                                        </div>
                                                                        <div class="col-md-2">
                                                                            <asp:DropDownList ID="ddlPayType" OnSelectedIndexChanged="ddlPayType_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control">
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>




                                                                <div id="Transfer" runat="server">
                                                                    <div id="Div2" class="row" style="margin-top: 5px; margin-bottom: 5px" runat="server">
                                                                        <div class="col-lg-12">
                                                                            <div class="col-md-2">
                                                                                <label class="control-label ">Product Code :<span class="required"> *</span></label>
                                                                            </div>
                                                                            <div class="col-md-2">
                                                                                <asp:TextBox ID="txtProdType1" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtProdType1_TextChanged" PlaceHolder="Product Type"></asp:TextBox>
                                                                            </div>
                                                                            <div class="col-md-4">
                                                                                <div class="input-icon">
                                                                                    <i class="fa fa-search"></i>
                                                                                    <asp:TextBox ID="txtProdName1" CssClass="form-control" PlaceHolder="Product Name" OnTextChanged="txtProdName1_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                                                                    <div id="CustList3" style="height: 200px; overflow-y: scroll;"></div>
                                                                                    <asp:AutoCompleteExtender ID="autoglname1" runat="server" TargetControlID="txtProdName1" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                                        MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList3" ServiceMethod="GetGlName">
                                                                                    </asp:AutoCompleteExtender>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div id="Div3" class="row" style="margin-top: 5px; margin-bottom: 5px" runat="server">
                                                                        <div class="col-lg-12">
                                                                            <div class="col-md-2">
                                                                                <label class="control-label ">Acc No / Name:<span class="required"> *</span></label>
                                                                            </div>
                                                                            <div class="col-md-2">
                                                                                <asp:TextBox ID="TxtAccNo1" CssClass="form-control" PlaceHolder="ID" runat="server" AutoPostBack="true" OnTextChanged="TxtAccNo1_TextChanged"></asp:TextBox>
                                                                            </div>
                                                                            <div class="col-md-4">
                                                                                <div class="input-icon">
                                                                                    <i class="fa fa-search"></i>
                                                                                    <asp:TextBox ID="TxtAccName1" CssClass="form-control" PlaceHolder="Customer Name" runat="server" AutoPostBack="true" OnTextChanged="TxtAccName1_TextChanged"></asp:TextBox>
                                                                                    <div id="CustList4" style="height: 200px; overflow-y: scroll;"></div>
                                                                                    <asp:AutoCompleteExtender ID="AutoAccname1" runat="server" TargetControlID="TxtAccName1" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                                                        MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList4" ServiceMethod="GetAccName">
                                                                                    </asp:AutoCompleteExtender>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-md-2">
                                                                                <label class="control-label "><span class="required"></span></label>
                                                                            </div>
                                                                            <div class="col-md-2">
                                                                                <asp:TextBox ID="txtBalance" Visible="false" CssClass="form-control" PlaceHolder="Balance" runat="server" Enabled="true"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div id="divIntrument" runat="server">
                                                                    <div id="Div4" class="row" style="margin-top: 5px; margin-bottom: 5px" runat="server">
                                                                        <div class="col-lg-12">
                                                                            <div class="col-md-2">
                                                                                <label class="control-label ">Instrument No:<span class="required"> *</span></label>
                                                                            </div>
                                                                            <div class="col-md-3">
                                                                                <asp:TextBox ID="TxtChequeNo" placeholder="CHEQUE NUMBER" MaxLength="6" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                                            </div>
                                                                            <div class="col-md-2">
                                                                                <label class="control-label ">Instrument Date:<span class="required"> *</span></label>
                                                                            </div>
                                                                            <div class="col-md-3">
                                                                                <asp:TextBox ID="TxtChequeDate" placeholder="CHEQUE DATE" CssClass="form-control" onkeyup="FormatIt(this)" runat="server"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div id="divNarration" runat="server">
                                                                    <div id="Div5" class="row" style="margin-top: 5px; margin-bottom: 5px" runat="server">
                                                                        <div class="col-lg-12">
                                                                            <div class="col-md-2">
                                                                                <label class="control-label ">Narration:</label>
                                                                            </div>
                                                                            <div class="col-md-6">
                                                                                <asp:TextBox ID="txtNarration" CssClass="form-control" runat="server" />
                                                                            </div>
                                                                            <div class="col-md-2">
                                                                                <label class="control-label ">Amount:</label>
                                                                            </div>
                                                                            <div class="col-md-2">
                                                                                <asp:TextBox ID="txtAmount" Enabled="true" CssClass="form-control" runat="server" />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="form-actions">
                                                                    <div class="row">

                                                                        <div class="col-md-offset-3 col-md-9">
                                                                            <asp:Button ID="BtnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" TabIndex="49" OnClick="BtnSubmit_Click" />
                                                                            <asp:Button ID="Btnclear" runat="server" Text="ClearAll" CssClass="btn btn-primary" TabIndex="50" />
                                                                            <asp:Button ID="btnExit" runat="server" Text="Go To Main Menu" CssClass="btn btn-primary" OnClick="btnExit_Click" TabIndex="51" />
                                                                            <asp:Button ID="btnCancle" runat="server" Text="Cancle" Visible="false" CssClass="btn btn-primary" TabIndex="51" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


