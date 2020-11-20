<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmInwardAuthorize.aspx.cs" Inherits="FrmInwardAuthorize" %>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="portlet box blue" id="form_wizard_1">
                        <div class="portlet-title">
                            <div class="caption">
                                Inward Authorize
                                
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
                                                <label class="control-label col-md-3">Bank Code : <span class="required">* </span></label>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtbankcd" CssClass="form-control" runat="server" OnTextChanged="txtbankcd_TextChanged" AutoPostBack="true" onkeypress="javascript:return isNumber (event)" PlaceHolder="Bank code"></asp:TextBox>
                                                </div>
                                                <div class="col-md-5">
                                                    <asp:TextBox ID="txtbnkdname" CssClass="form-control" runat="server" AutoPostBack="true" PlaceHolder="Bank name" OnTextChanged="txtbnkdname_TextChanged"></asp:TextBox>
                                                    <asp:AutoCompleteExtender ID="AutoBank" runat="server" TargetControlID="txtbnkdname"
                                                        UseContextKey="true"
                                                        CompletionInterval="1"
                                                        CompletionSetCount="20"
                                                        MinimumPrefixLength="1"
                                                        EnableCaching="true"
                                                        ServicePath="~/WebServices/Contact.asmx"
                                                        ServiceMethod="GetBankName">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                            </div>
                                                </div>

                                                <div class="row" style="margin: 7px 0 7px 0; padding-top: 15px;">
                                                    <div class="col-lg-6">
                                                         
                                                            <label id="LblFDT" class="control-label col-md-3">Date</label>
                                                        
                                                        <div class="col-md-5">
                                                            <asp:TextBox ID="TxtINWDate" CssClass="form-control" PlaceHolder="dd/mm/yyyy" runat="server" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                            <asp:TextBoxWatermarkExtender ID="TxtFDate_WatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtINWDate">
                                                            </asp:TextBoxWatermarkExtender>
                                                            <asp:CalendarExtender ID="TxtFDate_CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtINWDate">
                                                            </asp:CalendarExtender>
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
                                                            <asp:Button ID="Authorize" runat="server" CssClass="btn btn-primary" Text="Process" OnClick="Authorize_Click" />
                                                            <asp:Button ID="Exit" runat="server" Text="Exit" CssClass="btn btn-primary" />
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
                                    EditRowStyle-BackColor="#FFFF99" OnRowDataBound="GrdAN_RowDataBound"
                                    PageIndex="10" PageSize="25"
                                    OnSelectedIndexChanged="GrdAN_SelectedIndexChanged"
                                    PagerStyle-CssClass="pgr" Width="100%">
                                    <Columns>

                                        <asp:TemplateField HeaderText="PRODUCT TYPE">
                                            <ItemTemplate>
                                                <asp:Label ID="SUBGLCODE" runat="server" CommandArgument='<%#Eval("SUBGLCODE") %>' Text='<%# Eval("SUBGLCODE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CUSTOMER NO">
                                            <ItemTemplate>
                                                <asp:Label ID="CUSTNO" runat="server" CommandArgument='<%#Eval("CUSTNO") %>' Text='<%# Eval("CUSTNO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="ACCOUNT NO" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="ACCNO" runat="server" CommandArgument='<%#Eval("ACCNO") %>' Text='<%# Eval("ACCNO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="NAME" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="ANAME" runat="server" CommandArgument='<%#Eval("ANAME") %>' Text='<%# Eval("ANAME") %>'></asp:Label>
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
                                                <asp:Label ID="IBAL" runat="server" CommandArgument='<%#Eval("IBAL") %>' Text='<%# Eval("IBAL") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Authorize" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkAutho" runat="server" CommandName="select" class="glyphicon glyphicon-plus" OnClick="LnkAutho_Click"></asp:LinkButton>                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Verify Account" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LnkVerify" runat="server" OnClick="LnkVerify_Click" CommandArgument='<%#Eval("CUSTNO") %>' CommandName="select" class="glyphicon glyphicon-search"></asp:LinkButton>                                                
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






