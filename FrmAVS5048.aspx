<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAVS5048.aspx.cs" Inherits="FrmAVS5048" %>

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
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
     
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="portlet box blue" id="form_wizard_1">
                        <div class="portlet-title">
                            <div class="caption">
                               OverDue Calculation
                            </div>
                        </div>
                        <div class="portlet-body form">
                            <div class="form-horizontal">
                                <div class="form-wizard">
                                    <div class="form-body">
                                        <div class="tab-content">
                                            <div style="border: 1px solid #3598dc">
                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-10">
                                                          <div class="col-md-2">
                                                            <label class="control-label">Brcd:</label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="txtbrcd"  PlaceHolder="Brcd"  runat="server" CssClass="form-control"></asp:TextBox>
                                                          
                                                        </div>
                                                        <div class="col-md-3">
                                                            <label class="control-label">As on Date:</label>
                                                        </div>
                                                         <div class="col-md-3">
                                                            <asp:TextBox ID="txtfromdate" CssClass="form-control" type="text" PlaceHolder="dd/mm/yyyy" runat="server" onkeyup="FormatIt(this)"></asp:TextBox>
                                                             <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtfromdate">
                                                            </asp:TextBoxWatermarkExtender>
                                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtfromdate">
                                                            </asp:CalendarExtender>
                                                        </div>
                                                    
                                                    </div>
                                                </div>

                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-10">
                                                        <div class="col-md-3">
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:Button ID="BtnSave" runat="server" CssClass="btn btn-primary" Text="Calculate" OnClick="BtnSave_Click" />
                                                            <asp:Button ID="BtnPrint" runat="server" CssClass="btn btn-primary" Text="Report" OnClick="BtnPrint_Click" />
                                                           
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

