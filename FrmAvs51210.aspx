<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAvs51210.aspx.cs" Inherits="FrmAvs51210" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
    <div class="col-md-12">
        <div class="portlet box green" id="Div1">
            <div class="portlet-title">
                <div class="caption">
                    CASE STATUS
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

                                            <li>
                                                <asp:LinkButton ID="lnkAdd" runat="server" Text="a" class="btn btn-default" OnClick="lnkAdd_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-asterisk"></i>Add New</asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton ID="lnkModify" runat="server" Text="VW" class="btn btn-default" OnClick="lnkModify_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-arrows"></i>Modify</asp:LinkButton>
                                            </li>

                                            <li>
                                                <asp:LinkButton ID="lnkAuthorized" runat="server" Text="a" class="btn btn-default" OnClick="lnkAuthorized_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-arrows"></i>Authorise</asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton ID="lnkDelete" runat="server" Text="MD" class="btn btn-default" OnClick="lnkDelete_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-pencil-square-o"></i>Cancel</asp:LinkButton>
                                            </li>

                                            <li class="pull-right">
                                                <asp:Label ID="lblStst" runat="server" Text="Activity Perform :" Style="font-weight: bold;"></asp:Label>
                                                <asp:Label ID="lblActivity" runat="server" Text=""></asp:Label>
                                            </li>
                                        </ul>
                                    </div>
                                    <div style="border: 1px solid #3598dc">
                                        <div class="row" style="margin-bottom: 10px;">


                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">


                                                    <label class="control-label col-md-2">Case Year <span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtCaseY" MaxLength="5" CssClass="form-control" runat="server" Placeholder="YY-YY" onkeyup="Year(this)" onkeypress="javascript:return isNumber (event)" TabIndex="1"></asp:TextBox>
                                                    </div>
                                                    <label class="control-label col-md-3">Case No<span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtCaseNO" CssClass="form-control" Placeholder="Case No" OnTextChanged="txtCaseNO_TextChanged" runat="server" TabIndex="2" AutoPostBack="true" onkeypress="javascript:return isNumber (event)"></asp:TextBox>


                                                    </div>
                                                    <%--<label class="control-label col-md-2">Date:<span class="required">*</span></label>

                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtDate" TabIndex="4" Placeholder="DD/MM/YYYY" CssClass="form-control" runat="server" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender8" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtDate">
                                                        </asp:TextBoxWatermarkExtender>
                                                        <asp:CalendarExtender ID="CalendarExtender8" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtDate">
                                                        </asp:CalendarExtender>
                                                    </div>--%>
                                                </div>
                                            </div>

                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">


                                                    <label class="control-label col-md-2">Date:<span class="required">*</span></label>

                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtDate" TabIndex="3" Placeholder="DD/MM/YYYY" CssClass="form-control" runat="server" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                        <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender8" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtDate">
                                                        </asp:TextBoxWatermarkExtender>
                                                        <asp:CalendarExtender ID="CalendarExtender8" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtDate">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                    <label class="control-label col-md-3">Society Type & MemberNo:<span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtmem" CssClass="form-control" AutoPostBack="true" onkeypress="javascript:return isNumber (event)" PlaceHolder="Society Type & MemberNo" TabIndex="4" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                   
                                                    <label class="control-label col-md-2">Society Name<span class="required">*</span></label>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtSocietyName" Placeholder="Society Name" Width="318px" onkeydown="return CheckFirstChar(event.keyCode, this);" onkeypress="javascript:return OnltAlphabets(event)" CssClass="form-control" runat="server" TabIndex="5" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                    <label class="control-label col-md-2">Society Address<span class="required">*</span></label>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtSociadd" Placeholder="Society Address" TextMode="MultiLine" onkeydown="return CheckFirstChar(event.keyCode, this);" onkeypress="javascript:return OnltAlphabets(event)" CssClass="form-control" runat="server" TabIndex="6" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                </div>



                                               
                                            </div>
                                            <div id="Div5" runat="server" class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Defaulter Name<span class="required">*</span></label>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtdefaltername" Width="318px" Placeholder="Defaulter Name" onkeydown="return CheckFirstChar(event.keyCode, this);" onkeypress="javascript:return OnltAlphabets(event)" CssClass="form-control" runat="server" TabIndex="7" AutoPostBack="true"></asp:TextBox>
                                                    </div>

                                                </div>
                                            </div>

                                            <div id="Div2" runat="server" class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">City<span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtCity" Placeholder="City" CssClass="form-control" runat="server" TabIndex="8" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                    <label class="control-label col-md-3">Pincode<span class="required">*</span></label>

                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtPincode" CssClass="form-control" TabIndex="9" runat="server" MaxLength="6" onkeypress="javascript:return isNumber(event)" placeholder="Pincode"></asp:TextBox>

                                                    </div>

                                                </div>
                                            </div>
                                            <div id="Div3" runat="server" class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">R.C.NO.<span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtrcno" onkeypress="javascript:return isNumber (event)" TabIndex="10" Placeholder="Recovery Officer No" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                    <label class="control-label col-md-3">R.C.DATE<span class="required">*</span></label>

                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtMovDate" CssClass="form-control" TabIndex="11" runat="server" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber(event)" placeholder="dd/mm/yyyy"></asp:TextBox>
                                                        <asp:TextBoxWatermarkExtender ID="txtnextfileWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtMovDate">
                                                        </asp:TextBoxWatermarkExtender>
                                                        <asp:CalendarExtender ID="txtnextfile_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtMovDate">
                                                        </asp:CalendarExtender>
                                                    </div>

                                                </div>
                                            </div>
                                            <div id="Div4" runat="server" class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <%--<label class="control-label col-md-2">Decretal Name<span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtDecretalName" Placeholder="Decretal Name" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="12" AutoPostBack="true"></asp:TextBox>
                                                    </div>--%>
                                                    <label class="control-label col-md-2" >Decretal Amount<span class="required">*</span></label>
                                                    <div class="col-md-2" style="margin-right:90px;">
                                                        <asp:TextBox ID="txtDecretalAmount" Placeholder="Decretal Amount" onkeydown="return CheckFirstChar(event.keyCode, this);" CssClass="form-control" runat="server" TabIndex="13" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                    <label class="control-label col-md-2" >Case Status<span class="required">*</span></label>
                                                    <div class="col-md-1">
                                                        <%--<asp:DropDownList ID="ddlstatus" runat="server" CssClass="form-control" TabIndex="5"  AutoPostBack="true" EnableViewState="true"></asp:DropDownList>--%>
                                                        <asp:TextBox ID="txtCaseStatus" runat="server" placeholder="Case Status" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtCaseStatus_TextChanged" TabIndex="14" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <%--<asp:DropDownList ID="ddlstatus" runat="server" CssClass="form-control" TabIndex="5"  AutoPostBack="true" EnableViewState="true"></asp:DropDownList>--%>
                                                        <asp:TextBox ID="txtCaseStatusName" runat="server" placeholder="Case Status Name" TabIndex="15" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="margin: 7px 0 7px 0">
                                    <div class="col-lg-10">
                                        <div class="col-md-4">
                                        </div>
                                        <div class="col-md-6">
                                            <asp:Button ID="BtnSubmit" runat="server" CssClass="btn btn-primary" Text="Submit" TabIndex="15" OnClick="BtnSubmit_Click" OnClientClick="Javascript:return IsValide()" />
                                            <asp:Button ID="BtnClear" runat="server" CssClass="btn btn-primary" Text="Clear" TabIndex="16" OnClick="BtnClear_Click" />
                                            <asp:Button ID="BtnExit" runat="server" CssClass="btn btn-primary" OnClick="BtnExit_Click" TabIndex="17" Text="Exit" />
                                        </div>
                                        <div class="row" >
                                            <div class="col-lg-12" style="width:119%">
                                                <div class="table-scrollable" style="border: none">
                                                    <table class="table table-striped table-bordered table-hover">
                                                        <thead>
                                                            <tr>
                                                                <th >
                                                                    <asp:GridView ID="GridCase" runat="server" AllowPaging="True"
                                                                        AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                                        EditRowStyle-BackColor="LightBlue"
                                                                        PageIndex="10" PageSize="10" 
                                                                        PagerStyle-CssClass="pgr" >
                                                                        <HeaderStyle Font-Bold="true"  BackColor="LightBlue" HorizontalAlign="Center" />
                                                                        <AlternatingRowStyle  />
                                                                        <Columns>
                                                                            <%--<asp:TemplateField>
                                                                                <HeaderStyle BackColor="LightGray" ForeColor="DarkBlue" runat="server"/>

                                                                            </asp:TemplateField>--%>
                                                                            <asp:TemplateField HeaderText="Branch Code" >
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="BRCD1" runat="server" Text='<%# Eval("BRCD") %>' Style="padding:50px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            

                                                                            <asp:TemplateField HeaderText="CASEYEAR">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="CASEYEAR1" runat="server" Text='<%# Eval("CASE_YEAR") %>' Style="padding:50px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Case No">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="CASENO1" runat="server" Text='<%# Eval("CASENO") %>' Style="padding:50px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="APPLICTIONDATE">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="APPLICTIONDATE1" runat="server" Text='<%# Eval("APPLICTIONDATE","{0:dd/MM/yyyy}") %>' Style="padding:50px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                             <asp:TemplateField HeaderText="SOCIETYNAME">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="SOCIETYNAME1" runat="server" Text='<%# Eval("SOCIETYNAME") %>' Style="padding:50px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <%--<asp:TemplateField HeaderText="SOCIETYADDRESS">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="SOCIETYADDRESS1" runat="server" Text='<%# Eval("SOCIETYADDRESS") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>--%>
                                                                            <%--<asp:TemplateField HeaderText="CITY">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="CITY1" runat="server" Text='<%# Eval("CITY") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="PINCODE">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="PINCODE1" runat="server" Text='<%# Eval("PINCODE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>--%>
                                                                            <asp:TemplateField HeaderText="RCNO">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="RCNO1" runat="server" Text='<%# Eval("RCNO") %>' Style="padding:50px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="RCDATE">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="RCDATE1" runat="server" Text='<%# Eval("RCDATE") %>' Style="padding:50px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                           <%-- <asp:TemplateField HeaderText="DEFAULTERNAME">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="DEFAULTERNAME" runat="server" Text='<%# Eval("DEFAULTERNAME") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>--%>
                                                                            <%--<asp:TemplateField HeaderText="DECRETALAMOUNT">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="DECRETALAMOUNT1" runat="server" Text='<%# Eval("DECRETALAMOUNT") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>--%>
                                                                            <asp:TemplateField HeaderText="CASESTATUS">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="CASESTATUS1" runat="server" Text='<%# Eval("CASESTATUS") %>' Style="padding:50px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="STAGE">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="STAGE1" runat="server" Text='<%# Eval("STAGE") %>' Style="padding:50px"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            


                                                                            <%--<asp:TemplateField HeaderText="View" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkView1" runat="server" CommandArgument='<%#Eval("BRCD")+"_"+Eval("CASENO")+"_"+Eval("CASE_YEAR")%>' CommandName="select" ></asp:LinkButton>
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
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

