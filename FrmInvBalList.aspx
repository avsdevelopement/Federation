<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmInvBalList.aspx.cs" Inherits="FrmInvBalList" %>
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
     <div class="row">
                <div class="col-md-12">
                    <div class="portlet box blue" id="form_wizard_1">
                        <div class="portlet-title">
                            <div class="caption">
                                Investment Balance List
                            </div>
                        </div>
                        <div class="portlet-body form">
                            <div class="form-horizontal">
                                <div class="form-wizard">
                                    <div class="form-body">
                                        <div class="tab-content">
                                            <div style="border: 1px solid #3598dc">
                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                            <label class="control-label">As On Date:</label>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtasondate" CssClass="form-control" type="text" PlaceHolder="dd/mm/yyyy" runat="server" onkeyup="FormatIt(this)"></asp:TextBox>
                                                            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtasondate">
                                                            </asp:TextBoxWatermarkExtender>
                                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtasondate">
                                                            </asp:CalendarExtender>
                                                        </div>
                                                        </div>
                                                    
                                                    </div>
                                                 <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12" align="center">
                                                     <div class="col-md-12">
                                                             <asp:Button ID="BtnBalList" runat="server" CssClass="btn btn-primary" Text="Balance List" OnClick="BtnBalList_Click" />
                                                            <asp:Button ID="BtnExit" runat="server" CssClass="btn btn-primary" Text="Exit" OnClick="BtnExit_Click" />
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

