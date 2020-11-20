<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmSMSMstReport.aspx.cs" Inherits="FrmSMSMstReport" %>
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
      
      function dateLen(dt) {
          var dt1 = dt + '';
          if (dt1.length == 1)
              dt1 = '0' + dt;

          return dt1;
      }
        </script>
    <script type="text/javascript">

        function CheckForFutureDate() {

            var EnteredDate = document.getElementById('<%=txttodate.ClientID%>').value;
           var date = EnteredDate.substring(0, 2);
           var month = EnteredDate.substring(3, 5);
           var year = EnteredDate.substring(6, 10);

           var myDate = new Date(year, month - 1, date);

           var today = new Date();

           if (myDate > today) {
               message = 'Inavlid Date. System cannot accept Future Date!!!\n';
               $('#alertModal').find('.modal-body p').text(message);
               $('#alertModal').modal('show')
               document.getElementById('<%=txttodate.ClientID%>').value = '';
               $('#<%=txttodate.ClientID %>').focus();
               return false;
           }


           else {
               return true;
           }
       }
        function CheckForFutureDatefrm() {

            var EnteredDate = document.getElementById('<%=txtfromdate.ClientID%>').value;
            var date = EnteredDate.substring(0, 2);
            var month = EnteredDate.substring(3, 5);
            var year = EnteredDate.substring(6, 10);

            var myDate = new Date(year, month - 1, date);

            var today = new Date();

            if (myDate > today) {
                message = 'Inavlid Date. System cannot accept Future Date!!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtfromdate.ClientID%>').value = '';
               $('#<%=txtfromdate.ClientID %>').focus();
               return false;
           }


           else {
               return true;
           }
       }
   </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="col-md-12">
        <div class="portlet box green" id="Div1">
            <div class="portlet-title">
                <div class="caption">
                    SMS MASTER REPORT
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
                                                            <asp:TextBox ID="txtfromdate" CssClass="form-control" type="text" PlaceHolder="dd/mm/yyyy" runat="server" onkeyup="FormatIt(this);CheckForFutureDatefrm()"></asp:TextBox>
                                                             <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtfromdate">
                                                            </asp:TextBoxWatermarkExtender>
                                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtfromdate">
                                                            </asp:CalendarExtender>
                                                        </div>
                                                <div class="col-md-2">
                                                    </div>
                                                  <label class="control-label col-md-2">To Date:<span class="required">*</span></label>
                                                <div class="col-md-2">
                                                            <asp:TextBox ID="txttodate" type="text" PlaceHolder="dd/mm/yyyy" onkeyup="FormatIt(this);CheckForFutureDate()" runat="server" CssClass="form-control"></asp:TextBox>
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
                                                <label class="control-label col-md-2">Mobile: <span class="required">* </span></label>
                                                <div class="col-md-2">
                                                    <asp:RadioButtonList ID="rblmob" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblmob_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Value="1" style="padding: 5px">Specific</asp:ListItem>
                                                        <asp:ListItem Value="2" style="padding: 5px" Selected="True">All</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtMobileno" runat="server"  PlaceHolder="mobile no" CssClass="form-control" AutoPostBack="true" Visible="false"></asp:TextBox>
                                                </div>
                                                </div>
                                             </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-10">
                                                        <div class="col-md-3">
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:Button ID="BtnReport" runat="server" CssClass="btn btn-primary" Text="Report" OnClick="BtnReport_Click"/>
                                                            <asp:Button ID="btntrail" runat="server" CssClass="btn btn-primary" Text="Trial SMS" OnClick="btntrail_Click" /> 
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
</asp:Content>

