<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAVS51187.aspx.cs" Inherits="FrmAVS51187" %>

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
                            CASE STATUS MASTER
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

                                                

                                                    <li class="pull-right">
                                                        <asp:Label ID="lblStst" runat="server" Text="Activity Perform :" Style="font-weight: bold;"></asp:Label>
                                                        <asp:Label ID="lblActivity" runat="server" Text=""></asp:Label>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div style="border: 1px solid #3598dc">
                                                <div class="row">
                                                        <div class="col-lg-12">
                                                            <div class="col-lg-12"><strong style="color: #3598dc">Case Status Detail's : </strong></div>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                              <label class="control-label col-md-2">Case Year:<span class="required">*</span></label>
                                                            <div class="col-md-1">
                                                                <asp:TextBox ID="txtCaseY" Placeholder="YY-YY" onkeyup="Year(this)" runat="server" CssClass="form-control" onkeypress="javascript:return isNumber (event)"  TabIndex="1"></asp:TextBox>
                                                            </div>
                                                            <label class="control-label col-md-2">Case No:<span class="required">*</span></label>
                                                            <div class="col-md-1">
                                                                <asp:TextBox ID="tctCaseNo" OnTextChanged="tctCaseNo_TextChanged" Placeholder="Case No" runat="server" CssClass="form-control" onkeypress="javascript:return isNumber (event)" AutoPostBack="true" TabIndex="2"></asp:TextBox>
                                                            </div>

                                                          
                                                             <label class="control-label col-md-1">Stage<span class="required">*</span></label>
                                                            <div class="col-md-1">
                                                                <asp:TextBox ID="txtstage" Placeholder="Stage"  runat="server" CssClass="form-control" onkeypress="javascript:return isNumber (event)"  TabIndex="3"></asp:TextBox>
                                                            </div>

                                                            <label class="control-label col-md-1">Date:<span class="required">*</span></label>

                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtDate" TabIndex="4" Placeholder="DD/MM/YYYY" CssClass="form-control" runat="server" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" ></asp:TextBox>
                                                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender8" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtDate">
                                                                </asp:TextBoxWatermarkExtender>
                                                                <asp:CalendarExtender ID="CalendarExtender8" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtDate">
                                                                </asp:CalendarExtender>
                                                            </div>



                                                        </div>
                                                    </div>

                                                <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">

                                                            <label class="control-label col-md-2">Status:<span class="required">*</span></label>

                                                            <div class="col-md-2">
                                                                <asp:DropDownList ID="ddlstatus" runat="server" CssClass="form-control" TabIndex="5" OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged" AutoPostBack="true" EnableViewState="true"></asp:DropDownList>
                                                            </div>
                                                            <label class="control-label col-md-1">Remark<span class="required">*</span></label>
                                                            <div class="col-md-3">
                                                                <asp:TextBox ID="txtRemark"  runat="server" CssClass="form-control"   TabIndex="6"></asp:TextBox>
                                                            </div>




                                                        </div>
                                                    </div>
                                                 <div class="row">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"></div>
                                                        <div class="col-md-4"><strong style="color: #3598dc">Payment  Detail's : </strong></div>

                                                    </div>
                                                </div>
                                                  <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">
                                                        <label class="control-label col-md-2">Amount:</label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtAwardAmt" TabIndex="7" Placeholder="Award Amount:" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                        <label class="control-label col-md-2">Date:<span class="required">*</span></label>

                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtamtdate" TabIndex="8" Placeholder="DD/MM/YYYY" CssClass="form-control" runat="server" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" ></asp:TextBox>
                                                            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtamtdate">
                                                            </asp:TextBoxWatermarkExtender>
                                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtamtdate">
                                                            </asp:CalendarExtender>
                                                        </div>


                                                    </div>
                                                     <div class="col-md-12"></div>
                                                </div>
                                                 <div class="row">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"></div>
                                                        <div class="col-md-4"><strong style="color: #3598dc">Attachment Date: </strong></div>

                                                    </div>
                                                </div>
                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">
                                                        <label class="control-label col-md-2">BankAccount Attachment Date:</label>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="TxtSAODT" TabIndex="9" CssClass="form-control" type="text" PlaceHolder="dd/mm/yyyy" runat="server" onkeyup="FormatIt(this)"></asp:TextBox>
                                                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtSAODT">
                                                                </asp:TextBoxWatermarkExtender>
                                                                <asp:CalendarExtender ID="CalendarExtender5" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtSAODT">
                                                                </asp:CalendarExtender>
                                                            </div>
                                                      <label class="control-label col-md-2">Immuvable Attachment Date</label>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtImmuvableDate" TabIndex="10" Placeholder="DD/MM/YYYY" CssClass="form-control" runat="server" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                                <asp:TextBoxWatermarkExtender ID="txtImmuvableDateExtender6" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtImmuvableDate">
                                                                </asp:TextBoxWatermarkExtender>
                                                                <asp:CalendarExtender ID="txtImmuvableDateCalendarExtender6" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtImmuvableDate">
                                                                </asp:CalendarExtender>
                                                            </div>

                                                            <label class="control-label col-md-2">Movable Attachment Date:</label>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtMovable" TabIndex="11" Placeholder="DD/MM/YYYY" CssClass="form-control" runat="server" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtMovable">
                                                                </asp:TextBoxWatermarkExtender>
                                                                <asp:CalendarExtender ID="CalendarExtender6" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtMovable">
                                                                </asp:CalendarExtender>


                                                    </div>
                                                </div>
                                            

                                        </div>

                                                </div>
                                               
                                        <div class="row" style="margin: 7px 0 7px 0" id="Div_Submit" runat="server" visible="true">
                                            <div class="col-lg-10">
                                                <div class="col-md-4">
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:Button ID="BtnSubmit" runat="server" CssClass="btn btn-primary" Text="Submit"  OnClick="BtnSubmit_Click"  TabIndex="52" OnClientClick="Javascript:return IsValide()" />
                                                    <asp:Button ID="BtnClear" runat="server" CssClass="btn btn-primary" Text="Clear"  TabIndex="53" />
                                                    <asp:Button ID="BtnExit" runat="server" CssClass="btn btn-primary" Text="Exit"  TabIndex="54" />
                                                </div>
                                                <div class="col-md-5">
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
                                                PagerStyle-CssClass="pgr" Width="100%">
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

                                                    <asp:TemplateField HeaderText="CASE_STATUS">
                                                        <ItemTemplate>
                                                            <asp:Label ID="CASEYAER" runat="server" Text='<%# Eval("CASE_STATUS") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="LOGINCODE">
                                                        <ItemTemplate>
                                                            <asp:Label ID="CASEYAER" runat="server" Text='<%# Eval("MID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    

                                                 

                                                    <asp:TemplateField HeaderText="Modify" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkModify" runat="server" CommandArgument='<%#Eval("CASENO")+"_"+Eval("CASE_YEAR")%>' CommandName="select" OnClick="lnkModify_Click" class="glyphicon glyphicon-edit"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cancel" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Eval("CASENO")+"_"+Eval("CASE_YEAR")%>' CommandName="select" OnClick="lnkDelete_Click" class="glyphicon glyphicon-trash"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Authorize" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkAuthorize" runat="server" CommandArgument='<%#Eval("CASENO")+"_"+Eval("CASE_YEAR")%>' CommandName="select" OnClick="lnkAuthorize_Click" class="glyphicon glyphicon-check"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="View" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkView" runat="server" CommandArgument='<%#Eval("CASENO")+"_"+Eval("CASE_YEAR")%>' CommandName="select" OnClick="lnkView_Click" class="glyphicon glyphicon-check"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="Th" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                        <FooterStyle BackColor="#ccffcc" Font-Bold="True" ForeColor="Black" HorizontalAlign="Right" BorderStyle="None" />
                                  
                                                <PagerStyle CssClass="pgr" />
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
    </asp:UpdatePanel>
</asp:Content>

