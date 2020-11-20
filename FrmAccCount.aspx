<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAccCount.aspx.cs" Inherits="FrmAccCount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function validate() {
            var brcd = document.getElementById('<%=TxtBRCD.ClientID%>').value;
            var glcode = document.getElementById('<%=TxtPRD.ClientID%>').value;
            if (brcd == "") {
                message = 'Please Fill SMS Body....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtBRCD.ClientID%>').focus();
                return false;
            }
            if (glcode == "") {
                message = 'Please Fill SMS Body....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtPRD.ClientID%>').focus();
                return false;
            }
        }
    </script>
     <script>
         function isNumber(evt) {
             var iKeyCode = (evt.which) ? evt.which : evt.keyCode
             if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                 return false;

             return true;
         }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="col-md-12">
        <div class="portlet box green" id="Div1">
                 <div class="portlet-title">
                      <div class="caption">
                         ACCOUNT NO. COUNT
                      </div>
                  </div>
              <div class="portlet-body form">
                <div class="form-horizontal">
                    <div class="form-wizard">
                        <div class="form-body">
                            <div class="tab-content">
                                <div class="tab-pane active" id="tab1">
                                    <div class="row" style="margin-bottom: 10px;">
                                        <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                <label class="control-label col-md-1">BRCD:<span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtBRCD" Placeholder="BRCD" onkeypress="javascript:return isNumber(evt)" CssClass="form-control" runat="server" OnTextChanged="TxtBRCD_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                               <div class="col-md-3">
                                                    <asp:TextBox ID="TxtBRCDName" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                                                </div>
                                             </div>
                                         
                                        </div>
                                         <div class="row" style="margin: 7px 0 7px 0">
                                            <div class="col-lg-12">
                                                 <label class="control-label col-md-1">GLCode:<span class="required">*</span></label>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtPRD" Placeholder="GLCode" onkeypress="javascript:return isNumber(evt)" CssClass="form-control" runat="server" OnTextChanged="TxtPRD_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                                </div>
                                             </div>
                                      
                                    <div class="row" style="margin: 7px 0 7px 0; text-align: center">
                                            <div class="col-lg-12">
                                                <div class="col-md-6">

                                                   <asp:Button ID="Btnreport" runat="server" Text="Report" CssClass="btn btn-success" OnClick="Btnreport_Click" />
                                                   
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
</asp:Content>

