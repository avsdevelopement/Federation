<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAVS5147.aspx.cs" Inherits="FrmAVS5147" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46) {
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        Folio Charges
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">

                                    <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                        <div class="col-md-12">
                                            <label class="control-label col-md-2">Posting Date :</label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtEDate" runat="server" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this)" CssClass="form-control" ></asp:TextBox>
                                            </div>
                                            <label class="control-label col-md-2">Apply Charges :</label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtCharges" runat="server" onkeypress="javascript:return isNumber (event)" CssClass="form-control" ></asp:TextBox>
                                            </div>
                                            <asp:Label ID="lblStatus" Text="" runat="server" />
                                        </div>
                                    </div>

                                    <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                        <div class="col-md-12">
                                            <label class="control-label col-md-2">From Branch :</label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtFBrCode" runat="server" onkeypress="javascript:return isNumber (event)" CssClass="form-control" ></asp:TextBox>
                                            </div>
                                            <label class="control-label col-md-2">To Branch :</label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtTBrCode" runat="server" onkeypress="javascript:return isNumber (event)" CssClass="form-control" ></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                        <div class="col-md-12">
                                            <label class="control-label col-md-2">Debit Product :</label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtDrProdCode" runat="server" CssClass="form-control" onkeypress="javascript:return isNumber (event)" OnTextChanged="txtDrProdCode_TextChanged" AutoPostBack="true" ></asp:TextBox>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="input-icon">
                                                    <i class="fa fa-search"></i>
                                                    <asp:TextBox ID="txtDrProdName" CssClass="form-control" Placeholder="Search Product Name" runat="server" AutoPostBack="true" OnTextChanged="txtDrProdName_TextChanged"></asp:TextBox>
                                                    <div id="CustList1" style="height: 200px; overflow-y: scroll;"></div>
                                                    <asp:AutoCompleteExtender ID="AutoDrGlName" runat="server" TargetControlID="txtDrProdName" UseContextKey="true"
                                                        CompletionInterval="1" CompletionSetCount="20" MinimumPrefixLength="1" EnableCaching="true"
                                                        ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList1" ServiceMethod="GetGlWiseName">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    
                                    <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                        <div class="col-md-12">
                                            <label class="control-label col-md-2">Credit Product :</label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtCrProdCode" runat="server" CssClass="form-control" onkeypress="javascript:return isNumber (event)" OnTextChanged="txtCrProdCode_TextChanged" AutoPostBack="true" ></asp:TextBox>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="input-icon">
                                                    <i class="fa fa-search"></i>
                                                    <asp:TextBox ID="txtCrProdName" CssClass="form-control" Placeholder="Search Product Name" runat="server" AutoPostBack="true" OnTextChanged="txtCrProdName_TextChanged"></asp:TextBox>
                                                    <div id="CustList2" style="height: 200px; overflow-y: scroll;"></div>
                                                    <asp:AutoCompleteExtender ID="AutoCrGlName" runat="server" TargetControlID="txtCrProdName" UseContextKey="true"
                                                        CompletionInterval="1" CompletionSetCount="20" MinimumPrefixLength="1" EnableCaching="true"
                                                        ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList2" ServiceMethod="GetGlWiseName">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                        <div class="col-md-12">
                                            <label class="control-label col-md-2">Narration :</label>
                                            <div class="col-md-5">
                                                <asp:TextBox ID="txtNarration" runat="server" CssClass="form-control" Placeholder="Enter some narration" />
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-12">
                                        <asp:Button ID="btnTrail" runat="server" CssClass="btn blue" Text="Trail" UseSubmitBehavior="false" OnClick="btnTrail_Click" OnClientClick="this.disabled='true';this.value='Please Wait'" />
                                        <asp:Button ID="btnApply" runat="server" CssClass="btn blue" Text="Apply" UseSubmitBehavior="false" OnClick="btnApply_Click" OnClientClick="this.disabled='true';this.value='Please Wait'" />
                                        <asp:Button ID="btnClear" runat="server" CssClass="btn blue" Text="ClearAll" OnClick="btnClear_Click" />
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

