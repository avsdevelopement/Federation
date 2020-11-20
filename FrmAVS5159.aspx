<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAVS5159.aspx.cs" Inherits="FrmAVS5159" %>
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        DDS Collection Patch
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    
                                    <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                        <div class="col-md-12">
                                            <label class="control-label col-md-2" style="width: 140px">Branch Code :<span class="required">*</span></label>
                                            <div class="col-md-3">
                                                <asp:DropDownList ID="ddlBrName" CssClass="form-control" OnSelectedIndexChanged="ddlBrName_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtBrCode" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2" style="width: 140px">Agent Code :<span class="required">*</span></label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtAgentCode" runat="server" CssClass="form-control" OnTextChanged="txtAgentCode_TextChanged" AutoPostBack="true" onkeypress="javascript:return isNumber(event)" Placeholder="Product Code"></asp:TextBox>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="input-icon">
                                                    <i class="fa fa-search"></i>
                                                    <asp:TextBox ID="txtAgentName" runat="server" CssClass="form-control" OnTextChanged="txtAgentName_TextChanged" AutoPostBack="true" Placeholder="Search Agent Name" onkeypress="return checkQuote();"></asp:TextBox>
                                                    <div id="CustList1" style="height: 200px; overflow-y: scroll;"></div>
                                                    <asp:AutoCompleteExtender ID="AutoGlName" runat="server" TargetControlID="txtAgentName" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                        MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" ServiceMethod="GetGlWiseName" CompletionListElementID="CustList1">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2" style="width: 140px">Agent Balance :</label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtAgentBalance" Enabled="false" CssClass="form-control" runat="server" placeholder="Agent Balance" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                                <label class="control-label">Entry Date </label>
                                            </div>
                                            <div class="col-md-2">
                                                <label class="control-label">Online Collection </label>
                                            </div>
                                            <div class="col-md-2">
                                                <label class="control-label">Mobile Data Send </label>
                                            </div>
                                            <div class="col-md-2">
                                                <label class="control-label">Mobile Data Recived </label>
                                            </div>
                                            <div class="col-md-2">
                                                <label class="control-label">Posting Data </label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                        <div class="col-lg-12">
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtEDate" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this)" runat="server" CssClass="form-control" OnTextChanged="txtEDate_TextChanged" AutoPostBack="true" ></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtOnlineColl" Enabled="false" CssClass="form-control" runat="server" />
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtMobDataSend" Enabled="false" CssClass="form-control" runat="server" />
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtMobDataRec" Enabled="false" CssClass="form-control" runat="server" />
                                            </div>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtPostingData" Enabled="false" CssClass="form-control" runat="server" />
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-12">
                                        <asp:Button ID="btnMobDataSend" runat="server" CssClass="btn blue" Text="Mobile Data Send" UseSubmitBehavior="false" OnClick="btnMobDataSend_Click" OnClientClick="this.disabled='true';this.value='Please Wait'" />
                                        <asp:Button ID="btnUnAuthorize" runat="server" CssClass="btn blue" Text="Un-Authorize" UseSubmitBehavior="false" OnClick="btnUnAuthorize_Click" OnClientClick="this.disabled='true';this.value='Please Wait'" />
                                        <asp:Button ID="btnDeleteData" runat="server" CssClass="btn blue" Text="Delete Data" UseSubmitBehavior="false" OnClick="btnDeleteData_Click" OnClientClick="this.disabled='true';this.value='Please Wait'" />
                                        <asp:Button ID="btnClear" runat="server" CssClass="btn blue" Text="Clear All" OnClick="btnClear_Click" />
                                        <asp:Button ID="btnExit" runat="server" CssClass="btn blue" Text="Exit" OnClick="btnExit_Click" />
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

