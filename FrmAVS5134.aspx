<%@ Page Title="" Language="C#" MasterPageFile="~/CBSMaster.master" AutoEventWireup="true" CodeFile="FrmAVS5134.aspx.cs" Inherits="FrmAVS5134" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
      <script type="text/javascript">
          function FormatIt(obj) {
              if (obj.value.length == 2) // Day
                  obj.value = obj.value + "/";
              if (obj.value.length == 5) // month 
                  obj.value = obj.value + "/";
              if (obj.value.length == 11) // year 
                  alert("Please Enter valid Date");
          }

        <%-- Only alphabet --%>
          function onlyAlphabets(e, t) {
              try {
                  if (window.event) {
                      var charCode = window.event.keyCode;
                  }
                  else if (e) {
                      var charCode = e.which;
                  }
                  else { return true; }
                  if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123))
                      return true;
                  else
                      return false;
              }
              catch (err) {
                  alert(err.Description);
              }
          }

        <%-- Only Numbers --%>
          function isNumber(evt) {
              var iKeyCode = (evt.which) ? evt.which : evt.keyCode
              if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                  return false;
              return true;
          }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <div class="row">
        <div class="col-md-12">
            <div class="portlet box green">
                <div class="portlet-title">
                    <div class="caption">
                        N.O.C. Certificate
                                <asp:Literal ID="LtrlHeading" runat="server"></asp:Literal>
                    </div>
                    <div class="tools">
                        <a href="javascript:;" class="collapse" data-original-title="" title=""></a>
                        <a href="#portlet-config" data-toggle="modal" class="config" data-original-title="" title=""></a>
                    </div>
                </div>

                <div class="portlet-body">
                   <div id="Depositdiv" runat="server">
                                <div class="row" style="margin: 7px 0 7px 0">
                                    <div class="col-lg-12">
                                        <label class="control-label col-md-2">Product Code: <span class="required">*</span></label>
                                        <div class="col-md-1">
                                            <asp:TextBox ID="txtprdcode" onkeypress="javascript:return isNumber (event)" OnTextChanged="txtprdcode_TextChanged" AutoPostBack="true" placeholder="Code" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtprdname" CssClass="form-control" placeholder="Name" OnTextChanged="txtprdname_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                            <div id="CustList" style="height: 200px; overflow-y: scroll;"></div>
                                            <asp:AutoCompleteExtender ID="autoglname" runat="server" TargetControlID="txtprdname"
                                                UseContextKey="true"
                                                CompletionInterval="1"
                                                CompletionSetCount="20"
                                                MinimumPrefixLength="1"
                                                EnableCaching="true"
                                                ServicePath="~/WebServices/Contact.asmx"
                                                ServiceMethod="getglname" CompletionListElementID="CustList">
                                            </asp:AutoCompleteExtender>
                                        </div>
                                        <label class="control-label col-md-2">Account No: <span class="required">*</span></label>
                                        <div class="col-md-1">
                                            <asp:TextBox ID="txtaccno" onkeypress="javascript:return isNumber (event)" CssClass="form-control" placeholder="No" OnTextChanged="txtaccno_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtaccname" CssClass="form-control" placeholder="Account Name" OnTextChanged="txtaccname_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                            <div id="CustList2" style="height: 200px; overflow-y: scroll;"></div>
                                            <asp:AutoCompleteExtender ID="AutoAccname" runat="server" TargetControlID="txtaccname"
                                                UseContextKey="true"
                                                CompletionInterval="1"
                                                CompletionSetCount="20"
                                                MinimumPrefixLength="1"
                                                EnableCaching="true"
                                                ServicePath="~/WebServices/Contact.asmx"
                                                ServiceMethod="GetAccName" CompletionListElementID="CustList2">
                                            </asp:AutoCompleteExtender>
                                        </div>

                                    </div>
                                </div>
                   
                       <div class="row" style="margin: 7px 0 7px 0">
                                    <div class="col-lg-12">
                                        <label class="control-label col-md-2">Sr No: <span class="required"></span></label>
                                        <div class="col-md-2">
                                            <asp:DropDownList ID="ddlsrno" runat="server" CssClass="form-control">
                                               
                                            </asp:DropDownList>
                                        </div>
                                       

                                    </div>
                                </div>
                <div class="form-actions">
                                    <div class="row">
                                        <div class="col-md-offset-4 col-md-9">
                                            <asp:Button ID="BtnPrint" runat="server" Text="Print Certificate" CssClass="btn btn-success" OnClick="BtnPrint_Click" OnClientClick="javascript:return validate();" />
                                            
                                        </div>
                                    </div>
                                </div>
                   
                        </div>
                    </div>

                  
                </div>
            </div>
        </div>
</asp:Content>

