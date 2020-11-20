<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmMISTransfer.aspx.cs" Inherits="FrmMISTransfer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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

            var ASON, FBR, TBR, FPRD, TPRD,
            ASON = document.getElementById('<%=TxtAsOnDate.ClientID%>').value;
            TBR = document.getElementById('<%=TxtTBRCD.ClientID%>').value;
            FPRD = document.getElementById('<%=TxtFPRD.ClientID%>').value;
            TPRD = document.getElementById('<%=TxtTPRD.ClientID%>').value;
            FBR = document.getElementById('<%=TxtFBRCD.ClientID%>').value;

            var message = '';

            if (ASON == "") {
                message = 'Please Enter As On Date Date....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtAsOnDate.ClientID%>').focus();
                return false;
            }
            if (TBR == "") {
                message = 'Please Enter To Branch Code....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtTBRCD.ClientID%>').focus();
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
            if (FBR == "") {
                message = 'Please Enter From Branch Code....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtFBRCD.ClientID%>').focus();
                return false;
            }

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="Div1">
                <div class="portlet-title">
                    <div class="caption">
                        MIS INTEREST TRANSFER
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
                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-1">BRCD <span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtFBRCD" Placeholder="From BRCD" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" OnTextChanged="TxtFBRCD_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TxtFBRCDName" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                    <label class="control-label col-md-1">BRCD <span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtTBRCD" Placeholder="To BRCD" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" OnTextChanged="TxtTBRCD_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TxtTBRCDName" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-1">PRD Cd.<span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtFPRD" Placeholder="From Product Code" onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" OnTextChanged="TxtFPRD_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TxtFPRDName" CssClass="form-control" runat="server" OnTextChanged="TxtFPRDName_TextChanged" AutoPostBack="true"></asp:TextBox>
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
                                                        <asp:TextBox ID="TXtTPRDName" CssClass="form-control" runat="server" OnTextChanged="TXtTPRDName_TextChanged" AutoPostBack="true"></asp:TextBox>
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


                                            <div class="row" style="margin: 7px 0 7px 0" >
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-1">From A/C <span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtFAcc" Enabled="false" Placeholder="From A/C No." onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" OnTextChanged="TxtFAcc_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TxtFAccName" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                    <label class="control-label col-md-1">To A/C <span class="required">*</span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="TxtTAcc" Enabled="false" Placeholder="To A/C No." onkeypress="javascript:return isNumber (event)" CssClass="form-control" runat="server" OnTextChanged="TxtTAcc_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TxtTAccName" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="row" style="margin: 20px; border-bottom: 1px solid rgba(53, 152, 220, 0.55);"><strong></strong></div>
                                            <div class="row" style="margin: 7px 0 7px 0; text-align: center">
                                                <div class="col-lg-12">
                                                    <div class="col-md-6">
                                                        <asp:Button ID="TrailEntry"  OnClientClick="Javascript:return isvalidate();" runat="server" Text="Trial Run" CssClass="btn btn-primary" OnClick="TrailEntry_Click" />
                                                        <asp:Button ID="ApplyEntry"  OnClientClick="Javascript:return isvalidate();" runat="server" Text="Post Entry" CssClass="btn btn-success" OnClick="ApplyEntry_Click" />
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
        <div class="row">
        <div class="col-md-12">
            <div class="table-scrollable" style="width: 100%; height: 500px; overflow-x: auto; overflow-y: auto">
                    <table class="table table-striped table-bordered table-hover">
                        <thead>
                            <tr>
                                <th>
                                  <asp:GridView ID="GrdFDInt" runat="server" BorderStyle="Double"
                                        AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                        EditRowStyle-BackColor="#FFFF99"
                                        Width="100%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SR NO." HeaderStyle-BackColor="#ff9966">
                                                <ItemTemplate>
                                                    <asp:Label ID="ID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Dr.GLCODE" HeaderStyle-BackColor="#ff9966">
                                                <ItemTemplate>
                                                    <asp:Label ID="DBGLCODE" runat="server" Text='<%# Eval("DBGLCODE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Dr PRD" HeaderStyle-BackColor="#ff9966">
                                                <ItemTemplate>
                                                    <asp:Label ID="DRPRD" runat="server" Text='<%# Eval("DRPRD") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Dr. A/C NO." Visible="true" HeaderStyle-BackColor="#ff9966">
                                                <ItemTemplate>
                                                    <asp:Label ID="DRACCNO" runat="server" Text='<%# Eval("DRACCNO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Dr. PRD NAME" HeaderStyle-BackColor="#ff9966">
                                                <ItemTemplate>
                                                    <asp:Label ID="DRGLNAME" runat="server" Text='<%# Eval("DRGLNAME") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dr. AMT" HeaderStyle-BackColor="#ff9966">
                                                <ItemTemplate>
                                                    <asp:Label ID="DRAMT" runat="server" Text='<%# Eval("DRAMT") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Cr PRD" HeaderStyle-BackColor="#99ff99">
                                                <ItemTemplate>
                                                    <asp:Label ID="CRPRD" runat="server" Text='<%# Eval("CRPRD") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Cr. A/C NO." Visible="true" HeaderStyle-BackColor="#99ff99">
                                                <ItemTemplate>
                                                    <asp:Label ID="CRACCNO" runat="server" Text='<%# Eval("CRACCNO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="CUST NAME" HeaderStyle-BackColor="#99ff99">
                                                <ItemTemplate>
                                                    <asp:Label ID="CRCUSTNAME" runat="server" Text='<%# Eval("CRCUSTNAME") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cr AMT" HeaderStyle-BackColor="#99ff99">
                                                <ItemTemplate>
                                                    <asp:Label ID="CRAMT" runat="server" Text='<%# Eval("CRAMT") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Cr/Dr MATCH" HeaderStyle-BackColor="#99ff99">
                                                <ItemTemplate>
                                                    <asp:Label ID="FLAG_APR" runat="server" Text='<%# Eval("FLAG_APR") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="BRCD" HeaderStyle-BackColor="#99ff99">
                                                <ItemTemplate>
                                                    <asp:Label ID="BRCD" runat="server" Text='<%# Eval("BRCD") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                       
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

