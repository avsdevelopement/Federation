<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmScrutinySheet.aspx.cs" Inherits="FrmScrutinySheet" %>
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
    
    <script type="text/javascript">
        function IsValidate() {

            var AppNo = document.getElementById('<%=txtAppNo.ClientID%>').value;
            var CustNo = document.getElementById('<%=txtCustNo.ClientID%>').value;

            var message = '';

            if (AppNo == "") {
                message = 'Enter application number first...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtAppNo.ClientID%>').focus();
                return false;
            }

            if (CustNo == "") {
                message = 'Enter customer number first...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtCustNo.ClientID%>').focus();
                return false;
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        Scrutiny Sheet Report
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
                                            <div class="col-lg-12">
                                                <div class="col-md-1">
                                                    <label class="control-label ">Cust No:<span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtCustNo" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtCustNo_TextChanged" PlaceHolder="Customer No"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="input-icon">
                                                        <i class="fa fa-search"></i>
                                                        <asp:TextBox ID="txtCustName" CssClass="form-control" PlaceHolder="Search Customer Name" OnTextChanged="txtCustName_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                                        <div id="CustList2" style="height: 200px; overflow-y: scroll;"></div>
                                                        <asp:AutoCompleteExtender ID="AutoCustName" runat="server" TargetControlID="txtCustName" UseContextKey="true" CompletionInterval="1" CompletionSetCount="20"
                                                            MinimumPrefixLength="1" EnableCaching="true" ServicePath="~/WebServices/Contact.asmx" CompletionListElementID="CustList2" ServiceMethod="GetCustNames">
                                                        </asp:AutoCompleteExtender>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">Application No:<span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtAppNo" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtAppNo_TextChanged" PlaceHolder="Application No"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="form-actions">
                            <div class="row">
                                <div class="col-md-offset-3 col-md-9">
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn blue" Text="SUBMIT" OnClick="btnSubmit_Click" OnClientClick="Javascript:return IsValidate();" />
                                </div>
                            </div>
                        </div>
                    </div>
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
                    <p>
                        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                    </p>

                </div>
                <div class="modal-footer">
                    <button id="btnClose" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

