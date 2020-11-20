<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAVS5041.aspx.cs" Inherits="FrmAVS5041" %>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <div class="col-md-12">
        <div class="portlet box green" id="Div1">
            <div class="portlet-title">
                <div class="caption">
                   Dividend Payable Report
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
                                                            <asp:TextBox ID="txtFdate" CssClass="form-control" type="text" PlaceHolder="dd/mm/yyyy" runat="server" onkeyup="FormatIt(this);CheckForFutureDatefrm()"></asp:TextBox>
                                                             <asp:TextBoxWatermarkExtender ID="txtFdateWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtFdate">
                                                            </asp:TextBoxWatermarkExtender>
                                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtFdate">
                                                            </asp:CalendarExtender>
                                                        </div>
                                                 <label class="control-label col-md-2">TO Date:<span class="required">*</span></label>
                                                <div class="col-md-2">
                                                            <asp:TextBox ID="txtTdate" CssClass="form-control" type="text" PlaceHolder="dd/mm/yyyy" runat="server" onkeyup="FormatIt(this);CheckForFutureDatefrm()"></asp:TextBox>
                                                             <asp:TextBoxWatermarkExtender ID="txtTdateWatermarkExtender2" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtTdate">
                                                            </asp:TextBoxWatermarkExtender>
                                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtTdate">
                                                            </asp:CalendarExtender>
                                                        </div>
                                                </div>
                                            </div>
                                         <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                
                                                </div>
                                            </div>
                                           <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-10">
                                                        <div class="col-md-3">
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:Button ID="BtnReport" runat="server" CssClass="btn btn-primary" Text="Report" OnClick="BtnReport_Click"/>
                                                            <asp:Button ID="BtnExit" runat="server" CssClass="btn btn-primary" Text="Exit" OnClick="BtnExit_Click"/>
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

