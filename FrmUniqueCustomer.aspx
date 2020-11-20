<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmUniqueCustomer.aspx.cs" Inherits="FrmUniqueCustomer" %>

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
            var message = '';
            var BrCode = document.getElementById('<% =txtBrCode.ClientID%>').value;
            var AsOnDate = document.getElementById('<% =txtAsOnDate.ClientID%>').value;

            if (BrCode == "") {
                message = 'Select any branch first...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<% =ddlBrName.ClientID%>').focus();
                return false;
            }

            if (AsOnDate == "") {
                message = 'Enter working date first...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<% =txtAsOnDate.ClientID%>').focus();
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
                        Unique Customer Process
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
                                                <div class="col-md-2">
                                                    <label class="control-label ">Branch Code<span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="ddlBrName" CssClass="form-control" OnSelectedIndexChanged="ddlBrName_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtBrCode" Enabled="false" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label ">As On Date<span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="txtAsOnDate" MaxLength="10" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="DD/MM/YYYY" runat="server"></asp:TextBox>
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
                    <p> <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label> </p>
                </div>
                <div class="modal-footer">
                    <button id="btnClose" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

