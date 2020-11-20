<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmInvIntCalculation.aspx.cs" Inherits="FrmInvIntCalculation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
    <script type="text/javascript">
        function FormatIt(obj) {
            if (obj.value.length == 2) // Day
                obj.value = obj.value + "/";
            if (obj.value.length == 5) // month 
                obj.value = obj.value + "/";
            if (obj.value.length == 11) // year 
                alert("Please Enter valid Date");
        }
    </script>
    <script>
        function isvalidate() {

            var ASON, FBR, TBR, FPRD, TPRD, FAC, TAC,
            ASON = document.getElementById('<%=TxtAsOnDate.ClientID%>').value;
          
            FPRD = document.getElementById('<%=TxtFPRD.ClientID%>').value;
            TPRD = document.getElementById('<%=TxtTPRD.ClientID%>').value;
           
           

            var message = '';

            if (ASON == "") {
                message = 'Please Enter As On Date Date....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtAsOnDate.ClientID%>').focus();
                  return false;
              }
            
              if (FPRD == "") {
                  message = 'Please Enter From Product Code....!!\n';
                  $('#alertModal').find('.modal-body p').text(message);
                  $('#alertModal').modal('show')
                  document.getElementById('<%=TxtFPRD.ClientID%>').focus();
                  return false;
              }
            if (TPRD == "") {
                message = 'Please Enter To Product Code....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtTPRD.ClientID%>').focus();
                return false;
            }
             
            
          }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="row">
        <div class="col-md-12">
            <div class="portlet box green" id="Div1">
                <div class="portlet-title">
                    <div class="caption">
                        Investment Interest Calculation
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab1">
                                        <div class="row" style="margin-bottom: 10px;">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-1">As On Date : <span class="required">* </span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtAsOnDate" CssClass="form-control" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this)" runat="server"></asp:TextBox>
                                                    <asp:TextBoxWatermarkExtender ID="TxtAsOnDate_Extender2" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtAsOnDate">
                                                    </asp:TextBoxWatermarkExtender>
                                                    <asp:CalendarExtender ID="TxtAsOnDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtAsOnDate">
                                                    </asp:CalendarExtender>
                                                </div>
                                            </div>
                                            <br />
                                            <br />
                                            <div class="row" style="margin: 20px; border-bottom: 1px solid rgba(53, 152, 220, 0.55);"><strong></strong></div>
                                            <%--<div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-1">BRCD <span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtFBRCD" Placeholder="From BRCD" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="TxtFBRCD_TextChanged"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TxtFBRCDName" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                    <label class="control-label col-md-1">BRCD <span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtTBRCD" Placeholder="To BRCD" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="TxtTBRCD_TextChanged"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TxtTBRCDName" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>--%>

                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-1">PRD Cd.<span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtFPRD" Placeholder="From Product Code" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server"  AutoPostBack="true" OnTextChanged="TxtFPRD_TextChanged"></asp:TextBox>
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
                                                        <asp:TextBox ID="TxtTPRD" Placeholder="To Product Code" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="TxtTPRD_TextChanged"></asp:TextBox>
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
                                                <%--<div class="col-lg-12">
                                                    <label class="control-label col-md-1">From A/C <span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtFAcc" Placeholder="From A/C No." onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server"  AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TxtFAccName" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                    <label class="control-label col-md-1">To A/C <span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtTAcc" Placeholder="To A/C No." onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server"  AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TxtTAccName" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                   

                                                </div>--%>

                                                <div class="row" style="margin: 20px; border-bottom: 1px solid rgba(53, 152, 220, 0.55);"><strong></strong></div>
                                                <div class="row" style="margin: 7px 0 7px 0; text-align: center">
                                                    <div class="col-lg-12">

                                                        <asp:Button ID="TrailEntry" OnClientClick="Javascript:return isvalidate();" runat="server" Text="Trial Run" CssClass="btn btn-primary" OnClick="TrailEntry_Click" />
                                                        <asp:Button ID="ApplyEntry" OnClientClick="Javascript:return isvalidate();" runat="server" Text="Apply Entry" CssClass="btn btn-success" OnClick="ApplyEntry_Click"/>
                                                        <asp:Button ID="Report" runat="server" Text="Report" CssClass="btn btn-Primary" />
                                                        <asp:Button ID="Btn_ClearAll" runat="server" Text="Clear All" OnClick="Btn_ClearAll_Click" CssClass="btn btn-success" />
                                                        <asp:Button ID="Btn_Exit" runat="server" Text="Exit" OnClick="Btn_Exit_Click" CssClass="btn btn-success" />


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
            <div class="row">
                <div class="col-lg-12">
                    <div class="table-scrollable">
                        <table class="table table-striped table-bordered table-hover">
                            <thead>
                                <tr>
                                    <th>
                                        <asp:GridView ID="GrdFDInt" runat="server" AllowPaging="True"
                                            AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                            EditRowStyle-BackColor="#FFFF99"
                                           PageIndex="10" PageSize="25"
                                            PagerStyle-CssClass="pgr" Width="100%">
                                            <Columns>

                                                <%-- <asp:TemplateField HeaderText="EntryDate" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="EDATE" runat="server" Text='<%# Eval("EDT","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>

                                                <asp:TemplateField HeaderText="PRD CODE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="PRD" runat="server" Text='<%# Eval("SUBGLCODE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="ACCNO">
                                                    <ItemTemplate>
                                                        <asp:Label ID="AC" runat="server" Text='<%# Eval("ACCNO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="CUSTNAME">
                                                    <ItemTemplate>
                                                        <asp:Label ID="parti" runat="server" Text='<%# Eval("CUSTNAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="INT AMT" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="CREDIT" runat="server" Text='<%# Eval("CREDIT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%-- <asp:TemplateField HeaderText="DEBIT">
                                                        <ItemTemplate>
                                                            <asp:Label ID="DEBIT" runat="server" Text='<%# Eval("DEBIT") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>

                                                <asp:TemplateField HeaderText="DEPOSIT DATE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="FDATE" runat="server" Text='<%# Eval("FDATE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MATURITY DATE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="TDATE" runat="server" Text='<%# Eval("TDATE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DAYS">
                                                    <ItemTemplate>
                                                        <asp:Label ID="DDIFF" runat="server" Text='<%# Eval("DAYSDF") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="RATE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="RATE" runat="server" Text='<%# Eval("RATE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PRN AMT">
                                                    <ItemTemplate>
                                                        <asp:Label ID="DAMT" runat="server" Text='<%# Eval("DPAMT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="BRCD">
                                                    <ItemTemplate>
                                                        <asp:Label ID="BRCD" runat="server" Text='<%# Eval("BRCD") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                            <PagerStyle CssClass="pgr" />
                                            <SelectedRowStyle BackColor="#66FF99" />
                                            <EditRowStyle BackColor="#FFFF99" />
                                            <AlternatingRowStyle CssClass="alt" />
                                        </asp:GridView>
                                    </th>
                                </tr>
                            </thead>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

