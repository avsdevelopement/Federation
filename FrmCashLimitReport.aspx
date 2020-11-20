<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmCashLimitReport.aspx.cs" Inherits="FrmCashLimitReport" %>
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
     <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        CASH LIMIT MASTER
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div id="error">
                                    </div>
                                    <div class="tab-pane active" id="tab1">
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-md-12">
                                                <label class="control-label col-md-1">Prod Code:<span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtPrd" Placeholder="Product Code" CssClass="form-control" runat="server" OnTextChanged="TxtPrd_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="TxtProdName" CssClass="form-control" runat="server" AutoPostBack="true" Placeholder="Product Name" OnTextChanged="TxtProdName_TextChanged"></asp:TextBox>
                                                    <div id="ProdList" style="height: 200px; overflow-y: scroll;"></div>
                                                    <asp:AutoCompleteExtender ID="autoFglname" runat="server" TargetControlID="TxtProdName"
                                                        UseContextKey="true"
                                                        CompletionInterval="1"
                                                        CompletionSetCount="20"
                                                        MinimumPrefixLength="1"
                                                        EnableCaching="true"
                                                        ServicePath="~/WebServices/Contact.asmx"
                                                        ServiceMethod="getglname" CompletionListElementID="ProdList">
                                                    </asp:AutoCompleteExtender>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-1">As On Date: <span class="required"></span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtAsOnDate" CssClass="form-control" PlaceHolder="dd/mm/yyyy" runat="server" onkeyup="FormatIt(this)"></asp:TextBox>
                                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtAsOnDate">
                                                    </asp:TextBoxWatermarkExtender>
                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtAsOnDate">
                                                    </asp:CalendarExtender>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="row" style="margin: 20px; border-bottom: 1px solid rgba(53, 152, 220, 0.55);"><strong></strong></div>
                                        <div class="row" style="margin: 7px 0 7px 0; text-align: center">
                                            <div class="col-lg-12">
                                                <div class="col-md-6">

                                                    <asp:Button ID="Btn_Report" runat="server" Text="Report" CssClass="btn btn-success" OnClick="Btn_Report_Click" />
                                                    <asp:Button ID="Btn_ClearAll" runat="server" Text="Clear All" CssClass="btn btn-success" OnClick="Btn_ClearAll_Click" />
                                                    <asp:Button ID="Btn_Exit" runat="server" Text="Exit" CssClass="btn btn-success" OnClick="Btn_Exit_Click" />
                                                    
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

