<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="AVS5036.aspx.cs" Inherits="AVS5036" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
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
        function IsValide() {

            var Operation, PostDate;
            Operation = document.getElementById('<%=ddlOperation.ClientID%>').value;
            PostDate = document.getElementById('<%=txtPostDate.ClientID%>').value;
            var message = '';

            if (Operation == "0") {
                message = 'Select operation first...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=ddlOperation.ClientID%>').focus();
                return false;
            }

            if (PostDate == "") {
                message = 'Enter post date first...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtPostDate.ClientID%>').focus();
                return false;
            }
        }
    </script>
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        Standing Instruction
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">

                                    <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                        <div class="col-lg-12">
                                            <div class="col-md-2">
                                                <label class="control-label ">SI Type :<span class="required"> *</span></label>
                                            </div>
                                            <div class="col-md-10">
                                                <asp:RadioButtonList ID="rbtnSIType" runat="server" RepeatDirection="Horizontal" Style="width: 400px;" >
                                                    <asp:ListItem Text="SB To RD" Value="1" Selected="True"/>
                                                    <asp:ListItem Text="SB To Loan" Value="2" />
                                                    <%--<asp:ListItem Text="DDS To Loan" Value="3" />--%>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                                        <div class="col-lg-12">
                                            <div class="col-md-2">
                                                <label class="control-label ">Operation :<span class="required"> *</span></label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:DropDownList ID="ddlOperation" CssClass="form-control" runat="server" />
                                            </div>
                                            <div class="col-md-2">
                                                <label class="control-label ">Post Date :<span class="required"> *</span></label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtPostDate" runat="server" CssClass="form-control" onkeyup="FormatIt(this);" onkeypress="javascript:return isNumber(event)" placeholder="DD/MM/YYYY" />
                                                <%--<asp:CalendarExtender ID="txtPostDate_CE" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtPostDate" />--%>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="form-actions">
                                <div class="row">
                                    <div class="col-md-offset-3 col-md-9">
                                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn blue" Text="Submit" OnClick="btnSubmit_Click" OnClientClick="Javascript:return IsValide();" />
                                        <asp:Button ID="btnReport" runat="server" CssClass="btn blue" Text="Report" OnClick="btnReport_Click" />
                                        <asp:Button ID="btnClear" runat="server" CssClass="btn blue" Text="Clear All" OnClick="btnClear_Click" />
                                        <asp:Button ID="btnExit" runat="server" CssClass="btn btn-primary" Text="Exit" OnClick="btnExit_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                    </div>
                </div>
            </div>
        </div>

        <div id="alertModal" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <center><h4 class="modal-title" style="color:#ff0000">AVS Company</h4></center>
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

    </div>
</asp:Content>

