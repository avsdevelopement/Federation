<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmAVS5056.aspx.cs" MasterPageFile="~/CBSMaster.master" Inherits="FrmAVS5056" %>
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
                   Loan Nill Report
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
                                                            <asp:TextBox ID="txttodate" type="text" PlaceHolder="dd/mm/yyyy" onkeyup="FormatIt(this)"  runat="server" CssClass="form-control"></asp:TextBox>
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
                                                <label class="control-label col-md-2">From BRCD:<span class="required">*</span></label>
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="TxtFBRCD" Placeholder="BRCD" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" OnTextChanged= "TxtFBRCD_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtFBRCDName" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-2">To BRCD:<span class="required">*</span></label>
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="TxtTBRCD" Placeholder="BRCD" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" OnTextChanged= "TxtTBRCD_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtTBRCDName" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                         <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">From Prd CD:<span class="required">*</span></label>
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="TxtProdCode1" Placeholder="Prd CD" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" OnTextChanged= "TxtProdCode1_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtProdCodeName1" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                                <label class="control-label col-md-2">To Prd CD:<span class="required">*</span></label>
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="TxtProdCode2" Placeholder="Prd CD" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" OnTextChanged= "TxtProdCode2_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtProdCodeName2" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-10">
                                                        <div class="col-md-3">
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:Button ID="BtnReport" runat="server" CssClass="btn btn-primary" Text="Report" OnClick= "BtnReport_Click"/>
                                                            <asp:Button ID="BtnClear" runat="server" CssClass="btn btn-primary" Text="Clear" OnClick= "BtnClear_Click"/>
                                                            <asp:Button ID="BtnExit" runat="server" CssClass="btn btn-primary" Text="Exit" OnClick= "BtnExit_Click"/>
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
