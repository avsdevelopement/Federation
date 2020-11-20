<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="frmSMSMaster.aspx.cs" Inherits="frmSMSMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function validate() {
            var smsbdy = document.getElementById('<%=txtSmsBody.ClientID%>').value;
            var mob = document.getElementById('<%=txtMobileNo.ClientID%>').value;
            var msg = '';
            if (sms =="") {
                message = 'Please Fill SMS Body....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtSmsBody.ClientID%>').focus();
                return false;
            }
            if (mob == "")
            {
                message = 'Please Enter Mobile Number....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtMobileNo.ClientID%>').focus();
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
    
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="portlet box blue" id="form_wizard_1">
                        <div class="portlet-title">
                            <div class="caption">
                                SMS MASTER
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
                                                
                                                
                                                 <div class="row" style="margin: 7px 0 7px 0; padding-top:15px;">
                                                    <div class="col-lg-6">

                                                      
                                                        <%--<div class="col-md-4">
                                                           <asp:RadioButton ID="RBLALL" runat="server" Text="All" AutoPostBack="true"  RepeatDirection="Horizontal" OnCheckedChanged="RBLALL_CheckedChanged"></asp:RadioButton>
                                                        </div>
                                                         <div class="col-md-4">
                                                           <asp:RadioButton ID="RBLSPECIFIC" runat="server" Text="Specific" AutoPostBack="true" RepeatDirection="Horizontal" OnCheckedChanged="RBLSPECIFIC_CheckedChanged"></asp:RadioButton>
                                                            </div>
                                                        </div>--%>
                                                        <asp:RadioButtonList ID="Rdb_type" runat="server" RepeatDirection="Horizontal" Width="400px" AutoPostBack="true">
                                                            <asp:ListItem Text="Specific" Value="1" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="All" Value="2"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                   
                                                </div>
                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">

                                                      <div class="col-md-2">
                                                        <label class="control-label">SMS Body</label>
                                                       </div>
                                                        <div class="col-md-4">
                                                          <asp:TextBox ID="txtSmsBody" placeholder="Write a message.." AutoPostBack="true" TextMode="MultiLine" CssClass="form-control" runat="server" onkeypress="javascript:return validate()"></asp:TextBox>
                                                        </div>
                                                        </div>
                                                    </div>
                                                        <div id="div_mobile" class="row" runat="server" style="margin: 7px 0 7px 0">
                                                             <div class="col-lg-12">
                                                        <div class="col-md-2">
                                                        <label class="control-label">Mobile Number</label>
                                                       </div>
                                                        <div class="col-md-4">
                                                          <asp:TextBox ID="txtMobileNo" AutoPostBack="true" placeholder="Mobile No." CssClass="form-control" runat="server" onkeypress="javascript:return isNumber(event)" MaxLength="10"></asp:TextBox>
                                                        </div>

                                                       </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-lg-12" style="text-align: left; margin-top:12px;margin-bottom:13px; margin-left:15px;">
                                                        <div class="col-lg-6">
                                                        <asp:Button ID="BtnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="BtnSubmit_Click"/>
                                                            <asp:Button ID="BtnClearAll" runat="server" Text="Clear All" CssClass="btn btn-primary" OnClick="BtnClearAll_Click" />
                                                        <asp:Button ID="BtnExit" runat="server" Text="Exit" CssClass="btn btn-primary" OnClick="BtnExit_Click" />
                                                        <br />
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
        </ContentTemplate>
    </asp:UpdatePanel>
    

</asp:Content>

