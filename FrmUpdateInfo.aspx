<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmUpdateInfo.aspx.cs" Inherits="FrmUpdateInfo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
        function IsValide() {
            var ddlprefix = document.getElementById('<%=Txtprodcode.ClientID%>').value;
            var message = '';

            if (txtcstno == "") {
                //alert("Please Select Prefix.....!!");
                message = 'Please Enter Productcode number....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=Txtprodcode.ClientID %>').focus();
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
        function FormatIt(obj) {
            if (obj.value.length == 2) // Day
                obj.value = obj.value + "/";
            if (obj.value.length == 5) // month 
                obj.value = obj.value + "/";
            if (obj.value.length == 11) // year 
                alert("Please Enter valid Date");
        }
    </script>
    <style type="text/css">
        .btn {
            text-decoration: none;
            border: 1px solid #000;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        <div class="portlet box green" id="Div1">
            <div class="portlet-title">
                <div class="caption">
                    Update Information Master
                </div>
            </div>
            <div class="portlet-body form">
                <div class="form-horizontal">
                    <div class="form-wizard">
                        <div class="form-body">
                            <div class="tab-content">
                                <div class="tab-pane active" id="tab1">

                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Branch No</label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtBrcd" CssClass="form-control" runat="server" TabIndex="9"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin: 7px 0 7px 0" runat="server">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-2">Product Code<span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="Txtprodcode" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" OnTextChanged="Txtprodcode_TextChanged" AutoPostBack="true" TabIndex="3"></asp:TextBox>
                                                </div>
                                                <div class="col-lg-4">
                                                    <asp:TextBox ID="txtname" runat="server" CssClass="form-control" AutoPostBack="true" TabIndex="4" OnTextChanged="txtname_TextChanged"></asp:TextBox>
                                                    <div id="CustList3" style="height: 200px; overflow-y: scroll;"></div>
                                                    <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="txtname"
                                                        UseContextKey="true"
                                                        CompletionInterval="1"
                                                        CompletionSetCount="20"
                                                        MinimumPrefixLength="1"
                                                        EnableCaching="true"
                                                        ServicePath="~/WebServices/Contact.asmx"
                                                        ServiceMethod="GetCustNames" CompletionListElementID="CustList3">
                                                    </asp:AutoCompleteExtender>
                                                </div>


                                            </div>
                                        </div>




                                    <div id="DIVACC" runat="server" class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Account No</label>
                                            <div class="col-lg-2">
                                                <asp:TextBox ID="txtaccno" runat="server" CssClass="form-control" onkeypress="javascript:return isNumber (event)" OnTextChanged="txtaccno_TextChanged" AutoPostBack="true" TabIndex="5"></asp:TextBox>
                                            </div>
                                            <div class="col-lg-4">
                                                <asp:TextBox ID="TxtAccName" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="TxtAccName_TextChanged" TabIndex="6"></asp:TextBox>
                                                <div id="CustList" style="height: 200px; overflow-y: scroll;"></div>
                                                <asp:AutoCompleteExtender ID="AutoAccname" runat="server" TargetControlID="TxtAccName"
                                                    UseContextKey="true"
                                                    CompletionInterval="1"
                                                    CompletionSetCount="20"
                                                    MinimumPrefixLength="1"
                                                    EnableCaching="true"
                                                    ServicePath="~/WebServices/Contact.asmx"
                                                    ServiceMethod="GetAccName" CompletionListElementID="CustList">
                                                </asp:AutoCompleteExtender>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Show Last Date</label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="txtlastdate" CssClass="form-control" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this);CheckForFutureDate()" onkeypress="javascript:return isNumber (event)" AutoPostBack="true" runat="server" TabIndex="18"></asp:TextBox>
                                                <asp:CalendarExtender ID="TxtLDT_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtlastdate">
                                                </asp:CalendarExtender>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin: 7px 0 7px 0">
                                        <div class="col-lg-12">
                                            <label class="control-label col-md-2">Update Date</label>
                                            <div class="col-md-2">
                                                <asp:TextBox ID="Txtupdate" CssClass="form-control" placeholder="DD/MM/YYYY" onkeyup="FormatIt(this);CheckForFutureDate()" onkeypress="javascript:return isNumber (event)" AutoPostBack="true" runat="server" TabIndex="18"></asp:TextBox>

                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="Txtupdate">
                                                </asp:CalendarExtender>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin: 7px 0 7px 0; text-align: center">
                                        <div class="col-lg-12">
                                            <div class="col-md-6">

                                                <asp:Button ID="Btn_Submit" runat="server" Text="Submit" CssClass="btn btn-success " OnClick="Btn_Submit_Click" OnClientClick="return btnSubmit_Click();" />
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
        
</asp:Content>

