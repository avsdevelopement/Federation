<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAVS51176.aspx.cs" Inherits="FrmAVS51176" %>

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
                    $('#<%=LBlAmt.ClientID%>').valueOf(total.toFixed(2));


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

            window.location = "FrmAVS51176.aspx";
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
        var popup;
        function getCurrentIndex(row) {
            var rowData = row.parentNode.parentNode;
            var rowIndex = rowData.rowIndex;

            // alert(rowIndex);
            document.getElementById("<%=hdnRowIndex.ClientID %>").value = rowIndex;

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

          function Calculation(grid) {

              var grid = document.getElementById("<%= grdInsert.ClientID%>");
            var a = ["0"];
            var b = 0
            for (var i = 0; i < grid.rows.length - 1; i++) {
                var txtAmountReceive = $("input[id*=PRODID]")
                if (txtAmountReceive[i].value != '') {
                     //alert(txtAmountReceive[i].value);

                    b = a.indexOf(txtAmountReceive[i].value);
                    a.push(txtAmountReceive[i].value);

                }

            }
            //alert(b);
            if (b > 0) {
                document.getElementById("<%=hdnCrossCheck.ClientID %>").value = 0;

            }
            else {
                document.getElementById("<%=hdnCrossCheck.ClientID %>").value = 1;

            }

        }
</script>  

      <script type="text/javascript">

          function Calculation1(grid) {

              var grid = document.getElementById("<%= grdInsert.ClientID%>");
              var a = ["0"];
              var b = 0
              for (var i = 0; i < grid.rows.length - 1; i++) {
                  var txtAmountReceive = $("input[id*=PRODNAME]")
                  
                  if (txtAmountReceive[i].value != '') {
                     // alert(txtAmountReceive[i].value);

                      b = a.indexOf(txtAmountReceive[i].value);
                      a.push(txtAmountReceive[i].value);

                  }

              }
              //alert(b);
              if (b > 0) {
                  document.getElementById("<%=hdnCrossCheck.ClientID %>").value = 0;

            }
            else {
                document.getElementById("<%=hdnCrossCheck.ClientID %>").value = 1;

            }

        }
</script>  
    <script type="text/javascript">
        function isvalidateForVenMaster() {


            var Venderno = document.getElementById('<%=txtVenderID.ClientID%>').value;
            var VenderName = document.getElementById('<%=txtVendorName.ClientID%>').value;
            var PONO = document.getElementById('<%=txtPONO.ClientID%>').value;
            var ENDTRYDATE = document.getElementById('<%=txtENDTRYDATE.ClientID%>').value;


            var message = '';

            if (Venderno == "") {
                message = 'Please Enter Vendor Number....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtVenderID.ClientID%>').focus();
                return false;
            }

            if (VenderName == "") {
                message = 'Please Enter Vendor Name....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtVendorName.ClientID%>').focus();
                return false;
            }

            if (PONO == "") {
                message = 'Please Enter Contact Person Name....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtPONO.ClientID%>').focus();
                return false;
            }


            if (ENDTRYDATE == "") {
                message = 'Please Enter Email Id....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtENDTRYDATE.ClientID%>').focus();
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
                        Purchase Master Details
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
                                                <%--  <li>
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
                                                                <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Purchase Master Detail's : </strong></div>
                                                            </div>
                                                        </div>
                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-12">
                                                                <div class="col-md-1">
                                                                    <label class="control-label">PONO  </label>
                                                                </div>
                                                                <div class="col-md-2">
                                                                    <asp:TextBox ID="txtPONO" runat="server" placeholder=" Product Order Number" OnTextChanged="txtPONO_TextChanged" AutoPostBack="true" CssClass="form-control" TabIndex="27" onkeypress="javascript:return isNumber (event)"></asp:TextBox>

                                                                </div>
                                                                <div class=" col-md-2">
                                                                    <label class="control-label">Entry Date  </label>
                                                                </div>

                                                                <div class=" col-md-3">
                                                                    <asp:TextBox ID="txtENDTRYDATE" runat="server" onblur="WorkingDate()" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this)" CssClass="form-control" TabIndex="27" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                                </div>
                                                                <div class=" col-md-1">
                                                                    <label class="control-label">BRCD </label>
                                                                </div>

                                                                <div class=" col-md-2">
                                                                    <asp:TextBox ID="txtBRCD" runat="server" onblur="WorkingDate()" placeholder="BRCD" CssClass="form-control" TabIndex="27" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                                </div>

                                                            </div>
                                                        </div>

                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-12">
                                                                <div class="col-md-1">
                                                                    <label class="control-label ">Vendor ID</label>
                                                                </div>
                                                                <div class="col-md-2">
                                                                    <asp:TextBox ID="txtVenderID" placeholder="Vendor ID" CssClass="form-control" onkeypress="javascript:return isNumber (event)" runat="server" OnTextChanged="txtVenderID_TextChanged" AutoPostBack="true" TabIndex="1"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-2">
                                                                    <label class="control-label">Vendor Name</label>
                                                                </div>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="txtVendorName" runat="server" AutoPostBack="true" placeholder="Enter Vendor Name" CssClass="form-control" OnTextChanged="txtVendorName_TextChanged" TabIndex="2"></asp:TextBox>
                                                                    <%--<div id="Div2" style="height: 200px; overflow-y: scroll;"></div>--%>
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

                                                        <div style="border: 1px solid #3598dc">
                                                            <div class="portlet-body">
                                                                <div class="row">
                                                                    <div class="col-lg-12">
                                                                        <div class="col-md-10" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #3598dc">Purchase </strong></div>
                                                                        <div class="col-lg-2 right-align">
                                                                            <asp:Button ID="btnAddNewRow" CssClass="btn btn-primary addnewbtn" runat="server" Text="Add New Row" OnClick="btnAddNewRow_Click" onkeydown="EnterToTab()" OnClientClick="javascript:return Confirm();" />
                                                                        </div>
                                                                    </div>
                                                                </div>


                                                                <br />
                                                                <div id="divInsert" runat="server">
                                                                    <asp:GridView ID="grdInsert" runat="server" AutoGenerateColumns="false" class="noborder fullwidth">
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
                                                                                    <asp:TextBox ID="PRODID" onkeypress="getCurrentIndex(this);return isNumber(event)"  onchange="Calculation(this)" OnTextChanged="PRODID_TextChanged" CssClass="form-control" onkeydown="EnterToTab()" runat="server" AutoPostBack="true"  commandArgument="<%# Container.DataItemIndex %>"    Text='<%#Eval("PRODID") %>'  />
                                                                              <%--  <asp:AutoCompleteExtender ID="autoid" runat="server" TargetControlID="PRODID"
                                                                                        UseContextKey="true"
                                                                                        CompletionInterval="1"
                                                                                        CompletionSetCount="20"
                                                                                        MinimumPrefixLength="1"
                                                                                        EnableCaching="true"
                                                                                        ServicePath="~/WebServices/Contact.asmx"
                                                                                        ServiceMethod="GetProductID">
                                                                                    </asp:AutoCompleteExtender>--%>
                                                                                     </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="PRODUCT NAME" ItemStyle-Width="250px">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="PRODNAME" Enabled="true" onkeypress="getCurrentIndex(this)" CssClass="form-control"  onchange="Calculation1(this)" OnTextChanged="TxtPRODUCTNAME_TextChanged" AutoPostBack="true" runat="server" Text='<%#Eval("PRODNAME") %>' />
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
                                                                                    <asp:TextBox ID="QTY" CssClass="form-control" onkeypress="javascript:return isNumber(event)" AutoPostBack="true" OnTextChanged="QUANTITY_TextChanged" runat="server" Text='<%#Eval("QTY") %>' />

                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="RATE" ItemStyle-Width="100px">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="UNITCOST" CssClass="form-control" onkeypress="javascript:return isNumber(event)" runat="server" Enabled="false" Text='<%#Eval("UNITCOST") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="GST" ItemStyle-Width="100px">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="GST" CssClass="form-control" onkeypress="javascript:return isNumber(event)" Enabled="false" runat="server" Text='<%#Eval("GST") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="GSTAMT" ItemStyle-Width="70px">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="GSTAMT" Enabled="false" onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server" Text='<%#Eval("GSTAMT") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="AMOUNT" ItemStyle-Width="120px">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="TOTALAMT" Enabled="false" CssClass="form-control" runat="server" Text='<%#Eval("TOTALAMT") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                        </Columns>
                                                                    </asp:GridView>
                                                                     <asp:HiddenField ID="hdnCrossCheck" runat="server" />
                                                                    <asp:HiddenField ID="hdnRowIndex" runat="server" />
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="col-md-9">
                                                                            <%-- <asp:Label Text="Total Amount" runat="server"></asp:Label>--%>
                                                                            <asp:Label id="LBlAmt" Style="width: 138px; margin-left: 921px;" Text='<%#Eval("LBlAmt") %>' runat="server" />
                                                                        </div>
                                                                    </div>
                                                                    -
                                                                </div>

                                                            </div>


                                                            <div class="form-actions">
                                                                <div class="row">

                                                                    <div class="col-md-offset-3 col-md-9">
                                                                        <asp:Button ID="BtnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="BtnSubmit_Click" OnClientClick="Javascript:return IsValide();CheckForFutureDate();IsEmailAddress(txtemailid);" TabIndex="49" />
                                                                        <asp:Button ID="Btnclear" runat="server" Text="ClearAll" CssClass="btn btn-primary" OnClick="Btnclear_Click" TabIndex="50" />
                                                                        <asp:Button ID="btnExit" runat="server" Text="Go To Main Menu" CssClass="btn btn-primary" OnClick="btnExit_Click" TabIndex="51" />
                                                                        <asp:Button ID="btnCancle" runat="server" Text="Cancle" Visible="false" CssClass="btn btn-primary" OnClick="btnCancle_Click" TabIndex="51" />
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
                                                                                   
                                                                                         <asp:GridView ID="GrdPurchase" runat="server" CellPadding="6" CellSpacing="7"
                                                                                         ForeColor="#333333" OnPageIndexChanged="GrdPurchase_PageIndexChanged" OnPageIndexChanging="GrdPurchase_PageIndexChanging"
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
                                                                                            <asp:TemplateField HeaderText="PONO" Visible="true">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="PONO" runat="server" Text='<%# Eval("PONO") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
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
                                                                                            <%--<asp:TemplateField HeaderText="PRODUCT ID." Visible="true">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="PRODID" runat="server" Text='<%# Eval("PRODID ") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>--%>

                                                                                            <%--  <asp:TemplateField HeaderText="PRODUCT NAME" Visible="true">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="PRODNAME" runat="server" Text='<%# Eval("UNITCOST") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>--%>
                                                                                            <asp:TemplateField HeaderText="QUANTITY" Visible="true">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="RATE" runat="server" Text='<%# Eval("QTY ") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <%-- <asp:TemplateField HeaderText="GST" Visible="true">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="GST" runat="server" Text='<%# Eval("GST ") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>--%>
                                                                                            <asp:TemplateField HeaderText="GST" Visible="true">
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

                                                                                            <%--<asp:TemplateField HeaderText="Authorize" Visible="true">
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton ID="LnkAutorise" runat="server" CommandArgument='<%#Eval("PONO")%>' CommandName="Select" OnClick="LnkAutorise_Click" class="glyphicon glyphicon-pencil"></asp:LinkButton>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>--%>



                                                                                            <asp:TemplateField HeaderText="View" Visible="true">
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton ID="lnkView" runat="server" CommandArgument='<%#Eval("PONO")%>' CommandName="select" OnClick="lnkView_Click" class="glyphicon glyphicon-pencil"></asp:LinkButton>
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

