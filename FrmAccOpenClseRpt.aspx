<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAccOpenClseRpt.aspx.cs" Inherits="FrmAccOpenClseRpt" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
        function ShowCalender() {
            var pid = '<%=Request.QueryString["P_ID"] %>';
            if (pid == '') {
                var d = new Date();
                $('#txtfromdate').val(d.getFullYear() + '-' + dateLen(d.getMonth() + 1) + '-' + dateLen(d.getDate()));
                $('#txttodate').val(d.getFullYear() + '-' + dateLen(d.getMonth() + 1) + '-' + dateLen(d.getDate()));
                //$("#txtNoOfDays").val(1);
            }

            myCalendar = new dhtmlXCalendarObject(["txtfromdate"]);
            myCalendar1 = new dhtmlXCalendarObject(["txttodate"]);
            //myCalendar2 = new dhtmlXCalendarObject(["txtAppDate"]);

            //myCalendar2.setSkin('omega');
            //myCalendar2.setDateFormat("%Y-%m-%d");
            //myCalendar2.hideTime();

            myCalendar.setSkin('omega');
            myCalendar.setDateFormat("%Y-%m-%d");
            myCalendar.hideTime();



            myCalendar.attachEvent("onClick", function (d) {
                //                var d = new Date();
                //                var str = d.getDay() + '-' + d.getMonth() + '-' + d.getFullYear();
                document.getElementById('txttodate').value = d.getFullYear() + '-' + dateLen((d.getMonth() + 1)) + '-' + dateLen(d.getDate());
                //document.getElementById('txtNoOfDays').value = 1;
                myCalendar1.setSensitiveRange(document.getElementById('txtfromdate').value, null);
                NoOfDays();
            });


            myCalendar1.setSkin('omega');
            myCalendar1.setDateFormat("%Y-%m-%d");
            myCalendar1.hideTime();
            myCalendar1.setSensitiveRange(document.getElementById('txtfromdate').value, null);

            //myCalendar.onclick = {NoOfDays();};

            myCalendar1.attachEvent("onClick", function (d) {
                NoOfDays();
            });
        }

        function dateLen(dt) {
            var dt1 = dt + '';
            if (dt1.length == 1)
                dt1 = '0' + dt;

            return dt1;
        }
        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="col-md-12">
        <div class="portlet box green" id="Div1">
            <div class="portlet-title">
                <div class="caption">
                    Account Open Close Report
                </div>
            </div>
            <div class="portlet-body form">
                <div class="form-horizontal">
                    <div class="form-wizard">
                        <div class="form-body">
                            <div class="tab-content">
                                <div class="tab-pane active" id="tab1">
                                    <div class="row" style="margin-bottom: 10px;">
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">From Date:<span class="required">*</span></label>
                                                <div class="col-md-2">
                                                            <asp:TextBox ID="txtfromdate" CssClass="form-control" type="text" PlaceHolder="dd/mm/yyyy" runat="server" onkeyup="FormatIt(this);ShowCalender()"></asp:TextBox>
                                                             <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtfromdate">
                                                            </asp:TextBoxWatermarkExtender>
                                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtfromdate">
                                                            </asp:CalendarExtender>
                                                        </div>
                                                <div class="col-md-2">
                                                    </div>
                                                  <label class="control-label col-md-2">To Date:<span class="required">*</span></label>
                                                <div class="col-md-2">
                                                            <asp:TextBox ID="txttodate" type="text" PlaceHolder="dd/mm/yyyy" onkeyup="FormatIt(this)" OnTextChanged="txttodate_TextChanged" AutoPostBack="true" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:TextBoxWatermarkExtender ID="TxtDate_WatermarkExtender2" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txttodate">
                                                            </asp:TextBoxWatermarkExtender>
                                                            <asp:CalendarExtender ID="TxtDate_CalendarExtender2" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txttodate">
                                                            </asp:CalendarExtender>
                                                        </div>
                                                <div class="col-md-2">
                                                    </div>
                                                    </div>
                                                </div>
                                       
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">BRCD:<span class="required">*</span></label>
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="TxtFBRCD" Placeholder="BRCD" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" OnTextChanged="TxtFBRCD_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtFBRCDName" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-2">BRCD:<span class="required">*</span></label>
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="TxtTBRCD" Placeholder="BRCD" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" OnTextChanged="TxtTBRCD_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtTBRCDName" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Prod Code:<span class="required">*</span></label>
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="TxtProdCode" Placeholder="Prod Code" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" OnTextChanged="TxtProdCode_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                                 <div class="col-md-3">
                                                    <asp:TextBox ID="TxtProdName" CssClass="form-control" runat="server" OnTextChanged="TxtProdName_TextChanged"></asp:TextBox>
                                                     <div id="FirstProdList" style="height: 200px; overflow-y:scroll;"></div>
                                                    <asp:AutoCompleteExtender ID="autoFglname" runat="server" TargetControlID="TxtProdName"
                                                        UseContextKey="true"
                                                        CompletionInterval="1"
                                                        CompletionSetCount="20"
                                                        MinimumPrefixLength="1"
                                                        EnableCaching="true"
                                                        ServicePath="~/WebServices/Contact.asmx"
                                                        ServiceMethod="getglname" CompletionListElementID="FirstProdList">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                            </div>
                                        </div>

                                         <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Type : <span class="required">* </span></label>
                                                <div class="col-md-8">
                                                <asp:RadioButtonList ID="RblType" runat="server" AutoPostBack="true" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="Open" Value="0" style="margin: 5px;"></asp:ListItem>
                                                    <asp:ListItem Text="Close" Value="1" style="margin: 5px;"></asp:ListItem>
                                                </asp:RadioButtonList>
                                                    </div>
                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-10">
                                                        <div class="col-md-3">
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:Button ID="BtnReport" runat="server" CssClass="btn btn-primary" Text="Report" OnClick="BtnReport_Click"/>
                                                            <asp:Button ID="BtnClear" runat="server" CssClass="btn btn-primary" Text="Clear" OnClick="BtnClear_Click"/>
                                                            <asp:Button ID="BtnExit" runat="server" CssClass="btn btn-primary" Text="Exit" OnClick="BtnExit_Click"/>
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
            </div>
         </div>

</asp:Content>

