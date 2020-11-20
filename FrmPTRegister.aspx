<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmPTRegister.aspx.cs" Inherits="FrmPTRegister" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script type="text/javascript">
         function isvalidate() {

             var Brcode, Mon, Year, RDiv, RCode, DebitC;
             Brcode = document.getElementById('<%=ddlBrCode.ClientID%>').value;
              Mon = document.getElementById('<%=TxtMM.ClientID%>').value;
              Year = document.getElementById('<%=TxtYYYY.ClientID%>').value;
              

              var message = '';

              if (Brcode == "0") {
                  message = 'Enter Branch Code....!!\n';
                  $('#alertModal').find('.modal-body p').text(message);
                  $('#alertModal').modal('show')
                  document.getElementById('<%=ddlBrCode.ClientID%>').focus();
                 return false;
             }

             if (Mon == "") {
                 message = 'Enter Month....!!\n';
                 $('#alertModal').find('.modal-body p').text(message);
                 $('#alertModal').modal('show')
                 document.getElementById('<%=TxtMM.ClientID%>').focus();
                return false;
            }

            if (Year == "") {
                message = 'Enter Year....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtYYYY.ClientID%>').focus();
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
            <div class="panel-heading">PT Register</div>
            <div class="panel-body">
                <div class="tab-content">

                    <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                        <div class="col-lg-12">
                        </div>
                    </div>
                     <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                        <div class="col-lg-12">
                            <div class="col-md-1">
                                <label class="control-label ">Brcd</label>
                            </div>
                            <div class="col-md-2">
                                <asp:DropDownList ID="ddlBrCode" CssClass="form-control" runat="server" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-1">
                                <label class="control-label ">MM/YYYY</label>
                            </div>
                            <div class="col-md-1">
                                <asp:TextBox ID="TxtMM" MaxLength="2" placeholder="MM"  onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server" OnTextChanged="TxtMM_TextChanged" AutoPostBack="true" />
                            </div>
                            <div class="col-md-1">
                                <asp:TextBox ID="TxtYYYY" MaxLength="4" placeholder="YYYY"  onkeypress="javascript:return isNumber(event)" CssClass="form-control" runat="server" OnTextChanged="TxtYYYY_TextChanged" AutoPostBack="true" />
                            </div>
                            
                        </div>
                    </div>
                    <div class="row" style="margin-top: 12px; margin-bottom: 10px;">
                        <div class="col-lg-12">
                            <div class="col-md-1">
                                <label class="control-label ">Rec div:</label>
                            </div>
                            <div class="col-md-2">
                                <asp:DropDownList ID="DdlRecDiv" CssClass="form-control" runat="server" OnSelectedIndexChanged="DdlRecDiv_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-1">
                                <label class="control-label ">Rec Dept:</label>
                            </div>
                            <div class="col-md-6">
                                <asp:DropDownList ID="DdlRecDept" CssClass="form-control" runat="server">
                                </asp:DropDownList>
                            </div>

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12 text-center">
                            <asp:Button ID="Btn_Report" runat="server" CssClass="btn btn-danger" Text="Show" OnClick="Btn_Report_Click"  OnClientClick="Javascript:return isvalidate();" />
                            <asp:Button ID="Btn_ClearAll" runat="server" CssClass="btn btn-success" Text="Clear All" OnClick="Btn_ClearAll_Click" />
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

