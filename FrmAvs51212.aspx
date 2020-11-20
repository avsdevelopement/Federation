<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAvs51212.aspx.cs" Inherits="FrmAvs51212" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

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
             if (obj.value.length == 4) //DAY
                 obj.value = obj.value;
             obj.value = obj.value;

         }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="upComplaintActions" runat ="server"   UpdateMode="Conditional" >
    <ContentTemplate >


      <div class="col-md-12">
        <div class="portlet box green" id="Div1">
            <div class="portlet-title">
                <div class="caption">
                   SRO Monthly Report
                </div>
            </div>
            <div class="portlet-body form">
                <div class="form-horizontal">
                    <div class="form-wizard">
                        <div class="form-body">
                            <div class="tab-content">
                                <div class="tab-pane active" id="tab1">
                                   
                                    <div style="border: 1px solid #3598dc">
                                        <div class="row" style="margin-bottom: 10px;">


                                         <%--   <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">

                                                    <label class="control-label col-md-3">Month<span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtCaseNO" CssClass="form-control" MaxLength="2" Placeholder="MM"  runat="server" TabIndex="5" AutoPostBack="true" onkeypress="javascript:return isNumber (event)"></asp:TextBox>


                                                    </div>
                                                    <label class="control-label col-md-2">Year <span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtCaseY" MaxLength="4" CssClass="form-control" runat="server" Placeholder="YYYY" onkeyup="Year(this)" onkeypress="javascript:return isNumber (event)" TabIndex="3"></asp:TextBox>
                                                    </div>
                                                    
                                                      </div>
                                                </div>--%>
                                            <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <div class="col-md-2">
                                                                <label class="control-label col-md-2"><span class="required"></span></label>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:RadioButton ID="rbtSep" runat="server" Text="Specific" GroupName="YN" name="minor" OnCheckedChanged="rbtSep_CheckedChanged" AutoPostBack="true" TabIndex="1" />
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:RadioButton ID="rdbAll" runat="server" Text="All" GroupName="YN" name="minor" OnCheckedChanged="rdbAll_CheckedChanged" AutoPostBack="true" TabIndex="2" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                            <div class="row" style="margin: 7px 0 7px 0 ;padding-top:20px">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-2">From Srno:<span class="required">*</span></label>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtFsro" TabIndex="3" Placeholder="From Srno" CssClass="form-control" runat="server" Enabled="false"  onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                               
                                                            </div>
                                                            <label class="control-label col-md-2">To Srno:<span class="required">*</span></label>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtTsro" TabIndex="4" Placeholder="To Srno" CssClass="form-control" runat="server" Enabled="false"  onkeypress="javascript:return isNumber (event)" AutoPostBack="true" ></asp:TextBox>
                                                                
                                                            </div>
                                                             <label class="control-label col-md-1">CaseStatus:<span class="required">*</span></label>
                                                <div class="col-md-2">
                                                     <asp:DropDownList ID="ddlcasestaus" runat="server" CssClass="form-control"  TabIndex="5"></asp:DropDownList>
                                                  
                                                           </div>
                                        </div>
                                                </div>

                                               <div class="row" style="margin: 7px 0 7px 0">
                                                        <div class="col-lg-12">
                                                            <label class="control-label col-md-2">From Date:<span class="required">*</span></label>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtFromDate" TabIndex="5" Placeholder="DD/MM/YYYY" CssClass="form-control" runat="server" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender7" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtFromDate">
                                                                </asp:TextBoxWatermarkExtender>
                                                                <asp:CalendarExtender ID="CalendarExtender7" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtFromDate">
                                                                </asp:CalendarExtender>
                                                            </div>
                                                            <label class="control-label col-md-2">To Date:<span class="required">*</span></label>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="txtToDate" TabIndex="6" Placeholder="DD/MM/YYYY" CssClass="form-control" runat="server" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" AutoPostBack="true" ></asp:TextBox>
                                                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender8" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtToDate">
                                                                </asp:TextBoxWatermarkExtender>
                                                                <asp:CalendarExtender ID="CalendarExtender8" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtToDate">
                                                                </asp:CalendarExtender>
                                                            </div>
                                                            <label class="control-label col-md-1" id="divche1">Stage:</label>
                                                             <div class="col-md-2">
                                                                      <asp:DropDownList  ID="DDLSTAGE" runat="server" AutoPostBack="true" CssClass="form-control" TabIndex="13">                                                                                                                                               
                                                                     </asp:DropDownList>
                                                                 </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="margin: 7px 0 7px 0">
                                    <div class="col-lg-12">
                                        <div class="col-lg-1"> </div>
                                        
                                        <div class="col-md-10">
                                            <asp:Button ID="BTNMONTHLY" runat="server" CssClass="btn btn-primary" OnClick="BTNMONTHLY_Click" Text="MONTHLYSRO_Report" TabIndex="7" OnClientClick="Javascript:return IsValide()" />

                                            <asp:Button ID="btnsrorpt" runat="server" CssClass="btn btn-primary" OnClick="btnsrorpt_Click" Text="SUMMARYSRO_Report" TabIndex="7" OnClientClick="Javascript:return IsValide()" />
                                            <asp:Button ID="BtnSubmit" runat="server" CssClass="btn btn-primary" OnClick="BtnSubmit_Click" Text="SUMMARY_Report" TabIndex="7" OnClientClick="Javascript:return IsValide()" />
                                             <asp:Button ID="BTNOCASEPT" runat="server" CssClass="btn btn-primary" Text="NO_OF_CASE_REPORT" OnClick="BTNOCASEPT_Click" TabIndex="22" />
                                              <asp:Button ID="BtnClear" runat="server" CssClass="btn btn-primary"  OnClick="BtnClear_Click" Text="Clear"  TabIndex="8" />
                                         
                                                 </div>
                                       
                                        <%--<div class="col-md-12">
                                             <asp:Button ID="BTNACTION" runat="server" CssClass="btn btn-primary" Text="ACTION_Download" OnClick="BTNACTION_Click" TabIndex="22" />
                             
                                             <asp:Button ID="btnExecution" runat="server" CssClass="btn btn-primary" Text="EXECUTION_REPORT" OnClick="btnExecution_Click" TabIndex="22" />
                                             <asp:Button ID="btncost" runat="server" CssClass="btn btn-primary" Text="COSTOFPROCESS_REPORT" OnClick="btncost_Click" TabIndex="22" />
                                     
                                            <asp:Button ID="BtnClear" runat="server" CssClass="btn btn-primary"  OnClick="BtnClear_Click" Text="Clear"  TabIndex="8" />
                                            <asp:Button ID="BtnExit" runat="server" CssClass="btn btn-primary" Text="Exit" OnClick="BtnExit_Click" TabIndex="9" />
                                     
                                        </div>--%>

                                    </div>
                                </div>
                                         <div class="row" style="margin: 7px 0 7px 0">
                                    <div class="col-lg-12">
                                        <div class="col-lg-1" ></div>
                                      
                                       <div class="col-md-10">
                                              <asp:Button ID="BtnDownloadCASE" runat="server" CssClass="btn btn-primary" Text="CASESTATUS_REPORT" OnClick="BtnDownloadCASE_Click" TabIndex="22" />
                             
                                             <asp:Button ID="BTNACTION" runat="server" CssClass="btn btn-primary" Text="ACTION_REPORT" OnClick="BTNACTION_Click" TabIndex="22" />
                             
                                             <asp:Button ID="btnExecution" runat="server" CssClass="btn btn-primary" Text="EXECUTION_REPORT" OnClick="btnExecution_Click" TabIndex="22" />
                                             <asp:Button ID="btncost" runat="server" CssClass="btn btn-primary" Text="COSTOFPROCESS_REPORT" OnClick="btncost_Click" TabIndex="22" />
                                     
                                             <asp:Button ID="BtnExit" runat="server" CssClass="btn btn-primary" Text="Exit" OnClick="BtnExit_Click" TabIndex="9" />
                                     
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
            </ContentTemplate>
    <Triggers >
   
          <%--<asp:PostBackTrigger ControlID="btnExportToExcel" EventName="btnExportToExcel_Click" /> --%>
          <asp:PostBackTrigger ControlID ="BtnDownloadCASE" />
          <asp:PostBackTrigger ControlID ="BTNACTION" />
          <asp:PostBackTrigger ControlID ="btnExecution" />
          <asp:PostBackTrigger ControlID ="btncost" />
          <asp:PostBackTrigger ControlID ="BTNOCASEPT" />
    </Triggers>
</asp:UpdatePanel>
</asp:Content>