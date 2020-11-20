<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="AVSUPSETP.aspx.cs" Inherits="AVSUPSETP" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script type="text/javascript" src="https://www.google.com/jsapi?key=YourKeyHere">
    </script>
    <%-- <script  type="text/javascript">
        google.load("elements", "1", {
            packages: "transliteration"
        });

        function onLoad() {
            var options = {
                sourceLanguage: google.elements.transliteration.LanguageCode.MARATHI,
                destinationLanguage: google.elements.transliteration.LanguageCode.MARATHI, // available option English, Bengali, Marathi, Malayalam etc.
                shortcutKey: 'ctrl+g',
                transliterationEnabled: true
            };

            var control = new google.elements.transliteration.TransliterationControl(options);
            control.makeTransliteratable(['txtBranchname']);
        }
        google.setOnLoadCallback(onLoad);

    </script>--%>
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
                            Upset Prize Detail's 
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
                                                </ul>
                                            </div>
                                            <div style="border: 1px solid #3598dc">
                                                <div class="row" style="margin-bottom: 10px;">
                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <div class="col-lg-12"><strong style="color: #3598dc">Upset Prize Detail's : </strong></div>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-2">बीआरसीडी:<span class="required">*</span></label>
                                                            <div class="col-md-1">
                                                                <asp:TextBox ID="TxtBRCD" Placeholder="बीआरसीडी" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" OnTextChanged="TxtBRCD_TextChanged" TabIndex="1"></asp:TextBox>
                                                            </div>

                                                            <label class="control-label col-md-2">केस वर्ष:<span class="required">*</span></label>
                                                            <div class="col-md-1">
                                                                <asp:TextBox ID="txtCaseY" Placeholder="YY-YY" onkeyup="Year(this)" runat="server" CssClass="form-control" onkeypress="javascript:return isNumber (event)" AutoPostBack="false" TabIndex="2"></asp:TextBox>
                                                            </div>
                                                            <label class="control-label col-md-1">केस क्र:<span class="required">*</span></label>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtCaseNo" Placeholder="Case No" runat="server" CssClass="form-control" onkeypress="javascript:return isNumber (event)" AutoPostBack="true" TabIndex="3"></asp:TextBox>
                                                            </div>
                                                            <asp:Label runat="server" CssClass="control-label col-md-1" ID="Label2">तारीख:</asp:Label>

                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtdate" TabIndex="4" CssClass="form-control" type="text" PlaceHolder="dd/mm/yyyy" AutoPostBack="true" runat="server" onkeyup="FormatIt(this)"></asp:TextBox>
                                                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtdate">
                                                                </asp:TextBoxWatermarkExtender>
                                                                <asp:CalendarExtender ID="CalendarExtender5" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtdate">
                                                                </asp:CalendarExtender>

                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin: 7px 0 7px 0;">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-2">मराठी फॉन्ट:</label>
                                                            <div class="col-md-2">
                                                                <asp:DropDownList ID="ddlmarathi" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlmarathi_SelectedIndexChanged" CssClass="form-control" TabIndex="5">
                                                                    <asp:ListItem Text="--Select-" Value="0" />
                                                                    <asp:ListItem Text="Shivaji01" Value="1" />
                                                                </asp:DropDownList>
                                                            </div>

                                                            <label class="control-label col-md-1">शाखेचे नाव:</label>
                                                            <div class="col-md-3">
                                                                <asp:TextBox ID="txtBranchname" Placeholder="शाखेचे नाव" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="6"></asp:TextBox>
                                                            </div>

                                                            <label class="control-label col-md-1">वसुली अधिकारी क्र:</label>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtrecoveryoffNo" Placeholder="वसुली अधिकारी क्र" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="7"></asp:TextBox>
                                                            </div>
                                                            <div></div>
                                                            </br>
                                                                 
                                                            <br />
                                                            <div class="row">
                                                                <div class="col-lg-12">
                                                                    <label class="control-label col-md-2">वसुली अधिकारी नाव:</label>
                                                                    <div class="col-md-3">
                                                                        <asp:TextBox ID="txtrecoveryOffName" Placeholder="वसुली अधिकारी नाव" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="7"></asp:TextBox>
                                                                    </div>
                                                                    <label class="control-label col-md-1">वसुली अधिकारी जा.क्र:</label>
                                                                    <div class="col-md-2">
                                                                        <asp:TextBox ID="txtRecOffCastno" Placeholder="वसुली अधिकारी जा.क्र" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="7"></asp:TextBox>
                                                                    </div>

                                                                </div>
                                                            </div>

                                                            <br />
                                                            <br />
                                                            <div class="row" style="margin-bottom: 10px;">
                                                                <div class="row">
                                                                    <div class="col-lg-12">
                                                                        <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"></div>

                                                                        <div class="col-lg-12"><strong style="color: #3598dc">वसुली दाखलाची महिती:</strong></div>
                                                                    </div>
                                                                </div>

                                                                <label class="control-label col-md-1">वसुली दाखलाचा प्रकार</label>



                                                                <div class="col-md-2">
                                                                    <%--   <asp:DropDownList ID="ddlRCNo" runat="server" AutoPostBack="true"   CssClass="form-control" TabIndex="6" OnSelectedIndexChanged="ddlRCNo_SelectedIndexChanged" EnableViewState="true"></asp:DropDownList>--%>
                                                                    <asp:DropDownList ID="ddlRCNo1" runat="server" AutoPostBack="true" CssClass="form-control" TabIndex="11" OnSelectedIndexChanged="ddlRCNo1_SelectedIndexChanged" EnableViewState="true"></asp:DropDownList>

                                                                </div>
                                                                <%--<label class="control-label col-md-1">वसुली दाखलाचा क्र:</label>--%>
                                                                <asp:Label runat="server" CssClass="control-label col-md-2" ID="lbl101Rc"></asp:Label>

                                                                <div class="col-md-2">
                                                                    <asp:TextBox ID="txtAdmisnNo" Placeholder="Recovery Admission NO" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="7"></asp:TextBox>

                                                                </div>
                                                                <asp:Label runat="server" CssClass="control-label col-md-2" ID="lbl101rcd"></asp:Label>

                                                                <%--    <label class="control-label col-md-1">वसुली दाखलाची तारीख:</label>
                                                                --%>
                                                                <div class="col-md-2">
                                                                    <asp:TextBox ID="txtadmisnDate" Placeholder="Recovery Admission Date" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="7"></asp:TextBox>
                                                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender8" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtadmisnDate">
                                                                    </asp:TextBoxWatermarkExtender>
                                                                    <asp:CalendarExtender ID="CalendarExtender8" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtadmisnDate">
                                                                    </asp:CalendarExtender>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-lg-12">
                                                                        <label class="control-label col-md-1">98 प्रमाणपत्र तारीख</label>
                                                                        <div class="col-md-3">
                                                                            <asp:TextBox ID="txtcertificateDate" onkeyup="FormatIt(this)" Placeholder="98 Certificate Date" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="7"></asp:TextBox>
                                                                            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtcertificateDate">
                                                                            </asp:TextBoxWatermarkExtender>
                                                                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtcertificateDate">
                                                                            </asp:CalendarExtender>
                                                                        </div>

                                                                        <label class="control-label col-md-1">वार्ड नाव:</label>
                                                                        <div class="col-md-3">
                                                                            <asp:TextBox ID="txtWardName" Placeholder="Ward Name" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="7"></asp:TextBox>

                                                                        </div>



                                                                        <label class="control-label col-md-1">वार्ड पत्ता:</label>
                                                                        <div class="col-md-3">
                                                                            <asp:TextBox ID="txtaddreess" TextMode="MultiLine" Placeholder=">Ward Address" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="7"></asp:TextBox>

                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-lg-12">
                                                                            <label class="control-label col-md-1">अवार्ड रक्कम:</label>
                                                                            <div class="col-md-3">
                                                                                <asp:TextBox ID="txtawardAmt" Placeholder="Award Amount" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="7"></asp:TextBox>

                                                                            </div>
                                                                            <label class="control-label col-md-1">मूद्दल रक्कम:</label>
                                                                            <div class="col-md-3">
                                                                                <asp:TextBox ID="txtprinciAmt" Placeholder="Principal Amount" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="7"></asp:TextBox>

                                                                            </div>

                                                                            <label class="control-label col-md-1">व्याज रक्कम:</label>
                                                                            <div class="col-md-3">
                                                                                <asp:TextBox ID="txtInterestAmt" Placeholder="Interest Amount" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="7"></asp:TextBox>

                                                                            </div>

                                                                            <div class="row">
                                                                                <div class="col-lg-12">
                                                                                    <label class="control-label col-md-1">व्याज तारीख पासून:</label>
                                                                                    <div class="col-md-3">
                                                                                        <asp:TextBox ID="txtFromInterestdate" onkeyup="FormatIt(this)" Placeholder="From Interest Date" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="7"></asp:TextBox>
                                                                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtFromInterestdate">
                                                                                        </asp:TextBoxWatermarkExtender>
                                                                                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtFromInterestdate">
                                                                                        </asp:CalendarExtender>
                                                                                    </div>

                                                                                    <label class="control-label col-md-1">एकूण वासुली रक्कम:</label>
                                                                                    <div class="col-md-3">
                                                                                        <asp:TextBox ID="txtToatalAmt" Placeholder="Total Recovery Amount" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="7"></asp:TextBox>

                                                                                    </div>


                                                                                    <div class="row" style="margin-bottom: 12px;">
                                                                                        <div class="row">
                                                                                            <div class="col-lg-12">
                                                                                                <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"></div>

                                                                                                <div class="col-lg-12"><strong style="color: #3598dc; margin-left: 20px;">थकबाकीदारांची माहिती :</strong></div>
                                                                                            </div>
                                                                                        </div>

                                                                                        <div class="row">
                                                                                            <div class="col-lg-12">
                                                                                                <label class="control-label col-md-1">मालमत्तेचे प्रकार:</label>
                                                                                                <div class="col-md-2">
                                                                                                    <asp:TextBox ID="txtpropType" Placeholder="Property Type" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="7"></asp:TextBox>

                                                                                                </div>
                                                                                                <label class="control-label col-md-1">सदनिका क्र.</label>
                                                                                                <div class="col-md-2">
                                                                                                    <asp:TextBox ID="txthouseno" Placeholder="House No." onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="7"></asp:TextBox>

                                                                                                </div>
                                                                                                <label class="control-label col-md-1">मजला :</label>
                                                                                                <div class="col-md-2">
                                                                                                    <asp:TextBox ID="txtFlatNo" Placeholder="Flat No." onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="7"></asp:TextBox>

                                                                                                </div>
                                                                                                <div class="col-md-1" style="margin-left: -5px">
                                                                                                    <asp:Button runat="server" ID="btnarea" Text="ADD" CssClass="btn btn-success" TabIndex="33" OnClick="btnarea_Click" />
                                                                                                    <asp:Button runat="server" ID="BtnModifyd" Text="Modify" CssClass="btn btn-success" TabIndex="33" OnClick="BtnModifyd_Click" Visible="false" />
                                                                                                    <asp:Button runat="server" ID="btndel" Text="delete" CssClass="btn btn-success" TabIndex="33" OnClick="btndel_Click" Visible="false" />
                                                                                                </div>

                                                                                            </div>
                                                                                        </div>

                                                                                        <div class="row">
                                                                                            <div class="col-lg-12">
                                                                                                <label class="control-label col-md-1">थकबाकीदार क्र: </label>
                                                                                                <div class="col-md-3">
                                                                                                    <asp:TextBox ID="txtArrearno" Placeholder=">Arrears no" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="7"></asp:TextBox>

                                                                                                </div>

                                                                                                <label class="control-label col-md-1">थकबाकीदाराचे नाव :</label>
                                                                                                <div class="col-md-3">
                                                                                                    <asp:TextBox ID="txtArrname" Placeholder=">Arrears name" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="7"></asp:TextBox>

                                                                                                </div>
                                                                                                <label class="control-label col-md-1">थकबाकीदाराचा पत्ता :</label>
                                                                                                <div class="col-md-3">
                                                                                                    <asp:TextBox ID="txtAddress" TextMode="MultiLine" Placeholder=">Arrears Address" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="7"></asp:TextBox>

                                                                                                </div>
                                                                                                <label class="control-label col-md-1">पत्रव्यवहाराचा पत्ता:</label>
                                                                                                <div class="col-md-3">
                                                                                                    <asp:TextBox ID="txtPostalAddress" TextMode="MultiLine" Placeholder=">Postal Address" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="7"></asp:TextBox>

                                                                                                </div>
                                                                                                <label class="control-label col-md-1">व्यवसायाचा पत्ता:</label>
                                                                                                <div class="col-md-3">
                                                                                                    <asp:TextBox ID="txtbusinessAdd" TextMode="MultiLine" Placeholder=">Business Address" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="7"></asp:TextBox>

                                                                                                </div>
                                                                                                <label class="control-label col-md-1">जप्ती आदेश तारीख:</label>
                                                                                                <div class="col-md-3">
                                                                                                    <asp:TextBox ID="txtOrderPo" Placeholder="DD/MM/YYYY" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="7"></asp:TextBox>
                                                                                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender7" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtOrderPo">
                                                                                                    </asp:TextBoxWatermarkExtender>
                                                                                                    <asp:CalendarExtender ID="CalendarExtender7" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtOrderPo">
                                                                                                    </asp:CalendarExtender>
                                                                                                </div>
                                                                                            </div>
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
                                                                                                                        <asp:TemplateField HeaderText="बीआरसीडी">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label ID="BRCD2" runat="server" Text='<%# Eval("BRCD") %>'></asp:Label>
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField HeaderText="केस क्र">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label ID="CASENO2" runat="server" Text='<%# Eval("CASENO") %>'></asp:Label>
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>

                                                                                                                        <asp:TemplateField HeaderText="केस वर्ष">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label ID="CASEYAER2" runat="server" Text='<%# Eval("CASE_YEAR") %>'></asp:Label>
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField HeaderText="थकबाकीदार क्र:">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label ID="DEFAULTERNO" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>

                                                                                                                        <asp:TemplateField HeaderText="थकबाकीदाराचे नाव">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label ID="DEFAULTERNAME" Font-Names="Shivaji01" Font-Size="14" runat="server" Text='<%# Eval("DEFAULTERNAME") %>'></asp:Label>
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField HeaderText="पत्रव्यवहाराचा पत्ता">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label ID="DEFAULTPROPERTY" Font-Names="Shivaji01" Font-Size="14" runat="server" Text='<%# Eval("DEFAULTPROPERTY") %>'></asp:Label>
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





                                                                                    <div class="row" style="margin-bottom: 12px;">
                                                                                        <div class="row">
                                                                                            <div class="col-lg-12">
                                                                                                <div></div>
                                                                                                <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"></div>

                                                                                                <div class="col-lg-12"><strong style="color: #3598dc; margin-left: 20px;">जप्त मालमत्ता माहिती :</strong></div>
                                                                                            </div>
                                                                                        </div>

                                                                                        <%--   <label class="control-label col-md-2">Designation:</label>--%>
                                                                                        <div class="col-md-1">
                                                                                            <asp:DropDownList ID="ddlDesignation" Visible="false" Enabled="false" AutoPostBack="false" runat="server" OnSelectedIndexChanged="ddlDesignation_SelectedIndexChanged" CssClass="form-control" TabIndex="5">
                                                                                            </asp:DropDownList>
                                                                                        </div>
                                                                                        <%--  <label class="control-label col-md-1">Ward:</label>--%>
                                                                                        <div class="col-md-1">

                                                                                            <asp:DropDownList ID="DropDownList1" Visible="false" Enabled="false" runat="server" CssClass="form-control" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" TabIndex="6"></asp:DropDownList>
                                                                                        </div>

                                                                                    </div>
                                                                                </div>

                                                                                <div class="row" style="margin: 7px 0 7px 0">
                                                                                    <div class="col-lg-12">
                                                                                        <label class="control-label col-md-2">एसआरओ ऑर्डर संदर्भ: <span class="required">*</span></label>
                                                                                        <div class="col-md-2">
                                                                                            <asp:TextBox ID="TxtSRNO" TextMode="MultiLine" CssClass="form-control" PlaceHolder="SRO No" TabIndex="8" runat="server" AutoPostBack="false"> </asp:TextBox>
                                                                                        </div>
                                                                                        <label class="control-label col-md-1">एसआरओ ऑर्डर तारीख:<span class="required">*</span></label>
                                                                                        <div class="col-md-2">
                                                                                            <asp:TextBox ID="txtSROORDERDATE" CssClass="form-control" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" PlaceHolder="SRO No" TabIndex="9" runat="server" AutoPostBack="true"> </asp:TextBox>
                                                                                            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtSROORDERDATE">
                                                                                            </asp:TextBoxWatermarkExtender>
                                                                                            <asp:CalendarExtender ID="CalendarExtender6" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtSROORDERDATE">
                                                                                            </asp:CalendarExtender>
                                                                                        </div>

                                                                                        <label class="control-label col-md-2">क्षेत्रफळ:<span class="required">*</span></label>
                                                                                        <div class="col-md-2">
                                                                                            <asp:TextBox ID="txtarea" CssClass="form-control" PlaceHolder="AREA OF PROPERTY Sq.<span" TabIndex="10" runat="server" AutoPostBack="false"> </asp:TextBox>
                                                                                        </div>

                                                                                    </div>
                                                                                </div>
                                                                                <div class="row" style="margin: 7px 0 7px 0">
                                                                                    <div class="col-lg-12">
                                                                                        <label class="control-label col-md-2">संपत्ती शीर्षक जोडा:<span class="required">*</span></label>
                                                                                        <div class="col-md-2">
                                                                                            <asp:TextBox ID="txtATTACHED" TextMode="MultiLine" CssClass="form-control" PlaceHolder="ATTACHED PROPERTY_TITLE" TabIndex="11" runat="server" AutoPostBack="false"> </asp:TextBox>
                                                                                        </div>
                                                                                        <label class="control-label col-md-1">मूल्यांकन तारीख:<span class="required">*</span></label>
                                                                                        <div class="col-md-2">
                                                                                            <asp:TextBox ID="txtVALUATIONDATE" CssClass="form-control" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" PlaceHolder="SRO No" TabIndex="12" runat="server" AutoPostBack="false"> </asp:TextBox>
                                                                                            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtVALUATIONDATE">
                                                                                            </asp:TextBoxWatermarkExtender>
                                                                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtVALUATIONDATE">
                                                                                            </asp:CalendarExtender>
                                                                                        </div>
                                                                                        <label class="control-label col-md-2">व्हॅल्यूर नाव:<span class="required">*</span></label>
                                                                                        <div class="col-md-3">
                                                                                            <asp:TextBox ID="txtVALUERNAME" CssClass="form-control" PlaceHolder="VALUER NAME" TabIndex="13" runat="server" AutoPostBack="false"> </asp:TextBox>
                                                                                        </div>
                                                                                    </div>

                                                                                </div>

                                                                                <div class="row" style="margin: 7px 0 7px 0">
                                                                                    <div class="col-lg-12">

                                                                                        <label class="control-label col-md-2">नोंदणी क्रमांक:.<span class="required">*</span></label>
                                                                                        <div class="col-md-2">
                                                                                            <asp:TextBox ID="TXTrEGNO" CssClass="form-control" TextMode="MultiLine" PlaceHolder="REG NO." TabIndex="14" runat="server" AutoPostBack="false"> </asp:TextBox>
                                                                                        </div>
                                                                                        <label class="control-label col-md-1">नोंदणी क्रमांक तारीख:<span class="required">*</span></label>
                                                                                        <div class="col-md-2">
                                                                                            <asp:TextBox ID="TXTREGNDATE" CssClass="form-control" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" PlaceHolder="REG NO.DATE" TabIndex="16" runat="server" AutoPostBack="false"> </asp:TextBox>
                                                                                            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TXTREGNDATE">
                                                                                            </asp:TextBoxWatermarkExtender>
                                                                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TXTREGNDATE">
                                                                                            </asp:CalendarExtender>
                                                                                        </div>
                                                                                        <label class="control-label col-md-2">बाजार भाव:<span class="required">*</span></label>
                                                                                        <div class="col-md-2">
                                                                                            <asp:TextBox ID="TXTMARKET" CssClass="form-control" onkeypress="javascript:return isNumber (event)" PlaceHolder="MARKET VALUE" TabIndex="17" runat="server" AutoPostBack="false"> </asp:TextBox>
                                                                                        </div>
                                                                                    </div>

                                                                                </div>

                                                                                <div class="row" style="margin: 7px 0 7px 0">
                                                                                    <div class="col-lg-12">

                                                                                        <label class="control-label col-md-2">योग्य बाजार भाव:<span class="required">*</span></label>
                                                                                        <div class="col-md-2">
                                                                                            <asp:TextBox ID="TXTFAIRMARKETVALUE" CssClass="form-control" onkeypress="javascript:return isNumber (event)" PlaceHolder="FAIRMARKETVALUE" TabIndex="18" runat="server" AutoPostBack="false"> </asp:TextBox>
                                                                                        </div>
                                                                                        <label class="control-label col-md-1">सब नोंदणी मूल्य<span class="required">*</span></label>
                                                                                        <div class="col-md-2">
                                                                                            <asp:TextBox ID="TXTSUBREG" CssClass="form-control" onkeypress="javascript:return isNumber (event)" PlaceHolder="SUBREG.VALUE" TabIndex="19" runat="server" AutoPostBack="false"> </asp:TextBox>

                                                                                        </div>
                                                                                        <label class="control-label col-md-2">>सब नोंदणी पदनाम :<span class="required">*</span></label>
                                                                                        <div class="col-md-2">
                                                                                            <asp:TextBox ID="DDLCCONCERNSUB" CssClass="form-control" PlaceHolder="CON_SUB.REG.DESIGA" TabIndex="20" runat="server" AutoPostBack="false"> </asp:TextBox>

                                                                                        </div>
                                                                                    </div>

                                                                                </div>
                                                                                <div></div>

                                                                                <div class="row" style="margin-bottom: 10px;">
                                                                                    <div class="row">
                                                                                        <div class="col-lg-12">
                                                                                            <div>
                                                                                                <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"></div>

                                                                                            </div>
                                                                                            <div class="col-lg-12"><strong style="color: #3598dc">मालमत्तेचे मागील ३ वर्षात झालेल्या व्यवहाराच्या विक्रीची किंमत </strong></div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                                                        <div class="col-lg-12">

                                                                                            <label class="control-label col-md-2">अ क.<span class="required">*</span></label>
                                                                                            <div class="col-md-2">
                                                                                                <asp:TextBox ID="TXTRSRNO" CssClass="form-control" TextMode="MultiLine" PlaceHolder="SR. NO." TabIndex="21" runat="server" AutoPostBack="false"> </asp:TextBox>
                                                                                            </div>
                                                                                            <label class="control-label col-md-1">सन :<span class="required">*</span></label>
                                                                                            <div class="col-md-1">
                                                                                                <asp:TextBox ID="TXTYEAR" CssClass="form-control" onkeypress="javascript:return isNumber (event)" PlaceHolder="YEAR" TabIndex="22" runat="server" AutoPostBack="false"> </asp:TextBox>

                                                                                            </div>
                                                                                            <label class="control-label col-md-2">तपशील प्रॉपर्टी: <span class="required">*</span></label>
                                                                                            <div class="col-md-3">
                                                                                                <asp:TextBox ID="TXTDETAILSP" TextMode="MultiLine" CssClass="form-control" PlaceHolder="DETAILS PROERTY" TabIndex="23" runat="server" AutoPostBack="false"> </asp:TextBox>

                                                                                            </div>
                                                                                            <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary" Text="Add" OnClick="btnAdd_Click" TabIndex="24" OnClientClick="Javascript:return IsValide()" />

                                                                                        </div>
                                                                                    </div>
                                                                                    <div></div>
                                                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                                                        <div class="col-lg-12">

                                                                                            <label class="control-label col-md-2">अ क. <span class="required">*</span></label>
                                                                                            <div class="col-md-2">
                                                                                                <asp:TextBox ID="TXTREGNOPR" CssClass="form-control" TextMode="MultiLine" PlaceHolder="REG.NO." TabIndex="24" runat="server" AutoPostBack="false"> </asp:TextBox>
                                                                                            </div>
                                                                                            <label class="control-label col-md-1">किंमत:<span class="required">*</span></label>
                                                                                            <div class="col-md-2">
                                                                                                <asp:TextBox ID="TXTPRICE" CssClass="form-control" onkeypress="javascript:return isNumber (event)" PlaceHolder="PRICE" TabIndex="25" runat="server" AutoPostBack="false"> </asp:TextBox>

                                                                                            </div>
                                                                                            <label class="control-label col-md-2">दर प्रति चौ/मिटर:  <span class="required">*</span></label>
                                                                                            <div class="col-md-2">
                                                                                                <asp:TextBox ID="TXTRATE" CssClass="form-control" onkeypress="javascript:return isNumber (event)" PlaceHolder="RATE FOR PER SQ. METER " TabIndex="26" runat="server" AutoPostBack="false"> </asp:TextBox>

                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div></div>
                                                                                    <div class="col-lg-12">
                                                                                        <div class="table-scrollable" style="border: none">
                                                                                            <table class="table table-striped table-bordered table-hover">
                                                                                                <thead>
                                                                                                    <tr>
                                                                                                        <th>
                                                                                                            <asp:GridView ID="Grdinfo" runat="server" AllowPaging="True"
                                                                                                                AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                                                                                EditRowStyle-BackColor="#FFFF99"
                                                                                                                PageIndex="20" PageSize="10"
                                                                                                                PagerStyle-CssClass="pgr" Width="98%" OnPageIndexChanging="Grdinfo_PageIndexChanging">
                                                                                                                <AlternatingRowStyle BackColor="White" />
                                                                                                                <Columns>
                                                                                                                    <asp:TemplateField HeaderText="SRNO">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="SRNO" runat="server" Text='<%# Eval("id") %>'></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
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

                                                                                                                    <asp:TemplateField HeaderText="DATE">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="NOTICE_ISS_DT1" runat="server" Text='<%# Eval("EDATE") %>'></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="YEAR">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="YEAR" runat="server" Text='<%# Eval("YEAR") %>'></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="REGNO">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:Label ID="REGNO" runat="server" Text='<%# Eval("DPREGNO") %>'></asp:Label>
                                                                                                                        </ItemTemplate>
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="Modify" Visible="true">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:LinkButton ID="lnkmd" runat="server" CommandArgument='<%#Eval("ID")+"_"+Eval("BRCD")+"_"+Eval("CASENO")+"_"+Eval("CASE_YEAR")%>' CommandName="select" OnClick="lnkmd_Click" class="glyphicon glyphicon-edit"></asp:LinkButton>
                                                                                                                        </ItemTemplate>
                                                                                                                        <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                                                                                                    </asp:TemplateField>
                                                                                                                    <asp:TemplateField HeaderText="Cancel1" Visible="true">
                                                                                                                        <ItemTemplate>
                                                                                                                            <asp:LinkButton ID="lnkdel" runat="server" CommandArgument='<%#Eval("ID")+"_"+Eval("BRCD")+"_"+Eval("CASENO")+"_"+Eval("CASE_YEAR")%>' CommandName="select" OnClick="lnkdel_Click" class="glyphicon glyphicon-trash"></asp:LinkButton>
                                                                                                                        </ItemTemplate>
                                                                                                                        <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                                                                                                    </asp:TemplateField>

                                                                                                                    <%-- <asp:TemplateField HeaderText="View" Visible="true">
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

                                                                        <div class="row" style="margin-bottom: 10px;">
                                                                            <div class="row">
                                                                                <div class="col-lg-12">

                                                                                    <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"></div>

                                                                                    <div class="col-lg-12"><strong style="color: #3598dc; margin-left: 25px;">पदाधिकाऱ्यांची माहिती:</strong></div>
                                                                                </div>
                                                                            </div>




                                                                            <div class="row" style="margin: 7px 0 7px 0">
                                                                                <div class="col-lg-12">

                                                                                    <label class="control-label col-md-2">प्रकार:</label>
                                                                                    <div class="col-md-2">
                                                                                        <asp:TextBox ID="txtType" CssClass="form-control" PlaceHolder="Type " TabIndex="26" runat="server" AutoPostBack="false"> </asp:TextBox>

                                                                                    </div>

                                                                                    <label class="control-label col-md-1">सदनिका क्र:</label>
                                                                                    <div class="col-md-2">
                                                                                        <asp:TextBox ID="txtHouseNoOfficer" CssClass="form-control" onkeypress="javascript:return isNumber (event)" PlaceHolder="House no" TabIndex="26" runat="server" AutoPostBack="false"> </asp:TextBox>

                                                                                    </div>

                                                                                    <label class="control-label col-md-1">मजला :</label>
                                                                                    <div class="col-md-1">
                                                                                        <asp:TextBox ID="txtFlat" CssClass="form-control" onkeypress="javascript:return isNumber (event)" PlaceHolder="Flat" TabIndex="26" runat="server" AutoPostBack="false"> </asp:TextBox>

                                                                                    </div>
                                                                                    <div class="col-md-1" style="margin-left: -5px">
                                                                                        <asp:Button runat="server" ID="btnmodifycomm" Text="modify" CssClass="btn btn-success" TabIndex="33" OnClick="btnmodifycomm_Click" Visible="false" />
                                                                                        <asp:Button runat="server" ID="btnAddComm" Text="ADD" CssClass="btn btn-success" TabIndex="33" OnClick="btnAddComm_Click" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>


                                                                            <div class="row" style="margin: 7px 0 7px 0">
                                                                                <div class="col-lg-12">

                                                                                    <label class="control-label col-md-2">पदनाम :</label>
                                                                                    <div class="col-md-2">
                                                                                        <asp:TextBox ID="txtDesignation" CssClass="form-control" PlaceHolder="Designation" TabIndex="26" runat="server" AutoPostBack="false"> </asp:TextBox>

                                                                                    </div>
                                                                                    <label class="control-label col-md-1">पत्ता :</label>
                                                                                    <div class="col-md-2">
                                                                                        <asp:TextBox ID="txtAddressOfficer" TextMode="MultiLine" CssClass="form-control" PlaceHolder="Address" TabIndex="26" runat="server" AutoPostBack="false"> </asp:TextBox>

                                                                                    </div>
                                                                                    <label class="control-label col-md-1">नाव :</label>
                                                                                    <div class="col-md-2">
                                                                                        <asp:TextBox ID="txtName" CssClass="form-control" PlaceHolder="Name" TabIndex="26" runat="server" AutoPostBack="false"> </asp:TextBox>

                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="row" runat="server" id="div_Grid1">
                                                                            <div class="col-lg-12">
                                                                                <div class="table-scrollable" style="border: none">
                                                                                    <table class="table table-striped table-bordered table-hover">
                                                                                        <thead>
                                                                                            <tr>
                                                                                                <th>
                                                                                                    <asp:GridView ID="GridViewCommit" runat="server" AllowPaging="True"
                                                                                                        AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                                                                        EditRowStyle-BackColor="#FFFF99"
                                                                                                        PageIndex="20" PageSize="10"
                                                                                                        PagerStyle-CssClass="pgr" Width="98%" OnPageIndexChanging="GridViewCommit_PageIndexChanging">
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
                                                                                                                    <asp:LinkButton ID="lnkcomm" runat="server" CommandArgument='<%#Eval("BRCD")+"_"+Eval("ID")+"_"+Eval("CASENO")+"_"+Eval("CASE_YEAR")%>' CommandName="select" OnClick="lnkcomm_Click" class="glyphicon glyphicon-edit"></asp:LinkButton>
                                                                                                                </ItemTemplate>
                                                                                                                <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderText="Cancle" Visible="true">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:LinkButton ID="lnlcancom" runat="server" CommandArgument='<%#Eval("BRCD")+"_"+Eval("ID")+"_"+Eval("CASENO")+"_"+Eval("CASE_YEAR")%>' CommandName="select" OnClick="lnlcancom_Click" class="glyphicon glyphicon-edit"></asp:LinkButton>
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


                                                                        <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"></div>

                                                                        <div class="row" style="margin: 7px 0 7px 0" id="Div_Submit" runat="server">
                                                                            <div class="col-lg-10">
                                                                                <div class="col-md-4">
                                                                                </div>
                                                                                <div class="col-md-6">
                                                                                    <asp:Button ID="BtnSubmit" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="BtnSubmit_Click" TabIndex="60" OnClientClick="Javascript:return IsValide()" />
                                                                                    <asp:Button ID="btnreport" runat="server" CssClass="btn btn-primary" Text="Report" OnClick="btnreport_Click" TabIndex="60" OnClientClick="Javascript:return IsValide()" />
                                                                                    <asp:Button ID="BtnClear" runat="server" CssClass="btn btn-primary" Text="Clear" OnClick="BtnClear_Click" TabIndex="61" />
                                                                                    <asp:Button ID="BtnExit" runat="server" CssClass="btn btn-primary" Text="Exit" OnClick="BtnExit_Click" TabIndex="62" />

                                                                                </div>
                                                                                <div class="col-md-5">
                                                                                </div>
                                                                            </div>
                                                                        </div>


                                                                    </div>


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
                                                                                                    <asp:TemplateField HeaderText="SRNO">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="SRNO" runat="server" Text='<%# Eval("id") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
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

                                                                                                    <%-- <asp:TemplateField HeaderText="DATE">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="NOTICE_ISS_DT1" runat="server" Text='<%# Eval("EDATE") %>'></asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>--%>

                                                                                                    <asp:TemplateField HeaderText="Modify" Visible="true">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:LinkButton ID="lnkSelect1" runat="server" CommandArgument='<%#Eval("ID")+"_"+Eval("BRCD")+"_"+Eval("CASENO")+"_"+Eval("CASE_YEAR")%>' CommandName="select" OnClick="lnkSelect1_Click" class="glyphicon glyphicon-edit"></asp:LinkButton>
                                                                                                        </ItemTemplate>
                                                                                                        <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Cancel1" Visible="true">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:LinkButton ID="lnkDelete1" runat="server" CommandArgument='<%#Eval("ID")+"_"+Eval("BRCD")+"_"+Eval("CASENO")+"_"+Eval("CASE_YEAR")%>' CommandName="select" OnClick="lnkDelete1_Click" class="glyphicon glyphicon-trash"></asp:LinkButton>
                                                                                                        </ItemTemplate>
                                                                                                        <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                                                                                    </asp:TemplateField>
                                                                                                    <%-- <asp:TemplateField HeaderText="Report" Visible="true">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:LinkButton ID="linReport" runat="server" CommandArgument='<%#Eval("BRCD")+"_"+Eval("CASENO")+"_"+Eval("CASE_YEAR")+"_"+Eval("EDATE")%>' CommandName="select" OnClick="linReport_Click" class="glyphicon glyphicon-plus"></asp:LinkButton>
                                                                                                            </ItemTemplate>
                                                                                                            <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                                                                                        </asp:TemplateField>--%>
                                                                                                    <%-- <asp:TemplateField HeaderText="View" Visible="true">
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
        <Triggers>
            <asp:PostBackTrigger ControlID="btnreport" />
         <%--   <asp:PostBackTrigger ControlID="ddlRCNo1" />--%>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

