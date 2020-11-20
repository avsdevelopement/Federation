<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAVSReco.aspx.cs" Inherits="FrmAVSReco" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function FormatIt(obj) {
            if (obj.value.length == 2) // Day
                obj.value = obj.value + "/";
            if (obj.value.length == 5) // month 
                obj.value = obj.value + "/";
            if (obj.value.length == 11) // year 
                alert(" Enter valid Date");
            if (obj.value.length == 6) {
                var EnteredDate = obj.value;
                var date = EnteredDate.substring(0, 2);
                var month = EnteredDate.substring(3, 5);
                if (month == "01" || month == "03" || month == "05" || month == "07" || month == "08" || month == "10" || month == "12") {
                    if (date < "01" || date > "31") {
                        alert("Enter valid Date");
                        obj.value = "";
                    }
                }
                else if (month == "04" || month == "06" || month == "09" || month == "11") {
                    if (date < "01" || date > "30") {
                        alert("Enter valid Date");
                        obj.value = "";
                    }
                }
                else if (month == "02") {
                    if (date < "01" || date > "29") {
                        alert("Enter valid Date");
                        obj.value = "";
                    }
                }
                if (month < "01" || month > "12") {
                    alert("Enter valid Date");
                    obj.value = "";
                }
            }
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
        <Triggers>
            <asp:PostBackTrigger ControlID="btnFileUpload" />
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="portlet box blue" id="form_wizard_1">
                        <div class="portlet-title">
                            <div class="caption">
                                AVS Reconcilation
                            </div>
                        </div>
                        <div class="portlet-body form">
                            <div class="form-horizontal">
                                <div class="form-wizard">
                                    <div class="form-body">
                                        <div class="tab-content">
                                            <div class="tab-pane active" id="tab__blue">
                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-9">

                                                        <div class="col-md-2">
                                                            <label class="control-label">Br Code</label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtBrcd" runat="server" OnTextChanged="txtBrcd_TextChanged" Placeholder="Br Code" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-9">
                                                        <div runat="server" id="FDT">
                                                            <div class="col-md-2">
                                                                <label class="control-label">From Date</label>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="TxtFDate" CssClass="form-control" PlaceHolder="dd/mm/yyyy" runat="server" onkeyup="FormatIt(this);"></asp:TextBox>
                                                                <asp:TextBoxWatermarkExtender ID="TxtFDT_WatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtFDate">
                                                                </asp:TextBoxWatermarkExtender>
                                                                <asp:CalendarExtender ID="TxtFDT_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtFDate">
                                                                </asp:CalendarExtender>
                                                            </div>
                                                        </div>

                                                        <div runat="server" id="TDT">
                                                            <div class="col-md-2">
                                                                <label class="control-label">To Date</label>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:TextBox ID="TxtTDate" CssClass="form-control" onkeypress="javascript:return isNumber (event);" PlaceHolder="dd/mm/yyyy" runat="server" onkeyup="FormatIt(this);"></asp:TextBox>
                                                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtTDate">
                                                                </asp:TextBoxWatermarkExtender>
                                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtTDate">
                                                                </asp:CalendarExtender>
                                                                     <asp:CompareValidator ID="Comparevalidator1" runat="server" ErrorMessage="'From Date' should be less than 'To Date'" ForeColor="Red"
                                     Operator="greaterThanEqual" ControlToValidate="TxtTDate" Type="date" ControlToCompare="TxtFDate" Display="Dynamic" />
                                                      </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-9">

                                                        <div class="col-md-2">
                                                            <label class="control-label">Bank Code</label>
                                                        </div>
                                                        <div class="col-md-2">

                                                            <asp:TextBox ID="txtBankCode" runat="server" Placeholder="Bank Code" onkeypress="javascript:return isNumber (event);" OnTextChanged="txtBankCode_TextChanged" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-5">
                                                            <asp:TextBox ID="TxtProName" placeholder="PRODUCT NAME" CssClass="form-control" OnTextChanged="TxtProName_TextChanged" runat="server" AutoPostBack="true" TabIndex="3"></asp:TextBox>
                                                            <div id="CustList" style="height: 200px; overflow-y: scroll;"></div>
                                                            <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="TxtProName"
                                                                UseContextKey="true"
                                                                CompletionInterval="1"
                                                                CompletionSetCount="20"
                                                                MinimumPrefixLength="1"
                                                                EnableCaching="true"
                                                                ServicePath="~/WebServices/Contact.asmx"
                                                                ServiceMethod="GetGlName" CompletionListElementID="CustList">
                                                            </asp:AutoCompleteExtender>
                                                        </div>
                                                        
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-12" style="text-align: left; margin-top: 12px; margin-bottom: 13px;">
                                                        <div class="col-lg-2">
                                                            <label class=" form-control-label">Excel File Upload</label>
                                                        </div>
                                                        <div class="col-lg-3" style="margin-left:-45px">
                                                            <asp:FileUpload ID="fuBorrDataExcel" runat="server" />
                                                        </div>
                                                      
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-12" style="text-align: left; margin-top: 12px; margin-bottom: 13px; margin-left: 15px;">
                                                       <div class="col-md-1"></div>
                                                        <div class="col-md-1">
                                                            <asp:Button ID="btnFileUpload" runat="server" Text="File Upload" OnClick="btnFileUpload_Click" CssClass="btn btn-primary" />                                                           
                                                        </div>
                                                       
                                                        <div class="col-md-1" style="margin-left:30px;margin-right:30px">
                                                            <asp:Button ID="btnMatch" runat="server" Text="Reconcilation" OnClick="btnMatch_Click" CssClass="btn btn-primary" />                                                           
                                                        </div>
                                                         <div class="col-md-1" style="margin-left:20px;margin-right:30px">
                                                            <asp:Button ID="btnCleatData" runat="server" Text="Clear Data" OnClick="btnCleatData_Click" CssClass="btn btn-primary" />                                                           
                                                        </div>
                                                         <div class="col-md-1">
                                                            <asp:Button ID="btnTemplate" runat="server" Text="Template" Visible="false" OnClick="btnTemplate_Click" CssClass="btn btn-primary" />                                                           
                                                        </div>
                                                        <div class="col-lg-3">
                                                            
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

                    
          
               
                
                        <div class="container" style=" overflow-y: scroll; max-height:250px">
                            <div class="row">
                                <div class="col-lg-12 ">
                                    <div class="table-responsive">
                                        <asp:GridView ID="grdView" runat="server" Width="86%" CssClass="table table-striped table-bordered table-hover" HeaderStyle-BackColor="Black" ShowHeader="true"  AutoGenerateColumns="False" DataKeyNames="SRNO" EmptyDataText="There are no data records to display.">
                                            <Columns>
                                                <asp:BoundField DataField="SRNO" HeaderText="SR NO" ReadOnly="True" SortExpression="SRNO" />
                                                <asp:BoundField DataField="DATE" HeaderText="DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="DATE" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                <asp:BoundField DataField="INSTRUMENTNO" HeaderText="INSTRUMENT NO" SortExpression="INSTRUMENTNO" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                                                <asp:BoundField DataField="CREDIT" HeaderText="CREDIT" SortExpression="DEBIT" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                <asp:BoundField DataField="DEBIT" HeaderText="DEBIT" SortExpression="DEBIT" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                                                <asp:BoundField DataField="ENTRYDATE" DataFormatString="{0:dd/MM/yyyy}" HeaderText="ENTRYDATE" SortExpression="ENTRYDATE" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                                                <asp:BoundField DataField="AMOUNT" HeaderText="AMOUNT" SortExpression="AMOUNT" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                <asp:BoundField DataField="TRXTYPE" HeaderText="TRXTYPE" SortExpression="TRXTYPE" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                <asp:BoundField DataField="STATUS" HeaderText="STATUS" SortExpression="STATUS" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />

                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>          
               
                       <div class="row">
                          <br />
                           <div class="col-md-2" style="margin-left:15px">
                               <asp:Label ID="lblCount"  runat="server">Count</asp:Label>
                           </div>
                          
                       </div>

                </div>
            </div>

            <!--GRID VIEW-->



            </div>
        
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>



