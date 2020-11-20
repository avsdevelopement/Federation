<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAVS5038.aspx.cs" Inherits="FrmAVS5038" %>

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
               { alert("Please Enter valid Date"); obj.value = "";}
           }

           function IsValid() {
               var TxtFdate = document.getElementById('<%=TxtFdate.ClientID%>').value;
               var TxtToDate = document.getElementById('<%=TxtToDate.ClientID%>').value;
               var TxtSrno = document.getElementById('<%=TxtSrno.ClientID%>').value;
               var TxtDirectNo = document.getElementById('<%=TxtDirectNo.ClientID%>').value;
               var TxtDirectName = document.getElementById('<%=TxtDirectName.ClientID%>').value;
               var TxtMobile = document.getElementById('<%=TxtMobile.ClientID%>').value;
               var ddlPost = document.getElementById('<%=ddlPost.ClientID%>').value;
               var ddlSMS = document.getElementById('<%=ddlSMS.ClientID%>').value;

              var message = '';

              if (TxtFdate == "") {
                  message = 'Please enter From Date...!!\n';
                  $('#alertModal').find('.modal-body p').text(message);
                  $('#alertModal').modal('show')
                  document.getElementById('<%=TxtFdate.ClientID%>').focus();
                return false;
            }

               if (TxtToDate == "") {
                message = 'Please Enter To Date...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtToDate.ClientID%>').focus();
                return false;
            }

               if (TxtSrno == "") {
                message = 'Please Enter Srno...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtSrno.ClientID%>').focus();
                return false;
            }

               if (TxtDirectNo == "") {
                message = 'Please enter Director No...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtDirectNo.ClientID%>').focus();
                return false;
            }

               if (TxtDirectName == "") {
                message = 'Please Enter director name...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtDirectName.ClientID%>').focus();
                return false;
            }

               if (TxtMobile == "") {
                message = 'Please Enter Mobile no..!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=TxtMobile.ClientID%>').focus();
                return false;
            }

               if (ddlPost == "0") {
                message = 'Please Select Post...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=ddlPost.ClientID%>').focus();
                return false;
            }

               if (ddlSMS == "0") {
                message = 'Please select Sms...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=ddlSMS.ClientID%>').focus();
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
                        Director Details
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                    <div style="border: 1px solid #3598dc">
                                        <div class="row" style="margin-top: 10px">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label">From Date <span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtFdate" onkeyup="FormatIt(this);" onkeypress="javascript:return isNumber(event)" placeholder="dd/mm/yyyy" CssClass="form-control" runat="server" TabIndex="1" />
                                                    <asp:CalendarExtender ID="TxtFdate_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtFdate">
                                                    </asp:CalendarExtender>
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label">To Date <span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtToDate" onkeyup="FormatIt(this);" onkeypress="javascript:return isNumber(event)" placeholder="dd/mm/yyyy" CssClass="form-control" runat="server" TabIndex="2" />
                                                    <asp:CalendarExtender ID="TxtToDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtToDate">
                                                    </asp:CalendarExtender>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 10px">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label">SR No <span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtSrno" onkeypress="javascript:return isNumber(event)" placeholder="Srno" CssClass="form-control" runat="server" TabIndex="3" />

                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label">Director No <span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtDirectNo" onkeypress="javascript:return isNumber(event)" placeholder="Director No" CssClass="form-control" runat="server" TabIndex="4" AutoPostBack="true" OnTextChanged="TxtDirectNo_TextChanged" />
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="TxtDirectName" placeholder="Director Name" CssClass="form-control" runat="server" OnTextChanged="TxtDirectName_TextChanged" AutoPostBack="true" TabIndex="5" />
                                                        <asp:AutoCompleteExtender ID="autoCustname" runat="server" TargetControlID="TxtDirectName"
                                                    UseContextKey="true"
                                                    CompletionInterval="1"
                                                    CompletionSetCount="20"
                                                    MinimumPrefixLength="1"
                                                    EnableCaching="true"
                                                    ServicePath="~/WebServices/Contact.asmx"
                                                    ServiceMethod="GetCustNames">
                                                </asp:AutoCompleteExtender>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 10px">
                                            <div class="col-lg-12">
                                                <div class="col-md-2">
                                                    <label class="control-label">Mobile No <span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:TextBox ID="TxtMobile" onkeypress="javascript:return isNumber(event)" TabIndex="6" placeholder="Mobile No" CssClass="form-control" runat="server" />
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label">Post <span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:DropDownList runat="server" ID="ddlPost" CssClass="form-control" ToolTip="Lookupform1 1105" TabIndex="7"></asp:DropDownList>
                                                </div>
                                                <div class="col-md-1">
                                                    <label class="control-label">SMS <span class="required">*</span></label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:DropDownList runat="server" ID="ddlSMS" CssClass="form-control" TabIndex="8">
                                                        <asp:ListItem Value="0" Selected="True">--Select--</asp:ListItem>
                                                        <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                        <asp:ListItem Value="N">No</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-actions">
                                            <div class="row" style="margin-top: 1px;">
                                                <div class="col-md-offset-3 col-md-9">
                                                    <asp:Button ID="BtnSave" runat="server" Text="Submit" CssClass="btn blue" OnClick="BtnSave_Click" OnClientClick="Javascript:return IsValid();" TabIndex="9" />
                                                    <asp:Button ID="BtnModify" runat="server" Text="Modify" CssClass="btn blue" OnClick="BtnModify_Click" OnClientClick="Javascript:return IsValid();" Visible="false" TabIndex="10" />
                                                    <asp:Button ID="BtnDelete" runat="server" Text="Delete" CssClass="btn blue" OnClick="BtnDelete_Click" OnClientClick="Javascript:return IsValid();" Visible="false" TabIndex="11" />
                                                    <asp:Button ID="BtnClear" runat="server" Text="Clear" CssClass="btn blue" OnClick="BtnClear_Click" TabIndex="12" />
                                                    <asp:Button ID="BtnExit" runat="server" Text="Exit" CssClass="btn blue" OnClick="BtnExit_Click" TabIndex="13" />
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
      <div class="col-md-12">
                        <div class="table-scrollable">
                            <table class="table table-striped table-bordered table-hover">
                                <thead>
                                    <tr>
                                        <th>
                                            <asp:GridView ID="grdDirector" runat="server" AllowPaging="True" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False"
                                                EditRowStyle-BackColor="#FFFF99" PagerStyle-CssClass="pgr" Width="100%" OnPageIndexChanging="grdDirector_PageIndexChanging">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="DirNo">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDirNo" runat="server" Text='<%# Eval("DIRNO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                     <asp:TemplateField HeaderText="Custno">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCustno" runat="server" Text='<%# Eval("CUSTNO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                     <asp:TemplateField HeaderText="DirName">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDirName" runat="server" Text='<%# Eval("DIRNAME") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                     <asp:TemplateField HeaderText="Post">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPost" runat="server" Text='<%# Eval("POST") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                     <asp:TemplateField HeaderText="Mobile No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMobileNo" runat="server" Text='<%# Eval("MOBILENO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="SMS Y/N">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSMS" runat="server" Text='<%# Eval("SMSYN") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="Add New" Visible="true" runat="server">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkAdd" runat="server" OnClick="lnkAdd_Click" class="glyphicon glyphicon-plus"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="Modify" Visible="true" runat="server">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkMod" runat="server" CommandName='<%# Eval("DIRNO") %>' CommandArgument='<%#Eval("DIRNO")%>' OnClick="lnkMod_Click" class="glyphicon glyphicon-pencil"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="Delete" Visible="true" runat="server">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDel" runat="server" CommandName='<%# Eval("DIRNO") %>' CommandArgument='<%#Eval("DIRNO")%>' OnClick="lnkDel_Click" class="glyphicon glyphicon-trash"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                                <PagerStyle CssClass="pgr" />
                                                <SelectedRowStyle BackColor="#66FF99" />
                                                <EditRowStyle BackColor="#FFFF99" />
                                                <AlternatingRowStyle CssClass="alt" />
                                            </asp:GridView>
                                        </th>
                                    </tr>
                                </thead>
                            </table>
                        </div>
                    </div>
</asp:Content>

