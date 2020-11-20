<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmS0003.aspx.cs" Inherits="FrmS0003" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>

        function FormatIt(obj) {
            if (obj.value.length == 2) //DAY
                obj.value = obj.value + "/";
            if (obj.value.length == 5) //MONTH
                obj.value = obj.value + "/";
            if (obj.value.length == 11) //YEAR
                alert("Enter Valid Date!....");
        }
    </script>

      <script>

          function Year(obj) {
              if (obj.value.length == 2) //DAY
                  obj.value = obj.value + "-";
              obj.value = obj.value;
          }
    </script>
    <script>
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && iKeyCode != 13 && (iKeyCode < 48 || iKeyCode > 57))
                return false;
            return true;
        }
    </script>
    <script>

        function dateLen(dt) {
            var dt1 = dt + '';
            if (dt1.length == 1)
                dt1 = '0' + dt;

            return dt1;
        }
    </script>
    <script type="text/javascript">
        function IsValide() {
            var srno = document.getElementById('<%=txtSRo.ClientID%>').value;
             var srname = document.getElementById('<%=TXTSROName.ClientID%>').value;
             var prdcd = document.getElementById('<%=txtCaseNO.ClientID%>').value;
             var brcd = document.getElementById('<%=txtBrcd.ClientID%>').value;
            var accno = document.getElementById('<%=txtCaseY.ClientID%>').value;
            var memno = document.getElementById('<%=txtMemberNo.ClientID%>').value;


             if (prdcd == "") {
                 message = 'Please Enter Case No....!!\n';
                 $('#alertModal').find('.modal-body p').text(message);
                 $('#alertModal').modal('show')
                 $('#<%=txtCaseNO.ClientID %>').focus();
                return false;
            }
            if (brcd == "") {
                message = 'Please Enter Branch Code....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=txtBrcd.ClientID %>').focus();
                return false;
            }
            if (accno == "") {
                message = 'Please Enter  Year....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=txtCaseY.ClientID %>').focus();
                 return false;
             }
             if (srno == "") {
                 message = 'Please Enter Srno Number....!!\n';
                 $('#alertModal').find('.modal-body p').text(message);
                 $('#alertModal').modal('show')
                 $('#<%=txtSRo.ClientID %>').focus();
                return false;
            }
           
            if (memno == "") {
                message = 'Please Enter MemNo ....!!\n';
                $('#alertModal').find('.modal-body p').text(message);
                $('#alertModal').modal('show')
                $('#<%=txtMemberNo.ClientID %>').focus();
                return false;
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-md-12">
        <div class="portlet box green" id="Div1">
            <div class="portlet-title">
                <div class="caption">
                    FILE ASSIGN TO SRO
                </div>
            </div>
            <div class="portlet-body form">
                <div class="form-horizontal">
                    <div class="form-wizard">
                        <div class="form-body">
                            <div class="tab-content">
                                <div class="tab-pane active" id="tab1">
                                    <div class="tab-pane active" id="tab__blue">
                                        <ul class="nav nav-pills">

                                            <li>
                                                <asp:LinkButton ID="lnkAdd" runat="server" Text="a" class="btn btn-default" OnClick="lnkAdd_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-asterisk"></i>Add New</asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton ID="lnkModify" runat="server" Text="VW" class="btn btn-default" OnClick="lnkModify_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-arrows"></i>Modify</asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton ID="lnkDelete" runat="server" Text="MD" class="btn btn-default" OnClick="lnkDelete_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-pencil-square-o"></i>Cancel</asp:LinkButton>
                                            </li>
                                            <li>
                                                <asp:LinkButton ID="lnkAuthorized" runat="server" Text="a" class="btn btn-default" OnClick="lnkAuthorized_Click" Style="border: 1px solid #3561dc; padding: 6px 5px;"><i class="fa fa-arrows"></i>Authorise</asp:LinkButton>
                                            </li>

                                            <li class="pull-right">
                                                <asp:Label ID="lblStst" runat="server" Text="Activity Perform :" Style="font-weight: bold;"></asp:Label>
                                                <asp:Label ID="lblActivity" runat="server" Text=""></asp:Label>
                                            </li>
                                        </ul>
                                    </div>
                                    <div style="border: 1px solid #3598dc">
                                        <div class="row" style="margin-bottom: 10px;">
                                            <div class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Branch Code <span class="required">*</span></label>

                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="txtBrcd" CssClass="form-control" runat="server" onkeypress="javascript:return isNumber (event)" OnTextChanged="txtBrcd_TextChanged" AutoPostBack="true" TabIndex="1"></asp:TextBox>
                                                    </div>
                                                    <div class="col-lg-3">
                                                        <asp:TextBox ID="txtBrcdname" runat="server" CssClass="form-control" TabIndex="2"></asp:TextBox>
                                                    </div>

                                                <label class="control-label col-md-2">Member No<span class="required">*</span></label>
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="txtMemberNo"  CssClass="form-control" runat="server" AutoPostBack="true"   Placeholder="Member No" OnTextChanged="txtMemberNo_TextChanged"   onkeypress="javascript:return isNumber (event)"  TabIndex="3"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtmemname" Enabled="true" Placeholder="Member  Name" AutoPostBack="true"  CssClass="form-control" OnTextChanged="txtmemname_TextChanged" runat="server"></asp:TextBox>
                                                     <div id="CustList2" style="height: 200px; overflow-y: scroll;"></div>
                                                    <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="txtmemname"
                                                        UseContextKey="true"
                                                        CompletionInterval="1"
                                                        CompletionSetCount="20"
                                                        MinimumPrefixLength="1"
                                                        EnableCaching="true"
                                                        ServicePath="~/WebServices/Contact.asmx"
                                                        ServiceMethod="GetMemberName"
                                                        CompletionListElementID="CustList2">
                                                    </asp:AutoCompleteExtender>
                                                        
                                                           </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" style="margin: 7px 0 7px 0" runat="server" id="CUSTNODIV">
                                                <div class="col-lg-12">
                                                     <label class="control-label col-md-2">Case Year <span class="required">*</span></label>
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="txtCaseY" MaxLength="5" CssClass="form-control" runat="server"  Placeholder="YY-YY"  onkeyup="Year(this)" onkeypress="javascript:return isNumber (event)"  TabIndex="3"></asp:TextBox>
                                                    </div>
                                                    <label class="control-label col-md-1">Case No<span class="required">*</span></label>
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="txtCaseNO" CssClass="form-control" OnTextChanged="txtCaseNO_TextChanged" runat="server" TabIndex="5" AutoPostBack="true" onkeypress="javascript:return isNumber (event)"></asp:TextBox>
                                                    </div>
                                                   
                                                    <label class="control-label col-md-3">SRO No <span class="required">*</span></label>
                                                    <div class="col-md-1">
                                                        <asp:TextBox ID="txtSRo" CssClass="form-control" runat="server" TabIndex="7" OnTextChanged="txtSRo_TextChanged" onkeypress="javascript:return isNumber (event)" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TXTSROName" Enabled="false" Placeholder="SrNo  Name" CssClass="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="Div3" runat="server" class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">File Status</label>
                                                    <div class="col-lg-3">
                                                        <%--<asp:DropDownList ID="ddlFileSts" runat="server" CssClass="form-control" AutoPostBack="true" TabIndex="8" ></asp:DropDownList>--%>
                                                        <asp:TextBox ID="txtFilestatus" runat="server" Text="File Assign To SRO" CssClass="form-control" Enabled="true" TabIndex="8"></asp:TextBox>
                                                    </div>
                                                    <div>
                                                    </div>
                                                    <label class="control-label col-md-3">Follow Up Date  </label>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="TxtFdate" CssClass="form-control" runat="server" TabIndex="9" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber(event)" AutoPostBack="true" placeholder="dd/mm/yyyy"></asp:TextBox>
                                                        <asp:TextBoxWatermarkExtender ID="TxtFdateWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="TxtFdate">
                                                        </asp:TextBoxWatermarkExtender>
                                                        <asp:CalendarExtender ID="TxtFdate_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="TxtFdate">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="Div4" runat="server" class="row" style="margin: 7px 0 7px 0">
                                                <div class="col-lg-12">
                                                    <label class="control-label col-md-2">Follow Up Remarks</label>
                                                    <div class="col-lg-3">
                                                        <asp:TextBox ID="TXTREMARKS" runat="server" TabIndex="10" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                    <div>
                                                    </div>
                                                    <label class="control-label col-md-3">Next Follow Up Date  </label>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtnextfile" CssClass="form-control" TabIndex="11" runat="server" onkeyup="FormatIt(this)" onkeypress="javascript:return isNumber(event)" placeholder="dd/mm/yyyy"></asp:TextBox>
                                                        <asp:TextBoxWatermarkExtender ID="txtnextfileWatermarkExtender1" WatermarkText="DD/MM/YYYY" runat="server" Enabled="True" TargetControlID="txtnextfile">
                                                        </asp:TextBoxWatermarkExtender>
                                                        <asp:CalendarExtender ID="txtnextfile_CalendarExtender" runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtnextfile">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="margin: 7px 0 7px 0">
                                    <div class="col-lg-10">
                                        <div class="col-md-4">
                                        </div>
                                        <div class="col-md-6">
                                            <asp:Button ID="BtnSubmit" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="BtnSubmit_Click" TabIndex="12" OnClientClick="Javascript:return IsValide()" />
                                            <asp:Button ID="BtnClear" runat="server" CssClass="btn btn-primary" Text="Clear" OnClick="BtnClear_Click" TabIndex="13" />
                                            <asp:Button ID="BtnExit" runat="server" CssClass="btn btn-primary" Text="Exit" OnClick="BtnExit_Click" TabIndex="14" />
                                        </div>
                                        <div class="col-md-5">
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

