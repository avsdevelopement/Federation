<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmLoanOverdueOnly.aspx.cs" Inherits="FrmLoanOverdueOnly" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script type="text/javascript">
         function FormatIT(obj) {
             if (obj.value.length == 2)
                 obj.value = obj.value + "/";//DATE
             if (obj.value.length == 5)
                 obj.value = obj.value + "/";//month
             if (obj.value.length == 11) //Year
                 alert("Enter Valid Date");
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12">
                    <div class="portlet box blue" id="Div1">
                        <div class="portlet-title">
                            <div class="caption">
                                Loan Overdue Only Report
                            </div>
                        </div>
                        <div class="portlet-body form">
                            <!--<form action="#" class="form-horizontal" id="submit_form" method="POST">-->
                            <div class="form-horizontal">
                                <div class="form-wizard">
                                    <div class="form-body">

                                        <div class="tab-content">
                                            <div id="error">
                                            </div>
                                            <div class="tab-pane active" id="tab1">

                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-12" style="border-bottom: 1px solid rgba(128, 128, 128, 0.31);"><strong style="color: #fa4e0d"></strong></div>
                                                    </div>
                                                </div>

                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">
                                                        <label class="control-label col-md-1">PRD Cd.<span class="required">*</span></label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtFPRD" Placeholder="From Product Code" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" OnTextChanged="TxtFPRD_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="TxtFPRDName" CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                                                            <div id="CustList" style="height: 200px; overflow-y: scroll;"></div>
                                                            <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="TxtFPRDName"
                                                                UseContextKey="true"
                                                                CompletionInterval="1"
                                                                CompletionSetCount="20"
                                                                MinimumPrefixLength="1"
                                                                EnableCaching="true"
                                                                ServicePath="~/WebServices/Contact.asmx"
                                                                ServiceMethod="getglname" CompletionListElementID="CustList">
                                                            </asp:AutoCompleteExtender>
                                                        </div>

                                                        <label class="control-label col-md-1">PRD Cd.<span class="required">*</span></label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtTPRD" Placeholder="To Product Code" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" OnTextChanged="TxtTPRD_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="TXtTPRDName" CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                                                            <div id="Div2" style="height: 200px; overflow-y: scroll;"></div>
                                                            <asp:AutoCompleteExtender ID="autoglname1" runat="server" TargetControlID="TXtTPRDName"
                                                                UseContextKey="true"
                                                                CompletionInterval="1"
                                                                CompletionSetCount="20"
                                                                MinimumPrefixLength="1"
                                                                EnableCaching="true"
                                                                ServicePath="~/WebServices/Contact.asmx"
                                                                ServiceMethod="getglname" CompletionListElementID="Div2">
                                                            </asp:AutoCompleteExtender>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">
                                                        <div class="col-md-1">
                                                            <label class="control-label">AsOn Date <span class="required">*</span></label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="TxtDate" CssClass="form-control" PlaceHolder="dd/mm/yyyy" runat="server" onkeyup="FormatIT(this)"></asp:TextBox>
                                                            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtDate">
                                                            </asp:TextBoxWatermarkExtender>
                                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtDate">
                                                            </asp:CalendarExtender>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin: 7px 0 7px 0">
                                                </div>
                                                <div class="row" style="margin: 7px 0 7px 0">
                                                </div>
                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">

                                                        <label class="control-label col-md-2">A/C Type</label>

                                                        <div class="col-md-9">
                                                            <asp:RadioButtonList ID="Rdb_AccType" runat="server" RepeatDirection="Horizontal" CssClass="radio-list" OnSelectedIndexChanged="Rdb_AccType_SelectedIndexChanged" AutoPostBack="true">
                                                                   <asp:ListItem Text="All A/C's" Value="3" style="margin: 25px;" Selected="True"> </asp:ListItem>                                                               
                                                                 <asp:ListItem Text="Only Court File" Value="1" style="margin: 15px;" > </asp:ListItem>
                                                                <asp:ListItem Text="No Court Files" Value="2" style="margin: 25px;"> </asp:ListItem>
                                                               
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="form-actions">
                                        <div class="row">
                                            <div class="col-md-offset-3 col-md-9">
                                                <asp:Button ID="Btn_LOReport" runat="server" Text="Loan Overdue" CssClass="btn btn-success" OnClick="Btn_LOReport_Click" />
                                                <asp:Button ID="Exit" runat="server" Text="Exit" CssClass="btn btn-success" OnClick="Exit_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--</form>-->
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

