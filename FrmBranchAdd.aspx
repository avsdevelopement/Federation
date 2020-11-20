<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmBranchAdd.aspx.cs" MasterPageFile="~/CBSMaster.master" Inherits="FrmBranchAdd" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

      <script type="text/javascript">
          function isvalidate() {

              var txtBrcode, txtadd1, txtadd2, txtmobile, txtEmail, txtdayopen;
              txtBrcode = document.getElementById('<%=txtBrcode.ClientID%>').value;
              txtadd1 = document.getElementById('<%=txtadd1.ClientID%>').value;
              txtadd2 = document.getElementById('<%=txtadd2.ClientID%>').value;
              txtmobile = document.getElementById('<%=txtmobile.ClientID%>').value;
              txtEmail = document.getElementById('<%=txtEmail.ClientID%>').value;
              txtdayopen = document.getElementById('<%=txtdayopen.ClientID%>').value;
            var message = '';

            if (txtBrcode == "") {
                message = 'Please Enter Br Code...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtBrcode.ClientID%>').focus();
                return false;
            }
            debugger;
            if (txtadd1 == "") {
                message = 'Please Enter Addr 1...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtadd1.ClientID%>').focus();
                return false;
            }

              if (txtadd2 == "") {
                message = 'Please Enter Add 2...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtadd2.ClientID%>').focus();
                return false;
            }

              if (txtmobile == "") {
                message = 'Please Enter Mobile No...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtmobile.ClientID%>').focus();
                return false;
            }
              if (txtEmail == "") {
                message = 'Please Enter Email Add...!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                document.getElementById('<%=txtEmail.ClientID%>').focus();
                return false;
              }
              if (txtdayopen == "") {
                  message = 'Please Enter Day Open Date...!!\n';
                  $('#alertModal').find('.modal-body p').text(message);
                  $('#alertModal').modal('show')
                  document.getElementById('<%=txtdayopen.ClientID%>').focus();
                return false;
            }


        }
    </script>

    <script type="text/javascript">
        function FormatIt(obj) {
            if (obj.value.length == 2) // Day
                obj.value = obj.value + "/";
            if (obj.value.length == 5) // month 
                obj.value = obj.value + "/";
            if (obj.value.length == 11) // year 
                alert("Please Enter valid Date");
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="portlet box blue" id="Div1">
                <div class="portlet-title">
                    <div class="caption">
                       Branch Create 
                    </div>
                </div>
                <div class="portlet-body form">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <div class="tab-content">
                                        <div class="tab-pane active" id="tab__blue">
                                         <asp:LinkButton ID="LBtn_Add" Text="Add A/C" runat="server" CssClass="btn" style="font-family:Verdana;" OnClick= "LBtn_Add_Click"></asp:LinkButton>
                                          &nbsp;&nbsp;
                                         <asp:LinkButton ID="LBtn_Mod" Text="Modify A/C" runat="server" CssClass="btn" style="font-family:Verdana;" OnClick= "LBtn_Mod_Click"></asp:LinkButton>
                                          &nbsp;&nbsp;
                                          <asp:LinkButton ID="LBtn_View" Text="View A/C" runat="server"  CssClass="btn" style="font-family:Verdana;" OnClick= "LBtn_View_Click" ></asp:LinkButton>
                                          <div style="border: 1px solid #3598dc">
                                            <asp:Table ID="Table1" runat="server">
                                                <asp:TableRow ID="TableRow1" runat="server" Style="width: 300px">
                                                    <asp:TableCell ID="TableCell1" runat="server" Style="width: 2000px">  

                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">
                                                        <label class="control-label col-md-2">Bank Code<span class="required"></span></label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtbankcode" OnTextChanged= "txtbankcode_TextChanged" CssClass="form-control" runat="server" placeholder="Bank Code " onkeypress="javascript:return isNumber(event)" AutoPostBack="true"></asp:TextBox>
                                                        </div>
                                                        <label class="control-label col-md-2">Bank Name<span class="required"></span></label>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtbankname" CssClass="form-control" runat="server" placeholder="Bank Name"  Enabled="true"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">
                                                        <label class="control-label col-md-2">Branch Code<span class="required"></span></label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtBrcode" CssClass="form-control" runat="server" placeholder="Branch Code" OnTextChanged="txtBrcode_TextChanged" AutoPostBack="true" onkeypress="javascript:return isNumber (event)" ></asp:TextBox>
                                                        </div>
                                                        <label class="control-label col-md-2">Branch Name<span class="required"></span></label>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="Txtbrname" CssClass="form-control" runat="server" placeholder="Branch Name" OnTextChanged="Txtbrname_TextChanged"  ></asp:TextBox>
                                                            
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-lg-12">
                                                        <label class="control-label col-md-2">Address <span class="required"></span></label>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtadd1" CssClass="form-control" runat="server" placeholder="Addrss 1"></asp:TextBox>
                                                        </div>
                                                        <label class="control-label col-md-2">Address 2 <span class="required"></span></label>
                                                        <div class="col-md-4">
                                                            <asp:TextBox ID="txtadd2" CssClass="form-control" runat="server" placeholder="Address 2" ></asp:TextBox>
                                                    </div>
                                                </div>
                                                    </div>
                                                          <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Mobile No<span class="required"></span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtmobile" MaxLength="10"  CssClass="form-control"   runat="server" placeholder="Mobile No" onkeypress="javascript:return isNumber(event)" ></asp:TextBox>
                                                    </div>
                                                     <label class="control-label col-md-2">E-mail<span class="required"></span></label>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server" placeholder=" E mail Addr"></asp:TextBox>
                                                    </div>
                                                </div>
                                                      </div>
                                                        <div class="row" style="margin: 7px 0 7px 0">
                                                     <div class="col-lg-12">
                                                    <label class="control-label col-md-2">ReGistration No<span class="required"></span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtreg"  CssClass="form-control" runat="server" placeholder=" ReGistration No"  ></asp:TextBox>
                                                    </div>
                                                     <label class="control-label col-md-2">Bank Short Name<span class="required"></span></label>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtbanksname" CssClass="form-control" runat="server" placeholder="Bank Short Name"></asp:TextBox>
                                                    </div>
                                                </div>
                                                </div>
                                                         <div class="row" style="margin: 7px 0 7px 0">
                                                     <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Admin Gl Code<span class="required"></span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtAdmingl"  CssClass="form-control" runat="server" placeholder="Admin Gl Code" onkeypress="javascript:return isNumber(event)" ></asp:TextBox>
                                                    </div>
                                                     <label class="control-label col-md-2">Admin SubGl Code<span class="required"></span></label>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtadminsubgl" CssClass="form-control" runat="server" placeholder="Admin Sub Gl Code"></asp:TextBox>
                                                    </div>
                                                </div>
                                                </div>
                                                          <div class="row" style="margin: 7px 0 7px 0">
                                                     <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Day Open Date<span class="required"></span></label>
                                                    <div class="col-md-2">
                                                        <asp:TextBox ID="txtdayopen"  CssClass="form-control" runat="server" onkeyup="FormatIt(this)"  PlaceHolder="DD/MM/YYYY"  onkeypress="javascript:return isNumber (event)" ></asp:TextBox>
                                                        <asp:CalendarExtender ID="txtdayopen_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtdayopen">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                  
                                                </div>
                                                </div>
                                                  </asp:TableCell>
                                                </asp:TableRow>
                                            </asp:Table>
                                        </div>

                                                <div class="row" style="margin: 7px 0 7px 0">
                                                    <div class="col-md-offset-3 col-md-9">
                                                      

                                                        <asp:Button ID="Submit" runat="server" CssClass="btn btn-primary" Text="" OnClick= "Submit_Click" OnClientClick="Javascript:return isvalidate();" />
                                                        <asp:Button ID="Btn_Clear" runat="server" CssClass="btn btn-primary" Text="Clear All" OnClick= "Btn_Clear_Click" />
                                                        <asp:Button ID="Btn_Exit" runat="server" CssClass="btn btn-primary" Text="Exit" OnClick= "Btn_Exit_Click" />
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
    <div id="alertModal" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <center><h4 class="modal-title" style="color:#ff0000">AVS CORE</h4></center>
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
