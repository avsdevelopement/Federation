<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmProposalSale.aspx.cs" Inherits="FrmProposalSale" %>

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
                            Proposal Sale
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
                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-2">बीआरसीडी:<span class="required">*</span></label>
                                                            <div class="col-md-1">
                                                                <asp:TextBox ID="TxtBRCD" Placeholder="BRCD" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" TabIndex="1"></asp:TextBox>
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
                                                            </br>
                                                            <label class="control-label col-md-2">मराठी फॉन्ट:</label>
                                                            <div class="col-md-2">
                                                                <asp:DropDownList ID="ddlmarathi" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlmarathi_SelectedIndexChanged" CssClass="form-control" TabIndex="5">
                                                                    <asp:ListItem Text="--Select-" Value="0" />
                                                                    <asp:ListItem Text="Shivaji01" Value="1" />
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin: 7px 0 7px 0;">
                                                        <div class="col-lg-12">

                                                            <label class="control-label col-md-1">मालमत्ता प्रकार:</label>
                                                            <div class="col-md-3">
                                                                <asp:TextBox ID="txtProperty" Placeholder="Branch Name" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="6"></asp:TextBox>
                                                            </div>

                                                            <label class="control-label col-md-1">सदनिका क्र.</label>
                                                            <div class="col-md-3">
                                                                <asp:TextBox ID="txthouseno" Placeholder="House No." onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="7"></asp:TextBox>

                                                            </div>

                                                            <label class="control-label col-md-1">मजला :</label>
                                                            <div class="col-md-3">
                                                                <asp:TextBox ID="txtplot" Placeholder="Flat No." onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="7"></asp:TextBox>

                                                            </div>

                                                        </div>


                                                        <div class="row">
                                                            <div class="col-lg-12">
                                                                <label class="control-label col-md-1">थकबाकीदार क्र: </label>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="txtArrears" Placeholder=">Arrears no" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="7"></asp:TextBox>

                                                                </div>
                                                                <label class="control-label col-md-1">शाखेचे नाव:</label>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="txtbranch" Placeholder="Branch Name" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="6"></asp:TextBox>
                                                                </div>
                                                                <label class="control-label col-md-1">वार्ड नाव:</label>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="txtward" Placeholder="Ward Name" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="7"></asp:TextBox>

                                                                </div>


                                                            </div>
                                                        </div>



                                                        <div class="row">
                                                            <div class="col-lg-12">
                                                                <label class="control-label col-md-1">थकबाकीदाराचे नाव :</label>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="txtArrearsname" Placeholder=">Arrears name" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="7"></asp:TextBox>

                                                                </div>
                                                                <label class="control-label col-md-1">वर्तमानपत्रात जाहिरात दिलेली  तारीख :</label>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="txtnewspaperDate" Placeholder=">Ward Address" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="7"></asp:TextBox>
                                                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtnewspaperDate">
                                                                    </asp:TextBoxWatermarkExtender>
                                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtnewspaperDate">
                                                                    </asp:CalendarExtender>
                                                                </div>


                                                                <label class="control-label col-md-1">वाजवी किंमत मंजुरी पत्र संदर्भ क:</label>
                                                                <div class="col-md-3">
                                                                    <asp:TextBox ID="txtAmount" Placeholder=" Amount" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="7"></asp:TextBox>

                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-lg-12">
                                                                    <label class="control-label col-md-1">तारीख:</label>
                                                                    <div class="col-md-3">
                                                                        <asp:TextBox ID="txtDatee" Placeholder=">date" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="7"></asp:TextBox>
                                                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtDatee">
                                                                        </asp:TextBoxWatermarkExtender>
                                                                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtDatee">
                                                                        </asp:CalendarExtender>
                                                                    </div>



                                                                    <label class="control-label col-md-1">मंजुर वाजवी किंमत रु:</label>
                                                                    <div class="col-md-3">
                                                                        <asp:TextBox ID="txmanjuriAmt" Placeholder="manjuri Amount" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="7"></asp:TextBox>

                                                                    </div>

                                                                    <label class="control-label col-md-1">सर्वाधिक बोली रक्कम:</label>
                                                                    <div class="col-md-3">
                                                                        <asp:TextBox ID="txtbolliAmt" Placeholder="Bolli Amount" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="7"></asp:TextBox>

                                                                    </div>

                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-lg-12">

                                                                        <label class="control-label col-md-1">अंतीम बोली रक्कम:</label>
                                                                        <div class="col-md-3">
                                                                            <asp:TextBox ID="txtToatalAmt" Placeholder="Total  Amount" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="7"></asp:TextBox>

                                                                        </div>





                                                                        <label class="control-label col-md-1">सर्वाधिक बोलीधारकाचे नाव:</label>
                                                                        <div class="col-md-3">
                                                                            <asp:TextBox ID="txtbolidarak" Placeholder="" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="7"></asp:TextBox>

                                                                        </div>

                                                                    </div>
                                                            </div>

                                                            <div class="row">
                                                                <div class="col-lg-12">
                                                                    <label class="control-label col-md-1">लिलावचा रोजनामा तारीख: </label>
                                                                    <div class="col-md-3">
                                                                        <asp:TextBox ID="txtArrearnodate" Placeholder="Arrears no" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="7"></asp:TextBox>
                                                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtArrearnodate">
                                                                        </asp:TextBoxWatermarkExtender>
                                                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtArrearnodate">
                                                                        </asp:CalendarExtender>
                                                                    </div>



                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" style="margin: 7px 0 7px 0" id="Div_Submit" runat="server">
                                                            <div class="col-lg-10">
                                                                <div class="col-md-4">
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <asp:Button ID="BtnSubmit" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="BtnSubmit_Click" TabIndex="60" OnClientClick="Javascript:return IsValide()" />
                                                                    <asp:Button ID="BtnClear" runat="server" CssClass="btn btn-primary" Text="Clear" TabIndex="61" />
                                                                    <asp:Button ID="BtnExit" runat="server" CssClass="btn btn-primary" Text="Exit" TabIndex="62" />
                                                                    <asp:LinkButton ID="btnreport" runat="server" CssClass="btn btn-primary" Text="report" OnClick="btnreport_Click"></asp:LinkButton>
                                                                </div>
                                                                <div class="col-md-5">
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-12">
                                                            <div class="table-scrollable" style="border: none">
                                                                <table class="table table-striped table-bordered table-hover">
                                                                    <thead>
                                                                        <tr>
                                                                            <th>
                                                                                <asp:GridView ID="grdSale" runat="server" AllowPaging="True"
                                                                                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                                                    EditRowStyle-BackColor="#FFFF99"
                                                                                    PageIndex="20" PageSize="10"
                                                                                    PagerStyle-CssClass="pgr" Width="98%" OnPageIndexChanging="grdSale_PageIndexChanging">
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
                                                                                        <%--  <asp:TemplateField HeaderText="Report" Visible="true">
                                                                                        <ItemTemplate>
                                                                                            <asp:LinkButton ID="linReport" runat="server" CommandArgument='<%#Eval("BRCD")+"_"+Eval("CASENO")+"_"+Eval("CASE_YEAR")+"_"+Eval("EDATE")%>' CommandName="select" OnClick="linReport_Click" class="glyphicon glyphicon-plus"></asp:LinkButton>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

