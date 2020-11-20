<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmDocumentRegister.aspx.cs" Inherits="FrmDocumentRegister" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function FormatIT(obj) {
            if (obj.value.length == 2)//date
                obj.value = obj.value + "/";
            if (obj.value.length == 5)//month
                obj.value = obj.value + "/";
            if (obj.value.length == 11)//month
                alert("Please enter valid date!....");
        }
    </script>
    <script>
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="portlet box blue" id="form_wizard_1">
                        <div class="portlet-title">
                            <div class="caption">
                                Document Register
                            </div>
                        </div>
                        <div class="portlet-body form">
                            <div class="form-horizontal">
                                <div class="form-wizard">
                                    <div class="form-body">
                                        <div class="tab-content">
                                            <div class="tab-pane active" id="tab__blue">
                                            </div>
                                            <div style="border: 1px solid #3598dc">
                                                <div id="DOU_DIV" class="row" style="margin: 7px 0 7px 0; padding-top: 15px;" runat="server">
                                                    <div class="col-md-6">
                                                        <div class="col-md-3">
                                                            <label id="Label1" class="control-label">From Upload date</label>
                                                        </div>
                                                        <div class="col-md-5">
                                                            <asp:TextBox ID="TxtFDOUpload" CssClass="form-control" PlaceHolder="dd/mm/yyyy" runat="server" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                            <asp:TextBoxWatermarkExtender ID="TxtFDOUpload_WatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtFDOUpload">
                                                            </asp:TextBoxWatermarkExtender>
                                                            <asp:CalendarExtender ID="TxtFDOUpload_CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtFDOUpload">
                                                            </asp:CalendarExtender>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="col-md-3">
                                                            <label id="Label4" class="control-label">To Upload date</label>
                                                        </div>
                                                        <div class="col-md-5">
                                                            <asp:TextBox ID="TxtTDOUpload" CssClass="form-control" PlaceHolder="dd/mm/yyyy" runat="server" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                            <asp:TextBoxWatermarkExtender ID="TxtTDOUpload_WatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtTDOUpload">
                                                            </asp:TextBoxWatermarkExtender>
                                                            <asp:CalendarExtender ID="TxtTDOUpload_CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtTDOUpload">
                                                            </asp:CalendarExtender>
                                                        </div>
                                                    </div>

                                                </div>
                                                <div id="FTDT_DIV" class="row" style="margin: 7px 0 7px 0" runat="server">
                                                    <div class="col-md-9">
                                                        <div class="col-md-2">
                                                            <label id="Label2" class="control-label">From Document type</label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtFDocType" runat="server" Placeholder=" From Doc code" CssClass="form-control" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <label id="Label3" class="control-label">To Document type</label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtTDocType" runat="server" Placeholder=" To Doc code" CssClass="form-control" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-6">
                                                        <asp:Button ID="BtnDownloadPDF" runat="server" CssClass="btn btn-success" Text="Download PDF" OnClick="BtnDownloadPDF_Click"/>
                                                        <asp:Button ID="BtnTextReport" runat="server" CssClass="btn btn-primary" Text="Download Text Report" OnClick="BtnTextReport_Click"/>
                                                        <asp:Button ID="Exit" runat="server" Text="Exit" CssClass="btn btn-primary" OnClick="Exit_Click" />
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

