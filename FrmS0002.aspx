
<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmS0002.aspx.cs" Inherits="FrmS0002" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">


    <%--  <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="500" SizeToReportContent = "true">
    </rsweb:ReportViewer>--%>
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
    <script type="text/javascript">
        function ShowHideDiv() {
            if ($("#RdbSingle").is(":checked")) {
                $("#Single").hide();
                $("#Multiple").show();
            }
            else {
                $("#Single").show();
                $("#Multiple").hide();
            }
        }
    </script>
    <script type="text/javascript">
        function ShowHideDiv1() {
            if ($("#RdbMultiple").is(":checked")) {
                $("#Single").show();
                $("#Multiple").hide();
            }
            else {
                $("#Single").hide();
                $("#Multiple").show();
            }
        }
    </script>
    <script type="text/javascript">
        function ConfirmOnDelete() {
            if (confirm("Are you want to Confirm?") == true)
                return true;
            else
                return false;
        }
    </script>


    <script>

        function ClearLabel() {

            // alert("hello");

            document.getElementById("lblAddMessage").innerHTML = "";


        }
    </script>


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

    <script>

        function FormatIt(obj) {
            if (obj.value.length == 2) //DAY
                obj.value = obj.value + "/";
            if (obj.value.length == 5) //MONTH
                obj.value = obj.value + "/";
            if (obj.value.length == 11) //YEAR
                alert("Enter Valid Date!....");
        }
    </script>
    <script>
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && iKeyCode != 13 && (iKeyCode < 48 || iKeyCode > 57))
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
    <script>

        function dateLen(dt) {
            var dt1 = dt + '';
            if (dt1.length == 1)
                dt1 = '0' + dt;

            return dt1;
        }
    </script>
    <script type="text/javascript">
        function IsValide() {
            var srno = document.getElementById('<%=TxtSRNO.ClientID%>').value;
            var srname = document.getElementById('<%=TXTSROName.ClientID%>').value;
            var prdcd = document.getElementById('<%=txtCaseNo.ClientID%>').value;
            var brcd = document.getElementById('<%=TxtBRCD.ClientID%>').value;
            var accno = document.getElementById('<%=txtCaseY.ClientID%>').value;
            var member = document.getElementById('<%=txtMember.ClientID%>').value;
            var PrnAMT = document.getElementById('<%=txtPriAmt.ClientID%>').value;
            var Rate = document.getElementById('<%=txtRate.ClientID%>').value;
            var FDate = document.getElementById('<%=txtFromDate.ClientID%>').value;
            var TDate = document.getElementById('<%=txtToDate.ClientID%>').value;
            var Defaluter = document.getElementById('<%=txtDefaulterName.ClientID%>').value;
            var DefalutVal = document.getElementById('<%=txtDefaultValue.ClientID%>').value;
             var AwardAmt=document.getElementById('<%=txtAwardAmt.ClientID%>').value;

            if (prdcd == "") {
                message = 'Please Enter Product Code....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=txtCaseNo.ClientID %>').focus();
                return false;
            }
            if (brcd == "") {
                message = 'Please Enter Branch Code....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=TxtBRCD.ClientID %>').focus();
                return false;
            }
            if (accno == "") {
                message = 'Please Enter Account Number....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=txtCaseY.ClientID %>').focus();
                return false;
            }
            if (member == "") {
                message = 'Please Enter Member No....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=txtMember.ClientID %>').focus();
                return false;
            }
            if (srno == "") {
                message = 'Please Enter Srno Number....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=TxtSRNO.ClientID %>').focus();
                return false;
            }
            if (srname == "") {
                message = 'Please Enter Srno name....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=TXTSROName.ClientID %>').focus();
                return false;
            }

            if (PrnAMT == "") {
                message = 'Please Enter Principle Amount....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=txtPriAmt.ClientID %>').focus();
                return false;
            }
            if (Rate == "") {
                message = 'Please Enter Rate Of Interest....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=txtRate.ClientID %>').focus();
                return false;
            }
            if (FDate == "") {
                message = 'Please Enter FromDate....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=txtFromDate.ClientID %>').focus();
                return false;
            }
            if (TDate == "") {
                message = 'Please Enter ToDate....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=txtToDate.ClientID %>').focus();
                return false;
            }
            
            if (DefalutVal == "") {
                message = 'Please Enter Default Value....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=txtDefaultValue.ClientID %>').focus();
                return false;
            }
          
       

            if (AwardAmt == "") {
                message = 'Please Enter AwardAmt Value....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=txtAwardAmt.ClientID %>').focus();
                return false;
            }
        }

    </script>
    <script>

        function Year(obj) {
            if (obj.value.length == 2) //DAY
                obj.value = obj.value + "-";
            obj.value = obj.value;

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel runat="server" ID="UpdatePanel" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="col-md-12">
                <div class="portlet box green" id="Div1">
                    <div class="portlet-title">
                        <div class="caption">
                            DEMAND MASTER
                        </div>
                    </div>
                    <div class="portlet-body form">
                        <div class="form-horizontal">
                            <div class="form-wizard">
                                <div class="form-body">
                                    <div class="tab-content">
                                        <div class="tab-pane active" id="tab1">
                                            <div class="tab-pane active" id="tab__blue">
                                                <ul class="nav nav-pills">

                                                    <%-- <li>
                                                <asp:LinkButton ID="lnkAdd" runat="server" Text="a" class="btn btn-default" OnClick="lnkAdd_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;" TabIndex="1"><i class="fa fa-asterisk"></i>Add New</asp:LinkButton>
                                            </li>--%>
                                                    <%-- <li>
                                                <asp:LinkButton ID="lnkModify" runat="server" Text="VW" class="btn btn-default" OnClick="lnkModify_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;" TabIndex="2"><i class="fa fa-arrows"></i>Modify</asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton ID="lnkDelete" runat="server" Text="MD" class="btn btn-default" OnClick="lnkDelete_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;" TabIndex="3"><i class="fa fa-pencil-square-o"></i>Cancel</asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton ID="lnkAuthorized" runat="server" Text="a" class="btn btn-default" OnClick="lnkAuthorized_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;" TabIndex="4"><i class="fa fa-arrows"></i>Authorise</asp:LinkButton>
                                            </li>--%>

                                                    <li class="pull-right">
                                                        <asp:Label ID="lblStst" runat="server" Text="Activity Perform :" Style="font-weight: bold;"></asp:Label>
                                                        <asp:Label ID="lblActivity" runat="server" Text=""></asp:Label>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div style="border: 1px solid #3598dc">
                                                <div class="row" style="margin-bottom: 10px;">
                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <div class="col-lg-12"><strong style="color: #3598dc">Case Detail's : </strong></div>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-2">BRCD:<span class="required">*</span></label>
                                                            <div class="col-md-1">
                                                                <asp:TextBox ID="TxtBRCD" Placeholder="BRCD" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" OnTextChanged="TxtBRCD_TextChanged" AutoPostBack="true" TabIndex="1"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <asp:TextBox ID="TxtBRCDName" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                            <label class="control-label col-md-1">Case Year:<span class="required">*</span></label>
                                                            <div class="col-md-1">
                                                                <asp:TextBox ID="txtCaseY" Placeholder="YY-YY" onkeyup="Year(this)" runat="server" CssClass="form-control" onkeypress="javascript:return isNumber (event)" AutoPostBack="true" TabIndex="2"></asp:TextBox>
                                                            </div>
                                                            <label class="control-label col-md-1">Case No:<span class="required">*</span></label>
                                                            <div class="col-md-1">
                                                                <asp:TextBox ID="txtCaseNo" Placeholder="Case No" runat="server" OnTextChanged="txtCaseNo_TextChanged" CssClass="form-control" onkeypress="javascript:return isNumber (event)" AutoPostBack="true" TabIndex="3"></asp:TextBox>
                                                            </div>


                                                            <%-- <label class="control-label col-md-1">Member No:<span class="required">*</span></label>
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="txtMember" CssClass="form-control" onkeypress="javascript:return isNumber (event)" PlaceHolder="Member No" TabIndex="4" runat="server" ></asp:TextBox>
                                                    </div>
                                                            --%>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin: 7px 0 7px 0;">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-2">Member Type:<span class="required">*</span></label>
                                                                <div class="col-md-2">
                                                                    <asp:DropDownList ID="ddlMemType" runat="server" CssClass="form-control" TabIndex="4" Visible="true" OnSelectedIndexChanged="ddlMemType_SelectedIndexChanged" AutoPostBack="true" EnableViewState="true"></asp:DropDownList>

                                                                </div>
                                                            <label class="control-label col-md-1">Member No:<span class="required">*</span></label>
                                                            <div class="col-md-1">
                                                                <asp:TextBox ID="txtMember" CssClass="form-control" AutoPostBack="true" onkeypress="javascript:return isNumber (event)" OnTextChanged="txtMember_TextChanged" PlaceHolder="Member No" TabIndex="5" runat="server"></asp:TextBox>
                                                            </div>

                                                            <div class="col-md-3">
                                                                <asp:TextBox ID="txtMemberName" Placeholder="Member Name" onkeydown="return CheckFirstChar(event.keyCode, this);" AutoPostBack="true" onkeypress="javascript:return OnltAlphabets(event)" Enabled="true" OnTextChanged="txtMemberName_TextChanged" CssClass="form-control" runat="server" TabIndex="6"></asp:TextBox>
                                                                <div id="CustList2" style="height: 200px; overflow-y: scroll;"></div>
                                                                <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="txtMemberName"
                                                                    UseContextKey="true"
                                                                    CompletionInterval="1"
                                                                    CompletionSetCount="20"
                                                                    MinimumPrefixLength="1"
                                                                    EnableCaching="true"
                                                                    ServicePath="~/WebServices/Contact.asmx"
                                                                    ServiceMethod="GetMemberName"
                                                                    CompletionListElementID="CustList2">
                                                                </asp:AutoCompleteExtender>

                                                            </div>

                                                            <div class="col-md-1" >
                                                                    <asp:Button runat="server" ID="BtnModify" Text="Modify" CssClass="btn btn-success" TabIndex="7"  OnClick="BtnModify_Click" />
                                                                    <%--  <asp:Button runat="server" ID="btnareremove" Text="Delete" CssClass="btn btn-success" OnClick="btnareremove_Click" OnClientClick="return ConfirmOnDelete();" />
                                                                    <asp:Button runat="server" ID="btnareaCancel" Text="Cancel" CssClass="btn btn-success" OnClick="btnareaCancel_Click" Visible="false" />--%>
                                                                </div>
                                                            <div class="col-md-1" >
                                                                <asp:Button runat="server" ID="BtnAddNew" Text ="ADD" CssClass="btn btn-success" TabIndex="8" OnClick="BtnAddNew_Click"/>
                                                            </div>
                                                          


                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                              <div runat="server" id="div_Sro" visible="true">
                                                                <label class="control-label col-md-2">SRO No : <span class="required">*</span></label>
                                                                <div class="col-md-1">
                                                                    <asp:TextBox ID="TxtSRNO" CssClass="form-control" onkeypress="javascript:return isNumber (event)" PlaceHolder="SRO No" TabIndex="8" runat="server" AutoPostBack="true" OnTextChanged="TxtSRNO_TextChanged"> </asp:TextBox>
                                                                </div>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="TXTSROName" Placeholder="SrNo Name" Enabled="true" onkeydown="return CheckFirstChar(event.keyCode, this);" onkeypress="javascript:return OnltAlphabets(event)" CssClass="form-control" runat="server" TabIndex="9"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div runat="server" id="div8" visible="false">
                                                                
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="txtmemADD" Placeholder="Address" TextMode="MultiLine"  Visible="true" onkeydown="return CheckFirstChar(event.keyCode, this);" onkeypress="javascript:return OnltAlphabets(event)" CssClass="form-control" runat="server" TabIndex="10"></asp:TextBox>
                                                                </div>
                                                                
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"></div>
                                                            <div class="col-md-4"><strong style="color: #3598dc">Recovery Certificate Detail's : </strong></div>

                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">

                                                            </div>
                                                        </div>
                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                             <asp:Label runat="server" CssClass="control-label col-md-2" ID="Label1" Text ="RC TYPE:"></asp:Label>
                                                            <%-- <label class="control-label col-md-2">101 RC No:</label>--%>
                                                            <div class="col-md-2">
                                                               <asp:DropDownList ID="ddlRCNo" runat="server" AutoPostBack="true"   CssClass="form-control" TabIndex="11" OnSelectedIndexChanged="ddlRCNo_SelectedIndexChanged" EnableViewState="true"></asp:DropDownList>

                                                             </div>
                                                            <asp:Label runat="server" CssClass="control-label col-md-2" ID="lbl101Rc" ></asp:Label>
                                                            <%-- <label class="control-label col-md-2">101 RC No:</label>--%>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="TxtCaseFlNo" TabIndex="12" Placeholder=" Recovery Certificate No." OnTextChanged="TxtCaseFlNo_TextChanged" AutoPostBack="true"  CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                            <asp:Label runat="server" CssClass="control-label col-md-2"   ID="lbl101rcd"></asp:Label>

                                                     <%--       <label class="control-label col-md-3">101 RC Order Date:</label>
                                                    --%>        <div class="col-md-2">
                                                                <asp:TextBox ID="TxtCaseFlDate"  OnTextChanged="TxtCaseFlDate_TextChanged" TabIndex="13" CssClass="form-control" type="text" PlaceHolder="dd/mm/yyyy" AutoPostBack="FALSE" runat="server" onkeyup="FormatIt(this)"></asp:TextBox>
                                                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtCaseFlDate">
                                                                </asp:TextBoxWatermarkExtender>
                                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtCaseFlDate">
                                                                </asp:CalendarExtender>

                                                            </div>
                                                           
                                                        </div>
                                                    </div>


                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-2"></label>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="TxtCaseFlNo91" TabIndex="14" AutoPostBack=" FALSE" Placeholder="98 Case File No" Visible="false" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>

                                                            <label class="control-label col-md-2"></label>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="TxtCaseFlDate91" AutoPostBack="FALSE"  TabIndex="15" CssClass="form-control" Visible="false" type="text" PlaceHolder="dd/mm/yyyy" runat="server" onkeyup="FormatIt(this)"></asp:TextBox>
                                                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtCaseFlDate91">
                                                                </asp:TextBoxWatermarkExtender>
                                                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtCaseFlDate91">
                                                                </asp:CalendarExtender>
                                                            </div>
                                                           
                                                        </div>
                                                    </div>
                                                     <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                          <label class="control-label col-md-2">98 Execution Date:</label>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtExecutionD" TabIndex="16"  CssClass="form-control" type="text" PlaceHolder="dd/mm/yyyy" runat="server" AutoPostBack="FALSE" onkeyup="FormatIt(this)"></asp:TextBox>
                                                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender9" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtCaseFlDate91">
                                                                </asp:TextBoxWatermarkExtender>
                                                                <asp:CalendarExtender ID="CalendarExtender9" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtCaseFlDate91">
                                                                </asp:CalendarExtender>
                                                            </div>
                                                           <label class="control-label col-md-2">Application Filing Date: </label>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="TxtRODT" TabIndex="17" CssClass="form-control" type="text" PlaceHolder="dd/mm/yyyy" runat="server" AutoPostBack="FALSE" onkeyup="FormatIt(this)"></asp:TextBox>
                                                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtRODT">
                                                                </asp:TextBoxWatermarkExtender>
                                                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtRODT">
                                                                </asp:CalendarExtender>
                                                            </div>
                                                            <label class="control-label col-md-2">Order By:</label>
                                                            <div class="col-md-2">
                                                                   <asp:DropDownList ID="ddlorder" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlorder_SelectedIndexChanged"   CssClass="form-control" TabIndex="18"  EnableViewState="true"></asp:DropDownList>

                                                
                                                                          </div>

                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin: 7px 0 7px 0" >
                                                        <div class="col-lg-12">
                                                         
                                                            <label class="control-label col-md-3"></label>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="TxtNoticeIssDt" TabIndex="19" Visible="false" Width="150px" CssClass="form-control" type="text" PlaceHolder="dd/mm/yyyy" runat="server" onkeyup="FormatIt(this)"></asp:TextBox>
                                                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" WatermarkText="DD/MM/YYYY" runat="server" Enabled="false" TargetControlID="TxtNoticeIssDt">
                                                                </asp:TextBoxWatermarkExtender>
                                                                <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtNoticeIssDt">
                                                                </asp:CalendarExtender>
                                                            </div>

                                                        </div>
                                                    </div>


                                                  
                                                    <div id="Div3" runat="server" class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                              
                                                            <label class="control-label col-md-2">Ward:</label>
                                                            <div class="col-md-2">
                                                                <%-- <asp:TextBox ID="txtWard" Placeholder="Ward" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="10" AutoPostBack="true"></asp:TextBox>
                                                                --%>
                                                                <asp:DropDownList ID="txtWard" runat="server" CssClass="form-control" OnSelectedIndexChanged="txtWard_SelectedIndexChanged" TabIndex="20"></asp:DropDownList>
                                                            </div>
                                                            <label class="control-label col-md-2">City:</label>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="TxtDivCityName" onkeypress="javascript:return OnltAlphabets(event)" Placeholder="City" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="21"></asp:TextBox>
                                                            </div>
                                                            <label class="control-label col-md-2">Pincode:</label>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtPincode" Width="150px"  Placeholder="Pincode" onkeypress="javascript:return isNumber (event)" MaxLength="6" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="22"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-2">Award Amount:<span class="required">*</span></label>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtAwardAmt" TabIndex="23" Placeholder="Award Amount:" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                            <label class="control-label col-md-2">Principle Amount:<span class="required">*</span></label>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtPriAmt" Visible="true" Enabled="true" TabIndex="24" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                            </div>
                                                            <label class="control-label col-md-2">Rate:<span class="required">*</span></label>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtRate" TabIndex="25" Width="150px" Placeholder="% Rate" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>

                                                        </div>

                                                    </div>

                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-2">From Date:<span class="required">*</span></label>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtFromDate" TabIndex="26" Placeholder="DD/MM/YYYY" CssClass="form-control" runat="server" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender7" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtFromDate">
                                                                </asp:TextBoxWatermarkExtender>
                                                                <asp:CalendarExtender ID="CalendarExtender7" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtFromDate">
                                                                </asp:CalendarExtender>
                                                            </div>
                                                            <label class="control-label col-md-2">To Date:<span class="required">*</span></label>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtToDate" TabIndex="27" Placeholder="DD/MM/YYYY" CssClass="form-control" runat="server" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" AutoPostBack="true" OnTextChanged="txtToDate_TextChanged"></asp:TextBox>
                                                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender8" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtToDate">
                                                                </asp:TextBoxWatermarkExtender>
                                                                <asp:CalendarExtender ID="CalendarExtender8" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtToDate">
                                                                </asp:CalendarExtender>
                                                            </div>
                                                            <label class="control-label col-md-2">Months:</label>
                                                            <div class="col-md-1">
                                                                <asp:TextBox ID="txtMonth" TabIndex="28" Width="150px" Placeholder="Total Months" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>

                                                        </div>

                                                        <%-- <div class="col-md-2">
                                                        <asp:DropDownList ID="DdlaccStatus" runat="server" CssClass="form-control" TabIndex="29" OnSelectedIndexChanged="DdlaccStatus_SelectedIndexChanged" AutoPostBack="true" EnableViewState="true"></asp:DropDownList>
                                                    </div>
                                                             
                                                       <%-- <label class="control-label col-md-2">Remark2: </label>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="TxtRemark2" TabIndex="20" Placeholder="Remark2" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>--%>
                                                    </div>
                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">

                                                            <label class="control-label col-md-2">Total Interest:</label>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtInterest" TabIndex="29" Placeholder="Interest" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>

                                                            <label class="control-label col-md-2">DefaultValue:<span class="required">*</span></label>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtDefaultValue"  TabIndex="30" Placeholder="Default Value" OnTextChanged="txtDefaultValue_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                             <label class="control-label col-md-2">Cost Application:</label>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtCost" TabIndex="31" Width="150px" OnTextChanged="txtCost_TextChanged" AutoPostBack="true" Placeholder="Cost Application" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">

                                                            <label class="control-label col-md-2">Execution Charges:</label>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtExecutionC" TabIndex="32" Width="150px" onkeypress="javascript:return isNumber (event)"  Placeholder="Execution Charges:" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>

                                                            <label class="control-label col-md-2">Cost Of Process:</label>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtCostProcss" TabIndex="34"  Placeholder="Cost Of Process" OnTextChanged="txtCostProcss_TextChanged" AutoPostBack="true" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                            <label class="control-label col-md-2">Total Receivable Amt: </label>
                                                            <div class="col-md-1">
                                                                <asp:TextBox ID="TxtTotalRec" TabIndex="35" Width="150px" Placeholder="Total Receivable Amt" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <div>
                                                          
                                                        </div>


                                                    </div>
                                                      
                                                               <%--<div class="row">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"></div>
                                                            <div class="col-md-4"><strong style="color: #3598dc">Payment Mode Detail's : </strong></div>

                                                        </div>
                                                    </div>
                                                    <div runat="server" id="div12" visible="true">
                                                        <div class="row" style="margin: 7px 0 7px 0" runat="server" id="Div13">
                                                        <div class="col-lg-12" id="Div14">

                                                            <label class="control-label col-md-2">Payment Mode:</label>
                                                             <div class="col-md-2">
                                                               <asp:DropDownList ID="ddlPaymentMode"  runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPaymentMode_SelectedIndexChanged"  CssClass="form-control" TabIndex="10"  EnableViewState="true"></asp:DropDownList>
                                                                 
                                                             </div>
                                                            
                                                            <label class="control-label col-md-2" >Amount</label>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtAmount"  onkeydown="return CheckFirstChar(event.keyCode, this);" placeholder="Amount" CssClass="form-control" runat="server" TabIndex="35"></asp:TextBox>
                                                            </div>
                                                                  
                                                          </div>
                                                            </div>
                                                      <div class="row" style="margin: 7px 0 7px 0" runat="server" id="Div15">
                                                        <div class="col-lg-12" id="Div16">
                                                              <label class="control-label col-md-2" id="divche1">Cheque No</label>
                                                             <div class="col-md-2" id="divche2">
                                                                <asp:TextBox ID="txtChequeNo"   onkeydown="return CheckFirstChar(event.keyCode, this);" placeholder="Cheque No" CssClass="form-control" runat="server" TabIndex="35"></asp:TextBox>
                                                            </div>
                                                            </div>
                                                        </div>--%>
                                                    <div class="col-lg-12"></div>
                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"></div>
                                                            <div class="col-md-4"><strong style="color: #3598dc">Defaulter Detail's : </strong></div>

                                                        </div>
                                                    </div>
                                                    <div runat="server" id="div11" visible="true">
                                                      <div class="row" style="margin: 7px 0 7px 0" runat="server" id="Div2">
                                                        <div class="col-lg-12" id="DefDef">

                                                            <label class="control-label col-md-2">Type:</label>
                                                             <div class="col-md-1">
                                                               <asp:DropDownList ID="DDLType" Width="130px" runat="server" AutoPostBack="true"  OnSelectedIndexChanged="DDLType_SelectedIndexChanged"  CssClass="form-control" TabIndex="36"  EnableViewState="true"></asp:DropDownList>
                                                                 
                                                             </div>
                                                             <div class="col-md-2" style="margin-left:50px">
                                                                <asp:TextBox ID="txtTypeNo"    onkeydown="return CheckFirstChar(event.keyCode, this);" placeholder="Property No" CssClass="form-control" runat="server" TabIndex="37"></asp:TextBox>
                                                            </div>
                                                             <label class="control-label col-md-1" >Floor No:</label>
                                                            <div class="col-md-1">
                                                                <asp:TextBox ID="txtFloor"   onkeydown="return CheckFirstChar(event.keyCode, this);" placeholder="Floor" CssClass="form-control" runat="server" TabIndex="38"></asp:TextBox>
                                                            </div>
                                                          
                                                           </div>
                                                            
                                                        </div>
                                                    </div>

                                                    <div runat="server" id="div_All" visible="true">
                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-12">
                                                                <label class="control-label col-md-2">Defaulter No:<span class="required">*</span></label>
                                                                <div class="col-md-1">
                                                                    <asp:TextBox ID="txtdefaulterNo" CssClass="form-control" onkeydown="return CheckFirstChar(event.keyCode, this);" onkeypress="javascript:return OnltAlphabets(event)" PlaceHolder="Defaulter Name" TabIndex="39" runat="server" AutoPostBack="true"></asp:TextBox>
                                                                </div>

                                                                <label class="control-label col-md-1">Def-Name:<span class="required">*</span></label>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="txtDefaulterName" CssClass="form-control" onkeydown="return CheckFirstChar(event.keyCode, this);"  PlaceHolder="Defaulter Name" TabIndex="40" runat="server"></asp:TextBox>
                                                                </div>
                                                             
                                                                <div class="col-md-1" style="margin-left: -5px">
                                                                    <asp:Button runat="server" ID="btnarea" Text="ADD" CssClass="btn btn-success" TabIndex="33" OnClick="btnarea_Click" />
                                                                    <asp:Button runat="server" ID="BtnModifyd" Text="Modify" CssClass="btn btn-success" TabIndex="33" OnClick="BtnModifyd_Click" Visible="false" />
                                                                    <asp:Button runat="server" ID="btndel" Text="delete" CssClass="btn btn-success" TabIndex="33" OnClick="btndel_Click" Visible="false" />
                                                                      </div>
                                                                <div class="col-md-3">
                                                                    <asp:ListBox ID="lstarea" runat="server" Visible="false" CssClass="form-control" Style="height: 50px" OnSelectedIndexChanged="lstarea_SelectedIndexChanged" TabIndex="41" AutoPostBack="true"></asp:ListBox>

                                                                </div>


                                                            </div>
                                                        </div>

                                                    </div>

                                                  
                                                       <div class="row" style="margin: 7px 0 7px 0" runat="server" id="Div9">
                                                        <div class="col-lg-12" id="Div10">
                                                             <label class="control-label col-md-2">Default Property:</label>
                                                           <div class="col-md-3">
                                                                <asp:TextBox ID="txtDefaultProperty" TextMode="MultiLine"  onkeydown="return CheckFirstChar(event.keyCode, this);" placeholder="Default Property" CssClass="form-control" runat="server" TabIndex="42"></asp:TextBox>
                                                            </div>
                                                            <label class="control-label col-md-2">Correspondence Address:</label>
                                                            <div class="col-md-3">
                                                                <asp:TextBox ID="txtCorrespondence" TextMode="MultiLine" Placeholder="Correspondence Address" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="43"></asp:TextBox>
                                                            </div>
                                                           
                                                        </div>
                                                    </div>

                                                    <div class="row" style="margin: 7px 0 7px 0" runat="server" id="Div4">
                                                        <div class="col-lg-12">

                                                            <label class="control-label col-md-2"></label>

                                                            <div class="col-md-5">
                                                                <asp:ListBox ID="lstDefProperty" runat="server" Visible="false" TabIndex="44" CssClass="form-control" Style="height: 50px" AutoPostBack="true"></asp:ListBox>

                                                            </div>


                                                            <div class="col-md-5">
                                                                <asp:ListBox ID="lstCorrespondenceAdd" runat="server" Visible="false" TabIndex="45" CssClass="form-control" Style="height: 50px" AutoPostBack="true"></asp:ListBox>

                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row" style="margin: 7px 0 7px 0" runat="server" id="Div6">
                                                        <div class="col-lg-12" id="occDiv">


                                                            <label class="control-label col-md-2">Occupation Details</label>
                                                            <div class="col-md-2">
                                                                <asp:DropDownList ID="ddlOccupation" AutoPostBack="true" runat="server"  OnSelectedIndexChanged="ddlOccupation_SelectedIndexChanged" CssClass="form-control" TabIndex="46">
                                                                </asp:DropDownList>
                                                            </div>
                                                            <label class="control-label col-md-3" style="margin-right: 3px">Occupation Address</label>
                                                            <div class="col-md-3">
                                                                <asp:TextBox ID="txtOccupationAdd"  TextMode="MultiLine" Placeholder="Occupation Address" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="47"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>


                                                    <div class="row" style="margin: 7px 0 7px 0" runat="server" id="Div5">
                                                        <div class="col-lg-12" id="mbdiv">

                                                            <label class="control-label col-md-2">Mobile No<span class="required"></span></label>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtMob1" MaxLength="10" onkeydown="return CheckFirstChar(event.keyCode, this);" onkeypress="javascript:return isNumber (event)" placeholder="Mobile No 1" CssClass="form-control" runat="server" TabIndex="48"></asp:TextBox>
                                                            </div>
                                                            <label class="control-label col-md-3">Mobile No</label>

                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtmob2" MaxLength="10" onkeydown="return CheckFirstChar(event.keyCode, this);" onkeypress="javascript:return isNumber (event)" placeholder="Mobile No 2" CssClass="form-control" runat="server" TabIndex="49"></asp:TextBox>
                                                            </div>
                                                            <br />
                                                            <div>  <div class="col-md-12"></div></div>
                                                        </div>
                                                    </div>

                                                     <div class="col-lg-12"></div>
                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"></div>
                                                            <div class="col-md-4"><strong style="color: #3598dc"></strong></div>

                                                        </div>
                                                    </div>

                                                    <div class="row" runat="server" id="divdef">
                    <div class="col-lg-12">
                        <div class="table-scrollable" style="border: none">
                            <table class="table table-striped table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th>
                                            <asp:GridView ID="grdDefulter" runat="server" AllowPaging="True"
                                                AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                EditRowStyle-BackColor="#FFFF99" 
                                                    PageIndex="10" PageSize="10"
                                                PagerStyle-CssClass="pgr" Width="98%" OnPageIndexChanging="grdDefulter_PageIndexChanging">
                                                 <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Branch Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="BRCD2" runat="server" Text='<%# Eval("BRCD") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Case No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="CASENO2" runat="server" Text='<%# Eval("CASENO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="CASEYAER">
                                                        <ItemTemplate>
                                                            <asp:Label ID="CASEYAER2" runat="server" Text='<%# Eval("CASE_YEAR") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="DEFAULTER NO">
                                                        <ItemTemplate>
                                                            <asp:Label ID="DEFAULTERNO" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="DEFAULTERNAME">
                                                        <ItemTemplate>
                                                            <asp:Label ID="DEFAULTERNAME" runat="server" Text='<%# Eval("DEFAULTERNAME") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="DEFAULTPROPERTY">
                                                        <ItemTemplate>
                                                            <asp:Label ID="DEFAULTPROPERTY" runat="server" Text='<%# Eval("DEFAULTPROPERTY") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Modify" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDefaulter" runat="server" CommandArgument='<%#Eval("BRCD")+"_"+Eval("ID")+"_"+Eval("CASENO")+"_"+Eval("CASE_YEAR")%>' CommandName="select" OnClick="lnkDefaulter_Click" class="glyphicon glyphicon-edit"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDeldefl" runat="server" CommandArgument='<%#Eval("BRCD")+"_"+Eval("ID")+"_"+Eval("CASENO")+"_"+Eval("CASE_YEAR")%>' CommandName="select" OnClick="lnkDeldefl_Click" class="glyphicon glyphicon-trash"></asp:LinkButton>
                                                        </ItemTemplate>
                                                     <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                     <%--  <asp:TemplateField HeaderText="Authorize" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkAuthorize" runat="server" CommandArgument='<%#Eval("BRCD")+"_"+Eval("CASENO")+"_"+Eval("CASE_YEAR")%>' CommandName="select" OnClick="lnkAuthorize_Click" class="glyphicon glyphicon-check"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="View" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkView" runat="server" CommandArgument='<%#Eval("BRCD")+"_"+Eval("CASENO")+"_"+Eval("CASE_YEAR")%>' CommandName="select" OnClick="lnkView_Click" class="glyphicon glyphicon-check"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                                    </asp:TemplateField>--%>
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
                                 

                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"></div>
                                                            <div class="col-md-4"><strong style="color: #3598dc">Committee Detail's : </strong></div>

                                                        </div>
                                                    </div>
                                                    <div>

                                                    </div>

                                                    <div runat="server" id="div7" visible="true">

                                                           <div runat="server" id="div12" visible="true">
                                                      <div class="row" style="margin: 7px 0 7px 0" runat="server" id="Div13">
                                                        <div class="col-lg-12" id="Div14">

                                                            <label class="control-label col-md-2">Type:</label>
                                                             <div class="col-md-1">
                                                               <asp:DropDownList ID="ddltype1" Width="100px" runat="server" AutoPostBack="true"  OnSelectedIndexChanged="ddltype1_SelectedIndexChanged"  CssClass="form-control" TabIndex="50"  EnableViewState="true"></asp:DropDownList>
                                                                 
                                                             </div>
                                                             <div class="col-md-2" style="margin-left:20px">
                                                                <asp:TextBox ID="txtPropertytype1"    onkeydown="return CheckFirstChar(event.keyCode, this);" placeholder="Property No" CssClass="form-control" runat="server" TabIndex="51"></asp:TextBox>
                                                            </div>
                                                             <label class="control-label col-md-2" >Floor No:</label>
                                                            <div class="col-md-1">
                                                                <asp:TextBox ID="txtFloor1"   onkeydown="return CheckFirstChar(event.keyCode, this);" placeholder="Floor" CssClass="form-control" runat="server" TabIndex="52"></asp:TextBox>
                                                            </div>
                                                             <div class="col-md-1" style="margin-left: -5px">
                                                                    <asp:Button runat="server" ID="btnmodifycomm" Text="modify" CssClass="btn btn-success" TabIndex="33" OnClick="btnmodifycomm_Click" Visible="false" />
                                                                    <asp:Button runat="server" ID="btnAddComm" Text="ADD" CssClass="btn btn-success" TabIndex="33" OnClick="btnAddComm_Click" />
                                                                      </div>
                                                          
                                                           </div>
                                                            
                                                        </div>
                                                    </div>
                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-12">
                                                                <label class="control-label col-md-2">Designation:</label>
                                                                <div class="col-md-2">
                                                                    <asp:DropDownList ID="ddlDesignation" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlDesignation_SelectedIndexChanged" CssClass="form-control" TabIndex="53">
                                                                    </asp:DropDownList>
                                                                </div>

                                                                <label class="control-label col-md-3">Name:</label>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="txtNamed" CssClass="form-control" onkeydown="return CheckFirstChar(event.keyCode, this);"  PlaceHolder=" Name" TabIndex="54" runat="server"></asp:TextBox>
                                                                </div>
                                                                

                                                            </div>
                                                        </div>
                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-12">
                                                                <label class="control-label col-md-2">Mobile No1:</label>
                                                                <div class="col-md-2">
                                                                    <asp:TextBox ID="txtdMob1" CssClass="form-control" MaxLength="10" onkeydown="return CheckFirstChar(event.keyCode, this);" onkeypress="javascript:return isNumber (event)" PlaceHolder="Mobile No" TabIndex="55" runat="server"></asp:TextBox>

                                                                </div>
                                                                <label class="control-label col-md-3">Address:</label>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="txtDeflterAddress"  CssClass="form-control" onkeydown="return CheckFirstChar(event.keyCode, this);"  PlaceHolder=" Address" TabIndex="56" runat="server"></asp:TextBox>

                                                                </div>

                                                                <label class="control-label col-md-1"><span class="required"></span></label>
                                                                <div class="col-md-1">
                                                                    <asp:TextBox ID="txtdMob2" CssClass="form-control" Visible="false" MaxLength="10" onkeydown="return CheckFirstChar(event.keyCode, this);" onkeypress="javascript:return isNumber (event)" PlaceHolder="Mobile No" TabIndex="57" runat="server"></asp:TextBox>
                                                                </div>


                                                            </div>
                                                        </div>
                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                            <div class="col-lg-12">
                                                              

                                                                <label class="control-label col-md-2"></label>
                                                                <div class="col-md-1">

                                                                    <asp:DropDownList ID="dddlWardComm" Visible="false" Width="150px" runat="server" CssClass="form-control" TabIndex="55"></asp:DropDownList>
                                                                </div>
                                                                <label class="control-label col-md-1"></label>
                                                                <div class="col-md-1">
                                                                    <asp:TextBox ID="txtCityComm" Placeholder="City" Visible="false"  onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="58"></asp:TextBox>
                                                                </div>
                                                                <label class="control-label col-md-1" style="margin-left: -10px"></label>
                                                                <div class="col-md-1">
                                                                    <asp:TextBox ID="txtPincodeComm" Placeholder="Pincode" Visible="false" onkeypress="javascript:return isNumber (event)" MaxLength="6" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="59"></asp:TextBox>
                                                                </div>

                                                            </div>
                                                        </div>


                                                         <div class="col-lg-12"></div>
                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"></div>
                                                            <div class="col-md-4"><strong style="color: #3598dc"> </strong></div>

                                                        </div>
                                                    </div>
                                                        <div class="row" runat="server" id="div_Grid1">
                    <div class="col-lg-12">
                        <div class="table-scrollable" style="border: none">
                            <table class="table table-striped table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th>
                                            <asp:GridView ID="grdCommitee" runat="server" AllowPaging="True"
                                                AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                EditRowStyle-BackColor="#FFFF99" 
                                                    PageIndex="20" PageSize="10"
                                                PagerStyle-CssClass="pgr" Width="98%" OnPageIndexChanging="grdCommitee_PageIndexChanging">
                                                 <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Branch Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="BRCD1" runat="server" Text='<%# Eval("BRCD") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Case No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="CASENO1" runat="server" Text='<%# Eval("CASENO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="CASEYAER">
                                                        <ItemTemplate>
                                                            <asp:Label ID="CASEYAER1" runat="server" Text='<%# Eval("CASE_YEAR") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ID">
                                                        <ItemTemplate>
                                                            <asp:Label ID="ID1" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="DESIGNATION">
                                                        <ItemTemplate>
                                                            <asp:Label ID="DESIGNATION" runat="server" Text='<%# Eval("DESIGNATION") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="COMM_NAME">
                                                        <ItemTemplate>
                                                            <asp:Label ID="NOTICE_ISS_DT1" runat="server" Text='<%# Eval("COMM_NAME") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Modify" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkSelect1" runat="server" CommandArgument='<%#Eval("BRCD")+"_"+Eval("ID")+"_"+Eval("CASENO")+"_"+Eval("CASE_YEAR")%>' CommandName="select" OnClick="lnkSelect1_Click" class="glyphicon glyphicon-edit"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cancle" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LNKCOMCANL" runat="server" CommandArgument='<%#Eval("BRCD")+"_"+Eval("ID")+"_"+Eval("CASENO")+"_"+Eval("CASE_YEAR")%>' CommandName="select" OnClick="LNKCOMCANL_Click" class="glyphicon glyphicon-edit"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                   <%-- <asp:TemplateField HeaderText="Cancel1" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDelete1" runat="server" CommandArgument='<%#Eval("BRCD")+"_"+Eval("CASENO")+"_"+Eval("CASE_YEAR")%>' CommandName="select" OnClick="lnkDelete_Click1" class="glyphicon glyphicon-trash"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Authorize" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkAuthorize" runat="server" CommandArgument='<%#Eval("BRCD")+"_"+Eval("CASENO")+"_"+Eval("CASE_YEAR")%>' CommandName="select" OnClick="lnkAuthorize_Click" class="glyphicon glyphicon-check"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="View" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkView" runat="server" CommandArgument='<%#Eval("BRCD")+"_"+Eval("CASENO")+"_"+Eval("CASE_YEAR")%>' CommandName="select" OnClick="lnkView_Click" class="glyphicon glyphicon-check"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                                    </asp:TemplateField>--%>
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

                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0" id="Div_Submit" runat="server" visible="False">
                                            <div class="col-lg-10">
                                                <div class="col-md-4">
                                                </div>
                                                <div class="col-md-8">
                                                    <asp:Button ID="BtnSubmit" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="BtnSubmit_Click" TabIndex="60" OnClientClick="Javascript:return IsValide()" />
                                                    <asp:Button ID="BtnClear" runat="server" CssClass="btn btn-primary" Text="Clear" OnClick="BtnClear_Click" TabIndex="61" />
                                                    <asp:Button ID="BtnExit" runat="server" CssClass="btn btn-primary" Text="Exit" OnClick="BtnExit_Click" TabIndex="62" />
                                                 <asp:Button ID="BtnRecipt" runat="server" CssClass="btn btn-primary" Text="Receipt" OnClick="BtnRecipt_Click" TabIndex="63"  />
                                                 <asp:Button ID="btnDownlod" runat="server" Visible="TRUE" CssClass="btn btn-primary" Text="Download_Report" OnClick="btnDownlod_Click" TabIndex="63"  />
                                               
                                                     </div>
                                                <div class="col-md-5">
                                                </div>
                                            </div>
                                        </div>

                                        <%-- <div class="row" style="margin: 7px 0 7px 0" id="Div_addnew" runat="server">
                                        <div class="col-lg-10">
                                            <div class="col-md-4">
                                            </div>
                                            <div class="col-md-6">
                                                <asp:Button ID="BtnAddNew" runat="server" CssClass="btn btn-primary" Text="Add New" OnClick="BtnAddNew_Click" TabIndex="24"/>
                                            </div>
                                            </div>
                                        </div>--%>
                                    </div>
                                    <div class="tab-pane active" id="Div_MidVid">
                                        <ul class="nav nav-pills">

                                            <li class="pull-right">
                                                <asp:Label ID="LblName" runat="server" Text="Maker :" Style="font-weight: bold;"></asp:Label>
                                                <asp:Label ID="LblMid" runat="server" Text=""></asp:Label>
                                            </li>
                                        </ul>
                                        <ul class="nav nav-pills">

                                            <li class="pull-right">
                                                <asp:Label ID="LblName1" runat="server" Text="Checker :" Style="font-weight: bold;"></asp:Label>
                                                <asp:Label ID="LblVid" runat="server" Text=""></asp:Label>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            </div>
                <div class="row" runat="server" id="div_Grid">
                    <div class="col-lg-12">
                        <div class="table-scrollable" style="border: none">
                            <table class="table table-striped table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th>
                                            <asp:GridView ID="GrdDemand" runat="server" AllowPaging="True"
                                                AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                EditRowStyle-BackColor="#FFFF99" 
                                                    PageIndex="10" PageSize="10"
                                                PagerStyle-CssClass="pgr" Width="100%" OnPageIndexChanging="GrdDemand_PageIndexChanging">
                                                 <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Branch Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="BRCD" runat="server" Text='<%# Eval("BRCD") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Case No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="CASENO" runat="server" Text='<%# Eval("CASENO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="CASEYAER">
                                                        <ItemTemplate>
                                                            <asp:Label ID="CASEYAER" runat="server" Text='<%# Eval("CASE_YEAR") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="SRO No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="SRO_NO" runat="server" Text='<%# Eval("SRO_NO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Notice Issue Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="NOTICE_ISS_DT" runat="server" Text='<%# Eval("NOTICE_ISS_DT","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Modify" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkSelect" runat="server" CommandArgument='<%#Eval("BRCD")+"_"+Eval("CASENO")+"_"+Eval("CASE_YEAR")%>' CommandName="select" OnClick="lnkSelect_Click" class="glyphicon glyphicon-edit"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cancel" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Eval("BRCD")+"_"+Eval("CASENO")+"_"+Eval("CASE_YEAR")%>' CommandName="select" OnClick="lnkDelete_Click1" class="glyphicon glyphicon-trash"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Authorize" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkAuthorize" runat="server" CommandArgument='<%#Eval("BRCD")+"_"+Eval("CASENO")+"_"+Eval("CASE_YEAR")%>' CommandName="select" OnClick="lnkAuthorize_Click" class="glyphicon glyphicon-check"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="View" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkView" runat="server" CommandArgument='<%#Eval("BRCD")+"_"+Eval("CASENO")+"_"+Eval("CASE_YEAR")%>' CommandName="select" OnClick="lnkView_Click" class="glyphicon glyphicon-check"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                     <%--<asp:TemplateField HeaderText="REPORT" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LNKREPORT" runat="server" CommandArgument='<%#Eval("BRCD")+"_"+Eval("CASENO")+"_"+Eval("CASE_YEAR")%>' CommandName="select" OnClick="LNKREPORT_Click" class="glyphicon glyphicon-check"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                                    </asp:TemplateField>--%>
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
         <Triggers >
   
        
          <asp:PostBackTrigger ControlID="btnDownlod" />
    </Triggers>
    </asp:UpdatePanel>
</asp:Content>
