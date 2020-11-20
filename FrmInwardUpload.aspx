<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmInwardUpload.aspx.cs" Inherits="FrmInwardUpload" %>

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
    <script type="text/javascript">
        function isvalidate() {

            var entrydate, inwarddate;
            entrydate = document.getElementById('<%=TxtTDate.ClientID%>').value;
            inwarddate = document.getElementById('<%=TxtINWDate.ClientID%>').value;
            var message = '';

            if (entrydate == "") {
                message = 'Please Enter Entry Date....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtTDate.ClientID%>').focus();
                return false;
            }

            if (protype == "") {
                message = 'Please Enter Inward Date....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtINWDate.ClientID%>').focus();
                return false;
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
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="portlet box blue" id="form_wizard_1">
                        <div class="portlet-title">
                            <div class="caption">
                                Inward Upload
                                
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

                                                <div id="FTDT" class="row" style="margin: 7px 0 7px 0; padding-top: 15px;" runat="server">
                                                    <div class="col-lg-6">

                                                        <div class="col-md-3">
                                                            <label id="LblIDT" class="control-label">Inward Date</label>
                                                        </div>
                                                        <div class="col-md-5">
                                                            <asp:TextBox ID="TxtINWDate" CssClass="form-control" PlaceHolder="dd/mm/yyyy" runat="server" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" TabIndex="1"></asp:TextBox>
                                                            <asp:TextBoxWatermarkExtender ID="TxtINWDate_WatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtINWDate">
                                                            </asp:TextBoxWatermarkExtender>
                                                            <asp:CalendarExtender ID="TxtINWDate_CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtINWDate">
                                                            </asp:CalendarExtender>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6">

                                                        <div class="col-md-3">
                                                            <label id="LblTDT" class="control-label">Entry Date</label>
                                                        </div>
                                                        <div class="col-md-5">
                                                            <asp:TextBox ID="TxtTDate" CssClass="form-control" PlaceHolder="dd/mm/yyyy" runat="server" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" Enabled="False"></asp:TextBox>
                                                            <asp:TextBoxWatermarkExtender ID="TxtTDate_WatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtTDate">
                                                            </asp:TextBoxWatermarkExtender>
                                                            <asp:CalendarExtender ID="TxtTDate_CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtTDate">
                                                            </asp:CalendarExtender>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row" style="margin: 7px 0 7px 0; padding-top: 15px;">
                                                    <div class="col-lg-6">
                                                        <label class="control-label col-md-3">Select Bank<span class="required">* </span></label>

                                                        <div class="col-md-6">
                                                            <asp:DropDownList ID="ddlBankName" CssClass="form-control" runat="server" TabIndex="2"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <label class="control-label col-md-3">Upload File : <span class="required">* </span></label>
                                                        <div class="col-md-5">

                                                            <asp:FileUpload ID="FlUpload" runat="server" TabIndex="3"/>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin: 7px 0 7px 0; padding-top: 15px;">
                                                    <div class="col-lg-6">
                                                        <asp:RadioButton ID="RdbAutho" Text="To Clearing" GroupName="AS" runat="server" TabIndex="4"/>
                                                        <asp:RadioButton ID="RdbNonAutho" Text="To Return" GroupName="AS" runat="server"/>
                                                    </div>
                                               </div>


                                                <div class="row">
                                                    <div class="col-lg-12" style="text-align: left; margin-top: 12px; margin-bottom: 13px; margin-left: 15px;">

                                                        <div class="col-lg-6">
                                                            <asp:Button ID="Process" runat="server" CssClass="btn btn-primary" Text="Process" OnClick="Process_Click" OnClientClick="Javascript:return isvalidate();" TabIndex="5"/>
                                                            <asp:Button ID="Exit" runat="server" Text="Exit" CssClass="btn btn-primary" OnClick="Exit_Click" TabIndex="6"/>
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
            <div class="table-scrollable">
                <table class="table table-striped table-bordered table-hover">
                    <thead>
                        <tr>
                            <th>
                                <asp:GridView ID="GrdAN" runat="server" AllowPaging="True"
                                    AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                    EditRowStyle-BackColor="#FFFF99"
                                    PageIndex="10" PageSize="25"
                                    PagerStyle-CssClass="pgr" Width="100%">
                                    <Columns>

                                        <asp:TemplateField HeaderText="PRODUCT TYPE">
                                            <ItemTemplate>
                                                <asp:Label ID="SUBGLCODE" runat="server" Text='<%# Eval("SUBGLCODE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="CUSTOMER NO">
                                            <ItemTemplate>
                                                <asp:Label ID="CUSTNO" runat="server" Text='<%# Eval("CUSTNO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="ACCOUNT NO" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="ACCNO" runat="server" Text='<%# Eval("ACCNO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="NAME" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="ANAME" runat="server" Text='<%# Eval("ANAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="INSTRUMENT NO" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="INSTNO" runat="server" CommandArgument='<%#Eval("INSTNO") %>' Text='<%# Eval("INSTNO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="INSTRUMENT DATE" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="INSTD" runat="server" CommandArgument='<%#Eval("INSTD") %>' Text='<%# Eval("INSTD") %>'></asp:Label>
                                            </ItemTemplate>
                                             </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ACCOUNT BALANCE">
                                            <ItemTemplate>
                                                <asp:Label ID="ABALANCE" runat="server" Text='<%# Eval("ABALANCE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="INSTRUMENT AMOUNT">
                                            <ItemTemplate>
                                                <asp:Label ID="IBAL" runat="server" Text='<%# Eval("IBAL") %>'></asp:Label>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>




