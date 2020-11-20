
<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAVS5072.aspx.cs" Inherits="FrmAVS5072" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script type="text/javascript">
         function isvalidate() {

             var Brcode, FMon, FYear, TMon, TYear, RDiv, RCode, DebitC;
             Brcode = document.getElementById('<%=ddlBrCode.ClientID%>').value;
            FMon = document.getElementById('<%=TxtFMM.ClientID%>').value;
             FYear = document.getElementById('<%=TxtFYYYY.ClientID%>').value;
             TMon = document.getElementById('<%=TxtTMM.ClientID%>').value;
             TYear = document.getElementById('<%=TxtTYYYY.ClientID%>').value;
            


            var message = '';

            if (Brcode == "0") {
                message = 'Enter Branch Code....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=ddlBrCode.ClientID%>').focus();
                return false;
            }

            if (FMon == "") {
                message = 'Enter From Month....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtFMM.ClientID%>').focus();
                return false;
            }

            if (FYear == "") {
                message = 'Enter From Year....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtFYYYY.ClientID%>').focus();
                return false;
            }

             if (TMon == "") {
                 message = 'Enter To Month....!!\n';
                 $('#alertModal').find('.modal-body p').text(message);
                 $('#alertModal').modal('show')
                 document.getElementById('<%=TxtTMM.ClientID%>').focus();
                return false;
            }

            if (TYear == "") {
                message = 'Enter To Year....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtTYYYY.ClientID%>').focus();
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="page-wrapper">

        <div class="panel panel-warning">
            <div class="panel-heading">Recovery Opeartion </div>
            <div class="panel-body">
                <div class="tab-content">
                   
                    <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                        <div class="col-lg-12">
                            <div class="col-md-2">
                                <label class="control-label ">Brcd</label>
                            </div>
                            <div class="col-md-4">
                                <asp:DropDownList ID="ddlBrCode" CssClass="form-control" runat="server" OnTextChanged="ddlBrCode_TextChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 5px; margin-bottom: 5px;">
                        <div class="col-lg-12">

                            <div class="col-md-2">
                                <label class="control-label ">From MM/YYYY</label>
                            </div>
                            <div class="col-md-1">
                                <asp:TextBox ID="TxtFMM" placeholder="MM" onkeypress="javascript:return isNumber(event)" OnTextChanged="TxtFMM_TextChanged" CssClass="form-control" runat="server" AutoPostBack="true" />
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="TxtFYYYY" placeholder="YYYY" onkeypress="javascript:return isNumber(event)" OnTextChanged="TxtFYYYY_TextChanged" CssClass="form-control" runat="server"  AutoPostBack="true" />
                            </div>

                            <div class="col-md-2">
                                <label class="control-label ">To MM/YYYY</label>
                            </div>
                            <div class="col-md-1">
                                <asp:TextBox ID="TxtTMM" placeholder="MM" onkeypress="javascript:return isNumber(event)" OnTextChanged="TxtTMM_TextChanged" CssClass="form-control" runat="server" AutoPostBack="true" />
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="TxtTYYYY" placeholder="YYYY" onkeypress="javascript:return isNumber(event)" OnTextChanged="TxtTYYYY_TextChanged" CssClass="form-control" runat="server" AutoPostBack="true" />
                            </div>

                        </div>
                    </div>
                 



                    <div class="row">
                        <div class="col-lg-12 text-center">
                            <asp:Button ID="Btn_Report" runat="server" CssClass="btn btn-success" Text="Report" Visible="true" OnClick="Btn_Report_Click" />
                            <asp:Button ID="BtnExit" runat="server" CssClass="btn btn-danger" Text="Exit" OnClick="BtnExit_Click" />

                        </div>


                    </div>

                </div>
            </div>

        </div>

    <asp:HiddenField ID="hdnCustNo" runat="server" Value="0" />

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

</asp:Content>

