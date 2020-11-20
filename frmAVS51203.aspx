<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="frmAVS51203.aspx.cs" Inherits="frmAVS51203" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">


    <%--  <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="500" SizeToReportContent = "true">
    </rsweb:ReportViewer>--%>
    <style>
        .gridview {
            font-family: Arial;
            background-color: #FFFFFF;
            Membe border: solid 1px #CCCCCC;
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
         <Triggers>
            <%--<asp:AsyncPostBackTrigger  ControlID="btnSubmit" EventName="Click" />--%>
            <asp:PostBackTrigger ControlID="lnkAdd" />
               <asp:PostBackTrigger ControlID="lnkDelete" />
        </Triggers>

        <ContentTemplate>
            <div class="col-md-12">
                <div class="portlet box green" id="Div1">
                    <div class="portlet-title">
                        <div class="caption">
                            Payment Mode
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
                                                        <asp:LinkButton ID="lnkAdd" runat="server" Text="a" class="btn btn-default" OnClick="lnkAdd_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;" TabIndex="1"><i class="fa fa-asterisk"></i>Reciept</asp:LinkButton>
                                                    </li>
                                                  <%--  <li>
                                                        <asp:LinkButton ID="lnkModify" runat="server" Text="VW" class="btn btn-default" OnClick="lnkModify_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;" Visible="false" TabIndex="2"><i class="fa fa-arrows"></i></asp:LinkButton>
                                                    </li>--%>
                                                     <li>
                                                <asp:LinkButton ID="lnkDelete" runat="server" Text="MD" class="btn btn-default" OnClick="lnkDelete_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;" TabIndex="3"><i class="fa fa-pencil-square-o"></i>Payment</asp:LinkButton>
                                            </li>
                                                    <%--
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
                                                            <div class="col-lg-12"><strong style="color: #3598dc">Payment Mode Detail's </strong></div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">
                                                        <label class="control-label col-md-2">BRCD:<span class="required">*</span></label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtBRCD" Enabled="true" Placeholder="BRCD" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" AutoPostBack="true" TabIndex="4"></asp:TextBox>
                                                        </div>
                                                        <%--<div class="col-md-3">
                                                                <asp:TextBox ID="TxtBRCDName" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>--%>
                                                        <label class="control-label col-md-1">Case Year:<span class="required">*</span></label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtCaseY" Enabled="true" Placeholder="YY-YY" onkeyup="Year(this)" runat="server" CssClass="form-control" onkeypress="javascript:return isNumber (event)" AutoPostBack="true" TabIndex="5"></asp:TextBox>
                                                        </div>
                                                        <label class="control-label col-md-1">Case No:<span class="required">*</span></label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtCaseNo" Enabled="true" Placeholder="Case No" OnTextChanged="txtCaseNo_TextChanged" runat="server" CssClass="form-control" onkeypress="javascript:return isNumber (event)" AutoPostBack="true" TabIndex="6"></asp:TextBox>
                                                        </div>


                                                        <%-- <label class="control-label col-md-1">Member No:<span class="required">*</span></label>
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="txtMember" CssClass="form-control" onkeypress="javascript:return isNumber (event)" PlaceHolder="Member No" TabIndex="4" runat="server" ></asp:TextBox>
                                                    </div>
                                                        --%>
                                                    </div>
                                                </div>

                                                 <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">
                                                        <label class="control-label col-md-2">BankGl Code:<span class="required">*</span></label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtBankGl" Enabled="true" Placeholder="BankGl Code" onkeypress="javascript:return isNumber (event)" OnTextChanged="txtBankGl_TextChanged" CssClass="form-control" runat="server" AutoPostBack="true" TabIndex="7"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-3">
                                                                <asp:TextBox ID="TxtBankName" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>

                                                        <label class="control-label col-md-1">Receipt No:<span class="required">*</span></label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtRecipt" Enabled="true" Placeholder="Case No" runat="server" OnTextChanged="txtRecipt_TextChanged" CssClass="form-control" onkeypress="javascript:return isNumber (event)" AutoPostBack="true" TabIndex="8"></asp:TextBox>
                                                        </div>
                                                                                                              
                                                    </div>
                                                </div>
                                                 <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">
                                                       
                                                        <label class="control-label col-md-2">Product Gl:<span class="required">*</span></label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtprcd" Enabled="true" Placeholder="Product Gl"  onkeypress="javascript:return isNumber (event)" OnTextChanged="txtprcd_TextChanged"  runat="server" CssClass="form-control"  AutoPostBack="true" TabIndex="9"></asp:TextBox>
                                                        </div>
                                                         <div class="col-md-3">
                                                                <asp:TextBox ID="txtprcdname" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                         <label class="control-label col-md-1">Date:<span class="required">*</span></label>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtentdate" AutoPostBack="true" TabIndex="10" Placeholder="DD/MM/YYYY" CssClass="form-control" runat="server" onkeyup="FormatIt(this)"  OnTextChanged="txtentdate_TextChanged" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender7" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtentdate">
                                                                </asp:TextBoxWatermarkExtender>
                                                                <asp:CalendarExtender ID="CalendarExtender7" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtentdate">
                                                                </asp:CalendarExtender>
                                                            </div>
                                                       
                                                       
                                                    </div>
                                                </div>


                                               
                                                <div runat="server" id="div12" visible="true">
                                                    <div class="row" style="margin: 7px 0 7px 0" runat="server" id="Div13">
                                                        <div class="col-lg-12" id="Div14">

                                                            <label class="control-label col-md-2">Payment Mode:</label>
                                                            <div class="col-md-2">
                                                                <asp:DropDownList ID="ddlPaymentMode" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPaymentMode_SelectedIndexChanged" CssClass="form-control" TabIndex="11"></asp:DropDownList>

                                                            </div>

                                                            <label class="control-label col-md-1">Amount</label>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtAmount" onkeydown="return CheckFirstChar(event.keyCode, this);" placeholder="Amount" CssClass="form-control" runat="server" TabIndex="12"></asp:TextBox>
                                                            </div>
                                                          
                                                             <label class="control-label col-md-1" id="divche1">Stage:</label>
                                                             <div class="col-md-2">
                                                                      <asp:DropDownList  ID="DDLSTAGE" runat="server" AutoPostBack="true" CssClass="form-control" TabIndex="13">                                                                                                                                               
                                                                     </asp:DropDownList>
                                                                 </div>

                                                                 </div>
                                                            
                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin: 7px 0 7px 0" runat="server" id="Div15" visible="false">
                                                        <div class="col-lg-12" id="Div16">
                                                            <label class="control-label col-md-2">Cheque No</label>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtChequeNo" onkeydown="return CheckFirstChar(event.keyCode, this);" placeholder="Cheque No" CssClass="form-control" runat="server" TabIndex="14"></asp:TextBox>
                                                            </div>
                                                             <label class="control-label col-md-1">Cheque Date :</label>
                                                      
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtChequeDate" placeholder="CHEQUE DATE" CssClass="form-control" onkeyup="FormatIt(this)" runat="server" TabIndex="15"></asp:TextBox>
                                                        </div>
                                                        </div>
                                                  

                                                </div>

                                                 <div class="row" style="margin: 7px 0 7px 0" id="bnkc1" visible="false">
                                                    <div class="col-lg-12" id="div22">
                                                       
                                                        <label class="control-label col-md-2" id="div23">Bank Code:<span class="required">*</span></label>
                                                        <div class="col-md-2" id="div24">
                                                            <asp:TextBox ID="txtbcd" Enabled="true" Placeholder="Bank Code"  onkeypress="javascript:return isNumber (event)" OnTextChanged="txtbcd_TextChanged"  runat="server" CssClass="form-control"  AutoPostBack="true" TabIndex="16"></asp:TextBox>
                                                        </div>
                                                         <div class="col-md-3" id="div25">
                                                                <asp:TextBox ID="txtbcdname" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtbcdname_TextChanged" TabIndex="17"></asp:TextBox>
                                                          <div id="Custlist2" style="height: 200px; overflow-y: scroll;"></div>
                                                        <asp:AutoCompleteExtender ID="AutoAccname" runat="server" TargetControlID="txtbcdname" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20" MinimumPrefixLength="1"
                                                            EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" ServiceMethod="GetBankName" CompletionListElementID="Custlist2">
                                                        </asp:AutoCompleteExtender>
   
                                                         </div>
                                                        <%-- <label class="control-label col-md-1">Branch Code:<span class="required">*</span></label>
                                                        <div class="col-md-1" id="div26">
                                                            <asp:TextBox ID="txtbrchcode" Enabled="true" Placeholder="Bank Code"  onkeypress="javascript:return isNumber (event)" OnTextChanged="txtbrchcode_TextChanged"  runat="server" CssClass="form-control"  AutoPostBack="true" TabIndex="2"></asp:TextBox>
                                                        </div>
                                                         <div class="col-md-3" id="div27">
                                                                <asp:TextBox ID="txtbrchname" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                         --%>
                                                       
                                                       
                                                    </div>
                                                </div>
                                                      <div class="row" style="margin: 7px 0 7px 0" runat="server" id="Div3" visible="false">
                                                        <div class="col-lg-12" id="Div4">
                                                            <label class="control-label col-md-2">Expence Mode:</label>
                                                            <div class="col-md-2">
                                                                <asp:DropDownList ID="ddlEXPENCE" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPaymentMode_SelectedIndexChanged" CssClass="form-control" TabIndex="18"></asp:DropDownList>

                                                            </div>
                                                            <%-- <label class="control-label col-md-1">Cheque Date :</label>
                                                      
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TextBox2" placeholder="CHEQUE DATE" CssClass="form-control" onkeyup="FormatIt(this)" runat="server"></asp:TextBox>
                                                        </div>--%>
                                                        </div>
                                                  

                                                </div>

                                                <div class="row" style="margin: 7px 0 7px 0" id="Div_Submit" runat="server" visible="true">
                                                    <div class="col-lg-10">
                                                        <div class="col-md-4">
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:Button ID="BtnRecipt" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="BtnRecipt_Click"  Visible="false" TabIndex="19" />
                                                             <asp:Button ID="btnSociety" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="btnSociety_Click"   Visible="false" TabIndex="20" />
                                                             <asp:Button ID="btnExpence" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="btnExpence_Click"   Visible="false" TabIndex="21" />

                                                            <asp:Button ID="BtnClear" runat="server" CssClass="btn btn-primary" Text="Clear" OnClick="BtnClear_Click" TabIndex="22" />
                                                            <asp:Button ID="BtnExit" runat="server" CssClass="btn btn-primary" Text="Exit" OnClick="BtnExit_Click" TabIndex="23" />
                                                              <asp:Button ID="BtnBack" runat="server" CssClass="btn btn-primary" Text="Back" OnClick="BtnBack_Click" TabIndex="24" />

                                                        </div>
                                                        <div class="col-md-5">
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

                   <%-- <div class="row" runat="server" id="div_Grid">
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
                                                     <asp:TemplateField HeaderText="AMOUNT">
                                                        <ItemTemplate>
                                                            <asp:Label ID="AMOUNT" runat="server" Text='<%# Eval("AMOUNT") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="PAYMENTMODE">
                                                        <ItemTemplate>
                                                            <asp:Label ID="PAYMENTMODE" runat="server" Text='<%# Eval("PAYMENTMODE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ENTRYDATE">
                                                        <ItemTemplate>
                                                            <asp:Label ID="ENTRYDATE" runat="server" Text='<%# Eval("ENTRYDATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="View" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkView" runat="server" CommandArgument='<%#Eval("BRCD")+"_"+Eval("CASENO")+"_"+Eval("CASE_YEAR")%>' CommandName="select" OnClick="lnkView_Click"  class="glyphicon glyphicon-check"></asp:LinkButton>
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
                </div>--%>
                 <div class="row" runat="server" id="div2">
                    <div class="col-lg-12">
                        <div class="table-scrollable" style="border: none">
                            <table class="table table-striped table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th>
                                            <asp:GridView ID="GridCAsh" runat="server" AllowPaging="True"
                                                AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" OnPageIndexChanging="GridCAsh_PageIndexChanging" OnSelectedIndexChanged="GridCAsh_SelectedIndexChanged"
                                                EditRowStyle-BackColor="#FFFF99" 
                                                    PageIndex="10" PageSize="10"
                                                PagerStyle-CssClass="pgr" Width="100%">
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

                                                    <asp:TemplateField HeaderText="SETNO">
                                                        <ItemTemplate>
                                                            <asp:Label ID="SETNO" runat="server" Text='<%# Eval("SETNO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                   <%--  <asp:TemplateField HeaderText="AMOUNT">
                                                        <ItemTemplate>
                                                            <asp:Label ID="AMOUNT1" runat="server" Text='<%# Eval("Dens") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                     <asp:TemplateField HeaderText="SCROLLNO">
                                                        <ItemTemplate>
                                                            <asp:Label ID="PAYMENTMODE1" runat="server" Text='<%# Eval("SCROLLNO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ENTRYDATE">
                                                        <ItemTemplate>
                                                            <asp:Label ID="ENTRYDATE1" runat="server" Text='<%# Eval("ENTRYDATE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                       <asp:TemplateField HeaderText="Reciept" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkView1" runat="server" CommandArgument='<%#Eval("BRCD")+"_"+Eval("CASENO")+"_"+Eval("CASE_YEAR")+"_"+Eval("SETNO")+"_"+Eval("SCROLLNO")+"_"+Eval("ENTRYDATE")%>' CommandName="select" OnClick="lnkView_Click"  class="glyphicon glyphicon-plus"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Modify" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkModify" runat="server" CommandArgument='<%#Eval("BRCD")+"_"+Eval("CASENO")+"_"+Eval("CASE_YEAR")+"_"+Eval("SETNO")+"_"+Eval("SCROLLNO")+"_"+Eval("ENTRYDATE")%>' CommandName="select" OnClick="lnkModify_Click1"  class="glyphicon glyphicon-edit"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                 

                                                      <asp:TemplateField HeaderText="Delete" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Eval("BRCD")+"_"+Eval("CASENO")+"_"+Eval("CASE_YEAR")+"_"+Eval("SETNO")+"_"+Eval("SCROLLNO")+"_"+Eval("ENTRYDATE")%>' CommandName="select" OnClick="lnkDelete_Click1"  class="glyphicon glyphicon-check"></asp:LinkButton>
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

